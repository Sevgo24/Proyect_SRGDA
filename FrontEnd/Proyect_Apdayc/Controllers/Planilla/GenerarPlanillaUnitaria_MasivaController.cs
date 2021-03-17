using Proyect_Apdayc.Clases;
using SGRDA.BL;
using SGRDA.Documento;
using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Planilla
{
    public class GenerarPlanillaUnitaria_MasivaController : Base
    {
        // GET: GenerarPlanillaUnitaria_Masiva
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GenerarFormatoPlanillaJson(decimal IdLicencia, decimal idSerie, decimal idReportPlanilla)
        {
            Resultado retorno = new Resultado();
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
            try
            {

                if (!isLogout(ref retorno))
                {
                    ViewBag.Error = "";
                    //decimal IdLicencia = idRef;
                    //var obj = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, idObj);

                    if (idSerie != 0)
                    {
                        //var idReport = obj.WRKF_OINTID;
                        WordDocumentReport r = new WordDocumentReport();
                        string WRKF_OPATH = "PlanillaEjecucionEstandar_" + Convert.ToString(IdLicencia);
                        #region "Configurar nombre de archivos"

                        var nameCopyPDF = string.Empty;
                        var nameCopy = string.Format("{1}_{0}", WRKF_OPATH, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        var rutaFile = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, WRKF_OPATH);
                        // var rutaFile = string.Format("{0}{1}",, obj.WRKF_OPATH);
                        var rutaFileCopy = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, nameCopy);
                        if (rutaFileCopy.IndexOf(".docx") != -1)
                        {
                            rutaPDF = rutaFileCopy.Replace(".docx", ".pdf");
                            nameCopyPDF = nameCopy.Replace(".docx", ".pdf");
                        }
                        else
                        {
                            if (rutaFileCopy.IndexOf(".doc") != -1)
                                rutaPDF = rutaFileCopy.Replace(".doc", ".pdf");
                            nameCopyPDF = nameCopy.Replace(".doc", ".pdf");
                        }
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        //#region "generar documento según su código interno"
                        var existOINT = true;
                        string nombre_oficina = Convert.ToString(Session[Constantes.Sesiones.Oficina]);
                      
                        ExitoReporte = r.CrearReportePlanillaEjecicionStandard(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla, nombre_oficina);
                                

                        var errorGenPlan = false;
                        if (ExitoReporte == false)
                        {
                            errorGenPlan = true;
                            var listmessage = string.Join(", ", GlobalVars.Global.ListMessageReport);
                            //  ViewBag.Error = "No se pudo generar documento. Faltan datos.\n" + listmessage;
                            //  return View();
                            retorno.result = Constantes.MensajeRetorno.ERROR;
                            retorno.message = "No se pudo generar documento. Faltan datos:<br>" + listmessage;
                        }

                        if (errorGenPlan == false) //existOINT &
                        {
                            //ViewBag.Error = "Documento generado y descargando..";
                            //agregar a tab documenos el doc generado
                            decimal doc_type = 57;
                            registraDocumentoGenerado(IdLicencia, nameCopyPDF, rutaPDF, doc_type);
                            retorno.result = Constantes.MensajeRetorno.OK;
                            retorno.message = rutaWebPDF;
                            // return File(rutaWebPDF, "application/pdf");
                        }
                      
                    }
                    else
                    {
                        //ViewBag.Error = "Código de Objeto no encontrado";
                        //return View();
                        retorno.result = Constantes.MensajeRetorno.ERROR;
                        retorno.message = "Código de Objeto no encontrado";
                    }
                }
                else
                {
                    //ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                    //return View();

                    retorno.result = Constantes.MensajeRetorno.LOGOUT;
                    retorno.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormatoPlanilla", ex);
                //ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                //return View();
                retorno.result = Constantes.MensajeRetorno.ERROR;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public void registraDocumentoGenerado(decimal codLic, string nombreGenerado, string rutaActual, decimal idTipoDoc)
        {
            Resultado retorno = new Resultado();

            var docGral = new BEDocumentoGral();

            docGral.DOC_ID = 0;
            docGral.OWNER = GlobalVars.Global.OWNER;
            docGral.DOC_TYPE = Convert.ToInt32(idTipoDoc);
            docGral.DOC_PATH = nombreGenerado;
            docGral.DOC_DATE = DateTime.Now;
            docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
            docGral.DOC_USER = UsuarioActual;
            docGral.LOG_USER_CREAT = UsuarioActual;
            docGral.DOC_VERSION = 1;

            var objDocLic = new BEDocumentoLic
            {
                LIC_ID = codLic,
                OWNER = docGral.OWNER,
                LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                DOC_ORG = Constantes.OrigenDocumento.SALIDA
            };
            var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, objDocLic);

            var pathDestino = GlobalVars.Global.RutaDocLicSalida;

            //*****************************************pris
            string oldPath = rutaActual.Replace(".pdf", ".docx");
            string newpath = pathDestino;
            string newFileName = nombreGenerado.Replace(".pdf", ".docx");
            FileInfo f1 = new FileInfo(oldPath);
            f1.CopyTo(string.Format("{0}{1}", newpath, newFileName));
            //****************************************************
            string savePath = String.Format("{0}{1}", pathDestino, nombreGenerado);
            FileStream sourceStream = new FileStream(rutaActual, FileMode.Open);
            FileStream targetStream = new FileStream(savePath, FileMode.CreateNew);
            sourceStream.CopyTo(targetStream);
            targetStream.Close();
            sourceStream.Close();

        }


    }
}