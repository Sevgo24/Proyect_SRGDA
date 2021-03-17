/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_TAB = 300;
var K_HEIGHT_TAB = 150;

/************************** INICIO CARGA********************************************/
$(function () {
    $("#tabs").tabs();
    $("#hidAccionMvDet").val("0");

    var id = (GetQueryStringParams("id"));
    $("#txtNombre").focus();
    $('#txtCodInterno').on("keypress", function (e) { return solonumeros(e); });
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow- Estados de Procesos / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidId").val(0);
        $("#trId").hide();
        LoadTipoEstadoWrkf("ddlTipo");
        loadDataTab();
    } else {
        $('#divTituloPerfil').html("Workflow- Estados de Procesos / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        $("#hidId").val(id);
        obtenerDatos(id);
    }

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

    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../State/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../State/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    
    loadLicenciaTab("ddlTab", '0');
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

    var estado = {
        WRKF_SID: id,
        WRKF_SNAME: $("#txtDescripcion").val(),
        WRKF_SLABEL: $("#txtEtiqueta").val(),
        WRKF_SDESC: $("#txtDetalle").val(),
        WRKF_STID: $("#ddlTipo").val()
    };

    $.ajax({
        url: '../State/Insertar',
        data: estado,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../State/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../State/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_SID);
                $("#txtDescripcion").val(obj.WRKF_SNAME);
                $("#txtEtiqueta").val(obj.WRKF_SLABEL);
                $("#txtDetalle").val(obj.WRKF_SDESC);
                LoadTipoEstadoWrkf("ddlTipo", obj.WRKF_STID);
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
