var VARIABLES = {
    K_OK_SIMBOL: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    K_ALERT_SIMBOL: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    K_SIN_SIMBOLO: '',
    K_SI: 1,
    K_NO: 0,
    K_WIDTH_NOT: 600,
    K_HEIGHT_NOT: 325,
    CERO: 0,
    UNO: 1,
    VACIO: "",
    K_GENERAL: 1,
    K_APROBADO: 2,
    K_OBSERVADO: 3,
    K_ELEGIR_DATOS:"POR FAVOR DE SELECCIONAR LA OFICINA Y LA MODALIDAD"
}

$(function () {

    $("#tabs").tabs();

    $("#mvleyendaSeguimiento").dialog({
        autoOpen: false,
        width: VARIABLES.K_WIDTH_NOT,
        height: VARIABLES.K_HEIGHT_NOT,
        buttons: {
            "OKEY": function () {

                $("#mvleyendaSeguimiento").dialog("close");

            }
        },
        modal: true
    });

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    loadTipoGrupo('dllGruModalidad', VARIABLES.CERO);


    $("#btnBuscarSeguimiento").on("click", function () {
        ListarSeguimientoLocal();
    });

    

    $("#btnRecalcular").on("click", function () {

        Recalcular();
        

    });

    $("#btnPdfSeg").on("click", function () {

        $("#contenedor").show();

        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html(); 
        var SUBTIPO = "-";

        ExportarLicenciaSeguimiento('PDF', VARIABLES.K_GENERAL, OFICINA, PERIODO, SUBTIPO);


    });

    $("#btnExcelSeg").on("click", function () {

        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html();
        var SUBTIPO = "-";

        ExportarLicenciaSeguimiento('EXCEL', VARIABLES.K_GENERAL, OFICINA, PERIODO, SUBTIPO);
    });


    $("#btnPdfSegApr").on("click", function () {

        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html();
        var SUBTIPO = $('#lstopcionaprob option:selected').html();

        $("#contenedor").show();

        ExportarLicenciaSeguimiento('PDF', VARIABLES.K_APROBADO, OFICINA, PERIODO, SUBTIPO);
    });

    $("#btnExcelSegApr").on("click", function () {

        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html();
        var SUBTIPO = $('#lstopcionaprob option:selected').html();
        ExportarLicenciaSeguimiento('EXCEL', VARIABLES.K_APROBADO, OFICINA, PERIODO, SUBTIPO);
    });


    $("#btnPdfSegObs").on("click", function () {
        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html();
        var SUBTIPO = $('#lstrazon option:selected').html(); 
        $("#contenedor").show();

        ExportarLicenciaSeguimiento('PDF', VARIABLES.K_OBSERVADO, OFICINA, PERIODO, SUBTIPO);
    });

    $("#btnExcelSegObs").on("click", function () {
        var OFICINA = $("#lbOficina").html();
        var PERIODO = $('#lstMes option:selected').html();
        var SUBTIPO = $('#lstrazon option:selected').html();
        ExportarLicenciaSeguimiento('EXCEL', VARIABLES.K_OBSERVADO, OFICINA, PERIODO, SUBTIPO);
    });



    $("#leyendageneral").on("click", function () {
        $("#mvleyendaSeguimiento").dialog("open");
    });


    $("#lstMes").on("change", function () {

        $("#trListaLicenciaBuscar").show();
        $("#trListarLicenciaAprob").show();
        $("#trListarLicenciaObs").show();
        $("#contenedor").hide();
        ListarSeguimientoLocal();
        //ListarAprobadas();

    });


    $("#lstopcionaprob").on("change", function () {
        $("#trListaLicenciaBuscar").show();
        $("#trListarLicenciaAprob").show();
        $("#trListarLicenciaObs").show();
        $("#contenedor").hide();
        ListarAprobadas();
        //ListarAprobadas();

    });

    $("#lstrazon").on("change", function () {
        $("#trListaLicenciaBuscar").show();
        ListarObservados();

    });

    //ListarSeguimientoLocal();
    $("#trListaLicenciaBuscar").hide();

    LimpiarBuscarSeguimiento();

    //inicializando 
    var fechaActual = new Date();
    $("#lstMes").val(parseInt(fechaActual.getMonth())+1);
    $("#txtanio").val(fechaActual.getFullYear());

    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZA EL SEGUIMIENTO DE LICENCIAS POR AÑO", "INFORMACION", VARIABLES.K_OK_SIMBOL);

    

});

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
            } else if (dato.result == VARIABLES.K_NO) {
                alert(dato.message);
            }
        }
    });
}

