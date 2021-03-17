/************************** INICIO CONSTANTES****************************************/
var LimiteFacturaSelect = 300;
var K_WIDTH_OBS = 600;
var K_HEIGHT_OBS = 285;
/************************** INICIO CARGA********************************************/
var tipoDivisionADM = 'ADM'
$(function () {
    loadTipoLicencia('dllTipoLicencia', '0');
    loadFortmatoFacturacion('ddlTipoImpresion', 0);
    loadTipoFactura('ddlTipoDocumento', 0);
    loadEstadoFactura('ddlEstado', 0);
    loadTipoNotaCredito('ddlTipoNotaCredito', 0);
    loadEstadoFacturaSunat('dllEstadoSunat',0);

    $('#txtNumFact').on("keypress", function (e) { return solonumeros(e); });
    $('#txtNumLic').on("keypress", function (e) { return solonumeros(e); });
    $('#txtIdFact').on("keypress", function (e) { return solonumeros(e); });
    $('#hidIdFactA').on("keypress", function (e) { return solonumeros(e); });

    //$("#mvObservacion").dialog({
    //    autoOpen: false,
    //    width: K_WIDTH_OBS,
    //    height: K_HEIGHT_OBS,
    //    buttons: {
    //        "Grabar": addObsAnulacionfact,
    //        "Cancelar": function () {
    //            $('#ddlTipoNotaCredito').val(0);
    //            $("#mvObservacion").dialog("close");
    //            $('#txtObservacion').css({ 'border': '1px solid gray' });
    //        }
    //    },
    //    modal: true
    //});

    $("#mvAnulacion").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS,
        height: K_HEIGHT_OBS,
        buttons: {
            "Grabar": addObsAnulacionfact,
            "Cancelar": function () {
                $("#mvAnulacion").dialog("close");
                $('#txtDescripcion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    //limpiar();
    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);

    $("#chkConFecha").prop('checked', true);

    $("#chkConFecha").change(function () {
        if ($('#chkConFecha').is(':checked')) {
            $('#txtFecInicial').data('kendoDatePicker').enable(true);
            $('#txtFecFinal').data('kendoDatePicker').enable(true);
        } else {
            $('#txtFecInicial').data('kendoDatePicker').enable(false);
            $('#txtFecFinal').data('kendoDatePicker').enable(false);
        }
    });

    var moneda = (GetQueryStringParams("Moneda"));
    if (moneda === undefined) {
        //alert('1');
        loadMonedas('ddlMoneda', '0');
    } else {
        loadMonedas('ddlMoneda', moneda);
        ConsultaDocumento(moneda);
    }
    //-------------------------- EVENTO BOTONES ---------------------
    $("#btnBuscar").on("click", function () {        
        var estadoRequeridos = ValidarRequeridos();
        var idMoneda = $('#ddlMoneda').val();
        if (idMoneda != '0') ConsultaDocumento(idMoneda);
    });

    $('#txtNumFact').keypress(function (e) { BuscarConsulta(e); });
    $('#txtIdFact').keypress(function (e) { BuscarConsulta(e); });
    $('#txtNumLic').keypress(function (e) { BuscarConsulta(e); });

    $("#btnLimpiar").on("click", function () {
        $("#grid").html('');
        limpiarControles();
    });


    //mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficin" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarAgente({ container: "ContenedormvBuscarAgenteComercial", idButtonToSearch: "btnBuscarAGE", idDivMV: "mvBuscarAgente", event: "reloadEventoAgente", idLabelToSearch: "lbAgente" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscaGrupo", idDivMV: "mvBuscarGrupo", event: "reloadEventoGrupo", idLabelToSearch: "lbGrupo" });
    mvInitNotaCredito({ container: "ContenedormvNotaCredito", idButtonToSearch: "btnNotaCredito", idDivMV: "mvNotaCredito", event: "reloadEventoNotaCredito", idLabelToSearch: "" });
    mvInitBuscarCorrelativoNotaCredito({ container: "ContenedormvBuscarCorrelativoNC", idButtonToSearch: "btnBuscarSerieNC", idDivMV: "mvBuscarCorrelativoNC", event: "reloadEventoCorrelativoNC", idLabelToSearch: "lblSer" });
    mvInitSubDivisiones({ container: "ContenedormvSubDivision", idButtonToSearch: "btnBuscaSubDivision", idDivMV: "mvBuscaSubDivision", event: "reloadEventoSubDivision", idLabelToSearch: "lbSubDivision" });
    mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });

    $("#btnNotaCredito").on("click", function () {
        limpiarNotaCredito();
        var validacion = ValidarFechaNotaCredito($("#hidId").val());
        if (validacion) {
            var estadoNC = ObtenerDatosCabecera($("#hidId").val());
            if (estadoNC) {
                $("#mvNotaCredito").dialog("open");
                ObtenerDetalleFactura($("#hidId").val());
            }
        }
        else
            alert("No se puede crear nota de crédito para esta factura, no se encuentra dentro del mes");
    });
    $("#btnNotaCredito").hide();
    $("#btnNotaCredito2").hide();
    $("#btnEnviarAprobacion").hide();

    $("#btnReenviarAnulado").on("click", function () {
        //var tipoImp = $('#ddlTipoImpresion').val();
        //if (tipoImp != 0) {
        //    $('#ddlTipoImpresion').css({ 'border': '1px solid gray' });
            obtenerFacturasSeleccionadasAnular(1);
        //} else {
        //    $('#ddlTipoImpresion').css({ 'border': '1px solid red' });
        //    alert('Seleccione el tipo de impresión.');
        //}
    });
    $("#btnReenviarAnulado").hide();
    $("#btnReenviarSunat").on("click", function () {
        obtenerFacturasSeleccionadasAnular(2);
        //verReporteCompleto('PDF');
    });

    $("#btnReenviarSunatLocTrans").on("click", function () {
        ReenviarDocumentosMasivosEmision();
        //verReporteCompleto('PDF');
    });

    //$("#btnExcel").on("click", function () {
    //    verReporteCompleto('EXCEL');
    //});

    //$("#btnReenviarAnulado").show();
    //$("#btnPdf").hide();
    //$("#btnExcel").hide();

    $("#mvQuiebra").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS,
        height: K_HEIGHT_OBS,
        buttons: {
            "Grabar": addObsQuiebrafact,
            "Cancelar": function () {
                $("#mvQuiebra").dialog("close");
                $('#txtDescripcion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });
});

function BuscarConsulta(e) {
    if (e.which == 13) {
        var estadoRequeridos = ValidarRequeridos();
        var idMoneda = $('#ddlMoneda').val();
        if (idMoneda != '0') {
            ConsultaDocumento(idMoneda);
        }
    }
}

function ObtenerDetalleFactura(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../FacturacionConsulta/ObtenerDetalleFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDetalloFactura(idSel);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataDetalloFactura(id) {
    loadDataGridTmpDet('../FacturacionConsulta/ListarDetalleFactura', "#gridDetalleFactura", id);
}

function loadDataGridTmpDet(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { Id: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                calcularTotalBase();
                calcularTotalImpuesto();
                calcularTotalNeto();
                calcularTotalBaseCobrado();
                calcularTotalImpuestoCobrado();
                calcularTotalNetoCobrado();
                calcularTotalPendiente();

                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ReenvioSunat(id, tipo) {
    var moneda = $('#ddlMoneda').val();
    Confirmar(' ¿Desea enviar el comprobante a sunat?',
    function () {
        $.ajax({
            url: '../FacturacionConsulta/ReenvioSunat',
            data: {
                id: id,
                tipo: tipo
            },
            type : 'POST',
            beforeSend: function () {
            },
                    success: function(response) {
                var dato = response;
                if (dato.result == 1) {
                    alert(dato.message);
                    ConsultaDocumento(moneda);
                    //BuscarFacturasConsulta(moneda);
                }
            }
        });
        },
        function () {

            },
            'Confirmar'
        )
}

//****************************  FUNCIONES ****************************
function verReporteCompleto(tipo) {
    var noImpresas = 0;
    var noAnuladas = 0;

    var serie = $("#hidSerie").val();
    var numFact = $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val();
    var idTipoLic = $("#dllTipoLicencia").val() == '' ? 0 : $("#dllTipoLicencia").val();

    var idBps = $("#hidEdicionEnt").val();
    var idGF = $("#hidGrupo").val();

    var codMoneda = $("#ddlMoneda").val();


    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    var idFact = $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val();
    var numLic = $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val();

    if ($('#chkNoImp').attr('checked'))
        noImpresas = 1

    if ($('#chkNoAnu').attr('checked'))
        noAnuladas = 1

    var tipLicencia = $("#dllTipoLicencia").val();
    var idBpsAgen = $("#hidEdicionEntAGE").val();

    var conFecha = $('#chkConFecha').is(':checked') == true ? 1 : 0;

    var Impresas = $('#chkImp').is(':checked') == true ? 1 : 0;
    var Anuladas = $('#chkAnu').is(':checked') == true ? 1 : 0;

    var tipoDoc = $("#ddlTipoDocumento").val();
    var idOficina = $("#hidOficina").val();

    var valorDivision = $("#hidSubDivision").val();
    var estado = $("#ddlEstado").val();

    var url = "../FacturacionConsulta/ReporteCompleto?" +
            "numSerial=" + serie + "&" +
            "numFact=" + numFact + "&" +
            "idSoc=" + idBps + "&" +
            "grupoFact=" + idGF + "&" +
            "moneda=" + codMoneda + "&" +
            "idLic=" + numLic + "&" +
            "Fini=" + ini + "&" +
            "Ffin=" + fin + "&" +
            "idFact=" + idFact + "&" +
            "impresas=" + Impresas + "&" +
            "anuladas=" + Anuladas + "&" +
            "licTipo=" + tipLicencia + "&" +
            "agenteBpsId=" + idBpsAgen + "&" +
            "conFecha=" + conFecha + "&" +
            "tipoDoc=" + tipoDoc + "&" +
            "idOficina=" + idOficina + "&" +
            "valorDivision=" + valorDivision + "&" +
            "estado=" + +estado + "&" +
            "format=" + tipo;

    var poPup = '';
    poPup = window.open(url, "Registro de Ventas", "menubar=false,resizable=false,width=750,height=550");
}

//function verReporte(cod) {
//    var url = "../FacturacionConsulta/Reporte?id=" + cod;
//    var poPup = '';
//    poPup = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
//}

//FACTURA ELECTRONICA
function verReporte(cod) {
    $.ajax({
        url: "../FacturacionConsulta/Reporte",
        type: 'POST',
        data: { id: cod },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var ruta = dato.valor;
                var poPup = '';
                poPup = window.open(ruta, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
            } else if (dato.result == 2) {
                //poPup = window.open("", "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550")
                //alert(dato.valor);

                //var url = "../FacturacionConsulta/ReporteManual?id=" + cod;
                var url = "../FacturacionConsulta/ReporteFacturacionConsulta?id=" + cod;
                var poPup1 = '';
                poPup1 = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
            }
            else {
                //alert(dato.message);
                var url = "../FacturacionConsulta/ReporteManual?id=" + cod;
                var poPup1 = '';
                poPup1 = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
            }
        }
    });
}

function verReporteFacturacion(cod) {
    var url = "../FacturacionConsulta/ReporteFacturacionConsulta?id=" + cod;
    var poPup = '';
    poPup = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
}

function BuscarFacturasConsulta(moneda) {
    var serie = $("#hidSerie").val();
    var numFact = $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val();
    var idTipoLic = $("#dllTipoLicencia").val() == '' ? 0 : $("#dllTipoLicencia").val();

    var idBps = $("#hidEdicionEnt").val();
    var idGF = $("#hidGrupo").val();

    var codMoneda = '';
    if (moneda === undefined)
        var codMoneda = $("#ddlMoneda").val();
    else
        codMoneda = moneda;

    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    var idFact = $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val();
    var numLic = $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val();

    if ($('#chkNoImp').attr('checked'))
        noImpresas = 1

    if ($('#chkNoAnu').attr('checked'))
        noAnuladas = 1

    var tipLicencia = $("#dllTipoLicencia").val();
    var idBpsAgen = $("#hidEdicionEntAGE").val();

    var conFecha = $('#chkConFecha').is(':checked') == true ? 1 : 0;

    var Impresas = $('#chkImp').is(':checked') == true ? 1 : 0;
    var Anuladas = $('#chkAnu').is(':checked') == true ? 1 : 0;

    var tipoDoc = $("#ddlTipoDocumento").val();
    var idOficina = $("#hidOficina").val();

    var valorDivision = $("#hidSubDivision").val();
    var estado = $("#ddlEstado").val();

    var idBpsGroup = $("#hidCodigoGrupoEmpresarial").val();

    $.ajax({
        url: '../FacturacionConsulta/ListarConsulta',
        type: 'POST',
        data: {
            numSerial: serie,
            numFact: numFact,
            idSoc: idBps,
            grupoFact: idGF,
            moneda: codMoneda,
            idLic: numLic,
            Fini: ini,
            Ffin: fin,
            idFact: idFact,
            impresas: Impresas,
            anuladas: Anuladas,
            licTipo: tipLicencia,
            agenteBpsId: idBpsAgen,
            conFecha: conFecha,
            tipoDoc: tipoDoc,
            idOficina: idOficina,
            valorDivision: valorDivision,
            idBpsGroup: idBpsGroup,
            estado: estado,
            estadoSunat:0

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFacturaConsulta();
            } else if (dato.result == 0) {
                $("#grid").html('');
                alert(dato.message);
            }
        }
    });
}

function loadDataFacturaConsulta(estado) {
    loadDataGridTmp('../FacturacionConsulta/ListarFactConsulta', "#grid", estado == undefined ? 0 : estado);
}

function loadDataGridTmp(Controller, idGrilla, estado) {
    $.ajax({
        type: 'POST', data: { estado: estado }, url: Controller,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                $("#CantidadRegistros").html(dato.Code);
            } else if (dato.result == 0) {
                $("#grid").html('');
                $("#CantidadRegistros").html('0');
                alert(dato.message);
            }
        }
    });
}

function limpiarControles() {
    $("#hidCorrelativo").val(0);
    $("#lbCorrelativo").html('Seleccione una serie.');
    $("#lbCorrelativo").css('color', 'black');
    $("#hidEdicionEnt").val(0);
    $("#lbResponsable").html('Seleccione un socio.');
    $("#hidSubDivision").val(0);
    $("#lbSubDivision").html('Seleccione una subdivisión.');
    loadTipoFactura('ddlTipoDocumento', 0);

    $("#txtNumFact").val('');
    $("#hidGrupo").val(0);
    $("#lbGrupo").html('Seleccione un grupo facturación.');
    //$("#hidOficina").val(0);
    //$("#lbOficina").html('Seleccione una Oficina.');
    $("#txtIdFact").val('');

    $('#dllTipoLicencia').val(0);
    $('#ddlMoneda').val('PEN');
    $("#ddlEstado").val(0);
    $("#hidEdicionEntAGE").val(0);
    $("#lbAgente").html('Seleccione un Agente.');
    $("#txtNumLic").val('');
    //$('#ddlFormapago').val(0);
    $("#hidSerie").val(0);
    $("#hidActual").val(0);

    $("#chkNoAnu").prop('checked', false);
    $("#chkNoImp").prop('checked', false);
    $("#grid").html('');
    $("#CantidadRegistros").html('0');
}

//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
//GRUPO - BUSQ. GENERAL
var reloadEventoGrupo = function (idSel) {
    $("#hidGrupo").val(idSel);
    $("#hidEdicionEntGRU").val(idSel);
    obtenerNombreGrupo($("#hidGrupo").val(), 'lbGrupo');
};

function obtenerNombreGrupo(id, control) {
    $.ajax({
        url: "../GrupoFacturacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $('#lbGrupo').html(tipo.INVG_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//SERIE - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

//SERIE - CORRELATIVO BUSQ. GENERAL NOTA CREDITO
var reloadEventoCorrelativoNC = function (idSel) {
    //alert(idSel);
    $("#hidIdSerieNC").val(idSel);
    obtenerNombreCorrelativoNC($("#hidIdSerieNC").val());
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerNombreCorrelativoNC(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var cor = dato.data.Data;
                //alert(cor.NMR_ID);
                $("#lblSer").html(cor.NMR_SERIAL);
                $("#hidIdSerieNC").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lblSer").css('color', 'black');
                $("#txtCorrelativoNC").val(cor.NMR_NOW);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

// OFICINA - BUSQ. GENERAL
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

function ObtenerDatosCabecera(idFactura) {
    var estado = false;
    $.ajax({
        data: { Id: idFactura },
        url: '../FacturacionConsulta/ObtieneCabeceraFactura',
        async: false,
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtSerie").val(en.NMR_SERIAL);
                    $("#txtNumero").val(en.INV_NUMBER);
                    loadMonedas('ddlMonedas', en.CUR_ALPHA);
                    $("#txtUsuarioDerecho").val(en.SOCIO);
                    loadTipoFactura('ddlTipoFactNC', '3');
                    $("#ddlTipoFactNC").prop('disabled', true);

                    var d1 = $("#txtFechaV").data("kendoDatePicker");
                    var valFecha = formatJSONDate(en.INV_DATE);
                    d1.value(valFecha);

                    var d1 = $("#txtFechaE").data("kendoDatePicker");
                    var valFecha = formatJSONDate(en.INV_EXPDATE);
                    d1.value(valFecha);
                    estado = true;
                }
            } else if (dato.result == 2) {
                alert(dato.message);
                estado = false;
            } else if (dato.result == 0) {
                alert(dato.message);
                estado = false;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
            estado = false;
        }
    });
    return estado;
}

//SOCIO - BUSQ. GENERAL
var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    obtenerNombreSocio($("#lbResponsable").val(), 'lbResponsable');
};

function obtenerNombreSocio(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
            }
        }
    });
}

