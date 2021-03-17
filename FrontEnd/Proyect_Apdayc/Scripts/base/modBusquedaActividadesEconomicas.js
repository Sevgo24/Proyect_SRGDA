/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ parametro: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../ACTIVIDADESECONOMICAS/usp_listar_ActividadEconomicaJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECECONACTIVITIES", total: 'TotalVirtual' }
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
              {
                  title: 'Eliminar', width: 5, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${ECON_ID}'/>"
              },
              {
                  field: "ECON_ID", width: 8, title: "Id", template: "<a  href='../ACTIVIDADESECONOMICAS/Edit/${OWNER},${ECON_ID}'style='color:gray;'>${ECON_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 1, title: "PROPIETARIO", template: "<a  href='../ACTIVIDADESECONOMICAS/Edit/${OWNER},${ECON_ID}' style='color:gray;'>${OWNER}</a>"
              },
              {
                  field: "ECON_DESC", width: 30, title: "Descripción", template: "<a  href='../ACTIVIDADESECONOMICAS/Edit/${OWNER},${ECON_ID}'style='color:gray;'>${ECON_DESC}</a>"
              },
              {
                  field: "ECON_BELONGS", width: 50, title: "Actividad Dependencia", template: "<a  href='../ACTIVIDADESECONOMICAS/Edit/${OWNER},${ECON_ID}' style='color:gray;'>${ECON_BELONGS}</a>"
              },
              { field: "ACTIVO", width: 5, title: "Estado", template: "<a  href='../ACTIVIDADESECONOMICAS/Edit/${OWNER},${ECON_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
              ]
    })
}


$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la actividad economica?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        ECON_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../ACTIVIDADESECONOMICAS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../ACTIVIDADESECONOMICAS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
    $("#ddlEstado").val(0);
}
