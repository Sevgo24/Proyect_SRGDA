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
    public class BLReporteRecaudacionSedes
    {
        //RECAUDACION
        public List<BEReporteListarRecaudacionSedes> ListarRecaudacionSedes(string ffni, string ffin, int oficina
             , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion)
        {
            return new DAReporeRecaudacionSedes().ListarReporteRecaudacionSedes(ffni, ffin, oficina
                 , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion);
        }

        //CONTABLE
        public List<BEReporteListarRecaudacionSedes> ListarReporteContableRecaudacionSedes(string ffni, string ffin, string oficina, decimal idContable)
        {
            return new DAReporeRecaudacionSedes().ListarReporteContableRecaudacionSedes(ffni, ffin, oficina, idContable);
        }

        public string ObtenerFechaUltActualizacionRepRecaudacionSedes()
        {
            return new DAReporeRecaudacionSedes().ObtenerFechaUltActualizacionRepRecaudacionSedes();
        }


    }
}
