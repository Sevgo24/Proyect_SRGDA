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
        $("#divTituloPerfil").html("Calificadores - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoCalificador('ddlTipoCalificador');
    } else {
        $("#divTituloPerfil").html("Calificadores - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#ddlTipoCalificador").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#ddlTipoCalificador").removeAttr('disabled');
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {

        location.href = "../Calificadores/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTipoCalificador").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
        $('#aviso').html('');
        $("#txtDescripcion").css('border', '1px solid gray');
        $("#ddlTipoCalificador").css('border', '1px solid gray');
    }).button();

    $("#btnGrabar").on("click", function () {
        var estado = ValidarRequeridos();
        if (estado)
            grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../Calificadores/Index";
    }).button();

});

var grabar = function () {
    var Calificador = {
        QUC_ID: $("#txtid").val(),
        QUA_ID: $("#ddlTipoCalificador").val(),
        DESCRIPTION: $("#txtDescripcion").val()
    };

    $.ajax({
        url: "../Calificadores/Insertar",
        data: Calificador,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                location.href = "../Calificadores/Index";
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
    $.ajax({
        url: "../Calificadores/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.QUC_ID);
                    loadTipoCalificador('ddlTipoCalificador', tipo.QUA_ID);
                    $("#txtDescripcion").val(tipo.DESCRIPTION);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}