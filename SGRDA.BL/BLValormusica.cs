using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLValormusica
    {
        public List<BEValormusica> ListaValorMusicaPaginada(string owner, DateTime fechaini, DateTime fechafin, int st, int pagina, int cantRegxPag)
        {
            return new DAValormusica().ListaValorMusicaPaginada(owner, fechaini, fechafin, st, pagina, cantRegxPag);
        }

        public BEValormusica Obtener(string owner, string id)
        {
            return new DAValormusica().Obtener(owner, id);
        }

        public List<BEValormusica> Listar(string owner)
        {
            return new DAValormusica().Listar(owner);
        }

        public int Insertar(BEValormusica en)
        {
            var cod = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                var item = new DAValormusica().ActualizarFechaUltimoRegistro(en);
                cod = new DAValormusica().Insertar(en);
                transa.Complete();
            }
            return cod;
        }

        public int Actualizar(BEValormusica en)
        {
            return new DAValormusica().Actualizar(en);
        }

        public int ActualizarFechaUltimoRegistro(BEValormusica en)
        {
            return new DAValormusica().ActualizarFechaUltimoRegistro(en);
        }

        public int Eliminar(BEValormusica en)
        {
            return new DAValormusica().Eliminar(en);
        }

        public BEValormusica ObtenerActivo(string owner)
        {
            return new DAValormusica().ObtenerActivo(owner);
        }
        public List<BEValormusica> ListarHistorico(string owner)
        {
            return new DAValormusica().ListarHistorico(owner);
        }
    }
}
