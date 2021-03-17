/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        var estadoDescripcion = validarInsertar();
        if (estadoRequeridos && estadoDescripcion) {
            grabar();
        }
    }).button();

    $("#ddlTipoGasto").on("change", function () {
        $('#ddlGrupoGasto option').remove();
        var tipo = $("#ddlTipoGasto").val();
        loadGrupoGasto('ddlGrupoGasto', tipo);
    });

    $("#btnVolver").on("click", function () {
        location.href = "../DefinicionGasto/Index";
    }).button();
    $('#ddlGrupoGasto').append($("<option />", { value: 0, text: '--Seleccione--' }));
    loadTipoGasto('ddlTipoGasto', 0);
});

function validarInsertar() {
    var estado = false;
    var id = '0';
    id = $("#txtDesCorta").val();

    var en = {
        EXP_ID: id,
        EXP_DESC: $("#txtDescripcion").val()
    };
    
    $.ajax({
        url: '../DefinicionGasto/ObtenerDatosValidar',
        type: 'POST',
        dataType: 'JSON',
        data: en,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 0) {
                estado = false;
                if (dato.message != null)
                    alert(dato.message);
            } else {
                estado = true;
            }
        }
    });
    return estado;
}

var grabar = function () {

    var DefinicionGasto = {
        EXP_TYPE: $("#ddlTipoGasto").val(),
        EXPG_ID: $("#ddlGrupoGasto").val(),
        EXP_ID: $("#txtDesCorta").val(),
        EXP_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: "../DefinicionGasto/Insertar",
        data: DefinicionGasto,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../DefinicionGasto/Index";                
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    //}
    return false;
};