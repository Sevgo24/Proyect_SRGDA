/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#txtDescripcion").focus();
    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();

    var id = GetQueryStringParams("set");
    loadEstados("ddlEstado");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Teléfonos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblEstado").hide();
        $("#ddlEstado").hide();

    } else {
        $("#divTituloPerfil").html("Tipos de Teléfonos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtObservacion").attr("disabled", "disabled");
        $("#ddlEstado").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#txtObservacion").removeAttr('disabled');
        $("#ddlEstado").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoTelefono/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtObservacion").attr("disabled", "disabled");
        $("#ddlEstado").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    });

    $("#btnGrabar").on("click", function () {
        grabarTipoTelefono();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../TipoTelefono/";
    });

});

function grabarTipoTelefono() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        grabar();
    }
}

var grabar = function () {
    var estado = $("#ddlEstado").val();
    var desc = $("#txtDescripcion").val();
    if (desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var TipoTelefono = {
            PHONE_TYPE: $("#txtid").val(),
            PHONE_TDESC: $("#txtDescripcion").val(),
            PHONE_TOBSERV: $("#txtObservacion").val(),
            ESTADO: estado
        };
        $.ajax({
            url: "../TipoTelefono/Insertar",
            data: TipoTelefono,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoTelefono/";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../TipoTelefono/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.PHONE_TYPE);
                    $("#txtDescripcion").val(tipo.PHONE_TDESC);
                    $("#txtObservacion").val(tipo.PHONE_TOBSERV);
                    if (tipo.ENDS == null) {
                        loadEstados("ddlEstado", 1);
                    }
                    else {
                        loadEstados("ddlEstado", 2);
                    }
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}