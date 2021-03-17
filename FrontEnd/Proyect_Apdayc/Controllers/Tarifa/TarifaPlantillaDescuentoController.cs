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

namespace Proyect_Apdayc.Controllers.Tarifa
{
    public class TarifaPlantillaDescuentoController : Base
    {
        //
        // GET: /TarifaPlantillaDescuento/
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_VARIABLE_TARIFA = "___DTOVariableTarifa_Dsc";
        private const string K_SESION_VARIABLE_TARIFA_DEL = "___DTOariableTarifaDEL_Dsc";
        private const string K_SESION_VARIABLE_TARIFA_ACT = "___DTOariableTarifaACT_Dsc";
        private const string K_SESION_VALOR_TARIFA = "___DTOValorTarifa_Dsc";
        private const string K_SESION_VALOR_TARIFA_DEL = "___DTOValorTarifaDEL_Dsc";
        private const string K_SESION_VALOR_TARIFA_ACT = "___DTOValorTarifaACT_Dsc";
        private const string K_SESION_MATRIZ_DSC = "___DTOMatriz_Dsc";


        List<DTOTarifaCaracteristica> caracteristicas = new List<DTOTarifaCaracteristica>();
        List<DTOTarifaValor> valores = new List<DTOTarifaValor>();
        List<DTOPlantillaDescuentoMatriz> valoresMatriz = new List<DTOPlantillaDescuentoMatriz>();

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_VARIABLE_TARIFA);
            Session.Remove(K_SESION_VARIABLE_TARIFA_DEL);
            Session.Remove(K_SESION_VARIABLE_TARIFA_ACT);
            Session.Remove(K_SESION_VALOR_TARIFA);
            Session.Remove(K_SESION_VALOR_TARIFA_DEL);
            Session.Remove(K_SESION_VALOR_TARIFA_ACT);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string desc, decimal nro, string fini, string ffin, int estado, int confecha)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, desc, nro, Convert.ToDateTime(fini), Convert.ToDateTime(ffin), estado, confecha, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETarifaPlantillaDescuento { ListarTarifaPlantillaDsc = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETarifaPlantillaDescuento { ListarTarifaPlantillaDsc = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETarifaPlantillaDescuento> BLListar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int confecha, int pagina, int cantRegxPag)
        {
            return new BLTarifaPlantillaDescuento().Listar(owner, desc, nro, fini, ffin, estado, confecha, pagina, cantRegxPag);
        }

        #region CARACTERISTICAS

        public List<DTOTarifaCaracteristica> CaracteristicasTmp
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA] = value;
            }
        }

        private List<DTOTarifaCaracteristica> CaracteristicasTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA_ACT];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA_ACT] = value;
            }
        }

        private List<DTOTarifaCaracteristica> CaracteristicasTmpDelBD
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA_DEL];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOTarifaCaracteristica entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                decimal sequencia = 0;
                int registroNuevo = 0;
                int registroModificar = 0;
                caracteristicas = CaracteristicasTmp;
                if (caracteristicas != null)
                {
                    if (entidad.Id == 0) // Nuevo
                    {
                        registroNuevo = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica).Count();
                    }
                    else
                    {
                        registroModificar = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && p.Id != entidad.Id).Count();
                    }
                }


                if ((entidad.Id == 0 && registroNuevo == 0)
                     || (entidad.Id != 0 && registroModificar == 0)
                   )
                {

                    if (caracteristicas == null) caracteristicas = new List<DTOTarifaCaracteristica>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (caracteristicas.Count > 0) nuevoId = caracteristicas.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
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

                    foreach (var item in CaracteristicasTmp.OrderBy(x => x.Id))
                    {
                        sequencia += 1;
                        item.Sequencia = sequencia;
                    }
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
        public JsonResult DellAddCaracteristica(decimal id)
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

                            if (CaracteristicasTmpUPDEstado == null) CaracteristicasTmpUPDEstado = new List<DTOTarifaCaracteristica>();
                            if (CaracteristicasTmpDelBD == null) CaracteristicasTmpDelBD = new List<DTOTarifaCaracteristica>();

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
                            //caracteristicas.Add(objDel);
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

        private List<BETarifaPlantillaDescuentoCaracteristica> obtenerCaracteristicas()
        {
            List<BETarifaPlantillaDescuentoCaracteristica> datos = new List<BETarifaPlantillaDescuentoCaracteristica>();
            if (CaracteristicasTmp != null)
            {
                CaracteristicasTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaDescuentoCaracteristica
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMP_ID_DSC_CHAR = Convert.ToInt32(x.Id),
                        CHAR_ID = x.IdCaracteristica,
                        SECC_CHARSEQ = x.Sequencia,
                        IND_TR = Convert.ToDecimal(x.Tramo),
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

        public JsonResult ListarCaracteristica(decimal idCaracteristica = 0)
        {
            caracteristicas = CaracteristicasTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget' id='tblCaracteristica'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='k-header'></th>");
                shtml.Append("<th class='k-header'>Id</th>");
                shtml.Append("<th class='k-header'>Caracteristica</th>");
                shtml.Append("<th class='k-header'>Tramo</th>");
                shtml.Append("<th class='k-header'>Usuario Creación</th>");
                shtml.Append("<th class='k-header'>Fecha Creación</th>");
                shtml.Append("<th  class='k-header' style='width:60px'></th></tr></thead>");

                if (caracteristicas != null)
                {
                    foreach (var item in caracteristicas.OrderBy(x => x.Id))
                    {
                        if (item.Activo)
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td style='width:25px'> ");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.Id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Id);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td nowrap>{0}</td>", item.DesCaracteristica);
                            if (item.Tramo == 1)
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED checked  ></td>");
                            else
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED></td>");

                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);

                            shtml.AppendFormat("<td style='width:80px'>");
                            shtml.AppendFormat("                        <a href=# onclick='nuevoValor({0},{2});'><img src='../Images/botones/nuevo.png' width=20px    border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Agregar valor.", item.Tramo);
                            shtml.AppendFormat("                        <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar caracteristica.");
                            shtml.AppendFormat("                        <a href=# onclick='delAddCaracteristica({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar caracteristica." : "Activar caracteristica.");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='6'>");

                            if (item.Id == idCaracteristica)
                                shtml.Append("<div id='" + "div" + item.Id.ToString() + "'  > ");
                            else
                                shtml.Append("<div style='display:none;' id='" + "div" + item.Id.ToString() + "'  > ");
                            shtml.Append(getHtmlTableDetaCaracteristica(item.Id));

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
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableDetaCaracteristica(decimal id)
        {
            var carac = CaracteristicasTmp.Where(p => p.Id == id).FirstOrDefault();
            valores = ValoresTmp;
            if (valores != null && valores.Count > 0)
                valores = ValoresTmp.Where(p => p.IdCaracteristica == id).ToList();
            StringBuilder shtml = new StringBuilder();

            shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<td colspan=10 class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Valores de la caracteristica</td>");
            shtml.Append("</tr>");

            shtml.Append("<tr>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Id</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none'>IdCaracteristica</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Valor</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tramo</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Desde</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Hasta</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Usuario Creación</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Fecha Creación</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:60px'></th></tr></thead>");

            if (valores != null && valores.Count > 0)
            {
                foreach (var item in valores.OrderBy(x => x.Id))
                {
                    if (item.Activo)
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td >{0}</td>", item.Id);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdCaracteristica);
                        shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion);
                        shtml.AppendFormat("<td >{0}</td>", carac.Tramo == 0 ? "NO" : "SI");
                        shtml.AppendFormat("<td >{0}</td>", item.Desde.ToString("### ##0.0000"));

                        shtml.AppendFormat("<td >{0}</td>", (carac.Tramo == 0 || item.Hasta == 0) ? "" : item.Hasta.ToString("### ##0.0000"));
                        shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                        shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);

                        shtml.AppendFormat("<td style='width:60px; text-align:right'> <a href=# onclick='updAddValor({0},{2});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Variable", carac.Tramo);
                        shtml.AppendFormat("                        <a href=# onclick='delAddValor({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Variable" : "Activar Variable");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
            }
            shtml.Append("</table>");
            return shtml;
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

        [HttpPost]
        public JsonResult AddValor(DTOTarifaValor entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<DTOTarifaValor> listaValidacion = null;
                valores = ValoresTmp;
                if (valores != null)
                {

                    var car = CaracteristicasTmp.Where(p => p.Id == entidad.IdCaracteristica).FirstOrDefault();
                    var valoresCaracteristica = valores.Where(p => p.IdCaracteristica == entidad.IdCaracteristica).ToList();

                    if (car.Tramo == 0)
                    {
                        listaValidacion = valoresCaracteristica.Where(p => p.Id != entidad.Id && p.Activo == true &&
                                                                           p.IdCaracteristica == entidad.IdCaracteristica &&
                                                                           p.Desde == entidad.Desde).ToList();
                    }
                    else if (car.Tramo == 1)
                    {
                        listaValidacion = valoresCaracteristica.Where(p => p.Id != entidad.Id && p.Activo == true &&
                                                                           p.IdCaracteristica == entidad.IdCaracteristica &&
                                                                           (p.Desde == entidad.Desde || p.Hasta == entidad.Hasta || p.Desde == entidad.Hasta || p.Hasta == entidad.Desde)
                                                                           ).ToList();
                    }

                    //listaValidacion = valoresCaracteristica.Where(p => p.IdCaracteristica == entidad.IdCaracteristica &&
                    //                                    p.Id != entidad.Id &&
                    //                                    p.Activo == true &&
                    //    (car.Tramo == 0 && (p.Desde == entidad.Desde)) ||
                    //    (car.Tramo == 1 && (p.Desde == entidad.Desde || p.Hasta == entidad.Hasta) )
                    //    ).ToList();

                    //**************************************************************
                    //var car = CaracteristicasTmp.Where(p => p.Id == entidad.IdCaracteristica).FirstOrDefault();
                    //var valoresCaracteristica = valores.Where(p => p.IdCaracteristica == entidad.IdCaracteristica).ToList();

                    //listaValidacion = valoresCaracteristica.Where(p => p.IdCaracteristica == entidad.IdCaracteristica &&
                    //                                    p.Id != entidad.Id &&
                    //                                    p.Activo == true &&
                    //    (car.Tramo == 0 && (p.Desde == entidad.Desde)) ||
                    //    (car.Tramo == 1 && (p.Desde == entidad.Desde || p.Hasta == entidad.Hasta))
                    //    ).ToList();


                }

                if (listaValidacion == null || listaValidacion.Count == 0)
                {
                    if (valores == null) valores = new List<DTOTarifaValor>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (valores.Count > 0) nuevoId = valores.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        valores.Add(entidad);
                    }
                    else
                    {
                        var item = valores.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        valores.Remove(item);
                        valores.Add(entidad);
                    }
                    ValoresTmp = valores;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "Los datos ingresados ya existen, ingrese otro valor.";
                }

            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddValor(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                valores = ValoresTmp;
                if (valores != null)
                {
                    var objDel = valores.Where(x => x.Id == id).FirstOrDefault();

                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (ValoresTmpUPDEstado == null) ValoresTmpUPDEstado = new List<DTOTarifaValor>();
                            if (ValoresTmpDelBD == null) ValoresTmpDelBD = new List<DTOTarifaValor>();

                            var itemUpd = ValoresTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = ValoresTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) ValoresTmpUPDEstado.Add(objDel);
                                if (itemDel != null) ValoresTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) ValoresTmpDelBD.Add(objDel);
                                if (itemUpd != null) ValoresTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            valores.Remove(objDel);
                            //valores.Add(objDel);
                        }
                        else
                        {
                            valores.Remove(objDel);
                        }
                        ValoresTmp = valores;
                        retorno.result = 1;
                        retorno.message = "OK";
                        retorno.data = Json(objDel, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BETarifaPlantillaDescuentoSeccion> obtenerSeccion()
        {
            List<BETarifaPlantillaDescuentoSeccion> datos = new List<BETarifaPlantillaDescuentoSeccion>();
            if (ValoresTmp != null)
            {
                ValoresTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaDescuentoSeccion
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMP_ID_DSC_VAL = Convert.ToInt32(x.Id),
                        TEMP_ID_DSC_CHAR = Convert.ToInt32(x.IdCaracteristica),
                        SECC_DESC = x.Descripcion,
                        SECC_FROM = x.Desde,
                        SECC_TO = x.Hasta,
                        LOG_USER_CREAT = UsuarioActual,

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

        #endregion

        #region MATRIZ
        public List<DTOPlantillaDescuentoMatriz> MatrizDscTmp
        {
            get
            {
                return (List<DTOPlantillaDescuentoMatriz>)Session[K_SESION_MATRIZ_DSC];
            }
            set
            {
                Session[K_SESION_MATRIZ_DSC] = value;
            }
        }

        [HttpPost]
        public JsonResult GenerarMatriz()
        {
            Resultado retorno = new Resultado();
            try
            {
                //Generar Matriz
                BETarifaPlantillaDescuentoSeccion RegMatriz = null;
                List<BETarifaPlantillaDescuentoSeccion> ListaMatriz = new List<BETarifaPlantillaDescuentoSeccion>();
                int contador = 0;
                foreach (var itemChar in CaracteristicasTmp.Where(x => x.Activo))
                {
                    contador += 1;
                    foreach (var itemSec in ValoresTmp.Where(x => x.IdCaracteristica == itemChar.Id && x.Activo))
                    {
                        RegMatriz = new BETarifaPlantillaDescuentoSeccion();
                        RegMatriz.TEMP_ID_DSC_VAL = itemSec.Id;
                        RegMatriz.TEMP_ID_DSC_CHAR = itemChar.Id;
                        RegMatriz.CHAR_ID = itemChar.IdCaracteristica;
                        RegMatriz.SECC_DESC = itemSec.Descripcion;
                        RegMatriz.SECC_CHARSEQ = itemChar.Sequencia;
                        ListaMatriz.Add(RegMatriz);
                    }
                }

                //Agregar a la variable temporal
                List<BETarifaPlantillaDescuentoValores> MatrizGenerada = new List<BETarifaPlantillaDescuentoValores>();
                MatrizGenerada = new BLTarifaPlantillaDescuento().GenerarMatriz(ListaMatriz, contador);
                valoresMatriz = new List<DTOPlantillaDescuentoMatriz>();
                decimal correlativo = 0;
                foreach (var item in MatrizGenerada)
                {
                    DTOPlantillaDescuentoMatriz registroM = new DTOPlantillaDescuentoMatriz();
                    correlativo += 1;
                    registroM.Id = correlativo;
                    registroM.ValId_1 = item.TEMP_ID_DSC_VAL_1 != 0 ? item.TEMP_ID_DSC_VAL_1 : 0;
                    registroM.Val_Desc1 = item.TEMP_SEC_DESC_1 != null ? item.TEMP_SEC_DESC_1 : null;
                    registroM.ValId_2 = item.TEMP_ID_DSC_VAL_2 != 0 ? item.TEMP_ID_DSC_VAL_2 : 0;
                    registroM.Val_Desc2 = item.TEMP_SEC_DESC_2 != null ? item.TEMP_SEC_DESC_2 : null;
                    registroM.ValId_3 = item.TEMP_ID_DSC_VAL_3 != 0 ? item.TEMP_ID_DSC_VAL_3 : 0;
                    registroM.Val_Desc3 = item.TEMP_SEC_DESC_3 != null ? item.TEMP_SEC_DESC_3 : null;
                    valoresMatriz.Add(registroM);
                }

                MatrizDscTmp = valoresMatriz;
                retorno.Code = contador;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "GenerarMatriz", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    retorno.result = 0;
            //    retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
            //    ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            //}
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarValorMatriz(int Cantidad)
        {
            valoresMatriz = MatrizDscTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblMatriz'>");
                shtml.Append("<thead>");
                shtml.Append("<tr>");
                shtml.Append("<th class='k-header' style='width:50px'>Id</th>");
                if (Cantidad >= 1)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_1</th>");
                    shtml.Append("<th class='k-header'>Descripción - Valor 1</th>");
                }
                if (Cantidad >= 2)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_2</th>");
                    shtml.Append("<th class='k-header'>Descripción  Valor 2</th>");
                }
                if (Cantidad >= 3)
                {
                    shtml.Append("<th class='k-header' style='display:none'>IdVal_3</th>");
                    shtml.Append("<th class='k-header'>Descripción - Valor 3</th>");
                }

                shtml.Append("<th class='k-header'> Formula </th>");
                shtml.Append("</tr>");

                if (valoresMatriz != null && valoresMatriz.Count > 0)
                {
                    foreach (var item in valoresMatriz)
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:50px' class='tmpIDMatriz'>{0}</td>", item.Id);
                        if (Cantidad >= 1)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.ValId_1);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Val_Desc1);
                        }

                        if (Cantidad >= 2)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.ValId_2);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Val_Desc2);
                        }

                        if (Cantidad >= 3)
                        {
                            shtml.AppendFormat("<td style='display:none' style='width:250px'>{0}</td>", item.ValId_3);
                            shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Val_Desc3);
                        }
                        shtml.AppendFormat("<td style='width:250px; text-align:center '> <input class='requerido' id='txtMatrizFormula" + item.Id + "' type='text' value='" + item.Formula + "' style='width:120px' maxlength='16' /> </td>");

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
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarValorMatriz", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerMatrizValor(List<DTOPlantillaDescuentoMatriz> ReglaValor)
        {
            foreach (var item in MatrizDscTmp)
            {
                item.Formula = ReglaValor.Where(p => p.Id == item.Id).FirstOrDefault().Formula;
            }
            Resultado retorno = new Resultado();
            retorno.result = 1;
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        private List<BETarifaPlantillaDescuentoValores> obtenerValores()
        {
            List<BETarifaPlantillaDescuentoValores> datos = new List<BETarifaPlantillaDescuentoValores>();
            if (MatrizDscTmp != null)
            {
                MatrizDscTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaDescuentoValores
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMP_ID_DSC_MAT = Convert.ToInt32(x.Id),
                        TEMP_ID_DSC_VAL_1 = x.ValId_1,
                        TEMP_SEC_DESC_1 = x.Val_Desc1,
                        TEMP_ID_DSC_VAL_2 = x.ValId_2,
                        TEMP_SEC_DESC_2 = x.Val_Desc2,
                        TEMP_ID_DSC_VAL_3 = x.ValId_3,
                        TEMP_SEC_DESC_3 = x.Val_Desc3,
                        VAL_FORMULA = x.Formula,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        #endregion

        //*************************************************************
        [HttpPost]
        public JsonResult Insertar(BETarifaPlantillaDescuento plantilla)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    plantilla.OWNER = GlobalVars.Global.OWNER;
                    plantilla.LstDscCaracteristica = obtenerCaracteristicas();
                    plantilla.LstDscSeccion = obtenerSeccion();
                    plantilla.LstDscValores = obtenerValores(); //Datos de la matriz

                    if (plantilla.TEMP_ID_DSC == 0)
                    {
                        plantilla.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLTarifaPlantillaDescuento().Insertar(plantilla);
                    }
                    else
                    {
                        plantilla.LOG_USER_UPDATE = UsuarioActual;

                        List<BETarifaPlantillaDescuentoCaracteristica> listaCaracteristicaDel = null;
                        if (CaracteristicasTmpDelBD != null)
                        {
                            listaCaracteristicaDel = new List<BETarifaPlantillaDescuentoCaracteristica>();
                            CaracteristicasTmpDelBD.ForEach(x => { listaCaracteristicaDel.Add(new BETarifaPlantillaDescuentoCaracteristica { TEMP_ID_DSC_CHAR = x.Id }); });
                        }

                        List<BETarifaPlantillaDescuentoSeccion> listaValorDel = null;
                        if (ValoresTmpDelBD != null)
                        {
                            listaValorDel = new List<BETarifaPlantillaDescuentoSeccion>();
                            ValoresTmpDelBD.ForEach(x => { listaValorDel.Add(new BETarifaPlantillaDescuentoSeccion { TEMP_ID_DSC_VAL = x.Id, TEMP_ID_DSC_CHAR = x.IdCaracteristica }); });
                        }

                        var datos = new BLTarifaPlantillaDescuento().Actualizar(plantilla, listaCaracteristicaDel, listaValorDel);
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
                BETarifaPlantillaDescuento tarifa = new BETarifaPlantillaDescuento();
                tarifa = new BLTarifaPlantillaDescuento().Obtener(GlobalVars.Global.OWNER, id);

                if (tarifa != null)
                {
                    caracteristicas = new List<DTOTarifaCaracteristica>();
                    if (tarifa.LstDscCaracteristica != null)
                    {
                        tarifa.LstDscCaracteristica.ForEach(s =>
                        {
                            caracteristicas.Add(new DTOTarifaCaracteristica
                            {
                                Id = s.TEMP_ID_DSC_CHAR,
                                IdCaracteristica = s.CHAR_ID,
                                DesCaracteristica = s.CHAR_SHORT,
                                Tramo = Convert.ToDecimal(s.IND_TR),
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                Sequencia = s.SECC_CHARSEQ,
                            });
                        });
                        CaracteristicasTmp = caracteristicas;
                    }

                    valores = new List<DTOTarifaValor>();
                    if (tarifa.LstDscSeccion != null)
                    {
                        tarifa.LstDscSeccion.ForEach(s =>
                        {
                            valores.Add(new DTOTarifaValor
                            {
                                Id = s.TEMP_ID_DSC_VAL,
                                IdCaracteristica = s.TEMP_ID_DSC_CHAR, // TEMP_ID_DSC
                                Descripcion = s.SECC_DESC,
                                Desde = s.SECC_FROM,
                                Hasta = s.SECC_TO,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                SequenceChar = s.SECC_CHARSEQ
                            });
                        });
                        ValoresTmp = valores;
                    }



                    valoresMatriz = new List<DTOPlantillaDescuentoMatriz>();
                    if (tarifa.LstDscValores != null)
                    {
                        tarifa.LstDscValores.ForEach(s =>
                        {
                            valoresMatriz.Add(new DTOPlantillaDescuentoMatriz
                            {
                                Id = s.TEMP_ID_DSC_MAT,
                                ValId_1 = s.TEMP_ID_DSC_VAL_1 != 0 ? s.TEMP_ID_DSC_VAL_1 : 0,
                                Val_Desc1 = s.TEMP_SEC_DESC_1 != null ? s.TEMP_SEC_DESC_1 : null,
                                ValId_2 = s.TEMP_ID_DSC_VAL_2 != 0 ? s.TEMP_ID_DSC_VAL_2 : 0,
                                Val_Desc2 = s.TEMP_SEC_DESC_2 != null ? s.TEMP_SEC_DESC_2 : null,
                                ValId_3 = s.TEMP_ID_DSC_VAL_3 != 0 ? s.TEMP_ID_DSC_VAL_3 : 0,
                                Val_Desc3 = s.TEMP_SEC_DESC_3 != null ? s.TEMP_SEC_DESC_3 : null,
                                Formula = s.VAL_FORMULA,
                                EnBD = true,
                                //Activo = s.ENDS.HasValue ? false : true,
                            });
                        });
                        MatrizDscTmp = valoresMatriz;
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
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BETarifaPlantilla plantilla)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTarifaPlantilla servicio = new BLTarifaPlantilla();
                plantilla.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(plantilla);
                if (resultado >= 1)
                    retorno.Code = 1;
                else
                    retorno.Code = 0;

                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
