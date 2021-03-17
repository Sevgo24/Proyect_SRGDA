var K_MENSAJE_MONTO = "El monto no es el correcto.";
var K_MENSAJE_MONTO_INGRESAR = "El monto debe ser diferente de cero.";
var K_MENSAJE_RECIBOS = "Debe agregar factura(s).";
var K_MENSAJE_DUPLICADOVOUCHER = "El código de depósito ya existe, ingrese uno nuevo."
var K_MENSAJE_VALIDACION_MONTOS_PAGAR = "Los montos montos a aplicar no son los correctos."
var K_POPUP_VOUCHER_ANCHO = 640;
var K_POPUP_VOUCHER_ALTO = 395;
var K_MENSAJE_VAL_TOT_FACTURA = "Ingrese Facturas.";
var K_MENSAJE_VAL_VOUCHER_MONEDA = "Seleccione la moneda con la cual realizara el cobro.";
var K_MENSAJE_CAMBIO_TIPO_MONEDA = "¿Desea modificar el tipo de moneda de los comprobantes?";
var K_MENSAJE_CAB_VAL_TOT_VOUCHER = "Ingrese los depositos.";
var K_MENSAJE_CAB_VAL_CLIENTE = "Ingrese un cliente(s) al cual se le realizara el cobro.";
var K_MENSAJE_CAB_VAL_FACT_MENOR_VOUCHER = "El monto total de las facturas es mayor al monto total de los depositos.";
var K_MENSAJE_DET_FACT = "Imgrese los montos a aplicar de las facturas correctamente.";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_CODIGO_CARPETA_DEPOSITOS_ALFRESCO = "6018d570-ea3e-4d56-8207-c1d5d2e083c4";
//var K_MENSAJE_DIFERENCIA_MONTOS = "LA DIFERENCIA DE MONTOS ENTRE LO DEPOSITADO Y LO APLICADO EXCEDE EL VALOR DE 1 SOL , POR FAVOR DE SOLICITAR EL PERMISO POR PARTE DE GENAREC";
var K_MENSAJE_DIFERENCIA_MONTOS_APLICAR = "POR FAVOR DE INGRESAR TODO EL MONTO A APLICAR , COMO MAXIMO SE PERMITE 1 SOL DE DIFERENCIA | EN CASO DE NO HABER CREADO LA BEC , POR FAVOR DE CREARLA SIN SELECCIONAR NINGUNA FACTURA ";
var K_MENSAJE_ELEGIR_TIPO_DE_REQUERIMIENTO = "POR FAVOR DE ELEGIR UN TIPO DE REQUERIMIENTO PARA SU REGISTRO";

var K_DIV_MESSAGE = {
    DIV_OFICINA: "divMensajeError",
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento",
    DIV_DIRECCION: "avisoMV"
};

var K_DIV_POPUP = {
    VOUCHER: "mvVoucherBancario",
    VOUCHER_MSJ_VALIDAR: "divMsjErrorVoucher",
    INGRESO: "DivCabeceraIngreso",
    INGRESO_MSJ_VALIDAR: "divMsjValIngresoDoc",
    FACTURAS: "gridCliente",
};

var K_COBRO_ESTADOS = { APLICADO: 1, PENDIENTE_APLICACION: 0, PARCIAL_APLICADO: 2 };
var K_COBRO_VERSION = { ANTIGUA: 'A', NUEVA: 'N' };


