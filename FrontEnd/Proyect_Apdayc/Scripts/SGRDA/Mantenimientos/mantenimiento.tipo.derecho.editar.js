/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        $("#divTituloPerfil").html("Tipos de Derechos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Tipos de Derechos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    $("#txtTipo").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");
    $("#txtMapeo").attr("disabled", "disabled");
    $("#txtDerecho").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        $("#txtDesCorta").removeAttr('disabled');
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtMapeo").removeAttr('disabled');
        $("#txtDerecho").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });


    $("#btnNuevo").on("click", function () {
        location.href = '../TipoDerecho/Nuevo'
    });
    $("#btnDescartar").on("click", function () {
        $("#txtDesCorta").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtMapeo").attr("disabled", "disabled");
        $("#txtDerecho").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    });

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertarTipoderecho();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    });

    $("#btnVolver").on("click", function () {
        location.href = "../TipoDerecho/";
    });

});

function limpiar() {
    $("#txtDesCorta").val("");
    $("#txtDescripcion").val("");
}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function validarInsertarTipoderecho() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#txtTipo").val();
    var en = {
        RIGHT_COD: id,
        RIGHT_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TipoDerecho/Validacion',
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
    var TipoDerecho = {
        RIGHT_COD: $("#txtTipo").val(),
        RIGHT_DESC: $("#txtDescripcion").val(),
        WORK_RIGHT_CODE: $("#txtMapeo").val(),
        WORK_RIGHT_DESC: $("#txtDerecho").val()
    };

    $.ajax({
        url: "../TipoDerecho/Actualizar",
        data: TipoDerecho,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                location.href = "../TipoDerecho/Index";
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    limpiar();
    $.ajax({
        url: "../TipoDerecho/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtTipo").val(tipo.RIGHT_COD);
                    $("#txtDescripcion").val(tipo.RIGHT_DESC);
                    $("#txtMapeo").val(tipo.WORK_RIGHT_CODE);
                    $("#txtDerecho").val(tipo.WORK_RIGHT_DESC);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function actualizar(idSel) {

    $.ajax({
        url: "../TipoDerecho/Actualizar",
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (!(dato.result == 1)) {
                alert(dato.message);
                location.href = "../TipoDerecho/Index";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function eliminar(idSel) {

    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../TipoDerecho/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}