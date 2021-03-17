/************************** INICIO CARGA********************************************/
var K_ITEM = { CHOOSE: '--SELECCIONE--', ALL: '--TODOS--' };
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

    //$("input[name=valida]").hide();
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

    loadComboTipoCobro('ddlTipoCobro', '0');
    //-------------------------- EVENTO BOTONES ---------------------

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


});

function loadComboTipoCobro(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.ALL }));
    $.ajax({
        url: '../ReporteDepositoBanco/LISTA_TIPO_COBRO',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, TipoCobro) {
                    if (TipoCobro.Value == valSel)
                        $('#' + control).append($("<option />", { value: TipoCobro.Value, text: TipoCobro.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: TipoCobro.Value, text: TipoCobro.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function ExportarReportef(tipo) {
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();


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
    var TIPO = $("#ddlTipoCobro").val();
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteDepositoBanco/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, TipoCobro: TIPO
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
                    //    + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    url = "../ReporteDepositoBanco/ReporteDepositoBanco?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                    + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;

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

function ExportarReportef2(tipo) {
    var con_ingreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;

    var con_confirmacion = $('#chkConFechaConfirmacion').is(':checked') == true ? 1 : 0;
    var ini_Con = $("#txtFecInicialConfirmacion").val();
    var fin_Con = $("#txtFecFinalConfirmacion").val();

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
    var TIPO = $("#ddlTipoCobro").val();
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteDepositoBanco/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, TipoCobro: TIPO
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
                    url = "../ReporteDepositoBanco/ReporteDepositoBanco?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                    + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con;

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