$(function () {

    kendo.culture('es-PE');
    LimpiarCabecera(); LimpiarTotalVoucher(); LimpiarTotalAplicarFactura(); LimpiarPoPupVoucher();
    $('#hidMonedaGridVoucher').val('0');
    $("#hidIdVoucherEdit").val(K_ACCION.Nuevo);

    //$('#hidIdVoucher').hide();

    //$('#txtFecDepositoDoc').kendoDatePicker({ format: "dd/MM/yyyy" });//FECHA DEPOSITO  QUE ESTA EN LACABECERA
    $('#txtFecDeposito').kendoDatePicker({ format: "dd/MM/yyyy" }); // FECHA DE LOS DEPOSITOS BANCARIOS


    $('#tdClienteFactura').hide();
    var Id = (GetQueryStringParams("id"));//REC_ID (V=>N)- MREC_ID(V=>A)
    var Ver = (GetQueryStringParams("ver"));
    var Modo = (GetQueryStringParams("modo"));

    $('#hidRecId').val(Id);
    $('#hidVersion').val(Ver);
    

    if (Id === undefined) {
        var fullDate = new Date(); console.log(fullDate);

        var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
        var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
        var currentDate = twoDigitDate + "/" + twoDigitMonth + "/" + fullDate.getFullYear(); console.log(currentDate);

        //var today = new Date();
        //var dd = today.getDate();
        //var mm = today.getMonth() ; //January is 0!
        //var yyyy = today.getFullYear();
        //if (dd < 10) {
        //    dd = '0' + dd
        //}
        //if (mm < 10) {
        //    mm = '0' + mm
        //}
        //today = mm + '/' + dd + '/' + yyyy;
        ////document.write(today);

        $('#txtFechaCreacion').val(currentDate);
        CargarDDLBancosCabecera();
        CargarDDLBancosVoucher();
        $('input[name=Tipo]').attr("disabled", false);
        $('#hidOpcionEdit').val(K_ACCION.Nuevo);
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#lblMRecId").html('');
        $('#hidRecId').val(0);
        $('#hidVersion').val('');
        $('#hidMonedaGridVoucher').val('0');

        $('#tdVoucher').show();
        $('#tdCliente').show();
        $('#tdVoucherEdicion').hide();
        $('#tdClienteEdicion').hide();
        $("#txtTipoCambio").val(ObtenerTipoCambioActual());
        $('#btnBuscarCorrelativoRecAux').show();
        mvInitBuscarCorrelativoReciboAux({ container: "ContenedormvBuscarCorrelativoSerieRecAux", idButtonToSearch: "btnBuscarCorrelativoRecAux", idDivMV: "mvBuscarCorrelativoRecAux", event: "reloadEventoCorrelativoRecAux", idLabelToSearch: "lbCorrelativoRecAux" });
        $('#btnGrabar').show();
        $('#txtEstadoMrec').hide();
        
    } else {
        CargarDDLBancosCabeceraEditar();
        //CargarDDLBancosCabecera();
        $('input[name=Tipo]').attr("disabled", true);
        $('#hidOpcionEdit').val(K_ACCION.Modificacion);
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#tdVoucher').hide();
        $('#tdCliente').hide();
        $('#tdVoucherEdicion').show();
        $('#tdClienteEdicion').show();

        $('#btnBuscarCorrelativoRecAux').hide();
        ObtenerDatos(Id, Ver, Modo);
        //$('#txtFecDepositoDoc').data('kendoDatePicker').enable(false);
        $('#txtEstadoMrec').show();
        $('#btninactivar').show();
        loadTipoRquerimiento('ddltiporequerimiento', 0, 6);
        loadTipoRquerimiento('ddltiporequerimientoSalt', 0, 7);
        //CargarDDLBancosCabecera();
        CargarDDLBancosVoucher();

    }

    $("#btnGrabar").on("click", function () {
        var validacion = true;
        var validacionMontos = ValidaDiferenciaMontos();
        var msjValidacionDetalles = '';

        var estadoCorrelativo = ValidarCorrelativo();
        var validarCabecera = ValidarObligatorio(K_DIV_POPUP.INGRESO_MSJ_VALIDAR, K_DIV_POPUP.INGRESO);

        var valTotalVoucher = parseFloat($("#hidMVoucher").val()).toFixed(2);
        var valTotalFactura = parseFloat($("#hidMAplicar").val()).toFixed(2);

        var rowCantidadCliente = $('#tblCliente tr').length;

        if (valTotalVoucher == 0) {
            msjValidacionDetalles += K_MENSAJE_CAB_VAL_TOT_VOUCHER + '\r\n';
            validacion = false;
        }

        if (rowCantidadCliente == 0) {
            msjValidacionDetalles += K_MENSAJE_CAB_VAL_CLIENTE + '\r\n';
            validacion = false;
        }

        if (parseFloat(valTotalFactura) > parseFloat(valTotalVoucher)) {
            msjValidacionDetalles += K_MENSAJE_CAB_VAL_FACT_MENOR_VOUCHER + '\r\n';
            validacion = false;
        }


        var validarMontoAplicar = ValidarMontosAplicarFactura();
        if (validarMontoAplicar == false) {
            msjValidacionDetalles += K_MENSAJE_DET_FACT + '\r\n';
            validacion = false;
        }

        if (estadoCorrelativo && validarCabecera) {
            if (validacionMontos) {
                if (validacion) {
                    grabarBEC();

                } else {
                    alert(msjValidacionDetalles);
                }
            } else {
                alert(K_MENSAJE_DIFERENCIA_MONTOS);
            }
        }

    }).button();


    $("#btnBuscarConsultaFactura").on("click", function () {
        $("#mvBuscarFactura").dialog("open");
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../BEC/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        location.href = "../BEC/";
    }).button();

    $("#btninactivar").on("click", function () {
        $("#mvSolicitudRequeBec").dialog("open");
        $("#lblbecid").html($("#lblMRecId").html());
    }).button();

    //$("#btnEliminar").on("click", function () {
    //    EliminarCobro(id);
    //}).button();

    //mvInitBuscarCorrelativoRecibo({ container: "ContenedormvBuscarCorrelativoSerieBec", idButtonToSearch: "btnBuscarCorrelativoBec", idDivMV: "mvBuscarCorrelativoBec", event: "reloadEventoCorrelativoBec", idLabelToSearch: "lbCorrelativoBec" });
    //mvInitBuscarCorrelativoReciboAux({ container: "ContenedormvBuscarCorrelativoSerieRecAux", idButtonToSearch: "btnBuscarCorrelativoRecAux", idDivMV: "mvBuscarCorrelativoRecAux", event: "reloadEventoCorrelativoRecAux", idLabelToSearch: "lbCorrelativoRecAux" });
    mvInitBuscarRecaudacionFacturaBec({ container: "ContenedormvBuscarConsultaFacuraBec", idButtonToSearch: "Abrir", idDivMV: "mvBuscarFacturaBec", event: "addfacturaDet", idLabelToSearch: "lbResponsable" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarClienteDoc", idButtonToSearch: "Abrir", idDivMV: "mvBuscarClienteDoc", event: "addClienteDoc", idLabelToSearch: "lbResponsable" });

    //XXX PoPup - VOUCHER XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    $("#mvVoucherBancario").dialog({
        autoOpen: false, width: K_POPUP_VOUCHER_ANCHO, height: K_POPUP_VOUCHER_ALTO,
        buttons: {
            "Guardar": addDetalleVoucherBancario,//function () { $("#mvVoucherBancario").dialog("close"); },//addNumeracion,
            "Cancelar": function () { $("#mvVoucherBancario").dialog("close"); }
        },
        modal: true
    });

    $("#mvSolicitudRequeBec").dialog({
        autoOpen: false, width: K_POPUP_VOUCHER_ANCHO, height: K_POPUP_VOUCHER_ALTO,
        buttons: {
            "Guardar": RegistrarRequerimientoBec,//function () { $("#mvVoucherBancario").dialog("close"); },//addNumeracion,
            "Cancelar": function () { $("#mvSolicitudRequeBec").dialog("close"); }
        },
        modal: true
    });

    $("#mvSolicitudRequeBecSaltar").dialog({
        autoOpen: false, width: K_POPUP_VOUCHER_ANCHO, height: K_POPUP_VOUCHER_ALTO,
        buttons: {
            "Guardar": RegistrarRequerimientoBec,//function () { $("#mvVoucherBancario").dialog("close"); },//addNumeracion,
            "Cancelar": function () { $("#mvSolicitudRequeBecSaltar").dialog("close"); }
        },
        modal: true
    });
    


    $(".addVoucher").on("click", function () { AbrirPoPupVoucherBancario(); });

    //XXX PoPup - CLIENTE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    $(".addCliente").on("click", function () {
        $("#mvBuscarClienteDoc").dialog("open");
        $("#txtRazonSocialBPS").val('');

        if ($("#gridBPS").data("kendoGrid").dataSource.data().length > 0)
            $("#gridBPS").data('kendoGrid').dataSource.data([]);
    });

    //XXX PoPup - FACTURA XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    $(".addFacturaDetalle").on("click", function () { $("#mvBuscarFactura").dialog("open"); });

    $("input:radio[name=Tipo]").click(function () {
        var tipo = $('input[name=Tipo]:checked').val();
        if (tipo == 'S') {
            validarBotonCLiente();
        } else if (tipo == 'C') {
            $('#tdCliente').show();
        }
    });

    $('#txtMontoDepositoVoucher').on("keypress", function (e) { return solonumerosDoc(e); });

    /*
    if (Modo != undefined && Modo === 'E') {
        $("#btnDescartar").button().hide();
        $("#btnNuevo").button().hide();
        $("#btnGrabar").button().hide();
        //$("#btnGrabar").prop("disabled", true);
        //$("#btnNuevo").prop("disabled", true);
    }
    */



    //document.getElementById("#btnNuevo").disabled = true;

    $("#btnCambiarBancoDestino").on("click", function () {
        $("#ddlBancoDoc").prop('disabled', false);
        $("#ddlMonedaDoc").prop('disabled', true);
        $("#ddlCuentaDoc").prop('disabled', false);
        $("#divbtnCambiarBancoDestino").hide(); $("#divbtnCambiarGrabarBancoDestino").show();

        var idBanco = $("#ddlBancoDoc").val();
        var idMoneda = $("#ddlMonedaDoc").val();
        var idCuenta = $("#ddlCuentaDoc").val();
        var Moneda = $("#ddlMonedaDoc :selected").text();
        $('#ddlCuentaDoc option').remove();
        $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuentaDoc', idBanco, idCuenta, idMoneda);
    });


    $("#btnCambiarGrabarBancoDestino").on("click", function () {
        var validarCabecera = ValidarObligatorio(K_DIV_POPUP.INGRESO_MSJ_VALIDAR, K_DIV_POPUP.INGRESO);
        if (validarCabecera) {
            $("#ddlBancoDoc").prop('disabled', true);
            $("#ddlMonedaDoc").prop('disabled', true);
            $("#ddlCuentaDoc").prop('disabled', true);
            $("#divbtnCambiarBancoDestino").show(); $("#divbtnCambiarGrabarBancoDestino").hide();
            var idCobro = $('#lblMRecId').html();
            var idBanco = $('#ddlBancoDoc').val();
            var idCuenta = $('#ddlCuentaDoc').val();
            ActualizarBancoDestino(idCobro, idBanco, idCuenta);
        }
    });


    //ver Imagen del Cobro 
    //$("#btnVerImagenCobro").on("click", function (e) {
    //    alert('Jhon');
    //    ObtenerRuta($("#hidIdCobro").val())

    //    //ShowPopUpDetalleCobro($("#hidIdCobro").val());
    //});

    //$("#btnCambiarBancoDestino").hide();
    //$("#btnCambiarGrabarBancoDestino").hide();
});

