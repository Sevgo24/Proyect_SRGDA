using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using SGRDA.BL;
using SGRDA.DA.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.BancosPagos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
using SGRDA.Entities.Reporte;
using Microsoft.Reporting.WebForms;

namespace Proyect_Apdayc.Controllers.MetodoPago
{
    public class CargarArchivoPlanoController : Base
    {
        private const string K_SESSION_DOCUMENTO = "___DTODocumento";
        // GET: CargarArchivoPlano
        public class VARIABLES
        {
            public const string MSG_ERROR_CUENTA = "EL ARCHIVO NO PERTENECE AL BANCO SELECCIONADO";
            public const string MSG_ERROR_SELECCIONE_BANCO = "POR FAVOR SELECCIONE UN ARCHIVO";
            public const string MSG_OK = "SE CARGO EXITOSAMENTE";
            public const string MSG_error = "NO SE AH PODIDO CARGAR EL ARCHIVO POR FAVOR COMUNIQUESE CON EL ADMINISTRADOR";
            public const string MSG_ARCHIVO_EXISTENTE = "EL ARCHIVO SELECCIONADO YA AH SIDO CARGADO AL SISTEMA";
            public const int All = 99999;
            public const String BCP_CUENTA1 = "001102410100013032";
            public const decimal VarCero = 0;

        }

        private static string Cuenta; 
        private static int BNK_ID; 
        private static int BNK_ID_CUENTA;
        private static string NombreArchivo;
        private static string Nombrebanco;
        private static List<BEArchivosPlanosBancos> VerImporteTmp;
        private static List<BEArchivosPlanosBancos> VerListaDetalleTmpTodos;
        private static List<BEArchivosPlanosBancos> VerCabeceraTmp;
        private static int Id_FileCobroCabecera = 0;
        public static List<BEREPORTE_DEPOSITO_BANCO> ListaReporteDepositoBancoTmp;

