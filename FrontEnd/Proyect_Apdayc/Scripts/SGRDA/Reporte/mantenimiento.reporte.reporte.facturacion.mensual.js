/************************** INICIO CARGA********************************************/
var INEI = 1
$(function () {

    loadComboTerritorio(0);
    Territorio: $("#ddlTerritorio").val();
    $("#hidCodigoUbigeo").val(0);
    $("#txtUbigeo").val(" ");
    //chekFechaCan  Disabled
    document.getElementById('chekFechaCan').checked = false;
    //chekFechaEmi disabled
    document.getElementById('chekFechaEmi').checked = false;
    //chekFechaCon disabled
    document.getElementById('chekFechaCon').checked = false;

    TipoTerritorioDesc: $("#ddlTerritorio option:selected").text();

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $('#txtFecInicialCancel').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinalCancel').kendoDatePicker({ format: "dd/MM/yyyy" });

    $('#txtFecIniConfirmacion').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinConfirmacion').kendoDatePicker({ format: "dd/MM/yyyy" });
    
    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $("#txtFecInicialCancel").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicialCancel").data("kendoDatePicker").value();

    $("#txtFecFinalCancel").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinalCancel").data("kendoDatePicker").value();

    $("#txtFecIniConfirmacion").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecIniConfirmacion").data("kendoDatePicker").value();

    $("#txtFecFinConfirmacion").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinConfirmacion").data("kendoDatePicker").value();
    ActivacionFecha();
    $('#TddRubro').hide();

    //Inicializar ubigeo
    $("#txtUbigeo").keypress(function () {
        var ubigeo = $(this).val()
        if (ubigeo == '') {
            $("#hidCodigoUbigeo").val(0);
        }
    });
    $('#divEstado').val(0);
    $('#divTipDoc').val(0);
    BuscarTipoDocumento();
    ListaEstado();
    //-------------------------- EVENTO BOTONES ---------------------

    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        obtenerModalidadesSeleccionadas();
        obtenerTipoDocumento();
        obtenerEstado();
        $("#btnPdf").attr("disabled", true);
        ExportarReportef('PDF');
        $("#btnPdf").attr("disabled", false);
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        obtenerModalidadesSeleccionadas();
        obtenerTipoDocumento();
        obtenerEstado();

        $("#btnExcel").attr("disabled", true);
        ExportarReportef2('EXCEL');
    });

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();

    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);

        if (oculta == true) {

        } else {
            $('#trRubro').hide();
        }
    }
    if (ocultacombo == 1) {
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
        loadTipoEnvio('dllTipoEnvio', '2');
        var dato = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    }
    else {
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);
        $('#TddRubro').hide();
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
    //    }
    //});
    $("#dllOficina").on("change", function () {
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
    //Carga ubigeo

    loadTipoDivision('ddlDivision', 'GEO', INEI);
    $("#ddlDivision").on("change", function () {
        var idDivision = $(this).val();
        loadSubTipoDivisiones(idDivision, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');
    });
    loadSubTipoDivisiones(INEI, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');


    $("#ddlSubTipo1").on("change", function () {
        var idDivision = $('#ddlDivision').val();
        var subtipo = $('#hidSubTipo2').val();
        var belog = $(this).val();
        loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo2', 0);
    });

    $("#ddlSubTipo2").on("change", function () {
        var idDivision = $('#ddlDivision').val();
        var subtipo = $('#hidSubTipo3').val();
        var belog = $(this).val();
        loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo3', 0);
    });
    $("#chekFechaEmi").on("change", function () {
        ActivacionFecha();
    });
    $("#chekFechaCan").on("change", function () {
        ActivacionFecha();
    });
    $("#chekFechaCon").on("change", function () {
        ActivacionFecha();
    });
});

function ActivacionFecha() {
    if ($('#chekFechaCan').is(':checked')) {
        $('#txtFecInicialCancel').data('kendoDatePicker').enable(true);
        $('#txtFecFinalCancel').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecInicialCancel').data('kendoDatePicker').enable(false);
        $('#txtFecFinalCancel').data('kendoDatePicker').enable(false);
    }
    if ($('#chekFechaEmi').is(':checked')) {
        $('#txtFecInicial').data('kendoDatePicker').enable(true);
        $('#txtFecFinal').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecInicial').data('kendoDatePicker').enable(false);
        $('#txtFecFinal').data('kendoDatePicker').enable(false);
    }
    if ($('#chekFechaCon').is(':checked')) {
        $('#txtFecIniConfirmacion').data('kendoDatePicker').enable(true);
        $('#txtFecFinConfirmacion').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecIniConfirmacion').data('kendoDatePicker').enable(false);
        $('#txtFecFinConfirmacion').data('kendoDatePicker').enable(false);
    }


}
function BuscarModalidadesXOficina(dato) {
    var soc = dato;
    $.ajax({
        url: '../ReporteResumenFacturacionMensual/ConsultaModalidadXOficina',
        type: 'POST',
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
                alert(dato.message);
            }
        }
    });

}

