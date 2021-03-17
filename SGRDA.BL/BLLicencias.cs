using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using SGRDA.BL.WorkFlow;

namespace SGRDA.BL
{
    public class BLLicencias
    {
        public List<BELicencias> usp_Get_LicenciaPage(string owner, decimal LIC_ID, decimal LIC_TYPE, decimal LICS_ID, string CUR_ALPHA, decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, decimal LICMAS, decimal BPS_GROUP, decimal BPS_GROUP_FACT, int confecha, DateTime ffiniauto, DateTime ffinauto, string desc_artista, decimal cod_artista_sgs, int pagina, int cantRegxPag, decimal idOficina, int estadoLic)
        {
            var datos = new DALicencias().usp_Get_LicenciaPage(owner, LIC_ID, LIC_TYPE, LICS_ID, CUR_ALPHA, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, LICMAS, BPS_GROUP, BPS_GROUP_FACT, confecha, ffiniauto, ffinauto, desc_artista, cod_artista_sgs, estadoLic, pagina, cantRegxPag, idOficina);
            /*     datos.ForEach(x =>
                 {
                    // var objTipo = new BLREC_LIC_TYPE().GET_REC_LIC_TYPE_X_COD(x.LIC_TYPE);
                     //var objEstado = new BLREC_LIC_STAT().GET_REC_LIC_STAT_X_COD(x.LICS_ID);
                     var objEstado = new BL_WORKF_STATES().ObtenerEstados(GlobalVars.Global.OWNER, x.LICS_ID);
                    // var objModalidad = new BLREC_MODALITY().GET_REC_MODALITY_X_COD(x.MOD_ID);
                    // var objUsu = new BLSocioNegocio().ObtenerDatos(x.BPS_ID, GlobalVars.Global.OWNER);
                    // var objEst = new BLEstablecimiento().ObtenerNombreEstablecimiento(GlobalVars.Global.OWNER, x.EST_ID);

                    // x.Tipo = objTipo == null ? "" : objTipo.LIC_TDESC;
                     x.Estado = objEstado == null ? "" : objEstado.WRKF_SLABEL;
                   //  x.Modalidad = objModalidad == null ? "" : objModalidad.MOD_DEC;
                   //  x.Establecimiento = objEst == null ? "" : objEst.EST_NAME;
                   //  x.UsuDerecho = string.Format("{0} {1} {2} {3}", objUsu.BPS_NAME, objUsu.BPS_FIRST_NAME, objUsu.BPS_FATH_SURNAME, objUsu.BPS_MOTH_SURNAME);
                     //if (objUsu != null)
                     //{
                     //    if (objUsu.TAXT_ID == 1)
                     //    {
                     //        x.UsuDerecho = objUsu.BPS_NAME;
                     //    }else{
                     //        x.UsuDerecho = string.Concat("{0}{1}{2}{3}{4}",objUsu.BPS_FIRST_NAME," ",objUsu.BPS_FATH_SURNAME," ",objUsu.BPS_MOTH_SURNAME);
                     //};
                 });*/

            return datos;
        }
        public List<BELicencias> usp_Get_LicenciaPage2(string owner, decimal LIC_ID,  decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, int pagina, int cantRegxPag, decimal idOficina)
        {
            var datos = new DALicencias().usp_Get_LicenciaPage2(owner, LIC_ID, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, pagina, cantRegxPag, idOficina);
          

            return datos;
        }
        public int Eliminar(BELicencias LIC)
        {
            return new DALicencias().Eliminar(LIC);
        }
        public decimal Insertar(BELicencias entidad)
        {
            return new DALicencias().Insertar(entidad);
        }
        public decimal Actualizar(BELicencias entidad)
        {
            return new DALicencias().Actualizar(entidad);
        }
        public List<string> ListarTabsXEstadoLic(string estadoTipoLic, string empresa)
        {
            return new DALicencias().ListarTabsXEstadoLic(estadoTipoLic, empresa);
        }
        public BELicencias ObtenerLicenciaXCodigo(decimal idLicencia, string owner)
        {
            return new DALicencias().ObtenerLicenciaXCodigo(idLicencia, owner);
        }
        public bool ValidarLicenciaMultiple(string owner, decimal tipoLic, decimal idLicValidar)
        {

            return new DALicencias().ValidarLicenciaMultiple(owner, tipoLic, idLicValidar);

        }
        public int UpdateLicenciaFacturacion(decimal codLic, string owner, string docReq, string actReq, string plaReq, string desVis, string Envio, decimal facGruop, decimal facForm, string LIC_EMI_MEN)
        {
            return new DALicencias().UpdateLicenciaFacturacion(codLic, owner, docReq, actReq, plaReq, desVis, Envio, facGruop, facForm, LIC_EMI_MEN);
        }
        public int ActualizarEstadoLicencia(string owner, decimal idLic, decimal sidWrkf)
        {
            return new DALicencias().ActualizarEstadoLicencia(owner, idLic, sidWrkf);
        }
        public int ActualizarEstadoLicenciaXActionsMapping(string owner, decimal idLic, decimal amidWrkf)
        {
            return new DALicencias().ActualizarEstadoLicenciaXActionsMapping(owner, idLic, amidWrkf);
        }

