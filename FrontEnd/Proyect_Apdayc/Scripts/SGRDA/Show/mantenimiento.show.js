function insertarShow() {
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_SHOW, K_DIV_POPUP.SHOWS)) {
        var codigo = $("#hidIdShowEditar").val() == "" ? 0 : $("#hidIdShowEditar").val();
        var show = {
            Codigo: codigo,
            Nombre: $("#txtShowNombre").val(),
            FechaInicio: $("#txtShowFecIni").val(),
            FechaFin: $("#txtShowFecFin").val(),
            Observacion: $("#txtShowObserv").val(),
            Orden: $("#txtOrden").val(),
            CodigoAutorizacion: $("#hidIdAutorizacionShow").val()
        };
        $.ajax({
            url: '../Show/Insertar',
            type: 'POST',
            data: show,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    listarShow(show.CodigoAutorizacion);
                    // alert(dato.message);
                    $("#" + K_DIV_POPUP.SHOWS).dialog("close");

                } else if (dato.result == 0) {
                    //alert(dato.message);
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_SHOW, dato.message);
                }
            }
        });


    }

}
function listarShow(idAutorizacion) {
    $.ajax({
        url: '../Show/ListarShowHtml',
        type: 'POST',
        data: { CodigoAutorizacion: idAutorizacion },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#divShow_" + idAutorizacion).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function agregarShow(idAut) {
    limpiarShow();
    $("#hidAccionMvShow").val(K_ACCION.Nuevo);
    $("#hidIdAutorizacionShow").val(idAut);
    $("#" + K_DIV_POPUP.SHOWS).dialog("option", "title", "Registro de Nuevo Show.");
    $("#" + K_DIV_POPUP.SHOWS).dialog("open");
}
function editarShow(idShow) {
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_SHOW, "");
    limpiarShow();
    $("#hidIdShowEditar").val(idShow);
    $("#hidAccionMvShow").val(K_ACCION.Modificacion);

    $.ajax({
        url: '../Show/Obtener',
        type: 'POST',
        data: { id: idShow },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var show = dato.data.Data;
                $("#txtShowNombre").val(show.Nombre);
                $("#txtShowFecIni").val(show.FechaInicio);
                $("#txtShowFecFin").val(show.FechaFin);

                $("#txtShowObserv").val(show.Observacion);
                $("#txtOrden").val(show.Orden);
                $("#hidIdAutorizacionShow").val(show.CodigoAutorizacion);

            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_SHOW, dato.message);
            }
        }
    });
    $("#" + K_DIV_POPUP.SHOWS).dialog("option", "title", "Actualización de Show.");
    $("#" + K_DIV_POPUP.SHOWS).dialog("open");
}
function limpiarShow() {
    $("#hidAccionMvShow").val("");
    $("#hidIdShowEditar").val("");
    $("#hidIdAutorizacionShow").val("");
    $("#txtShowNombre").val("");
    $("#txtShowFecIni").val("");
    $("#txtShowFecFin").val("");
    $("#txtShowObserv").val("");
    $("#txtOrden").val("0");
}
/*lista la grilla de shows luego de una actualizacion o inserccion*/
function verShows(id) {
    if ($("#expand" + id).attr('src') == '../Images/iconos/minus.png') {
        $("#expand" + id).attr('src', '../Images/iconos/plus.png');
        $("#expand" + id).attr('title', 'Ver Shows de Autorización.');
        $("#expand" + id).attr('alt', 'Ver Shows de Autorización.');
        $("#divShow_" + id).css("display", "none");
        $("#tdShow_" + id).css("background", "transparent");

    } else {
        $("#expand" + id).attr('src', '../Images/iconos/minus.png');
        $("#expand" + id).attr('title', 'Ocultar Shows de Autorización.');
        $("#expand" + id).attr('alt', 'Ocultar Shows de Autorización.');
        listarShow(id);
        $("#divShow_" + id).css("display", "inline");
        $("#tdShow_" + id).css("background", "#dbdbde");
    }

}
function delShow(idDel, esActivo, idAuto) {
    var inactiva = ValidarLicenciaFacturada();
    if (!inactiva) {//si es verdad entonces es por que no tiene ninguna factura cancelada
    $.ajax({
        url: '../Show/Eliminar',
        type: 'POST',
        data: { id: idDel, EsActivo: esActivo },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                listarShow(idAuto);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
    } else {
        alert("No puede Activarse/Inactivarse una factura Relacionada a la Licencia ha sido Cancelada ");
    }
}