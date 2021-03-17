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
    public class BLClaseCreacion
    {
        public List<BEClaseCreacion> Listar_Page_Clase_Creacion(string owner, string clas, int st, int pagina, int cantRegxPag)
        {
            return new DAClaseCreacion().Listar_Page_Clase_Creacion(owner, clas, st, pagina, cantRegxPag);
        }

        public int Insertar(BEClaseCreacion ins)
        {
            //return new DAClaseCreacion().Insertar(ins);
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAClaseCreacion().Insertar(ins);

                if (ins.ClaseCreacion != null)
                {
                    foreach (var detalle in ins.ClaseCreacion)
                    {
                        detalle.CLASS_COD = ins.CLASS_COD;
                        var codigoGenVal = new DAClaseCreacion().InsertarDetalle(detalle);
                    }
                }
                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BEClaseCreacion form, List<BEClaseCreacion> detEliminar, List<BEClaseCreacion> listDelActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAClaseCreacion().Actualizar(form);

                ///logica de negocio para actualizar los Valores
                DAClaseCreacion Det = new DAClaseCreacion();
                if (form.ClaseCreacion != null)
                {
                    foreach (var item in form.ClaseCreacion)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BEClaseCreacion DetObtener = Det.ObtenerDetalle(form.OWNER, form.CLASS_COD, item.auxRIGHT_COD);

                        if (DetObtener == null)
                        {
                            item.LOG_USER_UPDATE = form.LOG_USER_CREAT;
                            item.CLASS_COD = form.CLASS_COD;
                            var codigoGenAdd = Det.InsertarDetalle(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion
                            if (DetObtener.CLASS_COD != form.CLASS_COD || DetObtener.auxRIGHT_COD != item.RIGHT_COD)
                            {
                                item.CLASS_COD = form.CLASS_COD;
                                item.LOG_USER_UPDATE = form.LOG_USER_UPDATE;
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
                        Det.Eliminar(new BEClaseCreacion
                        {
                            OWNER = form.OWNER,
                            CLASS_COD = form.CLASS_COD,
                            //RIGHT_COD = item.RIGHT_COD,
                            SEQUENCE = item.SEQUENCE,
                            LOG_USER_UPDATE = form.LOG_USER_UPDATE
                        });
                    }
                }
                /// activa los valores
                if (listDelActivar != null)
                {
                    foreach (var item in listDelActivar)
                    {
                        Det.Activar(new BEClaseCreacion
                        {
                            OWNER = form.OWNER,
                            CLASS_COD = form.CLASS_COD,
                            //RIGHT_COD = item.RIGHT_COD,
                            SEQUENCE = item.SEQUENCE,
                            LOG_USER_UPDATE = form.LOG_USER_UPDATE
                        });
                    }
                }
                transa.Complete();
            }
            return upd;
        }

        public int Eliminar(BEClaseCreacion del)
        {
            return new DAClaseCreacion().Eliminar(del);
        }

        public BEClaseCreacion Obtener(string owner,string clas)
        {
            var carac = new DAClaseCreacion().Obtener(owner, clas);
            if (carac != null)
            {
                carac.ClaseCreacion = new DADerecho().Listar(owner, carac.CLASS_COD);
            }
            return carac;
        }
    }
}
