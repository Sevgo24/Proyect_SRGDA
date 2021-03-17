using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionCancelacionDirecta
    {
        public List<BEAdministracionCancelacionDirecta> ListarCancelacionDirecta(decimal CodigoDocumento, decimal CodigoSerie, decimal NumeroDocumento, decimal CodigoSocio,
            decimal Oficina, int ConFecha, DateTime FechaInicio, DateTime FechaFin)
        {
            return new DAAdministracionCancelacionDirecta().ListarSocioCabezeraCobros(CodigoDocumento, CodigoSerie, NumeroDocumento, CodigoSocio, Oficina, ConFecha, FechaInicio, FechaFin);
        }

        public  List<BEAdministracionCancelacionDirecta> ListarTipoCaancelacionDirecta()
        {
            return new DAAdministracionCancelacionDirecta().ListarTipoCancelacionDirecta();
        }

        public BEAdministracionCancelacionDirecta ObtieneDocumento(decimal CodigoDocumento)
        {
            return new DAAdministracionCancelacionDirecta().ObtieneDocumento(CodigoDocumento);
        }

        public List<BEAdministracionCancelacionDirecta> ListarControl()
        {
            return new DAAdministracionCancelacionDirecta().ListarControl();
        }
        
        public decimal RegistrarCancelacionDirecta(BEAdministracionCancelacionDirecta ent)
        {
            return new DAAdministracionCancelacionDirecta().RegistrarCancelacionDirecta(ent,GlobalVars.Global.OWNER);
        }

        public List<BEAdministracionCancelacionDirecta> ListarOficinaCancelacionDirecta(decimal CodigoDoc)
        {
            return new DAAdministracionCancelacionDirecta().ListarOficinaCancelacionDirecta(CodigoDoc);
        }
        
    }
}
