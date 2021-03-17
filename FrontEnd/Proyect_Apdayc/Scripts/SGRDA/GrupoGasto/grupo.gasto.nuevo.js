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
        location.href = "../GrupoGasto/Index";
    }).button();

    loadTipoGasto();
});

function loadTipoGasto() {
    $.ajax({
        url: "../General/ListarTipoGasto",
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlTipoGasto').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                var tipo = $("#ddlTipoGasto").val();
            }
            else {
                alert(dato.message);
            }
        }
    });
}

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
    var tipo = $("#ddlTipoGasto").val();
    var descorta = $("#txtDesCorta").val();
    var desc = $("#txtPDescripcion").val();

    if (tipo == "" || descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var GrupoGasto = {
            EXP_TYPE: tipo,
            EXPG_ID: descorta,
            EXPG_DESC: desc
        };

        $.ajax({
            url: "../GrupoGasto/Insertar",
            data: GrupoGasto,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../GrupoGasto/Index";                    
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};