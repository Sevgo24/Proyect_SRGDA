/************************** INICIO CARGA********************************************/

var ModalidadDetalle = "";
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
    $('#TddRubro').hide();

    //-------------------------- EVENTO BOTONES ---------------------

    //$('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        obtenerModalidadesSeleccionadas();
        $("#btnPdf").attr("disabled", true);
        ExportarReportef('PDF');
        $("#btnPdf").attr("disabled", false);
       
    });
   
    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        obtenerModalidadesSeleccionadas();
        $("#btnExcel").attr("disabled", true);
        ExportarReportef2('EXCEL');
        
       
    });

    $("#btnBuscar").on("click", function () {
        ListarArtistas();
    });
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    if (ocultacombo == false) {
        $('.tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);

        // lista tipo de envio manual 0 electronico
        $('.tddllTipoEnvio').hide();
        $('#tddllTipoEnvio2').hide();
        $('#dllTipoEnvio').prop('disabled', true);
        //

        //if (oculta == true) {
        //    $('#trRubro').show();
        //    //$("input[name=valida]").prop('enabled',true);
        //} else {
        //    $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        //}
    }
    //Si ocultacombo sera =1 Siempre que el usuario que ingreso sea Admin o Contabilidad
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
        $('#tddllOficina2').show();
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
        obtenerModalidadesSeleccionadas();
        ListarModalidadDet();
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
        obtenerModalidadesSeleccionadas();
        ListarModalidadDet();

        //loadTipoEnvio('dllTipoEnvio', '2');
        //$('#tddllTipoEnvio').prop('enabled', true);
        //$('#tddllTipoEnvio').show();
        //$('#tddllTipoEnvio2').show();
    }

    $("#dllOficina").on("change", function () {
        var dato = $("#dllOficina").val();
        $('#TddRubro').hide();
        BuscarModalidadesXOficina(dato);
        obtenerModalidadesSeleccionadas();
        ListarModalidadDet();
    });
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



function obtenerModalidadesSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;

    if ($('#checkALL').prop('checked')) {
        ReglaValor[contador]={MOG_ID:'0'};
    } else {
        $('#tblModalidadXOficina input[type=checkbox]').each(function () {
            if (this.checked) {
                var id = $(this).val();
                ReglaValor[contador] = { MOG_ID: id };
                contador += 1;
            }
        });
    }
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    //if (contador > 0) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        async:false,
        url: '../ReporteFacturaCancelada/ModalidadesSeleccionadasTemporalesOriginal',
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
}

function ListaModalidadOficina(dato){
    var soc = dato;
    //alert('Lista');
    $.ajax({
        url: '../ReporteFacturaCancelada/ListaModalidadXOficina',
        type: 'POST',
        async: false,
        data: {
            IdOficina: soc
        },
        beforeSend: function () { },
        success: function (response) {
            var dato2 = response.result;
            DesmarcarALL_ResetearLista(dato2);
        }
    });
}

function BuscarModalidadesXOficina(dato) {

    var soc = dato;

    $.ajax({
        url: '../ReporteFacturaCancelada/ConsultaModalidadXOficina',
        type: 'POST',
        async:false,
        data: {
            IdOficina: soc

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#TddRubro').show();
                $("#gridModalidad").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridModalidad").html('');
                //alert(dato.message);
            }
        }
    });

    //alert(soc);
    // }

}
//function check1(checkbox) {
//    if (checkbox.checked) {
//        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
//        BuscarModalidadesXOficina(dato);
//    }   
//}

function check1() {
    if ($('#checkALL').prop('checked')) {
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
        obtenerModalidadesSeleccionadas();
        ListarModalidadDet();
    } else {
        $(".Check").removeAttr("checked");
        obtenerModalidadesSeleccionadas();
        ListarModalidadDet();
    }
}

