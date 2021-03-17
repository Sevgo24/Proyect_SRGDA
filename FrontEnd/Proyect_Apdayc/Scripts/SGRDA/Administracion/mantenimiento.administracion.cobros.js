var K_variables = {
    OkSimbolo: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    AlertaSimbolo:'<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    SinSimbolo: '',
    Si: 1,
    No: 0,
    Cero: 0,
    MenosUno:'-1',
    CeroLetra: '0',
    TextoVacio: "",
    MinimoHeight: 75,
    blindDuration: 1000,
    hideDuration: 1000,
    Uno:1
}
$(function () {

    $('#txtcodcob').on("keypress", function (e) { return solonumeros(e); });
    $('#txtnum').on("keypress", function (e) { return solonumeros(e); });


    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    //var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + K_variables.Uno, fechaActual.getDate());
    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaActual);

    loadBancos('ddlBanco', K_variables.Cero);

    loadValoresConfiguracion('ddlEstadoConfirmacion', 'COBRO', 'CONFIRMACION');

    $('#ddlCuenta').append($("<option />", { value: K_variables.Cero, text: '-- SELECCIONE --' }));

    $("#ddlBanco").change(function () {
        var id = $("#ddlBanco").val();
        $('#ddlCuenta option').remove();
        $('#ddlCuenta').append($("<option />", { value: K_variables.Cero, text: '-- SELECCIONE --' }));
        loadCuentaBancariaXbanco('ddlCuenta', id, K_variables.CeroLetra);
    });
    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX BOTONES
    $("#btnBuscarCobros").on("click", function () {
        $("#contenedor").hide();
        ListarCobros();
    });

    $("#btnLimpiarCobros").on("click", function () {
        LimpiarCobros();
    });

    $("#btnPdf").on("click", function () {
        $("#grid").html('');
        $("#contenedor").show();

        ExportarReporteCobros('PDF');
    });

    $("#btnExcel").on("click", function () {
        $("#grid").html('');
        ExportarReporteCobros('EXCEL');
        //ExportarReportef2('EXCEL');
    });


    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    $("#trCantidadCobros").hide();

    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZAN LAS CONSULTAS DE COBROS PARCIALMENTE COBRADOS , CON EL FIN DE ALERTAR A LAS GESTORAS O TOMAR MEDIDAS", "INFORMACION", K_variables.OkSimbolo);
})

