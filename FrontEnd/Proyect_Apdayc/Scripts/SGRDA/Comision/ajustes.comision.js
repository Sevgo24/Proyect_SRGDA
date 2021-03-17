var K_WIDTH = 500;
var K_HEIGHT = 290;

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    $("#hidResponsable").val(0);
    $("#hidOpcionEdit").val(0);
    InicializarFechas();
    var eventoKP = "keypress";
    $('#txtvalorComision').on(eventoKP, function (e) { return solonumeros(e); });
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    loadMonedaRecaudacion('ddlMoneda', 0);
    loadOrigenComision('ddlOrigenComision', 0);

    $("#tabs").tabs();
    $("#mvAjusteComision").dialog({ autoOpen: false, width: K_WIDTH, height: K_HEIGHT, buttons: { "Garbar": grabar, "Cancelar": function () { $("#mvAjusteComision").dialog("close"); } }, modal: true });

    $(".addAjusteComision").on("click", function () {
        limpiar();
        if ($("#hidResponsable").val() != 0 && $("#hidLicencia").val() != 0) {
            var estado = validarInsertarAjusteComision();
            if (estado)
                $("#mvAjusteComision").dialog("open");
        }
        else
            $("#mvAjusteComision").dialog("open");
    });
    $("#btnBuscarAjusteComision").on("click", function () {
        CargaGridAjusteComision();
        obtenerTotalValor();
    });

    $("#btnLimpiarAjusteComision").on("click", function () {
        limpiarAjusteComision();
    });

    $("#linkAjustes").on("click", function () {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        if ($("#hidResponsable").val() == 0 && $("#hidLicencia").val() == 0) {
            $("#mvAjusteComision").dialog("close");
            alert("Para poder agregar busque, El respresentante y la licencia");
        }
        else if ($("#hidResponsable").val() == 0 && $("#hidLicencia").val() != 0) {
            $("#mvAjusteComision").dialog("close");
            alert("Busque, El respresentante");
        }
        else if ($("#hidResponsable").val() != 0 && $("#hidLicencia").val() == 0) {
            $("#mvAjusteComision").dialog("close");
            alert("Busque, La licencia");
        }
        else {
            $("#txtAgente").val($("#lbResponsable").html());
            $("#txtLicencia").val($("#hidLicencia").val())
        }
    });

    mvInitModalidadUso({
        container: "ContenedormvModalidad",
        idButtonToSearch: "btnBuscarMod",
        idDivMV: "mvModalidad",
        event: "reloadEventoMod",
        idLabelToSearch: "lblModalidad"
    });

    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento",
        idLabelToSearch: "lbResponsable"
    });

    mvInitLicencia({
        container: "ContenedormvLicencia",
        idButtonToSearch: "btnBuscarLic",
        idDivMV: "mvBuscarLicencia",
        event: "reloadEventoLicencia",
        idLabelToSearch: "lblLicencia"
    });

    loadDataAjusteComision();
    obtenerTotalValor();
});

function validarInsertarAjusteComision() {
    var estado = false;
    var en = {
        BPS_ID: $("#hidResponsable").val(),
        LIC_ID: $("#hidLicencia").val()
    };

    $.ajax({
        url: '../AjustesComision/Validacion',
        type: 'POST',
        dataType: 'JSON',
        data: en,
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

function editar(idSel) {
    $("#hidOpcionEdit").val(1);
    $("#hidIdAjusteComision").val(idSel);
    K_ACCION_ACTUAL = K_ACCION.Modificacion;
    obtenerDatos(idSel);
    $('#txtAgente').css({ 'border': '1px solid gray' });
    $('#txtLicencia').css({ 'border': '1px solid gray' });
    $('#ddlOrigenComision').css({ 'border': '1px solid gray' });
    $('#txtvalorComision').css({ 'border': '1px solid gray' });
    $("#mvAjusteComision").dialog("open");
};

var reloadEventoMod = function (idModSel) {
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
};

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    obtenerNombreSocio($("#hidResponsable").val());
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
};

var reloadEventoLicencia = function (idSel) {
    $("#hidLicencia").val(idSel);
    ObtenerNombreLicencia($("#hidLicencia").val());
};

function ObtenerNombreLicencia(idSel) {
    $.ajax({
        data: { codigoLic: idSel },
        url: '../General/ObtenerNombreLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lblLicencia").html(dato.valor);
            }
        }
    });
};