function DesmarcarALL_ResetearLista(count) {
    for (var i = 1; i <= count; i++) {
        if ($('#checkValue' + i + '').prop('checked')) {
            //var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
            //BuscarModalidadesXOficina(dato);
            obtenerModalidadesSeleccionadas();
            ListarModalidadDet();
        }
        else {
            obtenerModalidadesSeleccionadas();
            ListarModalidadDet();
        }
        if ($('#checkValue' + i + '').prop('checked') == false) {
            document.getElementById("checkALL").checked = false;
        }
    }
}
function checkValue() {
    var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    ListaModalidadOficina(dato);  
   
    }    



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
    idoficina = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
    var idtipoenvio
    idtipoenvio = $("#dllTipoEnvio").val() == null ? 2 : $("#dllTipoEnvio").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    //Si la validacion de fecha es Igual a 1 entonces :
    nombreoficina = nombreoficina.replace('&', 'Y');
    //&& validaselecciondelCombo == 1
    if (validafecha == 1 ) {
        $("#contenedor").show();

        
        $.ajax({
            url: '../ReporteFacturaCancelada/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, formato: tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, tipoenvio: 2
                ,ModalidadDetalle:ModalidadDetalle
            },
            beforeSend: function () {
                
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //url = "../ReporteFacturaCancelada/ReporteFacturaCancelada?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                    //    + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    url = "../ReporteFacturaCancelada/ReporteFacturaCancelada?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                        + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con
                        + "&ModalidadDetalle=" + ModalidadDetalle;

                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                    
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
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
    //Si la validacion de fecha es Igual a 1 entonces :
    //|| validaselecciondelCombo != 1
    if (validafecha != 1 ) {
        $("#btnExcel").attr("disabled", false);
    }
    //&& validaselecciondelCombo == 1
    if (validafecha == 1 ) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteFacturaCancelada/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, formato: tipo, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,
                conFechaIngreso: con_ingreso, conFechaConfirmacion: con_confirmacion, finiConfirmacion: ini_Con, ffinConfirmacion: fin_Con, tipoenvio: idtipoenvio
                ,ModalidadDetalle: ModalidadDetalle
            },
            //beforeSend: function () {
            //    var load = '../Images/otros/loading.GIF';
            //    $('#externo').attr("src", load);
            //},
            success: function (response) {
                var dato = response;
               
                validarRedirect(dato);
                if (dato.result == 1) {
                    //url = "../ReporteFacturaCancelada/ReporteFacturaCancelada?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                    //    + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
                    url = "../ReporteFacturaCancelada/ReporteFacturaCancelada?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                        + "&conIng=" + con_ingreso + "&conCon=" + con_confirmacion + "&finiCon=" + ini_Con + "&ffinCon=" + fin_Con
                        + "&ModalidadDetalle=" + ModalidadDetalle;
                    window.open(url);
                    $("#btnExcel").attr("disabled", false);
                    $("#contenedor").hide();
                    //$("#contenedor").show();
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

function loadModalidadDet(control, valSel) {
    //var Artista = $("#txtArtista").val();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: 'TODOS' }));
    $.ajax({
        url: '../ReporteFacturaCancelada/GetModalidad',
        data: {},
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function ListarModalidadDet(){
    loadModalidadDet('cmbModalidad', '0');

    if ($("#cmbModalidad").data("ui-dropdownchecklist")) {
        $("#cmbModalidad").dropdownchecklist("destroy");
    }
    $("#cmbModalidad").dropdownchecklist({
        firstItemChecksAll: true,
        explicitClose: '...close',
        width: 250,
        forceMultiple: true, onComplete: function (selector) {
            var values = "";
            for (i = 0; i < selector.options.length; i++) {
                if (selector.options[i].selected && (selector.options[i].value != "") && (selector.options[i].value != "0")) {
                    if (values != "" && values != "0") values += ",";
                    values += selector.options[i].value;
                }
            }
            ModalidadDetalle = values;
            //alert(values);
        }
    });
}

