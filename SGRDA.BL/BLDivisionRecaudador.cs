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
    public class BLDivisionRecaudador
    {
        //public int Insertar(BEDivisionRecaudador recaudador)
        //{            
        //    foreach (var item in recaudador.AgenteRecaudo)
        //    {
        //        recaudador.BPS_ID = item.BPS_ID;
        //        new DADivisionRecaudador().Insertar(recaudador);
        //    }

        //    return 1;
        //}

        //public int Actualizar(BEDivisionRecaudador recaudador, List<BEDivisionRecaudador> agenteEliminar, List<BEDivisionRecaudador> listAgenteActivar)
        //{
        //    DADivisionRecaudador proxyAge = new DADivisionRecaudador();
        //    BEDivisionRecaudador en = new BEDivisionRecaudador();

        //    if (agenteEliminar != null)
        //    {
        //        foreach (var item in agenteEliminar)
        //        {
        //            en.BPS_ID = item.BPS_ID;
        //            en.DAD_ID = recaudador.DAD_ID;
        //            en.OWNER = recaudador.OWNER;
        //            en.LOG_USER_UPDAT = item.LOG_USER_UPDAT;
        //            proxyAge.Eliminar(en);
        //        }
        //    }   

        //    if (listAgenteActivar != null && listAgenteActivar.Count() != 0)
        //    {
        //        listAgenteActivar.ForEach(x => { proxyAge.Activar(recaudador.OWNER, recaudador.DAD_ID, x.BPS_ID, recaudador.LOG_USER_UPDAT); });
        //    }

        //    if (recaudador.AgenteRecaudo.Count > 0)
        //    {
        //        foreach (var item in recaudador.AgenteRecaudo)
        //        {
        //            var obtenerAgente = new DADivisionRecaudador().ObtenerAgenteRecaudo(recaudador.OWNER, recaudador.DAD_ID_ANT, item.BPS_ID);
        //            if (obtenerAgente == null)
        //            {
        //                recaudador.BPS_ID = item.BPS_ID;
        //                new DADivisionRecaudador().Insertar(recaudador);
        //            }
        //        }
        //    }

        //    if (recaudador.DAD_ID != recaudador.DAD_ID_ANT)
        //    {
        //        new DADivisionRecaudador().Actualizar(recaudador);
        //    }

        //    return 1;
        //}

        public BEDivisionRecaudador Obtener(BEDivisionRecaudador recaudador)
        {
            var obj = new DADivisionRecaudador().Obtener(recaudador);
            {
                obj.AgenteRecaudo = new DADivisionRecaudador().ListarAgenteRecaudo(GlobalVars.Global.OWNER, recaudador.DAD_ID);
            }
            return obj;
        }

        //actualiza el ends = gerdate() 
        public int Eliminar(BEDivisionRecaudador recaudador)
        {
            return new DADivisionRecaudador().Eliminar(recaudador);
        }

        public List<SocioNegocio> ListarAgenteRecaudo(BEDivisionRecaudador recaudador)
        {
            return new DADivisionRecaudador().ListarAgenteRecaudo(GlobalVars.Global.OWNER, recaudador.DAD_ID);
        }

        public SocioNegocio ValidarAgenteRecaudo(string owner, decimal Id)
        {
            return new DADivisionRecaudador().ValidarAgenteRecaudo(owner, Id);
        }

        public BEDivisionRecaudador ValidarDivision(string owner, decimal Id)
        {
            return new DADivisionRecaudador().ValidarDivision(owner, Id);
        }
    }
}
