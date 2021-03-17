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
    public class BLCaracteristica
    {
        public List<BECaracteristica> ListarCartacteristica()
        {
            return new DACaracteristica().ListarCartacteristica();
        }

        public List<BECaracteristica> ListarTarifaCartacteristica(string owner)
        {
            return new DACaracteristica().ListarTarifaCartacteristica(owner);
        }

        public List<BECaracteristica> ObtenerReglaCartacteristica(string owner, decimal idRegla)
        {
            return new DACaracteristica().ObtenerReglaCartacteristica(owner, idRegla);
        }
 
        /// <summary>
        /// addon dbs
        /// lista las caracteristicas de un licencia segun  establecimineto y modalidad 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ListarCaractLicencia(string owner, decimal idLic,string fecha,decimal idLicPlan )
        {
            return new DACaracteristica().ListarCaractLicencia(owner, idLic, fecha, idLicPlan);
        }

        /// <summary>
        /// addon dbs
        /// Registra las caracterisitcas de una licencia
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public int InsertarCaractLicencia( List<BECaracteristicaLic> lista)
        {
            if (lista != null && lista.Count > 0)
            {
                using (TransactionScope transa = new TransactionScope())
                {
                  //  new DACaracteristica().InactivaCaractLicencia(lista[0].OWNER,lista[0].LIC_ID,lista[0].LIC_PL_ID);
                
                        //lista.ForEach(item =>new DACaracteristica().InsertarCaractLicencia(item));
                    for (int i = 0; i < lista.Count; i++)
                    {
                        // Assign string reference based on induction variable.
                        try
                        {
                            if (lista[i].CHAR_ID != 0)
                            {
                                new DACaracteristica().InsertarCaractLicencia(lista[i]);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    transa.Complete();
                }
               return 1;
            }
            else {
                return 0;
            }
          
        }

        /// <summary>
        /// addon dbs
        /// lista las fechas que existen registros de caracteristicas de una licencia para un dropdownlist
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <returns></returns>
        public List<BESelectListItem> ListarFechaCaractLicencia(string owner, decimal idLic, decimal idLicPlan)
        {
            return new DACaracteristica().ListarFechaCaractLicencia(owner, idLic, idLicPlan);
        }


        /// <summary>
        /// addon dbs
        /// lista las caracteristicas de un licencia segun  establecimineto y modalidad 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ListarCaracteristicasXPeriodo(string owner, decimal idLic, decimal idLicPlan)
        {
            return new DACaracteristica().ListarCaracteristicasXPeriodo(owner, idLic,  idLicPlan);
        }


        #region Cadenas
        //Devuelve Licencia por Codigo de Establecimiento
        public List<BECaracteristicaLic> ListaLicenciaxCodEst(decimal CODEST)
        {
            return new DACaracteristica().LicxCodEst(CODEST);
        }
        //Inserta Caracteristicas por Licencia Hija XML
        public List<BECaracteristicaLic> InsertaCaractersiticasLicHijaXML(List<BECaracteristicaLic> listaxml)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllistacarlicencia = string.Empty;
            xmllistacarlicencia = Utility.Util.SerializarEntity(listaxml);
            return new DACaracteristica().InsertaCaractersiticasLicHijaXML(owner, xmllistacarlicencia);
        }

        /// <summary>
        /// Actualiza Caracteristicas de Licencia y inactiva las existentes
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ActualizaCaracteristicasXML(List<BECaracteristicaLic> lista)
        {
            string owner = GlobalVars.Global.OWNER;
            string listaxml = string.Empty;
            listaxml = Utility.Util.SerializarEntity(lista);

            return new DACaracteristica().ActualizaCaracteristicasXML(owner, listaxml);

        }

        public int ActualizarCaractersiticasEst(BECaracteristicaEst entidad)
        {
            int r = new DACaracteristicaEst().ActualizarCaractersiticasEst(entidad);
            return r;
        }
        #endregion

        #region megaconcierto
        public List<BECaracteristicaLic> ListarCaractDescPlantillaxTarifa(decimal lic_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DACaracteristica().ListarCaractDescPlantillaxTarifa(owner, lic_id);
        }

        public List<BECaracteristicaLic> ListarCaractLicDscPlantilla(decimal lic_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DACaracteristica().ListarCaractLicDscPlantilla(owner, lic_id);
        }
        
        #endregion
    }
}
