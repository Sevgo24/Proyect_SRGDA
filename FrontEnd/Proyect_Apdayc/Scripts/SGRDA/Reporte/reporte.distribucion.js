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

    obtenerUltimaActualizacion();
    loadTipoReporteDistribucion('ddlTipoReporte', '0', '0');

    
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        if (estadoRequeridos) {
            ExportarReporte('PDF');
            DeshabilitarBotnes(true);
        }
        else
            alert("Seleccione un tipo de reporte.");
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();        
        if (estadoRequeridos) {
            ExportarReporte('EXCEL');
            DeshabilitarBotnes(true);
        }
        else
            alert("Seleccione un tipo de reporte.");
    });


});

function DeshabilitarBotnes(estado) {
    $("#btnPdf").prop('disabled', estado);
    $("#btnExcel").prop('disabled', estado);

}
// PDF *******************************************
function ExportarReporte(tipo) {
    var tipoReporte = $("#ddlTipoReporte").val();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    var validafecha = validate_fechaMayorQue(ini, fin);
    var vacio = "";
    var url = "";

    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../ReporteDistribucion/ConusltarReporte',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, tipoReporte: tipoReporte
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../ReporteDistribucion/GenerarReporte?" +
                    "fini=" + ini + "&" +
                    "ffin=" + fin +
                    "&" + "formato=" + tipo +
                    "&" + "tipo=" + tipo +
                    "&" + "tipoReporte=" + tipoReporte;

                    if (tipo == 'PDF') {
                        $("#contenedor").show();
                        $('#externo').attr("src", url);                        
                    } else if (tipo == 'EXCEL') {
                        $("#contenedor").hide();
                        window.open(url);
                    }
                    DeshabilitarBotnes(false);
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                    DeshabilitarBotnes(false);
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


