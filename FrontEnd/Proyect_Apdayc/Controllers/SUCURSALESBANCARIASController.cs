using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers
{   
    public class SUCURSALESBANCARIASController : Base
    {
        #region varialbles log
        const string nomAplicacion = "SGRDA";
        //string BancoID = string.Empty;
        #endregion

        //
        // GET: /SUCURSALESBANCARIAS/

        #region variables de sesion
        private const string K_SESION_DIRECCION = "___DTODirecciones";
        private const string K_SESION_DIRECCION_DEL = "___DTODireccionesDEL";
        private const string K_SESION_DIRECCION_ACT = "___DTODireccionesACT";

        private const string K_SESSION_CONTACTO = "___DTOContacto";
        private const string K_SESSION_CONTACTO_DEL = "___DTOContactoDEL";
        private const string K_SESSION_CONTACTO_ACT = "___DTOContactoACT";
        #endregion

        #region DTO set y get
        List<DTODireccion> direcciones = new List<DTODireccion>();
        List<DTOContacto> contactos = new List<DTOContacto>();

        private List<DTODireccion> DireccionesTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_ACT];
            }
            set
            {
                Session[K_SESION_DIRECCION_ACT] = value;
            }
        }
        private List<DTODireccion> DireccionesTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_DEL];
            }
            set
            {
                Session[K_SESION_DIRECCION_DEL] = value;
            }
        }
        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION];
            }
            set
            {
                Session[K_SESION_DIRECCION] = value;
            }
        }

        private List<DTOContacto> ContactoTmpUPDEstado
        {
            get
            {
                return (List<DTOContacto>)Session[K_SESSION_CONTACTO_ACT];
            }
            set
            {
                Session[K_SESSION_CONTACTO_ACT] = value;
            }
        }
        private List<DTOContacto> ContactoTmpDelBD
        {
            get
            {
                return (List<DTOContacto>)Session[K_SESSION_CONTACTO_DEL];
            }
            set
            {
                Session[K_SESSION_CONTACTO_DEL] = value;
            }
        }
        public List<DTOContacto> ContactoTmp
        {
            get
            {
                return (List<DTOContacto>)Session[K_SESSION_CONTACTO];
            }
            set
            {
                Session[K_SESSION_CONTACTO] = value;
            }
        }
        #endregion


        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = SucursalesBancariasListarPag("", "", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_BANKS_BRANCH> usp_listar_SucursalesBancarias()
        {
            return new BLREC_BANKS_BRANCH().Get_REC_BANKS_BRANCH();
        }

        public JsonResult usp_listar_SucursalesBancariasJson(int skip, int take, int page, int pageSize, string group, string owner, string dato, decimal? idBanco, int st)
        {
            Init();//add sysseg
            var lista = SucursalesBancariasListarPag(GlobalVars.Global.OWNER, dato,idBanco, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_BANKS_BRANCH { RECBANKSBRANCH = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new BEREC_BANKS_BRANCH { RECBANKSBRANCH = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREC_BANKS_BRANCH> SucursalesBancariasListarPag(string owner, string dato, decimal? idBanco, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_BANKS_BRANCH().REC_BANKS_BRANCH_Page(owner, dato, idBanco, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            Session.Remove(K_SESION_DIRECCION_ACT);
            Session.Remove(K_SESION_DIRECCION_DEL);
            Session.Remove(K_SESION_DIRECCION);

            Session.Remove(K_SESSION_CONTACTO);
            Session.Remove(K_SESSION_CONTACTO_ACT);
            Session.Remove(K_SESSION_CONTACTO);

            listaBancos();

            ViewData["Bancos"] = codBan;

            //FormCollection frm = new FormCollection();
            //var d = Request.Form["BRCH_ID"].ToString();
            //var g = Request.QueryString["Food"].ToString(); 

            return View();
        }

        public static string cod = "";
        public ActionResult Edit()
        {
            Init(false);//add sysseg
            return View();
        }

        public static string codBan = "";

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            Resultado retorno = new Resultado();
            var valid = ModelState.IsValid;
            BEREC_BANKS_BRANCH en = new BEREC_BANKS_BRANCH();
            en.LOG_USER_CREAT = UsuarioActual;
            en.BRCH_NAME = frm["BRCH_NAME"];
            en.BRCH_ID = frm["BRCH_ID"];
            en.BNK_ID = Convert.ToDecimal(frm["lista_Bancos"]); //frm["lista_Bancos"];
            en.Direccion = obtenerDirecciones();
            en.Contacto = obtenerContactos(frm);

            if (frm["BRCH_NAME"] == string.Empty && frm["BRCH_ID"] == string.Empty && frm["lista_Bancos"] == string.Empty && en.Direccion == null)
            {
                //ModelState.AddModelError("BRCH_NAME", "");
                //ValidateModel(frm);
                valid = false;
                retorno.message = "Ingrese una dirección para el registro.";
            }

            if (valid == true)
            {
                bool std = new BLREC_BANKS_BRANCH().InsertarSucursal(en);

                //    if (std)
                //    {
                //        TempData["msg"] = "Registrado Correctamente";
                //        TempData["class"] = "alert alert-success";
                //    }
                //    else
                //    {
                //        TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                //        TempData["class"] = "alert alert-danger";
                //    }
                //}
                //else
                //    TempData["class1"] = "alert alert-danger";
            }
            listaBancos();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLREC_BANKS_BRANCH servicio = new BLREC_BANKS_BRANCH();
                var sucursal = new BLREC_BANKS_BRANCH().Obtiene(id, GlobalVars.Global.OWNER);

                if (sucursal != null)
                {
                    DTOSucursales sucursalesDTO = new DTOSucursales()
                    {
                        Id = sucursal.BRCH_ID,
                        idBanco = sucursal.BNK_ID,
                        Nombre = sucursal.BRCH_NAME,
                        idDireccion = sucursal.ADD_ID.ToString(),
                    };

                    sucursal.Direccion = new BLDirecciones().DireccionXSucursales(GlobalVars.Global.OWNER, sucursalesDTO.Id, sucursalesDTO.idBanco);
                    sucursal.Contacto = new BLSocioNegocioBanco().SocioNegocioBancoXSucursalesListar(sucursalesDTO.Id, GlobalVars.Global.OWNER, sucursalesDTO.idBanco);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(sucursal, JsonRequestBehavior.AllowGet);

                    if (sucursal.Direccion != null)
                    {
                        direcciones = new List<DTODireccion>();
                        sucursal.Direccion.ForEach(s =>
                        {
                            direcciones.Add(new DTODireccion
                            {
                                Id = s.ADD_ID,
                                TipoDireccion = Convert.ToString(s.ADD_TYPE),
                                Territorio = Convert.ToString(s.TIS_N),
                                RazonSocial = s.ADDRESS,
                                Lote = s.HOU_LOT,
                                Manzana = s.HOU_MZ,
                                Numero = Convert.ToString(s.HOU_NRO),
                                CodigoUbigeo = Convert.ToString(s.GEO_ID),
                                Referencia = s.ADD_REFER,
                                TipoAvenida = Convert.ToString(s.ROU_ID),
                                Avenida = s.ROU_NAME,
                                TipoEtapa = s.HOU_TETP,
                                Etapa = s.HOU_NETP,
                                TipoUrb = s.HOU_TURZN,
                                Urbanizacion = s.HOU_URZN,
                                CodigoPostal = Convert.ToString(s.CPO_ID),
                                EsPrincipal = Convert.ToString(s.MAIN_ADD),
                                TipoDireccionDesc = new BLREF_ADDRESS_TYPE().Obtiene(GlobalVars.Global.OWNER, s.ADD_TYPE).DESCRIPTION,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                DescripcionUbigeo = new BLUbigeo().ObtenerDescripcion(s.TIS_N, s.GEO_ID).NOMBRE_UBIGEO
                            });
                        });
                        DireccionesTmp = direcciones;
                    }

                    if (sucursal.Contacto != null)
                    {
                        contactos = new List<DTOContacto>();
                        sucursal.Contacto.ForEach(s =>
                        {
                            contactos.Add(new DTOContacto
                            {
                                Id = s.Id,
                                Idsucursal = s.BRCH_ID,
                                contacto = s.BPS_ID.ToString(),
                                idRol = s.ROL_ID,
                                idDocumento = Convert.ToDecimal(s.TAX_ID),
                                Documento = s.TAXN_NAME,
                                Numero = s.TAXT_ID.ToString(),
                                Nombre = s.BPS_NAME,
                                Rol = s.ROL_DESC,
                                idBanco = s.BNK_ID,
                                usercreate = UsuarioActual,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true
                            });
                        });
                        ContactoTmp = contactos;
                    }
                    retorno.data = Json(sucursalesDTO, JsonRequestBehavior.AllowGet);
                    retorno.message = "Socio encontrado";
                    retorno.result = 1;

                }
                else
                {
                    retorno.message = "No se ha podido encontrar al socio";
                    retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos sucursal bancaria", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editar(BEREC_BANKS_BRANCH sucursal)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEREC_BANKS_BRANCH obj = new BEREC_BANKS_BRANCH();
                obj.OWNER = GlobalVars.Global.OWNER;
                obj.BNK_ID = sucursal.BNK_ID;
                obj.BRCH_ID = sucursal.BRCH_ID;
                obj.ADD_ID = sucursal.ADD_ID;
                obj.BRCH_NAME = sucursal.BRCH_NAME;
                obj.LOG_USER_UPDATE = UsuarioActual;
                obj.auxBNK_ID = sucursal.auxBNK_ID;
                obj.Direccion = obtenerDirecciones();
                obj.Contacto = obtenerContactos(null);

                List<BEREC_BANKS_BPS> listaConDel = null;
                if (ContactoTmpDelBD != null)
                {
                    listaConDel = new List<BEREC_BANKS_BPS>();
                    ContactoTmpDelBD.ForEach(x => { listaConDel.Add(new BEREC_BANKS_BPS { BPS_ID = Convert.ToDecimal(x.contacto) }); });
                }

                List<BEREC_BANKS_BPS> listaConUpdEst = null;
                if (ContactoTmpUPDEstado != null)
                {
                    listaConUpdEst = new List<BEREC_BANKS_BPS>();
                    ContactoTmpUPDEstado.ForEach(x => { listaConUpdEst.Add(new BEREC_BANKS_BPS { BPS_ID = Convert.ToDecimal(x.contacto) }); });
                }

                List<BEDireccion> listaDirDel = null;
                if (DireccionesTmpDelBD != null)
                {
                    listaDirDel = new List<BEDireccion>();
                    DireccionesTmpDelBD.ForEach(x => { listaDirDel.Add(new BEDireccion { ADD_ID = x.Id }); });
                }

                List<BEDireccion> listaDirUpdEst = null;
                if (DireccionesTmpUPDEstado != null)
                {
                    listaDirUpdEst = new List<BEDireccion>();
                    DireccionesTmpUPDEstado.ForEach(x => { listaDirUpdEst.Add(new BEDireccion { ADD_ID = x.Id }); });
                }

                var datos = new BLREC_BANKS_BRANCH().ActualizarSucursal(obj, listaConDel, listaDirDel, listaDirUpdEst, listaConUpdEst);

                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "actualizar sucursal", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Eliminar
        public JsonResult Eliminar(string Codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLREC_BANKS_BRANCH servicio = new BLREC_BANKS_BRANCH();
                var result = servicio.REC_BANKS_BRANCH_Del(new BEREC_BANKS_BRANCH
                {
                    OWNER = GlobalVars.Global.OWNER,
                    BNK_ID = Convert.ToDecimal(Codigo.Split(',')[0]),
                    BRCH_ID = Codigo.Split(',')[1],
                    LOG_USER_UPDATE = UsuarioActual
                });
                retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Tipo uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Listar
        public JsonResult ListarDireccion()
        {
            direcciones = DireccionesTmp;
            Resultado retorno = new Resultado();

            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' style='display:none'>Id</th><th  class='k-header'>Tipo Direccion</th>");
                shtml.Append("<th class='k-header'>Dirección</th>");
                shtml.Append("<th class='k-header'>Estado</th>");
                //shtml.Append("<th class='k-header'>Principal</th>");
                shtml.Append("<th  class='k-header'></th>");
                shtml.Append("</tr></thead>");

                if (direcciones != null)
                {
                    //string strChecked = "";
                    foreach (var item in direcciones.OrderBy(x => x.Id))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Id);
                        shtml.AppendFormat("<td >{0}</td>", item.TipoDireccionDesc);
                        shtml.AppendFormat("<td >{0}</td>", item.RazonSocial);
                        shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");

                        //if (item.EsPrincipal == "1")
                        //    strChecked = " checked='checked'";
                        //else
                        //    strChecked = "";

                        //shtml.AppendFormat("<td ><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);
                        shtml.AppendFormat("<td> <a href=# onclick='updAddDireccion({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                        shtml.AppendFormat("<a href=# onclick='delAddDireccion({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Dirección" : "Activar Dirección");
                        shtml.Append("</td>");
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

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarContacto()
        {
            contactos = ContactoTmp;
            Resultado retorno = new Resultado();

            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' style='display:none'>Id</th>");
                shtml.Append("<th class='k-header' style='display:none'>Contacto</th>");
                shtml.Append("<th class='k-header'>Documento</th>");
                shtml.Append("<th class='k-header'>Número</th>");
                shtml.Append("<th class='k-header'>Nombre</th>");
                shtml.Append("<th class='k-header'>Rol</th>");
                shtml.Append("<th class='k-header'>Estado</th>");
                shtml.Append("<th class='k-header'></th>");
                shtml.Append("</tr></thead>");

                if (contactos != null)
                {
                    foreach (var item in contactos.OrderBy(x => x.Id))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Idsucursal);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.contacto);
                        shtml.AppendFormat("<td >{0}</td>", item.Documento);
                        shtml.AppendFormat("<td >{0}</td>", item.idDocumento);
                        shtml.AppendFormat("<td >{0}</td>", item.Nombre);
                        shtml.AppendFormat("<td >{0}</td>", item.Rol);
                        shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                        shtml.AppendFormat("<td> <a href=# onclick='updAddContacto({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                        shtml.AppendFormat("<a href=# onclick='delAddContacto({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Contacto" : "Activar Contacto");
                        shtml.Append("</td>");
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

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region getRazonSocial
        private string getRazonSocial(DTODireccion entidad)
        {
            StringBuilder rz = new StringBuilder();

            rz.AppendFormat("{0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);

            if (!String.IsNullOrEmpty(entidad.Numero))
            {
                rz.AppendFormat("  Nro {0}", entidad.Numero);
            }
            if (!String.IsNullOrEmpty(entidad.Manzana))
            {
                rz.AppendFormat("    Mz {0}", entidad.Manzana);
            }
            if (!String.IsNullOrEmpty(entidad.Lote))
            {
                rz.AppendFormat("  Lote {0}", entidad.Lote);
            }
            if (!String.IsNullOrEmpty(entidad.NroPiso))
            {
                rz.AppendFormat("  {0} {1}", entidad.TipoDepaDes, entidad.NroPiso);
            }
            if (!String.IsNullOrEmpty(entidad.Avenida))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoAvenidaDes, entidad.Avenida);
            }
            if (!String.IsNullOrEmpty(entidad.Etapa))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoDepaDes, entidad.Etapa);
            }
            return rz.ToString();
        }
        #endregion

        #region Agregar
        [HttpPost]
        public JsonResult AddDireccion(DTODireccion direccion)
        {
            Resultado retorno = new Resultado();
            try
            {
                direcciones = DireccionesTmp;

                direccion.RazonSocial = getRazonSocial(direccion);
                if (direcciones == null) direcciones = new List<DTODireccion>();

                if (Convert.ToInt32(direccion.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (direcciones.Count > 0) nuevoId = direcciones.Max(x => x.Id) + 1;
                    direccion.Id = nuevoId;
                    direccion.Activo = true;
                    direccion.EnBD = false;
                    direccion.EsPrincipal = "1";
                    direcciones.Add(direccion);
                }
                else
                {
                    var item = direcciones.Where(x => x.Id == direccion.Id).FirstOrDefault();
                    direccion.EnBD = item.EnBD;//indicador que item viene de la BD
                    direccion.Activo = item.Activo;
                    direccion.EsPrincipal = "0";
                    direcciones.Remove(item);
                    direcciones.Add(direccion);
                }

                DireccionesTmp = direcciones;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Direccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddContacto(DTOContacto entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                contactos = ContactoTmp;
                if (contactos == null) contactos = new List<DTOContacto>();

                if (Convert.ToInt32(entidad.Id) <= 0)
                //if (entidad.Idsucursal != "")
                {
                    decimal nuevoId = 1;
                    if (contactos.Count > 0) nuevoId = contactos.Max(x => x.Id) + 1;
                    //if (contactos.Count > 0) nuevoId = contactos.Max(x => x.Id);
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    contactos.Add(entidad);
                }
                else
                {
                    var item = contactos.Where(x => x.Idsucursal == entidad.Idsucursal).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    contactos.Remove(item);
                    contactos.Add(entidad);
                }

                ContactoTmp = contactos;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Direccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Quitar
        [HttpPost]
        public JsonResult DellAddDireccion(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                direcciones = DireccionesTmp;
                if (direcciones != null)
                {
                    var objDel = direcciones.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (DireccionesTmpUPDEstado == null) DireccionesTmpUPDEstado = new List<DTODireccion>();
                            if (DireccionesTmpDelBD == null) DireccionesTmpDelBD = new List<DTODireccion>();

                            var itemUpd = DireccionesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = DireccionesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) DireccionesTmpUPDEstado.Add(objDel);
                                if (itemDel != null) DireccionesTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) DireccionesTmpDelBD.Add(objDel);
                                if (itemUpd != null) DireccionesTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            direcciones.Remove(objDel);
                            direcciones.Add(objDel);
                        }
                        else
                        {
                            direcciones.Remove(objDel);
                        }

                        DireccionesTmp = direcciones;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
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
        public JsonResult DellAddContacto(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                contactos = ContactoTmp;
                if (contactos != null)
                {
                    var objDel = contactos.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (ContactoTmpUPDEstado == null) ContactoTmpUPDEstado = new List<DTOContacto>();
                            if (ContactoTmpDelBD == null) ContactoTmpDelBD = new List<DTOContacto>();

                            var itemUpd = ContactoTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = ContactoTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) ContactoTmpUPDEstado.Add(objDel);
                                if (itemDel != null) ContactoTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) ContactoTmpDelBD.Add(objDel);
                                if (itemUpd != null) ContactoTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            contactos.Remove(objDel);
                            contactos.Add(objDel);
                        }
                        else
                        {
                            contactos.Remove(objDel);
                        }

                        ContactoTmp = contactos;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region obtener
        public JsonResult ObtieneDireccionTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                var direccion = DireccionesTmp.Where(x => x.Id == idDir).FirstOrDefault();
                retorno.data = Json(direccion, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneDireccionTmp", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneContactoTmp(decimal idCon)
        {
            Resultado retorno = new Resultado();
            try
            {
                var contacto = ContactoTmp.Where(x => x.Id == idCon).FirstOrDefault();
                retorno.data = Json(contacto, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneContactoTmp", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDireccion> obtenerDirecciones()
        {
            List<BEDireccion> datos = new List<BEDireccion>();
            if (DireccionesTmp != null)
            {
                DireccionesTmp.ForEach(x =>
                {
                    var obj = new BEDireccion();
                    obj.ADD_ID = x.Id;
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.ENT_ID = 10;
                    obj.ADD_TYPE = Convert.ToDecimal(x.TipoDireccion);
                    obj.TIS_N = Convert.ToDecimal(x.Territorio);
                    obj.ADDRESS = x.RazonSocial;
                    obj.HOU_LOT = x.Lote;
                    obj.HOU_MZ = x.Manzana;
                    obj.HOU_NRO = Convert.ToString(x.Numero);
                    obj.GEO_ID = Convert.ToDecimal(x.CodigoUbigeo);
                    obj.ADD_REFER = x.Referencia;
                    obj.ROU_ID = Convert.ToDecimal(x.TipoAvenida);
                    obj.ROU_NAME = x.Avenida;
                    obj.ROU_NUM = "1";
                    obj.HOU_TETP = x.TipoEtapa;
                    obj.HOU_NETP = x.Etapa;
                    obj.HOU_TURZN = x.TipoUrb;
                    obj.HOU_URZN = x.Urbanizacion;
                    obj.REMARK = "";
                    obj.CPO_ID = 2;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.MAIN_ADD = Convert.ToChar(x.EsPrincipal == "" ? "0" : x.EsPrincipal);
                    datos.Add(obj);
                });
            }
            return datos;
        }

        private List<BEREC_BANKS_BPS> obtenerContactos(FormCollection frm)
        {            
            List<BEREC_BANKS_BPS> datos = new List<BEREC_BANKS_BPS>();
            if (ContactoTmp != null)
            { 
                ContactoTmp.ForEach(x =>
                {
                    if (x.Idsucursal == null)
                        x.Idsucursal = frm["BRCH_ID"].ToString();
                    if (x.idBanco == null)
                        x.idBanco = Convert.ToDecimal(frm["lista_Bancos"]);
                    datos.Add(new BEREC_BANKS_BPS
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        Id = x.Id,                        
                        BNK_ID = x.idBanco,
                        BRCH_ID = x.Idsucursal.ToString(),
                        BPS_ID = Convert.ToDecimal(x.contacto),
                        ROL_ID = x.idRol,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual,
                    });
                });

                //List<BEREC_BANKS_BPS> dato = new List<BEREC_BANKS_BPS>();
                //BEREC_BANKS_BPS en = null;
                //string idbanco = "";
                //string idSucursal = "";
                //foreach (var item in ContactoTmp)
                //{
                //    if (item.idBanco == null)
                //        idbanco = frm["lista_Bancos"].ToString();
                //    else
                //        idbanco = item.idBanco;
                //    if (item.Idsucursal == null)
                //        idSucursal = frm["BRCH_ID"].ToString();
                //    else
                //        idSucursal = item.Idsucursal;


                //}
            }
            return datos;
        }
        #endregion

        IEnumerable<SelectListItem> lista1;
        //REC_BANKS_GRAL
        private void listaBancos()
        {
            lista1 = new BLREC_BANKS_GRAL().Get_REC_BANKS_GRAL()
            .Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.BNK_ID),
                Text = c.BNK_NAME
            });
            ViewData["lista_Bancos"] = lista1;
            ViewData["lista_Bancos"] = new SelectList(lista1, "Value", "Text", cod);
        }

        #region Reportes
        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BANKS_BRANCH.rdlc");

            List<BEREC_BANKS_BRANCH> lista = new List<BEREC_BANKS_BRANCH>();
            lista = usp_listar_SucursalesBancarias();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            /*if (territory != null)
            {
                var customerfilterList = from c in customerList
                                         where c.Territory == territory
                                         select c;


                reportDataSource.Value = customerfilterList;
            }
            else*/
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "Image";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>jpeg</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, "pdf");
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }

        public ActionResult DownloadReport(string format)
        {
            Init(false);//add sysseg
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BANKS_BRANCH.rdlc");

            List<BEREC_BANKS_BRANCH> lista = new List<BEREC_BANKS_BRANCH>();
            lista = usp_listar_SucursalesBancarias();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;
            localReport.DataSources.Add(reportDataSource);

            ReportParameter parametro = new ReportParameter();
            parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
            localReport.SetParameters(parametro);

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + format + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, mimeType);
            }
            else if (format == "EXCEL")
            {
                return File(renderedBytes, mimeType);
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }
        #endregion
    }
}