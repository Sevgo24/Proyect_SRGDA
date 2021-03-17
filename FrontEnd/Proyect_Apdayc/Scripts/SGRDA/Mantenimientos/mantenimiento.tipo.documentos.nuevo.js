var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    loadEstados("ddlEstado");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Documentos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtDescripcion").focus();
        $("#lblEstado").hide();
        $("#ddlEstado").hide();
    } else {
        $("#divTituloPerfil").html("Tipos de Documentos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "Index";
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../DOCUMENTOSTIPO/Nuevo";
    });
});

function grabar() {
    var estado = $("#ddlEstado").val();
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
        }
        else
            id = $("#txtCodigo").val();
        var item = {
            DOC_TYPE: id,
            DOC_DESC: $("#txtDescripcion").val(),
            DOC_OBSERV: $("#txtObservacion").val(),
            ESTADO: estado
        };
        $.ajax({
            url: "../DOCUMENTOSTIPO/Insertar",
            data: item,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../DOCUMENTOSTIPO/";
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../DOCUMENTOSTIPO/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtDescripcion").val(en.DOC_DESC);
                    $("#txtObservacion").val(en.DOC_OBSERV);
                    if (tipo.ENDS == null) {
                        loadEstados("ddlEstado", 1);
                    }
                    else {
                        loadEstados("ddlEstado", 2);
                    }
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