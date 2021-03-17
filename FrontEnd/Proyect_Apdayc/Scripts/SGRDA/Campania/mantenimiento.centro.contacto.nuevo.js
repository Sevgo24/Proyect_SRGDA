var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Centro de Contactos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtCodigo").hide();
        $("#lbIdCentroContacto").hide();
        loadComboOficina("ddlOficinaComercial", 0);
    } else {
        $("#divTituloPerfil").html("Centro de Contactos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }

    $("#txtNombre").focus();

    $("#btnGrabar").on("click", function () {
        grabarCentroContacto();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../CentroContacto/";
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../CentroContacto/Nuevo";
        limpiar();
    }).button();
});

function limpiar() {
    $("#txtCodigo").val(''),
    $("#txtNombre").val('');
    $("#ddlOficinaComercial").val(0);
    $("#txtDescripcion").val('');
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../CentroContacto/Obtiene',
        data: { Id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtCodigo").val(en.CONC_ID);
                    $("#txtNombre").val(en.CONC_NAME);
                    $("#txtDescripcion").val(en.CONC_DESC);
                    loadComboOficina("ddlOficinaComercial", en.OFF_ID);
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

function grabarCentroContacto() {
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
            CONC_ID: id,
            CONC_NAME: $("#txtNombre").val(),
            CONC_DESC: $("#txtDescripcion").val(),
            OFF_ID: $("#ddlOficinaComercial").val()
        };
        $.ajax({
            url: '../CentroContacto/Insertar',
            data: usorepertorio,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    document.location.href = '../CentroContacto/';
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
        CONC_NAME: $("#txtNombre").val(),
        OFF_ID: $("#ddlOficinaComercial").val()
    };

    $.ajax({
        url: '../CentroContacto/ObtenerXDescripcion',
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
    var sociedad = $("#txtNombre").val();
    if (sociedad != '') {
        var estadoDuplicado = validarDuplicadoDescripcion();
        if (!estadoDuplicado) {
            return true;
        } else {
            alert("El centro de contacto ingresado ya existe para la oficina seleccionada, Ingrese uno nuevo.");
            return false;
        }
    }
}