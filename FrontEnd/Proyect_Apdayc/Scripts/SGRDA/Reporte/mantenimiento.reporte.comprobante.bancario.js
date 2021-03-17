/************************** INICIO CARGA********************************************/
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


    //NUEVO
    //FECHA RECHAZO
    $('#txtFecInicialRechazo').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinalRechazo').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicialRechazo").data("kendoDatePicker").value(new Date());
    var dCon = $("#txtFecInicialRechazo").data("kendoDatePicker").value();

    $("#txtFecFinalRechazo").data("kendoDatePicker").value(new Date());
    var dFINCon = $("#txtFecFinalRechazo").data("kendoDatePicker").value();

    $('#txtFecInicialRechazo').data('kendoDatePicker').enable(true);
    $('#txtFecFinalRechazo').data('kendoDatePicker').enable(true);

    //-------------------------- EVENTO BOTONES ---------------------
    $('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });

    $("#btnExcel_Detallado").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef3('EXCEL2');
    });
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    $('#trRubro').hide();
    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);

        if (oculta == true) {
            //$('#trRubro').show();
            $('#trRubro').hide();
        } else {
            $('#trRubro').hide();
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
                //$('#trRubro').show();
                $('#trRubro').hide();
            }
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        }
    });

    // NUEVO
    // FECHA INGRESO
    $("#chkConFechaIngreso").prop('checked', true);
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
    $("#chkConFechaConfirmacion").prop('checked', false);
    $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(false);
    $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(false);
    $("#chkConFechaConfirmacion").change(function () {
        if ($('#chkConFechaConfirmacion').is(':checked')) {
            $("#dllEstado").val("C").change();
            $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(true);
            $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFecInicialConfirmacion').data('kendoDatePicker').enable(false);
            $('#txtFecFinalConfirmacion').data('kendoDatePicker').enable(false);
        }
    });

    //FECHA RECHAZO
    $("#chkConFechaRechazo").prop('checked', false);
    $('#txtFecInicialRechazo').data('kendoDatePicker').enable(false);
    $('#txtFecFinalRechazo').data('kendoDatePicker').enable(false);
    $("#chkConFechaRechazo").change(function () {
        if ($('#chkConFechaRechazo').is(':checked')) {
            $("#dllEstado").val("R").change();
            $('#txtFecInicialRechazo').data('kendoDatePicker').enable(true);
            $('#txtFecFinalRechazo').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFecInicialRechazo').data('kendoDatePicker').enable(false);
            $('#txtFecFinalRechazo').data('kendoDatePicker').enable(false);
        }
    });
    loadBancos('ddlBanco', 0);

});

function ExportarReportef(tipo) {
    var estado = $("#dllEstado").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();


    var con_Rechazo = $('#chkConFechaRechazo').is(':checked') == true ? 1 : 0;
    var ini_Rech = $("#txtFecInicialRechazo").val();
    var fin_Rech = $("#txtFecFinalRechazo").val();
    var idBanco = $("#ddlBanco").val();
    

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
    if (idoficina == 0)
        nombreoficina = 'Consulta General';
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var validaselecciondelCombo = 1;
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();

        $.ajax({
            url: '../ReporteComprobanteBancario/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin,formato:tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina ,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, estado: estado
                , con_Rechazo: con_Rechazo, ini_Rech: ini_Rech, fin_Rech: fin_Rech, idBanco: idBanco
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                //    
                //    //url = "../ReporteComprobanteBancario/ReporteDiarioCaja?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                //    //    + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    url = "../ReporteComprobanteBancario/ReporteDiarioCaja?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                        + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con + "&estado=" + estado;
                     //+ "&con_Rechazo=" + con_Rechazo + "&ini_Rech=" + ini_Rech + "& fin_Rech=" + fin_Rech + "& idBanco=" + idBanco;
                    
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
        {
            $("#contenedor").hide();
        }
    }
}

function ExportarReportef2(tipo) {
    var estado = $("#dllEstado").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();


    var con_Rechazo = $('#chkConFechaRechazo').is(':checked') == true ? 1 : 0;
    var ini_Rech = $("#txtFecInicialRechazo").val();
    var fin_Rech = $("#txtFecFinalRechazo").val();
    var idBanco = $("#ddlBanco").val();


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
    if (idoficina == 0)
        nombreoficina = 'Consulta General';
    //Valida Seleccion del combo

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var validaselecciondelCombo = 1;
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();

        $.ajax({
            url: '../ReporteComprobanteBancario/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin,formato:tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina ,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, estado: estado
                , con_Rechazo: con_Rechazo, ini_Rech: ini_Rech, fin_Rech: fin_Rech, idBanco: idBanco

            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    var url = "../ReporteComprobanteBancario/ReporteDiarioCaja?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                           + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                           + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con + "&estado=" + estado;
                            //+ "&con_Rechazo=" + con_Rechazo + "&ini_Rech=" + ini_Rech + "&fin_Rech=" + fin_Rech + "&idBanco=" + idBanco;

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
        
function ExportarReportef3(tipo) {
    var estado = $("#dllEstado").val();
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();


    var con_Rechazo = $('#chkConFechaRechazo').is(':checked') == true ? 1 : 0;
    var ini_Rech = $("#txtFecInicialRechazo").val();
    var fin_Rech = $("#txtFecFinalRechazo").val();
    var idBanco = $("#ddlBanco").val();


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
    if (idoficina == 0)
        nombreoficina = 'Consulta General';
    //Valida Seleccion del combo

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var validaselecciondelCombo = 1;
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();

        $.ajax({
            url: '../ReporteComprobanteBancario/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, formato: tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, estado: estado
                , con_Rechazo: con_Rechazo, ini_Rech: ini_Rech, fin_Rech: fin_Rech, idBanco: idBanco

            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    var url = "../ReporteComprobanteBancario/ReporteDiarioCaja?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                           + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                           + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con + "&estado=" + estado;
                    //+ "&con_Rechazo=" + con_Rechazo + "&ini_Rech=" + ini_Rech + "&fin_Rech=" + fin_Rech + "&idBanco=" + idBanco;

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
    
                
    