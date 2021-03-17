$(function () {
    $("#tabs").tabs();
    //ObtenerDatos(1, 2);
    //loadDataDescuentos();
    var id = GetQueryStringParams("idLic");
    $("#hidLicId").val(id);

    var idLic = $("#hidLicId").val();
    loadDataDescuentos(idLic);
    loadDataEntidades(idLic)
    loadDataAuditoria(idLic)
});

//COMENTARLO SI BOTA ERROR 
//function loadDataDescuentos() {
//    loadDataGridTmp('ListarDescuentos', "#gridDescuento");
//} 

//function loadDataGridTmp(Controller, idGrilla) {
//    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
//}

//function ObtenerDatos(idSel, idRate, idLic) {
//    $("#hidOpcionEdit").val(1);
//    $.ajax({
//        url: '../Descuento/Obtiene',
//        data: { idUsuDerecho: idSel, idTarifa: idRate, idLicencia: idLic },
//        type: 'GET',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                var datos = dato.data.Data;
//                if (datos != null) {
//                    //alert("OK");
//                }
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status);
//            alert(thrownError);
//        }
//    });
//}