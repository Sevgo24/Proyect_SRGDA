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
        $("#divTituloPerfil").html("Bloqueos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Bloqueos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#chkBloqueo").attr("disabled", "disabled");
        $("#chkAutorizacion").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
         $("#txtDescripcion").focus();
        $("#chkBloqueo").removeAttr('disabled');
        $("#chkAutorizacion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../Bloqueo/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#chkBloqueo").attr("disabled", "disabled");
        $("#chkAutorizacion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../Bloqueo/Index";
    }).button();

});

var grabar = function () {
    var desc = $("#txtDescripcion").val();
    var estadoBloqueo = "N";
    var estadoAutorizado = "N";

    if ($("#chkBloqueo").is(':checked')) 
        estadoBloqueo = "S";
    
    if ($("#chkAutorizacion").is(':checked'))
        estadoAutorizado = "S";

    if (desc == "") {
        alert("Ingrese los datos para el registro.");
    }

    else {
        var TipoObservacion = {
            BLOCK_ID: $("#txtid").val(),
            BLOCK_DESC: $("#txtDescripcion").val(),
            BLOCK_PULL: estadoBloqueo,
            BLOCK_AUT: estadoAutorizado
        };
        $.ajax({
            url: "../Bloqueo/Insertar",
            data: TipoObservacion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../Bloqueo/Index";                    
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
        url: "../Bloqueo/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.BLOCK_ID);
                    $("#txtDescripcion").val(tipo.BLOCK_DESC);
                    if (tipo.BLOCK_PULL=='S')
                        $("#chkBloqueo").prop('checked', true);
                    else
                        $("#chkBloqueo").prop('checked', false);

                    if (tipo.BLOCK_AUT == 'S')
                        $("#chkAutorizacion").prop('checked', true);
                    else
                        $("#chkAutorizacion").prop('checked', false);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}