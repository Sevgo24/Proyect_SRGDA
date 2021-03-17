var K_WIDTH_CON = 400;
var K_HEIGHT_CON = 200;

$(function () {
    kendo.culture('es-PE');
    $("#txtFechaIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaPago").kendoDatePicker({ format: "dd/MM/yyyy" });

    var eventoKP = "keypress";
    $('#txtCodigoPago').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtNroOrden').on(eventoKP, function (e) { return solonumeros(e); });

    InicializarFechas();
    loadAgente('ddlNivelAgente', 0);
    //loadTipoComision('ddlTipoComision', 0);
    //loadMonedaRecaudacion('ddlMoneda', 0);
    //loadDivisionesadministrativas('');
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvBuscarEstablecimiento", event: "reloadEventoEstablecimiento", idLabelToSearch: "lbEstablecimiento" });
    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
    //mvInitBuscarTarifa({ container: "ContenedormvBuscarTarifa", idButtonToSearch: "btnBuscarTarifa", idDivMV: "mvBuscarTarifa", event: "reloadEventoTarifa", idLabelToSearch: "lbTarifa" });
    //mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    //mvInitBuscarSocioBusqueda({ container: "ContenedormvSocioBus", idButtonToSearch: "btnBuscarUsuario", idDivMV: "mvBuscarSocioBus", event: "reloadEventSocioBus", idLabelToSearch: "lblUsuario" });

    
    $("#mvPagos").dialog({
        autoOpen: false,
        width: K_WIDTH_CON,
        height: K_HEIGHT_CON,
        buttons: {
            "Agregar": addPago,
            "Cancel": function () {
                $("#mvPagos").dialog("close");
                $('#txtCodigoPago').css({ 'border': '1px solid gray' });
                $('#txtFechaPago').css({ 'border': '1px solid gray' });
                $('#txtNroOrden').css({ 'border': '1px solid gray' });
            }
        }, modal: true
    });

    $("#btnBuscar").on("click", function () {
        CargaGridPagos();
        obtenerTotalValor();
    });

    $("#btnPago").on("click", function () {
        $("#mvPagos").dialog({});
        $("#mvPagos").dialog("open");
        limpiarPago();
    });

    $("#btnPrePago").on("click", function () {
        obtenerComisionesPrePagos();
    });

    $("#btnLimpiarPagos").on("click", function () {
        LimpiarPagos();
    });
      

    loadDataPagos();
    obtenerTotalValor();
    FechaActual();
});

function FechaActual() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;

    $('#txtFechaPago').val(today);
}
 
