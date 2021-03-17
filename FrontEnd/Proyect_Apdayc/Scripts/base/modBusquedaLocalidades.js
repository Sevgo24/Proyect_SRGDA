$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        $("#txtBusqueda").focus();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#txtBusqueda").focus();
});

function loadData() {
    $("#txtBusqueda").focus();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../LOCALIDADES/usp_listar_LocalidadesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECSECTIONS", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns:
            [
            {
                title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${SEC_ID}'/>"
            },
            {
                field: "SEC_ID", width: 7, title: "Id", template: "<a  href='../LOCALIDADES/Edit/${OWNER},${SEC_ID}'style='color:gray;text-decoration:none;'>${SEC_ID}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 50, title: "Propietario", template: "<a  href='../LOCALIDADES/Edit/${OWNER},${SEC_ID}' style='color:gray;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "SEC_DESC", width: 100, title: "Descripción", template: "<a  href='../LOCALIDADES/Edit/${OWNER},${SEC_ID}'style='color:gray;text-decoration:none;'>${SEC_DESC}</a>"
            },
            { field: "Activo", width: 10, title: "Estado", template: "<a  href='../LOCALIDADES/Edit/${OWNER},${SEC_ID}' style='color:gray;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
            ]
    })
};

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la localidad?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        SEC_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../LOCALIDADES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../LOCALIDADES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});


function limpiar() {
    $("#txtBusqueda").val("");
}