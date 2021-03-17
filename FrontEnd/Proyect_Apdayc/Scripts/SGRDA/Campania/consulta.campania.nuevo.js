var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

var K_DIV_VALIDAR = {
    DIV_CAB: "divCabeceraCamp"
};
var K_DIV_MESSAGE = {
    DIV_CAMPANIA: "divMensajeError",
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento"
};

var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};

var K_WIDTH_DOC = 580;
var K_HEIGHT_DOC = 280;

$(function () {
    loadCentrosContactos('ddlCentroContacto', 0);
    loadTipoCampania('ddlTipoCampania', 0);

    //loadCampaniaContacto('ddlEstadoCampania', 0);
    $('#ddlEstadoCampania').append($("<option />", { value: '', text: '--SELECCIONE--' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'INI', text: 'INICIADA' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'OPE', text: 'OPERACION' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'CER', text: 'CERRADA' }));

    loadEntidades('ddlPerfilCliente', 0);
    loadTipoDoc('ddlTipoDocumento', 0);

    kendo.culture('es-PE');
    $('#txtFechaIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFin').kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    //$('#txtFechaIni').kendoDatePicker({ change: checkDateValidacionIniCamp });
    //$('#txtFechaFin').kendoDatePicker({ change: checkDateValidacionFinCamp });


    $("#tabs").tabs();
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_DOC, height: K_HEIGHT_DOC, buttons: { "Agregar": addDocumento, "Cancel": function () { $("#mvDocumento").dialog("close"); } }, modal: true });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addAgente").on("click", function () { limpiarAsociado(); $("#mvBuscarSocio").dialog("open"); });
    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });

    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    var id = GetQueryStringParams("id");
    if (id === undefined) {
        $("#divTituloPerfil").html("Campania - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Campania - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigo").val(id);
        ObtenerDatos(id);
    }

    $("#btnVolver").on("click", function () {
        location.href = "../CampaniaCallCenter/";
    }).button();

    $("#btnGrabar").on("click", function () {
        var estado = addvalidacionAgente();
        if (estado) {
            //dentro de obtenerLoteTrabajo esta la función para grabar.
            obtenerLoteTrabajo();
        };
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../CampaniaCallCenter/Nuevo";
    }).button();

    $(".addLote").on("click", function () {
        addLoteTrabajo();
    });
});

//function checkDateValidacionIniCamp() {

//}

//function checkDateValidacionFinCamp() {

//}

function addLoteTrabajo() {
    var IdAdd = 0;
    if ($("#hidAccionMvEnt").val() == "1") IdAdd = $("#hidEdicionEnt").val();
    var entidad = {
        Id: IdAdd
    };
    $.ajax({
        url: 'AddLoteTrabajo',
        type: 'POST',
        data: entidad,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                loadLoteTrabajo($("#hidCodigo").val());

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadLoteTrabajo(codCampania) {
    loadDataGridLoteTrabajoTmp('ListarLoteTrabajo', "#gridLoteTrabajo", codCampania);
}

function loadDataGridLoteTrabajoTmp(Controller, idGrilla, codCampania) {
    $.ajax({
        data: { idCampania: codCampania },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato);
            $(idGrilla).html(dato.message);
            addFecha();
        }
    });
}

function addFecha() {
    var FechaIniLote;
    var FechaFinLote;

    $('#tbLoteTrabajo tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            $('#txtFechaIni_' + id).kendoDatePicker({ format: "dd/MM/yyyy" });
            $('#txtFechaFin_' + id).kendoDatePicker({ format: "dd/MM/yyyy" });
        }

        var startDate = $('#txtFechaIni_' + id).kendoDatePicker({
            change: checkDateValidacionIni
        });
        var endDate = $('#txtFechaFin_' + id).kendoDatePicker({
            change: checkDateValidacionFin
        });

        function checkDateValidacionIni() {
            var fini = $('#txtFechaIni_' + id).val();
            var ffin = $('#txtFechaFin_' + id).val();
            var finicamp = $('#txtFechaIni').val();
            var ffincamp = $('#txtFechaFin').val();
            $.ajax({
                url: '../CampaniaCallCenter/ValidaRangoFechasLoteTrabajoInicioFin',
                type: 'POST',
                data: { FechaIni: fini, FechaFin: ffin, FechaIniCamp: finicamp, FechaFinCamp: ffincamp },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        $('#txtFechaIni_' + id).css({ 'border': '1px solid gray' });
                    } else if (dato.result == 0) {
                        $('#txtFechaIni_' + id).css({ 'border': '1px solid red' });
                        alert(dato.message);
                    }
                }
            });
        }

        function checkDateValidacionFin() {
            var ffin = $('#txtFechaFin_' + id).val();
            var fini = $('#txtFechaIni_' + id).val();
            var ffincamp = $('#txtFechaFin').val();
            var finicamp = $('#txtFechaIni').val();
            $.ajax({
                url: '../CampaniaCallCenter/ValidaRangoFechasLoteTrabajoFinInicio',
                type: 'POST',
                data: { FechaFin: ffin, FechaIni: fini, FechaFinCamp: ffincamp, FechaIniCamp: finicamp },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        $('#txtFechaFin_' + id).css({ 'border': '1px solid gray' });
                    } else if (dato.result == 0) {
                        $('#txtFechaFin_' + id).css({ 'border': '1px solid red' });
                        alert(dato.message);
                    }
                }
            });
        }
    });
}

