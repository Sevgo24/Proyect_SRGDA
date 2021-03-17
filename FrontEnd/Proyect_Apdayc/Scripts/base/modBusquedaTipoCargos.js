/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

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

    $("#txtBusqueda").focus();
    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadData();

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
                    url: "../TIPOCARGOS/usp_listar_TipocargosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "REFROLES", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 8, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${ROL_ID}'/>"
              },
              {
                  field: "ROL_ID", width: 5, title: "Id", template: "<a  href='../TIPOCARGOS/Edit/${OWNER},${ROL_ID}'style='color:gray;'>${ROL_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 1, title: "Propietario", template: "<a  href='../TIPOCARGOS/Edit/${OWNER},${ROL_ID}' style='color:gray;'>${OWNER}</a>"
              },
              {
                  field: "ROL_DESC", width: 80, title: "Descripción", template: "<a  href='../TIPOCARGOS/Edit/${OWNER},${ROL_ID}'style='color:gray;'>${ROL_DESC}</a>"
              },
              { field: "ACTIVO", width: 12, title: "Estado", template: "<a  href='../TIPOCARGOS/Edit/${OWNER},${ROL_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
              ]
    })
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo de cargo?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        ROL_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOCARGOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOCARGOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
function limpiar() {
    $("#txtBusqueda").val("");
}
