/************************** INICIO CARGA********************************************/
var INEI = 1
$(function () {

    //chekFechaAut disabled
    //document.getElementById('chekFechaAut').checked = false;
    

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });


    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    //ActivacionFecha();

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
        BuscarModalidadesXOficina(dato);
    }
    //mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });

});


function ExportarReportef(tipo) {
    //Fecha Emision
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    ////fecha cancelacion
    //var iniCan = $("#txtFecInicialCancel").val();
    //var finCan = $("#txtFecFinalCancel").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    //var validafecha = validate_fechaMayorQue(iniCan, finCan);
    var url = "";
    var artista = $('#txtArtista').val();
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
    if (validafecha == 1) {
        $("#contenedor").show();

        $.ajax({
            url: '../ReporteArtista/ReporteTipo',
            type: 'POST',
            data:   {
                        artista:artista, fini: ini, ffin: fin, formato: tipo
                    },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../ReporteArtista/ReporteArtista?" + "artista=" + artista + "&"
                                  + "fini=" + ini + "&" + "ffin=" + fin + "&"
                                  + "formato=" + tipo ;


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
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //fecha cancelacion
   
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    //var validafecha = validate_fechaMayorQue(iniCan, finCan);
    var url = "";
    var artista = $('#txtArtista').val();

    //var artista1 = $("#ArtistaTxt").text;
    var ocultacombo = validarOficinaReportedl();

    var oculta = validarOficinaReporte();
    var idoficina
    idoficina = $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina); //oficina
    //if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
    //    rubro = vacio;
    //}

    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :

    $("#contenedor").show();
    $.ajax({
        url: '../ReporteArtista/ReporteTipo',
        type: 'POST',
        data: {
            artista:artista, fini: ini, ffin: fin, formato: tipo
        },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                url = "../ReporteArtista/ReporteArtista?" + "artista=" + artista + "&"
                                  + "fini=" + ini + "&" + "ffin=" + fin + "&"
                                  + "formato=" + tipo
                window.open(url);
                $("#contenedor").hide();
            } else if (dato.result == 0) {
                $("#contenedor").hide();
                url = alert(dato.message);
            }
        }
    });

}