function obtenerLoteTrabajo() {
    var LoteTrabajo = [];
    var contador = 0;
    $('#tbLoteTrabajo tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            LoteTrabajo[contador] = {
                Id: $('#txtId_' + id).val(),
                IdAgente: $('#ddlAgente_' + id).val(),
                IdCampania: $("#hidCodigo").val(),
                FechaIni: $('#txtFechaIni_' + id).val(),
                FechaFin: $('#txtFechaFin_' + id).val()
            };
            contador += 1;
        }
    });

    var LoteTrabajo = JSON.stringify({ 'LoteTrabajo': LoteTrabajo });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../CampaniaCallCenter/ObtenerLoteTrabajo',
        data: LoteTrabajo,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                Grabar();
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function delAddLoteTrabajo(idDel) {
    $.ajax({
        url: 'DellAddLoteTrabajo',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadLoteTrabajo($("#hidCodigo").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadDataDocumento() {
    loadDataGridTmp('ListarDocumento', "#gridDocumento");
}

function loadDataAsociado() {
    loadDataGridAsociaodsTmp('ListarAsociado', "#gridAgente");
}

function loadDataGridAsociaodsTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) {
            var dato = response; $(idGrilla).html(dato.message);
            addRoles();
        }
    });
}

function addRoles() {
    $('#tbAsociado tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        var idRol = parseFloat($(this).find("td").eq(6).html());
        if (!isNaN(id)) {
            if (idRol == undefined)
                idRol = 0;
            loadRol("ddlAplicable_" + id, idRol);
        }
    });
}

