var K_DIV_MESSAGE = {
    DIV_LICENCIA: "divResultadoCab"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {

    var id = GetQueryStringParams("id");
    var idAgent = GetQueryStringParams("idAgent");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Comisión de Agentes por Recaudo / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#btnBuscarTarifa").hide();
    } else {
        $("#divTituloPerfil").html("Comisión de Agentes por Recaudo / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidModalidad").val(id);
        obtenerNombreModalidad(id, "lblModalidad");
        ObtenerDatosModalidad(id);
        ObtenerDatos(id, idAgent);
        obtenerNombreSocio(idAgent);
        $("#ddlTemporalidad").prop('disabled', true);
        $("#btnBuscarTarifa").hide();
    }
    //---------------------------------------------------------------
    var eventoKp = "keypress";
    $('#txtValor').on(eventoKp, function (e) { return solonumeros(e); });
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadOrigenComision('ddlOrigenComision', 0);

    $("#btnGrabar").on("click", function () {
        //if ($("#hidOpcionEdit").val() == 0) {
        //    var estado = validarDuplicado();
        //    if (estado) {
        //        grabar();
        //    }
        //}
        //else
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../ComisionAgenteRecaudo/";
    });

    $("#ddlTemporalidad").on("change", function () {
        var index = document.getElementById("ddlTemporalidad").selectedIndex;
        if (index > 0) {
            var codigo = $("#ddlTemporalidad").val();
            ObtenerTarifaTemporalidad($("#hidModalidad").val(), codigo);
        }
        //else
        //    $("#lbTarifa").html("Seleccione");
    });
});

var reloadEventoMod = function (idModSel) {
    $("#hididTarifa").val(0);
    //$("#lbTarifa").html("Seleccione");
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    loadTemporalidad('ddlTemporalidad', $("#hidModalidad").val(), 0);
    ObtenerDatosModalidad($("#hidModalidad").val());
};

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    var estado = ObtenerNivelAgente($("#hidResponsable").val());
    if (estado) {
        obtenerNombreSocio($("#hidResponsable").val());
    };
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
}

function ObtenerDatos(id, id2) {
    $.ajax({
        url: "../ComisionAgenteRecaudo/ObtieneDatos",
        type: 'GET',
        data: { id: id, idAgente: id2 },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {


                    if ($("#hidOpcionEdit").val() == 1) {
                        loadTemporalidad('ddlTemporalidad', $("#hidModalidad").val(), en.RAT_FID);
                        var codigo = en.RAT_FID;
                        ObtenerTarifaTemporalidad($("#hidModalidad").val(), codigo);
                    }

                    loadTipoComision('ddlTipoComision', en.COMT_ID);
                    loadOrigenComision('ddlOrigenComision', en.COM_ORG);
                    loadFormatoComision('ddlFormatoComision', en.Formato);
                    $("#txtValor").val(en.Valor);
                    $("#hidNivelAgente").val(en.LEVEL_ID);
                    $("#hidResponsable").val(en.BPS_ID);
                    $("#hidResponsableAux").val(en.auxBPS_ID);
                    var datepicker = $("#txtFecha").data("kendoDatePicker");
                    datepicker.value(en.fechaStart);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerDatosModalidad(id) {
    $.ajax({
        url: "../ComisionAgenteRecaudo/ObtieneDatosModalidad",
        type: 'GET',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtOrigen").val(en.MOD_ODESC);
                    $("#txtSociedad").val(en.MOG_SDESC);
                    $("#txtClasesCreacion").val(en.CLASS_DESC);
                    $("#txtDerecho").val(en.RIGHT_DESC);
                    $("#txtGrupoModalidad").val(en.MOG_DESC);
                    $("#txtIncidencia").val(en.MOD_IDESC);
                    $("#txtFrecuenciaUso").val(en.MOD_DUSAGE);
                    $("#txtRepertorio").val(en.MOD_DREPER);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerTarifaTemporalidad(idMod, idTemp) {
    $.ajax({
        url: "../ComisionAgenteRecaudo/ObtieneTarifaTemporalidad",
        type: 'GET',
        data: { idModalidad: idMod, idTemporalidad: idTemp },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hididPeriodicidad").val(en.RAT_FID);
                    $("#hididTarifa").val(en.RATE_ID);
                    $("#lbTarifa").html(en.NAME);
                }
            } else if (dato.result == 0) {
                $("#lbTarifa").html("");
                alert(dato.message);
            }
        }
    });
}

function ObtenerNivelAgente(id) {
    var estado = false;
    $.ajax({
        url: '../ComisionAgenteRecaudo/ObtenerNivelAgente',
        type: 'GET',
        dataType: 'JSON',
        data: { idAgente: id },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidNivelAgente").val(en.LEVEL_ID);
                    estado = true;
                }
            } else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

//function validarDuplicado() {
//    var estado = false;
//    var en = {
//        MOD_ID: $("#hidModalidad").val(),
//        BPS_ID: $("#hidResponsable").val(),
//        RAT_FID: $("#ddlTemporalidad").val(),
//        COM_START: $("#txtFecha").val()
//    };

//    $.ajax({
//        url: '../ComisionAgenteRecaudo/Validacion',
//        type: 'POST',
//        dataType: 'JSON',
//        data: en,
//        async: false,
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                estado = false;
//                alert(dato.message);
//            } else {
//                estado = true;
//            }
//        }
//    });
//    return estado;
//}

function grabar() {
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidModalidad").val();
        }
        else
            id = $("#hidModalidad").val();

        var TipoComision = $("#ddlTipoComision").val();
        var OrigenComision = $("#ddlOrigenComision").val();
        var FormatoComision = $("#ddlFormatoComision").val();
        var Periodicidad = $("#ddlTemporalidad").val();

        var en = {
            valgraba: val,
            MOD_ID: id,
            RAT_FID: Periodicidad,
            BPS_ID: $("#hidResponsable").val(),
            auxBPS_ID: $("#hidResponsableAux").val(),
            LEVEL_ID: $("#hidNivelAgente").val(),
            COMT_ID: TipoComision,
            COM_ORG: OrigenComision,
            COM_START: $("#txtFecha").val(),
            Formato: FormatoComision,
            COM_PER: $("#txtValor").val(),
            COM_VAL: $("#txtValor").val()
        };

        $.ajax({
            url: 'Insertar',
            data: en,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    document.location.href = '../ComisionAgenteRecaudo/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};

