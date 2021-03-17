using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;


namespace Proyect_Apdayc.Controllers
{
    public class DescuentoAprobacionController : Base
    {
        // GET: DescuentoAprobacion
        public ActionResult Index()
        {
            Init(false);
            return View();
        }


        public JsonResult LISTAR_DESCUENTOS_APROBACIONES_JSONPAGE(int skip, int page, int pageSize, decimal LIC_ID,string NOM_LIC,int OFI_ID ,int EST_DESC ,string NOM_DESC,int CON_FECHA,string fecha_inicio ,string fecha_fin)
        {
            Resultado retorno = new Resultado();
            List<BEDescuentos> lista = null;
            try
            {
                DateTime fecha_ini = Convert.ToDateTime(fecha_inicio);
                DateTime fecha_final = Convert.ToDateTime(fecha_fin);
                lista = new BLLicenciaDescuento().listaDescuentosxAprobar(page, pageSize,LIC_ID, NOM_LIC, OFI_ID, EST_DESC, NOM_DESC, CON_FECHA, fecha_ini, fecha_final);
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            var tot = lista.Select(s => s.CANTIDAD).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDescuentos { Descuentos = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDescuentos { Descuentos = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerDescuentoPanel(decimal DISC_ID,decimal LIC_ID )
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;

                    BEDescuentos dato = new BLLicenciaDescuento().ObtieneDescuento_Panel(owner, DISC_ID, LIC_ID);

                    retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    retorno.message = "OK";


                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        public JsonResult ActualizaDescuentoPanel(decimal DISC_ID, int estado, string observ_respuesta,decimal LIC_ID)
        {
            Resultado retorno = new Resultado();


            try
            {
                if (!isLogout(ref retorno))
                {
                    string Usuario = UsuarioActual;
                    new BLLicenciaDescuento().ActualizaDescuentoLicencia_Panel(DISC_ID, estado, observ_respuesta, LIC_ID);

                    if (estado == 1)
                    {
                        try
                        {
                            DescuentoController entidadDescuentoAprobacion = new DescuentoController();
                            var resp = entidadDescuentoAprobacion.ActualizarMontoCalculadoDescuentoLicencia(LIC_ID);
                        }catch(Exception ex)
                        {

                        }
                    }

                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {

                retorno.result = 0;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}