using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Globalization;
using SGRDA.BL.Reporte;


namespace Proyect_Apdayc.Controllers.AdministracionModuloRequerimientos
{
    public class AdministracionModuloRequerimientosController : Base
    {


        public class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int NO_PERMITE_MODIFICAR = 2;
            public const int DOCUMENTO_MANUAL_FECHA_NO_PERMITIDA = 3;

            //public const int ACTIVAR_LICENCIA
            public const string MSG_OK_REQUERIMIENTO_ACTUALIZADO = "EL REQUERIMIENTO FUE ACTUALIZADO CORRECTAMENTE ";
            public const string MSG_OK_REQUERIMIENTO_REGISTRADO = "EL REQUERIMIENTO FUE REGISTRADO CORRECTAMENTE ";

            public const string MSG_ERROR_INESPERADO_LISTAR_REQUERIMIENTOS = "OCURRIO UN ERROR AL REALIZAR LA CONSULTA | LISTAR REQUERIMIENTOS | DETALLE LOS PARAMETROS DE BUSQUEDA AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA";
            public const string MSG_ERROR_LISTAR_TIPO_REQUERIMIENTO = "OCURRO UN ERROR AL LISTAR | TIPO REQUERIMIENTOS | DETALLE LOS PARAMETROS DE BUSQUEDA AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA ";
            public const string MSG_ERROR_OBTENER_REQUERIMIENTO = "OCURRIO UN ERROR AL  |OBTENER REQUERIMIENTO| DETALLE EL ID QUE DESEO OBTENER AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA ";
            public const string MSG_ERROR_RESPONDER_REQUERIMIENTO = "OCURRIO UN ERROR AL |ACTUALIZAR RESPUESTA DE REQUERIMIENTO| DETALLE EL ID QUE DESEO MODIFICAR AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA  ";
            public const string MSG_ADVERTENCIA_OFICINA_SIN_PRIVILEGIOS_MODIFICACION = "SU OFICINA NO CUENTA CON LOS PERMISOS PARA MODIFICAR LOS REQUERIMIENTOS | SI CONSIDERA LO CONTRARIO CONSULTE AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA DETALLANDO SU OFICINA ";
            public const string MSG_ERROR_REGISTRO_REQUERIMIENTO_EST = " EL REQUERIMIENTO YA SE ENCUENTRA REGISTRADO  | SI NO ES ASI DETALLE LOS PARAMETROS DE REGISTRO AL ADMINISTRADOR PARA LA REVISION DEL PROBLEMA";
            public const string MSG_EL_DOCUMENTO_MANUAL_NO_PASO_LA_VALIDACION = "EL DOCUMENTO MANUAL NO SE PUEDE MODIFICAR DEBIDO A QUE ESTE YA FUE MIGRADO A SAP";
        }

