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
    public class BLTrasladoAgentesRecaudo
    {
        public List<SocioNegocio> BUSCAR_RECAUDADORES_X_NOMBRE(string Owner, string datos)
        {
            return new DATrasladoAgentesRecaudo().BUSCAR_RECAUDADORES_X_NOMBRE(Owner, datos);
        }

        public SocioNegocio BuscarAgenterecaudadorXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            return new DATrasladoAgentesRecaudo().BuscarAgenterecaudadorXtipodocumento(idTipoDocumento, nroDocumento);
        }

        public List<BETrasladoAgentesRecaudo> usp_Get_TrasladoAgentesRecaudoPage(string owner, decimal agente, int pagina, int cantRegxPag)
        {
            return new DATrasladoAgentesRecaudo().usp_Get_TrasladoAgentesRecaudoPage(owner, agente, pagina, cantRegxPag);
        }

        public BETrasladoAgentesRecaudo ObtenerOficinaActualAgente(string owner, decimal idAgente)
        {
            return new DATrasladoAgentesRecaudo().ObtenerOficinaActualAgente(owner, idAgente);
        }
        //public int InsertarB(BETrasladoAgentesRecaudo Traslado)
        //{
        //    return new DATrasladoAgentesRecaudo().Insertar(Traslado);
        //}
        public int Insertar(BETrasladoAgentesRecaudo Traslado)
        {
            var codOficina = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codOficina = new DATrasladoAgentesRecaudo().Insertar(Traslado);
                var result1 = new DATrasladoAgentesRecaudo().ActualizarOficinaTraslado(new BETrasladoAgentesRecaudo 
                { 
                    OWNER = Traslado.OWNER,
                    BPS_ID = Traslado.BPS_ID,
                    LOG_USER_UPDAT = Traslado.LOG_USER_UPDAT
                });

                var result2 = new DATrasladoAgentesRecaudo().ActualizarOficina(new BETrasladoAgentesRecaudo
                {
                    OWNER = Traslado.OWNER,
                    OFF_ID = Traslado.OFF_ID,
                    OFF_IDAux = Traslado.OFF_IDAux,
                    BPS_ID = Traslado.BPS_ID,
                    LOG_USER_UPDAT = Traslado.LOG_USER_UPDAT
                });
    
                transa.Complete();
            }
            return codOficina;
        }

        public int Actualizar(BETrasladoAgentesRecaudo traslado)
        {
            var codOficina = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codOficina = new DATrasladoAgentesRecaudo().Actualizar(traslado);
                var result2 = new DATrasladoAgentesRecaudo().ActualizarOficina(new BETrasladoAgentesRecaudo
                {
                    OWNER = traslado.OWNER,
                    OFF_ID = traslado.OFF_ID,
                    OFF_IDAux = traslado.OFF_IDAux,
                    BPS_ID = traslado.BPS_ID,
                    LOG_USER_UPDAT = traslado.LOG_USER_UPDAT
                });
                transa.Complete();
            }
            return codOficina;
        }

        public BETrasladoAgentesRecaudo ObtenerDatosOficina(string owner, decimal idOficina, decimal idAgente)
        {
            return new DATrasladoAgentesRecaudo().ObtenerDatosOficina(owner, idOficina, idAgente);
        }

        public BETrasladoAgentesRecaudo ValidarTrasladoOficinaAgente(string owner, decimal idOficina)
        {
            return new DATrasladoAgentesRecaudo().ValidarTrasladoOficinaAgente(owner, idOficina);
        }
    }
}
