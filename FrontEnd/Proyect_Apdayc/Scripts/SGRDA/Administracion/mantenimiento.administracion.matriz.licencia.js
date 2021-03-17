var K_PAGINACION_MATRIZ = 1500;
//var K_GRID_COL = {
//    MODALIDAD: "MODALIDAD",
//    ULT_PER_FACT: 7,
//    PER_NOT_FACT: 8,
//    MONTO: 9,
//}
K_VARIABLES = {
    
    REPORTE_DIFERETES:2
}
$(function () {


    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarAgente({ container: "ContenedormvBuscarAgenteComercial", idButtonToSearch: "btnBuscarAGE", idDivMV: "mvBuscarAgente", event: "reloadEventoAgente", idLabelToSearch: "lbAgente" });

    loadDivisionTipo('ddTipoDivision', '0');

    loadTipoGrupo('dllGruModalidad', '0');

    $("#ddTipoDivision").on("change", function () {
        var tipo = $("#ddTipoDivision").val();
        loadDivisionXTipo('ddDivision', tipo, 'TODOS');
    });
    //FECHA========================================================================================
    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFactaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFactFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecInicialH').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinalH').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecInicialV').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFinalV').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecCreaInicial").data("kendoDatePicker").value();

    //$("#txtFecFactaInicial").data("kendoDatePicker").value(fechaActual);
    //var d2 = $("#txtFecFactaInicial").data("kendoDatePicker").value();

    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecCreaFinal").data("kendoDatePicker").value();

    //$("#txtFecFactFinal").data("kendoDatePicker").value(fechaFin);
    //var dFIN2 = $("#txtFecFactFinal").data("kendoDatePicker").value();



    $("#txtFecInicialH").data("kendoDatePicker").value(fechaActual);
    $("#txtFecFinalH").data("kendoDatePicker").value(fechaFin);

    //$("#txtFecInicialV").data("kendoDatePicker").value(fechaActual);
    //$("#txtFecFinalV").data("kendoDatePicker").value(fechaFin);
    //========================================================================================
    $("#btnBuscar").on("click", function () {
        ListarMatrizLicencia();
        $("#contenedor").hide();
    });

    $("#btnLimpiar").on("click", function () {
        limpiarMatrizLicencia();
    });

    //Ocultar 
    $("#btnEliminarLic").hide();
    $("#btnEliminarLic").on("click", function () {

        //var $tabs = $('#tabs').tabs();
        //var active = $tabs.tabs('option', 'active');
        var values = [];


        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk').attr('checked');
            if (checked == "checked") {
                var codigoLic = $row.find('.kendo-chk').attr('value');
                //alert(codigoLic);
                values.push(codigoLic);
                eliminarLicMatriz(codigoLic);
            }
        });
        if (values.length == 0) {
            alert("Seleccione para eliminar.");
        }
        ListarMatrizLicencia();


    });

    //$("#chkvalidacionLicencias").on("click", function () {
    //    $("#chkConFechaFact").prop("checked", false);
    //    //$("#chkConFechaCrea").prop("checked", false);
    //    $("#chkConPeriodoSinFact").prop("checked", false);
    //});

    //$("#chkConFechaFact").on("click", function () {
    //    $("#chkvalidacionLicencias").prop("checked", false);
    //    //$("#chkConFechaCrea").prop("checked", false);
    //    $("#chkConPeriodoSinFact").prop("checked", false);
    //});
    
    
    //$("#chkConPeriodoSinFact").on("click", function () {
    //    $("#chkvalidacionLicencias").prop("checked", false);
    //    $("#chkConFechaFact").prop("checked", false);
    //    //$("#chkConFechaCrea").prop("checked", false);
    //});



    $("#btnPdf").on("click", function () {

        $("#contenedor").show();

        ExportarReporteMatrizf('PDF');
    });

    $("#btnExcel").on("click", function () {

        ExportarReporteMatrizf('EXCEL');
        //ExportarReportef2('EXCEL');
    });

    limpiarMatrizLicencia();

    loadValoresConfiguracion('ddlOpcionesListar', 'LISTA MATRIZ', 'OPCIONES');
});


