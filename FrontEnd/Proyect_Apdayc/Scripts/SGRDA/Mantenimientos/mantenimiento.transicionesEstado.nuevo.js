var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var idEstOri = GetQueryStringParams("idEstOri");
    var idEstDes = GetQueryStringParams("idEstDes");

    //---------------------------------------------------------------
    if (idEstOri === undefined && idEstDes === undefined) {
        $("#divTituloPerfil").html("Transición Estado / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadEstadosLicencia("ddlEstadoOri");
        loadEstadosLicencia("ddlEstadoDes");
    } else {
        $("#divTituloPerfil").html("Transición Estado / Actualizacion");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoOri").val(idEstOri);
        $("#hidCodigoDes").val(idEstDes);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos($("#hidCodigoOri").val(), $("#hidCodigoDes").val());
    }
    //---------------------------------------------------------------

    $("#btnDescartar").on("click", function () {
        location.href = "Index";
    });

    $("#btnGrabar").on("click", function () {
        var validacion = valida($("#ddlEstadoOri").val(), $("#ddlEstadoDes").val());
        if (validacion)
            grabar();
    });
});

function ObtenerDatos(id1, id2) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../TransicionesEstado/Obtiene',
        data: { idori: id1, iddes: id2 },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;
                if (item != null) {
                    loadEstadosLicencia("ddlEstadoOri", item.LICS_ID);
                    loadEstadosLicencia("ddlEstadoDes", item.LICS_IDT);
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

function valida(id1, id2) {
    var estado = false;
    $.ajax({
        url: '../TransicionesEstado/Validacion',
        data: { idori: id1, iddes: id2 },
        async: false,
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            }else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

function grabar() {
    if (ValidarRequeridos()) {
        var idori = 0;
        var iddes = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) {
                idori = $("#hidCodigoOri").val();
                iddes = $("#hidCodigoDes").val();
            }
        }
        else {
            idori = 0;
            iddes = 0;
        }

        var item = {
            auxLICS_ID: idori,
            auxLICS_IDT: iddes,
            LICS_ID: $("#ddlEstadoOri").val(),
            LICS_IDT: $("#ddlEstadoDes").val()
        };
        $.ajax({
            url: 'Insertar',
            data: item,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../TransicionesEstado/";
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}