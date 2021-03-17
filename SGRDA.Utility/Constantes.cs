using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SGRDA.Utility
{
    public class Constantes
    {
        public class CodigoAplicacion
        {

            public const int CODE_ERROR_PERSONALIZADO = 999;
            public const int CODE_ERROR_SESION_TIME_OUT = 998;
            public const int CODE_ERROR_NO_DATA = 997;

        }

        public class MensajeGenerico
        {
            public const string MSG_ERROR_GRABAR = "No se ha podido guardar. En caso que persista el problema, contácte con el administrador.";
            public const string MSG_ERROR_ELIMINAR = "No se ha podido eliminar el registro, verifíque que no tenga información asociado a este registro.";
            public const string MSG_ERROR_GENERICO = "Se ha producido un error inesperado y este ha sido registrado. En caso que persista el problema, contácte con el administrador.";
            public const string MSG_OK_GRABAR = "Se ha guardado exitosamente.";
            public const string MSG_OK_ELIMINAR = "Se ha eliminado exitosamente.";
            public const string MSG_OK_GENERICO = "Se ha realizado la acción exitosamente.";
            public const string MSG_LOGOUT = "La Sesion actual ha caducado.";
            public const string MSG_ERROR_VALIDACION_NUMDOC = "Para poder registrar, el Socio de negocio debe tener un número de nocumento.";
        }
        public class MensajeLicenciamiento
        {
            public const string MSG_VALIDACION_PERIODO = "El periodo ya tiene una planificación registrada.";
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
            public const decimal TARIFA =3;
            public const decimal LICENCIAMIENTO =1;
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

        public class FacturaTipo
        {
            public const int BOLETA = 1;
            public const int FACTURA = 2;
            public const int NC = 3;
            public const int NB = 4;
        }

        public class MensajeEjecutarAccion
        {
            public const string MSG_ERROR_ACCESO_ACCION = "El usuario con el perfil actual no puede ejecutar la acción. No tiene asignado el permiso respectivo.";
            public const string MSG_ERROR_ENVIO_MAIL = "Error al enviar correo electrónico.";
            public const string MSG_ERROR_SIN_PLANTILLA = "No existe la plantilla para la accion ejecutada.";
            public const string MSG_ERROR_SIN_CORREOS = "No se encontró ningún correo de destino para la notificación.";
            public const string MSG_ERROR_OBTENER_PLANTILLA = "Error al intentar obtener la plantilla. Comuníquese con el administrador.";

            public const string MSG_WARNING_ACCION_EJECUTADA = "Ya ha sido ejecutado la accion.";
            public const string MSG_WARNING_SIN_PARAMETROS = "No se ha configurado parametros para el cambio de estado.";
            public const string MSG_ERROR_SIN_REQUISITOS = "No ha cumplido los pre requisitos para cambiar el estado";
        }

    }
}