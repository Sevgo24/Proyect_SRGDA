/************************** INICIO CARGA********************************************/
var INEI = 1
var artista = "";
$(function () {

    //chekFechaAut disabled
    //document.getElementById('chekFechaAut').checked = false;


    //chekFechaCan  Disabled
    document.getElementById('chekFechaEvento').checked = false;
    //chekFechaEmi disabled
    document.getElementById('chekFechaEmi').checked = false;
    //chekFechaCon disabled
    document.getElementById('chekFechaCan').checked = false;
    //chekFechaCon disabled
    document.getElementById('chekFechaCon').checked = false;

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();
    //   Fecha Emision
    $('#txtFecEmiIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecEmiFin').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecEmiIni").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecEmiIni").data("kendoDatePicker").value();

    $("#txtFecEmiFin").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecEmiFin").data("kendoDatePicker").value();
    //   Fecha Cancelacion
    $('#txtFecCanIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCanFin').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecCanIni").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecCanIni").data("kendoDatePicker").value();

    $("#txtFecCanFin").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecCanFin").data("kendoDatePicker").value();
    //   Fecha Cancelacion
    $('#txtFecConIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecConFin').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecConIni").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecConIni").data("kendoDatePicker").value();

    $("#txtFecConFin").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecConFin").data("kendoDatePicker").value();

    ActivacionFecha();

    //-------------------------- EVENTO BOTONES ---------------------

    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //obtenerModalidadesSeleccionadas();
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //obtenerModalidadesSeleccionadas();
        ExportarReportef2('EXCEL');
    });

    $("#btnExcelFiltro").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //obtenerModalidadesSeleccionadas();
        ExportarReportef2('EXCEL_FILTRO');
    });

    $("#btnBuscar").on("click", function () {        
        ListarArtistas();
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
    }
    else {
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        //BuscarModalidadesXOficina(dato);
    }

    $("#chekFechaEvento").on("change", function () {
        ActivacionFecha();
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
    //mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });
    //loadComboOficina('s10', '0');
        

});


function ActivacionFecha() {
    if ($('#chekFechaEvento').is(':checked')) {
        $('#txtFecInicial').data('kendoDatePicker').enable(true);
        $('#txtFecFinal').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecInicial').data('kendoDatePicker').enable(false);
        $('#txtFecFinal').data('kendoDatePicker').enable(false);
    }       
    if ($('#chekFechaEmi').is(':checked')) {
        $('#txtFecEmiIni').data('kendoDatePicker').enable(true);
        $('#txtFecEmiFin').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecEmiIni').data('kendoDatePicker').enable(false);
        $('#txtFecEmiFin').data('kendoDatePicker').enable(false);
    }  
    if ($('#chekFechaCan').is(':checked')) {
        $('#txtFecCanIni').data('kendoDatePicker').enable(true);
        $('#txtFecCanFin').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecCanIni').data('kendoDatePicker').enable(false);
        $('#txtFecCanFin').data('kendoDatePicker').enable(false);
    } 
    if ($('#chekFechaCon').is(':checked')) {
        $('#txtFecConIni').data('kendoDatePicker').enable(true);
        $('#txtFecConFin').data('kendoDatePicker').enable(true);
    } else {
        $('#txtFecConIni').data('kendoDatePicker').enable(false);
        $('#txtFecConFin').data('kendoDatePicker').enable(false);
    }


}

function ExportarReportef(tipo) {
    //Fecha Emision
    var femi_ini = $("#txtFecEmiIni").val();
    var femi_fin = $("#txtFecEmiFin").val();
    var feve_ini = $("#txtFecInicial").val();
    var feve_fin = $("#txtFecFinal").val();
    var fcan_ini = $("#txtFecCanIni").val();
    var fcan_fin = $("#txtFecCanFin").val();
    var fcon_ini = $("#txtFecConIni").val();
    var fcon_fin = $("#txtFecConFin").val();
    //var artista = $("#txtArtista").val();

    var validafechaEmi = validate_fechaMayorQue(femi_ini, femi_fin);
    var validafechaEvento = validate_fechaMayorQue(feve_ini, feve_fin);
    var validafechaCancelacion = validate_fechaMayorQue(fcan_ini, fcan_fin);
    var validafechaConfirmacion = validate_fechaMayorQue(fcon_ini, fcon_fin);

    var con_Emision = $('#chekFechaEmi').is(':checked') == true ? 1 : 0;
    var con_Evento = $('#chekFechaEvento').is(':checked') == true ? 1 : 0;
    var con_Cancelacion = $('#chekFechaCan').is(':checked') == true ? 1 : 0;
    var con_Contable = $('#chekFechaCon').is(':checked') == true ? 1 : 0;

    var url = "";
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
    //var vacio = $('input:radio[name=valida]:checked').val();

    //Si la validacion de fecha es Igual a 1 entonces :
    nombreoficina = nombreoficina.replace('&', 'Y');
    if (validafechaEmi == 1 || validafechaEvento == 1 || validafechaCancelacion == 1 || validafechaConfirmacion == 1) {
        //alert('pdf2')
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteArtistaDetallado/ReporteTipo',
            type: 'POST',
            data: {
                femi_ini: femi_ini, femi_fin: femi_fin, feve_ini: feve_ini, feve_fin: feve_fin, fcan_ini: fcan_ini, fcan_fin: fcan_fin
                , fcon_ini: fcon_ini, fcon_fin: fcon_fin, artista: artista, formato: tipo, con_Emision: con_Emision, con_Evento: con_Evento
                , con_Cancelacion: con_Cancelacion, con_Contable: con_Contable
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteArtistaDetallado/ReporteArtistaDetallado?" +
                    "femi_ini=" + femi_ini + "&" +
                    "femi_fin=" + femi_fin + "&" +
                    "feve_ini=" + feve_ini + "&" +
                    "feve_fin=" + feve_fin + "&" +
                    "fcan_ini=" + fcan_ini + "&" +
                    "fcan_fin=" + fcan_fin + "&" +
                    "fcon_ini=" + fcon_ini + "&" +
                    "fcon_fin=" + fcon_fin + "&" +
                    "artista=" + artista + "&" +
                    "formato=" + tipo + "&" +
                    "con_Emision=" + con_Emision + "&" +
                    "con_Evento=" + con_Evento + "&" +
                    "con_Cancelacion=" + con_Cancelacion + "&" +
                    "con_Contable=" + con_Contable;
                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    //alert('No se encontro registro para este reporte')
                    //event.preventDefault();
                    //$('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {
        $("#contenedor").hide();
    }

}
//var reloadEventoArt = function (idArtSel) {
//    //msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
//    $("#hidArtistaSel").val(idArtSel);
//    obtenerNombreArtista(idArtSel, "ArtistaTxt");
//};
function ExportarReportef2(tipo) {
    //Fecha Emision
    var femi_ini = $("#txtFecEmiIni").val();
    var femi_fin = $("#txtFecEmiFin").val();
    var feve_ini = $("#txtFecInicial").val();
    var feve_fin = $("#txtFecFinal").val();
    var fcan_ini = $("#txtFecCanIni").val();
    var fcan_fin = $("#txtFecCanFin").val();
    var fcon_ini = $("#txtFecConIni").val();
    var fcon_fin = $("#txtFecConFin").val();
    //var artista = $("#txtArtista").val();

    var validafechaEmi = validate_fechaMayorQue(femi_ini, femi_fin);
    var validafechaEvento = validate_fechaMayorQue(feve_ini, feve_fin);
    var validafechaCancelacion = validate_fechaMayorQue(fcan_ini, fcan_fin);
    var validafechaConfirmacion = validate_fechaMayorQue(fcon_ini, fcon_fin);

    var con_Emision = $('#chekFechaEmi').is(':checked') == true ? 1 : 0;
    var con_Evento = $('#chekFechaEvento').is(':checked') == true ? 1 : 0;
    var con_Cancelacion = $('#chekFechaCan').is(':checked') == true ? 1 : 0;
    var con_Contable = $('#chekFechaCon').is(':checked') == true ? 1 : 0;

    var url = "";

    //var artista1 = $("#ArtistaTxt").text;
    var ocultacombo = validarOficinaReportedl();

    var oculta = validarOficinaReporte();
    var idoficina
    idoficina = $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();

    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :

    if (validafechaEmi == 1 || validafechaEvento == 1 || validafechaCancelacion == 1 || validafechaConfirmacion == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteArtistaDetallado/ReporteTipo',
            type: 'POST',
            data: {
                femi_ini: femi_ini, femi_fin: femi_fin, feve_ini: feve_ini, feve_fin: feve_fin, fcan_ini: fcan_ini, fcan_fin: fcan_fin
                , fcon_ini: fcon_ini, fcon_fin: fcon_fin, artista: artista, formato: tipo, con_Emision: con_Emision, con_Evento: con_Evento
                , con_Cancelacion: con_Cancelacion, con_Contable: con_Contable
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteArtistaDetallado/ReporteArtistaDetallado?" +
                    "femi_ini=" + femi_ini + "&" +
                    "femi_fin=" + femi_fin + "&" +
                    "feve_ini=" + feve_ini + "&" +
                    "feve_fin=" + feve_fin + "&" +
                    "fcan_ini=" + fcan_ini + "&" +
                    "fcan_fin=" + fcan_fin + "&" +
                    "fcon_ini=" + fcon_ini + "&" +
                    "fcon_fin=" + fcon_fin + "&" +
                    "artista=" + artista + "&" +
                    "formato=" + tipo + "&" +
                    "con_Emision=" + con_Emision + "&" +
                    "con_Evento=" + con_Evento + "&" +
                    "con_Cancelacion=" + con_Cancelacion + "&" +
                    "con_Contable=" + con_Contable;

                    window.open(url);
                    $("#contenedor").hide();
                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }

        });
    }

}

function loadArtista(control, valSel) {
    var Artista = $("#txtArtista").val();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text:'TODOS' }));
    $.ajax({
        url: '../ReporteArtistaDetallado/GetCiudades',
        data: { Artista: Artista },
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

       
function ListarArtistas() {
    loadArtista('cmbCiudad', '0');

    if ($("#cmbCiudad").data("ui-dropdownchecklist")) {
        $("#cmbCiudad").dropdownchecklist("destroy");
    }

    $("#cmbCiudad").dropdownchecklist({
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
            artista = values;
            //alert(values);
        }
    });
}

