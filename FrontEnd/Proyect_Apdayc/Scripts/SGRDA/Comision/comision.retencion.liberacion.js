var K_WIDTH = 500;
var K_HEIGHT = 290;

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {
    kendo.culture('es-PE');
    $("#txtFechaIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });

    InicializarFechas();
    loadAgente('ddlNivelAgente', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadMonedaRecaudacion('ddlMoneda', 0);
    loadDivisionesadministrativas('');

    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvBuscarEstablecimiento", event: "reloadEventoEstablecimiento", idLabelToSearch: "lbEstablecimiento" });
    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
    mvInitBuscarTarifa({ container: "ContenedormvBuscarTarifa", idButtonToSearch: "btnBuscarTarifa", idDivMV: "mvBuscarTarifa", event: "reloadEventoTarifa", idLabelToSearch: "lbTarifa" });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    //mvInitBuscarSocioBusqueda({ container: "ContenedormvSocioBus", idButtonToSearch: "btnBuscarUsuario", idDivMV: "mvBuscarSocioBus", event: "reloadEventSocioBus", idLabelToSearch: "lblUsuario" });

    $("#btnBuscaOficina").on("click", function () {
        $("#ddlEstadoOficina").val(1);
        $("#txtOficinaSearch").val("");
        loadDataOficina();
    });

    $("#btnBuscar").on("click", function () {
        CargaGridRetencionLiberacion();
        obtenerTotalValor();
    });

    $("#btnEliminar").on("click", function () {
        obtenerRetenidos();
    });

    $("#btnLimpiarRetLib").on("click", function () {
        LimpiarRetencionComisiones();
    });
    
    loadDataLiberacionRetencion();
    obtenerTotalValor();
});

//var reloadEventSocioBus = function (idSel) {
//    $("#hidIdUsuario").val(idSel);
//    var estado = validarUsuarioDerecho(idSel);
//    if (estado)
//        obtenerNombreUsuario($("#hidIdUsuario").val());
//};

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
        url: '../ComisionRetencionLiberacion/ValidacionPerfilAgenteRecaudo',
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

function validarUsuarioDerecho(id) {
    var estado = false;
    $.ajax({
        data: { idAsociado: id },
        url: '../ComisionRetencionLiberacion/ValidacionPerfilUsuarioDerecho',
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

//function obtenerNombreUsuario(idSel) {
//    $.ajax({
//        data: { codigoBps: idSel },
//        url: '../General/ObtenerNombreSocio',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                $("#lblUsuario").html(dato.valor);
//            }
//        }
//    });
//};

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

var reloadEventoTarifa = function (idSel) {
    $("#hididTarifa").val(idSel);
    obtenerNombreConsultaTarifa($("#hididTarifa").val());
};

function obtenerNombreConsultaTarifa(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/ObtenerNombreTarifa',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbTarifa").html(dato.valor);
            }
        }
    });
}

var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
};

function obtenerNombreConsultaOficina(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/obtenerNombreOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            }
        }
    });
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

function loadDataLiberacionRetencion() {
    $("#gridRetencionLiberacion").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../ComisionRetencionLiberacion/ListarRetLib",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
                            IdTipoComision: $("#ddlTipoComision").val(),
                            IdNivel: $("#ddlNivelAgente").val(),
                            IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
                            IdEstablecimiento: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
                            IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
                            IdTarifa: $("#hididTarifa").val() == "" ? 0 : $("#hididTarifa").val(),
                            IdOficina: $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val(),
                            IdMoneda: $("#ddlMoneda").val(),
                            IdDivAdm: $("#ddlDivisionAdministrativa").val(),
                            FechaIni: $("#txtFechaIni").val(),
                            FechaFin: $("#txtFechaFin").val()
                        });
                }
            },
            schema: { data: "listaRetLibComision", total: 'TotalVirtual' }
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
            { field: "SEQUENCE", width: 20, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${SEQUENCE}</a></font>" },
            { field: "BPS_NAME", width: 60, title: "<font size=2px>Representante</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "LOG_DATE_CREAT", type: "date", width: 20, title: "<font size=2px>Fecha</font>", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "COM_INVOICE", width: 20, title: "<font size=2px>Factura</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${COM_INVOICE}</a></font>" },
            { field: "COM_VALUE", width: 30, title: "<font size=2px>Valor</font>", template: "<a id='single_2' class='kendo-tr-value' style='color:gray;text-decoration:none;font-size:11px'>${COM_VALUE}</a>" },
            { field: "LIC_ID", width: 30, title: "<font size=2px>Licencia Id</font>", template: "<font color='green'><a id='single_2'   style='color:gray !important;'>${LIC_ID}</a></font>" },
            { field: "COM_DESC", width: 30, title: "<font size=2px>Tipo Comisión</font>", template: "<font color='green'><a id='single_2' style='color:gray !important;'>${COM_DESC}</a></font>" },
            { field: "COM_PERC", width: 10, title: "<font size=2px>%</font>", template: "<a id='single_2' class='kendo-tr-value' style='color:gray;text-decoration:none;font-size:11px'>${COM_PERC}</a>" },
            {
                hidden: true,
                field: "COM_RDATE", type: "date", width: 30, title: "<font size=2px>Retención</font>", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>" + '#=(COM_RDATE==null)?"":kendo.toString(kendo.parseDate(COM_RDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>"
            },
            {
                field: "Retencion",
                width: 12,
                template: '<input type="checkbox" id="chkSel" class="kendo-chk" name="chkSel" value="${SEQUENCE}" #= Retencion ? checked="checked" : "" # />'
            }
           ]
    });
}

