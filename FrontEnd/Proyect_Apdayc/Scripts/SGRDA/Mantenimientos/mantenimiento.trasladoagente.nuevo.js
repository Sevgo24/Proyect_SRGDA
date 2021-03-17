$(function () {
    $("#txtOficinaActual").attr("disabled", "disabled");

    loadComboNivelAgente('ddlNivelAgente', 0);
    loadComboOficinaDestino('ddlOficinaDestino', 0)

    //$("#txtFechaInicio").datepicker({ dateFormat: 'dd/mm/yy' });
    //$("#txtFechaFin").datepicker({ dateFormat: 'dd/mm/yy' });

    $("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });

        
    var id = GetQueryStringParams("id");
    $("#hidCodigoBPS").val(id);
    ObtenerOficinaActualAgente(id);


    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnDescartar").on("click", function () {
        location.href = "../TrasladoAgentesRecaudo/";
    });
});

function ObtenerOficinaActualAgente(id) {
    $.ajax({
        url: '../General/ObtenerOficinaActualAgente',
        type: 'POST',
        data: { idAgente: id },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#hidCodigoOFI").val(dato.Code);
                $("#txtOficinaActual").val(dato.valor);
            } else {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

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
            url: 'Insertar',
            data: oficina,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TrasladoAgentesRecaudo/";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};