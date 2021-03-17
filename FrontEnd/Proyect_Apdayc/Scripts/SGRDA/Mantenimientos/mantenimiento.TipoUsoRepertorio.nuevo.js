/************************** INICIO CONSTANTES****************************************/
var K_MENSAJE = {
    LongitudCodigo: "La longitud del código debe contener 3 digitos.",
    DuplicadoCodigo: "El código ya existe, ingrese uno nuevo.",
    DuplicadoDescripcion: "El tipo uso repertoio ya existe, ingrese uno nuevo."
};
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Tipos de Uso del Repertorio / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Tipos de Uso del Repertorio / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoUsorep").val(id);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        grabarUsorepertorio();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TipoUsorepertorio";
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../TipoUsorepertorio/Nuevo';
    });

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
        url: '../TipoUsorepertorio/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var usorepertorio = dato.data.Data;
                if (usorepertorio != null) {
                    $("#txtCodigo").val(usorepertorio.MOD_REPER);
                    $("#txtDescripcion").val(usorepertorio.MOD_DREPER);
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

function grabarUsorepertorio() {
    var estadoRequeridos = ValidarRequeridos();
    var estadoCodigoLength = validarCodigolength();
    var estadoDescripcion = validarDescripcion();
    if(estadoCodigoLength)
    var estadoCodigo = validarCodigo();

    if (estadoRequeridos && estadoCodigoLength && estadoDescripcion && estadoCodigo) {
        grabar();   
    }
};

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigoUsorep").val();
    }
    else
        id = $("#txtCodigo").val();

    var usorepertorio = {
        valgraba: val,
        MOD_REPER: id,
        MOD_DREPER: $("#txtDescripcion").val()
    };
    $.ajax({
        url: '../TipoUsorepertorio/Insertar',
        data: usorepertorio,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../TipoUsorepertorio/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};
//-----------------------Fin Grabar Datos-------------------------------------



//****************************  FUNCIONES ****************************
function validarCodigolength() {
    var length = $("#txtCodigo").val().length;
    if (length === 3) {
        $("#txtCodigo").css('border', '1px solid gray');
        msgErrorDiv("divResultValidacionCod", "");
        return true;
    } else {
        $("#txtCodigo").css('border', '1px solid red');
        msgErrorDiv("divResultValidacionCod", K_MENSAJE.LongitudCodigo);
        return false;
    }
}

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
                msgErrorDiv("divResultValidacionCod", K_MENSAJE.DuplicadoCodigo);
                return false;
            }
        } else {
            return true
        }
    }
}

function validarDescripcion() {
    var repertorio = $("#txtDescripcion").val();
    if (repertorio != '') {
        var estadoDuplicado = validarDuplicadoDescripcion();
        if (!estadoDuplicado) {
            $("#txtDescripcion").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtDescripcion").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE.DuplicadoDescripcion);
            return false;
        }
    }
}

function validarDuplicadoDescripcion() {
    var estado = false;
    var usorepertorio = {
        MOD_REPER: $("#txtCodigo").val(),
        MOD_DREPER: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TipoUsorepertorio/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: usorepertorio,
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

function validarDuplicadoCodigo() {
    var estado = false;
    var usorepertorio = {
        MOD_REPER: $("#txtCodigo").val()
    };

    $.ajax({
        url: '../TipoUsorepertorio/ObtenerXCodigo',
        type: 'POST',
        dataType: 'JSON',
        data: usorepertorio,
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