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
    public class BLREC_TAXES
    {
        public List<BEREC_TAXES> REC_TAXES_GET(string owner, string descripcion, decimal territorio)
        {
            return new DAREC_TAXES().REC_TAXES_GET(owner, descripcion, territorio);
        }

        public List<BEREC_TAXES> REC_TAXES_GET_by_TAX_ID(decimal TAX_ID)
        {
            return new DAREC_TAXES().REC_TAXES_GET_by_TAX_ID(TAX_ID);
        }

        public List<BEREC_TAXES> usp_REC_GET_TAXES_Page(string owner, string param, decimal territorio, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_TAXES().usp_REC_GET_TAXES_Page(owner, param, territorio, st, pagina, cantRegxPag);
        }

        public bool REC_TAXES_Ins(BEREC_TAXES en)
        {
            var lista = new DAREC_TAXES().REC_TAXES_GET_by_TAX_ID(en.TAX_ID);
            if (lista.Count != 0) return false;
            return new DAREC_TAXES().REC_TAXES_Ins(en);
        }

        public bool REC_TAXES_Upd_by_TAX_ID(BEREC_TAXES en)
        {
            return new DAREC_TAXES().REC_TAXES_Upd_by_TAX_ID(en);
        }

        public bool REC_TAXES_Del_by_TAX_ID(decimal TAX_ID)
        {
            return new DAREC_TAXES().REC_TAXES_Del_by_TAX_ID(TAX_ID);
        }

        public List<BEREC_TAXES> Listar(string owner, string descripcion, decimal territorio, int estado, int pagina, int cantRegxPag)
        {
            return new DAREC_TAXES().Listar(owner, descripcion, territorio, estado, pagina, cantRegxPag);
        }

        public int Eliminar(BEREC_TAXES impuesto)
        {
            return new DAREC_TAXES().Eliminar(impuesto);
        }

        public BEREC_TAXES Obtener(string owner, decimal id)
        {
            //return new DAREC_TAXES().Obtener(owner, id);
            var impuesto = new DAREC_TAXES().Obtener(owner, id);
            if (impuesto != null)
            {
                impuesto.Valores = new DAImpuestoValor().Listar(owner, id);
            }
            return impuesto;
        }

        public int Insertar(BEREC_TAXES impuesto)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAREC_TAXES().Insertar(impuesto);

                if (impuesto.Valores != null)
                {
                    foreach (var valor in impuesto.Valores)
                    {
                        valor.TAX_ID = codigoGen;
                        var codigoGenVal = new DAImpuestoValor().Insertar(valor);
                    }
                }

                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BEREC_TAXES impuesto, List<BEImpuestoValor> valEliminar, List<BEImpuestoValor> listValActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAREC_TAXES().Actualizar(impuesto);

                ///logica de negocio para actualizar los Valores
                DAImpuestoValor proxyVal = new DAImpuestoValor();

                if (impuesto.Valores != null)
                {
                    foreach (var item in impuesto.Valores)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BEImpuestoValor proxyValObtener = proxyVal.Obtener(impuesto.OWNER, item.TAXV_ID);
                        if (proxyValObtener == null)
                        {
                            item.TAX_ID = impuesto.TAX_ID;
                            item.LOG_USER_CREAT = impuesto.LOG_USER_UPDATE;
                            var codigoGenAdd = proxyVal.Insertar(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            if (proxyValObtener.DIV_ID != item.DIV_ID || 
                                proxyValObtener.TAXV_VALUEP != item.TAXV_VALUEP || 
                                proxyValObtener.TAXV_VALUEM != item.TAXV_VALUEM || 
                                proxyValObtener.FechaVigencia != item.FechaVigencia 
                                )
                            {
                                item.TAX_ID = impuesto.TAX_ID;
                                item.LOG_USER_UPDAT = impuesto.LOG_USER_UPDATE;
                                var result = proxyVal.Actualizar(item);
                            }
                        }
                    }
                }

                /// se elimina los valores
                if (valEliminar != null)
                {
                    //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                    foreach (var item in valEliminar)
                    {
                        proxyVal.Eliminar(new BEImpuestoValor
                        {
                            OWNER = impuesto.OWNER,
                            TAXV_ID = item.TAXV_ID,
                            LOG_USER_UPDAT = impuesto.LOG_USER_UPDATE
                        });
                    }
                }
                /// activa los valores
                if (listValActivar != null)
                {
                    foreach (var item in listValActivar)
                    {
                        proxyVal.Activar(new BEImpuestoValor
                        {
                            OWNER = impuesto.OWNER,
                            TAXV_ID = item.TAXV_ID,
                            LOG_USER_UPDAT = impuesto.LOG_USER_UPDATE
                        });
                    }
                }

                transa.Complete();
            }

            return upd;
        }

        public int ObtenerXDescripcion(BEREC_TAXES impuesto)
        {
            return new DAREC_TAXES().ObtenerXDescripcion(impuesto);
        }

        public decimal ObtenerIGV( decimal division)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAREC_TAXES().ObtenerIGV(owner,division);
        }

    }
}
