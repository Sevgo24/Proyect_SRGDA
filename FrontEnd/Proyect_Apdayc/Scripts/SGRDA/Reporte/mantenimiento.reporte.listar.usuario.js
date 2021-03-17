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
        //$('#externo').attr("src", ExportarReportef('PDF'));
          ExportarReportef('PDF');
    });
    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });


    ////prueba
    //$('#ddtipo').change(function () {
    //    var selectedValue = $(this).val();
    //    alert(selectedValue);
    //    if (selectedValue === 'Ra') {
    //        var usu = $("#txtSociedad").val();
    //        var num = "";
    //    } else if (selectedValue === 'Ru') {
    //        var usu = "";
    //        var num = $("#txtSociedad").val();

    //    }
    //    //else {
    //    //    var usu = "";
    //    //    var num = "";
    //    //}
    //});

});

function ExportarReportef(tipo) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var usu = "";
    var num = "";

    var selectedValue = $('#ddtipo').val();
    if (selectedValue === 'Ra') {
        var usu = $("#txtSociedad").val();
        var num = "";
    } else if (selectedValue === 'Ru') {
        var usu = "";
        var num = $("#txtSociedad").val();
    }
    //
    var vacio = "";
    var url = "";

    $.ajax({
        url: '../ReporteListarUsuarios/ReporteTipo',
        type: 'POST',
        data: { fini: ini, ffin: fin,usu:usu,numero:num },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                url = "../ReporteListarUsuarios/ReporteListarusuarios?" +
      "fini=" + ini + "&" +
      "ffin=" + fin + "&" +
      "usu=" + usu + "&" +
      "numero=" + num + "&" +
        "formato=" + tipo;
                $("#contenedor").show();
                $('#externo').attr("src", url);
            } else if (dato.result == 0) {
                $('#externo').attr("src", vacio);
                $("#contenedor").hide();
                url = alert(dato.message);
            }
        }
    });



    //  var poPup = '';
    //  poPup = window.open(url, "Registro de Ventas", "menubar=false,resizable=false,width=750,height=550");

    //   alert(poPup);
    //return url;
}


function ExportarReportef2(tipo) {

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var usu = "";
    var num = "";

    var selectedValue = $('#ddtipo').val();
    if (selectedValue === 'Ra') {
        var usu = $("#txtSociedad").val();
        var num = "";
    } else if (selectedValue === 'Ru') {
        var usu = "";
        var num = $("#txtSociedad").val();
    }
    //

    var url = "../ReporteListarUsuarios/ReporteListarusuarios?" +
      "fini=" + ini + "&" +
      "ffin=" + fin + "&" +
      "usu=" + usu + "&" +
      "numero=" + num + "&" +
        "formato=" + tipo;


    $.ajax({
        url: '../ReporteListarUsuarios/ReporteTipo',
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