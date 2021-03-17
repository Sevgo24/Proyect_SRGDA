var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipo de Campaña - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtCodigo").hide();
        $("#lbIdCampania").hide();
        loadTipoObservacion("ddlTipoObservacion", 0);
    } else {
        $("#divTituloPerfil").html("Tipo de Campaña - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);        
        ObtenerDatos(id);
    }

    $("#txtDescripcion").focus();

    $("#btnGrabar").on("click", function () {
        grabarTipoCampania();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../TipoCampania/";
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoCampania/Nuevo";
        limpiar();
    }).button();
});

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../TipoCampania/Obtiene',
        data: { Id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    //$("#ddlTipoObservacion").val(en.OBS_TYPE);
                    $("#txtCodigo").val(en.CONC_CTID);
                    $("#txtDescripcion").val(en.CONC_CTNAME);
                    loadTipoObservacion("ddlTipoObservacion", en.OBS_TYPE);
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

function grabarTipoCampania() {
    var estadoRequeridos = ValidarRequeridos();

    if (estadoRequeridos) {
        grabar();
    }
};

function grabar() {
    var id = 0;
    var estadoDescripcion;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
    }
    else
        id = $("#txtCodigo").val();

    if (id == 0) {
        estadoDescripcion = validarDescripcion();
    }
    else
        estadoDescripcion = true;

    if (estadoDescripcion) {
        var usorepertorio = {
            valgraba: val,
            CONC_CTID: id,
            CONC_CTNAME: $("#txtDescripcion").val(),
            OBS_TYPE: $("#ddlTipoObservacion").val()
        };
        $.ajax({
            url: '../TipoCampania/Insertar',
            data: usorepertorio,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    document.location.href = '../TipoCampania/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};

function validarDuplicadoDescripcion() {
    var estado = false;
    var en = {
        CONC_CTNAME: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TipoCampania/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: en,
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

function validarDescripcion() {
    var sociedad = $("#txtDescripcion").val();
    if (sociedad != '') {
        var estadoDuplicado = validarDuplicadoDescripcion();
        if (!estadoDuplicado) {
            return true;
        } else {
            alert("El tipo de campaña ingresado ya existe, Ingrese uno nuevo.");
            return false;
        }
    }
}