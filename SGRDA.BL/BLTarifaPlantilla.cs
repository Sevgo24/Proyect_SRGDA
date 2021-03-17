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
    public class BLTarifaPlantilla
    {

        public List<BETarifaPlantilla> Listar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado,int confecha, int pagina, int cantRegxPag)
        {
            return new DATarifaPlantilla().Listar(owner, desc, nro, fini, ffin, estado,confecha, pagina, cantRegxPag);
        }

        public int Eliminar(BETarifaPlantilla plantilla)
        {
            return new DATarifaPlantilla().Eliminar(plantilla);
        }

        public int Insertar(BETarifaPlantilla plantilla)
        {
            var codigoGen = 0;
            //using (TransactionScope transa = new TransactionScope())
            //{
                codigoGen = new DATarifaPlantilla().Insertar(plantilla);

                if (plantilla.Caracteristicas != null)
                {
                    foreach (var caracteristica in plantilla.Caracteristicas)
                    {
                        caracteristica.TEMP_ID = codigoGen;
                        var codigoGenCar = new DATarifaPlantillaCaracteristica().Insertar(caracteristica);

                        foreach (var valor in plantilla.Valores.Where(p=>p.TEMPL_ID ==caracteristica.TEMPL_ID ) )
                        {
                            valor.TEMPL_ID = codigoGenCar;
                            var codigoGenValAdd = new DATarifaPlantillaValor().Insertar(valor);
                        }
                    }                  

                }
            //    transa.Complete();
            //}
            return codigoGen;
        }

        public BETarifaPlantilla Obtener(string owner, decimal id)
        {   
            var tarifa = new DATarifaPlantilla().Obtener(owner, id);
            if (tarifa != null)
            {
                tarifa.Caracteristicas = new DATarifaPlantillaCaracteristica().Listar(owner, id);
                tarifa.Valores = new DATarifaPlantillaValor().Listar(owner, id);
            }
            return tarifa;
        }

        public int Actualizar(BETarifaPlantilla plantilla,
                            List<BETarifaPlantillaCaracteristica> carEliminar,
                            List<BETarifaPlantillaCaracteristica> listCarActivar,
                            List<BETarifaPlantillaValor> valEliminar,
                            List<BETarifaPlantillaValor> listValActivar
                            )
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DATarifaPlantilla().Actualizar(plantilla);

                DATarifaPlantillaCaracteristica proxyCar = new DATarifaPlantillaCaracteristica();
                DATarifaPlantillaValor proxyVal = new DATarifaPlantillaValor();
                if (plantilla.Caracteristicas != null)
                {
                    foreach (var item in plantilla.Caracteristicas)
                    {
                        item.TEMP_ID = plantilla.TEMP_ID;
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva caracteristicas
                        BETarifaPlantillaCaracteristica proxyCarObtener = proxyCar.Obtener(plantilla.OWNER, plantilla.TEMP_ID, item.TEMPL_ID);
                        if (proxyCarObtener == null)
                        {
                            var codigoGenAdd = proxyCar.Insertar(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la caracteristicas 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyCar.Actualizar(item);
                        }
                    }
                }

                /// se elimina las caracteristicas
                if (carEliminar != null)
                {
                    foreach (var item in carEliminar)
                    {

                        int r = proxyCar.Eliminar(new BETarifaPlantillaCaracteristica()
                        {
                            OWNER = plantilla.OWNER,
                            TEMPL_ID = item.TEMPL_ID,
                            TEMP_ID = plantilla.TEMP_ID,
                            LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE
                        });
                    }
                }
                /// activa las caracteristicas
                if (listCarActivar != null)
                {
                    foreach (var item in listCarActivar)
                    {
                        int r = proxyCar.Activar(new BETarifaPlantillaCaracteristica()
                        {
                            OWNER = plantilla.OWNER,
                            TEMPL_ID = item.TEMPL_ID,
                            TEMP_ID = plantilla.TEMP_ID,
                            LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE
                        });
                    }
                }


                
                if (plantilla.Valores != null)
                {
                    foreach (var item in plantilla.Valores)
                    {
                        //item.TEMP_ID = plantilla.TEMP_ID;
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva valor
                        BETarifaPlantillaValor proxyValObtener = proxyVal.Obtener(plantilla.OWNER, item.TEMPS_ID, item.TEMPL_ID);
                        if (proxyValObtener == null)
                        {
                            var codigoGenValAdd = proxyVal.Insertar(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la valor 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyVal.Actualizar(item);
                        }
                    }
                }

                /// se elimina los valores
                if (valEliminar != null)
                {
                    foreach (var item in valEliminar)
                    {
                        int r = proxyVal.Eliminar(new BETarifaPlantillaValor()
                        {
                            OWNER = plantilla.OWNER,
                            TEMPS_ID = item.TEMPS_ID,
                            TEMPL_ID = item.TEMPL_ID,
                            LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE
                        });
                    }
                }

                /// activa los valores
                if (listValActivar != null)
                {
                    foreach (var item in listValActivar)
                    {
                        int r = proxyVal.Activar(new BETarifaPlantillaValor()
                        {
                            OWNER = plantilla.OWNER,
                            TEMPS_ID = item.TEMPS_ID,
                            TEMPL_ID = item.TEMPL_ID,
                            LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE
                        });
                    }
                }

                transa.Complete();
            }
            return upd;
        }


        public int ObtenerXDescripcion(BETarifaPlantilla plantilla)
        {
            return new DATarifaPlantilla().ObtenerXDescripcion(plantilla);
        }

        public List<BETarifaPlantilla> ListarPlantilla(string owner)
        {
            return new DATarifaPlantilla().ListarPlantilla(owner);
        }
    }
}