//XXX CABECERA XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
var grabarBEC = function () {
    $('#<').hide();

    var idMultiRecibo = $('#lblMRecId').html() != '' ? $('#lblMRecId').html() : 0;
    var valTipo = $('input[name=Tipo]:checked').val();
    var RserieRecibo = $("#hidCorrelativoRecAux").val();
    var MRobservacion = $("#txtObservacion").val();
    var Banco = $('#ddlBancoDoc').val();
    var Cuenta = $('#ddlCuentaDoc').val();
    //var fechaDep = $('#txtFecDepositoDoc').val();
    var tipocambio = $('#txtTipoCambio').val();
    var totaldepositos = parseFloat($('#hidMVoucher').val());
    var totalfacturas = parseFloat($('#hidMAplicar').val());

    $.ajax({
        url: '../BEC/Insertar',
        data: {
            idMultiRecibo: idMultiRecibo,
            tipo: valTipo,
            RserieRecibo: RserieRecibo,
            MRobservacion: MRobservacion,
            idBanco: Banco,
            idCuenta: Cuenta,
            //fecha: fechaDep,
            tipoCambio: tipocambio,
            totalDepositos: totaldepositos,
            totalFacturas: totalfacturas
        },
        type: 'POST',
        beforeSend: function () {
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert('activar alfresco')
                ActivarAlfresco(dato.Code);
                alert(dato.message);
                $('#btnGrabar').show();
                //alert(dato.valor);
                if (dato.valor > 0) {
                    
                    document.location.href = '../BEC/Nuevo?id=' + dato.valor + '&ver=' + 'N'
                } else {
                    document.location.href = '../BEC/';
                }
                
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
    return false;
};

var validarBotonCLiente = function () {
    var modoEdicion = $('#hidOpcionEdit').val() == K_ACCION.Modificacion ? true : false;

    if (modoEdicion) {
        $('#tdCliente').hide();
    } else {
        var rowCountCliente = $('#tblCliente tr').length;
        if (rowCountCliente > 1) {// Boton agregar cliente
            var cantidadCliente = 0;
            var valTipo = $('input[name=Tipo]:checked').val();
            $('#tblCliente tr').each(function () {
                var IdBpsTmp = $(this).find(".idBps").html();
                if (!isNaN(IdBpsTmp) && IdBpsTmp != null)
                    cantidadCliente += 1;
            });


            if (valTipo == 'C') {
                $('#tdCliente').show();
            } else if (valTipo == 'S' && cantidadCliente > 1) {
                alert('No se puede cambiar al tipo Simple porque se tiene mas de 1 cliente agregado.');
                $('#rbtCompuesto').prop('checked', true);
            } else if (valTipo == 'S' && cantidadCliente == 0) {
                $('#tdCliente').show();
            } else if (valTipo == 'S' && cantidadCliente == 1) {
                $('#tdCliente').hide();
            }


        } else {
            $('#tdCliente').show();
        }
    }
}

function CargarDDLBancosCabecera() {
    loadBancos('ddlBancoDoc', 0);
    loadMonedas('ddlMonedaDoc', 0);
    $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));

    $("#ddlBancoDoc").change(function () {
        var id = $("#ddlBancoDoc").val();
        $("#ddlMonedaDoc").val(0);
        $('#ddlCuentaDoc option').remove();
        $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    });

    $("#ddlMonedaDoc").change(function () {
        var idBanco = $("#ddlBancoDoc").val();
        var idMoneda = $("#ddlMonedaDoc").val();
        var Moneda = $("#ddlMonedaDoc :selected").text();
        $('#ddlCuentaDoc option').remove();
        $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuentaDoc', idBanco, '0', idMoneda);

        $("#txtMonedaComprobante").val(Moneda);
        $("#hidMonedaComprobante").val(idMoneda);

        var rowCount = $('#tblVoucherDetalle tr').length;
        var idMonedaGridVoucher = $("#hidMonedaGridVoucher").val();


        if (rowCount > 0 && (idMoneda != '0' && idMoneda != idMonedaGridVoucher)) {
            //ActualizarMonedaComprobante(idMoneda, Moneda);
            Confirmar(K_MENSAJE_CAMBIO_TIPO_MONEDA,
                   function () {
                       ActualizarMonedaComprobante(idMoneda, Moneda);
                   },
                   function () {
                       $("#ddlMonedaDoc").val(idMonedaGridVoucher);
                       $("#txtMonedaComprobante").val($("#ddlMonedaDoc :selected").text());
                       $("#hidMonedaComprobante").val(idMonedaGridVoucher);
                   },
                   'Confirmar'
               )

        }

    });
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

function CargarDDLBancosCabeceraEditar() {
    loadBancos('ddlBancoDoc', 0);
    $("#ddlMonedaDoc").prop('disabled', true);
    //loadMonedas('ddlMonedaDoc', 0);
    $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));

    $("#ddlBancoDoc").change(function () {
        var id = $("#ddlBancoDoc").val();
        //$("#ddlMonedaDoc").val(0);
        $('#ddlCuentaDoc option').remove();
        $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));

        var idBanco = $("#ddlBancoDoc").val();
        var idMoneda = $("#ddlMonedaDoc").val();

        var Moneda = $("#ddlMonedaDoc :selected").text();
        $('#ddlCuentaDoc option').remove();
        $('#ddlCuentaDoc').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuentaDoc', idBanco, '0', idMoneda);
    });
}

//CAMBIAR CALCULO DE MONTO  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
function calcularMontos() {
    validarBotonCLiente();

    var rowCountFactura = $('#tblDetalleFactura tr').length;
    if (rowCountFactura > 1) {
        var estado = false;
        var tipoCambio = parseFloat($('#txtTipoCambio').val());
        var totalRecibos = 0;
        var totalBase = 0;
        var totalImpuesto = 0;
        var totalRetencion = 0;
        var totalNeto = 0;
        var totalDescuento = 0;

        var totalAplicar = 0;

        //variables para validar que no se esta dejando saldo pendiente en el cobro 
        var MontoPendienteDoc = 0;
        var MontoaAplicarDoc = 0;
        var MontoDepositadoTotal = $("#txtMVoucher").val();

        $('#tblDetalleFactura tr').each(function () {
            var IdFacturaTmp = $(this).find(".tmpIdFactura").html();
            if (!isNaN(IdFacturaTmp) && IdFacturaTmp != null) {
                var Base = $(this).find(".tmpBase").html();
                var Impuesto = $(this).find(".tmpImpuesto").html();
                var Retencion = $(this).find(".tmpRetencion").html();
                var Descuento = $(this).find(".tmpDescuento").html();



                var saldo = $(this).find(".tmpFinal").html();
                var pendienteAplicacion = $(this).find(".tmpPendienteAplicacion").html();
                var montoAplicar = quitarformatoMoneda($('#txtFactMontoAplicar' + IdFacturaTmp).val());
                var estadoEnDB = $(this).find(".tmpEnDB").html();

                MontoPendienteDoc = saldo;
                MontoaAplicarDoc = montoAplicar;

                //VALIDACION MONTO MENOR
                if (estadoEnDB == false) {
                    if (parseFloat(montoAplicar) != 0 && parseFloat(montoAplicar) <= parseFloat(saldo - pendienteAplicacion)) {
                        $('#txtFactMontoAplicar' + IdFacturaTmp).css('border', '1px solid gray');
                    } else {
                        if (isNaN(montoAplicar) || montoAplicar == null || montoAplicar == '') {
                            $('#txtFactMontoAplicar' + IdFacturaTmp).val(0);
                            montoAplicar = 0;
                        }
                        $('#txtFactMontoAplicar' + IdFacturaTmp).css('border', '1px solid red');
                    }
                }

                var detalleFactura = {
                    IdFactura: IdFacturaTmp,
                    montoAplicarNuevo: montoAplicar
                };

                ActualizarFacturaDetalle(detalleFactura);
                var IdMonedaTmp = $(this).find(".tmpIdMonedaFact").html();
                if (IdMonedaTmp == '44') { //Moneda Dolar
                    montoAplicar = (parseFloat(montoAplicar) * tipoCambio);
                }

                totalAplicar = parseFloat(totalAplicar) + parseFloat(montoAplicar);
                totalRecibos += 1;
                estado = true;
            }
        });


        if (estado == true) {


            $("#txtMAplicar").val(formatoCurrency(totalAplicar));
            $("#hidMAplicar").val(totalAplicar);
            ValidaMontosAplicarSaldo(MontoDepositadoTotal, totalAplicar, MontoaAplicarDoc, MontoPendienteDoc);
        }
    } else {
        LimpiarTotalAplicarFactura();
    }
}

