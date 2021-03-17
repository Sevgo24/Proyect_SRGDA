/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Método de Pago - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Método de Pago - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoMet").val(id);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        grabarMetodo();
    }).button();

    $("#btnDescartar").on("click", function () {
        location.href = "../MetodoPago/";
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
});

function limpiar() {
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtCodigo").focus();
}


//---------------------------Editar Datos-------------------------------------
function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../MetodoPago/Obtiene',
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtCodigo").val(en.REC_PWID);
                    $("#txtDescripcion").val(en.REC_PWDESC);
                    $("#chk").prop('checked', en.REC_AUT);
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
//-----------------------------------------------------------------------------



//---------------------------Grabar Datos-------------------------------------

function grabarMetodo() {
    var estadoRequeridos = ValidarRequeridos();
    //var estadoCodigoLength = validarCodigolength();
    var estadoCodigo = validarCodigo();
    var estadoDescripcion = validarDescripcion();

    if (estadoRequeridos && estadoCodigo && estadoDescripcion) {
        grabar();
    }
};

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigoMet").val();
    }
    else
        id = $("#txtCodigo").val();

    var en = {
        valgraba: val,
        REC_PWID: id,
        REC_PWDESC: $("#txtDescripcion").val(),
        REC_AUT: $("#chk").is(':checked')
    };
    $.ajax({
        url: '../MetodoPago/Insertar',
        data: en,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../MetodoPago/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};
//-----------------------Fin Grabar Datos-------------------------------------



//****************************  FUNCIONES ****************************
//function validarCodigolength() {
//    var length = $("#txtCodigo").val().length;
//    if (length === 2) {
//        $("#txtCodigo").css('border', '1px solid gray');
//        msgErrorDiv("divResultValidacionCod", "");
//        return true;
//    } else {
//        $("#txtCodigo").css('border', '1px solid red');
//        msgErrorDiv("divResultValidacionCod", "Longitud del código debe contener 2 dígitos.");
//        return false;
//    }
//}

function validarCodigo() {
    var codigo = $("#txtCodigo").val();
    if (codigo != '') {
        if (K_ACCION_ACTUAL == K_ACCION.Nuevo) {
            var estadoDuplicadoCodigo = validarDuplicadoCodigo();
            if (!estadoDuplicadoCodigo) {
                $("#txtCodigo").css('border', '1px solid gray');
                msgErrorDiv("divResultValidacionCod", "");
                return true;
            } else {
                $("#txtCodigo").css('border', '1px solid red');
                msgErrorDiv("divResultValidacionCod", "El código ya existe, ingrese uno nuevo.");
                return false;
            }
        } else {
            return true
        }
    }
}

function validarDescripcion() {
    var sociedad = $("#txtDescripcion").val();
    if (sociedad != '') {
        var estadoDuplicado = validarDuplicadoDescripcion();
        if (!estadoDuplicado) {
            $("#txtDescripcion").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtDescripcion").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", "Este método de pago ya existe, ingrese uno nuevo.");
            return false;
        }
    }
}

function validarDuplicadoDescripcion() {
    var estado = false;
    var val = $("#hidOpcionEdit").val();

    var en = {
        REC_PWID: $("#txtCodigo").val(),
        REC_PWDESC: $("#txtDescripcion").val(),
        valgraba: val
    };

    $.ajax({
        url: '../MetodoPago/ObtenerXDescripcion',
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
                //alert(dato.message);
            }
        }
    });
    return estado;
}

function validarDuplicadoCodigo() {
    var estado = false;
    var val = $("#hidOpcionEdit").val();

    var en = {
        REC_PWID: $("#txtCodigo").val(),
        valgraba: val
    };

    $.ajax({
        url: '../MetodoPago/ObtenerXCodigo',
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
                //alert(dato.message);
            }
        }
    });
    return estado;
}