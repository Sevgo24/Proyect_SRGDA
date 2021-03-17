$(function () {
    var eventoKP = "keypress";
    $('#txtImporte').on(eventoKP, function (e) { return solonumeros(e); });
    $("#hidCodigoBPS").val(0);

    LoadSerie("ddlSerie", 0);
    $("#txtNumero").attr('disabled', 'disabled');

    mvInitBuscarSocio({ container: "ContenedormvBS", idButtonToSearch: "btnBuscarBSocio", idDivMV: "mvBuscarBS", event: "reloadEventBS", idLabelToSearch: "lbSocio" });

    $("#btnBuscar").on("click", function () {
        if ($("#hidCodigoBPS").val() == 0) {
            alert("Seleccione usuario de derecho");
        }
        else
            BuscarFacturas();
    });

    $("#btnBuscarSerie").on("click", function () {
        BuscarFacturasSerie();
    });

    $("#btnRegistrarPago").on("click", function () {
        var idSel = $("#hidCodigoBPS").val();
        if (idSel != 0) {
            document.location.href = '../Cobro/Nuevo?id=' + idSel;
        }
        else
            alert("Seleccione usuario de derecho");
    })  

    $("#btnRegistrarPagoSerie").on("click", function () {
        var idSel = 0;
        if ($("#ddlSerie").val() != 0)
            document.location.href = '../Cobro/Nuevo?id=' + idSel;
        else
            alert("Seleccione la serie");
    });

    $("#btnLimpiarSerie").on("click", function () {
        $("#hidCodigoBPS").val(0);
        $("#lbSocio").html("Seleccione");
        $("#grid").empty();
        $("#gridRecibos").empty();
    });

    $("#btnLimpiar").on("click", function () {
        $("#hidCodigoBPS").val(0);
        $("#lbSocio").html("Seleccione");
        $("#grid").empty();
        $("#gridRecibos").empty();
    });

    $("#ddlSerie").on("change", function () {
        $('#txtNumero').removeAttr('disabled');

        if ($("#ddlSerie").val() == 0) {
            $("#txtNumero").attr('disabled', 'disabled');
        };
    });

    $("#tabs").tabs();
});

function loadDataDetalloMetodoPago(id) {
    loadDataGridTmpFormaPagoDet('ListarDetalleFormaPago', "#gridDetallePago", id);
}

function loadDataGridTmpFormaPagoDet(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idRecibo: id }, url: Controller, beforeSend: function () { },
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

var reloadEventBS = function (idSel) {
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
                $("#lbSocio").html(dato.valor);
            }
        }
    });
}

function BuscarFacturas() {
    var usu = $("#hidCodigoBPS").val();
    var ser = $("#ddlSerie").val();
    var num = $("#txtNumero").val() == "" ? 0 : $("#txtNumero").val();

    $.ajax({
        url: '../Cobro/ListarFacturacionPendienteCobro',
        type: 'POST',
        data: { usuDerecho: usu, serie: ser, numero: num },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFactura(0);
                loadDataRecibosPendientes();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function BuscarFacturasSerie() {
    var usu = 0;
    var ser = $("#ddlSerie").val();
    var num = $("#txtNumero").val() == "" ? 0 : $("#txtNumero").val();

    $.ajax({
        url: '../Cobro/ListarFacturacionPendienteCobro',
        type: 'POST',
        data: { usuDerecho: usu, serie: ser, numero: num },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFactura(0);
                loadDataRecibosPendientes();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataFactura(id) {
    loadDataGridTmp('ListarFacturaCabecera', "#grid", id);
}

function loadDataGridTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idFactura: id }, url: Controller, beforeSend: function () { },
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

function loadDataGridTmp2(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadDataRecibosPendientes() {
    loadDataGridTmp2('ListarRecibosPendientes', "#gridRecibos");
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

function verDetaLic(id) {
    //alert(id);
    if ($("#expandLic" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandLic" + id).attr('src', '../Images/botones/more.png');
        $("#expandLic" + id).attr('title', 'ver detalle.');
        $("#expandLic" + id).attr('alt', 'ver detalle.');
        $("#divLic" + id).css("display", "none");

    } else {
        $("#expandLic" + id).attr('src', '../Images/botones/less.png');
        $("#expandLic" + id).attr('title', 'ocultar detalle.');
        $("#expandLic" + id).attr('alt', 'ocultar detalle.');
        $("#divLic" + id).css("display", "inline");
    }
    return false;
}

function verDetaMetodosPago(id) {
    if ($("#expandDet" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandDet" + id).attr('src', '../Images/botones/more.png');
        $("#expandDet" + id).attr('title', 'ver detalle.');
        $("#expandDet" + id).attr('alt', 'ver detalle.');
        $("#div" + id).css("display", "none");

    } else {
        $("#expandDet" + id).attr('src', '../Images/botones/less.png');
        $("#expandDet" + id).attr('title', 'ocultar detalle.');
        $("#expandDet" + id).attr('alt', 'ocultar detalle.');
        $("#div" + id).css("display", "inline");
    }
    return false;
};

function obtenerId(id, idusu) {
    $("#hidIdRecibo").val(id);
    $("#hidCodigoBPS").val(idusu);
    var idRecibo = $("#hidIdRecibo").val();
    var idUsu = $("#hidCodigoBPS").val();
    //alert("Recibo : " + idRecibo + "  Usuario : " + idUsu);
    document.location.href = '../Cobro/Nuevo?id=' + idUsu + "&idRecibo=" + idRecibo;
}

function color1() {
    //alert("2");
}

function color2() {
    //alert("1");
}