        // GET: AdministracionModuloRequerimientos
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(decimal ID_REQ, int TIPO, int ESTADO, int CON_FECHA, string FECHA_INI, string FECHA_FIN, decimal OFICINA, decimal LIC_ID, decimal INV_ID, decimal EST_ID,decimal BEC_ID,int INACT_TYPE)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));




                    if (opcAdm == Variables.NO) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                        OFICINA = oficina;

                    List<BEAdministracionRequerimientos> lista = new BLAdministracionRequerimiento().LISTA(ID_REQ, TIPO, ESTADO, CON_FECHA, FECHA_INI, FECHA_FIN, OFICINA, LIC_ID, INV_ID, EST_ID, BEC_ID, INACT_TYPE);

                    // if (LISTA!= null && LISTA.Count>0)
                    //{
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ID REQ</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO REQ</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO REQ</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE CREACION </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.ID_REQ);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.REQUERIMENTS_DESC);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.DESC_ESTADO);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DESC_OFICINA);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_CREAT);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOG_DATE_CREAT);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarRequerimiento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_REQ, "Modificar");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);
                            //href = javascript:editar('${DISC_ID}', '${LIC_ID}')

                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.SI;
                    retorno.message = shtml.ToString();


                }
            }
            catch (Exception ex)
            {
                retorno.message = Variables.MSG_ERROR_INESPERADO_LISTAR_REQUERIMIENTOS;
                retorno.result = Variables.NO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarRequerimiento", ex);
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public JsonResult ListarTipoRequerimiento(int tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAdministracionRequerimiento().listaTipoRequerimiento(tipo)
                    .Select(c => new SelectListItem
                    {
                        //Value = Convert.ToString(c.PAR_SUBTYPE),
                        Value = Convert.ToString(c.ID_REQ_TYPE),
                        Text = c.REQUERIMENTS_DESC
                    });
                    retorno.result = Variables.SI;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_ERROR_LISTAR_TIPO_REQUERIMIENTO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneRequerimiento(decimal ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionRequerimientos entidad = new BLAdministracionRequerimiento().ObtieneRequerimiento(ID);
                    if (entidad != null)
                    {
                        retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                        retorno.result = Variables.SI;
                    }
                }


            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_ERROR_OBTENER_REQUERIMIENTO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RespuestaRequerimiento(decimal ID_REQ, int ESTADO, string RECHAZO_RESP /*,decimal LIC_ID,decimal INV_ID */)
        {
            Resultado retorno = new Resultado();

            try
            {
                BEAdministracionRequerimientos entidad = new BEAdministracionRequerimientos();
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));
                int resp = 0;
                entidad.ID_REQ = ID_REQ;
                entidad.ESTADO = ESTADO;
                entidad.REQ_DESCRIPCION_RESP = RECHAZO_RESP;
                //entidad.LIC_ID = LIC_ID;
                //entidad.INV_ID = INV_ID;

                if (opcAdm == Variables.SI)
                {
                    resp = new BLAdministracionRequerimiento().ActualizaRequerimiento(entidad);

                    if (resp == Variables.SI && entidad.ESTADO == Variables.SI)
                    {
                        //realizar la accion segun sea el tipo de requerimiento --inactivar activar , aprobar , modificar documento manual

                        new BLAdministracionRequerimiento().ActualizaRespuestaRequerimiento(entidad.ID_REQ, UsuarioActual);
                        retorno.result = Variables.SI;
                        retorno.message = Variables.MSG_OK_REQUERIMIENTO_ACTUALIZADO;
                    }
                    else if (resp == Variables.DOCUMENTO_MANUAL_FECHA_NO_PERMITIDA)
                    {
                        retorno.result = Variables.NO;
                        retorno.message = Variables.MSG_EL_DOCUMENTO_MANUAL_NO_PASO_LA_VALIDACION;
                    }
                    else
                    {
                        retorno.result = Variables.SI;
                        retorno.message = Variables.MSG_OK_REQUERIMIENTO_ACTUALIZADO;

                    }

                }
                else
                {
                    retorno.result = Variables.NO_PERMITE_MODIFICAR;
                    retorno.message = Variables.MSG_ADVERTENCIA_OFICINA_SIN_PRIVILEGIOS_MODIFICACION;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_ERROR_RESPONDER_REQUERIMIENTO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistraRequerimientoGral(decimal EST_ID, decimal ID_REQ_TYPE, string RAZON, int ACTIVO, decimal MONTO, string FECHA, decimal INV_ID, decimal LIC_ID, decimal BPS_ID,decimal BEC_ID,int TipoInactivacion)
        {
            Resultado retorno = new Resultado();

            try
            {
                BEAdministracionRequerimientos entidad = new BEAdministracionRequerimientos();

                int resp = 0;
                entidad.ID_REQ_TYPE = ID_REQ_TYPE;
                entidad.EST_ID = EST_ID;
                entidad.REQ_DESCRIPCION = RAZON;
                entidad.LIC_ID = LIC_ID;
                entidad.INV_ID = INV_ID;
                entidad.ACTIVO = ACTIVO;
                entidad.MONTO = MONTO;
                entidad.REQ_DATE = FECHA;
                entidad.BPS_ID = BPS_ID;
                entidad.OFICINA = Convert.ToDecimal(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]); ;
                entidad.LOG_USER_CREAT = UsuarioActual;
                entidad.BEC_ID = BEC_ID;
                entidad.CodigoTipoInactivacion = TipoInactivacion;


                resp = Convert.ToInt32(new BLAdministracionRequerimiento().RegistraRequerimientoGral(entidad));
                if (resp >= Variables.SI)
                {
                    retorno.result = Variables.SI;
                    retorno.Code = resp;
                    retorno.message = Variables.MSG_OK_REQUERIMIENTO_REGISTRADO;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSG_ERROR_REGISTRO_REQUERIMIENTO_EST;
                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_ERROR_REGISTRO_REQUERIMIENTO_EST;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "RegistrarRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}