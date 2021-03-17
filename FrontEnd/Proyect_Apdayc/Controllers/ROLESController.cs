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

namespace SGRDA.MVC.Controllers
{
    public class ROLESController : Base
    {
        //
        // GET: /ROLES/

        public ActionResult Index()
        {
            Init(false);
            var lista = RolesListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<ROLES> usp_listar_Roles()
        {
            return new BLROLES().usp_listar_Roles();
        }

        public JsonResult usp_listar_RolesJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            //var lista = usp_listar_Roles().Where(p => p.ROL_VNOMBRE_ROL.Contains(dato)).ToList();            
            var lista = RolesListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new ROLES { ROL = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            //return Json(new ROLES {  TotalVirtual = 14 }, JsonRequestBehavior.AllowGet);
        }

        public List<ROLES> RolesListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLROLES().usp_Get_RolesPage(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            if (string.IsNullOrEmpty(frm["nombre"]))
            {
                ModelState.AddModelError("nombre", "Ingrese Nombre");
            }

            if (string.IsNullOrEmpty(frm["txtdescripcion"]))
            {
                ModelState.AddModelError("txtdescripcion", "Ingrese Descripcion");
            }

            if (string.IsNullOrEmpty(frm["std"]))
            {
                ModelState.AddModelError("std", "Ingrese Estado");
            }

            var valid = ModelState.IsValid;
            ROLES rol = new ROLES()
                {
                    ROL_VNOMBRE_ROL = frm["nombre"],
                    ROL_VDESCRIPCION_ROL = frm["txtdescripcion"],
                    ROL_CACTIVO_ROL = frm["std"],
                    LOG_USER_UPDATE = "USERCREAT"
                };

            if (valid == true)
            {
                int std = new BLROLES().usp_Ins_Roles(rol);

                if (std == 1)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ROLES rol = new ROLES();
            var lista = new BLROLES().usp_listar_Roles_by_codigo(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    rol.ROL_ICODIGO_ROL = item.ROL_ICODIGO_ROL;
                    rol.ROL_VNOMBRE_ROL = item.ROL_VNOMBRE_ROL;
                    rol.ROL_VDESCRIPCION_ROL = item.ROL_VDESCRIPCION_ROL;
                    rol.ROL_CACTIVO_ROL = item.ROL_CACTIVO_ROL;
                }
            }
            return View(rol);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var rol = new ROLES()
            {
                ROL_ICODIGO_ROL = Convert.ToInt32(frm["id"]),
                ROL_VNOMBRE_ROL = frm["nombre"],
                ROL_VDESCRIPCION_ROL = frm["descripcion"],
                ROL_CACTIVO_ROL = frm["std"],
                LOG_USER_UPDATE = "USERMOD"
            };
            int std = new BLROLES().usp_Upd_Roles(rol);

            if (std == 1)
            {
                TempData["msg"] = "Actualizado Correctamente";
                TempData["class"] = "alert alert-success";
            }
            else
            {
                TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                TempData["class"] = "alert alert-danger";
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<ROLES> dato)
        {
            foreach (var item in dato)
            {
                var rol = new ROLES()
                {
                    ROL_ICODIGO_ROL = item.ROL_ICODIGO_ROL,
                    ROL_CACTIVO_ROL = "2",
                    LOG_USER_UPDATE = "USERMOD"
                };
                int std = new BLROLES().usp_Upd_estado_Roles(rol);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ROLES.rdlc");

            List<ROLES> lista = new List<ROLES>();
            lista = usp_listar_Roles();

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
            Init(false);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ROLES.rdlc");

            List<ROLES> lista = new List<ROLES>();
            lista = usp_listar_Roles();

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
        public JsonResult Listar()
        {
            Resultado retorno = new Resultado();
            try
            {
                List<DTORol> roles = new List<DTORol>();

                var datos = new BLROLES().usp_listar_Roles();
                datos.ForEach(x =>
                {
                    roles.Add(new DTORol
                    {
                        Codigo = x.ROL_ICODIGO_ROL,
                        Descripcion = x.ROL_VDESCRIPCION_ROL,
                        Estado = x.ROL_CACTIVO_ROL,
                        Nombre = x.ROL_VNOMBRE_ROL
                    });
                });
                retorno.result = 1;
                retorno.data = Json(roles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                /// almacenar el log de errores. Por Implementarlo
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
