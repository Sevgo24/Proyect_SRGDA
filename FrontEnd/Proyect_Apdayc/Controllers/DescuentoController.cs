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


namespace Proyect_Apdayc.Controllers
{
    public class DescuentoController : Base
    {
        //
        // GET: /Descuento/
        // public const string UsuarioActual = "klescano";
        //  public const string nomAplicacion = "SRGDA";
        #region clases
        /// <summary>
        /// CLASE DONDE SE ENCUENTRA LAS VARIABLES A UTILIZAR PARA LOS DESCUENTOS DE LA PLANTILLA
        /// </summary>
        public class DescPlantilla
        {
            #region Generales
            public const int ES_MEGACONCIERTO = 1;
            #endregion
            #region DESCUENTOS UIT
            public const int TRIBUNA_VIP_UIT = 56;
            public const int ARTISTA_VIP_UIT = 58;
            public const int ARTISTA_GENERAL_UIT = 54;
            #endregion

            #region MEGACONCIERTO-GENERAL
            //***ID DE LOS DESCUENTOS
            public const int ID_TRIBUNA_APDAYC_GENERAL = 39;
            public const int ID_PRONTO_PAGO_GENERAL = 41;
            public const int ID_ARTISTA_NACIONAL_GENERAL = 42;
            public const int ID_ENTRADA_CORTESIA_GENERAL = 43;
            //ID DE LAS CARACTERISTICAS
            public const int ID_CARACT_TRIBUNA = 81;
            public const int ID_CARACT_PRODUCCION = 80;
            public const int ID_CARACT_TAQUILLA = 79;
            //VALORES CARACTERISTICAS
            public const int VAL_TRIBUNA_GENERAL = 0;
            #endregion

            #region VIENEDELBLFACTURA
            /// <summary>
            /// SI = 1
            /// </summary>
            public const int SI = 1;
            /// <summary>
            /// NO = 0
            /// </summary>
            public const int NO = 0;
            /// <summary>
            /// ANUAL =12
            /// </summary>
            public const int ANUAL = 12;
            /// <summary>
            /// SEMESTRAL = 6
            /// </summary>
            public const int SEMESTRAL = 6;
            /// <summary>
            /// ABIERTO=A
            /// </summary>
            public const string ABIERTO = "A";
            /// <summary>
            /// PENDIENTE=P
            /// </summary>
            public const string PENDIENTE = "P";
            /// <summary>
            /// DESCUENTO PARA USUARIOS GENERALES DE LOCALES PERMANENTES (NO CADENAS) //DISC_ID
            /// </summary>
            public const int DESCUENTO_INDIVIDUAL = 35;
            /// <summary>
            /// USUARIOS DE MUSICA INDISPENSABLE
            /// </summary>
            public const int MUSICA_INDISPENSABLE = 0;
            #endregion
        }

        public class DescuentoAprobaciones
        {
            public const int aprobado = 1;
            public const int rechazado = 2;
            public const int pendiente = 0;
        }

        #endregion


        private const string K_SESSION_DESCUENTO = "___DTODescuento";
        private const string K_SESSION_DESCUENTO_DEL = "___DTODescuentoDEL";
        private const string K_SESSION_DESCUENTO_ACT = "___DTODescuentoACT";
        public static string K_SESSION_DESCUENTO_SOCIO = "__DTODescuentoSocio";
        public static string K_SESSION_DESCUENTO_SOCIOCOMP = "__DTODescuentoSocioComp";
        List<DTODescuento> Descuento = new List<DTODescuento>();

        private List<DTODescuento> DescuentoTmpUPDEstado
        {
            get
            {
                return (List<DTODescuento>)Session[K_SESSION_DESCUENTO_ACT];
            }
            set
            {
                Session[K_SESSION_DESCUENTO_ACT] = value;
            }
        }
        private List<DTODescuento> DescuentoTmpDelBD
        {
            get
            {
                return (List<DTODescuento>)Session[K_SESSION_DESCUENTO_DEL];
            }
            set
            {
                Session[K_SESSION_DESCUENTO_DEL] = value;
            }
        }
        public List<DTODescuento> DescuentoTmp
        {
            get
            {
                return (List<DTODescuento>)Session[K_SESSION_DESCUENTO];
            }
            set
            {
                Session[K_SESSION_DESCUENTO] = value;
            }
        }
        //Lista DescuentoSocio Temporarl
        public List<DTODescuentoSocio> DescuentoSocioTmp
        {
            get
            {
                return (List<DTODescuentoSocio>)Session[K_SESSION_DESCUENTO_SOCIO];
            }
            set
            {
                Session[K_SESSION_DESCUENTO_SOCIO] = value;
            }
        }

