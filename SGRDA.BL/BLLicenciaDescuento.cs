using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLLicenciaDescuento
    {
        public List<BEDescuentos> ListaDescuentos(string Owner, decimal idLic )
        {
            //BEDescuentos obj = new BEDescuentos();
            //obj.Descuentos = new DALicenciaDescuento().ListaDescuentos(Owner, IdUsuarioDerecho, IdTarifa);
            //return obj.Descuentos;

            return new DALicenciaDescuento().ListaDescuentos(Owner, idLic   );
        }
        public int Insertar(List<BEDescuentos> entidades)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                entidades.ForEach(item => new DALicenciaDescuento().Insertar(item));
                transa.Complete();
            }

            return 0;
        }
        public int ActualizarAplicable(decimal idLicDes,string owner,string valor,string usuModi)
        {
            
                return new DALicenciaDescuento().ActualizarAplicable(idLicDes,owner,valor,usuModi);
                
        }
        public int Eliminar(decimal idLicDes, string owner,   string usuModi)
        {

            return new DALicenciaDescuento().Eliminar(idLicDes, owner, usuModi);

        }
        //public int Actualizar(BELicenciaReporte entidad)
        //{
        //    return new DALicenciaReporte().Actualizar(entidad);
        //}
        //public int Eliminar(BELicenciaReporte entidad)
        //{
        //    return new DALicenciaReporte().Eliminar(entidad);
        //}
        //public int Activar(string owner, decimal id, string usuModi)
        //{
        //    return new DALicenciaReporte().Activar(owner, id, usuModi);
        //}
        #region Descuentos Socio

        public int InsertarDesAuto(List<BEDescuentos> entidades)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                entidades.ForEach(item => new DALicenciaDescuento().InsertarDescAuto(item));
                transa.Complete();
            }

            return 0;
        }

        public List<BEDescuentos> InsertaDescuentosGRUPOXML(List<BEDescuentos> lista)
        {
            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(lista);
            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaDescuento().InsertaDescuentosGRUPOXML(xmlLicencia);
        }

        //Inserta Desceuntos en las licencias Despues de agregarlas a las licencias Padres
        public int InsertaDescuentosLicenciaXML(List<BELicencias> lista)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllicencia = string.Empty;
            xmllicencia = Utility.Util.SerializarEntity(lista);

            return new DALicenciaDescuento().InsertaDescuentosLicenciaXML(xmllicencia, owner);
        }
        #endregion

        #region Descuentos_Validacion
        public BEDescuentos ObtieneDescuento_DiscLic(string owner,decimal LIC_DISC_ID)
        {
            return new DALicenciaDescuento().ObtieneDescuento_DiscLic(owner, LIC_DISC_ID);
        }

        public void ActualizaDescuentoLICencia(decimal LIC_DISC_ID,string NombDesc, decimal MontDesc,string usuarioActual)
        {
            string OWNER = GlobalVars.Global.OWNER;
            new DALicenciaDescuento().ActualizaDescuentoLicencia(OWNER, LIC_DISC_ID, NombDesc, MontDesc, usuarioActual);
        }

        public List<BEDescuentos> listaDescuentosxAprobar(int pagina, int cantRegxPag, decimal LIC_ID, string NOM_LIC, int OFI_ID, int EST_DESC, string NOM_DESC, int CON_FECHA, DateTime FECHA_INICIO, DateTime FECHA_FIN)
        {

            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaDescuento().ListaDescuentosxAprobar( pagina, cantRegxPag, owner, LIC_ID, NOM_LIC, OFI_ID, EST_DESC, NOM_DESC, CON_FECHA, FECHA_INICIO, FECHA_FIN);
        }

        public BEDescuentos ObtieneDescuento_Panel(string owner, decimal DISC_ID,decimal LIC_ID)
        {
            return new DALicenciaDescuento().ObtieneDescuento_Panel(owner, DISC_ID, LIC_ID);
        }

        public void ActualizaDescuentoLicencia_Panel(decimal DISC_ID, int estado, string observ_respuesta ,decimal LIC_ID)
        {
            string OWNER = GlobalVars.Global.OWNER;
            new DALicenciaDescuento().ActualizaDescuentoLicencia_Panel(OWNER, DISC_ID, estado, observ_respuesta, LIC_ID);
        }

        public int ActualizaDescuentoLicenciaCalc(decimal LIC_ID)
        {
            return new DALicenciaDescuento().ActualizaDescuentoLicenciaCalc( LIC_ID);
        }
        #endregion

    }
}
