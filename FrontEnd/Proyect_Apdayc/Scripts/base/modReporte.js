$(function () {
    $("#btnReporte").click(function () {
        var busqueda = $("#txtBusqueda").val();
        $.getJSON("/AdmReportes/obtenerReporte", { valorBusqueda: busqueda },
            function (resultado) {
                $("#spReporte").html(resultado);
            });
    })
});