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
    public class BLReporteComprobantesBancarios
    {
        public List<BEComprobanteBancario> ReporteComprobanteBancario(DateTime Fini, DateTime Ffin, string oficina,
                                int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion,
                                string estado, string con_Rechazo, string ini_Rech, string fin_Rech,int idBanco
                                )
        {
            return new DAReporteComprobantesBancarios().ReporteComprobanteBancario(Fini, Ffin, oficina,
                                conFechaIngreso,  conFechaConfirmacion,  finiConfirmacion,  ffinConfirmacion,
                                estado,  con_Rechazo,  ini_Rech,  fin_Rech, idBanco
                                );
        }

        public List<BEComprobanteBancario> ReporteComprobanteBancario_Excel(DateTime Fini, DateTime Ffin, string oficina,
                              int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion,
                              string estado, string con_Rechazo, string ini_Rech, string fin_Rech, int idBanco
                              )
        {
            return new DAReporteComprobantesBancarios().ReporteComprobanteBancario_Excel(Fini, Ffin, oficina,
                                conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion,
                                estado, con_Rechazo, ini_Rech, fin_Rech, idBanco
                                );
        }

    }
}
