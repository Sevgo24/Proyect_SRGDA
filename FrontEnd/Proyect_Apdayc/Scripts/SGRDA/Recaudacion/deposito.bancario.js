var K_POPUP_CONFIRMACION_ANCHO = 500;
var K_POPUP_CONFIRMACION_ALTO = 410;
var K_MSJ_VOUCHER_DUPLICADO = 'Anteriormente el voucher ya fue confirmado con el mismo: \r\n banco,  cuenta,  fec. depósito,  monto y  cod. de confirmación.   \r\n ¿que deseas hacer? ';
var K_TIPO_PERMISO_X_OFICINA = 0;
var K_PERMISO = { Consulta: 1, Confirmaciones: 2, Bec_Especiales: 3, Permiso_Total: 4 };

$(function () {
    kendo.culture('es-PE');

    $('#txtID').on("keypress", function (e) { return solonumeros(e); });
    $('#txtCobro').on("keypress", function (e) { return solonumeros(e); });

    $('#txtFechaIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFin').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaDepMod').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();

    var fechaIni = new Date(fechaActual.getFullYear(), fechaActual.getMonth() - 2, fechaActual.getDate());
    $("#txtFechaIni").data("kendoDatePicker").value(fechaIni);
    $("#txtFechaDepMod").data("kendoDatePicker").value(fechaIni);

    var d = $("#txtFechaIni").data("kendoDatePicker").value();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
    $("#txtFechaFin").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFechaFin").data("kendoDatePicker").value();

    // *** BUSQUEDA DE CREACIÓN ***
    $('#txtFechaIniIngreso').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFinIngreso').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActualIngreso = new Date();
    var fechaIniIngreso = new Date(fechaActual.getFullYear(), fechaActual.getMonth() - 2, fechaActual.getDate());
    var fechaFinIngreso = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());


    //var dIngreso = $("#txtFechaIniIngreso").data("kendoDatePicker").value();
    $("#txtFechaIniIngreso").data("kendoDatePicker").value(fechaIniIngreso);
    $("#txtFechaFinIngreso").data("kendoDatePicker").value(fechaFinIngreso);
    //var dFINIngreso = $("#txtFechaFinIngreso").data("kendoDatePicker").value();

    // *******************

    //$("#txtFechaDepMod").kendoDatePicker({
    //    max: fechaActual,
    //    format: "dd/MM/yyyy"        
    //});

    //$("#txtFechaDepMod").closest("span.k-datepicker").width(130);
    //$("#txtFechaDepMod").val('');


    loadBancos('ddlBanco', 0);
    LoadMetodoPago('ddlTipoPago', 0);
    loadMonedas('ddlMoneda', 0);
    loadValoresConfiguracion('ddlEstadoConfirmacion', 'COBRO', 'DEPOSITO');

    loadBancos('ddlBancoDestino', 0);
    $('#ddlCuentaDestino').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));

    $("#ddlBancoDestino").change(function () {
        var idBancoDestino = $("#ddlBancoDestino").val();
        $('#ddlCuentaDestino option').remove();
        $('#ddlCuentaDestino').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuentaDestino', idBancoDestino, '0');
    });


    $("#chkConFecha").prop('checked', true);
    $("#chkConFecha").change(function () {
        if ($('#chkConFecha').is(':checked')) {
            $('#txtFechaIni').data('kendoDatePicker').enable(true);
            $('#txtFechaFin').data('kendoDatePicker').enable(true);
            $("#txtFechaIni").addClass("requerido");
            $("#txtFechaFin").addClass("requerido");
        } else {
            $('#txtFechaIni').data('kendoDatePicker').enable(false);
            $('#txtFechaFin').data('kendoDatePicker').enable(false);
            $("#txtFechaIni").removeClass("requerido");
            $("#txtFechaFin").removeClass("requerido");
            $("#txtFechaIni").css("border", "gray solid 1px");
            $("#txtFechaFin").css("border", "gray solid 1px");
        }
    });

    $("#chkConFechaIngreso").prop('checked', false);
    $('#txtFechaIniIngreso').data('kendoDatePicker').enable(false);
    $('#txtFechaFinIngreso').data('kendoDatePicker').enable(false);
    $("#chkConFechaIngreso").change(function () {
        if ($('#chkConFechaIngreso').is(':checked')) {
            $('#txtFechaIniIngreso').data('kendoDatePicker').enable(true);
            $('#txtFechaFinIngreso').data('kendoDatePicker').enable(true);
            $("#txtFechaIniIngreso").addClass("requerido");
            $("#txtFechaFinIngreso").addClass("requerido");
        } else {
            $('#txtFechaIniIngreso').data('kendoDatePicker').enable(false);
            $('#txtFechaFinIngreso').data('kendoDatePicker').enable(false);
            $("#txtFechaIniIngreso").removeClass("requerido");
            $("#txtFechaFinIngreso").removeClass("requerido");
            $("#txtFechaIniIngreso").css("border", "gray solid 1px");
            $("#txtFechaFinIngreso").css("border", "gray solid 1px");
        }
    });


    LimpiarBandeja();

    $("#btnBuscar").on("click", function (e) {
        loadDataFiltros();
    });

    $('#txtCodigoBancario').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            loadDataFiltros();
        }
    });

    $('#txtMontoDepositado').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            loadDataFiltros();
        }
    });

    $('#txtCodigoConfirmacion').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            loadDataFiltros();
        }
    });

    $('#txtID').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            loadDataFiltros();
        }
    });


    $("#btnLimpiar").on("click", function (e) { LimpiarBandeja(); });

    //K_TIPO_PERMISO_X_OFICINA = ObtenerPermisoxOficina();
    //if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Bec_Especiales) {
    //    $("#ddlBecEspecial").val(1);
    //    $("#ddlEstadoAprobacion").val(0);
    //}
    //loadData();

    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    //XXX PoPup - VOUCHER XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    $("#mvBecEspecial").dialog({
        autoOpen: false, width: K_POPUP_CONFIRMACION_ANCHO, height: K_POPUP_CONFIRMACION_ALTO,
        title: 'Aprobación de Bec Especiales',
        buttons: {
            "Registrar": ActualizarBecEspecial,//addDetalleVoucherBancario,
            "Cancelar": function () { $("#mvBecEspecial").dialog("close"); }
        },
        modal: true

    });

    $("#mvConfirmacion").dialog({
        autoOpen: false, width: K_POPUP_CONFIRMACION_ANCHO, height: K_POPUP_CONFIRMACION_ALTO,
        title: 'Depósito Bancario',
        buttons: {
            "Registrar": ActualizarEstadoDeposito,//addDetalleVoucherBancario,
            "Cancelar": function () { $("#mvConfirmacion").dialog("close"); }
        },
        modal: true
    });

    $("#mvConfirmacionModificar").dialog({
        autoOpen: false, width: K_POPUP_CONFIRMACION_ANCHO, height: K_POPUP_CONFIRMACION_ALTO - 50,
        title: 'Depósito Bancario - Modificar',
        buttons: {
            "Actualizar": ActualizarEstadoDepositoModificar,
            "Cancelar": function () { $("#mvConfirmacionModificar").dialog("close"); }
        },
        modal: true
    });

    $("#btnVerDetalle").on("click", function (e) {
        //alert('fer');
        ShowPopUpDetalleCobro($("#hidIdCobro").val());
    });

    $("#btnVerDetalleM").on("click", function (e) {
        //alert('fer');
        ShowPopUpDetalleCobro($("#hidIdCobroM").val());
    });

    $("#btnVerImagenCobro").on("click", function (e) {
        //alert('Jhon');
        ObtenerRuta($("#hidIdCobro").val())

        //ShowPopUpDetalleCobro($("#hidIdCobro").val());
    });

    $("#BEbtnVerImagenCobro").on("click", function (e) {
        ObtenerRuta($("#BEhidIdCobro").val());
    });

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });

    K_TIPO_PERMISO_X_OFICINA = ObtenerPermisoxOficina();
    if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Bec_Especiales) {
        $("#ddlBecEspecial").val(1);
        $("#ddlEstadoAprobacion").val(0);
    }

    $("#ddlMonedaMod").prop('disabled', true);
    $("#txtFechaDepMod").prop('disabled', true);

    $("#btnPdf").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        //obtenerModalidadesSeleccionadas();
        ExportarReporte('PDF');
    });

    $("#btnExcel").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        //obtenerModalidadesSeleccionadas();
        ExportarReporte('EXCEL');
    });

    loadData();
});