//XXX EDITAR BEC - VISTA XXX
function ObtenerDatos(idrec, ver, modo) {
    $.ajax({
        url: '../BEC/Obtener',
        data: { idRecibo: idrec, version: ver },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var recibo = dato.data.Data;
                loadDataVoucherDet(K_ACCION_ACTUAL);
                loadDataClienteDoc(K_ACCION_ACTUAL);
                //ActivarBoton(recibo.MREC_ID);
                //$('#txtObservacion').prop('readonly', true);
                $("#lblMRecId").html(recibo.MREC_ID);

                if (recibo.ESTADO_MULTIRECIBO == K_COBRO_ESTADOS.PENDIENTE_APLICACION)
                    $("#txtEstadoMrec").css('color', 'red');
                else if (recibo.ESTADO_MULTIRECIBO == K_COBRO_ESTADOS.APLICADO) {
                    $("#txtEstadoMrec").css('color', 'green');
                    //$("#btninactivar").hide();
                }
                else if (recibo.ESTADO_MULTIRECIBO == K_COBRO_ESTADOS.PARCIAL_APLICADO)
                    $("#txtEstadoMrec").css('color', 'rgb(245, 155, 0)');
                else if (recibo.ESTADO_MULTIRECIBO == K_COBRO_ESTADOS.APLICADO)
                    $("#txtEstadoMrec").css('color', 'green');
                else
                    $("#txtEstadoMrec").css('color', 'black');
                $("#txtEstadoMrec").val(recibo.ESTADO_MULTIRECIBO_DES);

                $("#hidCorrelativoRecAux").val(recibo.NMR_ID);
                $("#hidSerieRecAux").val(recibo.SERIAL);
                $("#lbCorrelativoRecAux").html(recibo.SERIAL);
                $("#txtObservacion").val(recibo.MREC_OBSERVATION);
                $("#lblUsuarioCrea").html(recibo.LOG_USER_CREAT);
                $("#lblFechaCrea").html(recibo.FECHA);

                //$("#txtMVoucher").val(parseFloat(recibo.MREC_TTOTAL).toFixed(2));// Total facturas.
                $("#txtMVoucher").val(formatoCurrency(recibo.MREC_TDEPOSITOS));
                $("#hidMVoucher").val(recibo.MREC_TDEPOSITOS);


                loadBancos('ddlBancoDoc', recibo.BNK_ID);
                $("#ddlBancoDoc").prop('disabled', true);
                loadCuentaBancariaXbanco('ddlCuentaDoc', recibo.BNK_ID, recibo.BACC_NUMBER);
                $("#ddlCuentaDoc").prop('disabled', true);

                if (recibo.TIPO_MONEDA == 'DOL')
                    loadMonedas('ddlMonedaDoc', '44');
                else
                    loadMonedas('ddlMonedaDoc', 'PEN');
                $("#ddlMonedaDoc").prop('disabled', true);

                if (recibo.TIPO == 'S')
                    $("#rbtSimple").prop('checked', true);
                else
                    $("#rbtCompuesto").prop('checked', true);

                //var dVencimiento = $("#txtFecDepositoDoc").data("kendoDatePicker");
                //var valFechaVencimiento = formatJSONDate(recibo.MREC_DATE);
                //dVencimiento.value(valFechaVencimiento);

                $("#txtFechaCreacion").val(recibo.FECHA);

                if (recibo.CUR_VALUE == 0)// No tiene tipo de cambio registrado.
                    $("#txtTipoCambio").val(ObtenerTipoCambioActual());
                else
                    $("#txtTipoCambio").val(parseFloat(recibo.CUR_VALUE).toFixed(3));


                if (modo != undefined && modo === 'E') {
                    $("#btnDescartar").button().hide();
                    $("#btnNuevo").button().hide();
                    $("#btnGrabar").button().hide();
                    //$("#btnGrabar").prop("disabled", true);
                    //$("#btnNuevo").prop("disabled", true);
                } else {
                    if (ver == K_COBRO_VERSION.ANTIGUA) //Si esta aplicado o es una version antigua (BEC).
                        $('#btnGrabar').hide();
                    else if (ver == K_COBRO_VERSION.NUEVA && recibo.ESTADO_MULTIRECIBO == K_COBRO_ESTADOS.APLICADO) //Si esta aplicado o es una version nueva (COBROS).
                        $('#btnGrabar').hide();
                    else
                        $('#btnGrabar').show();
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//XXXXXXXXXXXXXXXXXXXXXXXXXX DETALE DE PAGO - VOUCHER XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
function CargarDDLBancosVoucher() {
    LoadMetodoPago('ddlTipoPago', 0);
    loadBancos('ddlBanco', 0);
    //$('#ddlSucursal').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    $('#txtSucursal').val('');
    $('#hidCodigoSucursal').val('');


    $("#ddlTipoPago").change(function () {
        var idTipoPago = $("#ddlTipoPago").val();
        if (idTipoPago == 'EF') {//DEPOSITO EN EFECTIVO            
            $("#ddlBanco").val(36);
            var id = $("#ddlBanco").val();
            //LoadrSucursalesBanco('ddlSucursal', id, 36);            
            $('#txtSucursal').val('DEPOSITO EN EFECTIVO');
            $('#hidCodigoSucursal').val(36);
            $('#txtCuenta').val('DEPOSITO EN EFECTIVO');
            //loadCuentaBancariaXbanco('ddlCuenta', 36, '113');          

            $('#ddlTipoPago').css({ 'border': '1px solid gray' });
            $('#ddlBanco').prop('disabled', true).css({ 'border': '1px solid gray' }).removeClass('requeridoLst');
            //$('#ddlSucursal').prop('disabled', true).css({ 'border': '1px solid gray' }).removeClass('requeridoLst');
            $('#txtCuenta').prop("disabled", true).removeClass('requerido').val('EFECTIVO');
            $('#txtCuenta').prop("disabled", true).val('EFECTIVO');
        } else if (idTipoPago == 'TB') {//DEPOSITO EN EFECTIVO      
            $('#txtSucursal').val('BANCA POR INTERNET');
            $('#hidCodigoSucursal').val(7777);
        } else {
            $('#ddlBanco').prop('disabled', false);
            //$('#ddlSucursal').prop('disabled', false);
            $('#ddlCuenta').prop('disabled', false);
            $('#ddlBanco').addClass('requeridoLst');
            //$('#ddlSucursal').addClass('requeridoLst');
            //$('#ddlSucursal').addClass('requeridoLst');
            $('#txtCuenta').prop("disabled", false).val('');
        }
    });

    $("#ddlBanco").change(function () {
        $('#txtSucursal').val(''); $('#hidCodigoSucursal').val('');
        var id = $("#ddlBanco").val();
        //LoadrSucursalesBanco('ddlSucursal', id);        
        initAutoCompletarAgenciaXbanco("txtSucursal", "hidCodigoSucursal", id);

        var idTipoPago = $("#ddlTipoPago").val();
        if (idTipoPago == 'TB') {//DEPOSITO EN EFECTIVO      
            $('#txtSucursal').val('BANCA POR INTERNET');
            $('#hidCodigoSucursal').val(7777);
        }
    });
}

function AbrirPoPupVoucherBancario() {
    $("#hidIdVoucherEdit").val(K_ACCION.Nuevo);
    $("#hidIdVoucher").val(0);
    $("#txtVoucher").css('border', '1px solid gray');
    var idMoneda = $("#ddlMonedaDoc").val();
    if (idMoneda != '0') {
        LimpiarPoPupVoucher();
        $("#mvVoucherBancario").dialog("open");
    } else {
        alert(K_MENSAJE_VAL_VOUCHER_MONEDA);
    }
}

var addDetalleVoucherBancario = function () {
    var modoVoucherEdicion = $('#hidIdVoucherEdit').val();
    var valorDeposito = $('#txtMontoDepositoVoucher').val().trim();

    var validacion = ValidarObligatorio(K_DIV_POPUP.VOUCHER_MSJ_VALIDAR, K_DIV_POPUP.VOUCHER);
    //var validacion = true;
    if (validacion && valorDeposito == 0) {
        $('#txtMontoDepositoVoucher').css({ 'border': '1px solid red' });
        msgErrorB(K_DIV_POPUP.VOUCHER_MSJ_VALIDAR, 'El valor del deposito debe ser mayor a 0.');
    }


    if (validacion && (valorDeposito != 0 && valorDeposito != '')) {
        var idBancoTmp = $('#ddlBanco').val();
        //var idSucursalTmp = $('#ddlSucursal').val();
        var idSucursalTmp = $('#hidCodigoSucursal').val();
        var CuentaBancariaTmp = $('#txtCuenta').val();
        var fechaDepositoTmp = $('#txtFecDeposito').val();
        var voucherTmp = $('#txtVoucher').val();
        var valorDepositoTmp = quitarformatoMoneda(valorDeposito);
        var idTipoPagoTmp = $('#ddlTipoPago').val();
        var confirmacion = "S";
        var confirmacionDesc = "Sin Confirmación";
        var idMoneda = $('#hidMonedaComprobante').val();
        var MonedaDesc = $('#txtMonedaComprobante').val();
        var idVoucher = $('#hidIdVoucher').val();


        var voucher = {
            valorIngreso: valorDepositoTmp,
            fechaDeposito: fechaDepositoTmp,
            idBanco: idBancoTmp,
            idSucursal: idSucursalTmp,
            CuentaBancaria: CuentaBancariaTmp,
            Voucher: voucherTmp,
            idTipoPago: idTipoPagoTmp,

            Banco: $('#ddlBanco option:selected').text(),
            Sucursal: $('#txtSucursal').val(),
            CuentaBancaria: $('#txtCuenta').val(),
            TipoPago: $('#ddlTipoPago option:selected').text(),
            confirmacionIngreso: confirmacion,
            confirmacionIngresoDesc: confirmacionDesc,
            IdMoneda: idMoneda,
            Moneda: MonedaDesc,
            id: idVoucher
        }

        var validarCodigoDeposito = false;
        if (voucher.Voucher == '')
            validarCodigoDeposito = true;
        else
            validarCodigoDeposito = validarDescripcionVoucher(voucher.idBanco, voucher.fechaDeposito, voucher.Voucher, idVoucher);

        if (validarCodigoDeposito) {
            if (modoVoucherEdicion == K_ACCION.Nuevo) {
                $.ajax({
                    url: '../BEC/AddVoucherDet',
                    type: 'POST',
                    data: voucher,
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);
                        if (dato.result == 1) {
                            loadDataVoucherDet(K_ACCION_ACTUAL);
                            $("#hidMonedaGridVoucher").val(idMoneda);
                            $("#mvVoucherBancario").dialog("close");
                            msgErrorDiv("divMsjErrorVoucher", '');
                            $("#tdVoucher").hide();
                        } else if (dato.result == 0) {
                            //alert(dato.message);
                            $("#txtVoucher").css('border', '1px solid red');
                            msgErrorDiv("divMsjErrorVoucher", dato.message);
                        }
                    }
                });

            } else if (modoVoucherEdicion == K_ACCION.Modificacion) {
                var id = $('#hidRecId').val();
                var ver = $('#hidVersion').val();
                var MREC_ID = $('#lblMRecId').html();
                $.ajax({
                    url: '../BEC/ActualizarVoucherDet',                    
                    type: 'POST',
                    data: voucher,
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);
                        if (dato.result == 1) {                         

                            Obtener_Inv_Id(MREC_ID);
                            document.location.href = '../BEC/Nuevo?id=' + id + '&ver=' + ver;
                            //loadDataVoucherDet(K_ACCION_ACTUAL);
                            //$("#hidMonedaGridVoucher").val(idMoneda);
                            //$("#mvVoucherBancario").dialog("close");
                            //msgErrorDiv("divMsjErrorVoucher", '');
                        } else if (dato.result == 0) {
                            //alert(dato.message);
                            //$("#txtVoucher").css('border', '1px solid red');
                            //msgErrorDiv("divMsjErrorVoucher", dato.message);
                        }
                    }
                });
            }

        }

    }
};