function obtenerDatos(id) {
    $.ajax({
        data: { Id: id },
        url: '../AjustesComision/ObtenerDatos',
        type: 'GET',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidResponsable").val(en.BPS_ID);
                    $("#txtAgente").val(en.BPS_NAME);
                    $("#txtLicencia").val(en.LIC_ID);
                    loadOrigenComision('ddlOrigenComision', en.COMT_ORIGEN);
                    $("#txtvalorComision").val(en.COM_VALUE);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerTotalValor() {
    var entidad = {
        BPS_ID: $("#hidResponsable").val(),
        CUR_ALPHA: $("#ddlMoneda").val(),
        LIC_ID: $("#hidLicencia").val(),
        MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        LOG_DATE_CREAT: $("#txtFecha").val()
    };

    $.ajax({
        url: '../AjustesComision/TotalValorAjusteComision',
        type: 'POST',
        dataType: 'JSON',
        data: entidad,
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#TotalComision").html(en.COM_VALUE);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataAjusteComision() {
    $("#gridAjuste").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../AjustesComision/ListarAjusteComisiones",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            IdAgente: $("#hidResponsable").val(),
                            Fecha: $("#txtFecha").val(),
                            IdMoneda: $("#ddlMoneda").val(),
                            IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
                            IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val()
                        });
                }
            },
            schema: { data: "listaAjustesCom", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            {
                hidden: true,
                title: 'Eliminar', width: 60, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${COM_ID}'/>"
            },
            { field: "COM_ID", width: 30, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${COM_ID}') style='color:gray !important;'>${COM_ID}</a>" },
            { field: "CUR_DESC", width: 50, title: "<font size=2px>Moneda</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ID}') style='color:gray !important;'>${CUR_DESC}</a></font>" },
            {
                hidden: true,
                field: "BPS_ID", width: 10, title: "<font size=2px>Id Agente</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ID}'') style='color:gray !important;'>${BPS_ID}</a></font>"
            },
            { field: "BPS_NAME", width: 100, title: "<font size=2px>Representante</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ID}') style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "LOG_DATE_CREAT", type: "date", width: 40, title: "<font size=2px>Fecha</font>", template: "<a id='single_2' href=javascript:editar('${COM_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "MOD_DEC", width: 100, title: "<font size=2px>Concepto</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ID}') style='color:gray !important;'>${MOD_DEC}</a></font>" },
            { field: "LIC_ID", width: 30, title: "<font size=2px>Licencia Id</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ID}') style='color:gray !important;'>${LIC_ID}</a></font>" },
            { field: "COM_VALUE", width: 20, title: "<font size=2px>Valor</font>", template: "<a id='single_2' class='kendo-tr-value' href=javascript:editar('${COM_ID}') style='color:gray;text-decoration:none;font-size:11px'>${COM_VALUE}</a>" },
           ]
    });
}

function limpiarAjusteComision() {
    $("#hidLicencia").val(0);
    $("#lblLicencia").html("Seleccione");
    $("#hidResponsable").val(0);
    $("#lbResponsable").html("Seleccione");
    $("#hidModalidad").val(0);
    $("#lblModalidad").html("Seleccione");
    InicializarFechas();
    $("#ddlMoneda").val(0);
};

function limpiar() {
    $("#ddlOrigenComision").val(0);
    $("#txtvalorComision").val("");
    $('#txtAgente').css({ 'border': '1px solid gray' });
    $('#txtLicencia').css({ 'border': '1px solid gray' });
    $('#ddlOrigenComision').css({ 'border': '1px solid gray' });
    $('#txtvalorComision').css({ 'border': '1px solid gray' });
}

function InicializarFechas() {
    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var anoIni = parseInt(fullDate.getFullYear());
    var currentDateIni = fullDate.getDate() + "/" + twoDigitMonth + "/" + (anoIni).toString();
    $('#txtFecha').val(currentDateIni);
}

function CargaGridAjusteComision() {
    $('#gridAjuste').data('kendoGrid').dataSource.query({
        IdAgente: $("#hidResponsable").val(),
        Fecha: $("#txtFecha").val(),
        IdMoneda: $("#ddlMoneda").val(),
        IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
        IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}

function grabar() {
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidIdAjusteComision").val();
        }
        else
            id = 0;

        var en = {
            valgraba: id,
            BPS_ID: $("#hidResponsable").val(),
            LIC_ID: $("#hidLicencia").val(),
            COMT_ORIGEN: $("#ddlOrigenComision").val(),
            COM_VALUE: $("#txtvalorComision").val()
        };

        $.ajax({
            url: '../AjustesComision/Insertar',
            data: en,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    $("#mvAjusteComision").dialog("close");
                    alert(dato.message);
                    CargaGrid();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}

//function CalcularTotalComision() {
//    var total = 0;
//    $(".k-grid-content tbody tr").each(function () {
//        var $row = $(this);
//        var col = parseFloat($row.find('.kendo-tr-value').text());
//        if (!isNaN(col)) {
//            if (col != 0 && col != undefined) {
//                total += col;
//                $("#TotalComision").html(total);
//            }
//        }
//    });
//}

