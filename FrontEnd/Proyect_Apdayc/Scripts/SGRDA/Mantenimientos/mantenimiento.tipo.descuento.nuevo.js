/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_ID_DESCUENTO_ESPECIAL = 11;

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
        $("#divTituloPerfil").html("Tipos de Descuentos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Tipos de Descuentos - Actualización");
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
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoDescuento/Nuevo";
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
        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertarTipodescuento();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    });

    $("#btnVolver").on("click", function () {
        location.href = "../TipoDescuento/Index";
    });

});

function validarInsertarTipodescuento() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#txtid").val();
    var en = {
        DISC_TYPE: id,
        DISC_TYPE_NAME: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TipoDescuento/Validacion',
        type: 'POST',
        dataType: 'JSON',
        data: en,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 0) {
                estado = false;
                if (dato.message != null)
                    alert(dato.message);
            } else {
                estado = true;
            }
        }
    });
    return estado;
}

var grabar = function () {
    var TipoDescuento = {
        DISC_TYPE: $("#txtid").val(),
        DISC_TYPE_NAME: $("#txtDescripcion").val()
    };

    $.ajax({
        url: "../TipoDescuento/Insertar",
        data: TipoDescuento,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../TipoDescuento/Index";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
    //}
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../TipoDescuento/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.DISC_TYPE);
                    $("#txtDescripcion").val(tipo.DISC_TYPE_NAME);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}