function BuscarTipoDocumento() {
   
    $.ajax({
        url: '../ReporteResumenFacturacionMensual/ListaTipoDocumentoRadioButton',
        type: 'POST',
        data: {

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridTipoDoc").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridTipoDoc").html('');
                alert(dato.message);
            }
        }
    });

}

function ListaEstado() {

    $.ajax({
        url: '../ReporteResumenFacturacionMensual/ListaEstadoRadioButton',
        type: 'POST',
        data: {

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridEstado").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridEstado").html('');
                alert(dato.message);
            }
        }
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
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',        
        url: '../ReporteResumenFacturacionMensual/ModalidadesSeleccionadasTemporalesOriginal',
        data: ReglaValor,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else if (dato.result == 0) {

                alert(dato.message);
            }
        }
    });
}

function obtenerTipoDocumento() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblTipoDocumento input[type=checkbox]').each(function () {
        if (this.checked) {
            var id = $(this).val();
            ReglaValor[contador] = { Value: id };
            contador += 1;
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',        
        url: '../ReporteResumenFacturacionMensual/ObtenerTipoDocSeleccionadas',
        data: ReglaValor,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else if (dato.result == 0) {

                alert(dato.message);
            }
        }
    });
}

function obtenerEstado() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblEstado input[type=checkbox]').each(function () {
        if (this.checked) {
            var id = $(this).val();
            ReglaValor[contador] = { Value: id };
            contador += 1;
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../ReporteResumenFacturacionMensual/ObtenerEstadoSeleccionadas',
        data: ReglaValor,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else if (dato.result == 0) {

                alert(dato.message);
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

function check2() {
    if ($('#checkALL2').prop('checked')) {
        BuscarTipoDocumento();
    } else {
        $(".Check2").removeAttr("checked");
    }
}

function check3() {
    if ($('#checkALL3').prop('checked')) {
        ListaEstado();
    } else {
        $(".Check3").removeAttr("checked");
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
    //Fecha Emision

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //fecha cancelacion
    var iniCan = $("#txtFecInicialCancel").val();
    var finCan = $("#txtFecFinalCancel  ").val();    
    // fecha confirmacion
    var iniCon = $("#txtFecIniConfirmacion").val();
    var finCon = $("#txtFecFinConfirmacion").val();

    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var validafecha = validate_fechaMayorQue(iniCan, finCan);
    var url = "";
    var DEPARTAMENTO = $("#ddlSubTipo1").val();
    var PROVINCIA = $("#ddlSubTipo2").val();
    var DISTRITO = $("#ddlSubTipo3").val();
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //var estado = $("#divEstado").val();
    //var TipoDoc = $("#divTipDoc").val();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val();

    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina); //oficina
    var vacio = $('input:radio[name=valida]:checked').val();

    var FechaEmi = 1;
    var FechaCan = 1;
    var FechaCon = 1;
    if ($("#chekFechaEmi").is(":checked")) {
        FechaEmi = 0;
    }
    if ($("#chekFechaCan").is(":checked")) {
        FechaCan = 0;
    }
    if ($("#chekFechaCon").is(":checked")) {
        FechaCon = 0;
    }
    if (FechaCan == 1 & FechaEmi == 1 & FechaCon == 1) {
        alert('Seleccione una fecha ')
        event.preventDefault();
    }


    //Si la validacion de fecha es Igual a 1 entonces :
    nombreoficina = nombreoficina.replace('&', 'Y');
    if (validafecha == 1) {
      

        $("#contenedor").show();

        $.ajax({
            url: '../ReporteResumenFacturacionMensual/ReporteTipo',
            type: 'POST',
            data: {
                               fini: ini, ffin: fin, finiCan: iniCan, ffinCan: finCan, formato: tipo, idoficina: idoficina, DEPARTAMENTO: DEPARTAMENTO,
                PROVINCIA: PROVINCIA, DISTRITO: DISTRITO, FechaEmi: FechaEmi, FechaCan: FechaCan, nombreoficina: nombreoficina
                , tipoenvio: 2, finiCon: iniCon, ffinCon: finCon, FechaCon: FechaCon
         
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteResumenFacturacionMensual/ReporteResumenFacturacionMensual?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                                                                   + "finiCan=" + iniCan + "&" + "ffinCan=" + finCan + "&"
                                                                   + "formato=" + tipo + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                                                                   + "&" + "FechaEmi=" + FechaEmi + "&" + "FechaCan=" + FechaCan + "&" + "FechaCon=" + FechaCon + "&"
                                                                   + "finiCon=" + iniCon + "&" + "ffinCon=" + finCon;


                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    //alert('No se encontro registro para este reporte')
                    //event.preventDefault();
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
    //Fecha Emision

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //fecha cancelacion
    var iniCan = $("#txtFecInicialCancel").val();
    var finCan = $("#txtFecFinalCancel").val();
    // fecha confirmacion
    var iniCon = $("#txtFecIniConfirmacion").val();
    var finCon = $("#txtFecFinConfirmacion").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var validafecha = validate_fechaMayorQue(iniCan, finCan);
    var url = "";
    var DEPARTAMENTO = $("#ddlSubTipo1").val();
    var PROVINCIA = $("#ddlSubTipo2").val();
    var DISTRITO = $("#ddlSubTipo3").val();
    var ocultacombo = validarOficinaReportedl();
    //var estado = $("#divEstado").val();
    //var TipoDoc = $("#divTipDoc").val();
    var oculta = validarOficinaReporte();
    var idoficina
    idoficina = $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();

    var idtipoenvio
    idtipoenvio = $("#dllTipoEnvio").val() == null ? 2 : $("#dllTipoEnvio").val();
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina); //oficina
    //if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
    //    rubro = vacio;
    //}
    var FechaEmi = 1;
    var FechaCan = 1;
    var FechaCon = 1;
    if ($("#chekFechaEmi").is(":checked")) {
        FechaEmi = 0;
    }
    if ($("#chekFechaCan").is(":checked")) {
        FechaCan = 0;
    }
    if ($("#chekFechaCon").is(":checked")) {
        FechaCon = 0;
    }
    if (FechaCan == 1 & FechaEmi == 1 & FechaCon == 1) {
        alert('Seleccione una fecha ')
        event.preventDefault();
    }


    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1) {

        $("#contenedor").show();
        $.ajax({
            url: '../ReporteResumenFacturacionMensual/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, finiCan: iniCan, ffinCan: finCan, formato: tipo, idoficina: idoficina, DEPARTAMENTO: DEPARTAMENTO,
                PROVINCIA: PROVINCIA, DISTRITO: DISTRITO, FechaEmi: FechaEmi, FechaCan: FechaCan, nombreoficina: nombreoficina
                , tipoenvio: idtipoenvio, finiCon: iniCon, ffinCon: finCon, FechaCon: FechaCon
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteResumenFacturacionMensual/ReporteResumenFacturacionMensual?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                                                 + "finiCan=" + iniCan + "&" + "ffinCan=" + finCan + "&"
                                                 + "formato=" + tipo + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                                                 + "&" + "FechaEmi=" + FechaEmi + "&" + "FechaCan=" + FechaCan + "&" + "FechaCon=" + FechaCon                    + "&"
                                                 + "finiCon=" + iniCon + "&" + "ffinCon=" + finCon;
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

    }
}