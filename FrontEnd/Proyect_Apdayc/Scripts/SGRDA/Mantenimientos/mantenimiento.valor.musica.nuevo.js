var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtVum").focus();

    var eventoKP = "keypress";
    $('#txtVum').on(eventoKP, function (e) { return solonumeros(e); });

    var id = GetQueryStringParams("id");
    $("#txtVum").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Valor unitario de la música - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Valor unitario de la música - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1); 
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnDescartar").on("click", function () {
        location.href = "Index";
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnNuevo").on("click", function () {
        $("#txtVum").val("");
    });
});

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../ValorMusica/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;
                if (item != null) {
                    $("#txtVum").val(item.VUM_VAL);
                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(item.START);
                    d1.value(valFecha);
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
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
        }
        else
            id = $("#txtCodigo").val();  
        var item = {
            VUM_ID: id,            
            START: $("#txtFecha").val(),
            VUM_VAL: $("#txtVum").val()
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
                    location.href = "../ValorMusica/";
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}