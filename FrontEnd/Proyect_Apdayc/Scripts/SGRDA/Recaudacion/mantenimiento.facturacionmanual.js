/************************** INICIO CONSTANTES****************************************/
var K_DIAS_ATRAS = 45;
var K_WIDTH_OBS = 580;
var K_HEIGHT_OBS = 255;
var K_TIPO = { ELECTRONICO: '1', MANUAL: '2' };
var K_TIPO_MONEDA = { SOLES: 'PEN', DOLAR: '44' };
var K_WIDTH_OBS2_FACT = 600;
var K_HEIGHT_OBS2_FACT = 325;
/************************** INICIO CARbtnSiguienteGA********************************************/
var MSG_VALIDACION_RADIO_BORRADOR = 'Seleccione una factura.';
var MSG_VALIDACION_SERIE_BORRADOR = 'Seleccione una serie.';
var MSG_VALIDACION_BORRADOR = '';
var MSG_VALIDACION_DISTRIBUCION = 'Complete los datos.';
var TIPO_FACTURA = { TOTAL: 'T', PARCIAL: 'P', ABIERTO: 'A', OTROS: 'O' };
var MSG_VALIDACION_DISTRIBUCION = {
    VALIDACION: 'Complete los datos.',
    MONTOS: 'Ingrese montos validos.',
    MONTOS_MAYORES: 'Los montos ingresados no pueden ser mayor al saldo pendiente.'
};

