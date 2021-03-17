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
    public class BLPreImpresion
    {

        /// <summary>
        /// Lista PreImpresiones Pendiente
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<BEPreImpresion> Pendientes( string localhost)
        {
            return new DAPreImpresion().Pendientes( localhost);
        }

        public BEPreImpresion ObtenerPreImpresion(decimal idImpresion) {
            return new DAPreImpresion().ObtenerPreImpresion(idImpresion); 
        
        }
        //public int Eliminar(BEAgente agente)
        //{
        //    return new DAAgente().Eliminar(agente);
        //}

      

        //public int Insertar(BEAgente agente)
        //{
        //    return new DAAgente().Insertar(agente);
        //}



        public int ActualizarEstado(decimal codigoImpresion, string host,string estado) 
        {
            return new DAPreImpresion().ActualizarEstado(codigoImpresion, host, estado);
        }

        //public List<BEAgente> ListarCombo(string owner)
        //{
        //    return new DAAgente().ListarCombo(owner);
        //}

        public bool RegistrarPreImpresionMasiva(List<BEPreImpresion> listaPreImpresion)
        {
            bool result = false;
            bool exitoPreImpresion = false;
            string xmlPreImpresion = string.Empty;
            using (TransactionScope transa = new TransactionScope())
            {
                xmlPreImpresion = Utility.Util.SerializarEntity(listaPreImpresion);
                exitoPreImpresion = new DAPreImpresion().RegistrarPreImpresionXML(xmlPreImpresion);
                transa.Complete();
            }
            return result;
        }
    }
}
