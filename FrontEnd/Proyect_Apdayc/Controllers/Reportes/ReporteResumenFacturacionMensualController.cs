using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;
using System.Globalization;
using SGRDA.Entities.Reporte;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteResumenFacturacionMensualController : Base
    {
        // GET: ReporteResumenFacturacionMensual
        private const string K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL = "___K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL";
        List<BEFacturaCancelada> listarModSeg = new List<BEFacturaCancelada>();
        List<BEFacturaCancelada> ConModalidadXOficina = new List<BEFacturaCancelada>();
        List<BESelectListItem> ListaTipoDoc = new List<BESelectListItem>();
        List<BESelectListItem> ListaEstado= new List<BESelectListItem>();
        public static string K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO = "__ModalidadXOficinaDestinoTmp";
        public static string K_SESION_CONSULTA_MODALIDAD_OFICINA = "__ConsultaModalidadXOficinaTmp";
        public static string K_SESION_LISTA_TIPO_DOC = "__ListaTipoDoc";
        public static string K_SESION_LISTA_ESTADO = "__K_SESION_ListaEstadoTMP"; 
        public static string parametrosRubro;
        public static string parametrosTipoDoc;
        public static string parametrosEstado;
        public static string DESC_Modalidad;
        public static string DESC_TipoDoc;
        public static string DESC_Estado;
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL);
            Init(false);
            return View();
        }
        public List<BEReporteResumenFacturacionMensual> listaReporteResumenFacturacionMensualTMP
        {
            get
            {
                return (List<BEReporteResumenFacturacionMensual>)Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO];
            }
            set
            {
                Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO] = value;
            }
        }
        #region ModalidadTmp
        public List<BEFacturaCancelada> ModalidadXOficinaDestinoTmp
        {
            get
            {
                return (List<BEFacturaCancelada>)Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO];
            }
            set
            {
                Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO] = value;
            }
        }
        public List<BEFacturaCancelada> ConsultaModalidadXOficinaTmp
        {
            get
            {
                return (List<BEFacturaCancelada>)Session[K_SESION_CONSULTA_MODALIDAD_OFICINA];
            }
            set
            {
                Session[K_SESION_CONSULTA_MODALIDAD_OFICINA] = value;
            }
        }
        public List<BESelectListItem> ListaTipoDocTMP
        {
            get
            {
                return (List<BESelectListItem>)Session[K_SESION_LISTA_TIPO_DOC];
            }
            set
            {
                Session[K_SESION_LISTA_TIPO_DOC] = value;
            }
        }

        public List<BESelectListItem> ListaEstadoTMP
        {
            get
            {
                return (List<BESelectListItem>)Session[K_SESION_LISTA_ESTADO];
            }
            set
            {
                Session[K_SESION_LISTA_ESTADO] = value;
            }
        }
        #endregion
        #region Modalidad
        public JsonResult ConsultaModalidadXOficina(int IdOficina)
        {
            parametrosRubro = null;
            if (IdOficina == 1)
            {
                IdOficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //Session.Remove(K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL);
                    var ModalidadLicOrigen = new BLReporteFacturaCancelada().ListarGrupoModXOficina(IdOficina);
                    if (ModalidadLicOrigen != null)
                    {
                        ConsultaModalidadXOficinaTmp = ModalidadLicOrigen;
                    }
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ModalidadLicOrigen, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaModalidadXOficina", ex);
            }
            try
            {
                StringBuilder shtml = new StringBuilder();

                if (ConsultaModalidadXOficinaTmp.Count() > 0)
                {
                    string TODOS = "TODOS";
                    shtml.Append("<table class='tblModalidadXOficina' border=0 class='k-grid k-widget' id='tblModalidadXOficina'>");
                    shtml.Append("<thead><tr>");
                    shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;text-align:left-center;width=2.5px';><input type='checkbox' id='checkALL' name='Check' value='" + '0' + "' class='select-all' onclick='check1();' checked /> &nbsp;&nbsp;" + TODOS + " &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;</td>");

                }
                if (ConsultaModalidadXOficinaTmp != null)
                {
                    int Contador = 0;
                    foreach (var item in ConsultaModalidadXOficinaTmp.OrderBy(x => x.MOG_ID))
                    {
                        Contador = Contador + 1;
                        shtml.AppendFormat("<td align='left-center' style='background-color:white;cursor:pointer;text-align:left-center; width:auto;padding-right:15px; ';><input type='checkbox'  id='checkValue" + Contador + "'name='Check' value='" + item.MOG_ID + "' class='Check' onclick='checkValue();' checked />&nbsp;&nbsp;" + item.MOG_DESC + "</td>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModalidadesSeleccionadasTemporalesOriginal(List<BEFacturaCancelada> ReglaValor)
        {
            string para = " ";
            string Mod = "";
            string parametro = " ";
            int count = 0;
            int count2 = 0;
            DESC_Modalidad = null;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        listarModSeg = ReglaValor;
                    }
                    else
                    {

                        listarModSeg = new List<BEFacturaCancelada>();
                    }
                    //Lista que recupera el Mog_ID
                    foreach (var item in listarModSeg.OrderBy(x => x.MOG_ID))
                    {
                        para = item.MOG_ID;
                        count += 1;
                        if (count > 1)
                        {
                            parametro = parametro + "," + para;
                        }
                        else
                        {
                            parametro = para;

                        }
                    };

                    // Recupera la descripcion de las modalidades seleccionadas
                    foreach (var item in listarModSeg.OrderBy(x => x.MOG_ID))
                    {
                        foreach (var item2 in ConsultaModalidadXOficinaTmp.Where(x => x.MOG_ID == item.MOG_ID))
                        {
                            Mod = item2.MOG_DESC;
                            count2 += 1;
                            if (count2 > 1)
                            {
                                DESC_Modalidad = DESC_Modalidad + " - " + Mod;
                            }
                            else
                            {
                                DESC_Modalidad = Mod;
                            }
                        };

                    }
                    parametrosRubro = parametro;
                    if (parametrosRubro == " ")
                    {
                        parametrosRubro = null;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ConsultaModalidadXOficinaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ConsultaModalidadXOficina", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Tipo Doc y Estado
        public JsonResult ListaTipoDocumentoRadioButton()
        {
            parametrosTipoDoc = null;
            ListaTipoDocTMP = new List<BESelectListItem>();
            List<BESelectListItem> lista = new List<BESelectListItem>();
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //Session.Remove(K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL);

                    #region Llenando lista
                    //BESelectListItem be = new BESelectListItem();
                    //be.Text = "TODOS";
                    //be.Value = "0";
                    //lista.Add(be);

                    BESelectListItem be1 = new BESelectListItem();
                    be1.Text = "FACTURA";
                    be1.Value = "1";
                    lista.Add(be1);
                    ListaTipoDocTMP.Add(be1);

                    BESelectListItem be2 = new BESelectListItem();
                    be2.Text = "BOLETA";
                    be2.Value = "2";
                    lista.Add(be2);
                    ListaTipoDocTMP.Add(be2);

                    BESelectListItem be3 = new BESelectListItem();
                    be3.Text = "NOTA DE CREDITO";
                    be3.Value = "3";
                    lista.Add(be3);
                    ListaTipoDocTMP.Add(be3);
                    #endregion
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(lista, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaModalidadXOficina", ex);
            }
            try
            {
                StringBuilder shtml = new StringBuilder();

                if (lista.Count() > 0)
                {
                    string TODOS = "TODOS";
                    shtml.Append("<table class='tblTipoDocumento' border=0 class='k-grid k-widget' id='tblTipoDocumento'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<td width=16% style='background-color:#F5F5F5' align=LEFT>TIPO DOCUMENTO : </td>");
                    shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;text-align:left-center;width=2.5px';>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='checkALL2' name='Check2' value='" + '0' + "' class='select-all' onclick='check2();' checked /> &nbsp;&nbsp;" + TODOS + " &nbsp;&nbsp;&nbsp;&nbsp;</td>");



                    //shtml.Append("<table class='tblTipoDocumento' border=0 width='65%;' class='k-grid k-widget' id='tblTipoDocumento'>");
                    //shtml.Append("<thead><tr>");
                    //shtml.Append("<td width=16% style='background-color:#F5F5F5' align=LEFT>TIPO DOCUMENTO : </td>");
                }
                if (lista != null)
                {
                    foreach (var item in lista.OrderBy(x => x.Value))
                    {          // width:2.5px
                        shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;cursor:pointer;text-align:left-center; width:auto;padding-right:15px; ';><input type='checkbox' name='Check2' value='" + item.Value + "' class='Check2' checked />&nbsp;&nbsp;" + item.Text + "</td>");
                        //shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;cursor:pointer;text-align:left-center;width=2.5px ';><input type='checkbox' name='Check' value='" + item.Value + "' class='Check' checked />&nbsp;" + item.Text + "</td>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaEstadoRadioButton()
        {
            parametrosEstado = null;
            List<BESelectListItem> lista = new List<BESelectListItem>();
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //Session.Remove(K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL);
                    #region Llenano Lista
                    //BESelectListItem be = new BESelectListItem();
                    //be.Text = "TODOS";
                    //be.Value = "0";
                    //lista.Add(be);

                    BESelectListItem be1 = new BESelectListItem();
                    be1.Text = "CANCELADO";
                    be1.Value = "1";
                    lista.Add(be1);

                    BESelectListItem be2 = new BESelectListItem();
                    be2.Text = "PENDIENTE";
                    be2.Value = "2";
                    lista.Add(be2);

                    BESelectListItem be3 = new BESelectListItem();
                    be3.Text = "PARCIALMENTE CANCELADO";
                    be3.Value = "3";
                    lista.Add(be3);

                    BESelectListItem be4 = new BESelectListItem();
                    be4.Text = "ANULADO";
                    be4.Value = "4";
                    lista.Add(be4);
                    #endregion
                    ListaEstadoTMP = new List<BESelectListItem>();
                    ListaEstadoTMP = lista;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(lista, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaModalidadXOficina", ex);
            }
            try
            {
                StringBuilder shtml = new StringBuilder();

                if (lista.Count() > 0)
                {

                    string TODOS = "TODOS";
                    shtml.Append("<table class='tblEstado' border=0 class='k-grid k-widget' id='tblEstado'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<td width=16% style='background-color:#F5F5F5' align=LEFT>ESTADO : </td>");
                    shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;text-align:left-center;width=2.5px';>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='checkALL3' name='Check3' value='" +'0' + "' class='select-all' onclick='check3();' checked /> &nbsp;&nbsp;" + TODOS + " &nbsp;&nbsp;&nbsp;&nbsp;</td>");


                    //shtml.Append("<table class='tblEstado' border=0 width='65%;' class='k-grid k-widget' id='tblEstado'>");
                    //shtml.Append("<thead><tr>");
                    //shtml.Append("<td width=16% style='background-color:#F5F5F5' align=LEFT>ESTADO : </td>");
                }
                if (lista != null)
                {
                    foreach (var item in lista.OrderBy(x => x.Value))
                    {          // width:2.5px
                        shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;cursor:pointer;text-align:left-center; width:auto;padding-right:15px; ';><input type='checkbox' name='Check3' value='" + item.Value + "' class='Check3' checked />&nbsp;&nbsp;" + item.Text + "</td>");
                        //shtml.AppendFormat("<td align='left-center' style='background-color:#F5F5F5;cursor:pointer;text-align:left-center;width=2.5px ';><input type='checkbox' name='Check' value='" + item.Value + "' class='Check' checked />&nbsp;" + item.Text + "</td>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipoDocSeleccionadas(List<BESelectListItem> ReglaValor)
        {
            string para = " ";
            string Tipo = "";
            string parametro = " ";
            int count = 0;
            int count2 = 0;
            DESC_TipoDoc = null;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        ListaTipoDoc = ReglaValor;
                    }
                    else
                    {

                        ListaTipoDoc = new List<BESelectListItem>();
                    }
                    //Lista que recupera el Mog_ID
                    foreach (var item in ListaTipoDoc.OrderBy(x => x.Value))
                    {
                        para = item.Value;
                        count += 1;
                        if (count > 1)
                        {
                            parametro = parametro + "," + para;
                        }
                        else
                        {
                            parametro = para;

                        }
                    };

                    // Recupera la descripcion de las modalidades seleccionadas
                    foreach (var item in ListaTipoDoc.OrderBy(x => x.Value))
                    {
                        foreach (var item2 in ListaTipoDocTMP.Where(x => x.Value == item.Value))
                        {
                            Tipo = item2.Value;
                            count2 += 1;
                            if (count2 > 1)
                            {
                                DESC_TipoDoc = DESC_TipoDoc + " - " + Tipo;
                            }
                            else
                            {
                                DESC_TipoDoc = Tipo;
                            }
                        };

                    }
                    parametrosTipoDoc = parametro;
                    if (parametrosTipoDoc == " ")
                    {
                        parametrosTipoDoc = null;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ConsultaModalidadXOficinaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ConsultaModalidadXOficina", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEstadoSeleccionadas(List<BESelectListItem> ReglaValor)
        {
            string para = " ";
            string Estado = "";
            string parametro = " ";
            int count = 0;
            int count2 = 0;
            DESC_Estado = null;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        ListaEstado = ReglaValor;
                    }
                    else
                    {

                        ListaEstado = new List<BESelectListItem>();
                    }
                    //Lista que recupera el Mog_ID
                    foreach (var item in ListaEstado.OrderBy(x => x.Value))
                    {
                        para = item.Value;
                        count += 1;
                        if (count > 1)
                        {
                            parametro = parametro + "," + para;
                        }
                        else
                        {
                            parametro = para;

                        }
                    };

                    // Recupera la descripcion de las modalidades seleccionadas
                    foreach (var item in ListaEstado.OrderBy(x => x.Value))
                    {
                        foreach (var item2 in ListaEstado.Where(x => x.Value == item.Value))
                        {
                            Estado = item2.Value;
                            count2 += 1;
                            if (count2 > 1)
                            {
                                DESC_Estado = DESC_Estado + " - " + Estado;
                            }
                            else
                            {
                                DESC_Estado = Estado;
                            }
                        };

                    }
                    parametrosEstado = parametro;
                    if (parametrosEstado == " ")
                    {
                        parametrosEstado = null;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ConsultaModalidadXOficinaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ConsultaModalidadXOficina", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        public ActionResult ReporteResumenFacturacionMensual(string fini, string ffin, string finiCan, string ffinCan, string formato
            , string idoficina, string nombreoficina, int FechaEmi, int FechaCan,int FechaCon
            , string finiCon, string ffinCon,string ModalidadDetalle)
        {
            Resultado retorno = new Resultado();

            string format = formato;
            int oficina_id = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            if (opcAdm == 1)
            {
                idoficina = idoficina;
                oficina = nombreoficina;
            }
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {

                LocalReport localReport = new LocalReport();
                if (formato == "PDF")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_RESUMEN_FACTURACION_MENSUAL.rdlc");
                else if (formato == "EXCEL")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_RESUMEN_FACTURACION_MENSUAL_EXCEL.rdlc");
                //localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_RESUMEN_FACTURACION_MENSUAL.rdlc");

                List<BEReporteResumenFacturacionMensual> lstReporte = new List<BEReporteResumenFacturacionMensual>();

                lstReporte = listaReporteResumenFacturacionMensualTMP;
                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);
                    if (FechaEmi == 1)
                    {
                        fini = "--/--/----";
                    }
                    ReportParameter parametroFecha1 = new ReportParameter();
                    parametroFecha1 = new ReportParameter("FechaInicio", fini.ToString());
                    localReport.SetParameters(parametroFecha1);
                    if (FechaEmi == 1)
                    {
                        ffin = "--/--/----";
                    }
                    ReportParameter parametroFecha2 = new ReportParameter();
                    parametroFecha2 = new ReportParameter("FechaFin", ffin.ToString());
                    localReport.SetParameters(parametroFecha2);
                    if (FechaCan == 1)
                    {
                        finiCan = "--/--/----";
                    }
                    ReportParameter parametroFechaCan1 = new ReportParameter();
                    parametroFechaCan1 = new ReportParameter("FechaInicioCancel", finiCan.ToString());
                    localReport.SetParameters(parametroFechaCan1);
                    if (FechaCon == 1)
                    {
                        ffinCan = "--/--/----";
                    }
                    ReportParameter parametroFechaCan2 = new ReportParameter();
                    parametroFechaCan2 = new ReportParameter("FechaFinCancel", ffinCan.ToString());
                    localReport.SetParameters(parametroFechaCan2);
                    if (FechaCon == 1)
                    {
                        finiCon = "--/--/----";
                    }
                    ReportParameter parametroFechaCon1 = new ReportParameter();
                    parametroFechaCon1 = new ReportParameter("FechaInicioConfirmacion", finiCon.ToString());
                    localReport.SetParameters(parametroFechaCon1);
                    if (FechaCan == 1)
                    {
                        ffinCon = "--/--/----";
                    }
                    ReportParameter parametroFechaCon2 = new ReportParameter();
                    parametroFechaCon2 = new ReportParameter("FechaFinConfirmacion", ffinCon.ToString());
                    localReport.SetParameters(parametroFechaCon2);
                    ReportParameter fecha = new ReportParameter();

                    fecha = new ReportParameter("Fecha", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);
                    if (idoficina == "0")
                    {
                        oficina = "TODAS LAS OFICINAS";
                    }
                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);
                    if (DESC_Modalidad == null)
                    {
                        DESC_Modalidad = "TODAS LAS MODALIDADES";
                    }
                    ReportParameter parametroModalidad = new ReportParameter();
                    parametroModalidad = new ReportParameter("DESC_Modalidad", DESC_Modalidad);
                    localReport.SetParameters(parametroModalidad);

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
                    "  <PageHeight>8.3in</PageHeight>" +
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
                    localReport.DisplayName = "REPORTE RESUMEN FACTURACION MENSUAL";

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

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Facturacion Mensual", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ReporteTipo(string fini, string ffin, string finiCan, string ffinCan, string formato, string idoficina, int DEPARTAMENTO, int PROVINCIA, int DISTRITO, int FechaEmi, int FechaCan, string nombreoficina,  string tipoenvio
            ,string finiCon, string ffinCon,int FechaCon,string ModalidadDetalle)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_RESUMEN_FACTURACION_MENSUAL);
            Resultado retorno = new Resultado();
            if (FechaCan == 1)
            {
                finiCan = "";
                ffinCan = "";
            }
            if (FechaEmi == 1)
            {
                fini = "";
                ffin = "";
            }
            if (FechaCon == 1)
            {
                finiCon = "";
                ffinCon = "";
            }
            string format = formato;
            int oficina_id = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            //if (oficina_id == 10081 || oficina_id == 10082)
            if (opcAdm != 1)
            {
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                //if(parametrosRubro== null)
                //{
                //    parametrosRubro = "0";
                //}
                List<BEReporteResumenFacturacionMensual> listar = new List<BEReporteResumenFacturacionMensual>();
                listar = new BLReporteResumenFacturacionMensual().ReporteResumenFacturacionMensual(fini, ffin, finiCan, ffinCan, finiCon, ffinCon, idoficina, parametrosRubro, DEPARTAMENTO, PROVINCIA, DISTRITO, parametrosEstado, parametrosTipoDoc,  tipoenvio, ModalidadDetalle);
                listaReporteResumenFacturacionMensualTMP = new List<BEReporteResumenFacturacionMensual>();
                listaReporteResumenFacturacionMensualTMP = listar;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Facturacion Mensual", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}