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
    public class BLRequerimientoDinero
    {
        public List<BERequerimientoDinero> Listar(string owner, decimal id, string tipo, string nro, string nombre, int estado, int pagina, int cantRegxPag)
        {
            return new DARequerimientoDinero().Listar(owner, id, tipo, nro, nombre, estado, pagina, cantRegxPag);
        }

        public BERequerimientoDinero Obtener(string owner, decimal id)
        {
            var req = new DARequerimientoDinero().Obtener(owner, id);
            if (req != null)
            {
                req.DetalleGasto = new DADetalleGasto().Listar(owner, id);
            }
            return req;
        }

        public int Insertar(BERequerimientoDinero req)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DARequerimientoDinero().Insertar(req);

                if (req.DetalleGasto != null)
                {
                    foreach (var detalle in req.DetalleGasto)
                    {
                        detalle.MNR_ID = Convert.ToDecimal(codigoGen);
                        var codigoGenVal = new DADetalleGasto().Insertar(detalle);
                    }
                }
                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BERequerimientoDinero req, List<BEDetalleGasto> detEliminar, List<BEDetalleGasto> listDelActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DARequerimientoDinero().Actualizar(req);

                ///logica de negocio para actualizar los Valores
                //DAImpuestoValor proxyVal = new DAImpuestoValor();
                DADetalleGasto proxyDet = new DADetalleGasto();

                if (req.DetalleGasto != null)
                {
                    foreach (var item in req.DetalleGasto)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BEDetalleGasto proxyDetObtener = proxyDet.Obtener(req.OWNER, item.MNR_DET_ID, req.MNR_ID);
                        if (proxyDetObtener == null)
                        {
                            item.MNR_ID = req.MNR_ID;
                            item.LOG_USER_CREAT = req.LOG_USER_UPDATE;
                            var codigoGenAdd = proxyDet.Insertar(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            if (proxyDetObtener.EXP_TYPE != item.EXP_TYPE ||
                                proxyDetObtener.EXPG_ID != item.EXPG_ID ||
                                proxyDetObtener.EXP_ID != item.EXP_ID ||
                                proxyDetObtener.EXP_VAL_PRE != item.EXP_VAL_PRE ||
                                proxyDetObtener.EXP_VAL_APR != item.EXP_VAL_APR ||
                                proxyDetObtener.EXP_VAL_CON != item.EXP_VAL_CON
                                )
                            {
                                item.MNR_ID = req.MNR_ID;
                                item.LOG_USER_UPDAT = req.LOG_USER_UPDATE;
                                var result = proxyDet.Actualizar(item);
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
                        proxyDet.Eliminar(new BEDetalleGasto
                        {
                            OWNER = req.OWNER,
                            MNR_DET_ID = item.MNR_DET_ID,
                            MNR_ID = req.MNR_ID,
                            LOG_USER_UPDAT = req.LOG_USER_UPDATE
                        });
                    }
                }
                /// activa los valores
                if (listDelActivar != null)
                {
                    foreach (var item in listDelActivar)
                    {
                        proxyDet.Activar(new BEDetalleGasto
                        {
                            OWNER = req.OWNER,
                            MNR_DET_ID = item.MNR_DET_ID,
                            MNR_ID = req.MNR_ID,
                            LOG_USER_UPDAT = req.LOG_USER_UPDATE
                        });
                    }
                }

                transa.Complete();
            }

            return upd;
        }

        public int Actualizar_Estado(BERequerimientoDinero req, List<BEDetalleGasto> detEliminar, List<BEDetalleGasto> listDelActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DARequerimientoDinero().Actualizar_Estado_Apro(req);

                ///logica de negocio para actualizar los Valores
                //DAImpuestoValor proxyVal = new DAImpuestoValor();
                DADetalleGasto proxyDet = new DADetalleGasto();

                if (req.DetalleGasto != null)
                {
                    foreach (var item in req.DetalleGasto)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        //BEDetalleGasto proxyDetObtener = proxyDet.Obtener(req.OWNER, item.MNR_DET_ID, req.MNR_ID);
                        //if (proxyDetObtener == null)
                        //{
                        //    item.MNR_ID = req.MNR_ID;
                        //    item.LOG_USER_CREAT = req.LOG_USER_UPDATE;
                        //    var codigoGenAdd = proxyDet.Insertar(item);
                        //}
                        //else
                        //{
                        //    ///sino  solo se actualiza la informacion de la direccion 
                        //    item.MNR_ID = req.MNR_ID;
                        //    item.LOG_USER_UPDAT = req.LOG_USER_UPDATE;
                        //    var result = proxyDet.Actualizar_Apro(item);
                        //}

                        item.MNR_ID = req.MNR_ID;
                        item.LOG_USER_UPDAT = req.LOG_USER_UPDATE;
                        var result = proxyDet.Actualizar_Apro(item);

                    }
                }

                ///// se elimina los valores
                //if (detEliminar != null)
                //{
                //    //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                //    foreach (var item in detEliminar)
                //    {
                //        proxyDet.Eliminar(new BEDetalleGasto
                //        {
                //            OWNER = req.OWNER,
                //            MNR_DET_ID = item.MNR_DET_ID,
                //            MNR_ID = req.MNR_ID,
                //            LOG_USER_UPDAT = req.LOG_USER_UPDATE
                //        });
                //    }
                //}
                ///// activa los valores
                //if (listDelActivar != null)
                //{
                //    foreach (var item in listDelActivar)
                //    {
                //        proxyDet.Activar(new BEDetalleGasto
                //        {
                //            OWNER = req.OWNER,
                //            MNR_DET_ID = item.MNR_DET_ID,
                //            MNR_ID = req.MNR_ID,
                //            LOG_USER_UPDAT = req.LOG_USER_UPDATE
                //        });
                //    }
                //}

                transa.Complete();
            }

            return upd;
        }

        public int Actualizar_Estado_Rendir(BERequerimientoDinero req, BELegalizacion leg)
        {
            int upd = 0;
            int codGenLeg = 0;
            decimal codGenADJ = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (leg.LEG_ADJ == "C")
                {
                    BERequerimientoDinero reintegro = new BERequerimientoDinero();
                    reintegro.OWNER=req.OWNER    ;
                    reintegro.BPS_ID=req.BPS_ID    ;
                    reintegro.STT_ID= 2   ;
                    reintegro.MNR_DESC=  "REINTEGRO DE DINERO."  ;
                    reintegro.MNR_DATE=  req.MNR_DATE  ;
                    reintegro.MNR_VALUE_PRE = leg.MNR_VALUE_ADJ;
                    reintegro.MNR_VALUE_APR = leg.MNR_VALUE_ADJ;
                    reintegro.MNR_VALUE_CON = leg.MNR_VALUE_ADJ;
                    reintegro.LOG_USER_CREAT = req.LOG_USER_UPDATE;
                    codGenADJ = new DARequerimientoDinero().InsertarReintegro(reintegro);

                    upd = new DARequerimientoDinero().Actualizar_Estado_Rendir(req);
                }
                else
                {
                    upd = new DARequerimientoDinero().Actualizar_Estado_Rendir(req);
                }
                //registrar
                if (upd > 0)
                {
                    if(codGenADJ!=0)
                        leg.MNR_DOC_ADJ = codGenADJ.ToString();
                    

                    codGenLeg = new DALegalizacion().Insertar(leg);

                    ///logica de negocio para actualizar los Valores                  
                    if (req.DetalleGasto != null)
                    {
                        DADetalleGasto proxyDet = new DADetalleGasto();
                        foreach (var item in req.DetalleGasto)
                        {
                            item.MNR_ID = req.MNR_ID;
                            item.LOG_USER_UPDAT = req.LOG_USER_UPDATE;
                            item.LEG_ID = codGenLeg;
                            var result = proxyDet.Actualizar_Rendir(item);
                        }
                    } 
                }
                              
                transa.Complete();
            }
            return upd;
        }



    }
}
