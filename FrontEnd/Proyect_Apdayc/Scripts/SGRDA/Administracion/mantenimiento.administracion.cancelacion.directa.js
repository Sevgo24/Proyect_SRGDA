var K_WIDTH_OBS2 = 500;
var K_HEIGHT_OBS2 = 450;
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
    MsgRellenarCamposSolicitados: "RELLENAR LOS CAMPOS SELECCIONADOS ",
    PreJudicial: 10153,
    Genarec: 10154
}

$(function () {

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });
    mvInitBuscarRecaudacionFacturaBec({ container: "ContenedormvBuscarConsultaFacuraBec", idButtonToSearch: "Abrir", idDivMV: "mvBuscarFactura", event: "BuscarFacturaRegistrar", idLabelToSearch: "lbResponsable" });



    //loadComboOficina('dllOficina', '0');
    loadComboOficina('dllOficinaRes', '0');
    
    LoadTipoCancelacionDirecta('lstListarTipoCancelacion', '0');
    LoadTipoControl('lstcontrol','0');

    $('#txtFecRegInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecRegFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaMemo').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    $("#txtFecRegInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecRegInicial").data("kendoDatePicker").value();

    $("#txtFecRegFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecRegFinal").data("kendoDatePicker").value();

    
    $("#txtFechaMemo").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFechaMemo").data("kendoDatePicker").value();

    $("#mvAgregarCancelacionDirecta").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": RegistrarCancelacionDirecta,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvAgregarCancelacionDirecta").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });



    $("#btnBuscarCancelacionDirecta").on("click", function () {
        ListarCancelacionDirecta();
    });

    $("#btnregistrarCancelacionDirecta").on("click", function () {
        $("#mvAgregarCancelacionDirecta").dialog("open");
        $("#dllOficina").val(K_variables.PreJudicial);
        Limpiar_Registro();
    });

    $("#btnLimpiarCancelacionDirecta").on("click", function () {
        Limpiar();
    });

    
})


