var K_POPUP_VOUCHER_ANCHO = 500;
var K_POPUP_VOUCHER_ALTO = 410;
var K_MENSAJE_VAL_VOUCHER_MONEDA = "Seleccione la moneda con la cual realizara el cobro.";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_MENSAJE_DUPLICADOVOUCHER = "El código de depósito ya existe, ingrese uno nuevo."

var K_DIV_POPUP = {
    VOUCHER: "mvVoucherBancario",
    VOUCHER_MSJ_VALIDAR: "divMsjErrorVoucher",
    INGRESO: "DivCabeceraIngreso",
    INGRESO_MSJ_VALIDAR: "divMsjValIngresoDoc",
    FACTURAS: "gridCliente",
};
// *******************************************************************************************
$(function () {
    //alert('fer');
    kendo.culture('es-PE');
    $('#txtFecDeposito').kendoDatePicker({ format: "dd/MM/yyyy" }); // FECHA DE LOS DEPOSITOS BANCARIOS
    $('#txtFecDeposito_Filtro').kendoDatePicker({ format: "dd/MM/yyyy" }); // FECHA DE LOS DEPOSITOS BANCARIOS
    $('#txtFecDeposito_Filtro_Fin').kendoDatePicker({ format: "dd/MM/yyyy" }); // FECHA DE LOS DEPOSITOS BANCARIOS
    LoadMetodoPago('ddlTipoPago', 0);
    loadBancos('ddlBanco', 0);
    Estado();
    loadMonedas('ddlMoneda', 0);

    loadBancos('ddlBancoFiltro', 0);


    $('#ddlCuenta').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    
   
    $("#ddlBanco").change(function () {
        var idBanco = $("#ddlBanco").val();
        var idMoneda = $("#ddlMoneda").val();
        $('#ddlCuenta option').remove();
        $('#ddlCuenta').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuenta', idBanco, '0', idMoneda);
    });

    $("#ddlMoneda").change(function () {
        var idBanco = $("#ddlBanco").val();
        var idMoneda = $("#ddlMoneda").val();
        var Moneda = $("#ddlMoneda :selected").text();
        $('#ddlCuenta option').remove();
        $('#ddlCuenta').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuenta', idBanco, '0', idMoneda);

        //$("#txtTipoCambio").prop('disabled', true);
        //$("#txtTipoCambio").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'rgb(235, 235, 228)');



        if (idMoneda == 'PEN') {
            $("#txtTipoCambio").prop("disabled", true);
            $("#txtTipoCambio").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'rgb(235, 235, 228)');
            $("#txtTipoCambio").val(1);
        } else {
            $("#txtTipoCambio").prop("disabled", false);
            $("#txtTipoCambio").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'white');
            $("#txtTipoCambio").val('');
        }

    });


    $('#txtTipoCambio').on("keypress", function (e) { return solonumerosDoc(e); });
    $('#txtMontoDepositoVoucher').on("keypress", function (e) { return solonumerosDoc(e); });

    var fechaActual = new Date();
    $("#txtFecDeposito").kendoDatePicker({
        max: fechaActual,
        format: "dd/MM/yyyy"
    }).closest("span.k-datepicker").width(130).val('');

    //$("#txtFecDeposito_Filtro").kendoDatePicker({
    //    max: fechaActual,
    //    format: "dd/MM/yyyy"
    //}).closest("span.k-datepicker").width(130).val('');


    //XXX PoPup - VOUCHER XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    $("#mvVoucherBancario").dialog({
        autoOpen: false, width: K_POPUP_VOUCHER_ANCHO, height: K_POPUP_VOUCHER_ALTO,
        buttons: {
            "Agregar": addDocumentoNoIdentificado,//function () { $("#mvVoucherBancario").dialog("close"); },//addNumeracion,
            "Cancelar": function () { $("#mvVoucherBancario").dialog("close"); }
        },
        modal: true
    });

    $(".addVoucher").on("click", function () { AbrirPoPupVoucherBancario(); });
    $("#Descargar").on("click", function () { DescargarPlantilla(); });

    $("#btnGrabar").on("click", function () {
        alert();
    }).button();
    //loadDataVoucherDet();
    document.getElementById("boton").disabled = true;
    $("#frmFormulario")[0].reset();

    $("#btnBucsar").on("click", function () {
        loadDataVoucherDet();
    }).button();

    //$("#btnPdf").on("click", function () {
    //    //$('#externo').attr("src", ExportarReportef('PDF'));
    //    ExportarReportef('PDF');
    //});

    $("#btnExcel").on("click", function () {
        //$("#btnExcel").attr("disabled", true);
        ExportarReportef2('EXCEL');
    });

});




