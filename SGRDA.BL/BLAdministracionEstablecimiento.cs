using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SGRDA.DA;
using SGRDA.Entities;
namespace SGRDA.BL
{
    public class BLAdministracionEstablecimiento
    {
        public List<BEAdministracionEstablecimiento> ListaEstablecimientosAdministracion(string owner, decimal EST_ID, string BPS_NAME, string BPS_FIRST_NAME, string BPS_FATH_SURNAME, string BPS_MOTH_SURNAME, string TAX_ID, string LOG_USER_UPDAT, int CON_FECHA_CREA, string FECHA_INI_CREA, string FECHA_FIN_CREA, int CON_FECHA_UPD, string FECHA_INI_UPD, string FECHA_FIN_UPD, decimal LIC_ID, string EST_NAME, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID)
        {
            return new DAAdministracionEstablecimiento().ListaEstablecimientosAdministracion(owner, EST_ID, BPS_NAME, BPS_FIRST_NAME, BPS_FATH_SURNAME, BPS_MOTH_SURNAME, TAX_ID, LOG_USER_UPDAT, CON_FECHA_CREA, FECHA_INI_CREA, FECHA_FIN_CREA, CON_FECHA_UPD, FECHA_INI_UPD, FECHA_FIN_UPD, LIC_ID, EST_NAME, DIV_ID, DEP_ID, PROV_ID, DIST_ID);
        }


        public int AgruparEstablecimientos(decimal EST_ID, List<BEAdministracionEstablecimiento> listaestab, string LOG_USER_UPDAT, int ACTEST, int ACTLIC)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllistaInactivar = string.Empty; //Se declara la variable xml
            xmllistaInactivar = Utility.Util.SerializarEntity(listaestab); //se instancia con la lista que hayamos creado
            return new DAAdministracionEstablecimiento().AgruparEstablecimientos(EST_ID, xmllistaInactivar, owner, LOG_USER_UPDAT, ACTEST, ACTLIC);
        }



        public int ValidaEstablecimientoModif( string usuario,decimal EST_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionEstablecimiento().ValidaEstablecimientosModif(EST_ID, owner, usuario);
        }

        public int ModificaEstablecimientosporSocioSeleccionado(List<BEAdministracionEstablecimiento> lista,string usuario,decimal CodigoSOcio)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllista = string.Empty; //Se declara la variable xml
            xmllista = Utility.Util.SerializarEntity(lista); //se instancia con la lista que hayamos creado
            return new DAAdministracionEstablecimiento().ModificaEstablecimientosporSocioSeleccionado(xmllista, owner,usuario, CodigoSOcio);
        }
    }
}
