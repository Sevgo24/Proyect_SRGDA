/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    var id = GetQueryStringParams("set");
    loadEstados("ddlEstado");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Parámetro - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtDescripcion").focus();
        $("#lblEstado").hide();
        $("#ddlEstado").hide();

    } else {
        $("#divTituloPerfil").html("Tipos de Parámetro - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {

    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoParametro/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoParametro/";
    });

    $("#btnGrabar").on("click", function () {
        grabarTipoParametro();
    });
});

function grabarTipoParametro() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        grabar();
    }
}

var grabar = function () {
    var estado = $("#ddlEstado").val();
    var TipoObservacion = {
        PAR_TYPE: $("#txtid").val(),
        PAR_DESC: $("#txtDescripcion").val(),
        PAR_OBSERV: $("#txtObservacion").val(),
        ESTADO: estado
    };
    $.ajax({
        url: "../TipoParametro/Insertar",
        data: TipoObservacion,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../TipoParametro/Index";
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
        url: "../TipoParametro/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.PAR_TYPE);
                    $("#txtDescripcion").val(tipo.PAR_DESC);
                    $("#txtObservacion").val(tipo.PAR_OBSERV);
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