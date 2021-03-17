/*INICIO CONSTANTES*/
var K_WIDTH = 380;
var K_HEIGHT = 130;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#txtid").attr("disabled", "disabled");
    $("#btnDescartar").hide();
    $("#btnGrabar").show();
    $("#btnEditar").hide();
    $("#btnNuevo").hide();
    $("#btnVolver").show();
    $("#trId").hide();
    //var id = GetQueryStringParams("set");
    var idEst = GetQueryStringParams("idEst");
    var idSub = GetQueryStringParams("idSub");
    var idCar = GetQueryStringParams("idCar");
    //---------------------------------------------------------------
    if (idEst === undefined && idSub === undefined && idCar === undefined) {
        $("#divTituloPerfil").html("Características Predefinidas - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#trId").hide();
        loadTipoestablecimiento('ddlTipo', 0);
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', 0);
        loadCaracteristica(0);
        loadDataCaracteristica();
    } else {
        $("#divTituloPerfil").html("Características Predefinidas - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(idCar);
        $("#btnEditar").show();
        $("#btnNuevo").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $(".addCaracteristica").tabs({ disabled: [0] });
        $("#ddlTipo").attr("disabled", "disabled");
        $("#ddlSubtipoestablecimiento").attr("disabled", "disabled");
        ObtenerDatos(idEst, idSub, idCar);
    }
    //---------------------------------------------------------------

    //--------------------------POPUP ----------------------------------------------
    $("#mvCaracteristica").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        overflow: false,
        buttons: {
            "Agregar": addCaracteristica,
            "Cancelar": function () { $("#mvCaracteristica").dialog("close"); }
        },
        modal: true
    });
    $("#mvCaracteristica").css({ overflow: 'hidden' })

    $(".addCaracteristica").on("click", function () {
        var tipo = $('#ddlCaracteristica').val();
        var sub = $('#ddlSubtipoestablecimiento').val();
        if (tipo == 0 && sub == 0) {
            alert("Seleccione un Tipo o SubTipo de Establecimiento");
        } else {
            loadCaracteristica(0);
            $('#ddlCaracteristica option').remove();
            $("#ddlCaracteristica").prepend("<option selected='selected' value='0'>-- SELECCIONAR --</option>");
            $("#ddlCaracteristica").css('border', '1px solid gray');
            $("#mvCaracteristica").dialog("open");
        }
    });
    $("#tabs").tabs();

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnEditar").on("click", function () {
        $("#ddlTipo").removeAttr('disabled');
        $("#ddlSubtipoestablecimiento").removeAttr('disabled');
        $("#tabs").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnVolver").show();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
    });

    $("#btnNuevo").on("click", function () {
        //$("#ddlTipo").val(0);
        //$("#ddlTipo").removeAttr('disabled');
        //$("#ddlSubtipoestablecimiento").val(0);
        //$("#ddlSubtipoestablecimiento").removeAttr('disabled');
        //$("#btnGrabar").show();
        //$("#btnEditar").hide();
        //$("#btnNuevo").hide();
        //$("#btnVolver").show();
        location.href = "../CaracteristicaPredefinida/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlTipo").attr("disabled", "disabled");
        $("#ddlSubtipoestablecimiento").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnVolver").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
        $('#aviso').html('');
    }).button();

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        if (estadoRequeridos)
            grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../CaracteristicaPredefinida/Index";
    }).button();

    $("#ddlTipo").on("change", function () {
        var codigo = $("#ddlTipo").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo);
    });
});

//-------------------------------------  FUNCIONES  ------------------------------------------------
var grabar = function () {
    var Caracteristica = {
        CHAR_TYPES_ID: $("#txtid").val(),
        EST_ID: $("#ddlTipo").val(),
        SUBE_ID: $("#ddlSubtipoestablecimiento").val(),
        CHAR_ID: $("#ddlCaracteristica").val()
    };

    $.ajax({
        url: "../CaracteristicaPredefinida/Insertar",
        data: Caracteristica,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../CaracteristicaPredefinida/Index";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function ObtenerDatos(idEst, idSub, idCar) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../CaracteristicaPredefinida/Obtiene",
        type: 'POST',
        data: { idTipoEst: idEst, idSubTipoEst: idSub, idCar: idCar },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtid").val(tipo.CHAR_TYPES_ID);
                    loadTipoestablecimiento('ddlTipo', tipo.EST_ID);
                    loadSubTipoestablecimiento(tipo.SUBE_ID);
                    loadDataCaracteristica();
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-- VALIDACIONES
function validarCaracteristica() {
    var estado = true;
    var id = $("#ddlCaracteristica").val();
    if (id == 0) {
        alert("Seleccione una Característica");
        estado = false;
    }
    return estado;
}

function addCaracteristica() {
    var estadoValidacion = validarCaracteristica();
    if (estadoValidacion) {
        var IdAdd = 0;
        if ($("#hidAccionMvCar").val() === "1") IdAdd = $("#hidEdicionCar").val();

        var caracteristica = {
            Id: IdAdd,
            IdCaracteristica: $("#ddlCaracteristica").val(),
            caracteristica: $("#ddlCaracteristica option:selected").text(),
            IdEstablecimiento: $("#ddlTipo").val(),
            TipoEstablecimiento: $("#ddlTipo option:selected").text(),
            IdSubTipoEstablecimiento: $("#ddlSubtipoestablecimiento").val(),
            SubTipoEstablecimiento: $("#ddlSubtipoestablecimiento option:selected").text()
        };
        $.ajax({
            url: '../CaracteristicaPredefinida/AddCaracteristica',
            type: 'POST',
            data: caracteristica,
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
    }
}

function loadDataCaracteristica() {
    loadDataGridTmp('../CaracteristicaPredefinida/ListarCaracteristica', "#gridCaracteristica");
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

function updAddCaracteristica(idUpd) {
    $.ajax({
        url: '../CaracteristicaPredefinida/ObtieneCaracteristicaTmp',
        data: { idCarac: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvCar").val("1");
                    $("#hidEdicionCar").val(doc.Id);
                    loadCaracteristica(doc.Idcaracteristica);
                    $("#mvCaracteristica").dialog("open");
                } else {
                    alert("No se pudo obtener la característica para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function DellAddCaracteristica(idDel) {
    $.ajax({
        url: '../CaracteristicaPredefinida/DellAddCaracteristica',
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