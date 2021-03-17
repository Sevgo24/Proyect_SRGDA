var K_WIDTH_OBS2_DOC = 600;
var K_HEIGHT_OBS2_DOC = 325;
var K_ES_SOLICITUD_DOCUMENTO_PENDIENTE_CANCELADO = 7;
var K_ES_MODIF_DOC_MANUAL = 3;
var K_ES_MODIF_DOC_PEND_CANC = 4;
var K_MODIFICAR_DOCUMENTO_MANUAL = 6;

$(function () {
    //ObtenerOficinaConsultaDocumeno();
    $("#btnBuscarDemo").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var idMoneda = $('#ddlMoneda').val();
        //if (idMoneda != '0')
        ConsultaDocumento(idMoneda);
    });

    $("#btnPdf").on("click", function () {

        $("#contenedor").show();

        ExportarReporteFacturaCons('PDF');
    });

    $("#btnExcel").on("click", function () {

        ExportarReporteFacturaCons('EXCEL');
        //ExportarReportef2('EXCEL');
    });


    $("#tdocultar").hide();
    $("#ddlTipoFacturacionIndividual").prop('selectedIndex', 0);

    loadListarFiltroOrdenConsultaDocumento('ddlorden', '0');
    

    $("#mvSolicitudRequeDoc").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2_DOC,
        height: K_HEIGHT_OBS2_DOC,
        buttons: {
            "Registrar": GuardarRequerimiento,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvSolicitudRequeDoc").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });


    //$("#").
    $('#txtfechadocumento').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    $("#txtfechadocumento").data("kendoDatePicker").value("");
    
    loadTipoRquerimiento('ddltiporequerimiento', 6, K_ES_MODIF_DOC_MANUAL);
    $("#ddltiporequerimiento").on("change", function () {
        if ($(this).val() != K_MODIFICAR_DOCUMENTO_MANUAL) {
            $("#trmonto").hide();
            $("#txtmonto").val("");
            $("#trfecha").hide();
            $("#txtfechadocumento").val("");
        } else {
            $("#trmonto").show();
            $("#trfecha").show();
        }
    });
    ObtenerOficinaConsultaDocumeno();

    $("#btnEnviarManuales").hide();
    $("#btnEnviarManuales").on("click", function () {
        EnviarDocumentosManuales();
        //verReporteCompleto('PDF');
    });
});

function ConsultaDocumento(moneda) {
    $("#btnNotaCredito").hide();
    $("#contenedor").hide();
    var idSerial = $("#hidSerie").val();
    var numFact = $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val();
    var idFactura = $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val();

    var idSocio = $("#hidEdicionEnt").val();
    var idGrupoFacturacion = $("#hidGrupo").val();
    var idGrupoEmpresarial = $("#hidCodigoGrupoEmpresarial").val();

    var conFecha = $('#chkConFecha').is(':checked') == true ? 1 : 0;
    var Fini = $("#txtFecInicial").val();
    var Ffin = $("#txtFecFinal").val();
    var idLicencia = $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val();

    var idDivision = 0;
    var idOficina = $("#hidOficina").val();
    var idAgente = $("#hidEdicionEntAGE").val();

    var idMoneda = '';
    if (moneda === undefined)
        idMoneda = $("#ddlMoneda").val();
    else
        idMoneda = moneda;
    var tipoDoc = $("#ddlTipoDocumento").val();
    var estado = $("#ddlEstado").val();

    var idDepartamento = 0;
    var idProvincia = 0;
    var idDistrito = 0;
    var estadoSun = $("#dllEstadoSunat").val();

    var orden = $("#ddlorden").val();
    //var idTipoLic = $("#dllTipoLicencia").val() == '' ? 0 : $("#dllTipoLicencia").val();
    //if ($('#chkNoImp').attr('checked'))
    //    noImpresas = 1

    //if ($('#chkNoAnu').attr('checked'))
    //    noAnuladas = 1

    //var tipLicencia = $("#dllTipoLicencia").val();
    //    var Impresas = $('#chkImp').is(':checked') == true ? 1 : 0;
    //var Anuladas = $('#chkAnu').is(':checked') == true ? 1 : 0;
    // var valorDivision = $("#hidSubDivision").val();




    $.ajax({
        url: '../ConsultaDocumento/ConsultaDocumento',
        type: 'POST',
        data: {
            idSerial: idSerial, numFact: numFact, idFactura: idFactura,
            idSocio: idSocio, idGrupoFacturacion: idGrupoFacturacion, idGrupoEmpresarial: idGrupoEmpresarial,
            conFecha: conFecha, Fini: Fini, Ffin: Ffin, idLicencia: idLicencia,
            idDivision: idDivision, idOficina: idOficina, idAgente: idAgente,
            idMoneda: idMoneda, tipoDoc: tipoDoc, estado: estado,
            idDepartamento: idDepartamento, idProvincia: idProvincia, idDistrito: idDistrito, estadoSunat: estadoSun, ORDEN: orden
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#grid").html(dato.message);
                $("#CantidadRegistros").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#grid").html('');
                $("#CantidadRegistros").html("");
                alert(dato.message);
            }
        }
    });
}



function verDetalleDocumento(id) {
    var url = '../ConsultaDocumentoDetalle/Index?id=' + id;
    window.open(url, "_blank");
}

