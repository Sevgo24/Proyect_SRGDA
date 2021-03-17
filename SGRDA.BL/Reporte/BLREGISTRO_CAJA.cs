using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System.Transactions;

namespace SGRDA.BL.Reporte
{
    public class BLREGISTRO_CAJA
    {
        public List<BEREPORTE_CAJA> ReporteRegistroCaja(string fini, string ffin, string oficina, int? rubro)
        {
            return new DAREPORTE_CAJA().ListarReporteCaja(fini, ffin, oficina, rubro);
        }
        //
        public int ValidarReporteOficina(int oficina)
        {
            return new DAREPORTE_CAJA().ValidarReporteOficina(oficina);
        }
        //
        public int ValidarReporteOficinaDL(int oficina)
        {
            return new DAREPORTE_CAJA().ValidarReporteOficinaDL(oficina);
        }

        //RECAUDACION
        public List<BEREPORTE_CAJA> ReporteRegistroCaja_Cobros(string fini, string ffin, string oficina
                    , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                    , int? rubro, string TipoCobro)
                    //, string rubros)
        {
            return new DAREPORTE_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, oficina
                , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                //, rubros);
                , rubro, TipoCobro);
        }

        //CONTABLE
        public List<BEREPORTE_CAJA> ReporteContableRegistroCaja(string fini, string ffin, string oficina        , int? rubro, decimal idContable)
        {
            return new DAREPORTE_CAJA().ReporteContableRegistroCaja(fini, ffin, oficina, idContable,rubro);
        }
        public List<BE_TipoCobro> ListarTipoCobro()
        {
            return new DAREPORTE_CAJA().ListarTipoCobro();
        }


    }
}
