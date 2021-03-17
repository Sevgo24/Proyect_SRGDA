var N = 'N';
var A = 'A';
$(function () {
    //ObtenerOficinaConsultaDocumeno();
    //$("#btnBuscarDemo").on("click", function () {
    //    var estadoRequeridos = ValidarRequeridos();
    //    var idMoneda = $('#ddlMoneda').val();
    //    if (idMoneda != '0')
    //        ConsultaDocumento(idMoneda);
    //});
    var idFactura = (GetQueryStringParams("id"));
    CD_Cabecera(idFactura);
    CD_Referencia(idFactura);
    CD_COBRO(idFactura);
    //alert(1),
    $("#tabs").tabs();

});


function CD_Cabecera(idFactura) {

    $.ajax({
        url: '../ConsultaDocumentoDetalle/ObtenerCabecera',
        type: 'POST',
        data: {
            idFactura: idFactura
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridCDCabecera").html(dato.message);
                CD_Licencia(idFactura);
                //$("#CantidadRegistros").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#gridCDCabecera").html('');
                //$("#CantidadRegistros").html("");
                alert(dato.message);
            }
        }
    });

}


function CD_Licencia(idFactura) {

    $.ajax({
        url: '../ConsultaDocumentoDetalle/ObtenerLicencia',
        type: 'POST',
        data: {
            idFactura: idFactura
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridCDDetalle").html(dato.message);
                $("#lblTotalLicencias").html(dato.TotalFacturas);
                
                //$("#CantidadRegistros").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#gridCDDetalle").html('');
                $("#lblTotalLicencias").html('0');
                alert(dato.message);
            }
        }
    });

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

function CD_Referencia(idFactura) {

    $.ajax({
        url: '../ConsultaDocumentoDetalle/ObtenerReferencia',
        type: 'POST',
        data: {
            idFactura: idFactura
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridReferencia").html(dato.message);
                //CD_Licencia(idFactura);
            } else if (dato.result == 0) {
                $("#gridReferencia").html('');
                alert(dato.message);
            }
        }
    });

}

function CD_COBRO(idFactura) {

    $.ajax({
        url: '../ConsultaDocumentoDetalle/GenerarGrillaCobro',
        type: 'POST',

        data: {
            CodigoFactura: idFactura
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridCobro").html(dato.message);
                //CD_Licencia(idFactura);
            } else if (dato.result == 0) {
                $("#gridCobro").html('');
                alert(dato.message);
            }
        }
    });

}

function ObtenerCobro(idSel, version) {
    //alert(version);
    document.location.href = '../BEC/Nuevo?id=' + idSel + '&ver=' + version
}