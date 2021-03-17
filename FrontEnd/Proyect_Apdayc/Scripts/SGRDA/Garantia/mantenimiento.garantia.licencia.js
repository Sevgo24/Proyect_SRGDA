

function limpiarGarantia() {
    loadMonedaRecaudacion('ddlMonedaGar', '0');
    $("#txtValorGar").val("");
    $("#ddlTipoGar").val("0");
    $("#txtNumeroGar").val("");
    $("#txtEntidad").val("");
    $("#txtFechaReceepcion").val("");
    //$("#txtFechaDevolucion").val("");
    //$("#txtValApl").val("");
    //$("#txtValDev").val("");
    //$("#txtFechaRetencion").val("");

    $("#hidAccionMvGar").val("0");
    $("#hidEdicionGar").val("0");

    $("#lblFechaDevolucion").css("display", "none");
    $("#txtFechaDevolucion").css("display", "none");

    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, K_DIV_POPUP.NUEVO_GARANTIA);
}

function loadDataGarantia(codLic) {
     
        $.ajax({
            data: { codigoLic: codLic },
            type: 'POST', 
            url: '../Garantia/ListarGarantia',
            beforeSend: function () { },
            success: function (response) {
                var dato = response; 
                validarRedirect(dato); /*add sysseg*/
                $("#gridGarantia").html(dato.message);
            }
        });
    }

function delAddGarantia(idDel, esActivo) {
    $.ajax({
        url: '../Garantia/Eliminar',
        type: 'POST',
        data: { id: idDel, EsActivo: esActivo },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataGarantia($(K_HID_KEYS.LICENCIA).val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}


function addGarantia() {
    //dFecha: $("#txtFechaDevolucion").val(),
    //aValor: $("#txtValApl").val(),
    //dValor: $("#txtValDev").val(),
    //tFecha: $("#txtFechaRetencion").val()

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, K_DIV_POPUP.NUEVO_GARANTIA)) {
        var garantia = {
            idGarantia: $("#hidEdicionGar").val(),
            idLic: $(K_HID_KEYS.LICENCIA).val(),
            valor: $("#txtValorGar").val(),
            moneda: $("#ddlMonedaGar").val(),
            tipo: $("#ddlTipoGar").val(),
            numero: $("#txtNumeroGar").val(),
            entidad: $("#txtEntidad").val(),
            rFecha: $("#txtFechaReceepcion").val(),
            dFecha: null,
            aValor: null,
            dValor: null,
            tFecha: null,
        }
        if ($("#hidAccionMvGar").val() == 1) {
            garantia.dFecha = $("#txtFechaDevolucion").val();
            $.ajax({
                url: '../Garantia/UpdGarantia',
                data: garantia,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        //alert(dato.message);
                        msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, dato.message);
                        loadDataGarantia($(K_HID_KEYS.LICENCIA).val());
                        $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("close");
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, dato.message);
                        //alert(dato.message);
                    }
                }
            });
        } else {
            $.ajax({
                url: '../Garantia/Insertar',
                data: garantia,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        //alert(dato.message);
                        msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, dato.message);
                        loadDataGarantia($(K_HID_KEYS.LICENCIA).val());
                        $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("close");
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_GARANTIA, dato.message);
                        //alert(dato.message);
                    }
                }
            });
        }

    }

}


function updAddGarantia(idUpd,devuelto) {
    limpiarGarantia();
    $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("option", "title", "Modificar Garantía");
    $.ajax({
        url: '../Garantia/ObtenerXCodigo',
        data: { idLic: $(K_HID_KEYS.LICENCIA).val(), idGarantia: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {

                    $("#hidAccionMvGar").val("1");
                    $("#hidEdicionGar").val(obs.idGarantia);

                    $("#txtValorGar").val(obs.Valor);
                    loadMonedaRecaudacion('ddlMonedaGar', obs.moneda);
                    $("#ddlTipoGar").val(obs.tipo);
                    $("#txtNumeroGar").val(obs.numero);
                    $("#txtEntidad").val(obs.entidad);

                    var datepicker1 = $("#txtFechaReceepcion").data("kendoDatePicker");  
                    datepicker1.value(obs.FechaRecepcionChar);

                    if (devuelto) {
                        $("#lblFechaDevolucion").css("display", "inline");
                        $("#txtFechaDevolucion").css("display", "inline");

                        var datepicker2 = $("#txtFechaDevolucion").data("kendoDatePicker");
                        datepicker2.value(obs.FechaDevolucionChar);


                        $("#ddlTipoGar").attr("disabled", "disabled");
                        $("#txtNumeroGar").attr("disabled", "disabled");
                        $("#txtEntidad").attr("disabled", "disabled");
                        $("#txtFechaReceepcion").data("kendoDatePicker").enable(false);
                        $("#txtValorGar").attr("disabled", "disabled");
                        $("#ddlMonedaGar").attr("disabled", "disabled");

                        $("#txtFechaDevolucion").addClass("requerido");

                    } else {
                        $("#lblFechaDevolucion").css("display","none");
                        $("#txtFechaDevolucion").css("display", "none");


                        $("#ddlTipoGar").removeAttr("disabled");
                        $("#txtNumeroGar").removeAttr("disabled");
                        $("#txtEntidad").removeAttr("disabled");
                        $("#txtFechaReceepcion").data("kendoDatePicker").enable(true);
                        $("#txtValorGar").removeAttr("disabled");
                        $("#ddlMonedaGar").removeAttr("disabled");

                        $("#txtFechaDevolucion").val("");
                        $("#txtFechaDevolucion").removeClass("requerido");
                    }
                    //var datepicker3 = $("#txtFechaRetencion").data("kendoDatePicker");
                    //datepicker3.value(obs.FechaRetencion);
                    //$("#txtValApl").val(obs.ValorAplicado);
                    //$("#txtValDev").val(obs.ValorDevuelto);

                    $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("open");
                } else {
                    alert("No se pudo obtener la Garantia para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}



function devolverGarantia() {
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DEVOLVER_GARANTIA, K_DIV_POPUP.DEVOLVER_GARANTIA)) {
        var garantia = {
            idGarantia: $("#hidIdGarantiaDev").val(),
            idLic: $(K_HID_KEYS.LICENCIA).val(),
            dFecha: $("#txtFechaDevol").val()
        }
            $.ajax({
                url: '../Garantia/Devolver',
                data: garantia,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        //alert(dato.message);
                        msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DEVOLVER_GARANTIA, dato.message);
                        loadDataGarantia($(K_HID_KEYS.LICENCIA).val());
                        $("#" + K_DIV_POPUP.DEVOLVER_GARANTIA).dialog("close");
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DEVOLVER_GARANTIA, dato.message);
                        //alert(dato.message);
                    }
                }
            });
    }
}

function devolver(idGarantia) {
    $("#txtFechaDevol").css({ 'border': '1px solid gray' });
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DEVOLVER_GARANTIA,"");
    $("#hidIdGarantiaDev").val(idGarantia);
    $("#" + K_DIV_POPUP.DEVOLVER_GARANTIA).dialog("open");
}