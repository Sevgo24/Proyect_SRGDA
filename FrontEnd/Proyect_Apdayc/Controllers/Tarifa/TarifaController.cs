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

namespace Proyect_Apdayc.Controllers.Tarifa
{
    public class TarifaController : Base
    {   
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_TARIFA_REGLA = "___DTOTarifaRegla";
        private const string K_SESION_TARIFA_REGLA_DEL = "___DTOtarifaReglaDEL";
        private const string K_SESION_TARIFA_REGLA_ACT = "___DTOTarifaReglaACT";
        private const string K_SESION_TARIFA_CAR = "___DTOTarifaCar";
        private const string K_SESION_TARIFA_CAR_DEL = "___DTOtarifaCarDEL";
        private const string K_SESION_TARIFA_CAR_ACT = "___DTOTarifaCarACT";
        private const string K_SESION_TARIFA_PARAM = "___DTOTarifaParametro";
        private const string K_SESION_TARIFA_PARAM_DEL = "___DTOtarifaParametroDEL";
        private const string K_SESION_TARIFA_PARAM_ACT = "___DTOTarifaParametroACT";
        private const string K_SESION_TARIFA_DESC = "___DTOTarifaDescuento";
        private const string K_SESION_TARIFA_DESC_DEL = "___DTOtarifaDescuentoDEL";
        private const string K_SESION_TARIFA_DESC_ACT = "___DTOTarifaDescuentoACT";
        private const string K_SESION_TARIFA_LISTA_CAR = "___DTOTarifaListaCarACT";

        List<DTOTarifaManReglaAsoc> reglaAsoc = new List<DTOTarifaManReglaAsoc>();
        List<DTOTarifaManCaracteristica> caracteristica = new List<DTOTarifaManCaracteristica>();
        List<DTOTarifaManParametroAsoc> parametro = new List<DTOTarifaManParametroAsoc>();
        List<DTOTarifaDescuento> descuento = new List<DTOTarifaDescuento>();
        //
        // GET: /Tarifa/
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            
            Session.Remove(K_SESION_TARIFA_REGLA);
            Session.Remove(K_SESION_TARIFA_REGLA_DEL);
            Session.Remove(K_SESION_TARIFA_REGLA_ACT);
            Session.Remove(K_SESION_TARIFA_CAR);
            Session.Remove(K_SESION_TARIFA_CAR_DEL);
            Session.Remove(K_SESION_TARIFA_CAR_ACT);
            Session.Remove(K_SESION_TARIFA_PARAM);
            Session.Remove(K_SESION_TARIFA_PARAM_DEL);
            Session.Remove(K_SESION_TARIFA_PARAM_ACT);
            Session.Remove(K_SESION_TARIFA_DESC);
            Session.Remove(K_SESION_TARIFA_DESC_DEL);
            Session.Remove(K_SESION_TARIFA_DESC_ACT);
            Session.Remove(K_SESION_TARIFA_LISTA_CAR);
            Init(false);
            return View();
        }

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