function addPago() {
    if ($("#txtCodigoPago").val() == '' || $("#txtFechaPago").val() == '' || $("#txtNroOrden").val() == '') {
        $('#txtCodigoPago').css({ 'border': '1px solid red' });
        $('#txtFechaPago').css({ 'border': '1px solid red' });
        $('#txtNroOrden').css({ 'border': '1px solid red' });

    } else {            
        var entidad = {
            PAY_ID: $("#txtCodigoPago").val(),
            COM_PDATE: $("#txtFechaPago").val(),
            COM_PNUM: $("#txtNroOrden").val()
        };

        $.ajax({
            url: '../ComisionPago/AddPago',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {                    
                    obtenerComisionesPagos();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvPagos").dialog("close");
        $('#txtCodigoPago').css({ 'border': '1px solid gray' });
        $('#txtFechaPago').css({ 'border': '1px solid gray' });
        $('#txtNroOrden').css({ 'border': '1px solid gray' });
    }
}

function limpiarPago() {
    $("#txtCodigoPago").val("");
    $("#txtFecha").val("");
    $("#txtNroOrden").val("");
}

function CargaGridPagos() {
    $('#gridPagos').data('kendoGrid').dataSource.query({
        IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        IdNivel: $("#ddlNivelAgente").val(),
        IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        IdEstablecimiento: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
        IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_20
    });
}

function LimpiarPagos() {
    InicializarFechas();
    $("#hidResponsable").val(0);
    $("#ddlNivelAgente").val(0);
    $("#hidModalidad").val(0);
    $("#hidIdEstablecimiento").val(0);
    $("#hidLicencia").val(0);

    $("#lbResponsable").html("Seleccione");
    $("#lblModalidad").html("Seleccione");
    $("#lblEstablecimiento").html("Seleccione");
    $("#lblLicencia").html("Seleccione");

    CargaGridPagos();
    obtenerTotalValor();
}

function InicializarFechas() {
    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;

    var ultimoDia = new Date(fullDate.getFullYear(), fullDate.getMonth() + 1, 0);
    var anoIni = parseInt(fullDate.getFullYear());

    var currentDateIni = "01" + "/" + twoDigitMonth + "/" + (anoIni - 1).toString();
    var currentDateFin = ultimoDia.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();

    $('#txtFechaIni').val(currentDateIni);
    $('#txtFechaFin').val(currentDateFin);
}

function obtenerTotalValor() {
    var entidad = {
        BPS_ID: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        LEVEL_ID: $("#ddlNivelAgente").val(),
        MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        EST_ID: $("#hidIdEstablecimiento").val(),
        LIC_ID: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val()
    };

    $.ajax({
        url: '../ComisionPago/TotalValor',
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

var reloadEventoMod = function (idModSel) {
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
};

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    var estado = validarRolAgenteRecaudo(idSel);
    if (estado)
        obtenerNombreSocio($("#hidResponsable").val());
};

function validarRolAgenteRecaudo(id) {
    var estado = false;
    $.ajax({
        data: { idAsociado: id },
        url: '../ComisionPreLiquidar/ValidacionPerfilAgenteRecaudo',
        type: 'POST',
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

var reloadEventoEstablecimiento = function (idSel) {
    $("#hidIdEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
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

function loadDataPagos() {
    var grid = $("#gridPagos").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../ComisionPago/ListaPagoComision",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
                            IdNivel: $("#ddlNivelAgente").val(),
                            IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
                            IdEstablecimiento: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
                            IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
                            FechaIni: $("#txtFechaIni").val(),
                            FechaFin: $("#txtFechaFin").val()
                        });
                }
            },
            schema: { data: "listaPago", total: 'TotalVirtual' }
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
                field: "SEQUENCE", width: 0, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${SEQUENCE}</a></font>"
            },
            {
                field: "BPS_ID",
                width: 20, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2' class='classBpsId' style='color:gray !important;'>${BPS_ID}</a></font>"
            },
            { field: "BPS_NAME", width: 60, title: "<font size=2px>Representante</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "LOG_DATE_CREAT", type: "date", width: 20, title: "<font size=2px>Fecha</font>", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "COM_INVOICE", width: 15, title: "<font size=2px>Factura</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${COM_INVOICE}</a></font>" },
            { field: "COM_VALUE", width: 30, title: "<font size=2px>Valor</font>", template: "<a id='single_2' class='kendo-tr-value' style='color:gray;text-decoration:none;font-size:11px'>${COM_VALUE}</a>" },
            { field: "LIC_ID", width: 30, title: "<font size=2px>Licencia Id</font>", template: "<font color='green'><a id='single_2'   style='color:gray !important;'>${LIC_ID}</a></font>" },
            { field: "COM_DESC", width: 30, title: "<font size=2px>Tipo Comisión</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${COM_DESC}</a></font>" },
            { field: "COM_PERC", width: 10, title: "<font size=2px>%</font>", template: "<a id='single_2' class='kendo-tr-value' style='color:gray;text-decoration:none;font-size:11px'>${COM_PERC}</a>" },
            {
                hidden: true,
                field: "COM_PDATE", type: "date", width: 30, title: "<font size=2px>Retención</font>", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>" + '#=(COM_PDATE==null)?"":kendo.toString(kendo.parseDate(COM_PDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>"
            },
            {
                field: "PG",
                width: 8,
                template: '<input type="checkbox" id="chkSel" class="kendo-chk" name="chkSel" value="${SEQUENCE}" #= Pago ? checked="checked" : "" # />'
            }
           ]
    })
}

function obtenerComisionesPrePagos() {
    var PrePagos = [];
    var contador = 0;
    $('.k-grid-content tbody tr').each(function () {

        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idSeq = $row.find('.kendo-chk').attr('value');

            PrePagos[contador] = {
                SEQUENCE: idSeq,
                Liquidacion: 1
            };
            contador += 1;
        }
    });

    var PrePagos = JSON.stringify({ 'PrePagos': PrePagos });


    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ComisionPago/SetDatosPrePagos',
        data: PrePagos,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var url = "/ComisionPago/DownloadReportPrePagos";
                window.open(url);
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

function obtenerComisionesPagos() {
    var Pagos = [];
    var contador = 0;
    $('.k-grid-content tbody tr').each(function () {

        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idSeq = $row.find('.kendo-chk').attr('value');

            Pagos[contador] = {
                SEQUENCE: idSeq,
                Liquidacion: 1
            };
            contador += 1;
        }
    });

    var Pagos = JSON.stringify({ 'Pagos': Pagos });


    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ComisionPago/SetDatosPagos',
        data: Pagos,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var url = "../ComisionPago/DownloadReportPagos";
                window.open(url);
                CargaGridPagos();
                alert(dato.message);
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