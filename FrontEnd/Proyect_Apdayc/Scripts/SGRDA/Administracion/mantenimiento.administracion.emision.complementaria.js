var K_WIDTH_OBS2 = 700;
var K_HEIGHT_OBS2 = 650;
var K_WIDTH_OBS3 = 350;
var K_HEIGHT_OBS3 = 325;
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
    MsgRellenarCamposSolicitados: "RELLENAR LOS CAMPOS ANTERIORES",
    PreJudicial: 10153,
    Genarec: 10154
}

$(function () {

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    Limpiar();

    $('#txtcodemicab').on("keypress", function (e) { return solonumeros(e); });
    $('#txtcodlic').on("keypress", function (e) { return solonumeros(e); });
    $('#txtaddcodlic').on("keypress", function (e) { return solonumeros(e); });
    $('#txtaddanio').on("keypress", function (e) { return solonumeros(e); });
    $('#txtaddmes').on("keypress", function (e) { return solonumeros(e); });

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();

    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecCreaInicial").data("kendoDatePicker").value();

    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaActual);
    var dFIN = $("#txtFecCreaFinal").data("kendoDatePicker").value();


    $("#btnBuscarComplementaria").on("click", function () {
        ListarEmisionComplementaria();
    });

    $("#btnbuscarperiodoCompl").on("click", function () {
        ListarConsultaLicenciaDetalle();
        ListarLicenciaRegistradaDetalle();//borrar esto
    });


    $("#btnLimpiarComplementaria").on("click", function () {
        Limpiar();
    });

    $("#btnbuscarLicenciasComplementarias").on("click", function () {
        if ($("#txtaddnomproc").val() != "" && $("#txtEmisionComplementariaDesc").val()!="")
        {
            $("#trbuscalicencias").show();
            InsertarActualizarCabComplementaria();
        }
        else {
            VentanaAviso(K_variables.MsgRellenarCamposSolicitados, "ALERTA", K_variables.AlertaSimbolo);
        }
    });


    $("#mvAgregarEmisionComplementaria").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": ConfirmarRegistroEmisionComplementaria,
            "Cancelar": function () {

                $("#mvAgregarEmisionComplementaria").dialog("close");
            }
        },
        show: {
            effect: "scale",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "scale",
            duration: K_variables.hideDuration
        },
        modal: true
    });

    $("#MvObtenerEmisionComplementaria").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS3,
        height: K_HEIGHT_OBS3,
        buttons: {
            "Grabar": GuardarCambiosEmisionComplementaria,
            "Cancelar": function () {

                $("#MvObtenerEmisionComplementaria").dialog("close");
            }
        },
        show: {
            effect: "scale",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "scale",
            duration: K_variables.hideDuration
        },
        modal: true
    });

    



    $("#MvMostrarDocumentosxSolicitud").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Cerrar": function () {

                $("#MvMostrarDocumentosxSolicitud").dialog("close");
            }
        },
        show: {
            effect: "scale",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "scale",
            duration: K_variables.hideDuration
        },
        modal: true
    });

    

    $("#btnregistrarEmisionComplementaria").on("click", function () {
        $("#mvAgregarEmisionComplementaria").dialog("open");
    });

    $("#trbuscalicencias").hide();
    $("#trcabadd").show();

    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZAN LAS EMISIONES COMPLEMENTARIAS DE LOCALES PERMANENTES", "INFORMACION", K_variables.OkSimbolo);


    //setInterval(ListarEmisionComplementaria, 30000);
});


function ListarEmisionComplementaria() {

    $.ajax({
        data: {
            COdigoEmision: $("#txtcodemicab").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcodemicab").val(),
            CodigoLicencia: $("#txtcodlic").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtcodlic").val(),
            CodigoOficina: $("#hidOficina").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidOficina").val(),
            Estado: $("#ddlestadoAprobacionEmiComp").val(),
            ConFecha: $("#ddlTipoFecha").val(),
            FechaInicial: $("#txtFecCreaInicial").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaInicial").val(),
            FechaFinal: $("#txtFecCreaFinal").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaFinal").val(),

        },
        url: '../AdministracionEmisionComplementaria/Listar',
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
                $("#lbResponsable").html(dato.valor);
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
        minHeight: K_variables.MinimoHeight,
        show: {
            effect: "scale",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "scale",
            duration: K_variables.hideDuration
        }


    });
}

function verDetalleEmisionComplementaria(id) {
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
        mostrarDetalleCEmisionComplementaria(id);
    }
    return false;
}

