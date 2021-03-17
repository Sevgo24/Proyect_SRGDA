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

namespace Proyect_Apdayc.Controllers
{
    public class GarantiaController : Base
    {
        //
        // GET: /Garantia/
        public ActionResult Dialogs()
        {
            Init(false);
            return View();
        }
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public JsonResult UpdGarantia(decimal idGarantia, decimal idLic, decimal valor, string moneda, string tipo, string numero, string entidad, string rFecha, string dFecha, decimal? aValor, decimal? dValor, DateTime? tFecha)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                     var fechaValida = true;
                    DateTime? fechaDevol=null;
                      try
                      {
                          var x = Convert.ToDateTime(rFecha);
                          if (dFecha != ""){  fechaDevol = Convert.ToDateTime(dFecha);}
                      }
                      catch
                      {
                          fechaValida = false;
                      }
                   
                      if (fechaValida)
                      {
                          entidad = Convert.ToString(entidad).ToUpper();
                          numero = Convert.ToString(numero).ToUpper();
                          var result = new BLGarantia().Actualizar(GlobalVars.Global.OWNER, idGarantia, idLic, valor, moneda, tipo, numero, entidad, Convert.ToDateTime(rFecha), fechaDevol, aValor, dValor, tFecha, UsuarioActual);

                          retorno.result = 1;
                          retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                      }
                      else
                      {
                          retorno.message = "La fecha no tiene el formato correcto MM/DD/YYYY";
                          retorno.result = 0;

                      }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdGarantia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Eliminar(decimal id, int EsActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (EsActivo == 1)
                        new BLGarantia().Eliminar(GlobalVars.Global.OWNER, id, UsuarioActual);
                    else
                        new BLGarantia().Activar(GlobalVars.Global.OWNER, id, UsuarioActual);

                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerXCodigo(decimal idGarantia, decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var garantia = new BLGarantia().ObtenerGarantiaXCod(GlobalVars.Global.OWNER, idGarantia, idLic);
                    DTOGarantia garantiaDTO = null;
                    if (garantia != null)
                    {
                        garantiaDTO = new DTOGarantia();
                        garantiaDTO.idGarantia = garantia.GUAR_ID;
                        garantiaDTO.Valor = garantia.GUAR_VAL;
                        garantiaDTO.moneda = garantia.CUR_ALPHA;
                        garantiaDTO.tipo = garantia.GUAR_TYPE;
                        garantiaDTO.numero = garantia.GUAR_NRO;
                        garantiaDTO.entidad = garantia.GUAR_ENTITY;
                        garantiaDTO.FechaRecepcion = garantia.GUAR_RDATE;
                        garantiaDTO.FechaDevolucion =  garantia.GUAR_DDATE;
                        garantiaDTO.ValorAplicado = garantia.GUAR_AVAL;
                        garantiaDTO.ValorDevuelto = garantia.GUAR_DVAL;
                        garantiaDTO.FechaRetencion =  garantia.GUAR_TDATE;
                        garantiaDTO.FechaRecepcionChar = garantia.GUAR_RDATE.ToShortDateString();
                        garantiaDTO.FechaDevolucionChar = garantia.GUAR_DDATE.HasValue ? garantia.GUAR_DDATE.Value.ToShortDateString() : string.Empty;
                    }
                    retorno.result = 1;
                    retorno.data = Json(garantiaDTO, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insertar(decimal idLic, decimal valor, string moneda, string tipo, string numero, string entidad, string rFecha, DateTime? dFecha, decimal? aValor, decimal? dValor, DateTime? tFecha)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var fechaValida = true;
                    DateTime fechaRecep = DateTime.Now;
                      try
                      {
                          fechaRecep = Convert.ToDateTime(rFecha);
                      }
                      catch
                      {

                          fechaValida = false;
                      }
                        if (fechaValida)
                        {
                            entidad = Convert.ToString(entidad).ToUpper();
                            numero = Convert.ToString(numero).ToUpper();
                           
                            
                            var result = new BLGarantia().Insertar(GlobalVars.Global.OWNER, idLic, valor, moneda, tipo, numero, entidad,fechaRecep, dFecha, aValor, dValor, tFecha, UsuarioActual);

                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                            retorno.result = 1;
                        }
                        else {

                            retorno.message = "La fecha no tiene el formato correcto dd/mm/yyyy";
                            retorno.result = 0;
                        
                        }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Garantia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarGarantia(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            var garantias = new BLGarantia().ListarGarantia(GlobalVars.Global.OWNER, codigoLic);
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Valor</th>");
                    shtml.Append("<th class='k-header'>Moneda</th>");
                    shtml.Append("<th class='k-header'>Tipo</th>");
                    shtml.Append("<th class='k-header'>Numero</th>");
                    shtml.Append("<th class='k-header'>Entidad</th>");
                    shtml.Append("<th class='k-header'>Fecha de Recepción</th>");
                    shtml.Append("<th class='k-header'>Fecha de Devolución</th>");
                    shtml.Append("<th class='k-header'>Valor Aplicado</th>");
                    shtml.Append("<th class='k-header'>Valor Devuelto</th>");
                    shtml.Append("<th class='k-header'>Fecha de Retención</th>");
                    shtml.Append("<th class='k-header'>Situacion</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (garantias != null)
                    {
                        garantias.ForEach(c =>
                        {
                            var garantiaDTO = new DTOGarantia();
                            garantiaDTO.idGarantia = c.GUAR_ID;
                            garantiaDTO.Valor = c.GUAR_VAL;
                            garantiaDTO.moneda = new BLREF_CURRENCY().ObtenerMoneda(GlobalVars.Global.OWNER, c.CUR_ALPHA).CUR_DESC;
                            if (c.GUAR_TYPE == "T")
                            {
                                garantiaDTO.tipo = "Título Valor";
                            }
                            else if (c.GUAR_TYPE == "E")
                            {
                                garantiaDTO.tipo = "EFECTIVO";
                            }
                            else if (c.GUAR_TYPE == "S")
                            {
                                garantiaDTO.tipo = "SEGURO";
                            }
                            else
                            {
                                garantiaDTO.tipo = "POLIZA";
                            }
                            //garantiaDTO.tipo = c.GUAR_TYPE;
                            garantiaDTO.numero = c.GUAR_NRO;
                            garantiaDTO.entidad = c.GUAR_ENTITY;
                            garantiaDTO.FechaRecepcion = c.GUAR_RDATE;
                            garantiaDTO.FechaDevolucion = c.GUAR_DDATE;
                            garantiaDTO.ValorAplicado = c.GUAR_AVAL;
                            garantiaDTO.ValorDevuelto = c.GUAR_DVAL;
                            garantiaDTO.FechaRetencion = c.GUAR_TDATE;
                            garantiaDTO.Activo = c.ENDS.HasValue ? false : true;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.idGarantia);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.Valor);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.moneda);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.tipo);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.numero);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.entidad);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.FechaRecepcion.ToShortDateString());
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.FechaDevolucion == null ? "" : garantiaDTO.FechaDevolucion.Value.ToShortDateString());
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.ValorAplicado);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.ValorDevuelto);
                            shtml.AppendFormat("<td >{0}</td>", garantiaDTO.FechaRetencion);
                            shtml.AppendFormat("<td >{0}</td>", !(garantiaDTO.Activo) ? "Inactivo" :  (c.GUAR_DDATE == null && c.GUAR_DVAL == null) ? "Por Devolver" : "Devuelto"); 
                           
                            shtml.AppendFormat("<td>");

                            if (garantiaDTO.Activo && c.GUAR_DDATE == null && c.GUAR_DVAL == null)
                            {
                                shtml.AppendFormat("<a href=# onclick='devolver({0});' alt='Devolver garantía'><img class='activado' src='../Images/iconos/undoMoney.png' border=0></a>&nbsp;&nbsp;", garantiaDTO.idGarantia);
                            }
                            else {
                                shtml.Append("<img  class='desactivado' src='../Images/iconos/undoMoney.png' border=0>&nbsp;&nbsp;");
                            }
                            if (c.GUAR_DDATE == null)
                            {
                                shtml.AppendFormat("<a href=# onclick='updAddGarantia({0},false);' alt='Editar garantía'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", garantiaDTO.idGarantia);
                            }
                            else {
                                shtml.AppendFormat("<a href=# onclick='updAddGarantia({0},true);' alt='Editar garantía'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", garantiaDTO.idGarantia);
                            }
                            if (c.GUAR_DDATE == null)
                            {
                                shtml.AppendFormat("<a href=# onclick='delAddGarantia({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", garantiaDTO.idGarantia, garantiaDTO.Activo ? "delete.png" : "activate.png", garantiaDTO.Activo ? "Eliminar Garantía" : "Activar Garantía", garantiaDTO.Activo == true ? 1 : 0);
                            }
                            else {
                                shtml.Append("<img src='../Images/iconos/delete.png' title='No se puede eliminar, garantía devuelta.' border=0>");
                            }
                          
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarGarantia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Devolver(decimal idGarantia, decimal idLic, string dFecha)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var fechaValida = true;
                    try
                    {
                        var fechad = Convert.ToDateTime(dFecha);
                    }
                    catch
                    {
                        fechaValida = false;
                    }
                    if (fechaValida)
                    {
                        var result = new BLGarantia().Devolver(GlobalVars.Global.OWNER, idGarantia, idLic, Convert.ToDateTime(dFecha), UsuarioActual);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    }
                    else
                    {
                        retorno.message = "La fecha no tiene el formato correcto MM/DD/YYYY";
                        retorno.result = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Devolver", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
