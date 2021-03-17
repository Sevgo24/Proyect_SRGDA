
var mvInitBuscarRecaudacionFactura = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + "mvBuscarFactura" + '"> ';
    elemento += '<input type="hidden"  id="hidIdidView" value="' + "mvBuscarFactura" + '"/>';
    elemento += '<input type="hidden"  id="hidId" value="' + parametro.event + ' "/>';

    elemento += '<div id="ContenedormvBuscarCorrelativo"></div>';
    elemento += '<div id="ContenedormvBuscarSocio"></div>';
    elemento += '<div id="ContenedormvBuscarGrupoFacuracion"></div>';
    elemento += '<div id="ContenedormvOficina"></div>';
    elemento += '<div id="ContenedormvBuscarAgenteComercial"></div>';
    //elemento += '<div id="ContenedormvModalidad"></div>';
    //elemento += '<div id="ContenedormvLicencia"></div>';

    elemento += '<table border=0  style=" width:100%; border:1px;">';
    elemento += '<tr>';
    elemento += '<td>';
    elemento += '<div class="contenedor">';
    elemento += '<table border=0 style=" width:100%; ">';


    elemento += '<tr>';
    elemento += '<td> Serie: </td>';
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarCorrelativo" style="cursor:pointer;" alt="Búsqueda de Serie" title="Búsqueda de Serie"/> </td>';
    elemento += '<td> <input type="hidden" id="hidCorrelativo"/> <label id="lbCorrelativo" style="cursor:pointer;" alt="Búsqueda de Serie" title="Búsqueda de Serie">Seleccione Serie</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '<td> Nro Factura: </td>'
    elemento += '<td> <input type="text" id="txtNumFact" maxlength="20" style="width:90px"/> </td>'
    elemento += '<td style="width:100px"> Tipo Licencia: </td>'
    elemento += '<td> <select id="dllTipoLicencia" ></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Socio de Negocio: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarBS" style="cursor:pointer;" alt="Búsqueda de Socio" title="Búsqueda de Socio"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdSocio"/> <label id="lbResponsable" style="cursor:pointer;" alt="Búsqueda de Socio" title="Búsqueda de Socio">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '<td> Grupo de Facturación: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarGRUPO" style="cursor:pointer;" alt="Búsqueda de Grupo Facturación" title="Búsqueda de Grupo Facturación"/> </td>';
    elemento += '<td> <input type="hidden" id="hidGrupo"/> <label id="lbGrupo" style="cursor:pointer;" alt="Búsqueda de Grupo Facturación" title="Búsqueda de Grupo Facturación">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '<td> Moneda: </td>'
    elemento += '<td> <select id="ddlMoneda" ></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Fecha Inicial: </td>'
    elemento += '<td> <input type="text" id="txtFecInicial" readonly="false" class="requerido"/> </td>'
    elemento += '<td> Fecha Final: </td>'
    elemento += '<td> <input type="text" id="txtFecFinal" readonly="false" class="requerido"/> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> División Administrativa: </td>'
    elemento += '<td> <select id="ddlDivisionAdministrativa" ></select> </td>'
    elemento += '<td> Oficina Comercial: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscaOficina" style="cursor:pointer;" alt="Búsqueda de Oficina" title="Búsqueda de Oficina"/> </td>';
    elemento += '<td> <input type="hidden" id="hidOficina"/> <label id="lbOficina" style="cursor:pointer;" alt="Búsqueda de Oficina" title="Búsqueda de Oficina">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '<td> Agente Comercial: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarAGE" style="cursor:pointer;" alt="Búsqueda de Agente Comercial" title="Búsqueda de Agente Comercial"/> </td>';
    elemento += '<td> <input type="hidden" id="hidEdicionEntAGE" value="0"/> <label id="lbAgente" style="cursor:pointer;" alt="Búsqueda de Agente Comercial" title="Búsqueda de Agente Comercial">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Factura ID: </td>'
    elemento += '<td> <input type="text" id="txtIdFact" maxlength="20" style="width:90px"/> </td>'
    elemento += '<td> N° Licencia: </td>'
    elemento += '<td> <input type="text" id="txtNumLic" maxlength="20" style="width:90px"/> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td colspan="6"><center><button id="btnBuscarFactura"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarFactura"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';
  
    elemento += '</table>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '<table>';
    elemento += '<tr>';
    elemento += '<td colspan="6" align="center"><div id="gridFacturaConsulta"></div></td>';
    elemento += '</tr>';
    elemento += '</table>';

    $("#" + parametro.container).append(elemento);
    $("#mvBuscarFactura").dialog({
        modal: true,
        autoOpen: false,
        width: 1200,
        height: 450,
        title: "Búsqueda General Recaudación Factura."
    });

    var tipoDivisionADM = 'ADM'
    loadMonedas('ddlMoneda', '0');
    loadTipoLicencia('dllTipoLicencia', '0');
    loadTipoPago('ddlFormapago', '0');
    $('#ddlFormapago').append($("<option />", { value: '0', text: '--SELECCIONE--' }));
    loadDivisionXTipo('ddlDivisionAdministrativa', tipoDivisionADM);
    $('#txtNumFact').on("keypress", function (e) { return solonumeros(e); });
    $('#txtNumLic').on("keypress", function (e) { return solonumeros(e); });
    $('#txtIdFact').on("keypress", function (e) { return solonumeros(e); });

    limpiar();
    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();
    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitBuscarSocioEntidad({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarAgente({ container: "ContenedormvBuscarAgenteComercial", idButtonToSearch: "btnBuscarAGE", idDivMV: "mvBuscarAgente", event: "reloadEventoAgente", idLabelToSearch: "lbAgente" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRUPO", idDivMV: "mvBuscarGrupo", event: "reloadEventoGrupo", idLabelToSearch: "lbGrupo" });

    $("#hidIdSocio").val(0);
    $("#hidSerie").val(0);

    $("#btnBuscarFactura").on("click", function () {
        loadDataFacturas();
        //var codMoneda = '';
        //if (moneda === undefined)
        //    var codMoneda = $("#ddlMoneda").val();
        //else
        //    codMoneda = moneda;
        //$('#gridFacturaConsulta').data('kendoGrid').dataSource.query({
        //    numSerial: $("#hidSerie").val(),
        //    numFact: $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val(),
        //    idSoc: $("#hidIdSocio").val(),
        //    grupoFact: $("#hidGrupo").val(),
        //    moneda: codMoneda,
        //    idLic: $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val(),
        //    Fini: $("#txtFecInicial").val(),
        //    Ffin: $("#txtFecFinal").val(),
        //    idFact: $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val(),
        //    page: 1, pageSize: K_PAGINACION.LISTAR_15
        //});
    });

    $("#btnLimpiarFactura").on("click", function () {
        limpiar();
    });

    loadDataFacturas();
};

function loadDataFacturas() {
    var serie = $("#hidSerie").val() == undefined ? 0 : $("#hidSerie").val();
    var numFact = $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val();
    var idTipoLic = $("#dllTipoLicencia").val() == '' ? 0 : $("#dllTipoLicencia").val();
    var idBps = $("#hidIdSocio").val() == undefined ? 0 : $("#hidIdSocio").val();
    var idGF = $("#hidGrupo").val();
    var codMoneda = $("#ddlMoneda").val();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var idFact = $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val();
    var numLic = $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val();
    var idBpsAgen = $("#hidEdicionEntAGE").val() == undefined ? 0 : $("#hidEdicionEntAGE").val();

    $("#gridFacturaConsulta").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_5,
            transport: {
                read: {
                    url: "../FacturacionConsulta/ListarConsultaFactura",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            numSerial: serie,
                            numFact: numFact,
                            idSoc: idBps,
                            grupoFact: idGF,
                            moneda: codMoneda,
                            idLic: numLic,
                            Fini: ini,
                            Ffin: fin,
                            idFact: idFact,
                            licTipo: idTipoLic,
                            idBpsAgen: idBpsAgen
                        })
                }
            },
            schema: { data: "ListaConsultaFactura", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
            { field: "INV_ID", width: 5, title: " ID", template: "<a id='single_2'  href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INV_ID}</a>" },
            { field: "INVT_DESC", width: 20, title: "Tipo", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INVT_DESC}</a>" },
            //{ field: "INV_TYPE", width: 20, title: "Tipo", template: "<a id='single_2'  href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INV_TYPE}</a>" },
            { field: "NMR_SERIAL", width: 10, title: "Serie", template: "<a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${NMR_SERIAL}</a>" },
            { field: "INV_NUMBER", width: 5, title: "#", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INV_NUMBER}</a>" },
            { field: "INV_DATE", type: "date", width: 30, title: "Fecha", template: "<a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>" + '#=(INV_DATE==null)?"":kendo.toString(kendo.parseDate(INV_DATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            //{ field: "INV_NULL", width: 15, title: "Anulado", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INV_NULL}</a>" },
            { field: "SOCIO", width: 100, title: "Socio de negocio", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${SOCIO}</a>" },
            { field: "MONEDA", width: 20, title: "Moneda", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${MONEDA}</a>" },
            { field: "INV_NET", width: 15, title: "Total", template: "<font color='green'><a id='single_2' href=javascript:getINV('${INV_ID}') style='color:gray !important;'>${INV_NET}</a>" }
           ]
    });
};

var getINV = function (id) {
    var hidIdidView = $("#hidIdidView").val();
    var fnc = $("#hidId").val();
    $("#" + hidIdidView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiar() {
    $('#ddlMoneda').val(0);
    $('#dllTipoLicencia').val(0);
    $('#ddlFormapago').val(0);

    $("#hidEdicionEnt").val(0);
    $("#lbResponsable").html('Seleccione un socio.');

    $("#hidGrupo").val(0);
    $("#lbGrupo").html('Seleccione un grupo facturación.');

    $("#hidCorrelativo").val(0);
    $("#lbCorrelativo").html('Seleccione una serie.');
    $("#lbCorrelativo").css('color', 'black');
    $("#hidSerie").val(0);
    $("#hidActual").val(0);

    $("#txtIdFact").val('');
    $("#txtNumFact").val('');
    $("#txtNumLic").val('');
    $("#ddlDivisionAdministrativa").val(0);
    $("#hidOficina").val(0);
    $("#lbOficina").html('Seleccione una Oficina.');
    $("#hidEdicionEntAGE").val(0);
    $("#lbAgente").html('Seleccione un Agente.');

    //$("#gridFacturaConsulta").empty();
    //kendo.culture('es-PE');
    //$('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$("#txtFecInicial").data("kendoDatePicker").value(new Date());
    //var d = $("#txtFecInicial").data("kendoDatePicker").value();
    //$("#txtFecFinal").data("kendoDatePicker").value(new Date());
    //var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();
    loadDataFacturas();
}

//SERIE - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            }
        }
    });
}

// OFICINA - BUSQ. GENERAL
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

//SOCIO - BUSQ. GENERAL
var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    obtenerNombreSocioX($("#lbResponsable").val(), 'lbResponsable');

};

function obtenerNombreSocioX(idSel, control) {
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

//GRUPO - BUSQ. GENERAL
var reloadEventoGrupo = function (idSel) {
    $("#hidGrupo").val(idSel);
    $("#hidEdicionEntGRU").val(idSel);
    obtenerNombreGrupo($("#hidGrupo").val(), 'lbGrupo');
};

function obtenerNombreGrupo(id, control) {
    $.ajax({
        url: "../GrupoFacturacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#" + control).html(tipo.INVG_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//AGENTE COMERCIAL - BUSQ. GENERAL
var reloadEventoAgente = function (idSel) {
    $("#lbAgente").val(idSel);
    $("#hidEdicionEntAGE").val(idSel);
    obtenerNombreSocioX($("#lbAgente").val(), 'lbAgente');
};