
$(function () {
    kendo.culture('es-PE');
    $('#txtNumRecibo').on("keypress", function (e) { return solonumeros(e); });
    $('#txtIdCobro').on("keypress", function (e) { return solonumeros(e); });

    $('#txtFechaIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFin').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();    
    //var fechaIni = new Date(fechaActual.getFullYear(), fechaActual.getMonth() , (fechaActual.getDate() - (fechaActual.getDate() - 1)));
    var fechaIni = new Date(fechaActual.getFullYear(), fechaActual.getMonth() - 1, fechaActual.getDate());
    $("#txtFechaIni").data("kendoDatePicker").value(fechaIni);
    var d = $("#txtFechaIni").data("kendoDatePicker").value();

    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
    $("#txtFechaFin").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFechaFin").data("kendoDatePicker").value();

    loadValoresConfiguracion('ddlEstadoMultirecibo', 'COBRO', 'MULTIRECIBO');
    loadValoresConfiguracion('ddlEstadoConfirmacion', 'COBRO', 'CONFIRMACION');
    loadBancos('ddlBanco', 0);

    $('#ddlCuenta').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    $("#ddlBanco").change(function () {
        var id = $("#ddlBanco").val();
        $('#ddlCuenta option').remove();
        $('#ddlCuenta').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuenta', id, '0')
    });
    $("#hidEdicionEnt").val(0);
    
    $("#ddlBanco").change(function () {
        var id = $("#ddlBanco").val();        
    });

    $("#chkConFecha").prop('checked', true);
    $("#chkConFecha").change(function () {
        if ($('#chkConFecha').is(':checked')) {
            $('#txtFechaIni').data('kendoDatePicker').enable(true);
            $('#txtFechaFin').data('kendoDatePicker').enable(true);
            $("#txtFechaIni").addClass("requerido");
            $("#txtFechaFin").addClass("requerido");
        } else {
            $('#txtFechaIni').data('kendoDatePicker').enable(false);
            $('#txtFechaFin').data('kendoDatePicker').enable(false);
            $("#txtFechaIni").removeClass("requerido");
            $("#txtFechaFin").removeClass("requerido");
            $("#txtFechaIni").css("border", "gray solid 1px");
            $("#txtFechaFin").css("border", "gray solid 1px");
        }
    });

    $("#btnLimpiar").on("click", function () {
        Limpiar();
    });

    $("#btnBuscar").on("click", function (e) {
        var estadoRequeridos = ValidarRequeridos();
        if (estadoRequeridos) {
            buscarFiltro();
        }
    });

    loadData();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../BEC/Nuevo';
    });

    mvInitBuscarCorrelativoRecibo({ container: "ContenedormvBuscarCorrelativoSerieBec", idButtonToSearch: "btnBuscarCorrelativoBec", idDivMV: "mvBuscarCorrelativoBec1", event: "reloadEventoCorrelativoBec", idLabelToSearch: "lbCorrelativoBec" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    $("#txtIdCobro").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13')  buscarFiltro();
    });
    $("#txtNumRecibo").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') buscarFiltro();
    });
    $("#txtVoucher").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') buscarFiltro();
    });
    $("#ddlestadoCobro option[value=" + 1 + "]").attr("selected", true);
});

