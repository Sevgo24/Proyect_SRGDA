/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertarTipoderecho();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    });

    $("#btnVolver").on("click", function () {
        location.href = '../TipoDerecho/'
    });

});

function limpiar() {
    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
}

function limpiarValidacion() {
    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function validarInsertarTipoderecho() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Nuevo) id = $("#txtTipo").val();
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
        url: "../TipoDerecho/Insertar",
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