function AddAsociado(idAsociado, Nombre) {
    var IdAdd = 0;
    if ($("#hidAccionMvEnt").val() == "1") IdAdd = $("#hidEdicionEnt").val();
    var entidad = {
        Id: IdAdd,
        Codigo: $("#hidCodigoBPS").val(),
        CodigoAsociado: idAsociado,
        NombreAsociado: Nombre,
        RolTipo: "",
        RolTipoDesc: "",
        NroDocAsociado: $("#txtDocumento").val()
    };
    if (ValidarRequeridosET()) {
        $.ajax({
            url: 'AddAsociado',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataAsociado();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#avisoMVEntidad").html('');
        $("#mvEntidad").dialog("close");
    }
}

function limpiarDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#hidNombreFile").val("");
    $('#file_upload').css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#file_upload").val("");
}

function limpiarAsociado() {
    $("#hidAccionMvEnt").val("0");
    $("#hidEdicionEnt").val("");
    $("#ddlTipoDocumento1").val("");
    $("#txtDocumento").val("");
    $("#txtSocioAsociado").val("");
    $("#ddlRol").val("");
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    msgErrorET("", "txtSocioAsociado");
}

function ObtenerDatos(id) {
    $.ajax({
        url: '../CampaniaCallCenter/ObtieneDatos',
        type: 'GET',
        data: { Id: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    loadCentrosContactos('ddlCentroContacto', en.CONC_ID);
                    loadTipoCampania('ddlTipoCampania', en.CONC_CTID);
                    loadCampaniaContacto('ddlEstadoCampania', en.CONC_CSTATUS);
                    loadEntidades('ddlPerfilCliente', en.ENT_ID);
                    $("#txtNombre").val(en.CONC_CNAME);
                    var datepickerIni = $("#txtFechaIni").data("kendoDatePicker");
                    datepickerIni.value(en.FechaIni);
                    var datepickerFin = $("#txtFechaFin").data("kendoDatePicker");
                    datepickerFin.value(en.FechaFin);
                    $("#txtDescripcion").val(en.CONC_CDESC)
                }
                loadDataAsociado();
                loadDataDocumento();
                loadLoteTrabajo($("#hidCodigo").val());

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function Grabar() {
    msgOkB(K_DIV_MESSAGE.DIV_CAMPANIA, "");

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_CAMPANIA, K_DIV_VALIDAR.DIV_CAB)) {

        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigo").val();
        }
        else
            id = $("#hidCodigo").val();

        var CentroContacto = $("#ddlCentroContacto").val();
        var TipoCampania = $("#ddlTipoCampania").val();
        var EstadoCampania = $("#ddlEstadoCampania").val();
        var PerfilCliente = $("#ddlPerfilCliente").val();

        var en = {
            CONC_CID: id,
            CONC_CNAME: $("#txtNombre").val(),
            CONC_ID: CentroContacto,
            CONC_CDESC: $("#txtDescripcion").val(),
            CONC_CTID: TipoCampania,
            ENT_ID: PerfilCliente,
            CONC_CSTATUS: EstadoCampania,
            CONC_CDINI: $("#txtFechaIni").val(),
            CONC_CDEND: $("#txtFechaFin").val()
        };

        $.ajax({
            url: 'Insertar',
            data: en,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    document.location.href = '../CampaniaCallCenter/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
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
                        InitUploadTabDocCampania("file_upload", dato.Code);
                    }
                    loadDataDocumento();
                } else {
                    alert(dato.message);
                }
            }
        });
        loadDataDocumento();
        $("#mvDocumento").dialog("close");
    }
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
                loadDataDocumento();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddAsociado(idDel) {
    $.ajax({
        url: 'DellAddAsociado',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataAsociado();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}

var reloadEvento = function (idSel) {
    $("#hidAsocioado").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#hidNombreAsoc").val(dato.valor);
                AddAsociado($("#hidAsocioado").val(), $("#hidNombreAsoc").val());
            }
        }
    });
};

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

function UpdRol(id, idAsoc) {

    $('#ddlAplicable_' + id).css({ 'border': '1px solid gray' });

    var rol = {
        idRol: $("#ddlAplicable_" + id).val(),
        idAsociado: idAsoc
    };
    $.ajax({
        data: rol,
        url: 'UpdRol',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else {
                alert(dato.message);
            }
        }
    });
}

function UpdAgente(id, idAge) {

    $('#ddlAgente_' + id).css({ 'border': '1px solid gray' });

    var Agente = {
        Id: $('#ddlAgente_' + id).val(),
        IdAgente: $('#ddlAgente_' + id).val()
    };
    $.ajax({
        data: Agente,
        url: 'UpdAgente',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else {
                alert(dato.message);
            }
        }
    });
}

function addvalidacionAgente() {
    var result = true;
    $('#tbAsociado tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            //alert(id + " " + $('#ddlAplicable_' + id).val());
            if ($('#ddlAplicable_' + id).val() == 0) {
                $('#ddlAplicable_' + id).css({ 'border': '1px solid red' });
                result = false;
            }
            else {
                $('#ddlAplicable_' + id).css({ 'border': '1px solid gray' });
            }
        }
    });
    return result;
}

