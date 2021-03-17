/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    //FECHA INGRESO
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);
    
    //-------------------------- EVENTO BOTONES ---------------------
    $('#trReporteCheck').hide();
    $('#trFiltroFecha').hide();    
    loadListaContableDesplegable('ddlPeriodoContable', '0');

    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        if ($("#chktodo").is(':checked')) {
            ExportarReportefTodos('PDF');
        } else {
            ExportarReportef('PDF');
        }
    });
    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //  $('#externo').attr("src", ExportarReportef('PDF'));
        if ($("#chktodo").is(':checked')) {
            ExportarReportef2TO('EXCEL');

        } else {
            ExportarReportef2('EXCEL');
        }
    });

    //******************VALIDACIONES
    var ocultacombo = validarOficinaReportedl();
    if (ocultacombo == false) {
        $('#trReporteCheck').hide();
    } else {
        $('#trReporteChek').show();
        $("#chktodo").prop("checked", true);
    }

    $('#txtFecInicial').val('');
    $('#txtFecFinal').val('');
});

// PDF *******************************************
function ExportarReportefTodos(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    //var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    //var ini_Con = $("#txtFecInicialConfirmacion").val();
    //var fin_Con = $("#txtFecFinalConfirmacion").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";

    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteContableRecaudacionSedes/ReporteContableTipoTodas',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, idContable: idContable
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../ReporteContableRecaudacionSedes/ReporteContableRecaudacionSedesTodas?" +
                    "fini=" + ini + "&" +
                    "ffin=" + fin +
                    "&" + "formato=" + tipo +
                    "&" + "Contable=" + ContableDesc;                    
                    
                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {
        $("#contenedor").hide();
    }
}

function ExportarReportef(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteContableRecaudacionSedes/ReporteContableTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, idContable: idContable
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../ReporteContableRecaudacionSedes/ReporteContableRecaudacionSedes?" +
                    "fini=" + ini + "&" +
                    "ffin=" + fin +
                    "&" + "formato=" + tipo+
                    "&" + "Contable=" + ContableDesc;

                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {
        $("#contenedor").hide();
    }
}

// EXCEL *****************************************
function ExportarReportef2(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    //var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;
    //var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    //var ini_Con = $("#txtFecInicialConfirmacion").val();
    //var fin_Con = $("#txtFecFinalConfirmacion").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        var url = "../ReporteContableRecaudacionSedes/ReporteContableRecaudacionSedes?" +
          "fini=" + ini + "&" +
          "ffin=" + fin + "&" +
            "formato=" + tipo+
            "&" + "Contable=" + ContableDesc;
        $.ajax({
            url: '../ReporteContableRecaudacionSedes/ReporteContableTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, idContable: idContable
            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    window.open(url);
                    $("#contenedor").hide();
                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {
        $("#contenedor").hide();
    }
}

function ExportarReportef2TO(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    //var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;
    //var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    //var ini_Con = $("#txtFecInicialConfirmacion").val();
    //var fin_Con = $("#txtFecFinalConfirmacion").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        var url = "../ReporteContableRecaudacionSedes/ReporteContableRecaudacionSedesTodas?" +
          "fini=" + ini + "&" +
          "ffin=" + fin + "&" +
            "formato=" + tipo +
            "&" + "Contable=" + ContableDesc;
        //+ "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;
        $.ajax({
            url: '../ReporteContableRecaudacionSedes/ReporteContableTipoTodas',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, idContable: idContable
                //conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con
            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    window.open(url);
                    $("#contenedor").hide();
                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {
        $("#contenedor").hide();
    }
}
