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

namespace Proyect_Apdayc.Controllers
{
    public class CaracteristicaPredefinidaController : Base
    {
        private const string K_SESION_CARACTERISTICA     = "___DTOCaracteristica";
        private const string K_SESION_CARACTERISTICA_DEL = "______DTOCaracteristicaDEL";
        private const string K_SESION_CARACTERISTICA_ACT = "___DTOCaracteristicaACT";

        // GET: /CaracteristicaPredefinida/
        List<DTOCaracteristica> Caracteristica = new List<DTOCaracteristica>();

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_CARACTERISTICA);
            Session.Remove(K_SESION_CARACTERISTICA_DEL);
            Session.Remove(K_SESION_CARACTERISTICA_ACT);
            return View();
        }

        public JsonResult Listar_PageJson_CaracteristicaPredefinida(int skip, int take, int page, int pageSize, string group, decimal tipo, decimal? subtipo, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_CaracteristicaPredefinida(tipo, subtipo, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BECaracteristicaPredefinida { CaracteristicaPredefinida = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BECaracteristicaPredefinida { CaracteristicaPredefinida = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BECaracteristicaPredefinida> Listar_Page_CaracteristicaPredefinida(decimal tipo, decimal? subtipo, int st, int pagina, int cantRegxPag)
        {
            return new BLCaracteristicaPredefinida().Listar_Page_CaracteristicaPredefinida(tipo, subtipo, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene( decimal idTipoEst, decimal idSubTipoEst,decimal idCar)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BECaracteristicaPredefinida tipo = new BECaracteristicaPredefinida();
                    tipo = new BLCaracteristicaPredefinida().Obtener(GlobalVars.Global.OWNER, idCar, idTipoEst,idSubTipoEst);
                    //tipo.CaracteristicaPredefinida = obtenerDetalles();

                    if (tipo != null)
                    {
                        if (tipo.CaracteristicaPredefinida != null)
                        {
                            Caracteristica = new List<DTOCaracteristica>();
                            if (tipo.CaracteristicaPredefinida != null)
                            {
                                tipo.CaracteristicaPredefinida.ForEach(s =>
                                {
                                    Caracteristica.Add(new DTOCaracteristica
                                    {
                                        Id = s.CHAR_TYPES_ID,
                                        Idcaracteristica=s.CHAR_ID.ToString(),
                                        IdEstablecimiento= s.EST_ID.ToString(),
                                        IdSubTipoEstablecimiento=s.SUBE_ID.ToString(),
                                        TipoEstablecimiento = s.TIPO,
                                        SubTipoEstablecimiento = s.SUBTIPO,
                                        caracteristica = s.CHAR_SHORT,
                                        UsuarioCrea=s.LOG_USER_CREAT,
                                        FechaCrea=s.LOG_DATE_CREAT,
                                        UsuarioModifica=s.LOG_USER_UPDATE,
                                        FechaModifica=s.LOG_DATE_UPDATE,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });

                                DetallesTmp = Caracteristica;
                                ListarCaracteristica();
                            }
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado las Características Predefinidas";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene las Características Predefinidas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtieneCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BECaracteristicaPredefinida tipo = new BECaracteristicaPredefinida();
                    var lista = new BLCaracteristicaPredefinida().ObtenerCaracteristica(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        Caracteristica = new List<DTOCaracteristica>();
                        //if (lista != null)
                        //{
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.CHAR_TYPES_ID = item.CHAR_TYPES_ID;
                            tipo.CHAR_ID = item.CHAR_ID;
                            tipo.EST_ID = item.EST_ID;
                            tipo.SUBE_ID = item.SUBE_ID;
                        }

                        //}
                        DetallesTmp = Caracteristica;
                        ListarCaracteristica();

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el grupo de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene las Características Predefinidas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLCaracteristicaPredefinida servicio = new BLCaracteristicaPredefinida();
                    var result = servicio.Eliminar(new BECaracteristicaPredefinida
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CHAR_ID = codigo,
                        LOG_USER_UPDATE = UsuarioActual,
                    });

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina la característica predefinida", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BECaracteristicaPredefinida entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    entidad.OWNER = GlobalVars.Global.OWNER;
                    entidad.CaracteristicaPredefinida = obtenerDetalles(entidad.EST_ID,entidad.SUBE_ID);


                    if (entidad.CHAR_TYPES_ID == 0)
                    {
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLCaracteristicaPredefinida().Insertar(entidad);
                    }
                    else
                    {
                        entidad.CHAR_TYPES_ID = entidad.CHAR_TYPES_ID;
                        entidad.LOG_USER_UPDATE = UsuarioActual;

                        //1.setting caracteristicas eliminar
                        List<BECaracteristicaPredefinida> listaCarDel = null;
                        if (CaracteristicaTmpDelBD != null)
                        {
                            listaCarDel = new List<BECaracteristicaPredefinida>();
                            CaracteristicaTmpDelBD.ForEach(x => { listaCarDel.Add(new BECaracteristicaPredefinida { CHAR_TYPES_ID = x.Id }); });
                        }
                        //setting Caracteristicas activar
                        List<BECaracteristicaPredefinida> listaCarUpdEst = null;
                        if (CaracteristicaTmpUPDEstado != null)
                        {
                            listaCarUpdEst = new List<BECaracteristicaPredefinida>();
                            CaracteristicaTmpUPDEstado.ForEach(x => { listaCarUpdEst.Add(new BECaracteristicaPredefinida { CHAR_TYPES_ID = x.Id }); });
                        }

                        var datos = new BLCaracteristicaPredefinida().Actualizar(entidad, listaCarDel, listaCarUpdEst);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar la característica predefinida", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Agregar y Listar Características

        public List<DTOCaracteristica> DetallesTmp
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESION_CARACTERISTICA];
            }
            set
            {
                Session[K_SESION_CARACTERISTICA] = value;
            }
        }

        private List<DTOCaracteristica> CaracteristicaTmpUPDEstado
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESION_CARACTERISTICA_DEL];
            }
            set
            {
                Session[K_SESION_CARACTERISTICA_DEL] = value;
            }
        }

        private List<DTOCaracteristica> CaracteristicaTmpDelBD
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESION_CARACTERISTICA_ACT];
            }
            set
            {
                Session[K_SESION_CARACTERISTICA_ACT] = value;
            }
        }

        public JsonResult ObtieneCaracteristicaTmp(decimal idCarac)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DetallesTmp.Where(x => x.Id == idCarac).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneCaracteristicaTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BECaracteristicaPredefinida> obtenerDetalles(decimal EST_ID, decimal SUBE_ID)
        {
            List<BECaracteristicaPredefinida> datos = new List<BECaracteristicaPredefinida>();
            if (DetallesTmp != null)
            {
                DetallesTmp.ForEach(x =>
                {
                    datos.Add(new BECaracteristicaPredefinida
                    {
                        CHAR_TYPES_ID = x.Id,
                        CHAR_ID = Convert.ToDecimal(x.Idcaracteristica),                        
                        OWNER = GlobalVars.Global.OWNER,
                        EST_ID = Convert.ToDecimal(x.IdEstablecimiento),
                        SUBE_ID = Convert.ToDecimal(x.IdSubTipoEstablecimiento),     
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                });
            }
            return datos;
        }

        public JsonResult ListarCaracteristica()
        {
            Caracteristica = DetallesTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header' style='width:300px'>Característica</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'>Usuario Modificación</th>");
                    shtml.Append("<th class='k-header'>Fecha Modificación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (Caracteristica != null)
                    {
                        foreach (var item in Caracteristica.OrderBy(x => x.Id))
                        {
                            if (item.Activo == true || item.Activo == false)
                            {
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td style='width:300px'>{0}</td>", item.caracteristica);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);                                
                                shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                                shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                                shtml.AppendFormat("                        <a href=# onclick='DellAddCaracteristica({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Caracteristica" : "Activar Caracteristica");
                                shtml.Append("</td>");
                                shtml.Append("</tr>");
                            }
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
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOCaracteristica carac)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Caracteristica = DetallesTmp;
                    if (Caracteristica == null) Caracteristica = new List<DTOCaracteristica>();
                    int registroNuevo = 0;
                    int registroModificar = 0;
                    //caracteristicas = CaracteristicasTmp;
                    if (Caracteristica != null)
                    {
                        registroNuevo = Caracteristica.Where(p => p.Idcaracteristica == carac.Idcaracteristica && carac.Id == 0).Count();
                        registroModificar = Caracteristica.Where(p => p.Idcaracteristica != carac.Idcaracteristica && p.Id == carac.Id).Count();
                    }
                    else
                    {
                        Caracteristica = new List<DTOCaracteristica>();
                    }

                    if ((carac.Id == 0 && registroNuevo == 0)
                         || (carac.Id != 0 && registroModificar > 0)
                       )
                    {
                        if (Convert.ToInt32(carac.Id) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (Caracteristica.Count > 0) nuevoId = Caracteristica.Max(x => x.Id) + 1;
                            carac.Id = nuevoId;
                            carac.Activo = true;
                            carac.EnBD = false;
                            carac.UsuarioCrea = UsuarioActual;
                            carac.FechaCrea = DateTime.Now;
                            Caracteristica.Add(carac);
                        }
                        else
                        {
                            var item = Caracteristica.Where(x => x.Id == carac.Id).FirstOrDefault();
                            carac.Id = item.Id;
                            carac.IdEstablecimiento = item.IdEstablecimiento;
                            carac.IdSubTipoEstablecimiento = item.IdSubTipoEstablecimiento;
                            carac.EnBD = item.EnBD;//indicador que item viene de la BD
                            carac.Activo = item.Activo;
                            carac.UsuarioCrea = item.UsuarioCrea;
                            carac.FechaCrea = item.FechaCrea;
                            carac.UsuarioModifica = item.UsuarioModifica;
                            carac.FechaModifica = item.FechaModifica;
                            Caracteristica.Remove(item);
                            Caracteristica.Add(carac);
                        }
                        DetallesTmp = Caracteristica;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "La caracteristica ya existe.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Caracteristica = DetallesTmp;
                    if (Caracteristica != null)
                    {
                        var objDel = Caracteristica.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (CaracteristicaTmpUPDEstado == null) CaracteristicaTmpUPDEstado = new List<DTOCaracteristica>();
                                if (CaracteristicaTmpDelBD == null) CaracteristicaTmpDelBD = new List<DTOCaracteristica>();

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
                                Caracteristica.Remove(objDel);
                                Caracteristica.Add(objDel);
                            }
                            else
                            {
                                Caracteristica.Remove(objDel);
                            }
                            DetallesTmp = Caracteristica;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
