using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using SGRDA.BL.Reporte;
using SGRDA.Entities.Reporte;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;


namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteDepositoBancoController : Base
    {
        //
        // GET: /ReporteDepositoBanco/
        private const string K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO = "___K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO  ";

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO);
            return View();
        }

        private List<BEREPORTE_DEPOSITO_BANCO> ListaReporteDepositoBancoTmp
        {
            get
            {
                return (List<BEREPORTE_DEPOSITO_BANCO>)Session[K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO] = value;
            }
        }


        //****************************REPORTE*******************************
        //,string oficina le quite esta variable para no confundir a el JAVASCRIPT
        #region LISTA_TIPO_COBRO
        public JsonResult LISTA_TIPO_COBRO()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREPORTE_DEPOSITO_BANCARIO().ListarTipoCobro().Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.VALUE),
                        Text = Convert.ToString(c.VDESC)
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
        #endregion
        public ActionResult ReporteDepositoBanco(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina
                                                , int conIng, int conCon, string finiCon, string ffinCon)
        {
            Resultado retorno = new Resultado();
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //si oficina es igual a && oficina_id != 26
            //if (oficina_id == 10081 || oficina_id == 10082)
            //{            
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            if (opcAdm == 1)
            {
                idoficina = idoficina;
                oficina = nombreoficina;
            }
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            /*
             * 26 OFICINA TV / CABLE
                25 OFICINA TRANSPORTE, DISCOTECA Y RADIO
             *  27 OFICINA SINCRONIZACIÓN Y REDEs 
             */

            if (idoficina == "26")
            {
                if (rubro == "T")
                {
                    rubrofiltro = 50;
                    oficina = "TELEVISIÓN";
                }
                else
                {
                    rubrofiltro = 39;
                    oficina = "CABLE";
                }
            }

            //Buscando Oficina como Admin o Contabilidad

            try
            {
                List<BEREPORTE_DEPOSITO_BANCO> listar = new List<BEREPORTE_DEPOSITO_BANCO>();
                bool repCobro = false;
                string TotalDeposito = string.Empty;
                LocalReport localReport = new LocalReport();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);
                DateTime FechaCobroCorte = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["FECHA_COBRO_INICIO"]);

                if ((FechaI.CompareTo(FechaCobroCorte) == 0 || FechaI.CompareTo(FechaCobroCorte) == 1)
                     && (FechaF.CompareTo(FechaCobroCorte) == 0 || FechaF.CompareTo(FechaCobroCorte) == 1))
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DEPOSITO_BANCO_COBROS.rdlc");
                    //listar = new BLREPORTE_DEPOSITO_BANCARIO().ReporteDepositoBancario_Cobro(fini, ffin, idoficina, rubrofiltro);
                    listar = ListaReporteDepositoBancoTmp;
                    repCobro = true;

                    //var agruparDepositos = from p in listar
                    //                       let k = new
                    //                       {
                    //                           k_ID_COBRO = p.ID_COBRO,
                    //                           k_CUENTA = p.CUENTA,
                    //                           k_DEPOSITO_MONTO_SOLES = p.DEPOSITO_MONTO_SOLES,
                    //                       }
                    //                       group p by k into t
                    //                       select new
                    //                       {
                    //                           //R_Key = t.Key,
                    //                           R_ID_COBRO = t.Key.k_ID_COBRO,
                    //                           R_CUENTA = t.Key.k_CUENTA,
                    //                           R_DEPOSITO_MONTO_SOLES = t.Key.k_DEPOSITO_MONTO_SOLES,
                    //                           //R_REC_TBASE = t.Sum(p => p.montoAplicar),
                    //                       };
                    //TotalDeposito = agruparDepositos.Sum(x => x.R_DEPOSITO_MONTO_SOLES).ToString();
                    TotalDeposito = "0";
                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DEPOSITO_BANCO.rdlc");
                    //listar = new BLREPORTE_DEPOSITO_BANCARIO().ReporteDepositoBancario(fini, ffin, idoficina, rubrofiltro);
                    listar = ListaReporteDepositoBancoTmp;
                }


                if (listar.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);

                    //PARAMETROS
                    ReportParameter parametroFechaIni = new ReportParameter();
                    parametroFechaIni = new ReportParameter("FechaInicio", conIng == 1 ? fini : "-");
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", conIng == 1 ? ffin : "-");
                    localReport.SetParameters(parametroFechaFin);

                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina.Replace("Y", "&"));
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter parametroFechaIniCon = new ReportParameter();
                    parametroFechaIniCon = new ReportParameter("FechaIniCon", conCon == 1 ? finiCon : "-");
                    localReport.SetParameters(parametroFechaIniCon);

                    ReportParameter parametroFechaFinCon = new ReportParameter();
                    parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    localReport.SetParameters(parametroFechaFinCon);


                    if (repCobro)
                    {
                        ReportParameter parametroTotalDeposito = new ReportParameter();
                        parametroTotalDeposito = new ReportParameter("TotalDeposito", TotalDeposito.ToString());
                        localReport.SetParameters(parametroTotalDeposito);
                    }


                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + format + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.3in</MarginTop>" +
                        "  <MarginLeft>0.2in</MarginLeft>" +
                        "  <MarginRight>0.0in</MarginRight>" +
                        "  <MarginBottom>0.0in</MarginBottom>" +
                        "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte Depósito a Banco";

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
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Depósito a Banco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //VALIDA REPORTE
        //****************************REPORTE*******************************
        //,string oficina le quite esta variable para no confundir a el JAVASCRIPT
        //PARAMETROS FECHA INICIAL,FECHA FINAL,FORMATO DE EL ARCHIV,EL RUBRO Y EL ID DE LA OFICINA , NOMBRE DE LA OFICINA

        public ActionResult ReporteTipo(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina      
                                        , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion   
                                        ,string TipoCobro)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DEPOSITO_BANCO);
            Resultado retorno = new Resultado();
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //if (oficina_id == 10081 || oficina_id == 10082)
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            if (opcAdm == 1)
                idoficina = idoficina;
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            if (idoficina == "26")
            {
                if (rubro == "T")
                    rubrofiltro = 50;
                else
                    rubrofiltro = 39;
            }

            try
            {
                List<BEREPORTE_DEPOSITO_BANCO> listar = new List<BEREPORTE_DEPOSITO_BANCO>();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);
                DateTime FechaCobroCorte = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["FECHA_COBRO_INICIO"]);

                if ((FechaI.CompareTo(FechaCobroCorte) == 0 || FechaI.CompareTo(FechaCobroCorte) == 1)
                     && (FechaF.CompareTo(FechaCobroCorte) == 0 || FechaF.CompareTo(FechaCobroCorte) == 1))
                    listar = new BLREPORTE_DEPOSITO_BANCARIO().ReporteDepositoBancario_Cobro(fini, ffin, idoficina
                        , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                        , rubrofiltro, TipoCobro);
                else
                    listar = new BLREPORTE_DEPOSITO_BANCARIO().ReporteDepositoBancario(fini, ffin, idoficina, rubrofiltro);

                ListaReporteDepositoBancoTmp = new List<BEREPORTE_DEPOSITO_BANCO>();
                ListaReporteDepositoBancoTmp = listar;

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Diario de Caja", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


    }
}
