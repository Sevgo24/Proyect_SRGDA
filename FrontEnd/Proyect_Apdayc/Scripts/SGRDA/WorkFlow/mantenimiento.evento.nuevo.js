/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = GetQueryStringParams("id");
    $("#txtDescripcion").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Eventos / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Eventos / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidEvento").val(id);
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        grabarEvento();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../Evento/";
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $("#txtEtiqueta").focus();
        }
    });
});

function limpiar() {
    $("#txtDescripcion").val("");
    $("#txtEtiqueta").val("");
    $("#txtDescripcion").focus();
}


//---------------------------Editar Datos-------------------------------------
function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Evento/Obtiene',
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {                    
                    $("#hidEvento").val(en.WRKF_EID);
                    $("#txtDescripcion").val(en.WRKF_ENAME);
                    $("#txtEtiqueta").val(en.WRKF_ELABEL);
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

function grabarEvento() {
    var estadoRequeridos = ValidarRequeridos();
    var estadoDescripcion = validarDuplicadoDescripcion();

    if (estadoRequeridos && estadoDescripcion) {
        grabar();
    }
};

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidEvento").val();
    }

    var en = {
        WRKF_EID: id,
        WRKF_ENAME: $("#txtDescripcion").val(),
        WRKF_ELABEL: $("#txtEtiqueta").val()
    };
    $.ajax({
        url: '../Evento/Insertar',
        data: en,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Evento/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};
//-----------------------Fin Grabar Datos-------------------------------------



//****************************  FUNCIONES ****************************

function validarDuplicadoDescripcion() {
    var estado = false;
    $.ajax({
        url: '../Evento/ValidarDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: { Descripcion: $("#txtDescripcion").val() },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}