function delAddVoucherDet(idDel) {
    $.ajax({
        url: '../BEC/DellAddVoucherDet',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataVoucherDet(K_ACCION_ACTUAL);
                $("#tdVoucher").show();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function LimpiarPoPupVoucher() {
    $('#ddlBanco').addClass('requeridoLst');
    //$('#ddlSucursal').addClass('requeridoLst');
    //$('#txtCuenta').addClass('requerido');
    $('#ddlBanco').prop('disabled', false);
    //$('#ddlSucursal').prop('disabled', false);
    $('#txtCuenta').prop('disabled', false);

    $('#' + K_DIV_POPUP.VOUCHER + ' .requerido').each(function (i, elem) {
        $(elem).css({ 'border': '1px solid gray' });
        $(elem).val('');
    });
    $('#' + K_DIV_POPUP.VOUCHER + ' .requeridoLst').each(function (i, elem) {
        $(elem).css({ 'border': '1px solid gray' });
        $(elem).val(0);
    });
    $('#' + K_DIV_POPUP.VOUCHER_MSJ_VALIDAR).html('');

    $('#txtSucursal').val('');
    $('#hidCodigoSucursal').val('');
    $('#txtVoucher').val('');
    $('#txtCuenta').val('');
}

function loadDataVoucherDet(accionActual) {
    loadDataVoucherGridTmp('ListarVoucherDet', "#gridVoucherBancario", accionActual);
}

function loadDataVoucherGridTmp(Controller, idGrilla, accionActual) {
    $.ajax({
        type: 'POST',
        data: { accion: accionActual },
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
            if (dato.Code == 1) {
                $("#divbtnCambiarBancoDestino").show(); $("#divbtnCambiarGrabarBancoDestino").hide();
                $("#btninactivar").show();
            } else {
                $("#divbtnCambiarBancoDestino").hide(); $("#divbtnCambiarGrabarBancoDestino").hide();
                $("#btninactivar").hide();
                
            }
        },
        complete: function () {
            calcularMontosVoucher(accionActual);
        }
    });
}

function calcularMontosVoucher(accionActual) {
    $('#tdClienteFactura').hide();
    if (accionActual == K_ACCION.Nuevo) {
        var rowCount = $('#tblVoucherDetalle tr').length;
        if (rowCount > 1) {
            var totalRecibosVoucher = 0;
            var montoTotalVaucher = 0;
            var tipoCambio = parseFloat($('#txtTipoCambio').val());

            $('#tblVoucherDetalle tr').each(function () {
                var IdVoucherTmp = $(this).find(".tmpIdVoucher").html();
                if (!isNaN(IdVoucherTmp) && IdVoucherTmp != null) {
                    var IdMonedaTmp = $(this).find(".tmpIdMoneda").html();
                    var MontoVoucher = $(this).find(".tmpMontoVoucher").html();

                    if (IdMonedaTmp == '44') { //Moneda Dolar
                        montoTotalVaucher = parseFloat(montoTotalVaucher) + (parseFloat(MontoVoucher) * tipoCambio);
                    }
                    else if (IdMonedaTmp == 'PEN') { //Moneda Soles
                        montoTotalVaucher = parseFloat(montoTotalVaucher) + parseFloat(MontoVoucher);
                    }
                    else {
                        montoTotalVaucher = 0;
                    }
                    totalRecibosVoucher += 1;
                }
            });

            if (totalRecibosVoucher > 0)
                $('#tdClienteFactura').show();
            $("#txtMVoucher").val(formatoCurrency(montoTotalVaucher));
            $("#hidMVoucher").val(montoTotalVaucher);

        } else {
            LimpiarTotalVoucher();
        }
    } else {
        $('#tdClienteFactura').show();
    }

}

function ActualizarMonedaComprobante(idmoneda, moneda) {
    $.ajax({
        url: '../BEC/ActualizarMonedaComprobante',
        type: 'POST',
        data: { IdMoneda: idmoneda, Moneda: moneda },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataVoucherDet(K_ACCION_ACTUAL);
                $("#hidMonedaGridVoucher").val(idmoneda);
                $("#txtMonedaComprobante").val(moneda);
                $("#hidMonedaComprobante").val(idmoneda);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            alert("No se logro actualizar los comprobantes.");
        }
    });
}

//XXXXXXXXXXXXXXXXXXXXXXXXXX CLIENTE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
var addClienteDoc = function (idSel) {
    var IdAdd = idSel;
    $.ajax({
        url: '../BEC/AddCliente',
        type: 'POST',
        data: { Id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataClienteDoc(K_ACCION_ACTUAL);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function delAddCliente(idDel) {
    $.ajax({
        url: '../BEC/delAddCliente',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataClienteDoc(K_ACCION_ACTUAL);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function loadDataClienteDoc(accionActual) {
    loadDataClienteDocGridTmp('ListarClienteDoc', "#gridCliente", accionActual);
}

function loadDataClienteDocGridTmp(Controller, idGrilla, accionActual) {
    $.ajax({
        type: 'POST',
        data: { accion: accionActual },
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        },
        complete: function () {
            calcularMontos();
            addvalidacionFacturaSoloNumero();
        }
    });
}

function addvalidacionFacturaSoloNumero() {
    //var rowCount = $('#gridFacturaConsulta tr').length;
    //if (rowCount > 0) {
    $('#tblDetalleFactura tr').each(function () {
        var id = parseFloat($(this).find(".tmpIdFactura").html());
        if (!isNaN(id)) {
            $('#txtFactMontoAplicar' + id).on("keypress", function (e) { return solonumerosDoc(e); });
        }
    });
    //}
}

//XXXXXXXXXXXXXXXXXXXXXXXXXX DETALE FACTURA XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
function AbrirPoPupAddFactura(idBps) {
    var limpiar = true;
    var idBpsAnterior = $("#hidIdSocioDocFact").val();
    limpiar = (idBpsAnterior == idBps) ? false : true;

    $("#hidIdSocioDocFact").val(idBps);
    obtenerNombreSocioX(idBps, 'lbResponsableDocFact');
    $("#mvBuscarFactura").dialog("open");
    loadDataFacturas();
    //if ($("#gridFacturaConsulta").data("kendoGrid").dataSource.data().length > 0 && limpiar) {
    //    loadDataFacturas();
    //}
    //else {
    //    alert('NO, Ingreso facturas nuevas');
    //}

}

var addfacturaDet = function (idSel) {
    var IdAdd = idSel;
    $.ajax({
        url: '../BEC/AddFacturaDet',
        type: 'POST',
        data: { Id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataClienteDoc(K_ACCION_ACTUAL);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function delAddFacturaDet(idDel) {
    $.ajax({
        url: '../BEC/DellAddFacturasDet',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //loadDataFacturaDet(0);
                $("#btnGrabar").show();//corregir david
                loadDataClienteDoc(K_ACCION_ACTUAL);
            } else if (dato.result == 0) {
                alert(dato.message);
                //alert()';
            }
        }
    });
    return false;
}

function ActualizarFacturaDetalle(detalleFactura) {
    $.ajax({
        //dataType: 'json',
        url: '../BEC/ActualizarFacturaDet',
        type: 'POST',
        data: detalleFactura,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert("Se actualizo.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            alert("No se logro enviar la factura a actualizar.");
        }
    });
}

//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// SERIE BEC - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativoBec = function (idSel) {
    $("#hidCorrelativoBec").val(idSel);
    obtenerNombreCorrelativoBec($("#hidCorrelativoBec").val());
};

function obtenerNombreCorrelativoBec(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativoBec").html(cor.NMR_SERIAL);
                $("#hidSerieBec").val(cor.NMR_SERIAL);
                $("#hidActualBec").val(cor.NMR_NOW);
                $("#lbCorrelativoBec").css('color', 'black');
            }
        }
    });
}

// SERIE RECIBO - CORRELATIVO BUSQ. GENERAL AUXILIAR
var reloadEventoCorrelativoRecAux = function (idSel) {
    $("#hidCorrelativoRecAux").val(idSel);
    obtenerNombreCorrelativoRecAux($("#hidCorrelativoRecAux").val());
};

function obtenerNombreCorrelativoRecAux(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativoRecAux").html(cor.NMR_SERIAL);
                $("#hidSerieRecAux").val(cor.NMR_SERIAL);
                $("#hidActualRecAux").val(cor.NMR_NOW);
                $("#lbCorrelativoRecAux").css('color', 'black');
            }
        }
    });
}

