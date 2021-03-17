var K_DIV_MESSAGE = {
    DIV_LICENCIA: "divResultadoCab"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () {

    var id = GetQueryStringParams("id");
    var idNivAgent = GetQueryStringParams("idNivAgent");
    var idOficina = GetQueryStringParams("idOficina");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Comisión Oficinas Comerciales / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        //alert("Nuevo");
    } else {
        $("#divTituloPerfil").html("Comisión Oficinas Comerciales / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidModalidad").val(id);
        obtenerNombreModalidad(id, "lblModalidad");
        ObtenerDatosModalidad(id);
        ObtenerDatos(id, idNivAgent, idOficina);
        //alert("Actualizar");
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
    loadOficinasComerciales('ddlOficinaComercial', 0);
    
    $("#btnGrabar").on("click", function () {
        //if ($("#hidOpcionEdit").val() == 0) {
        //    var estado = validarDuplicado();
        //    if (estado) {
        //        grabar();
        //    }
        //}
        //else
        //    grabar();

        //var estado = validarDuplicado();
        //if (estado) {
        //    grabar();
        //}

        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../ComisionOficinasComerciales/";
    });
});

var reloadEventoMod = function (idModSel) {
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    ObtenerDatosModalidad($("#hidModalidad").val());
};

function ObtenerDatosModalidad(id) {
    $.ajax({
        url: "../ComisionOficinasComerciales/ObtieneDatosModalidad",
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

function ObtenerDatos(id, id2, id3) {
    $.ajax({
        url: "../ComisionOficinasComerciales/ObtieneDatos",
        type: 'GET',
        data: { id: id, idNivAgent: id2, idOficina: id3 },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    loadTipoComision('ddlTipoComision', en.COMT_ID);
                    loadOrigenComision('ddlOrigenComision', en.COM_ORG);
                    loadFormatoComision('ddlFormatoComision', en.Formato);
                    $("#txtValor").val(en.Valor);
                    loadAgente('ddlNivelAgente', en.LEVEL_ID);
                    $("#hidAuxNivAgent").val(en.LEVEL_ID);
                    loadOficinasComerciales('ddlOficinaComercial', en.OFF_ID);
                    $("#hidAuxOficina").val(en.OFF_ID);
                    var datepicker = $("#txtFecha").data("kendoDatePicker");
                    datepicker.value(en.fechaStart);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//function validarDuplicado() {
//    var estado = false;
//    var en = {
//        LEVEL_ID: $("#ddlNivelAgente").val(),
//        OFF_ID: $("#ddlOficinaComercial").val(),
//        MOD_ID: $("#hidModalidad").val()
//    };

//    $.ajax({
//        url: '../ComisionOficinasComerciales/Validacion',
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
        var NivelAgente = $("#ddlNivelAgente").val();
        var OficinaComercial = $("#ddlOficinaComercial").val();

        var en = {
            valgraba: val,
            MOD_ID: id,
            OFF_ID: OficinaComercial,
            auxOFF_ID: $("#hidAuxOficina").val(),
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
                    document.location.href = '../ComisionOficinasComerciales/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};