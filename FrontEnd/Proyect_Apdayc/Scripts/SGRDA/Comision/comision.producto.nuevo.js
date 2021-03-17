var K_DIV_MESSAGE = {
    DIV_LICENCIA: "divResultadoCab"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {

    var id = GetQueryStringParams("id");
    var idNivAgent = GetQueryStringParams("idNivAgent");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Comisión por Producto / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#btnBuscarTarifa").hide();
    } else {
        $("#divTituloPerfil").html("Comisión por Producto / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidModalidad").val(id);
        obtenerNombreModalidad(id, "lblModalidad");
        ObtenerDatosModalidad(id);
        ObtenerDatos(id, idNivAgent);
        $("#ddlTemporalidad").prop('disabled', true);
        $("#btnBuscarTarifa").hide();
    }
    //---------------------------------------------------------------
    var eventoKP = "keypress";
    $('#txtValor').on(eventoKP, function (e) { return solonumeros(e); });
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    
    loadAgente('ddlNivelAgente', 0);
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadOrigenComision('ddlOrigenComision', 0);

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../ComisionProducto/";
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
        url: "../ComisionProducto/ObtieneDatosModalidad",
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
        url: "../ComisionProducto/ObtieneDatos",
        type: 'GET',
        data: { id: id, idNivAgent: id2 },
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
                    loadAgente('ddlNivelAgente', en.LEVEL_ID);
                    $("#hidAuxNivAgent").val(en.LEVEL_ID);
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
//        LEVEL_ID: $("#ddlNivelAgente").val(),
//        MOD_ID: $("#hidModalidad").val(),
//        valgraba: $("#hidOpcionEdit").val()
//    };

//    $.ajax({
//        url: '../ComisionProducto/Validacion',
//        type: 'POST',
//        dataType: 'JSON',
//        data: en,
//        async: false,
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                estado = true;
//                if (dato.message != null)
//                    alert(dato.message);
//            } else {
//                estado = false;
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
        var NivelAgente = $("#ddlNivelAgente").val();
        var Periodicidad = $("#ddlTemporalidad").val();

        var en = {
            valgraba: val,
            MOD_ID: id,
            RAT_FID: Periodicidad,
            LEVEL_ID: NivelAgente,
            auxLEVEL_ID: $("#hidAuxNivAgent").val(),
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
                    document.location.href = '../ComisionProducto/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};