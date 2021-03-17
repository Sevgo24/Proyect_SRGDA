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
        $("#divTituloPerfil").html("Tipos de Gastos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Tipos de Gastos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    $("#txtDesCorta").attr("disabled", "disabled");
    $("#txtPDescripcion").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        $("#txtDesCorta").removeAttr('disabled');
        $("#txtPDescripcion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoGasto/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
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
        location.href = "../TipoGasto/";
    }).button();

});


function limpiar() {
    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");
}

function limpiarValidacion() {
    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

var grabar = function () {
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtPDescripcion").val();

    if (descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var TipoGasto = {
            EXP_TYPE: $("#txtDesCorta").val(),
            EXPT_DESC: $("#txtPDescripcion").val()
        };

        $.ajax({
            url: "../TipoGasto/Actualizar",
            data: TipoGasto,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoGasto/Index";
                    $("#mvTipoGasto").dialog("close");
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
        url: "../TipoGasto/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtDesCorta").val(tipo.EXP_TYPE);
                    $("#txtPDescripcion").val(tipo.EXPT_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function actualizar(idSel) {

    $.ajax({
        url: "../TipoGasto/Actualiza",
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            //  alert(dato.message);
            validarRedirect(dato);

            if (!(dato.result == 1)) {
                alert(dato.message);
                $("#mvTipoGasto").dialog("close");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../TipoGasto/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadData();           
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}