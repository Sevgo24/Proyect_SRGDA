﻿/************************** INICIO CARGA********************************************/
var oficina = "";
$(function () {

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);

    loadListaContableDesplegable('ddlPeriodoContable', '0');
    //-------------------------- EVENTO BOTONES ---------------------
    RecuperarOficinaId();
    $('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);

        if (oculta == true) {
            $('#trRubro').show();
            //$("input[name=valida]").prop('enabled',true);
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);

        }
    }
    //Si ocultacombo sera =1 Siempre que el usuario que ingreso sea Admin o Contabilidad
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
    }
    else {
        //deshabilita el Select
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);
    }

    $("#dllOficina").change(function () {
        if ($(this).val() == "26") {
            if (ocultacombo == 1) {
                $('#dllOficina').show();
                $('#trRubro').show();
            }
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        }
    });

    $('#txtFecInicial').val('');
    $('#txtFecFinal').val('');

});

function ExportarReportef(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();

    var ini = $("#txtFecInicial").val() == "" ? "01/01/1990" : $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val() == "" ? "01/01/3000" : $("#txtFecFinal").val();

    //var ini = $("#txtFecInicial").val() == "" ? "" : $("#txtFecInicial").val();
    //var fin = $("#txtFecFinal").val() == "" ? "" : $("#txtFecFinal").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //obtiene el valor del combo
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

    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    //if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        url = "http://ws2016-002/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fDepositoBanco%2fRSContableDepositoBancoFinalV1&" +
     "fecha1=" + ini + "&" +
     "fecha2=" + fin + "&" +
     "OFICINA=" + oficina_id + "&" +// me quede aqui
     "territorio=" + 0 + "&" +
     "idContable=" + idContable + "&" +
     //"conFechaConfirmacion=" + con_confirmacion + "&" +
      "OficinaNombre=" + nombreoficina + "&" +
      "Usuario=" + NombreUsuario + "&" +
    
       "rs:Command=Render&rc:Parameters=false&rc:Zoom=Page Width ";
        alert(url);
        $("#contenedor").show();
        $('#externo').attr("src", url);                   

    //} else {

    //    $("#contenedor").hide();
    //}
}

function ExportarReportef2(tipo) {
    var idContable = $("#ddlPeriodoContable").val();
    var ContableDesc = $("#ddlPeriodoContable option:selected").text();


    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }

    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteContableDepositoBanco/ReporteTipo',
            type: 'POST',
            //data: {
            //    fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
            //    conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con
            //},
            data: {
                fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina, idContable: idContable
                //conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //url = "../ReporteDepositoBanco/ReporteDepositoBanco?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                    //   + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    //url = "../ReporteContableDepositoBanco/ReporteDepositoBanco?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                    //    + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                    //+ "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;
                    url = "../ReporteContableDepositoBanco/ReporteDepositoBanco?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;

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
