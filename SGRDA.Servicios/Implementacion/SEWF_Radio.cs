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
    public class SEWF_Radio : ISEWF_Radio
    {
        public List<WF_Radio> ListaActualizarEstadoLicRadioDif(string owner)
        {
            //BL_WORKF_RADIO servRadio = new BL_WORKF_RADIO();
            //List<WF_Radio> ListaRadio = new List<WF_Radio>();
            //var Lista = servRadio.ListaActualizarEstadoLicRadioDif(owner);

            //foreach (var item in Lista)
            //{
            //    if (item.CANT_FACT_DEUDA > 3)
            //    {
            //        var acciones = new BLProceso().ListarProcesoXEstado(1, item.LICS_ID, owner, item.WRFK_ID, item.ID_LIC, false);
            //        foreach (var accion in acciones)
            //        {
            //            InsertarTracesProcesoLic(accion.WRKF_AID, accion.WRKF_ID, item.LICS_ID, item.ID_LIC, accion.PROC_ID, accion.OWNER, accion.PROC_MOD);
            //        }
            //    }
            //}
            return null;
        }

        public bool CambiarATEstadoWFRadio(string owner)
        {
            ////            --: Actualizar el campo LIC_SID = 9 (MOROSO) de la Tabla REC_LICENSES_GRAL por cada licencia 
            ////--	Si el NroFacturas > 3 : Actualizar el campo LIC_SID = 12 (EN LITIGIO) de la Tabla REC_LICENSES_GRAL por cada licencia 
            ////--	Si el NroFacturas < 3 : Actualizar el campo LIC_SID = 13 (AUTORIZACIÓN) de la Tabla REC_LICENSES_GRAL por cada licencia
            bool resultado=false;
            try
            {
                BL_WORKF_RADIO servRadio = new BL_WORKF_RADIO();
                List<WF_Radio> ListaRadio = new List<WF_Radio>();
                var Lista = servRadio.ListaActualizarEstadoLicRadioDif(owner);

                foreach (var item in Lista)
                {
                    ///ir a morosidad
                    if (item.CANT_FACT_DEUDA == 3)
                    {
                        var acciones = new BLProceso().ListarProcesoXEstado(1, item.LICS_ID, owner, item.WRFK_ID, item.ID_LIC, false);
                        foreach (var accion in acciones)
                        {

                            if (item.WRFK_ID > 0 && accion.WRKF_AID == 32)
                            {
                                ejecutarAccion(accion.WRKF_AID, item.WRFK_ID, item.LICS_ID, item.ID_LIC, accion.PROC_ID, accion.WRKF_AMID, owner);
                            }
                        }
                    }
                    ///pasar a en litigio
                    if (item.CANT_FACT_DEUDA > 3)
                    {
                        var acciones = new BLProceso().ListarProcesoXEstado(1, item.LICS_ID, owner, item.WRFK_ID, item.ID_LIC, false);
                        foreach (var accion in acciones)
                        {
                            if (item.WRFK_ID > 0 && accion.WRKF_AID == 20107)
                            {
                                ejecutarAccion(accion.WRKF_AID, item.WRFK_ID, item.LICS_ID, item.ID_LIC, accion.PROC_ID, accion.WRKF_AMID, owner);
                            }
                        }
                    }
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("Servicio Web", "Auto", "CambiarEstadoWFRadio", ex);
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

            if (result != -999 && idTrace != 0)
            {
                //retorno.result = 1;
                //retorno.valor = result.ToString();
                //retorno.Code = Convert.ToInt32(idTrace);
                //retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;

                if (result == -998)
                {
                    //retorno.result = 3;
                    //retorno.message = "Ya ha sido ejecutado la accion.";//Constantes.MensajeEjecutarAccion.MSG_WARNING_ACCION_EJECUTADA
                    //retorno.valor = sidWrkf.ToString();
                    ucLogApp.ucLog.GrabarLogTexto(string.Format("{0} - {1} - {2} - {3} -  ID LICENCIA: {4}", app, usuario, funcion, "Ya ha sido ejecutado la accion.", ref1Wrkf));
                }
                else if (result == -997)
                {
                    //retorno.result = 3;
                    //retorno.message = "No se ha configurado parametros para el cambio de estado.";//Constantes.MensajeEjecutarAccion.MSG_WARNING_SIN_PARAMETROS
                    //retorno.valor = sidWrkf.ToString();
                    ucLogApp.ucLog.GrabarLogTexto(string.Format("{0} - {1} - {2}- {3} -  ID LICENCIA: {4}", app, usuario, funcion, "No se ha configurado parametros para el cambio de estado.", ref1Wrkf));
                }
                else
                {

                    ucLogApp.ucLog.GrabarLogTexto(string.Format("{0} - {1} - {2}- {3} -  ID LICENCIA: {4}", app, usuario, funcion, "Inserccion en TRACER y CAMBIO de ESTADO CONFORME", ref1Wrkf));
                }
            }
            else
            {
                //retorno.result = 0;
                //retorno.message = "No ha cumplido los pre requisitos para cambiar el estado";//Constantes.MensajeEjecutarAccion.MSG_ERROR_SIN_REQUISITOS
                ucLogApp.ucLog.GrabarLogTexto(string.Format("{0} - {1} - {2}- {3} -  ID LICENCIA: {4}", app, usuario, funcion, "No ha cumplido los pre requisitos para cambiar el estado", ref1Wrkf));
            }
        }
    }


}