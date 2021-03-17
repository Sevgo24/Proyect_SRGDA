using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.BL.Reporte;
using SGRDA.DA;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.AdministracionControlLicenciasCreadas
{
    public class AdministracionControlLicenciasController : Base
    {
        // GET: AdministracionControlLicencias

        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const bool VERDADERO = true;
            public const bool FALSO = false;
            public const string MSJ_ERROR_AL_LISTAR = "SE PRODUJO UN ERROR AL LISTAR LAS LICENCIAS | POR FAVOR BRINDE LOS PARAMETROS DE CONSULTA Y CONTACTE AL ADMINISTRADOR";
            public const string MSJ_ACTUALIZACION_CORRECTA = "SE ACTUALIZO CORRECTAMENTE LA LICENCIA ";
            public const string MSJ_ACTUALIZACION_NO_CORRECTA = "SE PRODUJO UN ERROR AL ACTUALIZAR LA LICENCIA | CONTACTE CON EL ADMINSITRADOR Y BRINDE LOS PARAMETROS DE ACTUALIZACION (CODIGO DE LICENCIA )";
            public const string MSJ_OFICINA_SIN_PERMISO = "LA OFICINA NO CUENTA CON PERMISOS PARA REALIZAR MODIFICACIONS | SI ESTE ES UN ERROR COMUNIQUESE CON EL ADMINISTRADOR";
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LIC_ID"></param>
        /// <param name="OFF_ID"></param>
        /// <param name="CON_FECHA"></param>
        /// <param name="FECHA_INICIAL"></param>
        /// <param name="FECHA_FIN"></param>
        /// <returns></returns>
        public JsonResult ListarControlLicencias(decimal LIC_ID,decimal OFF_ID  ,int    CON_FECHA,string FECHA_INICIAL,string FECHA_FIN,int ESTADO)
        {
            Resultado retorno = new Resultado();

            try
            {
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));




                if (opcAdm == Variables.NO) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    OFF_ID = oficina;

                var lista = new BLAdministracionControlLicencia().ListaControlLicencias( LIC_ID, OFF_ID, CON_FECHA, FECHA_INICIAL, FECHA_FIN, ESTADO);

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO LICENCIA</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >PRIMER PERIODO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE CREACION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE MODIFICACION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE CARGA DE FICHA</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Opciones</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver.</th>");
                if (lista != null)
                {
                    lista.ForEach(item =>
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.LIC_ID);
                        shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.BPS_NAME);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.MONTO_LIRICS_BRUTO);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.PERIODO_DESCRIPCION);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OFICINA);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FECHA_DESCRIPCION);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LIC_AUT_START); //FECHA DE MODIFICACION DE LICENCIA
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LIC_AUT_END); // FECHA DE 
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.LIC_ID, "Aprobar Control","Rechazar Control");
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerLicenciVentanaNueva({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.LIC_ID, "Ver");
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
            catch(Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSJ_ERROR_AL_LISTAR;
            }

            return Json(retorno,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LIC_ID"></param>
        /// <returns></returns>
        public JsonResult ActualizarLicenciaLocal(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {

                bool resp = new BLAdministracionControlLicencia().ActualizaLicenciaAprobacionLocales(LIC_ID);

                if (resp)
                {
                    retorno.result = Variables.SI;
                }
                else
                {
                    retorno.message = Variables.MSJ_OFICINA_SIN_PERMISO;
                    retorno.result = Variables.NO;
                }

            }catch(Exception ex)
            {
                retorno.result = Variables.NO;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  ACTUALIZA EL ESTADO DE LA LICENCIA APROBA =1 RECHAZADA =0
        /// </summary>
        /// <param name="LIC_ID"></param> CODIGO DE LICENCIA
        /// <param name="ESTADO"></param>ESTADO
        /// <returns></returns> 
        public JsonResult ActualizarLicenciaEstadoAprob(decimal LIC_ID,int ESTADO)
        {
            Resultado retorno = new Resultado();
            try
            {
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));
                bool resp = Variables.FALSO;
                if (opcAdm == Variables.SI)
                    resp = new BLAdministracionControlLicencia().ActualizaLicenciaEstadoAprobacion(LIC_ID, ESTADO);

                if (resp)
                {
                    retorno.result = Variables.SI;
                    retorno.message = Variables.MSJ_ACTUALIZACION_CORRECTA;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSJ_ACTUALIZACION_NO_CORRECTA;
                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSJ_ACTUALIZACION_NO_CORRECTA;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}