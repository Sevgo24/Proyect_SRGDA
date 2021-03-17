$(function () {
    $("#txtOficinaActual").attr("disabled", "disabled");

    loadComboNivelAgente('ddlNivelAgente', 0);
    loadComboOficinaDestino('ddlOficinaDestino', 0)

    var id = GetQueryStringParams("id");
    var idAgente = GetQueryStringParams("idAgente");
    $("#hidCodigoOFI").val(id);
    ObtenerDatos(id, idAgente);

    //$("#txtFechaInicio").datepicker({ dateFormat: 'dd/mm/yy' });
    //$("#txtFechaFin").datepicker({ dateFormat: 'dd/mm/yy' });

    $("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TrasladoAgentesRecaudo/";
    });
   
});


function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function ObtenerDatos(idSel, id2) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: 'Obtiene',
        data: { id: idSel, idAgente: id2 },
        type: 'POST',
        success: function (response) {
            var dato = response;

            if (dato.result == 1) {
                var traslado = dato.data.Data;
                if (traslado != null) {
                    $("#txtOficinaActual").val(traslado.Oficina);
                    $("#ddlOficinaDestino").val(traslado.Codigo);
                    $("#hidCodigoBPS").val(traslado.CodigoSocio);
                    $("#ddlNivelAgente").val(traslado.Level);
                    //$("#txtFechaInicio").val(traslado.FechaInicio);
                    //$("#txtFechaFin").val(traslado.FechaFin);

                    var datepicker1 = $("#txtFechaInicio").data("kendoDatePicker");
                    datepicker1.value(traslado.FechaInicio);

                    var datepicker2 = $("#txtFechaFin").data("kendoDatePicker");
                    datepicker2.value(traslado.FechaFin);
                }
            } else {
                alert(dato.message);
            }
        }
    });
}


function grabar() {
    if (ValidarRequeridos()) {
        var idOfi = $("#hidCodigoOFI").val();
        var oficinaDestino = $("#ddlOficinaDestino").val();
        var fechainicio = $("#txtFechaInicio").val();
        var fechafin = $("#txtFechaFin").val();
        var nivel = $("#ddlNivelAgente").val();
        var oficina = {
            OFF_ID: idOfi,
            BPS_ID: $("#hidCodigoBPS").val(),
            OFF_IDAux: oficinaDestino,
            LEVEL_ID: nivel,
            START: fechainicio,
            ENDS: fechafin
        };
        $.ajax({
            url: 'Actualizar',
            data: oficina,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TrasladoAgentesRecaudo/";
                } else {
                    msgError(dato.message);
                }
            }
        });
    }
    return false;
};