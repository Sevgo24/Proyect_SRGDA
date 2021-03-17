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
    ////---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Observaciones - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtDescripcion").focus();
        $("#lblEstado").hide();
        $("#ddlEstado").hide();
    } else {
        $("#divTituloPerfil").html("Tipos de Observaciones - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        ObtenerDatos(id);
    }
    ////---------------------------------------------------------------

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoObservacion/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoObservacion/";
    });

    $("#btnGrabar").on("click", function () {
        grabarObservacion();
    });

});

function grabarObservacion() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        grabar();
    }
}

var grabar = function () {
    var estado = $("#ddlEstado").val();
    //if (estado == 1) {
    //    estado = null
    //}
    var TipoObservacion = {
        TIPO: $("#txtid").val(),
        OBS_DESC: $("#txtDescripcion").val(),
        OBS_OBSERV: $("#txtObservacion").val(),
        ESTADO: estado
    };
    $.ajax({
        url: "../TipoObservacion/Insertar",
        data: TipoObservacion,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../TipoObservacion/";
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
        url: "../TipoObservacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.TIPO);
                    $("#txtDescripcion").val(tipo.OBS_DESC);
                    $("#txtObservacion").val(tipo.OBS_OBSERV);
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