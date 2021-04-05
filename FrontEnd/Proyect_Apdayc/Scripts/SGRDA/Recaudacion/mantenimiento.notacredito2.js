var K_DIV_VALIDAR = { DIV_CAB: "divCabeceraNC" };
var K_DIV_MESSAGE = { DIV_NOTACREDITO: "divMensajeError" };
var K_MENSAJE_TIPO_NOTA_CREDITO = "Seleccione un Tipo de Nota de Credito.";
var K_WIDTH_OBS2_DOC = 600;
var K_HEIGHT_OBS2_DOC = 325;
var K_ES_SOLICITUD_DOCUMENTO_PENDIENTE_CANCELADO = 7;
var K_ES_MODIF_DOC_MANUAL = 3;
var K_ES_MODIF_DOC_PEND_CANC = 4;
var K_MODIFICAR_DOCUMENTO_MANUAL = 6;

$(function () {

    mvInitNotaCredito2({ container: "ContenedormvNotaCredito", idButtonToSearch: "btnNotaCredito2", idDivMV: "mvNotaCredito2", event: "reloadEventoNotaCredito", idLabelToSearch: "" });
    mvInitDevolucion({ container: "ContenedormvDevolucion", idButtonToSearch: "", idDivMV: "mvDevolucion", event: "", idLabelToSearch: "" });

    $("#btnBuscarDemo2").on("click", function () {
      
        var idMoneda = $('#ddlMoneda').val();
        //if (idMoneda != '0')
        ConsultaDocumento2();
    });
    $("#btnLimpiar2").on("click", function () {
        $("#grid2").html('');
        limpiarControles2();
    });
    $("#btnLimpiarNC").on("click", function () {
      
        limpiarTipoNotaCredito();
    });

    $("#ValidarNC").on("click", function () {
        
        ValidarNCredito();
    });
    $("#FiltrarNC").on("click", function () {
        var idSel = $("#hidId2").val()
        loadDataDetalloFactura2(idSel);
    });
    $("#btnNotaCredito2").on("click", function () {
        limpiarNotaCredito();
        var validacion = ValidarFechaNotaCredito($("#hidId2").val());
        if (validacion) {
            var estadoNC = ObtenerDatosCabecera2($("#hidId2").val());
            if (estadoNC) {
                $("#mvNotaCredito2").dialog("open");
                ObtenerDetalleFactura2($("#hidId2").val());
            }
        }
        else
            alert("No se puede crear nota de crédito para esta factura, no se encuentra dentro del mes");
    });
    $("#ddlTipoNC2").change(function () {
        var valor = $("#ddlTipoNC2").val();
        if (valor==1) {
            $("#TipoNCI").val($("#txtNeto2").val());
        } else {
            if (valor == 2) {
                $("#TipoNCI").val(100);
            }
            
        }
    });
    $('#chkdetalladoNC').click(function () {
        if ($('#chkdetalladoNC').is(':checked')) {
            $('#TipoNCI').val('');
            //$('#TipoNCI').prop('readonly', false);
            $('#TipoNCI').prop('disabled', true);
            //al Selecccionar si el monto cobrado es 0 entonces obtener el valor del monto Neto
            $("#gridDetalleFactura2").css("display", "inline");
            $("#gridDetalleFacturaFiltro").css("display", "inline");
            
            //ActivartodoslosCheckbox();

        } else {
            $('#TipoNCI').text = '';
            $('#TipoNCI').attr('disabled', false);
            $("#gridDetalleFactura2").css("display", "none");
            $("#gridDetalleFacturaFiltro").css("display", "none");
            //DesactivartodoslosCheckBox();

        }
    });
    
});
function limpiarControles2() {
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
    $("#hidSerie").val('');
    $("#hidActual").val(0);

    $("#chkNoAnu").prop('checked', false);
    $("#chkNoImp").prop('checked', false);
    $("#grid2").html('');
    $("#CantidadRegistros").html('0');
}
var mvInitNotaCredito2 = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdViewNC" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventNC" value="' + parametro.event + ' "/>';
    elemento += '<table border=0 style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td><div class="contenedor" id="divNC"><table border=0 style=" width:65%; "><tr>';
    elemento += '<td style="width:60px"> Serie NC: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <input type="hidden" id="hidIdSerieNC2" style="width:60px; text-align:center;"/><input type="text" id="textSerieNC2" style="width:60px; text-align:center;" disabled="disabled"/> <label id="lblSer" style="cursor:pointer;" alt="Búsqueda de Serie Nota Crédito" title="Búsqueda de Serie Nota Crédito">Serie</label> </td>';
    elemento += '<td> - <input type="text" id="txtCorrelativoNC2" style="width:60px; text-align:center;" readonly="true" class="requerido" disabled="disabled"></td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td></tr></table>';
    elemento += '</div></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor" id="divTipoNC"><table border=0 style=" width:100%; border:0px; ">';
    elemento += '<tr>';
    elemento += '<td style="width:60px">Tipo NC </td> <td style="width:10px"><select id="ddlTipoNC2" style="width: 190px;" class="requeridoLst"><option value="1">Monto</option><option value="2">Porfcentaje</option></select></td><td style="width:100px;"><input type="text" id="TipoNCI" style="width: 80px"></td>';
    elemento += '<td style="width:60px">Fecha Emisión </td> <td><input type="text" id="txtFechaE2" style="width: 100px"></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '<td style="width:60px">Tipo Sunat </td> <td style="width:10px"><select id="ddlTipoNotaCredito2" style="width: 190px;" class="requeridoLst"/></td><td  style="width:100px;"><textarea type="text" id="txtObservacion2" rows="3"></textarea></td>';
    elemento += '<td style="width:60px"><button id="btnLimpiarNC" class="boton" name="btnLimpiarNC">Limpiar<img src="../Images/botones/refresh.png" width="24px" /></button></td><td style="width:100px"><button id="ValidarNC" class="boton" name="ValidarNC">Validar<img <img src="../Images/botones/buscar.png" width="24px" /></button></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</div>';
    
    elemento += '</td';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor" id="divCabeceraNC">';
    elemento += '<table border=0 style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td style="width:80px">Tipo </td> <td style="width:10px"><select id="ddlTipoFactNC2" style="width: 190px" class="requeridoLst"/></td>';
    elemento += '<input type="hidden" id="hidId2" />';

    //elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarSerieNC" style="cursor:pointer;" alt="Búsqueda de Serie Nota Crédito" title="Búsqueda de Serie Nota Crédito"/> </td>';
    //elemento += '<td> <input type="hidden" id="hidIdSerieNC"/> <label id="lblSer" style="cursor:pointer;" alt="Búsqueda de Serie Nota Crédito" title="Búsqueda de Serie Nota Crédito">Serie</label> </td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:40px">Serie </td> <td><input type="text" id="txtSerie2" style="width:100px" disabled="disabled"></td>';
    elemento += '<td style="width:40px">Número </td> <td><input type="text" id="txtNumero2" style="width:123px" disabled="disabled"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:60px">Usuario Derecho </td> <td><input type="text" id="txtUsuarioDerecho2" style="width:300px" disabled="disabled"></td>';
    elemento += '<td style="width:60px">Moneda </td> <td style="width:7px"><select id="ddlMonedas2" style="width: 190px" disabled="disabled"/></td>';
    elemento += '</tr>';

    //elemento += '<tr>';
    ////elemento += '<td style="width:80px">Fecha Vencimiento </td> <td><input type="text" id="txtFechaV" disabled="disabled"></td>';
    //elemento += '<td style="width:80px">Fecha Emisión </td> <td><input type="text" id="txtFechaE" disabled="disabled"></td>';
    //elemento += '</tr>';
    //Check box para aplicar el monto total .
    elemento += '<tr>';
    elemento += '<td style="width:10px; text-align:right;"> <input type="checkbox" id="chkdetalladoNC" value="0"></td>';
    elemento += '<td>Habilitar detallado</td>';
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
    elemento += '</td>';

    elemento += '<table  border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td colspan="10"><div style="display: none;" id="gridDetalleFacturaFiltro">';
    elemento += '<table>';
    elemento += '<tr>';
    elemento += '<td style="width:40px">Mes </td> <td><input type="text" id="mesNC" style="width:100px" ></td>';
    elemento += '<td style="width:40px">Año </td> <td><input type="text" id="anioNC" style="width:123px"></td>';
    elemento += '<td style="width:40px">Licencia </td> <td><input type="text" id="licenciaNC" style="width:123px" ></td>';
    elemento += '<td style="width:100px"><button id="FiltrarNC" class="boton" name="ValidarNC">Filtrar<img <img src="../Images/botones/buscar.png" width="24px" /></button></td>';

    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</div></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '<td colspan="10"><div style="display: none;" id="gridDetalleFactura2"></div></td>';
    elemento += '</tr>';
    elemento += '</table>';

    //elemento += '<table  border="0" style="border-collapse: collapse;">';
    //elemento += '<tr>';
    //elemento += '<td style="width:10px"></td>';
    //elemento += '<td style="width:5px"></td>';
    //elemento += '<td style="width:10px"></td>';
    //elemento += '<td style="width:100px"></td>';
    //elemento += '<td style="width:100px"></td>';
    ////Periodo
    //elemento += '<td style="width:55px"></td>';
    ////Base
    //elemento += '<td style="width:105px; text-align:center;"><input type="text" id="txtBase2" readonly="true" style="width:70px"></td>';
    ////Impuesto
    //elemento += '<td style="width:105px; text-align:center;"><input type="text" id="txtImpuesto2" readonly="true" style="width:70px"></td>';
    ////Neto
    elemento += '<input type="hidden" id="txtNeto2"  style="width:70px">';
    elemento += '<input type="hidden" id="devolucion"  style="width:70px">';
    ////BaseCobrado
    //elemento += '<td style="width:130px; text-align:center;"><input type="text" id="txtBaseCobrado2" readonly="true" style="width:70px"></td>';
    ////ImpCobrado
    //elemento += '<td style="width:150px; text-align:center;"><input type="text" id="txtImpuestoCobrado2" readonly="true" style="width:70px"></td>';
    ////NetoCobrado
    //elemento += '<td style="width:110px; text-align:center;"><input type="text" id="txtNetoCobrado2" readonly="true" style="width:70px"></td>';
    ////Pendiente
    //elemento += '<td style="width:130px; text-align:center;"><input type="text" id="txtPendiente2" readonly="true" style="width:70px"></td>';
    //elemento += '<td style="width:20px"></td>';
    //elemento += '</tr>';
    //elemento += '</table>';
    elemento += '<table  border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td colspan="10"><div id="gridResumenNC"></div></td>';
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
        buttons: { "Guardar": Guardar2, "Cancelar": function () { $("#mvNotaCredito2").dialog("close"); } }
    });
   
    //$("#txtFechaV").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaE2").kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    $("#txtFechaE2").data("kendoDatePicker").value(fechaActual);
    $('#txtFechaE2').data('kendoDatePicker').enable(false);
    loadMonedas('ddlMonedas2', 'PEN');
    loadTipoFactura('ddlTipoFactNC2', '3');


    //$('#ddlTipoFactNC').selected('3');
    //$("#ddlTipoFactNC").on("change", function () {
    //    alert($("#ddlTipoFactNC").val());
    //})
    //var eventoKP = "keypress";
    //$('#txtValorNotaCredito_' + id).on(eventoKP, function (e) { return solonumeros(e); });

};
var mvInitDevolucion = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdViewNC" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventNC" value="' + parametro.event + ' "/>';
    elemento += '<table border=0 style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td>Se va emitir una Nota de crédito para devolución total de la factura. ¿Desea continuar?';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</div>';
    $("#" + parametro.container).append(elemento);

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 380,
        height: 150,
        title: "DEVOLUCIÓN",
        buttons: { "CONTINUAR": CONTINUARNC, "Cancelar": function () { $("#mvDevolucion").dialog("close"); } }
    });
}
function ConsultaDocumento2() {
    $("#btnNotaCredito2").hide();
    $("#contenedor").hide();
    var idSerial = $("#hidSerie").val();
    var numFact = $("#txtNumFact").val() == '' ? 0 : $("#txtNumFact").val();
    var idFactura = $("#txtIdFact").val() == '' ? 0 : $("#txtIdFact").val();

    //var idSocio = $("#hidEdicionEnt").val();
    //var idGrupoFacturacion = $("#hidGrupo").val();
    //var idGrupoEmpresarial = $("#hidCodigoGrupoEmpresarial").val();

    var conFecha = $('#chkConFecha').is(':checked') == true ? 1 : 0;
    var Fini = $("#txtFecInicial").val();
    var Ffin = $("#txtFecFinal").val();
    //var idLicencia = $("#txtNumLic").val() == '' ? 0 : $("#txtNumLic").val();

    //var idDivision = 0;
    var idOficina = $("#hidOficina").val();

    $.ajax({
        url: '../ConsultaDocumento/ConsultaDocumento2',
        type: 'POST',
        data: {
            idSerial: idSerial, numFact: numFact, idFactura: idFactura, idOficina: idOficina,
            conFecha: conFecha, Fini: Fini, Ffin: Ffin
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#grid2").html(dato.message);
                $("#CantidadRegistros2").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#grid2").html('');
                $("#CantidadRegistros2").html("");
                alert(dato.message);
            }
        }
    });
}
function obtenerId2(id, fa, habNC, quiebra, nota_Credito) {
    //alert(habNC);
    $("#hidId2").val(0);
    $("#devolucion").val(0);
    if (habNC === 1) {
        var pendienteEstado = fa;
       
        $("#hidId2").val(id);
        if (pendienteEstado!=0) {
            abrirNC(pendienteEstado);

        } else {
            $("#devolucion").val(pendienteEstado);
            $("#mvDevolucion").dialog("open");
           
        }
    } else {
        $("#hidId2").val(0);
        $("#btnNotaCredito2").hide();
    }
}
function abrirNC(pendiente)
{
    limpiarNotaCredito();
    limpiarTipoNotaCredito();
    var validacion = ValidarFechaNotaCredito($("#hidId2").val());
    if (validacion) {
        var estadoNC = ObtenerDatosCabecera2($("#hidId2").val());
        if (estadoNC) {
            $("#ddlTipoNotaCredito2").prop('disabled', false);
            if (pendiente == 0) {
                loadTipoNotaCredito('ddlTipoNotaCredito2', 06);
                $("#ddlTipoNotaCredito2").prop('disabled', true);
            }
            else {
                loadTipoNotaCredito('ddlTipoNotaCredito2', 0);
            }
            $("#mvNotaCredito2").dialog("open");
            ObtenerDetalleFactura2($("#hidId2").val());
        }
    }
    else {
        alert("No se puede crear nota de crédito para esta factura, no se encuentra dentro del mes");
    }

}
function ObtenerDatosCabecera2(idFactura,pendiente) {
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
                    $("#txtSerie2").val(en.NMR_SERIAL);
                    $("#txtNumero2").val(en.INV_NUMBER);
                    loadMonedas('ddlMonedas2', en.CUR_ALPHA);
                    $("#txtUsuarioDerecho2").val(en.SOCIO);
                    loadTipoFactura('ddlTipoFactNC2', '3');
                    
                    $("#ddlTipoFactNC2").prop('disabled', true);

                    if (en.INV_TYPE == 1) { $("#textSerieNC2").val('BN42'); $("#hidIdSerieNC2").val('1067'); }
                    if (en.INV_TYPE == 2) { $("#textSerieNC2").val('FN42'); $("#hidIdSerieNC2").val('1055'); }
                    if (en.INV_TYPE == 3) { $("#textSerieNC2").val('0342'); $("#hidIdSerieNC2").val('961'); }
                    //var d1 = $("#txtFechaV").data("kendoDatePicker");
                    //var valFecha = formatJSONDate(en.INV_DATE);
                    //d1.value(valFecha);
                    obtenerNombreCorrelativoNC2($("#hidIdSerieNC2").val());
                    var d1 = $("#txtFechaE2").data("kendoDatePicker");
                    $("#txtFechaE2").data("kendoDatePicker").value(new Date());
                    var d = $("#txtFecInicial").data("kendoDatePicker").value();
                    //var valFecha = formatJSONDate(en.INV_DATE);
                    //d1.value(valFecha);
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

function calcularTotalBase2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumB").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtBase2").val(total);
            }
        }
    });
}