//AGENTE COMERCIAL - BUSQ. GENERAL
var reloadEventoAgente = function (idSel) {
    $("#lbAgente").val(idSel);
    $("#hidEdicionEntAGE").val(idSel);
    obtenerNombreSocio($("#lbAgente").val(), 'lbAgente');
};

function eliminarFactura(id, tipo,tipo_fact) { // tipo AUTO O MANUAL , TIPO_fACT BO FAC NC
    //alert("Ingreso a Anular Factura Prueba");

    var ValidaAnulacion = Validar_Anulacion_X_Modalidad(id);
    var valida = validarDocumento_Cobro(id);
    var validaFecha = Valida_Fecha_Factura_Para_NC(id);
    if (ValidaAnulacion == 0) {
        if (validaFecha == 1) {
            if (valida == 1) {
                Confirmar(' ¿Desea anular la factura?',
                       function () {
                           $("#hidIdFactA").val(id);
                           $("#hidTipoF").val(tipo);
                           $("#txtDescripcion").val('');
                           $("#hidTipoDoc").val(tipo_fact);
                           $("#mvAnulacion").dialog("open");
                       },
                       function () {
                           $("#hidIdFactA").val('');
                           $("#hidTipoF").val('');
                       },
                       'Confirmar'
                   )
            } else {
                alert("El documento tiene asignado un cobro pendiente por confirmar.")
            }

        } else {
            alert("La Fecha de emisión del documento es mayor a la fecha actual.")
        }
    } else {
        alert("Usted no cuenta con permisos para anular esta factura.")
    }
    
    }
    


