var K_WIDTH_OBS2 = 600;
var K_HEIGHT_OBS2 = 325;
var K_RECHAZADO = 2;

$(function () {

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    LimpiarRequerimientos();
    loadTipoRquerimiento('ddltiporequerimiento', 0, 0);
    loadTipoInactivacionLicencia('ddltipoInactConsult', 0);
    loadTipoInactivacionLicencia('ddltipoInact', 0);

    Listar();
    $("#btnBuscar").on("click", function () {
        Listar();
    });

    $("#btnLimpiar").on("click", function () {
        LimpiarRequerimientos();
    });


    $("#mvSolicitudReque").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": GuardarSolicitud,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvSolicitudReque").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    $("#trdocumento").hide();
    $("#trlicencia").hide();
    $("#trlicencia2").hide();
    $("#trestablecimiento").hide();
    $("#trdocumento2").hide();
    $("#trsocio").hide();
    $("#trrechazo").hide();
    $("#trbec").hide();

    $("#ddltipoAprobacionResp").on("change", function () {

        if ($("#ddltipoAprobacionResp").val() == K_RECHAZADO) {
            $("#trrechazo").show();
        } else {
            $("#trrechazo").hide();
        }
    });


    // ABRIR VENTANAS

    $("#lbllicid").on("click", function () {
        window.open("../Licencia/Nuevo?set=" + $("#lbllicid").html());
    });
    $("#lbllicname").on("click", function () {
        window.open("../Licencia/Nuevo?set=" + $("#lbllicid").html());
    });

    $("#lblestid").on("click", function () {
        window.open("../Establecimiento/Create?id=" + $("#lblestid").html());
    });

    $("#lblestname").on("click", function () {
        window.open("../Establecimiento/Create?id=" + $("#lblestid").html());
    });



});


function Listar() {
    var ID_REQ = $("#txtidreq").val() == "" ? "0" : $("#txtidreq").val();
    var TIPO = $("#ddltiporequerimiento").val();
    var ESTADO = $("#ddlestadoAprobacion").val();
    var CON_FECHA = $("#chkConFechaCrea").is(':checked') == true ? 1 : 0;
    var FECHA_INI = $("#txtFecCreaInicial").val();
    var FECHA_FIN = $("#txtFecCreaFinal").val();
    var OFICINA = $("#hidOficina").val() == "" ? "0" : $("#hidOficina").val();
    var LIC_ID = $("#txtlic").val() == "" ? "0" : $("#txtlic").val();
    var INV_ID = $("#txtdoc").val() == "" ? "0" : $("#txtdoc").val();
    var EST_ID = $("#txtestid").val() == "" ? "0" : $("#txtestid").val();
    var BEC_ID = $("#txtbec").val() == "" ? "0" : $("#txtbec").val();
    var TIPO_INACTIVACION = $("#ddltipoInactConsult").val();

    $.ajax({
        data: {
            ID_REQ: ID_REQ, TIPO: TIPO, ESTADO: ESTADO, CON_FECHA: CON_FECHA,
            FECHA_INI: FECHA_INI, FECHA_FIN: FECHA_FIN, OFICINA: OFICINA, LIC_ID: LIC_ID, INV_ID: INV_ID, EST_ID: EST_ID, BEC_ID: BEC_ID, INACT_TYPE: TIPO_INACTIVACION
        },
        url: '../AdministracionModuloRequerimientos/Listar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#grid").html(dato.message);
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

function LimpiarRequerimientos() {
    $("#txtidreq").val("");
    $("#txtlic").val("");
    $("#txtdoc").val("");
    $("#chkConFechaCrea").prop("checked", false);
    $("#txtFecCreaInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecCreaFinal").data("kendoDatePicker").value(new Date());
    $("#ddlestadoAprobacion").prop('selectedIndex', 0);
    $("#hidOficina").val("");
    $("#lbOficina").html('Seleccione Oficina');
    $("#txtestid").val("");
    $("#trdocumento").hide();
    $("#trlicencia").hide();
    $("#trlicencia2").hide();
    $("#trestablecimiento").hide();
    $("#trdocumento2").hide();
    $("#trsocio").hide();
    $("#trbec").hide();
    $("#hidBecId").val("");
    $("#hidRecId").val("");
}




//function loadTipoRquerimiento(control, valSel) {

//    $('#' + control + ' option').remove();
//    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
//    $.ajax({
//        async :false,
//        url: '../AdministracionModuloRequerimientos/ListarTipoRequerimiento',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;
//                $.each(datos, function (indice, entidad) {
//                    if (entidad.Value == valSel)
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
//                    else
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
//                });
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}

function GuardarSolicitud() {

    var ID = $("#hidId").val();
    var ESTADO = $("#ddltipoAprobacionResp").val();
    var RECHAZO_RESP = $("#txtAprobacionDescResp").val();
    //var LIC_ID = $("#lbllicid").val() == "" ? 0 : $("#lbllicid").val();
    //var INV_ID = $("#hidIdDoc").val() == "" ? 0 : $("#hidIdDoc").val();

    $.ajax({
        data: { ID_REQ: ID, ESTADO: ESTADO, RECHAZO_RESP: RECHAZO_RESP },
        url: '../AdministracionModuloRequerimientos/RespuestaRequerimiento',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                $("#mvSolicitudReque").dialog("close");
                Listar();
                //$("#ddltipoAprobacion").val(entidad.TIPO);

            } else {
                alert(dato.message);
            }
        }
    });
}

