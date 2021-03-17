
$(function () {
    loadTipoDivisiones('ddlDivision', 0);


    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Divisiones/Nuevo';
    }).button();

    $("#btnDescartar").on("click", function () {
        document.location.href = '../Divisiones/';
    }).button();

});

//****************************  FUNCIONES ****************************
function grabar() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        insertar();
    }
}

function insertar() {

    var corta = $("#txtCorta").val();
    var nombre = $("#txtSubdivision").val();
    var tipo = $("#ddlDivision").val();
    var nombrelong = $("#txtDescripcion").val();
    $.ajax({
        url: '../Divisiones/Insertar',
        data: { code: corta, name: nombre, type: tipo, descripcion: nombrelong },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Divisiones/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}