using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.Entities;
using SGRDA.BL;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace SGRDA.MVC.Controllers
{
    public class GRUPOMODALIDADController : Base
    {

        private const string K_SESION_FORMATO = "___DTOFormato";
        private const string K_SESION_FORMATO_DEL = "______DTOFormatoDEL";
        private const string K_SESION_FORMATO_ACT = "___DTOFormatoACT";

        // GET: /CaracteristicaPredefinida/
        List<DTOFormatoFactura> Formato = new List<DTOFormatoFactura>();
        //
        // GET: /GRUPOMODALIDAD/

        public ActionResult Index()
        {
            Init(false);
            //var lista = GrupoModalidadListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_FORMATO);
            Session.Remove(K_SESION_FORMATO_DEL);
            Session.Remove(K_SESION_FORMATO_ACT);
            return View();
        }

        public List<BEREC_MOD_GROUP> usp_listar_GrupoModalidad()
        {
            return new BLREC_MOD_GROUP().REC_MOD_GROUP_GET();
        }

        public JsonResult usp_listar_GrupoModalidadJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();
            var lista = GrupoModalidadListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_MOD_GROUP { RECMODGROUP = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_MOD_GROUP { RECMODGROUP = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_MOD_GROUP> GrupoModalidadListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_MOD_GROUP().usp_REC_MOD_GROUP_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);
            //Session.Remove(K_SESION_FORMATO);
            //Session.Remove(K_SESION_FORMATO_DEL);
            //Session.Remove(K_SESION_FORMATO_ACT);
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(BEREC_MOD_GROUP en)
        //{
        //    //if (string.IsNullOrEmpty(frm["txtDescripcion"]))
        //    //    ModelState.AddModelError("txtterritorioImpuesto", "Ingrese la descripción");

        //    //var valid = ModelState.IsValid;
        //    //BEREC_MOD_GROUP grupomodalidada = new BEREC_MOD_GROUP()
        //    //{
        //    //    OWNER = frm["txtPropietario"],
        //    //    MOG_ID = frm["txtGrupomodalidad"],
        //    //    MOG_DESC = frm["txtDescripcion"],
        //    //    LOG_USER_CREAT = "USERCREAT"
        //    //};
        //    var valid = ModelState.IsValid;

        //    en.LOG_USER_CREAT = "USERCREAT";
        //    if (valid == true)
        //    {
        //        bool std = new BLREC_MOD_GROUP().REC_MOD_GROUP_Ins(en);

        //        if (std)
        //        {
        //            //TempData["msg"] = "Registrado Correctamente";
        //            //TempData["class"] = "alert alert-success";
        //            TempData["flag"] = 1;
        //        }
        //        else
        //        {
        //            TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
        //            TempData["class"] = "alert alert-danger";
        //        }
        //    }
        //    else
        //    {
        //        TempData["class1"] = "alert alert-danger";
        //    }
        //    return View();
        //}

        public ActionResult Edit(string id)
        {
            Init(false);
            Session.Remove(K_SESION_FORMATO);
            Session.Remove(K_SESION_FORMATO_DEL);
            Session.Remove(K_SESION_FORMATO_ACT);
            BEREC_MOD_GROUP grupomodalidada = new BEREC_MOD_GROUP();
            var lista = new BLREC_MOD_GROUP().REC_MOD_GROUP_GET_by_MOG_ID(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    grupomodalidada.OWNER = item.OWNER;
                    grupomodalidada.MOG_ID = item.MOG_ID;
                    grupomodalidada.MOG_DESC = item.MOG_DESC;
                }
            }
            return View(grupomodalidada);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_MOD_GROUP en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDAT = UsuarioActual;

            bool std = new BLREC_MOD_GROUP().REC_MOD_GROUP_Upd_by_MOG_ID(en);

            if (std)
            {
                //TempData["msg"] = "Actualizado Correctamente";
                //TempData["class"] = "alert alert-success";
                TempData["flag"] = 1;
            }
            else
            {
                TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                TempData["class"] = "alert alert-danger";
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_MOD_GROUP> dato)
        {
            Init(false);
            foreach (var item in dato)
            {
                var impuesto = new BEREC_MOD_GROUP()
                {
                    MOG_ID = item.MOG_ID
                };
                bool std = new BLREC_MOD_GROUP().REC_MOD_GROUP_Del(item.MOG_ID);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_MOD_GROUP.rdlc");

            List<BEREC_MOD_GROUP> lista = new List<BEREC_MOD_GROUP>();
            lista = usp_listar_GrupoModalidad();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            /*if (territory != null)
            {
                var customerfilterList = from c in customerList
                                         where c.Territory == territory
                                         select c;


                reportDataSource.Value = customerfilterList;
            }
            else*/
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "Image";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>jpeg</OutputFormat>" +
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
                return File(renderedBytes, "pdf");
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }

        public ActionResult DownloadReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_MOD_GROUP.rdlc");

            List<BEREC_MOD_GROUP> lista = new List<BEREC_MOD_GROUP>();
            lista = usp_listar_GrupoModalidad();

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

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEREC_MOD_GROUP tipo = new BEREC_MOD_GROUP();
                tipo = new BLREC_MOD_GROUP().Obtener(GlobalVars.Global.OWNER, id);

                if (tipo != null)
                {
                    if (tipo.FormatoModalidad != null)
                    {
                        Formato = new List<DTOFormatoFactura>();
                        if (tipo.FormatoModalidad != null)
                        {
                            tipo.FormatoModalidad.ForEach(s =>
                            {
                                Formato.Add(new DTOFormatoFactura
                                {
                                    IdGrupo = s.MOG_ID,
                                    Grupo = s.GRUPO,
                                    IdFormato = s.INVF_ID,
                                    IdFormatoAnt = s.INVF_ID_ANT,
                                    Formato = s.FORMATO,
                                    LOG_USER_CREAT = s.LOG_USER_CREAT,
                                    LOG_DATE_CREAT = s.LOG_DATE_CREAT,
                                    LOG_USER_UPDAT = s.LOG_USER_UPDATE,
                                    LOG_DATE_UPDATE = s.LOG_DATE_UPDATE,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });

                            FormatoTmp = Formato;
                            ListarFormato();
                        }
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha encontrado las Modalidades por el Formato de Factura";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene los Grupos de Modalidades", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Agregar y Listar Características

        public List<DTOFormatoFactura> FormatoTmp
        {
            get
            {
                return (List<DTOFormatoFactura>)Session[K_SESION_FORMATO];
            }
            set
            {
                Session[K_SESION_FORMATO] = value;
            }
        }

        private List<DTOFormatoFactura> FormatoTmpUPDEstado
        {
            get
            {
                return (List<DTOFormatoFactura>)Session[K_SESION_FORMATO_DEL];
            }
            set
            {
                Session[K_SESION_FORMATO_DEL] = value;
            }
        }

        private List<DTOFormatoFactura> FormatoTmpDelBD
        {
            get
            {
                return (List<DTOFormatoFactura>)Session[K_SESION_FORMATO_ACT];
            }
            set
            {
                Session[K_SESION_FORMATO_ACT] = value;
            }
        }

        public JsonResult ListarFormato()
        {
            Formato = FormatoTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                shtml.Append("<th class='k-header'>Formato de Factura</th>");
                shtml.Append("<th class='k-header'>Usuario Crea</th>");
                shtml.Append("<th class='k-header'>Fecha Crea</th>");
                shtml.Append("<th class='k-header'>Usu. Modi</th>");
                shtml.Append("<th class='k-header'>Fecha Modi</th>");
                shtml.Append("<th class='k-header'>Estado</th>");
                shtml.Append("<th  class='k-header'></th>");
                shtml.Append("</tr></thead>");

                if (Formato != null)
                {
                    foreach (var item in Formato.OrderBy(x => x.IdFormato))
                    {
                        if (item.Activo == true || item.Activo == false)
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.IdFormato);
                            shtml.AppendFormat("<td >{0}</td>", item.Formato);
                            shtml.AppendFormat("<td >{0:C}</td>", item.LOG_USER_CREAT);
                            shtml.AppendFormat("<td >{0}</td>", item.LOG_DATE_CREAT);
                            shtml.AppendFormat("<td >{0:C}</td>", item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td >{0}</td>", item.LOG_DATE_UPDATE);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddFormato({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.IdFormato);
                            shtml.AppendFormat("                        <a href=# onclick='DellAddFormato({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.IdFormato, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Formato" : "Activar Formato");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
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
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddFormato(DTOFormatoFactura form)
        {
            Resultado retorno = new Resultado();
            try
            {
                Formato = FormatoTmp;
                if (Formato == null) Formato = new List<DTOFormatoFactura>();

                // if (Convert.ToInt32(documento.Id) <= 0) documento.Id = Convert.ToString(documentos.Count+1);
                //if (Convert.ToInt32(form.Id) <= 0)

                int registroNuevo = 0;
                int registroModificar = 0;
                //caracteristicas = CaracteristicasTmp;
                if (Formato != null)
                {
                    //registroNuevo = Formato.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && entidad.Id == 0).Count();
                    //registroModificar = Formato.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && p.Id == entidad.Id).Count();
                    registroNuevo = Formato.Where(p => p.IdFormato == form.IdFormato && form.IdFormatoAnt == 0).Count();
                    registroModificar = Formato.Where(p => p.IdFormato == form.IdFormatoAnt && p.IdFormatoAnt != form.IdFormato).Count();
                }
                else
                {
                    //caracteristicas = new List<DTOTarifaReglaData>();
                    //Formato = new List<DTOFormatoFactura>();
                }

                if ((form.IdFormatoAnt == 0 && registroNuevo == 0)
                     || (form.IdFormatoAnt != 0 && registroModificar > 0)
                   )
                

                //if (true)
                {

                    if (Convert.ToInt32(form.IdFormatoAnt) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (Formato.Count > 0) nuevoId = Formato.Max(x => x.IdFormato) + 1;
                        form.Id = nuevoId;
                        form.IdFormatoAnt = form.IdFormato;
                        form.Activo = true;
                        form.EnBD = false;
                        form.LOG_USER_CREAT = UsuarioActual;
                        form.LOG_DATE_CREAT = DateTime.Now;
                        Formato.Add(form);
                    }
                    else
                    {
                        var item = Formato.Where(x => x.IdFormato == form.IdFormatoAnt).FirstOrDefault();
                        form.Id = form.IdFormato;
                        form.Grupo = item.Grupo;
                        form.IdGrupo = item.IdGrupo;
                        //form.IdFormato = item.IdFormato;
                        //form.IdFormatoAnt = item.IdFormato;
                        //form.Formato = item.Formato;

                        form.EnBD = item.EnBD; //indicador que item viene de la BD
                        form.Activo = item.Activo;

                        form.LOG_USER_CREAT = item.LOG_USER_CREAT;
                        form.LOG_USER_UPDAT = UsuarioActual;
                        form.LOG_DATE_CREAT = DateTime.Now;
                        if (form.EnBD)
                        {
                            form.LOG_USER_UPDAT = UsuarioActual;
                            form.LOG_DATE_UPDATE = DateTime.Now;
                        }
                        Formato.Remove(item);
                        Formato.Add(form);
                    }
                    FormatoTmp = Formato;
                    retorno.result = 1;
                    retorno.message = "OK";

                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "El formato ya existe.";
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddFormato", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddFormato(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                Formato = FormatoTmp;
                if (Formato != null)
                {
                    var objDel = Formato.Where(x => x.IdFormato == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (FormatoTmpUPDEstado == null) FormatoTmpUPDEstado = new List<DTOFormatoFactura>();
                            if (FormatoTmpDelBD == null) FormatoTmpDelBD = new List<DTOFormatoFactura>();

                            var itemUpd = FormatoTmpUPDEstado.Where(x => x.IdFormato == id).FirstOrDefault();
                            var itemDel = FormatoTmpDelBD.Where(x => x.IdFormato == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) FormatoTmpUPDEstado.Add(objDel);
                                if (itemDel != null) FormatoTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) FormatoTmpDelBD.Add(objDel);
                                if (itemUpd != null) FormatoTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            Formato.Remove(objDel);
                            Formato.Add(objDel);
                        }
                        else
                        {
                            Formato.Remove(objDel);
                        }
                        FormatoTmp = Formato;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddFormato", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneFormatoTmp(decimal idForm)
        {
            Resultado retorno = new Resultado();
            try
            {
                var param = FormatoTmp.Where(x => x.IdFormato == idForm).FirstOrDefault();
                retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneFormatoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public JsonResult Insertar(BEREC_MOD_GROUP entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                entidad.OWNER = GlobalVars.Global.OWNER;
                entidad.LOG_USER_CREAT = "USERCREAT";
                entidad.RECMODGROUP = obtenerDetalles();

                var datos = new BLREC_MOD_GROUP().Insertar(entidad);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar la el grupo de modalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BEREC_MOD_GROUP entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                entidad.OWNER = GlobalVars.Global.OWNER;
                entidad.LOG_USER_UPDAT = UsuarioActual;
                entidad.RECMODGROUP = obtenerDetalles();

                //1.setting formatos de modalidad eliminar
                List<BEREC_MOD_GROUP> listaCarDel = null;
                if (FormatoTmpDelBD != null)
                {
                    listaCarDel = new List<BEREC_MOD_GROUP>();
                    FormatoTmpDelBD.ForEach(x => { listaCarDel.Add(new BEREC_MOD_GROUP { IdFormato = x.IdFormato, MOG_ID = x.IdGrupo }); });
                }
                //setting formatos de modalidad activar
                List<BEREC_MOD_GROUP> listaCarUpdEst = null;
                if (FormatoTmpUPDEstado != null)
                {
                    listaCarUpdEst = new List<BEREC_MOD_GROUP>();
                    FormatoTmpUPDEstado.ForEach(x => { listaCarUpdEst.Add(new BEREC_MOD_GROUP { IdFormato = x.IdFormato, MOG_ID = x.IdGrupo }); });
                }

                var datos = new BLREC_MOD_GROUP().Actualizar(entidad, listaCarDel, listaCarUpdEst);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Actualiza la el grupo de modalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEREC_MOD_GROUP> obtenerDetalles()
        {
            List<BEREC_MOD_GROUP> datos = new List<BEREC_MOD_GROUP>();
            if (FormatoTmp != null)
            {
                FormatoTmp.ForEach(x =>
                {
                    datos.Add(new BEREC_MOD_GROUP
                    {
                        IdFormato = x.IdFormato,
                        IdFormatoAnt = x.IdFormatoAnt,
                        Formato = x.Formato,
                        MOG_ID = x.IdGrupo,
                        MOG_DESC = x.Grupo,
                        LOG_USER_CREAT = x.LOG_USER_CREAT
                    });
                });
            }
            return datos;
        }
    }
}
