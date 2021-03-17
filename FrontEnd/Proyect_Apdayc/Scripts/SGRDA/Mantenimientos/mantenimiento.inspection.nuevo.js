/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_CAR = 450;
var K_HEIGHT_CAR = 230;

$(function () {
    mvInitEstablecimiento({
        container: "ContenedormvEstablecimiento",
        idButtonToSearch: "btnBuscarEstablecimiento",
        idDivMV: "mvEstablecimiento",
        event: "reloadEventoEst"
    });
    
    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarSocio",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento"
    });

    $("#tabs").tabs();
    $("#mvCaracteristica").dialog({ autoOpen: false, width: K_WIDTH_CAR, height: K_HEIGHT_CAR, buttons: { "Agregar": addCaracteristica, "Cancel": function () { $("#mvCaracteristica").dialog("close"); $('#txtvalor').css({ 'border': '1px solid gray' }); } }, modal: true });

    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Inspeccion / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#lblCodigo").hide();
        $("#txtCodigo").hide();
        $("#txthora").hide();
        $("#checkhora").hide();
        $("#lblmodifica").hide();
    } else {
        $("#divTituloPerfil").html("Inspeccion / Actualizacion");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoInspection").val(id);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos(id);
        $("#txthoras").hide();
        $("#txthora").prop('disabled', true);
    }
    //---------------------------------------------------------------

    $("#btnGrabar").on("click", function () {
        grabarInspection();
    }).button();

    $("#btnDescartar").on("click", function () {
        location.href = "../Inspection";
    }).button();

    $("#btnNuevo").on("click", function () {
        $("#txtCodigo").removeAttr("disabled");
        limpiar();
    }).button();

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#btnVolver").on("click", function () {
        location.href = "../Inspection/";
    }).button();

    $("#txtDocumento").focus();
    
    $("#checkhora").change(function () {
        if ($("#checkhora").is(':checked')) {
            $("#txthora").hide();
            $("#txthoras").show();
            $("#txthoras").addClass("requerido");
        } else {
            $("#txthora").show();
            $("#txthoras").hide();
            $("#txthoras").removeClass("requerido");
        }
    });

    $("#addCaracteristica").on("click", function () {
        $("#mvCaracteristica").dialog({});
        $("#mvCaracteristica").dialog("open");
        limpiarCaracteristica();
    });
    loadCaracteristica(0);
    loadDataCaracteristica();
    loadDataCaracteristica();
    loadDataCaracteristica();
});

function loadDataCaracteristica() {
    loadDataGridTmp('ListarCaracteristica', "#gridCaracteristica");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidBPSid").val(idSel);
    alert($("#hidBPSid").val());
    ObtieneNombreEntidad(idSel, "lblsocio");
};

var reloadEventoEst = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidCodigoEST").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblNombreestablecimiento");
    obtenerCaracteristicaestablecimiento($("#hidCodigoEST").val());
    loadDataCaracteristica();
};

function limpiar() {
    $("#txtCodigo").val("");
    $("#txtDocumento").val("");
    $("#txthora").val("");
    $("#txtInspector").val("");
    $("#txtCodigo").focus();
}

//---------------------------Grabar Datos-------------------------------------
function grabarInspection() {
    var estadoRequeridos = ValidarRequeridos();
    var estadoBusquedaGeneral = validarBusquedaGeneral();
    var estadovalor = validacionCaracteristicaValores();

    if (estadoRequeridos && estadoBusquedaGeneral && estadovalor) {
        grabar();
    }
};

function validarBusquedaGeneral() {
    var resultado = true;
    if ($("#hidBPSid").val() == 0) {
        $("#lblsocio").html("busque socio");
        resultado = false;
    }

    if ($("#hidCodigoEST").val() == 0) {
        $("#lblNombreestablecimiento").html("busque establecimiento");
        resultado = false;
    }
    return resultado;
}