function ListarCobros() {

    $.ajax({
        data: {
            IdCobro: $("#txtcodcob").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcodcob").val(),
            NumeroOperacion: $("#txtnumope").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtnumope").val(),
            Monto: $("#txtnumontodep").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnumontodep").val(),
            IdBancoDestino: $("#ddlBanco").val(),
            IdBancoOrigen: K_variables.Cero,
            IdCuenta: $("#ddlCuenta").val(),
            IdOficina: $("#hidOficina").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidOficina").val(),
            EstadoConfirmacion:K_variables.Cero,
            EstadoCobro: $("#ddlEstadoConfirmacion").val() == K_variables.MenosUno ? K_variables.Cero : $("#ddlEstadoConfirmacion").val(),
            ConFecha: $("#ddlTipoFecha").val(), // Modificara por una Lista.
            FechaInicial: $("#txtFecCreaInicial").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaInicial").val(),
            FechaFinal: $("#txtFecCreaFinal").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaFinal").val(),
            IdSocio: $("#hidResponsable").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidResponsable").val(),
            IdSerie: $("#hidCorrelativo").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidCorrelativo").val(),
            NumeroDocumento: $("#txtnum").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnum").val()
            
        },
        url: '../AdministracionCobros/Listar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#grid").html(dato.message);
                $("#lblCantCobros").html(dato.Code);
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
        minHeight: K_variables.MinimoHeight,
        show: {
            effect: "blind",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "explode",
            duration: K_variables.hideDuration
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
            if (dato.result == K_variables.Si) {
                $("#lbOficina").html(dato.valor);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}


var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
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
            if (dato.result == K_variables.Si) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}



function LimpiarCobros() {
    $("#txtcodcob").val(K_variables.TextoVacio);
    $("#txtnumope").val(K_variables.TextoVacio);
    $("#txtnumontodep").val(K_variables.TextoVacio);
    $("#txtnum").val(K_variables.TextoVacio);
    $("#ddlBanco").prop('selectedIndex', K_variables.Cero);
    $("#ddlCuenta").prop('selectedIndex', K_variables.Cero);
    $("#ddlEstadoConfirmacion").prop('selectedIndex', K_variables.MenosUno);
    $("#hidOficina").val(K_variables.TextoVacio);
    $("#hidCorrelativo").val(K_variables.TextoVacio);
    $("#hidResponsable").val(K_variables.TextoVacio);
    $("#txtFecCreaInicial").val(K_variables.TextoVacio);
    $("#txtFecCreaFinal").val(K_variables.TextoVacio);
    $("#ddlTipoFecha").prop('selectedIndex', K_variables.Cero);
    
}

var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

};


function verDetalleCobroSocio(id) {
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
        mostrarDetalleCobroSocio(id);
    }
    return false;
}

function mostrarDetalleCobroSocio(id) {

    $.ajax({
        data: { IdCobro: id },
        url: '../AdministracionCobros/ListarCabezeraSociosCobros',
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


function verDetalleCobroSocioDocumento(id,idsocio) {
    if ($("#expandDoc" + id + idsocio).attr('src') == '../Images/botones/less.png') {
        $("#expandDoc" + id + idsocio).attr('src', '../Images/botones/more.png');
        $("#expandDoc" + id + idsocio).attr('title', 'ver detalle.');
        $("#expandDoc" + id + idsocio).attr('alt', 'ver detalle.');
        $("#divDoc" + id + "-" + idsocio).css("display", "none");
    } else {
        $("#expandDoc" + id + idsocio).attr('src', '../Images/botones/less.png');
        $("#expandDoc" + id + idsocio).attr('title', 'ocultar detalle.');
        $("#expandDoc" + id + idsocio).attr('alt', 'ocultar detalle.');
        $("#divDoc" + id + "-" + idsocio).css("display", "inline");
        mostrarDetalleCobroSocioDetalle(id, idsocio);
    }
    return false;
}

function mostrarDetalleCobroSocioDetalle(id,idsocio) {

    $.ajax({
        data: { IdCobro: id, IdSocio: idsocio },
        url: '../AdministracionCobros/ListarCabezeraSociosDetalleCobros',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#divDoc" + id + "-" + idsocio).html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}


function editar(idSel) {
    window.open('../BEC/Nuevo?id=' + idSel + '&ver=' + 'N');
}


function VerSocio(id) {
    window.open("../Socio/Nuevo?set=" + id);
}


function VerDocumento (id){

    window.open( "../ConsultaDocumentoDetalle/Index?id="+id);
    
}

function ExportarReporteCobros(tipo) {

    var url = "";

    $.ajax({
        url: '../AdministracionCobros/ReporteTipo',
        type: 'POST',
        async: false,
        data: {
            IdCobro: $("#txtcodcob").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcodcob").val(),
            NumeroOperacion: $("#txtnumope").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtnumope").val(),
            Monto: $("#txtnumontodep").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnumontodep").val(),
            IdBancoDestino: $("#ddlBanco").val(),
            IdBancoOrigen: K_variables.Cero,
            IdCuenta: $("#ddlCuenta").val(),
            IdOficina: $("#hidOficina").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidOficina").val(),
            EstadoConfirmacion: K_variables.Cero,
            EstadoCobro: $("#ddlEstadoConfirmacion").val() == K_variables.MenosUno ? K_variables.Cero : $("#ddlEstadoConfirmacion").val(),
            ConFecha: $("#ddlTipoFecha").val(), // Modificara por una Lista.
            FechaInicial: $("#txtFecCreaInicial").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaInicial").val(),
            FechaFinal: $("#txtFecCreaFinal").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaFinal").val(),
            IdSocio: $("#hidResponsable").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidResponsable").val(),
            IdSerie: $("#hidCorrelativo").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidCorrelativo").val(),
            NumeroDocumento: $("#txtnum").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtnum").val()

        },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == K_variables.Si) {
                $("#grid").html('');
                url = "../AdministracionCobros/ReporteListarCobrosParciales?" +
                    "formato=" + tipo;
                $("#contenedor").show();
                $('#externo').attr("src", url);
                if (tipo == "EXCEL")
                    $("#contenedor").hide();


            } else if (dato.result == K_variables.No) {
                $("#contenedor").hide();
                alert(dato.message);
            }
        }
    });

}


function InactivarCobro(id,idRec) {

    ConfirmarAccion('SE INACTIVARA/ACTIVARA EL COBRO , (ESTA SEGURO(A) DE CONTINUAR ?',
    function () { ActualizaEstadoCobro(id, idRec); },
    function () { },
    'Confirmar');

}


function ConfirmarAccion(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        },show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }


    });

}

function ActualizaEstadoCobro(id,idRecCobro) {

    $.ajax({
        data: { IdCobro: id, IdRecCobro: idRecCobro },
        url: '../AdministracionCobros/ActualizarEstadoCobro',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "CORRECTO", K_variables.OkSimbolo);
                ListarCobros();

            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}