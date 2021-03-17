/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../ClaseCreacion/Index";
    });

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
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtPDescripcion").val();

    if (descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var ClaseCreacion = {
            CLASS_COD: $("#txtDesCorta").val(),
            CLASS_DESC: $("#txtPDescripcion").val()
        };

        $.ajax({
            url: "../ClaseCreacion/Insertar",
            data: ClaseCreacion,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    location.href = "../ClaseCreacion/Index";
                    alert(dato.message);
                } else {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};