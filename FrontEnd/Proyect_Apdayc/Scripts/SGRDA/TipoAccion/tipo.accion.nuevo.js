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
        $("#divTituloPerfil").html("Workflow - Tipos de Acciones / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblId").hide();
        $("#txtId").hide();

    } else {
        $("#divTituloPerfil").html("Workflow - Tipos de Acciones / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtId").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtEtiqueta").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#txtEtiqueta").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").Hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        $("#txtDescripcion").val("");
        $("#txtDescripcion").removeAttr('disabled');
        $("#txtDescripcion").focus();
        $("#txtEtiqueta").val("");
        $("#txtEtiqueta").removeAttr('disabled');
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
        $("#btnVolver").show();
    });

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#txtEtiqueta").attr("disabled", "disabled");
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
        location.href = "../TiposAcciones/Index";
    });

});

var grabar = function () {
    var desc = $("#txtDescripcion").val();
    var etiqueta = $("#txtEtiqueta").val();

    if (desc == "" || etiqueta == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var TipoAccion = {
            WRKF_ATID: $("#txtId").val(),
            WRKF_ATNAME: desc,
            WRKF_ATLABEL: etiqueta
        };
        $.ajax({
            url: "../TiposAcciones/Insertar",
            data: TipoAccion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TiposAcciones/Index";
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
        url: "../TiposAcciones/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtId").val(tipo.WRKF_ATID);
                    $("#txtDescripcion").val(tipo.WRKF_ATNAME);
                    $("#txtEtiqueta").val(tipo.WRKF_ATLABEL);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}