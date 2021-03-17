$(function () {

    var LIC_ID = (GetQueryStringParams("id"));
    //var LIC_NAME = (GetQueryStringParams("LIC_NAME"));
    Empadronamiento_Detalle(LIC_ID);
   $('#LIC_ID').html(LIC_ID);
});

function Empadronamiento_Detalle(LIC_ID) {
    $.ajax({
        url: '../DetalleEmpadronamiento/ObtenerEmpadronamientoDetalle',
        type: 'POST',
        data: {
            LIC_ID: LIC_ID
        },
        beforeSend: function () { }, 
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#LIC_NAME').html(dato.valor);                 
                $("#gridDetalle").html(dato.message);
                //CD_Licencia(LIC_ID);
                //$("#CantidadRegistros").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#gridDetalle").html('');
                //$("#CantidadRegistros").html("");
                alert(dato.message);
            }
        }
    });

}