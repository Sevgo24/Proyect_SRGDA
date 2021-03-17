using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers
{
    public class PERIODICIDADTARIFAController : Base
    {
        #region varialbles log
        const string nomAplicacion = "SGRDA";
        //string BancoID = string.Empty;
        #endregion

        //
        // GET: /PERIODICIDADTARIFA/

        #region variables de sesion
        private const string K_SESION_PERIODO = "___DTOPeriodos";
        private const string K_SESION_PERIODO_DEL = "___DTOPeriodosDEL";
        private const string K_SESION_PERIODO_ACT = "___DTOPeriodosACT";
        #endregion

        #region DTO set y get
        List<DTOPeriodo> periodo = new List<DTOPeriodo>();

        private List<DTOPeriodo> PeriodosTmpUPDEstado
        {
            get
            {
                return (List<DTOPeriodo>)Session[K_SESION_PERIODO_ACT];
            }
            set
            {
                Session[K_SESION_PERIODO_ACT] = value;
            }
        }

        private List<DTOPeriodo> PeriodosTmpDelBD
        {
            get
            {
                return (List<DTOPeriodo>)Session[K_SESION_PERIODO_DEL];
            }
            set
            {
                Session[K_SESION_PERIODO_DEL] = value;
            }
        }

        public List<DTOPeriodo> PeriodosTmp
        {
            get
            {
                return (List<DTOPeriodo>)Session[K_SESION_PERIODO];
            }
            set
            {
                Session[K_SESION_PERIODO] = value;
            }
        }
        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public List<BEREC_RATE_FREQUENCY> usp_listar_periocidadtarifa()
        {
            return new BLREC_RATE_FREQUENCY().Get_REC_RATE_FREQUENCY();
        }

        public JsonResult usp_listar_periocidadtarifaJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            var lista = periocidadtarifaListarPag(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_RATE_FREQUENCY { RECRATEFREQUENCY = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_RATE_FREQUENCY { RECRATEFREQUENCY = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_RATE_FREQUENCY> periocidadtarifaListarPag(string owner, string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Page(owner, dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);
            Session.Remove(K_SESION_PERIODO_ACT);
            Session.Remove(K_SESION_PERIODO_DEL);
            Session.Remove(K_SESION_PERIODO);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        [HttpPost]
        public JsonResult Insertar(BEREC_RATE_FREQUENCY periodo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEREC_RATE_FREQUENCY obj = new BEREC_RATE_FREQUENCY();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.RAT_FID = periodo.RAT_FID;
                    obj.RAT_FDESC = periodo.RAT_FDESC;
                    obj.LOG_USER_UPDAT = UsuarioActual;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.PeriodoFrecuencia = obtenerPeriodo();

                    if (obj.RAT_FID == 0)
                    {
                        var datos = new BLREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Ins(obj);
                    }
                    else
                    {
                        List<BEPeriodoFrecuencia> listaPerDel = null;
                        if (PeriodosTmpDelBD != null)
                        {
                            listaPerDel = new List<BEPeriodoFrecuencia>();
                            PeriodosTmpDelBD.ForEach(x => { listaPerDel.Add(new BEPeriodoFrecuencia { RAT_FID = x.Id, FRQ_NPER = x.NroordenPeriodo }); });
                        }

                        List<BEPeriodoFrecuencia> listaPerUpdEst = null;
                        if (PeriodosTmpUPDEstado != null)
                        {
                            listaPerUpdEst = new List<BEPeriodoFrecuencia>();
                            PeriodosTmpUPDEstado.ForEach(x => { listaPerUpdEst.Add(new BEPeriodoFrecuencia { RAT_FID = x.Id, FRQ_NPER = x.NroordenPeriodo }); });
                        }

                        var datos = new BLREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Upd(obj, listaPerDel, listaPerUpdEst);

                    }
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar periodo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Eliminar
        public JsonResult Eliminar(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_RATE_FREQUENCY servicio = new BLREC_RATE_FREQUENCY();
                    var result = servicio.Eliminar(new BEREC_RATE_FREQUENCY
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        RAT_FID = Id,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Tipo uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_RATE_FREQUENCY periodicidad = new BLREC_RATE_FREQUENCY();
                    var periodicidadTarifa = periodicidad.Obtener(id);

                    if (periodicidadTarifa != null)
                    {
                        DTOPeriodicidadTarifa PeriodicidadTarifaDTO = new DTOPeriodicidadTarifa()
                        {
                            Id = periodicidadTarifa.RAT_FID,
                            Descripcion = periodicidadTarifa.RAT_FDESC
                        };

                        periodicidadTarifa.PeriodoFrecuencia = new BLPeriodoFrecuencia().Listar(id, GlobalVars.Global.OWNER);


                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(periodicidadTarifa, JsonRequestBehavior.AllowGet);

                        if (periodicidadTarifa.PeriodoFrecuencia != null)
                        {
                            periodo = new List<DTOPeriodo>();
                            periodicidadTarifa.PeriodoFrecuencia.ForEach(s =>
                            {
                                periodo.Add(new DTOPeriodo
                                {
                                    Id = s.RAT_FID,
                                    NroordenPeriodo = s.FRQ_NPER,
                                    NroordenPeriodoAnt = s.FRQ_NPER_ANT,
                                    NombrePeriodo = s.FRQ_DESC,
                                    NrodiasPeriodo = s.FRQ_DAYS,
                                    Fecha = s.FRQ_DATE,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            PeriodosTmp = periodo;
                        }
                        retorno.data = Json(PeriodicidadTarifaDTO, JsonRequestBehavior.AllowGet);
                        retorno.message = "Periodicidad tarifa encontrado";
                        retorno.result = 1;

                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar periodicidad tarifa";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos periodicidad tarifa", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEPeriodoFrecuencia> obtenerPeriodo()
        {
            List<BEPeriodoFrecuencia> datos = new List<BEPeriodoFrecuencia>();
            if (PeriodosTmp != null)
            {
                PeriodosTmp.ForEach(x =>
                {
                    var obj = new BEPeriodoFrecuencia();
                    obj.RAT_FID = x.Id;
                    obj.FRQ_NPER = x.NroordenPeriodo;
                    obj.FRQ_NPER_ANT = x.NroordenPeriodoAnt;
                    obj.FRQ_DESC = x.NombrePeriodo;
                    obj.FRQ_DAYS = x.NrodiasPeriodo;
                    obj.FRQ_DATE = x.Fecha;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    datos.Add(obj);
                });
            }
            return datos;
        }

        #region Listar
        public JsonResult ListarPeriodos()
        {
            periodo = PeriodosTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' style='display:none' >Id</th>");
                    shtml.Append("<th class='k-header'>Nro orden</th>");
                    shtml.Append("<th class='k-header'>Descripción</th>");
                    shtml.Append("<th class='k-header'>Nro dias</th>");
                    shtml.Append("<th class='k-header'>F. facturación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (periodo != null)
                    {
                        foreach (var item in periodo.OrderBy(x => x.NroordenPeriodo))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.NroordenPeriodo);
                            shtml.AppendFormat("<td >{0}</td>", item.NombrePeriodo);
                            shtml.AppendFormat("<td >{0}</td>", item.NrodiasPeriodo);
                            shtml.AppendFormat("<td >{0}</td>", item.Fecha);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddPeriodo({0},{1});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id, item.NroordenPeriodo);
                            shtml.AppendFormat("<a href=# onclick='delAddPeriodo({0},{1});'> <img src='../Images/iconos/{2}' title='{3}' border=0></a>", item.Id, item.NroordenPeriodo, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Perido" : "Activar Perido");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Agregar
        [HttpPost]
        public JsonResult AddPeriodo(DTOPeriodo entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool validacionNroPeriodo = false;
                    bool validacionNomPeriodo = false;
                    bool validacion = true; periodo = PeriodosTmp;
                    if (periodo == null) periodo = new List<DTOPeriodo>();

                    string mensaje = string.Empty;
                    List<DTOPeriodo> Nroperiodo = new List<DTOPeriodo>();

                    if (entidad.Accion == 0)//nuevo                    
                    {
                        validacionNroPeriodo = (from x in periodo
                                                where x.NroordenPeriodo == entidad.NroordenPeriodo
                                                select x).ToList().Count == 0 ? true : false;
                        validacionNomPeriodo = (from x in periodo
                                                where x.NombrePeriodo.ToUpper() == entidad.NombrePeriodo.ToUpper()
                                                select x).ToList().Count == 0 ? true : false;
                    }
                    else
                    {
                        if (entidad.NroordenPeriodo == entidad.NroordenPeriodoAnt)
                        {
                            validacionNroPeriodo = true;
                            validacionNomPeriodo = (from x in periodo
                                                    where x.NombrePeriodo.ToUpper() == entidad.NombrePeriodo.ToUpper() && x.NroordenPeriodo != entidad.NroordenPeriodo
                                                    select x).ToList().Count == 0 ? true : false;
                        }
                        else                        
                        {
                            validacionNroPeriodo = (from x in periodo
                                                    where x.NroordenPeriodo == entidad.NroordenPeriodo
                                                    select x).ToList().Count == 0 ? true : false;
                            validacionNomPeriodo = (from x in periodo
                                                    where x.NroordenPeriodo != entidad.NroordenPeriodoAnt &&  x.NombrePeriodo.ToUpper() == entidad.NombrePeriodo.ToUpper() 
                                                    select x).ToList().Count == 0 ? true : false;
                        }
                    }


                    if (!validacionNroPeriodo)
                    {
                        mensaje = "El numero de periodo ingresado ya existe" + Environment.NewLine;
                        retorno.message = String.Format("{0}", mensaje);
                        retorno.result = 0;
                        validacion = false;
                    }

                    if (!validacionNomPeriodo)
                    {
                        mensaje += "El nombre de periodo ingresado ya existe";
                        retorno.message = String.Format("{0}", mensaje);
                        retorno.result = 0;
                        validacion = false;
                    }


                    if (validacion)
                    {
                        //var cantReg = periodo.Where(p => p.NroordenPeriodo == entidad.NroordenPeriodo).ToList().Count;
                        //if (Convert.ToInt32(entidad.Id) <= 0)
                        var cantReg = periodo.Where(p => p.NroordenPeriodo == entidad.NroordenPeriodoAnt).ToList().Count;
                        if (cantReg == 0)
                        {
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            periodo.Add(entidad);
                        }
                        else
                        {
                            var item = periodo.Where(x => x.NroordenPeriodo == entidad.NroordenPeriodoAnt).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            entidad.UsuarioModifica = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                            periodo.Remove(item);
                            periodo.Add(entidad);
                        }
                        PeriodosTmp = periodo;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add periodo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Quitar
        [HttpPost]
        public JsonResult DellAddPeriodo(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    periodo = PeriodosTmp;
                    if (periodo != null)
                    {
                        var objDel = periodo.Where(x => x.NroordenPeriodo == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (PeriodosTmpUPDEstado == null) PeriodosTmpUPDEstado = new List<DTOPeriodo>();
                                if (PeriodosTmpDelBD == null) PeriodosTmpDelBD = new List<DTOPeriodo>();

                                var itemUpd = PeriodosTmpUPDEstado.Where(x => x.NroordenPeriodo == id).FirstOrDefault();
                                var itemDel = PeriodosTmpDelBD.Where(x => x.NroordenPeriodo == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) PeriodosTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) PeriodosTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) PeriodosTmpDelBD.Add(objDel);
                                    if (itemUpd != null) PeriodosTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                periodo.Remove(objDel);
                                periodo.Add(objDel);
                            }
                            else
                            {
                                periodo.Remove(objDel);
                            }

                            PeriodosTmp = periodo;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region obtener
        public JsonResult ObtienePeriodoTmp(decimal idPer, decimal? NroOrden)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var periodo = PeriodosTmp.Where(x => x.Id == idPer && x.NroordenPeriodo == NroOrden).FirstOrDefault();
                    retorno.data = Json(periodo, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtienePeriodoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_RATE_FREQUENCY.rdlc");

            List<BEREC_RATE_FREQUENCY> lista = new List<BEREC_RATE_FREQUENCY>();
            lista = usp_listar_periocidadtarifa();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_RATE_FREQUENCY.rdlc");

            List<BEREC_RATE_FREQUENCY> lista = new List<BEREC_RATE_FREQUENCY>();
            lista = usp_listar_periocidadtarifa();

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
