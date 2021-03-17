/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    $("#txtDescripcion").focus();

    var id = GetQueryStringParams("set");
    loadEstados("ddlEstado");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de  Correos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblEstado").hide();
        $("#ddlEstado").hide();

    } else {
        $("#divTituloPerfil").html("Tipos de Correos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoCorreo/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoCorreo/";
    });

    $("#btnGrabar").on("click", function () {
        grabarTipoCorreo();
    });

});

function grabarTipoCorreo() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        grabar();
    }
}

var grabar = function () {
    var estado = $("#ddlEstado").val();
    var TipoCorreo = {
        MAIL_TYPE: $("#txtid").val(),
        MAIL_TDESC: $("#txtDescripcion").val(),
        MAIL_OBSERV: $("#txtObservacion").val(),
        ESTADO: estado
    };
    $.ajax({
        url: "../TipoCorreo/Insertar",
        data: TipoCorreo,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../TipoCorreo/Index";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../TipoCorreo/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.MAIL_TYPE);
                    $("#txtDescripcion").val(tipo.MAIL_TDESC);
                    $("#txtObservacion").val(tipo.MAIL_OBSERV);
                    if (tipo.ENDS == null) {
                        loadEstados("ddlEstado", 1);
                    }
                    else {
                        loadEstados("ddlEstado", 2);
                    }
                }
            }
                else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}