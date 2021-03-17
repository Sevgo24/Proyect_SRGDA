
$(function () {
    $("#tabs").tabs();
    loaddatasucursales();
    loadcontactos();
});

function loaddatasucursales() {
    $("#gridsucursales").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 20,
            transport: {
                read: {
                    type: "POST",
                    url: "/BANCOS/usp_listar_SucursalesXbancosjson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { id: $("#BNK_ID").val() })
                }
            },
            schema: { data: "RECBANKSBRANCH", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns:
              [
              { field: "BRCH_NAME", width: 100, title: "Nombre" },
              { field: "ADDRESS", width: 150, title: "Dirección" }
              ]
    })
}

function loadcontactos() {
    $("#gridcontactos").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 20,
            transport: {
                read: {
                    type: "POST",
                    url: "/BANCOS/usp_listar_ContactosXbancosjson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { id: $("#BNK_ID").val() })
                }
            },
            schema: { data: "RECBANKSBPS", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns:
              [
              { field: "TAXN_NAME", width: 30, title: "Documento" },
              { field: "TAX_ID", width: 60, title: "Número" },
              { field: "BPS_NAME", width: 150, title: "Nombre" },
              { field: "ROL_DESC", width: 60, title: "Rol" }
              ]
    })
}