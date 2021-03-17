$(function () {


});
function addAutorizacion() {

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_AUTORIZACION, K_DIV_POPUP.NUEVO_AUTORIZACION)) {

        var resp = ValidaLicenciaAutorizacion(1,0);

        if (resp) //-- igual a verdadero
        {
            var entidad = {
                FechaInicio: $("#txtAutoFecIni").val(),
                FechaFin: $("#txtAutoFecFin").val(),
                Observacion: $("#txtAutoObserv").val(),
                CodigoLicencia: $("#hidLicId").val(),
                CodigoAutorizacion: $("#hidEdicionAut").val()
            };
            //alert($("#hidAccionMvAut").val());
            if ($("#hidAccionMvAut").val() == 1) {
                $.ajax({
                    url: '../Autorizacion/UpdAutorizacion',
                    data: entidad,
                    type: 'POST',
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato); /*add sysseg*/
                        if (dato.result == 1) {
                            //alert(dato.message);
                            loadDataAutorizacion($("#hidLicId").val());
                            $("#" + K_DIV_POPUP.NUEVO_AUTORIZACION).dialog("close");
                        } else if (dato.result == 0) {
                            msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_AUTORIZACION, dato.message);
                            //alert(dato.message);
                        }
                    }
                });
            } else {
                $.ajax({
                    url: '../Autorizacion/Insertar',
                    type: 'POST',
                    data: entidad,
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato); /*add sysseg*/
                        if (dato.result == 1) {
                            //alert(dato.message);
                            loadDataAutorizacion($("#hidLicId").val());
                            $("#" + K_DIV_POPUP.NUEVO_AUTORIZACION).dialog("close");
                        } else if (dato.result == 0) {
                            msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_AUTORIZACION, dato.message);
                            //alert(dato.message);
                        }
                    }
                });
            }
        } else {
            alert("No se Inserto La Autorizacion ,no se encontro Planificacion para la Licencia");
        }

    }

}
function limpiarAutorizacion() {
    $("#txtAutoFecIni").val("");
    $("#txtAutoFecFin").val("");
    $("#txtAutoObserv").val("");
    $("#hidAccionMvAut").val("0");
    $("#hidEdicionAut").val("0");
    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_AUTORIZACION, K_DIV_POPUP.NUEVO_AUTORIZACION);

}
function loadDataAutorizacion(codLic) {
    loadDataGridAutorizaciones('ListarAutorizacion', "#gridAutorizacion", codLic);
}

function loadDataGridAutorizaciones(Controller, idGrilla, codLic) {
    $.ajax({
        data: { codigoLic: codLic },
        type: 'POST', url: "../Autorizacion/" + Controller,
        beforeSend: function () { },
        success: function (response) { var dato = response; validarRedirect(dato); /*add sysseg*/$(idGrilla).html(dato.message); }
    });
}

function delAddAutorizacion(idDel, esActivo) {
    var inactiva = ValidarLicenciaFacturada();
    if (!inactiva) {//si es verdad entonces es por que no tiene ninguna factura cancelada
    $.ajax({
        url: '../Autorizacion/Eliminar',
        type: 'POST',
        data: { id: idDel, EsActivo: esActivo },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataAutorizacion($("#hidLicId").val());
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

function updAddAutorizacion(idUpd) {
    limpiarAutorizacion();

    $.ajax({
        url: '../Autorizacion/ObtieneAutorizacion',
        data: { idLic: $("#hidLicId").val(), idAt: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var auto = dato.data.Data;
                if (auto != null) {
                    $("#hidAccionMvAut").val("1");
                    $("#hidEdicionAut").val(auto.CodigoAutorizacion);

                    $("#txtAutoFecIni").val(auto.FechaInicio);
                    $("#txtAutoFecFin").val(auto.FechaFin);
                    $("#txtAutoObserv").val(auto.Observacion);
                    $("#mvAutorizacion").dialog("option", "title", "Actualizar Autorización");
                    $("#mvAutorizacion").dialog("open");
                } else {
                    alert("No se pudo obtener la autorización para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}



function editarArtista(showid,artistaid) {

    $("#MVObrasArtista").dialog("open");
    obtenerNombreShow(showid, "lblnombreshow");
    obtenerNombreArtista(artistaid, "lblnombreartista");
    //alert(artistaid);
    //176724
    //alert("SELECCIONE CANCIONES AL ARTISTA");
}

function ValidaLicenciaAutorizacion(ACCION,PERIODO) {
    //alert("Entro");
    var lic_id = $("#hidLicId").val();
    var retorno = false;
    $.ajax({
        data: { LIC_ID: lic_id, ACCION: ACCION, LIC_PL_ID: PERIODO },
        url: "../Licencia/ValidaLicenciaPlanificacionAutorizacion",
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                retorno = true;
            }
            else {
                retorno = false;
            }
        }
    });

    return retorno;
}