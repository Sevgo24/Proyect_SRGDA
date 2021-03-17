var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var totalIngresado = 0;
var totalrecibo = 0;

var K_DIV_VALIDAR = {
    DIV_CAB: "divCabeceraPago"
};
var K_DIV_MESSAGE = {
    DIV_PAGO: "divMensajeError",
    DIV_TAB_POPUP_PAGO: "avisoMetodoPago"
};

var K_DIV_POPUP = {
    PAGO: "mvDetalleFormaPago"
};

$(function () {
    var eventoKP = "keypress";
    $("#hidTotal").val(0);
    $("#txtImporte").val(0);
    $("#btnRegistrarPago").hide();
    $("#tablaTotal").hide();
    $('#txtImporte').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtValor').on(eventoKP, function (e) { return solonumeros(e); });

    $(".addFormaPago").on("click", function () {
        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_PAGO, "");
        $('#ddlFormaPago').css({ 'border': '1px solid gray' });
        $('#txtValor').css({ 'border': '1px solid gray' });
        //$('#ddlBanco').css({ 'border': '1px solid gray' });
        //$('#ddlSucursal').css({ 'border': '1px solid gray' });
        $('#txtFecha').css({ 'border': '1px solid gray' });
        //$('#txtReferencia').css({ 'border': '1px solid gray' });
        //$('#ddlCuenta').css({ 'border': '1px solid gray' });


        if ($("#txtImporte").val() != "0" && $("#txtCorrelativo").val() != "") {
            $('#txtImporte').css({ 'border': '1px solid gray' });
            $('#txtCorrelativo').css({ 'border': '1px solid gray' });
            limpiar();
            $("#mvDetalleFormaPago").dialog("open");
        }
        else {
            $("#txtImporte").css({ 'border': '1px solid red' });
            $("#txtCorrelativo").css({ 'border': '1px solid red' });
            $("#mvDetalleFormaPago").dialog("close");
            msgErrorB(K_DIV_MESSAGE.DIV_PAGO, "Debe ingresar los campos requeridos");
        }
    });

    mvInitBuscarCorrelativoRecibo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitDetalleFormaPago({ container: "ContenedormvDetalleFormaPago", idButtonToSearch: "btnMetodoPago", idDivMV: "mvDetalleFormaPago", event: "reloadEventoAgregar", idLabelToSearch: "" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    var id = GetQueryStringParams("id");
    var idRecibo = GetQueryStringParams("idRecibo");
    if (idRecibo == undefined)
        idRecibo = 0;
    else
        $("#hidRecibo").val(idRecibo);

    //alert("Recibo : " + idRecibo + "  Usuario : " + id);
    $("#hidCodigoBPS").val(id);

    if (idRecibo === 0 || id === 0) {
        //alert("1");
        $("#divTitulo").html("PAGOS - NUEVOS");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        obtenerNombreSocio(id);
    }
    else {
        //alert("2");
        //alert("Recibo : " + idRecibo + "  Usuario : " + id);
        $("#divTitulo").html("PAGOS - NUEVOS");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);

        if ($("#hidCodigoBPS").val() != 0) {
            obtenerNombreSocio(id);
            ObtenerDetalleFormaPago(idRecibo);
            BuscarFacturas();
        }

        ObtenerSerie(idRecibo);
        document.getElementById("txtImporte").readOnly = true;
        $("#btnGrabarRecibo").hide();
        $("#trAgregar").hide();
    }

    $("#btnDescartar").on("click", function () {
        location.href = "../Cobro/";
    }).button();

    $("#btnGrabarRecibo").on("click", function () {
        if ($("#hidCodigoBPS").val() != 0) {
            //if ($("#txtImporte").val() != 0) {
            //alert("totalIngresado " + totalIngresado + " " + "totalrecibo " + totalrecibo);
            //if (totalIngresado < totalrecibo) {
            //    alert("La suma total del importe de los métodos de pago, no debe ser menor al importe del recibo");
            //    estado = false;
            //}
            //else if (totalIngresado == totalrecibo) {
            //    GrabarRecibo();
            //}
            //}
            //else {
            //alert("Ingrese importe.");
            //$("#txtImporte").focus();
            //}
            GrabarRecibo();
        }
        else
            alert("Busque al usuario de derecho.");
    }).button();

    $("#btnRegistrarPago").on("click", function () {
        var ValorCancelar = $("#txtTotal").val();
        var ImporteRecibo = $("#txtImporte").val();

        if (parseFloat(ValorCancelar) > parseFloat(ImporteRecibo)) {
            alert("El valor a cancelar no puede ser mayor al total ingresado en importe del recibo");
        }
        else if (parseFloat(ValorCancelar) < parseFloat(ImporteRecibo)) {
            alert("El valor a cancelar no puede ser menor al total ingresado en importe del recibo");
        }
        else {
            FacturasAplicar();
        }
    });

    $('body').on('keyup', '.elm', function () {
        var sum = 0;
        var d = 0;
        $('.elm').each(function () {
            if ($(this).val() != '' && !isNaN($(this).val())) {
                sum += parseFloat($(this).val());
            }
        });
        $('#txtTotal').val(sum);
    })
});