function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            Si: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            No: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}

var addObsAnulacionfact = function () {
    //Preguntar si Tienen los mismos valores 
    var id = $("#hidIdFactA").val();
    var obs = $("#txtDescripcion").val();
    var tipoF = $("#hidTipoF").val();
    var tipoDoc = $("#hidTipoDoc").val();

    if (obs == '') {
        alert('Ingrese el motivo de la anulación.');
    } else {
        Confirmar(' ¿Confirmas anular la factura?',
                            function () {

                                $.ajax({
                                    data: { id: id, observacion: obs, tipoF: tipoF, tipoDoc: tipoDoc },
                                    url: '../FacturacionConsulta/AnularFactura',
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
                                $("#hidTipoF").val('');
                                $("#mvAnulacion").dialog("close");
                            },
                            function () {
                                $("#mvAnulacion").dialog("close");
                                $("#hidIdFactA").val('');
                            },
                            'Confirmar'
                        )
    }
}

function ValidarFechaNotaCredito(idfact) {
    var estado = false;
    GlobalId = idfact;
    //alert(GlobalId);
    $.ajax({
        url: '../FacturacionConsulta/ValidarFechaNotaCredito',
        type: 'POST',
        dataType: 'JSON',
        data: { Id: idfact },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
                //alert(estado);
            } else {
                estado = false;
                //alert(estado);
            }
        }
    });
    return estado;
}