function ListarMatrizLicencia() {

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }

    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION_MATRIZ,
        transport: {
            read: {
                url: "../AdministracionMatrizLicencia/ListarMatrizLicencia",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {

                        LIC_ID:  $("#txtcodlic").val() == "" ? 0 : $("#txtcodlic").val(),
                        BPS_ID:  $("#txtcodsoc").val() == "" ? 0 : $("#txtcodsoc").val(),
                        RAZ_SOC: $("#txtraz").val() == "" ? "" : $("#txtraz").val(),
                        NUM_IDE: $("#txtruc").val() == "" ? "" : $("#txtruc").val(),
                        NOM_SOC: $("#txtnomsoc").val() == "" ? "" : $("#txtnomsoc").val(),
                        APE_SOC: $("#txtapepat").val() == "" ? "" : $("#txtapepat").val(),
                        MAT_SOC: $("#txtapemat").val() == "" ? "" : $("#txtapemat").val(),
                        EST_NAM: $("#txtnomest").val() == "" ? "" : $("#txtnomest").val(),
                        MOG_ID:  $("#dllGruModalidad").val() == "" ? "0" : $("#dllGruModalidad").val(),
                        CON_FEC: $("#chkConFechaCrea").is(':checked') == true ? 1 : 0,
                        FEC_INI: $("#txtFecCreaInicial").val(),
                        FEC_FIN: $("#txtFecCreaFinal").val(),
                        DIV_ID:  $("#ddDivision").val() == "" ? 0 : $("#ddDivision").val(),
                        DEP_ID:  $("#ddlSubTipo1").val() == "" ? 0 : $("#ddlSubTipo1").val(),
                        PROV_ID: $("#ddlSubTipo2").val() == "" ? 0 : $("#ddlSubTipo2").val(),
                        DIST_ID: $("#ddlSubTipo3").val() == "" ? 0 : $("#ddlSubTipo3").val(),
                        OFF_ID: $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val(),
                        ESTADO_LIC: $("#ddEstadoLic").val() == "" ? 0 : $("#ddEstadoLic").val(),
                        ESTADO_LIC_FACT: $("#ddEstadoLicFact").val() == "" ? 0 : $("#ddEstadoLicFact").val(),
                        ESTADO_PL_BLOQ: $("#ddEstadoPeriodo").val() == "" ? 0 : $("#ddEstadoPeriodo").val(),
                        CODIGO_AGENTE: $("#hidEdicionEntAGE").val() == "" ? 0 : $("#hidEdicionEntAGE").val(),
                        OPCIONES_BUSQ:$("#ddlOpcionesListar").val(),
                        FEC_INI_BUS: $("#txtFecInicialH").val(),
                        FEC_FIN_BUS: $("#txtFecFinalH").val()

                       
                    })
            }
        },
        schema: { data: "listaMatrizLicencia", total: 'TotalVirtual' }
    })


    var gridLic = $("#grid").kendoGrid({
        dataSource: data_sourceLic,
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
                {
                    title: '', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
                },
             { field: "LIC_ID", width: 7, title: "ID Lic.", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
             { field: "LIC_NAME", width: 15, title: "Socio", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${SOCIO} </a></font>" },
             { field: "NUM_DOC", width: 15, title: "Numero Documento", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${NUMERO_DOCUMENTO} </a></font>" },
             { field: "EST_NAME", width: 25, title: "Establecimiento", template: "<a id='single_2' style='color:gray;text-decoration:none;font-size:10px'>${EST_NAME}</a>" },
             //{ field: "LIC_MASTER", width: 7, title: "Lic. Multiple", template: "<a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:6px'>#if(LIC_MASTER != '0'){# ${LIC_MASTER} #} #  </a>" },
             { field: "DIRECCION", width: 20, title: "Direccion ", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${DIRECCION} </a></font>" },
             { field: "UBIGEO", width: 20, title: "Ubigeo ", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${UBIGEO} </a></font>" },
             { field: "MODALIDAD", width: 15, title: "Modalidad", template: " <font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${MODALIDAD} </a></font>" },
             { field: "ULT_PER_FACT", width: 10, title: "Ultimo Periodo", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${ULT_PER_FACT} </a></font>" },
             { field: "PER_NO_FACT", width: 10, title: "Periodo no Facturado", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${PER_NO_FACT} </a></font>" },
             { field: "MONTO", width: 7, title: "Monto ", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${MONTO} </a></font>" },
             { field: "ESTADO", width: 10, title: "Estado", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${ESTADO} </a></font>" },
             //{ field: "EST_ID", width: 18, title: "Establecimiento", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${EST_NAME} </a></font>" },
             { field: "LIC_ID", width: 5, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerLicenciVentanaNueva(${LIC_ID});'  border='0' title='Ver Licnecia En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho
            ]
    });
    //var grid = $("#grid").data("kendoGrid");
    ////if ($("#chkvalidacionLicencias").is(':checked') == true) {
    //grid.hideColumn(0);
        //grid.hideColumn(K_GRID_COL.ULT_PER_FACT);
        //grid.hideColumn(K_GRID_COL.PER_NOT_FACT);
        //grid.hideColumn(K_GRID_COL.MONTO);
    //}
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
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}



function VerLicenciVentanaNueva(lic_id) {

    window.open('../Licencia/Nuevo?set=' + lic_id, '_blank');
}






function eliminarLicMatriz(lic_id) {

    var codigosDel = { codigo: lic_id };
    $.ajax({
        url: '../Licencia/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result != 1) {
                alert(dato.message);
            }
        }
    });
}

function ExportarReporteMatrizf(tipo) {

    var url = "";

    $.ajax({
        url: '../AdministracionMatrizLicencia/ReporteTipo',
        type: 'POST',
        async: false,
        //data: { formato: tipo },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {

                //alert("ingreso");
                $("#grid").html('');
                if ($("#ddlOpcionesListar").val()<= K_VARIABLES.REPORTE_DIFERETES)
                {
                    url = "../AdministracionMatrizLicencia/ReporteMatrizLicencia?" +
                    "formato=" + tipo;
                } else {
                    url = "../AdministracionMatrizLicencia/ReporteMatrizLicenciaValidacion?" +
                    "formato=" + tipo;
                }
                $("#contenedor").show();
                $('#externo').attr("src", url);
                if(tipo=="EXCEL")
                    $("#contenedor").hide();


            } else if (dato.result == 0) {
                //alert("khe");

                //$('#externo').attr("src", vacio);
                $("#contenedor").hide();
                alert(dato.message);
            }
        }
    });

}


function limpiarMatrizLicencia() {

    
    $("#txtcodsoc").val("");
    $("#txtraz").val("");
    $("#txtcodlic").val("");
    $("#txtnomsoc").val("");
    $("#txtapepat").val("");
    $("#txtapemat").val("");
    $("#txtnomest").val("");
    $("#txtruc").val("");
    $("#dllGruModalidad").prop('selectedIndex', 10);//LOCALES
    $("#chkConFechaCrea").prop("checked", false);
    $("#txtFecCreaInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecCreaFinal").data("kendoDatePicker").value(new Date());
    $("#ddlDivision").prop('selectedIndex', 0);
    $("#ddTipoDivision").prop('selectedIndex', 0);
    $("#ddDivision").prop('selectedIndex', 0);
    $("#ddlSubTipo1").prop('selectedIndex', 0);
    $("#ddlSubTipo2").prop('selectedIndex', 0);
    $("#ddlSubTipo3").prop('selectedIndex', 0);
    $("#lbOficina").html('Seleccione Oficina');
    $("#hidOficina").val("");
    $("#ddEstadoLic").prop('selectedIndex', 0);
    $("#ddEstadoLicFact").prop('selectedIndex', 0);
    //$("#chkConFechaFact").prop("checked", false);
    //$("#txtFecFactaInicial").data("kendoDatePicker").value(new Date());
    //$("#txtFecFactFinal").data("kendoDatePicker").value(new Date());
    $("#ddEstadoPeriodo").prop('selectedIndex', 0);
    $("#hidEdicionEntAGE").val("");
    //$("#chkvalidacionLicencias").prop("checked", false);
    //$("#txtFecInicialV").data("kendoDatePicker").value(new Date());
    //$("#txtFecFinalV").data("kendoDatePicker").value(new Date());
    
}

//AGENTE COMERCIAL - BUSQ. GENERAL
var reloadEventoAgente = function (idSel) {
    $("#lbAgente").val(idSel);
    $("#hidEdicionEntAGE").val(idSel);
    obtenerNombreSocio($("#lbAgente").val(), 'lbAgente');

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