function ModificarRequerimiento(id) {
    $("#trdocumento").hide();
    $("#trlicencia").hide();
    $("#trlicencia2").hide();
    $("#trestablecimiento").hide();
    $("#trdocumento2").hide();


    ObtenerRequerimiento(id);
    $("#mvSolicitudReque").dialog("open");
}


function ObtenerRequerimiento(id) {

    $.ajax({
        data: { ID: id },
        url: '../AdministracionModuloRequerimientos/ObtieneRequerimiento',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var entidad = dato.data.Data;

                $("#lblltiporequerimiento").html(entidad.REQUERIMENTS_DESC);
                $("#lblserie").html(entidad.SERIE + '-');
                $("#lblnumero").html(entidad.NUMERO + ' | MONTO: ');
                $("#lblmontoactual").html(entidad.INV_NET + ' | FECHA ACTUAL:');
                $("#lblfechaactual").html(entidad.INV_DATE);
                $("#lblmontomod").html('MONTO A ACTUALIZAR: ' + (entidad.INV_NETACT == 0 ? 'SIN MODIFICACION ' : entidad.INV_NETACT));
                $("#lblfechamod").html(' | FECHA A ACTUALIZAR : ' + (entidad.REQ_DATE == '' ? 'SIN MODIFICAR' : entidad.REQ_DATE));


                $("#lbllicid").html(entidad.LIC_ID);
                $("#lbllicname").html(' |  ' + entidad.LIC_NAME);
                $("#lblestid").html(entidad.EST_ID);
                $("#lblestname").html(' |  ' + entidad.EST_NAME);
                $("#txtAprobacionDesc").val(entidad.REQ_DESCRIPCION);
                $("#hidId").val(entidad.ID_REQ);
                $("#hidIdDoc").val(entidad.INV_ID);
                $("#ddltipoAprobacionResp").val(entidad.ESTADO);

                $("#lblsocio").html(entidad.SOCIO);
                $("#lbldocumento").html(' |  ' + entidad.DOCUMENTOSOCIO);
                $("#hidSocioId").val(entidad.BPS_ID);
                $("#txtAprobacionDescResp").val(entidad.REQ_DESCRIPCION_RESP);
                $("#lblbec").html(entidad.BEC_ID);
                $("#hidBecId").val(entidad.BEC_ID);
                $("#hidRecId").val(entidad.REC_ID);
                $("#ddltipoInact").val(entidad.CodigoTipoInactivacion);

                if (entidad.LIC_ID > 0) {
                    $("#trlicencia").show();
                    $("#trlicencia2").show();
                }
                if (entidad.EST_ID > 0)
                    $("#trestablecimiento").show();
                if (entidad.INV_ID > 0) {
                    $("#trdocumento").show();
                    $("#trdocumento2").show();
                }

                if (entidad.BPS_ID > 0)
                    $("#trsocio").show();

                if (entidad.BEC_ID > 0)
                    $("#trbec").show();

                if ($("#ddltipoAprobacionResp").val() == K_RECHAZADO)
                    $("#trrechazo").show();
                else
                    $("#trrechazo").hide();
                //$("#ddltipoAprobacion").val(entidad.TIPO);

            } else {
                alert(dato.message);
            }
        }
    });

}

function VerSocio() {

    var SocioId = $("#hidSocioId").val();

    window.open("../Socio/Nuevo?set=" + SocioId);


}

function VerBec() {
    window.open('../BEC/Nuevo?id=' + $("#hidRecId").val() + '&ver=' + 'N');
}