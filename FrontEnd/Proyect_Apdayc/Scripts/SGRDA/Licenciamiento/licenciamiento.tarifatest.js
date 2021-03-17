var K_WIDTH_TT = 670;
var K_HEIGHT_TT = 430;
var K_POPUP_TEST_TARIFA = 'mvTestTarifa';
var K_TIPO_CALCULO_TT = {
    CARACTERISTICAS: 1,
    DESCUENTOS: 2
};

var K_DIV_MESSAGE_TT = {
    VAL_TARIFA: "La licencia no tiene tarifa",
    DIV_TAB_POPUP_TT: "avisoMVTestTarifa"
};
var K_MENSAJE_SIN_RESULTADO = "Con los datos ingresados no se obtuvieron valores.";
var K_MENSAJE_SEL_PERIODO = "Seleccione un periodo.";
var K_MENSAJE_SEL_PLAN = "Seleccione un planificación.";
var K_VALORES = {
    VAL_ING_DECLARADO: 0,
    VAL_CALCULO_TEST_TARIFA: 0,
    VAL_DESEA_FACTURAR: 0,
    VAL_RES_PORC_ING_DECLARADO: 0,
    VAL_RES_FINAL: 0
};

$(function () {
    $('#txtValorFacturar').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValorFacturarDescuento').on("keypress", function (e) { return solonumeros(e); });
    LimpiarTT();
    $('#' + K_POPUP_TEST_TARIFA).dialog({
        autoOpen: false,
        width: K_WIDTH_TT,
        height: K_HEIGHT_TT,
        buttons: {
        },
        modal: false,
        open: function (event, ui) {
            $('#' + K_POPUP_TEST_TARIFA).css('overflow', 'hidden');
        }
    });

    $("#btnCalculoNID").on('click', function () {
        initTestTarifa(K_TIPO_CALCULO_TT.CARACTERISTICAS);
    }).button({ icons: { secondary: "ui-icon-newwin" } });

    $("#btnCalculoTTDescuento").on("click", function () {
        initTestTarifa(K_TIPO_CALCULO_TT.DESCUENTOS);
    }).button({ icons: { secondary: "ui-icon-newwin" } });

    //Botones - Caular resultado final
    $("#btnCalTestTarifa").on("click", function () {
        CalcularTT()
    }).button({ icons: { secondary: "ui-icon-newwin" } });

    $("#btnCalcularTT").on("click", function () {
        if (validarValorFacturar('txtValorFacturar'))
            ValorFinalCaracteristicas();
    }).button({ icons: { secondary: "ui-icon-newwin" } });

    $("#btnCalcularTTDescuento").on("click", function () {
        if (validarValorFacturar('txtValorFacturarDescuento'))
            ValorFinalDescuentos();
    }).button({ icons: { secondary: "ui-icon-newwin" } });

});
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
function ObtenerTarifaHistorica(tarida, periodo, fecha) {
    var estado = 0;
    $.ajax({
        url: "../General/ObtenerTarifaHistorica",
        type: "GET",
        data: { IdTarifa: tarida, periodo: periodo, fecha: fecha },
        async:false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#hidIdTarifaPoPup").val(0);
                //$("#lblTarifaDesc").html('');
                $("#hidIdTarifaPoPup").val(obj.RATE_ID);
                //$("#lblTarifaDesc").html(obj.RATE_DESC);
                estado = 1;
            } else if (dato.result == 0) {
                alert(dato.message);
                $("#hidIdTarifaPoPup").val(0);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#hidIdTarifaPoPup").val(0);
            alert(xhr.status);
            alert(thrownError);
        }
    });
    return estado;
}

