$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();
    
    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), tipo: $("#ddlActividadEconomica").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    
    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), tipo: $("#ddlActividadEconomica").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadActividadEcon("ddlActividadEconomica",0);
    loadEstadosMaestro("ddlEstado");

    $("#txtBusqueda").focus();
    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), tipo: $("#ddlActividadEconomica").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });


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
                    url: "../TIPOSESTABLECIMIENTOS/usp_listar_TipoEstablecimientosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), tipo: $("#ddlActividadEconomica").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECESTTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${ESTT_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 60, title: "PROPIETARIO", template: "<a  href='../TIPOSESTABLECIMIENTOS/Edit/${OWNER},${ESTT_ID}' style='color:gray !important;'>${OWNER}</a>"
              },
              {
                  field: "ESTT_ID", width: 6, title: "Id", template: "<a  href='../TIPOSESTABLECIMIENTOS/Edit/${OWNER},${ESTT_ID}'style='color:gray !important;'>${ESTT_ID}</a>"
              },              
              {
                  hidden: true,
                  field: "ECON_ID", width: 70, title: "Actividad Económica", template: "<a  href='../TIPOSESTABLECIMIENTOS/Edit/${OWNER},${ESTT_ID}'style='color:gray !important;'>${ECON_ID}</a>"
              },
              {
                  field: "DESCRIPTION", width: 50, title: "Descripción", template: "<a  href='../TIPOSESTABLECIMIENTOS/Edit/${OWNER},${ESTT_ID}'style='color:gray !important;'>${DESCRIPTION}</a>"
              },
              {
                  field: "ECON_DESC", width: 90, title: "Actividad Económica", template: "<a  href='../TIPOSESTABLECIMIENTOS/Edit/${OWNER},${ESTT_ID}'style='color:gray !important;'>${ECON_DESC}</a>"
              },
              { field: "ACTIVO", width: 12, title: "<font size=2px>Estado </font>", template: "<a  href='../TIPOSDIRECCIONES/Edit/${OWNER},${ESTT_ID}'style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
          ]
    });
}
$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar tipo de establecimiento?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        ESTT_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOSESTABLECIMIENTOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOSESTABLECIMIENTOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
    loadActividadEcon("ddlActividadEconomica", 0);
}