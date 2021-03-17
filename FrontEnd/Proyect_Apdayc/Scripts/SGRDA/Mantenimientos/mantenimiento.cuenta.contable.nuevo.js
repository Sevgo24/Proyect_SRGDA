/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    kendo.culture('es-PE');
    $('#txtFechaVigencia').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtCuenta').on("keypress", function (e) { return solonumeros(e); });
    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Cuentas Contables - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#btnDescartar").hide();
        $("#btnNuevo").hide();
        $("#btnEditar").hide();

    } else {
        $("#divTituloPerfil").html("Cuentas Contables - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        ControlesDescartar();
        ObtenerDatos(id);
    }
    //----------------------Controles----------------------------------
    $("#btnEditar").on("click", function () { ControlesEditar(); }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../CuentaContable/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () { ControlesDescartar(); }).button();

    $("#btnGrabar").on("click", function () { grabar(); }).button();

    $("#btnVolver").on("click", function () { location.href = "../CuentaContable/Index"; }).button();
    //---------------------------------------------------------------
});

var grabar = function () {

    var des = $("#txtDescripcion").val();
    var cuenta = $("#txtCuenta").val();
    var fec = $("#txtFechaVigencia").val();

    if (des == "" || cuenta == "" || fec == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var CuentaContable = {
            LED_ID: $("#txtid").val(),
            LED_DESC: $("#txtDescripcion").val(),
            LED_NRO: $("#txtCuenta").val(),
            START: $("#txtFechaVigencia").val()
        };

        $.ajax({
            url: "../CuentaContable/Insertar",
            data: CuentaContable,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../CuentaContable/Index";
                    alert(dato.message);
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
        url: "../CuentaContable/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.LED_ID);
                    $("#txtDescripcion").val(tipo.LED_DESC);
                    $("#txtCuenta").val(tipo.LED_NRO);
                    var d1 = $("#txtFechaVigencia").data("kendoDatePicker");
                    var valFecha = formatJSONDate(tipo.START);
                    d1.value(valFecha);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


var ControlesEditar = function () {
    $("#txtDescripcion").removeAttr('disabled');
    $("#txtCuenta").removeAttr('disabled');
    $('#txtFechaVigencia').data('kendoDatePicker').enable(true);
    $("#btnEditar").hide();
    $("#btnVolver").show();
    $("#btnGrabar").show();
    $("#btnNuevo").hide();
    $("#btnDescartar").show();
}

var ControlesDescartar = function () {
    $("#txtDescripcion").attr("disabled", "disabled");
    $("#txtCuenta").attr("disabled", "disabled");
    $('#txtFechaVigencia').data('kendoDatePicker').enable(false);
    $("#btnNuevo").show();
    $("#btnEditar").show();
    $("#btnVolver").show();
    $("#btnGrabar").hide();
    $("#btnDescartar").hide();
}