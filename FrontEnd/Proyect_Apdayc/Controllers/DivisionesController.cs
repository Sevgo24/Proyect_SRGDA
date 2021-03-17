using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using System.Text.RegularExpressions;
namespace Proyect_Apdayc.Controllers
{
    public class DivisionesController : Base
    {
        //
        // GET: /Divisiones/

        public const string nomAplicacion = "SRGDA";

        private const string K_SESSION_DIV_AGENTE_RECAUDO = "___DTOAgenteRecaudo";
        private const string K_SESSION_DIV_AGENTE_RECAUDO_ACT = "___DTOAgenteRecaudoACT";
        private const string K_SESSION_DIV_AGENTE_RECAUDO_DEL = "___DTOAgenteRecaudoDEL";
        List<DTOAgenteRecaudo> DivAgenteRecaudo = new List<DTOAgenteRecaudo>();

        #region VISTA
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public ActionResult Editar()
        {
            Init(false);
            return View();
        }
        #endregion

        #region CONSULTA
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string tipo, string nombre, int estado)
        {
            var lista = ListarDivisiones(GlobalVars.Global.OWNER, tipo, nombre, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREF_DIVISIONES { REFDIVISIONES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_DIVISIONES { REFDIVISIONES = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREF_DIVISIONES> ListarDivisiones(string owner, string tipo, string nombre, int estado, int pagina, int cantRegxPag)
        {
            return new BLREF_DIVISIONES().Listar(owner, tipo, nombre, estado, pagina, cantRegxPag);
        }
        #endregion

        #region TreeView

        public ActionResult ReporteTreeview()
        {
            ViewBag.Fecha = DateTime.Now.ToShortDateString();
            ViewBag.Hora = DateTime.Now.ToShortTimeString();
            ViewBag.Usuario = UsuarioActual;
            return View();
        }

        public JsonResult getTreeview(decimal id, int tipo)
        {
            List<BETreeview> lista = new List<BETreeview>();
            if (tipo == 1)
                lista = new BLREF_DIV_SUBTYPE().ArbolSubReporte(GlobalVars.Global.OWNER, id);
            else
                lista = new BLREF_DIVISIONES_VALUES().ArbolValoresReporte(GlobalVars.Global.OWNER, id);

            lista.Add(new BETreeview { cod = 0, text = "", ManagerID = null });

            var padre = lista.Where(x => x.ManagerID == null).FirstOrDefault();
            SetChildren(padre, lista);
            return Json(padre, JsonRequestBehavior.AllowGet);
        }

        public void SetChildren(BETreeview model, List<BETreeview> lista)
        {
            var childs = lista.Where(x => x.ManagerID == model.cod).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildren(child, lista);
                    model.items.Add(child);
                }
            }
        }
        #endregion

        [HttpPost]
        public JsonResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLREF_DIVISIONES();
                    var division = new BEREF_DIVISIONES();

                    division.OWNER = GlobalVars.Global.OWNER;
                    division.DAD_ID = id;
                    division.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(division);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Insertar(string code, string name, string type, string descripcion)
        {

            Resultado retorno = new Resultado();
            BEREF_DIVISIONES divisiones = new BEREF_DIVISIONES();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    divisiones.OWNER = GlobalVars.Global.OWNER;
                    divisiones.DAD_CODE = code;
                    divisiones.DAD_NAME = name;
                    divisiones.DAD_TYPE = type;
                    divisiones.DIV_DESCRIPTION = descripcion;

                    validarInsert(out valResultado, out valMsg, divisiones);
                    if (valResultado)
                    {
                        divisiones.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLREF_DIVISIONES().REF_DIVISIONES_Ins(divisiones);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Actualizar(decimal id, string nombre, string tipo, string descripcion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    BEREF_DIVISIONES divisiones = new BEREF_DIVISIONES();
                    divisiones.OWNER = GlobalVars.Global.OWNER;
                    divisiones.DAD_ID = id;
                    divisiones.DAD_NAME = nombre;
                    divisiones.DAD_TYPE = tipo;
                    divisiones.DIV_DESCRIPTION = descripcion;
                    divisiones.LOG_USER_UPDATE = UsuarioActual;

                    validarActualizar(out valResultado, out valMsg, divisiones);
                    if (valResultado)
                    {
                        divisiones.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLREF_DIVISIONES().Actualizar(divisiones);
                        if (datos == 1)
                        {
                            BECaracteristicaValor ent = new BECaracteristicaValor();
                            ent.OWNER = GlobalVars.Global.OWNER;
                            ent.DAD_ID = id;
                            ent.DAD_TYPE = tipo;
                            ent.LOG_USER_UPDATE = UsuarioActual;
                            int r = new BLCaracteristicaValor().Actualizar(ent);
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Actualizar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validarInsert(out bool exito, out string msg_validacion, BEREF_DIVISIONES entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.DAD_NAME))
            {
                msg_validacion = "Ingrese descripción.";
                exito = false;
            }
        }
        private static void validarActualizar(out bool exito, out string msg_validacion, BEREF_DIVISIONES entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.DAD_NAME))
            {
                msg_validacion = "Ingrese descripción.";
                exito = false;
            }
        }
        private static void validarInsertSubdivision(out bool exito, out string msg_validacion, BEREF_DIV_SUBTYPE entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.DAD_NAME))
            {
                msg_validacion = "Ingrese subdivision.";
                exito = false;
            }
        }
        private static void validarInsertValores(out bool exito, out string msg_validacion, BEREF_DIVISIONES_VALUES entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.DAD_VNAME))
            {
                msg_validacion = "Ingrese Nombre del Valor.";
                exito = false;
            }
        }
        private static void validarInsertCaracteristica(out bool exito, out string msg_validacion, BECaracteristicaValor entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(Convert.ToString(entidad.VALUE)))
            {
                msg_validacion = "Ingrese del Valor.";
                exito = false;
            }
        }

        [HttpPost()]
        public JsonResult ObtenerValor(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES_VALUES().ObtenerValor(GlobalVars.Global.OWNER, id);
                    if (datos != null)
                    {
                        retorno.valor = datos.DAD_VNAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Descripción de la división no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Editar
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLREF_DIVISIONES().Obtener(GlobalVars.Global.OWNER, id);
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {

                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSubdivisiones(int skip, int take, int page, int pageSize, string group, decimal id)
        {
            var lista = ListarBESubdivisiones(GlobalVars.Global.OWNER, id, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREF_DIV_SUBTYPE { REFDIVSUBTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_DIV_SUBTYPE { REFDIVSUBTYPE = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREF_DIV_SUBTYPE> ListarBESubdivisiones(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new BLREF_DIV_SUBTYPE().Listar(owner, id, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult EliminarSubdivision(int id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLREF_DIV_SUBTYPE();

                    var subdivision = new BEREF_DIV_SUBTYPE();

                    subdivision.OWNER = GlobalVars.Global.OWNER;
                    subdivision.DAD_STYPE = id;
                    subdivision.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(subdivision);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "EliminarSubdivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult InsertarSubdivision(decimal id, string DAD_NAME, string DAD_SNAME, decimal DAD_BELONGS)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    BEREF_DIV_SUBTYPE subdivision = new BEREF_DIV_SUBTYPE();
                    subdivision.DAD_ID = id;
                    subdivision.DAD_NAME = DAD_NAME;
                    subdivision.DAD_SNAME = DAD_SNAME;
                    subdivision.DAD_BELONGS = DAD_BELONGS;
                    validarInsertSubdivision(out valResultado, out valMsg, subdivision);
                    if (valResultado)
                    {
                        subdivision.OWNER = GlobalVars.Global.OWNER;
                        subdivision.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLREF_DIV_SUBTYPE().insertar(subdivision);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDesAbrevSubdivision(decimal id, string abrev)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREF_DIV_SUBTYPE servicio = new BLREF_DIV_SUBTYPE();
                    BEREF_DIV_SUBTYPE subdivision = new BEREF_DIV_SUBTYPE();

                    subdivision.OWNER = GlobalVars.Global.OWNER;
                    subdivision.DAD_ID = id;
                    subdivision.DAD_SNAME = abrev;
                    int resultado = servicio.ObtenerXAbrev(subdivision);
                    if (resultado >= 1)
                        retorno.result = 1;
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDesAbrevSubdivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarValores(int skip, int take, int page, int pageSize, string group, decimal id, decimal subId, decimal depId, string nombre)
        {
            var lista = ListarBEValores(GlobalVars.Global.OWNER, id, subId, depId, nombre, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREF_DIVISIONES_VALUES { REFDIVISIONES_VALUES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_DIVISIONES_VALUES { REFDIVISIONES_VALUES = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREF_DIVISIONES_VALUES> ListarBEValores(string owner, decimal id, decimal subId, decimal depId, string nombre, int pagina, int cantRegxPag)
        {
            return new BLREF_DIVISIONES_VALUES().Listar(owner, id, subId, depId, nombre, pagina, cantRegxPag);
        }

        [HttpPost()]
        public JsonResult InsertarValores(BEREF_DIVISIONES_VALUES valores)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    validarInsertValores(out valResultado, out valMsg, valores);
                    if (valResultado)
                    {
                        valores.OWNER = GlobalVars.Global.OWNER;
                        valores.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLREF_DIVISIONES_VALUES().insertar(valores);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarValores", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarValores(int id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLREF_DIVISIONES_VALUES();
                    var subdivision = new BEREF_DIVISIONES_VALUES();

                    subdivision.OWNER = GlobalVars.Global.OWNER;
                    subdivision.DADV_ID = id;
                    subdivision.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(subdivision);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "EliminarValores", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDesAbrevValor(BEREF_DIVISIONES_VALUES valor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREF_DIVISIONES_VALUES servicio = new BLREF_DIVISIONES_VALUES();
                    valor.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ObtenerXAbrev(valor);
                    if (resultado >= 1)
                        retorno.result = 1;
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDesAbrevValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost()]
        public JsonResult InsertarCaracteristica(BECaracteristicaValor caracteristica)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    validarInsertCaracteristica(out valResultado, out valMsg, caracteristica);
                    if (valResultado)
                    {
                        caracteristica.OWNER = GlobalVars.Global.OWNER;
                        caracteristica.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLCaracteristicaValor().Insertar(caracteristica);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarValores", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCaracteristicas(int skip, int take, int page, int pageSize, string group, decimal id)
        {
            var lista = ListarBECaracteristicas(GlobalVars.Global.OWNER, id, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BECaracteristicaValor { ListaCaracteristica = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BECaracteristicaValor { ListaCaracteristica = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BECaracteristicaValor> ListarBECaracteristicas(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new BLCaracteristicaValor().Listar(owner, id, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult EliminarCaracteristica(int id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLCaracteristicaValor();

                    var subdivision = new BECaracteristicaValor();

                    subdivision.OWNER = GlobalVars.Global.OWNER;
                    subdivision.CHARVAL_ID = id;
                    subdivision.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(subdivision);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "EliminarSubdivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AGREGAR CLIENTE
        public List<DTOAgenteRecaudo> AgenteRecaudoTmp
        {
            get
            {
                return (List<DTOAgenteRecaudo>)Session[K_SESSION_DIV_AGENTE_RECAUDO];
            }
            set
            {
                Session[K_SESSION_DIV_AGENTE_RECAUDO] = value;
            }
        }
        public List<DTOAgenteRecaudo> AgenteRecaudoTmpUPD
        {
            get
            {
                return (List<DTOAgenteRecaudo>)Session[K_SESSION_DIV_AGENTE_RECAUDO_ACT];
            }
            set
            {
                Session[K_SESSION_DIV_AGENTE_RECAUDO_ACT] = value;
            }
        }
        public List<DTOAgenteRecaudo> AgenteRecaudoTmpDelBD
        {
            get
            {
                return (List<DTOAgenteRecaudo>)Session[K_SESSION_DIV_AGENTE_RECAUDO_DEL];
            }
            set
            {
                Session[K_SESSION_DIV_AGENTE_RECAUDO_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddAgenteRecaudo(decimal Id)
        {
            Resultado retorno = new Resultado();
            DTOAgenteRecaudo entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    DivAgenteRecaudo = AgenteRecaudoTmp;
                    if (DivAgenteRecaudo == null) DivAgenteRecaudo = new List<DTOAgenteRecaudo>();
                    bool existe = DivAgenteRecaudo.Where(x => x.IdAgenteRecaudo == Id).ToList().Count > 0 ? true : false;
                    if (!existe)
                    {
                        var Agente = new BLRecibo().ObtenerCliente(GlobalVars.Global.OWNER, Id);
                        if (Agente != null)
                        {
                            entidad = new DTOAgenteRecaudo();
                            decimal nuevoId = 1;
                            if (DivAgenteRecaudo.Count > 0) nuevoId = DivAgenteRecaudo.Max(x => x.Codigo) + 1;

                            entidad.Codigo = nuevoId;
                            entidad.NombreAgenteRecaudo = Agente.ENT_TYPE_NOMBRE;
                            //entidad.TipoPersona = cliente.ENT_TYPE_NOMBRE;
                            entidad.NombreDocumento = Agente.TAXN_NAME;
                            entidad.NumDocumento = Agente.TAX_ID;
                            entidad.NombreAgenteRecaudo = Agente.BPS_NAME;

                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.Activo = true;
                            entidad.EnBD = false;

                            DivAgenteRecaudo.Add(entidad);
                            AgenteRecaudoTmp = DivAgenteRecaudo;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Agente de Recaudo fue agregada anteriormene.\r\nSeleccione otro Agente de Recaudo.";
                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddAgenteRecaudo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult delAddCliente(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            DocCliente = DocClienteTmp;
        //            if (DocCliente != null)
        //            {
        //                var objDel = DocCliente.Where(x => x.Codigo == id).FirstOrDefault();
        //                if (objDel != null)
        //                {

        //                    if (objDel.EnBD)
        //                    {
        //                        bool blActivo = !objDel.Activo;

        //                        if (DocClienteTmpUPDEstado == null) DocClienteTmpUPDEstado = new List<DTOSocio>();
        //                        if (DocClienteTmpDelBD == null) DocClienteTmpDelBD = new List<DTOSocio>();

        //                        var itemUpd = DocClienteTmpUPDEstado.Where(x => x.Codigo == id).FirstOrDefault();
        //                        var itemDel = DocClienteTmpDelBD.Where(x => x.Codigo == id).FirstOrDefault();

        //                        if (!(objDel.Activo))
        //                        {
        //                            if (itemUpd == null) DocClienteTmpUPDEstado.Add(objDel);
        //                            if (itemDel != null) DocClienteTmpDelBD.Remove(itemDel);
        //                        }
        //                        else
        //                        {
        //                            if (itemDel == null) DocClienteTmpDelBD.Add(objDel);
        //                            if (itemUpd != null) DocClienteTmpUPDEstado.Remove(itemUpd);
        //                        }
        //                        objDel.Activo = blActivo;
        //                        DocCliente.Remove(objDel);
        //                        DocCliente.Add(objDel);
        //                    }
        //                    else
        //                    {
        //                        DocCliente.Remove(objDel);
        //                        if (FacturasDetTmp != null && FacturasDetTmp.Where(s => s.idBps == id).Count() > 0)
        //                            FacturasDetTmp.RemoveAll(s => s.idBps == id);
        //                    }
        //                    DocClienteTmp = DocCliente;
        //                    retorno.result = 1;
        //                    retorno.message = "OK";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "delAddCliente", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ListarAgenteRecaudo(string accion)
        {
            DivAgenteRecaudo = AgenteRecaudoTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblCliente' border=0 width='100%;'  ><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agente de Recaudo</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Documento</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >N° Documento</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Vigencia</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Baja</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agregar Modalidad</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (DivAgenteRecaudo != null)
                    {
                        int contador = 0;
                        foreach (var item in DivAgenteRecaudo.OrderBy(x => x.FechaCrea))
                        {
                            contador += 1;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:40px;text-align:center;color: black' class='idBps'>{0}</td>", item.Codigo);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.NombreAgenteRecaudo);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.NombreDocumento);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.NumDocumento);
                            //shtml.AppendFormat("<td style='text-align:left;color: black' class='fechaVigencia'>{0}</td>", item.FechaVigencia.ToShortDateString());
                            //shtml.AppendFormat("<td style='text-align:left;color: black' class='fechaBaja'>{0}</td>", item.FechaBaja.ToShortDateString() );
                            shtml.AppendFormat("<td style='text-align:left;color: black' class='fechaVigencia'>{0}</td>","" );
                            shtml.AppendFormat("<td style='text-align:left;color: black' class='fechaBaja'>{0}</td>", "");

                            shtml.AppendFormat("<td style='width:190px;text-align:center;''> <a href=# onclick='AbrirPoPupAddGrupoModalidad({0});'> <img src='../Images/botones/invoice_more.png' title='Agregar factura.' border=0></a>", item.Codigo, item.NombreAgenteRecaudo);
                            
                            if (!item.EnBD)
                                shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddCliente({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Codigo, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Cliente" : "Activar Cliente");
                            shtml.Append("</td></tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='4'>");

                            shtml.Append("<div id='divBps" + item.Codigo.ToString() + "'>");

                            //if (FacturasDetTmp != null && FacturasDetTmp.Where(p => p.idBps == item.Codigo).Count() > 0)
                            //    shtml.Append(getHtmlListarFacturasDet(item.Codigo));

                            shtml.Append("</div>");
                            shtml.Append("</td>");

                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("</tr>");

                            //if (DocCliente.Count != contador)
                            //    shtml.Append("<tr><td colspan='20' ><hr style'display: block;height: 1px;border: 0;border-top: 1px solid #ccc;margin: 1em 0;padding: 0;'></hr></td></tr>");

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAgenteRecaudo", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}
