using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using SGRDA.BL.Empadronamiento;
using SGRDA.BL.Reporte;
using SGRDA.Entities.Empadronamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Empadronamiento
{
    public class MatrizEmpadronamientoController : Base
    {
        // GET: MatrizEmpadronamiento
        private const string K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO = "___K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO";

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO);
            Init(false);
            return View();
        }
        private List<BEMatrizEmpadronamiento> ListaReporteDeEmpadronamientoTmp
        {
            get
            {
                return (List<BEMatrizEmpadronamiento>)Session[K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO];
            }
            set
            {
                Session[K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO] = value;
            }
        }

        private static List<BEMatrizEmpadronamiento> ListaReporteDeEmpadronamientoTODOSTmp;
 
        public List<BEMatrizEmpadronamiento> USP_MatrizEmpadronamiento_LISTARPAGE(string anio, string mes, int ID_OFICINA, string FLAG_PAGO, int LIC_ID, int pagina, int cantRegxPag)
        {
            Session.Remove(K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO);
            Resultado retorno = new Resultado();
            if (ID_OFICINA == 1)
            {
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            int oficina_loguea = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_loguea = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            List<BEMatrizEmpadronamiento> listar = new List<BEMatrizEmpadronamiento>();

            //Recupera el codigo de la oficina por la constante

            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                listar = new BLMatrizEmpadronamiento().ObtenerLista_Matriz_EMPADRONAMIENTO(anio, mes, ID_OFICINA, oficina_loguea, FLAG_PAGO, LIC_ID, pagina, cantRegxPag);
                ListaReporteDeEmpadronamientoTODOSTmp = new BLMatrizEmpadronamiento().ObtenerLista_Matriz_EMPADRONAMIENTO(anio, mes, ID_OFICINA, oficina_loguea, FLAG_PAGO, LIC_ID, pagina, 999999999);
                ListaReporteDeEmpadronamientoTmp = new List<BEMatrizEmpadronamiento>();
                ListaReporteDeEmpadronamientoTmp = listar;
                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte De Empadronamiento", ex);
            }
            return listar;
        }
        public JsonResult USP_MatrizEmpadronamiento_LISTARPAGEJSON(int skip, int take, int page, int pageSize, string anio, string mes, int ID_OFICINA, string TIPO_PAGO, int LIC_ID)
        {
            Resultado retorno = new Resultado();
            List<BEMatrizEmpadronamiento> lista = new List<BEMatrizEmpadronamiento>();

            try
            {
                //DateTime finiautodate = Convert.ToDateTime(finiauto);
                //DateTime ffinautodate = Convert.ToDateTime(ffinauto);
                lista = USP_MatrizEmpadronamiento_LISTARPAGE(anio, mes, ID_OFICINA, TIPO_PAGO, LIC_ID, page, pageSize);

            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR Licencia", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEMatrizEmpadronamiento { ListaMatrizEmpadronamiento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEMatrizEmpadronamiento { ListaMatrizEmpadronamiento = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarAnios()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLMatrizEmpadronamiento().ListarAnios().Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ACCOUNTANT_YEAR),
                        Text = Convert.ToString(c.ACCOUNTANT_YEAR)
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAniosCierre", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarMeses(int anio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLMatrizEmpadronamiento().ListarMeses(anio).Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ACCOUNTANT_MONTH),
                        Text = Convert.ToString(c.NOMBRE_MES)
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAniosCierre", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        //--------XXXXXXXXXXXXXXXXXXX Popup Tabla de Comisiones

        public JsonResult Listar_TablaComision()
        {
            Resultado retorno = new Resultado();
            //Creat una nueva planificacion vacia
            var Tabla = new BLMatrizEmpadronamiento().Lista_Tabla_Comision();
            StringBuilder shtml = new StringBuilder();
            try
            {


                shtml.Append("<table class='tblComision' border=0 width='100%;' class='k-grid k-widget' id='tblComision'>");
                shtml.Append("<thead><tr>");

                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");

                //shtml.Append("</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Rango</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Monto Desde</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Monto Hasta</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Porcentaje</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >User Creacion</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha Creacion</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Eliminar</th>");

                if (Tabla != null)
                {
                    //foreach (var item in ListarConsultaDocumento.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    Tabla.ForEach(item =>
                        {

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("</td>");

                            shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px'  class='IDCell' >{0}</td>", item.ID_COMISION);
                            switch (item.ID_RANGO)
                            {
                                case 1: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font></td>", "A"); break;
                                case 2: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", "B"); break;
                                case 3: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", "C"); break;
                                case 4: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font></td>", "D"); break;

                            }
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.MONTO_DESDE); //TIPO DOC
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.MONTO_HASTA); // SERIE
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.PORCENTAJE+"%"); // NUMERO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.LOG_USER_CREATE); // Usuario Creacion
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.Fecha_Creacion));// FECHA EMISION
                            shtml.AppendFormat("<td>");

                            shtml.AppendFormat("<a href=# onclick='eliminarRangoComision({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.ID_COMISION, "Anular Rango Comision");

                            shtml.AppendFormat("</td>");


                            shtml.Append("</tr>");
                            shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                        });
                }
                shtml.Append("</table>");

                retorno.message = shtml.ToString();
                retorno.TotalFacturas = Tabla.Count;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                shtml = null;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult ComboRangoComision()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    
                    var datos = new BLMatrizEmpadronamiento().Listar_Combo_RangoComision().Where(c => c.ID_RANGO!=0).Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ID_RANGO),
                        Text = Convert.ToString(c.Desc_Rango)
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertarRangoComision(int idRango,decimal monto_desde,decimal monto_hasta,decimal porcentaje)
        {
            Resultado retorno = new Resultado();
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            try
            {
                var datos = new BLMatrizEmpadronamiento().Insertar_RangoComision(idRango, monto_desde, monto_hasta, porcentaje, usuario);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EliminarRangoComision(int id)
        {
            Resultado retorno = new Resultado();
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            try
            {
                var datos = new BLMatrizEmpadronamiento().Delete_RangoComision(id, usuario);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
            }
            catch
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult InsertarTipoRango()
        {
            Resultado retorno = new Resultado();
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            try
            {
                var datos = new BLMatrizEmpadronamiento().Insertar_TipoRango(usuario);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Desactivar_TipoRango()
        {
            Resultado retorno = new Resultado();
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            try
            {
                var datos = new BLMatrizEmpadronamiento().Desactivar_TipoRango(usuario);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteTipo(string anio, string mes, int ID_OFICINA, int LIC_ID)
        {
            //Session.Remove(K_SESSION_MATRIZ_DE_EMPRADRONAMIENTO);
            Resultado retorno = new Resultado();
            int oficina_loguea = 0;
            oficina_loguea = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            if (ListaReporteDeEmpadronamientoTODOSTmp == null)
            {
                ListaReporteDeEmpadronamientoTODOSTmp = new BLMatrizEmpadronamiento().ObtenerLista_Matriz_EMPADRONAMIENTO(anio, mes, ID_OFICINA, oficina_loguea, "-1", LIC_ID, 1, 999999999);
            }

            try
            {
                if (ListaReporteDeEmpadronamientoTODOSTmp.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte De Empadronamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReporteDeEmpadronamiento(string MES, int ANIO, string formato, int ID_OFICINA, string nombreoficina)
        {
            string format = formato;
            int oficina_id = 0;
            if (nombreoficina.Equals("--SELECCIONE--"))
            {
                nombreoficina = ("TODAS LAS OFICINAS");
            }
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();
            if (ID_OFICINA == 1)
            {
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            if (opcAdm == 1)
            {
                oficina = nombreoficina;
            }
            //else
            //    ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {

                LocalReport localReport = new LocalReport();
                if (formato == "PDF")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DE_MATRIZ_EMPADRONAMIENTO.rdlc");

                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DE_MATRIZ_EMPADRONAMIENTO.rdlc");
                }
                List<BEMatrizEmpadronamiento> lstReporte = new List<BEMatrizEmpadronamiento>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteDeEmpadronamientoTODOSTmp;
                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroFecha1 = new ReportParameter();
                    parametroFecha1 = new ReportParameter("Anio", ANIO.ToString());
                    localReport.SetParameters(parametroFecha1);

                    ReportParameter parametroFecha2 = new ReportParameter();
                    parametroFecha2 = new ReportParameter("Mes", MES);
                    localReport.SetParameters(parametroFecha2);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);

                    string reportType = format;
                    string mimeType;
                    string encoding;

                    //aqui le cambie solo dejar string fileNameExtension en caso de error
                    string fileNameExtension;

                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>11in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>16.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Matriz Empadronamiento";

                    if (format == null)
                    {
                        return File(renderedBytes, "image/jpeg");
                    }

                    else if (format == "PDF")
                    {
                        return File(renderedBytes, mimeType);

                    }
                    else if (format == "EXCEL")
                    {
                        return File(renderedBytes, mimeType);
                    }

                    else
                    {
                        return File(renderedBytes, "image/jpeg");
                    }
                }
                else
                {
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Empadronamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}