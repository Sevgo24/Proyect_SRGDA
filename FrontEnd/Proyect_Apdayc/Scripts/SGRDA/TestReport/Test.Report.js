$(function () {
    $("#btnBuscarReporte1").on("click", function () {
        CrearReporte(1);
    });
    $("#btnBuscarReporte2").on("click", function () {
        
    });
    $("#btnBuscarReporte3").on("click", function () {
        CrearReporte(3);
    });
    $("#btnBuscarReporte4").on("click", function () {
        CrearReporte(4);
    });
    $("#btnBuscarReporte5").on("click", function () {
        CrearReporte(5);
    });
    $("#btnBuscarReporte6").on("click", function () {
        CrearReporte(6);
    });

    //$("#mvPDF").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvPDF").dialog("close"); } }, modal: true });
});

function MostrarReporte(op) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TestReport/ActivaReporte',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //var url = "/ComisionPreLiquidar/DownloadReportLiquidadas";
                //window.open(url);
                var id = dato.valor;
                var estado = dato.valor;

                if (op == 1) {
                    abrir_ventana('../Formatos/ReporteContratoBailesEspectaculos/?idObj=' + id, estado, 650, 500);
                }
                else if (op == 2) {
                    abrir_ventana('../Formatos/ReporteContratoLocalesPermanentes/?idObj=' + id, estado, 650, 500);
                }
                else if (op == 3) {
                    abrir_ventana('../Formatos/ReporteFichaInscripcion/?idObj=' + id, estado, 650, 500);
                }
                else if (op == 4) {
                    abrir_ventana('../Formatos/ReporteWord', estado, 650, 500);
                }
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function CrearReporte(op) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TestReport/ActivaReporte/',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var id = dato.valor;
                var estado = dato.valor;

                if (op == 1) {
                    var idRepot = "RBE";
                    abrir_ventana('../Formatos/DisplayPDF/?idObj=' + id + '&idReport=' + idRepot, estado, 650, 500);
                }

                if (op == 3) {
                    var idRepot = "RFS";
                    abrir_ventana('../Formatos/DisplayPDF/?idObj=' + id + '&idReport=' + idRepot, estado, 650, 500);
                }

                if (op == 4) {
                    var idRepot = "CIVOM";
                    abrir_ventana('../Formatos/DisplayPDF/?idObj=' + id + '&idReport=' + idRepot, estado, 650, 500);
                }

                if (op == 5) {
                    var idRepot = "CRUOM";
                    abrir_ventana('../Formatos/DisplayPDF/?idObj=' + id + '&idReport=' + idRepot, estado, 650, 500);
                }

                if (op == 6) {
                    var idRepot = "FLI";
                    abrir_ventana('../Formatos/DisplayPDF/?idObj=' + id + '&idReport=' + idRepot, estado, 650, 500);
                }
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

//$(document).ready(function () {
//    $('#mvPDF')
//      .dialog({
//          autoOpen: true,
//          width: 600,
//          height: 400,
//          position: 'center',
//          resizable: true,
//          draggable: true
//      });
//});

//function verPDF(url) {
//    $("#mvPDF").dialog("open");
//    //$("#ifContenedor").attr("src", url);
//    return false;
//}


