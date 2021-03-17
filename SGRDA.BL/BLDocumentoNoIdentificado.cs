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
    public class BLDocumentoNoIdentificado
    {
        public int InsertarDNI(BEDocumentoNoIdentificado obj)
        {
            // FACTURA - CABECERA
            obj.INV_ID = new DADocumentoNoIdentificado().InsertarFacturaCabecera(obj);
            // FACTURA - DETALLE
            obj.INVL_ID = new DADocumentoNoIdentificado().InsertarFacturaDetalle(obj);

            //COBRO - CABECERA
            obj.MREC_ID = new DADocumentoNoIdentificado().InsertarCobroCabecera(obj);
            //RECIBO - CABECERA
            obj.REC_ID = new DADocumentoNoIdentificado().InsertarReciboCabecera(obj);
            //RECIBO - DETALLE
            obj.REC_DID = new DADocumentoNoIdentificado().InsertarReciboDetalle(obj);
            //COBRO - DETALLE
            var result = new DADocumentoNoIdentificado().InsertarCobroDetalle(obj);

            //DEPOSITO
            obj.REC_PID = new DADocumentoNoIdentificado().InsertarDeposito(obj);
            //TRANSACCION
            obj.REC_PID = new DADocumentoNoIdentificado().InsertaTransaccionRecaudo(obj);

            return 1;
        }

        public List<BEDocumentoNoIdentificado> ListarDNI(decimal bnk_id, string fecha_ini, string fecha_fin, int estado)
        {
            return new DADocumentoNoIdentificado().ListarDNI(bnk_id, fecha_ini, fecha_fin, estado);
        }
        public List<BEDocumentoNoIdentificado> ListarDNI_EXCEL(decimal bnk_id, string fecha_ini, string fecha_fin, int estado)
        {
            return new DADocumentoNoIdentificado().ListarDNI_EXCEL(bnk_id, fecha_ini, fecha_fin, estado);
        }
        

        public int EliminarDNI(decimal id)
        {
            // FACTURA - CABECERA
            int result = new DADocumentoNoIdentificado().EliminarDNI(id);
            return result;
        }

        public int Validar_DNI(BEDocumentoNoIdentificado obj)
        {
            int result = new DADocumentoNoIdentificado().Validar_DNI(obj);
            return result;
        }


    }
}
