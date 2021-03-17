$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $("#txtBusqueda").focus();

    $(".alert-link").hide();
    $("#message").hide();
    
    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), TipoEst: $("#ddlTipoestablecimiento").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), TipoEst: $("#ddlTipoestablecimiento").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), TipoEst: $("#ddlTipoestablecimiento").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });
    loadTipoestablecimiento('ddlTipoestablecimiento', 0);
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
                    url: "../SUBTIPOSESTABLECIMIENTOS/usp_listar_SubTipoEstablecimientosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), TipoEst: $("#ddlTipoestablecimiento").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECESTSUBTYPE", total: 'TotalVirtual' }
        },
        sortable: false,
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${SUBE_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 60, title: "PROPIETARIO", template: "<a  href='../SUBTIPOSESTABLECIMIENTOS/Edit/${OWNER},${SUBE_ID}' style='color:gray !important;'>${OWNER}</a>"
              },
              {
                  field: "SUBE_ID", width: 8, title: "Id", template: "<a  href='../SUBTIPOSESTABLECIMIENTOS/Edit/${OWNER},${SUBE_ID}'style='color:gray !important;'>${SUBE_ID}</a>"
              },
              {
                  field: "DESCRIPTION", width: 60, title: "Descripción", template: "<a  href='../SUBTIPOSESTABLECIMIENTOS/Edit/${OWNER},${SUBE_ID}'style='color:gray !important;'>${DESCRIPTION}</a>"
              },
              {
                  field: "DESCRIPTIONTYPE", width: 40, title: "Tipo", template: "<a  href='../SUBTIPOSESTABLECIMIENTOS/Edit/${OWNER},${SUBE_ID}'style='color:gray !important;'>${DESCRIPTIONTYPE}</a>"
              },
              //{
              //    field: "ECON_DESC", width: 40, title: "Actividad economica", template: "<a  href='../SUBTIPOSESTABLECIMIENTOS/Edit/${OWNER},${SUBE_ID}'style='color:gray !important;'>${ECON_DESC}</a>"
              //},
              { field: "ACTIVO", width: 12, title: "Estado", template: "<a  href='../TIPOSDIRECCIONES/Edit/${OWNER},${SUBE_ID}'style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
          ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar sub tipo de establecimiento?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        SUBE_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../SUBTIPOSESTABLECIMIENTOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../SUBTIPOSESTABLECIMIENTOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
    $("#ddlTipoestablecimiento").val(0);
}