//XXX LIMPIAR DATOS
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
function LimpiarCabecera() {
    $("#hidCorrelativoBec").val(0);
    $("#hidSerieBec").val(0);
    $("#hidActualBec").val(0);
    $("#lbCorrelativoBec").val('Seleccione una Serie.');

    $("#hidCorrelativoRecAux").val(0);
    $("#hidSerieRecAux").val(0);
    $("#hidActualRecAux").val(0);
    $("#lbCorrelativoRecAux").val('Seleccione una Serie.');


    //$("#txtMonto").val('');
    $("#txtVoucher").val('');
    $("#txtFecDeposito").val('');
    $("#txtObservacion").val('');

    $("#txtMAplicar").val(0);
    $("#hidMAplicar").val(0);
    $("#hidMVoucher").val(0);
    $("#txtMVoucher").val(0);
    //$('#hidValidacionMontoAplicar').val(1);
}

function LimpiarTotalAplicarFactura() {
    //$("#txtTRecibos").val('');
    //$("#txtTBase").val('');
    //$("#txtTImpuesto").val('');
    //$("#txtTRetencion").val('');
    //$("#txtTNeto").val('');
    //$("#txtTDescuento").val('');
    $("#txtMAplicar").val(0);

    //$("#hidTBase").val('');
    //$("#hidTImpuesto").val('');
    //$("#hidTRetencion").val('');
    //$("#hidTDescuento").val('');
    //$("#hidTNeto").val('');
    $("#hidMAplicar").val(0);
}

function LimpiarTotalVoucher() {
    $("#hidMVoucher").val(0);
    $("#txtMVoucher").val(0);
}

