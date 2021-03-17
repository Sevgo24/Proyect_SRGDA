using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities.WorkFlow;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;

namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class ObjectsController : Base
    {
        //
        // GET: /Objects/

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

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string nombre, string codInterno, decimal idTipoObjeto, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, nombre, codInterno, idTipoObjeto, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_OBJECTS { ListarObject = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_OBJECTS { ListarObject = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_OBJECTS> BLListar(string owner, string nombre, string codInterno, decimal idTipoObjeto, int estado, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_OBJECTS().Listar(owner, nombre, codInterno, idTipoObjeto, estado, pagina, cantRegxPag);
        }

        public JsonResult TraceProcess(decimal aidWrkf, decimal idWrkf, decimal sidWrkf, decimal ref1Wrkf, decimal idProc)
        {
            Resultado retorno = new Resultado();
            try
            {
                var Object = new BL_WORKF_OBJECTS().ObtenerObjectsXActions(GlobalVars.Global.OWNER, aidWrkf);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "TraceProcess", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                WORKF_OBJECTS objeto = new WORKF_OBJECTS();
                objeto.OWNER = GlobalVars.Global.OWNER;
                objeto.WRKF_OID = id;
                objeto.LOG_USER_UPDATE = UsuarioActual;
                var datos = new BL_WORKF_OBJECTS().Eliminar(objeto);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public ActionResult Insertar(WORKF_OBJECTS objeto)
        {
            Resultado retorno = new Resultado();
            try
            {
                //if (objeto.WRKF_OPATH == "file") { objeto.WRKF_OPATH = null; }

                objeto.OWNER = GlobalVars.Global.OWNER;
                if (objeto.WRKF_OID == 0)
                {
                    objeto.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BL_WORKF_OBJECTS().Insertar(objeto);
                }
                else
                {
                    objeto.LOG_USER_UPDATE = UsuarioActual;
                    var datos = new BL_WORKF_OBJECTS().Actualizar(objeto);
                }
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Obtener(decimal id)
        {
            StringBuilder shtml = new StringBuilder();
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var cliente = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, id);

                    //Correo electrónico a Usuarios y Terceros
                    if (cliente.TipoObjeto.WRKF_OPREF == GlobalVars.Global.PrefijoMailTer || cliente.TipoObjeto.WRKF_OPREF == GlobalVars.Global.PrefijoMailUsu)
                    {
                        retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 3;
                    }
                    // Documento de Entrada
                    else if (cliente.TipoObjeto.WRKF_OPREF == GlobalVars.Global.PrefijoDocumentoEntrada)
                    {
                        retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 4;
                    }
                    else
                    {
                        retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SeleccionarTipo(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var tipo = new BL_WORKF_OBJECTS_TYPE().SeleccionarTipo(GlobalVars.Global.OWNER, id);

                    //Correo electrónico a Usuarios y Terceros
                    if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailTer || tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailUsu)
                    {
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 3;
                    }
                    // Documento de Entrada
                    else if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoDocumentoEntrada)
                    {
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 4;
                    }
                    else
                    {
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                        retorno.message = "OK";
                        retorno.result = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult Upload(WORKF_OBJECTS objeto)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        var fec = DateTime.Now.ToString("yyyyMMddHHmmss");
        //        var guid = Guid.NewGuid().ToString();
        //        var file = Request.Files["Filedata"];
        //        var name = file.FileName;
        //        string savePath = "";

        //        var tipo = new BL_WORKF_OBJECTS_TYPE().SeleccionarTipo(GlobalVars.Global.OWNER, objeto.WRKF_OTID);

        //        //Correo electrónico a Usuarios y Terceros
        //        if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailTer || tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailUsu)
        //        {
        //            var path = GlobalVars.Global.RutaPlantillaCorreo;
        //            savePath = String.Format("{0}{1}", path, name);
        //            file.SaveAs(savePath);
        //        }
        //        // Documento de Entrada
        //        else if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoDocumentoEntrada)
        //        {
        //            var path = GlobalVars.Global.RutaPlantillaLicenciaWeb;
        //            savePath = String.Format("{0}{1}", path, name);
        //            file.SaveAs(savePath);
        //        }
        //        else
        //        {
        //            var path = GlobalVars.Global.RutaFisicaImgObjects;
        //            savePath = String.Format("{0}{1}", path, name);
        //            file.SaveAs(savePath);
        //        }

        //        if (objeto.WRKF_OID == 0)
        //        {
        //            objeto.OWNER = GlobalVars.Global.OWNER;
        //            objeto.LOG_USER_CREAT = UsuarioActual;
        //            objeto.WRKF_OPATH = name;
        //            var datos = new BL_WORKF_OBJECTS().Insertar(objeto);
        //        }
        //        else
        //        {
        //            objeto.OWNER = GlobalVars.Global.OWNER;
        //            objeto.WRKF_OPATH = name;
        //            objeto.LOG_USER_UPDATE = UsuarioActual;

        //            var path2 = GlobalVars.Global.RutaFisicaImgObjects;
        //            string savePath2 = String.Format("{0}{1}", path2, name);
        //            if (System.IO.File.Exists(savePath2))
        //            {
        //                System.IO.File.Delete(savePath2);
        //            }
        //            file.SaveAs(savePath2);

        //            var datos = new BL_WORKF_OBJECTS().Actualizar(objeto);
        //        }
        //        retorno.result = 1;
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Upload", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult VerImagen(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            var imagen = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, id);

        //            if (imagen.WRKF_OPATH != null)
        //            {
        //                imagen.WRKF_OPATH = imagen.WRKF_OPATH;

        //                var tipo = new BL_WORKF_OBJECTS_TYPE().SeleccionarTipo(GlobalVars.Global.OWNER, imagen.WRKF_OTID);

        //                //Correo electrónico a Usuarios y Terceros
        //                if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailTer || tipo.WRKF_OPREF == GlobalVars.Global.PrefijoMailUsu)
        //                {
        //                    var pathWeb = GlobalVars.Global.RutaPlantillaCorreoWeb;
        //                    var ruta = string.Format("{0}{1}", pathWeb, imagen.WRKF_OPATH);
        //                    imagen.WRKF_OPATH = ruta;
        //                }
        //                // Documento de Entrada
        //                else if (tipo.WRKF_OPREF == GlobalVars.Global.PrefijoDocumentoEntrada)
        //                {
        //                    var pathWeb = GlobalVars.Global.RutaPlantillaLicenciaWeb;
        //                    var ruta = string.Format("{0}{1}", pathWeb, imagen.WRKF_OPATH);
        //                    imagen.WRKF_OPATH = ruta;
        //                }
        //                else
        //                {
        //                    var pathWeb = GlobalVars.Global.RutaWebImgObjects;
        //                    var ruta = string.Format("{0}{1}", pathWeb, imagen.WRKF_OPATH);
        //                    imagen.WRKF_OPATH = ruta;
        //                }

        //                retorno.data = Json(imagen, JsonRequestBehavior.AllowGet);
        //                retorno.message = "OK";
        //                retorno.result = 1;
        //            }
        //            else
        //            {
        //                retorno.data = Json(imagen, JsonRequestBehavior.AllowGet);
        //                retorno.message = "No se encontró ningún archivo";
        //                retorno.result = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult ActualizarNombreDocTmp(string nombre, decimal idDoc)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            retorno.valor = "-";

        //            documentos = DocumentosTmp;
        //            if (documentos == null) documentos = new List<DTODocumento>();
        //            documentos.ForEach(x => { if (x.Id == idDoc) x.Archivo = nombre; });
        //            if (documentos.Count == 1) documentos[0].Archivo = nombre;
        //            DocumentosTmp = documentos;

        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
        //            retorno.result = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarNombreDocTmp", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AddDocumento(DTODocumento documento)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            documentos = DocumentosTmp;
        //            if (documentos == null) documentos = new List<DTODocumento>();

        //            // if (Convert.ToInt32(documento.Id) <= 0) documento.Id = Convert.ToString(documentos.Count+1);
        //            if (Convert.ToInt32(documento.Id) <= 0)
        //            {
        //                decimal nuevoId = 1;
        //                if (documentos.Count > 0) nuevoId = documentos.Max(x => x.Id) + 1;
        //                documento.Id = nuevoId;
        //                documento.Activo = true;
        //                documento.EnBD = false;
        //                documento.UsuarioCrea = UsuarioActual;
        //                documento.FechaCrea = DateTime.Now;
        //                documentos.Add(documento);
        //            }
        //            else
        //            {
        //                var item = documentos.Where(x => x.Id == documento.Id).FirstOrDefault();
        //                documento.EnBD = item.EnBD;//indicador que item viene de la BD
        //                documento.Activo = item.Activo;
        //                documento.Archivo = item.Archivo;
        //                documento.UsuarioCrea = item.UsuarioCrea;
        //                documento.FechaCrea = item.FechaCrea;
        //                if (documento.EnBD)
        //                {
        //                    documento.UsuarioModifica = UsuarioActual;
        //                    documento.FechaModifica = DateTime.Now;
        //                }
        //                documentos.Remove(item);
        //                documentos.Add(documento);
        //            }
        //            DocumentosTmp = documentos;

        //            retorno.result = 1;
        //            retorno.Code = Convert.ToInt32(documento.Id);
        //            retorno.message = "OK";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDocumento", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

    }
}
