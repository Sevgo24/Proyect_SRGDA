using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public  class BLAdministracionSocio
    {
        public List<BESocioAdministracion> ListaSociosAdministracion(string owner, decimal BPS_ID, string BPS_NAME, string BPS_FIRST_NAME, string BPS_FATH_SURNAME, string BPS_MOTH_SURNAME, string TAX_ID, string LOG_USER_UPDAT, int CON_FECHA_CREA, string FECHA_INI_CREA, string FECHA_FIN_CREA, int CON_FECHA_UPD, string FECHA_INI_UPD, string FECHA_FIN_UPD,decimal LIC_ID,string EST_NAME)
        {
            return new DAAdministracionSocio().ListaSociosAdministracion(owner,BPS_ID, BPS_NAME, BPS_FIRST_NAME, BPS_FATH_SURNAME, BPS_MOTH_SURNAME, TAX_ID, LOG_USER_UPDAT, CON_FECHA_CREA, FECHA_INI_CREA, FECHA_FIN_CREA, CON_FECHA_UPD, FECHA_INI_UPD, FECHA_FIN_UPD,LIC_ID, EST_NAME);
        }

        public int AgruparSocios(decimal SOCIO_ACTIVO ,List<BESocioAdministracion> listasocioInactivar,string LOG_USER_UPDAT, int ACTEST,int ACTLIC)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllistaInactivar = string.Empty; //Se declara la variable xml
            xmllistaInactivar = Utility.Util.SerializarEntity(listasocioInactivar); //se instancia con la lista que hayamos creado
            return new DAAdministracionSocio().AgruparSocios(SOCIO_ACTIVO, xmllistaInactivar, owner, LOG_USER_UPDAT, ACTEST, ACTLIC);
        }

        public int ValidaSocioModif(decimal BPS_ID,string usuario)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionSocio().ValidaSocioModif(BPS_ID,owner, usuario);
        }

        public int ValidaUsuarioModif(string usuario,decimal BPS_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionSocio().ValidaUsuarioModif(usuario, BPS_ID);
        }
        

    }
}