function grabar() {
    var id = 0;
    var val = $("#hidOpcionEdit").val();
    if (val == 1) {
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCodigoInspection").val();
    }
    else
        id = $("#txtCodigo").val();

    var inspection = {
        valgraba: val,
        INSP_ID: id,
        EST_ID: $("#hidCodigoEST").val(),
        INSP_DOC: $("#txtDocumento").val(),
        INSP_OBS: $("#txtObservacion").val(),
        BPS_ID: $("#hidBPSid").val(),
        INSP_DATE: $("#txtFecha").val(),
        INSP_HOUR: $("#txthoras").val()
    };
    $.ajax({
        url: 'Insertar',
        data: inspection,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Inspection/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

//-----------------------Fin Grabar Datos-------------------------------------


function obtenerCaracteristicaestablecimiento(idSel) {
    $.ajax({
        url: '../Inspection/obtenerCaracteristicaestablecimiento',
        type: 'POST',
        data: { id: idSel },
        beforeSend: function () { },
        success: function (response) {}        
    });
}


//---------------------------Editar Datos-------------------------------------
function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Inspection/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var inspection = dato.data.Data;
                if (inspection != null) {
                    $("#txtCodigo").val(inspection.INSP_ID);
                    $("#txtDocumento").val(inspection.INSP_DOC);
                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(inspection.INSP_DATE);
                    d1.value(valFecha);
                    $("#txthora").val(inspection.HOUR);
                    $("#hidCodigoEST").val(inspection.EST_ID);
                    $("#hidBPSid").val(inspection.BPS_ID);
                    ObtieneNombreEntidad(inspection.BPS_ID, "lblsocio");
                    ObtenerNombreEstablecimiento(inspection.EST_ID, "lblNombreestablecimiento");
                    $("#txtObservacion").val(inspection.INSP_OBS);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
//-----------------------------------------------------------------------------


var addCaracteristica = function () {

    if ($("#txtvalor").val() == '') {
        $('#txtvalor').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        if ($("#hidAccionMvCar").val() === "1") IdAdd = $("#hidEdicionCar").val();

        var entidad = {
            Id: IdAdd,
            Idcaracteristica: $("#ddlCaracteristica option:selected").val(),
            caracteristica: $("#ddlCaracteristica option:selected").text(),
            //TipoEstablecimiento: $("#ddlTipoestablecimiento option:selected").val(),
            //IdSubTipoEstablecimiento: $("#ddlSubtipoestablecimiento option:selected").val(),
            Valor: $("#txtvalor").val()
        };

        $.ajax({
            url: 'AddCaracteristica',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataCaracteristica();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvCaracteristica").dialog("close");
        $('#txtvalor').css({ 'border': '1px solid gray' });
        $("#hidEdicionCar").val("");
    }
}

function updAddCaracteristica(idUpd) {
    limpiarCaracteristica();
    $.ajax({
        url: 'ObtieneCaracteristicaTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var car = dato.data.Data;
                if (car != null) {
                    $("#hidAccionMvCar").val("1");
                    $("#hidEdicionCar").val(car.Id);
                    $("#ddlCaracteristica").val(car.Idcaracteristica);
                    $("#txtvalor").val(car.Valor);
                    $("#mvCaracteristica").dialog("open");
                } else {
                    alert("No se pudo obtener la caracteristica para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function delAddCaracteristica(idDel) {
    $.ajax({
        url: 'DellAddCaracteristica',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataCaracteristica();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarCaracteristica() {
    $("#ddlCaracteristica").val(0);
    $("#txtvalor").val("");
    $("#hidAccionMvPar").val(0);
    $("#hidEdicionPar").val(0);
}

function validacionCaracteristicaValores() {
    var resultado = true;
    $('#tblCaracteristica tr').each(function () {
        var valor = $(this).find("td").eq(3).html();
        if (valor != null) {
            //alert(valor);
            if (valor == 0) {
                resultado = false
                alert("Verifique. tiene un valor con cero en las caracteristicas");
            }
        }
    })
    return resultado;
}