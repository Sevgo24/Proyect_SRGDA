
var K_WIDTH_OBS2 = 600;
var K_HEIGHT_OBS2 = 285;

$(document).ready(function () {


    $('#txtcod').on("keypress", function (e) { return solonumeros(e); });
    $('#txtnum').on("keypress", function (e) { return solonumeros(e); });
    
    if ($("#txtFecCreaInicial").length) {
        $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
        $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
        var fechaActual = new Date();
        var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

        $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
        var d = $("#txtFecCreaInicial").data("kendoDatePicker").value();
        $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);
        var dFIN = $("#txtFecCreaFinal").data("kendoDatePicker").value();
    }




    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });


    $("#btnEnviarAprobacion").on("click", function () {
        //alert("Solicitando Aprobacion factura" + $("#hidId").val());
        $("#mvSolicitud").dialog("open");
    });

    $("#btnBuscarSolicitud").on("click", function () {

        Listar();
    });

    $("#btnBuscarDemo").on("click", function () {

        $("#btnNotaCredito").hide();
        $("#btnEnviarAprobacion").hide();
    });

    
    $("#btnLimpiarSolicitud").on("click", function () {

        LimpiarSolicitudAprobacion();
    });
    

    $("#trocultarddl").hide();

    $("#mvSolicitud").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": GuardarSolicitud,
            "Cancelar": function () {
                $('#ddltipoAprobacion').val(0);
                $("#mvSolicitud").dialog("close");
                $('#txtAprobacionDescSo').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    $("#mvSolicitudRespt").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": GuardarSolicitud,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvSolicitudRespt").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    LimpiarSolicitudAprobacion();

    loadTipoSolicitud('ddltipoAprobacion', 0);   
    
});

function GuardarSolicitud() {

    var ID = $("#hidId").val();
    var TIPO = $("#ddltipoAprobacion").val();
    var DESC = $("#txtAprobacionDescSo").val();
    var RESPT = $("#ddltipoAprobacionResp").val() == undefined ? 0 : $("#ddltipoAprobacionResp").val();
 

    if (TIPO > 0) {
        Confirmar(' ¿Esta seguro de Continuar ?',
                   function () {
                       $.ajax({
                           data: { INV_ID: ID, TIPO: TIPO, DESC: DESC, RESPT: RESPT },
                           url: '../AdministracionSolicitudDocumentos/ActualizarDocumentoSolicitudAprobacion',
                           type: 'POST',
                           beforeSend: function () { },
                           success: function (response) {
                               var dato = response;
                               validarRedirect(dato);
                               if (dato.result == 1) {
                                   $("#mvSolicitud").dialog("close");
                                   //BuscarFacturasConsulta();
                                   $("#btnEnviarAprobacion").hide();

                                   if ($("#btnBuscarDemo").length) {
                                       ConsultaDocumento(0);
                                       $("#mvSolicitud").dialog("close");
                                   } else {
                                       LimpiarSolicitudAprobacion();
                                       Listar();
                                       $("#mvSolicitudRespt").dialog("close");
                                   }
                                   //ConsultaDocumento(0);
                                   alert(dato.message);
                               } else if (dato.result == 0) {
                                   alert(dato.message);
                               }
                           }
                       });

                   },
                   function () {
                       $("#hidIdFactA").val('');
                       $("#hidTipoF").val('');
                   },
                   'Confirmar'
               )
    } else {
        alert("POR FAVOR DE ELEGIR UN TIPO DE REQUERIMIENTO");
    }
}

function Listar() {

    var invid = $("#txtcod").val() == "" ? "0" : $("#txtcod").val();
    var serie = $("#hidCorrelativo").val() == "" ? "0" : $("#hidCorrelativo").val();
    var numero = $("#txtnum").val() == "" ? "0" : $("#txtnum").val();
    var ofi = $("#hidOficina").val() == "" ? "0" : $("#hidOficina").val();
    var estado = $("#ddlestadoAprobacion").val();
    var tipo = $("#ddltipoAprobacion").val();
    var confecha= $("#chkConFechaCrea").is(':checked') == true ? 1 : 0;
    var fechaini = $("#txtFecCreaInicial").val();
    var fechafin = $("#txtFecCreaFinal").val();

    //alert(estado);

    $.ajax({
        data: { INV_ID: invid, SERIE: serie, INV_NUMBER: numero, CONFECHA: confecha, FECHA_INI: fechaini, FECHA_FIN: fechafin, OFF_ID: ofi, ESTADO_APROB: estado, TIPO: tipo },
        url: '../AdministracionSolicitudDocumentos/Listar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lblcantidad").html(dato.Code);
                $("#grid").html(dato.message);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ModificarAprobacion(INV_ID) {


    //alert("MODIFICAR");

    $("#hidId").val(INV_ID);
    //Limpiar();
    ObtenerSolicitud(INV_ID);
    $("#mvSolicitudRespt").dialog("open");

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


function ObtenerSolicitud(INV_ID) {

    $.ajax({
        data: { INV_ID: INV_ID },
        url: '../AdministracionSolicitudDocumentos/Obtiene',
        type: 'POST',
        async:false ,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var entidad = dato.data.Data;

                //alert(entidad.DESCRIPCION);
                $("#txtAprobacionDescSo").val(entidad.DESCRIPCION);
                $("#ddltipoAprobacion").val(entidad.TIPO);
                $("#lblTipoSolicitud").html(entidad.TIPO_DESC);
                $("#lblserie").html(entidad.SERIE);
                $("#lblnumero").html(entidad.NUMERO);

               

            } else if (dato.result == 0) {
                alert(dato.message);
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


function LimpiarSolicitudAprobacion() {
    $("#txtcod").val("");
    $("#hidCorrelativo").val("");
    $("#lbCorrelativo").html("SELECCIONA UNA SERIE");
    $("#txtnum").val("");
    $("#chkConFechaCrea").prop("checked", false);


    if ($("#txtFecCreaInicial").length) {
        $("#txtFecCreaInicial").data("kendoDatePicker").value(new Date());
        $("#txtFecCreaFinal").data("kendoDatePicker").value(new Date());
    }
    $("#ddlestadoAprobacion").prop('selectedIndex', 0);
    $("#ddltipoAprobacion").prop('selectedIndex', 0);
    $("#hidOficina").val("");
    $("#lbOficina").html("SELECCIONA UNA OFICINA");
    
}


function loadTipoSolicitud(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: -1, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../AdministracionSolicitudDocumentos/ListaTipoSolicitudes',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function verDetalleDocumento(id) {
    var url = '../ConsultaDocumentoDetalle/Index?id=' + id;
    window.open(url, "_blank");
}
