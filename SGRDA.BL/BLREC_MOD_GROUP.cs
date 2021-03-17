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
    public class BLREC_MOD_GROUP
    {
        public List<BEREC_MOD_GROUP> REC_MOD_GROUP_GET()
        {
            return new DAREC_MOD_GROUP().REC_MOD_GROUP_GET();
        }

        public List<BEREC_MOD_GROUP> usp_REC_MOD_GROUP_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_MOD_GROUP().usp_REC_MOD_GROUP_Page(param, st, pagina, cantRegxPag);
        }

        public bool REC_MOD_GROUP_Ins(BEREC_MOD_GROUP en)
        {
            var lista = new DAREC_MOD_GROUP().REC_MOD_GROUP_GET_by_MOG_ID(en.MOG_ID);
            if (lista.Count == 0)
                return new DAREC_MOD_GROUP().REC_MOD_GROUP_Ins(en);
            else
                return false;
        }

        public int Insertar(BEREC_MOD_GROUP ins)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAREC_MOD_GROUP().Insertar(ins);

                if (ins.RECMODGROUP != null)
                {
                    foreach (var detalle in ins.RECMODGROUP)
                    {
                        //var codigoGenVal = new DAFormatoFacturaxGrupoModalidad().Insertar(detalle);
                        var codigoGenVal = new DAREC_MOD_GROUP().InsertarDetalle(detalle);
                    }
                }
                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BEREC_MOD_GROUP form, List<BEREC_MOD_GROUP> detEliminar, List<BEREC_MOD_GROUP> listDelActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAREC_MOD_GROUP().Actualizar(form);

                ///logica de negocio para actualizar los Valores
                DAREC_MOD_GROUP Det = new DAREC_MOD_GROUP();
                if (form.RECMODGROUP != null)
                {
                    foreach (var item in form.RECMODGROUP)
                    {
                        ///verifica si no existe el valor, si no existe se registra.
                        //BEREC_MOD_GROUP DetObtener = Det.ObtenerDetalle(form.OWNER, item.MOG_ID,item.IdFormato);
                        BEREC_MOD_GROUP DetObtener = Det.ObtenerDetalle(form.OWNER, item.MOG_ID, item.IdFormatoAnt);
                        if (DetObtener == null)
                        {
                            item.LOG_USER_CREAT = form.LOG_USER_UPDAT;
                            var codigoGenAdd = Det.InsertarDetalle(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion
                            if (DetObtener.IdFormato != item.IdFormato)
                            {
                                item.LOG_USER_UPDAT = form.LOG_USER_UPDAT;
                                var result = Det.ActualizarDetalle(item);
                            }
                        }
                    }
                }
                /// se elimina los valores
                if (detEliminar != null)
                {
                    //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                    foreach (var item in detEliminar)
                    {
                        Det.Eliminar(new BEREC_MOD_GROUP
                        {
                            OWNER = form.OWNER,
                            IdFormato = item.IdFormato,
                            MOG_ID = item.MOG_ID,
                            LOG_USER_UPDAT = item.LOG_USER_UPDAT
                        });
                    }
                }
                /// activa los valores
                if (listDelActivar != null)
                {
                    foreach (var item in listDelActivar)
                    {
                        Det.Activar(new BEREC_MOD_GROUP
                        {
                            OWNER = form.OWNER,
                            IdFormato = item.IdFormato,
                            MOG_ID = item.MOG_ID,
                            LOG_USER_UPDAT = item.LOG_USER_UPDAT
                        });
                    }
                }
                transa.Complete();
            }
            return upd;
        }

        public bool REC_MOD_GROUP_Upd_by_MOG_ID(BEREC_MOD_GROUP en)
        {
            return new DAREC_MOD_GROUP().REC_MOD_GROUP_Upd_by_MOG_ID(en);
        }

        public List<BEREC_MOD_GROUP> REC_MOD_GROUP_GET_by_MOG_ID(string MOG_ID)
        {
            return new DAREC_MOD_GROUP().REC_MOD_GROUP_GET_by_MOG_ID(MOG_ID);
        }

        public bool REC_MOD_GROUP_Del(string MOG_ID)
        {
            return new DAREC_MOD_GROUP().REC_MOD_GROUP_Del(MOG_ID);
        }

        public List<BEREC_MOD_GROUP> ListarTipo(string owner, decimal idOficinaLog=0)
        {
            return new DAREC_MOD_GROUP().ListarTipo(owner, idOficinaLog);
        }

        public BEREC_MOD_GROUP Obtener(string owner, string id)
        {
            var group = new DAREC_MOD_GROUP().Obtener(owner, id);
            if (group != null)
            {
                group.FormatoModalidad = new DAFormatoFacturaxGrupoModalidad().Listar(owner, group.MOG_ID);
            }
            return group;
        }
    }
}