//XXX FUNCIONES GENERALES
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
function formatoCurrency(valor) {
    var currency = 0;
    currency = valor.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    return currency;
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

function validarDescripcionVoucher(idBanco, fechaDeposito, Voucher, idVoucher) {
    var nom = $("#txtVoucher").val();
    if (nom != '') {
        var noExistenVoucherRep = validarDuplicadoVoucher(idBanco, fechaDeposito, Voucher, idVoucher);
        if (noExistenVoucherRep) {
            $("#txtVoucher").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtVoucher").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE_DUPLICADOVOUCHER);
            return false;
        }
    }
}

function ValidarCorrelativo() {
    var estado = false;
    //var CorrelativoBec = $("#hidCorrelativoBec").val();
    var CorrelativoRecAux = $("#hidCorrelativoRecAux").val();

    //if (CorrelativoBec != 0)
    //    estado = true;
    //else
    //    $("#lbCorrelativoBec").css("color", "red");;

    if (CorrelativoRecAux != 0)
        estado = true;
    else
        $("#lbCorrelativoRecAux").css("color", "red");;
    return estado;
}

function ObtenerTipoCambioActual() {
    var tipoCambio = 0;
    $.ajax({
        url: '../BEC/ObtenerTipoCambioActual',
        type: 'POST',
        dataType: 'JSON',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cambio = dato.data.Data;
                tipoCambio = parseFloat(cambio.CUR_VALUE).toFixed(3);
            } else {
                tipoCambio = 0;
            }
        }
    });
    return tipoCambio;
}

function addvalidacionSoloNumeroValor() {
    $('#tblMatriz tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            $('#txtMatrizTarifa' + id).on("keypress", function (e) { return solonumeros(e); });
        }
    });
}

function solonumerosDoc(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    if (key == 46)
        return (target.value.length > 0 && target.value.indexOf(".") == -1);
    //return (key <= 12 || (key >= 48 && key <= 57) || (key >= 35 && key <= 46) || key == 0);
    return (key <= 12 || (key >= 48 && key <= 57) || key == 37 || key == 39 || key == 35 || key == 36 || key == 0);
}


function ValidarMontosAplicarFactura() {
    var estadoValidacion = true;

    var rowCountFactura = $('#tblDetalleFactura tr').length;
    if (rowCountFactura > 1) {

        var totalAplicar = 0;
        $('#tblDetalleFactura tr').each(function () {
            var IdFacturaTmp = $(this).find(".tmpIdFactura").html();

            if (!isNaN(IdFacturaTmp) && IdFacturaTmp != null) {
                var saldo = $(this).find(".tmpFinal").html();
                var pendienteAplicacion = $(this).find(".tmpPendienteAplicacion").html();
                var montoAplicar = quitarformatoMoneda($('#txtFactMontoAplicar' + IdFacturaTmp).val());
                var estadoEnDB = $(this).find(".tmpEnDB").html();
                //alert(estadoEnDB);

                //validar solo a las facturas nuevas.
                if (estadoEnDB == false) {
                    //VALIDACION MONTO MENOR      
                    if (parseFloat(montoAplicar) != 0 && parseFloat(montoAplicar) <= parseFloat(saldo - pendienteAplicacion)) {
                        $('#txtFactMontoAplicar' + IdFacturaTmp).css('border', '1px solid gray');
                    } else {
                        estadoValidacion = false
                    }
                }
            }

        });
    } else {
        estadoValidacion = true;
    }

    return estadoValidacion;
}


//function validacionMoneda() {    
//    var valid = /^\d{0,4}(\.\d{0,2})?$/.test(this.value),
//       val = this.value;

//    if (!valid) {
//        console.log("Invalid input!");
//        this.value = val.substring(0, val.length - 1);
//    }
//}


//var valid = /^\d{0,10}(\.\d{0,3})?$/.test(this.value),
//        val = this.value;
//if (!valid) {
//    console.log("Invalid input!");
//    this.value = val.substring(0, val.length - 1);
//}



function delUppVoucherDet(id) {
    //$("#hidIdVoucher").val(id);
    $("#hidIdVoucherEdit").val(K_ACCION.Modificacion);
    var idMoneda = $("#ddlMonedaDoc").val();
    if (idMoneda != '0') {
        LimpiarPoPupVoucher();
        obtenerVoucher_x_Id(id);
        $("#mvVoucherBancario").dialog("open");
    } else {
        alert(K_MENSAJE_VAL_VOUCHER_MONEDA);
    }
}


function obtenerVoucher_x_Id(id) {
    $.ajax({
        url: '../BEC/ObtenerXidVoucher',
        data: { IdVoucher: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var voucher = dato.data.Data;
                $("#hidIdVoucher").val(voucher.REC_PID);
                LoadMetodoPago('ddlTipoPago', voucher.REC_PWID);
                loadBancos('ddlBanco', voucher.BNK_ID);
                initAutoCompletarAgenciaXbanco("txtSucursal", "hidCodigoSucursal", voucher.BNK_ID);
                $("#txtCuenta").val(voucher.BACC_NUMBER);
                $("#txtFecDeposito").val(voucher.FECHA_DEP);
                $("#txtVoucher").val(voucher.REC_REFERENCE);
                $("#txtMontoDepositoVoucher").val(voucher.REC_PVALUE);
                var moneda = voucher.REC_PVALUE = 'PEN' ? 'SOLES' : 'DOLARES';
                $("#txtMonedaComprobante").val(moneda);


            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//XXX BANCO CABECERA
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

function ActualizarBancoDestino(idCobro, idBanco, idCuenta) {
    var id = $('#hidRecId').val();
    var ver = $('#hidVersion').val();

    $.ajax({
        //dataType: 'json',
        //url: '../BEC/ActualizarFacturaDet',
        url: '../BEC/ActualizarBancoDestino',
        type: 'POST',
        data: { idCobro: idCobro, idBanco: idBanco, idCuenta: idCuenta },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                document.location.href = '../BEC/Nuevo?id=' + id + '&ver=' + ver;
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            alert("No se logro enviar la factura a actualizar.");
        }
    });
}

//XXX ELIMINAR COBRO
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

function EliminarCobro(idCobro) {
    $.ajax({
        url: '../BEC/EliminarCobro',
        type: 'POST',
        data: { idCobro: idCobro },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Se elimino el cobro.");
                location.href = "../BEC/";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            alert("No se logro enviar la factura a actualizar.");
        }
    });
}
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
//-----------------------------------------------------------------------------------------------------
//Alfresco
function AddVoucherAlfresco(Cod) {
    var fileInput = document.getElementById('file_upload');
    var filePath = fileInput.value;
    if (filePath != "") {
        $.ajax({
            url: '../BEC/AddDocumento',
            type: 'POST',
            data: {
                Cod: Cod
            },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    InitUploadTabDocAlfresco("file_upload", K_CODIGO_CARPETA_DEPOSITOS_ALFRESCO, 3, Cod, 0);
                    ActualizarNombreDocAlfresco(dato.Code,filePath.replace('C:\\fakepath\\',''));
                    //loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                } else {
                    alert(dato.message);
                }
            }
        });

       
    } else {
        alert('No se adjunto Imagen')
    }
}

function AddVoucherLyrics(Cod) {
        var fileInput = document.getElementById('file_upload');
        var filePath = fileInput.value;
        if (filePath != "") {
            $.ajax({
                    url: '../BEC/AddDocumento',
                    type: 'POST',
                    data: {
                            Cod: Cod
            },
                    success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    InitUploadBEC("file_upload", Cod, dato.Code);
                    //loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                } else {
                    alert(dato.message);
                    }
            }
        });
        } else {
            alert('No se adjunto Imagen')
    }




}

function ActivarAlfresco(Cod) {
    $.ajax({
            type: 'POST',
            url: '../BEC/ActivarAlfrescoCobros',
            async: false,
            success: function (response) {
            var dato = response;
            if (dato.valor == 'T') {
                AddVoucherAlfresco(Cod);
            } else {
                AddVoucherLyrics(Cod);
            }
    }


})
}
    //-----------------------------------------------------------------------------------------------------

    function ObtenerRuta(MREC_ID) {

        //var MREC_ID = $('#lblMRecId').html();
        $.ajax({
                type: 'POST',
                url: '../DepositoBancario/ValidarImagen',
                data: {
                        MREC_ID: MREC_ID
        },
                async: false,
                success: function (response) {
            var dato = response;
            if (dato.valor == '1') {
                abrirVentana(dato.message);
            } else {
                alert('No tiene imagen adjunta')
                //$("#btnVerImagenCobro").hide();

                }
        }


})
}
    function abrirVentana(url) {
        window.open(url, "_blank");
    }

