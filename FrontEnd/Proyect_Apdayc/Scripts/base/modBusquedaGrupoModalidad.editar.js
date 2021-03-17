/*INICIO CONSTANTES*/
//var K_WIDTH = 350;
var K_WIDTH =450;
//var K_HEIGHT = 240;
var K_HEIGHT = 140;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();

    var id = GetQueryStringParams("set");

    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Grupos de Modalidades de Uso - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
    } else {
        $("#divTituloPerfil").html("Grupos de Modalidades de Uso - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#txtid").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#ddlFormato").attr("disabled", "disabled");
        ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    //--------------------------POPUP ----------------------------------------------

    $("#mvFormato").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        overflow: false,
        buttons: {
            "Agregar": addFormato,
            "Cancelar": function () { $("#mvFormato").dialog("close"); }
        },
        modal: true
    });
    $("#mvFormato").css({ overflow: 'hidden' })

    $(".addFormato").on("click", function () {
        $("#hidAccionMvCar").val("0");
        $("#hidEdicionCar").val('0');
        loadFormatoFacturacion('ddlFormato', 0);
        $('#ddlFormato option').remove();
        $("#ddlFormato").prepend("<option selected='selected' value='0'>SELECCIONAR</option>");
        $("#ddlFormato").css('border', '1px solid gray');
        $("#mvFormato").dialog("open");
    });

    $("#tabs").tabs();

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnEditar").on("click", function () {
        $("#txtDescripcion").removeAttr('disabled');
        $("#ddlFormato").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        //$("#txtid").val("");
        //$("#txtid").removeAttr('disabled');
        //$("#txtDescripcion").val("");
        //$("#txtDescripcion").removeAttr('disabled');
        //$("#ddlFormato").val(0);
        //$("#ddlFormato").removeAttr('disabled');
        //$("#btnGrabar").show();
        //$("#btnEditar").hide();
        //$("#btnNuevo").hide();
        //$("#btnVolver").show();
        location.href = "../GRUPOMODALIDAD/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        $("#txtid").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#ddlFormato").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
        $('#aviso').html('');
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../GRUPOMODALIDAD/Index";
    });
    //-------------------------- CARGAR GRILLA DE FORMATOS DE FACTURAS --------------------------------------------
    loadDataFormato();
});

function loadDataFormato() {
    loadDataGridTmp('../GRUPOMODALIDAD/ListarFormato', "#gridFormato");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        }
    });
}

//-- VALIDACIONES
function validarFormato() {
    var estado = true;
    if ($("#ddlFormato").val() === 0) {
        alert("Seleccione un Formato");
        estado = false;
    } else {
        return estado;
    }
}

var grabar = function () {
    var Modalidad = {
        MOG_ID: $("#txtid").val(),
        MOG_DESC: $("#txtDescripcion").val(),
        INVF_ID: $("#ddlFormato").val()
    };

    $.ajax({
        url: "../GRUPOMODALIDAD/Actualizar",
        data: Modalidad,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../GRUPOMODALIDAD/Index";
            } else {
                alert(dato.message);
            }
        }
    });
    return false;
};

function addFormato() {
    var idFormatoAnt = 0;
    var estadoValidacion = validarFormato();
    if (estadoValidacion == true) {
        var IdAdd = 0;
        if ($("#hidAccionMvCar").val() === "0") //Nuevo
            idFormatoAnt = 0;
        else
            idFormatoAnt = $("#hidEdicionCar").val();

        var formato = {
            Id: IdAdd,
            IdFormato: $("#ddlFormato").val(),
            IdFormatoAnt: idFormatoAnt,
            Formato: $("#ddlFormato option:selected").text(),
            IdGrupo: $("#txtid").val(),
            Grupo: $("#txtDescripcion").val()
        };
        $.ajax({
            url: '../GRUPOMODALIDAD/AddFormato',
            type: 'POST',
            data: formato,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataFormato();

                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvFormato").dialog("close");
    }
}

function updAddFormato(idUpd) {

    $.ajax({
        url: '../GRUPOMODALIDAD/ObtieneFormatoTmp',
        data: { idForm: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvCar").val("1");
                    $("#hidEdicionCar").val(doc.IdFormato);
                    loadFormatoFacturacion('ddlFormato', doc.IdFormato);
                    $("#mvFormato").dialog("open");
                } else {
                    alert("No se pudo obtener el formato para editar.");
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

function DellAddFormato(idDel) {
    $.ajax({
        url: '../GRUPOMODALIDAD/DellAddFormato',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadDataFormato();
            }
        }
    });
    return false;
}

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../GRUPOMODALIDAD/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.MOG_ID);
                    $("#txtDescripcion").val(tipo.MOG_DESC);
                }
                loadDataFormato();
            } else {
                alert(dato.message);
            }
        }
    });
}