function calcularTotalImpuesto2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        //var solicitado = parseFloat($(this).find("td").eq(9).html());
        var solicitado = $(this).find(".sumI").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtImpuesto2").val(total);
            }
        }
    });
}

function calcularTotalNeto2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumN").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtNeto2").val(total);
            }
        }
    });
}

function calcularTotalBaseCobrado2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumBC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtBaseCobrado2").val(total);
            }
        }
    });
}

function calcularTotalImpuestoCobrado2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumIC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtImpuestoCobrado2").val(total);
            }
        }
    });
}

function calcularTotalNetoCobrado2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumNC").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtNetoCobrado2").val(total);
            }
        }
    });
}

function calcularTotalPendiente2() {
    var total = 0;
    $('#tblDetalleFactura2 tr').each(function () {
        var solicitado = $(this).find(".sumP").html();
        if (!isNaN(solicitado)) {
            if (solicitado != null) {
                total = parseFloat(total) + parseFloat(solicitado);
                $("#txtPendiente2").val(total);
            }
        }
    });
}
function loadDataDetalloFactura2(id) {
    var mes = $('#mesNC').val();
    var anio = $('#anioNC').val();
    var lic = $('#licenciaNC').val();
    var licencia = 0;
    if (lic != "") licencia = lic;
    loadDataGridTmpDet2('../FacturacionConsulta/ListarDetalleFactura2', "#gridDetalleFactura2", id, mes,anio,licencia);
}

