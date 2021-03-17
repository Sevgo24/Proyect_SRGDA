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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Windows.Forms;

namespace Proyect_Apdayc.Controllers.Tarifa
{
    public class TarifaReglaController : Base
    {
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_REGLA_CARACTERISTICA = "___DTOReglaCaracteristica";
        private const string K_SESION_REGLA_CARACTERISTICA_DEL = "___DTOReglaCaracteriticaDEL";
        private const string K_SESION_REGLA_CARACTERISTICA_ACT = "___DTOReglaCaracteristicaACT";

        private const string K_SESION_VALOR_TARIFA = "___DTOValorTarifa";
        private const string K_SESION_VALOR_TARIFA_DEL = "___DTOValorTarifaDEL";
        private const string K_SESION_VALOR_TARIFA_ACT = "___DTOValorTarifaACT";

        private const string K_SESION_MATRIZ_VAL_TARIFA = "___DTOValorMatrizTarifa";
        private const string K_SESION_MATRIZ_VAL_TARIFA_DEL = "___DTOValorMatrizTarifaDEL";
        private const string K_SESION_MATRIZ_VAL_TARIFA_ACT = "___DTOValorMatrizTarifaACT";

        private const string K_SESION_MATRIZ_CANT_CAR = "___DTOMatrizCantCaracteristica";
        private const string K_SESION_MATRIZ_TABLA = "___DTOMatrizTabla";
        private const string K_SESION_BE_OBTENER_MATRIZ_TABLA = "___DTOBE_ObtenerMatrizTabla";