        public List<BEArchivosPlanosBancos> VerListaPagosTmp
        {
            get
            {
                return (List<BEArchivosPlanosBancos>)Session[K_SESSION_DOCUMENTO];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO] = value;
            }
        }
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_DOCUMENTO);
            Init(false);
            return View();
        }
        public JsonResult Cargar(HttpPostedFileBase archivoTXT)
         {
            //List<BEArchivosPlanosBancos> listatmp = new List<BEArchivosPlanosBancos>();
            Resultado retorno = new Resultado();
            try
            {
                StreamReader objReader = new StreamReader(archivoTXT.InputStream);
                string sLine = "";
                NombreArchivo = archivoTXT.FileName;
                //string Cabecera = "";
              
                List<BEArchivosPlanosBancos> listaArchivoPlano = new List<BEArchivosPlanosBancos>();
                
                while ((sLine = objReader.ReadLine()) !=null)
                {
                    BEArchivosPlanosBancos archivo = new BEArchivosPlanosBancos();
                    archivo.TXT = sLine;             

                    listaArchivoPlano.Add(archivo);
                }
                VerListaPagosTmp = listaArchivoPlano;
                if (VerListaPagosTmp!=null)
                {

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                //lista = USP_ARCHIVO_LISTARPAGE(page, pageSize).Where(x => x.TR == "02").ToList();
                
                //cabecera = USP_ARCHIVO_LISTARPAGE(page, pageSize).Where(x => x.TR == "01").ToList();
                //var cuenta = cabecera.Select(x => x.CUENTA_DESTINO).Take(1).ToList();
                objReader.Close();
                

            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Se produjo un error : " + ex.Message;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        IEnumerable<SelectListItem> itemTipoBanco;
        public JsonResult ListaBancosPagos()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemTipoBanco = new BLReporteArchivosPlanosBancos().ListaBancosPagos()
                    .Select(c => new SelectListItem
                    {
                        Value = c.VALUE,
                        Text = c.VDESC
                    });

                    retorno.result = 1;
                    retorno.data = Json(itemTipoBanco, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SRGDA", UsuarioActual, "ListarBanco", ex);
            }
           
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult USP_ARCHIVO_LISTARPAGEJSON(int skip, int take, int page, int pageSize)
        {
            Resultado retorno = new Resultado();
            List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();
            //List<BEArchivosPlanosBancos> Listacabecera = new List<BEArchivosPlanosBancos>();

            try
            {
                //lista = USP_ARCHIVO_LISTARPAGE( page, pageSize);
                VerListaDetalleTmpTodos = USP_ARCHIVO_LISTARPAGE(page, VARIABLES.All).Where(x => x.TR == "02").ToList();
                lista = USP_ARCHIVO_LISTARPAGE(page, pageSize).Where(x => x.TR == "02").ToList();
                VerCabeceraTmp = USP_ARCHIVO_LISTARPAGE(page, pageSize).Where(x => x.TR == "01").ToList();
                VerImporteTmp = USP_ARCHIVO_LISTARPAGE(page, pageSize).Where(x => x.TR == "03").ToList();

            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR ARCHIVOS", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                
                return Json(new BEArchivosPlanosBancos { ListaArchivosPlanos = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {              
                return Json(new BEArchivosPlanosBancos { ListaArchivosPlanos = lista, TotalVirtual = Convert.ToInt32(tot[0])-2 }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<BEArchivosPlanosBancos> USP_ARCHIVO_LISTARPAGE(int pagina, int cantRegxPag)
        {
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
            return new BLReporteArchivosPlanosBancos().CargarArchivoPlano(VerListaPagosTmp, pagina, cantRegxPag);
        }
        public JsonResult ValidarArchivo(int id)
        {
            //var label = "";           
            List<BEFileBanco> listFile = new List<BEFileBanco>();
            string User = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Nombre]);
            listFile = new BLReporteArchivosPlanosBancos().LISTAR_ARCHIVOS_CARGADOS(id);
            Resultado retorno = new Resultado();
            
            try
            {
                if(listFile.Exists(x => x.DESC_FILE == NombreArchivo))
                {
                    retorno.result = 0;
                    retorno.message = VARIABLES.MSG_ARCHIVO_EXISTENTE;
                }
                else
                {
                    var CountCabeceras = VerListaDetalleTmpTodos.GroupBy(x => x.NRO_MOVIMIENTO).Count();
                    decimal MontoCabeceras = 0;
                    foreach (var i in VerListaDetalleTmpTodos)
                    {
                        MontoCabeceras += Convert.ToDecimal(i.IMPORTE_DEPOSITADO);
                    }
                    retorno.result = 1;
                    int Id_FileCobro = new BLReporteArchivosPlanosBancos().InsertarArchivosCargados(NombreArchivo, id, CountCabeceras, MontoCabeceras, User, 0);
                    Id_FileCobroCabecera = Id_FileCobro;
                    retorno.valor = Cuenta + '-' + Nombrebanco;
                }
                         
                  
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene la cuenta de banco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidarBanco(int id)
        {
            //var label = "";
            decimal id_bancoStore;

            List<BE_CargaArchivoPlano> lista = new List<BE_CargaArchivoPlano>();

            Resultado retorno = new Resultado();
            try
            {
                if (VerListaPagosTmp != null)
                {
                    Cuenta = VerListaPagosTmp[0].TXT.Substring(28, 18).TrimStart('0');                

                lista = new BLReporteArchivosPlanosBancos().ListaBancosxCuenta(Cuenta);
                if (lista.Count() > 0)
                {
                    id_bancoStore = lista[0].BNK_ID;

                }
                else
                {
                    id_bancoStore = VARIABLES.VarCero;
                }
                if (id_bancoStore == id)
                {
                        if (!isLogout(ref retorno))
                        {
                            //BCP_= 27 
                             BNK_ID = Convert.ToInt32(lista[0].BNK_ID); 
                             BNK_ID_CUENTA = Convert.ToInt32(lista[0].BPS_ACC_ID); 
                            Nombrebanco = lista[0].BNK_NAME;
                            retorno.valor = Cuenta + '-' + Nombrebanco;

                            retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        //retorno.data = Json(retorno, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                     {
                    retorno.result = 0;
                    retorno.message = VARIABLES.MSG_ERROR_CUENTA;
                     }
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = VARIABLES.MSG_ERROR_SELECCIONE_BANCO;
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene la cuenta de banco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarCobrosDeBanco(HttpPostedFileBase archivoTXT)
        {
            Resultado retorno = new Resultado();

            string carga = "";
            try
            {
               string User =Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Nombre]);
                 
                carga = new BLReporteArchivosPlanosBancos().CargarPagos(VerListaDetalleTmpTodos, BNK_ID,BNK_ID_CUENTA, User, Id_FileCobroCabecera);
                if (carga != "n")
                {
                    retorno.result = 1;
                    retorno.message = VARIABLES.MSG_OK;
                }else
                {
                    retorno.result = 0;
                    retorno.message = VARIABLES.MSG_error;
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Se produjo un error : " + ex.Message;
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult USP_ARCHIVOS_CARGADOS_LISTARPAGEJSON(int skip, int take, int page, int pageSize)
        {
            Resultado retorno = new Resultado();
            List<BEFileBanco> lista = new List<BEFileBanco>();
            lista = USP_ARCHIVOS_CARGADOS_LISTARPAGE(page, pageSize);
            //List<BEArchivosPlanosBancos> Listacabecera = new List<BEArchivosPlanosBancos>();
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            try
            {
                lista = USP_ARCHIVOS_CARGADOS_LISTARPAGE(page, pageSize);
                //lista = USP_ARCHIVO_LISTARPAGE( page, pageSize);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR ARCHIVOS", ex);
            }

            if (tot.Count == 0)
            {

                return Json(new BEFileBanco { ListaFileBanco = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new BEFileBanco { ListaFileBanco = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }

        }
        public List<BEFileBanco> USP_ARCHIVOS_CARGADOS_LISTARPAGE(int pagina, int cantRegxPag)
        {
           
            return new BLReporteArchivosPlanosBancos().LISTAR_ArchivosGenerados_Page(27, pagina, cantRegxPag);
        }
        public ActionResult ReporteBanco(int id,string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina)
        {
            string format = formato;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
           
            try
            {

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_BANCOS_FILE.rdlc");

                List<BEREPORTE_DEPOSITO_BANCO> lstReporte = new List<BEREPORTE_DEPOSITO_BANCO>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteDepositoBancoTmp;
                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    //ReportParameter parametroFecha1 = new ReportParameter();
                    //parametroFecha1 = new ReportParameter("FechaInicio", fini.ToString());
                    //localReport.SetParameters(parametroFecha1);

                    //ReportParameter parametroFecha2 = new ReportParameter();
                    //parametroFecha2 = new ReportParameter("FechaFin", ffin.ToString());
                    //localReport.SetParameters(parametroFecha2);

                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);
                  

                    string reportType = format;
                    string mimeType;
                    string encoding;

                    //aqui le cambie solo dejar string fileNameExtension en caso de error
                    string fileNameExtension;

                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                           "  <OutputFormat>" + format + "</OutputFormat>" +
                           "  <PageWidth>8.7in</PageWidth>" +
                           "  <PageHeight>11in</PageHeight>" +
                           "  <MarginTop>0.3in</MarginTop>" +
                           "  <MarginLeft>0.2in</MarginLeft>" +
                           //"  <MarginRight>0.5in</MarginRight>" +
                           "  <MarginRight>0.2in</MarginRight>" +
                           "  <MarginBottom>0.5in</MarginBottom>" +
                           "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte Bancos";

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
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Registro de Venta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        //****************************REPORTE*******************************
        //PARAMETROS FECHA INICIAL,FECHA FINAL,FORMATO DE EL ARCHIV,EL RUBRO Y EL ID DE LA OFICINA , NOMBRE DE LA OFICINA
        public ActionResult ReporteTipo(int id,string formato)
        {           
            Resultado retorno = new Resultado();
            //string format = formato;
            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                List<BEREPORTE_DEPOSITO_BANCO> listar = new List<BEREPORTE_DEPOSITO_BANCO>();
                listar = new BLReporteArchivosPlanosBancos().ListarReporteDepositoBancario_Cobro(id);
                ListaReporteDepositoBancoTmp = new List<BEREPORTE_DEPOSITO_BANCO>();
                ListaReporteDepositoBancoTmp = listar;
                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Diario de Caja", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }

}