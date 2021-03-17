using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLicenciaPlaneamiento
    {

        public bool ActualizarPlanificacion(BELicenciaPlaneamiento en)
        {
            var exitoPlanificacion = new DALicenciaPlaneamiento().ActualizarPlanificacion(en);
            return exitoPlanificacion;
        }

        public List<BELicenciaPlaneamiento> ListarAnio(string owner, decimal lic_id)
        {
            return new DALicenciaPlaneamiento().ListarAnio(owner,lic_id);
        }
        public List<BELicenciaPlaneamiento> ListarFechaPlanificacion(string owner, decimal idLic)
        {
            return new DALicenciaPlaneamiento().ListarFechaPlanificacion(owner, idLic);
        }
        public List<BELicenciaPlaneamiento> ListarXLicAnio(string owner,decimal idTemp, decimal idLic, decimal anio)
        {
            return new DALicenciaPlaneamiento().ListarXLicAnio(owner,idTemp, idLic, anio);
        }
        public List<BELicenciaPlaneamiento> ListarNuevaPlanificacion(string owner, decimal anio, decimal periodo,decimal mes)
        {
            return new DALicenciaPlaneamiento().ListarNuevaPlanificacion(owner, anio, periodo,mes);
        }
        public int Insertar(string owner, decimal codLic, decimal anio, string mesDesc, decimal licOrd, DateTime fecha, string user, decimal? codBloqueo, string codTipoPago, bool esInsert)
        {
            return new DALicenciaPlaneamiento().Insertar(owner, codLic, anio, mesDesc, licOrd, fecha, user, codBloqueo, codTipoPago, esInsert);
        }
        public int ValidarPeriodoRepetido(string owner, decimal codLic, decimal anio)
        {
            return new DALicenciaPlaneamiento().ValidarPeriodoRepetido(owner, codLic, anio);
        }
        /// <summary>
        /// Lista los periodos de la planificacion de facturacion de una Licencia
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <returns></returns>
        public List<BESelectListItem> ListarPeriodoPlanificacion(string owner, decimal idLic) {
            return new DALicenciaPlaneamiento().ListarPeriodoPlanificacion(owner, idLic);
        }
         /// <summary>
        /// OBTIENE LA PLANIFICACION DE UNA LICENCIA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLicPlan"></param>
        /// <returns></returns>
        public BELicenciaPlaneamiento ObtenerPlanificacion(string owner, decimal idLicPlan)
        {
            return new DALicenciaPlaneamiento().ObtenerPlanificacion(owner, idLicPlan);
        }


        #region Cadenas
        public decimal ListaCodigoPLaneamiento(decimal LicId, decimal year, decimal month)
        {
            return new DALicenciaPlaneamiento().ListaCodigoPLaneamiento(LicId, year, month);
        }

        //Listando Planeamiento para para poder realizar el Insert Auto
        public List<BELicenciaPlaneamiento> ListaPlaneamientoxLicHij(string owner, decimal licid)
        {
            return new DALicenciaPlaneamiento().ListaPlaneamientoxLicHija(owner, licid);
        }

        //Insertando Planeamiento en Licencias Hijas XML
        public List<BELicenciaPlaneamiento> InsertaPlaneamientoLicHijaXML(List<BELicenciaPlaneamiento> listaxml)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listaxml);

            return new DALicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(owner, xmlLicencia);

        }
        //Actualizando Planeamiento en Licencia Hija XML
        public int ActualizaPlaneamientoLicenciaHijaIndividualXML(List<BELicenciaPlaneamiento> xml)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmllicencia = string.Empty;
            xmllicencia = Utility.Util.SerializarEntity(xml);

            return new DALicenciaPlaneamiento().ActualizaPlaneamientoLicenciaHijaIndividualXML(owner, xmllicencia);
        }

        /// <summary>
        /// OBTIENE LA PLANIFICACION DE LA LICENCIA HIJA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="LICID"></param>
        /// <returns></returns>
        public List<BELicenciaPlaneamiento> ListaTodaPlanificacionxLicencia(decimal LICID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaPlaneamiento().ListaTodaPlanificacionxLicencia(owner, LICID);
        }

        public int BloqueaPlaneamientoLicenciaIndividual(decimal lic_pl_id, decimal block_id)
        {

            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaPlaneamiento().BloqueaPlaneamientoLicenciaIndividual(owner, lic_pl_id, block_id);
        }

        #endregion

        #region Descuentos Plantilla

        public List<BELicencias> ValidaPeriodosLicenciaAlDia(List<BELicencias> lista, DateTime fini)
        {
            string owner = GlobalVars.Global.OWNER;
            string listxml = string.Empty;
            listxml = Utility.Util.SerializarEntity(lista);

            return new DALicenciaPlaneamiento().ValidaPeriodosLicenciaAlDia(owner, listxml, fini);
        }

        #endregion

        #region INSERTA PLANEAMIENTO ACTUAL
        public List<BELicenciaPlaneamiento>  InsertaPlaneamientoActual(decimal LIC_ID,int ANIO , int MES ,int DIA, string USUARIO)
        {

            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaPlaneamiento().InsertaPlaneamientoActual(owner,LIC_ID, ANIO, MES, DIA,USUARIO);
        }
        #endregion


        #region ACTUALIZA PERIODO LICENCIA

        public int ValidaPeriodoLicencia(decimal lic_pl_id)
        {

            
            return new DALicenciaPlaneamiento().ValidaPeriodoLicencia(lic_pl_id);
        }

        public int ActualizarPeriodoLicenciaAct(decimal LIC_PL_ID, int OPCION,string USUARIO_ACTUAL)
        {


            return new DALicenciaPlaneamiento().ActualizarPeriodoLicenciaAct(LIC_PL_ID, OPCION, USUARIO_ACTUAL);
        }

        #endregion
    }
}