function ObtenerRutaAlfresco(MREC_ID) {

  $.ajax({
                type: 'POST',
                url: '../BEC/ObtenerRutaAlfresco',
                data: {
                        MREC_ID: MREC_ID
        },
                async: false,
                success: function (response) {
            var dato = response;
            if (dato.valor == '1') {
                abrirVentana(dato.message);
            } else {
                alert('No tiene imagen adjunta')
                //$("#btnVerImagenCobro").hide();

                }
        }


})
    }

function ActualizarNombreDocAlfresco(id, name) {
 $.ajax({
                        async: false,
                        url: '../General/ActualizarNombreDoc',
                        type: 'POST',
                        data: { idDoc: id, nombre: name},
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato); /*add sysseg*/
                            //if (dato.result == 1) {

                            //} else if (dato.result == 0) {
                            //    alert(dato.message);
                            //}
                        }
                    });
}

function GrabarRequerimiento() {
    alert('Grabado');
    $("#mvSolicitudRequeBec").dialog("close");
}

function RegistrarRequerimientoBec() {

    var EST_ID = 0;
    var ID_REQ_TYPE = $("#ddltiporequerimiento").val() == 0 ? $("#ddltiporequerimientoSalt").val() : $("#ddltiporequerimiento").val();

    var RAZON = $("#txtAprobacionDesc").val() == '' ? $("#txtAprobacionBecDesc").val() : $("#txtAprobacionDesc").val();
    var ACTIVO = $("#ddltiporequerimiento").val() == 1 ? 0 : 1;
    var MONTO = 0;
    var FECHA = "";
    var INV_ID = 0;
    var LIC_ID = 0;
    var BPS_ID = 0;
    var BEC_ID = $('#lblbecid').html();
    var TIP_LIC_INACT = 0;

    //alert(ID_REQ_TYPE);
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
                    $("#mvSolicitudRequeBec").dialog("close");
                    $("#mvSolicitudRequeBecSaltar").dialog("close");
                    //$("#ddltipoAprobacion").val(entidad.TIPO);
                    $("#btnGrabar").show();

                } else {
                    alert(dato.message);
                }
            }
        });
    } else {
        alert(K_MENSAJE_ELEGIR_TIPO_DE_REQUERIMIENTO);
    }
}

function Obtener_Inv_Id(MREC_ID) {
    var fileInput = document.getElementById('file_upload');
    var filePath = fileInput.value;
    if (filePath != "") {
    $.ajax({
        async: false,
        type: 'POST',
        url: '../BEC/Obtener_Inv_id',
        data: { MREC_ID: MREC_ID },
        success: function (response) {
            var dato = response;
            Editar_ActivarAlfresco(dato.valor);
        }
    })
    }
}

function Editar_ActivarAlfresco(Cod) {
    $.ajax({
        type: 'POST',
        url: '../BEC/ActivarAlfrescoCobros',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.valor == 'T') {
                EditVoucherAlfresco(Cod);
            } else {
                EditVoucherLyrics(Cod);
            }
        }


    })
}

function EditVoucherLyrics(Cod) {
    var fileInput = document.getElementById('file_upload');
    var filePath = fileInput.value;
    if (filePath != "") {
        $.ajax({
            url: '../BEC/AddDocumento',
            async: false,
            type: 'POST',            
            data: {
                Cod: Cod
            },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    InitUploadBEC("file_upload", Cod, dato.Code);
                    //loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                } else {
                    alert(dato.message);
                }
            }
        });
    } else {
        alert('No se adjunto Imagen')
    }




}

function EditVoucherAlfresco(Cod) {
    var fileInput = document.getElementById('file_upload');
    var filePath = fileInput.value;
    if (filePath != "") {
        $.ajax({
            url: '../BEC/AddDocumento',
            async: false,
            type: 'POST',           
            data: {
                Cod: Cod
            },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    InitUploadTabDocAlfresco("file_upload", K_CODIGO_CARPETA_DEPOSITOS_ALFRESCO, 3, Cod, 0);
                    ActualizarNombreDocAlfresco(dato.Code, filePath.replace('C:\\fakepath\\',''));
                    //loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                } else {
                    alert(dato.message);
                }
            }
        });


    } 
	// else {
        // alert('No se adjunto Imagen')
    // }
}

function ValidaDiferenciaMontos() {
    var resultado = true;
    //var resultado = false;

//    var montotoal = $("#txtMVoucher").val();
//    var montoaplicar = $("#txtMAplicar").val();
//    var Diferencia = 0;

//    $.ajax({
//        url: '../BEC/ObtenerMontoMinimo',
//        async: false,
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                Diferencia = dato.valor;
//            } else {
//                alert(dato.message);
//            }
//        }
//    });

//    var resta =( parseFloat(montotoal.replace(',', '')) - parseFloat(montoaplicar.replace(',', ''))).toFixed(2);
//    if (resta <= parseFloat(Diferencia))
//    {
//        resultado = true;
////        alert("entro");
//    } else {
//        $("#lblbecidSalt").html($("#lblMRecId").html());
        $("#lblbecid").html($("#lblMRecId").html());

//        resultado = ValidaCobroMonto();
//    }


    return resultado;
}


function ValidaCobroMonto() {

    var resultado =false;

    var codigocobro =$('#lblMRecId').html();

    if(codigocobro!='' && codigocobro >0){
        $.ajax({
            url: '../BEC/CobroSaltaValidacionMontoMinimo',
            async: false,
            type: 'POST',
            data: {
                CodigoCobro: codigocobro
                },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) { // 1 permitido

                    resultado=true;

                } else {

                    resultado = false;
                    //alert(dato.message);
                    $("#lblbecidSalt").html($("#lblMRecId").html());
                    //$("#mvSolicitudRequeBecSaltar").dialog("open");
                }
            }
        });
    }

    return resultado;
}


function ValidaMontosAplicarSaldo(MontoDepositado,totalAplicar, MontoaAplicar, MontoPendiente) {

    $("#lblbecid").html($("#lblMRecId").html());

    var resultado = false;
    //.replace(',', '')
    //var resta = (parseFloat(MontoDepositado).toFixed(2) - parseFloat(totalAplicar)).toFixed(2);
    var resta = (MontoDepositado - parseFloat(totalAplicar)).toFixed(2);
    var diferencia = (parseFloat(MontoPendiente) - parseFloat(MontoaAplicar)).toFixed(2);

    
    //alert(MontoDepositado +' - ' + parseFloat(totalAplicar).toFixed(2));
    //alert(diferencia);


    //if (resta <= 1) {

    //    //alert("EsMenorOigualaUno");
        
    //    $("#btnGrabar").show();

    //} else {

        //alert("Entro");

        if (!ValidaCobroMonto()) {

            //validar que ingrese lo que es 
            if (diferencia > 1) {

                alert(K_MENSAJE_DIFERENCIA_MONTOS_APLICAR);
                $("#btnGrabar").hide();

                Confirmar("DESEA ENVIAR UN REQUERIMIENTO  PARA PERMITIR ESTA ACCION ? ",
                 function () {
                     $("#mvSolicitudRequeBecSaltar").dialog("open");
                 },
                 function () {
                     $("#mvSolicitudRequeBecSaltar").dialog("close");
                 },
                 'Confirmar')

            } else {
                $("#btnGrabar").show();
            }
        }

    //}

}