function Estado() {
    $('#ddlEstadoFiltro').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    $('#ddlEstadoFiltro').append($("<option />", { value: 1, text: 'ENVIADO SAP' }));
    $('#ddlEstadoFiltro').append($("<option />", { value: 3, text: 'REVERTIDO' }));
}

// **********  FUNCOINES  *********************************************************************************
function loadDataVoucherDet() {
    loadDataVoucherGridTmp('ListarDIN', "#gridDIN");
}

function loadDataVoucherGridTmp(Controller, idGrilla) {

    
    var Id_Banco = $('#ddlBancoFiltro').val();
    var FechaDeposito_ini = $('#txtFecDeposito_Filtro').val();
    var FechaDeposito_fin = $('#txtFecDeposito_Filtro_Fin').val();
    var Estado = $('#ddlEstadoFiltro').val();


    $.ajax({
        type: 'POST',
        data: { bnk_id: Id_Banco, fecha_ini: FechaDeposito_ini, fecha_fin: FechaDeposito_fin, estado:Estado },
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
            //if (dato.Code == 1) {
            //    $("#divbtnCambiarBancoDestino").show(); $("#divbtnCambiarGrabarBancoDestino").hide();
            //} else {
            //    $("#divbtnCambiarBancoDestino").hide(); $("#divbtnCambiarGrabarBancoDestino").hide();
            //}
        },
        complete: function () {
        }
    });
}

function LimpiarPoPupVoucher() {
    $('#ddlBanco').addClass('requeridoLst');
    $('#ddlBanco').prop('disabled', false);

    $('#' + K_DIV_POPUP.VOUCHER + ' .requerido').each(function (i, elem) {
        $(elem).css({ 'border': '1px solid gray' });
        $(elem).val('');
    });
    $('#' + K_DIV_POPUP.VOUCHER + ' .requeridoLst').each(function (i, elem) {
        $(elem).css({ 'border': '1px solid gray' });
        $(elem).val(0);
    });
    $('#' + K_DIV_POPUP.VOUCHER_MSJ_VALIDAR).html('');
    $('#txtCodigoConfirmacion').val('');
    $('#txtObservacion').val('');
}

