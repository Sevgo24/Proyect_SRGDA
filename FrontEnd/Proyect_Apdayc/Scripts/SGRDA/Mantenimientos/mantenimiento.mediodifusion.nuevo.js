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
        $("#divTituloPerfil").html("Medios de Difusión - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Medios de Difusión - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtDescripcion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        //$("#txtDescripcion").val("");
        //$("#txtDescripcion").removeAttr('disabled');
        //$("#txtDescripcion").focus();
        //$("#btnGrabar").show();
        //$("#btnEditar").hide();
        //$("#btnNuevo").hide();
        //$("#btnVolver").show();
        location.href = "../MedioDifusion/Nuevo";
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
        location.href = "../MedioDifusion/Index";
    }).button();
});

var grabar = function () {

    var desc = $("#txtDescripcion").val();

    if (desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var MedioDifusion = {
            BROAD_ID: $("#txtid").val(),
            BROAD_DESC: $("#txtDescripcion").val()
        };

        $.ajax({
            url: "../MedioDifusion/Insertar",
            data: MedioDifusion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../MedioDifusion/Index";
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
        url: "../MedioDifusion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {

                    $("#txtid").val(tipo.BROAD_ID);
                    $("#txtDescripcion").val(tipo.BROAD_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}