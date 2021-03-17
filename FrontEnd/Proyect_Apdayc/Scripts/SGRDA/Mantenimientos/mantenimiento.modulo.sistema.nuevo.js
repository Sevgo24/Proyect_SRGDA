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
        $("#divTituloPerfil").html("Módulos de Sistemas - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblid").hide();
        $("#txtid").hide();

    } else {
        $("#divTituloPerfil").html("Módulos de Sistemas - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtid").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtEtiqueta").attr("disabled", "disabled");
        $("#txtApi").attr("disabled", "disabled");
        $("#txtSentencia").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtEtiqueta").removeAttr('disabled');
        $("#txtApi").removeAttr('disabled');
        $("#txtSentencia").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        $("#txtDescripcion").val("");
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtEtiqueta").val("");
        $("#txtEtiqueta").removeAttr('disabled');
        $("#txtApi").val("");
        $("#txtApi").removeAttr('disabled');
        $("#txtSentencia").val("");
        $("#txtSentencia").removeAttr('disabled');
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
        $("#btnVolver").show();
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtEtiqueta").attr("disabled", "disabled");
        $("#txtApi").attr("disabled", "disabled");
        $("#txtSentencia").attr("disabled", "disabled");
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
        location.href = "../ModuloSistema/Index";
    }).button();
});

var grabar = function () {
    if (ValidarRequeridos()) {
        var FormatoFactura = {
            PROC_MOD: $("#txtid").val(),
            MOD_DESC: $("#txtDescripcion").val(),
            MOD_CLABEL: $("#txtEtiqueta").val(),
            MOD_CAPIKEY: $("#txtApi").val(),
            MOD_CSECRETKEY: $("#txtSentencia").val()
        };

        $.ajax({
            url: "../ModuloSistema/Insertar",
            data: FormatoFactura,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../ModuloSistema/Index";
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
        url: "../ModuloSistema/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.PROC_MOD);
                    $("#txtDescripcion").val(tipo.MOD_DESC);
                    $("#txtEtiqueta").val(tipo.MOD_CLABEL);
                    $("#txtApi").val(tipo.MOD_CAPIKEY);
                    $("#txtSentencia").val(tipo.MOD_CSECRETKEY);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}