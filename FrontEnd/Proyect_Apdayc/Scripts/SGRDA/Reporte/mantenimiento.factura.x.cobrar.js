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

    //$("input[name=valida]").hide();

    //-------------------------- EVENTO BOTONES ---------------------

    //$('#trRubro').hide();

    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        obtenerModalidadesSeleccionadas();
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        obtenerModalidadesSeleccionadas();
        $("#btnExcel").attr("disabled", true);
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
        $('#dllOficina').prop('disabled', true);
        // lista tipo de envio manual 0 electronico
        $('.tddllTipoEnvio').hide();
        $('#tddllTipoEnvio2').hide(); 
        $('#dllTipoEnvio').prop('disabled', true);
        
        //if (oculta == true) {
        //    $('#trRubro').show();
        //    //$("input[name=valida]").prop('enabled',true);
        //} else {
        //    $('#trRubro').hide();
        //    //$("input[name=valida]").prop('disabled', true);

        //}

    }
    //Si ocultacombo sera =1 Siempre que el usuario que ingreso sea Admin o Contabilidad
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();

        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
        //ListaTipoEnvio
        loadTipoEnvio('dllTipoEnvio', '2');
        $('#tddllTipoEnvio').prop('enabled', true);
        $('#tddllTipoEnvio').show();
        $('#tddllTipoEnvio2').show();
    }
    else {
        //deshabilita el Select
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    }

    //$("#dllOficina").change(function () {
    //    if ($(this).val() == "26") {
    //        if (ocultacombo == 1) {
    //            $('#dllOficina').show();
    //            $('#trRubro').show();
    //        }
    //    } else {
    //        $('#trRubro').hide();
    //        //$("input[name=valida]").prop('disabled', true);
    //    }
    //});

    $("#dllOficina").on("change", function () {
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
        
    });

    //check1();

});

// ** GRUPOS DE MODALIDAD ** //
//function BuscarModalidadesXOficina(dato) {
//    alert(dato);
//    var soc = dato;

//    $.ajax({
//        url: '../ReporteFacturaxCobrar/ConsultaModalidadXOficina',
//        type: 'POST',
//        data: {
//            IdOficina: soc

//        },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                $("#gridModalidad").html(dato.message);
//            } else if (dato.result == 0) {
//                $("#gridModalidad").html('');
//                alert(dato.message);
//            }
//        }
//    });

//}


function BuscarModalidadesXOficina(dato) {
    var soc = dato;
    $.ajax({
        url: '../ReporteFacturaxCobrar/ConsultaModalidadXOficina',
        type: 'POST',
        data: {
            IdOficina: soc
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //$('#TddRubro').show();
                $("#gridModalidad").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridModalidad").html('');
                //alert(dato.message);
            }
        }
    });
}

function check1() {
    if ($('#checkALL').prop('checked')) {
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    } else {
        $(".Check").removeAttr("checked");
    }
}
function checkValue() {
    for (var i = 0; i < 30; i++) {
        //if ($('#checkValue' + i + '').prop('checked')) {
        //    obtenerModalidadesSeleccionadas();
        //    ListarModalidadDet();
        //} else {
        //    obtenerModalidadesSeleccionadas();
        //    ListarModalidadDet();
        //}
        if ($('#checkValue' + i + '').prop('checked') == false) {
            document.getElementById("checkALL").checked = false;
        }
    }
}
// ** ****************************


function loadTipoEnvio(control, valSel) {
    var k_TipoEnvio = [{ Text: '--SELECCIONE--', Value: 2 },
                              { Text: 'ELECTRONICO', Value: 0 },
                              { Text: 'MANUAL', Value: 1 }
    ];
    $('#' + control + ' option').remove();
    $.each(k_TipoEnvio, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function obtenerModalidadesSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblModalidadXOficina input[type=checkbox]').each(function () {
        if (this.checked) {
            var id = $(this).val();
            ReglaValor[contador] = { MOG_ID: id };
            contador += 1;
        }
    });
    //alert(ReglaValor)
    //$('#tblModalidadXOficina tr').each(function () {
    //        var IdNro = $(this).find(".IDModOri").html();
    //        var idEst = $(this).find(".IDModOri").html();     
    //        if (IdNro != null) {              
    //            if ($('#chkModOrigen' + idEst).is(':checked')) {
    //                ReglaValor[contador] = {
    //                    MOG_ID: idEst,
    //                };
    //                contador += 1;
    //            }
    //        }
    //    //});
    //});
    //alert(contador);

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    //if (contador > 0) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ReporteFacturaxCobrar/ModalidadesSeleccionadasTemporalesOriginal',
        data: ReglaValor,
        //codigo malo..-:"
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else if (dato.result == 0) {

                alert(dato.message);
            }
        }
    });
    //} else {
    //    alert("Debe selecionar antes de continuar.");
    //}
}



function ExportarReportef(tipo) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini,fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    //idoficina = $("#dllOficina").val();
    idoficina = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();

    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    //&& validaselecciondelCombo == 1
    if (validafecha == 1 ) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteFacturaxCobrar/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin, formato: tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina, tipoenvio:2 },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteFacturaxCobrar/ReporteFacturaPendiente?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
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
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini,fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    //idoficina = $("#dllOficina").val();
    idoficina = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
    var idtipoenvio
    idtipoenvio = $("#dllTipoEnvio").val() == null ? 2 : $("#dllTipoEnvio").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    nombreoficina = nombreoficina.replace('&', 'Y');
    //|| validaselecciondelCombo != 1
    if (validafecha != 1 ) {
        $("#btnExcel").attr("disabled", false);
    }
    //Si la validacion de fecha es Igual a 1 entonces :
    //&& validaselecciondelCombo == 1
    if (validafecha == 1 ) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteFacturaxCobrar/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin, formato: tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina, tipoenvio: idtipoenvio },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteFacturaxCobrar/ReporteFacturaPendiente?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    window.open(url);
                    $("#btnExcel").attr("disabled", false);
                    $("#contenedor").hide();
                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    url = alert(dato.message);
                    $("#btnExcel").attr("disabled", false);

                }
            }
        });
    } else {
        $("#contenedor").hide();
    }








}