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
        $("#divTituloPerfil").html("Grupo de Gasto - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Grupo de Gasto - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    $("#ddlTipoGasto").attr("disabled", "disabled");
    $("#txtDesCorta").attr("disabled", "disabled");
    $("#txtPDescripcion").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        $("#ddlTipoGasto").removeAttr('disabled');
        $("#txtDesCorta").removeAttr('disabled');
        $("#txtPDescripcion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        $("#ddlTipoGasto").val(0);
        $("#txtDesCorta").val("");
        $("#txtPDescripcion").val("");
        $("#ddlTipoGasto").removeAttr('disabled');
        $("#txtDesCorta").removeAttr('disabled');
        $("#txtPDescripcion").removeAttr('disabled');
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTipoGasto").attr("disabled", "disabled");
        $("#txtDesCorta").attr("disabled", "disabled");
        $("#txtPDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "Index";
    }).button();
});

function limpiar() {

    $("#ddlTipoGasto").val(0);
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");
}

var grabar = function () {
    var tipo = $("#ddlTipoGasto").val();
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtPDescripcion").val();
    if (tipo == 0 || descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var GrupoGasto = {
            EXP_TYPE: tipo,
            EXPG_ID: descorta,
            EXPG_DESC: desc
        };

        $.ajax({
            url: "../GrupoGasto/Actualizar",
            data: GrupoGasto,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../GrupoGasto/Index";
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
        url: "../GrupoGasto/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    loadTipoGasto("ddlTipoGasto", tipo.EXP_TYPE);
                    $("#txtDesCorta").val(tipo.EXPG_ID);
                    $("#txtPDescripcion").val(tipo.EXPG_DESC);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}