var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Envio de Facturas / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtCodigo").hide();
        $("#Codigo").hide();
        $("#txtDescripcion").focus();
    } else {
        $("#divTituloPerfil").html("Tipos de Envio de Facturas / Actualizacion");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoTipoEnvio").val(id);
        $("#txtCodigo").attr("disabled", "disabled");

        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        grabarTipoenvioFactura();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoenvioFactura";
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoenvioFactura/Nuevo";
    });
});

function limpiar() {
    $("#txtDescripcion").val("");
    $("#txtDescripcion").focus();
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../TipoenvioFactura/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;

                if (en != null) {
                    $("#txtCodigo").val(en.LIC_SEND);
                    $("#txtDescripcion").val(en.LIC_FSEND);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function grabarTipoenvioFactura() {
    var estadoRequeridos = ValidarRequeridos();
    var estadoDescripcion = validarDescripcion();
    if (estadoRequeridos && estadoDescripcion) {
        grabar();
    }
};

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigoTipoEnvio").val();
    }
    else
        id = $("#txtCodigo").val();

    var tipo = {
        LIC_SEND: id,
        LIC_FSEND: $("#txtDescripcion").val()
    };
    $.ajax({
        url: 'Insertar',
        data: tipo,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../TipoenvioFactura/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function validarDescripcion() {
    var dato = $("#txtDescripcion").val();
    if (dato != '') {
        var estadoDuplicado = validarDuplicadoDescripcion();
        if (!estadoDuplicado) {
            $("#txtDescripcion").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtDescripcion").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", "Este tipo de envio ya existe, ingrese uno nuevo.");
            return false;
        }
    }
}

function validarDuplicadoDescripcion() {
    var estado = false;
    var item = {
        LIC_FSEND: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TipoenvioFactura/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: item,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
            }
        }
    });
    return estado;
}