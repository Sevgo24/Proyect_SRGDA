using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
namespace Proyect_Apdayc.Controllers.Tarifa
{
    public class TarifaTestController : Base
    {
        public const string nomAplicacion = "SRGDA";
        private int carOtro = new BLTarifaTest().IdCaracteristicaOtro(GlobalVars.Global.OWNER);
        public class Seleccion
        {
            public const string NO = "0";
            public const string SI = "1";
            public const string MANUAL = "2";
        }

        public class LetraCar
        {
            public const string A = "A";
            public const string B = "B";
            public const string C = "C";
            public const string D = "D";
            public const string E = "E";
        }

        public class LetraReg
        {
            public const string R = "R";
            public const string Rmin = "Rmin";
            public const string V = "V";
            public const string T = "T";
            public const string W = "W";
            public const string X = "X";
            public const string Y = "Y";
            public const string Z = "Z";
        }

        private const string K_SESION_TARIFA = "___DTOTarifa";
        private const string K_SESION_TARIFA_REGLA = "___DTOTarifaRegla";
        private const string K_SESION_TARIFA_CAR = "___DTOTarifaCar";
        private const string K_SESION_TARIFA_MATRIZ_TEST = "___DTOTarifaMatrizTest";

        // GET: /TarifaTest/
        DTOTarifa dtoTarifa = new DTOTarifa();
        List<DTOTarifaTestCaracteristica> caracteristica = new List<DTOTarifaTestCaracteristica>();
        //Lista de Caracteristica de Calculo Manual
        List<DTOTarifaTestCaracteristica> caracteristicaCalcMan = new List<DTOTarifaTestCaracteristica>();
        List<DTOTarifaManReglaAsoc> reglaAsoc = new List<DTOTarifaManReglaAsoc>();
        List<DTOTarifaTest> matrizTest = new List<DTOTarifaTest>();
        public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;
        List<BEValormusica> ListaVUM = new BLValormusica().ListarHistorico(GlobalVars.Global.OWNER);

        public ActionResult Nuevo()
        {
            Session.Remove(K_SESION_TARIFA);
            Session.Remove(K_SESION_TARIFA_REGLA);
            Session.Remove(K_SESION_TARIFA_CAR);
            Session.Remove(K_SESION_TARIFA_MATRIZ_TEST);
            Init(false);
            return View();
        }

        #region TARIFA
        public DTOTarifa TarifaTmp
        {
            get
            {
                return (DTOTarifa)Session[K_SESION_TARIFA];
            }
            set
            {
                Session[K_SESION_TARIFA] = value;
            }
        }
        #endregion

        #region REGLA
        public List<DTOTarifaManReglaAsoc> ReglaAsocTmp
        {
            get
            {
                return (List<DTOTarifaManReglaAsoc>)Session[K_SESION_TARIFA_REGLA];
            }
            set
            {
                Session[K_SESION_TARIFA_REGLA] = value;
            }
        }
        #endregion