function calcularTotal() {
    var totalSolicitado = 0;
    totalIngresado = 0;
    $("#hidTotal").val(0);

    $('#tblMetodosPago tr').each(function () {
        var solicitado = parseFloat($(this).find("td").eq(5).html());
        if (!isNaN(solicitado)) {
            totalSolicitado = parseFloat(totalSolicitado) + parseFloat(solicitado);
            //alert(totalSolicitado);
            //$("#spnAprobado").html(totalSolicitado.toFixed(2));
            $("#hidTotal").val(totalSolicitado);
            totalIngresado = totalSolicitado;
            //$("#txtImporte").val(totalSolicitado);
        }
    });
}

function addvalidacionSoloNumeroValor() {
    $('#tblFacturaPago tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());
        if (!isNaN(id)) {
            $('#txtValorCancelar_' + id).on("keypress", function (e) { return solonumeros(e); });
        }
    });
}

function validarTotalAgregar() {
    var estado = true;
    totalrecibo = $("#txtImporte").val();
    var TotalvalorIngresado = $("#txtValor").val();
    var TotalAcumulado = $("#hidTotal").val();
    totalIngresado = 0;
    totalIngresado = parseFloat(TotalvalorIngresado) + parseFloat(TotalAcumulado);

    //alert("totalIngresado :" + totalIngresado + "  " + "totalrecibo : " + totalrecibo);

    if (totalIngresado > totalrecibo) {
        alert("La suma total del importe de los métodos de pago, no debe ser mayor al importe del recibo.");
        estado = false;
    }
    if (totalIngresado <= 0) {
        alert("Valor no valido, ingrese valor mayor a cero.");
        estado = false;
    }
    return estado;
}

function Agregar() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_PAGO, "");
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_PAGO, K_DIV_POPUP.PAGO)) {
        if (ValidarRequeridos()) {
            var estado = validarTotalAgregar();

            if (estado) {
                var ddlBanco = "";
                var ddlSucursal = "";
                var CuentaBancaria = "";

                if ($("#ddlBanco option:selected").text() != "--SELECCIONE--") ddlBanco = $("#ddlBanco").val();
                else ddlBanco = "";

                if ($("#ddlSucursal option:selected").text() != "--SELECCIONE--") ddlSucursal = $("#ddlSucursal").val();
                else ddlSucursal = "";

                if ($("#ddlCuenta option:selected").text() != "--SELECCIONE--") CuentaBancaria = $("#ddlCuenta").val();
                else CuentaBancaria = "";

                var IdAdd;
                var entidad = {
                    IdRecibo: 0,
                    IdMetodoPago: $("#ddlFormaPago").val(),
                    ValorIgreso: $("#txtValor").val(),
                    IdBanco: ddlBanco,
                    IdSucursal: ddlSucursal,
                    FechaDeposito: $("#txtFecha").val(),
                    CuentaBancaria: CuentaBancaria,
                    Voucher: $("#txtReferencia").val(),
                    idMoneda: $("#ddlMoneda").val(),
                    //Descripciones
                    MetodoPago: $("#ddlFormaPago option:selected").text(),
                    Banco: $("#ddlBanco option:selected").text(),
                    Sucursal: $("#ddlSucursal option:selected").text()
                };
                $.ajax({
                    url: 'AddMetodoPago',
                    type: 'POST',
                    data: entidad,
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);
                        if (dato.result == 1) {
                            loadDataFormaPago(0);
                            limpiar();
                        } else if (dato.result == 0) {
                            alert(dato.message);
                        }
                    }
                });
            }
        }
        return;
    }
}

