var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    $("#txtDescripcion").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipo comisión / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblcodigo").hide();
        $("#txtCodigo").hide();
    } else {
        $("#divTituloPerfil").html("Tipo comisión / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos(id);
    }

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoComision";
    }).button();

    $("#btnNuevo").on("click", function () {
        $("#txtCodigo").removeAttr("disabled");
        limpiar();
    }).button();

    $("#txtCodigo").keypress(function (e) {
        if (e.which == 13) {
            $("#txtDescripcion").focus();
        }
    });

    $("#btnGrabar").on("click", function () {
        var validacion = valida($("#txtDescripcion").val());
        if (validacion)
            grabar();
    }).button();
});

function limpiar() {
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtCodigo").focus();
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../TipoComision/Obtiene',
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;

                if (item != null) {
                    $("#txtCodigo").val(item.COMT_ID);
                    $("#txtDescripcion").val(item.COM_DESC);
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

function valida(desc) {
    var estado = false;
    $.ajax({
        url: '../TipoComision/Validacion',
        data: { descripcion: desc },
        async: false,
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == null) {
                estado = true;
            } else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}


function grabar() {
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
        }
        else
            id = $("#txtCodigo").val();
        var item = {
            COMT_ID: id,
            COM_DESC: $("#txtDescripcion").val()
        };
        $.ajax({
            url: 'Insertar',
            data: item,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response; Validacion
                if (dato.result == 1) {
                    location.href = "../TipoComision/";
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}