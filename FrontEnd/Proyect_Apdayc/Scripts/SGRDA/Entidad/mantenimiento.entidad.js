//var K_WIDTH_OBS = 700;
//var K_DIV_MESSAGE = {
//    DIV_TAB_POPUP_ENTIDAD: "avisoEntidad"
//};

//var K_DIV_POPUP = {
//    NUEVO_ENTIDAD: "mvEntidad"
//};
function loadDataEntidades(idLic) {
    loadDataGridTmpEntidad('../Entidad/ListarEntidad', "#gridEntidad", idLic);
}

function loadDataGridTmpEntidad(Controller, idGrilla, idLic) {
    $.ajax({
        data: { idLic: idLic }, type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                //var datos = dato.data.Data;
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}
//$(function () {
    //var eventoKP = "keypress";
    //$('#txtDocumento').on("keypress", function (e) { return solonumeros(e); });

    //$("#hidEdicionEnt").val("0");
    //$("#hidEdicionEnt").val("1");

    //$("#mvEntidad").dialog({ autoOpen: false, width: 700, height: 280, buttons: { "Agregar": addEntidad, "Cancelar": function () { $("#mvEntidad").dialog("close"); } }, modal: true });
    //$(".addEntidad").on("click", function () { limpiarEntidad(); $("#mvEntidad").dialog("open"); });
    
    //loadTipoDocumento("ddlTipoDocumento1", 0);

    //$("#btnBuscarSocio").on("click", function () {
    //    buscarSocio();
    //});
//});

function delEntidad(idDel, esActivo) {
    $.ajax({
        url: '../Entidad/Eliminar',
        type: 'POST',
        data: { idEntidad: idDel, EsActivo: esActivo },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataEntidades($("#hidLicId").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function addEntidad() {
  //  if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_ENTIDAD, K_DIV_POPUP.NUEVO_ENTIDAD)) {
        var entidad = {
            idEntidad: $("#hidEntidadId").val(),
            idLic: $("#hidLicId").val(),
            idBps: $("#hidBpsId").val()            
        }
        if ($("#hidAccionMvEnt").val() == 1) {
            $.ajax({
                url: '../Entidad/UpdEntidad',
                data: entidad,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        alert(dato.message);
                        loadDataEntidades($("#hidLicId").val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        } else {
            $.ajax({
                url: '../Entidad/Insertar',
                data: entidad,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        alert(dato.message);
                        loadDataEntidades($("#hidLicId").val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        }
    //    $("#" + K_DIV_POPUP.NUEVO_ENTIDAD).dialog("close");
   /// }
}

function updAddEntidad(idUpd) {
    limpiarEntidad();
    $.ajax({
        url: '../Entidad/ObtenerXCodigo',
        data: { idEntidad: idUpd, idLic: $("#hidLicId").val() },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidAccionMvEnt").val("1");
                    $("#hidEntidadId").val(en.Id);
                    $("#txtNombreEntidad").val(en.Nombre);
                    $("#hidBpsId").val(en.IdBps);
                    $("#txtDocumento").val(en.NroDocumento);
                    $("#ddlTipoDocumento1").val(en.TipoDocumento);
                    $("#mvEntidad").dialog("open");
                } else {
                    alert("No se pudo obtener la Entidad para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiarEntidad() {
    $("#hidAccionMvEnt").val("0");
    $("#hidEdicionEnt").val("");
    $("#ddlTipoDocumento1").val("");
    $("#txtDocumento").val("");
    $("#txtSocioAsociado").val("");
    $("#txtNombreEntidad").val("");
    $("#hidEdicionEnt").val("0");
}

function buscarSocio() {
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    var doc = $("#ddlTipoDocumento1").val();
    var nro = $("#txtNombreEntidad").val();

    $.ajax({
        data: { tipo: doc, nro_tipo: nro },
        url: '../SOCIO/ObtenerSocioDocumento',
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $("#hidBpsId").val(datos.Codigo);
                var nombre = datos.Nombres + " " + datos.Paterno + " " + datos.Materno;
                if (doc == 1) {
                    nombre = datos.RazonSocial;
                }
                $("#txtNombreEntidad").val(nombre);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}