function obtenerId(id, fa, habNC, quiebra, nota_Credito) {
    //alert(habNC);
    $("#hidId").val(0);
    if(habNC === 1){
        var anulada = fa;
        var estado;
        $("#hidId").val(id);
        if (anulada == 1 || nota_Credito == 0 || quiebra > 0) {
            estado = "Anulada";
            $("#btnNotaCredito").hide();
            $("#hidId").val(id);

            if (anulada == 0 && (nota_Credito == 2 || (nota_Credito==0 && quiebra==0) )) { //-- SI SOLICITUD ES QUIEBRA Y ESTA RECHZADA PERMITE REALIZAR DE NUEVO LA SOLICITUD
                $("#btnEnviarAprobacion").show();
                $("#btnNotaCredito").hide();
            }
            else if (anulada == 0 && nota_Credito == 1 && quiebra > 0) { //SI ES NOTA DE CREDITO Y ESTA APROBADO PERMITE REALIZAR NC
                $("#btnEnviarAprobacion").hide();
                $("#btnNotaCredito").show();
            }
            else {
                alert("No se puede realizar una NC o Solicitud de Aprobacion");
            }

        } else
        {
            $("#btnNotaCredito").show();
        } 
    } else {
        $("#hidId").val(0);
        $("#btnNotaCredito").hide();
    }
}