function mostrarDetalleCEmisionComplementaria(id) {

    $.ajax({
        data: { IdCancelacionComplementaria: id },
        url: '../AdministracionEmisionComplementaria/ListarDetalleEmisionComplementaria',
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


function InactivarEmisionComplementaria(iddet,idcab) {

    $.ajax({
        data: { IdEmisionComplementaria: iddet },
        url: '../AdministracionEmisionComplementaria/ActualizarEstadDetalleComplementario',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                mostrarDetalleCEmisionComplementaria(idcab);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}


function ConfirmarRegistroEmisionComplementaria() {

    ConfirmarAccion('SE GUARDARA LA SOLICITUD DE EMISION COMPLEMENTARIA  , (ESTA SEGURO(A) DE CONTINUAR ?',
function () {
    RegistrarEmisionComplementaria();
},
function () { },
'Confirmar');

}



function RegistrarEmisionComplementaria() {

    $.ajax({
        data: { Codcab: $("#hidcabid").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidcabid").val() },
        url: '../AdministracionEmisionComplementaria/ActualizaDefinitivaCabDetComplementario',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#mvAgregarEmisionComplementaria").dialog("close");
                Limpiar();
                $("#trbuscalicencias").hide();
                $("#trcabadd").show()
                VentanaAviso(dato.message, "OK", K_variables.OkSimbolo);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}


function InsertarActualizarCabComplementaria() {

    var codcabcomp = $("#hidcabid").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidcabid").val();
    var Nombre = $("#txtaddnomproc").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtaddnomproc").val();
    var Descr = $("#txtEmisionComplementariaDesc").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtEmisionComplementariaDesc").val();

    $.ajax({
        data: { CodCabComple: codcabcomp, nombre: Nombre, descripcion: Descr },
        url: '../AdministracionEmisionComplementaria/InsertaActualizaCabEmiComplementaria',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#hidcabid").val(dato.Code);
                $("#trcabadd").hide();
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}





function ListarConsultaLicenciaDetalle() {

    $.ajax({
        data: {
            codLicencia: $("#txtaddcodlic").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtaddcodlic").val(),
            CodSocio: $("#hidResponsable").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidResponsable").val(),
            anio: $("#txtaddanio").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtaddanio").val(),
            mes: $("#txtaddmes").val() == K_variables.TextoVacio ? K_variables.Cero : $("#txtaddmes").val(),
            codcab: $("#hidcabid").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidcabid").val()
        },
        url: '../AdministracionEmisionComplementaria/ListarConsultaLicenciaDetalle',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#gridconsultalicencia").html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}



function ListarLicenciaRegistradaDetalle() {

    $.ajax({
        data: {
            codcab: $("#hidcabid").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidcabid").val()
        },
        url: '../AdministracionEmisionComplementaria/ListarLicenciarRegistradaDetalle',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#gridlicenciaRegistrada").html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}


function Limpiar() {

    $("#hidcabid").val("");
    $("#txtaddnomproc").val("");
    $("#txtEmisionComplementariaDesc").val("");
    $("#ddlestadoAprobacionEmiComp").prop('selectedIndex', 1);
    $("#ddlTipoFecha").prop('selectedIndex', 1);
    $("#hidOficina").val("");
    $("#lbOficina").html('SELECCIONE OFICINA')
    $("#txtcodemicab").val("");
    $("#txtcodlic").val("");
    $("#hidResponsable").val("");
    $("#lbResponsable").html('TODOS');
    $("#txtaddcodlic").val("");
    $("#txtaddanio").val("");
    $("#txtaddmes").val("");
    $("#gridconsultalicencia").html('');
    $("#gridlicenciaRegistrada").html('');
    $("#grid").html('');
}


function Agregar(codLic,codPeriodo) {

    var codCabezera = $("#hidcabid").val();

    $.ajax({
        data: {
            CodCab: codCabezera,
            CodLic: codLic,
            CodPl: codPeriodo
        },
        url: '../AdministracionEmisionComplementaria/InsertarLicenciaPlaneamientoDetalle',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                
                ListarConsultaLicenciaDetalle();
                ListarLicenciaRegistradaDetalle();
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
    
}

function quitar(codDetalle) {


    $.ajax({
        data: {
            CodDet: codDetalle
        },
        url: '../AdministracionEmisionComplementaria/QuitarLicenciaPlaneamientoDetalle',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                ListarConsultaLicenciaDetalle();
                ListarLicenciaRegistradaDetalle();
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });


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
        }, show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }


    });

}


function AprobarComplementario(idCab) {

    ConfirmarAccion(' SE APROBARÁ LA EMISION COMPLEMENTARIA ESTA ACCION NO ES REVERSIBLE, (ESTA SEGURO(A) DE CONTINUAR ?',
function () {
    ProcesarSolicitudEmisionComplementaria(idCab,K_variables.Si);
},
function () { },
'Confirmar');
}

function RechazarComplementario(idCab) {


    ConfirmarAccion(' SE RECHAZARÁ LA EMISION COMPLEMENTARIA ESTA ACCION NO ES REVERSIBLE , (ESTA SEGURO(A) DE CONTINUAR ?',
function () {
    ProcesarSolicitudEmisionComplementaria(idCab, K_variables.No);
},
function () { },
'Confirmar');

}



function ProcesarSolicitudEmisionComplementaria(codcab,opcion) {


    $.ajax({
        data: {
            CodCab: codcab, RespuestaSoli: opcion
        },
        url: '../AdministracionEmisionComplementaria/RespuestaGenerarFacturacionMensual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "OK", K_variables.OkSimbolo);
                ListarEmisionComplementaria();
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });


}


function VerDocComplementario(CodigoCabezera) {


    $.ajax({
        data: {
            codcab: CodigoCabezera
        },
        url: '../AdministracionEmisionComplementaria/ListaDocumentoGeneradoxEmiComplementaria',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#MvMostrarDocumentosxSolicitud").dialog("open");
                $("#gridListarFacturasxSolicitudAprobada").html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}

function VerAprobacion(id) {

  
    $.ajax({
        data: {
            codcab: id
        },
        url: '../AdministracionEmisionComplementaria/ObtenerEmisionComplementaria',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                var entidad = dato.data.Data;
                
                //alert(entidad.NombreEmisionComplementaria);
                $("#ProcEmiComplId").val(entidad.CodigoEmisionComplementaria);
                $("#txtnomemicomp").val(entidad.NombreEmisionComplementaria);
                $("#txtdescpemicomp").val(entidad.RespuestaEmisionComplementaria);

                $("#MvObtenerEmisionComplementaria").dialog("open");

            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });   
}

function GuardarCambiosEmisionComplementaria() {

}

function verDetalleDocumento(id) {
    var url = '../ConsultaDocumentoDetalle/Index?id=' + id;
    window.open(url, "_blank");
}