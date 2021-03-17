using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDescuentos
    {
        #region clases
        /// <summary>
        /// CLASE DONDE SE ENCUENTRA LAS VARIABLES A UTILIZAR PARA LOS DESCUENTOS DE LA PLANTILLA
        /// </summary>
        public class DescPlantilla
        {
            #region Generales
            public const int ES_MEGACONCIERTO = 1;
            #endregion
            #region DESCUENTOS UIT
            public const int TRIBUNA_VIP_UIT = 56;
            public const int ARTISTA_VIP_UIT = 58;
            public const int ARTISTA_GENERAL_UIT = 54;
            #endregion
            #region MEGACONCIERTO-GENERAL
            //***ID DE LOS DESCUENTOS
            public const int ID_TRIBUNA_APDAYC_GENERAL = 39;
            public const int ID_PRONTO_PAGO_GENERAL = 41;
            public const int ID_ARTISTA_NACIONAL_GENERAL = 42;
            public const int ID_ENTRADA_CORTESIA_GENERAL = 43;
            //ID DE LAS CARACTERISTICAS
            public const int ID_CARACT_TRIBUNA = 81;
            //VALORES CARACTERISTICAS
            public const int VAL_TRIBUNA_GENERAL = 0;
            #endregion

            #region VIENEDELBLFACTURA
            /// <summary>
            /// SI = 1
            /// </summary>
            public const int SI = 1;
            /// <summary>
            /// NO = 0
            /// </summary>
            public const int NO = 0;
            /// <summary>
            /// ANUAL =12
            /// </summary>
            public const int ANUAL = 12;
            /// <summary>
            /// SEMESTRAL = 6
            /// </summary>
            public const int SEMESTRAL = 6;
            /// <summary>
            /// ABIERTO=A
            /// </summary>
            public const string ABIERTO = "A";
            /// <summary>
            /// PENDIENTE=P
            /// </summary>
            public const string PENDIENTE = "P";
            /// <summary>
            /// DESCUENTO PARA USUARIOS GENERALES DE LOCALES PERMANENTES (NO CADENAS) //DISC_ID
            /// </summary>
            public const int DESCUENTO_INDIVIDUAL = 35;
            /// <summary>
            /// USUARIOS DE MUSICA INDISPENSABLE
            /// </summary>
            public const int MUSICA_INDISPENSABLE = 0;
            #endregion
        }
        #endregion

        public List<BEDescuentos> Listar_Page_Descuentos(decimal tipo, string param, int st, int pagina, int cantRegxPag)
        {
            return new DADescuentos().Listar_Page_Descuentos(tipo, param, st, pagina, cantRegxPag);
        }

        public int Insertar(BEDescuentos ins)
        {
            return new DADescuentos().Insertar(ins);
        }

        public int Actualizar(BEDescuentos upd)
        {
            return new DADescuentos().Actualizar(upd);
        }

        public int Eliminar(BEDescuentos del)
        {
            return new DADescuentos().Eliminar(del);
        }

        public List<BEDescuentos> Obtener(string owner, decimal id)
        {
            return new DADescuentos().Obtener(owner, id);
        }

        public List<BEDescuentos> ListarCombo(string owner, decimal idTipo)
        {
            return new DADescuentos().ListarCombo(owner, idTipo);
        }
        public List<BEDescuentos> DescuentoPorTarifa(string owner, decimal idTipo,decimal idTarifa)
        {
            return new DADescuentos().DescuentoPorTarifa(owner, idTipo, idTarifa);
        }
        public int InsertarTarifaDescuento(BETarifaDescuento descuento)
        {
            return new DATarifaDescuento().Insertar(descuento);
        }
        #region Descuentos Socios

        public int InsertarDescuentoSocioBPS(decimal BPSID, decimal DICID, string Usuario_Actual, string observacion)
        {


            return new DADescuentos().InsertarDescuentoSocioBPS(BPSID, DICID, Usuario_Actual, observacion);
        }

        //Inserta EN LA TABLA REC_DISCOUNTS

        public int InsertaDescuentoSoc(BEDescuentos descuentos)
        {

            return new DADescuentos().InsertaDescuentoSoc(descuentos);
        }

        //Listar Descuentos por SOcio
        public List<BEDescuentos> Listar_Descuentos_Socio(decimal bpsid)
        {

            return new DADescuentos().Listar_Descuentos_Socio(bpsid);
        }

        public int InactivaDescuentosSocio(decimal discid, decimal bpsid, decimal bpsgroup, string usuario)
        {

            return new DADescuentos().InactivaDescuentosSocio(discid, bpsid, bpsgroup, usuario);
        }

        //Activa Descuentos por Socio
        public int ActivaDescuentosSocio(decimal discid, decimal bpsid, decimal bpsgroup, string usuario)
        {

            return new DADescuentos().ActivaDescuentosSocio(discid, bpsid, bpsgroup, usuario);
        }
        //obtiene El descuento total Por Socio
        public List<BEDescuentos> ObtieneTotalDescuentoSoc(decimal licid)
        {
            return new DADescuentos().ObtieneTotalDescuentoSoc(licid);
        }

        //Lista los Descuentos por Tipo de Descuentos
        public List<BEDescuentos> ListaDescuentosxTipoDesc(decimal desctype)
        {

            return new DADescuentos().ListarDescuentosxTipoDesc(desctype);
        }

        public decimal RecuperaCodigodeSOcio(decimal licid)
        {
            decimal r = 0;

            r = new DADescuentos().RecuperaCodigodeSOcio(licid);

            return r;
        }

        //Recupera las LICID de SOCIO
        public List<BEDescuentos> ListaLicIdxSocio(string owner, decimal BPSID)
        {
            return new DADescuentos().ObtenerLicIdxSocio(owner, BPSID);
        }
        //Vlida Periodos por descuento
        public int ValidaPeriodoDescuento(string owner, decimal liplid)
        {
            return new DADescuentos().ValidaPeridoDescuento(owner, liplid);
        }
        //Inactiva Descuentos de licencia de Grupo Empresarial
        public int InactivaDesSocioxGrupoEmpresarial(string owner, decimal bpsid, decimal bpsidgroup)
        {
            return new DADescuentos().InactivaDesSocioxGrupoEmpresarial(owner, bpsid, bpsidgroup);
        }

        //Lista Plantilla de Descuentos

        public List<BEDescuentosPlantilla> listaDescuentoPlantilla(int page, int pagesize, string owner, string desc, DateTime? fecha)
        {

            return new DADescuentos().listaDescuentoPlantilla(page, pagesize, owner, desc, fecha);
        }


        public List<BEDescuentosPlantilla> listaDescuentoPlantillasinPaginado(string owner, decimal descID)
        {

            return new DADescuentos().listaDescuentoPlantillasinPaginado(owner, descID);
        }

        public int ActivaDescuentosGrupoEmpresarialXML(decimal BPS_ID_GROUP, List<BEDescuentos> listaxml)
        {

            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listaxml);
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().ActivaDescuentosGrupoEmpresarialXML(BPS_ID_GROUP, xmlLicencia);

        }

        public int InactivaDescuentosGrupoEmpresarialXML(decimal BPS_ID_GROUP, List<BEDescuentos> listaxml)
        {

            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listaxml);
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().InactivaDescuentosGrupoEmpresarialXML(BPS_ID_GROUP, xmlLicencia);

        }
        //Lista Licencias y descuentos que no poseen descuentos de el Grupo Empresarial
        public List<BEDescuentos> ListaLicenciasDescuentos(decimal DISC_ID, decimal BPS_ID)
        {

            return new DADescuentos().ListaLicenciasDescuentos(DISC_ID, BPS_ID);
        }
        #endregion
        #region  Descuento Plantilla
        /// <summary>
        /// DEVUELVE EL VALOR DE LA FORMULA DE PLANTILLA DE DESCUENTO
        /// </summary>
        /// <param name="DISC_ID"></param>
        /// <param name="valor1"></param>
        /// <param name="CHAR_ID1"></param>
        /// <param name="valor2"></param>
        /// <param name="CHAR_ID2"></param>
        /// <param name="valor3"></param>
        /// <param name="CHAR_ID3"></param>
        /// <returns></returns>
        public decimal ObtieneDescuentoPlantillaCadena(decimal DISC_ID, decimal? valor1, decimal? CHAR_ID1, decimal? valor2, decimal? CHAR_ID2, decimal? valor3, decimal? CHAR_ID3)
        {

            string owner = GlobalVars.Global.OWNER;

            return new DADescuentos().ObtieneDescuentoPlantillaCadena(owner, DISC_ID, valor1, CHAR_ID1, valor2, CHAR_ID2, valor3, CHAR_ID3);
        }
        /// <summary>
        /// valida si un descuento posee Plantilla
        /// </summary>
        /// <param name="DISC_ID"></param>
        /// <returns></returns>
        public int ValidaDescuentoPlantilla(decimal DISC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().ValidaDescuentoPlantilla(owner, DISC_ID);
        }

        /// <summary>
        /// obtiene las caracteristicas de un descuento segun su id plantilla
        /// </summary>
        /// <param name="DISC_ID"></param>
        /// <returns></returns>
        public List<BEDescuentosPlantilla> listaPlantillaxDISCID(decimal DISC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().listaPlantillaxDISCID(owner, DISC_ID);

        }


        #endregion

        #region Descuentos PlantillaGeneral
        /// <summary>
        ///CALCULANDO EL EL MONTO DE LA TARIFA CON LOS DESCUENTOS SEGUN LA MODALIDAD -PARA LOS BL DE FACTURA MANUAL Y FACTURA MASIVA 
        /// </summary>
        /// <param name="tarifa"></param>
        /// <param name="caracteristicastarifa"></param>
        /// <returns></returns>
        /// //        public decimal TarifaDescuentosPlantilla(DTOTarifa tarifa, List<DTOTarifaTestCaracteristica> caracteristicastarifa)
        public decimal TarifaDescuentosPlantilla(decimal vtarifa_id, decimal vmonto_tarifa, List<BETarifaCaracteristica> listaCaracteristicas)
        {
            //XXXXXXXXXXXXXXX DECLARANDO VARIABLES
            string owner = GlobalVars.Global.OWNER;//variable owner 
            int DescuentoUit = 0;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXX OBTENIENDO DATOS NECESARIOS
            decimal tarifa_id = vtarifa_id;//Recuperando El ID de la Tarifa
            decimal tarifa_monto = vmonto_tarifa;
            int respuestamodalidad = DescPlantilla.ES_MEGACONCIERTO;
            BEREC_RATES_GRAL tarifaobtener = new BLREC_RATES_GRAL().Obtener(owner, tarifa_id);//obtengo la tarifa y sus propiedades
            List<BETarifaDescuento> listaDescTarifa = tarifaobtener.Descuento;//lista de descuentos de la tarifa
            var UIT = new BLUit().ListaUit();
            decimal Valor_UIT = UIT.UITV_VALUEP;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            if (respuestamodalidad == DescPlantilla.ES_MEGACONCIERTO)//SOLO SI ES MEGACONCIERTO
            {
                foreach (var x in listaDescTarifa.OrderBy(x => x.RATE_DISC_ID))
                {
                    decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                    decimal? param1 = null, param2 = null, param3 = null;

                    var ListCaractxDesc = new BLDescuentos().listaPlantillaxDISCID(x.DISC_ID);//Obtengo las Caracteristicas ...
                    decimal valor_dsc_calc = 0;

                    if (ListCaractxDesc.Count > 0)
                    {
                        #region DESCUENTOS
                        if (ListCaractxDesc.Count == 1)
                        {
                            foreach (var y in listaCaracteristicas)
                            {
                                if (y.RATE_CALC_AR == ListCaractxDesc[0].CHAR_ID)
                                {
                                    param1 = y.LIC_CHAR_VAL;//0
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    if (x.DISC_ID == DescPlantilla.ARTISTA_GENERAL_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {
                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        DescuentoUit = 1;// para validar el calculo correspondiente
                                    }
                                }
                            }

                        }
                        if (ListCaractxDesc.Count == 2)
                        {
                            foreach (var y in listaCaracteristicas)
                            {
                                if (y.RATE_CALC_AR == ListCaractxDesc[0].CHAR_ID)
                                {
                                    param1 = y.LIC_CHAR_VAL;//0
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                }

                                if (y.RATE_CALC_AR == ListCaractxDesc[1].CHAR_ID)
                                {
                                    param2 = y.LIC_CHAR_VAL;//0
                                    CHAR2 = ListCaractxDesc[1].CHAR_ID;
                                }
                                if (param2 != null && param1 != null)
                                {
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    if (x.DISC_ID == DescPlantilla.TRIBUNA_VIP_UIT || x.DISC_ID == DescPlantilla.ARTISTA_VIP_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {
                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        DescuentoUit = 1;// para validar el calculo correspondiente
                                    }
                                }


                            }

                        }

                        #endregion

                        #region calculo
                        if (DescuentoUit == 0)
                            tarifa_monto = tarifa_monto - (tarifa_monto * valor_dsc_calc);
                        else
                        {
                            tarifa_monto = tarifa_monto - valor_dsc_calc;
                            DescuentoUit = 0;//devolver a valor 0
                        }
                        #endregion
                    }
                }
            }
            else //SI ES CADENAS 
            {
                //**REUTILIZAR CODIGO  QUE SE EMPLEO EN FACTURA
            }
            return tarifa_monto;
        }
       /// <summary>
       /// DEVUELE LA LISTA DE LOS DESCUENTOS CON LOS VALORES.
       /// </summary>
       /// <param name="vtarifa_id"></param>
       /// <param name="vmonto_tarifa"></param>
       /// <param name="listaCaracteristicas"></param>
       /// <returns></returns>
        public List<BEDescuentos> ListaDescuentosPlantilla(decimal vtarifa_id,List<BEDescuentos> listadesclicen, List<BETarifaCaracteristica> listaCaracteristicas)
        {
            //XXXXXXXXXXXXXXX DECLARANDO VARIABLES
            string owner = GlobalVars.Global.OWNER;//variable owner 
            int DescuentoUit = 0;
            List<BEDescuentos> ListaDescuentos = null;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXX OBTENIENDO DATOS NECESARIOS
            decimal tarifa_id = vtarifa_id;//Recuperando El ID de la Tarifa
            //decimal tarifa_monto = vmonto_tarifa;
            int respuestamodalidad = DescPlantilla.ES_MEGACONCIERTO;
            BEREC_RATES_GRAL tarifaobtener = new BLREC_RATES_GRAL().Obtener(owner, tarifa_id);//obtengo la tarifa y sus propiedades
            List<BETarifaDescuento> listaDescTarifa = tarifaobtener.Descuento;//lista de descuentos de la tarifa
            var UIT = new BLUit().ListaUit();
            decimal Valor_UIT = UIT.UITV_VALUEP;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            if (respuestamodalidad == DescPlantilla.ES_MEGACONCIERTO)//SOLO SI ES MEGACONCIERTO
            {
                foreach (var x in listaDescTarifa.OrderBy(x => x.RATE_DISC_ID))
                {
                    decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                    decimal? param1 = null, param2 = null, param3 = null;

                    var ListCaractxDesc = new BLDescuentos().listaPlantillaxDISCID(x.DISC_ID);//Obtengo las Caracteristicas ...
                    decimal valor_dsc_calc = 0;

                    if (ListCaractxDesc.Count > 0)
                    {
                        #region DESCUENTOS
                        if (ListCaractxDesc.Count == 1)
                        {
                            foreach (var y in listaCaracteristicas)
                            {
                                if (y.RATE_CALC_AR == ListCaractxDesc[0].CHAR_ID)
                                {

                                    param1 = y.LIC_CHAR_VAL;//0
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    x.DISC_PERC = valor_dsc_calc;
                                    x.monto = valor_dsc_calc;//para identificar un dsct plantilla
                                    if (x.DISC_ID == DescPlantilla.ARTISTA_GENERAL_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {
                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        DescuentoUit = 1;// para validar el calculo correspondiente
                                        x.DISC_VALUE = valor_dsc_calc;
                                        x.monto = valor_dsc_calc;//para identificar un dsct plantilla
                                        break;
                                    }
                                    break;
                                }
                            }

                        }
                        if (ListCaractxDesc.Count == 2)
                        {
                            foreach (var y in listaCaracteristicas)
                            {
                                if (y.RATE_CALC_AR == ListCaractxDesc[0].CHAR_ID)
                                {
                                    param1 = y.LIC_CHAR_VAL;//0
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                }

                                if (y.RATE_CALC_AR == ListCaractxDesc[1].CHAR_ID)
                                {
                                    param2 = y.LIC_CHAR_VAL;//0
                                    CHAR2 = ListCaractxDesc[1].CHAR_ID;
                                }
                                if (param2 != null && param1 != null)
                                {
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    if (x.DISC_ID == DescPlantilla.TRIBUNA_VIP_UIT || x.DISC_ID == DescPlantilla.ARTISTA_VIP_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {
                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        //DescuentoUit = 1;// para validar el calculo correspondiente
                                        x.DISC_VALUE = valor_dsc_calc;
                                        x.monto = valor_dsc_calc;
                                    }else
                                    {
                                        x.DISC_PERC = valor_dsc_calc;
                                        x.monto = valor_dsc_calc;
                                    }
                                }


                            }

                        }

                        #endregion

                    }
                }
                #region CompararListasdeDescuentos

                foreach (var x in listadesclicen)//lista de descuentos a retornar
                {
                    foreach (var y in listaDescTarifa)// lista de descuentos obtenidos y calculados
                    {
                        if (x.DISC_ID == y.DISC_ID)
                        {
                            if (x.DISC_ID == DescPlantilla.TRIBUNA_VIP_UIT || x.DISC_ID == DescPlantilla.ARTISTA_VIP_UIT || x.DISC_ID==DescPlantilla.ARTISTA_GENERAL_UIT)
                                x.DISC_VALUE = y.DISC_VALUE;//lleva montos(los que llevan UIT devuelven montos );
                            else
                                x.DISC_PERC = y.DISC_PERC; // lleva Porcentajes 
                        }
                    }
                }
                #endregion

            }
            else //SI ES CADENAS 
            {
                //**REUTILIZAR CODIGO  QUE SE EMPLEO EN FACTURA
            }
            return listadesclicen;
        }

        
        #endregion


        public int Obtiene_PlanificacionUnica_Megaconcierto(decimal LIC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().Obtiene_PlanificacionUnica_Megaconcierto(owner, LIC_ID);
        }

        public int ValidaLicenciaMegaconcierto(decimal LIC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DADescuentos().ValidaLicenciaMegaconcierto(owner, LIC_ID);
        }
        
    }
}