function ListarSeguimientoLocal() {

    var OficinaId = $("#hidOficina").val();
    var anio = $("#txtanio").val();
    var MesEva = $("#lstMes").val();
    var ModId = $("#dllGruModalidad").val();

    if ((OficinaId != null && OficinaId > 0) && (ModId != null && ModId != 0)) {

        $.ajax({
            data: { anio: anio, CodigoOficina: OficinaId, CodigoModalidad: ModId, MesEvaluar: MesEva },
            url: '../AdministracionSeguimientoLocalPermanente/ListarLicenciaSeguimiento',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == VARIABLES.K_SI) {
                    var entidad = dato.data.Data;

                    $("#trListaLicenciaBuscar").show();
                    $("#gridSeguimiento").html(dato.message);

                    $("#lblsegActiva").html(entidad.CantidadLicencias);
                    $("#lblsegaprob").html(entidad.CantidadLicenciasAprobadas);
                    $("#lblsegobs").html(entidad.CantidadLicenciasObservadas);

                    ListarAprobadas();
                    ListarObservados();
                } else if (dato.result == VARIABLES.K_NO) {
                    alert(dato.message);
                }
            }
        });
    } else {
        alert(VARIABLES.K_ELEGIR_DATOS);
    }


}

function ListarAprobadas() {

    var MesSeleccionado = $("#lstMes").val() == "" ? "0" : $("#lstMes").val();
    var TipoSeleccionado = $("#lstopcionaprob").val() == "" ? "0" : $("#lstopcionaprob").val();
    $.ajax({
        data: { Mes: MesSeleccionado, TIpo: TipoSeleccionado },
        url: '../AdministracionSeguimientoLocalPermanente/ListarLicenciaSeguimientoAprobados',
        type: 'POST',
        async : false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == VARIABLES.K_SI) {
                var entidad = dato.data.Data;
                $("#gridAprobados").html(dato.message);
                $("#lblCantidadAprobadas").html(entidad.CantidadLicencias);
                $("#lbllicaprobselect").html(entidad.CantidadLicenciasListaParaEmisionSeleccionada);
                $("#lbllicaprobadelant").html(entidad.CantidadLicenciasPagoAdelantado);
                $("#lbllicaprobasinper").html(entidad.CantidadLicenciasSinPeriodo);
            } else if (dato.result == VARIABLES.K_NO) {
                alert(dato.message);
            }
        }
    });

    
}

function ListarObservados() {
    var TipoRazon = $("#lstrazon").val() == "" ? "0" : $("#lstrazon").val();

    $.ajax({
        data: { razon: TipoRazon },
        url: '../AdministracionSeguimientoLocalPermanente/ListarLicenciaSeguimientoObservados',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == VARIABLES.K_SI) {
                var entidad = dato.data.Data;
                $("#gridRechazados").html(dato.message);
                $("#lbllicenobs").html(entidad.CantidadLicenciasObservadas);
                $("#lbllicenobsrazon").html(entidad.CantidadLicenciasobservadasRazon);
            } else if (dato.result == VARIABLES.K_NO) {
                alert(dato.message);
            }
        }
    });


}


function LimpiarBuscarSeguimiento() {
    $("#trListaLicenciaBuscar").hide();
    $("#lbOficina").html('SELECCIONE OFICINA');
    $("#hidOficina").val("");
    $("#dllGruModalidad").prop('selectedindex', VARIABLES.CERO);
    $("#txtanio").val("");
    $("#lstMes").prop('selectedindex', VARIABLES.CERO);
    $("#lstopcionaprob").prop('selectedindex', VARIABLES.UNO);

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


function ExportarLicenciaSeguimiento(tipo,ventana,oficina,periodo,subtipo) {

    var url = "";
    //alert(oficina);
    //alert(periodo);
    //alert(subtipo);

    $.ajax({
        url: '../AdministracionSeguimientoLocalPermanente/ReporteTipo',
        type: 'POST',
        async: false,
        //data: { formato: tipo },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {

                //alert("ingreso");
                $("#trListaLicenciaBuscar").hide();
                $("#trListarLicenciaAprob").hide();
                $("#trListarLicenciaObs").hide();

                url = "../AdministracionSeguimientoLocalPermanente/ReporteLicenciaSeguimiento?" +
                "formato=" + tipo + "&ventana=" + ventana + "&oficina=" + oficina + "&periodo=" + periodo + "&subtipo=" + subtipo;

                $("#contenedor").show();
                $('#externo').attr("src", url);

                if (tipo == "EXCEL")
                    $("#contenedor").hide();


            } else if (dato.result == 0) {
                //alert("khe");

                //$('#externo').attr("src", vacio);
                $("#contenedor").hide();
                alert(dato.message);
            }
        }
    });

}

function Recalcular() {

    var url = "";
    //alert(oficina);
    //alert(periodo);
    //alert(subtipo);
    var anio = $("#txtanio").val();
    var MesEva = $("#lstMes").val();
    $.ajax({
        url: '../AdministracionSeguimientoLocalPermanente/Recalcular',
        type: 'POST',
        async: false,
        data: { anio: anio, MesEva: MesEva },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                ListarSeguimientoLocal();
                ListarObservados();
                alert(dato.message);

            } else if (dato.result == 0) {

                alert(dato.message);
            }
        }
    });

}
