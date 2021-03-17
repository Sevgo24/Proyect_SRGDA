var busq;
var estado;
var tot;

$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () {
        //var grid = $("#grid").data("kendoGrid");
        //grid.dataSource.page(1);
        //grid.dataSource.read();
        //grid.refresh();

        //var grid = $("#grid").data("kendoGrid");
        //grid.refresh();        
        $('#grid').data('kendoGrid').dataSource.read({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() });
        //var currentPage = $('#grid').data('kendoGrid').dataSource.page();
        //$('#grid').data('kendoGrid').refresh();
        //alert(currentPage);

        //alert($('#grid').data('kendoGrid').dataSource.page());

        //$('#grid').data('kendoGrid').dataSource.query({ page: 1, pageSize: 3 });
        //$('#grid').data('kendoGrid').dataSource.page(1);
        //$('#grid').data('kendoGrid').dataSource.read();
        //alerta();
        //var grid = $("#grid").data("kendoGrid");
        //grid.refresh();

        //var vgrid = $("#grid").data("kendoGrid");
        //var datasource = vgrid.dataSource;
        //var newSource = { "dato": $("#txtBusqueda").val(), "st": $("#ddlEstado").val() };
        //Applying new source
        //datasource.data(newSource);
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        $("#txtBusqueda").focus();
        limpiar();
        var grid = $("#grid").data("kendoGrid");
        grid.dataSource.page(1);
    });

    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            loadData();
        }
    });
});




//function onDataBound(e) {
//    var grid = $("#grid").data("kendoGrid");
//    $(grid.tbody).on("click", "td", function (e) {
//        //var row = $(this).closest("tr");
//        //var rowIdx = $("tr", grid.tbody).index(row);
//        //var colIdx = $("td", row).index(this);
//        var pageIdx = grid.dataSource.page();
//        alert(pageIdx);
//        alert($("#txtBusqueda").val() + '' + $("#ddlEstado").val());
//        $('#grid').data('kendoGrid').dataSource.read({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() });
//    });
//}

function onDataBound(e) {
    var grid = $("#grid").data("kendoGrid");
    if ($("#txtBusqueda").val() != "") {
        e.dato = "dolar";
    }
    alert(grid.dataSource.page());
    //alert($("#txtBusqueda").val() + '  ' + $("#ddlEstado").val());
    $('#grid').data('kendoGrid').dataSource.sync();
    //$('#grid').data('kendoGrid').dataSource.read({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() });
}


function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        autoSync: true,
        serverPaging: true,
        pageSize: 2,
        type: "json",
        transport: {
            read: {
                url: "../MONEDA/usp_listar_MonedaJson",
                dataType: "json",
                type: "Post",
                cache: true,
                data: { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() }
            }
        },
        schema: { data: "REFCURRENCY", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({

        dataSource: sharedDataSource,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            },
            previousNext: true
        },
        selectable: "multiple row",

        dataBound: onDataBound,
        columns: [
            {
                title: 'Eliminar', width: 15, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${CUR_ALPHA}'/>"
            },
            {
                field: "CUR_ALPHA",
                title: "Código",
                width: 15
            }, {
                field: "CUR_DESC",
                title: "Moneda",
                width: 80
            }, {
                field: "CUR_NUM",
                title: "Código Numérico",
                width: 30
            }, {
                field: "FORMAT",
                title: "Formato",
                width: 30
            }, {
                field: "Activo",
                title: "Estado",
                width: 18
            }
        ]
    });
    searchResultsInitialised = true;
};

//function alerta() {
//    alert(JSON.stringify(sharedDataSource));
//    //sharedDataSource.add({ dato: "", st: 0 });
//    sharedDataSource.sync();
//    alert(JSON.stringify(sharedDataSource));
//    Item = kendo.data.Model.define({
//        id: "ID"
//    });
//}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la moneda?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        CUR_ALPHA: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../MONEDA/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../MONEDA/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
    busq = "";
    estado = "";
    tot = "";
}
