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

namespace Proyect_Apdayc.Controllers.AdministracionEstablecimiento
{
    public class AdministracionEstablecimientoController : Base
    {
        // GET: AdministracionEstablecimiento
        #region  Variables
        private const string K_SESION_LISTA_ESTABLECIMIENTOS = "___DTOEstablecimientosAdministracionLista";
        private const string K_SESION_LISTA_ESTABLECIMIENTOS_PRINCIPAL = "___DTOEstablecimientosAdministracionPrincipal";
        private const string K_SESION_LISTA_ESTABLECIMIENTOS_INACTIVAR = "___DTOEstablecimientosAdministracionInactivar";
        private const string K_SESION_LISTA_ESTABLECIMIENTOS_MODIFICAR_SOCIO = "___DTOEstablecimientosAdministracionMoidificarSocio";

        private class Variables
        {
            public const int SI = 1;
            public const int ERROR = 2;
            public const int NO = 0;
            public const string MSJ_OCURRIO_UN_ERROR = "Ocurrio un Error y este ha sido registrado por favor comuniquese con el desarrollador encargado";
            public const string MSJ_TIEMPO_DE_ESPERA = "TIEMPO DE CONEXION TERMINADO , POR FAVOR DE VOLVER A LOGUEARSE";
            public const string MSJ_SOCIO_MODIFICADO = "Se modifico correctamente el socio en los establecimientos Seleccionados .";
        }


        #endregion

        #region  TEMPORALES 
        public List<BEAdministracionEstablecimiento> EstablecimientosTemp
        {
            get
            {
                return (List<BEAdministracionEstablecimiento>)Session[K_SESION_LISTA_ESTABLECIMIENTOS];
            }
            set
            {
                Session[K_SESION_LISTA_ESTABLECIMIENTOS] = value;
            }
        }
        public List<BEAdministracionEstablecimiento> EstablecimientosPrincipalTmp
        {
            get
            {
                return (List<BEAdministracionEstablecimiento>)Session[K_SESION_LISTA_ESTABLECIMIENTOS_PRINCIPAL];
            }
            set
            {
                Session[K_SESION_LISTA_ESTABLECIMIENTOS_PRINCIPAL] = value;
            }
        }
        public List<BEAdministracionEstablecimiento> EstablecimientoInactivarTmp
        {
            get
            {
                return (List<BEAdministracionEstablecimiento>)Session[K_SESION_LISTA_ESTABLECIMIENTOS_INACTIVAR];
            }
            set
            {
                Session[K_SESION_LISTA_ESTABLECIMIENTOS_INACTIVAR] = value;
            }
        }

