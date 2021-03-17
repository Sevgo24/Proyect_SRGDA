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
    public class TrasladarLicenciaDivisionController : Base
    {
        #region VARIABLES SESION

        private const string K_SESION_LICENCIA_TRASLADO = "___dTOPLicenciaTraslado_LIC";
        private const string K_SESION_LICENCIA_TRASLADO_DEST = "___dTOPLicenciaTrasladoDest_LIC";
        private const string K_SESION_LICENCIA_SELECCIONADAS = "___DTOLicenciasSeleccionadas";
        private const string K_SESION_AGENTES_SELECCIONADOS = "___DTOAgentesSeleccionados";
        #endregion
        List<DTOLicencia> ListaLicencias = new List<DTOLicencia>();

        // GET: TrasladarLicenciaDivision
        public class Variables
        {
            public const int Si = 1;
            public const int No = 0;

        }
        List<BELicencias> LicenciasaModificar;

        #region TEMPORALES 
        public List<DTOLicencia> licenciaTemporalTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_TRASLADO];
            }
            set
            {
                Session[K_SESION_LICENCIA_TRASLADO] = value;
            }
        }

        public List<DTOLicencia> LicenciaTemporalDestinoTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_TRASLADO_DEST];
            }
            set
            {
                Session[K_SESION_LICENCIA_TRASLADO_DEST] = value;
            }
        }

        public List<BELicencias> LicenciaTemporalSeleccionadas
        {
            get
            {
                return (List<BELicencias>)Session[K_SESION_LICENCIA_SELECCIONADAS];
            }
            set
            {
                Session[K_SESION_LICENCIA_SELECCIONADAS] = value;
            }
        }

        public List<BELicenciaDivisionAgente> AgentesTemporalSeleccionados
        {
            get
            {
                return (List<BELicenciaDivisionAgente>)Session[K_SESION_AGENTES_SELECCIONADOS];
            }
            set
            {
                Session[K_SESION_AGENTES_SELECCIONADOS] = value;
            }
        }
        #endregion


        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_LICENCIA_TRASLADO);
            Session.Remove(K_SESION_LICENCIA_TRASLADO_DEST);
            Session.Remove(K_SESION_LICENCIA_SELECCIONADAS);
            Session.Remove(K_SESION_AGENTES_SELECCIONADOS);
            return View();
        }

        public ActionResult Create()
        {
            Session.Remove(K_SESION_LICENCIA_TRASLADO);
            Session.Remove(K_SESION_LICENCIA_TRASLADO_DEST);
            Session.Remove(K_SESION_LICENCIA_SELECCIONADAS);

            return View();
        }

        #region LICENCIAS A TRASLADAR 

        public JsonResult ConsultaLiencenciasTrasladar(Decimal BPS_ID,decimal LIC_ID, string NOM_LIC, decimal LIC_MASTER,decimal ID_GROUP,decimal OFF_ID ,decimal DIV1, decimal DIV2, decimal DIV3 ,decimal DIVISION ,string MOD_GROUP,decimal AGE_ID,int CON_FECHA_CREACION, string FECHA_CREA_INICIAL,string FECHA_CREA_FINAL)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_LICENCIA_TRASLADO);



                    List<BELicencias> lista = new BLAdministracion().ListaLicenciasTrasladar(BPS_ID, LIC_ID, NOM_LIC, LIC_MASTER, ID_GROUP, OFF_ID, DIV1, DIV2, DIV3, DIVISION, MOD_GROUP, AGE_ID,CON_FECHA_CREACION, FECHA_CREA_INICIAL, FECHA_CREA_FINAL);


                    if (lista != null)
                    {
                        ListaLicencias = new List<DTOLicencia>();
                        lista.ForEach(s =>
                        {
                          int valEst = 0; //  
                          if (LicenciaTemporalDestinoTmp != null)
                                valEst = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == s.LIC_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN

                            if (valEst == 0) // si no existe en la lista destino pues listar
                            {
                                ListaLicencias.Add(new DTOLicencia
                                {
                                    codLicencia = s.LIC_ID,
                                    nombreLicencia = s.LIC_NAME,
                                    DIVISION=s.DIVISION,
                                    OFICINA=s.OFICINA
                                    //DIVISION= s.DIS
                                });
                            }
                        });
                        licenciaTemporalTmp = ListaLicencias;


                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(ListaLicencias, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarLicenciasTrasladoOrigen()
        {
            Resultado retorno = new Resultado();

            try
            {
                List<DTOLicencia> lista = licenciaTemporalTmp;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicencias' border=0 width='100%;' class='k-grid k-widget' id='tblLicencias'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'><input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheckTraslado()'></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO LICENCIA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE LICENCIA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIVISION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.codLicencia))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:right'; class='IDEstOri'>{0}</td>", item.codLicencia);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.nombreLicencia);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDDivEstOri'>{0}</td>", item.DIVISION);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.OFICINA);
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

        public JsonResult ListarLicenciasTrasladoDestino()
        {
            Resultado retorno = new Resultado();

            try
            {

                List<DTOLicencia> lista = LicenciaTemporalDestinoTmp;
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasFin' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasFin'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'><input type='checkbox' id='idCheckd' name='Check' class='Checkd' onchange='clickCheckDestino()'></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >X</th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO LICENCIA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE LICENCIA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIVISION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.codLicencia))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellFin' ><input type='checkbox' id='{0}' name='Check' class='Checkd' />", "chkEstFin" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:right'; class='IDEstFin'>{0}</td>", item.codLicencia);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left';'class='IDNomEstFin'>{0}</td>", item.nombreLicencia);
                            shtml.AppendFormat("<td style='width:25%;cursor:pointer;text-align:left'; class='IDDivEstFin'>{0}</td>", item.DIVISION);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstFin'>{0}</td>", item.OFICINA);
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

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LicenciasSeleccionadas(List<DTOLicencia> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var item in ReglaValor.OrderBy(x => x.codLicencia))
                    {
                        int valLicOrigen = 0;
                        int vallICDestino = 0;

                        #region VALIDAR SI EXISTE EL CODIGO EN LAS LISTAS 

                 
                        if (licenciaTemporalTmp != null)
                        {
                            valLicOrigen = licenciaTemporalTmp.Where(x => x.codLicencia == item.codLicencia).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }

                        if (LicenciaTemporalDestinoTmp != null)
                        {
                            vallICDestino = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == item.codLicencia).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN
                        }
                        #endregion

                        #region ELIMINA DE LISTA ORIGEN Y AGREGA A LISTA DESTINO

                        if (valLicOrigen > 0 && vallICDestino == 0) // si existe en la lista origen  y no en la lista destino
                        {
                            if (LicenciaTemporalDestinoTmp == null)
                                LicenciaTemporalDestinoTmp = new List<DTOLicencia>();

                            if (LicenciaTemporalDestinoTmp != null)
                            {
                                LicenciaTemporalDestinoTmp.Add(new DTOLicencia
                                {
                                    codLicencia = item.codLicencia,
                                    nombreLicencia = licenciaTemporalTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().nombreLicencia,
                                    DIVISION = licenciaTemporalTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().DIVISION,
                                    OFICINA = licenciaTemporalTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().OFICINA
                                });
                            }

                            if (licenciaTemporalTmp != null)
                            {
                                licenciaTemporalTmp = licenciaTemporalTmp.Where(x => x.codLicencia != item.codLicencia).ToList();
                            }


                        }
                        #endregion

                        #region ELIMINA DE LISTA DESTINO Y AGREGA A LISTA ORIGEN

                        if (valLicOrigen == 0 && vallICDestino > 0) // si existe en la lista origen  y no en la lista destino
                        {

                            if (licenciaTemporalTmp != null)
                            {
                                licenciaTemporalTmp.Add(new DTOLicencia
                                {
                                    codLicencia = item.codLicencia,
                                    nombreLicencia = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().nombreLicencia,
                                    DIVISION = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().DIVISION,
                                    OFICINA = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == item.codLicencia).FirstOrDefault().OFICINA
                                });
                            }

                            if (LicenciaTemporalDestinoTmp != null)
                            {
                                LicenciaTemporalDestinoTmp = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia != item.codLicencia).ToList();
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


        public JsonResult RecuperaLicenciasTrasladar(List<DTOLicencia> ReglaValorL)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    LicenciaTemporalSeleccionadas = new List<BELicencias>();

                    ReglaValorL.ForEach(s =>
                    {
                        LicenciaTemporalSeleccionadas.Add( new BELicencias
                        {
                           LIC_ID =s.codLicencia
                       });
                    });

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecuperaAgentesTrasladar(List<DTOAgenteRecaudo> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    AgentesTemporalSeleccionados = new List<BELicenciaDivisionAgente>();

                    ReglaValor.ForEach(s =>
                    {
                        LicenciaTemporalSeleccionadas.ForEach(x=>{ // agregado
                                AgentesTemporalSeleccionados.Add(new BELicenciaDivisionAgente
                                {
                                    ID = s.Codigo,
                                    LIC_ID=x.LIC_ID
                                });
                        });// agregado
                    });

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult ActualizarLicenciasDivision(decimal DIV_ID , int FACPEND, int FACHISTO)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BELicencias> LISTA = LicenciaTemporalSeleccionadas; //recuperando el temporal en una lista para enviar al store
                    List<BELicenciaDivisionAgente> LISTA_AGENTE = AgentesTemporalSeleccionados;
                    
                    int r = new BLLicencias().ActualizaLicenciasDivision(LISTA, DIV_ID,UsuarioActual, LISTA_AGENTE);
                    int r2 = new BLAdministracion().AtualizarFactPendiCancelado(LISTA, LISTA_AGENTE, FACPEND, FACHISTO); 
                    //if (FACPEND>0) true;

                    if (r == 1 && r2 ==1)//PASO
                    {
                        retorno.result = 1;
                        LicenciaTemporalSeleccionadas = null;
                        LicenciaTemporalDestinoTmp = null;
                        AgentesTemporalSeleccionados = null;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "LOS AGENTES SELECCIONADOS PERTENECEN A DIFERENTES OFICINAS";
                        LicenciaTemporalSeleccionadas = null;
                        LicenciaTemporalDestinoTmp = null;
                        AgentesTemporalSeleccionados = null;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}