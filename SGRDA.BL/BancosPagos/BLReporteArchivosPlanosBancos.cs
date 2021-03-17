using SGRDA.DA.Reporte;
using SGRDA.Entities.BancosPagos;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLReporteArchivosPlanosBancos
    {
        public List<BEArchivosPlanosBancos> ObtenerDatosArchivosPlanosBancos(string fini, string ffin)
        {
            return new DAReporteArchivosPlanosBancos().ObtenerDatosArchivosPlanosBancos(fini, ffin);
        }

        public List<BEArchivosPlanosBancos> ObtenerImporteTotal(string fini, string ffin)
        {
            return new DAReporteArchivosPlanosBancos().ObtenerImporteTotal(fini, ffin);
        }

        public List<BEArchivosPlanosBancos> CargarArchivoPlano( List<BEArchivosPlanosBancos> listaxml, int pagina, int cantRegxPag)
        {

            string xmlTxt = string.Empty;
            xmlTxt = Utility.Util.SerializarEntity(listaxml);
            string owner = GlobalVars.Global.OWNER;
            return new DAReporteArchivosPlanosBancos().CargarArchivoPlano(xmlTxt,  pagina,  cantRegxPag);

        }
        public List<BE_CargaArchivoPlano> ListaBancosPagos()
        {
            return new DAReporteArchivosPlanosBancos().ListaBancosPagos();
            
        }
        public List<BE_CargaArchivoPlano> ListaBancosxCuenta(string nroCuenta)
        {
            return new DAReporteArchivosPlanosBancos().ListaBancosxCuenta(nroCuenta);
           
        }        
        public string CargarPagos(List<BEArchivosPlanosBancos> listaxmlDetalle , int idBanco,int idCuenta,string User, int Id_FileCobro)
        {
            string result = "";
            string xmlDetalle = string.Empty;
            xmlDetalle = Utility.Util.SerializarEntity(listaxmlDetalle);
            string owner = GlobalVars.Global.OWNER;
            using (TransactionScope transa = new TransactionScope())
            {
                result=   new DAReporteArchivosPlanosBancos().CargarPagos(xmlDetalle, idBanco, idCuenta, User, Id_FileCobro);
            transa.Complete();
        }
            return result;
        }


        public List<BEFileBanco> LISTAR_ARCHIVOS_CARGADOS(int bnk_id)
        {
            return new DAReporteArchivosPlanosBancos().LISTAR_ARCHIVOS_CARGADOS(bnk_id);

        }
        
        public int InsertarArchivosCargados(string name_file, int idBank,int cant_cabeceras,decimal montocabecera ,string User,int Id_FileCobro)
        {
            int result = 0;
            using (TransactionScope transa= new TransactionScope())
            {
               result= new DAReporteArchivosPlanosBancos().InsertarArchivosCargados(name_file, idBank, cant_cabeceras, montocabecera, User, Id_FileCobro);
                transa.Complete();
            }
            return result;
        }
        public string InsertarArchivoGenerado(string DESC_ARC, int idBank, string FECHA_INI, string FECHA_FIN, int CANT_ARC,decimal monto, string User)
        {
            return new DAReporteArchivosPlanosBancos().InsertarArchivoGenerado(DESC_ARC, idBank, FECHA_INI, FECHA_FIN, CANT_ARC, monto, User);
        }
       
        public List<BEGenerarArchivo> LISTAR_ArchivosGenerados(int bnk_id, int pagina, int cantRegxPag)
        {
            return new DAReporteArchivosPlanosBancos().LISTAR_ArchivosGenerados(bnk_id, pagina, cantRegxPag);
        }        
        public List<BEFileBanco> LISTAR_ArchivosGenerados_Page(int bnk_id, int pagina, int cantRegxPag)
        {
            return new DAReporteArchivosPlanosBancos().LISTAR_ArchivosGenerados_Page(bnk_id, pagina, cantRegxPag);
        }
        public List<BEGenerarArchivo> CABECERA_ARCHIVO_PLANO_CONTINENTAL(int bnk_id)
        {
            return new DAReporteArchivosPlanosBancos().CABECERA_ARCHIVO_PLANO_CONTINENTAL(bnk_id);
        }      
        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteDepositoBancario_Cobro(int id)
        {
            return new DAReporteArchivosPlanosBancos().ListarReporteDepositoBancario_Cobro(id);
        }
    }
}
