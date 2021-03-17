
/************************** INICIO CARGA********************************************/

$(function () {
    loadDataFacturaMasiva(0);
    $("#btnImprimir").on("click", function (e) {
        printPage();
    });
});

function printPage() {
    $("#btnImprimir").hide();
    window.print();
    if (window.stop) {
        window.stop();
        $("#btnImprimir").show();
    }
    return false;
}

function loadDataFacturaMasiva(estado) {
    loadDataGridTmp('ListarFacturaMasivaCabecera', "#grid", estado);
}

function loadDataGridTmp(Controller, idGrilla, estado) {
    $.ajax({
        type: 'POST', data: { estado: estado }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                //var cantidadRegistros = dato.Code;
                //$('#numVariable').val(cantidadRegistros);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function mostrarDetalleFactura(idSel, estado) {
    $.ajax({
        data: { nro: idSel, estado: estado },
        url: '../FacturacionMasiva/mostrarDetalleFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#div" + idSel).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function verDetaFactura(id) {
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
        mostrarDetalleFactura(id, 0);
    }
    return false;
}

function verDetaLic(id) {
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


function abrirSocio(id) {
    var url = "..//Socio/Nuevo?set=" + id;
    window.open(url, "_blank");
}
