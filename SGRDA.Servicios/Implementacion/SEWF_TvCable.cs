using SGRDA.BL;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using SGRDA.Servicios.Contrato;
using SGRDA.Servicios.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGRDA.Servicios.Implementacion
{
    public class SEWF_TvCable: ISEWF_TvCable
    {
        
        public bool CambiarEstadoAutomatico(string owner)
        {
            ////--: Actualizar el campo LIC_SID = 9 (MOROSO) de la Tabla REC_LICENSES_GRAL por cada licencia 
            ////--	Si el NroFacturas > 3 : Actualizar el campo LIC_SID = 12 (EN LITIGIO) de la Tabla REC_LICENSES_GRAL por cada licencia 
            ////--	Si el NroFacturas < 3 : Actualizar el campo LIC_SID = 13 (AUTORIZACIÓN) de la Tabla REC_LICENSES_GRAL por cada licencia

            bool resultado=false;
            try
            {
                #region INICIALIZADOR DE PARAM CONFIG

                decimal idModulo;
                decimal accionMorosoId ;
                decimal accionMorosoPrejudicialId;
                decimal cantidadMoroso;
                decimal cantidadMorosoPrejudicial;
                decimal workFlowTvCable;
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["idModulo"].ToString(), out idModulo))
                {
                    throw new Exception("Error al leer paramentro de web config: idModulo ");
                }
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["accionMorosoId"].ToString(), out accionMorosoId))
                {
                    throw new Exception("Error al leer paramentro de web config: accionMorosoId ");
                }
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["accionMorosoPrejudicialId"].ToString(), out accionMorosoPrejudicialId))
                {
                    throw new Exception("Error al leer paramentro de web config: accionMorosoPrejudicialId ");
                }
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["cantidadMoroso"].ToString(), out cantidadMoroso))
                {
                    throw new Exception("Error al leer paramentro de web config: cantidadMoroso ");
                }
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["cantidadMorosoPrejudicial"].ToString(), out cantidadMorosoPrejudicial))
                {
                    throw new Exception("Error al leer paramentro de web config: cantidadMorosoPrejudicial ");
                }
                if (!decimal.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["workFlowTvCable"].ToString(), out workFlowTvCable))
                {
                    throw new Exception("Error al leer paramentro de web config: workFlowTvCable ");
                }
                

                #endregion fIN INICIALIZADOR DE PARAM CONFIG

                BL_WORKF_RADIO servRadio = new BL_WORKF_RADIO();
                List<WF_Radio> ListaRadio = new List<WF_Radio>();
                var Lista = servRadio.ListaActualizarEstadoLicRadioDif(owner);

                foreach (var item in Lista)
                {
                    ///ir a morosidad
                    if (item.CANT_FACT_DEUDA >= cantidadMoroso && item.WRFK_ID == workFlowTvCable)
                    {
                        var acciones = new BLProceso().ListarProcesoXEstado(idModulo, item.LICS_ID, owner, item.WRFK_ID, item.ID_LIC, false);
                        foreach (var accion in acciones)
                        {
                            if ( accion.WRKF_AID == accionMorosoId)
                            {
                                ejecutarAccion(accion.WRKF_AID, item.WRFK_ID, item.LICS_ID, item.ID_LIC, accion.PROC_ID, accion.WRKF_AMID, owner);
                            }
                        }
                    }
                    /////pasar a moroso prejudicial
                    //if (item.CANT_FACT_DEUDA >= cantidadMorosoPrejudicial && item.WRFK_ID == workFlowTvCable)
                    //{
                    //    var acciones = new BLProceso().ListarProcesoXEstado(idModulo, item.LICS_ID, owner, item.WRFK_ID, item.ID_LIC, false);
                    //    foreach (var accion in acciones)
                    //    {
                    //        if ( accion.WRKF_AID == accionMorosoPrejudicialId)
                    //        {
                    //            ejecutarAccion(accion.WRKF_AID, item.WRFK_ID, item.LICS_ID, item.ID_LIC, accion.PROC_ID, accion.WRKF_AMID, owner);
                    //        }
                    //    }
                    //}
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("Servicio Web", "Auto", "CambiarEstadoWFTV", ex);
                resultado = false;
            }
            return resultado;

        }
        public class Modulo
        {
            public const decimal TARIFA = 3;
            public const decimal LICENCIAMIENTO = 1;
        }
        private void ejecutarAccion(decimal aidWrkf, decimal idWrkf, decimal sidWrkf, decimal ref1Wrkf, decimal idProc, decimal amidWrkf, string owner)
        {

            string funcion = "InsertarTracesProcesoLic";
            string usuario = "AUTO";
            string app = "Servicio Web";

            // Resultado retorno = new Resultado();
            BL_WORKF_TRACES traServ = new BL_WORKF_TRACES();
            WORKF_TRACES entidad = new WORKF_TRACES();
            entidad.OWNER = owner;
            entidad.WRKF_AID = aidWrkf;
            entidad.WRKF_ID = idWrkf;
            entidad.WRKF_SID = sidWrkf;
            entidad.WRKF_REF1 = ref1Wrkf;
            entidad.PROC_MOD = Modulo.LICENCIAMIENTO;
            entidad.PROC_ID = idProc;
            entidad.LOG_USER_CREAT = usuario;
            entidad.WRKF_AMID = amidWrkf;

            decimal idTrace;

            var result = traServ.InsertarTraceLic(entidad, amidWrkf, out idTrace);
            string msg = "";
            if (result != -999 && idTrace != 0)
            {
                if (result == -998)
                {
                    msg = "Ya ha sido ejecutado la accion.";
                }
                else if (result == -997)
                {
                    msg = "No se ha configurado parametros para el cambio de estado.";
                }
                else
                {
                    msg ="Inserccion en TRACER y CAMBIO de ESTADO CONFORME";
                }
           }
            else
            {
                msg = "No ha cumplido los pre requisitos para cambiar el estado";
            }
            ucLogApp.ucLog.GrabarLogTexto(string.Format("{0} - {1} - {2}- {3} -  ID LICENCIA: {4}", app, usuario, funcion, msg, ref1Wrkf));
        }





    }


}