$(function () {
    kendo.culture('es-PE');
    limpiar();

    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaDoc').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    //var d = fechaActual.getDate();

    $("#txtFecInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();




    //--- Fecha Manual
    var fechaDoc = new Date();
    var Fechamin = new Date();

    //var dias = 3;
    //fechaDoc.setDate(fechaDoc.getDate() + dias); // para que sea 1 de mayo | datepicker.value($("#value").val());
    //Fechamin.setDate(Fechamin.getDate() + dias); // para que sea 1 de mayo | datepicker.value($("#value").val());
    $("#txtFechaDoc").data("kendoDatePicker").value(fechaDoc);
    var dfechaDoc = $("#txtFechaDoc").data("kendoDatePicker").value();

    //alert(fechaDoc);

    var day_Actual = dfechaDoc.getDate(); // dia actual 

    var mes_Actual = dfechaDoc.getMonth() + 1; //  mes actual

    var anio_Actual = dfechaDoc.getFullYear(); // anio actual

    //alert(day_Actual + '/' + mes_Actual + '/' + anio_Actual);

    Fechamin.setDate(Fechamin.getDate() - K_DIAS_ATRAS);

    if (day_Actual - K_DIAS_ATRAS <= 0) {
        //alert("actualizo fecha");
        Fechamin = new Date(mes_Actual + '/' + "01" + '/' + anio_Actual);
    }

    //alert(Fechamin + " ~ hasta " + fechaDoc);

    //Fechamin.setDate(Fechamin.getDate() - day);
    //alert(day);
    //alert(Fechamin);



    $("#txtFechaDoc").kendoDatePicker({
        min: Fechamin,
        //min: new Date(2017, 1 ,0),
        //max: new Date(2017, 8, 7),
        max: fechaActual,
        format: "dd/MM/yyyy"
    });

    $("#txtFechaDoc").closest("span.k-datepicker").width(130);
    $("#txtFechaDoc").val('');


    /*
    //--- Fecha Manual
    var fechaDoc = new Date();
    $("#txtFechaDoc").data("kendoDatePicker").value(fechaDoc);
    var dfechaDoc = $("#txtFechaDoc").data("kendoDatePicker").value();


    var Fechamin = new Date();
    Fechamin.setDate(Fechamin.getDate() - K_DIAS_ATRAS);
    //var day = fechaActual.getDate()-1;
    //Fechamin.setDate(Fechamin.getDate() - day);
    //alert(day);
    //alert(Fechamin);



    $("#txtFechaDoc").kendoDatePicker({
        min: Fechamin,
        //min: new Date(2017, 1 ,0),
        //max: new Date(2017, 8, 7),
        max: fechaActual,
        format: "dd/MM/yyyy"
    });

    $("#txtFechaDoc").closest("span.k-datepicker").width(130);
    //$("#txtFechaDoc").val('');

    */





    //--------------------------
    //--------------------------
    /*
        var fechaDocFin = new Date();
        $("#txtFecFinal").data("kendoDatePicker").value(fechaDocFin);
        var dfechaDocFin = $("#txtFecFinal").data("kendoDatePicker").value();
    
        var FechaminFin = new Date();
        //FechaminFin.setDate(FechaminFin.getDate() - K_DIAS_ATRAS_DIAS);
        FechaminFin.setDate(FechaminFin.getDate() - 25);
        //var day = fechaActual.getDate()-1;
        //Fechamin.setDate(Fechamin.getDate() - day);
        //alert(day);
        //alert(Fechamin);
    
        $("#txtFecFinal").kendoDatePicker({
            min: FechaminFin,
            //min: new Date(2017, 1 ,0),
            //max: new Date(2017, 8, 7),
            max: fechaActual,
            format: "dd/MM/yyyy"
        });
        //$("#txtFecFinal").closest("span.k-datepicker").width(130);
    */
    //--------------------------
    //--------------------------


    $('#hidCorrelativo').val(0);
    $('#ddlMoneda').prop('disabled', true);
    $('#hidIdFactura').val(0);
    loadMonedas('ddlMoneda', '0');
    loadTipoPagofactura('ddlTipoFacturacion', 0);
    loadFortmatoFacturacion('ddlTipoImpresion', 0);
    loadListaTipoFacturacionManual('ddlTipoFacturacionIndividual', '1');
    //$('#ddlTipoFacturacionIndividual').prop('disabled',true);
    $('#txtNumeroDoc').on("keypress", function (e) { return solonumeros(e); });
    $('td[class^="tdOcultar"]').hide();

    $('#divFM').show();
    $('#divBorrador').hide();
    $('#divConsulta').hide();

    var idLic = (GetQueryStringParams("idLic"));
    if (idLic !== undefined) {
        $('#hidLicencia').val(idLic);
        ObtenerNombreLicencia(idLic);
    } else {
        $('#hidLicencia').val(0);
    }

    //$("#mvObservacion").dialog({
    //    autoOpen: false,
    //    width: K_WIDTH_OBS,
    //    height: K_HEIGHT_OBS,
    //    buttons: {
    //        "Grabar": addObsAnulacionfact,
    //        "Cancelar": function () {
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

    $("#mvSolicitudRequeFactIndiv").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2_FACT,
        height: K_HEIGHT_OBS2_FACT,
        buttons: {
            "Grabar": InsertarRequerimiento,
            "Cancelar": function () {
                $("#mvSolicitudRequeFactIndiv").dialog("close");
            }
        },
        modal: true
    });
    
    loadTipoRquerimiento('ddltiporequerimiento', 10, 5);//SOLICITUD DE FACTURACION Y Q EMPIEZE EN EL INDEX 10

    //-------------------------- EVENTO BOTONES ------------------------------------           
    $("#btnSalir").on("click", function () { window.close(); });
    $("#btnSalir").hide();
    //--- PASO 1 ---
    $('#chkDetalleReporte').change(function () {
        if (this.checked) {
            $('#txtDetalleReporte').show();

        } else {
            $('#txtDetalleReporte').hide();
        }
    });


    $("#btnSolicitarRequerimiento").on("click", function () {
        $("#lbllicid").html($("#hidLicencia").val());
        $("#mvSolicitudRequeFactIndiv").dialog("open");

    }).button({ icons: { secondary: "ui-icon-circle-zoomout" } });


    $("#btnBuscar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var estadoTraslape = validacionTraslape('txtFecInicial', 'txtFecFinal');
        var idLicencia = $('#hidLicencia').val();
        var Moneda = $("#ddlMoneda").val();
        if (estadoRequeridos) {
            //ObtenerCaracteristicaValorTT();
            if (idLicencia != 0) {
                if (Moneda !== '0') {
                    if (estadoTraslape) {
                        BuscarFacturasMasivas(idLicencia, Moneda);
                        loadTipoPagofactura('ddlTipoPagoFactura', 0);
                    }
                } else {
                    alert('Seleccione un tipo de moneda.');
                }
            } else {
                alert('Seleccione una licencia.');
            }
        }
    }).button({ icons: { secondary: "ui-icon-circle-zoomout" } });

    $("#btnLimpiar").on("click", function () {

        Confirmar('Desea Inactivar los Borradores de la Licencia Seleccionada ?',
            function () { QuitarBorradoresxLicencia(); },
            function () { limpiar(); },
            'Confirmar');


    }).button({ icons: { secondary: "ui-icon-circle-close" } });

    $("#btnSiguiente").on("click", function () {
        loadDataFoundCOR(); // comun.buscador.correlativo
        var Moneda = $("#ddlMoneda").val();
        var idLic = $('#hidLicencia').val();
        var resp=1;//SI
        if (Moneda == K_TIPO_MONEDA.DOLAR)
            resp= ConsultaTasadeCambioActiva();
        if (resp==1)//si posee tasa de cambio registrada para dolares seguir
            obtenerFacturasSeleccionadas(idLic, Moneda);
    }).button({ icons: { secondary: "ui-icon-circle-arrow-e" } });



    //--- PASO 2 ---
    $('#ddlTipoFacturacion').change(function () {
        var Moneda = $("#ddlMoneda").val();
        var tipoFacturacion = $("#ddlTipoFacturacion").val();
        CargarListaPaso2(tipoFacturacion);
    });

    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });

    //Facturar

    $("#btnSiguienteBorrador").attr("disabled", false);

    $("#btnSiguienteBorrador").on("click", function () {
        $("#btnSiguienteBorrador").attr("disabled", true);
        var LIC_ID = $('#hidLicencia').val();
        var ValidaFacturacion = ValidarFacturacion(LIC_ID);
        var TipoDocValidadoSOcio= ValidarSerieTipoSocio($('#hidIdFactura').val() , $("#hidSerie").val());
      

        if (ValidaFacturacion == 0) {
            if (TipoDocValidadoSOcio == 1) {
                Confirmar(' ¿Está seguro de emitir la factura?',
                   function () {

                       MSG_VALIDACION_BORRADOR = '';
                       var Moneda = $("#ddlMoneda").val();
                       var estadoSerie = validacionSerieBorrador();
                       var tipoFactura = $("#ddlTipoFacturacion").val();
                       var idLic = $('#hidLicencia').val();
                       var idFacturaGenerado = $('#hidIdFactura').val();

                       if (estadoSerie) {
                           if (tipoFactura === TIPO_FACTURA.TOTAL) {
                               obtenerFacturaTotales(idLic, Moneda);
                           } else if (tipoFactura === TIPO_FACTURA.PARCIAL) {
                               var estadoValPagoPar = validacionPagoParcial();//validar ingreso de montos
                               if (estadoValPagoPar) {
                                   var estadoMontosPagoPar = validacionMontosPagoParcial();//validar que el monto no sea mayor que el sub-total
                                   if (estadoMontosPagoPar) {
                                       obtenerFacturaParciales(idLic, Moneda, idFacturaGenerado);
                                       $('#ddlTipoImpresion').val(0);
                                       $('#ddlTipoImpresion').css({ 'border': '1px solid gray' });
                                   }
                               }
                           }
                       } else {
                           alert(MSG_VALIDACION_BORRADOR);
                       }
                   },
                   function () {
                       $("#btnSiguienteBorrador").attr("disabled", false);
                   },
                   'Confirmar'
               )
            } else {
                $("#btnSiguienteBorrador").attr("disabled", false);
                alert("El socio no acepta el tipo de Documento Seleccionado | Seleccione otra serie ");
            }

            } else {
                alert("Usted no cuenta con permisos para Facturar.")
            }
        
    
    }).button({ icons: { secondary: "ui-icon-document" } });;

    $("#btnVolverBorrador").on("click", function () {
        VolverInicio();
    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });

    $('#ddlTipoFacturacionIndividual').change(function () {
        var TipoFacturacionIndividual = $("#ddlTipoFacturacionIndividual").val();
        if (TipoFacturacionIndividual === '1') {
            $('td[class^="tdOcultar"]').hide();
        } else if (TipoFacturacionIndividual === '2') {//Manual
            $('td[class^="tdOcultar"]').show();
        }
        $("#hidSerie").val(0);
        $("#lbCorrelativo").html('Seleccione una serie.');
        loadDataFoundCOR(); //comun.buscador.correlativo reload
    });

    //PASO 3
    $("#btnEliminarFactura").on("click", function () {
        eliminarFactura();
    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });

    $("#btnVistaPrevia").on("click", function () {
        var idFacturaGenerado = $('#hidIdFactura').val();
        if (idFacturaGenerado != 0)
            verReporte(idFacturaGenerado)
    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });

    //VISTA PREVIA FACTURACION
    $("#btnPreView").on("click", function () {
        var idCor = $('#hidSerie').val();
        var idFacturaGenerado = $('#hidIdFactura').val();
        var glosa = $('#txtDetalleReporte').val();
        var importe = 0;
        var tipoFactura = $("#ddlTipoFacturacion").val();

        ///MANUAL
        var tipo = $('#ddlTipoFacturacionIndividual').val();
        var numero = $('#txtNumeroDoc').val();
        var fechaEmision = $('#txtFechaDoc').val();
        //-------------------------------------------------------------------

        if (tipoFactura === TIPO_FACTURA.PARCIAL) {
            $('#tblPlaneamientoFactura tr').each(function () {
                var id = $(this).find(".IdPlanificacion").html();
                if (id != null && id != 0) {
                    importe = parseFloat(quitarformatoMoneda($('#txtDistribucion' + id).val()));
                }
            });
        }
        else {
            importe = 0;
        }
        if (idCor != 0) {
            if (idFacturaGenerado != 0) {
                //Electronica
                if (tipo == 1) {
                    verReporteFacturacion(idCor, idFacturaGenerado, tipo, 0, '01/07/2017', glosa, importe);
                }
                    //MANUAL
                else {
                    verReporteFacturacion(idCor, idFacturaGenerado, tipo, numero, fechaEmision, glosa, importe);
                }
            }
        }
        else {
            $("#lbCorrelativo").css('color', 'red');
            alert(MSG_VALIDACION_SERIE_BORRADOR);
        }

    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });



    $("#btnRealizarImpresion").on("click", function () {
        var tipoImp = $('#ddlTipoImpresion').val();
        if (tipoImp != 0) {
            $('#ddlTipoImpresion').css({ 'border': '1px solid gray' });
            var idFacturaGenerado = $('#hidIdFactura').val();
            if (idFacturaGenerado != 0)
                obtenerFacturaImpresion(idFacturaGenerado);
        } else {
            $('#ddlTipoImpresion').css({ 'border': '1px solid red' });
            alert('Seleccione el tipo de impresión.');
        }
    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });

    $("#btnNuevafactura").on("click", function () {
        VolverInicio();
    }).button({ icons: { secondary: "ui-icon-circle-arrow-w" } });

    //$(document).tooltip({
    //    position: {
    //        my: "center bottom-20",
    //        at: "center top",
    //        using: function (position, feedback) {
    //            $(this).css(position);
    //            $("<div>")
    //              .addClass("arrow")
    //              .addClass(feedback.vertical)
    //              .addClass(feedback.horizontal)
    //              .appendTo(this);
    //        }
    //    }
    //});
    //-------------------------------------------------------------------------------
    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });

});

