using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;

namespace Proyect_Apdayc.Controllers
{
    public class CORRELATIVOSController : Base
    {
        //
        // GET: /CORRELATIVOS/
        public static string cod = "";
        public static string cod2 = "";
        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = CorrelativosListarPag(GlobalVars.Global.OWNER ,"", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_NUMBERING> usp_listar_Correlativos()
        {
            return new BLREC_NUMBERING().Get_REC_NUMBERING();
        }

        public JsonResult usp_listar_CorrelativosJson(int skip, int take, int page, int pageSize, string group, string owner, string dato, int st, string serie,int tipo)
        {
            Init();//add sysseg
            decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(off_id)); // si es una oficina administradora deberia de ver todo
            if (opcAdm == 1) // si es 1 es administrador
                off_id = 0; // mando 0 para consultar 

            var lista = CorrelativosListarPag(GlobalVars.Global.OWNER, dato, st,serie, off_id, tipo, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_NUMBERING> CorrelativosListarPag(string owner, string dato, int st,string serie,decimal off_id, int tipo, int pagina, int cantRegxpag)
        {
            return new BLREC_NUMBERING().REC_NUMBERING_Page(owner, dato, st, serie, off_id, tipo, pagina, cantRegxpag);
        }
        
        public JsonResult ListarCorrelativosReciboJson(int skip, int take, int page, int pageSize, string group, string owner, string dato, int? st,string serie)
        {
            Init();
            var lista = ListarCorrelativosReciboPag(GlobalVars.Global.OWNER, dato, st, serie, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_NUMBERING> ListarCorrelativosReciboPag(string owner, string dato, int? st,string serie, int pagina, int cantRegxpag)
        {
            return new BLREC_NUMBERING().ListarCorrelativosRecibo(owner, dato, st,serie, pagina, cantRegxpag);
        }

        public JsonResult ListarCorrelativosNotaCreditoJson(int skip, int take, int page, int pageSize, string group, string dato, string serie, int? st)
        {
            Init();
            var lista = ListarCorrelativosNotaCreditoPag(GlobalVars.Global.OWNER, dato, serie,st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_NUMBERING> ListarCorrelativosNotaCreditoPag(string owner, string dato,string serie, int? st, int pagina, int cantRegxpag)
        {
            return new BLREC_NUMBERING().ListarCorrelativosNotaCredito(owner, dato,serie, st, pagina, cantRegxpag);
        }

        IEnumerable<SelectListItem> listaD1;
        private void listaDivisor1(string idDiv = "")
        {
            List<BEREC_NUMBERING> ListNum = new List<BEREC_NUMBERING>();

            BEREC_NUMBERING ent = new BEREC_NUMBERING();
            ent.DIV_ID = "";
            ent.DIVIDER1 = "Ninguno";
            ListNum.Add(ent);

            BEREC_NUMBERING ent1 = new BEREC_NUMBERING();
            ent1.DIV_ID = "-";
            ent1.DIVIDER1 = "-";
            ListNum.Add(ent1);

            BEREC_NUMBERING ent2 = new BEREC_NUMBERING();
            ent2.DIV_ID = "/";
            ent2.DIVIDER1 = "/";
            ListNum.Add(ent2);

            BEREC_NUMBERING ent3 = new BEREC_NUMBERING();
            ent3.DIV_ID = "_";
            ent3.DIVIDER1 = "_";
            ListNum.Add(ent3);

            listaD1 = ListNum.Select(c => new SelectListItem
            {
                Value = c.DIV_ID,
                Text = c.DIVIDER1
            });

            ViewData["Lista_Div1"] = listaD1;
            ViewData["Lista_Div1"] = new SelectList(listaD1, "Value", "Text", idDiv);
        }

        IEnumerable<SelectListItem> listaPosSer;
        private void listaPosSerie(decimal idDiv = 1)
        {
            List<BEREC_NUMBERING> ListNum = new List<BEREC_NUMBERING>();

            BEREC_NUMBERING ent1 = new BEREC_NUMBERING();
            ent1.DIV_ID = "1";
            ent1.DIVIDER1 = "Inicio";
            ListNum.Add(ent1);

            BEREC_NUMBERING ent3 = new BEREC_NUMBERING();
            ent3.DIV_ID = "2";
            ent3.DIVIDER1 = "Fin";
            ListNum.Add(ent3);

            listaPosSer = ListNum.Select(c => new SelectListItem
            {
                Value = c.DIV_ID,
                Text = c.DIVIDER1
            });

            ViewData["Lista_PosSerie"] = listaPosSer;
            ViewData["Lista_PosSerie"] = new SelectList(listaPosSer, "Value", "Text", idDiv);
        }

        IEnumerable<SelectListItem> listaPosA;
        private void listaPosAno(decimal idDiv = 1)
        {
            List<BEREC_NUMBERING> ListNum = new List<BEREC_NUMBERING>();

            BEREC_NUMBERING ent1 = new BEREC_NUMBERING();
            ent1.DIV_ID = "1";
            ent1.DIVIDER1 = "Inicio";
            ListNum.Add(ent1);

            BEREC_NUMBERING ent3 = new BEREC_NUMBERING();
            ent3.DIV_ID = "2";
            ent3.DIVIDER1 = "Fin";
            ListNum.Add(ent3);

            listaPosA = ListNum.Select(c => new SelectListItem
            {
                Value = c.DIV_ID,
                Text = c.DIVIDER1
            });

            ViewData["Lista_PosAno"] = listaPosA;
            ViewData["Lista_PosAno"] = new SelectList(listaPosA, "Value", "Text", idDiv);
        }

        IEnumerable<SelectListItem> listaFormatA;
        private void listaFormatoA(decimal idDiv = 1)
        {
            List<BEREC_NUMBERING> ListNum = new List<BEREC_NUMBERING>();
            var date = DateTime.Now;

            BEREC_NUMBERING ent1 = new BEREC_NUMBERING();
            ent1.DIV_ID = "4";
            ent1.DIVIDER1 = DateTime.Now.Year.ToString();
            ListNum.Add(ent1);

            BEREC_NUMBERING ent3 = new BEREC_NUMBERING();
            ent3.DIV_ID = "2";
            ent3.DIVIDER1 = String.Format("{0:yy}", date);
            ListNum.Add(ent3);

            listaFormatA = ListNum.Select(c => new SelectListItem
            {
                Value = c.DIV_ID,
                Text = c.DIVIDER1
            });

            ViewData["Lista_FormatoAno"] = listaFormatA;
            ViewData["Lista_FormatoAno"] = new SelectList(listaFormatA, "Value", "Text", idDiv);
        }

        IEnumerable<SelectListItem> listaD2;
        private void listaDivisor2(string idDiv = "")
        {
            List<BEREC_NUMBERING> ListNum = new List<BEREC_NUMBERING>();

            BEREC_NUMBERING ent = new BEREC_NUMBERING();
            ent.DIV_ID = "";
            ent.DIVIDER1 = "Ninguno";
            ListNum.Add(ent);

            BEREC_NUMBERING ent1 = new BEREC_NUMBERING();
            ent1.DIV_ID = "-";
            ent1.DIVIDER1 = "-";
            ListNum.Add(ent1);

            BEREC_NUMBERING ent2 = new BEREC_NUMBERING();
            ent2.DIV_ID = "/";
            ent2.DIVIDER1 = "/";
            ListNum.Add(ent2);

            BEREC_NUMBERING ent3 = new BEREC_NUMBERING();
            ent3.DIV_ID = "_";
            ent3.DIVIDER1 = "_";
            ListNum.Add(ent3);

            listaD2 = ListNum.Select(c => new SelectListItem
            {
                Value = c.DIV_ID,
                Text = c.DIVIDER1
            });

            ViewData["Lista_Div2"] = listaD2;
            ViewData["Lista_Div2"] = new SelectList(listaD2, "Value", "Text", idDiv);
        }

        //REF_DIV_TYPE
        IEnumerable<SelectListItem> lista1;
        private void listaTipoTerritorio(decimal idTerritorio = 604)
        {
            lista1 = new BLTerritorio().Listar_Territorio()
            .Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.TIS_N),
                Text = c.NAME_TER
            });
            ViewData["Lista_TipoTerritorio"] = lista1;
            ViewData["Lista_TipoTerritorio"] = new SelectList(lista1, "Value", "Text", idTerritorio);
        }

        IEnumerable<SelectListItem> listaNum;
        private void listaTipoDocumento(string idTipo = "")
        {
            listaNum = new BLTipoNumerador().TipoDocumento(GlobalVars.Global.OWNER)
            .Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.NMR_TYPE),
                Text = Convert.ToString(c.NMR_TDESC)
            });
            ViewData["Lista_TipoDocumento"] = lista1;
            ViewData["Lista_TipoDocumento"] = new SelectList(listaNum, "Value", "Text", idTipo);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTipoTerritorio();
            listaTipoDocumento();
            listaDivisor1();
            listaDivisor2();
            listaPosSerie();
            listaPosAno();
            listaFormatoA();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREC_NUMBERING en, FormCollection frm)
        {
            var valid = ModelState.IsValid;
            decimal territorio = Convert.ToDecimal(frm["Lista_TipoTerritorio"]);
            string tipoDocumento = Convert.ToString(frm["Lista_TipoDocumento"]);
            string divisor1 = Convert.ToString(frm["Lista_Div1"]);
            string divisor2 = Convert.ToString(frm["Lista_Div2"]);
            decimal posSerie = Convert.ToDecimal(frm["Lista_PosSerie"]);
            decimal posAno = Convert.ToDecimal(frm["Lista_PosAno"]);
            decimal formatoAno = Convert.ToDecimal(frm["Lista_FormatoAno"]);

            string numManual;
            if (string.IsNullOrEmpty(frm["NMR_MANUAL"]))
                numManual = "0";
            else
                numManual = Convert.ToString(frm["NMR_MANUAL"]);

            var estadoAno = Convert.ToString(frm["chkAno"]);
            en.LOG_USER_CREAT = UsuarioActual;
            en.TIS_N = territorio;
            en.NMR_TYPE = tipoDocumento;
            en.NMR_MANUAL = numManual;
            en.DIVIDER1 = divisor1;
            en.DIVIDER2 = divisor2;
            en.POS_SERIAL = posSerie;
            en.POS_YEAR = posAno;
            en.LON_YEAR = formatoAno;

            if (valid == true)
            {
                bool std = new BLREC_NUMBERING().REC_NUMBERING_Ins(en);

                if (std)
                {
                    TempData["flag"] = 1;
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
            listaTipoTerritorio(territorio);
            listaTipoDocumento(tipoDocumento);
            listaDivisor1(divisor1);
            listaDivisor2(divisor2);
            listaPosSerie(posSerie);
            listaPosAno(posAno);
            listaFormatoA(formatoAno);
            frm["NMR_MANUAL"] = numManual;
            return View();

        }

        public JsonResult ObtenerXDescripcion(BEREC_NUMBERING correlativo, FormCollection frm)
        {
            Init();//add sysseg
            Resultado retorno = new Resultado();
            try
            {
                BLREC_NUMBERING servicio = new BLREC_NUMBERING();
                correlativo.OWNER = GlobalVars.Global.OWNER;

                int resultado = servicio.ObtenerXSerie(correlativo);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_NUMBERING correlativo = new BEREC_NUMBERING();
            var lista = new BLREC_NUMBERING().REC_NUMBERING_by_NMR_ID(Convert.ToDecimal(id.ToString().Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    correlativo.OWNER = item.OWNER;
                    correlativo.NMR_ID = item.NMR_ID;
                    correlativo.TIS_N = item.TIS_N;
                    correlativo.NMR_TYPE = item.NMR_TYPE;
                    correlativo.NMR_SERIAL = item.NMR_SERIAL;
                    correlativo.NMR_NAME = item.NMR_NAME;
                    correlativo.W_SERIAL = item.W_SERIAL;
                    correlativo.W_YEAR = item.W_YEAR;
                    correlativo.NMR_FORM = item.NMR_FORM;
                    correlativo.NMR_TO = item.NMR_TO;
                    correlativo.NMR_NOW = item.NMR_NOW;
                    correlativo.AJUST = item.AJUST;
                    correlativo.POS_SERIAL = item.POS_SERIAL;
                    correlativo.LON_YEAR = item.LON_YEAR;
                    correlativo.POS_YEAR = item.POS_YEAR;
                    correlativo.DIVIDER1 = item.DIVIDER1;
                    correlativo.DIVIDER2 = item.DIVIDER2;
                    correlativo.NMR_MANUAL = item.NMR_MANUAL;
                }
            }
            decimal territorio = Convert.ToDecimal(correlativo.TIS_N);
            string tipoDocumento = Convert.ToString(correlativo.NMR_TYPE);
            listaTipoTerritorio(territorio);
            listaTipoDocumento(tipoDocumento);
            listaDivisor1(correlativo.DIVIDER1);
            listaDivisor2(correlativo.DIVIDER2);
            listaPosSerie(correlativo.POS_SERIAL);
            listaPosAno(correlativo.POS_YEAR);
            listaFormatoA(correlativo.LON_YEAR);

            return View(correlativo);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_NUMBERING en, FormCollection frm)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;
            if (en.W_SERIAL == "0")
                en.NMR_SERIAL = "0";

            decimal territorio = Convert.ToDecimal(frm["Lista_TipoTerritorio"]);
            string tipoDocumento = Convert.ToString(frm["Lista_TipoDocumento"]);
            string divisor1 = Convert.ToString(frm["Lista_Div1"]);
            string divisor2 = Convert.ToString(frm["Lista_Div2"]);
            decimal posSerie = Convert.ToDecimal(frm["Lista_PosSerie"]);
            decimal posAno = Convert.ToDecimal(frm["Lista_PosAno"]);
            decimal formatoAno = Convert.ToDecimal(frm["Lista_FormatoAno"]);

            en.TIS_N = territorio;
            en.NMR_TYPE = tipoDocumento;
            en.NMR_TYPE = tipoDocumento;
            en.DIVIDER1 = divisor1;
            en.DIVIDER2 = divisor2;
            en.POS_SERIAL = posSerie;
            en.POS_YEAR = posAno;
            en.LON_YEAR = formatoAno;

            if (valid == true)
            {
                bool std = new BLREC_NUMBERING().REC_NUMBERING_Upd(en);

                if (std)
                {
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
                TempData["class1"] = "alert alert-danger";

            listaTipoTerritorio(territorio);
            listaTipoDocumento(tipoDocumento);
            listaDivisor1(divisor1);
            listaDivisor2(divisor2);
            listaPosSerie(posSerie);
            listaPosAno(posAno);
            listaFormatoA(formatoAno);
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_NUMBERING> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var en = new BEREC_NUMBERING()
                {
                    NMR_ID = item.NMR_ID
                };

                if (en != null)
                {
                    bool std = new BLREC_NUMBERING().REC_NUMBERING_Del(item.NMR_ID);
                }
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_NUMBERING.rdlc");

            List<BEREC_NUMBERING> lista = new List<BEREC_NUMBERING>();
            lista = usp_listar_Correlativos();

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
            Init(false);//add sysseg
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_NUMBERING.rdlc");

            List<BEREC_NUMBERING> lista = new List<BEREC_NUMBERING>();
            lista = usp_listar_Correlativos();

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

        public JsonResult ObtenerNombre(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEREC_NUMBERING correlativo = new BEREC_NUMBERING();
                correlativo = new  BLREC_NUMBERING().ObtenerNombre(GlobalVars.Global.OWNER, id);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.data = Json(correlativo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerNombre", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