var reloadEvento = function (idSel) {
    $("#hidCodigoBPS").val(idSel);
    obtenerNombreSocio($("#hidCodigoBPS").val());
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
}

function ObtenerDetalleFormaPago(id) {
    $.ajax({
        url: '../Cobro/ObtenerDetalleFormaPago',
        type: 'POST',
        data: { idRecibo: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFormaPagoAplicarFactura(id);
                calcularTotal();
            } else if (dato.result == 0) {
            }
        }
    });
}

function ObtenerSerie(idSel) {
    $.ajax({
        url: '../Cobro/ObtieneDatosRecibo',
        data: { idRecibo: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;
                if (item != null) {
                    if ($("#hidCodigoBPS").val() != 0) {
                        $("#txtImporte").val(item.REC_TTOTAL);
                    }
                    $("#hidImporte").val(item.REC_TTOTAL);
                    obtenerNombreCorrelativo(item.NMR_ID);
                }
            } else if (dato.result == 0) {
            }
        }
    });
}

var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

var reloadEventoAgregar = function () {

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
                $("#lbCorrelativo").css('color', 'black');
                $("#txtCorrelativo").val(cor.NMR_NOW);
            }
        }
    });
}

function BuscarFacturas() {
    //var usu = $("#hidCodigoBPS").val();
    //var impt = $("#txtImporte").val() == "" ? 0 : $("#txtImporte").val();
    //var impt = 0;
    //var mon = $("#ddlMoneda").val();

    var usu = $("#hidCodigoBPS").val();
    var ser = $("#ddlSerie").val() == undefined ? 0 : $("#ddlSerie").val();
    var num = $("#txtNumero").val() == undefined ? 0 : $("#txtNumero").val();

    $.ajax({
        url: '../Cobro/ListarFacturacionPendienteCobro',
        type: 'POST',
        //data: { usuDerecho: usu, importe: impt, moneda: mon },
        data: { usuDerecho: usu, serie: ser, numero: num },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFacturaRegistrarPago(0);
                $("#btnRegistrarPago").show();
                $("#tablaTotal").show();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function GrabarRecibo() {
    msgOkB(K_DIV_MESSAGE.DIV_PAGO, "");
    $('#txtImporte').css({ 'border': '1px solid gray' });

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_PAGO, K_DIV_VALIDAR.DIV_CAB)) {

        if (validarSerie()) {

            if ($("#txtImporte").val() == 0 || $("#txtImporte").val() == undefined) {
                $("#txtImporte").css({ 'border': '1px solid red' });
                msgErrorB(K_DIV_MESSAGE.DIV_PAGO, "Debe ingresar los campos requeridos");
                estado = false;
                return;
            }

            if (totalIngresado == 0 || totalIngresado == undefined) {
                alert("Ingrese métodos de pago");
                estado = false;
                return;
            }

            if (totalIngresado < totalrecibo) {
                alert("La suma total del importe de los métodos de pago, no debe ser menor al importe del recibo.");
                estado = false;
                return;
            }
            
            //alert("totalIngresado :" + totalIngresado + "  " + "totalrecibo : " + totalrecibo);


            if (totalrecibo == totalIngresado) {

                var IdAdd = $("#hidRecibo").val();
                var entidad = {
                    REC_ID: IdAdd,
                    NMR_ID: $("#hidCorrelativo").val(),
                    REC_NUMBER: $("#txtCorrelativo").val(),
                    BPS_ID: $("#hidCodigoBPS").val(),
                    REC_TBASE: $("#txtImporte").val(),
                    REC_TTOTAL: $("#txtImporte").val()
                };
                $.ajax({
                    url: '../Cobro/GrabarRecibo',
                    type: 'POST',
                    data: entidad,
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);
                        if (dato.result == 1) {
                            var en = dato.data.Data;
                            //alert(JSON.stringify(en));
                            if (en != null) {
                                $("#hidRecibo").val(en);
                            }
                            //alert( $("#hidRecibo").val());
                            alert(dato.message);
                            BuscarFacturas();
                            //$("#btnGrabarRecibo").hide();
                        } else if (dato.result == 0) {
                            alert(dato.message);
                        }
                    }
                });
            }
            else
                alert("Los métodos de pago ingresados deben sumar el total del importe del recibo.");
        }
    }
    else {
        msgErrorB(K_DIV_MESSAGE.DIV_PAGO, "Debe ingresar los campos requeridos");
        $("#txtImporte").css({ 'border': '1px solid red' });
    }
}