function initTestTarifa(tipocalculo) {    
    LimpiarTT();
    var idTarifaLic = $('#hidCodigoTarifa').val();
    var tariaHistorica = ObtenerTarifaHistorica(idTarifaLic, $('#ddlPerPlanFacCarac').val(), '');
    var idTarifa = $('#hidIdTarifaPoPup').val();

    if (idTarifa != 0) {
        var Tarifa = $('#lblTarifaDesc').html();
        var codLic = $('#txtCodigo').val();            

        if (tipocalculo == K_TIPO_CALCULO_TT.CARACTERISTICAS) {
            var idPL = $('#ddlPerPlanFacCarac').val();
            if (idPL != 0) {
                ObtenerDatosTT(idTarifa);
                $('#' + K_POPUP_TEST_TARIFA).dialog('open');
                $('#' + K_POPUP_TEST_TARIFA).dialog('option', 'title', 'ASISTENTE PARA OBTENER EL MONTO A FACTURAR');                
                $('#tblTTCaracteristicas').show();
                $('#tblTTDescuentos').hide();                
                var periodo = $("#ddlPerPlanFacCarac option:selected").text();
                
                $('#lblTitulo1').html('Usar esta opción cuando el monto a facturar no es el monto del calculo de una tarifa.');
                $('#lblTitulo2').html('Ingrese caracteristicas de un periodo anterior al seleccionado : ');
                $('#lblPeriodoTT').html(periodo);
                $('#lblPeriodoTT2').html(periodo);
                
                $('#lbTarifa').html(Tarifa);
                $('#hidTipoTT').val(tipocalculo);
                $('#hidCodLic').val(codLic);
                $('#hidIdPL').val(idPL);                
            } else {
                alert(K_MENSAJE_SEL_PERIODO);
            }

        } else if (tipocalculo == K_TIPO_CALCULO_TT.DESCUENTOS) {
            var idPL = $('#ddlPerPlanFacDesc').val();
            if (idPL != 0) {
                ObtenerDatosTT(idTarifa);
                $('#' + K_POPUP_TEST_TARIFA).dialog('open');
                $('#' + K_POPUP_TEST_TARIFA).dialog('option', 'title', 'ASISTENTE PARA OBTENER EL DESCUENTO');
                $('#tblTTCaracteristicas').hide();
                $('#tblTTDescuentos').show();
                var periodo = $("#ddlPerPlanFacDesc option:selected").text();

                $('#lblTitulo1').html('');
                $('#lblTitulo2').html('Ingrese caracteristicas de un periodo');
                $('#lblPeriodoTT').html(periodo);
                $('#lbTarifa').html(Tarifa);
                $('#hidTipoTT').val(tipocalculo);
                $('#hidCodLic').val(codLic);
                $('#hidIdPL').val(idPL);
                //$('#lblPeriodoTTDescuento').html(periodo);
            } else {
                alert(K_MENSAJE_SEL_PLAN);
            }
        }

    }else {
        $('#' + K_POPUP_TEST_TARIFA).dialog('option', 'title', '');
        $('#tblTTCaracteristicas').hide();
        $('#tblTTDescuentos').hide();
        alert(K_DIV_MESSAGE_TT.VAL_TARIFA);
    }
}

