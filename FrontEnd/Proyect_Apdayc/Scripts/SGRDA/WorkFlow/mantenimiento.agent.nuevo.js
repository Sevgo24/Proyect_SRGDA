/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    $("#txtNombre").focus();
    $('#txtCodInterno').on("keypress", function (e) { return solonumeros(e); });
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Agente / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidId").val(0);
        $("#trId").hide();
    } else {
        $('#divTituloPerfil').html("Workflow - Agente / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        $("#hidId").val(id);
        obtenerDatos(id);
    }
    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../Agent/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Agent/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

});

//****************************  FUNCIONES ****************************
function grabar() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos)
        insertar();
};

function insertar() {   
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion)
        id = $("#hidId").val();

    var agente = {
        WRKF_AGID: id,
        WRKF_AGNAME: $("#txtNombre").val(),
        WRKF_AGLABEL: $("#txtEtiqueta").val()
    };
   
    $.ajax({
        url: '../Agent/Insertar',
        data: agente,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Agent/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    return false;
}

function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../Agent/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_AGID);
                $("#txtNombre").val(obj.WRKF_AGNAME);
                $("#txtEtiqueta").val(obj.WRKF_AGLABEL);
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
