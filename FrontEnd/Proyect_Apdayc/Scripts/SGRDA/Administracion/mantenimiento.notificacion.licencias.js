var VARIABLES = {
    K_OK_SIMBOL: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    K_ALERT_SIMBOL: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    K_SIN_SIMBOLO: '',
    K_SI: 1,
    K_NO: 0,
    K_WIDTH_NOT: 600,
    K_HEIGHT_NOT: 325,
    CERO: 0,
    UNO:1,
    VACIO:""
}
$(function (){

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });

    $("#btnBuscarNotificacion").on("click", function () {
        ListarNotirifacionLicencias();
    });

    $("#mvNotificacionEvento").dialog({
        autoOpen: false,
        width: VARIABLES.K_WIDTH_NOT,
        height: VARIABLES.K_HEIGHT_NOT,
        buttons: {
            "Grabar": GuardarModificacionNotificacoinLicencia,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvNotificacionEvento").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);
    
    loadTarifaModalidad('ddTarifaAsociada', '0', VARIABLES.CERO);

    VentanaAviso("EN EL SIGUIENTE MODULO SE MODIFICAN LOS EVENTOS PARA QUE PUEDAN FACTURARSE - NOTIFICADOS POR EL AREA DE CAR ", "INFORMACION", VARIABLES.K_OK_SIMBOL);

    LimpiarNotificaciones();
});

function ListarNotirifacionLicencias() {

    var CodigoLicencia = $("#txtcodlic").val() == VARIABLES.VACIO ? VARIABLES.CERO : $("#txtcodlic").val();
    var Oficina = $("#hidOficina").val() == VARIABLES.VACIO ? VARIABLES.CERO : $("#hidOficina").val();
    var NombreLicencia = $("#txtnomlic").val() == VARIABLES.VACIO ? VARIABLES.VACIO : $("#txtnomlic").val();
    var NombreEstablecimiento = $("#txtnomest").val() == VARIABLES.VACIO ? VARIABLES.VACIO : $("#txtnomest").val();
    var CON_FECHA = $("#chkConFechaCrea").is(':checked') == true ? VARIABLES.UNO : VARIABLES.CERO;
    var FECHA_INI = $("#txtFecCreaInicial").val();
    var FECHA_FIN = $("#txtFecCreaFinal").val();
    var ESTADO_LIC = $("#ddlEstadoLic").val();

    $.ajax({
        data: {
            CodigoLicencia: CodigoLicencia, Oficina: Oficina, NombreLicencia: NombreLicencia, NombreEstablecimiento: NombreEstablecimiento,
            ConFecha: CON_FECHA, FechaInicial: FECHA_INI, FechaFin: FECHA_FIN, EstadoLicencia: ESTADO_LIC
        },
        url: '../AdministracionNotificacionLicencias/ListarLicenciasxAsignar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == VARIABLES.K_SI) {
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
            if (dato.result == VARIABLES.K_SI) {
                $("#lbOficina").html(dato.valor);
            } else if (dato.result == VARIABLES.CERO) {
                alert(dato.message);
            }
        }
    });
}


function ConfirmarControl(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
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

function editarLicencia(idLicencia,idEstablecimiento)
{
    LimpiarNotificaciones();
    $("#mvNotificacionEvento").dialog("open");
    $("#hidIdLic").val(idLicencia);
    if (idEstablecimiento > VARIABLES.CERO)
    {
        reloadEventoEst(idEstablecimiento);
        $("#btnBuscarEstablecimiento").hide();
    }else{
        $("#btnBuscarEstablecimiento").show();
    }

}

function GuardarModificacionNotificacoinLicencia() {

    var mod = $("#hidModalidad").val() == "" ? VARIABLES.CERO : $("#hidModalidad").val();
    var tarifa =$("#ddTarifaAsociada").val();
    var socio = $("#hidResponsable").val() == "" ? VARIABLES.CERO : $("#hidResponsable").val();
    var establecimiento = $("#hidEstablecimiento").val() == "" ? VARIABLES.CERO : $("#hidEstablecimiento").val();
    var Lic_id = $("#hidIdLic").val() == "" ? VARIABLES.CERO : $("#hidIdLic").val();

    
    if (!ValidaUsuarioMoroso(socio)) {
        $.ajax({
            data: { CodigoLicencia: Lic_id, CodigoModalidad: mod, CodigoTarifa: tarifa, CodigoEstablecimiento: establecimiento, CodigoSocio: socio },
            url: '../AdministracionNotificacionLicencias/ActualizarEstadoLicenciaNotificada',
            type: 'POST',
            success: function (response) {
                var dato = response;
                if (dato.result == VARIABLES.K_SI) {
                    //alert(dato.valor);
                    alert(dato.message);
                    $("#mvNotificacionEvento").dialog("close");
                    ListarNotirifacionLicencias();
                } else {
                    alert(dato.message);
                    //$("#mvNotificacionEvento").dialog("close");
                }
            }

        });

    }
}

var reloadEventoModalidad = function (idSel) {
    $("#hidModalidad").val(idSel);
    obtenerNombreModalidad(idSel, "lbModalidad");
    //obtenerNombreTarifaLabels(idSel, "lblTarifaDesc", "lblTemporalidadDesc");
    loadTarifaModalidad('ddTarifaAsociada', idSel, VARIABLES.CERO);
};

var reloadEventoEst = function (idSel) {
    $("#hidEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
    //ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
};

var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == VARIABLES.K_SI) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            }
        }

    });
}


function LimpiarNotificaciones() {

    $("#txtcodlic").val("");
    $("#lbModalidad").html("TODOS");
    $("#lbResponsable").html("TODOS");
    $("#lblEstablecimiento").html("TODOS");
    $("#hidModalidad").val("");
    $("#hidResponsable").val("");
    $("#hidEstablecimiento").val("");
    $("#ddTarifaAsociada").prop('selectedIndex', VARIABLES.CERO);

}

function VerLicenciVentanaNueva(IdLicencia) {
    window.open("../Licencia/Nuevo?set=" + IdLicencia);
}

function ValidaUsuarioMoroso(BPS_ID) {
    var respuesta = false;

    $.ajax({
        data: {
            BPS_ID: BPS_ID
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidaUsuarioMoroso',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {
                alert(dato.message);
                respuesta = true;
                //$("#btnGrabar").hide();
//                msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);

            } else if (dato.result < 0) {
                alert(dato.message);
            } else {
                respuesta = false;
            }
        }
    });

    return respuesta;
}