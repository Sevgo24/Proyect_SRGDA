using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionTotalesController : Base
    {
        //
        // GET: /ComisionTotales/
        public const string NomAplicacion = "SRGDA";

        private const string K_SESION_REPRESENTANTE = "___DTORepresentante";
        private const string K_SESION_REPRESENTANTE_DEL = "___DTORepresentanteDEL";
        private const string K_SESION_REPRESENTANTE_ACT = "___DTORepresentanteACT";

        private const string K_SESION_RANGO = "___DTORango";
        private const string K_SESION_RANGO_DEL = "___DTORangoDEL";
        private const string K_SESION_RANGO_ACT = "___DTORangoACT";

        List<DTORepresentante> representantes = new List<DTORepresentante>();
        List<DTORangorecaudador> rangos = new List<DTORangorecaudador>();

        private List<DTORepresentante> RepresentanteTmpUPDEstado
        {
            get
            {
                return (List<DTORepresentante>)Session[K_SESION_REPRESENTANTE_ACT];
            }
            set
            {
                Session[K_SESION_REPRESENTANTE_ACT] = value;
            }
        }
        private List<DTORepresentante> RepresentanteTmpDelBD
        {
            get
            {
                return (List<DTORepresentante>)Session[K_SESION_REPRESENTANTE_DEL];
            }
            set
            {
                Session[K_SESION_REPRESENTANTE_DEL] = value;
            }
        }
        public List<DTORepresentante> RepresentanteTmp
        {
            get
            {
                return (List<DTORepresentante>)Session[K_SESION_REPRESENTANTE];
            }
            set
            {
                Session[K_SESION_REPRESENTANTE] = value;
            }
        }

        private List<DTORangorecaudador> RangoTmpUPDEstado
        {
            get
            {
                return (List<DTORangorecaudador>)Session[K_SESION_RANGO_ACT];
            }
            set
            {
                Session[K_SESION_RANGO_ACT] = value;
            }
        }
        private List<DTORangorecaudador> RangoTmpDelBD
        {
            get
            {
                return (List<DTORangorecaudador>)Session[K_SESION_RANGO_DEL];
            }
            set
            {
                Session[K_SESION_RANGO_DEL] = value;
            }
        }
        public List<DTORangorecaudador> RangoTmp
        {
            get
            {
                return (List<DTORangorecaudador>)Session[K_SESION_RANGO];
            }
            set
            {
                Session[K_SESION_RANGO] = value;
            }
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_REPRESENTANTE);
            Session.Remove(K_SESION_REPRESENTANTE_DEL);
            Session.Remove(K_SESION_REPRESENTANTE_ACT);
            Session.Remove(K_SESION_RANGO);
            Session.Remove(K_SESION_RANGO_DEL);
            Session.Remove(K_SESION_RANGO_ACT);
            return View();
        }

        public JsonResult ListaComisionesTotales(int skip, int take, int page, int pageSize, decimal ProgramaId, string Ultfecha, decimal IdRepresentante, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, ProgramaId, Convert.ToDateTime(Ultfecha), IdRepresentante, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEComisionTotales { ListaComisionTotales = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEComisionTotales { ListaComisionTotales = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEComisionTotales> Lista(string Owner, decimal ProgramaId, DateTime Ultfecha, decimal IdRepresentante, int st, int pagina, int cantRegxPag)
        {
            return new BLComisionTotales().ListarPage(Owner, ProgramaId, Ultfecha, IdRepresentante, st, pagina, cantRegxPag);
        }

        [HttpPost()]
        public JsonResult ValidacionPerfilAgenteRecaudo(decimal idAsociado)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSocioNegocio servicio = new BLSocioNegocio();
                var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
                if (datos != null)
                {
                    if (datos.BPS_COLLECTOR.ToString() != "1")
                    {
                        retorno.result = 0;
                        retorno.message = "EL perfil del asociado no es el de Agente de Recaudo.";
                    }
                    else
                        retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ValidacionPerfil", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddRepresentante(DTORepresentante entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    representantes = RepresentanteTmp;
                    if (representantes == null) representantes = new List<DTORepresentante>();

                    if (Convert.ToInt32(entidad.sequence) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (representantes.Count > 0) nuevoId = representantes.Max(x => x.sequence) + 1;
                        entidad.sequence = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        representantes.Add(entidad);
                    }
                    else
                    {
                        var item = representantes.Where(x => x.sequence == entidad.sequence).FirstOrDefault();
                        entidad.EnBD = item.EnBD;
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;


                        foreach (var en in RangoTmp)
                        {
                            if (item.Id == en.IdRepresentante)
                                en.IdRepresentante = entidad.Id;
                        }


                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }
                        representantes.Remove(item);
                        representantes.Add(entidad);
                    }

                    RepresentanteTmp = representantes;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddRepresentante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddRango(DTORangorecaudador entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    rangos = RangoTmp;
                    if (rangos == null) rangos = new List<DTORangorecaudador>();

                    if (Convert.ToInt32(entidad.sequence) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (rangos.Count > 0) nuevoId = rangos.Max(x => x.sequence) + 1;
                        entidad.sequence = nuevoId;

                        if (entidad.FormatoComision == "P")
                        {
                            entidad.ValorComisionAdicional = 0;
                            entidad.FormatoComision = "%";
                        }
                        else if (entidad.FormatoComision == "M")
                        {
                            entidad.PorcentajeAdicional = 0;
                            entidad.FormatoComision = "S/.";
                        }

                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        rangos.Add(entidad);
                    }
                    else
                    {
                        var item = rangos.Where(x => x.sequence == entidad.sequence).FirstOrDefault();

                        if (entidad.FormatoComision == "P")
                        {
                            entidad.ValorComisionAdicional = 0;
                            entidad.FormatoComision = "%";
                        }
                        else if (entidad.FormatoComision == "M")
                        {
                            entidad.PorcentajeAdicional = 0;
                            entidad.FormatoComision = "S/.";
                        }

                        entidad.EnBD = item.EnBD;
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }
                        rangos.Remove(item);
                        rangos.Add(entidad);
                    }

                    RangoTmp = rangos;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddRepresentante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddRepresentante(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    representantes = RepresentanteTmp;
                    if (representantes != null)
                    {
                        var objDel = representantes.Where(x => x.sequence == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (RepresentanteTmpUPDEstado == null) RepresentanteTmpUPDEstado = new List<DTORepresentante>();
                                if (RepresentanteTmpDelBD == null) RepresentanteTmpDelBD = new List<DTORepresentante>();

                                var itemUpd = RepresentanteTmpUPDEstado.Where(x => x.sequence == id).FirstOrDefault();
                                var itemDel = RepresentanteTmpDelBD.Where(x => x.sequence == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) RepresentanteTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) RepresentanteTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) RepresentanteTmpDelBD.Add(objDel);
                                    if (itemUpd != null) RepresentanteTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                representantes.Remove(objDel);
                                representantes.Add(objDel);
                            }
                            else
                            {
                                representantes.Remove(objDel);
                            }
                            RepresentanteTmp = representantes;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddRepresentante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddRango(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    rangos = RangoTmp;
                    if (rangos != null)
                    {
                        var objDel = rangos.Where(x => x.sequence == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (RangoTmpUPDEstado == null) RangoTmpUPDEstado = new List<DTORangorecaudador>();
                                if (RangoTmpDelBD == null) RangoTmpDelBD = new List<DTORangorecaudador>();

                                var itemUpd = RangoTmpUPDEstado.Where(x => x.sequence == id).FirstOrDefault();
                                var itemDel = RangoTmpDelBD.Where(x => x.sequence == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) RangoTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) RangoTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) RangoTmpDelBD.Add(objDel);
                                    if (itemUpd != null) RangoTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                rangos.Remove(objDel);
                                rangos.Add(objDel);
                            }
                            else
                            {
                                rangos.Remove(objDel);
                            }
                            RangoTmp = rangos;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddRango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarRepresentante(decimal idCab = 0)
        {
            representantes = RepresentanteTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='tblComisionTotal' class='k-grid k-widget' id='tblComisionTotal'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'></th>");
                    shtml.Append("<th class='k-header' style='display:none;'>Sequence</th>");
                    shtml.Append("<th class='k-header' style='display:none;'>Id</th>");
                    shtml.Append("<th class='k-header'>Representante</th>");
                    shtml.Append("<th class='k-header'>Fecha Ingreso</th>");
                    shtml.Append("<th class='k-header'>Fecha Fin</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (representantes != null)
                    {
                        foreach (var item in representantes.OrderBy(x => x.sequence))
                        {
                            shtml.Append("<tr class='k-grid-content'>");

                            shtml.AppendFormat("<td style='width:25px'> ");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.sequence + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.sequence);
                            shtml.Append("</td>");

                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.sequence);
                            shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.Representante);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaInicio);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaFin);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);

                            shtml.AppendFormat("<td> <a href=# onclick='nuevoRango({0},{2});'><img src='../Images/botones/nuevo.png' width=20px    border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Agregar valor.", item.Id);
                            shtml.AppendFormat(" <a href=# onclick='updAddRepresentante({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.sequence);
                            shtml.AppendFormat("<a href=# onclick='delAddRepresentante({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.sequence, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Representante" : "Activar Representante");

                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='16'>");

                            //shtml.Append("<div id='" + "div" + item.sequence.ToString() + "'  > ");

                            if (item.sequence == idCab)
                                shtml.Append("<div id='" + "div" + item.sequence.ToString() + "'  > ");
                            else
                                shtml.Append("<div style='display:none;' id='" + "div" + item.sequence.ToString() + "'  > ");


                            shtml.Append(getHtmlTableDetaRango(item.sequence, item.Id));

                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarRepresentante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableDetaRango(decimal id, decimal IdRepresentante)
        {
            var rang = RangoTmp.Where(p => p.sequence == id).FirstOrDefault();
            rangos = RangoTmp;

            //if (rangos != null && rangos.Count > 0)
            //    rangos = RangoTmp.Where(p => p.sequence == id).ToList();
            //StringBuilder shtml = new StringBuilder();

            if (rangos != null && rangos.Count > 0)
                rangos = RangoTmp.Where(p => p.IdRepresentante == IdRepresentante).ToList();
            StringBuilder shtml = new StringBuilder();

            shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<td colspan=10 class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Rangos de representantes</td>");
            shtml.Append("</tr>");

            shtml.Append("<tr>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;'>Sequence</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;'>IdPrograma</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;'>BpsId</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Orden</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Valor desde</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Valor hasta</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Formato</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Comisión adicional</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Valor adicional</th>");


            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Usu. Crea</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Fecha Crea</th>");

            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Usu. Modi</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Fecha Modi</th>");

            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:60px'></th></tr></thead>");

            if (rangos != null && rangos.Count > 0)
            {
                foreach (var item in rangos.OrderBy(x => x.sequence))
                {
                    item.IdRepresentante = IdRepresentante;

                    shtml.Append("<tr class='k-grid-content'>");
                    shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.sequence);
                    shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdPrograma);
                    shtml.AppendFormat("<td style='display:none;'>{0}</td>", item.IdRepresentante);
                    shtml.AppendFormat("<td >{0}</td>", item.Orden);
                    shtml.AppendFormat("<td >{0}</td>", item.ValorInicial);
                    shtml.AppendFormat("<td >{0}</td>", item.ValorFinal);
                    shtml.AppendFormat("<td >{0}</td>", item.FormatoComision);
                    shtml.AppendFormat("<td >{0}</td>", item.ValorComisionAdicional);
                    shtml.AppendFormat("<td >{0}</td>", item.PorcentajeAdicional);
                    shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                    shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                    shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                    shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                    shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddRango({0},{2},{3});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.sequence, "Modificar Rango", item.IdRepresentante, item.IdRepresentante);
                    shtml.AppendFormat("<a href=# onclick='delAddRango({0});'><img src='../Images/iconos/{1}'title='{2}' border=0></a>", item.sequence, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Rango" : "Activar Rango");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public JsonResult ObtieneRepresentanteTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = RepresentanteTmp.Where(x => x.sequence == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneRepresentanteTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneRangoTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = RangoTmp.Where(x => x.sequence == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneRepresentanteTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEComisionRepresentantes> obtenerRepresentantes()
        {
            List<BEComisionRepresentantes> datos = new List<BEComisionRepresentantes>();

            if (RepresentanteTmp != null)
            {
                RepresentanteTmp.ForEach(x =>
                {
                    datos.Add(new BEComisionRepresentantes
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        PRG_ID = x.Idprograma,
                        BPS_ID = x.Id,
                        BPS_NAME = x.Representante,
                        STARTS = Convert.ToDateTime(x.FechaInicio),
                        ENDS = Convert.ToDateTime(x.FechaFin),
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual,
                        SEQUENCE = x.sequence
                    });
                });
            }
            return datos;
        }

        private List<BEComisionRecaudadorRango> obtenerRangos()
        {
            List<BEComisionRecaudadorRango> datos = new List<BEComisionRecaudadorRango>();

            if (RangoTmp != null)
            {
                RangoTmp.ForEach(x =>
                {
                    datos.Add(new BEComisionRecaudadorRango
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        PRG_ID = x.IdPrograma,
                        BPS_ID = x.IdRepresentante,
                        SEQUENCE = x.sequence,
                        PRG_ORDER = x.Orden,
                        PRG_VALUEI = x.ValorInicial,
                        PRG_VALUEF = x.ValorFinal,
                        PRG_PERC = x.PorcentajeAdicional,
                        PRG_VALUEC = x.ValorComisionAdicional,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                });
            }
            return datos;
        }

        [HttpPost]
        public JsonResult Insertar(BEComisionTotales en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEComisionTotales obj = new BEComisionTotales();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.PRG_ID = en.PRG_ID;
                    obj.PRG_DESC = en.PRG_DESC;
                    obj.PRG_LASTL = en.PRG_LASTL;
                    obj.RAT_FID = en.RAT_FID;
                    obj.START = en.START;
                    obj.ENDS = en.ENDS;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.Representantes = obtenerRepresentantes();
                    obj.Rangos = obtenerRangos();

                    if (obj.PRG_ID == 0)
                    {
                        var datos = new BLComisionTotales().Insertar(obj);
                    }
                    else
                    {
                        List<BEComisionRepresentantes> listRepDel = null;
                        if (RepresentanteTmpDelBD != null)
                        {
                            listRepDel = new List<BEComisionRepresentantes>();
                            RepresentanteTmpDelBD.ForEach(x => { listRepDel.Add(new BEComisionRepresentantes { SEQUENCE = x.sequence }); });
                        }
                        List<BEComisionRepresentantes> listaRepUpdEst = null;
                        if (RepresentanteTmpUPDEstado != null)
                        {
                            listaRepUpdEst = new List<BEComisionRepresentantes>();
                            RepresentanteTmpUPDEstado.ForEach(x => { listaRepUpdEst.Add(new BEComisionRepresentantes { SEQUENCE = x.sequence }); });
                        }

                        List<BEComisionRecaudadorRango> listRangoDel = null;
                        if (RangoTmpDelBD != null)
                        {
                            listRangoDel = new List<BEComisionRecaudadorRango>();
                            RangoTmpDelBD.ForEach(x => { listRangoDel.Add(new BEComisionRecaudadorRango { SEQUENCE = x.sequence }); });
                        }
                        List<BEComisionRecaudadorRango> listaRangoUpdEst = null;
                        if (RangoTmpUPDEstado != null)
                        {
                            listaRangoUpdEst = new List<BEComisionRecaudadorRango>();
                            RangoTmpUPDEstado.ForEach(x => { listaRangoUpdEst.Add(new BEComisionRecaudadorRango { SEQUENCE = x.sequence }); });
                        }

                        var datos = new BLComisionTotales().Actualizar(obj, listRepDel, listaRepUpdEst, listRangoDel, listaRangoUpdEst);

                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert Comisión totales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var comision = new BLComisionTotales().Obtiene(GlobalVars.Global.OWNER, id);
                    if (comision != null)
                    {
                        BEComisionTotales en = new BEComisionTotales()
                        {
                            PRG_ID = comision.PRG_ID,
                            PRG_DESC = comision.PRG_DESC,
                            PRG_LASTL = comision.PRG_LASTL,
                            RAT_FID = comision.RAT_FID,
                            START = comision.START,
                            ENDS = comision.ENDS,
                        };

                        if (comision.Representantes != null)
                        {
                            representantes = new List<DTORepresentante>();

                            foreach (var s in comision.Representantes)
                            {
                                var rep = new DTORepresentante();
                                rep.Idprograma = s.PRG_ID;
                                rep.Id = s.BPS_ID;
                                rep.FechaInicio = s.STARTS.ToShortDateString();
                                rep.FechaFin = s.ENDS.ToShortDateString();
                                rep.sequence = s.SEQUENCE;
                                rep.EnBD = true;
                                rep.UsuarioCrea = s.LOG_USER_CREAT;
                                rep.FechaCrea = s.LOG_DATE_CREATE;
                                rep.UsuarioModifica = s.LOG_USER_UPDATE;
                                rep.FechaModifica = s.LOG_DATE_UPDATE;
                                rep.Activo = s.Inactivo.HasValue ? false : true;
                                var usu = new BLSocioNegocio().ObtenerDatos(s.BPS_ID, GlobalVars.Global.OWNER);
                                if (usu != null)
                                {
                                    var nombres = "";
                                    if (Convert.ToString(usu.ENT_TYPE) == "N")
                                        nombres = String.Format("{0} {1} {2}", usu.BPS_FIRST_NAME, usu.BPS_FATH_SURNAME, usu.BPS_MOTH_SURNAME);
                                    else
                                        nombres = String.Format("{0}", usu.BPS_NAME);
                                    rep.Representante = nombres;
                                }
                                representantes.Add(rep);
                            }
                            RepresentanteTmp = representantes;
                        }

                        if (comision.Rangos != null)
                        {
                            rangos = new List<DTORangorecaudador>();

                            foreach (var s in comision.Rangos)
                            {
                                var ran = new DTORangorecaudador();
                                ran.IdPrograma = s.PRG_ID;
                                ran.sequence = s.SEQUENCE;
                                ran.IdRepresentante = s.BPS_ID;
                                ran.ValorInicial = s.PRG_VALUEI;
                                ran.ValorFinal = s.PRG_VALUEF;
                                ran.Orden = s.PRG_ORDER;
                                ran.ValorComisionAdicional = s.PRG_VALUEC;
                                ran.PorcentajeAdicional = s.PRG_PERC;
                                ran.FormatoComision = s.Formato;
                                ran.EnBD = true;
                                ran.UsuarioCrea = s.LOG_USER_CREAT;
                                ran.FechaCrea = s.LOG_DATE_CREATE;
                                ran.UsuarioModifica = s.LOG_USER_UPDATE;
                                ran.FechaModifica = s.LOG_DATE_UPDATE;
                                ran.Activo = s.ENDS.HasValue ? false : true;
                                rangos.Add(ran);
                            }
                            RangoTmp = rangos;
                        }
                        retorno.data = Json(en, JsonRequestBehavior.AllowGet);
                        retorno.message = "Comisión por total encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar la comisión por total";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos Comisión por totales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