        private List<DTOTarifaManReglaAsoc> ReglaAsocTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaManReglaAsoc>)Session[K_SESION_TARIFA_REGLA_ACT];
            }
            set
            {
                Session[K_SESION_TARIFA_REGLA_ACT] = value;
            }
        }

        private List<DTOTarifaManReglaAsoc> ReglaAsocTmpDelBD
        {
            get
            {
                return (List<DTOTarifaManReglaAsoc>)Session[K_SESION_TARIFA_REGLA_DEL];
            }
            set
            {
                Session[K_SESION_TARIFA_REGLA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddReglaAsoc(DTOTarifaManReglaAsoc entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int registroNuevo = 0;
                    int registroModificar = 0;
                    reglaAsoc = ReglaAsocTmp;
                    if (reglaAsoc != null)
                    {
                        registroNuevo = reglaAsoc.Where(p => p.IdRegla == entidad.IdRegla && entidad.Id == 0).Count();
                        registroModificar = reglaAsoc.Where(p => p.IdRegla == entidad.IdRegla && p.Id == entidad.Id).Count();
                    }

                    if ((entidad.Id == 0 && registroNuevo == 0)
                         || (entidad.Id != 0 && registroModificar > 0)
                       )
                    {
                        List<BECaracteristica> listaCaracteristica = new List<BECaracteristica>();
                        if (entidad.TipoCalculo == "R")
                            listaCaracteristica = new BLCaracteristica().ObtenerReglaCartacteristica(GlobalVars.Global.OWNER, entidad.IdRegla);

                        if (CaracteristicaTmp == null) CaracteristicaTmp = new List<DTOTarifaManCaracteristica>();
                        if (ParametroTmp == null) ParametroTmp = new List<DTOTarifaManParametroAsoc>();

                        //if (listaCaracteristica.Count + CaracteristicaTmp.Count <= 7)
                        if (listaCaracteristica.Count + CaracteristicaTmp.Count <= 15)
                        {

                            if (reglaAsoc == null) reglaAsoc = new List<DTOTarifaManReglaAsoc>();
                            if (Convert.ToInt32(entidad.Id) <= 0)
                            {
                                decimal nuevoId = 1;
                                if (reglaAsoc.Count > 0) nuevoId = reglaAsoc.Max(x => x.Id) + 1;
                                entidad.Id = nuevoId;
                                entidad.Activo = true;
                                entidad.EnBD = false;
                                entidad.UsuarioCrea = UsuarioActual;
                                entidad.FechaCrea = DateTime.Now;
                                reglaAsoc.Add(entidad);

                                //** Registrar caracteristica ********************************
                                //List<BECaracteristica> listaCaracteristica = new List<BECaracteristica>();
                                //if (entidad.TipoCalculo == "R")
                                //    listaCaracteristica = new BLCaracteristica().ObtenerReglaCartacteristica(GlobalVars.Global.OWNER, entidad.IdRegla);

                                if (caracteristica == null) caracteristica = new List<DTOTarifaManCaracteristica>();
                                if (parametro == null) parametro = new List<DTOTarifaManParametroAsoc>();

                                DTOTarifaManCaracteristica dtoChar = null;
                                DTOTarifaManParametroAsoc dtoParam = null;
                                foreach (var item in listaCaracteristica)
                                {
                                    //CARACTERISTICA
                                    dtoChar = new DTOTarifaManCaracteristica();
                                    decimal nuevoCharId = 1;
                                    if (CaracteristicaTmp.Count > 0) nuevoCharId = CaracteristicaTmp.Max(x => x.Id) + 1;
                                    dtoChar.Id = nuevoCharId;
                                    dtoChar.IdElemento = entidad.Id;
                                    dtoChar.IdRegla = entidad.IdRegla;
                                    dtoChar.IdCaracteristica = item.CHAR_ID;
                                    dtoChar.DescripcionCorta = item.CHAR_SHORT;
                                    dtoChar.DescripcionLarga = item.CHAR_LONG;
                                    dtoChar.IndImpresion = "0";
                                    dtoChar.FechaCrea = DateTime.Now;
                                    dtoChar.UsuarioCrea = UsuarioActual;
                                    dtoChar.TipoCalculo = entidad.TipoCalculo;
                                    if (entidad.TipoCalculo == "R")
                                    {
                                        dtoChar.Tipo = "CARACTERISTICA";
                                        dtoChar.TipoVariable = "C";
                                    }
                                    else
                                    {
                                        dtoChar.Tipo = "VARIABLE";
                                        dtoChar.TipoVariable = "V";
                                    }
                                    CaracteristicaTmp.Add(dtoChar);

                                    //PARAMETRO
                                    dtoParam = new DTOTarifaManParametroAsoc();
                                    dtoParam.Id = nuevoCharId;
                                    dtoParam.IdElemento = entidad.Id;
                                    dtoParam.IdCaracteristica = item.CHAR_ID;
                                    dtoParam.IdRegla = entidad.IdRegla;
                                    ParametroTmp.Add(dtoParam);
                                }
                                //*************************************************************
                            }
                            else
                            {
                                var item = reglaAsoc.Where(x => x.Id == entidad.Id).FirstOrDefault();
                                entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                                entidad.Activo = item.Activo;
                                entidad.UsuarioCrea = item.UsuarioCrea;
                                entidad.FechaCrea = item.FechaCrea;
                                reglaAsoc.Remove(item);
                                reglaAsoc.Add(entidad);
                            }
                            ReglaAsocTmp = reglaAsoc;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                        else
                        {
                            retorno.result = 2;
                            retorno.message = "El elemento excede la cantidad maxima de 7 caracteristicas.";
                        }
                    }
                }
                else
                {
                    retorno.result = 2;
                    retorno.message = "La regla ya existe.";
                }

            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddReglaAsoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public string GenerarLetraReglaAsoc(int contador)
        {
            string letra = string.Empty;
            switch (contador)
            {
                case 1: letra = "T"; break;
                case 2: letra = "W"; break;
                case 3: letra = "X"; break;
                case 4: letra = "Y"; break;
                case 5: letra = "Z"; break;
                default: letra = ".";
                    break;
            }
            return letra;
        }

        [HttpPost]
        public JsonResult DellAddReglaAsoc(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    reglaAsoc = ReglaAsocTmp;
                    caracteristica = CaracteristicaTmp;
                    parametro = ParametroTmp;
                    if (reglaAsoc != null)
                    {
                        var objDel = reglaAsoc.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (ReglaAsocTmpUPDEstado == null) ReglaAsocTmpUPDEstado = new List<DTOTarifaManReglaAsoc>();
                                if (ReglaAsocTmpDelBD == null) ReglaAsocTmpDelBD = new List<DTOTarifaManReglaAsoc>();

                                var itemUpd = ReglaAsocTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = ReglaAsocTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) ReglaAsocTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) ReglaAsocTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) ReglaAsocTmpDelBD.Add(objDel);
                                    if (itemUpd != null) ReglaAsocTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;

                                var objCaracteristicas = caracteristica.Where(x => x.IdElemento == objDel.Id).ToList();
                                foreach (var item in objCaracteristicas)
                                {
                                    caracteristica.Remove(item);
                                }

                                var objParametro = parametro.Where(x => x.IdElemento == objDel.Id).ToList();
                                //dtoParam.IdElemento = entidad.Id;
                                //dtoParam.IdCaracteristica = item.CHAR_ID;
                                foreach (var item in objParametro)
                                {
                                    parametro.Remove(item);
                                }

                                reglaAsoc.Remove(objDel);
                                //reglaAsoc.Add(objDel);
                            }
                            else
                            {
                                var objCaracteristicas = caracteristica.Where(x => x.IdElemento == objDel.Id).ToList();
                                foreach (var item in objCaracteristicas)
                                {
                                    caracteristica.Remove(item);
                                }

                                var objParametro = parametro.Where(x => x.IdElemento == objDel.Id).ToList();
                                //dtoParam.IdElemento = entidad.Id;
                                //dtoParam.IdCaracteristica = item.CHAR_ID;
                                foreach (var item in objParametro)
                                {
                                    parametro.Remove(item);
                                }
                                reglaAsoc.Remove(objDel);
                            }

                            ReglaAsocTmp = reglaAsoc;
                            CaracteristicaTmp = caracteristica;
                            ParametroTmp = parametro;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddReglaAsoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BETarifaReglaAsociada> obtenerReglaAsoc()
        {
            int contador = 0;
            List<BETarifaReglaAsociada> datos = new List<BETarifaReglaAsociada>();
            if (ReglaAsocTmp != null)
            {
                ReglaAsocTmp.ForEach(x =>
                {
                    contador += 1;
                    datos.Add(new BETarifaReglaAsociada
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        RATE_CALC_ID = Convert.ToInt32(x.Id),
                        RATE_CALC = x.IdRegla,
                        RATE_CALCT = x.TipoCalculo,
                        RATE_CALC_VAR = GenerarLetraReglaAsoc(contador),
                        LOG_USER_CREAT = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneReglaAsocTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var variable = ReglaAsocTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(variable, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneReglaAsocTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarReglaAsoc()
        {
            int contador = 0;
            int contGeneralCaracteristicas = 0;
            reglaAsoc = ReglaAsocTmp;
            caracteristica = CaracteristicaTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblReglasAsoc' border=0 width='100%;' class='k-grid k-widget' id='tblReglasAsoc'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header'>V</th>");
                    shtml.Append("<th class='k-header'>Tipo</th>");
                    shtml.Append("<th class='k-header'>Elemento</th>");
                    shtml.Append("<th class='k-header'>Parametro 1</th>");
                    shtml.Append("<th class='k-header'>Parametro 2</th>");
                    shtml.Append("<th class='k-header'>Parametro 3</th>");
                    shtml.Append("<th class='k-header'>Parametro 4</th>");
                    shtml.Append("<th class='k-header'>Parametro 5</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th  class='k-header' style='width:60px'></th></tr></thead>");

                    if (reglaAsoc != null)
                    {
                        foreach (var item in reglaAsoc.OrderBy(x => x.Id))
                        {
                            if (item.Activo)
                            {
                                contador += 1;
                                var tempCaracteristicas = caracteristica.Where(p => p.IdElemento == item.Id).ToList();
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td >{0}</td>", GenerarLetraReglaAsoc(contador));
                                shtml.AppendFormat("<td >{0}</td>", item.Tipo);
                                shtml.AppendFormat("<td nowrap>{0}</td>", item.Elemento);

                                string param1 = string.Empty;
                                string param2 = string.Empty;
                                string param3 = string.Empty;
                                string param4 = string.Empty;
                                string param5 = string.Empty;

                                int cantCarXelemento = 0;
                                foreach (var itemCar in tempCaracteristicas)
                                {
                                    contGeneralCaracteristicas += 1;
                                    cantCarXelemento += 1;
                                    if (cantCarXelemento == 1) param1 = GenerarLetraCaracteristica(contGeneralCaracteristicas);
                                    if (cantCarXelemento == 2) param2 = GenerarLetraCaracteristica(contGeneralCaracteristicas);
                                    if (cantCarXelemento == 3) param3 = GenerarLetraCaracteristica(contGeneralCaracteristicas);
                                    if (cantCarXelemento == 4) param4 = GenerarLetraCaracteristica(contGeneralCaracteristicas);
                                    if (cantCarXelemento == 5) param5 = GenerarLetraCaracteristica(contGeneralCaracteristicas);
                                }

                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", param1);
                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", param2);
                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", param3);
                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", param4);
                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", param5);

                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td style='width:80px'>");
                                shtml.AppendFormat("<a href=# onclick='delAddReglaAsoc({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Elemento." : "Activar Elemento.");
                                shtml.Append("</td>");
                                shtml.Append("</tr>");
                            }
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    if (reglaAsoc != null)
                    {
                        int registros = reglaAsoc.Where(p => p.Activo == true).Count();
                        //if (registros == 1)
                        //    retorno.Code = 11;
                        //else
                        //    retorno.Code = registros;
                        retorno.Code = registros;
                    }
                    else
                    {
                        //retorno.result = -2;// 0 registros
                        retorno.Code=0;// 0 registros
                    }
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarReglaAsoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CARACTERISTICA
        public List<DTOTarifaManCaracteristica> CaracteristicaLista
        {
            get
            {
                return (List<DTOTarifaManCaracteristica>)Session[K_SESION_TARIFA_LISTA_CAR];
            }
            set
            {
                Session[K_SESION_TARIFA_LISTA_CAR] = value;
            }
        }

        public List<DTOTarifaManCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOTarifaManCaracteristica>)Session[K_SESION_TARIFA_CAR];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR] = value;
            }
        }

        private List<DTOTarifaManCaracteristica> CaracteristicaTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaManCaracteristica>)Session[K_SESION_TARIFA_CAR_ACT];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR_ACT] = value;
            }
        }

        private List<DTOTarifaManCaracteristica> CaracteristicaTmpDelBD
        {
            get
            {
                return (List<DTOTarifaManCaracteristica>)Session[K_SESION_TARIFA_CAR_DEL];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOTarifaManCaracteristica entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    int registroNuevo = 0;
                    int registroModificar = 0;
                    caracteristica = CaracteristicaTmp;

                    if (caracteristica == null) caracteristica = new List<DTOTarifaManCaracteristica>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (caracteristica.Count > 0) nuevoId = caracteristica.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        caracteristica.Add(entidad);
                    }
                    else
                    {
                        var item = caracteristica.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        caracteristica.Remove(item);
                        caracteristica.Add(entidad);
                    }
                    CaracteristicaTmp = caracteristica;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
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
                case 8: letra = "H"; break;
                case 9: letra = "I"; break;
                case 10: letra = "J"; break;

                case 11: letra = "K"; break;
                case 12: letra = "L"; break;
                case 13: letra = "M"; break;
                case 14: letra = "N"; break;
                case 15: letra = "O"; break;

                default:
                    letra = "P";
                    break;
            }
            return letra;
        }

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                caracteristica = CaracteristicaTmp;
                if (caracteristica != null)
                {
                    var objDel = caracteristica.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (CaracteristicaTmpUPDEstado == null) CaracteristicaTmpUPDEstado = new List<DTOTarifaManCaracteristica>();
                            if (CaracteristicaTmpDelBD == null) CaracteristicaTmpDelBD = new List<DTOTarifaManCaracteristica>();

                            var itemUpd = CaracteristicaTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = CaracteristicaTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) CaracteristicaTmpUPDEstado.Add(objDel);
                                if (itemDel != null) CaracteristicaTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) CaracteristicaTmpDelBD.Add(objDel);
                                if (itemUpd != null) CaracteristicaTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            caracteristica.Remove(objDel);
                            caracteristica.Add(objDel);
                        }
                        else
                        {
                            caracteristica.Remove(objDel);
                        }
                        CaracteristicaTmp = caracteristica;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

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
                    shtml.Append("<table class='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget' id='tblCaracteristica'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdElemento</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdRegla</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>IdCaracteristica</th>");
                    shtml.Append("<th class='k-header'>V</th>");
                    shtml.Append("<th class='k-header'>Tipo</th>");
                    shtml.Append("<th class='k-header'>Elemento</th>");
                    shtml.Append("<th class='k-header'>Descripción de la Caracteristica</th>");
                    shtml.Append("<th class='k-header'>Unidad de Medida</th>");
                    shtml.Append("<th class='k-header'>Imprime</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("</tr></thead>");

                    if (caracteristica != null)
                    {
                        foreach (var item in caracteristica.OrderBy(x => x.Id))
                        {
                            contador += 1;
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdElemento);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdRegla);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdCaracteristica);
                            item.Letra = GenerarLetraCaracteristica(contador);
                            shtml.AppendFormat("<td >{0}</td>", item.Letra);
                            shtml.AppendFormat("<td >{0}</td>", item.Tipo);
                            shtml.AppendFormat("<td >{0}</td>", item.DescripcionCorta);
                            shtml.AppendFormat("<td style='width:250px; text-align:center'> <input id='txtDescripcionLarga" + item.Id + "' type='text' value='" + item.DescripcionLarga + "'style=widht:150px' /></td>");//class='requerido' 
                            shtml.AppendFormat("<td style='width:250px; text-align:center '> <input class='requerido' id='txtUnidadMedida" + item.Id + "' type='text' value='" + item.UnidadMedida + "' style='width:120px' maxlength='16' /> </td>");

                            if (item.IndImpresion.ToUpper() == "0")
                                shtml.AppendFormat("<td style='text-align:center'><input id='chkUnidadMedida" + item.Id + "'   type='checkbox' value='0'></td>");
                            else
                                shtml.AppendFormat("<td style='text-align:center'><input id='chkUnidadMedida" + item.Id + "'  type='checkbox'  checked  ></td>");

                            shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.FechaCrea);
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
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCaracteristica", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region PARAMETRO

        public List<DTOTarifaManParametroAsoc> ParametroTmp
        {
            get
            {
                return (List<DTOTarifaManParametroAsoc>)Session[K_SESION_TARIFA_PARAM];
            }
            set
            {
                Session[K_SESION_TARIFA_PARAM] = value;
            }
        }

        private List<DTOTarifaManParametroAsoc> ParametroTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaManParametroAsoc>)Session[K_SESION_TARIFA_PARAM_ACT];
            }
            set
            {
                Session[K_SESION_TARIFA_PARAM_ACT] = value;
            }
        }

        private List<DTOTarifaManParametroAsoc> ParametroTmpDelBD
        {
            get
            {
                return (List<DTOTarifaManParametroAsoc>)Session[K_SESION_TARIFA_PARAM_DEL];
            }
            set
            {
                Session[K_SESION_TARIFA_PARAM_DEL] = value;
            }
        }

        private List<BETarifaReglaParamAsociada> obtenerParametro()
        {
            //int contador = 0;
            List<BETarifaReglaParamAsociada> datos = new List<BETarifaReglaParamAsociada>();
            if (ParametroTmp != null)
            {
                ParametroTmp.ForEach(x =>
                {
                    //contador += 1;
                    var entidad = CaracteristicaTmp.Where(c => c.IdRegla == x.IdRegla && c.IdCaracteristica == x.IdCaracteristica).FirstOrDefault();
                    datos.Add(new BETarifaReglaParamAsociada
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        RATE_PARAM_ID = x.Id,
                        RATE_CHAR_ID = x.IdChar,
                        RATE_CALC_ID = x.IdElemento,
                        RATE_CALC_AR = entidad.IdCaracteristica,
                        RATE_CALC = entidad.IdRegla,
                        RATE_PARAM_VAR = entidad.Letra,
                        LOG_USER_CREAT = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        #endregion

        #region DESCUENTO

        public List<DTOTarifaDescuento> DescuentoTmp
        {
            get
            {
                return (List<DTOTarifaDescuento>)Session[K_SESION_TARIFA_DESC];
            }
            set
            {
                Session[K_SESION_TARIFA_DESC] = value;
            }
        }

        private List<DTOTarifaDescuento> DescuentoTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaDescuento>)Session[K_SESION_TARIFA_DESC_ACT];
            }
            set
            {
                Session[K_SESION_TARIFA_DESC_ACT] = value;
            }
        }

        private List<DTOTarifaDescuento> DescuentoTmpDelBD
        {
            get
            {
                return (List<DTOTarifaDescuento>)Session[K_SESION_TARIFA_DESC_DEL];
            }
            set
            {
                Session[K_SESION_TARIFA_DESC_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDescuento(DTOTarifaDescuento entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    int registroNuevo = 0;
                    int registroModificar = 0;
                    descuento = DescuentoTmp;
                    if (descuento != null)
                    {
                        registroNuevo = descuento.Where(p => p.IdDesc == entidad.IdDesc && entidad.Id == 0).Count();
                        registroModificar = descuento.Where(p => p.IdDesc == entidad.IdDesc && p.Id == entidad.Id).Count();
                    }

                    if ((entidad.Id == 0 && registroNuevo == 0)
                         || (entidad.Id != 0 && registroModificar > 0)
                       )
                    {
                        descuento = DescuentoTmp;
                        if (descuento == null) descuento = new List<DTOTarifaDescuento>();
                        if (Convert.ToInt32(entidad.Id) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (descuento.Count > 0) nuevoId = descuento.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            descuento.Add(entidad);
                        }
                        else
                        {
                            var item = descuento.Where(x => x.Id == entidad.Id).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            descuento.Remove(item);
                            descuento.Add(entidad);
                        }
                        DescuentoTmp = descuento;
                        retorno.result = 1;
                        retorno.message = "OK";

                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Descuento ya existe o esta en estado inactivo.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddDescuento(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    descuento = DescuentoTmp;
                    if (descuento != null)
                    {
                        var objDel = descuento.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DescuentoTmpUPDEstado == null) DescuentoTmpUPDEstado = new List<DTOTarifaDescuento>();
                                if (DescuentoTmpDelBD == null) DescuentoTmpDelBD = new List<DTOTarifaDescuento>();

                                var itemUpd = DescuentoTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DescuentoTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DescuentoTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) DescuentoTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DescuentoTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DescuentoTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                descuento.Remove(objDel);
                                descuento.Add(objDel);
                            }
                            else
                            {
                                descuento.Remove(objDel);
                            }
                            DescuentoTmp = descuento;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BETarifaDescuento> obtenerDescuento()
        {
            List<BETarifaDescuento> datos = new List<BETarifaDescuento>();
            if (DescuentoTmp != null)
            {
                DescuentoTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaDescuento
                    {
                        RATE_DISC_ID = x.Id,
                        OWNER = GlobalVars.Global.OWNER,
                        RATE_ID = x.IdTarifa,
                        DISC_ID = x.IdDesc,
                        DISC_TYPE = x.IdTipoDesc,
                        DISC_TYPE_NAME = x.TipoDescripcion,
                        DISC_NAME = x.Descripcion,
                        DISC_SIGN = x.Formato,
                        DISC_PERC = x.Porcentaje,
                        DISC_VALUE = x.Valor,
                        DISC_ACC = x.CuentaContable,
                        DISC_AUT = x.Disc_Aut,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneDescuentoTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DescuentoTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneDescuentoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDescuento()
        {
            descuento = DescuentoTmp;
            Resultado retorno = new Resultado();
            var dctoEspec = Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["idTipoEspecialDscto"]);

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header'>Tipo Descuento</th>");
                    shtml.Append("<th class='k-header'>Descuento</th>");
                    shtml.Append("<th class='k-header'>Signo</th>");
                    shtml.Append("<th class='k-header'>Formato</th>");
                    shtml.Append("<th class='k-header'>Monto o %</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'></th></tr></thead>");

                    if (descuento != null)
                    {
                        foreach (var item in descuento.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDescripcion);
                            shtml.AppendFormat("<td >{0}</td>", item.Descripcion);
                            shtml.AppendFormat("<td style='width:50px;text-align:center' >{0}</td>", item.Signo);
                            shtml.AppendFormat("<td style='width:50px;text-align:center' >{0}</td>", item.Formato);//item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.Valor.ToString("######.00"));//item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='text-align:center' >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td style='text-align:center' >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td style='width:60px'>");

                            
                            if (item.IdTipoDesc == dctoEspec)
                            {
                                shtml.Append("<img src='../Images/iconos/delete.png'  title='No se puede quitar un descuento Tipo Especial' title='No se puede quitar un descuento Tipo Especial.' border=0 style=' cursor:pointer; cursor: hand;' /> ");
                            }
                            else {

                                shtml.AppendFormat("<a href=# onclick='delAddDescuento({0});'><img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Descuento" : "Activar Descuento");
                            }
                          
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append(" </table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDescuento", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PAGINACION
        public JsonResult ListarTarifa(int skip, int take, int page, int pageSize, string owner, decimal IdTarifa, string moneda, string moduso, string incidencia, string sociedad, string repertorio, decimal IdModalidad, int st, string descripcion)
        {
            var lista = Listatarifa(GlobalVars.Global.OWNER, IdTarifa, moneda, moduso, incidencia, sociedad, repertorio, IdModalidad, st,descripcion, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_RATES_GRAL { listaTarifa = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_RATES_GRAL { listaTarifa = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_RATES_GRAL> Listatarifa(string owner, decimal IdTarifa, string moneda, string moduso, string incidencia, string sociedad, string repertorio, decimal IdModalidad, int st,string descripcion, int pagina, int cantRegxPag)
        {
            return new BLREC_RATES_GRAL().ListarTarifasPage(owner, IdTarifa, moneda, moduso, incidencia, sociedad, repertorio, IdModalidad, st, descripcion,pagina, cantRegxPag);
        }
        #endregion

        #region TARIFA
        [HttpPost]
        public JsonResult Insertar(BEREC_RATES_GRAL tarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    tarifa.OWNER = GlobalVars.Global.OWNER;
                    tarifa.ReglasAsoc = obtenerReglaAsoc();
                    tarifa.Caracteristica = obtenerCaracteristica();
                    tarifa.Parametro = obtenerParametro();
                    tarifa.Descuento = obtenerDescuento();

                    if (tarifa.RATE_ID == 0)
                    {
                        tarifa.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLREC_RATES_GRAL().Insertar(tarifa);
                        new BLREC_RATES_GRAL().ObtenerRateOrigen(datos);
                    }
                    else
                    {
                        //var fechaActual = FechaSistema.ToShortDateString();
                        //var fechaCreacion = tarifa.LOG_DATE_CREAT.ToShortDateString();
                        //if (fechaActual == fechaCreacion) // ACTUALIZAR - Esta dentro del mismo  dia de la creación de la Tarifa
                        //{
                            tarifa.LOG_USER_UPDATE = UsuarioActual;

                            //.setting Regla eliminar
                            List<BETarifaReglaAsociada> listaReglaAsocDel = null;
                            if (ReglaAsocTmpDelBD != null)
                            {
                                listaReglaAsocDel = new List<BETarifaReglaAsociada>();
                                ReglaAsocTmpDelBD.ForEach(x => { listaReglaAsocDel.Add(new BETarifaReglaAsociada { RATE_CALC_ID = x.Id }); });
                            }

                            //.setting Caracteristica eliminar
                            List<BETarifaCaracteristica> listaCaracteristicaDel = null;
                            if (CaracteristicaTmpDelBD != null)
                            {
                                listaCaracteristicaDel = new List<BETarifaCaracteristica>();
                                CaracteristicaTmpDelBD.ForEach(x => { listaCaracteristicaDel.Add(new BETarifaCaracteristica { RATE_CHAR_ID = x.Id }); });
                            }

                            //.setting Parametro eliminar
                            List<BETarifaReglaParamAsociada> listaParametroDel = null;
                            if (ParametroTmpDelBD != null)
                            {
                                listaParametroDel = new List<BETarifaReglaParamAsociada>();
                                ParametroTmpDelBD.ForEach(x => { listaParametroDel.Add(new BETarifaReglaParamAsociada { RATE_PARAM_ID = x.Id }); });
                            }

                            //.setting Descuento eliminar
                            List<BETarifaDescuento> listaDescuentoDel = null;
                            if (DescuentoTmpDelBD != null)
                            {
                                listaDescuentoDel = new List<BETarifaDescuento>();
                                DescuentoTmpDelBD.ForEach(x => { listaDescuentoDel.Add(new BETarifaDescuento { RATE_DISC_ID = x.Id }); });
                            }

                            //setting Descuento activar
                            List<BETarifaDescuento> listaDescuentoUpdEst = null;
                            if (DescuentoTmpUPDEstado != null)
                            {
                                listaDescuentoUpdEst = new List<BETarifaDescuento>();
                                DescuentoTmpUPDEstado.ForEach(x => { listaDescuentoUpdEst.Add(new BETarifaDescuento { RATE_DISC_ID = x.Id }); });
                            }


                            var datos = new BLREC_RATES_GRAL().Actualizar(tarifa,
                                                                            listaReglaAsocDel,
                                                                            listaCaracteristicaDel,
                                                                            listaParametroDel,
                                                                            listaDescuentoDel,
                                                                            listaDescuentoUpdEst
                                                                        );
                            new BLREC_RATES_GRAL().ObtenerRateOrigen(tarifa.RATE_ID);
                        //}
                        //else // Realizar una copia para generar un historial
                        //{
                        //    tarifa.LOG_USER_CREAT = UsuarioActual;
                        //    tarifa.RATE_ID_PREC = tarifa.RATE_ID;
                        //    var newRateId = new BLREC_RATES_GRAL().Insertar(tarifa);
                        //    if (newRateId > 0)
                        //    {
                        //        var eliminado = new BLREC_RATES_GRAL().Eliminar(tarifa);                      
                        //    }
                        //}
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    BEREC_RATES_GRAL regla = new BEREC_RATES_GRAL();
                    regla = new BLREC_RATES_GRAL().Obtener(GlobalVars.Global.OWNER, id);


                    if (regla.ReglasAsoc != null)
                    {
                        reglaAsoc = new List<DTOTarifaManReglaAsoc>();
                        if (regla.ReglasAsoc != null)
                        {
                            regla.ReglasAsoc.ForEach(s =>
                            {
                                reglaAsoc.Add(new DTOTarifaManReglaAsoc
                                {
                                    Id = s.RATE_CALC_ID,
                                    IdRegla = s.RATE_CALC,
                                    Elemento = s.ELEMENTO,
                                    Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "FUNCION",
                                    TipoCalculo = s.RATE_CALCT,
                                    IdTarifa = s.RATE_ID,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    Letra = s.RATE_CALC_VAR
                                });
                            });
                            ReglaAsocTmp = reglaAsoc;
                        }

                        caracteristica = new List<DTOTarifaManCaracteristica>();
                        if (regla.Caracteristica != null)
                        {
                            regla.Caracteristica.ForEach(s =>
                            {
                                caracteristica.Add(new DTOTarifaManCaracteristica
                                {
                                    Id = s.RATE_CHAR_ID,
                                    IdElemento = s.RATE_CALC_ID,
                                    IdRegla = s.RATE_CALC,
                                    IdCaracteristica = s.RATE_CALC_AR,
                                    Letra = s.RATE_CHAR_TVAR,
                                    Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "VARIABLE",
                                    DescripcionCorta = s.RATE_CHAR_SHORT,
                                    DescripcionLarga = s.RATE_CHAR_DESCVAR,
                                    UnidadMedida = s.RATE_CHAR_VARUNID,
                                    IndImpresion = s.RATE_CHAR_CARIDSW,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT
                                });
                            });
                            CaracteristicaTmp = caracteristica;
                        }

                        parametro = new List<DTOTarifaManParametroAsoc>();
                        if (regla.Parametro != null)
                        {
                            regla.Parametro.ForEach(s =>
                            {
                                parametro.Add(new DTOTarifaManParametroAsoc
                                {
                                    Id = s.RATE_PARAM_ID,
                                    IdChar = s.RATE_CHAR_ID,
                                    IdElemento = s.RATE_CALC_ID,
                                    IdCaracteristica = s.RATE_CALC_AR,
                                    IdRegla = s.RATE_CALC,
                                    Letra = s.RATE_PARAM_VAR,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT
                                });
                            });
                            ParametroTmp = parametro;
                        }

                        descuento = new List<DTOTarifaDescuento>();
                        if (regla.Descuento != null)
                        {
                            regla.Descuento.ForEach(s =>
                            {
                                descuento.Add(new DTOTarifaDescuento
                                {
                                    Id = s.RATE_DISC_ID,
                                    IdTarifa = s.RATE_ID,
                                    IdDesc = s.DISC_ID,
                                    IdTipoDesc = s.DISC_TYPE,
                                    TipoDescripcion = s.DISC_TYPE_NAME,
                                    Descripcion = s.DISC_NAME,
                                    Signo = s.DISC_SIGN,
                                    Formato = (s.DISC_PERC == 0) ? "S/." :"%"  ,
                                    Valor = (s.DISC_PERC == 0) ? s.DISC_VALUE : s.DISC_PERC,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT
                                });
                            });
                            DescuentoTmp = descuento;
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(regla, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult ObtenerListaCaracteristica(List<BETarifaCaracteristica> CaracteristicaValor)
        {
            Session.Remove(K_SESION_TARIFA_LISTA_CAR);
            List<BETarifaCaracteristica> ListaCaracteristicaValor = new List<BETarifaCaracteristica>();
            BETarifaCaracteristica entidad = null;
            CaracteristicaTmp.ForEach(s =>
            {
                entidad = new BETarifaCaracteristica();
                entidad = CaracteristicaValor.Where(p => p.RATE_CHAR_ID == s.Id).FirstOrDefault();
                s.DescripcionLarga = entidad.RATE_CHAR_DESCVAR;
                s.UnidadMedida = entidad.RATE_CHAR_VARUNID;
                s.IndImpresion = entidad.RATE_CHAR_CARIDSW;
            });

            Resultado retorno = new Resultado();
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
        public JsonResult ObtieneDescuento(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDescuentos tipo = new BEDescuentos();
                    var lista = new BLDescuentos().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.DISC_ID = item.DISC_ID;
                            tipo.DISC_TYPE = item.DISC_TYPE;
                            tipo.DISC_NAME = item.DISC_NAME;
                            tipo.DISC_SIGN = item.DISC_SIGN;
                            tipo.DISC_PERC = item.DISC_PERC;
                            tipo.DISC_VALUE = item.DISC_VALUE;
                            tipo.DISC_ACC = item.DISC_ACC;
                            tipo.DISC_AUT = item.DISC_AUT;
                            tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el descuento";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", "aatusparia", "obtiene Descuentos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerValorePopup()
        {
            Resultado retorno = new Resultado();
            try
            {
                List<DTOTarifaManReglaAsoc> listaValores = new List<DTOTarifaManReglaAsoc>();
                if (ReglaAsocTmp != null)
                {
                    int contador = 0;
                    DTOTarifaManReglaAsoc ent = null;
                    foreach (var item in ReglaAsocTmp)
                    {
                        contador += 1;
                        ent = new DTOTarifaManReglaAsoc();
                        ent.Id = item.Id;
                        ent.Letra = GenerarLetraReglaAsoc(contador);
                        listaValores.Add(ent);
                    }
                }

                var datos = listaValores
                 .Select(c => new SelectListItem
                 {
                     Value = c.Letra.ToUpper(),
                     Text = c.Letra.ToUpper()
                 });

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerValorePopup", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCantPeriodocidadXProd(BEREC_RATES_GRAL tarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    tarifa.OWNER = GlobalVars.Global.OWNER;
                    var cantReg = new BLREC_RATES_GRAL().ObtenerCantPeriodocidadXProd(tarifa);
                    retorno.data = Json(cantReg, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCantPeriodocidadXProd", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