        //Lista DescuentoSocio Temporarl para Poder Insertar
        public List<DTODescuentoSocio> DescuentoSocioComaparTmp
        {
            get
            {
                return (List<DTODescuentoSocio>)Session[K_SESSION_DESCUENTO_SOCIOCOMP];
            }
            set
            {
                Session[K_SESSION_DESCUENTO_SOCIOCOMP] = value;
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
            Session.Remove(K_SESSION_DESCUENTO);
            Session.Remove(K_SESSION_DESCUENTO_DEL);
            Session.Remove(K_SESSION_DESCUENTO_ACT);
            Session.Remove(K_SESSION_DESCUENTO_SOCIO);
            Session.Remove(K_SESSION_DESCUENTO_SOCIOCOMP);
            return View();
        }



        public JsonResult ListarDescuentos(decimal idLic, decimal valorTestTarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var cantDecimales = this.getCantDecimal((double)valorTestTarifa);
                    BLLicenciaDescuento servicio = new BLLicenciaDescuento();
                    BEDescuentos en = new BEDescuentos();
                    en.Descuentos = servicio.ListaDescuentos(GlobalVars.Global.OWNER, idLic);


                    if (en.Descuentos != null)
                    {
                        Descuento = new List<DTODescuento>();
                        en.Descuentos.ForEach(s =>
                        {
                            Descuento.Add(new DTODescuento
                            {
                                IdLicDesc = s.LIC_DISC_ID,
                                Orden = s.ORDEN,
                                DesOrigen = s.DISC_ORG_DESC,
                                Origen = Convert.ToInt32(s.ORIGEN),
                                Id = s.DISC_ID,
                                Tipo = s.TIPO,
                                Descuento = s.DISC_NAME,
                                Formato = s.FORMATO,
                                Valor = s.DISC_VALUE,
                                Base = valorTestTarifa,// s.BASE,
                                    EnBD = true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                UsuarioModifica = s.LOG_USER_UPDATE,
                                FechaModifica = s.LOG_DATE_UPDATE,
                                Activo = s.ENDS.HasValue ? false : true,
                                Aplicable = s.DISC_ORG,
                                esSuma = s.DISC_SIGN == "+" ? true : false,
                                esAutomatico = s.IS_AUTO,
                                LicId = idLic,
                                Observacion = s.OBSERVACION,
                                DISC_ESTADO = s.DISC_ESTADO,
                                DISC_REPS_OBSERVACION = s.DISC_RESP_OBSERVACION

                            });
                        });

                    }






                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblDescuento' border=0 width='100%;' style='border-collapse: collapse;'  class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Orden</th>");
                    shtml.Append("<th class='k-header'>Origen</th>");
                    shtml.Append("<th class='k-header' style='display:none'>IdLicDiscId</th>");
                    shtml.Append("<th class='k-header' style='display:none'>IdDescuento</th>");
                    shtml.Append("<th class='k-header'>Tipo</th>");
                    shtml.Append("<th class='k-header'>Descuento</th>");

                    shtml.Append("<th class='k-header'>Formato</th>");
                    shtml.Append("<th class='k-header'>Valor</th>");

                    //  shtml.Append("<th class='k-header'>Base</th>");

                    // shtml.Append("<th  class='k-header'>Valor Dsct</th>");
                    //shtml.Append("<th  class='k-header'>Neto</th>");
                    shtml.Append("<th  class='k-header'>Aplicable</th>");
                    shtml.Append("<th  class='k-header'>Auto.</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    decimal DsctoActumulado = 0;
                    decimal CargoAcumulado = 0;
                    decimal dbase = 0;
                    if (Descuento != null && Descuento.Count > 0)
                    {

                        int cuenta = 1;
                        decimal NetoActualizado = 0;
                        decimal DsctoActumuladoB = 0;
                        //foreach (var item in Descuento.OrderBy(x => x.Orden))
                        foreach (var item in Descuento.Where(x => x.Valor > 0))
                        {
                            decimal valDesto = 0;
                            decimal valNeto = 0;
                            if (cuenta == 1) { dbase = item.Base; NetoActualizado = item.Base; };
                            string bgcolor = " style='background:transparent;'";
                            string color = "";
                            string Estado_desc = "";
                            if (!item.esAutomatico) bgcolor = " style='background:lavender;'"; //verde


                            shtml.AppendFormat("<tr class='k-grid-content' {0}>", bgcolor);
                            shtml.AppendFormat("<td > <a href=# onclick='EditarDescuento({1});'>{0}</td>", item.Orden, item.IdLicDesc);
                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({1});'>{0}</td>", item.DesOrigen, item.IdLicDesc);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdLicDesc);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({1});'>{0}</td>", item.Tipo, item.IdLicDesc);
                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({1});'>{0}</td>", item.Descuento, item.IdLicDesc);

                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({1});'>{0}</td>", item.Formato, item.IdLicDesc);
                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({2});'>{0} ({1})</td>", item.Valor, item.esSuma ? "+" : "-", item.IdLicDesc);
                            //   shtml.AppendFormat("<td >{0}</td>", item.Base);//.ToString("N", CultureInfo.InvariantCulture));



                            if (item.Formato == "%") valDesto = (((item.Aplicable == "B" ? item.Base : NetoActualizado) * item.Valor) / 100);
                            else valDesto = item.Valor;


                            valDesto = (decimal)this.Truncate((double)valDesto, cantDecimales);

                            if (item.esSuma)
                            {
                                DsctoActumuladoB = DsctoActumuladoB - valDesto;
                                CargoAcumulado = CargoAcumulado + valDesto;
                            }
                            else
                            {
                                DsctoActumuladoB = DsctoActumuladoB + valDesto;
                                DsctoActumulado = DsctoActumulado + valDesto;
                            }

                            string selectedB = "";
                            string selectedN = "";
                            if (item.Aplicable == "B")
                            {
                                selectedB = " selected ";
                                if (item.esSuma)
                                    valNeto = item.Base + valDesto;
                                else
                                    valNeto = item.Base - valDesto;

                            }
                            else
                            {
                                selectedN = " selected ";
                                valNeto = item.Base - DsctoActumuladoB;
                            }
                            //     shtml.AppendFormat("<td ><input type='text' id='txtValorDscto_{0}' value={1} style='text-align:right; width:80px;' disabled/></td>", item.Orden, valDesto);//.ToString("N", CultureInfo.InvariantCulture));
                            //   shtml.AppendFormat("<td ><input type='text' id='txtNeto_{1}' disabled value={0} style='text-align:right;width:80px;' /></td>",  valNeto , item.Orden);

                            shtml.AppendFormat("<td ><select id='ddlAplicable_{0}'  {1} onchange='actualizarMonto(this,{4});'><option value='B' {2} >Base</option><option value='N'  {3}>Neto</option></select></td>", item.Orden, cuenta == 1 || item.DISC_ESTADO == DescuentoAprobaciones.aprobado ? "Disabled" : "", selectedB, selectedN, item.IdLicDesc);
                            shtml.AppendFormat("<td ><a href=# onclick='EditarDescuento({1});'>{0}</td>", item.esAutomatico ? "A" : "M", item.IdLicDesc);

                            if (item.DISC_ESTADO == DescuentoAprobaciones.aprobado)
                            {
                                color = "Green";
                                Estado_desc = "APROBADO";

                            }
                            else if (item.DISC_ESTADO == DescuentoAprobaciones.pendiente)
                            {
                                color = "orange";
                                Estado_desc = "PENDIENTE";
                            }
                            else
                            {
                                color = "Red";
                                Estado_desc = "Rechazado";
                            }


                            shtml.AppendFormat("<td  style=' text-align:center; color:" + color + "'>{0}</td>", Estado_desc, item.IdLicDesc);
                            shtml.AppendFormat("<td> <a href=# onclick='delAddDescuento({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td> ", item.IdLicDesc, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Descuento" : "Activar Descuento");


                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            cuenta++;
                            NetoActualizado = valNeto;

                        }
                    }
                    else {

                        if (idLic == -1)
                            shtml.Append("<tr><td colspan='12' style='height:30px;'><center><b>Seleccione un periodo planificación.</b></center></td></tr>");
                        else
                            shtml.Append("<tr><td colspan='12' style='height:30px;'><center><b>No se encontraron descuentos asociados a la Licencia.</b></center></td></tr>");
                    }
                    shtml.AppendFormat("<input type='hidden' id='hidMontoBaseDscts' value={0}></table>", dbase);
                    retorno.message = shtml.ToString();
                    retorno.data = Json(new
                    {
                        TotalDescuento = DsctoActumulado,//this.Truncate((double)DsctoActumulado, cantDecimales), 
                        TotalCargo = this.Truncate((double)CargoAcumulado, cantDecimales),
                        TotalTarifa = this.Truncate((double)dbase, cantDecimales)
                    }, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDescuentos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal idLicDes)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //if (DescuentoTmp != null && DescuentoTmp.Count>0)
                    //{
                    //    var itemDel=DescuentoTmp.Where(X => X.Orden == orden).FirstOrDefault();
                    //    DescuentoTmp.Remove(itemDel);
                    //}
                    new BLLicenciaDescuento().Eliminar(idLicDes, GlobalVars.Global.OWNER, UsuarioActual);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
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
        public JsonResult addItem(decimal idTipo, decimal id, string Tipo, decimal idLic, string OBSERVACION)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEDescuentos> lista = new List<BEDescuentos>();

                    lista.Add(new BEDescuentos
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        LIC_ID = idLic,
                        ORDEN = 0,
                        DISC_ORG = "B",
                        LOG_USER_CREAT = UsuarioActual,
                        DISC_ID = id,
                        IS_AUTO = false,
                        OBSERVACION = OBSERVACION
                    });

                    var i = new BLLicenciaDescuento().Insertar(lista);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "addItem DESCUENTO", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public BEDescuentos Obtiene(decimal id)
        {


            BEDescuentos tipo = null;
            var lista = new BLDescuentos().Obtener(GlobalVars.Global.OWNER, id);

            if (lista != null)
            {
                tipo = new BEDescuentos();
                foreach (var item in lista)
                {
                    tipo.OWNER = item.OWNER;
                    tipo.DISC_ID = item.DISC_ID;
                    tipo.DISC_TYPE = item.DISC_TYPE;
                    tipo.DISC_NAME = item.DISC_NAME;
                    tipo.DISC_SIGN = item.DISC_SIGN;
                    tipo.DISC_PERC = item.DISC_PERC;
                    tipo.DISC_VALUE = item.DISC_VALUE;
                    tipo.DISC_ACC = item.DISC_ACC;
                    tipo.DISC_AUT = item.DISC_AUT;
                    tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                }

            }

            return tipo;

        }
        public JsonResult Insertar()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    if (DescuentoTmp != null && DescuentoTmp.Count > 0)
                    {

                        List<BEDescuentos> lista = new List<BEDescuentos>();
                        DescuentoTmp.ForEach(x =>
                        {
                            lista.Add(new BEDescuentos
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                LIC_ID = x.LicId,
                                DISC_VALUE = 0,
                                ORDEN = x.Orden,
                                DISC_ORG = x.Aplicable,
                                LOG_USER_CREAT = UsuarioActual,
                                DISC_ID = x.Id
                            });
                        });
                        var i = new BLLicenciaDescuento().Insertar(lista);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.result = 1;
                    }
                    else {
                        retorno.message = "No existen descuentos para asociar a la licencia";
                        retorno.result = 0;
                    }


                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdAplicable(decimal idLicDes, string valorAplicable)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    if (valorAplicable == "B" || valorAplicable == "N") {
                        var i = new BLLicenciaDescuento().ActualizarAplicable(idLicDes, GlobalVars.Global.OWNER, valorAplicable, UsuarioActual);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.result = 1;
                    }
                    else {
                        retorno.message = "Sólo se permiten valores B o N como aplicables al descuento.";
                        retorno.result = 0;
                    }


                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdAplicable", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularTestDescuento(string montoTarifa, string montoDescuento, decimal licid, string montoCargo, string totImpuestoPer, string totImpuestoVal)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    if (string.IsNullOrEmpty(montoTarifa)) montoTarifa = "0";
                    if (string.IsNullOrEmpty(montoCargo)) montoCargo = "0";
                    if (string.IsNullOrEmpty(totImpuestoPer)) totImpuestoPer = "0";
                    if (string.IsNullOrEmpty(totImpuestoVal)) totImpuestoVal = "0";
                    if (string.IsNullOrEmpty(montoDescuento)) montoDescuento = "0";

                    decimal dtarifa = decimal.Parse(montoTarifa);
                    decimal dcargo = decimal.Parse(montoCargo);
                    decimal dImpuestoPer = decimal.Parse(totImpuestoPer);
                    decimal dImpuestoVal = decimal.Parse(totImpuestoVal);
                    decimal dDscto = decimal.Parse(montoDescuento);
                    //decimal dDsctoSocio = decimal.Parse(montoDescuentoSocio);

                    #region Descuentos por cliente
                    //Caculando los descuentos por Cliente
                    //var ListaDescuentoTotalSocio = new BLDescuentos().ObtieneTotalDescuentoSoc(licid);
                    //decimal TotalDesSocPos = 0;
                    //decimal TotalDesSocNeg = 0;
                    //decimal TarifaDescuentoSoc = dtarifa;

                    //Recorriendo la lista con los descuentos y valores y signos
                    //foreach (var item in ListaDescuentoTotalSocio)
                    //{
                    //    //si disc.value es >0 quiere decir q se trata de un que no es porcentaje
                    //    if ((item.DISC_VALUE != 0 && item.DISC_VALUE != null ))
                    //    {

                    //        if (item.DISC_SIGN == "+")
                    //        {
                    //            TarifaDescuentoSoc=TarifaDescuentoSoc+ (item.DISC_VALUE);
                    //            TotalDesSocPos = TotalDesSocPos + (item.DISC_VALUE);
                    //        }
                    //           //si es signo negativo quiere decir que es descuento si es positivo es penalidad
                    //        else
                    //        {
                    //            TarifaDescuentoSoc=TarifaDescuentoSoc- (item.DISC_VALUE);
                    //            TotalDesSocNeg = TotalDesSocNeg + (item.DISC_VALUE);

                    //        }
                    //    }
                    //    else if ((item.DISC_PERC != 0 && item.DISC_PERC != null))
                    //    {
                    //        if (item.DISC_SIGN == "+")
                    //        {
                    //            TotalDesSocPos = TotalDesSocPos + (TarifaDescuentoSoc * ((item.DISC_PERC) / 100));

                    //            TarifaDescuentoSoc = TarifaDescuentoSoc + (TarifaDescuentoSoc * ((item.DISC_PERC) / 100));



                    //        }
                    //        else
                    //        {
                    //            TotalDesSocNeg = TotalDesSocNeg + (TarifaDescuentoSoc * ((item.DISC_PERC) / 100));

                    //            TarifaDescuentoSoc = TarifaDescuentoSoc - (TarifaDescuentoSoc * ((item.DISC_PERC) / 100));


                    //        }

                    //    }


                    //}

                    #endregion
                    var cantDecimales = this.getCantDecimal((double)dtarifa);

                    decimal dImpuesto = 0;
                    decimal dMontoImpPer = 0;

                    if (dImpuestoPer > 0) {
                        dMontoImpPer = ((dtarifa * dImpuestoPer) / 100);
                    }

                    dImpuesto = dMontoImpPer + dImpuestoVal;
                    //TotalDessOCPOS = Penalidad y TotalDescNeg = Descuento
                    decimal tot = ((dtarifa + dcargo + dImpuesto /*+ *TotalDesSocPos*/) - (dDscto /*+TotalDesSocNeg*/));
                    //redondeando
                    tot = decimal.Round(tot, 2);
                    /*TotalDesSocNeg=decimal.Round(TotalDesSocNeg, 2);*/


                    retorno.result = 1;
                    retorno.valor = Convert.ToString(tot);
                    retorno.data = Json(new {
                        TarifaTotFormato = dtarifa.ToString(),// string.Format("{0}", this.Truncate((double)dtarifa, cantDecimales)),//.ToString("N", CultureInfo.InvariantCulture)),//"N", CultureInfo.InvariantCulture),
                        CargosTotFormato = dcargo,//string.Format("{0}", this.Truncate((double)dcargo, cantDecimales)),//.ToString("N", CultureInfo.InvariantCulture)),//dcargo.ToString(),//"N", CultureInfo.InvariantCulture),
                        ImpuestosTotFormato = dImpuesto,//string.Format("{0}", this.Truncate((double)dImpuesto, cantDecimales)),//.ToString("N", CultureInfo.InvariantCulture)),//dImpuesto.ToString(),//"N", CultureInfo.InvariantCulture),
                        DsctosTotFormato = dDscto,//string.Format("{0}", this.Truncate((double)dDscto, cantDecimales)),//.ToString("N", CultureInfo.InvariantCulture)),// dDscto.ToString(),//"N", CultureInfo.InvariantCulture),
                                                  /*  DsctoTotSocioFormato = TotalDesSocNeg,*/
                        TotalDsctoFormato = tot// string.Format("{0}", this.Truncate((double)tot, cantDecimales))//.ToString("N", CultureInfo.InvariantCulture))// tot.ToString(),//"N", CultureInfo.InvariantCulture),

                    }, JsonRequestBehavior.AllowGet);


                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "CaluclarTestDescuento", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult InsertarTarifaDescuento(BETarifaDescuento tarifaDescuento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLDescuentos();
                    tarifaDescuento.OWNER = GlobalVars.Global.OWNER;
                    tarifaDescuento.LOG_USER_CREAT = UsuarioActual;
                    int id = servicio.InsertarTarifaDescuento(tarifaDescuento);
                    retorno.Code = id;
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarTarifaDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Esta Llegando NUll
        #region Descuentos Socio

        //Consulta Descuentos Por Socio
        public JsonResult ConsultaDescuentosSocio(decimal bpsid)
        {
            Resultado retorno = new Resultado();
            //Borrar La Sesion 
            Session.Remove(K_SESSION_DESCUENTO_SOCIOCOMP);

            try
            {
                if (!isLogout(ref retorno))
                {
                    DescuentoSocioTmp = new List<DTODescuentoSocio>();
                    DescuentoSocioComaparTmp = new List<DTODescuentoSocio>();

                    var Consulta = new BLDescuentos().Listar_Descuentos_Socio(bpsid);


                    foreach (var x in Consulta)
                    {
                        //if(x.ORDEN==1)
                        //x.ORDEN = 1;

                        DescuentoSocioComaparTmp.Add(new DTODescuentoSocio
                        {
                            Orden = DescuentoSocioComaparTmp.Count + 1,
                            DISC_ID = x.DISC_ID,
                            Descuento = x.DISC_NAME,
                            DISC_VALUE = x.DISC_VALUE,
                            Activo = x.ENDS.HasValue ? false : true,
                            Tipo = x.DISC_TYPE,
                            DISC_PERC = x.DISC_PERC,
                            EnBD = true,
                            OBSERVACION = x.OBSERVACION
                        });
                        //x.ORDEN++;
                        retorno.result = 1;
                    }

                    foreach (var x in Consulta)
                    {
                        //if (x.ORDEN == 1)
                        //x.ORDEN = 1;

                        DescuentoSocioTmp.Add(new DTODescuentoSocio
                        {
                            Orden = DescuentoSocioTmp.Count + 1,
                            DISC_ID = x.DISC_ID,
                            Descuento = x.DISC_NAME,
                            DISC_VALUE = x.DISC_VALUE,
                            Activo = x.ENDS.HasValue ? false : true,
                            Tipo = x.DISC_TYPE,
                            DISC_PERC = x.DISC_PERC,
                            EnBD = true,
                            OBSERVACION = x.OBSERVACION
                        });
                        //x.ORDEN++;
                        retorno.result = 1;
                    }

                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Consuta Descuentos SOcio", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        //Listar Descuentos x Socio
        public JsonResult ArmaDescuentoSocioTmp(List<DTODescuentoSocio> DescuentosSoc)
        {

            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {

                    if (DescuentoSocioTmp == null)
                    {
                        DescuentoSocioTmp = new List<DTODescuentoSocio>();


                    }

                    if (DescuentosSoc != null)
                    {
                        //Variable para dejar Insertar en Temporales
                        Boolean permitir = true;
                        foreach (var s in DescuentosSoc)
                        {

                            //Antes de agregar se tiene que validar Que lo ingresado no se esta repitiendo

                            foreach (var x in DescuentoSocioTmp.OrderBy(x => x.Orden))
                            {//Recorriendo descuento ingresado
                                if (s.DISC_ID == x.DISC_ID || x.Descuento == s.Descuento)
                                {
                                    permitir = false;
                                    //mensaje de que esta repitiendo el mismo Codigo
                                    retorno.Code = 0;
                                    //Eso se recupera en el JQUERY
                                    //mantenimiento.descuento.js

                                }
                            }

                            //if(s.Orden==null || s.Orden==0)
                            //s.Orden=0;
                            //Aqui se Cae por valor Null

                            if (permitir)
                            {
                                DescuentoSocioTmp.Add(new DTODescuentoSocio
                                {
                                    DISC_ID = s.DISC_ID,
                                    Orden = DescuentoSocioTmp.Count + 1,
                                    Tipo = s.Tipo,
                                    Descuento = s.Descuento,
                                    DISC_VALUE = s.DISC_VALUE,
                                    esSuma = s.esSuma,
                                    DISC_ACC = s.DISC_ACC,
                                    DISC_AUT = s.DISC_AUT,
                                    DISC_PERC = s.DISC_PERC,
                                    EnBD = false,
                                    Activo = true,
                                    OBSERVACION = s.OBSERVACION
                                });
                                retorno.Code = 1;
                            }
                        }
                    }

                    //Armando La Tabla
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblDescuento' border=0 width='100%;' style='border-collapse: collapse;'  class='k-grid k-widget'>");
                    shtml.Append("<th class='k-header'>Descripcion del Descuento</th>");
                    shtml.Append("<th class='k-header'>Observacion del Descuento</th>");
                    shtml.Append("<th class='k-header'>Formato</th>");
                    shtml.Append("<th class='k-header'>Valor</th>");
                    shtml.Append("<th class='k-header'></th>");
                    shtml.Append("</tr></thead>");


                    if (DescuentoSocioTmp != null && DescuentoSocioTmp.Count > 0)
                    {
                        foreach (var item in DescuentoSocioTmp)
                        {
                            string bgcolor = " style='background:transparent;'";
                            //bgcolor = " style='background:lavender;'";

                            shtml.AppendFormat("<tr class='k-grid-content' {0}>", bgcolor);
                            //shtml.AppendFormat("<td >{0}</td>", item.Tipo);
                            shtml.AppendFormat("<td >{0}</td>", item.Descuento);
                            //shtml.AppendFormat("<td >{0}</td>",item.Tipo==11 ? item.DISC_VALUE:item.DISC_PERC/*, item.esSuma ? "+" : "-"*/);  
                            //shtml.AppendFormat("<td>{0}</td>",item.esSuma);
                            shtml.AppendFormat("<td >{0}</td>", item.OBSERVACION);
                            shtml.AppendFormat("<td>{0}</td>", item.DISC_VALUE > 0 ? "$" : "%");
                            // shtml.AppendFormat("<td >{0} ({1})</td>",item.DISC_VALUE!=0 ? item.DISC_VALUE :item.DISC_PERC,item.esSuma/*, item.esSuma ? "+" : "-"*/);  
                            shtml.AppendFormat("<td >{0} </td>", item.DISC_VALUE != 0 ? item.DISC_VALUE : item.DISC_PERC/*, item.esSuma ? "+" : "-"*/);
                            //shtml.AppendFormat("<td> <a onclick='DescativandoDescuentosSocio(" + item.Descuento + ");'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td> ", item.Descuento, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Descuento" : "Activar Descuento");
                            shtml.AppendFormat("<td> <img src='../Images/iconos/{1}' onclick='DescativandoDescuentosSocio(" + item.Orden + ");' title='{2}' border=0></td> ", item.Descuento, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Descuento" : "Activar Descuento");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                        }
                    }
                    shtml.AppendFormat("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }

            }


            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDescuentoSocio", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult InsertarDescuentosSocios(decimal BpsId)//Socio
        {
            Resultado retorno = new Resultado();
            //Insertando Registros
            List<BEDescuentos> listadescuentosP = new List<BEDescuentos>(); //por grupo
            List<BEDescuentos> listadescuentosS = new List<BEDescuentos>(); // por socio
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (DescuentoSocioTmp != null && DescuentoSocioTmp.Count > 0)
                    {
                        //List<BEDescuentos> lista = new List<BEDescuentos>();


                        //Preparando solo lo que se insertara Si es la primera vez que se inserta DescuentoSocioComaparTmp = null si es modf >0
                        if (DescuentoSocioComaparTmp != null)
                        {
                            foreach (var x in DescuentoSocioComaparTmp)
                            {
                                if (DescuentoSocioTmp != null)
                                {
                                    DescuentoSocioTmp = DescuentoSocioTmp.Where(y => y.Descuento != x.Descuento).ToList();
                                }

                            }
                        }


                        foreach (var x in DescuentoSocioTmp)
                        {
                            BEDescuentos desc = new BEDescuentos();
                            desc.OWNER = GlobalVars.Global.OWNER;
                            desc.DISC_ID = x.DISC_ID;
                            desc.DISC_TYPE = x.Tipo;
                            desc.DISC_NAME = x.Descuento;
                            desc.DISC_SIGN = x.esSuma;
                            desc.LOG_USER_CREAT = UsuarioActual;
                            desc.DISC_PERC = x.DISC_PERC;
                            desc.DISC_VALUE = x.DISC_VALUE;
                            desc.DISC_ACC = x.DISC_ACC;
                            desc.DISC_AUT = x.DISC_AUT;
                            desc.DISC_ORG = "B";
                            desc.OBSERVACION = x.OBSERVACION;
                            if (DescuentoSocioComaparTmp != null) desc.DISC_ORG = "N";

                            int id = Convert.ToInt32(desc.DISC_ID);
                            if (desc.DISC_ID == 0)
                                id = new BLDescuentos().InsertaDescuentoSoc(desc);
                            new BLDescuentos().InsertarDescuentoSocioBPS(BpsId, id, UsuarioActual, desc.OBSERVACION);
                            //----------------------------------------Insert de Descuentos EMRPESARIALES--------------------------
                            var listaBPSID = new BLSocioNegocio().BpsIdxGrupoEmpresarial(GlobalVars.Global.OWNER, BpsId);//LISTA LOS BPSID DE GRUPO EMP
                            if (listaBPSID != null && listaBPSID.Count > 0)
                            {
                                listadescuentosP = new List<BEDescuentos>();
                                listaBPSID.ForEach(c =>//RECORRIENDO EL ARREGLO DE LSITA DE BPSID POR SOCIO EMPRESARIAL
                                {
                                    var listaLicId = new BLDescuentos().ListaLicIdxSocio(GlobalVars.Global.OWNER, c.BPS_ID); //LISTA DE LICENCIAS POR SOCIO
                                    desc.BPS_ID = c.BPS_ID;
                                    if (listaLicId.Count > 0)
                                    {

                                        foreach (var y in listaLicId.OrderBy(y => y.LIC_ID))
                                        {
                                            if (desc.DISC_ID == 0 && id != null && id != 0) desc.DISC_ID = id;
                                            listadescuentosP.Add(new BEDescuentos
                                            {
                                                OWNER = GlobalVars.Global.OWNER,
                                                LIC_ID = y.LIC_ID,
                                                DISC_ID = desc.DISC_ID,
                                                ORDEN = desc.ORDEN,
                                                ORIGEN = desc.DISC_ORG,
                                                LOG_USER_CREAT = UsuarioActual,
                                                IS_AUTO = true,
                                                BPS_ID = BpsId,//BPS_ID=c.BPS_ID,
                                                OBSERVACION = desc.OBSERVACION

                                            });
                                        }
                                    }
                                });
                            }//---------------------------------DESCUENTOS POR SOCIO SIMPLES-------------------------------------
                            //   {---AQUI LISTA SI ES QUE EL GRUPO EMPRESARIAL O SI ES UN SOCIO NORMAL .....
                            #region SOCIO NORMAL
                            var listaLicIdxSocio = new BLDescuentos().ListaLicIdxSocio(GlobalVars.Global.OWNER, BpsId);
                            if (listaLicIdxSocio.Count > 0)
                            {
                                desc.BPS_ID = BpsId;
                                desc.ORDEN = x.Orden;
                                //Bucle para insertar en todas las 
                                foreach (var y in listaLicIdxSocio.OrderBy(y => y.LIC_ID))
                                {
                                    if (desc.DISC_ID == 0 && id != null && id != 0) desc.DISC_ID = id;
                                    listadescuentosS.Add(new BEDescuentos
                                    {
                                        OWNER = GlobalVars.Global.OWNER,
                                        LIC_ID = y.LIC_ID,
                                        DISC_ID = desc.DISC_ID,
                                        ORDEN = desc.ORDEN,
                                        ORIGEN = desc.DISC_ORG,
                                        LOG_USER_CREAT = UsuarioActual,
                                        IS_AUTO = true,
                                        BPS_ID = BpsId,
                                        OBSERVACION = desc.OBSERVACION
                                    });
                                }

                                //new BLLicenciaDescuento().Insertar(listadescuentos);
                            }
                            //    }
                            #endregion

                        }
                        if (listadescuentosP.Count > 0 && listadescuentosP != null)
                        {
                            new BLLicenciaDescuento().InsertaDescuentosGRUPOXML(listadescuentosP);//Inserta los descuentos  a todas las licencias de sus soscios,si es que es Grupo empresarial
                        }
                        if (listadescuentosS.Count > 0 && listadescuentosS != null)
                        {
                            new BLLicenciaDescuento().InsertaDescuentosGRUPOXML(listadescuentosS);//Si el grupo empresarial tuviera Licencias Tambien inserta a sus licencias
                        }
                        retorno.result = 1;

                    }
                }

            }
            catch (Exception ex)
            {

                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "INSERTAR DESCUENTOS SOCIOS", ex);

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        //Inactiva descuentos de la lista temporal Previo a Guardar en BD.
        [HttpPost]
        public JsonResult DellAddDescuentoSocio(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                //direcciones = DireccionesTmp;
                if (DescuentoSocioTmp != null)
                {
                    var objDel = DescuentoSocioTmp.Where(x => x.Orden == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        //Pero si Ademas de eso OBJDEL.ENBD esta en true quiere decir que se agrego por la consulta
                        //Vino de la Base de datos asi qie se debe Inactivar poniendo el campo ENDS=GETDATE()
                        if (objDel.EnBD)
                        {
                            //Mostrando el Boton De que ha sido Eliminado
                            foreach (var x in DescuentoSocioTmp.Where(x => x.DISC_ID == objDel.DISC_ID))
                            {
                                if (x.Activo == true)
                                    x.Activo = false;
                                else
                                    x.Activo = true;

                            }
                            //Ahora Insertar El item Actualizado so inactivado
                        }
                        else
                        {
                            //Si obtuvo dato lo borra de el temporal
                            foreach (var x in DescuentoSocioTmp)
                            {
                                DescuentoSocioTmp = DescuentoSocioTmp.Where(y => y.Orden != objDel.Orden).ToList();
                            }
                        }
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

        public JsonResult InactivarDescuentoSocioGrabar(decimal BPSID, decimal BPSID_GRUPO)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<SocioNegocio> lista = null;
                List<BEDescuentos> listaactivos = new List<BEDescuentos>();//lista descuentos activos
                List<BEDescuentos> listainactivos = new List<BEDescuentos>();//lista descuentos inactivos

                if (DescuentoSocioTmp != null) {
                    if (BPSID_GRUPO == 0) { BPSID_GRUPO = BPSID; }//esto es para cuando el G.E padre realiza un cambio
                    lista = new BLSocioNegocio().BpsIdxGrupoEmpresarial(GlobalVars.Global.OWNER, BPSID);//Aqui estoy trayendo Todos los Socios que estan afiliados al grupo empresarial

                    int contador = 0;

                    if (lista != null && lista.Count > 0)

                        contador = lista.Count;//valida que solo se ejecute el bucle la cantidad de veces de los socios correspondientes

                    foreach (var x in DescuentoSocioTmp.OrderBy(x => x.DISC_ID))
                    {
                        if (x.Activo == true && x.EnBD == true)//Si viene de la Base de datos quiere decir que  DescuentoSocioComparaTmp esta lleno
                        {
                            if (lista != null && lista.Count > 0)//LISTA LOS SOCIOS POR GRUPO EMPRESARIAL
                            {
                                foreach (var z in lista.OrderBy(z => z.BPS_ID))
                                {
                                    listaactivos.Add(new BEDescuentos {
                                        DISC_ID = x.DISC_ID,
                                        BPS_ID = z.BPS_ID, LOG_USER_CREAT = UsuarioActual,
                                        OWNER = GlobalVars.Global.OWNER//tambien debe realizarlo para su misma entidad y su mismas licencias
                                    });
                                }
                                listaactivos.Add(new BEDescuentos { DISC_ID = x.DISC_ID, BPS_ID = BPSID, LOG_USER_CREAT = UsuarioActual, OWNER = GlobalVars.Global.OWNER });//tambien agregar para sus licencias

                                new BLDescuentos().ActivaDescuentosSocio(x.DISC_ID, BPSID, BPSID, UsuarioActual); //activa solo a los suyos
                            }
                            else//si no es grupo empresarial Hace lo De siempre------------------------
                            {
                                new BLDescuentos().ActivaDescuentosSocio(x.DISC_ID, BPSID, BPSID, UsuarioActual);
                            }
                        }

                        else if (x.Activo == false && x.EnBD == true)
                        {
                            if (lista != null && lista.Count > 0)//SI la lista me devuelve null o >0 realiza el bucle 
                            {
                                foreach (var z in lista.OrderBy(z => z.BPS_ID))
                                {
                                    listainactivos.Add(new BEDescuentos
                                    {
                                        DISC_ID = x.DISC_ID,
                                        BPS_ID = z.BPS_ID,
                                        LOG_USER_CREAT = UsuarioActual,
                                        OWNER = GlobalVars.Global.OWNER//tambien debe realizarlo para su misma entidad y su mismas licencias
                                    });
                                }
                                listainactivos.Add(new BEDescuentos { DISC_ID = x.DISC_ID, BPS_ID = BPSID, LOG_USER_CREAT = UsuarioActual, OWNER = GlobalVars.Global.OWNER });//tambien agregar para sus licencias

                                new BLDescuentos().InactivaDescuentosSocio(x.DISC_ID, BPSID, BPSID, UsuarioActual);//tambien debe realizarlo para su misma entidad y su mismas licencias
                            }
                            else//si no hace de siempre
                            {
                                new BLDescuentos().InactivaDescuentosSocio(x.DISC_ID, BPSID, BPSID, UsuarioActual);
                            }

                        }

                    }
                    //Activando***************************
                    if (listaactivos.Count > 0 && listaactivos != null)
                    {
                        List<BEDescuentos> listadescuentosP = new List<BEDescuentos>(); //por grupo
                        /*********************ACTIVANDO LOS DESCUENTOS DE LAS LICENCIAS ******************************/
                        new BLDescuentos().ActivaDescuentosGrupoEmpresarialXML(BPSID, listaactivos);//activa en  masa xml
                                                                                                    //***************************************************************************
                        #region Inserta DescActivos en Lic que no lo poseen
                        /* ******************SI SE AGREGA UN SOCIO CUANDO UN DSC DEL GRUPO ESTA INACTIVO Y SE VUELVE A ACTIVAR 
                         DEBE DE INSERTARSE EN LAS LICENCIAS DE LOS SOCIOS QUE NO LO TIENEN
                         */
                        //foreach (var x in listaactivos.Where(x => x.BPS_ID != BPSID))//PARA NO LISTAR LAS LICENCIAS DEL PROPIO GRUPO
                        //{
                        //    List<BEDescuentos> listaLicenciasDesc = new BLDescuentos().ListaLicenciasDescuentos(x.DISC_ID, x.BPS_ID);
                        //    foreach (var y in listaLicenciasDesc)
                        //    {
                        //        listadescuentosP.Add(new BEDescuentos
                        //        {

                        //            OWNER = GlobalVars.Global.OWNER,
                        //            LIC_ID = y.LIC_ID,
                        //            DISC_ID = x.DISC_ID,
                        //            ORDEN = y.ORDEN,
                        //            ORIGEN = y.DISC_ORG,
                        //            LOG_USER_CREAT = UsuarioActual,
                        //            IS_AUTO = true,
                        //            BPS_ID = y.BPS_ID
                        //        });
                        //    }
                        //}
                        //   new BLLicenciaDescuento().InsertaDescuentosGRUPOXML(listadescuentosP);//Inserta los descuentos  a todas las licencias de sus soscios,si es que es Grupo empresarial
                        #endregion
                    }
                    //Inactivando*************************
                    if (listainactivos.Count > 0 && listainactivos != null)
                    {
                        new BLDescuentos().InactivaDescuentosGrupoEmpresarialXML(BPSID, listainactivos);//Inactiva en  masa xml
                    }

                    retorno.result = 1;

                }




                //Luego de inactivar Realizar la insercion de descuentos nuevos  En las licencias hijas
                //if (lista != null && lista.Count > 0)
                //{
                //    foreach (var z in lista.OrderBy(z => z.BPS_ID))
                //    {
                InsertarAutomDescLicencia(BPSID, BPSID_GRUPO);
                //    }
                //}

                //TABMEIN DE DEBEN DEL MISMO SOCIO O GRUPO SEA EL CASO...
                // InsertarAutomDescLicencia(BPSID,BPSID);

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ObtieneDescuentoTotalSocio(decimal licid)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno)) {

                    decimal total = 0;
                    total = 0; //new BLDescuentos().ObtieneTotalDescuentoSoc(licid);

                    retorno.valor = Convert.ToString(total);
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

        //Valida Periodo Por descuentos
        public JsonResult ValidarPeriodoDescuento(decimal CodPeriodo)
        {
            Resultado retorno = new Resultado();
            try
            {
                int valida = new BLDescuentos().ValidaPeriodoDescuento(GlobalVars.Global.OWNER, CodPeriodo);
                if (valida == 1)
                { //SI ES = 1 ENTONCES QUIERE DECIR QUE EL PERIODO ESTA CERRADO
                    retorno.result = 1;
                    retorno.message = "EL PERIODO ESTA CERRADO,POR LO CUAL NO ES POSIBLE AGREGAR DESCUENTOS";
                }
                else
                    retorno.result = 0;

            }
            catch (Exception ex)
            {
                //retorno.result = 0;
                retorno.message = ex.Message;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult InactivaDescuentosGrupoEmp(decimal bpsid, decimal bpsgroup)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int r = new BLDescuentos().InactivaDesSocioxGrupoEmpresarial(GlobalVars.Global.OWNER, bpsid, bpsgroup);
                    if (r == 0)//devuelve 0 al actualizar las licencias ._."
                    {
                        retorno.result = 1;
                    }
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = ex.Message;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        //Insertar Automaticamente Descuentos A las Licencias HIjas ,Individuales de los socios al Agregarse a un Grupo empresarial
        public JsonResult InsertarAutomDescLicencia(decimal bpsid, decimal bpsidgroup)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEDescuentos> listadescuentosP = new List<BEDescuentos>(); //por grupo
                    List<SocioNegocio> listasocios = new BLSocioNegocio().BpsIdxGrupoEmpresarial(GlobalVars.Global.OWNER, bpsid);//Aqui estoy trayendo Todos los Socios que estan afiliados al grupo empresarial
                    if (listasocios.Count == 0)
                        listasocios.Add(new SocioNegocio { BPS_ID = bpsid }); //si no es socio entonces que inserte los desc del grupo a el mismo
                    var listalicindiv = new BLLicencias().ListarLicenciasxCodigoSocio(GlobalVars.Global.OWNER, bpsid);//Lista sus propias licencias

                    #region lista de socios
                    if (listasocios != null && listasocios.Count > 0)
                    {
                        foreach (var ls in listasocios)
                        {
                            var lista = new BLLicencias().ListarLicenciasxCodigoSocio(GlobalVars.Global.OWNER, ls.BPS_ID);//BLDESCUENTOS/ListaLicIdxSocio  es similar
                            #region lista licencias x codigo de socio
                            if (lista != null && lista.Count > 0)    //Esto de aqui Inserta Automaticamente Descuentos De el Grupo Empresarial
                            {
                                var ListaDescxSocioGroup = new BLDescuentos().Listar_Descuentos_Socio(bpsidgroup);//Los descuentos Del socio

                                //---------SI LA LICENCIA YA POSEE DESCUENTOS--------
                                #region lista descuentos de Grupo empresarial
                                if (ListaDescxSocioGroup != null && ListaDescxSocioGroup.Count > 0)
                                {
                                    foreach (var x in ListaDescxSocioGroup.OrderBy(x => x.DISC_ID))
                                    {
                                        BEDescuentos desc = new BEDescuentos();
                                        desc.OWNER = GlobalVars.Global.OWNER;
                                        desc.DISC_ID = x.DISC_ID;
                                        desc.DISC_TYPE = x.DISC_TYPE;
                                        desc.DISC_NAME = x.DISC_NAME;
                                        desc.DISC_SIGN = x.DISC_SIGN;
                                        desc.LOG_USER_CREAT = UsuarioActual;
                                        desc.DISC_PERC = x.DISC_PERC;
                                        desc.DISC_VALUE = x.DISC_VALUE;
                                        desc.DISC_ACC = x.DISC_ACC;
                                        desc.DISC_AUT = 'S';
                                        //Si es "N" se calcula al Neto si es "B" se calcula al  BASE   
                                        desc.DISC_ORG = "N";
                                        desc.IS_AUTO = true;
                                        desc.ENDS = x.ENDS;
                                        desc.BPS_ID = bpsidgroup; //AGREGANDO EL BPSI
                                        //--UAN VEZ YA TENGO PREPARADA LAS ENTIDADES DEL GRUPO EMPRESARIAL-- 
                                        #region lista con descuentos a ingresar
                                        foreach (var y in lista.OrderBy(y => y.LIC_ID))
                                        {
                                            if (desc.ENDS == null)//Inserta solo los que Estan activos los que no (No se insertaran)
                                            {
                                                listadescuentosP.Add(new BEDescuentos
                                                {
                                                    OWNER = GlobalVars.Global.OWNER,
                                                    LIC_ID = y.LIC_ID,
                                                    DISC_ID = desc.DISC_ID,
                                                    ORDEN = desc.ORDEN,
                                                    ORIGEN = desc.DISC_ORG,
                                                    LOG_USER_CREAT = UsuarioActual,
                                                    IS_AUTO = true,
                                                    BPS_ID = bpsidgroup
                                                });
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                        }
                        if (listadescuentosP.Count > 0 && listadescuentosP != null)
                        {
                            new BLLicenciaDescuento().InsertaDescuentosGRUPOXML(listadescuentosP);//Inserta los descuentos  a todas las licencias de sus soscios,si es que es Grupo empresarial
                        }
                    }
                    #endregion
                    #region descuentos a su misma licencia
                    else//-----SI NO TIENE DESCUENTOS------------//Esto si aun no lo mejoro //
                    {
                        foreach (var x in listalicindiv.OrderBy(x => x.LIC_ID))
                        {
                            BLLicenciaDescuento servicio = new BLLicenciaDescuento();
                            new BLLicenciaDescuento().ListaDescuentos(GlobalVars.Global.OWNER, x.LIC_ID);//Al momento de lsitar VALIDA SI NO TIENE DESCUENTOS ,SI NO POSEE INSERTA SOLO INSERTA LA PRIMERA VEZ
                        }
                    }
                    #endregion

                    #endregion
                }
            }

            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = ex.Message;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPlantillaDescuentos(int page, int pagesize, string desc, DateTime? fecha)
        {
            Resultado retorno = new Resultado();
            try
            {
                string owner = GlobalVars.Global.OWNER;
                var lista = new BLDescuentos().listaDescuentoPlantilla(page, pagesize, owner, desc, fecha);

                var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

                if (tot.Count == 0)
                {
                    return Json(new BEDescuentosPlantilla { ListaDescuentosPlantilla = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new BEDescuentosPlantilla { ListaDescuentosPlantilla = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                //
                retorno.message = ex.Message;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Descuentos PlantillaGeneral
        /// <summary>
        ///CALCULANDO EL EL MONTO DE LA TARIFA CON LOS DESCUENTOS SEGUN LA MODALIDAD
        /// </summary>
        /// <param name="tarifa"></param>
        /// <param name="caracteristicastarifa"></param>
        /// <returns></returns>
        /// //        public decimal TarifaDescuentosPlantilla(DTOTarifa tarifa, List<DTOTarifaTestCaracteristica> caracteristicastarifa)
        public decimal TarifaDescuentosPlantilla(decimal vtarifa_id, decimal vmonto_tarifa, List<DTOTarifaTestCaracteristica> caracteristicastarifa)
        {
            //XXXXXXXXXXXXXXX DECLARANDO VARIABLES
            string owner = GlobalVars.Global.OWNER;//variable owner 
            int DescuentoUit = 0;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXX OBTENIENDO DATOS NECESARIOS
            decimal tarifa_id = vtarifa_id;//Recuperando El ID de la Tarifa
            decimal tarifa_monto = vmonto_tarifa;
            int respuestamodalidad = DescPlantilla.ES_MEGACONCIERTO;
            BEREC_RATES_GRAL tarifaobtener = new BLREC_RATES_GRAL().Obtener(owner, tarifa_id);//obtengo la tarifa y sus propiedades
            List<BETarifaDescuento> listaDescTarifa = tarifaobtener.Descuento;//lista de descuentos de la tarifa
            var UIT = new BLUit().ListaUit();
            decimal Valor_UIT = UIT.UITV_VALUEP;
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            if (respuestamodalidad == DescPlantilla.ES_MEGACONCIERTO)//SOLO SI ES MEGACONCIERTO
            {
                foreach (var x in listaDescTarifa.OrderBy(x => x.RATE_DISC_ID))
                {
                    decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                    decimal? param1 = null, param2 = null, param3 = null;

                    var ListCaractxDesc = new BLDescuentos().listaPlantillaxDISCID(x.DISC_ID);//Obtengo las Caracteristicas ...
                    decimal valor_dsc_calc = 0;

                    if (ListCaractxDesc.Count > 0)
                    {
                        #region DESCUENTOS
                        if (ListCaractxDesc.Count == 1)
                        {
                            foreach (var y in caracteristicastarifa)
                            {
                                if (y.IdCaracteristica == ListCaractxDesc[0].CHAR_ID)
                                {
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                    param1 = y.Valor;//0
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    if (x.DISC_ID == DescPlantilla.ARTISTA_GENERAL_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {

                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        DescuentoUit = 1;// para validar el calculo correspondiente
                                    }
                                }
                            }

                        }
                        if (ListCaractxDesc.Count == 2)
                        {
                            foreach (var y in caracteristicastarifa)
                            {
                                if (y.IdCaracteristica == ListCaractxDesc[0].CHAR_ID)
                                {
                                    param1 = y.Valor;//0
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                }

                                if (y.IdCaracteristica == ListCaractxDesc[1].CHAR_ID)
                                {
                                    param2 = y.Valor;//0
                                    CHAR2 = ListCaractxDesc[1].CHAR_ID;
                                    // break;
                                }

                                if (param2 != null && param1 != null)
                                {
                                    valor_dsc_calc = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                    if (x.DISC_ID == DescPlantilla.TRIBUNA_VIP_UIT || x.DISC_ID == DescPlantilla.ARTISTA_VIP_UIT)//SI SON VALORES QUE LLEVAN UIT
                                    {
                                        valor_dsc_calc = Valor_UIT * valor_dsc_calc; //AQUI RECUPERA UN MONTO QUE SE DEBE DE RESTAR NO SUMAR
                                        DescuentoUit = 1;// para validar el calculo correspondiente
                                    }
                                }

                            }

                        }

                        #endregion

                        #region calculo
                        if (DescuentoUit == 0)
                            tarifa_monto = tarifa_monto - (tarifa_monto * valor_dsc_calc);
                        else
                        {
                            tarifa_monto = tarifa_monto - valor_dsc_calc;
                            DescuentoUit = 0;//devolver a valor 0
                        }
                        #endregion
                    }
                }
            }
            else //SI ES CADENAS 
            {
                //**REUTILIZAR CODIGO  QUE SE EMPLEO EN FACTURA
            }
            return tarifa_monto;
        }




        public JsonResult ListarCaracteristicaDescuentos(decimal codigoLic)
        {
            Resultado retorno = new Resultado();


            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    //var caracteristicas = new BLCaracteristica().ListarCaractLicDscPlantilla(codigoLic);
                    var lic_pld = new BLDescuentos().Obtiene_PlanificacionUnica_Megaconcierto(codigoLic);
                    if (lic_pld == null) { lic_pld = 0; }
                    var caracteristicas = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, codigoLic, "0", lic_pld);
                    var caracteristicasCompara = caracteristicas;
                    var caracteristicas_dscPlantillaxTarifa = new BLCaracteristica().ListarCaractDescPlantillaxTarifa(codigoLic);

                    caracteristicas_dscPlantillaxTarifa = caracteristicas_dscPlantillaxTarifa.Where(z => z.CHAR_ID != DescPlantilla.ID_CARACT_PRODUCCION).ToList();
                    caracteristicas_dscPlantillaxTarifa = caracteristicas_dscPlantillaxTarifa.Where(z => z.CHAR_ID != DescPlantilla.ID_CARACT_TAQUILLA).ToList();
                    var valida_mega = new BLDescuentos().ValidaLicenciaMegaconcierto(codigoLic);
                    if (caracteristicas.Count > 0 && caracteristicas != null && valida_mega == 1)
                    {
                        shtml.Append("<table border=0 width='100%;' id='tblCaracteristicaDesc' class='k-grid k-widget'>");
                        shtml.Append("<thead><tr> <th  class='k-header'>Descripción</th>");
                        shtml.Append("<th class='k-header'>Periodo</th>");
                        shtml.Append("<th class='k-header'>Tipo</th>");
                        shtml.Append("<th class='k-header'>Fecha de Registro</th>");
                        shtml.Append("<th class='k-header'>Valor</th> ");
                        shtml.Append("<th class='k-header'>No es Valor real</th> ");
                        shtml.Append("<th class='k-header'>Comentario</th></tr></thead>");
                        //var caracteristicas = new BLCaracteristica().ListarCaractLicDscPlantilla(codigoLic);
                        //var caracteristicas_dscPlantillaxTarifa = new BLCaracteristica().ListarCaractDescPlantillaxTarifa(codigoLic);
                        #region obtieneCaractRepetidas
                        foreach (var c in caracteristicasCompara.OrderBy(c => c.CHAR_ID))
                        {
                            foreach (var y in caracteristicas_dscPlantillaxTarifa.OrderBy(y => y.CHAR_ID))
                            {
                                if (c.CHAR_ID == y.CHAR_ID)//solo debe listar las caracteristicas que necesita mostrar 

                                    caracteristicasCompara = caracteristicasCompara.Where(z => z.CHAR_ID != y.CHAR_ID).ToList();
                                // visible=" style='display:none'";
                            }
                        }
                        #endregion

                        #region valida Caract
                        foreach (var c in caracteristicas.OrderBy(y => y.CHAR_ID))
                        {
                            foreach (var y in caracteristicasCompara.OrderBy(y => y.CHAR_ID))
                            {
                                if (c.CHAR_ID == y.CHAR_ID)//solo debe listar las caracteristicas que necesita mostrar 

                                    caracteristicas = caracteristicas.Where(z => z.CHAR_ID != y.CHAR_ID).ToList();
                                // visible=" style='display:none'";
                            }
                        }
                        #endregion

                        if (caracteristicas != null)
                        {
                            Int16 cuenta = 0;

                            // var lista = caracteristicas.OrderBy(Y => Y.CHAR_LONG).ToList();
                            var lista = caracteristicas.Where(Y => Y.CHAR_LONG != null).ToList();//Solo que liste los periodos que pertenecen a la tarifa
                            // lista.ForEach(c =>
                            foreach (var c in caracteristicas.OrderBy(c => c.CHAR_ID))
                            {

                                //  String visible = " style='display:none'";
                                String visible = "";
                                #region visible

                                // foreach (var x in caracteristicas.OrderBy(x => x.CHAR_ID))
                                //{
                                //foreach (var y in caracteristicas_dscPlantillaxTarifa.OrderBy(y => y.CHAR_ID))
                                //{
                                //    if (c.CHAR_ID == y.CHAR_ID)//solo debe listar las caracteristicas que necesita mostrar 

                                //        caracteristicas = caracteristicas.Where(z => z.CHAR_ID != y.CHAR_ID).ToList();
                                //    visible = "";
                                //}

                                //}
                                #endregion


                                DTOLicenciaCaracteristica licCar = new DTOLicenciaCaracteristica();
                                licCar.CodigoCaracteristica = c.CHAR_ID;
                                licCar.CodigoLic = codigoLic;
                                licCar.DescCarateristica = c.CHAR_LONG;
                                licCar.FechaCaracteristicaLic = c.START;
                                licCar.Tipo = c.LIC_VAL_ORIGEN;
                                licCar.Valor = c.LIC_CHAR_VAL;
                                licCar.EsCaractAlterada = c.FLG_MANUAL;
                                licCar.CaractAlteradaDesc = c.COMMENT_FLG;

                                /*begin addon dbs - cambio por incluir planificacion de factura liencia*/
                                BLLicenciaPlaneamiento servLicPlan = new BLLicenciaPlaneamiento();
                                string desPeriodo = "NO HAY MES";
                                /*end addon dbs - cambio por incluir planificacion de factura liencia*/

                                shtml.Append("<tr class='k-grid-content'" + visible + ">");
                                shtml.AppendFormat("<td ><input type='hidden'  id='hidIdCaractDsc_{2}' value='{1}' /> {0}</td>", licCar.DescCarateristica, licCar.CodigoCaracteristica, cuenta);
                                shtml.AppendFormat("<td >{0}</td>", desPeriodo);
                                shtml.AppendFormat("<td ><input type='hidden'  id='hidIdLic_{1}' value='{2}' /><input type='hidden'  id='hidTipo_{1}' value=T />{0}</td>", licCar.Tipo, cuenta, codigoLic);
                                shtml.AppendFormat("<td >{0}</td>", licCar.FechaCaracteristicaLic.HasValue ? licCar.FechaCaracteristicaLic.Value.ToString("dd/MM/yyyy HH:mm") : "");
                                shtml.AppendFormat("<td ><input type='text' id='{1}' class='cssValCaractDsc'  value='{0}' style='width:150px;' maxlength='18'    /></td>", (licCar.Valor != null ? licCar.Valor.Value.ToString("N4") : ""), cuenta);
                                //class='cssValCaract k-formato-numerico'
                                shtml.Append("</td>");
                                //shtml.Append("<td ></td >");
                                //shtml.Append("<td ></td >");
                                shtml.AppendFormat("<td ><input type='checkbox' id='checkFlagChar_{0}' class='cssTabLicGridCheck' {1} /></td>", cuenta, licCar.EsCaractAlterada.HasValue && licCar.EsCaractAlterada.Value ? " checked='checked'" : string.Empty);
                                shtml.AppendFormat("<td ><textarea id='txtComentChar_{0}'  {2} >{1}</textarea></td>", cuenta, licCar.CaractAlteradaDesc, licCar.EsCaractAlterada.HasValue && licCar.EsCaractAlterada.Value ? string.Empty : " disabled=disabled ");
                                shtml.Append("</tr>");
                                cuenta++;
                                // });
                            }

                            shtml.Append("</table>");
                            retorno.message = shtml.ToString();
                            retorno.result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCaracteristica", ex);
            }
            if (retorno.message == null) retorno.message = "";
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region DescuentoValidacion
        public JsonResult ObtenerDescuento(decimal LIC_DISC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;

                    BEDescuentos dato = new BLLicenciaDescuento().ObtieneDescuento_DiscLic(owner, LIC_DISC_ID);

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


        public JsonResult ActualizaDescuento(decimal LIC_DISC_ID, string NomDesc, decimal MontoDesc)
        {
            Resultado retorno = new Resultado();


            try
            {
                if (!isLogout(ref retorno))
                {
                    string Usuario = UsuarioActual;
                    new BLLicenciaDescuento().ActualizaDescuentoLICencia(LIC_DISC_ID, NomDesc, MontoDesc, Usuario);

                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {

                retorno.result = 0;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ActualizarMontoCalculadoDescuentoLicencia(decimal CodigoLicencia)
        {
            Resultado retorno = new Resultado();

            try
            {

                var resp = new BLLicenciaDescuento().ActualizaDescuentoLicenciaCalc(CodigoLicencia);

                retorno.result = 1;

            }catch(Exception ex)
            {
                retorno.result = 0;
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}