//****************************  FUNCIONES ****************************
function VolverInicio() {
    var Moneda = $("#ddlMoneda").val();
    var idLic = $('#hidLicencia').val();
    BuscarFacturasMasivas(idLic, Moneda);
    $('#divFM').show();
    $('#divBorrador').hide();
    $('#divConsulta').hide();
    $('#hidIdFactura').val(0);

    $('#gridBorrador').html('');
    $('#gridBorradorLicencia').html('');
    $('#gridBorradorDetalle').html('');
    $('#gridConsulta').html('');
    $('#gridConsultaLicencia').html('');
    $('#gridConsultaDetalle').html('');

    $('#chkDetalleReporte').prop('checked', false);
    $('#txtDetalleReporte').val('');
    $('#txtDetalleReporte').hide();
    $('#txtNumeroDoc').val('');
    $("#btnSiguienteBorrador").attr("disabled", false);
}
//--- PASO 3 ---
function loadDataGridConsultaTmp(Controller, idGrilla, idFactura) {
    $.ajax({
        type: 'POST',
        //data: { idFactura: idFactura },
        url: Controller, beforeSend: function () { },
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

function BuscarFacturasConsulta(idLic, moneda, idFacturaGenerado) {
    var serie = 0;
    var numFact = 0;
    var idTipoLic = 0;

    var idBps = 0;
    var idGF = 0;
    var codMoneda = moneda;

    var ini = '17/08/2015';
    var fin = '17/08/2015';
    var idFact = idFacturaGenerado;
    var numLic = idLic;

    var tipLicencia = 0;
    var idBpsAgen = 0;

    var conFecha = 0;

    var Impresas = 0;
    var Anuladas = 0;

    var tipoDoc = 0;
    var idOficina = 0;

    var valorDivision = 0;
    var estado = 0;
    var idBpsGroup = 0;
    $.ajax({
        url: '../FacturacionManual/ListarConsulta',
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
            estado: estado,
            idBpsGroup: idBpsGroup
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                CargarListaPaso3();
                //loadDataFacturaConsulta();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataConsultaFactura() {
    loadDataGridConsultaTmp('../FacturacionManual/ListarFactConsulta', "#gridConsulta");
}

function loadDataConsultaLicencia() {
    loadDataGridConsultaTmp('../FacturacionManual/ListarLicenciaConsulta', "#gridConsultaLicencia");
}

function loadDataConsultaDetalle() {
    loadDataGridConsultaTmp('../FacturacionManual/ListarLicenciaPlanConsulta', "#gridConsultaDetalle");
}

function obtenerFacturaImpresion(idFactura) {
    var ReglaValor = [];
    var contador = 0;

    ReglaValor[contador] = {
        INV_ID: idFactura
    };
    contador += 1;
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../FacturacionConsulta/ObtenerFacturasSelImpresion',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert("Se envio a imprimir la factura.");
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar la factura.");
            }
        });
    } else {
        alert("No se logro enviar la factura.");
    }

}

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

                var url = "../FacturacionConsulta/ReporteManual?id=" + cod;
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

//function verReporte(cod) {
//    var url = "../FacturacionConsulta/Reporte?id=" + cod;
//    var poPup = '';
//    poPup = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
//}

function verReporteFacturacion(serie, cod, tipo, numero, fecha, glosa, importe) {
    var url = "../FacturacionManual/ReporteFacturacion?serie=" + serie + "&id=" + cod + "&tipo=" + tipo + "&numero=" + numero + "&fecha=" + fecha + "&glosa=" + glosa + "&imp=" + importe;
    var poPup = '';
    poPup = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
}

function verDetaCon(id) {
    if ($("#expandCon" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandCon" + id).attr('src', '../Images/botones/more.png');
        $("#expandCon" + id).attr('title', 'ver detalle.');
        $("#expandCon" + id).attr('alt', 'ver detalle.');
        $("#divCon" + id).css("display", "none");

    } else {
        $("#expandCon" + id).attr('src', '../Images/botones/less.png');
        $("#expandCon" + id).attr('title', 'ocultar detalle.');
        $("#expandCon" + id).attr('alt', 'ocultar detalle.');
        $("#divCon" + id).css("display", "inline");
    }
    return false;
}

function verDetaLicCon(id, lic) {
    var cod = (id + '-' + lic);
    if ($("#expandLicCon" + cod).attr('src') == '../Images/botones/less.png') {
        $("#expandLicCon" + cod).attr('src', '../Images/botones/more.png');
        $("#expandLicCon" + cod).attr('title', 'ver detalle.');
        $("#expandLicCon" + cod).attr('alt', 'ver detalle.');
        $("#divLicCon" + cod).css("display", "none");

    } else {
        $("#expandLicCon" + cod).attr('src', '../Images/botones/less.png');
        $("#expandLicCon" + cod).attr('title', 'ocultar detalle.');
        $("#expandLicCon" + cod).attr('alt', 'ocultar detalle.');
        $("#divLicCon" + cod).css("display", "inline");
    }
    return false;
}

//--- PASO 2 ---
function loadDataBorradorFacturaMasiva(tipoFacturacion) {
    loadDataGridBorradorTmp('ListarBorradorFactMasivaCabecera', "#gridBorrador", tipoFacturacion);
}

function loadDataBorradorDetaLicencia(tipoFacturacion) {
    loadDataGridBorradorTmp('ListarDetaLicenciaBorrador', "#gridBorradorLicencia", tipoFacturacion);
}

function loadDataBorradorDetaLicPlanificacion(tipoFacturacion) {
    loadDataGridBorradorDetalleTmp('ListarDetaLicPlanBorrador', "#gridBorradorDetalle", tipoFacturacion);
}

function BuscarFacturasBorrador(idLic, moneda, tipoFacturacion, idFacturaGenerado) {
    var ini = '17/08/2015';
    var fin = '17/08/2015';
    var idTipoLic = 0;
    var codMoneda = moneda;
    var idGF = 0;
    var idBps = 0;
    var idCorrelativo = '0';
    var idTipoPago = '0';
    var confecha = 0;
    $.ajax({
        url: '../FacturacionManual/ListarFacturacionBorrador',
        type: 'POST',
        data: {
            fini: ini, ffin: fin,
            tipoLic: idTipoLic, idMoneda: codMoneda,
            idGrufact: idGF, idBps: idBps,
            idCorrelativo: idCorrelativo,
            idTipoPago: idTipoPago,
            conFecha: confecha,
            idLic: idLic,
            tipoFacturacion: tipoFacturacion,
            idFacturaGenerado: idFacturaGenerado
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipoFacturacion = $("#ddlTipoFacturacion").val();
                CargarListaPaso2(tipoFacturacion);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
} // CAMBIAR

function validacionSerieBorrador() {
    var estado = true;
    var idCor = $('#hidCorrelativo').val();
    var msj = '';
    var tipo = $('#ddlTipoFacturacionIndividual').val();
    MSG_VALIDACION_BORRADOR = '';

    if (tipo == K_TIPO.ELECTRONICO && idCor == 0) {
        estado = false;
        $("#lbCorrelativo").css('color', 'red');
        MSG_VALIDACION_BORRADOR += MSG_VALIDACION_SERIE_BORRADOR;
    } else if (tipo == K_TIPO.MANUAL) {
        var numero = $('#txtNumeroDoc').val();
        var fecha = $('#txtFechaDoc').val();
        var estadoValidacionSerieNumero = ValidarSerieNumero(idCor, numero);

        if (idCor == 0) {
            $("#lbCorrelativo").css('color', 'red');
            MSG_VALIDACION_BORRADOR += MSG_VALIDACION_SERIE_BORRADOR + '\r\n';
            estado = false;
        }
        if (numero == '') {
            $("#txtNumeroDoc").css('border', '1px solid red');
            MSG_VALIDACION_BORRADOR += 'Ingrese Numero.\r\n'
            estado = false;
        }
        if (fecha == '') {
            $("#txtFechaDoc").css('border', '1px solid red');
            MSG_VALIDACION_BORRADOR += 'Ingrese Fecha.\r\n'
            estado = false;
        }

        if (!estadoValidacionSerieNumero) {
            MSG_VALIDACION_BORRADOR += 'La serie y numero del documento ya existe. Anule el documento existente. \r\n'
            estado = false;
        }

    } else {
        $("#lbCorrelativo").css('color', 'black');
    }

    if (!estado) {
        $("#btnSiguienteBorrador").attr("disabled", false);
    }

    return estado;
}

//OBTENER FACTURAS - PASO 2
function quitarformatoMoneda(valorTarifa) {
    var valor = ''
    valor = valorTarifa.replace(',', '')
    return valor;
}


function obtenerFacturaTotales(idLic, Moneda, idFacturaGenerado) {
    var ValidaFecha = 1;

    var tipo = $('#ddlTipoFacturacionIndividual').val();
    var numero = 0;
    var fechaEmision = '';
    var manual = new Boolean(false);

    var idCorrelativo = $("#hidCorrelativo").val();
    var detalleReporte = '';
    if ($('#chkDetalleReporte').is(":checked")) {
        detalleReporte = $('#txtDetalleReporte').val();
    }

    if (tipo == K_TIPO.MANUAL) {
        numero = $('#txtNumeroDoc').val();
        fechaEmision = $('#txtFechaDoc').val();
        manual = true;
        ValidaFecha = ValidarDiasMinimosFechaManual(fechaEmision);
    } else {
        manual = false;
        fechaEmision = '01/01/2017';
        ValidaFecha = 1;
    }



    if (ValidaFecha == 1) { //paso aprobacion
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '../FacturacionManual/ObtenerFacturasTotales',
            data: { serie: idCorrelativo, detalleReporte: detalleReporte, Manual: manual, Numero: numero, fecEmision: fechaEmision, ValidaFecha: ValidaFecha },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {

                    alert(dato.message);

                    //PASO 3
                    var idFacturaGenerado = $('#hidIdFactura').val();
                    BuscarFacturasConsulta(idLic, Moneda, idFacturaGenerado);
                    $('#divFM').hide();
                    $('#divBorrador').hide();
                    $('#divConsulta').show();
                } else if (dato.result == 2) {
                    var idBps = dato.Code;
                    alert(dato.message);
                    AbrirSocioEditar(idBps);
                    VolverInicio();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar las factura(s).");
            }
        });
    }
}


function obtenerFacturaParciales(idLic, Moneda, idFacturaGenerado) {
    var tipo = $('#ddlTipoFacturacionIndividual').val();
    var numero = 0;
    var fechaEmision = '';
    var manual = new Boolean(false);

    var ReglaValor = [];
    var contador = 0;
    var idCorrelativo = $("#hidCorrelativo").val();
    var detalleReporte = '';
    if ($('#chkDetalleReporte').is(":checked")) {
        detalleReporte = $('#txtDetalleReporte').val();
    }

    if (tipo == K_TIPO.MANUAL) {
        numero = $('#txtNumeroDoc').val();
        fechaEmision = $('#txtFechaDoc').val();
        manual = true;
    }

    $('#tblPlaneamientoFactura tr').each(function () {
        var actual = 0;
        var id = $(this).find(".IdPlanificacion").html();
        if (id != null && id != 0) {
            var distribucion = parseFloat(quitarformatoMoneda($('#txtDistribucion' + id).val()));
            ReglaValor[contador] = {
                LIC_PL_ID: id,
                LIC_INVOICE_LINE: distribucion,
                SERIE: idCorrelativo,
                INV_REPORT_DETAILS: detalleReporte,

                TIPO_MANUAL: manual,
                NUMERO: numero,
                FECHA_EMISION: fechaEmision,
            };
            contador += 1;
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../FacturacionManual/ObtenerFacturasParciales',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {

                    alert(dato.message);

                    //PASO 3
                    var moneda = $("#ddlMoneda").val();
                    BuscarFacturasConsulta(idLic, moneda, idFacturaGenerado);
                    $('#divFM').hide();
                    $('#divBorrador').hide();
                    $('#divConsulta').show();
                } else if (dato.result == 2) {
                    var idBps = dato.Code;
                    alert(dato.message);
                    AbrirSocioEditar(idBps);
                    VolverInicio();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar las factura(s).");
            }
        });
    } else {
        alert("Debe seleccionar una factura para continuar el proceso.");
    }

}

function AbrirSocioEditar(id) {
    var url = "..//Socio/Nuevo?set=" + id;
    window.open(url, "_blank");
}


//VALIDACIONES - PASO 2
function validacionPagoParcial() {
    var result = false;
    var acumulado = 0;
    var validado = 0;
    $('#tblPlaneamientoFactura tr').each(function () {
        var id = parseFloat($(this).find(".IdPlanificacion").html());
        if (!isNaN(id) && id != 0) {
            acumulado += 1;
            var distribucion = parseFloat($('#txtDistribucion' + id).val());
            if (!isNaN(distribucion) && distribucion !== 0) {
                validado += 1;
                $('#txtDistribucion' + id).css('border', '1px solid gray');
            } else {
                $('#txtDistribucion' + id).css('border', '1px solid red');
            }
        }
    });

    if (acumulado === validado) {
        result = true
    } else {
        alert(MSG_VALIDACION_DISTRIBUCION.MONTOS);
        $("#btnSiguienteBorrador").attr("disabled", false);
    }
    return result;
}

function validacionMontosPagoParcial() {
    var result = false;
    var acumulado = 0;
    var validado = 0;
    $('#tblPlaneamientoFactura tr').each(function () {
        var id = parseFloat($(this).find(".IdPlanificacion").html());
        var saldoPendiente = $(this).find(".saldoPendiente").html();
        if (!isNaN(id) && id != 0) {
            acumulado += 1;
            var distribucion = parseFloat(quitarformatoMoneda($('#txtDistribucion' + id).val()));
            if (distribucion <= saldoPendiente) {
                validado += 1;
                $('#txtDistribucion' + id).css('border', '1px solid gray');
            } else {
                $('#txtDistribucion' + id).css('border', '1px solid red');
            }
        }
    });

    if (acumulado === validado) {
        result = true
    } else {
        alert(MSG_VALIDACION_DISTRIBUCION.MONTOS_MAYORES);
        $("#btnSiguienteBorrador").attr("disabled", false);
    }
    return result;
}

function eliminarFactura() {
    Confirmar(' ¿Desea anular la factura?',
               function () {
                   $("#txtDescripcion").val('');
                   $("#mvAnulacion").dialog("open");
               },
               function () {
               },
               'Confirmar'
           )
}

var addObsAnulacionfact = function () {
    var lic = $('#hidLicencia').val();
    var moneda = $('#hidMoneda').val();
    var id = $("#hidIdFactura").val();
    //var id2 = $("#hidIdFactA").val();
    var obs = $("#txtDescripcion").val();
    //var tipoF = $("#hidTipoF").val();
    var tipoFacturacion = $('#ddlTipoFacturacionIndividual').val();
    var tipoF = 0;

    if (tipoFacturacion == 2)
        tipoF = 1; //manual
    else if (tipoFacturacion == 1)
        tipoF = 2; //electronico
    else
        tipoF = 0;

    if (obs == '') {
        alert('Ingrese el motivo de la anulación.');
    } else {

        $.ajax({
            data: { id: id, observacion: obs, tipoF: tipoF },
            url: '../FacturacionConsulta/AnularFactura',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    var tipoFacturacion = $("#ddlTipoFacturacion").val();
                    BuscarFacturasBorrador(lic, moneda, tipoFacturacion);
                    alert(dato.message);
                    VolverInicio();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvAnulacion").dialog("close");
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

//SERIE CORRELATIVO
var reloadEventoCorrelativo = function (idSel) {
    var idFacturaGenerado = $('#hidIdFactura').val();
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
    var des_serie = $("#hidSerie").val();
   //var des_serie= $("#hidCorrelativo").Text();
    $.ajax({
        data: { id: idSel },
        url: '../FacturacionManual/ValidarSerie',
        type: 'POST',
        data: { idfactura: idFacturaGenerado, serie: idSel, des_serie: des_serie },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                obtenerNombreCorrelativo($("#hidCorrelativo").val());
            }
            else if (dato.result == 2) {
                alert(dato.message);
            }
            else {
                alert(dato.message);
            }
        }
    });
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            }
        }
    });
}

