using SGRDA.DA.Alfresco;
using SGRDA.Entities;
using SGRDA.Entities.Alfresco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.BLAlfresco
{
    public class BLAlfresco
    {
        ValidarImagen daValida = new ValidarImagen();
        DAAlfreco da = new DAAlfreco();
        public void File(string ruta,string newFileName)
        {
             da.Upload_Files(ruta, newFileName);
        }
        public string Query_Alfresco(int Tipo)
        {
           return da.Query_Alfresco(Tipo);

        }
        public List<BEDocumentoGral> ListaDocumento(decimal lic_id, string query)
        {
            return da.ListaDocumento(lic_id, query);

        }
        public List<string> Listar_PropiedadesAlfresco_x_TipoDocumento(int TipoDocumento)
        {
            return da.Listar_PropiedadesAlfresco_x_TipoDocumento(TipoDocumento);
        }
        public List<BE_Datos> Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(int TipoDocumento, int Cod_Lic,int Artist_ID)
        {
            return da.Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(TipoDocumento, Cod_Lic, Artist_ID);

        }
        public List<string> Object_TypeId(int TipoDocumento)
        {
            return da.Object_TypeId(TipoDocumento);
        }
        public List<BESelectListItem> Lista_Artista_X_Licencia(int Lic_Id)
        {
            return da.Lista_Artista_X_Licencia(Lic_Id);
        }
        public BESelectListItem ValidarImagen_X_MRECID(string MREC_ID)
        {
            return da.ValidarImagen_X_MRECID(MREC_ID);
        }
        public void Upload_Files_Path(string ruta, decimal lic_id, int CodigoTipoDoc)
        {
            da.Upload_Files_Path(ruta, lic_id, CodigoTipoDoc);
        }
        public int Delete_Files(string cod_alfresco)
        {
           return  da.Delete_Files(cod_alfresco);
        }
        public List<BEMigrarContrato> ListaMigrarDocumentos()
        {
            return da.ListaMigrarDocumentos();
        }
        public void Upload_Files_Path_Migracion(string ruta, decimal lic_id, int CodigoTipoDoc)
        {
            da.Upload_Files_Path_Migracion(ruta, lic_id, CodigoTipoDoc);
        }
        public decimal Obtener_INV_ID_X_MREC_ID(int MREC_ID)
        {
           return da.Obtener_INV_ID_X_MREC_ID(MREC_ID);
        }
       public void Desactivar_Imagen_Cobro(int MREC_ID)
        {
            da.Desactivar_Imagen_Cobro(MREC_ID);
        }

    }
}
