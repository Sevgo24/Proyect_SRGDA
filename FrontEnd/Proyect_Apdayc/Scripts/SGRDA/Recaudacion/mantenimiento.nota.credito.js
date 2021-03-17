var K_DIV_VALIDAR = { DIV_CAB: "divCabeceraNC" };
var K_DIV_MESSAGE = { DIV_NOTACREDITO: "divMensajeError" };
var K_MENSAJE_TIPO_NOTA_CREDITO = "Seleccione un Tipo de Nota de Credito.";

$(document).ready(function () {
    //set initial state.
    //$('#textbox1').val($(this).is(':checked'));

    //loadTipoNotaCredito('ddlTipoNotaCredito', 0);

    $('#chkmontonc').click(function () {
        if ($('#chkmontonc').is(':checked')) {
            $('#txtmontoNC').prop('readonly', false);
            $('#txtmontoNC').prop('disabled', false);
            //al Selecccionar si el monto cobrado es 0 entonces obtener el valor del monto Neto


            ActivartodoslosCheckbox();

        } else {
            $('#txtmontoNC').text = '';
            $('#txtmontoNC').attr('disabled', true);
            DesactivartodoslosCheckBox();

        }
    });


    $("#mvObservacion").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS,
        height: K_HEIGHT_OBS,
        buttons: {
            "Grabar": GuardarObs,
            "Cancelar": function () {
                $('#ddlTipoNotaCredito').val(0);
                $("#mvObservacion").dialog("close");
                $('#txtObservacion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    $('#txtmontoNC').val('');
    
});

var mvInitNotaCredito = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdViewNC" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventNC" value="' + parametro.event + ' "/>';
    elemento += '<table border=0 style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td><div class="contenedor" id="divCabeceraNC"><table border=0 style=" width:65%; "><tr>';
    elemento += '<td style="width:80px">Tipo </td> <td style="width:10px"><select id="ddlTipoFactNC" style="width: 190px" class="requeridoLst"/></td>';
    elemento += '<input type="hidden" id="hidId" />';

    elemento += '<td> Serie NC: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarSerieNC" style="cursor:pointer;" alt="Búsqueda de Serie Nota Crédito" title="Búsqueda de Serie Nota Crédito"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdSerieNC"/> <label id="lblSer" style="cursor:pointer;" alt="Búsqueda de Serie Nota Crédito" title="Búsqueda de Serie Nota Crédito">Serie</label> </td>';
    elemento += '<td> - <input type="text" id="txtCorrelativoNC" style="width:60px; text-align:center;" readonly="true" class="requerido"></td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:40px">Serie </td> <td><input type="text" id="txtSerie" style="width:100px" disabled="disabled"></td>';
    elemento += '<td style="width:40px">Número </td> <td><input type="text" id="txtNumero" style="width:123px" disabled="disabled"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:60px">Usuario Derecho </td> <td><input type="text" id="txtUsuarioDerecho" style="width:300px" disabled="disabled"></td>';
    elemento += '<td style="width:60px">Moneda </td> <td style="width:7px"><select id="ddlMonedas" style="width: 190px" disabled="disabled"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:80px">Fecha Vencimiento </td> <td><input type="text" id="txtFechaV" disabled="disabled"></td>';
    elemento += '<td style="width:80px">Fecha Emisión </td> <td><input type="text" id="txtFechaE" disabled="disabled"></td>';
    elemento += '</tr>';
    //Check box para aplicar el monto total .
    elemento += '<tr>';
    elemento += '<td style="width:105px; text-align:right;"> <input type="checkbox" id="chkmontonc" value="0"></td>';
    elemento += '<td>Monto Total<input type="text" id="txtmontoNC" readonly=true style="width:70px"></td>';
    elemento += '<tr/>';
    //------------------------------------
    elemento += '<tr>';
    elemento += '<td style="height: 30px; text-align:center" colspan="20">';
    elemento += '<div id="divMensajeError" style=" width: 100% ; vertical-align: middle; text-align:right"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</div>';

    elemento += '<table  border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td colspan="10"><div id="gridDetalleFactura"></div></td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '<table  border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td style="width:10px"></td>';
    elemento += '<td style="width:5px"></td>';
    elemento += '<td style="width:10px"></td>';
    elemento += '<td style="width:100px"></td>';
    elemento += '<td style="width:100px"></td>';
    //Periodo
    elemento += '<td style="width:55px"></td>';
    //Base
    elemento += '<td style="width:105px; text-align:center;"><input type="text" id="txtBase" readonly="true" style="width:70px"></td>';
    //Impuesto
    elemento += '<td style="width:105px; text-align:center;"><input type="text" id="txtImpuesto" readonly="true" style="width:70px"></td>';
    //Neto
    elemento += '<td style="width:105px; text-align:center;"><input type="text" id="txtNeto" readonly="true" style="width:70px"></td>';
    //BaseCobrado
    elemento += '<td style="width:130px; text-align:center;"><input type="text" id="txtBaseCobrado" readonly="true" style="width:70px"></td>';
    //ImpCobrado
    elemento += '<td style="width:150px; text-align:center;"><input type="text" id="txtImpuestoCobrado" readonly="true" style="width:70px"></td>';
    //NetoCobrado
    elemento += '<td style="width:110px; text-align:center;"><input type="text" id="txtNetoCobrado" readonly="true" style="width:70px"></td>';
    //Pendiente
    elemento += '<td style="width:130px; text-align:center;"><input type="text" id="txtPendiente" readonly="true" style="width:70px"></td>';
    elemento += '<td style="width:20px"></td>';
    elemento += '</tr>';
    elemento += '</table>';

    limpiarNotaCredito();

    $("#" + parametro.container).append(elemento);

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 1300,
        height: 550,
        title: "NOTA DE CRÉDITO",
        buttons: { "Guardar": Guardar, "Cancelar": function () { $("#mvNotaCredito").dialog("close"); } }
    });

    $("#txtFechaV").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaE").kendoDatePicker({ format: "dd/MM/yyyy" });
    loadMonedas('ddlMonedas', 'PEN');
    loadTipoFactura('ddlTipoFactNC', '3');
    
    
    //$('#ddlTipoFactNC').selected('3');
    //$("#ddlTipoFactNC").on("change", function () {
    //    alert($("#ddlTipoFactNC").val());
    //})
    //var eventoKP = "keypress";
    //$('#txtValorNotaCredito_' + id).on(eventoKP, function (e) { return solonumeros(e); });

};

