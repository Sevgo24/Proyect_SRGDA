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
        $("#divTituloPerfil").html("Workflow - Tipos de Datos / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblId").hide();
        $("#txtId").hide();

    } else {
        $("#divTituloPerfil").html("Workflow - Tipos de Datos / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtId").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        $("#txtDescripcion").val("");
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
        $("#btnVolver").show();
    });

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../TipoDato/Index";
    });

});

var grabar = function () {
    var desc = $("#txtDescripcion").val();

    if (desc == "") {
        alert("Ingrese los datos para el registro.");
    }

    else {
        var TipoDato = {
            WRKF_DTID: $("#txtId").val(),
            WRKF_DTNAME: desc
        };
        $.ajax({
            url: "../TipoDato/Insertar",
            data: TipoDato,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoDato/Index";
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
        url: "../TipoDato/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtId").val(tipo.WRKF_DTID);
                    $("#txtDescripcion").val(tipo.WRKF_DTNAME);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}