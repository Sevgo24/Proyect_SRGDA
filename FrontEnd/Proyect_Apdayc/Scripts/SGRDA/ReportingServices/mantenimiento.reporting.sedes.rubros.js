/************************** INICIO CARGA********************************************/
var Rubros = "";
var ModalidadDetalle = "";
var oficina = "";
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

    //NUEVO
    //FECHA CONFIRMACION
    $('#txtFecInicialConfirmacion').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinalConfirmacion').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicialConfirmacion").data("kendoDatePicker").value(new Date());
    var dCon = $("#txtFecInicialConfirmacion").data("kendoDatePicker").value();

    $("#txtFecFinalConfirmacion").data("kendoDatePicker").value(new Date());
    var dFINCon = $("#txtFecFinalConfirmacion").data("kendoDatePicker").value();

    $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(true);
    $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(true);

    RecuperarOficinaId();
    //-------------------------- EVENTO BOTONES ---------------------
    $('#trReporteCheck').hide();

    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        if ($("#chktodo").is(':checked')) {
            ExportarReportefTodos('PDF');
        } else {
            ExportarReportef('PDF');
        }
    });
    //$("#btnExcel").on("click", function () {
    //    var estadoRequeridos = ValidarRequeridos();
    //    //  $('#externo').attr("src", ExportarReportef('PDF'));
    //    if ($("#chktodo").is(':checked')) {
    //        ExportarReportef2TO('EXCEL');

    //    } else {
    //        ExportarReportef2('EXCEL');
    //    }
    //});

    // NUEVO
    // FECHA INGRESO
    $("#chkConFechaIngreso").prop('checked', false);
    $('#txtFecInicial').data('kendoDatePicker').enable(false);
    $('#txtFecFinal').data('kendoDatePicker').enable(false);
    $("#chkConFechaIngreso").change(function () {
        if ($('#chkConFechaIngreso').is(':checked')) {
            $('#txtFecInicial').data('kendoDatePicker').enable(true);
            $('#txtFecFinal').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFecInicial').data('kendoDatePicker').enable(false);
            $('#txtFecFinal').data('kendoDatePicker').enable(false);
        }
    });

    //FECHA COMFIRMACION
    $("#chkConFechaConfirmacion").prop('checked', true);
    $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(true);
    $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(true);
    $("#chkConFechaConfirmacion").change(function () {
        if ($('#chkConFechaConfirmacion').is(':checked')) {
            $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(true);
            $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(false);
            $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(false);
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
    obtenerUltimaActualizacion();
});

// PDF *******************************************
function ExportarReportefTodos(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();


    //var idoficina = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();

    var oficina_id = "";
    var OficinaAdmin = $("#lblOficinaTit").text();
    var NombreUsuario = $("#lblUsuarioTit").text();
    var nombreoficina = $("#dllOficina option:selected").text() == "" ? OficinaAdmin : $("#dllOficina option:selected").text();
    if (nombreoficina == '--SELECCIONE--') {
        nombreoficina = 'TODAS LAS OFICINAS'
    }
    nombreoficina = nombreoficina.replace('&', 'Y');
    if (oficina == "-1") {
        oficina_id = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
    } else {
        oficina_id = oficina;
    }
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    //alert(idoficina);
    if (validafecha == 1) {
        $("#contenedor").show();
        url = "http://ws2016-002/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fSedesRubros%2fRSRecaudacionSedeRubroFinalV1&" +
            "fecha1=" + ini + "&" +
            "fecha2=" + fin + "&" +
            "oficina=" + oficina_id + "&" +
            "conFechaIngreso=" + con_ingreso + "&" +
            "conFechaConfirmacion=" + con_confirmacion + "&" +
             "FINI_CON=" + ini_Con + "&" +
             "FFIN_CON=" + fin_Con + "&" +
             "OficinaNombre=" + nombreoficina + "&" +
              "Usuario=" + NombreUsuario + "&" +
             //"NombreOficina=" + nombreoficina + "&" +
             //"Usuario=" + NombreUsuario + "&" +
              "rs:Command=Render&rc:Parameters=false&rc:Zoom=Page Width ";
        alert(url)
        $("#contenedor").show();
        $('#externo').attr("src", url);
    } else if (dato.result == 0) {
        $('#externo').attr("src", vacio);
        $("#contenedor").hide();
        url = alert(dato.message);
    }
}

function ExportarReportef(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();

    var oficina_id = "";
    var OficinaAdmin = $("#lblOficinaTit").text();
    var NombreUsuario = $("#lblUsuarioTit").text();
    var nombreoficina = $("#dllOficina option:selected").text() == "" ? OficinaAdmin : $("#dllOficina option:selected").text();
    if (nombreoficina == '--SELECCIONE--') {
        nombreoficina = 'TODAS LAS OFICINAS'
    }
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //if ((oculta == true && oficina == null) || (ocultacombo == true && oficina == "26")) {
    //    rubro = vacio;
    //}
    //alert(ModalidadDetalle);

    if (oficina == "-1") {
        oficina_id = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
    } else {
        oficina_id = oficina;
    }
    //Reporte_SedesYRubro
    //alert("Normal");
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        url = "http://ws2016-002/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fLyrics%2fRecaudacionSedeRubro%2fRSRecaudacionSedeRubro&" +
            "fecha1=" + ini + "&" +
            "fecha2=" + fin + "&" +
            "oficina=" + oficina_id + "&" +
            "conFechaIngreso=" + con_ingreso + "&" +
            "conFechaConfirmacion=" + con_confirmacion + "&" +
             "FINI_CON=" + ini_Con + "&" +
             "FFIN_CON=" + fin_Con + "&" +
             "OficinaNombre=" + nombreoficina + "&" +
             "Usuario=" + NombreUsuario + "&" +
              "rs:Command=Render&rc:Parameters=false&rc:Zoom=Page Width ";
        alert(url)
        $("#contenedor").show();
        $('#externo').attr("src", url);
    } else if (dato.result == 0) {
        $('#externo').attr("src", vacio);
        $("#contenedor").hide();
        url = alert(dato.message);
    }
}

// EXCEL *****************************************
function ExportarReportef2(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();


    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        var url = "../ReporteListarRecaudacionSedes/ReporteRecaudacionSedes?" +
          "fini=" + ini + "&" +
          "ffin=" + fin + "&" +
            "formato=" + tipo
            + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;
        $.ajax({
            url: '../ReporteListarRecaudacionSedes/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con
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

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();


    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        var url = "../ReporteListarRecaudacionSedes/ReporteRecaudacionSedesTodas?" +
          "fini=" + ini + "&" +
          "ffin=" + fin + "&" +
            "formato=" + tipo
        + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;
        $.ajax({
            url: '../ReporteListarRecaudacionSedes/ReporteTipoTodas',
            type: 'POST',
            data: {
                fini: ini, ffin: fin,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con
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



function obtenerUltimaActualizacion() {
    $.ajax({
        url: '../ReporteListarRecaudacionSedes/obtenerUltimaActualizacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var comprobante = dato.data;
                $("#lblUltActualizacion").html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function RecuperarOficinaId() {
    $.ajax({
        url: '../Reporte_FacturasCanceladas/RecuperarOficina',
        data: {},
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                oficina = dato.valor;
            } else {
                alert(dato.message);
            }
        }
    })
}