function ListarCancelacionDirecta() {

    var CodDocumento =$("#txtcoddoc").val() ==""? 0:$("#txtcoddoc").val();
    var CodSerie=$("#hidCorrelativo").val() ==""? 0:$("#hidCorrelativo").val();
    var NumCodigo =$("#txtnum").val() ==""? 0:$("#txtnum").val();
    var CodSocio=$("#hidResponsable").val() ==""? 0:$("#hidResponsable").val();
    var CodOficina=$("#hidOficina").val() ==""? 0:$("#hidOficina").val();
    var Check=$("#chkConFechaReg").is(':checked') == true ? 1 : 0;
    var FechaIni=$("#txtFecRegInicial").val();
    var FechaFin=$("#txtFecRegFinal").val();


    $.ajax({
        data: {
            CodigoDocumento: CodDocumento, CodigoSerie: CodSerie, NumeroDocumento: NumCodigo, CodigoSocio: CodSocio, Oficina: CodOficina,
            ConFecha: Check, FechaInicio: FechaIni, FechaFin: FechaFin
        },
        url: '../AdministracionCancelacionDirecta/ListarCancelacionDirecta',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == K_variables.Si) {
                $("#grid").html(dato.message);
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


function AbrirPoPupAddFactura() {
    $("#mvBuscarFactura").dialog("open");
}

function RegistrarCancelacionDirecta() {

}


function ListarTipoCancelacionDirecta() {


    $.ajax({
        url: '../AdministracionCancelacionDirecta/ListarTipoCancelacionDirecta',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#lblResponsable").html(dato.valor);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

    
}


function LoadTipoCancelacionDirecta(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../AdministracionCancelacionDirecta/ListarTipoCancelacionDirecta',
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

function BuscarFacturaRegistrar(IdDocumento) {

    $("#iddocumento").val(IdDocumento);

    $.ajax({
        data: { CodigoDocumento: IdDocumento },
        url: '../AdministracionCancelacionDirecta/ObtieneDocumento',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                var entidad = dato.data.Data;

                $("#lbldoc").html(entidad.Referencia);
                $("#idValorDocumento").val(entidad.NetoAplicar);
                $("#txtMontoAplicar").val(entidad.NetoAplicar);

                loadComboOficinaDirec('dllOficina', '0', IdDocumento);
                $("#dllOficina").val(K_variables.Genarec);

                $("#dllOficinaRes").val(entidad.CodigoOficinaSeleccionada);

            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

}


function LoadTipoControl(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../AdministracionCancelacionDirecta/ListarControl',
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


function RegistrarCancelacionDirecta() {

    var TipoCan= $("#lstListarTipoCancelacion").val();
    var CodDocumento = $("#iddocumento").val() == "" ? 0 : $("#iddocumento").val();
    var MontoApli = $("#txtMontoAplicar").val() == "" ? 0 : $("#txtMontoAplicar").val();
    var MontoApliMax = $("#idValorDocumento").val();
    var OficinaaComisionar = $("#dllOficina").val();
    var OficinaaResponsable = $("#dllOficinaRes").val();
    
    var FechaMemo = $("#txtFechaMemo").val();
    var AbrevOfi = $("#txtabrev").val() == "" ? "" : $("#txtabrev").val();
    var Nummemo = $("#txtnummemo").val() == "" ? "" : $("#txtnummemo").val();
    var ControlId = $("#lstcontrol").val();
    var concepto = $("#txtareaconcepto").val() == "" ? "" : $("#txtareaconcepto").val();
    
    //obtener los datos y enviarlo (Y) validar que no falte ninguno , FIN  FALTA NUM MEMO Y FECHA Y QUE SEA MEMO*

    if (TipoCan > 0 && CodDocumento > 0 && (MontoApli > 0 && MontoApli != "" && MontoApliMax >= MontoApli) && OficinaaComisionar > 0 && OficinaaResponsable > 0 && FechaMemo != "" && AbrevOfi != "" && Nummemo != "" && ControlId > 0 && concepto != "") {

   
        $.ajax({
            data: {
                CodigoDocumento: CodDocumento, TipoCancelacion: TipoCan, MontoAplicar: MontoApli, OficinaComisionar: OficinaaComisionar, OficinaaResponsable: OficinaaResponsable, NumeroMemo: Nummemo,
                AbrevOficinaMemo: AbrevOfi, MemoFecha: FechaMemo, Origen: ControlId, Concepto: concepto
            },
            url: '../AdministracionCancelacionDirecta/RegistrarCancelacionDirecta',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == K_variables.Si) {
                   

                    ListarCancelacionDirecta();
                    $("#mvAgregarCancelacionDirecta").dialog("close");

                } else {
                    VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
                }
            }

        });
    } else {
        alert(K_variables.MsgRellenarCamposSolicitados);
    }

}


function Limpiar() {

    $("#txtcoddoc").val("");
    $("#txtnum").val("");
    $("#lblResponsable").html("TODOS");
    $("#hidResponsable").val("0");
    $("#lbCorrelativo").html("SELECCIONE UNA SERIE");
    $("#hidCorrelativo").val("0");
    $("#lbOficina").html("SELECCIONE UNA OFICINA");
    $("#hidOficina").val("0");


    $("#lstListarTipoCancelacion").val(0);
    $("#lbldoc").html("DOCUMENTO..");
    $("#idValorDocumento").val("");
    $("#iddocumento").val(0);
    $("#dllOficina").val("10153");
    $("#dllOficinaRes").val("0");
    $("#txtabrev").val("");
    $("#txtnummemo").val("");
    $("#lstcontrol").val();
    $("#txtareaconcepto").val("");
}

function loadComboOficinaDirec(control, valSel, CodigoDocumento) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        data: {CodigoDoc : CodigoDocumento} ,
        url: '../AdministracionCancelacionDirecta/ListarOficinaCancelacionDirecta',
        type: 'POST',
        async : false,
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


function Limpiar_Registro() {
    $('#lstListarTipoCancelacion').val(0);
    $('#txtMontoAplicar').val('');
    $('#dllOficina').val(0);
    $('#dllOficinaRes').val(0);    
    $('#txtabrev').val('');
    $('#txtnummemo').val('');
    $('#lstcontrol').val(0);
    $('#txtareaconcepto').val('');
    $('#lbldoc').html('');

    
}