using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.Consulta;
using SGRDA.Entities;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using SGRDA.DA;

namespace SGRDA.BL.Consulta
{
    public class BLConsultaDocumento
    {

        #region CONSULTA_GENRAL
        public List<BEFactura> ListarConsultaDocumento(string owner, decimal idSerial, decimal numFact, decimal idFactura,
                                                                    decimal idSocio, decimal idGrupoFacturacion, decimal idGrupoEmpresarial,
                                                                    int conFecha, DateTime Fini, DateTime Ffin, decimal idLicencia,
                                                                    decimal idDivision, decimal idOficina, decimal idAgente,
                                                                    string idMoneda, decimal tipoDoc, decimal estado,
                                                                    decimal idDepartamento, decimal idProvincia, decimal idDistrito,int estadoSunat,int ORDEN)
        {
            return new DAConsultaDocumento().ListarConsultaDocumento(owner, idSerial, numFact, idFactura,
                                                                     idSocio, idGrupoFacturacion, idGrupoEmpresarial,
                                                                     conFecha, Fini, Ffin, idLicencia,
                                                                     idDivision, idOficina, idAgente,
                                                                     idMoneda, tipoDoc, estado,
                                                                     idDepartamento, idProvincia, idDistrito, estadoSunat, ORDEN);
        }
        public List<BEFactura> ListarConsultaDocumento2(string owner, string idSerial, decimal numFact, decimal idFactura, decimal idOficina,
                                                                   int conFecha, DateTime Fini, DateTime Ffin
                                                                   )
        {
            return new DAConsultaDocumento().ListarConsultaDocumento2(owner, idSerial, numFact, idFactura,  idOficina,
                                                                     conFecha, Fini, Ffin
                                                                     );
        }
        #endregion

        #region CONSULTA_DETALLE
        public List<BEFactura> CD_DETALLE_CABECERA(decimal idFactura)
        {
            return  new DAConsultaDocumento().CD_Cabecera(idFactura);
        }

        public List<BELicencias> CD_DETALLE_LICENCIA(decimal idFactura)
        {
            return new DAConsultaDocumento().CD_Licencia(idFactura);
        }

        public List<BEFacturaDetalle> CD_DETALLE_PERIODOS(decimal idFactura)
        {
            return new DAConsultaDocumento().CD_Periodos(idFactura);
        }

        public List<BEFactura> LICENCIA_DETALLE_FACTURA_X_PERIODO(decimal idPeriodo)
        {
            return new DAConsultaDocumento().LICENCIA_DETALLE_FACTURA_X_PERIODO(idPeriodo);
        }
           

        #endregion

        public int ValidaDocumentoCobro(int INV_ID)
        {
            return new DAConsultaDocumento().ValidaDocumentoCobro(INV_ID);
        }
        public int GuardarNuevaNotaCredito(BENotaCredito dato)
        {
            return new DAFactura().GuardarNuevaNotaCredito(dato);
        }
        public int Valida_Fecha_Factura_Para_NC(int INV_ID)
        {
            return new DAConsultaDocumento().Valida_Fecha_Factura_Para_NC(INV_ID);
        }


        public List<BEFactura> CD_DETALLE_REFERENCIA(decimal idFactura)
        {
            return new DAConsultaDocumento().CD_Referencia(idFactura);
        }

        public List<int> UsuariosAprobadosParaAnular()
        {
            return new DAConsultaDocumento().UsuariosAprobadosParaAnular();
        }

        public List<BEMultiRecibo> CobrosxFactura(decimal CodigoFactura)
        {
            return new DAConsultaDocumento().CobrosxFactura(CodigoFactura);
        }
        public int VALIDAR_ANULACION_X_MODALIDAD(decimal INV_ID, decimal OFF_ID)
        {
            return new DAConsultaDocumento().VALIDAR_ANULACION_X_MODALIDAD(INV_ID, OFF_ID);
        }
   }
}









