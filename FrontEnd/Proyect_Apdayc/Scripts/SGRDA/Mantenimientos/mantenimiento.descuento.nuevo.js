/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

var K_DIV_POPUP = {

    DESCUENTO: "mvBuscarPlantDesc"
};
/*INICIO CONSTANTES*/
$(function initPopups() {
  //  $("#" + K_DIV_POPUP.DESCUENTO).dialog({ autoOpen: false, width: 400, height: 280, buttons: { "Agregar": addPlantDesc, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
});

$(function () {


    //*********************************POP UPS *******************
    mvInitBuscarPlantillaDescuento({ container: "ContenedormvBuscarPlantillaDescuento", idButtonToSearch: "btnBuscarPlant", idDivMV: "mvBuscarPlantilla", event: "reloadEvento", idLabelToSearch: "lblPlantilla" });


    //-------------------------------------------------------------
    $("#txtid").attr("disabled", "disabled");
    $("#txtCuenta").attr("disabled", "disabled");
    $("#txtDescripcion").focus();
    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();
    $('#txtValor').on("keypress", function (e) { return solonumeros(e); });
    $("#chkPlantilla").prop("checked", false);
    $("#trBusquedaPlantilla").hide();
    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Descuentos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoDescuento("ddlTipoDescuento", 0);
        $("#lblid").hide();
        $("#txtid").hide();
        loadSigno("ddlSigno");
        loadPorcentaje("ddlPorcentaje");
        loadCuentaContable('ddlCuenta');
    } else {
        $("#divTituloPerfil").html("Descuentos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#ddlTipoDescuento").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#ddlSigno").attr("disabled", "disabled");
        $("#ddlPorcentaje").attr("disabled", "disabled");
        $("#ddlCuenta").attr("disabled", "disabled");
        $("#txtValor").attr("disabled", "disabled");
        $("#chkDescuento").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {

        ObtieneIdTipoDscto(function (retorno) {
            if ($("#ddlTipoDescuento").val() == retorno) {
                alert("El descuento tipo especial no puede ser modificado.");
            } else {
                $("#ddlTipoDescuento").removeAttr('disabled');
                $("#txtDescripcion").removeAttr('disabled');
                $("#ddlSigno").removeAttr('disabled');
                $("#ddlPorcentaje").removeAttr('disabled');
                $("#ddlCuenta").removeAttr('disabled');
                $("#txtValor").removeAttr('disabled');
                $("#chkDescuento").removeAttr('disabled');
                $("#btnEditar").hide();
                $("#btnVolver").show();
                $("#btnGrabar").show();
                $("#btnNuevo").hide();
                $("#btnDescartar").show();
            }
        });
       
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../Descuentos/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTipoDescuento").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#ddlSigno").attr("disabled", "disabled");
        $("#ddlPorcentaje").attr("disabled", "disabled");
        $("#ddlCuenta").attr("disabled", "disabled");
        $("#txtValor").attr("disabled", "disabled");
        $("#chkDescuento").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        if (estadoRequeridos)
            grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../Descuentos/Index";
    }).button();
    //**************************************************************************************************************************************
    //
    $("#ddlTipoDescuento").on("change",function () {
        //12
        if ($(this).val()==12) {
            $("#trBusquedaPlantilla").show();
            
        } else {
            $("hidPlantillaDes").val(0);
            $("#trBusquedaPlantilla").hide();

        }
    });
});

var grabar = function () {
    var tipo = $("#ddlTipoDescuento").val();
    var desc = $("#txtDescripcion").val();
    var signo = $("#ddlSigno").val();
    var porcentaje = $("#ddlPorcentaje").val();
    var monto = $("#txtValor").val();
    var valor1 = "";
    var valor2 = "";
    var cuenta = $("#ddlCuenta").val();
    var estadoDescuento = "N";
    var hidDescPlantilla = $("#hidPlantillaDes").val();

    if ($("#chkDescuento").is(':checked'))
        estadoDescuento = "S";

    if (signo == 0) { signo = "+"; } else { signo = "-" }

    if (porcentaje == 1) { valor1 = monto; } else { valor2 = monto; }

    if (tipo == 0 || desc == "" || signo == 0 || porcentaje == "") {
        alert("Ingrese todos los datos para el registro.");
    }
    else {
        var Descuentos = {
            DISC_ID: $("#txtid").val(),
            DISC_TYPE: tipo,
            DISC_NAME: desc,
            DISC_SIGN: signo,
            DISC_PERC: valor1,
            DISC_VALUE: valor2,
            DISC_ACC: cuenta,
            DISC_AUT: estadoDescuento,
            TEMP_ID_DSC:hidDescPlantilla
        };

        $.ajax({
            url: "../Descuentos/Insertar",
            data: Descuentos,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../Descuentos/";                    
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../Descuentos/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.DISC_ID);
                    loadTipoDescuento("ddlTipoDescuento", tipo.DISC_TYPE);
                    $("#txtDescripcion").val(tipo.DISC_NAME);
                    if (tipo.DISC_SIGN == '+') { loadSigno("ddlSigno", 0); } else { loadSigno("ddlSigno", 1); }

                    //**************Descuento PLANTILLA************************
                    $("#hidPlantillaDes").val(tipo.TEMP_ID_DSC);
                    //alert($("#hidPlantillaDes").val())
                    reloadEvento($("#hidPlantillaDes").val());
                    $("#trBusquedaPlantilla").show();
                    $("#btnBuscarPlant").hide();
                    //*********************************************************

                    var d = loadPorcentaje("ddlPorcentaje");

                    if (tipo.DISC_PERC != "") {
                        loadPorcentaje("ddlPorcentaje", 1);
                        $("#txtValor").val(tipo.DISC_PERC);
                    } else {
                        loadPorcentaje("ddlPorcentaje", 2);
                        $("#txtValor").val(tipo.DISC_VALUE);
                    }
                    loadCuentaContable('ddlCuenta', tipo.DISC_ACC);
                    if (tipo.DISC_AUT == 'S')
                        $("#chkDescuento").prop('checked', true);
                    else
                        $("#chkDescuento").prop('checked', false);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}