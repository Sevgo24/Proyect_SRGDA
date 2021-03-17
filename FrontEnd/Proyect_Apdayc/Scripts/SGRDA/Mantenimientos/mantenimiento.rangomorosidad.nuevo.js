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
        $("#divTituloLic").html("Rangos de Morosidades - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloLic").html("Rangos de Morosidades - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtRangoIni").attr("disabled", "disabled");
        $("#txtRangoFin").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#txtRangoIni").removeAttr('disabled');
        $("#txtRangoFin").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        //$("#txtDescripcion").val("");
        //$("#txtRangoIni").val("");
        //$("#txtRangoFin").val("");
        //$("#txtDescripcion").removeAttr('disabled');
        //$("#txtDescripcion").focus();
        //$("#txtRangoIni").removeAttr('disabled');
        //$("#txtRangoFin").removeAttr('disabled');
        //$("#btnGrabar").show();
        //$("#btnEditar").hide();
        //$("#btnNuevo").hide();
        //$("#btnVolver").show();
        location.href = "../RangoMorosidad/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtRangoIni").attr("disabled", "disabled");
        $("#txtRangoFin").attr("disabled", "disabled");
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
        location.href = "../RangoMorosidad/Index";
    }).button();
});

var grabar = function () {

    var desc = $("#txtDescripcion").val();
    var inicio = $("#txtRangoIni").val();
    var fin = $("#txtRangoFin").val();

    if (desc == "" || inicio == "" || fin == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var RangoMorosidad = {
            RANGE_COD: $("#txtid").val(),
            DESCRIPTION: $("#txtDescripcion").val(),
            RANGE_FROM: $("#txtRangoIni").val(),
            RANGE_TO: $("#txtRangoFin").val()
        };

        $.ajax({
            url: "../RangoMorosidad/Insertar",
            data: RangoMorosidad,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../RangoMorosidad/Index";
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
        url: "../RangoMorosidad/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {

                    $("#txtid").val(tipo.RANGE_COD);
                    $("#txtDescripcion").val(tipo.DESCRIPTION);
                    $("#txtRangoIni").val(tipo.RANGE_FROM);
                    $("#txtRangoFin").val(tipo.RANGE_TO);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}