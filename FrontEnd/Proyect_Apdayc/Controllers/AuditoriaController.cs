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

namespace Proyect_Apdayc.Controllers
{
    public class AuditoriaController : Base
    {
        //
        // GET: /Auditoria/
  //      public const string UsuarioActual = "klescano";
      //  public const string nomAplicacion = "SRGDA";
        

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult ListarAuditoria(decimal IdLicencia)
        {
            Resultado retorno = new Resultado();

            try
            {
                BLAuditoria servicio = new BLAuditoria();
                BEAuditoria en = new BEAuditoria();
                en.Auditoria = servicio.ListaAuditoria(GlobalVars.Global.OWNER, IdLicencia);

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblDescuento' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Código</th>");
                    shtml.Append("<th class='k-header'>Fecha</th>");
                    shtml.Append("<th class='k-header'>Auditor</th>");
                    shtml.Append("<th class='k-header'>Observaciones</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (en.Auditoria != null && en.Auditoria.Count > 0)
                    {
                        en.Auditoria.ForEach(c =>
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", c.AUDIT_ID);
                            shtml.AppendFormat("<td >{0}</td>", c.AUDIT_DATE);
                            shtml.AppendFormat("<td >{0}</td>", c.AUDITOR);
                            shtml.AppendFormat("<td >{0}</td>", c.AUDIT_OBSR);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
                    }
                    else
                    {
                        shtml.Append("<tr>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td colspan='6'  style='text-align:center;'> ");
                        shtml.Append("No se encontraron Reportes.");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                    shtml.Append("</tbody></table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAuditoria", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