var globalValorNC;
var globalValorPendiente;
var GlobalId;
function obtenerValor(id, val) {

    var valorNC = $('#txtValorNotaCredito_' + id).val();
    var ValorPediente = val;
    //Obteniendo
    globalValorNC = valorNC;
    //globalValorPendiente = ValorPediente;
    globalValorPendiente = $('#txtPendiente').val();
    //GlobalId = id;



    //if (ValorPediente != 0) {
    //    if (valorNC > ValorPediente) {
    //        alert("Ingrese valor valido. El valor ingresado en la nota de crédito no debe ser mayor al saldo pendiente.");
    //        $(".ui-button-text").hide();
    //    } else if (valorNC != 0) {
    //        alert("Ingrese valor valido. El valor ingresado debe ser mayor a 0.");
    //        $(".ui-button-text").hide();
    //    }
    //    else {
    //        $(".ui-button-text").show();
    //    }
    //} else {
    //    $(".ui-button-text").show();
    //}

    $(".ui-button-text").show();
}

function limpiarNotaCredito() {
    $("#hidIdSerieNC").val(0);
    $("#lblSer").html("Serie");
    $("#txtCorrelativoNC").val("");
    $("#ddlTipoFactNC").val(0);
    $('#ddlTipoFactNC').css({ 'border': '1px solid gray' });
    $('#txtCorrelativoNC').css({ 'border': '1px solid gray' });
    //DESACTIVANDO EL CHECK
    $("#chkmontonc").prop("checked", false);
    $("#txtmontoNC").val("");
};

function Guardar() {

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_NOTACREDITO, K_DIV_VALIDAR.DIV_CAB)) {
        $('#ddlTipoNotaCredito').val(0);
        $("#txtObservacion").val('');
        $("#mvObservacion").dialog("open");
    };
};