function ObtenerDatosTT(id) {
    $.ajax({
        url: "../TarifaTest/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                validarRedirect(req);
                if (req != null) {
                    //$("#txtTarifa").val(req.RATE_DESC);
                    //$("#txtProducto").val(req.MOD_DEC);
                    //$("#txtPeriodocidad").val(req.RAT_FDESC);
                    $("#txtVum").val(req.VUM);
                    var tipo = $('#hidTipoTT').val();
                    var codLic = $('#hidCodLic').val();
                    var idPL = $('#hidIdPL').val();
                    loadDataCaracteristicaTT(tipo, codLic, idPL);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataCaracteristicaTT(tipoCalculo, codigoLic, codigoLicPlan) {

    loadDataCaracteristicaGridTmpTT('../TarifaTest/ListarCaracteristicaTTLicencia', "#gridCaracteristicaTT", tipoCalculo, codigoLic, codigoLicPlan);
}

function loadDataCaracteristicaGridTmpTT(Controller, idGrilla, tipoCalculo, codigoLic, codigoLicPlan) {

    $.ajax({
        type: 'POST', url: Controller,
        data: { tipoCalculo: tipoCalculo, codigoLic: codigoLic, codigoLicPlan: codigoLicPlan },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                var cantidadRegistros = dato.Code;
                $('#txtNumVariable').val(cantidadRegistros);
                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}

function addvalidacionSoloNumeroValor() {
    $('#tblCaracteristicaTT tr').each(function () {
        var id = $(this).find("td").eq(0).html();
        if (id != null)
            $('#txt' + id).on("keypress", function (e) { return solonumeros(e); });
    });
}

function CalcularTT() {
    msgOkB(K_DIV_MESSAGE_TT.DIV_TAB_POPUP_TT, '');
    if (ValidarObligatorio(K_DIV_MESSAGE_TT.DIV_TAB_POPUP_TT, K_POPUP_TEST_TARIFA)) {
        ObtenerCaracteristicaValorTT();
    } else {
        var tipo = $('#hidTipoTT').val();
        if (tipo == K_TIPO_CALCULO_TT.CARACTERISTICAS) {            
            $('#txtValorFacturar').val('');
            $('#txtValorFinal').val('');
        } else if (tipo == K_TIPO_CALCULO_TT.DESCUENTOS) {
            $('#txtValor').val('');
        }
    }
}

function ObtenerCaracteristicaValorTT() {
    var CaracteristicaValor = [];
    var contador = 0;
    $('#tblCaracteristicaTT tr').each(function () {

        var letra = $(this).find("td").eq(0).html();
        if (letra !== null) {
            //formato moneda
            var valor = quitarformatoMoneda($('#txt' + letra).val());
            CaracteristicaValor[contador] = {
                Letra: letra,
                Valor: valor
            };
            contador += 1;
        }
    });

    var CaracteristicaValor = JSON.stringify({ 'CaracteristicaValor': CaracteristicaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TarifaTest/ObtenerCaracteristicaValor',
        data: CaracteristicaValor,
        success: function () {
            CalcularTarifaTT();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function CalcularTarifaTT() {
    $.ajax({
        url: "../TarifaTest/Calcular",
        type: 'POST',
        data: { VUMcalcular: $('#txtVum').val() },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    var formula = parseFloat(req.ValorFormula);
                    var minimo = parseFloat(req.ValorMinimo);
                    var valorTarifa = 0;
                    if (minimo >= formula)
                        valorTarifa = minimo.toFixed(2);
                    else
                        valorTarifa = formula.toFixed(2);
                    var val = formatoMoneda(valorTarifa);
                    $('#txtValor').val(val);

                    var tipoCalculoTT = $('#hidTipoTT').val();
                    if (tipoCalculoTT == K_TIPO_CALCULO_TT.CARACTERISTICAS) {
                        $('#txtValorFacturar').prop('disabled', false);
                        $('#txtValorFinal').prop('disabled', false);
                        //ValorFinalCaracteristicas();
                    } else if (tipoCalculoTT == K_TIPO_CALCULO_TT.DESCUENTOS) {
                        $('#txtValorFacturarDescuento').prop('disabled', false);
                        $('#txtValorFinalDescuento').prop('disabled', false);
                        //ValorFinalDescuentos();
                    }                   

                }
            } else if (dato.result == 0) {
                $('#txtValor').val('');
                alert(K_MENSAJE_SIN_RESULTADO);
            }
        }
    });
}

function ValorFinalCaracteristicas() {
    var ingresoDeclarado = obtenerValorIngresoDeclarado();
    var cien = parseFloat(100.00);
    K_VALORES.VAL_ING_DECLARADO = parseFloat(quitarformatoMoneda(ingresoDeclarado)); // X
    K_VALORES.VAL_CALCULO_TEST_TARIFA = parseFloat(quitarformatoMoneda($('#txtValor').val()));// Y
    K_VALORES.VAL_DESEA_FACTURAR = parseFloat(quitarformatoMoneda($('#txtValorFacturar').val()));// Z  
    K_VALORES.VAL_RES_PORC_ING_DECLARADO = ((K_VALORES.VAL_DESEA_FACTURAR * cien) / K_VALORES.VAL_CALCULO_TEST_TARIFA); // R
    K_VALORES.VAL_RES_FINAL = ((K_VALORES.VAL_ING_DECLARADO * K_VALORES.VAL_RES_PORC_ING_DECLARADO) / cien) // RF

    var valFinal = formatoMoneda(K_VALORES.VAL_RES_FINAL);
    $('#txtValorFinal').val(valFinal);
}

function ValorFinalDescuentos() {
    var calcTestTarifa =   parseFloat(quitarformatoMoneda($('#txtValor').val()));
    var valDeseafacturar = parseFloat(quitarformatoMoneda($('#txtValorFacturarDescuento').val()));
    if (validarDescuento(calcTestTarifa, valDeseafacturar)) {
        var ingresoDeclarado = obtenerValorIngresoDeclarado();
        var cien = parseFloat(100.00);
        K_VALORES.VAL_ING_DECLARADO = parseFloat(quitarformatoMoneda(ingresoDeclarado)); // X
        K_VALORES.VAL_CALCULO_TEST_TARIFA = calcTestTarifa;// Y
        K_VALORES.VAL_DESEA_FACTURAR = valDeseafacturar; // Z
        K_VALORES.VAL_RES_FINAL = K_VALORES.VAL_CALCULO_TEST_TARIFA - K_VALORES.VAL_DESEA_FACTURAR  // RF
        var valFinalDescuento = formatoMoneda(K_VALORES.VAL_RES_FINAL);
        $('#txtValorFinalDescuento').val(valFinalDescuento);
    }
}

function LimpiarTT() {
    $('#txtValor').val('');
    $('#txtMinimo').val('');
    $('#txtNumVariable').val('');
    $('#txtVum').val('');
    $('#lbTarifa').html('');  
    msgOkB(K_DIV_MESSAGE_TT.DIV_TAB_POPUP_TT, '');
    $('#gridCaracteristicaTT').html('');
    LimpiarValores();
    $('#txtValorFacturar').val('');
    $('#txtValorFinal').val('');
    $('#txtValorFinal').prop('disabled', true);
    $('#txtValorFacturar').prop('disabled', true);
    $('#txtValorFacturar').css('border', '1px solid gray');
    $('#txtValorFinal').css('border', '1px solid gray');

    $('#txtValorFacturarDescuento').val('');
    $('#txtValorFinalDescuento').val('');
    $('#txtValorFacturarDescuento').prop('disabled', true);
    $('#txtValorFinalDescuento').prop('disabled', true);
    $('#txtValorFacturarDescuento').css('border', '1px solid gray');
    $('#txtValorFinalDescuento').css('border', '1px solid gray');

}

function LimpiarValores() {
    K_VALORES.VAL_ING_DECLARADO = 0;
    K_VALORES.VAL_CALCULO_TEST_TARIFA = 0;
    K_VALORES.VAL_DESEA_FACTURAR = 0;
    K_VALORES.VAL_RES_PORC_ING_DECLARADO = 0;
    K_VALORES.VAL_RES_FINAL = 0;
}

function formatoMoneda(valorTarifa) {
    var val = parseFloat(valorTarifa, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
    return val;
}

function quitarformatoMoneda(valorTarifa) {
    var valor = ''
    valor = valorTarifa.replace(',', '').replace(',', '').replace(',', '').replace(',', '');
    return valor;
}

function validarValorFacturar(control) {
    var estado = false;
    var val = $('#' + control).val();
    if (val === '') {
        $('#' + control).css('border', '1px solid red');
        $('#txtValorFinal').val('');
        $('#txtValorFinal').val('');
        alert('Ingrese el valor que desea facturar.');
    } else {
        $('#' + control).css('border', '1px solid gray');
        estado = true;
    }
    return estado;
}

function obtenerValorIngresoDeclarado() {
    var valorMayor = 0; var nuevoValor = 0;
    
    $('#tblCaracteristicaTT tr').each(function () {
        var letra = $(this).find("td").eq(0).html();
        if (letra !== null) {
            nuevoValor = quitarformatoMoneda($('#txt' + letra).val());
            if (parseFloat( nuevoValor) >= parseFloat( valorMayor)) 
                valorMayor = nuevoValor;
        }
    });
    return valorMayor;
}

function validarDescuento(valorTest, valorFacturar) {
    var estado = true;
    if (valorFacturar >= valorTest) {
        alert('El valor que desea facturar debe ser menor al valor del test.');
        estado = false;
    }
    return estado;
}