//--- PASO 1 ---
function limpiar() {
    $('#txtFecInicial').val('');
    $('#txtFecFinal').val('');
    $('#lblLicencia').html('Seleccione una licencia.');
    $('#ddlMoneda').val('0');
    $('#hidLicencia').val(0);
    $('#hidMoneda').val(0);
    $('#chkHistorico').prop('checked', false);
    $("#ddlTipoFacturacion").prop('disabled', false);
    $("#ddlTipoFacturacion").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'white');
    $("#hidEdicionPaso2").val(0);
    $('#hidIdFactura').val(0);
    $('#gridFacturaManual').html('');

    $('#chkDetalleReporte').prop('checked', false);
    $('#txtDetalleReporte').val('');
    $('#txtDetalleReporte').hide();
    $("#ddlTipoFacturacionIndividual").prop('selectedIndex', 1);
}

function BuscarFacturasMasivas(idlic, moneda) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var idMod = 0;
    var idDad = '0';
    var idBps = 0;
    var idBps_age = 0;
    var idOfi = 0;
    var idGM = '0';
    var idTipoEst = 0;
    var idSubTipoEst = 0;
    var idLic = idlic;
    var idMoneda = moneda
    var Historico = $('#chkHistorico').is(':checked') == true ? 1 : 0;

    var idBpsGroup = 0;
    var groupfact = 0;
    var tipoFact = 0; //0=manual , 1 = masiva
    var EmiMensual = 0;
    $.ajax({
        url: '../FacturacionManual/ListarFacturaMasivaSubGrilla',
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
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFacturaLicencia(1);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataFacturaLicencia(estado) {
    loadDataGridTmp('ListarFacturaLicencia', "#gridFacturaManual", estado);
}

function loadDataGridTmp(Controller, idGrilla, estado) {
    $.ajax({
        type: 'POST', data: { estado: estado }, url: Controller, beforeSend: function () { },
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

function loadDataGridBorradorTmp(Controller, idGrilla, tipofacturacion) {
    $.ajax({
        type: 'POST', data: { tipofacturacion: tipofacturacion }, url: Controller, beforeSend: function () { },
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

function loadDataGridBorradorDetalleTmp(Controller, idGrilla, tipofacturacion) {
    $.ajax({
        type: 'POST', data: { tipofacturacion: tipofacturacion }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                $('.k-formato-numerico').each(function (i, elem) {
                    $(elem).priceFormat({
                        clearPrefix: true,
                        prefix: '',
                        limit: 11,
                        centsLimit: 2
                    });
                });

            } else if (dato.result == 0) {
                alert(dato.message);
            }
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

function obtenerFacturasSeleccionadas(idLic, Moneda) {
    var ReglaValor = [];
    var contador = 0;

    $('#tblPeriodos tr').each(function () {
        var IdNro = $(this).find(".IdNro").html();
        if (!isNaN(IdNro) && IdNro != null) {
            var IdLic = $(this).find(".IdLic").html();
            var idLP = $(this).find(".idLP").html();
            var idEstadoPeriodo = $(this).find(".idEstadoPeriodo").html();
            if ($('#chkPL' + idLP).is(':checked')) {
                ReglaValor[contador] = {
                    Nro: IdNro,
                    LIC_ID: IdLic,
                    LIC_PL_ID: idLP,
                    LIC_PL_STATUS: idEstadoPeriodo
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
            url: '../FacturacionManual/ObtenerFacturasSeleccionadas',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //PASO 2
                    $('#lbCorrelativo').html(' Seleccione una Serie.');
                    $('#txtNumeroDoc').val('');
                    $('#txtFechaDoc').val('');
                    $("#txtNumeroDoc").css('border', '1px solid gray');
                    $("#txtFechaDoc").css('border', '1px solid gray');
                    $('#ddlTipoFacturacionIndividual').val(1);
                    $('td[class^="tdOcultar"]').hide();

                    $('#hidCorrelativo').val(0);
                    $('#hidSerie').val(0);
                    $('#hidActual').val(0);
                    if (dato.valor === TIPO_FACTURA.PARCIAL) {
                        $("#ddlTipoFacturacion").val(TIPO_FACTURA.PARCIAL);
                        $("#ddlTipoFacturacion").prop('disabled', true);
                        $("#ddlTipoFacturacion").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'rgb(235, 235, 228)');
                        var tipoFacturacion = TIPO_FACTURA.PARCIAL;
                        $("#hidEdicionPaso2").val(1);
                    } else {
                        $("#ddlTipoFacturacion").val(TIPO_FACTURA.TOTAL);
                        $("#ddlTipoFacturacion").prop('disabled', false);
                        $("#ddlTipoFacturacion").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'white');
                        var tipoFacturacion = $("#ddlTipoFacturacion").val();
                        $("#hidEdicionPaso2").val(0);
                    }
                    $('#hidIdFactura').val(dato.Code);
                    var idFacturaGenerado = $('#hidIdFactura').val();
                    BuscarFacturasBorrador(idLic, Moneda, tipoFacturacion, idFacturaGenerado);

                    $('#divFM').hide();
                    $('#divBorrador').show();
                    $('#divConsulta').hide();
                    $('#gridFacturaManual').html('');
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

function CargarListaPaso2(tipoFacturacion) {
    loadDataBorradorFacturaMasiva(tipoFacturacion);
    loadDataBorradorDetaLicencia(tipoFacturacion);
    loadDataBorradorDetaLicPlanificacion(tipoFacturacion);
}

function CargarListaPaso3() {
    loadDataConsultaFactura();
    loadDataConsultaLicencia();
    loadDataConsultaDetalle();
}

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
                $('#hidLicencia').val(idSel);
                $('#hidMoneda').val(dato.message);
                $("#lblLicencia").html(dato.valor);
                loadMonedas('ddlMoneda', dato.message);
                $('#gridFacturaManual').html('');
            }
        }
    });
};

// VALIDACION
function ValidarSerieNumero(idSerie, numero) {

    var estado = false;
    if (idSerie != 0 && idSerie != '' && numero != '' && numero != null) {
        $.ajax({
            data: { idSerie: idSerie, numero: numero },
            url: '../FacturacionManual/ValidarSerieNumero',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    estado = true;
                } else if (dato.result == 0) {
                    estado = false;
                }
            }
        });
    }
    if (!estado) {
        $("#btnSiguienteBorrador").attr("disabled", false);
    }
    return estado;
};

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

function QuitarBorradoresxLicencia() {
    var lic_id = $("#hidLicencia").val();

    if (lic_id > 0) {
        $.ajax({
            data: { LIC_ID: lic_id },
            url: '../FacturacionManual/LimpiarBorradoresxLicencia',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {

                    alert("Se limpiaron los periodos correctamente");
                    VolverInicio();

                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Seleccione una Licencia");
    }
}


function ValidarDiasMinimosFechaManual(fecha_seleccionada) {

    var res = 0;
    $.ajax({
        data: { FechaSelect: fecha_seleccionada },
        url: '../FacturacionManual/ValidaFechaManual',

        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                res = dato.result;

            } else if (dato.result != 1) {

                alert(dato.message);
            }
        }
    });
    return res;
}

function InsertarRequerimiento() {

        var EST_ID = 0;
        var ID_REQ_TYPE = $("#ddltiporequerimiento").val();
        var RAZON = $("#txtAprobacionDescFact").val();
        var ACTIVO = 1;
        var MONTO = 0;
        var FECHA = "";
        var INV_ID = 0;
        var LIC_ID = $("#hidLicencia").val();
        var BPS_ID = 0;
        var BEC_ID = 0;
        var TIP_LIC_INACT = 0;

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
                    $("#mvSolicitudRequeFactIndiv").dialog("close");
                    //$("#ddltipoAprobacion").val(entidad.TIPO);

                } else {
                    alert(dato.message);
                }
            }
        });

    }
function ValidarFacturacion(id_lic) {

    var retorno = 0;
    $.ajax({
        url: '../Licencia/ValidarFacturacion',
        type: 'POST',
        dataType: 'JSON',
        data: { LIC_ID: id_lic },
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

function ValidarSerieTipoSocio(documento,serie) {


    var retorno = 0;

    $.ajax({
        url: '../FacturacionManual/ValidarSerieTipoSocio',
        type: 'POST',
        dataType: 'JSON',
        data: { idSerie: serie, documento: documento },
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


