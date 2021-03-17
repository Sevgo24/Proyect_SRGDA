var K_variables = {
    OkSimbolo: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    AlertaSimbolo: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    SinSimbolo: '',
    Si: 1,
    No: 0,
    Cero: 0,
    MenosUno: '-1',
    CeroLetra: '0',
    TextoVacio: "",
    MinimoHeight: 75,
    blindDuration: 1000,
    hideDuration: 1000,
    Uno: 1,
    NCElegida: 0,
    NCDocumentoOriginal:0
}

var K_WIDTH_OBS2 = 1000;
var K_HEIGHT_OBS2 = 700;


$(document).ready(function () {


    $('#txtcod').on("keypress", function (e) { return solonumeros(e); });
    $('#txtnum').on("keypress", function (e) { return solonumeros(e); });

    if ($("#txtFechaEmision").length) {
        $('#txtFechaEmision').kendoDatePicker({ format: "dd/MM/yyyy" });
        var fechaActual = new Date();

        $("#txtFechaEmision").data("kendoDatePicker").value(fechaActual);
        var d = $("#txtFechaEmision").data("kendoDatePicker").value();
    }




    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitBuscarCorrelativoNotaCredito({ container: "ContenedormvBuscarCorrelativoNC", idButtonToSearch: "btnBuscarCorrelativoNC", idDivMV: "mvBuscarCorrelativoNC", event: "reloadEventoCorrelativoNC", idLabelToSearch: "lbCorrelativoNC" });
    mvInitBuscarRecaudacionFacturaBec({ container: "ContenedormvBuscarConsultaFacuraBec", idButtonToSearch: "Abrir", idDivMV: "mvBuscarFacturaBec", event: "addfacturaDet", idLabelToSearch: "lbResponsable" });


    $("#mvCreacionBECNC").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": RegistrarCOBRONC,
            "Cancelar": function () {
                $("#mvCreacionBECNC").dialog("close");
                $('#txtDescripcion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });



    $("#btbuscarNCFACT").on("click", function () {

        ListarCOBRONC();
    });

    $("#btnNuevoCobroNC").on("click", function () {

        $("#mvCreacionBECNC").dialog("open");
    });

    $("#btbuscarNCFACTREG").on("click", function () {

        ListarCOBRONCREG();
    });
    

    $("#btnLimpiaNCFACT").on("click", function () {

        LimpiarBECNC();
    });



    LimpiarBECNC();

    LimpiarRegBECNC();
    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZAN BECS CON NOTAS DE CREDITO ", "INFORMACION", K_variables.OkSimbolo);

});



function ListarCOBRONC() {
    $.ajax({
        data: {
            CodigoNC: $("#txtcod").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcod").val(),
            COdigoSerie: $("#hidCorrelativo").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidCorrelativo").val(),
            NUmeroDocumento: $("#txtnum").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnum").val(),
            CONFECHA: 0,
            FechaEmision: $("#txtFechaEmision").val(),
            CodigoOficina: $("#hidOficina").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidOficina").val(),
            Tipo : K_variables.Si
        },
        url: '../BECNC/ListarCOBRONC',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#grid").html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}


function verDetalleCobroNCSocio(id) {
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
        mostrarDetalleCOBRONC(id);
    }
    return false;
}

function mostrarDetalleCOBRONC(id) {

    $.ajax({
        data: { CodigoNC: id },
        url: '../BECNC/ListarDetalleCOBRONC',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#div" + id).html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
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
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//SERIE - CORRELATIVO BUSQ. GENERAL
var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

var reloadEventoCorrelativoNC = function (idSel) {
    $("#hidCorrelativoNC").val(idSel);
    obtenerNombreCorrelativoNC($("#hidCorrelativoNC").val());
};


function obtenerNombreCorrelativo(idSel) {
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
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerNombreCorrelativoNC(idSel) {
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
                $("#lbCorrelativoNC").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidSerieNC").val(cor.NMR_ID);
                $("#hidActualNC").val(cor.NMR_NOW);
                $("#lbCorrelativoNC").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function LimpiarBECNC() {
    $("#txtcod").val("");
    $("#hidCorrelativo").val("");
    $("#lbCorrelativo").html("SELECCIONA UNA SERIE");
    $("#txtnum").val("");
    $("#chkConFechaCrea").prop("checked", false);


    if ($("#txtFechaEmision").length) {
        $("#txtFechaEmision").data("kendoDatePicker").value(new Date());
        
    }
    $("#hidOficina").val("");
    $("#lbOficina").html("SELECCIONA UNA OFICINA");

}




function verDetalleDocumento(id) {
    var url = '../ConsultaDocumentoDetalle/Index?id=' + id;
    window.open(url, "_blank");
}

function RegistrarCOBRONC() {


    Confirmar('ESTA SEGURO DE CREAR EL COBRO CON LA INFORMACION SELECCIONADA',
        function () {
            $.ajax({
                url: '../BECNC/InsertarBECNC',
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    if (dato.result == K_variables.Si) {
        
                        $("#mvCreacionBECNC").dialog("close");
                        VentanaAviso(dato.message, "OKEY", K_variables.OkSimbolo);
                        ListarCOBRONC();
                        LimpiarRegBECNC();
                        $("#gridSeleccionNC").html('');
                    } else {
                        VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
                    }
                }
        
            });
        },
        function () { },
'Confirmar');



}

function ListarCOBRONCREG() {
    $.ajax({
        data: {
            CodigoNC: $("#txtcoddocreg").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcoddocreg").val(),
            COdigoSerie: $("#hidCorrelativoNC").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidCorrelativoNC").val(),
            NUmeroDocumento: $("#txtnumdocreg").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnumdocreg").val(),
            CONFECHA: 0,
            FechaEmision: $("#txtFechaEmision").val(),
            CodigoOficina: $("#hidOficina").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidOficina").val(),
            Tipo : K_variables.No
        },
        url: '../BECNC/ListarCOBRONCREG',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#gridSeleccionNC").html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}


function AbrirPoPupAddFactura(idBps,invidnc) {

    //var limpiar = true;
    //var idBpsAnterior = $("#hidIdSocioDocFact").val();
    //limpiar = (idBpsAnterior == idBps) ? false : true;

    $("#hidIdSocioDocFact").val(idBps);
    obtenerNombreSocioX(idBps, 'lbResponsableDocFact');
    $("#mvBuscarFactura").dialog("open");
    loadDataFacturas();
    K_variables.NCElegida = invidnc;
    //K_variables.NCDocumentoOriginal = invcnref;

}

function addfacturaDet(fact) {


    $.ajax({
        data: { CodigoDocumento: fact },
        url: '../BECNC/ListarDetalleCOBRONCREG',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#divreg" + K_variables.NCElegida).css("display", "inline");
                $("#divreg" + K_variables.NCElegida).html(dato.message);

            } else {
                
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}

function VentanaAviso(dialogText, dialogTitle, SIMBOLO) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + SIMBOLO + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }


    });
}

function LimpiarRegBECNC() {
    $("#txtcoddocreg").val("");
    $("#hidCorrelativoNC").val("");
    $("#lbCorrelativoNC").html('SELECCIONE UNA SERIE');
    $("#txtnumdocreg").val("");
    
}