function obtenerIdDetalle(id) {
    $("#hidIdDetalleFactura").val(id);
    var id = $("#hidIdRecibo").val();
    //alert("Id: " + id);
}

$('body').on('keyup', '.elm', function () {
    var sum = 0;
    var d = 0;
    $('.elm').each(function () {
        if ($(this).val() != '' && !isNaN($(this).val())) {
            sum += parseFloat($(this).val());
        }
    });
    //alert(sum);
    //$('#txtTotal').val(sum);
})

function Habilitar(id) {
    if ($("#chkFact" + id).prop("checked")) {
        $("#txtValorNotaCredito_" + id).removeAttr('disabled');
    }
    else {
        $("#txtValorNotaCredito_" + id).attr('disabled', 'disabled');
        $("#txtValorNotaCredito_" + id).val('');
    }
}

function calcularTotalBase() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumB").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtBase").val(total);
            }
        }
    });
}

function calcularTotalImpuesto() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        //var solicitado = parseFloat($(this).find("td").eq(9).html());
        var solicitado = $(this).find(".sumI").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtImpuesto").val(total);
            }
        }
    });
}

function calcularTotalNeto() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumN").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtNeto").val(total);
            }
        }
    });
}

function calcularTotalBaseCobrado() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumBC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtBaseCobrado").val(total);
            }
        }
    });
}