function loadDataGridTmpDet2(Controller, idGrilla, id, mes, anio, idLic) {
    $.ajax({
        type: 'POST', data: { invId: id, mes: mes, anio: anio, idLic: idLic}, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html('');
                $(idGrilla).html(dato.message);
                //calcularTotalBase2();
                //calcularTotalImpuesto2();
                //calcularTotalNeto2();
                //calcularTotalBaseCobrado2();
                //calcularTotalImpuestoCobrado2();
                //calcularTotalNetoCobrado2();
                //calcularTotalPendiente2();

                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function loadValorNeto(id) {
    $.ajax({
        type: 'POST', data: { Id: id }, url: '../FacturacionConsulta/DetalleValorNeto', beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#txtNeto2').val(dato.message);
                var valor = $("#txtNeto2").val();
                $("#TipoNCI").val(valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function ObtenerDetalleFactura2(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../FacturacionConsulta/ObtenerDetalleFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDetalloFactura2(idSel);
                loadValorNeto(idSel);
                
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function obtenerNombreCorrelativoNC2(idSel) {
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
                //$("#lblSer").html(cor.NMR_SERIAL);
                $("#hidIdSerieNC2").val(cor.NMR_ID);
                //$("#hidActual").val(cor.NMR_NOW);
                //$("#lblSer").css('color', 'black');
                $("#txtCorrelativoNC2").val(cor.NMR_NOW);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function limpiarTipoNotaCredito() {
    $("#ddlTipoNC2").val(1);
    $("#TipoNCI").val('');
    $("#txtObservacion2").val('');
    $("#gridResumenNC").html('');
    $("#chkdetalladoNC").prop("checked", false);
    $("#gridDetalleFactura2").css("display", "none");
    $("#gridDetalleFacturaFiltro").css("display", "none");
};
function ValidarNCredito() {
    var hidId2 = $("#hidId2").val();
    var txtFechaE2 = $("#txtFechaE2").val();
    var ddlTipoNC2 = $("#ddlTipoNC2").val();
    var TipoNCI = $("#TipoNCI").val();  
    var textSerieNC2 = $("#textSerieNC2").val();
    var txtCorrelativoNC2 = $("#txtCorrelativoNC2").val();
    var txtNeto2 = $("#txtNeto2").val();
    var txtSerie2 = $("#txtSerie2").val();
    var txtNumero2 = $("#txtNumero2").val();
    var ddlTipoNotaCredito2 = $("#ddlTipoNotaCredito2").val();
    var txtObservacion2 = $("#txtObservacion2").val();
    var textoTipoSunat = $("#ddlTipoNotaCredito2").find('option:selected').text();
    var detalladoEstado = false;
    if ($('chkdetalladoNC').is(':checked')) { detalladoEstado = true; }
    
    loadResumenNC(hidId2, ddlTipoNC2, TipoNCI, txtFechaE2, textSerieNC2, txtCorrelativoNC2, txtNeto2, txtSerie2, txtNumero2, ddlTipoNotaCredito2, txtObservacion2, textoTipoSunat);
};
function detalladoResultado() {
    var detalleFactura = [];
    var contador = 0;

    $('#tblDetalleFactura2 tr').each(function () {
        var idLic = $(this).find(".idLic").html();//ID FACTURA

        if (!isNaN(idLic) && idLic != null) {
            //$("#chkFact" + id_invl).prop("checked", true);
            //var montoPendienteDet = parseFloat($(this).find("td").eq(numPenDet).html());
            $('#FiltroTabla_' + idLic + ' tr').each(function () {
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
            
        }
    })
   
}
function loadResumenNC(hidId, TipoNC, TextNC, FechaEmision, SerieNC, CorreelativoNC, MontoNeto, txtSerie2, txtNumero2, ddlTipoNotaCredito2, txtObservacion2, textoTipoSunat) {

    var neto = new Number(MontoNeto);
    var monto = new Number(TextNC);
    var elemento = '<div id="divResumen" style="text-align:center;"> ';
    elemento += '<table border=0 style=" width:200%; border:0px;">';
    elemento += '<tr>';
    if (TipoNC == 1 && neto >= monto && TextNC != "" && ddlTipoNotaCredito2 != 0 && txtObservacion2 != "") {
        elemento += '<td style="font-size:15px;">Se va emitir una Nota de Crédito con Serie NC ' + SerieNC + ' con Referencial ' + CorreelativoNC + '</td>';
        elemento += '</tr>';
        elemento += '<tr>';
        elemento += '<td style="font-size:15px;">A la factura con Serie ' + txtSerie2 + ', número ' + txtNumero2 +' con Fecha de Emision <strong>' + FechaEmision + '</strong></td>';
        elemento += '</tr>';

        elemento += '<tr>';
        elemento += '<td style="font-size:15px;">Con titpo Sunat <strong>' + textoTipoSunat + '</strong> y observación: <strong>' + txtObservacion2 +'</strong></td>';
        elemento += '</tr>';

        elemento += '<tr>';
        elemento += '<td style="font-size:15px;">Por el Monto de <strong>S/.' + TextNC + '</strong> del monto total S/.' + MontoNeto + '</td>';
        elemento += '</tr>';     
   
        elemento += '</table>';
        elemento += '</div>';
        $("#gridResumenNC").html(elemento);
    }
    else
    {
        if (TipoNC == 2 && (monto >= 0 && 100 >= monto) && monto != "" && ddlTipoNotaCredito2 != 0 && txtObservacion2 != "") {

            elemento += '<td style="font-size:15px;">Se va emitir una Nota de Crédito con Serie NC ' + SerieNC + ' con Referencial ' + CorreelativoNC + '</td>';
            elemento += '</tr>';
            elemento += '<tr>';
            elemento += '<td style="font-size:15px;">A la factura con Serie ' + txtSerie2 + ', número ' + txtNumero2 + ' con Fecha de Emision <strong>' + FechaEmision + '</strong></td>';
            elemento += '</tr>';

            elemento += '<tr>';
            elemento += '<td style="font-size:15px;">Con titpo Sunat <strong>' + textoTipoSunat + '</strong> y observación: <strong>' + txtObservacion2 + '</strong></td>';
            elemento += '</tr>';

            elemento += '<tr>';
            elemento += '<td style="font-size:15px;">Por el Porcentaje de <strong>' + TextNC + '%</strong> del monto total S/.' + MontoNeto + '</td>';
            elemento += '</tr>';
            elemento += '</table>';
            elemento += '</div>';
            $("#gridResumenNC").html(elemento);
        }
        else {
            elemento += '<td style="font-size:15px;"><strong>Datos inválidos</strong></td>';
            elemento += '</tr>';
            elemento += '</table>';
            elemento += '</div>'
            $("#gridResumenNC").html(elemento);
        } 
    }
}
function CONTINUARNC() {
   var pendiente = $("#devolucion").val();
    $("#mvDevolucion").dialog("close");
    abrirNC(pendiente);   
}

function Guardar2() {
    var hidId2 = $("#hidId2").val();
    var txtFechaE2 = $("#txtFechaE2").val();
    var ddlTipoNC2 = $("#ddlTipoNC2").val();
    var TipoNCI = $("#TipoNCI").val();
    var textSerieNC2 = $("#textSerieNC2").val();
    var txtCorrelativoNC2 = $("#txtCorrelativoNC2").val();
    var txtNeto2 = $("#txtNeto2").val();
    var txtSerie2 = $("#txtSerie2").val();
    var txtNumero2 = $("#txtNumero2").val();
    var ddlTipoNotaCredito2 = $("#ddlTipoNotaCredito2").val();
    var txtObservacion2 = $("#txtObservacion2").val();
    var neto = new Number(txtNeto2);
    var monto = new Number(TipoNCI);
    if (((ddlTipoNC2 == 1 && neto >= monto && monto != "") || (ddlTipoNC2 == 2 && (monto >= 0 && 100 >= TipoNCI) && TipoNCI != "")) && ddlTipoNotaCredito2 != 0 && txtObservacion2 != "")
    {
        $.ajax({
            data: { idFactura: hidId2, fechaEmision: txtFechaE2, TipoNC: ddlTipoNC2, TextoTipoNC: TipoNCI, TipoSunat: ddlTipoNotaCredito2, Observacion: txtObservacion2, serieNC: textSerieNC2},
            url: '../ConsultaDocumento/GuardarNC2',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;

                if (dato.result = 1) {
                    alert(dato.message);


                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvNotaCredito2").dialog("close");
    }
    else
    {
        alert('LLene todos los campos');
    }

};
function ActivarCheckCabecera(idLic,pendienteCab) {
    
    //si Pendiente es 0 entonces Quiere decir que Ya se cobro todo el neto 

    var numPenDet = 6;

    //$("#txtValorNotaCredito_" + id).prop('disabled', false);
    //$("#txtValorNotaCredito_" + id).val(pendienteCab);
            
   
    if ($("#chkFact" + idLic).prop("checked")) {
        $('#FiltroTabla_' + idLic + ' tr').each(function () {
            var id_invl = $(this).find(".idLic").html();//ID FACTURA

            if (!isNaN(id_invl) && id_invl != null) {
                $("#chkFact" + id_invl).prop("checked", true);
                var montoPendienteDet = parseFloat($(this).find("td").eq(numPenDet).html());
                Habilitar2(idLic, id_invl, montoPendienteDet);
            }
        })
    } else {
        $('#FiltroTabla_' + idLic + ' tr').each(function () {
            var id_invl = $(this).find(".idLic").html();//ID FACTURA

            if (!isNaN(id_invl) && id_invl != null) {
                $("#chkFact" + id_invl).prop("checked", false);
                //var montoPendienteDet = parseFloat($(this).find("td").eq(numPenDet).html());
                Habilitar2(idLic, id_invl, 0);
            }
        })
    }
    Habilitar2(idLic, 0, pendienteCab);
    
   
};
function Habilitar2(idlic, idinvl,pendiente) {
    if (idinvl == 0) {
        if ($("#chkFact" + idlic).prop("checked")) {
            $("#txtValorNotaCredito_" + idlic).removeAttr('disabled');
            $("#txtValorNotaCredito_" + idlic).val(pendiente);
        }
        else {
            $("#txtValorNotaCredito_" + idlic).attr('disabled', 'disabled');
            $("#txtValorNotaCredito_" + idlic).val('');
        }
    }
    else {
        if ($("#chkFact" + idinvl).prop("checked")) {

            var notaPendienteDet = Number( $("#txtValorNotaCredito_" + idinvl).val());
            var notaPendienteCab = Number($("#txtValorNotaCredito_" + idlic).val());
            if ($("#chkFact" + idlic).prop("checked") && (pendiente != "")) {

                $("#txtValorNotaCredito_" + idlic).val(notaPendienteCab + pendiente);
            }
            $("#txtValorNotaCredito_" + idinvl).removeAttr('disabled');
            $("#txtValorNotaCredito_" + idinvl).val(pendiente);
        }
        else {
            var notaPendienteDet = Number($("#txtValorNotaCredito_" + idinvl).val());
            var notaPendienteCab = Number($("#txtValorNotaCredito_" + idlic).val());
            if ($("#chkFact" + idlic).prop("checked") && (notaPendienteCab != "" && notaPendienteCab > 0 )) {
                
                $("#txtValorNotaCredito_" + idlic).val(notaPendienteCab - pendiente);
            }
            $("#txtValorNotaCredito_" + idinvl).attr('disabled', 'disabled');
            $("#txtValorNotaCredito_" + idinvl).val('');
        }
    }
   
}
