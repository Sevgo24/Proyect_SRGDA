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
    public class BLREC_RATES_GRAL
    {
        public List<BEREC_RATES_GRAL> GET_REC_RATES_GRAL(decimal codTarifa)
        {
            return new DAREC_RATES_GRAL().GET_REC_RATES_GRAL(codTarifa);
        }

        public List<BEREC_RATES_GRAL> obtenerTarifaAsociada(decimal codModalidad,decimal? codTemp)
        {
            return new DAREC_RATES_GRAL().obtenerTarifaAsociada(codModalidad, codTemp);
        }

        public List<BEREC_RATES_GRAL> ListarTarifasPage(string owner, decimal IdTarifa, string moneda, string moduso, string incidencia, string sociedad, string repertorio, decimal IdModalidad, int st,string descripcion, int pagina, int cantRegxPag)
        {
            return new DAREC_RATES_GRAL().ListarTarifasPage(owner, IdTarifa, moneda, moduso, incidencia, sociedad, repertorio, IdModalidad, st, descripcion,pagina, cantRegxPag);
        }

        public BEREC_RATES_GRAL ObtenerNombreTarifa(string owner, decimal IdTarifa)
        {
            return new DAREC_RATES_GRAL().ObtenerNombreTarifa(owner, IdTarifa);
        }

        public BEREC_RATES_GRAL Obtener(string owner, decimal idRate)
        {
            var tarifa = new DAREC_RATES_GRAL().Obtener(owner, idRate);
            if (tarifa != null)
            {
                tarifa.ReglasAsoc = new DATarifaReglaAsociada().Listar(owner, idRate);
                tarifa.Caracteristica = new DATarifaCaracteristica().Listar(owner, idRate);
                tarifa.Parametro = new DATarifaReglaParamAsociada().Listar(owner, idRate);
                tarifa.Descuento = new DATarifaDescuento().Listar(owner, idRate);
            }
            return tarifa;
        }

        public int Insertar(BEREC_RATES_GRAL tarifa)
        {
            var codigoGen = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAREC_RATES_GRAL().Insertar(tarifa);

                if (tarifa.ReglasAsoc != null)
                {
                    foreach (var regla in tarifa.ReglasAsoc)
                    {
                        //Regla Asoc
                        regla.RATE_ID = codigoGen;
                        var codigoGenReglaAsoc = new DATarifaReglaAsociada().Insertar(regla);

                        //Caracteristica
                        if (tarifa.Caracteristica != null)
                        {
                            var Caracteristica = tarifa.Caracteristica.Where(c => c.RATE_CALC == regla.RATE_CALC).ToList();
                            foreach (var item in Caracteristica)
                            {
                                item.RATE_ID = codigoGen;
                                var codigoGenCar = new DATarifaCaracteristica().Insertar(item);
                                //Parametro
                                if (tarifa.Parametro != null)
                                {
                                    var Parametro = tarifa.Parametro.Where(p => p.RATE_CALC == item.RATE_CALC && p.RATE_CALC_AR == item.RATE_CALC_AR).ToList();
                                    foreach (var param in Parametro)
                                    {
                                        param.RATE_CALC_ID = codigoGenReglaAsoc;
                                        param.RATE_CHAR_ID = codigoGenCar;
                                        var resultado = new DATarifaReglaParamAsociada().Insertar(param);
                                    }

                                }
                            }
                        }
                    }
                }

                if (tarifa.Descuento != null)
                {
                    foreach (var item in tarifa.Descuento)
                    {
                        item.RATE_ID = codigoGen;
                        var result = new DATarifaDescuento().Insertar(item);
                    }
                }

                transa.Complete();
            }

            return codigoGen;
        }

        public int Actualizar(BEREC_RATES_GRAL tarifa,
                                List<BETarifaReglaAsociada> listaReglaAsocDel,
                                List<BETarifaCaracteristica> listaCaracteristicaDel,
                                List<BETarifaReglaParamAsociada> listaParametroDel,
                                List<BETarifaDescuento> listaDescuentoDel,
                                List<BETarifaDescuento> listaDescuentoUpdEst
                   )
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAREC_RATES_GRAL().Actualizar(tarifa);

                DATarifaReglaAsociada proxyRegla = new DATarifaReglaAsociada();
                DATarifaCaracteristica proxyCar = new DATarifaCaracteristica();
                DATarifaReglaParamAsociada proxyParam = new DATarifaReglaParamAsociada();
                DATarifaDescuento proxyDesc = new DATarifaDescuento();

                if (tarifa.ReglasAsoc != null)
                {
                    foreach (var regla in tarifa.ReglasAsoc)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva caracteristicas
                        BETarifaReglaAsociada proxyReglaObtener = proxyRegla.Obtener(tarifa.OWNER, tarifa.RATE_ID, regla.RATE_CALC_ID);
                        regla.RATE_ID = tarifa.RATE_ID;
                        if (proxyReglaObtener == null)
                        {
                            var codigoGenReglaAsoc = new DATarifaReglaAsociada().Insertar(regla);
                            //Caracteristica
                            if (tarifa.Caracteristica != null)
                            {
                                var Caracteristica = tarifa.Caracteristica.Where(c => c.RATE_CALC == regla.RATE_CALC).ToList();
                                foreach (var item in Caracteristica)
                                {
                                    item.RATE_ID = tarifa.RATE_ID;
                                    var codigoGenCar = new DATarifaCaracteristica().Insertar(item);
                                    //Parametro
                                    if (tarifa.Parametro != null)
                                    {
                                        var Parametro = tarifa.Parametro.Where(p => p.RATE_CALC == item.RATE_CALC && p.RATE_CALC_AR == item.RATE_CALC_AR).ToList();
                                        foreach (var param in Parametro)
                                        {
                                            param.RATE_CALC_ID = codigoGenReglaAsoc;
                                            param.RATE_CHAR_ID = codigoGenCar;
                                            var resultado = new DATarifaReglaParamAsociada().Insertar(param);
                                        }
                                    }
                                }
                            }
                            //
                        }

                        else
                        {

                            //Modificar
                            if (proxyReglaObtener.RATE_CALC_VAR != regla.RATE_CALC_VAR)
                            {
                                var codigoGenAdd = proxyRegla.Actualizar(regla);

                                //Caracteristica
                                if (tarifa.Caracteristica != null)
                                {
                                    var Caracteristica = tarifa.Caracteristica.Where(c => c.RATE_CALC_ID == regla.RATE_CALC_ID).ToList();
                                    foreach (var item in Caracteristica)
                                    {
                                        item.RATE_ID = tarifa.RATE_ID;
                                        BETarifaCaracteristica proxyCarObtener = proxyCar.Obtener(tarifa.OWNER, tarifa.RATE_ID, item.RATE_CHAR_ID);

                                        if (proxyCarObtener.RATE_CHAR_TVAR != item.RATE_CHAR_TVAR)
                                        {
                                            var result = proxyCar.Actualizar(item);

                                            //Parametro
                                            if (tarifa.Parametro != null)
                                            {
                                                var Parametro = tarifa.Parametro.Where(p => p.RATE_CALC == item.RATE_CALC && p.RATE_CALC_AR == item.RATE_CALC_AR).ToList();
                                                foreach (var param in Parametro)
                                                {
                                                    var resultado = proxyParam.Actualizar(param);
                                                }
                                            }

                                        }


                                    }
                                }
                                //
                            }
                        }
                    }
                }


                if (tarifa.Caracteristica != null)
                {
                    foreach (var item in tarifa.Caracteristica)
                    {
                        item.RATE_ID = tarifa.RATE_ID;
                        BETarifaCaracteristica proxyCarObtener = proxyCar.Obtener(tarifa.OWNER, tarifa.RATE_ID, item.RATE_CHAR_ID);

                        if (proxyCarObtener != null)
                        {

                            if (proxyCarObtener.RATE_CHAR_CARIDSW != item.RATE_CHAR_CARIDSW ||
                                proxyCarObtener.RATE_CHAR_VARUNID != item.RATE_CHAR_VARUNID ||
                                proxyCarObtener.RATE_CHAR_TVAR != item.RATE_CHAR_TVAR ||
                                proxyCarObtener.RATE_CHAR_DESCVAR != item.RATE_CHAR_DESCVAR
                                )
                            {
                                var result = proxyCar.Actualizar(item);

                            }
                        }

                    }

                }
                /// Se elimina las regla
                if (listaReglaAsocDel != null)
                {
                    foreach (var item in listaReglaAsocDel)
                    {
                        int r = proxyRegla.Eliminar(new BETarifaReglaAsociada()
                        {
                            OWNER = tarifa.OWNER,
                            RATE_ID = tarifa.RATE_ID,
                            RATE_CALC_ID = item.RATE_CALC_ID,
                            LOG_USER_UPDAT = tarifa.LOG_USER_UPDATE
                        });
                    }
                }

                /// Se elimina las caracteristicas
                if (listaCaracteristicaDel != null)
                {
                    foreach (var item in listaCaracteristicaDel)
                    {
                        int r = proxyCar.Eliminar(new BETarifaCaracteristica()
                        {
                            OWNER = tarifa.OWNER,
                            RATE_CHAR_ID = item.RATE_CHAR_ID,
                            RATE_ID = tarifa.RATE_ID,
                            LOG_USER_UPDAT = tarifa.LOG_USER_UPDATE
                        });
                    }
                }

                /// Se elimina las parametro
                if (listaParametroDel != null)
                {
                    foreach (var item in listaParametroDel)
                    {
                        int r = proxyParam.Eliminar(new BETarifaReglaParamAsociada()
                        {
                            OWNER = tarifa.OWNER,
                            RATE_PARAM_ID = item.RATE_CHAR_ID,
                            RATE_CHAR_ID = item.RATE_CHAR_ID,
                            RATE_CALC_ID = item.RATE_CALC_ID,
                            LOG_USER_UPDAT = tarifa.LOG_USER_UPDATE
                        });
                    }
                }

                /// Se registrar las descuento
                if (tarifa.Descuento != null)
                {
                    foreach (var item in tarifa.Descuento)
                    {
                        item.RATE_ID = tarifa.RATE_ID;
                        var desc = new DATarifaDescuento().Obtener(
                                         tarifa.OWNER,
                                         tarifa.RATE_ID,
                                         item.RATE_DISC_ID,
                                         item.DISC_ID
                            );
                        if (desc == null)
                        {
                            var result = new DATarifaDescuento().Insertar(item);
                        }
                    }
                }

                /// Se activa las descuento
                if (listaDescuentoUpdEst != null)
                {
                    foreach (var item in listaDescuentoUpdEst)
                    {
                        int r = proxyDesc.Activar(new BETarifaDescuento()
                        {
                            OWNER = tarifa.OWNER,
                            RATE_DISC_ID = item.RATE_DISC_ID,
                            RATE_ID = tarifa.RATE_ID,
                            LOG_USER_UPDATE = tarifa.LOG_USER_UPDATE
                        });
                    }
                }

                /// Se elimina las descuento
                if (listaDescuentoDel != null)
                {
                    foreach (var item in listaDescuentoDel)
                    {
                        int r = proxyDesc.Eliminar(new BETarifaDescuento()
                        {
                            OWNER = tarifa.OWNER,
                            RATE_DISC_ID = item.RATE_DISC_ID,
                            RATE_ID = tarifa.RATE_ID,
                            LOG_USER_UPDATE = tarifa.LOG_USER_UPDATE
                        });
                    }
                }

                transa.Complete();
            }
            return upd;
        }


        public List<BEREC_RATES_GRAL> ListarCombo(string owner)
        {
            return new DAREC_RATES_GRAL().ListarCombo(owner);
        }

        public int Eliminar(BEREC_RATES_GRAL tarifa)
        {
            return new DAREC_RATES_GRAL().Eliminar(tarifa);
        }

        public int ActualizarModalidad(string owner, decimal rateId, decimal rateId_New, string userUpdate)
        {
            return new DAREC_RATES_GRAL().ActualizarModalidad(owner, rateId, rateId_New, userUpdate);
        }

        public DateTime ObtenerFechaSistema()
        {
            return new DAREC_RATES_GRAL().ObtenerFechaSistema();
        }
        public int ObtenerMesesQuiebra()
        {
            return new DAREC_RATES_GRAL().ObtenerMesesQuiebra();
        }
        public List<BEREC_RATES_GRAL> ListarComboModalidad(string owner, decimal idMod)
        {
            return new DAREC_RATES_GRAL().ListarComboModalidad(owner, idMod);
        }
        
        public int ObtenerCantPeriodocidadXProd(BEREC_RATES_GRAL tarifa)
        {
            return new DAREC_RATES_GRAL().ObtenerCantPeriodocidadXProd(tarifa);
        }
        public List<BEREC_RATES_GRAL> TarifasXModalidad(decimal codModalidad, string owner) {
            return new DAREC_RATES_GRAL().TarifasXModalidad(codModalidad, owner);
        }
        public BEREC_RATES_GRAL ObtenerTarifaHistorica(string owner, decimal IdTarifa, decimal periodo)
        {
            return new DAREC_RATES_GRAL().ObtenerTarifaHistorica(owner, IdTarifa, periodo);
        }
        public decimal ObtenerRateOrigen(decimal idtarifa)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAREC_RATES_GRAL().ObtenerRateOrigen(owner, idtarifa);
        }

    }
}
