/*INICIO CONSTANTES*/
var K_WIDTH = 450;
var K_HEIGHT = 130;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
   
    loadDerechoTipo('ddlDerecho', 0, 'Seleccione');
    var id = (GetQueryStringParams("set"));
    //var id = (GetQueryStringParams("id"));

    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Clases de Creación - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Clases de Creación - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(id);
        //$(".addDerecho").on("click", function () {$("#mvDerecho").dialog("Close");});
        ObtenerDatos(id);
    }

    //--------------------------POPUP ----------------------------------------------
    $("#mvDerecho").dialog({ autoOpen: false, width: K_WIDTH, height: K_HEIGHT, overflow: false, buttons: { "Agregar": addDerecho, "Cancelar": function () { $("#mvDerecho").dialog("close"); } }, modal: true });
    //$("#mvDerecho").css({ overflow: 'hidden' })

    //$(".addDerecho").on("click", function () {
    //    loadDerechoTipo('ddlDerecho', 0, 'Todos');
    //    $('#ddlDerecho option').remove();
    //    $("#ddlDerecho").prepend("<option selected='selected' value='0'>SELECCIONAR</option>");
    //    $("#ddlDerecho").css('border', '1px solid gray');
    //    $("#mvDerecho").dialog("open");
    //});

    $(".addDerecho").on("click", function () { limpiarDerecho(); $("#mvDerecho").dialog("open"); });

    $("#tabs").tabs();

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnDescartar").hide();
    $("#btnGrabar").hide();
    //$("#ddlDerecho").attr("disabled", "disabled");
    $("#txtDesCorta").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");

    $("#btnEditar").on("click", function () {
        $("#ddlDerecho").removeAttr('disabled');
        $("#txtDescripcion").removeAttr('disabled');
        $("#btnEditar").hide();
        $("#btnGrabar").show();
        $("#btnNuevo").hide();
        $("#btnDescartar").show();
        //$(".addDerecho").on("click", function () { $("#mvDerecho").dialog("open"); });
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../ClaseCreacion/Nuevo";
    }).button();

    $("#btnDescartar").on("click", function () {
        $("#ddlDerecho").attr("disabled", "disabled");
        $("#txtDesCorta").attr("disabled", "disabled");
        $("#txtDescripcion").attr("disabled", "disabled");
        $("#btnNuevo").show();
        $("#btnEditar").show();
        $("#btnGrabar").hide();
        $("#btnDescartar").hide();
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "Index";
    }).button();
});

function limpiarDerecho() {

    $('#ddlDerecho').val(0);
};

var grabar = function () {

    var busq1 = $("#ddlDerecho").val();
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtDescripcion").val();

    if (busq1 == "" || descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var ClaseCreacion = {
            RIGHT_COD: $("#ddlDerecho").val(),
            CLASS_COD: $("#txtDesCorta").val(),
            CLASS_DESC: $("#txtDescripcion").val()
        };

        $.ajax({
            url: "../ClaseCreacion/Actualizar",
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

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../ClaseCreacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtDesCorta").val(tipo.CLASS_COD);
                    $("#txtDescripcion").val(tipo.CLASS_DESC);
                    ObtenerDetalle();
                }

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerDetalle() {
    $.ajax({
        url: "../ClaseCreacion/ListarDerecho",
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridDerecho").append(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
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

//function DellAddDerecho(idDel) {

//    alert(idDel);

//    $.ajax({
//        url: '../ClaseCreacion/DellAddDerecho',
//        type: 'POST',
//        data: { id: idDel },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                loadDataDerecho();
//            }
//        }
//    });
//    return false;
//}
