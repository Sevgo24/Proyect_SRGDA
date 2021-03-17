

/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------------------------------------------------------------
    loadTreeview("treeview");

    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnImprimir").on("click", function (e) {
        printPage();
    });

});

//****************************  FUNCIONES ****************************
function loadTreeview(tr) {
    $.ajax({
        cache: false,
        url: '../OFFICE/getTreeview',
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

function printPage() {
    $("#btnImprimir").hide();
    window.print();
    if (window.stop) {
        window.stop();
        $("#btnImprimir").show();
    }
    return false;
}