        #region CADENAS
        //Lista Licencias Hijas X Cod Licencia Padre

        public List<BELicencias> ListarLicenciaHijasxCodMult(string owner, decimal CodLicPadre)
        {
            return new DALicencias().ListarLicenciasHijasxCodMultiple(owner, CodLicPadre);
        }
        //Inactivar Licencias Hijas
        public void InactivarLicenciasHijas(decimal CodLic, decimal licmaster)
        {
            string owner = GlobalVars.Global.OWNER;
            new DALicencias().InactivaLicenciasHijas(CodLic, owner, licmaster);
        }

        //validar Licencias Hijas 
        public int ValidarLicenciasMultiplesHijas(decimal COodLic)
        {
            return new DALicencias().ValidarLicenciaMultipleHija(COodLic);
        }
        //Validar Licencias Padres
        public int ValidarLicenciasMultiplesPadres(decimal CodLic)
        {
            return new DALicencias().ValidarLicenciaMultiplePadre(CodLic);
        }

        //Eliminar Licencias Padres y Hijas

        public int EliminarLicPadreHija(BELicencias LIC)
        {
            return new DALicencias().EliminarLicPadreHija(LIC);
        }

        //Listar Las Licencias Hijas X Padre
        public List<BELicencias> ListarLicHijasxPadre(decimal LicId)
        {
            return new DALicencias().ListarLienciasHijas(LicId);
        }
        //Listar Licencias Planeamiento x Hija

        public List<BELicenciaPlaneamiento> ListarLIcPlanxLicHija(decimal LicId)
        {
            return new DALicencias().ListarLicPlanxLicHija(LicId);
        }
        // Valida Establecimiento insertado
        public int ValidarEstablecimientoInsert(decimal CodEst)
        {
            return new DALicencias().ValidarEstblecimientoInsert(CodEst);
        }
        //Public Recupera COdigo de licencia MEDIANTE CODIGO DE ESTABLECIMIENTO
        public decimal RecuperaCodigoLicHijxCodEst(decimal CodEst, decimal licmaster)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().RecuperaCodigoLicHijxCodEst(CodEst, owner, licmaster);
        }
        //VALIDA SI UNA LICENCIA TIENE CARACTERISTICAS INSERTADAS
        public int ValidaLicenciaCaract(string owner, decimal licid)
        {
            return new DALicencias().ValidaCaractLicencia(owner, licid);

        }

        //Autogenera Codigo de licencia Padre
        public decimal AutogeneraCodLicpadre()
        {
            return new DALicencias().AutogeneraCodLimpieza();
        }

        #region  Insercion y Modificacion de licencias(XML)
        public int ActualizaLicenciasHijasXML(List<BELicencias> listalicenciashijas)
        {

            string xmlLicencia = string.Empty; //Se declara la variable xml
            xmlLicencia = Utility.Util.SerializarEntity(listalicenciashijas); //se instancia con la lista que hayamos creado
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ActualizaLicenciasHijasXML(owner, xmlLicencia);

        }

