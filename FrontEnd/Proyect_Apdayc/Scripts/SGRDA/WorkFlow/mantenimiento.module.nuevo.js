/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    $("#txtCliente").focus();
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Módulo / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        //$("#hidOpcionEdit").val(0);
        $("#hidId").val(0);
        $("#trId").hide();
    } else {
        $('#divTituloPerfil').html("CWorkflow - Módulo / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        //$("#hidOpcionEdit").val(1);
        $("#hidId").val(id);
        obtenerDatos(id);
    }
    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../ModuloCliente/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../ModuloCliente/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabarModulo();
    });

    $("#txtCodigo").keypress(function (e) {
        if (e.which == 13) {
            $("#txtSociedad").focus();
        }
    });
    //---------------------------------------------------------------------------------
});

//****************************  FUNCIONES ****************************
function grabarModulo() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) 
        insertar();
    
};

function insertar() {
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) 
        id = $("#hidId").val();

    var cliente = {
        PROC_MOD: id,
        MOD_DESC: $("#txtCliente").val(),
        MOD_CLABEL: $("#txtEtiqueta").val(),
        MOD_CAPIKEY: $("#txtApi").val(),
        MOD_CSECRETKEY: $("#txtSentencia").val()
    };

    $.ajax({
        url: '../ModuloCliente/Insertar',
        data: cliente,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../ModuloCliente/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../ModuloCliente/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.PROC_MOD);
                $("#txtCliente").val(obj.MOD_DESC);
                $("#txtEtiqueta").val(obj.MOD_CLABEL);
                $("#txtApi").val(obj.MOD_CAPIKEY);
                $("#txtSentencia").val(obj.MOD_CSECRETKEY);
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