        public List<BEAdministracionEstablecimiento> EstablecimientoModificarTmp
        {
            get
            {
                return (List<BEAdministracionEstablecimiento>)Session[K_SESION_LISTA_ESTABLECIMIENTOS_MODIFICAR_SOCIO];
            }
            set
            {
                Session[K_SESION_LISTA_ESTABLECIMIENTOS_MODIFICAR_SOCIO] = value;
            }
        }
        #endregion


        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS);
            Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_PRINCIPAL);
            Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_INACTIVAR);
            Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_MODIFICAR_SOCIO);
            return View();
        }


        public JsonResult ConsultaEstablecimientosAdministracion(decimal EST_ID, string BPS_NAME, string BPS_FIRST_NAME, string BPS_FATH_SURNAME, string BPS_MOTH_SURNAME, string TAX_ID, string LOG_USER_UPDAT, int CON_FECHA_CREA, string FECHA_INI_CREA, string FECHA_FIN_CREA, int CON_FECHA_UPD, string FECHA_INI_UPD, string FECHA_FIN_UPD, decimal LIC_ID, string EST_NAME,decimal DIV_ID , decimal DEP_ID ,decimal PROV_ID , decimal DIST_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS);
                    string owner = GlobalVars.Global.OWNER;
                    List<BEAdministracionEstablecimiento> lista = new BLAdministracionEstablecimiento().ListaEstablecimientosAdministracion(owner, EST_ID, BPS_NAME, BPS_FIRST_NAME, BPS_FATH_SURNAME, BPS_MOTH_SURNAME, TAX_ID, LOG_USER_UPDAT, CON_FECHA_CREA, FECHA_INI_CREA, FECHA_FIN_CREA, CON_FECHA_UPD, FECHA_INI_UPD, FECHA_FIN_UPD, LIC_ID, EST_NAME,DIV_ID, DEP_ID, PROV_ID, DIST_ID);
                    #region  LA LISTA PRINCIPAL NO DEBE CONTENER UN CODIGO REPETIDO QUE SE ENCUNTRA EN PRINCIPAL O A INACTIVAR 
                    var valLicOrigen = 0;
                    if (lista.Count > 0 && lista != null)
                    {
                        if (EstablecimientosPrincipalTmp != null)
                        {
                            foreach (var item in lista.OrderBy(x => x.EST_ID))
                            {
                                valLicOrigen = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).Count();
                                if (valLicOrigen > 0)
                                    lista = lista.Where(z => z.EST_ID != item.EST_ID).ToList();
                            }
                        }

                        if (EstablecimientoInactivarTmp != null)
                        {
                            foreach (var item in lista.OrderBy(x => x.EST_ID))
                            {
                                valLicOrigen = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).Count();
                                if (valLicOrigen > 0)
                                    lista = lista.Where(z => z.EST_ID != item.EST_ID).ToList();
                            }

                        }


                        EstablecimientosTemp = lista;
                    }
                    #endregion

                    retorno.result = Variables.SI;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSJ_TIEMPO_DE_ESPERA;
                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSJ_OCURRIO_UN_ERROR;
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListaEstablecimientosAdministracion()
        {
            Resultado retorno = new Resultado();
            try
            {
                List<BEAdministracionEstablecimiento> lista = EstablecimientosTemp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicencias' border=0 width='100%;' class='k-grid k-widget' id='tblLicencias'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO ESTABLECIMIENTO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE DE ESTABLECIMIENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >UBIGEO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR ESTABLECIMIENTO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.EST_ID))
                        {
                            shtml.AppendFormat("<tr id='{0}' style='background-color:white'>",item.EST_ID);

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input onclick='validaSocioModif(" + item.EST_ID + ")' type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.EST_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.EST_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.EST_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.BPS_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.UBIGEO);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td style='width:20%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL ESTABLECIMIENTO");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL USUARIO ");


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
                    retorno.result = Variables.SI;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSJ_TIEMPO_DE_ESPERA;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSJ_OCURRIO_UN_ERROR;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        public JsonResult EstablecimientosSeleccionadosPrincipal(List<BEAdministracionEstablecimiento> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var item in ReglaValor.OrderBy(x => x.EST_ID))
                    {
                        int valLicOrigen = 0;
                        int vallICDestino = 0;

                        #region VALIDAR SI EXISTE EL CODIGO EN LAS LISTAS 


                        if (EstablecimientosTemp != null)
                        {
                            valLicOrigen = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }

                        if (EstablecimientosPrincipalTmp != null)
                        {
                            vallICDestino = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }
                        #endregion

                        #region ELIMINA DE LISTA ORIGEN Y AGREGA A LISTA DESTINO

                        if (valLicOrigen > 0 && vallICDestino == 0) // si existe en la lista origen  y no en la lista destino
                        {
                            if (EstablecimientosPrincipalTmp == null)
                                EstablecimientosPrincipalTmp = new List<BEAdministracionEstablecimiento>();

                            if (EstablecimientosPrincipalTmp != null)
                            {
                                EstablecimientosPrincipalTmp.Add(new BEAdministracionEstablecimiento
                                {
                                    EST_ID = item.EST_ID,
                                    BPS_NAME = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().BPS_NAME,
                                    TAXN_NAME = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAX_ID,
                                    ENDS = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (EstablecimientosTemp != null)
                            {
                                EstablecimientosTemp = EstablecimientosTemp.Where(x => x.EST_ID != item.EST_ID).ToList();
                            }


                        }
                        #endregion

                        #region ELIMINA DE LISTA DESTINO Y AGREGA A LISTA ORIGEN

                        if (valLicOrigen == 0 && vallICDestino > 0) // si existe en la lista origen  y no en la lista destino
                        {

                            if (EstablecimientosTemp != null)
                            {
                                EstablecimientosTemp.Add(new BEAdministracionEstablecimiento
                                {
                                    EST_ID = item.EST_ID,
                                    BPS_NAME = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().BPS_NAME,
                                    TAXN_NAME = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAX_ID,
                                    ENDS = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = EstablecimientosPrincipalTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (EstablecimientosPrincipalTmp != null)
                            {
                                EstablecimientosPrincipalTmp = EstablecimientosPrincipalTmp.Where(x => x.EST_ID != item.EST_ID).ToList();
                            }



                        }
                        #endregion

                    }


                }
                retorno.result = Variables.SI;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoADMINISTRACION", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarEstablecimientoAdministracionPrincipal()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<BEAdministracionEstablecimiento> lista = EstablecimientosPrincipalTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasPrin' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasPrin'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO ESTABLECIMIENTO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE DE ESTABLECIMIENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR SOCIO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.EST_ID))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstFin" + item.EST_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstFin'>{0}</td>", item.EST_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstFin'>{0}</td>", item.EST_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.BPS_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL SOCIO DE NEGOCIO");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL USUARIO ");


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
                    retorno.result = Variables.SI;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSJ_TIEMPO_DE_ESPERA;
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


        public JsonResult EstablecimientoSeleccionadosInactivar(List<BEAdministracionEstablecimiento> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var item in ReglaValor.OrderBy(x => x.EST_ID))
                    {
                        int valLicOrigen = 0;
                        int vallICDestino = 0;

                        #region VALIDAR SI EXISTE EL CODIGO EN LAS LISTAS 


                        if (EstablecimientosTemp != null)
                        {
                            valLicOrigen = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }

                        if (EstablecimientoInactivarTmp != null)
                        {
                            vallICDestino = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }
                        #endregion

                        #region ELIMINA DE LISTA ORIGEN Y AGREGA A LISTA DESTINO

                        if (valLicOrigen > 0 && vallICDestino == 0) // si existe en la lista origen  y no en la lista destino
                        {
                            if (EstablecimientoInactivarTmp == null)
                                EstablecimientoInactivarTmp = new List<BEAdministracionEstablecimiento>();

                            if (EstablecimientoInactivarTmp != null)
                            {
                                EstablecimientoInactivarTmp.Add(new BEAdministracionEstablecimiento
                                {
                                    EST_ID = item.EST_ID,
                                    EST_NAME = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().EST_NAME,
                                    TAXN_NAME = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAX_ID,
                                    ENDS = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = EstablecimientosTemp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (EstablecimientosTemp != null)
                            {
                                EstablecimientosTemp = EstablecimientosTemp.Where(x => x.EST_ID != item.EST_ID).ToList();
                            }


                        }
                        #endregion

                        #region ELIMINA DE LISTA DESTINO Y AGREGA A LISTA ORIGEN

                        if (valLicOrigen == 0 && vallICDestino > 0) // si existe en la lista origen  y no en la lista destino
                        {

                            if (EstablecimientosTemp != null)
                            {
                                EstablecimientosTemp.Add(new BEAdministracionEstablecimiento
                                {
                                    EST_ID = item.EST_ID,
                                    EST_NAME = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().EST_NAME,
                                    TAXN_NAME = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAXN_NAME,
                                    TAX_ID = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().TAX_ID,
                                    ENDS = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().ENDS,
                                    LOG_USER_UPDAT = EstablecimientoInactivarTmp.Where(x => x.EST_ID == item.EST_ID).FirstOrDefault().LOG_USER_UPDAT
                                });
                            }

                            if (EstablecimientoInactivarTmp != null)
                            {
                                EstablecimientoInactivarTmp = EstablecimientoInactivarTmp.Where(x => x.EST_ID != item.EST_ID).ToList();
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


        public JsonResult ListarEstablecimientoAdministracionInactivar()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<BEAdministracionEstablecimiento> lista = EstablecimientoInactivarTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasInac' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasInac'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO ESTABLECIMIENTO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE DE ESTABLECIMIENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO IDENTIDAD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO MODIFICACION</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR SOCIO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.EST_ID))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellIna' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstIna" + item.EST_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstIna'>{0}</td>", item.EST_ID);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstIna'>{0}</td>", item.BPS_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.BPS_NAME);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL SOCIO DE NEGOCIO");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL USUARIO ");


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
                    retorno.result = Variables.SI;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSJ_TIEMPO_DE_ESPERA;
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

        public JsonResult AgruparEstablecimientos(int ACTEST, int ACTLIC)
        {
            Resultado retorno = new Resultado();
            int res = 0;
            try
            {
                if ((EstablecimientoInactivarTmp.Count > 0 && EstablecimientoInactivarTmp != null) && (EstablecimientosPrincipalTmp.Count > 0 && EstablecimientosPrincipalTmp != null))
                {

                    var BPS_EST_PRINCIPAL = EstablecimientosPrincipalTmp.FirstOrDefault().EST_ID; // EST PRINCIPAL

                    res = new BLAdministracionEstablecimiento().AgruparEstablecimientos(BPS_EST_PRINCIPAL, EstablecimientoInactivarTmp, UsuarioActual, ACTEST, ACTLIC);
                }
                if (res >= Variables.SI)
                    retorno.result = Variables.SI;
                else
                    retorno.result = Variables.ERROR;//UNA DE LAS LISTAS FALTA LLENAR

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

                Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS);
                Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_PRINCIPAL);
                Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_INACTIVAR);


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

        public JsonResult ValidaEstablecimientooMOdif(decimal EST_ID)
        {
            Resultado retorno = new Resultado();
            try
            {

                retorno.result = new BLAdministracionEstablecimiento().ValidaEstablecimientoModif(UsuarioActual, EST_ID);

            }
            catch (Exception ex)
            {
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtieneEstablecimientosporSocioSeleccionado(List<BEAdministracionEstablecimiento> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                Session.Remove(K_SESION_LISTA_ESTABLECIMIENTOS_MODIFICAR_SOCIO);

                EstablecimientoModificarTmp = ReglaValor;

                retorno.result = 1;//new BLAdministracionEstablecimiento().ModificaEstablecimientosporSocioSeleccionado(ReglaValor, UsuarioActual, BPS_ID);
                //retorno.message = Variables.MSJ_SOCIO_MODIFICADO;
            }
            catch (Exception ex)
            {
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ModificaEstablecimientosporSocioSeleccionado(decimal BPS_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (EstablecimientoModificarTmp != null && EstablecimientoModificarTmp.Count > 0)
                { 
                    retorno.result = new BLAdministracionEstablecimiento().ModificaEstablecimientosporSocioSeleccionado(EstablecimientoModificarTmp, UsuarioActual, BPS_ID);
                    retorno.message = Variables.MSJ_SOCIO_MODIFICADO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}