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
    public class BLReporteFacturaCancelada
    {
        public List<BEFacturaCancelada> ListarFacturaCancelada(string fini, string ffin, string oficina
                    , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                    , int? rubro,string parametrosRubro,string ModalidadDetalle)
        {
            return new DAReporteFacturaCancelada().ListarReporteFacturaCancelada(fini, ffin, oficina
                        , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                        , rubro, parametrosRubro, ModalidadDetalle);
        }
        public List<BEFacturaCancelada> ListarFacturaCanceladaEXCEL(string fini, string ffin, string oficina
            , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
            , int? rubro, string parametrosRubro, string tipoenvio,string ModalidadDetalle)
        {
            return new DAReporteFacturaCancelada().ListarReporteFacturaCanceladaEXCEL(fini, ffin, oficina
                        , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                        , rubro, parametrosRubro,  tipoenvio, ModalidadDetalle);
        }

        public List<BEFacturaCancelada> ListarGrupoModXOficina(int? ID_OFF)
        {
            return new DAReporteFacturaCancelada().ListarGrupoModXOficina(ID_OFF);
        }

    }
}
