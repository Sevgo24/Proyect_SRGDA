var K_DIV_MESSAGE = {
    DIV_LICENCIA: "divResultadoCab"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {

    var id = GetQueryStringParams("id");
    var idDivAdm = GetQueryStringParams("idDivAdm");

    //alert(idDivAdm);
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTitulo").html("Comisión por exclusión administrativa / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#btnBuscarTarifa").hide();
    } else {
        $("#divTitulo").html("Comisión por exclusión administrativa / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidModalidad").val(id);
        obtenerNombreModalidad(id, "lblModalidad");
        ObtenerDatosModalidad(id);
        ObtenerDatos(id, idDivAdm);
        $("#ddlTemporalidad").prop('disabled', true);
        $("#btnBuscarTarifa").hide();
    }
    //---------------------------------------------------------------
    var eventoKP = "keypress";
    $('#txtValor').on(eventoKP, function (e) { return solonumeros(e); });
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    loadDivisionesadministrativas('');
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadOrigenComision('ddlOrigenComision', 0);

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../ComisionExclusion/";
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

function ObtenerDatosModalidad(id) {
    $.ajax({
        url: "../ComisionExclusion/ObtieneDatosModalidad",
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

function ObtenerDatos(id, id2) {
    $.ajax({
        url: "../ComisionExclusion/ObtieneDatos",
        type: 'GET',
        data: { id: id, idDivAdm: id2 },
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
                    loadDivisionesadministrativas(en.DADV_ID);
                    $("#hidAuxDivAdm").val(en.DADV_ID);
                    var datepicker = $("#txtFecha").data("kendoDatePicker");
                    datepicker.value(en.fechaStart);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerTarifaTemporalidad(idMod, idTemp) {
    $.ajax({
        url: "../ComisionExclusion/ObtieneTarifaTemporalidad",
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

//function validarDuplicado() {
//    var estado = false;
//    var en = {
//        DADV_ID: $("#ddlDivisionAdministrativa").val(),
//        MOD_ID: $("#hidModalidad").val()
//    };

//    $.ajax({
//        url: '../ComisionExclusion/Validacion',
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
        var DivisionAdministrativa = $("#ddlDivisionAdministrativa").val();
        var Periodicidad = $("#ddlTemporalidad").val();

        var en = {
            valgraba: val,
            MOD_ID: id,
            RAT_FID: Periodicidad,
            DADV_ID: DivisionAdministrativa,
            auxDADV_ID: $("#hidAuxDivAdm").val(),
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
                    document.location.href = '../ComisionExclusion/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};