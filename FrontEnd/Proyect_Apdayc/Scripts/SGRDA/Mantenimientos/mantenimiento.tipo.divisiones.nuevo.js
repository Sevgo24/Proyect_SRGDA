var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Divisiones - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#hidaccion").val(1);
        $("#txtCodigo").focus();
    } else {
        $("#divTituloPerfil").html("Tipos de Divisiones - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }

    loadComboTerritorio(0);

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "Index";
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TIPOSDIVISIONES/Nuevo";
    });
});

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
            accion: $("#hidaccion").val(),
            DAD_TYPE: id,
            DAD_TNAME: $("#txtDivision").val(),
            TIS_N: $("#ddlTerritorio").val(),
            DIVT_OBSERV: $("#txtObservacion").val()
        };
        $.ajax({
            url: "../TIPOSDIVISIONES/Insertar",
            data: item,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../TIPOSDIVISIONES/";
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
        url: '../TIPOSDIVISIONES/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtCodigo").val(en.DAD_TYPE);
                    $("#txtDivision").val(en.DAD_TNAME);
                    loadComboTerritorio(en.TIS_N);
                    $("#txtObservacion").val(en.DIVT_OBSERV);
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