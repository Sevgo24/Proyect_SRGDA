/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/************************** INICIO CARGA********************************************/
$(function () {
    loadComboNivelAgente('ddlNivelDependencia', 0);

    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        $('#divTituloPerfil').html("Árbol de Agentes - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#hidAgeId").val(0);
    } else {
        $('#divTituloPerfil').html("Árbol de Agentes - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidAgeId").val(id);
        obtenerDatos(id);
    }

    //-------------------------- EVENTO BOTONES ------------------------------------   
    $("#btnLimpiarOfi").on("click", function () {
        Limpiar();
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../agente/Nuevo';
    }).button();

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();        
        if (estadoRequeridos)
        {
            if (K_ACCION_ACTUAL == K_ACCION.Nuevo) {
                var estadoDuplicado = validarDuplicado();
                if (!estadoDuplicado) {
                    grabarAgente();
                } else {

                    Confirmar('El nivel de agente ya existe, ¿Desea registrar?',
                            function () {
                                grabarAgente();
                            },
                            function () {
                            },
                            'Confirmar'
                        );

                }
            } else {
                grabarAgente();
            }
           
        }
    }).button();

    $("#btnDescartar").on("click", function () {
        document.location.href = '../agente/';
    }).button();
    //-------------------------- EVENTO CONTROLES ------------------------------------   
    $("#chkSinDep").change(function () {
        if ($('#chkSinDep').is(':checked')) {
            $("#ddlNivelDependencia").prop('disabled', true);
            $("#ddlNivelDependencia").removeClass('requeridoLst');
            $("#ddlNivelDependencia").css('border', '1px solid gray');

        } else {
            $("#ddlNivelDependencia").prop('disabled', false);
            $("#ddlNivelDependencia").addClass('requeridoLst');            
        }
    });       
});

//****************************  FUNCIONES ****************************
function grabarAgente() {
    var resultado = insertar();
    if (resultado)
        document.location.href = '../Agente/';
};

function insertar() {
    var id = 0;
    var pedendencia = 0;

    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidAgeId").val();

    if ($('#chkSinDep').is(':checked')) {
        pedendencia = 0;
    } else {
        pedendencia = $("#ddlNivelDependencia").val();
    }

    var agente = {
        DESCRIPTION: $("#txtNivel").val(),
        LEVEL_ID: id,
        LEVEL_DEP: pedendencia
    };

    $.ajax({
        url: '../Agente/InsertarAgenteNivel',
        data: agente,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                idDep = 0;
                alert(dato.message);
                document.location.href = '../Agente/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(idLevel)
{
    $.ajax ({
        url: "../Agente/Obtener",
        type: "GET",
        data: { id: idLevel },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var agente = dato.data.Data;
                $("#txtNivel").val(agente.DESCRIPTION);                
                if (agente.LEVEL_DEP == 0)
                {
                    $('#chkSinDep').prop('checked', true);
                    $('#ddlNivelDependencia').prop('disabled', true);
                    $("#ddlNivelDependencia").removeClass('requeridoLst');
                } else {
                    $('#ddlNivelDependencia').prop('disabled', false);
                    loadComboNivelAgente('ddlNivelDependencia', agente.LEVEL_DEP);
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

function validarDuplicado(descripcion)
{
    var estado=false;
    var agente = { DESCRIPTION: $("#txtNivel").val()   };
    
    $.ajax({
        url: '../Agente/ObtenerXDescripcion',
        type: 'POST',
        dataType:'JSON',
        data: agente,
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                if (dato.valor == '1')
                    estado= true;
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return estado;
}

function Limpiar() {
    $("#txtNivel").val('');
    $("#ddlNivelDependencia").val(0);

}
function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            OK: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Cancel: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}
