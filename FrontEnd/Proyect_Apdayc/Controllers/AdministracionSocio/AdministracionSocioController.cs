using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Globalization;

namespace Proyect_Apdayc.Controllers.Administracion
{
    public class AdministracionSocioController : Base
    {
        // GET: AdministracionSocio


        #region VARIABLES
        private const string K_SESION_LISTA_SOCIOS = "___DTOSociosAdministracionLista";
        private const string K_SESION_LISTA_SOCIO_PRINCIPAL = "___DTOSociosAdministracionPrincipal";
        private const string K_SESION_LISTA_SOCIOS_INACTIVAR = "___DTOSociosAdministracionInactivar";
        public class Variables
        {
            public const int Si = 1;
            public const int No = 0;

        }
        #endregion

        #region  TEMPORALES 
        public List<BESocioAdministracion> SociosTemporalTmp
        {
            get
            {
                return (List<BESocioAdministracion>)Session[K_SESION_LISTA_SOCIOS];
            }
            set
            {
                Session[K_SESION_LISTA_SOCIOS] = value;
            }
        }
        public List<BESocioAdministracion> SociosPrincipalTmp
        {
            get
            {
                return (List<BESocioAdministracion>)Session[K_SESION_LISTA_SOCIO_PRINCIPAL];
            }
            set
            {
                Session[K_SESION_LISTA_SOCIO_PRINCIPAL] = value;
            }
        }
        public List<BESocioAdministracion> SociosInactivarTmp
        {
            get
            {
                return (List<BESocioAdministracion>)Session[K_SESION_LISTA_SOCIOS_INACTIVAR];
            }
            set
            {
                Session[K_SESION_LISTA_SOCIOS_INACTIVAR] = value;
            }
        }
        #endregion
        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_LISTA_SOCIOS);
            Session.Remove(K_SESION_LISTA_SOCIO_PRINCIPAL);
            Session.Remove(K_SESION_LISTA_SOCIOS_INACTIVAR);
            return View();
        }
        public ActionResult Create()
        {
            Session.Remove(K_SESION_LISTA_SOCIOS);
            return View();
        }

        public JsonResult ConsultaSociosADmin(decimal BPS_ID, string BPS_NAME, string BPS_FIRST_NAME, string BPS_FATH_SURNAME, string BPS_MOTH_SURNAME, string TAX_ID, string LOG_USER_UPDAT, int CON_FECHA_CREA, string FECHA_INI_CREA, string FECHA_FIN_CREA, int CON_FECHA_UPD, string FECHA_INI_UPD, string FECHA_FIN_UPD, decimal LIC_ID,string EST_NAME)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_LISTA_SOCIOS);

                    var owner = GlobalVars.Global.OWNER;
                    List<BESocioAdministracion> lista = new BLAdministracionSocio().ListaSociosAdministracion(owner, BPS_ID, BPS_NAME, BPS_FIRST_NAME, BPS_FATH_SURNAME, BPS_MOTH_SURNAME, TAX_ID, LOG_USER_UPDAT, CON_FECHA_CREA, FECHA_INI_CREA, FECHA_FIN_CREA, CON_FECHA_UPD, FECHA_INI_UPD, FECHA_FIN_UPD, LIC_ID, EST_NAME);

                    #region  LA LISTA PRINCIPAL NO DEBE CONTENER UN CODIGO REPETIDO QUE SE ENCUNTRA EN PRINCIPAL O A INACTIVAR 
                    var valLicOrigen = 0;
                    if (lista.Count > 0 && lista != null)
                    {
                        if (SociosPrincipalTmp != null)
                        {
                            foreach (var item in lista.OrderBy(x => x.BPS_ID))
                            {
                                valLicOrigen = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();
                                if (valLicOrigen > 0)
                                    lista = lista.Where(z => z.BPS_ID != item.BPS_ID).ToList();
                            }

                        }

                        if (SociosInactivarTmp != null)
                        {
                            foreach (var item in lista.OrderBy(x => x.BPS_ID))
                            {
                                valLicOrigen = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();
                                if (valLicOrigen > 0)
                                    lista = lista.Where(z => z.BPS_ID != item.BPS_ID).ToList();
                            }

                        }


                        SociosTemporalTmp = lista;
                    }
                    #endregion


                    retorno.result = 1;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSociosAdministracion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarSociosAdministracion()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<BESocioAdministracion> lista = SociosTemporalTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicencias' border=0 width='100%;' class='k-grid k-widget' id='tblLicencias'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO SOCIO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.BPS_ID))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input onclick='validaSocioModif(" + item.BPS_ID + ")' type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.BPS_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.BPS_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.TAXN_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL SOCIO DE NEGOCIO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL USUARIO ");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }



            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult SociosSeleccionadosPrincipal(List<BESocioAdministracion> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var item in ReglaValor.OrderBy(x => x.BPS_ID))
                    {
                        int valLicOrigen = 0;
                        int vallICDestino = 0;

                        #region VALIDAR SI EXISTE EL CODIGO EN LAS LISTAS 


                        if (SociosTemporalTmp != null)
                        {
                            valLicOrigen = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }

                        if (SociosPrincipalTmp != null)
                        {
                            vallICDestino = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }
                        #endregion

                        #region ELIMINA DE LISTA ORIGEN Y AGREGA A LISTA DESTINO

                        if (valLicOrigen > 0 && vallICDestino == 0) // si existe en la lista origen  y no en la lista destino
                        {
                            if (SociosPrincipalTmp == null)
                                SociosPrincipalTmp = new List<BESocioAdministracion>();

                            if (SociosPrincipalTmp != null)
                            {
                                SociosPrincipalTmp.Add(new BESocioAdministracion
                                {
                                    BPS_ID = item.BPS_ID,
                                    SOCIO = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().SOCIO,
                                    TAXN_NAME = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAX_ID,
                                    ENDS = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (SociosTemporalTmp != null)
                            {
                                SociosTemporalTmp = SociosTemporalTmp.Where(x => x.BPS_ID != item.BPS_ID).ToList();
                            }


                        }
                        #endregion

                        #region ELIMINA DE LISTA DESTINO Y AGREGA A LISTA ORIGEN

                        if (valLicOrigen == 0 && vallICDestino > 0) // si existe en la lista origen  y no en la lista destino
                        {

                            if (SociosTemporalTmp != null)
                            {
                                SociosTemporalTmp.Add(new BESocioAdministracion
                                {
                                    BPS_ID = item.BPS_ID,
                                    SOCIO = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().SOCIO,
                                    TAXN_NAME = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAX_ID,
                                    ENDS = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = SociosPrincipalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (SociosPrincipalTmp != null)
                            {
                                SociosPrincipalTmp = SociosPrincipalTmp.Where(x => x.BPS_ID != item.BPS_ID).ToList();
                            }



                        }
                        #endregion

                    }


                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarial", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SociosSeleccionadosInactivar(List<BESocioAdministracion> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var item in ReglaValor.OrderBy(x => x.BPS_ID))
                    {
                        int valLicOrigen = 0;
                        int vallICDestino = 0;

                        #region VALIDAR SI EXISTE EL CODIGO EN LAS LISTAS 


                        if (SociosTemporalTmp != null)
                        {
                            valLicOrigen = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }

                        if (SociosInactivarTmp != null)
                        {
                            vallICDestino = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }
                        #endregion

                        #region ELIMINA DE LISTA ORIGEN Y AGREGA A LISTA DESTINO

                        if (valLicOrigen > 0 && vallICDestino == 0) // si existe en la lista origen  y no en la lista destino
                        {
                            if (SociosInactivarTmp == null)
                                SociosInactivarTmp = new List<BESocioAdministracion>();

                            if (SociosInactivarTmp != null)
                            {
                                SociosInactivarTmp.Add(new BESocioAdministracion
                                {
                                    BPS_ID = item.BPS_ID,
                                    SOCIO = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().SOCIO,
                                    TAXN_NAME = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAX_ID,
                                    ENDS = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = SociosTemporalTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (SociosTemporalTmp != null)
                            {
                                SociosTemporalTmp = SociosTemporalTmp.Where(x => x.BPS_ID != item.BPS_ID).ToList();
                            }


                        }
                        #endregion

                        #region ELIMINA DE LISTA DESTINO Y AGREGA A LISTA ORIGEN

                        if (valLicOrigen == 0 && vallICDestino > 0) // si existe en la lista origen  y no en la lista destino
                        {

                            if (SociosTemporalTmp != null)
                            {
                                SociosTemporalTmp.Add(new BESocioAdministracion
                                {
                                    BPS_ID = item.BPS_ID,
                                    SOCIO = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().SOCIO,
                                    TAXN_NAME = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().TAX_ID,
                                    ENDS = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = SociosInactivarTmp.Where(x => x.BPS_ID == item.BPS_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (SociosInactivarTmp != null)
                            {
                                SociosInactivarTmp = SociosInactivarTmp.Where(x => x.BPS_ID != item.BPS_ID).ToList();
                            }



                        }
                        #endregion

                    }


                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarial", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarSociosAdministracionPrincipal()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<BESocioAdministracion> lista = SociosPrincipalTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasPrin' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasPrin'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO SOCIO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.BPS_ID))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstFin" + item.BPS_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstFin'>{0}</td>", item.BPS_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstFin'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.TAXN_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL SOCIO DE NEGOCIO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL USUARIO ");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }



            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult ListarSociosAdministracionInactivarl()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<BESocioAdministracion> lista = SociosInactivarTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasInac' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasInac'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO SOCIO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.BPS_ID))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellIna' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstIna" + item.BPS_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstIna'>{0}</td>", item.BPS_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstIna'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstIna'>{0}</td>", item.TAXN_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstIna'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstIna'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstIna'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstIna'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL SOCIO DE NEGOCIO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstIna'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL USUARIO ");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }



            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult AgruparSocios(int ACTEST, int ACTLIC)
        {
            Resultado retorno = new Resultado();
            int res = 0;
            try
            {
                if ((SociosInactivarTmp.Count > 0 && SociosInactivarTmp != null) && (SociosPrincipalTmp.Count > 0 && SociosPrincipalTmp != null))
                { 

                    var BPS_SOCIO_PRINCIPAL = SociosPrincipalTmp.FirstOrDefault().BPS_ID; // SOCIO PRINCIPAL
                    
                    res=  new BLAdministracionSocio().AgruparSocios(BPS_SOCIO_PRINCIPAL, SociosInactivarTmp,UsuarioActual, ACTEST, ACTLIC); 
                }
                if (res >= Variables.Si)
                    retorno.result = 1;
                else
                    retorno.result = 2;//UNA DE LAS LISTAS FALTA LLENAR

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LimpiarTodo()
        {
            Resultado retorno = new Resultado();
            try
            {

                Session.Remove(K_SESION_LISTA_SOCIOS);
                Session.Remove(K_SESION_LISTA_SOCIO_PRINCIPAL);
                Session.Remove(K_SESION_LISTA_SOCIOS_INACTIVAR);


                retorno.result = 1;
                //retorno.message = "SE";
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult  ValidarSocioModif(decimal BPS_ID)
        {
            Resultado retorno = new Resultado();

            try
            {

                int res = new BLAdministracionSocio().ValidaSocioModif(BPS_ID,UsuarioActual);

                if (res == Variables.Si)

                    retorno.result = 1;
                else if (res == Variables.No)
                {
                    retorno.result = 2;
                    retorno.message = "EL SOCIO HA SIDO MODIFICADO POR OTRO USUARIO DATA QUALITY";
                }

            }catch(Exception ex)
            {
                retorno.result = 0;
            }
            return Json(retorno,JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidaUsuarioMOdif(decimal BPS_ID)
        {
            Resultado retorno = new Resultado();
            try
            {

                retorno.result= new BLAdministracionSocio().ValidaUsuarioModif(UsuarioActual, BPS_ID);
                //retorno.result = 2;
            }
            catch(Exception ex)
            {
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}