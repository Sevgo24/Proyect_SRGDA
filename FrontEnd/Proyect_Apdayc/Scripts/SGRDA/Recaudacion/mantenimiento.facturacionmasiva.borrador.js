var LimiteFacturaSelect = 300;
/************************** INICIO CONSTANTES****************************************/
var K_WIDTH_OBS = 580;
var K_HEIGHT_OBS = 255;
/************************** INICIO CARGA********************************************/
var tipoDivisionADM = 'ADM'

$(function () {
    loadTipoLicencia('dllTipoLicencia', '0');
    loadTipoPago('ddlFormapago', '0');
    $('#ddlFormapago').append($("<option />", { value: '0', text: '--SELECCIONE--' }));

    limpiar();
    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $("#mvObservacion").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS,
        height: K_HEIGHT_OBS,
        buttons: {
            "Grabar": addObsAnulacionfact,
            "Cancelar": function () {
                $("#mvObservacion").dialog("close");
                $('#txtObservacion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRUPO", idDivMV: "mvBuscarGrupo", event: "reloadEventoGrupo", idLabelToSearch: "lbGrupo" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });

    //-------------------------- EVENTO BOTONES ------------------------------------    

    var moneda = (GetQueryStringParams("Moneda"));
    if (moneda === undefined) {
        loadMonedas('ddlMoneda', 'PEN');
        BuscarFacturasBorrador(moneda);
    } else {
        loadMonedas('ddlMoneda', moneda);
        BuscarFacturasBorrador(moneda);
    }
    $("#btnBuscar").on("click", function () {
        var estadoTraslape = validacionTraslape('txtFecInicial', 'txtFecFinal');
        if (estadoTraslape) {
            var estadoRequeridos = ValidarRequeridos();
            if (estadoRequeridos)
                var idSel = $("#hidCorrelativo").val();
            if (idSel != 0) {
                BuscarFacturasBorradorSerie(idSel);
            }
            else {
                BuscarFacturasBorrador();
            }
        }
    });

    $("#btnLimpiar").on("click", function () {

        Confirmar('Desea Inactivar los Borradores ?',
            function () { QuitarBorradores(); },
            function () { limpiar();},
            'Confirmar')


    });

    $("#btnSiguiente").attr("disabled", false);

    $("#btnSiguiente").on("click", function () {
        $("#btnSiguiente").attr("disabled", true);
        var estado = true;
        var idCor = $('#hidCorrelativo').val();
        var msj = '';
        if (idCor == 0) {
            estado = false;
            msj += 'Seleccione una serie.';
            $("#lbCorrelativo").css('color', 'red');
        } else
            $("#lbCorrelativo").css('color', 'black');

        if (estado) {
            obtenerFacturasBorradorSeleccionadas();
        } else {
            $("#btnSiguiente").attr("disabled", true);
            alert(msj);
        }


    });
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
            $("#idCheck").prop('checked',true);
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

// Se enviaran a facrurar en estado difinitivo
function obtenerFacturasBorradorSeleccionadas() {
    //alert("ADVERTENCIA:\n - Todos los comprobantes que no han sido seleccionados y emitidas serán quitado(s) del borrador.");
    var ReglaValor = [];
    var contador = 0;
    var idCorrelativo = $("#hidCorrelativo").val();
    var actualCorrelativo = $("#hidActual").val();

    $('#tblFacturaMasiva tr').each(function () {
        var id = $(this).find(".IDCell").html();
        var ident = $(this).find(".IDENTIFICADORCell").html();
        var actual = 0;
        if (!isNaN(id) && id != null && ident != null && ident == 'F') {
            if ($('#chkFact' + id).is(':checked')) {
                actual = contador;
                ReglaValor[contador] = {
                    C: id + '-' + idCorrelativo
                    //INV_ID: id,
                    //INV_NMR: idCorrelativo,
                    //INV_NUMBER: actual
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {
        //if (contador < 401) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../FacturacionMasiva/ObtenerFacturasBorradorSeleccionadas',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //QuitarBorradores();
                    alert(dato.message + '\n' + "- Se enviaron " + dato.TotalFacturas + " factura(s) a sunat." + '\n' + "- No se enviaron " + dato.Code + " factura(s) a sunat.");
                    BuscarFacturasBorrador();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar las factura(s).");
            }
        });
        //} else {
        //    alert();
        //}
    } else {
        alert("Debe selecionar una factura para continuar el procso.");
    }

}

function QuitarBorradores()
{
    $.ajax({
        url: '../FacturacionMasiva/LimpiarBorradores',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                var moneda = (GetQueryStringParams("Moneda"));
                if (moneda === undefined) {
                    loadMonedas('ddlMoneda', 'PEN');
                    BuscarFacturasBorrador(moneda);
                } else {
                    loadMonedas('ddlMoneda', moneda);
                    BuscarFacturasBorrador(moneda);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function BuscarFacturasBorrador(moneda) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();

    var idTipoLic = $("#dllTipoLicencia").val();
    var codMoneda = '';
    if (moneda === undefined)
        var codMoneda = $("#ddlMoneda").val();
    else
        codMoneda = moneda;
    var idGF = $("#hidGrupo").val();
    var idBps = $("#hidEdicionEnt").val();
    var idCorrelativo = $('#hidCorrelativo').val();
    var idTipoPago = $('#ddlFormapago').val();
    var confecha = 1;
    $.ajax({
        url: '../FacturacionMasiva/ListarFacturacionBorrador',
        type: 'POST',
        data: {
            fini: ini, ffin: fin,
            tipoLic: idTipoLic, idMoneda: codMoneda,
            idGrufact: idGF, idBps: idBps,
            idCorrelativo: idCorrelativo,
            idTipoPago: idTipoPago,
            conFecha: confecha
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataBorradorFacturaMasiva(0);
                $("#lblNumfacturas").html(dato.Code);
                $("#btnSiguiente").attr("disabled", false);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function BuscarFacturasBorradorSerie(idSel) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var idTipoLic = $("#dllTipoLicencia").val();
    var codMoneda = $("#ddlMoneda").val();
    var idGF = $("#hidGrupo").val();
    var idBps = $("#hidEdicionEnt").val();
    var idTipoPago = $('#ddlFormapago').val();
    var confecha = 1;
    $.ajax({
        url: '../FacturacionMasiva/ListarFacturacionBorradorSerie',
        type: 'POST',
        data: {
            fini: ini,
            ffin: fin,
            tipoLic: idTipoLic,
            idMoneda: codMoneda,
            idGrufact: idGF,
            idBps: idBps,
            idCorrelativo: idSel,
            idTipoPago: idTipoPago,
            conFecha: confecha
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataBorradorFacturaMasiva(0);
                $("#lblNumfacturas").html(dato.Code);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataBorradorFacturaMasiva(id) {
    loadDataGridTmp('ListarBorradorFactMasivaCabecera', "#grid", id);
}

function loadDataGridTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idCaracteristica: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {

    $('#txtFecInicial').val('');
    $('#txtFecFinal').val('');

    $('#ddlMoneda').val(0);
    $('#dllTipoLicencia').val(0);
    $('#ddlFormapago').val(0);

    $("#hidEdicionEnt").val(0);
    $("#lbResponsable").html('Seleccione un socio.');

    $("#hidGrupo").val(0);
    $("#lbGrupo").html('Seleccione un grupo facturación.');

    $("#hidCorrelativo").val(0);
    $("#lbCorrelativo").html('Seleccione una serie.');
    $("#lbCorrelativo").css('color', 'black');
    $("#hidSerie").val(0);
    $("#hidActual").val(0);
    $("#grid").html('');
    loadMonedas('ddlMoneda', 'PEN');
}

function mostrarDetalleFacturaBorrador(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../FacturacionMasiva/mostrarDetalleFacturaBorrador',
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
        mostrarDetalleFacturaBorrador(id);
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

//SERIE - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
    alert("Solo se mostrarán todos los comprobantes a emitir por el tipo de documento seleccionado.\n -Si la serie es Factura: Socio de Negocio con RUC. \n -Si es Boleta: RUC y DNI");
    BuscarFacturasBorradorSerie(idSel);
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidActual").val(cor.NMR_NOW);
                //$("#hidTipo").val(cor.NMR_TYPE);
                $("#lbCorrelativo").css('color', 'black');
            }
        }
    });
}

function eliminarFactura(id) {
    Confirmar(' ¿Desea anular la factura?',
               function () {
                   $("#hidIdFact").val(id);
                   $("#txtObservacion").val('');
                   $("#mvObservacion").dialog("open");
               },
               function () {
                   $("#hidIdFact").val('');
               },
               'Confirmar'
           )
}

var addObsAnulacionfact = function () {
    var id = $("#hidIdFact").val();
    var obs = $("#txtObservacion").val();

    if (obs == '') {
        alert('Ingrese la descripción de la anulación.');
    } else {
        Confirmar(' ¿Confirmas anular la factura?',
                            function () {
                                $.ajax({
                                    data: { id: id, observacion: obs },
                                    url: '../FacturacionConsulta/AnularFactura',
                                    type: 'POST',
                                    beforeSend: function () { },
                                    success: function (response) {
                                        var dato = response;
                                        validarRedirect(dato);
                                        if (dato.result == 1) {
                                            BuscarFacturasBorrador();
                                            alert(dato.message);
                                        } else if (dato.result == 0) {
                                            alert(dato.message);
                                        }
                                    }
                                });
                                $("#mvObservacion").dialog("close");
                            },
                            function () {
                                $("#mvObservacion").dialog("close");
                                $("#hidIdFact").val('');
                            },
                            'Confirmar'
                        )
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

//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

