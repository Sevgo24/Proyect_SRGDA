/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
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
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Formatos de Facturas - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblid").hide();
        $("#txtid").hide();

    } else {
        $("#divTituloPerfil").html("Formatos de Facturas - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtid").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../FormatoFactura/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../FormatoFactura/Index";
    }).button();
});

var grabar = function () {

    var desc = $("#txtDescripcion").val();

    if (desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var FormatoFactura = {
            INVF_ID: $("#txtid").val(),
            INVF_DESC: $("#txtDescripcion").val()
        };

        $.ajax({
            url: "../FormatoFactura/Insertar",
            data: FormatoFactura,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../FormatoFactura/Index";
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
        url: "../FormatoFactura/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {

                    $("#txtid").val(tipo.INVF_ID);
                    $("#txtDescripcion").val(tipo.INVF_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}