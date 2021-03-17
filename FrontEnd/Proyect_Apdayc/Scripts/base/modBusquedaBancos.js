$(function () {
    $("#btnBusqueda").on("click", function (){
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ parametro: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
    $("#txtBusqueda").focus();
});

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../BANCOS/usp_listar_bancosjson",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
            }
        },
        schema: { data: "REC_BA", total: 'TotalVirtual' }
    });

    $("#listado").kendoGrid({
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
        columns:
        [
            {
                title: 'Eliminar', width: 8, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BNK_ID}'/>"
            },
            {
                hidden: true,
                field: "OWNER", width: 80, title: "PROPIETARIO", template: "<a  href='../BANCOS/Edit/${BNK_ID}'style='color:gray;'>${OWNER}</a>"
            },
            {
                field: "BNK_ID", width: 7, title: "Id", template: "<a  href='../BANCOS/Edit/${BNK_ID}'style='color:gray;'>${BNK_ID}</a>"
            },
            {
                field: "BNK_NAME", width: 70, title: "Nombre", template: "<a  href='../BANCOS/Edit/${BNK_ID}' style='color:gray;'>${BNK_NAME}</a>"
            },
            {
                hidden: true,
                field: "BNK_C_BRANCH", width: 30, title: "Sucursal", template: "<a  href='../BANCOS/Edit/${BNK_ID}'style='color:gray;'>${BNK_C_BRANCH}</a>"
            },
            {
                hidden: true,
                field: "BNK_C_DC", width: 30, title: "Longitud control", template: "<a  href='../BANCOS/Edit/${BNK_ID}' style='color:gray;' >${BNK_C_DC}</a>"
            },
            {
                hidden: true,
                field: "BNK_C_ACCOUNT", width: 30, title: "Cuenta", template: "<a  href='../BANCOS/Edit/${BNK_ID}' style='color:gray;' >${BNK_C_ACCOUNT}</a>"
            },
            { field: "ACTIVO", width: 10, title: "Estado", template: "<a  href='../BANCOS/Edit/${BNK_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });
};

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar banco?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        BNK_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../BANCOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../BANCOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
    $("#ddlEstado").val(1);
    $("#txtBusqueda").focu();
}



