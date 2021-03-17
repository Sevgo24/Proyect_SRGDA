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

    //-------------------------- EVENTO BOTONES ---------------------
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        $("#contenedor").show();
        $('#externo').attr("src", ExportarReportef('PDF'));
        //  ExportarReportef('PDF');
    });
    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });

});

function ExportarReportef(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();


    var url = "../ReporteComprobantexAutor/ReporteComprobantexAutor?" +
      "fini=" + ini + "&" +
      "ffin=" + fin + "&" +
        "formato=" + tipo;

    $.ajax({
        url: '../ReporteComprobantexAutor/ReporteTipo',
        type: 'POST',
        data: { fini: ini, ffin: fin },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                url;
                $("#contenedor").show();
            } else if (dato.result == 0) {
                $("#contenedor").hide();
                url = alert(dato.message);
            }
        }
    });



    //  var poPup = '';
    //  poPup = window.open(url, "Registro de Ventas", "menubar=false,resizable=false,width=750,height=550");

    //   alert(poPup);
    return url;
}


function ExportarReportef2(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();


    var url = "../ReporteComprobantexAutor/ReporteComprobantexAutor?" +
      "fini=" + ini + "&" +
      "ffin=" + fin + "&" +
        "formato=" + tipo;


    $.ajax({
        url: '../ReporteComprobantexAutor/ReporteTipo',
        type: 'POST',
        data: { fini: ini, ffin: fin },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                window.open(url);;
                $("#contenedor").show();
            } else if (dato.result == 0) {
                $("#contenedor").hide();
                url = alert(dato.message);
            }
        }
    });

    // window.open(url);
}