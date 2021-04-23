using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Proyect_Apdayc.Clases
{
    public class Constantes
    {
        public class CodigoAplicacion
        {
            public const int CODE_ERROR_PERSONALIZADO = 999;
            public const int CODE_ERROR_SESION_TIME_OUT = 998;
            public const int CODE_ERROR_NO_DATA = 997;
        }

        public class Mensaje_Sunat
        {
            public const string MSG_DOK_SUNAT = "Documento enviado a Sunat."; //DOK
            public const string MSG_SUNAT_DOK = "DOK"; //DOK
            public const string MSG_EXISTE_SUNAT = "Error al cargar : Documento ya existe."; //FIR
            public const string MSG_RCH_SUNAT = "Documento rechazado por el Tipo de Documento de Identidad a una Factura.";
            public const string MSG_ERROR = "Error en la Suite.";

            public const string MSG_FIR_SUNAT = "FIR";
            public const string MSG_FIR_SUNAT_OK = "DOCUMENTO FIRMADO POR SUNAT";

            public const string MSG_ERROR_PDF = "Error en la Suite - No se pudo cargar el documento PDF";
            public const string MSG_ACEPTADO = "ACEPTADO";
            public const string MSG_RECHAZADO = "RECHAZADO";

            public const string ERDTE = "EL COMPROBANTE YA EXISTE";

            public const string REENVIO = "NO SE OBTUVO RESPUESTA DE SUNAT - REENVIAR DOCUMENTO";

            public const string MSG_ANULACION_EXITOSA = "OK\nProceso Finalizado";
        }

        public class MensajeGenerico
        {
            public const string MSG_ERROR_GRABAR = "No se ha podido guardar. En caso que persista el problema, contácte con el administrador.";
            public const string MSG_ERROR_ELIMINAR = "No se ha podido eliminar el registro, verifíque que no tenga información asociado a este registro.";
            public const string MSG_ERROR_GENERICO = "Se ha producido un error y este ha sido registrado. En caso que persista el problema, contácte con el administrador.";
            public const string MSG_ERROR_REPORTE = "No se encontró registro para este reporte.";
            public const string MSG_OK_GRABAR = "Se ha guardado exitosamente.";
            public const string MSG_OK_ELIMINAR_SUNAT = "Se ha anulado exitosamente, documento enviado a sunat.";
            public const string MSG_OK_ELIMINAR = "Se ha anulado exitosamente.";
            public const string MSG_OK_GENERICO = "Se ha realizado la acción exitosamente.";
            public const string MSG_LOGOUT = "La Sesion actual ha caducado.";
            public const string MSG_ERROR_VALIDACION_NUMDOC = "Para poder registrar, el Socio de negocio debe tener un número de nocumento.";
            public const string MSG_SIN_PERMISO_USUARIO_OFI = "La Oficina a la que pertenece el usuario no tiene privilegios para actualizar la información.";
            public const string MSG_SIN_PERMISO_EDIT_LIC = "La Oficina a la que pertenece el usuario no tiene privilegios para actualizar la información.";
            public const string MSG_SIN_PERMISO_INS_LIC_EST = "Las oficinas a la que corresponden los agentes del establecimiento, no corresponde a la oficina del usuario logeado. Seleccione otro establecimiento.";
        }
        public class MensajeLicenciamiento
        {
            public const string MSG_VALIDACION_PERIODO = "El periodo ya tiene una planificación registrada.";
            public const string MSG_VALIDACION_PERIODO_LIC_MULTIPLE = "El Periodo ya existe en las Licencias Asociadas(Hijas)";
        }

        public class ClasesDireccion
        {
            public const string URBANIZACION = "URB";
            public const string INTERIOR = "INT";
            public const string ETAPA = "ETP";
            public const string VIA = "VIA";

        }

        public enum ObservacionType
        {
            usuarioDerecho = 2,
            recaudadores = 3,
            asociaciones = 4,
            gruposEmpresariales = 5,
            empleados = 6,
            proveedores = 7,
            tercerosOtros = 8,
            oficinasRecaudo = 9,
        }

        public class ENTIDAD
        {
            public const decimal USUARIODERECHO = 2;
            public const decimal RECAUDADOR = 3;
            public const decimal ASOCIACION = 4;
            public const decimal GRUPOEMPRESARIAL = 5;
            public const decimal EMPLEADO = 6;
            public const decimal PROVEEDOR = 7;
            public const decimal OTROS = 8;
            public const decimal OFICINARECAUDO = 9;
            public const decimal LICENCIAMIENTO = 10;


        }

        public class EstadoDetReq
        {
            public const string DENEGADO = "Denegado";
            public const string APROBADO = "Aprobado";
            public const string APROBADO_PARCIAL = "Aprobado Parcialmente";

        }

        public class EstadoReqDinero
        {
            public const decimal ABIERTO = 1;
            public const decimal PENDIENTE = 2;
            public const decimal ATENDIDO = 3;
            public const decimal ENTREGADO = 4;
            public const decimal RENDIDO = 5;
            public const decimal ANULADO = 6;
        }

        public class Modulo
        {
            public const decimal TARIFA = 3;
            public const decimal LICENCIAMIENTO = 1;
        }

        public class SaldoLegalizacion
        {
            public const string FAVOR = "F";
            public const string CONTRA = "C";
            public const string IGUAL = "I";
        }
        public class MensajeRetorno
        {
            public const int ERROR = 0;
            public const int OK = 1;
            public const int LOGOUT = 2;
            public const int NO_DATA = 3;
            public const int NO_ACCESS = 4;
            public const int DATA_FOUND = 5;
            public const int NO_TERRITORIO = 6;

        }
        public class DTOLogin
        {

            public string usu { get; set; }
            public string contra { get; set; }
            public string type { get; set; }
        }

        public class Sesiones
        {
            public const string Usuario = "__Usuario";
            public const string Nombre = "__Nombre";
            public const string CodigoOficina = "__CoigoOficina";
            public const string Oficina = "__NombreOficina";
            public const string CodigoPerfil = "__CodigoPerfil";
            public const string Perfil = "__NombrePerfil";
            public const string DetallePerfil = "__DetalleDePerfil";
            public const string CodigoUsuarioOficina = "__CodigoUsuarioOficina";
            public const string CodigoPerdilUsuario = "__CodigoPerdilUsuario";

            public const string MenuCambiaRol = "___CambioRol";
            public const string MenuCargado = "___loadMenu";
        }

        public class OrigenCaracteristica
        {
            public const string Plantilla = "P";
            public const string Manual = "M";
        }

        public class FormatoReporte
        {
            public const string PDF = "PDF";
            public const string EXCEL = "XLS";
            public const string IMAGEN = "PNG";

        }

        public class FacturaIndDet
        {
            public const string UNA = "0";
            public const string VARIAS = "1";
        }

        public class FacturaFase
        {
            public const string BORRADOR = "1";
            public const string DEFINITIVA = "2";
            public const string ARCHIVADO = "3";
        }

        public class FacturaBorrador
        {
            public const int SIN_FACTURAR = 0;
            public const int FACTURADO = 1;
        }

        public class FacturaTipo
        {
            public const int BOLETA = 1;
            public const int FACTURA = 2;
            public const int NC = 3;
            public const int NB = 4;
        }

        public class TipoComprobanteE
        {
            public const string FACTURA = "1";
            public const string BOLETA = "3";
            public const string NC = "7";
            public const string ND = "9";
        }

        public class MensajeEjecutarAccion
        {
            public const string MSG_ERROR_ACCESO_ACCION = "El usuario con el perfil actual no puede ejecutar la acción. No tiene asignado el permiso respectivo.";
        }


        public class OrigenDocumento
        {
            public const string ENTRADA = "I";
            public const string SALIDA = "O";
            public const string EXTERNO = "E";
        }

        public class EstadoFactura
        {
            public const string CANCELADA_PARCIAL = "CANCELADA PARCIAL";//1
            public const string CANCELADO = "CANCELADO";//2
            public const string PENDIENTE_PAGO = "PENDIENTE DE PAGO";//3
            public const string ANULADA = "ANULADA";//4
            public const string SOLICITUD_Nota_Credito = "SOLICITUD NC";//5
            public const string SOLICITUD_QUIEBRA = "SOLICITUD QUIEBRA";//6
            public const string CASTIGO = "CASTIGO";//11
            public const string COBRANZA_DUDOSA = "PROV. COBRANZA DUDOSA";//12

            public const string NC_DEVOLUCION= "NC  - Devolución";//7
            public const string NC_F1_F2 = "NC  - F1,F2";//7
            public const string NC_ANULACION = "NC - Anulación";//7
            public const string NC_DESCUENTO = "NC - Descuento";//7
            public const string NC_ANULADO = "NC - Anulada";//7
            public const string NC_OTRO = "NC - Otro";//7
            //public const string NC_ANULACION = "NC Anulación";//7

        }

        public class EstadosFacturaValor
        {
            public const decimal NC_DEVOLUCION = 1;
            public const decimal NC_ANULACION = 2;
            public const decimal NC_DESCUENTO = 3;
            public const decimal NC_ANULADO = 4;
            public const decimal NC_OTRO = 5;
        }

        public class CaracteristicaReq
        {
            public const string SI = "1";
            public const string NO = "0";
        }

        public class TipoCalculoTestTarifa
        {
            public const int CARACTERISTICAS = 1;
            public const int DESCUENTOS = 2;
        }

        public class EstadoPeriodo
        {
            public const string PARCIAL = "P";
            public const string TOTAL = "T";
            public const string ABIERTO = "A";
            public const string OTROS = "O";
            public const string TODOS = "0";
        }

        public class EstadoPeriodoDes
        {
            public const string PARCIAL = "PARCIAL";
            public const string TOTAL = "TOTAL";
            public const string ABIERTO = "ABIERTO";
            public const string OTROS = "OTROS";
            public const string TODOS = "TODOS";
        }

        public class TipoPersona
        {
            public const string JURIDICO = "J";
            public const string NATURAL = "N";
        }

        public class EstadosConfirmacion
        {
            //public const string CONFIRMACION = "CONFIRMACION";
            //public const string SIN_CONFIRMACION = "SIN CONFIRMACION";
            //public const string RECHAZADO = "RECHAZADO";
            public const string CONFIRMACION = "C";
            public const string SIN_CONFIRMACION = "S";
            public const string RECHAZADO = "R";
        }

        public class AccionVista
        {
            public const string Nuevo = "I";
            public const string Modificacion = "U";
        }

        public class EstadosMultirecibo
        {
            public const decimal Aplicado = 1;
            public const decimal Pendiente_Aplicacion = 0;
            public const decimal Parcialmente_Aplicacion = 2;
        }

        public class TipoMoneda
        {
            public const string SOLES = "PEN";
            public const string DOLARES = "44";
        }

        public class TipoCobro
        {
            public const string SIMPLE = "S";
            public const string COMPUESTO = "C";
        }

        public class Bancos
        {
            public const string BANCO_CONTINENTAL = "27";
            public const string BANCO_DE_CREDITO = "28";
            public const string BANCO_SCOTIABANK = "29";
            public const string BIF = "30";
            public const string BANCO_DE_LA_NACION = "31";
            public const string BANCO_INTERBANK = "32";
            public const string CANJE_DOCUMENTO = "33";
            public const string DEPOSITO_BANCO = "34";
            public const string DEPOSITO_CHEQUE = "35";
            public const string DEPOSITO_EFECTIVO = "36";
        }

        public class TipoEscalaRango
        {
            public const string VALOR = "V";
            public const string PORCENTAJE = "P";
        }

        public class EstadoVigencia
        {
            public const decimal ACTIVO = 1;
            public const decimal INACTIVO = 0;
        }

      
    }
}