function calcularTotalImpuestoCobrado() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumIC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtImpuestoCobrado").val(total);
            }
        }
    });
}

function calcularTotalNetoCobrado() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumNC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtNetoCobrado").val(total);
            }
        }
    });
}

function calcularTotalPendiente() {
    var total = 0;
    $('#tblDetalleFactura tr').each(function () {
        var solicitado = $(this).find(".sumP").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtPendiente").val(total);
            }
        }
    });
}

function clickCheck() {
    //var state = $("#idCheck").is(':checked');
    //if (state == 1) {
    //    $(".Check").attr('checked', true);
    //} else {
    //    $(".Check").attr('checked', false);
    //}
    var state = $("#idCheck").is(':checked');
    if (state == 1) {
        $(".Check").attr('checked', true);
        var cantidad = CantidadFacturasSeleccionadas();
        if (cantidad > LimiteFacturaSelect) {
            $(".Check").attr('checked', false);
            $("#idCheck").prop('checked', true);
            alert('Se seleccionaran los primeros ' + LimiteFacturaSelect + ' documentos.');
            ValidadCantidadFactSelecionadas(LimiteFacturaSelect);
        }
    } else {
        $(".Check").attr('checked', false);
    }
}

function CantidadFacturasSeleccionadas() {
    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 0;
    $('#tblFacturaMasiva tr').each(function () {
        var id = $(this).find(".IDCell").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if ($('#chkFact' + id).is(':checked')) {
                cantidad += 1;
            }
        }
    });
    return cantidad;
}

