/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;

$(function () {
    $("#txtBusqueda").focus();
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
        limpiar();
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
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../MOTIVOSDEVOLUCION/usp_listar_MotivodevolucionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECRETURNREASONS", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${RET_ID}'/>"
            },
            {
                field: "RET_ID", width: 13, title: "Id", template: "<a  href='../MOTIVOSDEVOLUCION/Edit/${OWNER},${RET_ID}'style='color:gray;text-decoration:none;'>${RET_ID}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 50, title: "Propietario", template: "<a  href='../MOTIVOSDEVOLUCION/Edit/${OWNER},${RET_ID}' style='color:gray;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "RET_DES", width: 150, title: "Descripción", template: "<a  href='../MOTIVOSDEVOLUCION/Edit/${OWNER},${RET_ID}'style='color:gray;text-decoration:none;'>${RET_DES}</a>"
            },
            { field: "Activo", width: 12, title: "Estado", template: "<a  href='../MOTIVOSDEVOLUCION/Edit/${OWNER},${RET_ID}' style='color:gray;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
            ]
    })
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el motivo de devolución?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        RET_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../MOTIVOSDEVOLUCION/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../MOTIVOSDEVOLUCION/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}