var GuardarObs = function () {
    var id = $("#hidId").val();
    var IdNota = $("#ddlTipoNotaCredito").val();
    var Obs = $("#txtObservacion").val();

    if (IdNota == 0 || Obs == '') {
        alert('Ingrese el tipo y/o la descripción de la anulación.');
    }
    else {
        Confirmar(' ¿Confirma en emitir la nota de crédito?',
                function () {

                    var r = ValidaNCDocumento();
                    if (r == 1) {
                        //Recuperar el valor total de los VALORESDENOTA DE CREDITO -*
                        var detalleFactura = [];
                        var contador = 0;
                        var acum = parseFloat(0);
                        $('#tblDetalleFactura tr').each(function () {
                            var id = parseFloat($(this).find("td").eq(2).html());
                            if (!isNaN(id)) {
                                if ($('#chkFact' + id).is(':checked')) {
                                    detalleFactura[contador] = {
                                        Id: $('#txtDetalleId_' + id).val(),
                                        codFactura: $('#txtFacturaId_' + id).val(),
                                        ValorNotaCredito: $('#txtValorNotaCredito_' + id).val()

                                    };
                                    acum = acum + parseFloat(detalleFactura[contador].ValorNotaCredito);
                                    contador += 1;
                                }
                            }
                        });
                        //-***al entrar en cancelar todo no se obtiene el valor pendiente
                        globalValorPendiente = $('#txtPendiente').val();
                        globalValorNC = parseFloat(acum);
                        var NETO = $("#txtNeto").val();
                        var COBRADO = $("#txtNetoCobrado").val();
                        //alert(globalValorPendiente);
                        //alert(globalValorNC);
                        //Valida si el valor de la factura es igual al valor de la nota de credito  if (globalValorPendiente == globalValorNC) {
                        if (globalValorPendiente == globalValorNC || (globalValorNC == COBRADO)) { //SI SE CUMPLE LO SEGUNDO ES POR QUE LA FACT YA ESTA CANCELADA || igual a 0 por si es q ke esta aplicando dsct a una fact canc NETO == COBRADO 
                            //alert('act oficina');
                            //addObsAnulacionfactNota();
                            //ObtenerTipoSerieFactura($("#ddlTipoFactNC").val(), $("#hidIdSerieNC").val(), $("#txtCorrelativoNC").val());
                            addObsFactCanceladaSinAnulacionfact();
                        } else {
                            ObtenerTipoSerieFactura($("#ddlTipoFactNC").val(), $("#hidIdSerieNC").val(), $("#txtCorrelativoNC").val());
                            //alert('NO');
                            //AddObs(obs)
                            //addObsFactCanceladaSinAnulacionfact2();
                            //Si agrego nota de Credito Se debe 
                        }
                    } else {
                        alert("LA SERIE DE NOTA DE CREDITO SELECCIONADA NO SE LE PUEDE APLICAR AL TIPO DE DOCUMENTO | ES ELECTRONICO O MANUAL | BOLETA O FACTURA ");
                    }

                },
                function () {
                    //alert("NO " + $("#hidId").val());
                },
        'Confirmar'
        )
    }
}