function ObtenerOficinaConsultaDocumeno() {
    $.ajax({
        //data: { Id: idSel },
        url: '../ConsultaDocumento/ObtenerOficinaConsultaDocumeno',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#hidOficina").val(dato.Code);
                $("#lbOficina").html(dato.valor);
                if (dato.Code == 10154 || dato.Code == 10081)
                    $("#btnBuscaOficina").show();
                else
                    $("#btnBuscaOficina").hide();

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function SolicitarRuerimiento(id, tipo) {

    loadTipoRquerimiento('ddltiporequerimiento', 6, K_ES_MODIF_DOC_MANUAL);
    $("#trmonto").show();
    $("#trfecha").show();
    $("#mvSolicitudRequeDoc").dialog("open");
    $("#lblinvid").html(id);
    $("#txtAprobacionDesc").val("");
    $("#txtmonto").val("");
    $("#txtfechadocumento").val("");
    $("#hidCodigoDocumento").val(id);

    if (tipo == K_ES_MODIF_DOC_PEND_CANC)
    {
        //alert("ES DE PEND A CANC");
        $("#ddltiporequerimiento").prop('selectedIndex', 0);
        SolicitarRequerimientoMarcarCobrada(id);
    }
}


function GuardarRequerimiento() {

    var EST_ID = 0;
    var ID_REQ_TYPE = $("#ddltiporequerimiento").val();
    var RAZON = $("#txtAprobacionDesc").val();
    var ACTIVO = $("#ddltiporequerimiento").val() == 5 ? 1 : 0; // SI DESEAN QUE EL DOCUMENTO ESTE 
    var MONTO = $("#txtmonto").val() == "" ? 0 : $("#txtmonto").val();
    var FECHA = $("#txtfechadocumento").val() == "" ? "" : $("#txtfechadocumento").val();
    var INV_ID = $("#hidCodigoDocumento").val();
    var LIC_ID = 0;
    var BPS_ID = 0;
    var BEC_ID = 0;
    var TIP_LIC_INACT = 0;

    if (ID_REQ_TYPE > 0) {

        $.ajax({
            data: { EST_ID: EST_ID, ID_REQ_TYPE: ID_REQ_TYPE, RAZON: RAZON, ACTIVO: ACTIVO, MONTO: MONTO, FECHA: FECHA, INV_ID: INV_ID, LIC_ID: LIC_ID, BPS_ID: BPS_ID, BEC_ID: BEC_ID, TipoInactivacion: TIP_LIC_INACT },
            url: '../AdministracionModuloRequerimientos/RegistraRequerimientoGral',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    $("#mvSolicitudRequeDoc").dialog("close");

                    if (ID_REQ_TYPE == K_ES_SOLICITUD_DOCUMENTO_PENDIENTE_CANCELADO)
                        AprobarRequerimientoAutomaticamente(dato.Code);


                } else {
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("POR FAVOR DE ELEGIR UN TIPO DE REQUERIMIENTO");
    }
}

function SolicitarRequerimientoMarcarCobrada(id) {
    // loadTipoRquerimiento('ddltiporequerimiento', K_ES_SOLICITUD_DOCUMENTO_PENDIENTE_CANCELADO,K_ES_MODIF_DOC_PEND_CANC);
    $("#ddltiporequerimiento").find("option[value='" + 5 + "']").prop("disabled", true);// DESACTIVANDO LAS DEMAS OPCIONES 
    $("#ddltiporequerimiento").find("option[value='" + 6 + "']").prop("disabled", true);// DESACTIVANDO LAS DEMAS OPCIONES 
    $("#trmonto").hide();
    $("#trfecha").hide();
    //$("#mvSolicitudRequeDoc").dialog("open");
    //$("#lblinvid").html(id);
    //$("#txtAprobacionDesc").val("");
    //$("#txtmonto").val("");
    //$("#txtfechadocumento").val("");
    //$("#hidCodigoDocumento").val(id);
}



function AprobarRequerimientoAutomaticamente(id) {
    
    var ESTADO_APROB = 1;

    $.ajax({
        data: { ID_REQ: id, ESTADO: ESTADO_APROB },
        url: '../AdministracionModuloRequerimientos/RespuestaRequerimiento',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                $("#mvSolicitudReque").dialog("close");
                Listar();
                //$("#ddltipoAprobacion").val(entidad.TIPO);

            } else {
                alert(dato.message);
            }
        }
    });
}

function EnviarDocumentosManuales() {

    var fechaInicio = $("#txtFecInicial").val();
    var fechaFin = $("#txtFecFinal").val();
    var Oficina = $("#hidOficina").val();
    
    $.ajax({
        data: { fechaInicio: fechaInicio, fechaFin: fechaFin, Oficina: Oficina },
        url: '../ConsultaDocumento/EnviarDOcumentosManuales',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //BuscarFacturasConsulta();
                ConsultaDocumento(0);
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });


}
function ExportarReporteFacturaCons(tipo) {

    var url = "";

    $.ajax({
        url: '../ConsultaDocumento/ReporteTipo',
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
                $("#grid").html('');
                url = "../ConsultaDocumento/ReporteFacturasConsultadas?" +
                    "formato=" + tipo;

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

