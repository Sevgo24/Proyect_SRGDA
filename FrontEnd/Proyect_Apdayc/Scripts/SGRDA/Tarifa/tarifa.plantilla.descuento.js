
/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    $('#txtFechaIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFin').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtNumero').on("keypress", function (e) { return solonumeros(e); });
    $("#chkConFecha").prop('checked', true);
    $("#chkConFecha").change(function () {
        if ($('#chkConFecha').is(':checked')) {
            $('#txtFechaIni').data('kendoDatePicker').enable(true);
            $('#txtFechaFin').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFechaIni').data('kendoDatePicker').enable(false);
            $('#txtFechaFin').data('kendoDatePicker').enable(false);
        }
    });
    limpiar();
    loadEstadosMaestro("ddlEstado");
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {

        $('#grid').data('kendoGrid').dataSource.query({
            desc: $("#txtDescripcion").val(),
            nro: $('#txtNumero').val() == '' ? 0 : $('#txtNumero').val(),
            fini: $("#txtFechaIni").val(),
            ffin: $("#txtFechaFin").val(),
            estado: $("#ddlEstado").val(),
            confecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
            page: 1,
            pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {

            $('#grid').data('kendoGrid').dataSource.query({
                desc: $("#txtDescripcion").val(),
                nro: $('#txtNumero').val() == '' ? 0 : $('#txtNumero').val(),
                fini: $("#txtFechaIni").val(),
                ffin: $("#txtFechaFin").val(),
                estado: $("#ddlEstado").val(),
                confecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                page: 1,
                pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();

        $('#grid').data('kendoGrid').dataSource.query({
            desc: $("#txtDescripcion").val(),
            nro: $('#txtNumero').val() == '' ? 0 : $('#txtNumero').val(),
            fini: $("#txtFechaIni").val(),
            ffin: $("#txtFechaFin").val(),
            estado: $("#ddlEstado").val(),
            confecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
            page: 1,
            pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../TarifaPlantillaDescuento/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });

    loadData();
});

//****************************  FUNCIONES ****************************
function loadData() {
    var Vdesc = $("#txtDescripcion").val();
    var Vestado = $("#ddlEstado").val();
    var vnro = 0;
    var vfini = $("#txtFechaIni").val();
    var vffin = $("#txtFechaFin").val();
    if ($("#txtNumero").val() != '') vnro = $("#txtNumero").val()
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    url: '../TarifaPlantillaDescuento/Listar', dataType: "json",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            desc: $("#txtDescripcion").val(),
                            nro: $('#txtNumero').val() == '' ? 0 : $('#txtNumero').val(),
                            fini: $("#txtFechaIni").val(),
                            ffin: $("#txtFechaFin").val(),
                            estado: $("#ddlEstado").val(),
                            confecha: $('#chkConFecha').is(':checked') == true ? 1 : 0
                        })
                }
            },
            schema: { data: 'ListarTarifaPlantillaDsc', total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
				{ title: 'Eliminar', width: 18, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${TEMP_ID_DSC}'/>" },
				{ field: "TEMP_ID_DSC", width: 20, title: "Id", template: "<a id='single_2'  href=javascript:editar('${TEMP_ID_DSC}') style='color:gray;text-decoration:none;'>${TEMP_ID_DSC}</a>" },
				{ field: "TEMP_DESC", width: 80, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${TEMP_ID_DSC}') style='color:gray;text-decoration:none;'>${TEMP_DESC}</a>" },
                { field: "TEMP_NVAR", width: 35, title: "N° Caracteristicas", template: "<a id='single_2'  href=javascript:editar('${TEMP_ID_DSC}') style='color:gray;text-decoration:none;'>${TEMP_NVAR}</a>" },
                { field: "STARTS", type: "date", width: 30, title: "F. Vigencia", template: "<a id='single_2' href=javascript:editar('${TEMP_ID_DSC}') style='color:gray;text-decoration:none;'>" + '#=(STARTS==null)?"":kendo.toString(kendo.parseDate(STARTS,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
				{ field: "ESTADO", width: 20, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${TEMP_ID_DSC}') style='color:gray;text-decoration:none;'>${ESTADO}</a>" },
			]
    });
}

function editar(idSel) {
    document.location.href = '../TarifaPlantillaDescuento/Nuevo?id=' + idSel;
}

function limpiar() {
    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;

    var anoIni = parseInt(fullDate.getFullYear());
    var currentDateIni = twoDigitDate + "/" + twoDigitMonth + "/" + (anoIni - 1).toString();
    var currentDateFin = twoDigitDate + "/" + twoDigitMonth + "/" + (anoIni + 1).toString();

    $('#txtFechaIni').val(currentDateIni);
    $('#txtFechaFin').val(currentDateFin);
    $("#ddlEstado").val(1);
    $("#txtNumero").val('');
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
    $("#chkConFecha").prop('checked', true);
    $('#txtFechaIni').data('kendoDatePicker').enable(true);
    $('#txtFechaFin').data('kendoDatePicker').enable(true);
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            delOrigen(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una plantilla.");
    } else {
        loadData();
        alert("Plantilla(s) eliminada(s) correctamente.");
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../TarifaPlantillaDescuento/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}