function validarSerie() {
    var serie = $("#hidSerie").val();
    var correlativo = $("#hidActual").val();
    if (serie != "" && correlativo != 0) {
        return true;
    } else {
        alert("Seleccione Serie - Correlativo");
        $("#txtCorrelativo").focus();
        return false;
    }
}

function limpiar() {
    $("#ddlFormaPago").val(0),
    $("#txtValor").val(""),
    $("#ddlBanco").val(0),
    $("#ddlSucursal").val(0),
    $("#txtFecha").val(""),
    $("#ddlCuenta").val(0),
    $("#txtReferencia").val("")
    msgErrorB(K_DIV_MESSAGE.DIV_PAGO, "");
}

function GrabraMetodoPago() {
    if (ValidarRequeridos()) {
        var IdAdd = 0;
        var entidad = {
            //REC_ID:   ("#hidRecibo$").val(),
            REC_ID: 1, // solo para pruebas aun falta el insertar recibo
            REC_PWID: $("#ddlFormaPago").val(),
            REC_PVALUE: $("#txtValor").val(),
            REC_DATEDEPOSITE: $("#txtFecha").val(),
            //REC_CONFIRMED: "S", //Crear función para obtener el estado de la confirmación según el método de pago     
            BNK_ID: $("#ddlBanco").val(),
            BRCH_ID: $("#ddlSucursal").val(),
            BACC_NUMBER: $("#ddlCuenta").val(),
            REC_REFERENCE: $("#txtReferencia").val()
        };
        $.ajax({
            url: 'GrabarDetalleMetodoPago',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    loadDataFormaPago(1);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
}

function loadDataFacturaRegistrarPago(id) {
    loadDataGridTmp('ListarFacturaFormaPago', "#gridFactura", id);
}

function loadDataFormaPago(id) {
    loadDataGridTmpFormaPagoDet('ListarFormaPago', "#gridFormaPago", id);
}

function loadDataFormaPagoAplicarFactura(id) {
    loadDataGridTmpFormaPagoDet('ListarFormaPagoAplicarFactura', "#gridFormaPago", id);
}

function loadDataGridTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idFactura: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                $("#tablaTotal").show();
                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataGridTmpFormaPagoDet(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idRecibo: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                calcularTotal();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function delDetalle(idDel, tot, idrec) {
    $.ajax({
        url: 'DellAddMetodoPago',
        type: 'POST',
        data: { id: idDel, Total: tot, idRecibo: idrec },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridFormaPago").empty();
                //$("#hidTotal").val(0);
                totalIngresado = 0;
                loadDataFormaPago(0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function Habilitar(id) {
    if ($("#chkFact" + id).prop("checked")) {
        $("#txtValorCancelar_" + id).removeAttr('disabled');
    }
    else {
        $("#txtValorCancelar_" + id).attr('disabled', 'disabled');
    }
}

function FacturasAplicar() {
    var Factura = [];
    var contador = 0;
    $('#tblFacturaPago tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());
        if (!isNaN(id)) {
            Factura[contador] = {
                Id: $('#txtFacturaId_' + id).val(),
                idFecha: $('#txtIdFecha_' + id).val(),
                idRecibo: $("#hidRecibo").val(),
                TotalPagar: $('#txtValorCancelar_' + id).val()
            };
            contador += 1;
        }
    });

    var Factura = JSON.stringify({ 'Factura': Factura });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Cobro/ObtenerFacturasAplicar',
        data: Factura,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../Cobro/";
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
