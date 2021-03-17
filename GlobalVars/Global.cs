using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalVars
{
    public class Global
    {
        public static string OWNER = ConfigurationManager.AppSettings["OWNER"];
        //public static string PREFIJO = ConfigurationManager.AppSettings["PREFIJO"];
        public static string PREFIJO = ConfigurationManager.AppSettings["PrefijoSist"];
        public static int AgenteAutorizado = Convert.ToInt32(ConfigurationManager.AppSettings["AGENTE_AUTORIZADO"]);
        public static int AgenteServicio = Convert.ToInt32(ConfigurationManager.AppSettings["AGENTE_SERVICIO"]);
        public static int tamanioPaginacion = Convert.ToInt32(ConfigurationManager.AppSettings["paginacion"]);
        public static int DiasFechaAnulacion = Convert.ToInt32(ConfigurationManager.AppSettings["DiasFechaAnulacion"]);

        //RutaDeDocumentosAlfresco
        public static string RutaDocumentoLyrics = ConfigurationManager.AppSettings["RutaDocumentoLyrics"];
        public static string RutaEntradaDocumentosAlfresco = ConfigurationManager.AppSettings["RutaEntradaDocumentosAlfresco"];
        public static string RutaSalidaDocumentosAlfresco = ConfigurationManager.AppSettings["RutaSalidaDocumentosAlfresco"];
        public static string rutaUploadAlfresco = ConfigurationManager.AppSettings["rutaUploadAlfresco"];
        public static string RutaSalidaWeb = ConfigurationManager.AppSettings["RutaSalidaWeb"];
        public static string UsuarioAlfresco = ConfigurationManager.AppSettings["UsuarioAlfresco"];
        public static string ContraseñaAlfreso = ConfigurationManager.AppSettings["ContraseñaAlfreso"];
        public static string EnviarDocumento = ConfigurationManager.AppSettings["ActivarAlfresco"];
        public static string ActivarAlfresco_Cobros = ConfigurationManager.AppSettings["ActivarAlfresco_Cobros"];
        public static string CarpetaDocumentosAutoGenerados = ConfigurationManager.AppSettings["CarpetaDocumentosAutoGenerados"];
        
        //RUTA DOCUMENTOS DE BEC 
        public static string RutaFisicaImgBECDoc = ConfigurationManager.AppSettings["RutaFisicaImgBECDoc"];
        public static string RutaWebImgBecDoc = ConfigurationManager.AppSettings["RutaWebImgBecDoc"];


        //public static string DocumentoBailesEspectaculos = ConfigurationManager.AppSettings["RutaDocBailesEspectaculos"];
        //public static string DocumentoFichaInscripcion = ConfigurationManager.AppSettings["RutaDocFichaInscripcion"];

        public static string DireccionApdayc = ConfigurationManager.AppSettings["Direccionoficina"];
        
        public static string PagoReparto = ConfigurationManager.AppSettings["PAGO_REPARTIR"];

        public static string RutaFisicaImgObjects = ConfigurationManager.AppSettings["RutaFisicaImgObjects"];
        public static string RutaWebImgObjects = ConfigurationManager.AppSettings["RutaWebImgObjects"];

        public static string RutaPlantillaLicencia = ConfigurationManager.AppSettings["RutaPlantillaLicencia"];
        public static string RutaPlantillaLicenciaWeb = ConfigurationManager.AppSettings["RutaPlantillaLicenciaWeb"];
        public static string RutaPlantillaCorreo = ConfigurationManager.AppSettings["RutaPlantillaCorreo"];
        public static string RutaPlantillaCorreoWeb = ConfigurationManager.AppSettings["RutaPlantillaCorreoWeb"];

        public static string RutaDocLicSalida = ConfigurationManager.AppSettings["RutaDocLicSalida"];
        public static string RutaDocLicSalidaWeb = ConfigurationManager.AppSettings["RutaDocLicSalidaWeb"];

        public static string RutaDocLicEntrada = ConfigurationManager.AppSettings["RutaDocLicEntrada"];
        public static string RutaDocLicEntradaWeb = ConfigurationManager.AppSettings["RutaDocLicEntradaWeb"];
        
        public static string RutaTabDocumentoLicWeb = ConfigurationManager.AppSettings["RutaWebImgLicenciaDoc"];
        public static string RutaTabDocumentoLic = ConfigurationManager.AppSettings["RutaFisicaImgLicenciaDoc"];

        public static string RutaTabDocumentoDerechoWeb = ConfigurationManager.AppSettings["RutaWebImgDerecho"];
        public static string RutaTabDocumentoDerecho = ConfigurationManager.AppSettings["RutaFisicaImgDerecho"];

        public static string RutaTabDocumentoRecaudadorWeb = ConfigurationManager.AppSettings["RutaWebImgRecaudador"];
        public static string RutaTabDocumentoRecaudador = ConfigurationManager.AppSettings["RutaFisicaImgRecaudador"];

        public static string RutaTabDocumentoProveedorWeb = ConfigurationManager.AppSettings["RutaWebImgProveedor"];
        public static string RutaTabDocumentoProveedor = ConfigurationManager.AppSettings["RutaFisicaImgProveedor"];

        public static string RutaTabDocumentoEmpleadoWeb = ConfigurationManager.AppSettings["RutaWebImgEmpleado"];
        public static string RutaTabDocumentoEmpleado = ConfigurationManager.AppSettings["RutaFisicaImgEmpleado"];

        public static string RutaTabDocumentoAsociacionWeb = ConfigurationManager.AppSettings["RutaWebImgAsociacion"];
        public static string RutaTabDocumentoAsociacion = ConfigurationManager.AppSettings["RutaFisicaImgAsociacion"];

        public static string RutaTabDocumentoSocioWeb = ConfigurationManager.AppSettings["RutaWebImgSocio"];
        public static string RutaTabDocumentoSocio = ConfigurationManager.AppSettings["RutaFisicaImgSocio"];

        public static string RutaTabDocumentoEstablecimientoWeb = ConfigurationManager.AppSettings["RutaWebImgEstablecimiento"];
        public static string RutaTabDocumentoEstablecimiento = ConfigurationManager.AppSettings["RutaFisicaImgEstablecimiento"];

        public static string RutaTabDocumentoCampaniaWeb = ConfigurationManager.AppSettings["RutaWebImgCampania"];
        public static string RutaTabDocumentoCampania = ConfigurationManager.AppSettings["RutaFisicaImgCampania"];

        public static string RutaTabDocumentoOficinaWeb = ConfigurationManager.AppSettings["RutaWebImgOficina"];
        public static string RutaTabDocumentoOficina = ConfigurationManager.AppSettings["RutaFisicaImgOficina"];

        public static string RutaTabDocumentoContactoCallCenterWeb = ConfigurationManager.AppSettings["RutaWebImgContactoCallCenter"];
        public static string RutaTabDocumentoContactoCallCenter = ConfigurationManager.AppSettings["RutaFisicaImgContactoCallCenter"];

        public static string RutaTabDocumentoObjetoWeb = ConfigurationManager.AppSettings["RutaWebImgObjeto"];
        public static string RutaTabDocumentoObjeto = ConfigurationManager.AppSettings["RutaFisicaImgObjeto"];

        public static decimal SizeFileUpload = Convert.ToDecimal(ConfigurationManager.AppSettings["SizeFileUpload"]);

        public static decimal WrkfoId = Convert.ToDecimal(ConfigurationManager.AppSettings["WRKF_OID"]);
        
        #region "setting para tipo de objetos"
        public static string PrefijoMailUsu = ConfigurationManager.AppSettings["prefijoObjetoMailUsu"];
        public static string PrefijoMailTer = ConfigurationManager.AppSettings["prefijoObjetoMailTer"];
        public static string PrefijoDocumentoEntrada = ConfigurationManager.AppSettings["prefijoObjetoDocIn"];
 
        #endregion

        public static List<string> ListMessageReport = null;

        public static string RucApdayc = ConfigurationManager.AppSettings["RucApdayc"];

        //REF_LIB_CONFIG
        public static decimal VarIdFechaPrimeraComunicacion = Convert.ToDecimal(ConfigurationManager.AppSettings["FECHA_PRIMERA_COMUNICACION_USUARIO"]);
        public static decimal VarIdRum = Convert.ToDecimal(ConfigurationManager.AppSettings["RUM"]);
        public static decimal VarIdParametroURL = Convert.ToDecimal(ConfigurationManager.AppSettings["PAGINA_WEB"]);
        public static decimal VarIdFrecuenciaRadios = Convert.ToDecimal(ConfigurationManager.AppSettings["FRECUENCIA_RADIO"]);
        public static decimal VarIdHorario = Convert.ToDecimal(ConfigurationManager.AppSettings["HORARIO"]);
        public static Boolean RegMontoLirics = Convert.ToBoolean(ConfigurationManager.AppSettings["RegMontoLirics"]);

        #region FACTURACION ELECTRONICA
        //FACTURACION ELECTRONICA
        public static Boolean FE = Convert.ToBoolean(ConfigurationManager.AppSettings["FE"]);
        public static Boolean ReporteElectronico = Convert.ToBoolean(ConfigurationManager.AppSettings["REPORTE_ELECTRONICO"]);

        public static string RutaFisicaFE = ConfigurationManager.AppSettings["RutaFisicaFE"];
        public static string RutaWebFE = ConfigurationManager.AppSettings["RutaWebFE"];
        public static Boolean ProcesoMasivo = Convert.ToBoolean(ConfigurationManager.AppSettings["ProcesoMasivo"]);
        public static Boolean PermitirRevertNC = Convert.ToBoolean(ConfigurationManager.AppSettings["PermitirRevertNC"]);

        #endregion
    }
}
