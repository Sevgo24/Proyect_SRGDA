$(function () {
    var accion = "SubirArchivosAjax";
    $("#fArchivo").fileupload({
        datatype: "json",
        url: accion,
        done: function (e, data) {
            $("#validaciones").text(data.result);
        }
    });
});