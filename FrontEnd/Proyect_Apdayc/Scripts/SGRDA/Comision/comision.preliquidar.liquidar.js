$(function () {
    kendo.culture('es-PE');
    $("#txtFechaIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });

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

    $("#btnBuscar").on("click", function () {
        CargaGridPreLiquidacion();
        obtenerTotalValor();
    });

    $("#btnLiquidar").on("click", function () {
        obtenerComisionesParaLiquidar();
        obtenerTotalValor();
    });

    $("#btnPreLiquidar").on("click", function () {
        obtenerComisionesPreLiquidar();

        //var url = "/ComisionPreLiquidar/GenerateAndDisplayReport"
        //document.location.target = '_blank';
        //document.location.href = url;

        //$.ajax({
        //    url: "/ComisionPreLiquidar/GenerateAndDisplayReport",
        //    type: 'GET',
        //    success: function (result) {
        //    //    //do the necessary updations
        //    },
        //    error: function (result) {
        //    }
        //});
    });

    $("#btnLimpiarLiquidacion").on("click", function () {
        LimpiarLiquidacion();
    });

    loadDataLiquidacion();
    obtenerTotalValor();
});

function obtenerComisionesParaLiquidar() {
    var Liquidados = [];
    var contador = 0;
    $('.k-grid-content tbody tr').each(function () {

        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idSeq = $row.find('.kendo-chk').attr('value');

            Liquidados[contador] = {
                SEQUENCE: idSeq,
                Liquidacion: 1
            };
            contador += 1;
        }
    });

    var Liquidados = JSON.stringify({ 'Liquidados': Liquidados });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        //url: '../ComisionPreLiquidar/ObtenerLiquidar',
        url: '../ComisionPreLiquidar/SetDatosLiquidar',
        data: Liquidados,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                var url = "/ComisionPreLiquidar/DownloadReportLiquidadas";
                window.open(url);

                alert(dato.message);
                CargaGridPreLiquidacion();
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

function obtenerComisionesPreLiquidar() {
    var PreLiquidados = [];
    var contador = 0;
    $('.k-grid-content tbody tr').each(function () {

        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idSeq = $row.find('.kendo-chk').attr('value');

            PreLiquidados[contador] = {
                SEQUENCE: idSeq,
                Liquidacion: 1
            };
            contador += 1;
        }
    });

    var PreLiquidados = JSON.stringify({ 'PreLiquidados': PreLiquidados });


    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ComisionPreLiquidar/SetDatos',
        data: PreLiquidados,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var url = "/ComisionPreLiquidar/DownloadReportPreLiquidadas";
                //document.location.target = '_blank';
                //document.location.href = url;
                //document.w
                window.open(url);
                //alert(dato.message);
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
        url: '../ComisionPreLiquidar/TotalValor',
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

function LimpiarLiquidacion() {
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

    CargaGridPreLiquidacion();
    obtenerTotalValor();
}

function loadDataLiquidacion() {
    var grid = $("#gridPreliquidar").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../ComisionPreLiquidar/ListaPreYLiquidacionComision",
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
            schema: { data: "listaPreLiquidacionComision", total: 'TotalVirtual' }
        },
        dataBound: onDataBound,
        //selectable: 'row',
        //change: function (arg) {

        //    var grid = $("#gridPreliquidar").data("kendoGrid");
        //    // Get selected rows
        //    var sel = $("input:checked", grid.tbody).closest("tr");
        //    // Get data item for each
        //    var items = [];
        //    $.each(sel, function (idx, row) {
        //        var item = grid.dataItem(row);
        //        items.push(item);
        //    });

        //    alert("selected: " + JSON.stringify(items));

        //},
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
                id:"clickcheck",
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
                field: "COM_LDATE", type: "date", width: 30, title: "<font size=2px>Retención</font>", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>" + '#=(COM_LDATE==null)?"":kendo.toString(kendo.parseDate(COM_LDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>"
            },
            {
                field: "LQ",
                width: 8,
                template: '<input type="checkbox" id="chkSel" class="kendo-chk" name="chkSel" onchange="cokclick()" value="${SEQUENCE}" #= Liquidacion ? checked="checked" : "" # />'
            }
           ]
    }).data("kendoGrid");


    //bind click event to the checkbox
    grid.table.on("click", ".checkbox", selectRow);
    $("#clickcheck").bind("click", function () {
        var checked = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                checked.push(i);
            }
        }
        alert(checked);
    });
}

var checkedIds = {};
//on click of the checkbox:
function selectRow() {
    var checked = this.checked,
    row = $(this).closest("tr"),
    grid = $("#gridPreliquidar").data("kendoGrid"),
    dataItem = grid.dataItem(row);
    checkedIds[dataItem.id] = checked;
    if (checked) {
        //-select the row
        row.addClass("k-state-selected");
    } else {
        //-remove selection
        row.removeClass("k-state-selected");
    }
}

function onDataBound(e) {
    var view = this.dataSource.view();
    for (var i = 0; i < view.length; i++) {
        if (checkedIds[view[i].id]) {
            this.tbody.find("tr[data-uid='" + view[i].uid + "']")
            .addClass("k-state-selected")
            .find(".checkbox")
            .attr("checked", "checked");
        }
    }
}

function CargaGridPreLiquidacion() {
    $('#gridPreliquidar').data('kendoGrid').dataSource.query({
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

//function cokclick() {
//    alert("Me hiciste click");
//}
	
function cokclick() {
    //var grid = $("#gridPreliquidar").data("kendoGrid");
    //var row = grid.select();
    //var dataItem = grid.dataItem(row);
    //var cval = $(dataItem.col(10)).is(':checked'); // how do I get the checkbox column value?? var pid = dataItem.PersonId; alert(pid + "; " + cval); // cval is undefined

    //$('.kendo-chk').click(function (e) {
    //    if ($(this).is(':checked')) {
    //        alert('checked');
    //    } else {
    //        alert('not checked');
    //    }
    //});
    //obtenerComisionesPreLiquidar();
};


//$('.kendo-chk').click(function (e) {
//    if ($(this).is(':checked')) {
//        alert('checked');
//        cokclick();
//    } else {
//        alert('not checked');
//    }
//});
