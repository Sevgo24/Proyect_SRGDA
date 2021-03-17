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
        $("#divTituloPerfil").html("Definiciones de Gasto - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Definiciones de Gasto - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    $("#ddlTipoGasto").attr("disabled", "disabled");
    $("#ddlGrupoGasto").attr("disabled", "disabled");
    $("#txtDesCorta").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        $("#ddlTipoGasto").removeAttr('disabled');
        $("#ddlGrupoGasto").removeAttr('disabled');
        $("#txtDescripcion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../DefinicionGasto/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTipoGasto").attr("disabled", "disabled");
        $("#ddlGrupoGasto").attr("disabled", "disabled");
        $("#txtDesCorta").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#ddlTipoGasto").on("change", function () {
        $('#ddlGrupoGasto option').remove();
        var tipo = $("#ddlTipoGasto").val();
        loadGrupoGasto('ddlGrupoGasto', tipo, 0);
    });

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertar();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "Index";
    }).button();
});

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../DefinicionGasto/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    loadTipoGasto('ddlTipoGasto', tipo.EXP_TYPE);
                    loadGrupoGasto('ddlGrupoGasto', tipo.EXP_TYPE, tipo.EXPG_ID);
                    $("#txtDesCorta").val(tipo.EXP_ID);
                    $("#txtDescripcion").val(tipo.EXP_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function validarInsertar() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#txtDesCorta").val();
    var en = {
        EXP_ID: id,
        EXP_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../DefinicionGasto/ObtenerDatosValidar',
        type: 'POST',
        dataType: 'JSON',
        data: en,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 0) {
                estado = false;
                if (dato.message != null)
                    alert(dato.message);
            } else {
                estado = true;
            }
        }
    });
    return estado;
}

var grabar = function () {
    var DefinicionGasto = {
        EXP_TYPE: $("#ddlTipoGasto").val(),
        EXPG_ID: $("#ddlGrupoGasto").val(),
        EXP_ID: $("#txtDesCorta").val(),
        EXP_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: "../DefinicionGasto/Actualizar",
        data: DefinicionGasto,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../DefinicionGasto/";                
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    //}
    return false;
};

