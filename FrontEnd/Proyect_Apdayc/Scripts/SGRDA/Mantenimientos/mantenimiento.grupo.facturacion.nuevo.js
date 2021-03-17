/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    
    var id = GetQueryStringParams("set");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Grupos de Facturación de la Licencia - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        ControlesNuevo();

    } else {
        $("#divTituloPerfil").html("Grupos de Facturación de la Licencia  - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        ControlesDescartar();
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#btnEditar").on("click", function () { ControlesEditar(); }).button();

    $("#btnNuevo").on("click", function () { ControlesNuevo(); }).button();

    $("#btnDescartar").on("click", function () { ControlesDescartar(); }).button();

    $("#btnGrabar").on("click", function () { grabar(); }).button();

    $("#btnVolver").on("click", function () { location.href = "../GrupoFacturacion/Index"; }).button();

    //initAutoCompletarRazonSocial("txtUsuario", "hidCodigoBPS");

    //--------------------------BUSQUEDA GENERAL  -----------------------------------------------
    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento",
        idLabelToSearch: "lbResponsable"
    });
});

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
                $("#txtUsuario").val(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

var grabar = function () {

    var user = $("#txtUsuario").val();
    var idgrup = $("#ddlGrupoModalidad").val();
    var grupo = $("#txtGrupoFacturacion").val();

    if (user == "" || idgrup == "0" || grupo == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var GrupoFacturacion = {
            INVG_ID: $("#txtid").val(),
            BPS_ID: $("#hidCodigoBPS").val(),
            MOG_ID: $("#ddlGrupoModalidad").val(),
            INVG_DESC: $("#txtGrupoFacturacion").val()
        };
        $.ajax({
            url: "../GrupoFacturacion/Insertar",
            data: GrupoFacturacion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../GrupoFacturacion/Index";
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
        url: "../GrupoFacturacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.INVG_ID);
                    $("#hidCodigoBPS").val(tipo.BPS_ID);
                    $("#txtUsuario").val(tipo.BPS_NAME);
                    loadTipoGrupo('ddlGrupoModalidad', tipo.MOG_ID);
                    $("#txtGrupoFacturacion").val(tipo.INVG_DESC);
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

var ControlesNuevo = function () {
    $("#txtUsuario").val("");
    $("#txtUsuario").removeAttr('disabled');
    loadTipoGrupo('ddlGrupoModalidad', '0');
    $("#ddlGrupoModalidad").removeAttr('disabled');
    $("#txtGrupoFacturacion").val("");
    $("#txtGrupoFacturacion").removeAttr('disabled');
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnDescartar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();
    $("#btnBuscarBS").show();
    $("#lbResponsable").hide();
}

var ControlesEditar = function () {
    $("#txtUsuario").removeAttr('disabled');
    $("#ddlGrupoModalidad").removeAttr('disabled');
    $("#txtGrupoFacturacion").removeAttr('disabled');
    $("#btnEditar").hide();
    $("#btnVolver").show();
    $("#btnGrabar").show();
    $("#btnNuevo").hide();
    $("#btnDescartar").show();
    $("#btnBuscarBS").show();
    $("#lbResponsable").hide();
}

var ControlesDescartar = function () {
    $("#txtUsuario").attr("disabled", "disabled");
    $("#ddlGrupoModalidad").attr("disabled", "disabled");
    $("#txtGrupoFacturacion").attr("disabled", "disabled");
    $("#btnNuevo").show();
    $("#btnEditar").show();
    $("#btnVolver").show();
    $("#btnGrabar").hide();
    $("#btnDescartar").hide();
    $("#btnBuscarBS").hide();
    $("#lbResponsable").hide();
}