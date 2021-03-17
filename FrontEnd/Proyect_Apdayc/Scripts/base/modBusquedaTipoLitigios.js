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

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadEstadosMaestro("ddlEstado");
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
                    url: "../TIPOSLITIGIOS/usp_listar_tipoLitigiosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECLAWSUITETYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 7, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${LAWS_ID}'/>"
              },
              {                 
                  field: "LAWS_ID", width: 6, title: "Id", template: "<a  href='../TIPOSLITIGIOS/Edit/${OWNER},${LAWS_ID}'style='color:gray;text-decoration:none;'>${LAWS_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "Propietario", template: "<a  href='../TIPOSLITIGIOS/Edit/${OWNER},${LAWS_ID}' style='color:gray;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "LAWS_DESC", width: 80, title: "Descripción", template: "<a  href='../TIPOSLITIGIOS/Edit/${OWNER},${LAWS_ID}'style='color:gray;text-decoration:none;'>${LAWS_DESC}</a>"
              },
              { field: "Activo", width: 8, title: "Estado", template: "<a  href='../TIPOSLITIGIOS/Edit/${OWNER},${LAWS_ID}' style='color:gray;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
              ]
    })
};

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo de litigio?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        LAWS_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOSLITIGIOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOSLITIGIOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}
