

/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    var tipo = (GetQueryStringParams("tipo"));
    if (tipo == 1) {
        $('#titulo').html("Árbol de Subdivisiones");
    } else {
        $('#titulo').html("Árbol de Valores de Subdivisiones");
    }

    loadTreeview("treeview",id,tipo);

   
});

//****************************  FUNCIONES ****************************
function loadTreeview(tr,idSub,tipoSub) {
    $.ajax({
        cache: false,
        url: '../Divisiones/getTreeview',
        data: { id: idSub, tipo: tipoSub },
        dataType: "JSON",
        type: 'GET',
        success: function (response) {
            var dato = response;

            $("#" + tr).kendoTreeView({
                dataSource: [
                       dato
                ]
            });
            var tree = $("#" + tr).data("kendoTreeView")
            tree.expand(".k-item");
            tree.dataSource.sort({ field: "text" });
        }

    });
}

