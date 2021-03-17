/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_TAB = 300;
var K_HEIGHT_TAB = 200;

/*INICIO CONSTANTES*/

$(function () {

    $("#tabs").tabs();
    $("#txtDescripcion").focus();

    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Estado de Licencias - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        loadEstadosLicencia("ddlEstLicencia", '0');
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Estado de Licencias - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------

    $("#mvTab").dialog({
        autoOpen: false,
        width: K_WIDTH_TAB,
        height: K_HEIGHT_TAB,
        buttons: {
            "Agregar": addTab,
            "Cancel": function () { $("#mvTab").dialog("close"); $('#ddlTab').css({ 'border': '1px solid gray' }); }
        }, modal: true
    });

    $(".addTab").on("click", function () { $("#mvTab").dialog({}); $("#mvTab").dialog("open"); limpiarTab(); });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../EstadoLicencia/Index";
    });
    loadDataTab();
    loadLicenciaTab("ddlEstLicencia", '0');
});

function limpiarTab() {
    $("#ddlTab").val(0);
}

function loadDataTab() {
    loadDataGridTmp('ListarTab', "#gridTab");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function addTab() {
    if ($("#ddlTab").val() == 0) {
        $('#ddlTab').css({ 'border': '1px solid red' });
    }
    else {
        var IdAdd = 0;
        if ($("#hidAccionMvTab").val() === "1") IdAdd = $("#hidIdSequencia").val();
        var entidad = {
            sequencia: IdAdd,
            IdTab: $("#ddlTab").val(),
            antIdTab: $("#hidTabanterior").val(),
            IdLicencia: $("#ddlEstLicencia").val(),
            Nombre: $("#ddlTab option:selected").text()                             
        };    
        $.ajax({
            url: 'AddTab',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataTab();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvTab").dialog("close");
        $('#ddlTab').css({ 'border': '1px solid gray' });
    }
}

var grabar = function () {
    var desc = $("#txtDescripcion").val();
    var estado = $("#ddlEstLicencia").val();
    var inicial = "0";
    var final = "0";

    if (estado == 1) {inicial = "1"; final = "0";}
    if (estado == 2) {inicial = "0"; final = "0";}
    if (estado == 3) {inicial = "0"; final = "1";}

    if (desc == "" || estado == 0) {
        alert("Ingrese los datos para el registro.");
    }

    else {
        var EstadoLicencia = {
            LICS_ID: $("#txtid").val(),
            LICS_NAME: $("#txtDescripcion").val(),
            LICS_INI: inicial,
            LICS_END: final
        };
        $.ajax({
            url: "../EstadoLicencia/Insertar",
            data: EstadoLicencia,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../EstadoLicencia/Index";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../EstadoLicencia/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {

                    $("#txtid").val(tipo.LICS_ID);
                    $("#txtDescripcion").val(tipo.LICS_NAME);

                    if (tipo.LICS_INI == '1' && tipo.LICS_END == '0') { loadEstadosLicencia("ddlEstLicencia", '1'); }
                    if (tipo.LICS_INI == '0' && tipo.LICS_END == '0') { loadEstadosLicencia("ddlEstLicencia", '2'); }
                    if (tipo.LICS_INI == '0' && tipo.LICS_END == '1') { loadEstadosLicencia("ddlEstLicencia", '3'); }
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function delAddTab(idDel) {
    $.ajax({
        url: 'DellAddTab',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataTab();
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

function updAddTab(idUpd) {
    limpiarTab();

    $.ajax({
        url: 'ObtieneLicenciaEstadoTabTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvTab").val("1");
                    $("#hidIdSequencia").val(param.sequencia);
                    $("#hidTabanterior").val(param.IdTab);
                    $("#hidEdicionTab").val(param.Id);
                    loadLicenciaTab("ddlTab", param.IdTab);
                    $("#mvTab").dialog("open");
                } else {
                    alert("No se pudo obtener los datos para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
            
        }
    });
}