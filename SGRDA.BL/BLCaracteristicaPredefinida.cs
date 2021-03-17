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
    public class BLCaracteristicaPredefinida
    {
        public List<BECaracteristicaPredefinida> Listar_Page_CaracteristicaPredefinida(decimal tipo, decimal? subtipo, int st, int pagina, int cantRegxPag)
        {
            return new DACaracteristicaPredefinida().Listar_Page_CaracteristicaPredefinida(tipo, subtipo, st, pagina, cantRegxPag);
        }

        public int Insertar(BECaracteristicaPredefinida ins)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (ins.CaracteristicaPredefinida != null)
                {
                    foreach (var detalle in ins.CaracteristicaPredefinida)
                    {
                        codigoGen = new DACaracteristicaPredefinida().Insertar(detalle);
                    }
                }
                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BECaracteristicaPredefinida carac, List<BECaracteristicaPredefinida> detEliminar, List<BECaracteristicaPredefinida> listDelActivar)
        {
            //return new DACaracteristicaPredefinida().Actualizar(upd);
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                //upd = new DACaracteristicaPredefinida().Actualizar(carac);

                ///logica de negocio para actualizar los Valores
                DACaracteristicaPredefinida Det = new DACaracteristicaPredefinida();
                if (carac.CaracteristicaPredefinida != null)
                {
                    foreach (var item in carac.CaracteristicaPredefinida)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BECaracteristicaPredefinida DetObtener = Det.Obtener(carac.OWNER, item.CHAR_TYPES_ID, item.EST_ID, item.SUBE_ID);
                        if (DetObtener == null)
                        {
                            var codigoGenAdd = Det.Insertar(new BECaracteristicaPredefinida
                            {
                                OWNER = carac.OWNER,
                                CHAR_ID = item.CHAR_ID,
                                EST_ID = carac.EST_ID,
                                SUBE_ID = carac.SUBE_ID,
                                LOG_USER_CREAT = carac.LOG_USER_UPDATE
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion
                            if (DetObtener.EST_ID != carac.EST_ID || DetObtener.SUBE_ID != carac.SUBE_ID || DetObtener.CHAR_ID != item.CHAR_ID)
                            {
                                var result = Det.Actualizar(new BECaracteristicaPredefinida
                                {
                                    OWNER = carac.OWNER,
                                    CHAR_TYPES_ID = item.CHAR_TYPES_ID,
                                    CHAR_ID = item.CHAR_ID,
                                    EST_ID = carac.EST_ID,
                                    SUBE_ID = carac.SUBE_ID,
                                    LOG_USER_UPDATE = carac.LOG_USER_UPDATE
                                });
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
                        Det.Eliminar(new BECaracteristicaPredefinida
                        {
                            OWNER = carac.OWNER,
                            CHAR_TYPES_ID = item.CHAR_TYPES_ID,
                            LOG_USER_UPDATE = item.LOG_USER_UPDATE
                        });
                    }
                }
                /// activa los valores
                if (listDelActivar != null)
                {
                    foreach (var item in listDelActivar)
                    {
                        Det.Activar(new BECaracteristicaPredefinida
                        {
                            OWNER = carac.OWNER,
                            CHAR_TYPES_ID = item.CHAR_TYPES_ID,
                            LOG_USER_UPDATE = item.LOG_USER_UPDATE
                        });
                    }
                }
                transa.Complete();
            }
            return upd;
        }

        public int Eliminar(BECaracteristicaPredefinida del)
        {
            return new DACaracteristicaPredefinida().Eliminar(del);
        }

        public BECaracteristicaPredefinida Obtener(string owner, decimal id, decimal idTipoEst, decimal idSubTipoEsta)
        {
            var carac = new DACaracteristicaPredefinida().Obtener(owner, id, idTipoEst, idSubTipoEsta);
            if (carac != null)
            {
                carac.CaracteristicaPredefinida = new DACaracteristicaPredefinida().Listar(owner, carac.EST_ID, carac.SUBE_ID);
            }
            return carac;
        }

        public List<BECaracteristicaPredefinida> ObtenerCaracteristica(string owner, decimal id)
        {
            return new DACaracteristicaPredefinida().ObtenerCaracteristica(owner, id);
        }
    }
}
