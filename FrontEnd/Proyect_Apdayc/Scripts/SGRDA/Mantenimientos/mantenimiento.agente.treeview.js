


$(function () {

    loadTreeview("treeview");

});

function loadTreeview(tr) {
    $.ajax({

        cache: false,
        url: '../AGENTE/getTreeview',
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