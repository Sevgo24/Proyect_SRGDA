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
    public class BLTarifaRegla
    {

        public int Insertar(BETarifaRegla regla)
        {
            bool exito = false;
            var codigoGen = 0;
            string xmlMatriz = string.Empty;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DATarifaRegla().Insertar(regla);

                if (regla.Caracteristicas != null)
                {
                    foreach (var caracteristica in regla.Caracteristicas)
                    {
                        caracteristica.CALR_ID = codigoGen;
                        var codigoGenCar = new DATarifaReglaData().Insertar(caracteristica);
                    }

                }

                if (regla.MatrizValores != null)
                {
                    foreach (var valMatriz in regla.MatrizValores)
                    {
                        valMatriz.CALR_ID = codigoGen;
                        valMatriz.CALRV_STARTS = regla.STARTS;
                        valMatriz.LOG_USER_CREAT = regla.LOG_USER_CREAT;
                        //var codigoGenCar = new DATarifaReglaValor().Insertar(valMatriz);
                    }
                    xmlMatriz = Utility.Util.SerializarEntity(regla.MatrizValores);
                    exito = new DATarifaReglaValor().InsertarXML(xmlMatriz);
                }

                transa.Complete();
            }
            return codigoGen;
        }

        public List<BETarifaRegla> Listar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int periodocidad, int confecha,int pagina, int cantRegxPag)
        {
            return new DATarifaRegla().Listar(owner, desc, nro, fini, ffin, estado, periodocidad, confecha,pagina, cantRegxPag);
        }

        public BETarifaRegla Obtener(string owner, decimal id)
        {
            var regla = new DATarifaRegla().Obtener(owner, id);
            if (regla != null)
            {
                regla.Caracteristicas = new DATarifaReglaData().Listar(owner, id);
                regla.Valores = new DATarifaPlantillaValor().ListarValorRegla(owner, regla.TEMP_ID);
                regla.MatrizValores = new DATarifaReglaValor().ObtenerDatosListar(owner, id);
                regla.CantReglaAsocMant = new DATarifaReglaAsociada().CantReglaAsocMant(owner, id);
                regla.CantCarMatriz = new DATarifaReglaValor().ObtenerCantidadCarMatriz(owner, id);
            }
            return regla;
        }

        public BETarifaPlantilla ObtenerPlantilla(string owner, decimal id, out int cant)
        {
            int count = 0;
            var tarifa = new DATarifaPlantilla().Obtener(owner, id);
            if (tarifa != null)
            {
                tarifa.Caracteristicas = new DATarifaPlantillaCaracteristica().Listar(owner, id);
                tarifa.Valores = new DATarifaPlantillaValor().ListarValorRegla(owner, id);
                tarifa.ValoresMatriz = new DATarifaReglaValor().Listar(owner, tarifa.TEMP_ID, out count);
            }
            cant = count;
            return tarifa;
        }

        public BETarifaPlantilla ObtenerPlantillaCaracteristicaEliminado(string owner, decimal idPlantilla,string chars, out int cant)
        {
            int count = 0;
            var tarifa = new DATarifaPlantilla().Obtener(owner, idPlantilla);
            if (tarifa != null)
            {
                tarifa.Valores = new DATarifaPlantillaValor().ListarValorReglaCaracteristicaEliminado(owner, idPlantilla, chars);
                tarifa.ValoresMatriz = new DATarifaReglaValor().ListarCaracteristicaEliminado(owner, idPlantilla ,chars, out count);
            }
            cant = count;
            return tarifa;
        }

        public int Actualizar(BETarifaRegla regla,
                              List<BETarifaReglaData> carEliminar,
                              List<BETarifaReglaData> listCarActivar
                        )
        {
            int upd = 0;
            bool exito = false;
            string xmlMatriz = string.Empty;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DATarifaRegla().Actualizar(regla);

                DATarifaReglaData proxyCar = new DATarifaReglaData();
                DATarifaReglaValor proxyVal = new DATarifaReglaValor();
                
                if (regla.Caracteristicas != null)
                {
                    foreach (var item in regla.Caracteristicas)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva caracteristicas
                        BETarifaReglaData proxyCarObtener = proxyCar.Obtener(regla.OWNER, regla.CALR_ID, item.CALRD_ID);
                        item.CALR_ID = regla.CALR_ID;
                        if (proxyCarObtener == null)
                        {
                            var codigoGenAdd = proxyCar.Insertar(item);
                        }
                        else
                        {
                            if (proxyCarObtener.CALRD_VAR != item.CALRD_VAR)
                            {
                                var codigoGenAdd = proxyCar.Actualizar(item);
                            }
                        }
                    }
                }

                /// se elimina las caracteristicas
                if (carEliminar != null)
                {
                    foreach (var item in carEliminar)
                    {
                        int r = proxyCar.Eliminar(new BETarifaReglaData()
                        {
                            OWNER = regla.OWNER,
                            CALRD_ID = item.CALRD_ID,
                            CALR_ID = regla.CALR_ID,
                            LOG_USER_UPDATE = regla.LOG_USER_UPDATE
                        });
                    }
                }
                
                if (regla.MatrizValores != null)
                {
                    int cantValores = proxyVal.ObtenerCantidadValores(regla.OWNER, regla.CALR_ID);

                    if (cantValores == regla.MatrizValores.Count) //se Actualizanran valores 
                    {
                        foreach (var valMatriz in regla.MatrizValores)
                        {
                            valMatriz.CALR_ID = regla.CALR_ID;
                            valMatriz.LOG_USER_UPDATE = regla.LOG_USER_UPDATE;
                            BETarifaReglaValor proxyValObtener = proxyVal.Obtener(valMatriz);
                            if (proxyValObtener == null)
                            {
                                var codigoGenCar = proxyVal.Insertar(valMatriz);
                            }
                            else
                            {
                                if (proxyValObtener.VAL_FORMULA != valMatriz.VAL_FORMULA || proxyValObtener.VAL_MINIMUM != valMatriz.VAL_MINIMUM)
                                {
                                    var codigoGenAdd = proxyVal.Actualizar(valMatriz);
                                }
                            }
                        }
                    }
                    else //Se regstran nuevos valores
                    {
                        var valEliminados = new DATarifaReglaValor().Eliminar(new BETarifaReglaValor {OWNER=regla.OWNER,
                                                                                                      CALR_ID=regla.CALR_ID,
                                                                                                      LOG_USER_UPDATE=regla.LOG_USER_UPDATE
                        });
                        if (valEliminados > 0)
                        {
                            foreach (var valMatriz in regla.MatrizValores)
                            {
                                valMatriz.CALR_ID = regla.CALR_ID;
                                valMatriz.CALRV_STARTS = regla.STARTS;
                                valMatriz.LOG_USER_CREAT = regla.LOG_USER_UPDATE;
                                //var codigoGenCar = new DATarifaReglaValor().Insertar(valMatriz);
                            }
                            xmlMatriz = Utility.Util.SerializarEntity(regla.MatrizValores);
                            exito = new DATarifaReglaValor().InsertarXML(xmlMatriz);
                            

                        }
                    }
                }

                transa.Complete();
            }
            return upd;
        }

        public List<BETarifaRegla> ListarRegla(string owner)
        {
            return new DATarifaRegla().ListarRegla(owner);
        }


        public int Eliminar(BETarifaRegla regla)
        {
            return new DATarifaRegla().Eliminar(regla);
        }

                
    }
}
