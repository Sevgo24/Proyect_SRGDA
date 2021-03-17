/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        $("#divTituloPerfil").html("Tipos de Correlativos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Tipos de Correlativos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    $("#txtTipo").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        //$("#txtTipo").removeAttr('disabled');
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoCorrelativo/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        $("#txtTipo").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "Index";
    });

});


function limpiar() {

    $("#txtDescripcion").val("");
    $("#txtTipo").val("");

}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

var grabar = function () {
    var tipo = $("#txtTipo").val();
    var desc = $("#txtDescripcion").val();

    if (tipo == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var TipoCorrelativo = {
            NMR_TYPE: $("#txtTipo").val(),
            NMR_TDESC: $("#txtDescripcion").val()
        };

        $.ajax({
            url: "../TipoCorrelativo/Actualizar",
            data: TipoCorrelativo,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoCorrelativo/Index";
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
    limpiar();
    $.ajax({
        url: "../TipoCorrelativo/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtTipo").val(tipo.NMR_TYPE);
                    $("#txtDescripcion").val(tipo.NMR_TDESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