function AddObs(id, obs) {
    $.ajax({
        data: { id: id, observacion: obs },
        url: '../FacturacionConsulta/ObsNotaCredito',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                ConsultaDocumento();
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    $("#mvObservacion").dialog("close");
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

function ObtenerTipoSerieFactura(tipo, serieId, correltv) {
    var idFact = $("#hidId").val();
    $.ajax({
        data: { Tipo: tipo, IdSerie: serieId, Correlativo: correltv },
        url: '../FacturacionConsulta/ObtenerTipoSerieFactura',
        async: false,
        type: 'GET',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                GrabarNotaCredito(idFact);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function GrabarNotaCredito(id) {

    var IdNota = $("#ddlTipoNotaCredito").val();
    var Obs = $("#txtObservacion").val();

    var detalleFactura = [];
    var contador = 0;
    $('#tblDetalleFactura tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());
        if (!isNaN(id)) {
            if ($('#chkFact' + id).is(':checked')) {
                detalleFactura[contador] = {
                    Id: $('#txtDetalleId_' + id).val(),
                    codFactura: $('#txtFacturaId_' + id).val(),
                    ValorNotaCredito: $('#txtValorNotaCredito_' + id).val(),
                    Motivo: Obs,
                    TipoNotaCredito: IdNota,
                };
                contador += 1;
            }
        }
    });

    var detalleFactura = JSON.stringify({ 'detalleFactura': detalleFactura });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../FacturacionConsulta/GrabarNotaCredito',
        async: false,
        data: detalleFactura,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                $("#mvObservacion").dialog("close");
                $("#mvNotaCredito").dialog("close");

                //location.href = "../FacturacionConsulta/";
                ConsultaDocumento(0);
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function addvalidacionSoloNumeroValor() {
    $('#tblDetalleFactura tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());
        if (!isNaN(id)) {
            $('#txtValorNotaCredito_' + id).on("keypress", function (e) { return solonumeros(e); });
        }
    });
}

function verDeta(id) {
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
    }
    return false;
}

/*Para poder habilitar el periodo pero sin anular la factura
*/
//var addObsFactCanceladaSinAnulacionfact2 = function () {
//    var id = GlobalId;
//    $.ajax({
//        data: { id: id},
//        url: '../FacturacionConsulta/FacturaCanSinAnular',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                BuscarFacturasConsulta();
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

var addObsFactCanceladaSinAnulacionfact = function () {
    var id = $("#hidId").val();
    var IdNota = $("#ddlTipoNotaCredito").val();
    var Obs = $("#txtObservacion").val();
    var idserie=$("#hidIdSerieNC").val()
    $.ajax({
        data: { id: id, TIPO_NOT_CRE: IdNota, OBSERV: Obs ,SERIE:idserie},
        url: '../FacturacionConsulta/FacturaCanSinAnular',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#mvObservacion").dialog("close");
                $("#mvNotaCredito").dialog("close");
                $("#btnNotaCredito").hide();
                alert(dato.message);
                ConsultaDocumento();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//******Activar todos lso check box

function ActivartodoslosCheckbox() {
    var num;
    //si Pendiente es 0 entonces Quiere decir que Ya se cobro todo el neto 
    var valPendiente = $("#txtPendiente").val();
    var valCobrado = $("#txtNetoCobrado").val();
    var numPen = 14;
    var numCan = 13;

    //if ($("#txtPendiente").val() > 0) {
        //if ($("#txtNetoCobrado").val() == 0) {
            ////aparecer el button
            $(".ui-button-text").show();
            ////posicion en el detalle a obtener 8 = neto 
            num = 8;
            $("#txtmontoNC").val($("#txtNeto").val());
            $('#txtmontoNC').prop('readonly', true);
            //alert("ingreso");
        //} else {
        //    ////aparecer el button
        //    $(".ui-button-text").show();
        //    ////14 pendiente
        //    num = 14;
        //    $("#txtmontoNC").val($("#txtPendiente").val());
        //    $('#txtmontoNC').prop('readonly', true);
        //    alert(num);
        //}
    //} else {
    //    num = num = 14;
    //    $(".ui-button-text").hide();
    //    alert("Por Ahora No se  puede Realizar esta Operacion, Consulte Otra Factura");
    //}
    
    //if ($("#txtPendiente").val() > 0) {
    //    if ($("#txtNetoCobrado").val() == 0) {
    //        ////aparecer el button
    //        $(".ui-button-text").show();
    //        ////posicion en el detalle a obtener 8 = neto 
    //        num = 8;
    //        $("#txtmontoNC").val($("#txtNeto").val());
    //        $('#txtmontoNC').prop('readonly', true);
    //        alert("ingreso");
    //    } else {
    //        ////aparecer el button
    //        $(".ui-button-text").show();
    //        ////14 pendiente
    //        num = 14;
    //        $("#txtmontoNC").val($("#txtPendiente").val());
    //        $('#txtmontoNC').prop('readonly', true);
    //        alert(num);
    //    }
    //} else {
    //    num = num = 14;
    //    $(".ui-button-text").hide();
    //    alert("Por Ahora No se  puede Realizar esta Operacion, Consulte Otra Factura");
    //}



    $(".ui-button-text").show();

    //$('#tblDetalleFactura tr').each(function () {
    //    var id = parseFloat($(this).find("td").eq(2).html());
    //    var monto = parseFloat(0.00);
    //    monto = parseFloat($(this).find("td").eq(num).html());
    //    //alert(monto);
    //    if (!isNaN(id)) {
    //        $("#chkFact" + id).prop("checked", true);
    //        $("#txtValorNotaCredito_" + id).prop('disabled', false);
    //        $("#txtValorNotaCredito_" + id).val(monto);
    //    }
    //})

    $('#tblDetalleFactura tr').each(function () {
        //var id = parseFloat($(this).find("td").eq(2).html());
        var id = $(this).find(".idPl_Invoice").html();//ID FACTURA
        //var monto = parseFloat(0.00);
        //monto = parseFloat($(this).find("td").eq(num).html());

        //var montoPendiente = parseFloat($(this).find("td").eq(numPen).html());
        //var montoCancelado = parseFloat($(this).find("td").eq(numCan).html());


        //alert(monto);
        if (!isNaN(id) && id != null) {
            //alert (num);
            //alert(id);
            //alert(montoPendiente);
            //alert(montoCancelado);

            var montoPendiente = parseFloat($(this).find("td").eq(numPen).html());
            var montoCancelado = parseFloat($(this).find("td").eq(numCan).html());

            $("#chkFact" + id).prop("checked", true);
            $("#txtValorNotaCredito_" + id).prop('disabled', false);
            if (montoPendiente>0)
                $("#txtValorNotaCredito_" + id).val(montoPendiente);
            else
                $("#txtValorNotaCredito_" + id).val(montoCancelado);
        }
    })

};

//***Desactiva todos lso check box 
function DesactivartodoslosCheckBox() {
    $("#txtmontoNC").val("");
    $('#tblDetalleFactura tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());

        if (!isNaN(id)) {

            $("#chkFact" + id).prop("checked", false);
            $("#txtValorNotaCredito_" + id).prop('disabled', true);
            $("#txtValorNotaCredito_" + id).val("0");
        }
    }
    )
};



function ValidaNCDocumento() {

    var resp=0;
    var id = $("#hidId").val();
    var idserie = $("#hidIdSerieNC").val();
    $.ajax({
        data: { NMR_ID: idserie, INV_ID: id },
        url: '../FacturacionConsulta/ValidarNCaDocumentoAplicar',
        async:false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                resp = dato.result;
            } else if (dato.result == 0) {
                resp = dato.result;
            }
        }
    });

    return resp;
}