function ValidadCantidadFactSelecionadas(LimiteFacturaSelect) {
    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 1;
    $('#tblFacturaMasiva tr').each(function () {
        var id = $(this).find(".IDCell").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if (cantidad <= LimiteFacturaSelect) {
                $('#chkFact' + id).prop('checked', 'true');
                cantidad += 1;
            }
        }
    });
    return cantidad;
}

function obtenerFacturasSeleccionadasAnular(tipo) {
    var ReglaValor = [];
    var contador = 0;

    $('#tblFacturaMasiva tr').each(function () {
        var id = $(this).find(".IDCell").html();
        var tipo = $(this).find(".TipCell").html();
        var actual = 0;
        if (!isNaN(id) && id != null) {
            if ($('#chkFact' + id).is(':checked')) {
                ReglaValor[contador] = {
                    INV_ID: id,
                    INVT_DESC:tipo,
                };
                contador += 1;
            }
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {

        if (tipo == 1) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '../ConsultaDocumento/ReenviarAnuladosDocumentosSeleccionados',
                data: ReglaValor,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        //var idMoneda = $('#ddlMoneda').val();                    
                        alert(dato.message);
                        ConsultaDocumento(0);
                        //BuscarFacturasBorrador(0);
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                },
                failure: function (response) {
                    alert("No se logro enviar las factura(s).");
                }
            });
        }
        else if (tipo == 2) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '../ConsultaDocumento/ReenviarDocumentosSeleccionados',
                data: ReglaValor,
                success: function (response) {
                var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        //var idMoneda = $('#ddlMoneda').val();                    
                        alert(dato.message);
                        ConsultaDocumento(0);
                            //BuscarFacturasBorrador(0);
                    } else if(dato.result == 0) {
                        alert(dato.message);
                    }
                        },
                failure: function (response) {
                alert("No se logro enviar las factura(s).");
                }
             });
        }

    } else {
        alert("Debe selecionar una factura.");
    }

}

// SUBDIVISION - BUSQ. GENERAL
var reloadEventoSubDivision = function (idSel) {
    $("#hidSubDivision").val(idSel);
    obtenerNombreConsultaSubDivision($("#hidSubDivision").val());
};

