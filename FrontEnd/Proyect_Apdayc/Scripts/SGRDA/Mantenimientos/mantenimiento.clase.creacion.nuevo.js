/*INICIO CONSTANTES*/
var K_WIDTH = 450;
var K_HEIGHT = 130;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    //--------------------------POPUP ----------------------------------------------
    $("#mvDerecho").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        overflow: false,
        buttons: {
            "Agregar": addDerecho,
            "Cancelar": function () { $("#mvDerecho").dialog("close"); }
        },
        modal: true
    });
    $("#mvDerecho").css({ overflow: 'hidden' })

    $(".addDerecho").on("click", function () {
        loadDerechoTipo('ddlDerecho', 0, 'Todos');
        $('#ddlDerecho option').remove();
        $("#ddlDerecho").prepend("<option selected='selected' value='0'>SELECCIONAR</option>");
        $("#ddlDerecho").css('border', '1px solid gray');
        $("#mvDerecho").dialog("open");
    });
    $("#tabs").tabs();

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../ClaseCreacion/Index";
    }).button();

    loadDerechoTipo('ddlDerecho', 0, 'Seleccione');
});


function limpiar() {

    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");

}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

var grabar = function () {
    var busq1 = $("#ddlDerecho").val();
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtDescripcion").val();

    if (descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var ClaseCreacion = {
            RIGHT_COD: $("#ddlDerecho").val(),
            CLASS_COD: $("#txtDesCorta").val(),
            CLASS_DESC: $("#txtDescripcion").val()
        };

        $.ajax({
            url: "../ClaseCreacion/Insertar",
            data: ClaseCreacion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../ClaseCreacion/Index";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

//-- VALIDACIONES
function validarDereccho() {
    var estado = true;
    if ($("#ddlDerecho").val() === 0) {
        alert("Seleccione una Derecho");
        estado = false;
    } else {
        return estado;
    }
}

function addDerecho() {
    var estadoValidacion = validarDereccho();
    if (estadoValidacion == true) {
        var IdAdd = 0;
        if ($("#hidAccionMvDer").val() === "1") IdAdd = $("#hidEdicionDer").val();

        var derecho = {
            Id: IdAdd,
            IdDerecho: $("#ddlDerecho").val(),
            Derecho: $("#ddlDerecho option:selected").text()
        };
        $.ajax({
            url: '../ClaseCreacion/AddDerecho',
            type: 'POST',
            data: derecho,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDerecho();

                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvDerecho").dialog("close");
    }
}

function loadDataDerecho() {
    loadDataGridTmp('../ClaseCreacion/ListarDerecho', "#gridDerecho");
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

function DellAddDerecho(idDel) {
    $.ajax({
        url: '../ClaseCreacion/DellAddDerecho',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDerecho();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddDerecho(idUpd) {

    $.ajax({
        url: '../ClaseCreacion/ObtieneDerechoTmp',
        data: { idDer: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDer").val("1");
                    $("#hidEdicionDer").val(doc.Id);
                    loadDerechoTipo('ddlDerecho', doc.IdDerecho);
                    $("#mvDerecho").dialog("open");
                } else {
                    alert("No se pudo obtener el Derecho para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}