function ObtenerRuta() {
    var MREC_ID = $("#hidIdCobro").val();
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
                $("#btnVerImagenCobro").hide();

            }
        }


    })
}
function ActivarBoton(MREC_ID) {
    //alert('Activar Boton')
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
                $("#btnVerImagenCobro").show();
                $("#BEbtnVerImagenCobro").show();
            } else {
                $("#btnVerImagenCobro").hide();
                $("#BEbtnVerImagenCobro").hide();
            }
        }


    })
}

function abrirVentana(url) {
    window.open(url, "_blank");
}

//FUNCIONES
function loadDataFiltros() {

    $('#gridDepositosBancarios').data('kendoGrid').dataSource.query({
        idBanco: $("#ddlBanco").val(),
        idTipoPago: $("#ddlTipoPago").val(),
        idMoneda: $("#ddlMoneda").val(),

        idEstadoConfirmacion: $("#ddlEstadoConfirmacion").val(),
        CodigigoDeposito: $("#txtCodigoBancario").val(),

        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
        FIni: $("#txtFechaIni").val(),
        FFin: $("#txtFechaFin").val(),
        IdBps: $("#hidEdicionEnt").val(),
        idBancoDestino: $("#ddlBancoDestino").val(),
        idCuentaDestino: $("#ddlCuentaDestino").val(),
        montoDepositado: $('#txtMontoDepositado').val(),

        conFechaIngreso: $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0,
        FIniIngreso: $("#txtFechaIniIngreso").val(),
        FFinIngreso: $("#txtFechaFinIngreso").val(),
        IdOficina: $("#hidOficina").val(),
        idVoucher: $("#txtID").val() == '' ? 0 : $("#txtID").val(),
        CodigoConfirmacion: $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val(),
        becEspecial: $("#ddlBecEspecial").val(),
        becEspecialAprobacion: $("#ddlEstadoAprobacion").val(),
        idCobro: $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val(),  // $("#txtIdCobro").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });

    $("#gridDepositosBancarios").show();
    $("#contenedor").hide();
}


function loadData() {
    $("#gridDepositosBancarios").show();
    $("#contenedor").hide();

    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../DepositoBancario/Listar",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        idBanco: $("#ddlBanco").val(),
                        idTipoPago: $("#ddlTipoPago").val(),
                        idMoneda: $("#ddlMoneda").val(),

                        idEstadoConfirmacion: $("#ddlEstadoConfirmacion").val(),
                        CodigigoDeposito: $("#txtCodigoBancario").val(),

                        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                        FIni: $("#txtFechaIni").val(),
                        FFin: $("#txtFechaFin").val(),
                        IdBps: $("#hidEdicionEnt").val(),

                        idBancoDestino: $("#ddlBancoDestino").val(),
                        idCuentaDestino: $("#ddlCuentaDestino").val(),
                        montoDepositado: $('#txtMontoDepositado').val(),

                        conFechaIngreso: $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0,
                        FIniIngreso: $("#txtFechaIniIngreso").val(),
                        FFinIngreso: $("#txtFechaFinIngreso").val(),
                        IdOficina: $("#hidOficina").val(),
                        idVoucher: $("#txtID").val() == '' ? 0 : $("#txtID").val(),
                        CodigoConfirmacion: $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val(),
                        becEspecial: $("#ddlBecEspecial").val(),
                        becEspecialAprobacion: $("#ddlEstadoAprobacion").val(),
                        idCobro: $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val(),  // $("#txtIdCobro").val(),

                    })
            }
        },
        schema: { data: "ListarDetalleMetodoPago", total: 'TotalVirtual' }
    });

    $("#gridDepositosBancarios").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
				{ field: "REC_PID", width: 10, title: "Id", template: "<a id='single_2'  href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}') style='color:black;text-decoration:none;font-size:11px'>${REC_PID}</a>" },
                { field: "FECHA_INGRESO", width: 13, title: "F. Crea.", attributes: { style: "text-align:center;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${FECHA_INGRESO}</a>" },
                { field: "BNK_NAME", width: 16, title: "Banco", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${BNK_NAME}</a>" },
                { field: "REC_PWDESC", width: 18, title: "Tipo Pago", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${REC_PWDESC}</a>" },
                { field: "FECHA_DEP", width: 13, title: "F. Depósito", attributes: { style: "text-align:center;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${FECHA_DEP}</a>" },
                { field: "REC_REFERENCE", width: 16, title: "Cod. Depósito", attributes: { style: "text-align:right;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${REC_REFERENCE}</a>" },
                { field: "MONEDA", width: 12, title: "Moneda", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${MONEDA}</a>" },

                { field: "MONTO_DEPOSITADO", width: 12, title: "Monto", attributes: { style: "text-align:right;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${MONTO_DEPOSITADO}</a>" },
                { field: "BANCO_DESTINO", width: 20, title: "Banco Destino", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px; text-align:right'>${BANCO_DESTINO}</a>" },
                { field: "CUENTA_DESTINO", width: 17, title: "Cuenta Destino", attributes: { style: "text-align:right;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${CUENTA_DESTINO}</a>" },
                { field: "ESTADO_DEPOSITO", width: 15, title: "Estado", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}') style='color: black;text-decoration:none;font-size:11px'>${ESTADO_DEPOSITO}</a>" },

                { field: "FECHA_CONFIRMACION", width: 13, title: "F. Confir.", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${FECHA_CONFIRMACION}</a>" },
                { field: "REC_CODECONFIRMED", width: 14, title: "Cod Confir.", attributes: { style: "text-align:right;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color: black;text-decoration:none;font-size:11px'>${REC_CODECONFIRMED}</a>" },
                { field: "OFICINA_RECAUDO", width: 17, title: "Oficina de Recaudo", template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color: black;text-decoration:none;font-size:11px'>${OFICINA_RECAUDO}</a>" },
                { field: "SALDO_MONTO_DEPOSITADO", width: 12, title: "Saldo", attributes: { style: "text-align:right;" }, template: "<a id='single_2' href=javascript:openVentana('${REC_PID}','${REC_CONFIRMED}','${REC_BEC_ESPECIAL}','${REC_BEC_ESPECIAL_APPROVED}')  style='color:black;text-decoration:none;font-size:11px'>${SALDO_MONTO_DEPOSITADO}</a>" },
                { field: "MREC_ID", width: 5, title: 'BEC', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='ShowPopUpDetalleCobro(${MREC_ID});'  border='0' title='Ver detalle del cobro.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho                
                ////////{ field: "MREC_ID", width: 5, title: 'Ver Voucher', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/file.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho                

                //{ field: "Voucher", width: 5, title: 'VER', headerAttributes: { style: 'text-align: center' }, template: "# if (DOC_ID>'1' && DOC_VERSION==1 ) { # <img src='../Images/iconos/file.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}else if (DOC_ID>'1' && DOC_VERSION==2 ) { # <img src='../Images/iconos/file.png'  width='16'  onclick='ObtenerRutaAlfresco(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}else if (DOC_ID == '0'){ # <center></center> #} #" },
                {
                    field: "Voucher", width: 5, title: 'VER', headerAttributes: { style: 'text-align: center' }, template: "# if (DOC_ID>'1' && DOC_VERSION==1 ) " +
                                                                                                                           "{ # <img src='../Images/iconos/file.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}" +
                                                                                                                           "else if (DOC_ID>'1' && DOC_VERSION==2 )" +
                                                                                                                           "{ # <img src='../Images/iconos/file.png'  width='16'  onclick='ObtenerRutaAlfresco(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}" +
                                                                                                                           "else if (DOC_ID == '0'){ # <center></center> #} #"
                },

                {
                    field: "REC_BEC_ESPECIAL", width: 5, title: 'B.E.', headerAttributes: { style: 'text-align: center' }, template: "# if (REC_BEC_ESPECIAL==1 ) " +
                                                                                                                                    "{ # <label  width='16'  onclick='ObtenerRuta(${MREC_ID});'  style=' cursor: pointer; cursor: hand;'>SI #}" +
                                                                                                                                    "else if (REC_BEC_ESPECIAL==0 ) " +
                                                                                                                                    "{ # <label  width='16'  onclick='ObtenerRuta(${MREC_ID});'  style=' cursor: pointer; cursor: hand;'>NO #}" +
                                                                                                                                    "else if (REC_BEC_ESPECIAL != 0 && REC_BEC_ESPECIAL != 1 ){ # <center></center> #} #"
                },


                {
                    field: "REC_BEC_ESPECIAL_APPROVED", width: 10, title: 'Aprob.', headerAttributes: { style: 'text-align: center' },
                    template: "# if (REC_BEC_ESPECIAL==0 ) " +
                             "{ # <label  width='16'  onclick='ObtenerRuta(${MREC_ID});'  style=' cursor: pointer; cursor: hand;'> #}" +
                             "else if (REC_BEC_ESPECIAL==1 && REC_BEC_ESPECIAL_APPROVED==0 ) " +
                             "{ # <img src='../Images/botones/grey.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}" +
                             "else if (REC_BEC_ESPECIAL==1 && REC_BEC_ESPECIAL_APPROVED==1 ) " +
                             "{ # <img src='../Images/botones/green.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}" +
                             "else if (REC_BEC_ESPECIAL==1 && REC_BEC_ESPECIAL_APPROVED==2 ) " +
                             "{ # <img src='../Images/botones/red.png'  width='16'  onclick='ObtenerRuta(${MREC_ID});'  border='0' title='Ver voucher'  style=' cursor: pointer; cursor: hand;'> #}" +
                             "else { # <center></center> #} #"
                },


        ]
    });
};

//var estadoCon = 2;
//var estiloEstadoDeposito="<a id='single_2' href=javascript:openConfirmacion('${REC_PID}','${REC_CODECONFIRMED}') style='color: green;text-decoration:none;font-size:11px'>${ESTADO_DEPOSITO}</a>";

var ActualizarEstadoDeposito = function () {
    var pasoValidacionDuplicidadConfirmados = false;
    var estadoMsj = true; var msj = ''

    var Id = $("#hidIdDepositoBancario").val();
    var IdCobro = $("#hidIdCobro").val();
    var codConfirmacion = $("#txtCodConfirmacion").val();
    var estadoConfirmacion = $('input[name=Estado]:checked').val();
    var Observacion = $("#txtObservacion").val();

    // MENSAJES DE VALIDACION - CODIGO DE CONFIRMACION Y ESTADO 
    if (estadoConfirmacion == undefined) {
        msj += 'Seleccione el nuevo estado.\r\n'; estadoMsj = false;
    }

    if (estadoConfirmacion == 'C' && (codConfirmacion == '' || codConfirmacion.trim() == '')) {
        msj += 'Ingrese el código de confirmación.'; estadoMsj = false;
    }

    if (!estadoMsj) {
        alert(msj); return; // EL RETURN ES PARA SALIR DE LA FUNCION.
    }
    // VALIDACION DE SI EL VOUCHER YA FUE CONFIRMADO ANTERIORMENTE -  (BANCO ,CTA, FEC. DEPOSITO Y CODIGO CONFIRMACION)


    if (estadoConfirmacion == 'C') // SOLO ESTADO CONFIRMADO "C" PASA LA VALIDACION
        pasoValidacionDuplicidadConfirmados = ValidarVoucherRepetidosConfirmados(Id, codConfirmacion);
    else if (estadoConfirmacion == 'R') // RECHAZADO NO PASA VALIDACION
        pasoValidacionDuplicidadConfirmados = true


    if (pasoValidacionDuplicidadConfirmados) {
        $.ajax({
            url: '../DepositoBancario/ActualizarEstadoDeposito',
            type: 'POST',
            data: {
                id: Id,
                codigoConfirmacion: codConfirmacion,
                estado: estadoConfirmacion,
                observacion: Observacion,
                idCobro: IdCobro
            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //alert('Se actualizo el estado del depósito.');
                    var pagina = $("#gridDepositosBancarios").data("kendoGrid").dataSource.page();
                    $("#mvConfirmacion").dialog("close");

                    $('#gridDepositosBancarios').data('kendoGrid').dataSource.query({
                        idBanco: $("#ddlBanco").val(),
                        idTipoPago: $("#ddlTipoPago").val(),
                        idMoneda: $("#ddlMoneda").val(),

                        idEstadoConfirmacion: $("#ddlEstadoConfirmacion").val(),
                        CodigigoDeposito: $("#txtCodigoBancario").val(),

                        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                        FIni: $("#txtFechaIni").val(),
                        FFin: $("#txtFechaFin").val(),
                        IdBps: $("#hidEdicionEnt").val(),
                        ///////
                        idBancoDestino: $("#ddlBancoDestino").val(),
                        idCuentaDestino: $("#ddlCuentaDestino").val(),
                        montoDepositado: $('#txtMontoDepositado').val(),

                        conFechaIngreso: $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0,
                        FIniIngreso: $("#txtFechaIniIngreso").val(),
                        FFinIngreso: $("#txtFechaFinIngreso").val(),
                        IdOficina: $("#hidOficina").val(),
                        idVoucher: $("#txtID").val() == '' ? 0 : $("#txtID").val(),
                        CodigoConfirmacion: $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val(),
                        becEspecial: $("#ddlBecEspecial").val(),
                        becEspecialAprobacion: $("#ddlEstadoAprobacion").val(),
                        idCobro: $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val(),  // $("#txtIdCobro").val(),
                        page: pagina,
                        pageSize: K_PAGINACION.LISTAR_15
                    });

                    $("#gridDepositosBancarios").show();
                    $("#contenedor").hide();

                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });


    }



    if (!pasoValidacionDuplicidadConfirmados) {
        Confirmar(K_MSJ_VOUCHER_DUPLICADO,
                  function () {
                      // Confirmar_Igualmente
                      $.ajax({
                          url: '../DepositoBancario/ActualizarEstadoDeposito',
                          type: 'POST',
                          data: {
                              id: Id,
                              codigoConfirmacion: codConfirmacion,
                              estado: estadoConfirmacion,
                              observacion: Observacion,
                              idCobro: IdCobro
                          },
                          beforeSend: function () { },
                          success: function (response) {
                              var dato = response;
                              validarRedirect(dato);
                              if (dato.result == 1) {
                                  $("#mvConfirmacion").dialog("close");
                                  loadDataFiltros();
                              } else if (dato.result == 0) {
                                  alert(dato.message);
                              }
                          }
                      });

                  },
                function () { },
                function () {

                    $.ajax({
                        data: { id: Id },
                        url: '../DepositoBancario/ObtenerComprobante',
                        type: 'POST',
                        beforeSend: function () { },
                        success: function (response) {
                            var dato = response;
                            if (dato.result == 1) {
                                var comprobante = dato.data.Data;
                                LimpiarBandeja();

                                var idBancoDestino = comprobante.BNK_ID;
                                $("#ddlBancoDestino").val(idBancoDestino);
                                $('#ddlCuentaDestino option').remove();
                                $('#ddlCuentaDestino').append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
                                loadCuentaBancariaXbanco('ddlCuentaDestino', idBancoDestino, comprobante.BACC_NUMBER);

                                $('#chkConFecha').prop('checked', 'true');
                                $('#txtFechaIni').data('kendoDatePicker').enable(true);
                                $('#txtFechaFin').data('kendoDatePicker').enable(true);
                                $('#txtFechaIni').val(comprobante.FECHA_DEP);
                                $('#txtFechaFin').val(comprobante.FECHA_DEP);

                                $('#txtMontoDepositado').val(comprobante.REC_PVALUE);
                                loadDataFiltros();

                                $("#mvConfirmacion").dialog("close");
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });


                },
                'Dupicidad de deposito'
              )
    }


};

function openVentana(idSel, idEstado, estadoBecEspecial, estadoBecEspecialAceptacion) {
    //alert(K_TIPO_PERMISO_X_OFICINA);
    //alert('fer');

    if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Consulta) {
        if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 2) // Bec Especial - Rechazado - solos msj
            alert('El depósito es una Bec Especial y fue rechazado por contabilidad.');
    }
    else if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Confirmaciones) {
        if (estadoBecEspecial == 0) // Se úede confirmar
            openConfirmacion(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 1) // Bec Especial - Aceptado y se puede confirmar.
            openConfirmacion(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 2) // Bec Especial - Rechazado -- solo msj
            alert('El depósito es una Bec Especial y fue rechazado por contabilidad.');
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 0) // Bec Especial - Rechazado -- solo msj
            alert('El depósito es una Bec Especial y está pendiente de una respuesta de parte de Contabilidad.');
    }
    else if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Bec_Especiales) {
        if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 0) // Bec Especial - En espera
            openBecEspecial(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 2) // Bec Especial - Rechazado - solo msj
            alert('El depósito es una Bec Especial y fue rechazado por contabilidad.');
    }
    else if (K_TIPO_PERMISO_X_OFICINA == K_PERMISO.Permiso_Total) {
        if (estadoBecEspecial == 0) // Se úede confirmar
            openConfirmacion(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 1) // Bec Especial - Aceptado y se puede confirmar.
            openConfirmacion(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 0) // Bec Especial - En espera
            openBecEspecial(idSel, idEstado);
        else if (estadoBecEspecial == 1 && estadoBecEspecialAceptacion == 2) // Bec Especial - Rechazado
            alert('El depósito es una Bec Especial y fue rechazado por contabilidad.');
    }

}

var ActualizarEstadoDepositoModificar = function () {

    var validar = $('#txtCodConfirmacionM').val() == '' ? false : true;
    if (validar) {

        var idCobro = $('#hidIdCobroM').val();
        var idBanco = $('#ddlBancoMod').val();
        var idCuenta = $('#ddlCtaMod').val();
        var fecDeposito = $('#txtFechaDepMod').val();
        var nrOperacion = $('#txtCodConfirmacionM').val();
        ActualizarBancoDestino(idCobro, idBanco, idCuenta, fecDeposito, nrOperacion);
    } else {
        alert('Ingrese el código de confirmación.');
    }

};

function openBecEspecial(idSel, idEstado) {
    BELimpiarPoPup();
    //if (idEstado == 'S') {
    $("#BEhidIdDepositoBancario").val(idSel);
    $('#BErbtConfirmado').prop('checked', false);
    $('#BErbtRechazado').prop('checked', false);
    $("#mvBecEspecial").dialog("open");
    BEobtenerComprobante(idSel);
    //} else {
    //    alert('El comprobante no puede ser modificado.');
    //}
}

function openConfirmacion(idSel, idEstado) {
    LimpiarPoPup();
    if (idEstado == 'S') {
        $("#hidIdDepositoBancario").val(idSel);
        $('#rbtConfirmado').prop('checked', false);
        $('#rbtRechazado').prop('checked', false);
        $("#mvConfirmacion").dialog("open");
        obtenerComprobante(idSel);
    } else if (idEstado == 'C') {
        $("#mvConfirmacionModificar").dialog("open");
        obtenerComprobanteModificar(idSel);
    }
    else {
        alert('El comprobante no puede ser modificado.');
    }
}


function LimpiarPoPup() {
    $('#rbtConfirmado').prop('checked', false);
    $('#rbtRechazado').prop('checked', false);
    $('#txtCodConfirmacion').val('');
    $('#txtObservacion').val('');
}

function BELimpiarPoPup() {
    $('#BErbtConfirmado').prop('checked', false);
    $('#BErbtRechazado').prop('checked', false);
    $('#BEtxtObservacion').val('');
}

function obtenerComprobante(idSel) {

    $.ajax({
        data: { id: idSel },
        url: '../DepositoBancario/ObtenerComprobante',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var comprobante = dato.data.Data;
                ActivarBoton(comprobante.MREC_ID);
                $("#hidIdCobro").val(comprobante.MREC_ID);
                $("#lblBanco").html(comprobante.BNK_NAME);
                $("#lblTipoPago").html(comprobante.REC_PWDESC);
                $("#lblFechaDeposito").html(comprobante.FECHA_DEP);
                $("#lblVoucher").html(comprobante.REC_REFERENCE);
                $("#lblMonto").html(parseFloat(comprobante.REC_PVALUE).toFixed(2));
                $("#lblMoneda").html(comprobante.MONEDA);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function BEobtenerComprobante(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../DepositoBancario/ObtenerComprobante',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var comprobante = dato.data.Data;
                ActivarBoton(comprobante.MREC_ID);
                $("#BEhidIdCobro").val(comprobante.MREC_ID);
                $("#BElblBanco").html(comprobante.BNK_NAME);
                $("#BElblTipoPago").html(comprobante.REC_PWDESC);
                $("#BElblFechaDeposito").html(comprobante.FECHA_DEP);
                $("#BElblVoucher").html(comprobante.REC_REFERENCE);
                $("#BElblMonto").html(parseFloat(comprobante.REC_PVALUE).toFixed(2));
                $("#BElblMoneda").html(comprobante.MONEDA);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerComprobanteModificar(idSel) {

    $.ajax({
        data: { id: idSel },
        url: '../DepositoBancario/ObtenerComprobante',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var comprobante = dato.data.Data;

                $("#hidIdCobroM").val(comprobante.MREC_ID);
                $("#lblTipoPagoM").html(comprobante.REC_PWDESC);
                $("#txtFechaDepMod").val(comprobante.FECHA_DEP);

                var value = comprobante.REC_PVALUE;
                var num = value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "1,");
                $("#lblMontoM").html(num);
                $("#txtCodConfirmacionM").val(comprobante.REC_CODECONFIRMED);

                CargarDDLBancosModificar(comprobante.BNK_ID, comprobante.CUR_ALPHA, comprobante.BACC_NUMBER);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function LimpiarBandeja() {
    $('#ddlBanco').val('0');
    $('#ddlTipoPago').val('0');
    $('#ddlMoneda').val('0');
    $('#ddlEstadoConfirmacion').val('-1');
    $('#txtCodigoBancario').val('');
    $('#lbResponsable').html('Seleccione un cliente.');
    $('#hidAccionMvEnt').html('0');
    $('#hidEdicionEnt').html('0');
    $('#txtMontoDepositado').val('');
    $("#txtID").val('');
    $("#txtCodigoConfirmacion").val('');
    $("#txtIdCobro").val('');

    //$('#lbOficina').html('Seleccione un oficina de recaudo.');
    $('#lbOficina').html('Seleccione.');
    $('#hidOficina').val(0);
    $('#ddlBancoDestino').val(0);
    $('#ddlCuentaDestino').val(0);
    $("#chkConFechaIngreso").prop('checked', false);
    $('#txtFechaIniIngreso').data('kendoDatePicker').enable(false);
    $('#txtFechaFinIngreso').data('kendoDatePicker').enable(false);
    $("#gridDepositosBancarios").show();
    $("#contenedor").hide();
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



// ******* PoPup - IR A VER DETALLE COBRO ************
function ShowPopUpDetalleCobro(id) {
    var idCobro = id;
    $.ajax({
        data: { idCobro: idCobro },
        url: '../DepositoBancario/ObtenerRecibosXIdCobro',
        type: 'GET',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var recibo = dato.Code;
                //var url = '../BEC/Nuevo?id='+recibo+'&ver=N';
                var url = '../BEC/Nuevo?id=' + recibo + '&ver=N&modo=E';
                window.open(url, "_blank");
            }
        }
    });
};

//var ShowPopUpDetalleCobro = function () {
//    var idCobro = $("#hidIdCobro").val();
//    $.ajax({
//        data: { idCobro: idCobro },
//        url: '../DepositoBancario/ObtenerRecibosXIdCobro',
//        type: 'GET',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var recibo = dato.Code;                        
//                //var url = '../BEC/Nuevo?id='+recibo+'&ver=N';
//                var url = '../BEC/Nuevo?id=' + recibo + '&ver=N&modo=E';
//                window.open(url, "_blank");
//            }
//        }
//    });
//};

//function ShowPopUpDetalleCobroGrilla(id) {
//    var idCobro = id;
//    $.ajax({
//        data: { idCobro: idCobro },
//        url: '../DepositoBancario/ObtenerRecibosXIdCobro',
//        type: 'GET',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var recibo = dato.Code;
//                //var url = '../BEC/Nuevo?id='+recibo+'&ver=N';
//                var url = '../BEC/Nuevo?id=' + recibo + '&ver=N&modo=E';
//                window.open(url, "_blank");
//            }
//        }
//    });
//};

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
                loadComboDivisionesXOficina('ddlDivision', idSel, 0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ValidarVoucherRepetidosConfirmados(idVoucher, nro_confirmacion) {
    var estado = false;

    $.ajax({
        url: '../DepositoBancario/ValidarVoucherRepetidosConfirmados',
        type: 'POST',
        dataType: 'JSON',
        data: { idVoucher: idVoucher, nro_confirmacion: nro_confirmacion },
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

//function validarDescripcionVoucher(idBanco, fechaDeposito, Voucher, idVoucher) {
//    var nom = $("#txtVoucher").val();
//    if (nom != '') {
//        var noExistenVoucherRep = validarDuplicadoVoucher(idBanco, fechaDeposito, Voucher, idVoucher);
//        if (noExistenVoucherRep) {
//            $("#txtVoucher").css('border', '1px solid gray');
//            msgErrorDiv("divResultValidacionDes", "");
//            return true;
//        } else {
//            $("#txtVoucher").css('border', '1px solid red');
//            msgErrorDiv("divResultValidacionDes", K_MENSAJE_DUPLICADOVOUCHER);
//            return false;
//        }
//    }
//}

function Confirmar(dialogText, okFunc, cancelFunc, listarFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 2000px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 175,
        buttons: {

            Listar_Depositos_Duplicados: function () {
                if (typeof (listarFunc) == 'function') {
                    setTimeout(listarFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Confirmar_Igualmente: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Salir: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            },
        }
    });
}

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
                $("#btnVerImagenCobro").hide();

            }
        }


    })
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
function abrirVentana(url) {
    window.open(url, "_blank");
}


var ActualizarBecEspecial = function () {
    var pasoValidacionDuplicidadConfirmados = false;
    var estadoMsj = true; var msj = '';

    var Id = $("#BEhidIdDepositoBancario").val();
    //var IdCobro = $("#BEhidIdCobro").val();
    var estadoAproacion = $('input[name=BEEstado]:checked').val();
    var Observacion = $("#BEtxtObservacion").val();

    // MENSAJES DE VALIDACION - CODIGO DE CONFIRMACION Y ESTADO 
    if (estadoAproacion == undefined || estadoAproacion == '') {
        msj += 'Seleccione el nuevo estado.\r\n';
        estadoMsj = false;
        pasoValidacionDuplicidadConfirmados = false
    } else {
        pasoValidacionDuplicidadConfirmados = true;
    }

    if (!estadoMsj) {
        alert(msj); return; // EL RETURN ES PARA SALIR DE LA FUNCION.
    }
    // VALIDACION DE SI EL VOUCHER YA FUE CONFIRMADO ANTERIORMENTE -  (BANCO ,CTA, FEC. DEPOSITO Y CODIGO CONFIRMACION)






    if (pasoValidacionDuplicidadConfirmados) {
        $.ajax({
            url: '../DepositoBancario/ActualizarBecEspecial',
            type: 'POST',
            data: {
                id: Id,
                estadoAprobacion: estadoAproacion,
                obs: Observacion
            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //alert('Se actualizo el estado del depósito.');
                    var pagina = $("#gridDepositosBancarios").data("kendoGrid").dataSource.page();
                    $("#mvBecEspecial").dialog("close");

                    $('#gridDepositosBancarios').data('kendoGrid').dataSource.query({
                        idBanco: $("#ddlBanco").val(),
                        idTipoPago: $("#ddlTipoPago").val(),
                        idMoneda: $("#ddlMoneda").val(),

                        idEstadoConfirmacion: $("#ddlEstadoConfirmacion").val(),
                        CodigigoDeposito: $("#txtCodigoBancario").val(),

                        conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                        FIni: $("#txtFechaIni").val(),
                        FFin: $("#txtFechaFin").val(),
                        IdBps: $("#hidEdicionEnt").val(),
                        ///////
                        idBancoDestino: $("#ddlBancoDestino").val(),
                        idCuentaDestino: $("#ddlCuentaDestino").val(),
                        montoDepositado: $('#txtMontoDepositado').val(),

                        conFechaIngreso: $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0,
                        FIniIngreso: $("#txtFechaIniIngreso").val(),
                        FFinIngreso: $("#txtFechaFinIngreso").val(),
                        IdOficina: $("#hidOficina").val(),
                        idVoucher: $("#txtID").val() == '' ? 0 : $("#txtID").val(),
                        CodigoConfirmacion: $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val(),
                        becEspecial: $("#ddlBecEspecial").val(),
                        becEspecialAprobacion: $("#ddlEstadoAprobacion").val(),
                        idCobro: $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val(),  // $("#txtIdCobro").val(),
                        page: pagina,
                        pageSize: K_PAGINACION.LISTAR_15
                    });
                    $("#gridDepositosBancarios").show();
                    $("#contenedor").hide();

                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }



};



function ObtenerPermisoxOficina() {
    var permiso = 0;
    $.ajax({
        type: 'POST',
        url: '../DepositoBancario/ObtenerOficinaxOficina',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                if (dato.Code == 1)
                    permiso = K_PERMISO.Consulta;
                else if (dato.Code == 2)
                    permiso = K_PERMISO.Confirmaciones;
                else if (dato.Code == 3)
                    permiso = K_PERMISO.Bec_Especiales;
                else if (dato.Code == 4)
                    permiso = K_PERMISO.Permiso_Total;

            } else if (dato.result == 0) {
                alert('No tiene permisos');
                permiso = 0;
            }
        }


    })

    return permiso;
}

function CargarDDLBancosModificar(idBanco, idMoneda, idCta) {
    loadBancos('ddlBancoMod', idBanco);
    loadMonedas('ddlMonedaMod', idMoneda);
    loadCuentaBancariaXbanco('ddlCtaMod', idBanco, idCta, idMoneda);
    $("#ddlBancoMod option[value='39']").remove();
    $("#ddlBancoMod option[value='37']").remove();
    $("#ddlBancoMod option[value='38']").remove();

    $("#ddlBancoMod").change(function () {
        var idBancoSelect = $('#ddlBancoMod').val();
        $('#ddlCtaMod option').remove();
        loadCuentaBancariaXbanco('ddlCtaMod', idBancoSelect, '0', idMoneda);
        $("#ddlCtaMod option[value='0']").remove();
    });
}

//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

function ActualizarBancoDestino(idCobro, idBanco, idCuenta, fecDeposito, nroOperacion) {

    $.ajax({
        url: '../DepositoBancario/ActualizarBancoDestinoFecDep',
        type: 'POST',
        data: { idCobro: idCobro, idBanco: idBanco, idCuenta: idCuenta, fecDeposito: fecDeposito, nroOperacion: nroOperacion },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#mvConfirmacionModificar").dialog("close");
                alert("Se realizó la actualización del depósito.");

                var pagina = $("#gridDepositosBancarios").data("kendoGrid").dataSource.page();

                $('#gridDepositosBancarios').data('kendoGrid').dataSource.query({
                    idBanco: $("#ddlBanco").val(),
                    idTipoPago: $("#ddlTipoPago").val(),
                    idMoneda: $("#ddlMoneda").val(),

                    idEstadoConfirmacion: $("#ddlEstadoConfirmacion").val(),
                    CodigigoDeposito: $("#txtCodigoBancario").val(),

                    conFecha: $('#chkConFecha').is(':checked') == true ? 1 : 0,
                    FIni: $("#txtFechaIni").val(),
                    FFin: $("#txtFechaFin").val(),
                    IdBps: $("#hidEdicionEnt").val(),
                    ///////
                    idBancoDestino: $("#ddlBancoDestino").val(),
                    idCuentaDestino: $("#ddlCuentaDestino").val(),
                    montoDepositado: $('#txtMontoDepositado').val(),

                    conFechaIngreso: $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0,
                    FIniIngreso: $("#txtFechaIniIngreso").val(),
                    FFinIngreso: $("#txtFechaFinIngreso").val(),
                    IdOficina: $("#hidOficina").val(),
                    idVoucher: $("#txtID").val() == '' ? 0 : $("#txtID").val(),
                    CodigoConfirmacion: $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val(),
                    becEspecial: $("#ddlBecEspecial").val(),
                    becEspecialAprobacion: $("#ddlEstadoAprobacion").val(),
                    idCobro: $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val(),  // $("#txtIdCobro").val(),
                    page: pagina,
                    pageSize: K_PAGINACION.LISTAR_15
                });
                $("#gridDepositosBancarios").show();
                $("#contenedor").hide();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        failure: function (response) {
            alert("No se logro enviar la factura a actualizar.");
        }
    });
}


function ExportarReporte(formato) {
    var url = "";

    var idBanco = $("#ddlBanco").val();
    var idTipoPago = $("#ddlTipoPago").prop('selectedIndex');
    var idMoneda = $("#ddlMoneda").val();
    var idEstadoConfirmacion = $("#ddlEstadoConfirmacion").val();
    var CodigigoDeposito = $("#txtCodigoBancario").val();

    var conFecha = $('#chkConFecha').is(':checked') == true ? 1 : 0;
    var FIni = $("#txtFechaIni").attr('value');
    var FFin = $("#txtFechaFin").val();
    var IdBps = $("#hidEdicionEnt").val();
    var idBancoDestino = $("#ddlBancoDestino").val();

    var idCuentaDestino = $("#ddlCuentaDestino").val();
    var montoDepositado = $('#txtMontoDepositado').val();
    var conFechaIngreso = $('#chkConFechaIngreso').is(':checked') == true ? 1 : 0;
    var FIniIngreso = $("#txtFechaIniIngreso").val();
    var FFinIngreso = $("#txtFechaFinIngreso").val();

    var IdOficina = $("#hidOficina").val();
    var idVoucher = $("#txtID").val() == '' ? 0 : $("#txtID").val();
    var CodigoConfirmacion = $("#txtCodigoConfirmacion").val() == '' ? '0' : $("#txtCodigoConfirmacion").val();
    var becEspecial = $("#ddlBecEspecial").val();
    var becEspecialAprobacion = $("#ddlEstadoAprobacion").val();

    var idCobro = $("#txtIdCobro").val() == '' ? 0 : $("#txtIdCobro").val();
    // *************************************************************************************************************
    var repBancoDestino = $("#ddlBancoDestino").val() == 0 ? 'Todos los bancos' : $("#ddlBancoDestino option:selected").text();
    var repCuentaDestino = $("#ddlCuentaDestino").val() == 0 ? 'Todas las cuentas' : $("#ddlCuentaDestino option:selected").text();
    var repEstadoConfirmacion = $("#ddlEstadoConfirmacion").val() == -1 ? 'Confirmado, pendiente y rechazados.' : $("#ddlEstadoConfirmacion option:selected").text();
    var repFecCreacion = (conFechaIngreso == 1) ? FIniIngreso + '  -  ' + FFinIngreso : "-";
    var repFecDeposito = (conFecha == 1) ? FIni + '  -  ' + FFin : "-";

    $.ajax({
        url: '../DepositoBancario/ConsultaFiltroReporteDeposito',
        type: 'POST',
        data: {
            idBanco: idBanco,
            idTipoPago: idTipoPago,
            idMoneda: idMoneda,
            idEstadoConfirmacion: idEstadoConfirmacion,
            CodigigoDeposito: CodigigoDeposito,

            conFecha: conFecha,
            FIni: FIni,
            FFin: FFin,
            IdBps: IdBps,
            idBancoDestino: idBancoDestino,

            idCuentaDestino: idCuentaDestino,
            montoDepositado: montoDepositado,
            conFechaIngreso: conFechaIngreso,
            FIniIngreso: FIniIngreso,
            FFinIngreso: FFinIngreso,

            IdOficina: IdOficina,
            idVoucher: idVoucher,
            CodigoConfirmacion: CodigoConfirmacion,
            becEspecial: becEspecial,
            becEspecialAprobacion: becEspecialAprobacion,

            idCobro: idCobro,
            formato: formato
        },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                url = "../DepositoBancario/GenerarReporteDeposito?"
                + "formato=" + formato + "&"
                + "BancoDestino=" + repBancoDestino + "&"
                + "cuentaDestino=" + repCuentaDestino + "&"
                + "estado=" + repEstadoConfirmacion + "&"
                + "fecIngreso=" + repFecCreacion + "&"
                + "fecDeposito=" + repFecDeposito;
                
                $("#gridDepositosBancarios").hide();
                $("#contenedor").show();
                //$('#externo').attr("src", url);

                if (formato == 'PDF') {            
                    $('#externo').attr("src", url);
                }
                else if (formato == 'EXCEL') {      
                    window.open(url);
                    $("#contenedor").hide();
                }
            } else if (dato.result == 0) {
                $('#externo').attr("src", "");
                $("#contenedor").hide();
                $("#gridDepositosBancarios").hide();
                alert(dato.message);
            }
        }


    });


}
