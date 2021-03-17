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
    public class BLProceso
    {
        public List<BEProceso> ListarProcesoXEstado(decimal idModulo, decimal idEstado, string owner,decimal idWrkf,decimal idWrkfRef,bool isManual)
        {
            return new DAProceso().ListarProcesoXEstado(idModulo, idEstado, owner, idWrkf, idWrkfRef, isManual);
        }

        public List<BEProceso> usp_Get_ProcesoPage(string owner, string dato, decimal tipo, decimal ciclo, decimal cliente, int st, int pagina, int cantRegxPag)
        {
            return new DAProceso().usp_Get_ProcesoPage(owner, dato, tipo, ciclo, cliente, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEProceso en)
        {
            return new DAProceso().Eliminar(en);
        }

        public BEProceso Obtener(string owner, decimal id)
        {
            return new DAProceso().Obtener(owner, id);
        }

        public int Insertar(BEProceso en)
        {
            return new DAProceso().Insertar(en);
        }

        public int Actualizar(BEProceso en)
        {
            return new DAProceso().Actualizar(en);
        }

        public List<BEProceso> ListarProceso(string owner)
        {
            return new DAProceso().ListarProceso(owner);
        }


        public List<BEProceso> ListarItem(string owner)
        {
            return new DAProceso().ListarItem(owner);
        }
        /// <summary>
        /// funcion de prueba eliminar luegoo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SocioNegocio ObtenerSocio(decimal id)
        {
            return new DAProceso().ObtenerSocio(id);
        }
    }
}
