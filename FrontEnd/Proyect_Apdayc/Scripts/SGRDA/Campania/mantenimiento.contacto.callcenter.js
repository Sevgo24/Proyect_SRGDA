var K_WIDTH_DOC = 580;
var K_HEIGHT_DOC = 280;
var K_WIDTH_OBS = 600;
var K_HEIGHT_OBS = 290;
var K_WIDTH_VERDOC = 1150;
var K_HEIGHT_VERDOC = 300;

var K_DIV_MESSAGE = {
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento"
};

var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    $('#ddlCampania').append($("<option />", { value: 0, text: '--SELECCIONE--' }));
    $('#ddlAgenteLote').append($("<option />", { value: 0, text: '--SELECCIONE--' }));

    loadTipoObservacion('ddlTipoObservacion', 0);
    loadCentroContacto('ddlCentroContacto', 0);
    loadTipoCampania('ddlTipoCampania', 0);

    $("#ddlTipoCampania").on("change", function () {
        var id = $("#ddlTipoCampania").val();
        loadCampaniaXtipo('ddlCampania', id, 0);
    });

    $("#ddlCampania").on("change", function () {
        var id = $("#ddlCampania").val();
        loadLoteAgenteXCampania('ddlAgenteLote', id, 0);
    });

    $("#ddlAgenteLote").on("change", function () {
        var id = $("#ddlAgenteLote").val();
        CargarDatos(id)
    });

    loadTipoDoc('ddlTipoDocumento', 0);
    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_DOC, height: K_HEIGHT_DOC, buttons: { "Agregar": addDocumento, "Cancel": function () { $("#mvDocumento").dialog("close"); $('#txtFecha').css({ 'border': '1px solid gray' }); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addObservacion, "Cancel": function () { $("#mvObservacion").dialog("close"); $('#txtObservacion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $("#mvVerDocumento").dialog({ autoOpen: false, width: K_WIDTH_VERDOC, height: K_HEIGHT_VERDOC, buttons: { "Cerrar": function () { $("#mvVerDocumento").dialog("close"); } }, modal: true });
});

function Grabar(i, id) {
    var Id = 0;

    if (id != 0)
        var Id = id;

    var en = {
        CONC_MID: Id,
        CONC_SID: $("#ddlAgenteLote").val(),
        CONC_CID: $("#ddlCampania").val(),
        OBS_ID: $("#txtObsid_" + i).val(),
        BPS_ID: $("#txtBpsid_" + i).val(),
        CONC_MEXPEC: $("#txtValExpectativa_" + i).val(),
        CONC_MREAL: $("#txtValReal_" + i).val()
    };
    $.ajax({
        url: '../ContactoCallCenter/Insertar',
        data: en,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                CargarDatos($("#ddlAgenteLote").val());
                //document.location.href = '../ContactoCallCenter/';
                loadDataLoteCliente();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function addDoc(id) {
    Id = id;
    limpiarDocumento();
    $("#hidAccionMvDoc").val("0");
    $("#hidEdicionDoc").val(Id);
    $("#hidContactoLlamada").val(Id);
    $("#mvDocumento").dialog("open");
}

function EditarObservacion(idObs, id) {
    Id = id;
    var IdObs = idObs;
    $("#hidEdicionObs").val(IdObs);
    $("#hidContactoLlamada").val(Id);
    limpiarObservacion();
    $("#hidAccionMvObs").val("1");
    updAddObservacion(IdObs);
}

function loadDataLoteCliente() {
    loadDataGridTmp('ListarCampaniaLoteCliente', "#gridContactoCallCenter");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function CargarDatos(id) {
    $.ajax({
        url: '../ContactoCallCenter/ListarLoteCliente',
        type: 'POST',
        data: { idLote: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataLoteCliente();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function addDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    var IdAdd = 0;
    if ($("#hidAccionMvDoc").val() === "1") {
        IdAdd = $("#hidEdicionDoc").val();
    }
    $("#txtFecha").addClass("requerido");
    if (IdAdd > 0) {
        $("#file_upload").removeClass("requerido");
    } else {
        $("#file_upload").addClass("requerido");
    }

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, K_DIV_POPUP.DOCUMENTO)) {
        var documento = {
            Id: IdAdd,
            IdContactoLlamada: $("#hidContactoLlamada").val(),
            TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
            TipoDocumentoDesc: $("#ddlTipoDocumento option:selected").text(),
            FechaRecepcion: $("#txtFecha").val(),
            Archivo: $("#hidNombreFile").val()
        };
        $.ajax({
            url: 'AddDocumento',
            type: 'POST',
            data: documento,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    if ($("#file_upload").val() != "") {
                        InitUploadTabDocContactoCallCenter("file_upload", dato.Code);
                    }
                    loadDataDocumento($("#hidContactoLlamada").val());
                    loadDataLoteCliente();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvDocumento").dialog("close");
    }
}

function limpiarDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#hidNombreFile").val("");
    $('#file_upload').css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $('#txtFecha').css({ 'border': '1px solid gray' });
    $("#file_upload").val("");
}

function limpiarObservacion() {
    $("#txtObservacion").val("");
    //$("#hidAccionMvObs").val(0);
    //$("#hidEdicionObs").val(0);
}

function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}

function updAddDocumento(idUpd) {
    limpiarDocumento();

    $.ajax({
        url: 'ObtieneDocumentoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDoc").val("1");
                    $("#hidEdicionDoc").val(doc.Id);
                    $("#ddlTipoDocumento").val(doc.TipoDocumento);

                    $("#txtFecha").val(doc.FechaRecepcion);

                    $("#mvDocumento").dialog("open");
                } else {
                    alert("No se pudo obtener el documento para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function verDocumentos(Id) {

    $("#mvVerDocumento").dialog("open");
    loadDataDocumento(Id);
}

function loadDataDocumento(Id) {
    $("#hidContactoLlamada").val(Id);
    loadDataGridDocumentosTmp('ListarDocumento', "#gridDocumentos", Id);
}

function loadDataGridDocumentosTmp(Controller, idGrilla, Id) {
    $.ajax({
        data: { IdContactoLlamada: Id },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato);
            $(idGrilla).html(dato.message);
        }
    });
}

function delAddDocumento(idDel) {
    $.ajax({
        url: 'DellAddDocumento',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDocumento($("#hidContactoLlamada").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function addObservacion() {
    if ($("#ddlTipoObservacion").val() == 0) {
        $('#ddlTipoObservacion').css({ 'border': '1px solid red' });
        return;
    }
    else {
        $('#ddlTipoObservacion').css({ 'border': '1px solid gray' });
    }

    if ($("#txtObservacion").val() == '') {
        $('#txtObservacion').css({ 'border': '1px solid red' });
        return;
    }
    else {
        $('#txtObservacion').css({ 'border': '1px solid gray' });
    }


    //var IdAdd = 0;
    //if ($("#hidAccionMvObs").val() === "1") IdAdd = $("#hidEdicionObs").val();

    var idcontactollamada = $("#hidContactoLlamada").val();
    var id = $("#hidEdicionObs").val();
    var idTipoObservacion = $("#ddlTipoObservacion option:selected").val();
    var Observacion = $("#txtObservacion").val();
    var TipoObservacionDesc = $("#ddlTipoObservacion option:selected").text();


    $.ajax({
        url: 'AddObservacion',
        type: 'POST',
        data: { IdTipoObservacion: idTipoObservacion, Observacion: Observacion, IdContactoLlamada: idcontactollamada, Id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataLoteCliente();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

    $("#mvObservacion").dialog("close");

}

function updAddObservacion(idUpd) {
    //limpiarObservacion();

    $.ajax({
        url: 'ObtieneObservacionTmp',
        data: { id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {
                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(obs.IdObs);
                    loadTipoObservacion('ddlTipoObservacion', obs.IdTipoObservacion);
                    $("#txtObservacion").val(obs.Observacion);
                    $("#mvObservacion").dialog("open");

                    alert($("#hidEdicionObs").val());
                } else {

                    $("#mvObservacion").dialog("open");
                    //alert("No se pudo obtener la observación para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}