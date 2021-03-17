using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;


namespace SGRDA.BL
{
    public class BLAdministracionCOBRONC
    {
        public List<BEFactura> ListarCOBRONC(decimal CodigoNC, decimal COdigoSerie, int NUmeroDocumento, int CONFECHA, DateTime FechaEmision, decimal CodigoOficina, int Tipo)
        {
            return new DAAdministracionCOBRONC().ListarCOBRONC(CodigoNC, COdigoSerie, NUmeroDocumento, CONFECHA, FechaEmision, CodigoOficina , Tipo);
        }
        public List<BEFactura> ListarDetalleCOBRONC(decimal CodigoNC)
        {
            return new DAAdministracionCOBRONC().ListarDetalleCOBRONC(CodigoNC);
        }
        public List<BEFactura> ListarDetalleCOBRONCFactSeleccionada(decimal CodigoDocumento)
        {
            return new DAAdministracionCOBRONC().ListarDetalleCOBRONCFactSeleccionada(CodigoDocumento);
        }

        public bool InsertarBECNC(decimal CodigoDocumento, decimal CodigoDocumento1, decimal CodigoDocumento2, decimal MontoAplicar, string Usuario)
        {

            bool exitoInserBECNC = false;
            using (TransactionScope transa = new TransactionScope())
            {

                exitoInserBECNC = new DAAdministracionCOBRONC().InsertarBECNC(CodigoDocumento, CodigoDocumento1, CodigoDocumento2, MontoAplicar, Usuario);

                transa.Complete();
            }
            return exitoInserBECNC;

        }

    }
}