function obtenerRetenidos() {
    var Retenidos = [];
    var contador = 0;
    $('.k-grid-content tbody tr').each(function () {

        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idSeq = $row.find('.kendo-chk').attr('value');

            Retenidos[contador] = {
                SEQUENCE: idSeq,
                Retencion: 1
            };
            contador += 1;
        }
    });

    var Retenidos = JSON.stringify({ 'Retenidos': Retenidos });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ComisionRetencionLiberacion/ObtenerMatrizRetenidos',
        data: Retenidos,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                alert(dato.message);
                CargaGridRetencionLiberacion();
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

function CargaGridRetencionLiberacion() {
    $('#gridRetencionLiberacion').data('kendoGrid').dataSource.query({
        IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        IdTipoComision: $("#ddlTipoComision").val(),
        IdNivel: $("#ddlNivelAgente").val(),
        IdModalidad: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        IdEstablecimiento: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
        IdLicencia: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
        IdTarifa: $("#hididTarifa").val() == "" ? 0 : $("#hididTarifa").val(),
        IdOficina: $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val(),
        IdMoneda: $("#ddlMoneda").val(),
        IdDivAdm: $("#ddlDivisionAdministrativa").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_20
    });
}

function LimpiarRetencionComisiones() {
    InicializarFechas();
    $("#hidResponsable").val(0);
    $("#ddlTipoComision").val(0);
    $("#ddlNivelAgente").val(0);
    $("#hidModalidad").val(0);
    $("#hidIdEstablecimiento").val(0);
    $("#hidLicencia").val(0);
    $("#hidOficina").val(0);
    $("#hididTarifa").val(0);
    $("#ddlMoneda").val(0);
    $("#ddlDivisionAdministrativa").val(0);

    $("#lbResponsable").html("Seleccione");
    $("#lblModalidad").html("Seleccione");
    $("#lblEstablecimiento").html("Seleccione");
    $("#lblLicencia").html("Seleccione");
    $("#lbTarifa").html("Seleccione");
    $("#lbOficina").html("Seleccione");

    CargaGridRetencionLiberacion();
    obtenerTotalValor();
}

