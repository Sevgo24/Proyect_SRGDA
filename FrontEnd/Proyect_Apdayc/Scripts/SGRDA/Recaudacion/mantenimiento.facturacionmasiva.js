var LimiteFacturaSelect = 300;
/************************** INICIO CARGA********************************************/
var tipoDivisionADM = 'ADM'
$(function () {
    kendo.culture('es-PE');
    $('#btnImprimir').hide();
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#btnImprimir").on("click", function (e) {
        window.open('../FacturacionMasiva/ReporteErroresLicencia');
    });

    //limpiar();
    //loadDivisionXTipo('ddlDivisionAdministrativa', tipoDivisionADM);
    loadMonedas('ddlMoneda', 'PEN');
    loadTipoestablecimiento('ddlTipoEstablecimiento', 0);
    loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', 0, 0);

    //loadGrupoModalidad('ddlGrupoModalidad');
    loadTipoGrupo('ddlGrupoModalidad');

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarAgente({ container: "ContenedormvBuscarAgenteComercial", idButtonToSearch: "btnBuscarAGE", idDivMV: "mvBuscarAgente", event: "reloadEventoAgente", idLabelToSearch: "lbAgente" });

    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitSubDivisiones({ container: "ContenedormvSubDivision", idButtonToSearch: "btnBuscaSubDivision", idDivMV: "mvBuscaSubDivision", event: "reloadEventoSubDivision", idLabelToSearch: "lbSubDivision" });
    mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRU", idDivMV: "MvBuscarGrupoFacturacion", event: "reloadEventoGrupoFact", idLabelToSearch: "lbGrupo" });
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#ddlTipoEstablecimiento").on("change", function () {
        var codigo = $("#ddlTipoEstablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo, 0);
    });

    $("#btnBuscar").on("click", function () {
        var estadoTraslape = validacionTraslape('txtFecInicial', 'txtFecFinal');
        if (estadoTraslape) {
            var estadoRequeridos = ValidarRequeridos();

            var idBps = $("#hidEdicionEnt").val() == "" ? 0 : $("#hidEdicionEnt").val();
            var idLic = $("#hidLicencia").val() == "" ? 0 : $("#hidLicencia").val();
            var idBpsGroup = $("#hidCodigoGrupoEmpresarial").val() == "" ? 0 : $("#hidCodigoGrupoEmpresarial").val();
            var groupfact = $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val();
            
            //var emisionMunsual = $("#chkEmiMensual").prop('checked');

            //alert(idBps + idLic + idBpsGroup + groupfact);
            //if (idBps == 0 && idLic == 0 && idBpsGroup == 0 && groupfact == 0 && emisionMunsual==false) {// es emision mensual validar
            if (idBps == 0 && idLic == 0 && idBpsGroup == 0 && groupfact == 0 ) {// es emision mensual validar
                //validar si esta en la fecha y hora para realizar su emision segun su codigo de oficina

                Confirmar('Desea Realizar una Emision Mensual (no se ha seleccionado ningun filtro) ?',
                    function () { ValidaPermisoOficinaEmisionMensual(estadoRequeridos); },
                    function () { },
                    'Confirmar');
                
            } else { // si selecciono al menos uno de los filtros de busqueda y no solo fechas 

                if (estadoRequeridos)
                    BuscarFacturasMasivas();// realiar busqueda
            }
        }
    });

    $("#btnLimpiar").on("click", function () {
        limpiarControles();
    });

    $("#btnSiguiente").on("click", function () {
    alert('ObtenerFacturasSeleccionadas')
        obtenerFacturasSeleccionadas();
    });
    //-------------------------- CARGA LISTA ------------------------------------
    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
});

//****************************  FUNCIONES ****************************
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
        var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null && ident != null && ident == 'F') {
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
        var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null && ident != null && ident == 'F') {
            if (cantidad <= LimiteFacturaSelect) {
                $('#chkFact' + id).prop('checked', 'true');
                cantidad += 1;
            }
        }
    });
    return cantidad;
}

function obtenerFacturasSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblFacturaMasiva tr').each(function () {
        var id = $(this).find(".IDCell").html();
        var ident = $(this).find(".IDENTIFICADORCell").html();

        if (!isNaN(id) && id != null && ident != null && ident == 'F') {
            if ($('#chkFact' + id).is(':checked')) {

                ReglaValor[contador] = {
                    Nro: id
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../FacturacionMasiva/ObtenerFacturasSeleccionadas',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert("Se enviaron " + contador + " factura(s) a borradore(s).");
                    document.location.href = '../FacturacionMasiva/ConsultaBorrador?Moneda=' + $("#ddlMoneda").val();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar las factura(s) a borradore(s).");
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
    }
}

function mostrarDetalleFactura(idSel, estado) {
    $.ajax({
        data: { nro: idSel, estado: estado },
        url: '../FacturacionMasiva/mostrarDetalleFactura',
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
        mostrarDetalleFactura(id, 1);
    }
    return false;
}

function verDetaLic(id) {
    if ($("#expandLic" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandLic" + id).attr('src', '../Images/botones/more.png');
        $("#expandLic" + id).attr('title', 'ver detalle.');
        $("#expandLic" + id).attr('alt', 'ver detalle.');
        $("#divLic" + id).css("display", "none");

    } else {
        $("#expandLic" + id).attr('src', '../Images/botones/less.png');
        $("#expandLic" + id).attr('title', 'ocultar detalle.');
        $("#expandLic" + id).attr('alt', 'ocultar detalle.');
        $("#divLic" + id).css("display", "inline");
    }
    return false;
}

function limpiarControles() {
    $('#txtFecInicial').val('');
    $("#ddlGrupoModalidad").val(0);
    $("#hidEdicionEnt").val(0);
    $("#lbResponsable").html('Seleccione un Socio.');
    $("#ddlTipoEstablecimiento").val(0);
    $("#chkHistorico").prop('checked', false);

    $('#txtFecFinal').val('');
    $("#ddlGrupoModalidad").val(0);
    $("#hidModalidad").val(0);
    $("#lblModalidad").html('Seleccione una modalidad.');
    $("#hidOficina").val(0);
    $("#lbOficina").html('Seleccione una Oficina.');
    $("#ddlSubtipoestablecimiento").val(0);

    $("#ddlMoneda").val(0);
    $("#hidSubDivision").val(0);
    $("#lbSubDivision").html('Seleccione una subdivisión.');
    $("#hidEdicionEntAGE").val(0);
    $("#lbAgente").html('Seleccione un Agente.');
    $("#hidLicencia").val(0);
    $("#lblLicencia").html('Seleccione una Licencia.');

    $("#lblGrupoEmpresarial").html('SELECCIONE UN GRUPO COMERCIAL');
    $("#hidCodigoGrupoEmpresarial").val(0);
    $("#hidGrupoFacturacion").val(0);
    $("#lbGrupo").html("SELECCIONE GRUPO DE FACTURACION");

}

function BuscarFacturasMasivas() {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var idMod = $("#hidModalidad").val();
    var idDad = $("#hidSubDivision").val();
    var idBps = $("#hidEdicionEnt").val();
    var idBps_age = $("#hidEdicionEntAGE").val();
    var idOfi = $("#hidOficina").val();
    var idGM = $("#ddlGrupoModalidad").val();
    var idTipoEst = $("#ddlTipoEstablecimiento").val();
    var idSubTipoEst = $("#ddlSubtipoestablecimiento").val();
    var idLic = $("#hidLicencia").val();
    var idMoneda = $("#ddlMoneda").val();
    var Historico = $('#chkHistorico').is(':checked') == true ? 1 : 0;
    //david agregando
    var idBpsGroup = $("#hidCodigoGrupoEmpresarial").val();
    var groupfact = $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val();
    var tipoFact = 1;// 1 =MASIVA , 0 = MANUAL
    var EmiMensual = $('#chkEmiMensual').is(':checked') == true ? 1 : 0;
    //alert(EmiMensual);
    $.ajax({
        url: '../FacturacionMasiva/ListarFacturaMasivaSubGrilla',
        type: 'POST',
        data: {
            fini: ini, ffin: fin,
            mogid: idGM, modId: idMod,
            dadId: idDad, bpsId: idBps,
            offId: idOfi, e_bpsId: idBps_age,
            tipoEstId: idTipoEst, subTipoEstId: idSubTipoEst,
            licId: idLic,
            monedaId: idMoneda,
            historico: Historico,
            idBpsGroup: idBpsGroup,
            groupfact: groupfact,
            tipoFact: tipoFact,
            EmiMensual: EmiMensual
        },
        beforeSend: function () { },
        success: function (response) {

            var dato = response;
            //alert(dato.Code);
            //$("#cantlic").html(dato.Code);
            //$("#cantlic").html('');
            $("#cantlic").html(dato.message).css("font-weight", "Bold");;

            validarRedirect(dato);
            if (dato.result == 1) {
                $('#btnImprimir').hide();
                loadDataFacturaMasiva(1);
            }
            else if (dato.result == 2) {
                $('#btnImprimir').show();
                loadDataFacturaMasiva(1);
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }

    });
}

function loadDataFacturaMasiva(estado) {
    loadDataGridTmp('ListarFacturaMasivaCabecera', "#grid", estado);
}

//---------------------------------------------
function loadDataGridTmp(Controller, idGrilla, estado) {
    $.ajax({
        type: 'POST', data: { estado: estado }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                //var cantidadRegistros = dato.Code;
                //$('#numVariable').val(cantidadRegistros);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
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

//LICENCIA - BUSQ. GENERAL
var reloadEventoLicencia = function (idSel) {
    $("#hidLicencia").val(idSel);
    ObtenerNombreLicencia($("#hidLicencia").val());
};

function ObtenerNombreLicencia(idSel) {
    $.ajax({
        data: { codigoLic: idSel },
        url: '../General/ObtenerNombreLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lblLicencia").html(dato.valor);
            }
        }
    });
};

//MODALIDAD - BUSQ. GENERAL
var reloadEventoMod = function (idModSel) {
    $("#lblModalidad").css('color', 'black');
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    ObtenerDatosModalidad($("#hidModalidad").val());
    var modalidad = $("#lblModalidad").val();
    $("#txtModalidad").val(modalidad);
    var estado = validarPeriodocidad();
};

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
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            }
        }
    });
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
            }
        }
    });
}

function abrirSocio(id) {
    var url = "..//Socio/Nuevo?set=" + id;
    window.open(url, "_blank");
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

function ValidaPermisoOficinaEmisionMensual(estadoRequeridos) {

    $.ajax({
        url: '../FacturacionMasiva/ValidarPermisoEmisionMensual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                if (estadoRequeridos)
                    BuscarFacturasMasivas();// realiar busqueda
            } else if (dato.result != 1) { // SI ES DIFERENTE DE 1 
                alert(dato.message);
            }
        }
    });

}