        #region CARACTERISTICA
        public List<DTOTarifaTestCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOTarifaTestCaracteristica>)Session[K_SESION_TARIFA_CAR];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR] = value;
            }
        }
        //Creando OTRO TEMPORAL CARACTERISTICA
        public List<DTOTarifaTestCaracteristica> CaracteristicaTmpCalcMan
        {
            get
            {
                return (List<DTOTarifaTestCaracteristica>)Session[K_SESION_TARIFA_CAR];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR] = value;
            }
        }

        public string GenerarLetraCaracteristica(int contador)
        {
            string letra = string.Empty;
            switch (contador)
            {
                case 1: letra = "A"; break;
                case 2: letra = "B"; break;
                case 3: letra = "C"; break;
                case 4: letra = "D"; break;
                case 5: letra = "E"; break;
                case 6: letra = "F"; break;
                case 7: letra = "G"; break;
                default:
                    letra = "X";
                    break;
            }
            return letra;
        }

        private List<BETarifaCaracteristica> obtenerCaracteristica()
        {
            int contador = 0;
            List<BETarifaCaracteristica> datos = new List<BETarifaCaracteristica>();
            if (CaracteristicaTmp != null)
            {
                CaracteristicaTmp.ForEach(x =>
                {
                    contador += 1;
                    datos.Add(new BETarifaCaracteristica
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        RATE_CHAR_ID = Convert.ToInt32(x.Id),
                        RATE_CHAR_TVAR = GenerarLetraCaracteristica(contador),
                        RATE_CHAR_DESCVAR = x.DescripcionLarga,
                        RATE_CHAR_VARUNID = x.UnidadMedida,
                        RATE_CHAR_CARIDSW = x.IndImpresion,
                        LOG_USER_CREAT = UsuarioActual,

                        RATE_CALC_ID = x.IdElemento,
                        RATE_CALC = x.IdRegla,
                        RATE_CALC_AR = x.IdCaracteristica
                    });
                });
            }
            return datos;
        }

        public JsonResult ListarCaracteristica()
        {
            int contador = 0;
            caracteristica = CaracteristicaTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblCaracteristica' border=0 width='98%;' class='k-grid k-widget' id='tblCaracteristica'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>V</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdElemento</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdRegla</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdCaracteristica</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='padding-left:20px;text-align:left'>Descripción</th>");
                    shtml.Append("<th class='k-header' ></th>");
                    shtml.Append("</tr></thead>");

                    if (caracteristica != null)
                    {
                        foreach (var item in caracteristica.OrderBy(x => x.Id))
                        {
                            contador += 1;
                            shtml.Append("<tr class='k-grid-content'>");
                            if (item.IdCaracteristica != carOtro)
                                shtml.AppendFormat("<td style='width:30px; text-align:center' >{0}</td>", item.Letra);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdElemento);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdRegla);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdCaracteristica);
                            if (item.IdCaracteristica != carOtro)
                                shtml.AppendFormat("<td style='width:250px;' >{0}</td>", item.DescripcionCorta);
                            if (item.IdCaracteristica != carOtro)
                                shtml.AppendFormat("<td style='width:250px; text-align:left' > <input class='requerido' id='txt" + item.Letra + "' type='text' value='' style='width:120px' maxlength='16' /> </td>");

                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    if (caracteristica != null)
                    {
                        int registros = caracteristica.Count();
                        retorno.Code = registros;
                    }
                    else
                    {
                        retorno.Code = 0;
                    }
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region TEST
        public List<DTOTarifaTest> MatrizTestTmp
        {
            get
            {
                return (List<DTOTarifaTest>)Session[K_SESION_TARIFA_MATRIZ_TEST];
            }
            set
            {
                Session[K_SESION_TARIFA_MATRIZ_TEST] = value;
            }
        }
        #endregion

        #region TARIFA_TEST
        [HttpPost]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL();
                    tarifa = new BLTarifaTest().Obtener(GlobalVars.Global.OWNER, id);

                    if (tarifa != null)
                    {
                        dtoTarifa = new DTOTarifa();
                        dtoTarifa.idTarifa = tarifa.RATE_ID;
                        dtoTarifa.TarifaDesc = tarifa.RATE_DESC;
                        dtoTarifa.CantVariable = tarifa.RATE_NVAR;
                        dtoTarifa.CantCaracteristica = tarifa.RATE_NCAL;
                        dtoTarifa.Formula = tarifa.RATE_FORMULA;
                        dtoTarifa.Minima = tarifa.RATE_MINIMUM;
                        dtoTarifa.FormulaTipo = tarifa.RATE_FTIPO;
                        dtoTarifa.MinimoTipo = tarifa.RATE_MTIPO;
                        dtoTarifa.FormulaDec = tarifa.RATE_FDECI;
                        dtoTarifa.MinimoDec = tarifa.RATE_MDECI;
                        dtoTarifa.Redondeo = tarifa.RATE_REDONDEO;
                        dtoTarifa.FechaIni = tarifa.RATE_START;
                        dtoTarifa.FechaFin = tarifa.RATE_END;
                        dtoTarifa.VUM = tarifa.VUM = ObtenerVUMHistorico(ListaVUM, tarifa.RATE_START, VUM);
                        TarifaTmp = dtoTarifa;
                        if (tarifa.Regla != null)
                        {
                            reglaAsoc = new List<DTOTarifaManReglaAsoc>();
                            tarifa.Regla.ForEach(s =>
                            {
                                reglaAsoc.Add(new DTOTarifaManReglaAsoc
                                {
                                    IdRegla = s.CALR_ID,
                                    IdPlantilla = s.TEMP_ID,
                                    Elemento = s.CALR_DESC,
                                    Letra = s.RATE_CALC_VAR,
                                    Variables = s.CALR_NVAR,
                                    AjustarUnidades = s.CALR_ADJUST,
                                    AcumularTramos = s.CALR_ACCUM,
                                    Formula = s.CALC_FORMULA,
                                    FormulaTipo = s.CALR_FOR_TYPE,
                                    FormulaDec = s.CALR_FOR_DEC,
                                    Minimo = s.CALC_MINIMUM,
                                    MinimoTipo = s.CALR_MIN_TYPE,
                                    MinimoDec = s.CALR_MIN_DEC,
                                    EnBD = true,
                                });
                            });
                            ReglaAsocTmp = reglaAsoc;
                        }


                        if (tarifa.Caracteristica != null)
                        {
                            caracteristica = new List<DTOTarifaTestCaracteristica>();
                            tarifa.Caracteristica.ForEach(s =>
                            {
                                caracteristica.Add(new DTOTarifaTestCaracteristica
                                {
                                    Id = s.RATE_CHAR_ID,
                                    IdElemento = s.RATE_CALC_ID,
                                    IdRegla = s.RATE_CALC,
                                    IdCaracteristica = s.RATE_CALC_AR,
                                    Letra = s.RATE_CHAR_TVAR,
                                    Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "VARIABLE",
                                    DescripcionCorta = s.RATE_CHAR_DESCVAR,
                                    DescripcionLarga = s.RATE_CHAR_DESCVAR,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    Tramo = s.IND_TR,
                                    LetraOrigenRegla = s.CHAR_ORI_REG,
                                });
                            });
                            //CaracteristicaTmpCalcMan = caracteristicaCalcMan;
                            CaracteristicaTmpCalcMan = caracteristica;
                        }


                        if (tarifa.Test != null)
                        {
                            matrizTest = new List<DTOTarifaTest>();
                            tarifa.Test.ForEach(s =>
                            {
                                matrizTest.Add(new DTOTarifaTest
                                {
                                    //Id = s.RATE_CHAR_ID,
                                    IdPlantilla = s.TEMP_ID,
                                    IdRegla = s.CALR_ID,

                                    IdVal_1 = s.TEMPS1_ID,
                                    IdCaracteristica1 = s.CHAR1_ID,
                                    Tramo_1 = s.IND1_TR,
                                    Desde_1 = s.CRI1_FROM,
                                    Hasta_1 = s.CRI1_TO,

                                    IdVal_2 = s.TEMPS2_ID,
                                    IdCaracteristica2 = s.CHAR2_ID,
                                    Tramo_2 = s.IND2_TR,
                                    Desde_2 = s.CRI2_FROM,
                                    Hasta_2 = s.CRI2_TO,

                                    IdVal_3 = s.TEMPS3_ID,
                                    IdCaracteristica3 = s.CHAR3_ID,
                                    Tramo_3 = s.IND3_TR,
                                    Desde_3 = s.CRI3_FROM,
                                    Hasta_3 = s.CRI3_TO,

                                    IdVal_4 = s.TEMPS4_ID,
                                    IdCaracteristica4 = s.CHAR4_ID,
                                    Tramo_4 = s.IND4_TR,
                                    Desde_4 = s.CRI4_FROM,
                                    Hasta_4 = s.CRI4_TO,

                                    IdVal_5 = s.TEMPS5_ID,
                                    IdCaracteristica5 = s.CHAR5_ID,
                                    Tramo_5 = s.IND5_TR,
                                    Desde_5 = s.CRI5_FROM,
                                    Hasta_5 = s.CRI5_TO,

                                    Tarifa = s.VAL_FORMULA,
                                    Minimo = s.VAL_MINIMUM
                                });
                            });
                            MatrizTestTmp = matrizTest;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tarifa, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneVUM()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER);
                    retorno.data = Json(VUM, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneCaracteristicaTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCaracteristicaValor(List<DTOTarifaTestCaracteristica> CaracteristicaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DTOTarifaTestCaracteristica entidad = null;
                    CaracteristicaTmp.ForEach(s =>
                    {
                        entidad = new DTOTarifaTestCaracteristica();
                        if (s.IdCaracteristica == carOtro)
                            s.Valor = 1;
                        else
                        {
                            entidad = CaracteristicaValor.Where(p => p.Letra == s.Letra).FirstOrDefault();
                            s.Valor = entidad.Valor;
                        }

                    });
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(CaracteristicaTmp, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCaracteristicaValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Calcular(decimal VUMCalcular)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (var regla in ReglaAsocTmp)
                    {
                        if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.SI)
                        {
                            ATsiAUsi(regla, VUMCalcular);
                        }
                        else if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.NO)
                        {
                            ATsiAUno(regla, VUMCalcular);
                        }
                        else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.SI)
                        {
                            ATnoAUsi(regla, VUMCalcular);
                        }
                        else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.NO)
                        {
                            ATnoAUno(regla, VUMCalcular);
                        }
                    }

                    CalcularTarifa(TarifaTmp, VUMCalcular);
                    DescuentoController DescuentoCtrllr = new DescuentoController();
                    decimal TTValorFormula = DescuentoCtrllr.TarifaDescuentosPlantilla(TarifaTmp.idTarifa, TarifaTmp.ValorFormula, CaracteristicaTmp);
                    var resp = new BLTarifaTest().ObtieneTarifaDescuentoEspecial(TarifaTmp.idTarifa);
                    TarifaTmp.ValorFormula = TTValorFormula;

                    //REDONDEO ESPECIAL POR PERIODO

                    //if (resp == 1)
                    //{
                    //    double decimales = Convert.ToDouble(TarifaTmp.ValorFormula) - Convert.ToDouble((long)TarifaTmp.ValorFormula);
                    //    if (decimales > 0.5 && decimales < 1)
                    //        decimales = 0.5;
                    //    else if (decimales < 0.5)
                    //        decimales = 0;
                    //    TarifaTmp.ValorFormula = Convert.ToDecimal((long)TarifaTmp.ValorFormula + decimales);
                    //}


                    var VUM = "";
                    retorno.data = Json(TarifaTmp, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Calcular", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public DTOTarifaManReglaAsoc ATsiAUsi(DTOTarifaManReglaAsoc regla, decimal VUMcalcular)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }


            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(
                                    v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
                                    ).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
                                    ).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
                                    ).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
                                    ).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                        v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
                        ).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
            }
            #endregion

            #region Calcular
            decimal? acumularR = 0;
            decimal? acumularRminimo = 0;
            decimal? vFormula = 0; decimal? vFormulaTemp = 0;
            decimal? vMinimo = 0; decimal? vMinimoTemp = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;//

            foreach (var item in listaObtenerR)
            {
                acumularR += item.Tarifa;
                acumularRminimo += item.Minimo;
            }
            vFormula = listaObtenerR.FirstOrDefault().Tarifa;
            vMinimo = listaObtenerR.FirstOrDefault().Minimo;

            valorR = vFormula;
            valorRmin = vMinimo;//

            //if (vMinimo > vFormula)
            //    valorR = vMinimo;
            //else
            //    valorR = vFormula;

            if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            {
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.Formula;
                formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

                //foreach (var car in CaracteristicaTmp)
                foreach (var car in caracteristicas)
                {
                    if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;//

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUMcalcular);
                listaValores[7] = Convert.ToDouble(valorRmin);//

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];//
                    Tbl.Rows.Add(linea);
                }

                regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            else
            {
                regla.ValorFormula = vFormula;
                regla.ValorMinimo = vMinimo;
                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            #endregion
            return regla;
        }  // CASO 1 -OK

        public DTOTarifaManReglaAsoc ATsiAUno(DTOTarifaManReglaAsoc regla, decimal VUMcalcular)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }


            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(
                                    v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
                                    ).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
                                    ).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
                                    ).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
                                    ).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                        v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
                        ).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
            }
            #endregion

            #region CalcularAcumulado
            decimal? tempR = 1;
            decimal? tempRminimo = 1;
            decimal? sumarR = 0;
            decimal? sumarRminimo = 0;
            foreach (var item in listaObtenerR)
            {
                tempR = 1;
                tempRminimo = 1;

                if (tA == Seleccion.SI)
                {
                    if (item.Hasta_1 < vA)
                    {
                        tempR *= item.Hasta_1;
                        tempRminimo *= item.Hasta_1;
                    }
                    else
                    {
                        tempR *= (vA - (item.Desde_1 - 1));
                        tempRminimo *= (vA - (item.Desde_1 - 1));
                    }
                }

                if (tB == Seleccion.SI)
                {
                    if (item.Hasta_2 < vB)
                    {
                        tempR *= item.Hasta_2;
                        tempRminimo *= item.Hasta_2;
                    }
                    else
                    {
                        tempR *= (vB - (item.Desde_2 - 1));
                        tempRminimo *= (vB - (item.Desde_2 - 1));
                    }
                }

                if (tC == Seleccion.SI)
                {
                    if (item.Hasta_3 < vC)
                    {
                        tempR *= item.Hasta_3;
                        tempRminimo *= item.Hasta_3;
                    }
                    else
                    {
                        tempR *= (vC - (item.Desde_3 - 1));
                        tempRminimo *= (vC - (item.Desde_3 - 1));
                    }
                }

                if (tD == Seleccion.SI)
                {
                    if (item.Hasta_4 < vD)
                    {
                        tempR *= item.Hasta_4;
                        tempRminimo *= item.Hasta_4;
                    }
                    else
                    {
                        tempR *= (vD - (item.Desde_4 - 1));
                        tempRminimo *= (vD - (item.Desde_4 - 1));
                    }
                }

                if (tE == Seleccion.SI)
                {
                    if (item.Hasta_5 < vE)
                    {
                        tempR *= item.Hasta_5;
                        tempRminimo *= item.Hasta_5;
                    }
                    else
                    {
                        tempR *= (vE - (item.Desde_5 - 1));
                        tempRminimo *= (vE - (item.Desde_5 - 1));
                    }
                }

                sumarR += (tempR * item.Tarifa);
                sumarRminimo += (tempRminimo * item.Minimo);
            }
            #endregion

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;//

            #region Calcular
            vFormula = sumarR;
            vMinimo = sumarRminimo;

            valorR = vFormula;
            valorRmin = vMinimo;//

            //if (vMinimo > vFormula)
            //    valorR = vMinimo;
            //else
            //    valorR = vFormula;

            if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            {
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.Formula;
                formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

                //foreach (var car in CaracteristicaTmp)
                foreach (var car in caracteristicas)
                {
                    if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;//

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUMcalcular);
                listaValores[7] = Convert.ToDouble(valorRmin);//

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));//
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];//
                    Tbl.Rows.Add(linea);
                }

                regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            else
            {
                regla.ValorFormula = sumarR;
                regla.ValorMinimo = sumarRminimo;

                if (sumarRminimo > sumarR)
                    regla.ValorR = sumarRminimo;
                else
                    regla.ValorR = sumarR;

            }
            #endregion

            return regla;
        }  // CASO 2

        public DTOTarifaManReglaAsoc ATnoAUsi(DTOTarifaManReglaAsoc regla, decimal VUMcalcular)
        {
            #region Obtener Valores
            decimal vA = 0; string tA = string.Empty; // v=valor; t=tramo    
            decimal vB = 0; string tB = string.Empty;
            decimal vC = 0; string tC = string.Empty;
            decimal vD = 0; string tD = string.Empty;
            decimal vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }

            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vC <= v.Hasta_3).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vD <= v.Hasta_4).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vE <= v.Hasta_5).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
            }
            #endregion

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;

            #region Calcular
            vFormula = listaObtenerR.FirstOrDefault().Tarifa;
            vMinimo = listaObtenerR.FirstOrDefault().Minimo;

            valorR = vFormula;
            valorRmin = vMinimo;



            //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
            //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            //{
            decimal? Ra = 0;
            decimal? Rb = 0;
            decimal? Rc = 0;
            decimal? Rd = 0;
            decimal? Re = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            DataTable Tbl = new DataTable();

            formula = regla.Formula;
            formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);
            //var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);
            //foreach (var car in CaracteristicaTmp)
            foreach (var car in caracteristicas)
            {
                if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
            }

            listaOperandos[0] = LetraCar.A;
            listaOperandos[1] = LetraCar.B;
            listaOperandos[2] = LetraCar.C;
            listaOperandos[3] = LetraCar.D;
            listaOperandos[4] = LetraCar.E;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;
            listaOperandos[7] = LetraReg.Rmin;

            listaValores[0] = Convert.ToDouble(Ra);
            listaValores[1] = Convert.ToDouble(Rb);
            listaValores[2] = Convert.ToDouble(Rc);
            listaValores[3] = Convert.ToDouble(Rd);
            listaValores[4] = Convert.ToDouble(Re);

            listaValores[5] = Convert.ToDouble(valorR);
            listaValores[6] = Convert.ToDouble(VUMcalcular);
            listaValores[7] = Convert.ToDouble(valorRmin);

            //Tbl.Columns.Add("variable", typeof(string));
            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add(listaOperandos[7], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                linea[7] = listaValores[7];
                Tbl.Rows.Add(linea);
            }


            regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
            regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

            if (regla.ValorMinimo > regla.ValorFormula)
                regla.ValorR = regla.ValorMinimo;
            else
                regla.ValorR = regla.ValorFormula;
            //}
            //else
            //{
            //    regla.ValorFormula = vFormula;
            //    regla.ValorMinimo = vMinimo;
            //    if (regla.ValorMinimo > regla.ValorFormula)
            //        regla.ValorR = regla.ValorMinimo;
            //    else
            //        regla.ValorR = regla.ValorFormula;
            //}
            #endregion
            return regla;
        }  // CASO 3 - OK

        public DTOTarifaManReglaAsoc ATnoAUno(DTOTarifaManReglaAsoc regla, decimal VUMcalcular)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;
            decimal? vFormulaTemp = 0;
            decimal? vMinimoTemp = 0;
            decimal? acumular = 1;

            var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }

            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                {
                    listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
                    acumular *= vA;
                }
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
                    acumular *= vB;
                }
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vC <= v.Hasta_3).ToList();
                    acumular *= vC;
                }
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vD <= v.Hasta_4).ToList();
                    acumular *= vD;
                }
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vE <= v.Hasta_5).ToList();
                    acumular *= vE;
                }
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
            }
            #endregion

            #region Calcular

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;

            vFormulaTemp = listaObtenerR.FirstOrDefault().Tarifa;
            vMinimoTemp = listaObtenerR.FirstOrDefault().Minimo;

            vFormula = acumular * vFormulaTemp;
            vMinimo = acumular * vMinimoTemp;

            valorR = vFormula;
            valorRmin = vMinimo;//


            //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
            //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            //{
            decimal? Ra = 0;
            decimal? Rb = 0;
            decimal? Rc = 0;
            decimal? Rd = 0;
            decimal? Re = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            DataTable Tbl = new DataTable();

            formula = regla.Formula;
            formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

            //foreach (var car in CaracteristicaTmp)
            foreach (var car in caracteristicas)
            {
                if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
            }

            listaOperandos[0] = LetraCar.A;
            listaOperandos[1] = LetraCar.B;
            listaOperandos[2] = LetraCar.C;
            listaOperandos[3] = LetraCar.D;
            listaOperandos[4] = LetraCar.E;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;
            listaOperandos[7] = LetraReg.Rmin;//

            listaValores[0] = Convert.ToDouble(Ra);
            listaValores[1] = Convert.ToDouble(Rb);
            listaValores[2] = Convert.ToDouble(Rc);
            listaValores[3] = Convert.ToDouble(Rd);
            listaValores[4] = Convert.ToDouble(Re);

            listaValores[5] = Convert.ToDouble(valorR);
            listaValores[6] = Convert.ToDouble(VUMcalcular);
            listaValores[7] = Convert.ToDouble(valorRmin);//

            //Tbl.Columns.Add("variable", typeof(string));
            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add(listaOperandos[7], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                linea[7] = listaValores[7];//
                Tbl.Rows.Add(linea);
            }

            regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
            regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

            if (regla.ValorMinimo > regla.ValorFormula)
                regla.ValorR = regla.ValorMinimo;
            else
                regla.ValorR = regla.ValorFormula;
            //}
            //else
            //{
            //    regla.ValorFormula = vFormula;
            //    regla.ValorMinimo = vMinimo;

            //    if (regla.ValorMinimo > regla.ValorFormula)
            //        regla.ValorR = regla.ValorMinimo;
            //    else
            //        regla.ValorR = regla.ValorFormula;
            //}


            #endregion

            return regla;
        }  // CASO 4 - OK

        public void CalcularTarifa(DTOTarifa tarifa, decimal VUMcalcular)
        {
            decimal? Rt = 0;
            decimal? Rw = 0;
            decimal? Rx = 0;
            decimal? Ry = 0;
            decimal? Rz = 0;
            //MINIMO
            decimal? Mt = 0;
            decimal? Mw = 0;
            decimal? Mx = 0;
            decimal? My = 0;
            decimal? Mz = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            //MIN
            double[] listaValoresMin = new double[10];
            DataTable Tbl = new DataTable();
            //MIN
            DataTable TblMin = new DataTable();

            formula = tarifa.Formula;
            formulaMinima = tarifa.Minima;

            foreach (var regla in ReglaAsocTmp)
            {
                if (regla.Letra == LetraReg.T) Rt = regla.ValorFormula;
                if (regla.Letra == LetraReg.T) Mt = regla.ValorMinimo;
                if (regla.Letra == LetraReg.W) Rw = regla.ValorFormula;
                if (regla.Letra == LetraReg.W) Mw = regla.ValorMinimo;
                if (regla.Letra == LetraReg.X) Rx = regla.ValorFormula;
                if (regla.Letra == LetraReg.X) Mx = regla.ValorMinimo;
                if (regla.Letra == LetraReg.Y) Ry = regla.ValorFormula;
                if (regla.Letra == LetraReg.Y) My = regla.ValorMinimo;
                if (regla.Letra == LetraReg.Z) Rz = regla.ValorFormula;
                if (regla.Letra == LetraReg.Z) Mz = regla.ValorMinimo;
                //if (regla.Letra == LetraReg.T) Rt = regla.ValorR;
                //if (regla.Letra == LetraReg.W) Rw = regla.ValorR;
                //if (regla.Letra == LetraReg.X) Rx = regla.ValorR;
                //if (regla.Letra == LetraReg.Y) Ry = regla.ValorR;
                //if (regla.Letra == LetraReg.Z) Rz = regla.ValorR;
            }

            listaOperandos[0] = LetraReg.T;
            listaOperandos[1] = LetraReg.W;
            listaOperandos[2] = LetraReg.X;
            listaOperandos[3] = LetraReg.Y;
            listaOperandos[4] = LetraReg.Z;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;

            listaValores[0] = Convert.ToDouble(Rt);
            listaValores[1] = Convert.ToDouble(Rw);
            listaValores[2] = Convert.ToDouble(Rx);
            listaValores[3] = Convert.ToDouble(Ry);
            listaValores[4] = Convert.ToDouble(Rz);
            listaValores[5] = 0;
            listaValores[6] = Convert.ToDouble(VUMcalcular);

            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                Tbl.Rows.Add(linea);
            }

            if (tarifa.Redondeo == 1)
                tarifa.ValorFormula = RedondeoTarifa(Convert.ToDecimal(Tbl.Rows[0][7].ToString()));
            else
                tarifa.ValorFormula = Math.Round(Convert.ToDecimal(Tbl.Rows[0][7].ToString()), 2);
            //Convert.ToInt32(tarifa.FormulaDec));


            //MIN
            listaValoresMin[0] = Convert.ToDouble(Mt);
            listaValoresMin[1] = Convert.ToDouble(Mw);
            listaValoresMin[2] = Convert.ToDouble(Mx);
            listaValoresMin[3] = Convert.ToDouble(My);
            listaValoresMin[4] = Convert.ToDouble(Mz);
            listaValoresMin[5] = 0;
            listaValoresMin[6] = Convert.ToDouble(VUMcalcular);

            TblMin.Columns.Add(listaOperandos[0], typeof(double));
            TblMin.Columns.Add(listaOperandos[1], typeof(double));
            TblMin.Columns.Add(listaOperandos[2], typeof(double));
            TblMin.Columns.Add(listaOperandos[3], typeof(double));
            TblMin.Columns.Add(listaOperandos[4], typeof(double));
            TblMin.Columns.Add(listaOperandos[5], typeof(double));
            TblMin.Columns.Add(listaOperandos[6], typeof(double));
            TblMin.Columns.Add("Tarifa", typeof(double), formula);
            TblMin.Columns.Add("Minimo", typeof(double), formulaMinima);
            {
                // crea una nueva línea 
                DataRow lineaMin = TblMin.NewRow();
                lineaMin[0] = listaValoresMin[0];
                lineaMin[1] = listaValoresMin[1];
                lineaMin[2] = listaValoresMin[2];
                lineaMin[3] = listaValoresMin[3];
                lineaMin[4] = listaValoresMin[4];
                lineaMin[5] = listaValoresMin[5];
                lineaMin[6] = listaValoresMin[6];
                TblMin.Rows.Add(lineaMin);
            }
            if (tarifa.Redondeo == 1)
                tarifa.ValorMinimo = RedondeoTarifa(Convert.ToDecimal(TblMin.Rows[0][8].ToString()));
            else
                tarifa.ValorMinimo = Math.Round(Convert.ToDecimal(TblMin.Rows[0][8].ToString()), 2);
            //Convert.ToInt32(tarifa.FormulaDec));

        }

        #endregion




        [HttpPost]
        public JsonResult ObtenerValorTestTarifa(decimal idTarifa, decimal idLicencia, decimal idLicPlan)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {


                    var caracteristicas = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, idLicencia, "0", idLicPlan);
                    if (caracteristicas != null && caracteristicas.Count > 0)
                    {
                        decimal VUMObtenido = 0;
                        BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL();
                        BLTarifaTest objTarifa = new BLTarifaTest();
                        var periodo = new BLLicenciaPlaneamiento().ObtenerPlanificacion(GlobalVars.Global.OWNER, idLicPlan);
                        var idTarifaActual = objTarifa.ObtenerTarifaActual(GlobalVars.Global.OWNER, idTarifa, periodo.LIC_DATE);
                        tarifa = objTarifa.Obtener(GlobalVars.Global.OWNER, idTarifaActual);
                        VUMObtenido = (periodo.LIC_CREQ == Constantes.CaracteristicaReq.SI) ? ObtenerVUMHistorico(ListaVUM, periodo.LIC_DATE, VUM) : VUM;

                        if (tarifa != null)
                        {
                            dtoTarifa = new DTOTarifa();
                            dtoTarifa.idTarifa = tarifa.RATE_ID;
                            dtoTarifa.TarifaDesc = tarifa.RATE_DESC;
                            dtoTarifa.CantVariable = tarifa.RATE_NVAR;
                            dtoTarifa.CantCaracteristica = tarifa.RATE_NCAL;
                            dtoTarifa.Formula = tarifa.RATE_FORMULA;
                            dtoTarifa.Minima = tarifa.RATE_MINIMUM;
                            dtoTarifa.FormulaTipo = tarifa.RATE_FTIPO;
                            dtoTarifa.MinimoTipo = tarifa.RATE_MTIPO;
                            dtoTarifa.FormulaDec = tarifa.RATE_FDECI;
                            dtoTarifa.MinimoDec = tarifa.RATE_MDECI;
                            dtoTarifa.Redondeo = tarifa.RATE_REDONDEO;
                            TarifaTmp = dtoTarifa;


                            #region INIT REGLAS
                            if (tarifa.Regla != null)
                            {
                                reglaAsoc = new List<DTOTarifaManReglaAsoc>();
                                tarifa.Regla.ForEach(s =>
                                {
                                    reglaAsoc.Add(new DTOTarifaManReglaAsoc
                                    {
                                        IdRegla = s.CALR_ID,
                                        IdPlantilla = s.TEMP_ID,
                                        Elemento = s.CALR_DESC,
                                        Letra = s.RATE_CALC_VAR,
                                        Variables = s.CALR_NVAR,
                                        AjustarUnidades = s.CALR_ADJUST,
                                        AcumularTramos = s.CALR_ACCUM,
                                        Formula = s.CALC_FORMULA,
                                        FormulaTipo = s.CALR_FOR_TYPE,
                                        FormulaDec = s.CALR_FOR_DEC,
                                        Minimo = s.CALC_MINIMUM,
                                        MinimoTipo = s.CALR_MIN_TYPE,
                                        MinimoDec = s.CALR_MIN_DEC,
                                        EnBD = true,
                                    });
                                });
                                ReglaAsocTmp = reglaAsoc;
                            }
                            #endregion

                            #region INIT CARACTERISTICAS
                            if (tarifa.Caracteristica != null)
                            {
                                ///nuevo para intgrar con licencias - addon dbs

                                caracteristica = new List<DTOTarifaTestCaracteristica>();
                                List<BETarifaCaracteristica> listChar = null;
                                tarifa.Caracteristica.ForEach(chars =>
                                {
                                    if (caracteristicas.Exists(c => c.CHAR_ID == chars.RATE_CALC_AR))
                                    {
                                        if (listChar == null) listChar = new List<BETarifaCaracteristica>();
                                        chars.VALUE = caracteristicas.Find(x => x.CHAR_ID == chars.RATE_CALC_AR).LIC_CHAR_VAL;
                                        listChar.Add(chars);
                                    };
                                });

                                ///fin - nuevo para intgrar con licencias - addon dbs
                                if (listChar != null)
                                {
                                    //tarifa.Caracteristica.ForEach(s =>
                                    listChar.ForEach(s =>
                                    {
                                        caracteristica.Add(new DTOTarifaTestCaracteristica
                                        {
                                            Id = s.RATE_CHAR_ID,
                                            IdElemento = s.RATE_CALC_ID,
                                            IdRegla = s.RATE_CALC,
                                            IdCaracteristica = s.RATE_CALC_AR,
                                            Letra = s.RATE_CHAR_TVAR,
                                            Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "VARIABLE",
                                            DescripcionCorta = s.RATE_CHAR_DESCVAR,
                                            DescripcionLarga = s.RATE_CHAR_DESCVAR,
                                            EnBD = true,
                                            Activo = s.ENDS.HasValue ? false : true,
                                            Tramo = s.IND_TR,
                                            LetraOrigenRegla = s.CHAR_ORI_REG,
                                            Valor = Convert.ToDecimal(s.VALUE)
                                        });
                                    });
                                    CaracteristicaTmpCalcMan = caracteristicaCalcMan;
                                }
                            }
                            #endregion

                            #region INIT TARIFA

                            if (tarifa.Test != null)
                            {
                                matrizTest = new List<DTOTarifaTest>();
                                tarifa.Test.ForEach(s =>
                                {
                                    matrizTest.Add(new DTOTarifaTest
                                    {
                                        //Id = s.RATE_CHAR_ID,
                                        IdPlantilla = s.TEMP_ID,
                                        IdRegla = s.CALR_ID,

                                        IdVal_1 = s.TEMPS1_ID,
                                        IdCaracteristica1 = s.CHAR1_ID,
                                        Tramo_1 = s.IND1_TR,
                                        Desde_1 = s.CRI1_FROM,
                                        Hasta_1 = s.CRI1_TO,

                                        IdVal_2 = s.TEMPS2_ID,
                                        IdCaracteristica2 = s.CHAR2_ID,
                                        Tramo_2 = s.IND2_TR,
                                        Desde_2 = s.CRI2_FROM,
                                        Hasta_2 = s.CRI2_TO,

                                        IdVal_3 = s.TEMPS3_ID,
                                        IdCaracteristica3 = s.CHAR3_ID,
                                        Tramo_3 = s.IND3_TR,
                                        Desde_3 = s.CRI3_FROM,
                                        Hasta_3 = s.CRI3_TO,

                                        IdVal_4 = s.TEMPS4_ID,
                                        IdCaracteristica4 = s.CHAR4_ID,
                                        Tramo_4 = s.IND4_TR,
                                        Desde_4 = s.CRI4_FROM,
                                        Hasta_4 = s.CRI4_TO,

                                        IdVal_5 = s.TEMPS5_ID,
                                        IdCaracteristica5 = s.CHAR5_ID,
                                        Tramo_5 = s.IND5_TR,
                                        Desde_5 = s.CRI5_FROM,
                                        Hasta_5 = s.CRI5_TO,

                                        Tarifa = s.VAL_FORMULA,
                                        Minimo = s.VAL_MINIMUM
                                    });
                                });
                                MatrizTestTmp = matrizTest;
                            }
                            #endregion

                            #region REALIZAR SETTING REGLAS
                            foreach (var regla in ReglaAsocTmp)
                            {
                                if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.SI)
                                {
                                    ATsiAUsi(regla, VUMObtenido);
                                }
                                else if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.NO)
                                {
                                    ATsiAUno(regla, VUMObtenido);
                                }
                                else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.SI)
                                {
                                    ATnoAUsi(regla, VUMObtenido);
                                }
                                else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.NO)
                                {
                                    ATnoAUno(regla, VUMObtenido);
                                }
                            }
                            #endregion

                            CalcularTarifa(TarifaTmp, VUMObtenido);
                            var resultado = new { ValorFormula = (TarifaTmp.ValorFormula > TarifaTmp.ValorMinimo ? TarifaTmp.ValorFormula : TarifaTmp.ValorMinimo), ValorMinimo = TarifaTmp.ValorMinimo };

                            retorno.result = 1;
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                            retorno.data = Json(resultado, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se ha encontrado la definición de gasto";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se encontraron Características activas para calcular el Test de Tarfia. Registre Caracteristicas para la licencia.";
                        retorno.data = Json(null, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerValorTestTarifa", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public decimal RedondeoTarifa(decimal monto)
        {
            decimal redondeo = 0;
            decimal num = 5;
            decimal resta = monto % num;
            if (resta < (num / 2))
                redondeo = monto - resta;
            else
                redondeo = monto + num - resta;
            return redondeo;
        }

        public decimal ObtenerVUMHistorico(List<BEValormusica> lista, DateTime fecha, decimal vumActual)
        {
            decimal VUM = 0;
            var resultado = lista.Where(x => fecha >= x.START &&
                                        ((x.ENDS == null) || (x.ENDS != null && fecha <= x.ENDS))
                                        ).ToList();

            if (resultado.Count > 0 && resultado != null)
                VUM = resultado.OrderBy(x => x.LOG_DATE_CREAT).FirstOrDefault().VUM_VAL;
            else
                VUM = vumActual;
            return VUM;
        }

        public JsonResult ListarCaracteristicaTTLicencia(int tipoCalculo, decimal codigoLic, decimal codigoLicPlan)
        {
            int contador = 0;
            caracteristicaCalcMan = CaracteristicaTmpCalcMan;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblCaracteristicaTT' border=0 width='98%;' class='k-grid k-widget' id='tblCaracteristicaTT'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>V</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdElemento</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdRegla</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdCaracteristica</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='padding-left:20px;text-align:left'>Descripción</th>");
                    shtml.Append("<th class='k-header' ></th>");
                    shtml.Append("</tr></thead>");

                    var valoresCaracteristicas = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, codigoLic, "0", codigoLicPlan);

                    if (caracteristicaCalcMan != null)
                    {
                        foreach (var item in caracteristicaCalcMan.OrderBy(x => x.Id))
                        {
                            contador += 1;
                            shtml.Append("<tr class='k-grid-content'>");
                            if (item.IdCaracteristica != carOtro)
                                shtml.AppendFormat("<td style='width:30px; text-align:center' >{0}</td>", item.Letra);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdElemento);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdRegla);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdCaracteristica);
                            if (item.IdCaracteristica != carOtro)
                                shtml.AppendFormat("<td style='width:250px;' >{0}</td>", item.DescripcionCorta);
                            if (item.IdCaracteristica != carOtro)
                            {
                                var car = valoresCaracteristicas.Where(x => x.CHAR_ID == item.IdCaracteristica).FirstOrDefault();
                                if (car != null)
                                {
                                    //shtml.AppendFormat("<td><input type='text' id='{1}'  class='requerido'     value='{0}' style='width:150px;' maxlength='18'    /></td>", (car.LIC_CHAR_VAL != null ? car.LIC_CHAR_VAL.Value.ToString("N", CultureInfo.InvariantCulture) : ""), "txt" + item.Letra);
                                    shtml.AppendFormat("<td ><input type='text' id='{1}' class='requerido cssValCaract'  value='{0}' style='width:150px;' maxlength='18'    /></td>", (car.LIC_CHAR_VAL != null ? car.LIC_CHAR_VAL.Value.ToString("N4") : ""), "txt" + item.Letra);
                                }
                                else
                                {
                                    shtml.AppendFormat("<td style='width:250px; text-align:left' > <input class='requerido' id='txt" + item.Letra + "' type='text' value='' style='width:120px' maxlength='16' /> </td>");

                                }
                            }
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    if (caracteristica != null)
                    {
                        int registros = caracteristica.Count();
                        retorno.Code = registros;
                    }
                    else
                    {
                        retorno.Code = 0;
                    }
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCaracteristicaTTLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        #region "FUNCIONES QUE ARMAN LA LOGICA PARA OBTENER EL TEST TARIFA"


        /// <summary>
        /// FUNCION QUE RETORNA EL MONTO DEL CALCULO DEL TEST DE TARIFA
        /// Si retorna -999   indica que  No se ha encontrado la definición de gasto
        /// Si retorna -998   indica que  No se ha encontrado caracteristicas registraddas para la licencia y periodo
        /// </summary>
        /// <param name="idTarifa"></param>
        /// <param name="idLicencia"></param>
        /// <param name="idLicPlan"></param>
        /// <returns></returns>
        public decimal CalcularTestTarifa(decimal idTarifa, decimal idLicencia, decimal idLicPlan)
        {
            decimal valorTest = 0;

            var caracteristicas = new BLCaracteristica().ListarCaracteristicasXPeriodo(GlobalVars.Global.OWNER, idLicencia, idLicPlan);
            if (caracteristicas != null && caracteristicas.Count > 0)
            {
                decimal VUMObtenido = 0;
                BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL();
                BLTarifaTest objTarifa = new BLTarifaTest();
                var periodo = new BLLicenciaPlaneamiento().ObtenerPlanificacion(GlobalVars.Global.OWNER, idLicPlan);
                var idTarifaActual = objTarifa.ObtenerTarifaActual(GlobalVars.Global.OWNER, idTarifa, periodo.LIC_DATE);
                tarifa = objTarifa.Obtener(GlobalVars.Global.OWNER, idTarifaActual);
                VUMObtenido = (periodo.LIC_CREQ == Constantes.CaracteristicaReq.SI) ? ObtenerVUMHistorico(ListaVUM, periodo.LIC_DATE, VUM) : VUM;

                if (tarifa != null)
                {
                    dtoTarifa = new DTOTarifa();
                    dtoTarifa.idTarifa = tarifa.RATE_ID;
                    dtoTarifa.TarifaDesc = tarifa.RATE_DESC;
                    dtoTarifa.CantVariable = tarifa.RATE_NVAR;
                    dtoTarifa.CantCaracteristica = tarifa.RATE_NCAL;
                    dtoTarifa.Formula = tarifa.RATE_FORMULA;
                    dtoTarifa.Minima = tarifa.RATE_MINIMUM;
                    dtoTarifa.FormulaTipo = tarifa.RATE_FTIPO;
                    dtoTarifa.MinimoTipo = tarifa.RATE_MTIPO;
                    dtoTarifa.FormulaDec = tarifa.RATE_FDECI;
                    dtoTarifa.MinimoDec = tarifa.RATE_MDECI;
                    dtoTarifa.Redondeo = tarifa.RATE_REDONDEO;

                    DTOTarifa TarifaTmp2 = new DTOTarifa();
                    List<DTOTarifaManReglaAsoc> ReglaAsocTmp2 = new List<DTOTarifaManReglaAsoc>();
                    List<DTOTarifaTestCaracteristica> CaracteristicaTmp2 = new List<DTOTarifaTestCaracteristica>();
                    List<DTOTarifaTest> MatrizTestTmp2 = new List<DTOTarifaTest>();

                    TarifaTmp2 = dtoTarifa;


                    #region INIT REGLAS
                    if (tarifa.Regla != null)
                    {
                        reglaAsoc = new List<DTOTarifaManReglaAsoc>();
                        tarifa.Regla.ForEach(s =>
                        {
                            reglaAsoc.Add(new DTOTarifaManReglaAsoc
                            {
                                IdRegla = s.CALR_ID,
                                IdPlantilla = s.TEMP_ID,
                                Elemento = s.CALR_DESC,
                                Letra = s.RATE_CALC_VAR,
                                Variables = s.CALR_NVAR,
                                AjustarUnidades = s.CALR_ADJUST,
                                AcumularTramos = s.CALR_ACCUM,
                                Formula = s.CALC_FORMULA,
                                FormulaTipo = s.CALR_FOR_TYPE,
                                FormulaDec = s.CALR_FOR_DEC,
                                Minimo = s.CALC_MINIMUM,
                                MinimoTipo = s.CALR_MIN_TYPE,
                                MinimoDec = s.CALR_MIN_DEC,
                                EnBD = true,
                            });
                        });
                        ReglaAsocTmp2 = reglaAsoc;
                    }
                    #endregion

                    #region INIT CARACTERISTICAS
                    if (tarifa.Caracteristica != null)
                    {
                        ///nuevo para intgrar con licencias - addon dbs

                        caracteristica = new List<DTOTarifaTestCaracteristica>();
                        List<BETarifaCaracteristica> listChar = null;
                        tarifa.Caracteristica.ForEach(chars =>
                        {
                            if (caracteristicas.Exists(c => c.CHAR_ID == chars.RATE_CALC_AR))
                            {
                                if (listChar == null) listChar = new List<BETarifaCaracteristica>();
                                chars.VALUE = caracteristicas.Find(x => x.CHAR_ID == chars.RATE_CALC_AR).LIC_CHAR_VAL;
                                listChar.Add(chars);
                            };
                        });

                        ///fin - nuevo para intgrar con licencias - addon dbs
                        if (listChar != null)
                        {
                            //tarifa.Caracteristica.ForEach(s =>
                            listChar.ForEach(s =>
                            {
                                caracteristica.Add(new DTOTarifaTestCaracteristica
                                {
                                    Id = s.RATE_CHAR_ID,
                                    IdElemento = s.RATE_CALC_ID,
                                    IdRegla = s.RATE_CALC,
                                    IdCaracteristica = s.RATE_CALC_AR,
                                    Letra = s.RATE_CHAR_TVAR,
                                    Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "VARIABLE",
                                    DescripcionCorta = s.RATE_CHAR_DESCVAR,
                                    DescripcionLarga = s.RATE_CHAR_DESCVAR,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    Tramo = s.IND_TR,
                                    LetraOrigenRegla = s.CHAR_ORI_REG,
                                    Valor = Convert.ToDecimal(s.VALUE)
                                });
                            });
                            CaracteristicaTmp2 = caracteristica;
                        }
                    }
                    #endregion

                    #region INIT TARIFA

                    if (tarifa.Test != null)
                    {
                        matrizTest = new List<DTOTarifaTest>();
                        tarifa.Test.ForEach(s =>
                        {
                            matrizTest.Add(new DTOTarifaTest
                            {
                                //Id = s.RATE_CHAR_ID,
                                IdPlantilla = s.TEMP_ID,
                                IdRegla = s.CALR_ID,

                                IdVal_1 = s.TEMPS1_ID,
                                IdCaracteristica1 = s.CHAR1_ID,
                                Tramo_1 = s.IND1_TR,
                                Desde_1 = s.CRI1_FROM,
                                Hasta_1 = s.CRI1_TO,

                                IdVal_2 = s.TEMPS2_ID,
                                IdCaracteristica2 = s.CHAR2_ID,
                                Tramo_2 = s.IND2_TR,
                                Desde_2 = s.CRI2_FROM,
                                Hasta_2 = s.CRI2_TO,

                                IdVal_3 = s.TEMPS3_ID,
                                IdCaracteristica3 = s.CHAR3_ID,
                                Tramo_3 = s.IND3_TR,
                                Desde_3 = s.CRI3_FROM,
                                Hasta_3 = s.CRI3_TO,

                                IdVal_4 = s.TEMPS4_ID,
                                IdCaracteristica4 = s.CHAR4_ID,
                                Tramo_4 = s.IND4_TR,
                                Desde_4 = s.CRI4_FROM,
                                Hasta_4 = s.CRI4_TO,

                                IdVal_5 = s.TEMPS5_ID,
                                IdCaracteristica5 = s.CHAR5_ID,
                                Tramo_5 = s.IND5_TR,
                                Desde_5 = s.CRI5_FROM,
                                Hasta_5 = s.CRI5_TO,

                                Tarifa = s.VAL_FORMULA,
                                Minimo = s.VAL_MINIMUM
                            });
                        });
                        MatrizTestTmp2 = matrizTest;
                    }
                    #endregion

                    #region REALIZAR SETTING REGLAS
                    foreach (var regla in ReglaAsocTmp2)
                    {
                        var ListaCaracteristicaXregla = CaracteristicaTmp2.Where(x => x.IdRegla == regla.IdRegla).ToList();
                        var ListavaloresXregla = MatrizTestTmp2.Where(v => v.IdRegla == regla.IdRegla).ToList();

                        if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.SI)
                        {
                            //ATsiAUsiB(regla, VUMObtenido,CaracteristicaTmp2,MatrizTestTmp2);
                            ATsiAUsiB(regla, VUMObtenido, CaracteristicaTmp2, MatrizTestTmp2);
                        }
                        else if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.NO)
                        {
                            ATsiAUnoB(regla, VUMObtenido, CaracteristicaTmp2, MatrizTestTmp2);
                        }
                        else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.SI)
                        {
                            ATnoAUsiB(regla, VUMObtenido, CaracteristicaTmp2, MatrizTestTmp2);
                        }
                        else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.NO)
                        {
                            ATnoAUnoB(regla, VUMObtenido, CaracteristicaTmp2, MatrizTestTmp2);
                        }
                    }
                    #endregion

                    CalcularTarifaB(TarifaTmp2, VUMObtenido, ReglaAsocTmp2);
                    //  var resultado = new { ValorFormula = (TarifaTmp2.ValorFormula > TarifaTmp2.ValorMinimo ? TarifaTmp2.ValorFormula : TarifaTmp2.ValorMinimo), ValorMinimo = TarifaTmp2.ValorMinimo };
                    DescuentoController DescuentoCtrllr = new DescuentoController();
                    decimal TTValorFormula = DescuentoCtrllr.TarifaDescuentosPlantilla(TarifaTmp2.idTarifa, TarifaTmp2.ValorFormula, CaracteristicaTmp2);
                    TarifaTmp2.ValorFormula = TTValorFormula;
                    valorTest = (TarifaTmp2.ValorFormula > TarifaTmp2.ValorMinimo ? TarifaTmp2.ValorFormula : TarifaTmp2.ValorMinimo);
                    //DescuentoController DescuentoCtrllr = new DescuentoController();
                    //decimal TTValorFormula = DescuentoCtrllr.TarifaDescuentosPlantilla(TarifaTmp2.idTarifa, TarifaTmp2.ValorFormula, CaracteristicaTmp2);
                    //valorTest = TTValorFormula;
                }
                else
                {
                    valorTest = -999;
                    //retorno.result = 0;
                    //retorno.message = "No se ha encontrado la definición de gasto";
                }
            }
            else
            {
                valorTest = -998;
                //retorno.result = 0;
                //retorno.message = "No se encontraron Características activas para calcular el Test de Tarfia. Registre Caracteristicas para la licencia.";
                //retorno.data = Json(null, JsonRequestBehavior.AllowGet);
            }

            return valorTest;
        }



        public void CalcularTarifaB(DTOTarifa tarifa, decimal VUMcalcular, List<DTOTarifaManReglaAsoc> ReglaAsocTmp2)
        {
            decimal? Rt = 0;
            decimal? Rw = 0;
            decimal? Rx = 0;
            decimal? Ry = 0;
            decimal? Rz = 0;
            //MINIMO
            decimal? Mt = 0;
            decimal? Mw = 0;
            decimal? Mx = 0;
            decimal? My = 0;
            decimal? Mz = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            //MIN
            double[] listaValoresMin = new double[10];
            DataTable Tbl = new DataTable();
            //MIN
            DataTable TblMin = new DataTable();

            formula = tarifa.Formula;
            formulaMinima = tarifa.Minima;

            foreach (var regla in ReglaAsocTmp2)
            {
                if (regla.Letra == LetraReg.T) Rt = regla.ValorFormula;
                if (regla.Letra == LetraReg.T) Mt = regla.ValorMinimo;
                if (regla.Letra == LetraReg.W) Rw = regla.ValorFormula;
                if (regla.Letra == LetraReg.W) Mw = regla.ValorMinimo;
                if (regla.Letra == LetraReg.X) Rx = regla.ValorFormula;
                if (regla.Letra == LetraReg.X) Mx = regla.ValorMinimo;
                if (regla.Letra == LetraReg.Y) Ry = regla.ValorFormula;
                if (regla.Letra == LetraReg.Y) My = regla.ValorMinimo;
                if (regla.Letra == LetraReg.Z) Rz = regla.ValorFormula;
                if (regla.Letra == LetraReg.Z) Mz = regla.ValorMinimo;
                //if (regla.Letra == LetraReg.T) Rt = regla.ValorR;
                //if (regla.Letra == LetraReg.W) Rw = regla.ValorR;
                //if (regla.Letra == LetraReg.X) Rx = regla.ValorR;
                //if (regla.Letra == LetraReg.Y) Ry = regla.ValorR;
                //if (regla.Letra == LetraReg.Z) Rz = regla.ValorR;
            }

            listaOperandos[0] = LetraReg.T;
            listaOperandos[1] = LetraReg.W;
            listaOperandos[2] = LetraReg.X;
            listaOperandos[3] = LetraReg.Y;
            listaOperandos[4] = LetraReg.Z;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;

            listaValores[0] = Convert.ToDouble(Rt);
            listaValores[1] = Convert.ToDouble(Rw);
            listaValores[2] = Convert.ToDouble(Rx);
            listaValores[3] = Convert.ToDouble(Ry);
            listaValores[4] = Convert.ToDouble(Rz);
            listaValores[5] = 0;
            listaValores[6] = Convert.ToDouble(VUMcalcular);

            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                Tbl.Rows.Add(linea);
            }

            if (tarifa.Redondeo == 1)
                tarifa.ValorFormula = RedondeoTarifa(Convert.ToDecimal(Tbl.Rows[0][7].ToString()));
            else
                tarifa.ValorFormula = Math.Round(Convert.ToDecimal(Tbl.Rows[0][7].ToString()),
                                            Convert.ToInt32(tarifa.FormulaDec));


            //MIN
            listaValoresMin[0] = Convert.ToDouble(Mt);
            listaValoresMin[1] = Convert.ToDouble(Mw);
            listaValoresMin[2] = Convert.ToDouble(My);
            listaValoresMin[3] = Convert.ToDouble(Mx);
            listaValoresMin[4] = Convert.ToDouble(Mz);
            listaValoresMin[5] = 0;
            listaValoresMin[6] = Convert.ToDouble(VUMcalcular);

            TblMin.Columns.Add(listaOperandos[0], typeof(double));
            TblMin.Columns.Add(listaOperandos[1], typeof(double));
            TblMin.Columns.Add(listaOperandos[2], typeof(double));
            TblMin.Columns.Add(listaOperandos[3], typeof(double));
            TblMin.Columns.Add(listaOperandos[4], typeof(double));
            TblMin.Columns.Add(listaOperandos[5], typeof(double));
            TblMin.Columns.Add(listaOperandos[6], typeof(double));
            TblMin.Columns.Add("Tarifa", typeof(double), formula);
            TblMin.Columns.Add("Minimo", typeof(double), formulaMinima);
            {
                // crea una nueva línea 
                DataRow lineaMin = TblMin.NewRow();
                lineaMin[0] = listaValoresMin[0];
                lineaMin[1] = listaValoresMin[1];
                lineaMin[2] = listaValoresMin[2];
                lineaMin[3] = listaValoresMin[3];
                lineaMin[4] = listaValoresMin[4];
                lineaMin[5] = listaValoresMin[5];
                lineaMin[6] = listaValoresMin[6];
                TblMin.Rows.Add(lineaMin);
            }
            if (tarifa.Redondeo == 1)
                tarifa.ValorMinimo = RedondeoTarifa(Convert.ToDecimal(TblMin.Rows[0][8].ToString()));
            else
                tarifa.ValorMinimo = Math.Round(Convert.ToDecimal(TblMin.Rows[0][8].ToString()),
                                            Convert.ToInt32(tarifa.FormulaDec));

        }

        public DTOTarifaManReglaAsoc ATsiAUsiB(DTOTarifaManReglaAsoc regla, decimal VUMcalcular, List<DTOTarifaTestCaracteristica> CaracteristicaTmp2, List<DTOTarifaTest> MatrizTestTmp2)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp2.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp2.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }


            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(
                                    v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
                                    ).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
                                    ).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
                                    ).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
                                    ).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                        v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
                        ).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
            }
            #endregion

            #region Calcular
            decimal? acumularR = 0;
            decimal? acumularRminimo = 0;
            decimal? vFormula = 0; decimal? vFormulaTemp = 0;
            decimal? vMinimo = 0; decimal? vMinimoTemp = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;//

            foreach (var item in listaObtenerR)
            {
                acumularR += item.Tarifa;
                acumularRminimo += item.Minimo;
            }
            vFormula = listaObtenerR.FirstOrDefault().Tarifa;
            vMinimo = listaObtenerR.FirstOrDefault().Minimo;

            valorR = vFormula;
            valorRmin = vMinimo;//

            //if (vMinimo > vFormula)
            //    valorR = vMinimo;
            //else
            //    valorR = vFormula;

            if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            {
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.Formula;
                formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);

                foreach (var car in caracteristicas)
                {
                    if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;//

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUMcalcular);
                listaValores[7] = Convert.ToDouble(valorRmin);//

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];//
                    Tbl.Rows.Add(linea);
                }

                regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            else
            {
                regla.ValorFormula = vFormula;
                regla.ValorMinimo = vMinimo;
                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            #endregion
            return regla;
        }  // CASO 1 -OK

        public DTOTarifaManReglaAsoc ATsiAUnoB(DTOTarifaManReglaAsoc regla, decimal VUMcalcular, List<DTOTarifaTestCaracteristica> CaracteristicaTmp2, List<DTOTarifaTest> MatrizTestTmp2)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp2.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp2.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }


            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(
                                    v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
                                    ).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
                                    ).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
                                    ).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                                    v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
                                    ).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(
                        v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
                        ).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
            }
            #endregion

            #region CalcularAcumulado
            decimal? tempR = 1;
            decimal? tempRminimo = 1;
            decimal? sumarR = 0;
            decimal? sumarRminimo = 0;
            foreach (var item in listaObtenerR)
            {
                tempR = 1;
                tempRminimo = 1;

                if (tA == Seleccion.SI)
                {
                    if (item.Hasta_1 < vA)
                    {
                        tempR *= item.Hasta_1;
                        tempRminimo *= item.Hasta_1;
                    }
                    else
                    {
                        tempR *= (vA - (item.Desde_1 - 1));
                        tempRminimo *= (vA - (item.Desde_1 - 1));
                    }
                }

                if (tB == Seleccion.SI)
                {
                    if (item.Hasta_2 < vB)
                    {
                        tempR *= item.Hasta_2;
                        tempRminimo *= item.Hasta_2;
                    }
                    else
                    {
                        tempR *= (vB - (item.Desde_2 - 1));
                        tempRminimo *= (vB - (item.Desde_2 - 1));
                    }
                }

                if (tC == Seleccion.SI)
                {
                    if (item.Hasta_3 < vC)
                    {
                        tempR *= item.Hasta_3;
                        tempRminimo *= item.Hasta_3;
                    }
                    else
                    {
                        tempR *= (vC - (item.Desde_3 - 1));
                        tempRminimo *= (vC - (item.Desde_3 - 1));
                    }
                }

                if (tD == Seleccion.SI)
                {
                    if (item.Hasta_4 < vD)
                    {
                        tempR *= item.Hasta_4;
                        tempRminimo *= item.Hasta_4;
                    }
                    else
                    {
                        tempR *= (vD - (item.Desde_4 - 1));
                        tempRminimo *= (vD - (item.Desde_4 - 1));
                    }
                }

                if (tE == Seleccion.SI)
                {
                    if (item.Hasta_5 < vE)
                    {
                        tempR *= item.Hasta_5;
                        tempRminimo *= item.Hasta_5;
                    }
                    else
                    {
                        tempR *= (vE - (item.Desde_5 - 1));
                        tempRminimo *= (vE - (item.Desde_5 - 1));
                    }
                }

                sumarR += (tempR * item.Tarifa);
                sumarRminimo += (tempRminimo * item.Minimo);
            }
            #endregion

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;//

            #region Calcular
            vFormula = sumarR;
            vMinimo = sumarRminimo;

            valorR = vFormula;
            valorRmin = vMinimo;//

            //if (vMinimo > vFormula)
            //    valorR = vMinimo;
            //else
            //    valorR = vFormula;

            if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            {
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.Formula;
                formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

                foreach (var car in CaracteristicaTmp2)
                {
                    if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                    if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;//

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUMcalcular);
                listaValores[7] = Convert.ToDouble(valorRmin);//

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));//
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];//
                    Tbl.Rows.Add(linea);
                }

                regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.ValorMinimo > regla.ValorFormula)
                    regla.ValorR = regla.ValorMinimo;
                else
                    regla.ValorR = regla.ValorFormula;
            }
            else
            {
                regla.ValorFormula = sumarR;
                regla.ValorMinimo = sumarRminimo;

                if (sumarRminimo > sumarR)
                    regla.ValorR = sumarRminimo;
                else
                    regla.ValorR = sumarR;

            }
            #endregion

            return regla;
        }  // CASO 2

        public DTOTarifaManReglaAsoc ATnoAUsiB(DTOTarifaManReglaAsoc regla, decimal VUMcalcular, List<DTOTarifaTestCaracteristica> CaracteristicaTmp2, List<DTOTarifaTest> MatrizTestTmp2)
        {
            #region Obtener Valores
            decimal vA = 0; string tA = string.Empty; // v=valor; t=tramo    
            decimal vB = 0; string tB = string.Empty;
            decimal vC = 0; string tC = string.Empty;
            decimal vD = 0; string tD = string.Empty;
            decimal vE = 0; string tE = string.Empty;

            var valores = MatrizTestTmp2.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp2.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }

            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                    listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vB <= v.Hasta_3).ToList();
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vB <= v.Hasta_4).ToList();
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                    listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vB <= v.Hasta_5).ToList();
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
            }
            #endregion

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;

            #region Calcular
            /*begin - actualizado por dbs. Error cuando listaObtenerR.Count =0 */
            vFormula = listaObtenerR.Count > 0 ? listaObtenerR.FirstOrDefault().Tarifa : 0;
            vMinimo = listaObtenerR.Count > 0 ? listaObtenerR.FirstOrDefault().Minimo : 0;
            /*end - actualizado por dbs. Error cuando listaObtenerR.Count =0 */

            valorR = vFormula;
            valorRmin = vMinimo;



            //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
            //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            //{
            decimal? Ra = 0;
            decimal? Rb = 0;
            decimal? Rc = 0;
            decimal? Rd = 0;
            decimal? Re = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            DataTable Tbl = new DataTable();

            formula = regla.Formula;
            formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);

            foreach (var car in caracteristicas)
            {
                if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
            }

            listaOperandos[0] = LetraCar.A;
            listaOperandos[1] = LetraCar.B;
            listaOperandos[2] = LetraCar.C;
            listaOperandos[3] = LetraCar.D;
            listaOperandos[4] = LetraCar.E;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;
            listaOperandos[7] = LetraReg.Rmin;

            listaValores[0] = Convert.ToDouble(Ra);
            listaValores[1] = Convert.ToDouble(Rb);
            listaValores[2] = Convert.ToDouble(Rc);
            listaValores[3] = Convert.ToDouble(Rd);
            listaValores[4] = Convert.ToDouble(Re);

            listaValores[5] = Convert.ToDouble(valorR);
            listaValores[6] = Convert.ToDouble(VUMcalcular);
            listaValores[7] = Convert.ToDouble(valorRmin);

            //Tbl.Columns.Add("variable", typeof(string));
            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add(listaOperandos[7], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                linea[7] = listaValores[7];
                Tbl.Rows.Add(linea);
            }

            regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
            regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

            if (regla.ValorMinimo > regla.ValorFormula)
                regla.ValorR = regla.ValorMinimo;
            else
                regla.ValorR = regla.ValorFormula;
            //}
            //else
            //{
            //    regla.ValorFormula = vFormula;
            //    regla.ValorMinimo = vMinimo;
            //    if (regla.ValorMinimo > regla.ValorFormula)
            //        regla.ValorR = regla.ValorMinimo;
            //    else
            //        regla.ValorR = regla.ValorFormula;
            //}
            #endregion
            return regla;
        }  // CASO 3 - OK

        public DTOTarifaManReglaAsoc ATnoAUnoB(DTOTarifaManReglaAsoc regla, decimal VUMcalcular, List<DTOTarifaTestCaracteristica> CaracteristicaTmp2, List<DTOTarifaTest> MatrizTestTmp2)
        {
            #region Obtener Valores
            decimal? vA = 0; string tA = string.Empty;
            decimal? vB = 0; string tB = string.Empty;
            decimal? vC = 0; string tC = string.Empty;
            decimal? vD = 0; string tD = string.Empty;
            decimal? vE = 0; string tE = string.Empty;
            decimal? vFormulaTemp = 0;
            decimal? vMinimoTemp = 0;
            decimal? acumular = 1;

            var valores = MatrizTestTmp2.Where(t => t.IdRegla == regla.IdRegla);
            var caracteristicas = CaracteristicaTmp2.Where(c => c.IdRegla == regla.IdRegla);

            int totCaracteristicas = caracteristicas.ToList().Count;
            if (totCaracteristicas > 0)
            {
                vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
                tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 1)
            {
                vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
                tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 2)
            {
                vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
                tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 3)
            {
                vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
                tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
            }
            if (totCaracteristicas > 4)
            {
                vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
                tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
            }

            //Buscar R
            List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
            if (totCaracteristicas > 0)
            {
                if (tA == Seleccion.SI)
                {
                    listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
                    acumular *= vA;
                }
                else if (tA == Seleccion.NO)
                    listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
            }

            if (totCaracteristicas > 1)
            {
                if (tB == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
                    acumular *= vB;
                }
                else if (tB == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
            }

            if (totCaracteristicas > 2)
            {
                if (tC == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vB <= v.Hasta_3).ToList();
                    acumular *= vC;
                }
                else if (tC == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
            }

            if (totCaracteristicas > 3)
            {
                if (tD == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vB <= v.Hasta_4).ToList();
                    acumular *= vD;
                }
                else if (tD == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
            }

            if (totCaracteristicas > 4)
            {
                if (tE == Seleccion.SI)
                {
                    listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vB <= v.Hasta_5).ToList();
                    acumular *= vE;
                }
                else if (tE == Seleccion.NO)
                    listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
            }
            #endregion

            #region Calcular

            decimal? vFormula = 0;
            decimal? vMinimo = 0;
            decimal? valorR = 0;
            decimal? valorRmin = 0;

            vFormulaTemp = listaObtenerR.FirstOrDefault().Tarifa;
            vMinimoTemp = listaObtenerR.FirstOrDefault().Minimo;

            vFormula = acumular * vFormulaTemp;
            vMinimo = acumular * vMinimoTemp;

            valorR = vFormula;
            valorRmin = vMinimo;//


            //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
            //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
            //{
            decimal? Ra = 0;
            decimal? Rb = 0;
            decimal? Rc = 0;
            decimal? Rd = 0;
            decimal? Re = 0;

            string formula;
            string formulaMinima;
            string[] listaOperandos = new string[10];
            double[] listaValores = new double[10];
            DataTable Tbl = new DataTable();

            formula = regla.Formula;
            formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

            foreach (var car in caracteristicas)
            {
                if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
                if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
            }

            listaOperandos[0] = LetraCar.A;
            listaOperandos[1] = LetraCar.B;
            listaOperandos[2] = LetraCar.C;
            listaOperandos[3] = LetraCar.D;
            listaOperandos[4] = LetraCar.E;
            listaOperandos[5] = LetraReg.R;
            listaOperandos[6] = LetraReg.V;
            listaOperandos[7] = LetraReg.Rmin;//

            listaValores[0] = Convert.ToDouble(Ra);
            listaValores[1] = Convert.ToDouble(Rb);
            listaValores[2] = Convert.ToDouble(Rc);
            listaValores[3] = Convert.ToDouble(Rd);
            listaValores[4] = Convert.ToDouble(Re);

            listaValores[5] = Convert.ToDouble(valorR);
            listaValores[6] = Convert.ToDouble(VUMcalcular);
            listaValores[7] = Convert.ToDouble(valorRmin);//

            //Tbl.Columns.Add("variable", typeof(string));
            Tbl.Columns.Add(listaOperandos[0], typeof(double));
            Tbl.Columns.Add(listaOperandos[1], typeof(double));
            Tbl.Columns.Add(listaOperandos[2], typeof(double));
            Tbl.Columns.Add(listaOperandos[3], typeof(double));
            Tbl.Columns.Add(listaOperandos[4], typeof(double));
            Tbl.Columns.Add(listaOperandos[5], typeof(double));
            Tbl.Columns.Add(listaOperandos[6], typeof(double));
            Tbl.Columns.Add(listaOperandos[7], typeof(double));
            Tbl.Columns.Add("Tarifa", typeof(double), formula);
            Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

            {
                // crea una nueva línea 
                DataRow linea = Tbl.NewRow();
                linea[0] = listaValores[0];
                linea[1] = listaValores[1];
                linea[2] = listaValores[2];
                linea[3] = listaValores[3];
                linea[4] = listaValores[4];
                linea[5] = listaValores[5];
                linea[6] = listaValores[6];
                linea[7] = listaValores[7];//
                Tbl.Rows.Add(linea);
            }

            regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
            regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

            if (regla.ValorMinimo > regla.ValorFormula)
                regla.ValorR = regla.ValorMinimo;
            else
                regla.ValorR = regla.ValorFormula;
            //}
            //else
            //{
            //    regla.ValorFormula = vFormula;
            //    regla.ValorMinimo = vMinimo;

            //    if (regla.ValorMinimo > regla.ValorFormula)
            //        regla.ValorR = regla.ValorMinimo;
            //    else
            //        regla.ValorR = regla.ValorFormula;
            //}


            #endregion

            return regla;
        }  // CASO 4 - OK
        #endregion
    }
}