var addDocumentoNoIdentificado = function () {
    var modoVoucherEdicion = 'I';

    var valorDeposito = $('#txtMontoDepositoVoucher').val().trim();
    var validacion = ValidarObligatorio(K_DIV_POPUP.VOUCHER_MSJ_VALIDAR, K_DIV_POPUP.VOUCHER);
    if (validacion && valorDeposito == 0) {
        $('#txtMontoDepositoVoucher').css({ 'border': '1px solid red' });
        msgErrorB(K_DIV_POPUP.VOUCHER_MSJ_VALIDAR, 'El valor del deposito debe ser mayor a 0.');
    }

    if (validacion && (valorDeposito != 0 && valorDeposito != '')) {
        var ID_Tipo_Deposito = $('#ddlTipoPago').val();
        var Id_Banco = $('#ddlBanco').val();
        var Id_Moneda = $('#ddlMoneda').val();
        var Id_Cuenta = $('#ddlCuenta').val();
        var FechaDeposito = $('#txtFecDeposito').val();
        var valorDeposito = quitarformatoMoneda(valorDeposito);
        var Nro_Confirmacion = $('#txtCodigoConfirmacion').val();
        var Observacion = $('#txtObservacion').val();
        var TipoCambio = $('#txtTipoCambio').val();
        var objDNI = {
            ID_Tipo_Deposito: ID_Tipo_Deposito,
            Id_Banco: Id_Banco,
            Id_Cuenta: Id_Cuenta,
            Fecha_Deposito: FechaDeposito,
            Id_Moneda: Id_Moneda,
            Monto_Original: valorDeposito,
            Tipo_Cambio: TipoCambio,
            Nro_Confirmacion: Nro_Confirmacion,
            Observacion: Observacion,
        }


        $.ajax({
            url: '../DocumentoNoIdentificado/RegistrarDNI',
            type: 'POST',
            data: objDNI,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataVoucherDet();
                    $("#mvVoucherBancario").dialog("close");
                    msgErrorDiv("divMsjErrorVoucher", '');
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });


        ////var validarCodigoDeposito = false;
        ////if (voucher.Voucher == '')
        ////    validarCodigoDeposito = true;
        ////else
        ////    validarCodigoDeposito = validarDescripcionVoucher(idBanco, fechaDeposito, codigoConfirmacion, 0);
        ////    //validarCodigoDeposito = validarDescripcionVoucher(idBanco, fechaDeposito, codigoConfirmacion, idVoucher);

        //var validarCodigoDeposito = true;

        //if (validarCodigoDeposito) {
        //    if (modoVoucherEdicion == K_ACCION.Nuevo) {
        //        $.ajax({
        //            url: '../DocumentoNoIdentificado/RegistrarDNI',
        //            type: 'POST',
        //            data: objDNI,
        //            beforeSend: function () { },
        //            success: function (response) {
        //                var dato = response;
        //                validarRedirect(dato);
        //                if (dato.result == 1) {
        //                    //loadDataVoucherDet();
        //                    $("#mvVoucherBancario").dialog("close");
        //                    msgErrorDiv("divMsjErrorVoucher", '');
        //                } else if (dato.result == 0) {
        //                    alert('ERROR');
        //                    alert(dato.message);
        //                }
        //            }
        //        });

        //    } else if (modoVoucherEdicion == K_ACCION.Modificacion) {
        //        //$.ajax({
        //        //    url: '../BEC/ActualizarVoucherDet',
        //        //    type: 'POST',
        //        //    data: voucher,
        //        //    beforeSend: function () { },
        //        //    success: function (response) {
        //        //        var dato = response;
        //        //        validarRedirect(dato);
        //        //        if (dato.result == 1) {
        //        //            document.location.href = '../BEC/Nuevo?id=' + id + '&ver=' + ver;
        //        //        } else if (dato.result == 0) {
        //        //        }
        //        //    }
        //        //});

        //    }
        //}

    }

};