function obtenerNombreConsultaSubDivision(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../Divisiones/ObtenerValor',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbSubDivision").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function mostrarDetalleFactura(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../FacturacionConsulta/mostrarDetalleFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#div" + idSel).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function verDetaFactura(id) {
    if ($("#expand" + id).attr('src') == '../Images/botones/less.png') {
        $("#expand" + id).attr('src', '../Images/botones/more.png');
        $("#expand" + id).attr('title', 'ver detalle.');
        $("#expand" + id).attr('alt', 'ver detalle.');
        $("#div" + id).css("display", "none");
    } else {
        $("#expand" + id).attr('src', '../Images/botones/less.png');
        $("#expand" + id).attr('title', 'ocultar detalle.');
        $("#expand" + id).attr('alt', 'ocultar detalle.');
        $("#div" + id).css("display", "inline");
        mostrarDetalleFactura(id);
    }
    return false;
}

function verDetaLic(id, lic) {
    var cod = (id + '-' + lic);
    if ($("#expandLic" + cod).attr('src') == '../Images/botones/less.png') {
        $("#expandLic" + cod).attr('src', '../Images/botones/more.png');
        $("#expandLic" + cod).attr('title', 'ver detalle.');
        $("#expandLic" + cod).attr('alt', 'ver detalle.');
        $("#divLic" + cod).css("display", "none");

    } else {
        $("#expandLic" + cod).attr('src', '../Images/botones/less.png');
        $("#expandLic" + cod).attr('title', 'ocultar detalle.');
        $("#expandLic" + cod).attr('alt', 'ocultar detalle.');
        $("#divLic" + cod).css("display", "inline");
    }
    return false;
}


function loadEstadoFacturaSunat(control, valSel) {
    var K_TipoPersonaItems = [{ Text: '--SELECCIONE--', Value: 0 },
                              { Text: 'ACEPTADO', Value: 1 },
                              { Text: 'RECHAZADO', Value: 2 },
                              { Text: 'OBSERVACION / SIN RESPUESTA', Value: 3 }
    ];
    $('#' + control + ' option').remove();
    $.each(K_TipoPersonaItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}
function quiebraFactura(id) {
    Confirmar('¿Desea mandar a quiebra la factura?',
        function () {
            $("#hidIdFactA").val(id);
            //$("#hidTipoF").val(tipo);
            $("#txtQuiebra").val('');
            $("#mvQuiebra").dialog("open");
        },
               function () {
                   $("#hidIdFactA").val('');
                   //$("#hidTipoF").val('');
               },
               'Confirmar'
        )
}
var addObsQuiebrafact = function () {
    //Preguntar si Tienen los mismos valores 
    var id = $("#hidIdFactA").val();
    var obs = $("#txtQuiebra").val();
    var tipoF = $("#hidTipoF").val();

    if (obs == '') {
        alert('Ingrese el motivo de la quiebra');
    } else {
        Confirmar(' ¿Confirmas mandar a quiebra la factura?',
                            function () {

                                $.ajax({
                                    data: { id: id, observacion: obs },
                                    url: '../FacturacionConsulta/QuiebraFactura',
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
                                $("#hidTipoF").val('');
                                $("#mvQuiebra").dialog("close");
                            },
                            function () {
                                $("#mvQuiebra").dialog("close");
                                $("#hidIdFactA").val('');
                            },
                            'Confirmar'
                        )
    }
}




function ReenviarDocumentosMasivosEmision() {

    var fechaInicio = $("#txtFecInicial").val();
    var fechaFin = $("#txtFecFinal").val();
    var Oficina = $("#hidOficina").val();

    $.ajax({
        data: { fechaInicio: fechaInicio, fechaFin: fechaFin, Oficina: Oficina },
        url: '../ConsultaDocumento/ReenviarDocumentosMasivosEmision',
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

function Validar_Anulacion_X_Modalidad(idfact) {

    var retorno = 0;
    $.ajax({
        url: '../ConsultaDocumento/Validar_Anulacion_X_Modalidad',
        type: 'POST',
        dataType: 'JSON',
        data: { Inv_id: idfact },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                retorno = dato.result;
                //alert(dato.message);
            } else {
                retorno = dato.result;
            }
        }
    });
    return retorno;
}