using Proyect_Apdayc.Clases;
using SGRDA.DA.Reporte;
using SGRDA.Entities.BancosPagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using SGRDA.BL;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ArchivosPlanosBancosController : Base
    {
        private const string K_SESSION_ARCHIVOS_PLANOS_BANCOS = "___K_SESSION_ARCHIVOS_PLANOS_BANCOS ";
        //private static List<BEArchivosPlanosBancos> ListaArchivosTmp;
        // GET: ArchivosPlanosBancos
        public class VARIABLES
        {
           
            public const string MSG_ERROR_Archivo = "No se encontró registro para este archivo.";


        }
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_ARCHIVOS_PLANOS_BANCOS);
            Init(false);
            return View();
        }     
       
        public FileResult DescargarArchivoPlanoBanco(string fini,string ffin)
        {
            BLReporteArchivosPlanosBancos bl = new BLReporteArchivosPlanosBancos();
            List<BEArchivosPlanosBancos> ListaArchivosTmp = new List<BEArchivosPlanosBancos>();
            ListaArchivosTmp = bl.ObtenerDatosArchivosPlanosBancos(fini, ffin);
            List<BEArchivosPlanosBancos> ListaImporteTmp = new List<BEArchivosPlanosBancos>();
              ListaImporteTmp = bl.ObtenerImporteTotal(fini, ffin);          
            
            Int32 numeroItems = ListaArchivosTmp.Count();
            StringWriter sw = new StringWriter();
           
            List<BEGenerarArchivo> LISTA_PARA_VERSION = new List<BEGenerarArchivo>();
            LISTA_PARA_VERSION = new BLReporteArchivosPlanosBancos().CABECERA_ARCHIVO_PLANO_CONTINENTAL(27);           
            string cabecera = LISTA_PARA_VERSION[0].DESC_ARC;
            using (sw)
            {
                sw.WriteLine(cabecera);
                foreach (var item in ListaArchivosTmp)
                {
                   
                    sw.WriteLine(item.TR);
                }
                foreach (var item in ListaImporteTmp)
                {

                    sw.WriteLine(item.IMPORTE_TOTAL);
                }

            }
            string monto =ListaImporteTmp[0].IMPORTE_TOTAL.Substring(12,16)+'.'+ ListaImporteTmp[0].IMPORTE_TOTAL.Substring(28, 2);          
            string User = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Nombre]);
            decimal m = Convert.ToDecimal(monto);
            int count = ListaArchivosTmp.Count();
            String contenido = sw.ToString();
            String NombreArchivo = "Archivo_Continental" + DateTime.Now;
            if (count > 0)
            {
                new BLReporteArchivosPlanosBancos().InsertarArchivoGenerado(NombreArchivo, 27, fini, ffin, count, m, User);
            }
            String ExtensionArchivo = "txt";
            return File(new System.Text.UTF8Encoding().GetBytes(contenido),
                "text/" + ExtensionArchivo, NombreArchivo + "." +
                ExtensionArchivo);
        }

        public JsonResult ValidarData(string fini, string ffin)
        {
            Resultado retorno = new Resultado();
            BLReporteArchivosPlanosBancos bl = new BLReporteArchivosPlanosBancos();
            List<BEArchivosPlanosBancos> ListaArchivosTmp = new List<BEArchivosPlanosBancos>();
            ListaArchivosTmp = bl.ObtenerDatosArchivosPlanosBancos(fini, ffin);
            //ListaArchivosTmp.Add(bl.ObtenerDatosArchivosPlanosBancos(fini, ffin).FirstOrDefault());
            if (ListaArchivosTmp.Count() == 0)
            {
                retorno.result = 0;
                retorno.message = VARIABLES.MSG_ERROR_Archivo;
            }
            else
            {
                retorno.result = 1;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
           
        }     
        public List<BEGenerarArchivo> USP_GenerarArchivo_LISTARPAGE(int bnk_id,int pagina, int cantRegxPag)
        {

            return new BLReporteArchivosPlanosBancos().LISTAR_ArchivosGenerados(bnk_id, pagina, cantRegxPag);
        }
        public JsonResult USP_GENERAR_ARCHIVO_LISTARPAGEJSON(int skip, int take, int page, int pageSize)
        {
            Resultado retorno = new Resultado();
            List<BEGenerarArchivo> lista = new List<BEGenerarArchivo>();
            int bnk_id = 27;
            try
            {
                lista = USP_GenerarArchivo_LISTARPAGE(bnk_id,page, pageSize);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR ARCHIVOS", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {

                return Json(new BEGenerarArchivo { ListaGenerarArchivo = lista, CANT_ARC = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEGenerarArchivo { ListaGenerarArchivo = lista, TotalVirtual = Convert.ToInt32(tot[0])}, JsonRequestBehavior.AllowGet);

            }
        }
    }
}