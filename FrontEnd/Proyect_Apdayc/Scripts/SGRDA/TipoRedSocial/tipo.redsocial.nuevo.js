/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#txtPDescripcion").focus();
    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();

    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Redes Sociales / Nuevo ");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblId").hide();
        $("#txtId").hide();

    } else {
        $("#divTituloPerfil").html("Tipos de Redes Sociales / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtId").attr("disabled", "disabled");
        $("#txtPDescripcion").attr("disabled", "disabled");
        $("#txtObservacion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtPDescripcion").removeAttr('disabled');
        $("#txtObservacion").removeAttr('disabled');
        $("#txtPDescripcion").focus();
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        $("#txtPDescripcion").val("");
        $("#txtPDescripcion").removeAttr('disabled');
        $("#txtObservacion").val("");
        $("#txtObservacion").removeAttr('disabled');
        $("#txtPDescripcion").focus();
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
        $("#btnVolver").show();
    });

    $("#btnDescartar").on("click", function () {
        $("#txtPDescripcion").attr("disabled", "disabled");
        $("#txtObservacion").attr("disabled", "disabled");
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
        location.href = "../TipoRedSocial/Index";
    });

});

var grabar = function () {
    var desc = $("#txtPDescripcion").val();
    var obs = $("#txtObservacion").val();

    if (desc == "" || obs == "") {
        alert("Ingrese los datos para el registro.");
    }

    else {
        var TipoRedSocial = {
            CONT_TYPE: $("#txtId").val(),
            CONT_TDESC: desc,
            CONT_OBSERV: obs
        };
        $.ajax({
            url: "../TipoRedSocial/Insertar",
            data: TipoRedSocial,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoRedSocial/Index";
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
        url: "../TipoRedSocial/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtId").val(tipo.CONT_TYPE);
                    $("#txtPDescripcion").val(tipo.CONT_TDESC);
                    $("#txtObervacion").val(tipo.CONT_OBSERV);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}