        public List<BELicencias> InsertaLicenciaHijaXML(List<BELicencias> listalicenciaxml)
        {

            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listalicenciaxml);
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().InsertaLicenciaHijaXML(owner, xmlLicencia);
        }

        /// <summary>
        /// Actualiza Licencia Facturacion de Licencias Hijas xml
        /// </summary>
        /// <param name="listalicenciashijas">Lista de Licencias Hijas</param>
        /// <returns></returns>
        public int ActualizaLicenciasFacturacionHijasXML(List<BELicencias> listalicenciashijas)
        {

            string xmlLicencia = string.Empty; //Se declara la variable xml
            xmlLicencia = Utility.Util.SerializarEntity(listalicenciashijas); //se instancia con la lista que hayamos creado
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ActualizaLicenciasFacturacionHijasXML(owner, xmlLicencia);

        }

        #endregion

        /// <summary>
        /// lISTA lICENCIAS X codigo de est
        /// </summary>
        /// <param name="ESTID"></param>
        /// <returns></returns>
        public List<BELicencias> ListaLicenciaxCodigoEst(decimal ESTID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ListaLicenciaxCodigoEst(owner, ESTID);
        }
        public List<BELicencias> ListaCodigoEstxCodigoLicencia(decimal LICID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ListaCodigoEstxCodigoLicencia(owner, LICID);
        }
        /// <summary>
        /// OBTIENE LA LICENCIA MAESTRA SI ES QUE ES LICENCIA HIJA
        /// </summary>
        /// <param name="LICID">ID DE LA LICENCIA.</param>
        /// <returns></returns>
        public BELicencias ListaLicenciaMaestraxLicid(decimal LICID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ListaLicenciaMaestraxLicid(owner, LICID);
        }

        public int ValidaModalidadLicencia(decimal LICID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ValidaModalidadLicencia(owner, LICID);
        }

        #endregion

        #region descuentos

        public List<BELicencias> ListarLicenciasxCodigoSocio(string owner, decimal bpsid)
        {
            return new DALicencias().ListarLicenciasxCodigoSocio(owner, bpsid);
        }

        #endregion  

        #region Megaconciertos

        public decimal ObtienePrimeraFacturaAutorizacion(decimal lic_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ObtienePrimeraFacturaAutorizacion(owner, lic_id);
        }

        public decimal ObtienePrimeraFacturaCandesPlanilla(decimal lic_id, decimal mes, decimal anio)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ObtienePrimeraFacturaCandesPlanilla(owner, lic_id, mes, anio);
        }

        public string ObtieneSerieNumFacturaCandenaPlanilla(decimal lic_id, decimal mes, decimal anio)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ObtieneSerieNumFacturaCandenaPlanilla(owner, lic_id, mes, anio);
        }

        public string ObtieneSerieNumFacturaEspectLic(decimal lic_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ObtieneSerieNumFacturaEspectLic(lic_id);
        }


        #endregion

        #region FACTURACION_MASIVA
        public int ActualizarMontoLirics(List<BELicencias> ListaMontoLirics)
        {
            String listaActualizarMontoLiricsXML = string.Empty;
            listaActualizarMontoLiricsXML = Utility.Util.SerializarEntity(ListaMontoLirics);
            //var licenciasNoAlDia = new DALicenciaPlaneamiento().ValidaLicenciaAlDia(owner, listalicenciaxml);
            return new DALicencias().ActualizarMontoLirics(listaActualizarMontoLiricsXML);
        }

        #endregion
        #region Licencia Validacion Orden 
        public int ValidaLicenciaPlanificacionAutorizacion(decimal LIC_ID, int ACCION, decimal LIC_PL_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ValidaLicenciaPlanificacionAutorizacion(LIC_ID, ACCION, LIC_PL_ID);
        }
        #endregion

        #region ENVIAR AL HISTORICO

        public decimal EnviarAlHistorico(decimal lic_id, decimal bps_id, string UsuarioCreacion)
        {
            return new DALicencias().EnviarAlHistorico(lic_id, bps_id, UsuarioCreacion);
        }
        #endregion

        #region TRASLADO DE LICENCIAS
        public int ActualizaLicenciasDivision(List<BELicencias> listalicenciastraslado, decimal DIV_ID, string usuariomodifi, List<BELicenciaDivisionAgente> listaAgentesSeleccionados)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmlLicencia = string.Empty; //Se declara la variable xml
            string xmlAgentes = string.Empty; //Se declara la variable xml
            xmlLicencia = Utility.Util.SerializarEntity(listalicenciastraslado); //se instancia con la lista que hayamos creado

            xmlAgentes = Utility.Util.SerializarEntity(listaAgentesSeleccionados); //se instancia con la lista que hayamos creado
            return new DALicencias().ActualizaLicenciasDivision(xmlLicencia, xmlAgentes, DIV_ID, owner, usuariomodifi);

        }


        #endregion
        #region  LICENCIA ACTUALIZA CADENA

        public int ActualizaLicenciaCadena(decimal lic_id, decimal lic_master, string USUARIO)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ActualizaLicenciaCadena(owner, lic_id, lic_master, USUARIO);
        }

        #endregion

        #region ACTUALIZAR MONTO BRUTO - DESC- NET 
        public int ActualizaLicenciaMontos(decimal lic_id, decimal BRUTO, decimal DESC, decimal NET, decimal DESC_REDOND)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ActualizaLicenciaMontos(lic_id, BRUTO, DESC, NET, DESC_REDOND);
        }
        #endregion

        #region Validar Usuario Moroso
        public int ValidarUsuarioMoros(decimal BPS_ID)
        {
            return new DALicencias().ValidarUsuarioMoroso(BPS_ID);
        }


        #endregion
        public int ValidaSocioTelefCorreo(decimal BPS_ID, decimal LIC_ID)
        {
            return new DALicencias().ValidaSocioTelefCorreo(BPS_ID, LIC_ID);
        }
        public int ValidaLicenciaLocalRequerimiento(decimal CodigoLicencia, int CodigoRequerimiento)
        {
            return new DALicencias().ValidaLicenciaLocalRequerimiento(CodigoLicencia, CodigoRequerimiento);
        }


        public List<BELicenciaTipoInactivacion> ListarTipoInactivacionLicencia()
        {
            return new DALicencias().ListarTipoInactivacionLicencia();
        }
        public int ValidarFacturacion(decimal LIC_ID, decimal OFF_ID)
        {
            return new DALicencias().ValidarFacturacion(LIC_ID, OFF_ID);
        }
    }
}
