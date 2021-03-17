using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRDA.DA;
using SGRDA.Entities;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;
using System.Transactions;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_WORKFLOWS
    {
        public decimal InsertarWorkFlow(WORKF_WORKFLOWS entidad)
        {
            decimal codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                codigoGen = new DA_WORKF_WORKFLOWS().InsertarWorkFlow(entidad);
                if (entidad.WorkflowEstados != null)
                {
                    foreach (var estado in entidad.WorkflowEstados)
                    {
                        estado.WRKF_ID = codigoGen;
                        var result = new DA_WORKF_STATES_WORKFLOW().Insertar(estado);

                    }
                }
                if (entidad.WorkflowTransiciones != null)
                {
                    foreach (var transicion in entidad.WorkflowTransiciones)
                    {
                        transicion.WRKF_ID = codigoGen;
                        var result = new DA_WORKF_TRANSITIONS().Insertar(transicion);

                    }
                }
                if (entidad.WorkflowTabs != null)
                {
                    foreach (var detalle in entidad.WorkflowTabs)
                    {
                        detalle.LOG_USER_UPDATE = entidad.LOG_USER_CREAT;
                        detalle.WORKF_ID = codigoGen;
                        var codigoGenVal = new DAREC_LIC_TAB_STAT().InsertarDetalle(detalle);
                    }
                }
                transa.Complete();
            }
            return codigoGen;
        }

        public decimal ActualizarWorkFlow(WORKF_WORKFLOWS entidad,
                                            List<WORKF_STATES_WORKFLOW> listaEstadoDel,
                                            List<WORKF_STATES_WORKFLOW> listaEstadoActivar,
                                            List<WORKF_TRANSITIONS> listaTransicionDel,
                                            List<WORKF_TRANSITIONS> listaTransicionActivar,
                                            List<BEREC_LIC_TAB_STAT> listaTabDel,
                                            List<BEREC_LIC_TAB_STAT> listaTabActivar
            )
        {
             decimal upd = 0;
             using (TransactionScope transa = new TransactionScope())
             {
                 upd = new DA_WORKF_WORKFLOWS().ActualizarWorkFlow(entidad);

                 DA_WORKF_STATES_WORKFLOW proxyWS = new DA_WORKF_STATES_WORKFLOW();
                 DA_WORKF_TRANSITIONS proxyT = new DA_WORKF_TRANSITIONS();
                 DAREC_LIC_TAB_STAT proxyTabs = new DAREC_LIC_TAB_STAT();
                 if (entidad.WorkflowEstados != null)
                 {
                     foreach (var item in entidad.WorkflowEstados)
                     {
                         item.WRKF_ID = entidad.WRKF_ID;
                         WORKF_STATES_WORKFLOW proxyEstObtener = proxyWS.Obtener(entidad.OWNER, item.WRKF_ID, item.WRKF_SID, item.WRKF_INI);
                         if (proxyEstObtener == null)
                         {
                             var codigoGenAdd = proxyWS.Insertar(item);
                         }
                         else
                         {
                             if (proxyEstObtener.WRKF_ID != item.WRKF_ID || proxyEstObtener.WRKF_SID != item.WRKF_SID || proxyEstObtener.WRKF_INI != item.WRKF_INI)
                             {
                                 var result = proxyWS.Actualizar(item);
                             }
                         }
                     }
                 }

                 if (entidad.WorkflowTransiciones != null)
                 {
                     foreach (var item in entidad.WorkflowTransiciones)
                     {
                         WORKF_TRANSITIONS proxyTranObtener = proxyT.ObtenerTransitionsWorkflow(entidad.OWNER, item.WRKF_TID,entidad.WRKF_ID);
                         if (proxyTranObtener == null)
                         {
                             item.WRKF_ID = entidad.WRKF_ID;
                             var result = new DA_WORKF_TRANSITIONS().Insertar(item);
                         }                         
                     }
                 }

                 if (entidad.WorkflowTabs != null)
                 {
                     foreach (var item in entidad.WorkflowTabs)
                     {
                         ///verifica si no existe el valor
                         ///si no existe se registra
                         item.WORKF_ID = entidad.WRKF_ID;
                         BEREC_LIC_TAB_STAT DetObtener = proxyTabs.ObtenerDetalle(entidad.OWNER, item.antTAB_ID, item.WORKF_SID, item.WORKF_ID);
                         if (DetObtener == null)
                         {
                             entidad.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                             item.WORKF_ID = entidad.WRKF_ID;
                             var codigoGenAdd = proxyTabs.InsertarDetalle(item);
                         }
                         else
                         {
                             ///sino  solo se actualiza la informacion
                             item.TAB_ID = item.TAB_ID;
                             item.WORKF_ID = entidad.WRKF_ID;
                             entidad.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                             var result = proxyTabs.ActualizarDetalle(item);
                         }
                     }
                 }

                 /// se elimina los estados
                 if (listaEstadoDel != null)
                 {
                     //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                     foreach (var item in listaEstadoDel)
                     {
                         proxyWS.Eliminar( new WORKF_STATES_WORKFLOW(){
                                             OWNER = entidad.OWNER,
                                             WRKF_ID = entidad.WRKF_ID,
                                             WRKF_SID = item.WRKF_SID,
                                             LOG_USER_UPDATE = entidad.LOG_USER_UPDATE
                                        } );
                     }  
                 }

                 /// activa los estados
                 if (listaEstadoActivar != null)
                 {
                     foreach (var item in listaEstadoActivar)
                     {
                         proxyWS.Activar(new WORKF_STATES_WORKFLOW()
                         {
                             OWNER = entidad.OWNER,
                             WRKF_ID = entidad.WRKF_ID,
                             WRKF_SID = item.WRKF_SID,
                             LOG_USER_UPDATE = entidad.LOG_USER_UPDATE
                         });
                     }                     
                 }

                 /// se eliminan la transiciones
                 if (listaTransicionDel != null)
                 {
                     //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                     foreach (var item in listaTransicionDel)
                     {
                         proxyT.Eliminar(new WORKF_TRANSITIONS()
                         {
                             OWNER = entidad.OWNER,
                             WRKF_TID = item.WRKF_TID,
                             LOG_USER_UPDATE = entidad.LOG_USER_UPDATE
                         });
                     }
                 }

                 /// activa las transiciones
                 if (listaTransicionActivar != null)
                 {
                     foreach (var item in listaTransicionActivar)
                     {
                         proxyT.Activar(new WORKF_TRANSITIONS()
                         {
                             OWNER = entidad.OWNER,
                             WRKF_TID = item.WRKF_TID,
                             LOG_USER_UPDATE = entidad.LOG_USER_UPDATE
                         });
                     }
                 }

                 /// se eliminan los Tabs
                 if (listaTabDel != null)
                 {
                     foreach (var item in listaTabDel)
                     {
                         proxyTabs.Eliminar(entidad.OWNER, item.SECUENCIA, entidad.LOG_USER_CREAT);
                     }
                 }

                 /// activa los tabs
                 if (listaTabActivar != null)
                 {
                     foreach (var item in listaTabActivar)
                     {
                         proxyTabs.Activar(entidad.OWNER, item.SECUENCIA, entidad.LOG_USER_UPDATE);
                     }
                 }
                 transa.Complete();
             }
             return upd;
        }

        public WORKF_WORKFLOWS ObtenerWorkFlow(string owner, decimal wrkf_id)
        {
            var workflow = new DA_WORKF_WORKFLOWS().ObtenerWorkFlow(owner, wrkf_id);
            if (workflow != null)
            {
                workflow.WorkflowEstados = new DA_WORKF_STATES_WORKFLOW().ListarItemEstados(owner, wrkf_id);
                workflow.WorkflowTransiciones = new DA_WORKF_TRANSITIONS().ListarTransicionesWorkflow(owner, wrkf_id);
                workflow.WorkflowTabs = new DAREC_LIC_TAB_STAT().TabxEstado(owner, Decimal.Zero, wrkf_id);
            }
            return workflow;
        }

        public List<WORKF_WORKFLOWS> Listar(string owner, string nombre, string etiqueta, decimal idCliente, int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_WORKFLOWS().Listar(owner, nombre, etiqueta, idCliente, estado, pagina, cantRegxPag);
        }
        public decimal EliminarWorkFlow(WORKF_WORKFLOWS entidad)
        {
            return new DA_WORKF_WORKFLOWS().EliminarWorkFlow(entidad);
        }
        public List<WORKF_WORKFLOWS> ListarItems(string owner)
        {
            return new DA_WORKF_WORKFLOWS().ListarItems(owner);
        }
    }
}
