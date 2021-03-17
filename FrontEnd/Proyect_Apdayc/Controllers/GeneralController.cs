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
using System.Text;
using SGRDA.DA;
using SGRDA.DA.FacturacionElectronica;
using SGRDA.BL.FacturaElectronica;
using SGRDA.BL.WorkFlow;
using SGRDA.BL.Reporte;
using SGRDA.Entities.WorkFlow;
using Proyect_Apdayc.Controllers.WorkFlow;
using System.Configuration;
using SGRDA.BL.Consulta;
using SGRDA.BL.BLAlfresco;
//using SGRDA.Utility;

namespace Proyect_Apdayc.Controllers
{
    public class GeneralController : Base
    {
        //
        // GET: /General/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lista la dependencia es el resultado de la subdivision 
        /// (subdivision = Departamente ---> Dependecia = provincias) 
        /// </summary>
        IEnumerable<SelectListItem> itemDependencia;

        //public   string UsuarioActual = "";
        public const string nomAplicacion = "SRGDA";

        public JsonResult ListaDependencia(decimal dSubTipoDivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dataitemDependencia = new BLDivision().ListarPorSubtipo(dSubTipoDivision);
                    itemDependencia = dataitemDependencia.Select(c => new SelectListItem
                    {
                        Value = c.DAD_VCODE.ToString(),
                        Text = c.DAD_VNAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemDependencia, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaDependencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista los tipos de divisiones
        /// </summary>
        IEnumerable<SelectListItem> itemDivision;
        public JsonResult ListaDivisiones()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemDivision = new BLREF_DIVISIONES().Get_REF_DIVISIONES()
                    .Select(c => new SelectListItem
                    {
                        Value = c.DAD_ID.ToString(),
                        Text = c.DAD_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemDivision, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaDivisiones", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemDivisionXTipo;
        public JsonResult ListaDivisionesXTipo(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemDivisionXTipo = new BLREF_DIVISIONES().GET_REF_DIVISIONES_BY_DAD_TYPE(tipo, GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.DAD_ID.ToString(),
                        Text = c.DAD_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemDivisionXTipo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaDivisionesXTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        IEnumerable<SelectListItem> itemTipoAforo;
        public JsonResult ListarTipoAforo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemTipoAforo = new BLAforo().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CAP_ID,
                        Text = c.CAP_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemTipoAforo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTipoAforo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        IEnumerable<SelectListItem> itemLocalidad;
        public JsonResult ListarLocalidad()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemLocalidad = new BLLocalidad().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.SECID.ToString(),
                        Text = c.SEC_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemLocalidad, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        IEnumerable<SelectListItem> itemBloqueo;
        public JsonResult ListarBloqueo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemBloqueo = new BLBloqueos().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.BLOCK_ID.ToString(),
                        Text = c.BLOCK_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemBloqueo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarBloqueo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemTemporalidad;
        public JsonResult ListaTemporalidad()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemTemporalidad = new BLREC_RATE_FREQUENCY().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.RAT_FID.ToString(),
                        Text = c.RAT_FDESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemTemporalidad, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaTemporalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemTarifaAsociada;
        public JsonResult ListaTarifaAsociada(decimal codTarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemTarifaAsociada = new BLREC_RATES_GRAL().GET_REC_RATES_GRAL(codTarifa)
                    .Select(c => new SelectListItem
                    {
                        Value = c.RATE_ID.ToString(),
                        Text = c.RATE_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemTarifaAsociada, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaTarifaAsociada", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemSocios;
        public JsonResult ListaSocios()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemSocios = new BLSocioNegocio().GET_REC_BUSINESS_PARTNER_GRAL()
                    .Select(c => new SelectListItem
                    {
                        Value = c.BPS_ID.ToString(),
                        Text = c.TAXT_ID == 1 ? c.BPS_NAME : String.Format("{0} {1} {2}", c.BPS_FIRST_NAME, c.BPS_FATH_SURNAME, c.BPS_MOTH_SURNAME)
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemSocios, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaSocios", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemModalidad;
        public JsonResult ListaModalidades(string tmp)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemModalidad = new BLREC_MODALITY().Get_Rec_Modality(tmp)
                    .Select(c => new SelectListItem
                    {
                        Value = c.MOD_ID.ToString(),
                        Text = c.MOD_DEC
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemModalidad, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaModalidades", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        IEnumerable<SelectListItem> itemEstablecimiento;
        public JsonResult ListaEstablecimiento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemEstablecimiento = new BLREC_ESTABLISHMENT_GRAL().GET_REC_ESTABLISHMENT_GRAL()
                    .Select(c => new SelectListItem
                    {
                        Value = c.EST_ID.ToString(),
                        Text = c.EST_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemEstablecimiento, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista el Grupo Empresarial
        /// </summary>
        IEnumerable<SelectListItem> itemGrupoEmp;
        public JsonResult ListaGrupoEmpresarial()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemGrupoEmp = new BLSocioNegocio().ObtenerGrupoEmp(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.BPS_ID.ToString(),
                        Text = c.ENT_TYPE == 'J' ? c.BPS_NAME : String.Format("{0} {1} {2}", c.BPS_FIRST_NAME, c.BPS_FATH_SURNAME, c.BPS_MOTH_SURNAME)
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemGrupoEmp, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaGrupoEmpresarial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista las sub divisiones
        /// </summary>
        IEnumerable<SelectListItem> itemSubDivision;
        public JsonResult ListaSubDivisiones(decimal? dDivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemSubDivision = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_GET_by_DAD_ID(dDivision.Value)
                    .Select(c => new SelectListItem
                    {
                        Value = c.DAD_STYPE.ToString(),
                        Text = c.DAD_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemSubDivision, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaSubDivisiones", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista  TIPO DE DIERECCION
        /// </summary>
        [HttpPost]
        public JsonResult ListarTipoDireccion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_ADDRESS_TYPE().ListarDirecciones()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ADDT_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTipoDireccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista  Territotiro
        /// </summary>
        [HttpPost]
        public JsonResult ListarTerritorio()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTerritorio().Listar_Territorio()
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.TIS_N),
                        Text = c.NAME_TER
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTerritorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerTarifaAsociada(decimal codModalidad, decimal? codTemp)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_RATES_GRAL tarifa = new BLREC_RATES_GRAL();
                    var datos = tarifa.obtenerTarifaAsociada(codModalidad, codTemp);
                    if (datos != null)
                    {
                        datos
                          .Select(c => new SelectListItem
                          {
                              Value = Convert.ToString(c.RATE_ID),
                              Text = c.RATE_DESC
                          });
                        retorno.result = 1;
                        retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Obtener Tarifa Asociada";
                    }
                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerTarifaAsociada", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoLicencia()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_LIC_TYPE().GET_REC_LIC_TYPE()
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.LIC_TYPE),
                        Text = c.LIC_TDESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstadoIniLicencia(decimal tipoLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_LIC_STAT().EstadoIniPorTipo(tipoLic, GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.LICS_ID),
                        Text = c.LICS_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEstadoIniLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDocumento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_TAX_ID().Get_REC_TAX_ID()
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.TAXT_ID),
                        Text = c.TAXN_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoObservacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoObservacion().usp_ListarTipoObservacion(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.OBS_TYPE),
                         Text = c.OBS_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoParametro()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoParametro().usp_ListarTipoParametro(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.PAR_TYPE),
                        Text = c.PAR_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarUbigeo(decimal valor, string nombre)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES_VALUES().usp_ListarUbigeo(valor, nombre)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.TIS_N),
                         Text = c.DAD_VNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarUbigeo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidacionDNI(string num, int id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool exito = ValidarDni(num, id);
                    if (exito)
                    {
                        retorno.result = 1;
                        retorno.message = "Número de DNI correcto";
                    }
                    else
                    {
                        retorno.result = 2;
                        retorno.message = "Número de DNI incorrecto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidacionDNI", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /*Validar su un DNI es correcto, se envía por parámetro el número del Dni y el identificador
       I<PER45793939<9<<<<<<<<<<<<<<<
       */
        private bool ValidarDni(string cDni, int nIdentificador)
        {
            bool exito = false;
            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0;

            a = Convert.ToInt32(cDni.Substring(0, 1));
            b = Convert.ToInt32(cDni.Substring(1, 1));
            c = Convert.ToInt32(cDni.Substring(2, 1));
            d = Convert.ToInt32(cDni.Substring(3, 1));
            e = Convert.ToInt32(cDni.Substring(4, 1));
            f = Convert.ToInt32(cDni.Substring(5, 1));
            g = Convert.ToInt32(cDni.Substring(6, 1));
            h = Convert.ToInt32(cDni.Substring(7, 1));

            long Suma = (a * 7 + b * 3 + c * 1 + d * 7 + e * 3 + f * 1 + g * 7 + h * 3);

            string sSuma = Convert.ToString(Suma);

            if (Convert.ToInt32(sSuma.Substring(sSuma.Length - 1, 1)) == nIdentificador) exito = true; else exito = false;

            exito = true;

            return exito;
        }

        /// <summary>
        /// ValorTipoDocumento: obtiene el valor esperado para el tipo de documento..DNI, RUC u Otros
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetConfigTipoDocumento(int tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_TAX_ID().REC_TAX_ID_GET_by_TAXT_ID(tipo);
                    var tax = datos.Where(x => x.TAXT_ID == tipo).FirstOrDefault();
                    if (tax != null)
                    {
                        retorno.valor = Convert.ToString(datos[0].TAXN_POS);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "No existe parametro configurado para el tipo de documentos.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GetConfigTipoDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRutas(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLRutas().Listar_Rutas(GlobalVars.Global.OWNER, tipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ROU_ID),
                         Text = c.ROU_COD
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarRutas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoEstablecimiento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_EST_TYPE().REC_EST_TYPE_GET()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ESTT_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSubTipoEstablecimiento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_GET()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.SUBE_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubTipoEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //public JsonResult ListarSubtipoEstablecimientoPorTipo(decimal? IdTipo)
        public JsonResult ListarSubtipoEstablecimientoPorTipo(decimal IdTipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_EST_SUBTYPE().ListarSubtipoEstablecimientoPorTipo(GlobalVars.Global.OWNER, IdTipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.SUBE_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubtipoEstablecimientoPorTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaIdentificadores()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_TAX_ID().Get_REC_TAX_ID()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.TAXT_ID),
                         Text = c.TAXN_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaIdentificadores", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ACBuscarGrupoEmpresarial()
        {
            Resultado retorno = new Resultado();
            List<DTOGrupoEmpresarial> GrupoEmp = new List<DTOGrupoEmpresarial>();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLSocioNegocio().ObtenerGrupoEmp(GlobalVars.Global.OWNER);

                    datos.ForEach(x =>
                    {
                        GrupoEmp.Add(new DTOGrupoEmpresarial
                        {
                            Codigo = x.BPS_ID,
                            value = x.ENT_TYPE == 'J' ? x.BPS_NAME : String.Format("{0} {1} {2}", x.BPS_FIRST_NAME, x.BPS_FATH_SURNAME, x.BPS_MOTH_SURNAME),
                            Descripcion = x.ENT_TYPE == 'J' ? x.BPS_NAME : String.Format("{0} {1} {2}", x.BPS_FIRST_NAME, x.BPS_FATH_SURNAME, x.BPS_MOTH_SURNAME)
                        });
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ACBuscarGrupoEmpresarial", ex);
            }
            return Json(GrupoEmp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ACBuscarUbigeo()
        {
            Resultado retorno = new Resultado();
            List<DTOUbigeo> ubigeos = new List<DTOUbigeo>();
            try
            {
                //if (!isLogout(ref retorno))
                //{
                string texto = Request.QueryString["term"];
                decimal idTerritorio = Convert.ToDecimal(Request.QueryString["tisn"]);

                var datos = new BLUbigeo().Listar_Ubigeo(idTerritorio, texto);



                datos.ForEach(x =>
                {
                    ubigeos.Add(new DTOUbigeo
                    {
                        Codigo = x.ID_UBIGEO,
                        value = x.NOMBRE_UBIGEO,
                        Descripcion = x.NOMBRE_UBIGEO
                    });
                });

                retorno.result = 1;
                retorno.data = Json(ubigeos, JsonRequestBehavior.AllowGet);
                //  }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ACBuscarUbigeo", ex);
            }
            return Json(ubigeos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ListarActividadEcon()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_ECON_ACTIVITIES().ListarActividadEcon(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ECON_ID),
                         Text = c.ECON_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarActividadEcon", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaDivisionesFiscales(decimal ter)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_TAX_DIVISION().Get_REF_TAX_DIVISION(GlobalVars.Global.OWNER, ter)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.TAXD_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDivisionesFiscales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaSoloFiscales()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES().ListarDivisionesFiscales(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DAD_ID),
                         Text = c.DAD_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaSoloFiscales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCartacteristica()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCaracteristica().ListarCartacteristica()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CHAR_ID),
                         Text = c.CHAR_LONG
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCartacteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ListaNumXtipo(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_NUMBERING().ListarXtipo(GlobalVars.Global.OWNER, tipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.NMR_ID),
                         Text = Convert.ToString(c.NMR_SERIAL)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaNumXtipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BuscarsocioTipoDocumento(decimal idTipoDocumento, string nroDocumento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLSocioNegocio().BuscarXtipodocumento(idTipoDocumento, nroDocumento);

                    if (datos != null)
                    {
                        retorno.Code = Convert.ToInt32(datos.BPS_ID);
                        retorno.valor = datos.BPS_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "No existe socio con este tipo y número de documento.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "BuscarsocioTipoDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult BuscarAgenterecaudadorTipoDocumento(decimal idTipoDocumento, string nroDocumento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTrasladoAgentesRecaudo().BuscarAgenterecaudadorXtipodocumento(idTipoDocumento, nroDocumento);

                    if (datos != null)
                    {
                        retorno.Code = Convert.ToInt32(datos.BPS_ID);
                        retorno.valor = datos.BPS_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "No existe agente para el numero de documento ingresado.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "BuscarAgenterecaudadorTipoDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarTipoDoc()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_DOCUMENT_TYPE().ListarComboTipoDoc(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DOC_TYPE),
                         Text = c.DOC_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoCorreo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoCorreo().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.MAIL_TYPE),
                         Text = c.MAIL_TDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoCorreo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoRedes()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLRedSocialType().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CONT_TYPE),
                         Text = c.CONT_TDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoCorreo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoTelefono()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoTelefono().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.PHONE_TYPE),
                         Text = c.PHONE_TDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoTelefono", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaNivelesDependencia()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAgente().ListarAgenteDependencia(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.LEVEL_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaNivelesDependencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarOfiActivas()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idoff = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var datos = new BLOffices().ListarOffActivasSERVICE(GlobalVars.Global.OWNER, idoff).Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.OFF_ID),
                        Text = c.OFF_NAME
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarOfiActivas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMonedas()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_CURRENCY().ListarMoneda()

                     .Select(c => new SelectListItem
                     {
                         Value = c.CUR_ALPHA,
                         Text = c.CUR_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarMonedas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoPago()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_PAYMENT_TYPE().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.PAY_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerOficinaActualAgente(decimal idAgente)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTrasladoAgentesRecaudo().ObtenerOficinaActualAgente(GlobalVars.Global.OWNER, idAgente);

                    if (datos != null)
                    {
                        retorno.Code = Convert.ToInt32(datos.OFF_ID);
                        retorno.valor = datos.OFF_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Este agente no tiene oficina asignada.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerOficinaActualAgente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoGasto()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoGasto().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.EXP_TYPE),
                         Text = c.EXPT_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoGasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarGrupoGasto(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLGrupoGasto().ListarCombo(GlobalVars.Global.OWNER, tipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.EXPG_ID),
                         Text = c.EXPG_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarGrupoGasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDefinicionGasto(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDefinicionGasto().ListarCombo(GlobalVars.Global.OWNER, tipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.EXP_ID),
                         Text = c.EXP_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDefinicionGasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDivision(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES().ListarDivisonesTipo(GlobalVars.Global.OWNER, tipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DAD_ID),
                         Text = c.DAD_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRoles()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_ROLES().Get_REF_ROLES()

                        .Select(c => new SelectListItem
                        {
                            Value = Convert.ToString(c.ROL_ID),
                            Text = c.ROL_DESC
                        });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarRoles", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTarifas()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_RATE_FREQUENCY().Get_REC_RATE_FREQUENCY()

                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.RAT_FID),
                         Text = c.RAT_FDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTarifas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValidarDocumento(decimal tipoDocumento, string numdocumento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dato = new BLSocioNegocio().BuscarXtipodocumentoRecaudador(tipoDocumento, numdocumento);
                    if (dato != null)
                    {
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarTrasladoAgente(decimal oficinaId)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTrasladoAgentesRecaudo().ValidarTrasladoOficinaAgente(GlobalVars.Global.OWNER, oficinaId);

                    if (datos != null)
                    {
                        retorno.Code = Convert.ToInt32(datos.OFF_ID);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Este traslado ya fue finalizado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarTrasladoAgente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarBancos()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_BANKS_GRAL().Get_REC_BANKS_GRAL()

                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.BNK_ID),
                         Text = c.BNK_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarBancos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarCargosOficina()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_COLL_LEVEL().LISTAR_REC_COLL_LEVEL()
                        .Select(c => new SelectListItem
                        {
                            Value = Convert.ToString(c.LEVEL_ID),
                            Text = c.DESCRIPTION
                        });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCargosOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRolesCargos()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_ROLES().Get_REF_ROLES()

                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ROL_ID),
                         Text = c.ROL_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarRolesCargos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDivisiones()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIV_TYPE().ListarTipoDivisiones(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DAD_TYPE,
                         Text = c.DAD_TNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDivisiones", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSubdivisionDep(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIV_SUBTYPE().ListarSubdivisionDep(GlobalVars.Global.OWNER, id)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DAD_STYPE.ToString(),
                         Text = c.DAD_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubdivisionDep", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSubdivision(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIV_SUBTYPE().ListarSubdivision(GlobalVars.Global.OWNER, id)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DAD_STYPE),
                         Text = c.DAD_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubdivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarValoresDep(decimal id, decimal subdivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES_VALUES().ListarValoresDep(GlobalVars.Global.OWNER, id, subdivision)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DADV_ID.ToString(),
                         Text = c.DAD_VNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarValoresDep", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoIncidencia()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLModalidadIncidencia().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOD_INCID,
                         Text = c.MOD_IDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoIncidencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoSociedad()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLSociedad().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOG_SOC,
                         Text = c.MOG_SDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoSociedad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDerecho(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDerecho().ListarTipo(GlobalVars.Global.OWNER, id)
                     .Select(c => new SelectListItem
                     {
                         Value = c.RIGHT_COD,
                         Text = c.RIGHT_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDerecho", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoCreacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_CREATION_CLASS().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.CLASS_COD,
                         Text = c.CLASS_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoCreacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoGrupo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    string idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
                    string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
                    if (idPerfil == Convert.ToString(idPerfilAdmin))
                    {
                        idOficina = 0;
                    }


                    var datos = new BLREC_MOD_GROUP().ListarTipo(GlobalVars.Global.OWNER, idOficina)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOG_ID,
                         Text = c.MOG_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoGrupo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoModUso()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLOrigenModalidad().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOD_ORIG,
                         Text = c.MOD_ODESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoModUso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoObra()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLUsorepertorio().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOD_USAGE,
                         Text = c.MOD_DUSAGE
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoObra", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoRepertorio()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoUsorepertorio().ListarTipo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.MOD_REPER,
                         Text = c.MOD_DREPER
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRepertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerLongitudCodigoSucursal(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_BANKS_GRAL().ObtenerLongitudCodigoSucursal(GlobalVars.Global.OWNER, Id);
                    if (datos != null || datos != string.Empty)
                    {
                        retorno.valor = datos;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "10";
                        retorno.message = "No existe longitud registrada, se asigna longitud por defecto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerLongitudCodigoSucursal", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDivionesTipo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //verificar enviar el owner
                    var datos = new BLREF_DIV_TYPE().usp_Get_REF_DIV_TYPE()
                     .Select(c => new SelectListItem
                     {
                         Value = c.DAD_TYPE,
                         Text = c.DAD_TNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDivionesTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDivionesValor(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //verificar enviar el owner
                    var datos = new BLREF_DIVISIONES_VALUES().ListarDivisionesValor(id)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DADV_ID.ToString(),
                         Text = c.DAD_VNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDivionesValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarValoresXSubdivision(decimal id, decimal subdivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIVISIONES_VALUES().ListarValoresXSubdivision(GlobalVars.Global.OWNER, id, subdivision)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DADV_ID.ToString(),
                         Text = c.DAD_VNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarValoresXSubdivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoCaracteristicas()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_DIV_CHARAC().ListarTipoCaracteristicas(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DAC_ID.ToString(),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoCaracteristicas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EjecutarProceso(decimal idModulo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dato = "OK";
                    retorno.result = 1;
                    retorno.message = dato.ToString();
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EjecutarProceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarProcesoHtml(decimal idEstado, decimal idWrkf, decimal idWrkfRef)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var modulo = new BLProceso().ListarProcesoXEstado(Constantes.Modulo.LICENCIAMIENTO, idEstado, GlobalVars.Global.OWNER, idWrkf, idWrkfRef, true);

                    shtml.Append("<table id='tbGridProceso' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' style=' width:30px;' >Orden</th>");
                    shtml.Append("<th class='k-header'>Workflow</th>");
                    shtml.Append("<th class='k-header'>Acci&oacute;n</th>");
                    shtml.Append("<th class='k-header'>Responsable de ejecuci&oacute;n</th>");
                    shtml.Append("<th class='k-header'>Fecha ejecuci&oacute;n</th>");
                    shtml.Append("<th class='k-header'>Acci&oacute;n</th></tr></thead>");

                    if (modulo != null)
                    {
                        foreach (var item in modulo.OrderBy(x => x.ORDER))
                        {
                            var bgcolor = "";

                            if (item.PROC_LAUNCH.HasValue)
                            {
                                bgcolor = " style='background:#DFEFFC;'  ";
                            }
                            shtml.AppendFormat("<tr class='k-grid-content' {0}>", bgcolor);


                            shtml.AppendFormat("<td><input type='hidden' value='{0}'/>{1}</td>", item.MOD_ID, item.ORDER);
                            shtml.AppendFormat("<td >{0}</td>", item.WRKF_NAME);
                            shtml.AppendFormat("<td class='{1}'>{0}</td>", item.WRKF_ANAME, item.WRKF_AMID);
                            //shtml.AppendFormat("<td class='{1}'>amid: {1} - aid:{2} - oid:{3} - {0}</td>", item.WRKF_ANAME, item.WRKF_AMID, item.WRKF_AID,item.WRKF_OID);
                            shtml.AppendFormat("<td>{0}</td>", item.LOG_USER_CREAT);
                            shtml.AppendFormat("<td>{0}</td>", item.PROC_LAUNCH);
                            //shtml.AppendFormat("<td ><a href='#' onClick='irProceso({0});' >Ejecutar proceso</a></td>", item.MOD_ID);

                            if (item.PROC_LAUNCH.HasValue)
                            {
                                shtml.Append("<td > <img src='../Images/iconos/procesado.png' width=20 title='Proceso Ejecutado.'  alt='Proceso Ejecutado.'/></td>");
                            }
                            else
                            {
                                ////validar si perfil tiene permisos
                                if (Generica.hasAccess(PerfilUsuarioActual, item.WRKF_AID))
                                {
                                    bool esTipoObjDocEntrada = false;
                                    BL_WORKF_OBJECTS objServ = new BL_WORKF_OBJECTS();
                                    var eObj = objServ.ObtenerObjects(GlobalVars.Global.OWNER, item.WRKF_OID);
                                    if (eObj != null &&
                                        eObj.TipoObjeto != null &&
                                         eObj.TipoObjeto.WRKF_OPREF == GlobalVars.Global.PrefijoDocumentoEntrada)
                                    {
                                        esTipoObjDocEntrada = true;
                                    }
                                    var icono = "<img src='../Images/iconos/proceso_pendiente.png' width=20 title='Pendiente de ejecución.'  alt='Pendiente de ejecución.'/>";
                                    if (esTipoObjDocEntrada)
                                    {
                                        shtml.AppendFormat("<td ><a href='#' onClick='InsertarTracesProceso({0},{1},{2},{3},{4},{5},{6},1,1);' alt='Ejecutar Proceso.' title='Ejecutar Proceso.' >{7}</a></td>", item.WRKF_AID, idWrkf, idEstado, idWrkfRef, item.PROC_ID, item.WRKF_AMID, item.WRKF_OID, icono);
                                    }
                                    else
                                    {
                                        shtml.AppendFormat("<td ><a href='#' onClick='InsertarTracesProceso({0},{1},{2},{3},{4},{5},{6},0,1);' alt='Ejecutar Proceso.' title='Ejecutar Proceso.' >{7}</a></td>", item.WRKF_AID, idWrkf, idEstado, idWrkfRef, item.PROC_ID, item.WRKF_AMID, item.WRKF_OID, icono);
                                    }
                                }
                                else
                                {
                                    shtml.Append("<td ><img src='../Images/iconos/proceso_denegado.png' width=20 title='No tiene permiso para ejecutar el proceso.'  alt='No tiene permiso para ejecutar el proceso.'/></td>");
                                }
                            }
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.result = 1;
                    retorno.message = shtml.ToString();
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarProcesoHtml", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerNombreSocio(decimal codigoBps)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLSocioNegocio().ObtenerDatos(codigoBps, GlobalVars.Global.OWNER);
                    if (datos != null)
                    {
                        retorno.valor = string.Format("{0} {1} {2} {3}", datos.BPS_NAME, datos.BPS_FIRST_NAME, datos.BPS_FATH_SURNAME, datos.BPS_MOTH_SURNAME);
                        //retorno.valor =  datos.BPS_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Razon social no registrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreSocio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerRespXEstablecimiento(decimal codigoEst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLEstablecimiento blestab = new BLEstablecimiento();
                    var establecimiento = blestab.ObtenerCabeceraEstablecimiento(codigoEst, GlobalVars.Global.OWNER);
                    if (establecimiento != null)
                    {
                        //decimal codigoBps = establecimiento.BPS_ID;
                        //var datos = new BLSocioNegocio().ObtenerDatos(codigoBps, GlobalVars.Global.OWNER);
                        //if (datos != null) // || datos.BPS_NAME != string.Empty)
                        //{
                        // var nombres =   string.Format("{0} {1} {2} {3}", datos.BPS_NAME, datos.BPS_FIRST_NAME, datos.BPS_FATH_SURNAME, datos.BPS_MOTH_SURNAME);
                        retorno.data = Json(new { idSocio = establecimiento.BPS_ID, responsable = establecimiento.BPS_NAME }, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                        //}
                        //else
                        //{
                        //    retorno.result = 0;
                        //    retorno.message = "Razon social no registrada";
                        //}
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Establecimineto no encontrado, no se puede obtener al responsable.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerRespXEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreLicencia(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicencias().ObtenerLicenciaXCodigo(codigoLic, GlobalVars.Global.OWNER);
                    if (datos != null)
                    {
                        retorno.valor = string.Format("{0}", datos.LIC_NAME);
                        retorno.message = string.Format("{0}", datos.CUR_ALPHA);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "10";
                        retorno.message = "Licencia social no registrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMediosDifusion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDifusion().ListarMedioDifusion(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.BROAD_ID.ToString(),
                         Text = c.BROAD_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarMediosDifusion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreEstablecimiento(decimal codigoEst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLEstablecimiento().ObtenerNombreEstablecimiento(GlobalVars.Global.OWNER, codigoEst);
                    if (datos != null && datos.EST_NAME != string.Empty)
                    {
                        retorno.valor = datos.EST_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Nombre de establecimiento no registrado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoCalificador()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoCalificador().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.QUA_ID.ToString(),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoCalificador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarGrupoFacturacion(decimal idSocio, decimal idGrupoFac)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLGrupoFacturacion().Listar(idSocio, idGrupoFac, GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.INVG_ID),
                        Text = c.INVG_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarGrupoFacturacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarAnioPlaneamiento(decimal licId)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicenciaPlaneamiento().ListarAnio(GlobalVars.Global.OWNER, licId)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.LIC_YEAR),
                        Text = Convert.ToString(c.LIC_YEAR)
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAnioPlaneamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarFormatoFacturacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLFormatoFacturacion().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.INVF_ID),
                        Text = c.INVF_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFormatoFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarEstadoLicencia(decimal idTipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_LIC_STAT().EstadoIniPorTipo(idTipo, GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.LICS_ID),
                        Text = c.LICS_NAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEstadoLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDerecho()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoDerecho().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.RIGHT_COD,
                         Text = c.RIGHT_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDerecho", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDescuento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoDescuento().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.DISC_TYPE.ToString(),
                         Text = c.DISC_TYPE_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerTemporalidad(decimal idTarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                DTOTemporalidad TemporalidadDTO = null;
                BLREC_RATE_FREQUENCY temp = new BLREC_RATE_FREQUENCY();


                if (!isLogout(ref retorno))
                {
                    var dato = temp.ObtenerXTarifa(idTarifa);
                    if (dato != null)
                    {
                        TemporalidadDTO = new DTOTemporalidad()
                        {
                            codigo = Convert.ToDecimal(dato.RAT_FID),
                            descripcion = dato.RAT_FDESC,
                        };
                    }
                    retorno.data = Json(TemporalidadDTO, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerTemporalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFormatoFactura()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLFormatoFactura().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.INVF_ID.ToString(),
                         Text = c.INVF_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFormatoFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTarifaCartacteristica()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCaracteristica().ListarTarifaCartacteristica(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CHAR_ID),
                         Text = c.CHAR_LONG
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTarifaCartacteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEnvioFacturacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoenvioFactura().Listar(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.LIC_SEND),
                        Text = c.LIC_FSEND
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEnvioFacturacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarPeriodicidad()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_RATE_FREQUENCY().ListarPeriodocidad(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.RAT_FID.ToString(),
                         Text = c.RAT_FDESC.ToUpper()
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarPeriodicidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarPlantillaTarifa()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTarifaPlantilla().ListarPlantilla(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.TEMP_ID.ToString(),
                         Text = c.TEMP_DESC.ToUpper()
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarPlantillaTarifa", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarLicenciaTab()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicenciaTabs().ListarLicenciaTab(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.TAB_ID),
                         Text = c.TAB_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLicenciaTab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoMoneda()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREF_CURRENCY().ListarTipoMoneda(GlobalVars.Global.OWNER)

                     .Select(c => new SelectListItem
                     {
                         Value = c.CUR_ALPHA,
                         Text = c.CUR_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoMoneda", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoproceso()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoProceso().Listar(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.PROC_TYPE),
                         Text = c.PROC_TDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoproceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarGrupoModalidad()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_MOD_GROUP().ListarTipo(GlobalVars.Global.OWNER, -2)//LISTAR TODOS (-2)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.MOG_ID),
                         Text = c.MOG_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarGrupoModalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarModulo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLModuloSistema().ListarModulo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.PROC_MOD),
                         Text = c.MOD_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarModulo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstadosLicencia()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLEstadoLicencia().ListarEstado(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.LICS_ID),
                         Text = c.LICS_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEstadosLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarAgenteRecaudo(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDivisionRecaudador().ValidarAgenteRecaudo(GlobalVars.Global.OWNER, Id);

                    if (datos != null)
                    {
                        retorno.Code = Convert.ToInt32(datos.BPS_ID);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "El agente seleccionado, no tiene perfil de agente de recaudo";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarAgenteRecaudo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarDivision(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDivisionRecaudador().ValidarDivision(GlobalVars.Global.OWNER, Id);

                    if (datos != null)
                    {
                        retorno.valor = "0";
                        retorno.message = "Ya se ha registrado agentes de recaudo para esta división";
                    }
                    else
                    {
                        //retorno.Code = Convert.ToInt32(datos.DAD_ID);
                        retorno.result = 1;
                    }
                }
                else
                {
                    //retorno.Code = Convert.ToInt32(datos.DAD_ID);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarDivision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRegla()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTarifaRegla().ListarRegla(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.CALR_ID),
                        Text = c.CALR_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarRegla", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFuncion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLFuncionCalculo().ListarDesplegable(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.FUNC_ID),
                         Text = c.FUNC_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFuncion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCuentaContable()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCuentasContables().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.LED_ID),
                         Text = c.LED_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCuentaContable", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarFechaPlanificacion(decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicenciaPlaneamiento().ListarFechaPlanificacion(GlobalVars.Global.OWNER, idLic)
                  .Select(c => new SelectListItem
                  {
                      Value = Convert.ToString(c.LIC_PL_ID),
                      Text = c.LIC_DATE.ToShortDateString()
                  });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFechaPlanificacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarFechaCaracteristicasLic(decimal idLic, decimal idLicPlan)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCaracteristica().ListarFechaCaractLicencia(GlobalVars.Global.OWNER, idLic, idLicPlan);

                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFechaCaracteristicasLic", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarDescuento(decimal idTipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDescuentos().ListarCombo(GlobalVars.Global.OWNER, idTipo)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DISC_ID),
                         Text = c.DISC_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarAgente()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAgente().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.LEVEL_ID),
                         Text = c.DESCRIPTION
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAgente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTarifaMantenimento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_RATES_GRAL().ListarCombo(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.RATE_ID),
                         Text = c.RATE_DESC.ToUpper()
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTarifaMantenimento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoComision()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoComision().Listar(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.COMT_ID.ToString(),
                         Text = c.COM_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTipoComision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarOrigenComision()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLOrigenComision().Listar(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.COM_ORG.ToString(),
                         Text = c.COM_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarOrigenComision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerPerfil(decimal codigoBps)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLSocioNegocio().ObtenerDatos(codigoBps, GlobalVars.Global.OWNER);
                    if (datos != null)
                    {
                        retorno.valor = datos.BPS_COLLECTOR.ToString();
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Nombre de establecimiento no registrado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerPerfil", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTarifaModalidad(decimal idMod)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_RATES_GRAL().ListarComboModalidad(GlobalVars.Global.OWNER, idMod)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.RATE_ID),
                         Text = c.RATE_DESC.ToUpper()
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarTarifaModalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTemporalidadPorModalidad(decimal idModalidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLComisionAgenteRecaudo().ListarTemporalidadporModalidad(GlobalVars.Global.OWNER, idModalidad)
                     .Select(c => new SelectListItem
                     {
                         Value = c.RAT_FID.ToString(),
                         Text = c.RAT_FDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTemporalidadPorModalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarOficinasComerciales()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new DAComisionOficinasComerciales().ListarOficinas(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = c.OFF_ID.ToString(),
                         Text = c.OFF_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarOficinasComerciales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSucursalesBancariasSegunBanco(decimal IdBanco)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new DAREC_BANKS_BRANCH().ListarSucursalesPorBanco(GlobalVars.Global.OWNER, IdBanco)
                .Select(c => new SelectListItem
                {
                    Value = c.BRCH_ID,
                    Text = c.BRCH_NAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSucursalesBancariasSegunBanco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreTarifa(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_RATES_GRAL().ObtenerNombreTarifa(GlobalVars.Global.OWNER, Id);
                    if (datos != null)
                    {
                        retorno.valor = string.Format("{0} ", datos.RATE_DESC);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Descripción de la tarifa no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreTarifa", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Js para validar sesion y realizar alguna operacion en el JS
        public JsonResult JsonValidarSesion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    retorno.result = 1;
                    retorno.message = "sesion activa";
                    retorno.isRedirect = false;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "JsonValidarSesion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreOficina(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLOffices().ObtenerNombre(GlobalVars.Global.OWNER, Id);
                    if (datos != null)
                    {
                        retorno.valor = datos.OFF_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Descripción de la oficina no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarNombreModulo()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLModuloCliente().ListarNombre(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.PROC_MOD.ToString(),
                    Text = c.MOD_DESC
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarNombreModulo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoObjeto()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_OBJECTS_TYPE().ListarTipoObjeto(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_OTID.ToString(),
                    Text = c.WRKF_OTDESC
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoObjeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoAgente()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLRolAgente().ListarCombo(GlobalVars.Global.PREFIJO)
                .Select(c => new SelectListItem
                {
                    Value = c.CodigoPerfil.ToString(),
                    Text = c.Nombre
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoObjeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoEstado(decimal IdCicloAprob)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATE_TYPE().ListarItemTiposEstados(GlobalVars.Global.OWNER, IdCicloAprob)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_STID.ToString(),
                    Text = c.WRKF_STNAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstadorPorWorkFlow(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATES().ListarEstadosPorWorkFlow(GlobalVars.Global.OWNER, Id)
                    .Select(c => new SelectListItem
                    {
                        Value = c.WRKF_SID.ToString(),
                        Text = c.WRKF_SLABEL
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstadoXTipo(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATES().ListarItemEstadosPorTipo(GlobalVars.Global.OWNER, Id)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_SID.ToString(),
                    Text = c.WRKF_SNAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEstadoXTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCicloAprobacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_WORKFLOWS().ListarItems(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_ID.ToString(),
                    Text = c.WRKF_NAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCicloAprobacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstadoXTipoWrkf()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATE_TYPE().ListarTiposEstados(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_STID.ToString(),
                    Text = c.WRKF_STNAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEstadoXTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEventos()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_EVENTS().ListarItems(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_EID.ToString(),
                    Text = c.WRKF_ENAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCicloAprobacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoAccion()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_ACTION_TYPES().ListarItem(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_ATID.ToString(),
                    Text = c.WRKF_ATNAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoAccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDato()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_DATA_TYPES().ListarItem(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_DTID.ToString(),
                    Text = c.WRKF_DTNAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDato", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProceso()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLProceso().ListarItem(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.PROC_ID.ToString(),
                    Text = c.PROC_NAME
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarProceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreTransicion(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_WORKF_TRANSITIONS().ObtenerCicloTransitions(GlobalVars.Global.OWNER, id);
                    if (datos != null && datos.WRKF_NAME != string.Empty)
                    {
                        //retorno.valor = datos.WRKF_TID.ToString();
                        retorno.valor = datos.WRKF_ENAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Ciclo de la transición no registrado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreAccion(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_WORKF_ACTIONS().Obtener(GlobalVars.Global.OWNER, Id);
                    if (datos != null)
                    {
                        retorno.valor = datos.WRKF_ANAME.ToUpper();
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Acción no registrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreAccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> ListarEventosWorkf()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var evento = new BL_WORKF_ACTIONS_MAPPINGS().ListarEvento(GlobalVars.Global.OWNER).ToList();
            foreach (var item in evento)
            {
                items.Add(new SelectListItem
                {
                    Value = item.WRKF_EID.ToString(),
                    Text = item.WRKF_ENAME
                });
            }

            return items;
        }

        public List<SelectListItem> ListarRolesEntidadesEst()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var roles = new BLREF_ROLES().Get_REF_ROLES().ToList();

            foreach (var item in roles)
            {
                items.Add(new SelectListItem
                {
                    Value = item.ROL_ID.ToString(),
                    Text = item.ROL_DESC
                });
            }
            return items;
        }

        //[HttpPost]
        //public JsonResult ListarEventosWorkf()
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            var datos = new BL_WORKF_ACTIONS_MAPPINGS().ListarEvento(GlobalVars.Global.OWNER)
        //             .Select(c => new SelectListItem
        //             {
        //                 Value = Convert.ToString(c.WRKF_EID),
        //                 Text = c.WRKF_ENAME
        //             });
        //            retorno.result = 1;
        //            retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEventosWorkf", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public List<SelectListItem> ListarBloqueoItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var bloqueos = new BLBloqueos().Listar(GlobalVars.Global.OWNER).ToList();
            foreach (var item in bloqueos)
            {
                items.Add(new SelectListItem
                {
                    Value = item.BLOCK_ID.ToString(),
                    Text = item.BLOCK_DESC
                });
            }

            return items;

        }

        public List<SelectListItem> ListarTipoPagoItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var bloqueos = new BLREC_PAYMENT_TYPE().ListarTipo(GlobalVars.Global.OWNER).ToList();
            foreach (var c in bloqueos)
            {
                items.Add(new SelectListItem
                {
                    Value = Convert.ToString(c.PAY_ID),
                    Text = c.DESCRIPTION
                });
            }

            return items;
        }

        public List<SelectListItem> ListaTransicionEstados(decimal Id, decimal Idestado)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var estados = new BL_WORKF_STATES().ListaTransicionEstados(GlobalVars.Global.OWNER, Id, Idestado).ToList();
            foreach (var c in estados)
            {
                items.Add(new SelectListItem
                {
                    Value = Convert.ToString(c.WRKF_TID),
                    Text = c.WRKF_LABEL
                });
            }

            return items;
        }

        [HttpPost]
        public JsonResult ListaEstadosWF(decimal idWorkFlow)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATES().ListarEstadosPorWorkFlow(GlobalVars.Global.OWNER, idWorkFlow)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_SID.ToString(),
                    Text = c.WRKF_SLABEL
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaEstadosWF", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaWorkFlowEstado()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BL_WORKF_STATES().ListaWorkFlowEstado(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = c.WRKF_SID.ToString(),
                    Text = c.WRKF_SLABEL
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaWorkFlowEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMetodoPago()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLMetodoPago().ListarMetodoPago(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.REC_PWID),
                         Text = c.REC_PWDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarMetodoPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCuentaBancaria(string sucbnkId)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_BANKS_BRANCH().ListarCuentaBancaria(GlobalVars.Global.OWNER, sucbnkId)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.BPS_ACC_ID),
                         Text = c.BACC_NUMBER
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCuentaBancaria", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerEstadoInicial(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var estadoIni = new BL_WORKF_STATES().ObtenerEstadoInicial(GlobalVars.Global.OWNER, id);
                    if (estadoIni > 0)
                    {
                        retorno.valor = Convert.ToString(estadoIni);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "No se encontró estado inicial para el Workflow";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerEstadoInicial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaSerie()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_NUMBERING().ListarSerie(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.NMR_ID.ToString(),
                        Text = c.NMR_SERIAL
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListaSerie", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> ListarAccionPreRequisito(decimal Idwrk, decimal Idst, decimal? wrkfaId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var datos = new BL_WORKF_ACTIONS_MAPPINGS().ListarPrerrequisito(GlobalVars.Global.OWNER, Idwrk, Idst, wrkfaId).ToList();

            foreach (var item in datos)
            {
                items.Add(new SelectListItem
                {
                    Value = item.WRKF_AIDAUXId.ToString(),
                    Text = item.WRKF_AID.ToString()
                });
            }
            return items;
        }

        //public List<SelectListItem> ListarParametroTabla(decimal idTipo, string referencia)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    var datos = new BL_WORKF_PARAMETERS().ListarParametroTabla(idTipo, referencia).ToList();

        //    foreach (var item in datos)
        //    {
        //        items.Add(new SelectListItem
        //        {
        //            Value = item.WRKF_PVALUE.ToString(),
        //            Text = item.WRKF_PNAME
        //        });
        //    }
        //    return items;
        //}

        public JsonResult ListarParametroTransicion(decimal idTipo, string referencia)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_WORKF_PARAMETERS().ListarParametroTransicion(idTipo, referencia)
                    .Select(c => new SelectListItem
                    {
                        Value = c.WRKF_PVALUE,
                        Text = c.WRKF_PNAME
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarParametroTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDescuentoXTarifa(decimal idTipo, decimal idTarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLDescuentos().DescuentoPorTarifa(GlobalVars.Global.OWNER, idTipo, idTarifa)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DISC_ID),
                         Text = c.DISC_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarTipoReporte()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoReporte().Obtener(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.REPORT_TYPE),
                         Text = c.RPT_DESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtienNombreShow(decimal idRep)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    retorno.valor = "-";
                    var report = new BLLicenciaReporte().Obtener(GlobalVars.Global.OWNER, idRep);
                    DTOreporte objEnte = new DTOreporte();
                    if (report != null)
                    {

                        var dato = new BLShow().ObtenerShow(GlobalVars.Global.OWNER, Convert.ToDecimal(report.SHOW_ID));
                        if (dato != null) retorno.valor = Convert.ToString(dato.SHOW_NAME);

                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtienNombreShow", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Actualizar 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public JsonResult ActualizarNombreDoc(string nombre, decimal idDoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    retorno.valor = "-";
                    var result = new BLDocumentoGral().UpdatePath(GlobalVars.Global.OWNER, idDoc, nombre);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarNombreDoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoFactura()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLFactura().ListarTipoFactura(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = Convert.ToString(c.INV_TYPE),
                    Text = c.INVT_DESC
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFortmatoFacturacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLFormatoFactura().FormatoFacturacion(GlobalVars.Global.OWNER)
                .Select(c => new SelectListItem
                {
                    Value = Convert.ToString(c.INVF_ID),
                    Text = c.INVF_DESC
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFortmatoFacturacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDropCentroContacto()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLCentroContacto().ListarDropCentroContacto(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.CONC_ID),
                        Text = c.CONC_NAME
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDropCentroContacto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaDropTipoCampania()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLTipoCampania().ListaDropTipoCampania(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.CONC_CTID),
                        Text = c.CONC_CTNAME
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDropTipoCampania", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaDropCampaniaContacto()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLCampaniaCallCenter().ListaDropCampaniaContacto(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CONC_CSTATUS,
                        Text = c.CONC_CSTATUS_DESC
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDropCampaniaContacto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaDropEntidades()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new DAEntidades().ListaDropEntidades(GlobalVars.Global.OWNER)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ENT_ID),
                        Text = c.ENT_DESC
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDropEntidades", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDivisioneXtipo(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLREF_DIVISIONES().ListarDivisioneXtipo(GlobalVars.Global.OWNER, tipo)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.DAD_ID),
                        Text = c.DAD_NAME
                    });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDivisioneXtipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSerieXtipo(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_NUMBERING().ListarSerieXtipo(GlobalVars.Global.OWNER, tipo)
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.NMR_ID),
                        Text = c.NMR_SERIAL
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSerieXtipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerCorrelativoXtipo(string tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_NUMBERING().ObtenerCorrelativoXtipo(GlobalVars.Global.OWNER, tipo);
                    if (datos != null)
                    {
                        retorno.valor = datos.NMR_NOW.ToString();
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Correlativo no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerCorrelativoXtipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> ListarAgenteRecaudador(decimal? IdCampania)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var evento = new BLCampaniaCallCenter().ListarAgenteRecaudador(GlobalVars.Global.OWNER, IdCampania).ToList();
            foreach (var item in evento)
            {
                items.Add(new SelectListItem
                {
                    Value = item.BPS_ID.ToString(),
                    Text = item.BPS_NAME
                });
            }

            return items;
        }

        [HttpPost]
        public JsonResult ListaPerPlanFact(decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<SelectListItem> datos = new List<SelectListItem>();
                var lista = new BLLicenciaPlaneamiento().ListarPeriodoPlanificacion(GlobalVars.Global.OWNER, idLic);
                foreach (var item in lista)
                {
                    datos.Add(new SelectListItem
                    {
                        Value = item.Value,
                        Text = item.Text
                    });
                }
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaPerPlanFact", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExisteCaracteristicasXLic(decimal idLic, decimal idLicPlan)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCaracteristica().ListarFechaCaractLicencia(GlobalVars.Global.OWNER, idLic, idLicPlan);
                    retorno.Code = datos != null && datos.Count == 0 ? 0 : 1;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ExisteCaracteristicasXLic", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCentroContacto()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCampaniaCallCenter().ListarCentroContacto(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CONC_ID),
                         Text = c.CONC_NAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCentroContacto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCampaniaPorTipo(decimal idTipoCampania)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLCampaniaCallCenter().ListarCampaniaPorTipo(GlobalVars.Global.OWNER, idTipoCampania)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CONC_CID),
                         Text = c.CONC_CNAME
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCampaniaPorTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarLoteAgenteXcampania(decimal IdCampania)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLoteTrabajo().ListaLoteAgente(GlobalVars.Global.OWNER, IdCampania)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.CONC_SID),
                         Text = c.LOTEAGENTE
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLoteAgenteXcampania", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtieneCodigoTipoEspecial()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt16(System.Web.Configuration.WebConfigurationManager.AppSettings["idTipoEspecialDscto"]);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneCodigoTipoEspecial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCuentaBancariaXbanco(decimal IdBanco, string moneda)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_BANKS_BRANCH().ListarCuentaBancariaXBanco(GlobalVars.Global.OWNER, IdBanco, moneda)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.BPS_ACC_ID),
                         Text = c.BACC_NUMBER
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCuentaBancariaXbanco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public JsonResult ObtenerTarifaHistorica(decimal IdTarifa, decimal periodo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_RATES_GRAL servicio = new BLREC_RATES_GRAL();
                    BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL();
                    //var datos = new BLREC_RATES_GRAL().ObtenerTarifaHistorica(GlobalVars.Global.OWNER, IdTarifa, periodo);
                    tarifa = servicio.ObtenerTarifaHistorica(GlobalVars.Global.OWNER, IdTarifa, periodo);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(tarifa, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerTarifaHistorica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarUbigeoXOficia(int ubigeo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int oficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina].ToString());
                    var datos = new BLDivision().ValidarUbigeoXOficina(GlobalVars.Global.OWNER, oficina, ubigeo);
                    if (datos >= 1)
                    {
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = Constantes.MensajeRetorno.NO_TERRITORIO;
                        retorno.message = System.Configuration.ConfigurationManager.AppSettings["MSG_VAL_UBIGEO"];
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarUbigeoXOficia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Reportes
        //David 
        public JsonResult ValidarOficinaReporte()
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    int oficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina].ToString());
                    var datos = new BLREGISTRO_CAJA().ValidarReporteOficina(oficina);
                    if (datos == 1)
                    {
                        retorno.result = 1;
                        retorno.Code = oficina;
                    }
                    else
                    {
                        //retorno.result = Constantes.MensajeRetorno.NO_TERRITORIO;
                        //retorno.message = System.Configuration.ConfigurationManager.AppSettings["MSG_VAL_UBIGEO"];
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarUbigeoXOficia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //David  VALIDA COMBO
        public JsonResult ValidarOficinaReporteDL()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int oficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina].ToString());
                    var datos = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina);
                    if (datos == 1)
                    {
                        retorno.result = 1;
                        retorno.Code = oficina;
                    }
                    else
                    {
                        retorno.result = 0;
                        //retorno.result = Constantes.MensajeRetorno.NO_TERRITORIO;
                        //retorno.message = System.Configuration.ConfigurationManager.AppSettings["MSG_VAL_UBIGEO"];
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarUbigeoXOficia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //David
        #region Descuentos
        public JsonResult ListarDescuentosxTipoDesc(decimal disctype)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    var datos = new BLDescuentos().ListaDescuentosxTipoDesc(disctype)
                     .Select(c => new BEDescuentos
                     {

                         DISC_ID = c.DISC_ID,
                         DISC_NAME = c.DISC_NAME,
                         DISC_VALUE = c.DISC_VALUE,
                         DISC_SIGN = c.DISC_SIGN
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);


            }


            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        //Busca la Descripcion del Id Descuento PLANTILLA para mostrar
        public JsonResult ObtenerNombreDescuentoPlantilla(decimal codigoDesc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var owner = GlobalVars.Global.OWNER;
                    var datos = new BLDescuentos().listaDescuentoPlantillasinPaginado(owner, codigoDesc);
                    if (datos != null)
                    {
                        retorno.valor = datos[0].TEMP_DESC;//solo devuelve un valor AL encontrar el ID
                        //retorno.valor =  datos.BPS_NAME;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se Encontro Dato";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreSocio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #endregion





        [HttpPost]
        public JsonResult ListarValoresConfiguracion(string tipo, string subTipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLMultiRecibo().ValoresConfig(tipo, subTipo)
                     .Select(c => new SelectListItem
                     {
                         Value = c.VALUE,
                         Text = c.VDESC
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarValoresConfiguracion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ACSucursalesBancarias()
        {
            string texto = Request.QueryString["term"];
            decimal idBanco = Convert.ToDecimal(Request.QueryString["idBanco"]);
            var datos = new DAREC_BANKS_BRANCH().ListarSucursalesPorNombre(GlobalVars.Global.OWNER, idBanco, texto);
            //var datos = new DAREC_BANKS_BRANCH().ListarSucursalesPorNombre(GlobalVars.Global.OWNER, 27, "ATE");
            List<DTOSocio> socios = new List<DTOSocio>();
            datos.ForEach(x =>
            {
                socios.Add(new DTOSocio
                {
                    Codigo = x.ID,
                    value = String.Format("{0}", x.BRCH_NAME)
                });
            });
            return Json(socios, JsonRequestBehavior.AllowGet);
        }

        //OFICINA
        public JsonResult ListarNumeradores(int skip, int take, int page, int pageSize, string group, string owner, string dato, int st, string serie, string tipoSerie)
        {
            Init();//add sysseg
            //var lista = CorrelativosListarPag(GlobalVars.Global.OWNER, dato, st, serie, page, pageSize);
            var lista = new BLREC_NUMBERING().ListarNumeradores(GlobalVars.Global.OWNER, dato, st, serie, tipoSerie, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_NUMBERING { RECNUMBERING = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ListaTipoNumerador()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoNumerador().ListarTipoNum(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.NMR_TYPE),
                         Text = Convert.ToString(c.NMR_TDESC)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaTipoNumerador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaDivisionesXOficina(decimal IdOficina)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAgente().ObtenerDivXOficina_Deplegable(IdOficina, GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ID_COLL_DIV),
                         Text = Convert.ToString(c.DIV_DESCRIPTION)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDivisionesXOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerNombreTarifaCombo(decimal codModalidad, decimal? codTemp, decimal? codtarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_RATES_GRAL tarifa = new BLREC_RATES_GRAL();
                    var datos = tarifa.obtenerTarifaAsociada(codModalidad, codTemp);
                    if (datos != null)
                    {
                        datos
                          .Select(c => new SelectListItem
                          {
                              Value = Convert.ToString(c.RATE_ID),
                              Text = c.RATE_DESC
                          });
                        retorno.result = 1;
                        retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.valor = "0";
                        retorno.message = "Obtener Tarifa Asociada";
                    }
                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerTarifaAsociada", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaTipoNotaCredito()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoNotaCredito().ListarTipoNotaCredito(GlobalVars.Global.OWNER)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.Code_Description),
                         Text = Convert.ToString(c.Description)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaTipoNumerador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaTipoFacturacionManual()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLFactura().ListaTipoFacturacionManual()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.VALUE),
                         Text = Convert.ToString(c.VDESC)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaTipoFacturacionManual", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarSubTipoDivisiones(decimal idDivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //return new DADivisiones().ListarSubTipoDivisiones(idDivision);
                    var datos = new BLDivision().ListarSubTipoDivisiones(idDivision).OrderBy(x => x.DAD_BELONGS)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DAD_STYPE),
                         Text = Convert.ToString(c.DAD_NAME)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubTipoDivisiones", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarValoresXsubtipo_Division(decimal idDivision, decimal idSubTipo, decimal idBelong)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //return new DADivisiones().ListarSubTipoDivisiones(idDivision);
                    var datos = new BLDivision().ListarValoresXsubtipo_Division(idDivision, idSubTipo, idBelong)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.DADV_ID),
                         Text = Convert.ToString(c.DAD_VNAME)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarValoresXsubtipo_Division", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListaContableDesplegable()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //return new DADivisiones().ListarSubTipoDivisiones(idDivision);
                    var datos = new BLContable().ListaContableDesplegable()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ACCOUNTANT_ID),
                         Text = Convert.ToString(c.ACCOUNTANT_DESC)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaContableDesplegable", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarSubTipoParametro(decimal idTIpoParametro)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLTipoParametro().ObtenerListaSubTipoParametro(GlobalVars.Global.OWNER, idTIpoParametro)
                    .Select(c => new SelectListItem
                    {
                        //Value = Convert.ToString(c.PAR_SUBTYPE),
                        Value = Convert.ToString(c.PAR_SUBTYPE_DESC),
                        Text = c.PAR_SUBTYPE_DESC
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarSubTipoParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarFiltroOrdenConsultaDocumento()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //return new DADivisiones().ListarSubTipoDivisiones(idDivision);
                    var datos = new BLFiltroOrden().listar()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.ID_VALUE),
                         Text = Convert.ToString(c.DESCRIPCION)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaContableDesplegable", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarDocumentoCobro(decimal INV_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int exito = new BLConsultaDocumento().ValidaDocumentoCobro(Convert.ToInt32(INV_ID));
                    if (exito == 0)
                    {
                        retorno.result = 1;
                        retorno.message = "El documento no tiene ningun cobro";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Documento se encuentra con un cobro.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidacionDNI", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Valida_Fecha_Factura_Para_NC(decimal INV_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int exito = new BLConsultaDocumento().Valida_Fecha_Factura_Para_NC(Convert.ToInt32(INV_ID));
                    if (exito == 1)
                    {
                        retorno.result = 1;
                        retorno.message = "La fecha es correcta";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "La fecha de emision es mayor a la actual";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidaFecha", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTipoDocAlfresco()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    BEREC_DOCUMENT_TYPE ent = new BEREC_DOCUMENT_TYPE();
                    ent = new BEREC_DOCUMENT_TYPE();
                    ent.DOC_TYPE = 0;
                    ent.DOC_OBSERV = "---";
                    ent.DOC_DESC = "-----SELECCIONE-----";

                    var datos = new BLREC_DOCUMENT_TYPE().ListarComboTipoDocAlfresco(GlobalVars.Global.OWNER);
                    datos.Add(ent);
                    var datos2 = datos.OrderBy(x => x.DOC_TYPE).Select(c => new BESelectListItem
                    {
                        Value_Alfresco = c.DOC_OBSERV,
                        Value = Convert.ToString(c.DOC_TYPE),
                        Text = c.DOC_DESC

                    });
                    retorno.result = 1;
                    retorno.data = Json(datos2, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Lista_Artista_X_Licencia(int Cod_lic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAlfresco().Lista_Artista_X_Licencia(Cod_lic)
                     .Select(c => new BESelectListItem
                     {
                         Value = c.Value,
                         Text = c.Text

                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoDoc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarPlaneamientoxLicenciaOpcion(decimal CodigoLicencia, int Opcion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicenciaReporteDeta().ListarPlaneamientoxLicenciaOpcion(CodigoLicencia, Opcion)
                     .Select(c => new BESelectListItem
                     {
                         Value = c.VALOR,
                         Text = c.DESCRIPCION

                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarPlaneamientoLicenciaxOpcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}