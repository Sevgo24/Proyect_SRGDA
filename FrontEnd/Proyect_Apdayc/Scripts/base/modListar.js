$(function () {
    $("#btnBuscar").click(function () {

        var $listado = $("#listado");
        $listado.empty();

        $.getJSON("/ManProductos/ProductoListarJson", {},
            function (resultado) {

                var datos = '';
                for (var i = 0; i < resultado.length; i++)
                    datos = datos + "<li>" + resultado[i].Descripcion + "</li>";

            $listado.append(datos);
        });
    });
});