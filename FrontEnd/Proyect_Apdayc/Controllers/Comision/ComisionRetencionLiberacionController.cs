using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionRetencionLiberacionController : Base
    {
        //
        // GET: /ComisionRetencionLiberacion/

        #region varialbles log
        const string NomAplicacion = "SGRDA";
        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult ListarRetLib(int skip, int take, int page, int pageSize, decimal IdRepresentante, decimal IdTipoComision, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, decimal IdTarifa, decimal IdOficina, string IdMoneda, decimal IdDivAdm, DateTime FechaIni, DateTime FechaFin)
        {
            var lista = Lista(GlobalVars.Global.OWNER, IdRepresentante, IdTipoComision, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, IdTarifa, IdOficina, IdMoneda, IdDivAdm, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAjustesComision { listaRetLibComision = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAjustesComision { listaRetLibComision = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEAjustesComision> Lista(string owner, decimal IdRepresentante, decimal IdTipoComision, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, decimal IdTarifa, decimal IdOficina, string IdMoneda, decimal IdDivAdm, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new BLAjustesComision().ListarRetLibComisiones(owner, IdRepresentante, IdTipoComision, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, IdTarifa, IdOficina, IdMoneda, IdDivAdm, FechaIni, FechaFin, pagina, cantRegxPag);
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

        [HttpPost()]
        public JsonResult ValidacionPerfilUsuarioDerecho(decimal idAsociado)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSocioNegocio servicio = new BLSocioNegocio();
                var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
                if (datos != null)
                {
                    if (datos.BPS_USER.ToString() != "1")
                    {
                        retorno.result = 0;
                        retorno.message = "EL perfil del asociado no es el de Usuario de derecho.";
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

        public List<BEAjustesComision> ListaComisionesRetenidas()
        {
            Resultado retorno = new Resultado();
            try
            {
                var lista = new BLAjustesComision().ListarRetenciones(GlobalVars.Global.OWNER);
                return lista;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                return null;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaComisionesRetenidas", ex);
            }
        }

        public JsonResult ObtenerMatrizRetenidos(List<BEAjustesComision> Retenidos)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (Retenidos != null)
                {
                    retorno.result = 1;
                    BLAjustesComision servicio = new BLAjustesComision();

                    var nR = Retenidos;
                    var R = ListaComisionesRetenidas();

                    foreach (BEAjustesComision i in R)
                    {
                        var query = from item in nR where item.SEQUENCE == i.SEQUENCE select item;

                        if (query.ToList().Count == 0)
                        {
                            var result = servicio.InactivarRetencionLiberacion(new BEAjustesComision
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                SEQUENCE = i.SEQUENCE
                            });
                        }
                    }

                    foreach (BEAjustesComision i in nR)
                    {
                        var query = from item in R where item.SEQUENCE == i.SEQUENCE select item;

                        if (query.ToList().Count == 0)
                        {
                            var result = servicio.ActivarRetencionLiberacion(new BEAjustesComision
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                SEQUENCE = i.SEQUENCE
                            });
                        }
                    }
                    retorno.message = "Actualización satisfactoria";
                    retorno.result = 1;
                }
                else
                {
                    var R = ListaComisionesRetenidas();

                    if (R == null || R.ToList().Count == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Seleccione una o varias comisiones, de una misma pagina";
                    }
                    else
                    {
                        BLAjustesComision servicio = new BLAjustesComision();

                        foreach (BEAjustesComision i in R)
                        {
                            var result = servicio.InactivarRetencionLiberacion(new BEAjustesComision
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                SEQUENCE = i.SEQUENCE
                            });
                        }

                        retorno.message = "Actualización satisfactoria";
                        retorno.result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerMatrizRetenidos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TotalValor(BEAjustesComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    en.OWNER = GlobalVars.Global.OWNER;
                    var resul = new BLAjustesComision().RetencionLiberacionTotal(en);
                    retorno.data = Json(resul, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "TotalValor Retención y Liberación de comisiones", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
