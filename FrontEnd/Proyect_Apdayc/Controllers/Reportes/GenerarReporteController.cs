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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class GenerarReporteController : Base
    {
        //
        // GET: /GenerarReporte/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public void RegistroVentaRpt()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool isValid = true;

                    string strNombreReporte = System.Web.HttpContext.Current.Session["NombreReporte"].ToString();    // Setting ReportName 
                    string strFechaInicio = System.Web.HttpContext.Current.Session["FechaInicio"].ToString();     // Setting FromDate  
                    string strFechaFin = System.Web.HttpContext.Current.Session["FechaFin"].ToString();         // Setting ToDate     
                    string strOficina = System.Web.HttpContext.Current.Session["Oficina"].ToString();         // Setting ToDate     
                    var rptListarReporte = System.Web.HttpContext.Current.Session["ListaReporte"];

                    if (string.IsNullOrEmpty(strNombreReporte))
                    {
                        isValid = false;
                    }

                    if (isValid)
                    {
                        ReportDocument rd = new ReportDocument();

                        //string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes//" + strNombreReporte;
                        string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/" + strNombreReporte);
                        rd.Load(strRptPath);
                        if (rptListarReporte != null && rptListarReporte.GetType().ToString() != "System.String")
                            rd.SetDataSource(rptListarReporte);

                        if (!string.IsNullOrEmpty(strFechaInicio))
                            rd.SetParameterValue("@F1", strFechaInicio);

                        if (!string.IsNullOrEmpty(strFechaFin))
                            rd.SetParameterValue("@F2", strFechaFin);

                        if (!string.IsNullOrEmpty(strFechaFin))
                            rd.SetParameterValue("@OFICINA", strOficina);

                        rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "RegistroVenta");

                        // Clear all sessions value 
                        Session["ReportName"] = null;
                        Session["rptFromDate"] = null;
                        Session["rptToDate"] = null;
                        Session["rptSource"] = null;
                    }
                    else
                    {
                        Response.Write("<H2>Nothing Found; No Report name found</H2>");
                    } 
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "RegistroVentaRpt", ex);
            }
        }

    }
}
