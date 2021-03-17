var VARIABLES = {
    K_OK_SIMBOL: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    K_ALERT_SIMBOL:'<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    K_SIN_SIMBOLO: '',
    K_SI: 1,
    K_NO :2
}

$(function () {

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });

    $("#btnBuscarSolicitud").on("click", function () {
        ListarControlLicencias();
    });  
    $("#dllestadolicencia").prop('selectedIndex', 1);
    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);

    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZAN LAS APROBACIONES O RECHAZOS PARA QUE UNA LICENCIA PUEDA FACTURAR  - VALIDAR QUE SE HAYAN INGRESADO EL CALCULO DE LA TARIFA Y FICHA DE LEVANTAMIENTO", "INFORMACION", VARIABLES.K_OK_SIMBOL);
});


function ListarControlLicencias() {

    var LIC_ID = $("#txtcodlic").val() == "" ? "0" : $("#txtcodlic").val();
    var OFF_ID = $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val();
    var ESTADO = $("#dllestadolicencia").val();
    var CON_FECHA = $("#chkConFechaCrea").is(':checked') == true ? 1 : 0;
    var FECHA_INI = $("#txtFecCreaInicial").val();
    var FECHA_FIN = $("#txtFecCreaFinal").val();
    //var OFICINA = $("#hidOficina").val() == "" ? "0" : $("#hidOficina").val();
    //var LIC_ID = $("#txtlic").val() == "" ? "0" : $("#txtlic").val();
    //var INV_ID = $("#txtdoc").val() == "" ? "0" : $("#txtdoc").val();
    //var EST_ID = $("#txtestid").val() == "" ? "0" : $("#txtestid").val();


    $.ajax({
        data: {
            LIC_ID: LIC_ID, OFF_ID: OFF_ID, CON_FECHA: CON_FECHA, FECHA_INICIAL: FECHA_INI, FECHA_FIN: FECHA_FIN, ESTADO: ESTADO
        },
        url: '../AdministracionControlLicencias/ListarControlLicencias',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#grid").html(dato.message);
            }
        }
    });

}

var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
};

function obtenerNombreConsultaOficina(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/obtenerNombreOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function AprobarControl(id) {

    ConfirmarControl('SE APROBARA EL CONTROL PARA ESTA LICENCIA , (ESTA SEGURO(A) DE CONTINUAR ?',
    function () { ActualizaLicenciaEstado(id,VARIABLES.K_SI); },
    function () { },
    'Confirmar');
}

function RechazarControl(id) {

    ConfirmarControl('SE RECHAZARA EL CONTROL PARA ESTA LICENCIA , (ESTA SEGURO(A) DE CONTINUAR ?',
    function () { ActualizaLicenciaEstado(id, VARIABLES.K_NO); },
    function () { },
    'Confirmar');
}



function ConfirmarControl(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">'+ dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }


    });

}

function VentanaAviso(dialogText, dialogTitle, SIMBOLO) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + SIMBOLO + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }


    });
}

function VerLicenciVentanaNueva(lic_id) {

    window.open('../Licencia/Nuevo?set=' + lic_id, '_blank');
}



function ActualizaLicenciaEstado(LIC_ID,estado) {

    //alert("ACTUALIZANDO");

    $.ajax({
        data: {
            LIC_ID: LIC_ID, ESTADO: estado
        },
        async: false,
        type: 'POST',
        url: '../AdministracionControlLicencias/ActualizarLicenciaEstadoAprob',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {

                VentanaAviso(dato.message, "FINALIZADO CORRECTAMENTE", VARIABLES.K_OK_SIMBOL);

            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", VARIABLES.K_ALERT_SIMBOL);
            }
        }
    });
}