using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using Microsoft.Reporting.WebForms;
namespace Proyect_Apdayc.Controllers
{
    public class TipoCampaniaController : Base
    {
        //
        // GET: /TipoCampania/
        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public JsonResult ListarTipoCampania(int skip, int take, int page, int pageSize, decimal tipo, string dato, int st)
        {
            var lista = ListaCampania(GlobalVars.Global.OWNER, tipo, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETipoCampania { ListaCampTipo = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoCampania { ListaCampTipo = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoCampania> ListaCampania(string owner, decimal tipo, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoCampania().ListarCampaniaPage(owner, tipo, dato, st, pagina, cantRegxPag);
        }

        public JsonResult Eliminar(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTipoCampania servicio = new BLTipoCampania();
                    var result = servicio.Eliminar(new BETipoCampania
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CONC_CTID = Id,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar tipo campaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTipoCampania servicio = new BLTipoCampania();
                    var dato = servicio.Obtener(GlobalVars.Global.OWNER, Id);

                    if (dato != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el tipo de campaña";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos tipo de campaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BETipoCampania en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoCampania obj = new BETipoCampania();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.CONC_CTID = en.CONC_CTID;
                    obj.CONC_CTNAME = en.CONC_CTNAME;
                    obj.OBS_TYPE = en.OBS_TYPE;
                    obj.valgraba = en.valgraba;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;

                    if (obj.valgraba == 0)
                    {
                        var datos = new BLTipoCampania().Insertar(obj);
                    }
                    else
                    {
                        var datos = new BLTipoCampania().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert tipo campaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BETipoCampania en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTipoCampania servicio = new BLTipoCampania();
                en.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(en);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion tipo campaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadReport(string format, decimal tipo, string desc, int st)
        {
            Init(false);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_TipoCampania.rdlc");

            List<BETipoCampania> lista = new List<BETipoCampania>();
            lista = new BLTipoCampania().ListaTipoCampania(GlobalVars.Global.OWNER, tipo, desc, st);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;
            localReport.DataSources.Add(reportDataSource);

            ReportParameter parametro = new ReportParameter();
            parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
            localReport.SetParameters(parametro);

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + format + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
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
}
