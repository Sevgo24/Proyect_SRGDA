/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_MSJ_VALIDACION_ESTADOS = "Los estados de transiciones deben ser distintos.";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));

    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Transiciones de Estados / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidId").val(0);
        $("#trId").hide();
        loadEstadoWF('dllOrigen', 0, 0);
        loadEstadoWF('dllDestino', 0, 0);
        LoadEventos('ddlEvento');
        LoadCicloAprobacion('ddlCiclo');
        $('#dllOrigen').attr('disabled', true).css('border', '1px solid gray');
        $('#dllDestino').attr('disabled', true).css('border', '1px solid gray');
    } else {
        $('#divTituloPerfil').html("Workflow - Transiciones de Estados / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        $("#txtCodigo").val(id);
        $("#hidId").val(id);
        obtenerDatos(id);
    }

    $("#ddlCiclo").change(function () {
        var id = $('option:selected', this).val();

        if (id == 0) {
            $('#dllOrigen').prop('disabled', true).val(0).css('border', '1px solid gray');
            $('#dllDestino').prop('disabled', true).val(0).css('border', '1px solid gray');
        } else {

            loadEstadoWF('dllOrigen', 0, id);
            loadEstadoWF('dllDestino', 0, id);
            $('#dllOrigen').prop('disabled', false);
            $('#dllDestino').prop('disabled', false);
        }

    });
    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../Transiciones/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Transiciones/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#txtCodigo").keypress(function (e) {
        if (e.which == 13) {
            $("#txtSociedad").focus();
        }
    });
    //---------------------------------------------------------------------------------

});

//****************************  FUNCIONES ****************************
function grabar() {
    var estadoTransicion = validarEstados();
    if (!estadoTransicion)
        alert(K_MSJ_VALIDACION_ESTADOS);
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos && estadoTransicion)
        insertar();
};

function validarEstados() {
    var estado = false;
    var ini = $("#dllOrigen").val();
    var fin = $("#dllDestino").val();
    if ((ini==0 && fin==0) || ini != fin)
        estado = true;
    return estado;

}

function insertar() {
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion)
        id = $("#hidId").val();

    var flujo = {
        WRKF_TID: id,
        WRKF_ID: $("#ddlCiclo").val(),
        WRKF_CSTATE: $("#dllOrigen").val(),
        WRKF_NSTATE: $("#dllDestino").val(),
        WRKF_EID: $("#ddlEvento").val()        
    };

    $.ajax({
        url: '../Transiciones/Insertar',
        data: flujo,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Transiciones/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../Transiciones/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                loadEstadoWF('dllOrigen', obj.WRKF_CSTATE, obj.WRKF_ID);
                loadEstadoWF('dllDestino', obj.WRKF_NSTATE, obj.WRKF_ID);
                LoadEventos('ddlEvento', obj.WRKF_EID);
                LoadCicloAprobacion('ddlCiclo', obj.WRKF_ID);
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

