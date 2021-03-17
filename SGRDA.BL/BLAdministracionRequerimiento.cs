using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionRequerimiento
    {


        public List<BEAdministracionRequerimientos> LISTA(decimal ID_REQ, int TIPO,int ESTAD, int CON_FECHA, string FECHA_INI, string FECHA_FIN, decimal OFICINA, decimal LIC_ID, decimal INV_ID,decimal EST_ID, decimal BEC_ID, int INACT_TYPE)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionRequerimiento().Lista(owner,ID_REQ, TIPO, ESTAD, CON_FECHA, FECHA_INI, FECHA_FIN, OFICINA, LIC_ID, INV_ID, EST_ID,BEC_ID,INACT_TYPE);
        }

        public List<BETipoRequerimiento> listaTipoRequerimiento(int tipo)
        {
            return new DAAdministracionRequerimiento().ListarTipoRequerimiento(tipo);
        }

        public BEAdministracionRequerimientos ObtieneRequerimiento(decimal id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionRequerimiento().ObtieneRequerimientos(owner,id);
        }
        

        public int ActualizaRequerimiento(BEAdministracionRequerimientos ent)
        {
            return new DAAdministracionRequerimiento().ActualizaRequerimiento(ent);
        }

        public int ActualizaRespuestaRequerimiento(decimal ID,string USUARIO)
        {
            return new DAAdministracionRequerimiento().ActualizaRespuestaRequerimiento(ID,USUARIO);
        }
        public decimal RegistraRequerimientoGral(BEAdministracionRequerimientos ent)
        {
            string owner = GlobalVars.Global.OWNER;

            return new DAAdministracionRequerimiento().RegistraRequerimientoGral(ent,owner);
        }

    }
}
