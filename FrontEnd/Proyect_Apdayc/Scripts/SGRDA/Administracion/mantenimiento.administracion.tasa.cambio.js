var K_WIDTH_OBS2 = 360;
var K_HEIGHT_OBS2 = 160;
var MSG = {
    MSG_SIN_VALOR_TASA_CAMBIO : 'INGRESE UN VALOR DE TASA DE CAMBIO '
}

$(function () {

    $('#txtfecini').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtfecfin').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();


    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    $("#txtfecini").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtfecini").data("kendoDatePicker").value();

    $("#txtfecfin").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtfecfin").data("kendoDatePicker").value();

    $("#btnBuscar").on("click", function () {
        ConsultaTasaCambio();
    });

    $("#btnLimpiar").on("click", function () {
        Limpiar();
    });


    $("#btnNuevo").on("click", function () {
        LimpiarPopupTasaCambio();
        $("#mvAgregarTasaCambio").dialog("open");
    });

    $("#txtfecha").val(fechaActual.getDate() +'/'+('00'+  (parseInt( fechaActual.getMonth()) +1)).slice(-2).toString() +'/'+fechaActual.getFullYear());


    $("#mvAgregarTasaCambio").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": GrabarTasaCambio,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvAgregarTasaCambio").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

});





function ConsultaTasaCambio() {
        
    var fecha_inicial = $("#txtfecini").val() == '' ? '' : $("#txtfecini").val();
    var fecha_final = $("#txtfecfin").val() == '' ? '' : $("#txtfecfin").val();
        $.ajax({
            data: { FEC_INI: fecha_inicial, FEC_FIN: fecha_final },
            url: '../AdministracionTasaCambio/ListaTasaCambio',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                //$("#trListaEstable").show();
                //$("#btnMarcaEstPrinci").show();
                //$("#btnMarcaEstInactivar").show();
                //$("#btnElegirSocio").show();
                $("#grid").html(dato.message);
                // $("#grid2").html(dato.message);
                //$("#trListaLicencias").show();
                //$("#trseleccionar").show();
            }
        }

    });
}

function GrabarTasaCambio() {


    var fecha = $("#txtfecha").val();
    var valor = $("#txtvalortasacambio").val();

    if (valor != '') {
        $.ajax({
            data: { FECHA: fecha, VALOR: valor },
            url: '../AdministracionTasaCambio/GrabarTasadeCambio',
            type: 'POST',
            //async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    $("#mvAgregarTasaCambio").dialog("close");
                    ConsultaTasaCambio()
                    alert(dato.message);
                } else {
                    alert(dato.message);
                    $("#mvAgregarTasaCambio").dialog("close");
                }
            }

        });

    } else {
        alert(MSG.MSG_SIN_VALOR_TASA_CAMBIO);
    }
}


function Limpiar() {
    $("#txtfecini").data("kendoDatePicker").value(new Date());
    $("#txtfecfin").data("kendoDatePicker").value(new Date());
    $("#trListaTasaCambio").hide();
}




function LimpiarPopupTasaCambio() {
    $("#txtvalortasacambio").val('');
}



function ConsultaTasadeCambioActiva() {
    var resp=0
    $.ajax({
        //data: { FECHA: fecha, VALOR: valor },
        url: '../AdministracionTasaCambio/ConsultaTasaCambio',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                resp = dato.result;
            } else {
                alert(dato.message);
                //$("#mvAgregarTasaCambio").dialog("close");
            }
        }

    });
    return resp;
}