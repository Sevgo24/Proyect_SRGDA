/************************** INICIO CONSTANTES****************************************/
var K_MENSAJE = {
    LongitudCodigo: "La longitud del código debe contener 3 digitos.",
    DuplicadoCodigo: "El código ya existe, ingrese uno nuevo.",
    DuplicadoDescripcion: "La Sociedad ya existe, ingrese uno nuevo."
};
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    $("#txtCodigo").focus();
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Tipos de Sociedades - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#hidId").val(0);
    } else {
        $('#divTituloPerfil').html("Tipos de Sociedades - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        $("#hidOpcionEdit").val(1);
        $("#hidId").val(id);
        obtenerDatos(id);
    }
    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../Sociedad/';
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Sociedad/Nuevo';
    }).button();

    $("#btnGrabar").on("click", function () {
        grabarSociedad();
    }).button();

    $("#txtCodigo").keypress(function (e) {
        if (e.which == 13) {
            $("#txtSociedad").focus();
        }
    });
    //---------------------------------------------------------------------------------
});

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
    var sociedad = $("#txtSociedad").val();
    if (sociedad != '') {
        var estadoDuplicado = validarDuplicado();
        if (!estadoDuplicado) {
            $("#txtSociedad").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtSociedad").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE.DuplicadoDescripcion);
            return false;
        }
    }
}
function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../SociedadPrueba/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.codigo);
                $("#txtSociedad").val(obj.descripcion);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
function grabarSociedad() {
    var estadoRequeridos = ValidarRequeridos();
    var estadoCodigoLength = validarCodigolength();
    var estadoDescripcion = validarDescripcion();
    if (estadoCodigoLength)
        var estadoCodigo = validarCodigo();

    if (estadoRequeridos && estadoCodigoLength && estadoDescripcion && estadoCodigo) {
        insertar();
    }
};

function insertar() {
    var id = '';
    var user = '';
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) {
        id = $("#hidId").val();
        user = $("#hidId").val();
    }
    else
        id = $("#txtCodigo").val();

    var sociedad = {
        MOG_SOC: id,
        MOG_SDESC: $("#txtSociedad").val(),
        LOG_USER_CREAT: user
    };

    $.ajax({
        url: '../Sociedad/Insertar',
        data: sociedad,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Sociedad/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

°

function validarDuplicado() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#txtCodigo").val();
    
    var sociedad = {
        MOG_SOC: id,
        MOG_SDESC: $("#txtSociedad").val()
    };

    $.ajax({
        url: '../Sociedad/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: sociedad,
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
    var sociedad = {
        MOG_SOC: $("#txtCodigo").val()
    };

    $.ajax({
        url: '../Sociedad/ObtenerXCodigo',
        type: 'POST',
        dataType: 'JSON',
        data: sociedad,
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
