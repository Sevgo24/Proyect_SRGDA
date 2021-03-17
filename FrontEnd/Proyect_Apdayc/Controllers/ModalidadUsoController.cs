using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WebForms;

namespace Proyect_Apdayc.Controllers
{
    public class ModalidadUsoController : Base
    {
        //
        // GET: /ModalidadUso/
        public const string nomAplicacion = "SRGDA";
        public static BEModalidad modUso = null;

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

        #region Index
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string MOD_DEC,
            string MOD_ORIG, string MOD_SOC, string CLASS_COD, string MOG_ID, string RIGHT_COD,
            string MOD_INCID, string MOD_USAGE, string MOD_REPER)
        {
            BEModalidad modalidad = new BEModalidad();
            modalidad.OWNER = GlobalVars.Global.OWNER;
            modalidad.MOD_DEC = MOD_DEC;
            modalidad.MOD_ORIG = MOD_ORIG;
            modalidad.MOD_SOC = MOD_SOC;
            modalidad.CLASS_COD = CLASS_COD;
            modalidad.MOG_ID = MOG_ID;
            modalidad.RIGHT_COD = RIGHT_COD;
            modalidad.MOD_INCID = MOD_INCID;
            modalidad.MOD_USAGE = MOD_USAGE;
            modalidad.MOD_REPER = MOD_REPER;

            string idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
            string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
            //if (idPerfil != Convert.ToString(idPerfilAdmin))
            //{
            //    if (modalidad.MOG_ID == "0") modalidad.MOG_ID = "-1";
            //}
            var lista = ListarModalidad(modalidad, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEModalidad { ListarModalidad = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEModalidad { ListarModalidad = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEModalidad> ListarModalidad(BEModalidad modalidad, int pagina, int cantRegxPag)
        {
            decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            string idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
            string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
            
            //TI y Contabilidad
            if (Convert.ToInt32(idOficina) == 10081 || Convert.ToInt32(idOficina) == 10082)
            {
                idOficina = 0;
            }

            return new BLModalidad().Listar(modalidad, pagina, cantRegxPag, idOficina, UsuarioActual);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLModalidad();
                    var modalidad = new BEModalidad();

                    modalidad.OWNER = GlobalVars.Global.OWNER;
                    modalidad.MOD_ID = id;
                    modalidad.LOG_USER_UPDAT = UsuarioActual;
                    servicio.Eliminar(modalidad);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public List<BEModalidad> ListarReporte(BEModalidad modalidad)
        {            
            return new BLModalidad().ListarReporte(modalidad);
        }

        public JsonResult Reporte(BEModalidad modalidad)
        {
            PasarValores(modalidad);
            Resultado retorno = new Resultado();
            try
            {
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        
        public void PasarValores(BEModalidad mod)
        {
            modUso = new BEModalidad();
            modUso = mod;
            modUso.OWNER = GlobalVars.Global.OWNER;        
        }

        public ActionResult DownloadReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/RptModalidadUso.rdlc");

            BEModalidad modalidad = new BEModalidad();
            modalidad.OWNER = GlobalVars.Global.OWNER;

            List<BEModalidad> lista = new List<BEModalidad>();
            lista = ListarReporte(modUso);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
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
        #endregion

        #region Nuevo
        public JsonResult Insertar(BEModalidad modalidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    modalidad.OWNER = GlobalVars.Global.OWNER;

                    validarInsert(out valResultado, out valMsg, modalidad);
                    if (valResultado)
                    {
                        if (modalidad.MOD_ID == 0)
                        {
                            modalidad.LOG_USER_CREAT = UsuarioActual;
                            var datos = new BLModalidad().Insertar(modalidad);
                            retorno.result = 1;
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        }
                        else
                        {
                            modalidad.LOG_USER_UPDAT = UsuarioActual;
                            var datos = new BLModalidad().Update(modalidad);
                            retorno.result = 1;
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLModalidad().Obtener(GlobalVars.Global.OWNER, id);
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.valor = obj.MOD_DEC;

                    //addon dbalvis 20150129
                    //retorna el codigo del workflow al que pertenece el producto
                    retorno.Code = Convert.ToInt32(obj.WRFK_ID); 


                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private static void validarInsert(out bool exito, out string msg_validacion, BEModalidad entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.MOD_DEC))
            {
                msg_validacion = "Ingrese descripción.";
                exito = false;
            }
        }
    }
}