        //
        // GET: /TarifaRegla/
        List<DTOTarifaReglaData> caracteristicas = new List<DTOTarifaReglaData>();
        List<DTOTarifaValor> valores = new List<DTOTarifaValor>();
        List<DTOTarifaReglaValor> valoresMatriz = new List<DTOTarifaReglaValor>();

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_REGLA_CARACTERISTICA);
            Session.Remove(K_SESION_REGLA_CARACTERISTICA_DEL);
            Session.Remove(K_SESION_REGLA_CARACTERISTICA_ACT);
            Session.Remove(K_SESION_VALOR_TARIFA);
            Session.Remove(K_SESION_VALOR_TARIFA_DEL);
            Session.Remove(K_SESION_VALOR_TARIFA_ACT);
            Session.Remove(K_SESION_MATRIZ_CANT_CAR);
            Session.Remove(K_SESION_MATRIZ_TABLA);
            Session.Remove(K_SESION_BE_OBTENER_MATRIZ_TABLA);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string desc, decimal nro, string fini, string ffin, int estado, int periodocidad, int confecha)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, desc, nro, Convert.ToDateTime(fini), Convert.ToDateTime(ffin), estado, periodocidad,confecha, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETarifaRegla { ListarTarifaRegla = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETarifaRegla { ListarTarifaRegla = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETarifaRegla> BLListar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int periodocidad,int confecha, int pagina, int cantRegxPag)
        {
            return new BLTarifaRegla().Listar(owner, desc, nro, fini, ffin, estado, periodocidad, confecha, pagina, cantRegxPag);
        }


        #region CARACTERISTICAS

        public List<DTOTarifaReglaData> CaracteristicasTmp
        {
            get
            {
                return (List<DTOTarifaReglaData>)Session[K_SESION_REGLA_CARACTERISTICA];
            }
            set
            {
                Session[K_SESION_REGLA_CARACTERISTICA] = value;
            }
        }

        private List<DTOTarifaReglaData> CaracteristicasTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaReglaData>)Session[K_SESION_REGLA_CARACTERISTICA_ACT];
            }
            set
            {
                Session[K_SESION_REGLA_CARACTERISTICA_ACT] = value;
            }
        }

        private List<DTOTarifaReglaData> CaracteristicasTmpDelBD
        {
            get
            {
                return (List<DTOTarifaReglaData>)Session[K_SESION_REGLA_CARACTERISTICA_DEL];
            }
            set
            {
                Session[K_SESION_REGLA_CARACTERISTICA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOTarifaReglaData entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                int registroNuevo = 0;
                int registroModificar = 0;
                caracteristicas = CaracteristicasTmp;
                if (caracteristicas != null)
                {
                    registroNuevo = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && entidad.Id == 0).Count();
                    registroModificar = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && p.Id == entidad.Id).Count();
                }
                else
                {
                    caracteristicas = new List<DTOTarifaReglaData>();
                }

                if ((entidad.Id == 0 && registroNuevo == 0)
                     || (entidad.Id != 0 && registroModificar > 0)
                   )
                {

                    if (caracteristicas == null) caracteristicas = new List<DTOTarifaReglaData>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (caracteristicas.Count > 0) nuevoId = caracteristicas.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.OrigenCaracteristica = Constantes.OrigenCaracteristica.Manual;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        entidad.Letra = GenerarLetraAddCaracteristica();
                        //Letra=  !string.IsNullOrEmpty(s.LETRA) ? s.LETRA : GenerarLetraCaracteristica(contador)
                        caracteristicas.Add(entidad);
                    }
                    else
                    {
                        var item = caracteristicas.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        caracteristicas.Remove(item);
                        caracteristicas.Add(entidad);
                    }
                    CaracteristicasTmp = caracteristicas;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "La caracteristica ya existe.";
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

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id, decimal idPlantilla = 0)
        {
            Resultado retorno = new Resultado();
            try
            {
                caracteristicas = CaracteristicasTmp;
                if (caracteristicas != null)
                {
                    var objDel = caracteristicas.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (CaracteristicasTmpUPDEstado == null) CaracteristicasTmpUPDEstado = new List<DTOTarifaReglaData>();
                            if (CaracteristicasTmpDelBD == null) CaracteristicasTmpDelBD = new List<DTOTarifaReglaData>();

                            var itemUpd = CaracteristicasTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = CaracteristicasTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) CaracteristicasTmpUPDEstado.Add(objDel);
                                if (itemDel != null) CaracteristicasTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) CaracteristicasTmpDelBD.Add(objDel);
                                if (itemUpd != null) CaracteristicasTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            caracteristicas.Remove(objDel);
                            //*********************************************
                            string charsDel = "";

                            foreach (var item in CaracteristicasTmpDelBD)
                            {
                                charsDel = charsDel + "," + item.IdCaracteristica;
                            }
                            charsDel = charsDel.Remove(0, 1);
                            int countChar = 0;
                            BETarifaPlantilla regla = new BETarifaPlantilla();
                            regla = new BLTarifaRegla().ObtenerPlantillaCaracteristicaEliminado(GlobalVars.Global.OWNER, idPlantilla, charsDel, out countChar);

                            CantidadCaracteristicasmatrizTmp = countChar;

                            valores = new List<DTOTarifaValor>();
                            if (regla.Valores != null)
                            {
                                regla.Valores.ForEach(s =>
                                {
                                    valores.Add(new DTOTarifaValor
                                    {
                                        Id = s.TEMPS_ID,
                                        IdCaracteristica = s.TEMPL_ID,
                                        CharId = s.CHAR_ID,
                                        Caracteristica = s.CARACTERISTICA,
                                        Descripcion = s.SECC_DESC,
                                        Desde = s.SECC_FROM,
                                        Hasta = s.SECC_TO,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true,
                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        Tramo = s.IND_TR
                                    });
                                });
                                ValoresTmp = valores;
                            }

                            valoresMatriz = new List<DTOTarifaReglaValor>();
                            if (regla.ValoresMatriz != null)
                            {
                                regla.ValoresMatriz.ForEach(s =>
                                {
                                    valoresMatriz.Add(new DTOTarifaReglaValor
                                    {
                                        Id = s.CALRV_ID,
                                        IdVal_1 = s.TEMPS1_ID,
                                        Descripcion_1 = s.SECC1_DESC,
                                        Desde_1 = s.CRI1_FROM,
                                        Hasta_1 = s.CRI1_TO,

                                        IdVal_2 = s.TEMPS2_ID,
                                        Descripcion_2 = s.SECC2_DESC,
                                        Desde_2 = s.CRI2_FROM,
                                        Hasta_2 = s.CRI2_TO,

                                        IdVal_3 = s.TEMPS3_ID,
                                        Descripcion_3 = s.SECC3_DESC,
                                        Desde_3 = s.CRI3_FROM,
                                        Hasta_3 = s.CRI3_TO,

                                        IdVal_4 = s.TEMPS4_ID,
                                        Descripcion_4 = s.SECC4_DESC,
                                        Desde_4 = s.CRI4_FROM,
                                        Hasta_4 = s.CRI4_TO,

                                        IdVal_5 = s.TEMPS5_ID,
                                        Descripcion_5 = s.SECC5_DESC,
                                        Desde_5 = s.CRI5_FROM,
                                        Hasta_5 = s.CRI5_TO,

                                        Tarifa = s.VAL_FORMULA,
                                        Minimo = s.VAL_MINIMUM,
                                        EnBD = true
                                    });
                                });
                                ValoresMatrizTmp = valoresMatriz;
                            }
                        }
                        else
                        {
                            caracteristicas.Remove(objDel);
                        }
                        CaracteristicasTmp = caracteristicas;
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

        private List<BETarifaReglaData> obtenerCaracteristicas()
        {
            int contador = 0;
            List<BETarifaReglaData> datos = new List<BETarifaReglaData>();
            if (CaracteristicasTmp != null)
            {
                CaracteristicasTmp.ForEach(x =>
                {
                    contador += 1;
                    datos.Add(new BETarifaReglaData
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CALRD_ID = x.Id,
                        CHAR_OTYPE=x.OrigenCaracteristica,
                        CHAR_ID = x.IdCaracteristica,
                        CHAR_LONG = x.DesCaracteristica,
                        CALRD_VAR = GenerarLetraCaracteristica(contador),
                        IND_TR = Convert.ToString(x.Tramo),
                        LOG_USER_CREAT = UsuarioActual,

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneCaracteristicaTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                var variable = CaracteristicasTmp.Where(x => x.Id == idDir).FirstOrDefault();
                retorno.data = Json(variable, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneCaracteristicaTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public string GenerarLetraAddCaracteristica()
        {
            string letra = string.Empty;
            if (CaracteristicasTmp != null)
            {
                string[] letras = { "A", "B", "C", "D", "E" };
                var letrasActivas = from c in CaracteristicasTmp where c.Activo == true select c.Letra;

                var l = from elemt in letras
                        where !(from c in CaracteristicasTmp
                                where c.Activo == true
                                select c.Letra).Contains(elemt)
                        select elemt;
                if (l != null)
                {
                    foreach (var item in l)
                    {
                        letra = item; break;
                    }
                }
                else
                {
                    letra = "X";
                }
            }
            else
            {
                letra = "A";
            }
            return letra;
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
                default: letra = "X";
                    break;
            }
            return letra;
        }

        public JsonResult ListarCaracteristica(decimal idCaracteristica = 0)
        {
            int contador = 0;
            caracteristicas = CaracteristicasTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget' id='tblCaracteristica'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='k-header'></th>");
                shtml.Append("<th class='k-header'>V</th>");
                shtml.Append("<th class='k-header' style='display:none;'>IdCar</th>");
                shtml.Append("<th class='k-header'>Caracteristica</th>");
                shtml.Append("<th class='k-header'>Tipo</th>");
                shtml.Append("<th class='k-header'>Tramo</th>");
                shtml.Append("<th class='k-header'>Usuario Creación</th>");
                shtml.Append("<th class='k-header'>Fecha Creación</th>");
                shtml.Append("<th  class='k-header' style='width:60px'></th>");
                shtml.Append("</tr></thead>");

                if (caracteristicas != null)
                {
                    foreach (var item in caracteristicas)
                    {
                        if (item.Activo)
                        {
                            contador += 1;
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", GenerarLetraCaracteristica(contador));
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdCaracteristica);
                            shtml.AppendFormat("<td nowrap>{0}</td>", item.DesCaracteristica);
                            shtml.AppendFormat("<td  style='text-align:center' nowrap>{0}</td>", item.OrigenCaracteristica);
                            if (item.Tramo == 1)
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED checked  ></td>");
                            else
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED></td>");

                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);

                            shtml.AppendFormat("<td style='width:80px'>");
                            if (item.OrigenCaracteristica == Constantes.OrigenCaracteristica.Manual)
                                shtml.AppendFormat("<a href=# onclick='delAddCaracteristica({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar caracteristica." : "Activar caracteristica.");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                if (caracteristicas != null)
                {
                    int registros = caracteristicas.Where(p => p.Activo == true).Count();
                    //retorno.result = registros;
                    retorno.Code = registros;
                }
                else
                {
                    retorno.Code = 0;
                }
                retorno.result = 1;
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

        #region VALORES

        public List<DTOTarifaValor> ValoresTmp
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA] = value;
            }
        }

        private List<DTOTarifaValor> ValoresTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA_ACT];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA_ACT] = value;
            }
        }

        private List<DTOTarifaValor> ValoresTmpDelBD
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA_DEL];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA_DEL] = value;
            }
        }

        private List<BETarifaPlantillaValor> obtenerValores()
        {
            List<BETarifaPlantillaValor> datos = new List<BETarifaPlantillaValor>();
            if (ValoresTmp != null)
            {
                ValoresTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaValor
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMPS_ID = Convert.ToInt32(x.Id),
                        TEMPL_ID = Convert.ToInt32(x.IdCaracteristica),
                        SECC_DESC = x.Descripcion,
                        SECC_FROM = x.Desde,
                        SECC_TO = x.Hasta,
                        LOG_USER_CREAT = UsuarioActual,
                        IND_TR = x.Tramo
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneValorTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                var variable = ValoresTmp.Where(x => x.Id == idDir).FirstOrDefault();
                retorno.data = Json(variable, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneValorTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarValor(decimal idValor = 0)
        {
            valores = ValoresTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='FiltroTabla'>");
                shtml.Append("<thead>");
                shtml.Append("<tr>");
                shtml.Append("<th class='k-header' ></th>");
                shtml.Append("<th class='k-header'>Caracteristica</th>");
                shtml.Append("<th class='k-header'>Valor</th>");
                shtml.Append("<th class='k-header'>Tramo</th>");
                shtml.Append("<th class='k-header'>Desde</th>");
                shtml.Append("<th class='k-header'>Hasta</th>");

                if (CaracteristicasTmp != null)
                {
                    var chars = (from c in CaracteristicasTmp
                                 select c.IdCaracteristica).ToArray();
                    var val = valores.Where(s => chars.Contains(s.CharId));

                    if (val != null && val.ToList().Count > 0)
                    {
                        foreach (var item in val.OrderBy(x => x.IdCaracteristica).ToList())
                        {
                            if (item.Activo)
                            {
                                shtml.Append("<tr style='background-color:white'>");
                                shtml.AppendFormat("<td style='width:50px'  style='display:none'>{0}</td>", item.CharId);
                                shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Caracteristica);
                                shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion);
                                shtml.AppendFormat("<td >{0}</td>", item.Tramo == "1" ? "S" : "N");
                                shtml.AppendFormat("<td >{0}</td>", item.Desde.ToString("### ###.00"));
                                shtml.AppendFormat("<td >{0}</td>", (item.Tramo == "0" || item.Hasta == 0) ? "" : item.Hasta.ToString("### ###.00"));
                                shtml.Append("</tr>");
                            }
                        }
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region VALORES_MATRIZ
        public List<BETarifaReglaValor> BEObtenerMatrizTabla
        {
            get
            {
                return (List<BETarifaReglaValor>)Session[K_SESION_BE_OBTENER_MATRIZ_TABLA];
            }
            set
            {
                Session[K_SESION_BE_OBTENER_MATRIZ_TABLA] = value;
            }
        }

        public int CantidadCaracteristicasmatrizTmp
        {
            get
            {
                return (int)Session[K_SESION_MATRIZ_CANT_CAR];
            }
            set
            {
                Session[K_SESION_MATRIZ_CANT_CAR] = value;
            }
        }

        public List<DTOTarifaReglaValor> ValoresMatrizTmp
        {
            get
            {
                return (List<DTOTarifaReglaValor>)Session[K_SESION_MATRIZ_VAL_TARIFA];
            }
            set
            {
                Session[K_SESION_MATRIZ_VAL_TARIFA] = value;
            }
        }

        private List<DTOTarifaReglaValor> ValoresMatrizTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaReglaValor>)Session[K_SESION_MATRIZ_VAL_TARIFA_ACT];
            }
            set
            {
                Session[K_SESION_MATRIZ_VAL_TARIFA_ACT] = value;
            }
        }

        private List<DTOTarifaReglaValor> ValoresMatrizTmpDelBD
        {
            get
            {
                return (List<DTOTarifaReglaValor>)Session[K_SESION_MATRIZ_VAL_TARIFA_DEL];
            }
            set
            {
                Session[K_SESION_MATRIZ_VAL_TARIFA_DEL] = value;
            }
        }

        private List<BETarifaPlantillaValor> obtenerValoresMatriz()
        {
            List<BETarifaPlantillaValor> datos = new List<BETarifaPlantillaValor>();
            if (ValoresTmp != null)
            {
                ValoresTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaValor
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMPS_ID = Convert.ToInt32(x.Id),
                        TEMPL_ID = Convert.ToInt32(x.IdCaracteristica),
                        SECC_DESC = x.Descripcion,
                        SECC_FROM = x.Desde,
                        SECC_TO = x.Hasta,
                        LOG_USER_CREAT = UsuarioActual,
                        IND_TR = x.Tramo
                    });
                });
            }
            return datos;
        }

        public JsonResult ListarValorMatriz(decimal idValor = 0)
        {
            valoresMatriz = ValoresMatrizTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblMatriz'>");
                //TablaMatrizTmp.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblMatriz'>");
                shtml.Append("<thead>");
                shtml.Append("<tr>");
                shtml.Append("<th class='k-header' style='width:50px'>Id</th>");
                if (CantidadCaracteristicasmatrizTmp >= 1)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_1</th>");
                    shtml.Append("<th class='k-header'>Descripción del tramo - Valor 1</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Desde_1</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Hasta_1</th>");
                }
                if (CantidadCaracteristicasmatrizTmp >= 2)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_2</th>");
                    shtml.Append("<th class='k-header'>Descripción del tramo - Valor 2</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Desde_2</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Hasta_2</th>");
                }
                if (CantidadCaracteristicasmatrizTmp >= 3)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_3</th>");
                    shtml.Append("<th class='k-header'>Descripción del tramo - Valor 3</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Desde_3</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Hasta_3</th>");
                }
                if (CantidadCaracteristicasmatrizTmp >= 4)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_4</th>");
                    shtml.Append("<th class='k-header'>Descripción del tramo - Valor 4</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Desde_4</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Hasta_4</th>");
                }
                if (CantidadCaracteristicasmatrizTmp >= 5)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_5</th>");
                    shtml.Append("<th class='k-header'>Descripción del tramo - Valor 5</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Desde_5</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Hasta_5</th>");
                }

                shtml.Append("<th class='k-header' >Tarifa</th>");
                shtml.Append("<th class='k-header' >Minimo</th>");
                shtml.Append("</tr>");
                if (valoresMatriz != null && valoresMatriz.Count > 0)
                {
                    //foreach (var item in valoresMatriz.OrderBy(x => x.Id))
                    foreach (var item in valoresMatriz)
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:50px'>{0}</td>", item.Id);
                        if (CantidadCaracteristicasmatrizTmp >= 1)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.IdVal_1);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion_1);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Desde_1.ToString("### ###.00"));
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", (item.Hasta_1 == 0) ? "" : item.Hasta_1.ToString("### ###.00"));
                        }

                        if (CantidadCaracteristicasmatrizTmp >= 2)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.IdVal_2);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion_2);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Desde_2.ToString("### ###.00"));
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", (item.Hasta_2 == 0) ? "" : item.Hasta_2.ToString("### ###.00"));
                        }

                        if (CantidadCaracteristicasmatrizTmp >= 3)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.IdVal_3);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion_3);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Desde_3.ToString("### ###.00"));
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", (item.Hasta_3 == 0) ? "" : item.Hasta_3.ToString("### ###.00"));
                        }

                        if (CantidadCaracteristicasmatrizTmp >= 4)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.IdVal_4);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion_4);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Desde_4.ToString("### ###.00"));
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", (item.Hasta_4 == 0) ? "" : item.Hasta_4.ToString("### ###.00"));
                        }

                        if (CantidadCaracteristicasmatrizTmp >= 5)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.IdVal_5);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion_5);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Desde_5.ToString("### ###.00"));
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", (item.Hasta_5 == 0) ? "" : item.Hasta_5.ToString("### ###.00"));
                        }

                        shtml.AppendFormat("<td style='width:250px; text-align:center '> <input class='requerido' id='txtMatrizTarifa" + item.Id + "' type='text' value='" + item.Tarifa + "' style='width:120px' maxlength='16' /> </td>");
                        shtml.AppendFormat("<td style='width:250px; text-align:center '> <input class='requerido' id='txtMatrizMinimo" + item.Id + "' type='text' value='" + item.Minimo + "'style='width:120px' maxlength='16'/> </td>");
                        shtml.Append("</tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarValorMatriz", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //*************************************************************
        [HttpPost]
        public JsonResult Insertar(BETarifaRegla regla)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    regla.OWNER = GlobalVars.Global.OWNER;
                    regla.Caracteristicas = obtenerCaracteristicas();
                    regla.MatrizValores = BEObtenerMatrizTabla;

                    if (regla.CALR_ID == 0)
                    {
                        regla.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLTarifaRegla().Insertar(regla);
                    }
                    else
                    {
                        regla.LOG_USER_UPDATE = UsuarioActual;
                        //.setting caracteristica eliminar

                        List<BETarifaReglaData> listaCaracteristicaDel = null;
                        if (CaracteristicasTmpDelBD != null)
                        {
                            listaCaracteristicaDel = new List<BETarifaReglaData>();
                            CaracteristicasTmpDelBD.ForEach(x => { listaCaracteristicaDel.Add(new BETarifaReglaData { CALRD_ID = x.Id }); });
                        }
                        //setting caracteristica activar
                        List<BETarifaReglaData> listaCaracteristicaUpdEst = null;
                        if (CaracteristicasTmpUPDEstado != null)
                        {
                            listaCaracteristicaUpdEst = new List<BETarifaReglaData>();
                            CaracteristicasTmpUPDEstado.ForEach(x => { listaCaracteristicaUpdEst.Add(new BETarifaReglaData { CALRD_ID = x.Id }); });
                        }

                        var datos = new BLTarifaRegla().Actualizar(regla,
                          listaCaracteristicaDel, listaCaracteristicaUpdEst
                            //listaValorDel, listaValorUpdEst
                        );
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
                BETarifaRegla regla = new BETarifaRegla();
                regla = new BLTarifaRegla().Obtener(GlobalVars.Global.OWNER, id);

                if (regla.Caracteristicas != null)
                {
                    //CantidadCaracteristicasmatrizTmp = regla.Caracteristicas.Count;
                    CantidadCaracteristicasmatrizTmp = regla.CantCarMatriz;
                    caracteristicas = new List<DTOTarifaReglaData>();
                    if (regla.Caracteristicas != null)
                    {
                        regla.Caracteristicas.ForEach(s =>
                        {
                            caracteristicas.Add(new DTOTarifaReglaData
                            {
                                Id = s.CALRD_ID,
                                IdPlantilla = s.TEMPL_ID,
                                IdCaracteristica = s.CHAR_ID,
                                DesCaracteristica = s.CHAR_LONG,
                                OrigenCaracteristica = s.CHAR_OTYPE,
                                Tramo = Convert.ToDecimal(s.IND_TR),
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                Letra = s.CALRD_VAR
                            });
                        });
                        CaracteristicasTmp = caracteristicas;
                    }

                    valores = new List<DTOTarifaValor>();
                    if (regla.Valores != null)
                    {
                        regla.Valores.ForEach(s =>
                        {
                            valores.Add(new DTOTarifaValor
                            {
                                Id = s.TEMPS_ID,
                                IdCaracteristica = s.TEMPL_ID,
                                CharId = s.CHAR_ID,
                                Caracteristica = s.CARACTERISTICA,
                                Descripcion = s.SECC_DESC,
                                Desde = s.SECC_FROM,
                                Hasta = s.SECC_TO,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                Tramo = s.IND_TR
                            });
                        });
                        ValoresTmp = valores;
                    }

                    valoresMatriz = new List<DTOTarifaReglaValor>();
                    if (regla.MatrizValores != null)
                    {
                        regla.MatrizValores.ForEach(s =>
                        {
                            valoresMatriz.Add(new DTOTarifaReglaValor
                            {
                                Id = s.CALRV_ID,
                                IdVal_1 = s.TEMPS1_ID,
                                Descripcion_1 = s.SECC1_DESC,
                                Desde_1 = s.CRI1_FROM,
                                Hasta_1 = s.CRI1_TO,

                                IdVal_2 = s.TEMPS2_ID,
                                Descripcion_2 = s.SECC2_DESC,
                                Desde_2 = s.CRI2_FROM,
                                Hasta_2 = s.CRI2_TO,

                                IdVal_3 = s.TEMPS3_ID,
                                Descripcion_3 = s.SECC3_DESC,
                                Desde_3 = s.CRI3_FROM,
                                Hasta_3 = s.CRI3_TO,

                                IdVal_4 = s.TEMPS4_ID,
                                Descripcion_4 = s.SECC4_DESC,
                                Desde_4 = s.CRI4_FROM,
                                Hasta_4 = s.CRI4_TO,

                                IdVal_5 = s.TEMPS5_ID,
                                Descripcion_5 = s.SECC5_DESC,
                                Desde_5 = s.CRI5_FROM,
                                Hasta_5 = s.CRI5_TO,

                                Tarifa = s.VAL_FORMULA,
                                Minimo = s.VAL_MINIMUM,
                                EnBD = true
                            });
                        });
                        ValoresMatrizTmp = valoresMatriz;
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(regla, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha encontrado la definición de gasto";
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
        public JsonResult ObtenerPlantilla(decimal id)
        {
            //Eliminar
            if (CaracteristicasTmp != null)
            {
                if (CaracteristicasTmpDelBD == null) CaracteristicasTmpDelBD = new List<DTOTarifaReglaData>();

                var itemCaracteristica = CaracteristicasTmp.ToList();
                foreach (var item in itemCaracteristica)
                {
                    CaracteristicasTmpDelBD.Add(item);
                }
            }
            //caracteristicas.Remove(objDel);
            //-------------------------------------------
            Session.Remove(K_SESION_REGLA_CARACTERISTICA);
            //Session.Remove(K_SESION_REGLA_CARACTERISTICA_DEL);
            Session.Remove(K_SESION_REGLA_CARACTERISTICA_ACT);
            Session.Remove(K_SESION_VALOR_TARIFA);
            Session.Remove(K_SESION_VALOR_TARIFA_DEL);
            Session.Remove(K_SESION_VALOR_TARIFA_ACT);
            Resultado retorno = new Resultado();
            try
            {
                BETarifaPlantilla tarifa = new BETarifaPlantilla();
                int countChar = 0;
                tarifa = new BLTarifaRegla().ObtenerPlantilla(GlobalVars.Global.OWNER, id, out countChar);
                if (tarifa != null)
                {
                    CantidadCaracteristicasmatrizTmp = countChar;
                    if (tarifa.Caracteristicas != null)
                    {
                        caracteristicas = new List<DTOTarifaReglaData>();
                        if (tarifa.Caracteristicas != null)
                        {
                            int contador = 0;
                            tarifa.Caracteristicas.ForEach(s =>
                            {
                                contador += 1;
                                caracteristicas.Add(new DTOTarifaReglaData
                                {
                                    Id = contador,
                                    IdPlantilla = s.TEMPL_ID,
                                    IdCaracteristica = s.CHAR_ID,
                                    DesCaracteristica = s.CHAR_LONG,
                                    OrigenCaracteristica=Constantes.OrigenCaracteristica.Plantilla,
                                    Tramo = Convert.ToDecimal(s.IND_TR),
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    Letra = !string.IsNullOrEmpty(s.LETRA) ? s.LETRA : GenerarLetraCaracteristica(contador)
                                });
                            });
                            CaracteristicasTmp = caracteristicas;
                        }

                        valores = new List<DTOTarifaValor>();
                        if (tarifa.Valores != null)
                        {
                            tarifa.Valores.ForEach(s =>
                            {
                                valores.Add(new DTOTarifaValor
                                {
                                    Id = s.TEMPS_ID,
                                    IdCaracteristica = s.TEMPL_ID,
                                    CharId = s.CHAR_ID,
                                    Caracteristica = s.CARACTERISTICA,
                                    Descripcion = s.SECC_DESC,
                                    Desde = s.SECC_FROM,
                                    Hasta = s.SECC_TO,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    Tramo = s.IND_TR
                                });
                            });
                            ValoresTmp = valores;
                        }

                        valoresMatriz = new List<DTOTarifaReglaValor>();
                        if (tarifa.ValoresMatriz != null)
                        {
                            tarifa.ValoresMatriz.ForEach(s =>
                            {
                                valoresMatriz.Add(new DTOTarifaReglaValor
                                {
                                    Id = s.CALRV_ID,
                                    IdVal_1 = s.TEMPS1_ID,
                                    Descripcion_1 = s.SECC1_DESC,
                                    Desde_1 = s.CRI1_FROM,
                                    Hasta_1 = s.CRI1_TO,

                                    IdVal_2 = s.TEMPS2_ID,
                                    Descripcion_2 = s.SECC2_DESC,
                                    Desde_2 = s.CRI2_FROM,
                                    Hasta_2 = s.CRI2_TO,

                                    IdVal_3 = s.TEMPS3_ID,
                                    Descripcion_3 = s.SECC3_DESC,
                                    Desde_3 = s.CRI3_FROM,
                                    Hasta_3 = s.CRI3_TO,

                                    IdVal_4 = s.TEMPS4_ID,
                                    Descripcion_4 = s.SECC4_DESC,
                                    Desde_4 = s.CRI4_FROM,
                                    Hasta_4 = s.CRI4_TO,

                                    IdVal_5 = s.TEMPS5_ID,
                                    Descripcion_5 = s.SECC5_DESC,
                                    Desde_5 = s.CRI5_FROM,
                                    Hasta_5 = s.CRI5_TO,

                                    EnBD = true
                                });
                            });
                            ValoresMatrizTmp = valoresMatriz;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tarifa, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
                else
                {
                    retorno.result = 1;
                    CaracteristicasTmp = null;
                    ValoresTmp = null;
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
        public JsonResult ObtenerValorePopup()
        {
            Resultado retorno = new Resultado();
            try
            {
                List<DTOTarifaReglaData> listaValores = new List<DTOTarifaReglaData>();
                if (CaracteristicasTmp != null)
                {
                    int contador = 0;
                    DTOTarifaReglaData ent = null;
                    foreach (var item in CaracteristicasTmp)
                    {
                        if (item.Activo)
                        {
                            contador += 1;
                            ent = new DTOTarifaReglaData();
                            ent.Id= item.Id;
                            ent.Letra=GenerarLetraCaracteristica(contador);
                            listaValores.Add(ent);
                        }
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult ObtenerMatrizValor(List<BETarifaReglaValor> ReglaValor)
        {
            Session.Remove(K_SESION_BE_OBTENER_MATRIZ_TABLA);
            List<BETarifaReglaValor> ListaMatriz = new List<BETarifaReglaValor>();
            BETarifaReglaValor entidad = null;
            ValoresMatrizTmp.ForEach(s =>
            {
                entidad = new BETarifaReglaValor();
                entidad = ReglaValor.Where(p => p.CALRV_ID == s.Id).FirstOrDefault();

                entidad.TEMPS1_ID = s.IdVal_1;
                //entidad.SECC1_DESC = s.Descripcion_1;
                entidad.CRI1_FROM = s.Desde_1;
                entidad.CRI1_TO = s.Hasta_1;

                entidad.TEMPS2_ID = s.IdVal_2;
                //entidad.SECC2_DESC = s.Descripcion_2;
                entidad.CRI2_FROM = s.Desde_2;
                entidad.CRI2_TO = s.Hasta_2;

                entidad.TEMPS3_ID = s.IdVal_3;
                //entidad.SECC3_DESC = s.Descripcion_3;
                entidad.CRI3_FROM = s.Desde_3;
                entidad.CRI3_TO = s.Hasta_3;

                entidad.TEMPS4_ID = s.IdVal_4;
                //entidad.SECC4_DESC = s.Descripcion_4;
                entidad.CRI4_FROM = s.Desde_4;
                entidad.CRI4_TO = s.Hasta_4;

                entidad.TEMPS5_ID = s.IdVal_5;
                //entidad.SECC5_DESC = s.Descripcion_5;
                entidad.CRI5_FROM = s.Desde_5;
                entidad.CRI5_TO = s.Hasta_5;

                ListaMatriz.Add(entidad);
            });
            BEObtenerMatrizTabla = ListaMatriz;

            Resultado retorno = new Resultado();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var servicio = new BLTarifaRegla();
                var regla = new BETarifaRegla();

                regla.OWNER = GlobalVars.Global.OWNER;
                regla.CALR_ID = id;
                regla.LOG_USER_UPDATE = UsuarioActual;
                servicio.Eliminar(regla);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //***********************************************************************************
        //[HttpPost]
        //public JsonResult PassThings(List<Thing> things)
        //{
        //    var t = things;
        //    Resultado retorno = new Resultado();
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public class Thing
        //{
        //    public int Id { get; set; }
        //    public string Color { get; set; }
        //}

    }
}