function obtenerTotalValor() {
    var entidad = {
        BPS_ID: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        COMT_ID: $("#ddlTipoComision").val(),
        LEVEL_ID: $("#ddlNivelAgente").val(),
        MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        EST_ID: $("#hidIdEstablecimiento").val(),
        LIC_ID: $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val(),
        RATE_ID: $("#hididTarifa").val() == "" ? 0 : $("#hididTarifa").val(),
        OFF_ID: $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val(),
        CUR_ALPHA: $("#ddlMoneda").val(),
        DAD_ID: $("#txtFecha").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val()
    };

    $.ajax({
        url: '../ComisionRetencionLiberacion/TotalValor',
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


//var mvInitBuscarSocioBusqueda = function (parametro) {
//    var elemento = '<div id="' + parametro.idDivMV + '"> ';
//    elemento += '<input type="hidden"  id="hidIdidModalViewBus" value="' + parametro.idDivMV + '"/>';
//    elemento += '<input type="hidden"  id="hidIdEventBus" value="' + parametro.event + ' "/>';
//    elemento += '<table border=0  style=" width:100%; border:1px;">';
//    elemento += '<tr>';
//    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
//    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPSBus" /></td>';
//    elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPSBus" /> <input type="text" id="txtNumeroBPSBus"></td>';
//    elemento += '</tr>';
//    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPSBus" size="70"></td>';
//    elemento += '</tr>';
//    elemento += '<tr>';
//    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPSBus" size="70"><input type="hidden" id="hidUbigeoBPSBus"></td>';
//    elemento += '</tr>';
//    elemento += '<tr>';
//    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSBus" /></td>';
//    elemento += '</tr>';
//    elemento += '<tr>';
//    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPSBus"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPSBus"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
//    elemento += '</td>';
//    elemento += '</tr>';
//    elemento += '</table></div>';
//    elemento += '</td>';
//    elemento += '</tr>';
//    elemento += '<tr>';
//    elemento += '<td colspan="4"><div id="gridBPSBus"></div>';
//    elemento += '</td>';
//    elemento += '</tr>';
//    elemento += '</table></div>';
//    elemento += '<style>';
//    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
//    elemento += '  html .ui-autocomplete {        height: 200px;    }';
//    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
//    elemento += ' </style> ';
//    $("#" + parametro.container).append(elemento);
//    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
//    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
//    $("#" + parametro.idDivMV).dialog({
//        modal: true,
//        autoOpen: false,
//        width: 800,
//        height: 500,
//        title: "Búsqueda General de Socio."
//    });
//    $("#btnBuscarSocioBPSBus").on("click", function () {
//        //loadDataFound();
//        //var est = $("#ddlEstadoBPS").val() == "A" ? 1 : 0;
//        //var valUbigeo = $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val();
//        $('#gridBPSBus').data('kendoGrid').dataSource.query({
//            tipoPersona: $("#ddlTipoPersonaBPSBus").val(),
//            tipo: $("#ddlTipoIdBPSBus").val() == "" ? 0 : $("#ddlTipoIdBPSBus").val(),
//            nro_tipo: $("#txtNumeroBPSBus").val(),
//            nombre: $("#txtRazonSocialBPSBus").val(),
//            ubigeo: $("#hidUbigeoBPSBus").val() == "" ? 0 : $("#hidUbigeoBPSBus").val(),
//            estado: $("#ddlEstadoBPSBus").val() == "A" ? 1 : 0,
//            page: 1, pageSize: 7
//        });
//    });
//    $("#btnLimpiarSocioBPSBus").on("click", function () {
//        limpiarBusqueda();
//    });
//    loadTipoDocumento("ddlTipoIdBPSBus", 0);
//    loadTipoPersona("ddlTipoPersonaBPSBus", 0);
//    loadEstados("ddlEstadoBPSBus", 0);
//    initAutoCompletarUbigeoB("txtUbigeoBPSBus", "hidUbigeoBPSBus", "604");

//    loadDataFoundBus();
//};

//var getBPSBus = function (id) {
//    var hidIdidModalViewBus = $("#hidIdidModalViewBus").val();
//    var fnc = $("#hidIdEventBus").val();
//    $("#" + hidIdidModalViewBus).dialog("close");
//    eval(fnc + " ('" + id + "');");
//};

//var limpiarBusquedaBus = function () {
//    $("#txtNumeroBPSBus").val("");
//    $("#txtRazonSocialBPSBus").val("");
//    $("#txtUbigeoBPSBus").val("");
//    $("#hidUbigeoBPSBus").val(0);
//};

//var loadDataFoundBus = function () {
//    $("#gridBPSBus").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: 7,
//            transport: {
//                read: {
//                    url: "../Socio/BuscarSocio",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {
//                            tipoPersona: $("#ddlTipoPersonaBPSBus").val(),
//                            tipo: $("#ddlTipoIdBPSBus").val() == "" ? 0 : $("#ddlTipoIdBPSBus").val(),
//                            nro_tipo: $("#txtNumeroBPSBus").val(),
//                            nombre: $("#txtRazonSocialBPSBus").val(),
//                            ubigeo: $("#hidUbigeoBPSBus").val() == "" ? 0 : $("#hidUbigeoBPSBus").val(),
//                            estado: $("#ddlEstadoBPSBus").val() == "A" ? 1 : 0
//                        });
//                }
//            },
//            schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: true,
//        selectable: true,
//        filterable: false,
//        columns:
//           [
//            { field: "BPS_ID", width: 5, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_ID}</a>" },
//            { field: "ENT_TYPE_NOMBRE", width: 10, title: "<font size=2px>Entidad</font>", template: "<a id='single_2'  href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ENT_TYPE_NOMBRE}</a>" },
//            { field: "TAXN_NAME", width: 10, title: "<font size=2px>Doc.</font>", template: "<a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${TAXN_NAME}</a>" },
//            { field: "TAX_ID", width: 12, title: "<font size=2px>Número</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${TAX_ID}</a></font>" },
//            { field: "BPS_NAME", width: 20, title: "<font size=2px>Razon Social</font>", template: "<font color='green'><a id='single_2'  href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a></font>" },
//            { field: "BPS_USER", width: 12, title: "<font size=2px>Usu Derecho</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_USER}</a></font>" },
//            { field: "BPS_COLLECTOR", width: 12, title: "<font size=2px>Aget Recaudo</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_COLLECTOR}</a></font>" },
//            { field: "BPS_ASSOCIATION", width: 12, title: "<font size=2px>Asociación</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_ASSOCIATION}</a></font>" },
//            { field: "BPS_EMPLOYEE", width: 12, title: "<font size=2px>Empleado</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_EMPLOYEE}</a></font>" },
//            { field: "BPS_GROUP", width: 12, title: "<font size=2px>Gr Económico</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_GROUP}</a></font>" },
//            { field: "BPS_SUPPLIER", width: 12, title: "<font size=2px>Proveedor</font>", template: "<font color='green'><a id='single_2' href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_SUPPLIER}</a></font>" },
//            { field: "Activo", width: 10, title: "<font size=2px>Estado</font>", template: "<font color='green'><a id='single_2'  href=javascript:getBPSBus('${BPS_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}# </a></font>" },
//           ]
//    });
//};