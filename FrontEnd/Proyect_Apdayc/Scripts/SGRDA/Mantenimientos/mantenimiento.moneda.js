$(function () {
    $("#txtBusqueda").focus();
    $("#btnBusqueda").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });
});

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../MONEDA/usp_listar_MonedaJson",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })            
            }
        },
        schema: { data: "REFCURRENCY", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns: [
            {
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${CUR_ALPHA}'/>"
            },
            {
                field: "CUR_ALPHA", width: 15, title: "Código ISO", template: "<a href='../MONEDA/Edit/${CUR_ALPHA}'style='color:gray;'>${CUR_ALPHA}</a>"
            }, {
                field: "CUR_DESC", width: 80, title: "Moneda", template: "<a  href='../MONEDA/Edit/${CUR_ALPHA}'style='color:gray;'>${CUR_DESC}</a>"
            }, {
                field: "CUR_NUM", width: 60, title: "Código Numérico", template: "<a href='../MONEDA/Edit/${CUR_ALPHA}'style='color:gray;'>${CUR_NUM}</a>"
            }, {
                field: "FORMAT", width: 40, title: "Formato", template: "<a  href='../MONEDA/Edit/${CUR_ALPHA}'style='color:gray;'>${FORMAT}</a>"
            }, {
                field: "ACTIVO", width: 12, title: "Estado", template: "<a href='../MONEDA/Edit/${CUR_ALPHA}' style='color:gray;'>${ACTIVO}</a></font>"
            }
        ]
    });
};

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
    $("#ddlEstado").val(1);
    $("#txtBusqueda").focus();
}