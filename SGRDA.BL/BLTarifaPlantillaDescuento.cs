using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using SGRDA.Utility;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLTarifaPlantillaDescuento
    {
        public int Insertar(BETarifaPlantillaDescuento plantilla)
        {
            var codigoGenIdPlantilla = 0;
            codigoGenIdPlantilla = new DATarifaPlantillaDescuento().Insertar(plantilla);

            if (plantilla.LstDscCaracteristica != null)
            {
                foreach (var caracteristica in plantilla.LstDscCaracteristica)
                {
                    caracteristica.TEMP_ID_DSC = codigoGenIdPlantilla;
                    var codigoGenCar = new DATarifaPlantillaDescuentoCaracteristica().Insertar(caracteristica);
                    foreach (var seccion in plantilla.LstDscSeccion.Where(p => p.TEMP_ID_DSC_CHAR == caracteristica.TEMP_ID_DSC_CHAR))
                    {
                        seccion.TEMP_ID_DSC = caracteristica.TEMP_ID_DSC;
                        seccion.TEMP_ID_DSC_CHAR = codigoGenCar;
                        var codigoGenValAdd = new DATarifaPlantillaDescuentoSeccion().Insertar(seccion);

                        //Setear Valores de la Matriz.
                        seccion.TEMP_ID_DSC_VAL_ANT = seccion.TEMP_ID_DSC_VAL;
                        seccion.TEMP_ID_DSC_VAL = codigoGenValAdd;

                        foreach (var valor in plantilla.LstDscValores)
                        {
                            if (valor.TEMP_ID_DSC_VAL_1 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_1 = seccion.TEMP_ID_DSC_VAL;

                            if (valor.TEMP_ID_DSC_VAL_2 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_2 = seccion.TEMP_ID_DSC_VAL;

                            if (valor.TEMP_ID_DSC_VAL_3 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_3 = seccion.TEMP_ID_DSC_VAL;
                        }
                    }
                }
            }

            //Matriz
            if (plantilla.LstDscValores != null)
            {
                foreach (var item in plantilla.LstDscValores)
                {
                    item.TEMP_ID_DSC = codigoGenIdPlantilla;
                    var registrar = new DATarifaPlantillaDescuentoValores().Insertar(item);
                }
            }
            return codigoGenIdPlantilla;
        }

        public int Actualizar(BETarifaPlantillaDescuento plantilla,
                    List<BETarifaPlantillaDescuentoCaracteristica> carEliminar, //List<BETarifaPlantillaDescuentoCaracteristica> listCarActivar,
                    List<BETarifaPlantillaDescuentoSeccion> valEliminar  //,List<BETarifaPlantillaDescuentoSeccion> listValActivar
                    )
        {
            int upd = 0;

            //using (TransactionScope transa = new TransactionScope())
            //{
            upd = new DATarifaPlantillaDescuento().Actualizar(plantilla);
            DATarifaPlantillaDescuentoCaracteristica proxyCar = new DATarifaPlantillaDescuentoCaracteristica();
            DATarifaPlantillaDescuentoSeccion proxyVal = new DATarifaPlantillaDescuentoSeccion();


            // Caracteristicas -- Agregar o Actualizar
            if (plantilla.LstDscCaracteristica != null)
            {
                foreach (var item in plantilla.LstDscCaracteristica)
                {
                    item.TEMP_ID_DSC = plantilla.TEMP_ID_DSC;
                    BETarifaPlantillaDescuentoCaracteristica proxyCarObtener = proxyCar.Obtener(plantilla.OWNER, plantilla.TEMP_ID_DSC, item.TEMP_ID_DSC_CHAR);
                    if (proxyCarObtener == null)
                    {
                        var codigoGenAdd = proxyCar.Insertar(item);
                    }
                    else
                    {
                        if (item.CHAR_ID != proxyCarObtener.CHAR_ID || item.IND_TR != proxyCarObtener.IND_TR)
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyCar.Actualizar(item);
                        }
                    }
                }
            }

            // Eliminar las Caracteristicas
            if (carEliminar != null)
            {
                foreach (var item in carEliminar)
                {
                    int r = proxyCar.Eliminar(new BETarifaPlantillaDescuentoCaracteristica()
                    {
                        OWNER = plantilla.OWNER,
                        TEMP_ID_DSC_CHAR = item.TEMP_ID_DSC_CHAR,
                        TEMP_ID_DSC = plantilla.TEMP_ID_DSC,
                        LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE
                    });
                }
            }

            // Sección X Caracteristica - Agregar o Actualizar
            if (plantilla.LstDscSeccion != null)
            {
                foreach (var seccion in plantilla.LstDscSeccion)
                {
                    seccion.TEMP_ID_DSC = plantilla.TEMP_ID_DSC;
                    BETarifaPlantillaDescuentoSeccion proxyValObtener = proxyVal.Obtener(seccion.OWNER, seccion.TEMP_ID_DSC_VAL, seccion.TEMP_ID_DSC_CHAR, seccion.TEMP_ID_DSC);
                    if (proxyValObtener == null)
                    {
                        var codigoGenValAdd = proxyVal.Insertar(seccion);
                        //Setear Valores de la Matriz.
                        seccion.TEMP_ID_DSC_VAL_ANT = seccion.TEMP_ID_DSC_VAL;
                        seccion.TEMP_ID_DSC_VAL = codigoGenValAdd;

                        foreach (var valor in plantilla.LstDscValores)
                        {
                            if (valor.TEMP_ID_DSC_VAL_1 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_1 = seccion.TEMP_ID_DSC_VAL;

                            if (valor.TEMP_ID_DSC_VAL_2 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_2 = seccion.TEMP_ID_DSC_VAL;

                            if (valor.TEMP_ID_DSC_VAL_3 == seccion.TEMP_ID_DSC_VAL_ANT)
                                valor.TEMP_ID_DSC_VAL_3 = seccion.TEMP_ID_DSC_VAL;
                        }

                    }
                    else
                    {   ///sino  solo se actualiza la informacion de la seccion 
                        if (seccion.SECC_DESC != proxyValObtener.SECC_DESC || seccion.SECC_FROM != proxyValObtener.SECC_FROM ||
                            seccion.SECC_TO != proxyValObtener.SECC_TO)
                        {
                            seccion.LOG_USER_UPDATE = seccion.LOG_USER_CREAT;
                            var result = proxyVal.Actualizar(seccion);
                        }
                    }
                }
            }

            // Se Eliminan los Valores
            if (valEliminar != null)
            {
                foreach (var item in valEliminar)
                {
                    item.OWNER = plantilla.OWNER;
                    item.TEMP_ID_DSC = plantilla.TEMP_ID_DSC;
                    item.LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE;
                    int r = proxyVal.Eliminar(item);
                }
            }

            if (plantilla.MATRIZ_CHANGE == 1) // Se Genera la Matriz Nuevamente
            {
                int eliminar = new DATarifaPlantillaDescuentoValores().EliminarMatriz(plantilla);
            }

            DATarifaPlantillaDescuentoValores proxyMatriz = new DATarifaPlantillaDescuentoValores();
            if (plantilla.LstDscValores != null)
            {
                foreach (var valorMatriz in plantilla.LstDscValores)
                {
                    valorMatriz.TEMP_ID_DSC = plantilla.TEMP_ID_DSC;
                    BETarifaPlantillaDescuentoValores valObtener = proxyMatriz.Obtener(valorMatriz.OWNER, valorMatriz.TEMP_ID_DSC_MAT, valorMatriz.TEMP_ID_DSC);
                    if (valObtener != null && valObtener.VAL_FORMULA != valorMatriz.VAL_FORMULA)
                    {
                        valorMatriz.LOG_USER_UPDATE = plantilla.LOG_USER_UPDATE;
                        var result = proxyMatriz.Actualizar(valorMatriz);
                    }
                    else
                    {
                        valorMatriz.TEMP_ID_DSC = plantilla.TEMP_ID_DSC;
                        var registrar = new DATarifaPlantillaDescuentoValores().Insertar(valorMatriz);
                    }

                }
            }

            //    transa.Complete();
            //}
            return upd;
        }


        public BETarifaPlantillaDescuento Obtener(string owner, decimal id)
        {
            var plantilla = new DATarifaPlantillaDescuento().Obtener(owner, id);
            if (plantilla != null)
            {
                plantilla.LstDscCaracteristica = new DATarifaPlantillaDescuentoCaracteristica().Listar(owner, id);
                plantilla.LstDscSeccion = new DATarifaPlantillaDescuentoSeccion().Listar(owner, id);
                plantilla.LstDscValores = new DATarifaPlantillaDescuentoValores().Listar(owner, id); // Valores de la Matriz
            }
            return plantilla;
        }

        public List<BETarifaPlantillaDescuento> Listar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int confecha, int pagina, int cantRegxPag)
        {
            return new DATarifaPlantillaDescuento().Listar(owner, desc, nro, fini, ffin, estado, confecha, pagina, cantRegxPag);
        }

        public List<BETarifaPlantillaDescuentoValores> GenerarMatriz(List<BETarifaPlantillaDescuentoSeccion> ListaValores, int Cantidad)
        {
            string xmlGenerarMatriz = string.Empty;
            xmlGenerarMatriz = Utility.Util.SerializarEntity(ListaValores);
            var ListaMatriz = new DATarifaPlantillaDescuentoValores().GenerarMatrizTemporalXML(xmlGenerarMatriz, Cantidad);
            return ListaMatriz;
        }
    }


}
