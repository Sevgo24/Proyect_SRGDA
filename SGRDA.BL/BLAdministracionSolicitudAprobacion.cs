using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace SGRDA.BL
{
   public class BLAdministracionSolicitudAprobacion
    {
        #region SOLICITUD DE APROBACION

        public int SolicitudAprobacionDocumento(decimal INV_ID, int TIPO, string DESCRIPCION, string usuario,int RESPT)
        {
            return new DAAdministracionSolicitudAprobacion().SolicitudAprobacionDocumento(INV_ID, TIPO, DESCRIPCION, usuario, RESPT);
        }

        public List<BEAdministracionSolicitudAprobacion> Lista(decimal INV_ID, decimal SERIE, decimal INV_NUMBER, int CONFECHA, string FECHA_INI, string FECHA_FIN, decimal OFF_ID, int ESTADO_APROB, int TIPO)
        {
            return new DAAdministracionSolicitudAprobacion().listar(INV_ID, SERIE, INV_NUMBER, CONFECHA, FECHA_INI, FECHA_FIN, OFF_ID, ESTADO_APROB, TIPO);
        }

        public BEAdministracionSolicitudAprobacion ObtieneSOlicitudAprobacion(decimal inv_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAdministracionSolicitudAprobacion().ObtieneSOlicitudAprobacion(inv_id, owner);
        }

        public List<BEAdministracionSolicitudAprobacion> ListarTipoSolicitud()
        {
            return new DAAdministracionSolicitudAprobacion().ListarTipoSolicitud();
        }
        
        #endregion
    }
}
