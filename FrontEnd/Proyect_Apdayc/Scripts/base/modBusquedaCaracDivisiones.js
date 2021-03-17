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
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").focus();
    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
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
                    url: "../CARACTERISTICASDIVISIONES/usp_listar_TipoCaracDivisionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "REFDIVCHARAC", total: 'TotalVirtual' }
        },
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
                    title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DAC_ID}'/>"
                }, {
                    hidden: true,
                    field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../CARACTERISTICASDIVISIONES/Edit/${DAC_ID}'style='color:gray !important;'>${OWNER}</a>"
                }, {
                    
                    field: "DAC_ID", width: 8, title: "Id", template: "<a  href='../CARACTERISTICASDIVISIONES/Edit/${DAC_ID}' style='color:gray !important;'>${DAC_ID}</a>"
                }, {
                    field: "DESCRIPTION", width: 100, title: "Descripción", template: "<a  href='../CARACTERISTICASDIVISIONES/Edit/${DAC_ID}'style='color:gray !important;'>${DESCRIPTION}</a>"
                },
                { field: "ACTIVO", width: 10, title: "Estado", template: "<a  href='../CARACTERISTICASDIVISIONES/Edit/${DAC_ID}'style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
            ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo de Caracteristica division?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        DAC_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../CARACTERISTICASDIVISIONES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../CARACTERISTICASDIVISIONES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});


function limpiar() {
    $("#txtBusqueda").val("");
}