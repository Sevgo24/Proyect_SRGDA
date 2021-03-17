/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnVolver").on("click", function () {
        location.href = "../TipoGasto/";
    }).button();

});


function limpiar() {

    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");

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
        var TipoGasto = {
            EXP_TYPE: $("#txtDesCorta").val(),
            EXPT_DESC: $("#txtPDescripcion").val()
        };

        $.ajax({
            url: "../TipoGasto/Insertar",
            data: TipoGasto,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../TipoGasto/";                    
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

