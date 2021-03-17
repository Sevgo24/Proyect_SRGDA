using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using System.Transactions;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using SGRDA.DA.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_STATES
    {
        public WORKF_STATES ObtenerEstados(string owner, decimal wrkf_sid)
        {
            return new DA_WORKF_STATES().ObtenerEstados(owner, wrkf_sid);
        }
        public List<WORKF_STATES> ListarItemEstados(string owner, decimal idCiclo)
        {
            return new DA_WORKF_STATES().ListarItemEstados(owner, idCiclo);
        }

        public List<WORKF_STATES> Listar(string owner, string nombre, string etiqueta, decimal idTipoEstado, int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_STATES().Listar(owner, nombre, etiqueta, idTipoEstado, estado, pagina,  cantRegxPag);
        }

        public WORKF_STATES Obtener(string owner, decimal id)
        {
            var est = new DA_WORKF_STATES().Obtener(owner, id);
            if (est != null)
            {
                est.ListaTab = new DAREC_LIC_TAB_STAT().TabxEstado(owner, 0, est.WRKF_SID);
            }
            return est;
        }

        public decimal Eliminar(WORKF_STATES entidad)
        {
            return new DA_WORKF_STATES().Eliminar(entidad);
        }

        public int Insertar(WORKF_STATES en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DA_WORKF_STATES().Insertar(en);

                if (en.ListaTab != null)
                {
                    foreach (var detalle in en.ListaTab)
                    {
                        detalle.LOG_USER_UPDATE = en.LOG_USER_CREAT;
                        detalle.WORKF_SID = codigoGen;
                        var codigoGenVal = new DAREC_LIC_TAB_STAT().InsertarDetalle(detalle);
                    }
                }
                transa.Complete();
            }
            return codigoGen;

            
            //return new DA_WORKF_STATES().Insertar(en);
        }

        public int Actualizar(WORKF_STATES en, List<BEREC_LIC_TAB_STAT> EstEliminar, List<BEREC_LIC_TAB_STAT> listEstActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DA_WORKF_STATES().Actualizar(en);

                ///logica de negocio para actualizar los Valores
                DAREC_LIC_TAB_STAT Det = new DAREC_LIC_TAB_STAT();
                if (en.ListaTab != null)
                {
                    foreach (var item in en.ListaTab)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BEREC_LIC_TAB_STAT DetObtener = Det.ObtenerDetalle(en.OWNER, item.antTAB_ID, en.WRKF_SID, en.WRKF_ID);
                        if (DetObtener == null)
                        {
                            item.LOG_USER_UPDATE = en.LOG_USER_CREAT;
                            item.WORKF_SID = en.WRKF_SID;
                            var codigoGenAdd = Det.InsertarDetalle(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion
                            item.TAB_ID = item.TAB_ID;
                            item.WORKF_SID = en.WRKF_SID;
                            item.LOG_USER_UPDATE = en.LOG_USER_CREAT;
                            var result = Det.ActualizarDetalle(item);
                        }
                    }
                }
                /// se elimina los valores
                if (EstEliminar != null)
                {
                    foreach (var item in EstEliminar)
                    {
                        Det.Eliminar(en.OWNER, item.SECUENCIA, en.LOG_USER_CREAT);
                    }
                }
                /// activa los valores
                if (listEstActivar != null)
                {
                    foreach (var item in listEstActivar)
                    {
                        Det.Activar(en.OWNER, item.SECUENCIA, en.LOG_USER_UPDATE);
                    }
                }
                transa.Complete();
            }
            return upd;

            //return new DA_WORKF_STATES().Actualizar(en);
        }


        public List<WORKF_STATES> ListarReporte(WORKF_STATES entidad)
        {
            return new DA_WORKF_STATES().ListarReporte(entidad);
        }

        public List<WORKF_STATES> ListarItemEstadosPorTipo(string owner, decimal Id)
        {
            return new DA_WORKF_STATES().ListarItemEstadosPorTipo(owner, Id);
        }

        public List<WORKF_EVENTS> ListaTransicionEstados(string Owner, decimal Id, decimal Idestado)
        {
            return new DA_WORKF_STATES().ListaTransicionEstados(Owner, Id, Idestado);
        }

        public List<WORKF_STATES> ListarEstadosPorWorkFlow(string Owner, decimal Id)
        {
            return new DA_WORKF_STATES().ListarEstadosPorWorkFlow(Owner, Id);
        }

        public List<WORKF_STATES> ListaWorkFlowEstado(string Owner)
        {
            return new DA_WORKF_STATES().ListaWorkFlowEstado(Owner);
        }

        public List<WORKF_STATES> ListarItems(string owner)
        {
            return new DA_WORKF_STATES().ListarItems(owner);
        }
        public decimal ObtenerEstadoInicial(string owner, decimal wrkf_id)
        {
            return new DA_WORKF_STATES_WORKFLOW().ObtenerEstadoInicial(owner, wrkf_id);
        }
    }
}
