/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = GetQueryStringParams("id");
    $("#txtDescripcion").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Origen Comisión / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Origen Comisión / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        //if ($("#hidOpcionEdit").val() == 0) {
        //    var requeridos = ValidarRequeridos();
        //    if (requeridos) {
        //        var estado = validarDuplicado();
        //        if (requeridos && estado) {
        //            grabar();
        //        }
        //    }
        //}
        //else {
        //    var requeridos = ValidarRequeridos();
        //    if (requeridos)
        //        grabar();
        //}

        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertarComisionOrigen();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../ComisionOrigen/";
    });

    $("#btnNuevo").on("click", function () {
        limpiar();
    });
});

function limpiar() {
    $("#txtDescripcion").val("");
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../ComisionOrigen/Obtiene',
        data: { Id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtDescripcion").val(en.COM_DESC);
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

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
    }
    else
        id = $("#txtCodigo").val();

    var en = {
        valgraba: val,
        COM_ORG: id,
        COM_DESC: $("#txtDescripcion").val()
    };
    $.ajax({
        url: 'Insertar',
        data: en,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../ComisionOrigen/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function validarInsertarComisionOrigen() {
    var estado = false;
    var id = '0';
    //if (K_ACCION_ACTUAL == K_ACCION.Nuevo) id = $("#hidCodigo").val();
    id = $("#hidCodigo").val();
    var en = {
        COM_ORG: id,
        COM_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../ComisionOrigen/Validacion',
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