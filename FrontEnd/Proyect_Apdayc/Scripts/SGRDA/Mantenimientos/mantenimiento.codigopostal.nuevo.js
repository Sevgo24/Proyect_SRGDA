/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvCodigoPostal";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#txtCodigo").focus();
    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();

    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Código Postal - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Código Postal - Actualizacion");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#ddlTerritorio").attr("disabled", "disabled");
        $("#txtCodigo").attr("disabled", "disabled");
        $("#txtPostal").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () {
        $("#ddlTerritorio").removeAttr('disabled');
        $("#txtCodigo").removeAttr('disabled');
        $("#txtPostal").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    }).button();

    $("#btnNuevo").on("click", function () {
        loadComboTerritorio(0);
        $("#ddlTerritorio").removeAttr('disabled');
        $("#txtCodigo").val("");
        $("#txtCodigo").removeAttr('disabled');
        $("#txtPostal").val("");
        $("#txtPostal").removeAttr('disabled');
        $("#btnGrabar").show();
        $("#btnEditar").hide();
        $("#btnNuevo").hide();
        $("#btnVolver").show();
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTerritorio").attr("disabled", "disabled");
        $("#txtCodigo").attr("disabled", "disabled");
        $("#txtPostal").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../CodigoPostal/Index";
    }).button();

    loadComboTerritorio(0);
    initAutoCompletarUbigeo("txtCodigo", "hidCodigoBPS");
});

var grabar = function () {

    var ubg = $("#hidCodigoBPS").val();
    var cod = $("#txtPostal").val();

    if (ubg == 0 || cod == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var CodigoPostal = {
            CPO_ID: $("#txtid").val(),
            TIS_N: $("#hidCodigoBPS").val(),
            POSITIONS: $("#txtPostal").val()
        };

        $.ajax({
            url: "../CodigoPostal/Insertar",
            data: CodigoPostal,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../CodigoPostal/Index";
                    alert(dato.message);
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
        url: "../CodigoPostal/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.CPO_ID);
                    loadComboTerritorio(0);
                    $("#txtCodigo").val(tipo.DescripcionUbigeo);
                    $("#txtPostal").val(tipo.POSITIONS);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}