function delAddVoucherDet(idDel) {
    $.ajax({
        url: '../DocumentoNoIdentificado/EliminarDNI',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataVoucherDet();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

//XXXXXXXXXXXX  VALIDACIONES | FORMATOS  XXXXXXXXXXXXX
function AbrirPoPupVoucherBancario() {
    $("#hidIdVoucherEdit").val(K_ACCION.Nuevo);
    $("#hidIdVoucher").val(0);
    $("#txtCodigoConfirmacion").css('border', '1px solid gray');
    var idMoneda = $("#ddlMonedaDoc").val();
    if (idMoneda != '0') {
        LimpiarPoPupVoucher();
        $("#mvVoucherBancario").dialog("open");
    } else {
        alert(K_MENSAJE_VAL_VOUCHER_MONEDA);
    }
}

function solonumerosDoc(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    if (key == 46)
        return (target.value.length > 0 && target.value.indexOf(".") == -1);
    //return (key <= 12 || (key >= 48 && key <= 57) || (key >= 35 && key <= 46) || key == 0);
    return (key <= 12 || (key >= 48 && key <= 57) || key == 37 || key == 39 || key == 35 || key == 36 || key == 0);
}

function validarDescripcionVoucher(idBanco, fechaDeposito, Voucher, idVoucher) {
    var nom = $("#txtCodigoConfirmacion").val();
    if (nom != '') {
        var noExistenVoucherRep = validarDuplicadoVoucher(idBanco, fechaDeposito, Voucher, idVoucher);
        if (noExistenVoucherRep) {
            $("#txtCodigoConfirmacion").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtCodigoConfirmacion").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE_DUPLICADOVOUCHER);
            return false;
        }
    }
}

function quitarformatoMoneda(valorTarifa) {
    var valor = ''
    valor = valorTarifa.replace(',', '')
    return valor;
}

function validarDuplicadoVoucher(idBanco, fechaDeposito, Voucher, idVoucher) {
    var estado = false;

    $.ajax({
        url: '../BEC/validarDuplicadoVoucher',
        type: 'POST',
        dataType: 'JSON',
        data: { idBanco: idBanco, fechaDeposito: fechaDeposito, Voucher: Voucher, idVoucher: idVoucher },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                if (dato.Code == 0) //No existen vouchers repetidos.   
                    estado = true;
                else
                    estado = false;
            } else if (dato.result == 1) {
                alert(dato.message);
            }
        }
    });
    return estado;
}
// XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

$(document).ready(function () {
    //sobreescribimos el metodo submit para que envie la solicitud por ajax
    $("#frmFormulario").submit(function (e) {
        //esto evita que se haga la petición común, es decir evita que se refresque la pagina
        e.preventDefault();
        //FormData es necesario para el envio de archivo, 
        //y de la siguiente manera capturamos todos los elementos del formulario
        var archivoTXT = new FormData($(this)[0])
        LeerArchivo(archivoTXT);
    })
})
function LeerArchivo(archivoTXT) {
    $.ajax({
        type: "POST",
        url: '../DocumentoNoIdentificado/Cargar',
        data: archivoTXT,
        contentType: false, //importante enviar este parametro en false
        processData: false, //importante enviar este parametro en false
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadDataVoucherDet();
                alert(dato.message);
                document.getElementById("boton").disabled = true;
                $("#frmFormulario")[0].reset();
            } else if (dato.result == 0) {
                alert(dato.message);
                document.getElementById("boton").disabled = true;
                $("#frmFormulario")[0].reset();
            }
        }
    })
}

function DescargarPlantilla() {
    var url = '\\\\192.168.252.105\\Archivos\\PlantillaDocNoIdentificado\\PLANTILLA_DNI'
    window.open(url, 'Download');
}

function Validar() {
    document.getElementById("boton").disabled = false;
}



function ExportarReportef(tipo) {

    var Id_Banco = $('#ddlBancoFiltro').val();
    var FechaDeposito_ini = $('#txtFecDeposito_Filtro').val();
    var FechaDeposito_fin = $('#txtFecDeposito_Filtro_Fin').val();
    var Estado = $('#ddlEstadoFiltro').val();


    url = "../DocumentoNoIdentificado/ReporteDNI?" + "bnk_id=" + Id_Banco + "&" + "fecha_ini=" + FechaDeposito_ini + "&"
        + "fecha_fin=" + FechaDeposito_fin + "&" + "estado=" + Estado + "&" + "formato=" + tipo;
    window.open(url);
                
    
    
}

function ExportarReportef2(tipo) {
    var Id_Banco = $('#ddlBancoFiltro').val();
    var FechaDeposito_ini = $('#txtFecDeposito_Filtro').val();
    var FechaDeposito_fin = $('#txtFecDeposito_Filtro_Fin').val();
    var Estado = $('#ddlEstadoFiltro').val();


    url = "../DocumentoNoIdentificado/ReporteDNI?" + "bnk_id=" + Id_Banco + "&" + "fecha_ini=" + FechaDeposito_ini + "&"
        + "fecha_fin=" + FechaDeposito_fin + "&" + "estado=" + Estado + "&" + "formato=" + tipo;
    window.open(url);
}