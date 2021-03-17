var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function () { 

    var eventoKP = "keypress";
    $('#txtNroEjecutar').on(eventoKP, function (e) { return solonumeros(e); });
    $('#chkVisible').prop('checked', false);
    var id = GetQueryStringParams("id");
    $("#txtCodigo").focus();

    //LoadCicloAprobacion('ddlCiclo');
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Workflow - Proceso / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#txtCodigo").hide();
        $("#codigo").hide();
        loadTipoproceso("ddlTipoproceso");
        LoadCicloAprobacion('ddlCiclo');
        LoadModuloNombre("ddlCliente");
        loadPeriodocidad("ddlFrecuenia");
    } else {
        $("#divTituloPerfil").html("Workflow - Proceso / Actualizacion");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoPRO").val(id);
        $("#txtCodigo").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnDescartar").on("click", function () {
        location.href = "Index";
    }).button();

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();
});


function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Procesos/Obtiene',
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;
                if (item != null) {
                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(item.PROC_LAUNCH);
                    d1.value(valFecha);
                    $("#txtCodigo").val(item.PROC_ID);
                    $("#txtNombre").val(item.PROC_NAME);
                    $("#txtDescripcion").val(item.PROC_DESC);
                    loadTipoproceso("ddlTipoproceso", item.PROC_TYPE);
                    LoadCicloAprobacion('ddlCiclo', item.WRKF_ID);
                    LoadModuloNombre("ddlCliente", item.WRKF_CID);
                    $("#txtFunción").val(item.PROC_FUCTION );
                    $("#txtNroEjecutar").val(item.PROC_JOBS);
                    LoadModuloNombre("ddlFrecuenia", item.PROC_FREQ_TYPE);
                    if(item.PROC_SHOW=='1')
                        $('#chkVisible').prop('checked', true);
                    else
                        $('#chkVisible').prop('checked', false);

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

function grabar() {
    if (ValidarRequeridos()) {
        var id = 0;  
        if (K_ACCION_ACTUAL === K_ACCION.Modificacion)
            id = $("#hidCodigoPRO").val();
        else
            id = $("#txtCodigo").val();

        var visible = '0';
        if ($('#chkVisible').is(':checked')) visible = '1';

        var proceso = {
            PROC_ID: id,
            PROC_NAME: $("#txtNombre").val(),
            PROC_DESC: $("#txtDescripcion").val(),
            PROC_DESC: $("#txtDescripcion").val(),
            PROC_SHOW: visible,
            PROC_TYPE: $("#ddlTipoproceso").val(),
            WRKF_ID: $("#ddlCiclo").val(),
            WRKF_CID: $("#ddlCliente").val(),
            PROC_FUCTION: $("#txtFunción").val(),
            PROC_JOBS: $("#txtNroEjecutar").val(),
            PROC_FREQ_TYPE: $("#ddlFrecuenia").val(),
            PROC_LAUNCH: $("#txtFecha").val()
        };
        $.ajax({
            url: 'Insertar',
            data: proceso,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../Procesos/";                    
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
}