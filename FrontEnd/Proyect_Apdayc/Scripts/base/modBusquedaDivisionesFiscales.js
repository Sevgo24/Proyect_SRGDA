/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();
    $("#txtBusqueda").focus();
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
                    url: "../DIVISIONESFISCALES/usp_listar_DivisionesFiscalesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECTAXDIVISION", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
            [
                {
                    title: 'Eliminar', width: 7, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${TAXD_ID}'/>"
                }, {
                    hidden: true,
                    field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                }, {
                   
                    field: "TAXD_ID", width: 5, title: "Id", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}' style='color:5F5F5F;text-decoration:none;'>${TAXD_ID}</a>"
                }, {
                    hidden: true,
                    field: "TIS_N", width: 50, title: "TERRITORIO DIV FISCAL", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}' style='color:5F5F5F;text-decoration:none;'>${TIS_N}</a>"
                }, {
                    field: "DESCRIPTION", width: 60, title: "División Fiscal", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
                }, {
                    field: "NAME_TER", width: 30, title: "Territorio", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}'style='color:5F5F5F;text-decoration:none;'>${NAME_TER}</a>"
                },
                { field: "Activo", width: 8, title: "<font size=2px>Estado </font>", template: "<a  href='../DIVISIONESFISCALES/Edit/${TAXD_ID}'style='color:5F5F5F;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
            ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo división fiscal?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        TAXD_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../DIVISIONESFISCALES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../DIVISIONESFISCALES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});


function limpiar() {
    $("#txtBusqueda").val("");
}