function buscarFiltro(){
    $('#grid').data('kendoGrid').dataSource.query({
        idSerie: $("#hidCorrelativoBec").val(),
        voucher: $("#txtVoucher").val(),
        idBanco: $("#ddlBanco").val(),
        idCuenta: $("#ddlCuenta").val(),
        fini: $("#txtFechaIni").val(),
        ffin: $("#txtFechaFin").val(),
        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
        estado: parseInt($("#ddlEstadoMultirecibo").val()),
        bpsId: $("#hidEdicionEnt").val(),
        estadoConfirmacion: parseInt($("#ddlEstadoConfirmacion").val()),
        estadoCobro: $("#ddlestadoCobro").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}

function Limpiar() {
    $('#hidCorrelativoBec').val(0);
    $('#hidSerieBec').val(0);
    $('#hidActualBec').val(0);
    $('#lbCorrelativoBec').html('Seleccione una Serie.');
    //$('#ddlMoneda').val('');
    $('#txtVoucher').val('');
    $('#ddlBanco').val(0);
    $('#ddlCuenta').val(0);
    //$('#txtFechaIni').val('');
    //$('#txtFechaFin').val('');
    //loadMonedas('ddlMoneda', 'PEN');
    $("#hidEdicionEnt").val(0);
    $("#lbResponsable").html('Seleccione un Cliente.');
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        buscarFiltro();
    }
}

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../BEC/Listar",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        idSerie: $("#hidCorrelativoBec").val(),
                        voucher: $("#txtVoucher").val(),
                        idBanco: $("#ddlBanco").val(),
                        idCuenta: $("#ddlCuenta").val(),
                        fini: $("#txtFechaIni").val(),
                        ffin: $("#txtFechaFin").val(),
                        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                        estado:parseInt( $("#ddlEstadoMultirecibo").val()),
                        bpsId: $("#hidEdicionEnt").val(),
                        estadoConfirmacion: parseInt($("#ddlEstadoConfirmacion").val()),
                        estadoCobro: parseInt($("#ddlestadoCobro").val()),
                        numRecibo: $("#txtNumRecibo").val(),
                        idCobro: $("#txtIdCobro").val(),
                    })
            }
        },
        schema: { data: "ListarMultiRecibo", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
                { field: "VERSION", width: 6, hidden: true, title: "VERSION", template: "<a id='single_2'  href=javascript:editar('${MREC_ID}','${VERSION}') style='color:gray;text-decoration:none;font-size:11px'>${VERSION}</a>" },
                //{ field: "VERSION", width: 6,  title: "VERSION", template: "<a id='single_2'  href=javascript:editar('${MREC_ID}','${VERSION}') style='color:gray;text-decoration:none;font-size:11px'>${VERSION}</a>" },
                { field: "MREC_ID", width: 4, title: "Id", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}') style='color:gray;text-decoration:none;font-size:11px'>${MREC_ID}</a>" },
				{ field: "REC_ID", width: 4, hidden:true, title: "Id", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}') style='color:gray;text-decoration:none;font-size:11px'>${REC_ID}</a>" },
                { field: "RECIBO_BEC", width: 5, title: "N° Recibo", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='color:gray;text-decoration:none;font-size:11px'>${RECIBO_BEC}</a>" },
                { field: "FECHA", width: 6, title: "Fecha de Creación", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='color:gray;text-decoration:none;font-size:11px'>${FECHA}</a>" },
                { field: "SOCIO", width: 20, title: "Cliente", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='color:gray;text-decoration:none;font-size:11px'>${SOCIO}</a>" },
                { field: "STRMREC_TTOTAL", width: 10, title: "Total Aplicado", template: "<div style='text-align:right;'><a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='color:gray;text-decoration:none;font-size:11px;'>${STRMREC_TTOTAL}</a></div>" },
                { field: "ESTADO_MULTIRECIBO_DES", width: 10, title: "Estado del Recibo", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='text-align:center; color:gray;text-decoration:none;font-size:11px'>${ESTADO_MULTIRECIBO_DES}</a>" },
                { field: "ESTADO_CONFIRMACION_DES", width: 10, title: "Estado de Confirmación", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='text-align:center; color:gray;text-decoration:none;font-size:11px'>${ESTADO_CONFIRMACION_DES}</a>" },
                { field: "ESTADO_COBRO", width: 7, title: "Estado del Cobro", template: "<a id='single_2'  href=javascript:editar('${REC_ID}','${VERSION}')  style='text-align:center; color:gray;text-decoration:none;font-size:11px'>${ESTADO_COBRO}</a>" },
        ]
    });
};

function editar(idSel, version) {
    document.location.href = '../BEC/Nuevo?id=' + idSel + '&ver=' + version
}

//SERIE BEC - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativoBec = function (idSel) {
    $("#hidCorrelativoBec").val(idSel);
    obtenerNombreCorrelativoBec($("#hidCorrelativoBec").val());
};

function obtenerNombreCorrelativoBec(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativoBec").html(cor.NMR_SERIAL);
                $("#hidSerieBec").val(cor.NMR_SERIAL);
                $("#hidActualBec").val(cor.NMR_NOW);
                $("#lbCorrelativoBec").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}


//SOCIO - BUSQ. GENERAL
var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    obtenerNombreSocio($("#lbResponsable").val(), 'lbResponsable');
};

function obtenerNombreSocio(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
            }
        }
    });
}
