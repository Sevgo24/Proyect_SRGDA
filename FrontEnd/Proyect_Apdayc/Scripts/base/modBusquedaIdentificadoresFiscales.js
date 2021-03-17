
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
    $("#txtBusqueda").focus();

    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../IDENTIFICADORESFISCALES/usp_listar_IdentificadoresFiscalesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECTAXID", total: 'TotalVirtual' }
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
                      title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${TAXT_ID}'/>"
                  },
                  {
                      
                      field: "TAXT_ID", width: 9, title: "ID", template: "<a  id='single_2' href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}'style='color:gray;'>${TAXT_ID}</a>"
                  },
                  {
                      hidden: true,
                      field: "OWNER", width: 20, title: "PROPIETARIO", template: "<a  id='single_2' href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}' style='color:gray;'>${OWNER}</a>"
                  },
                  {
                      hidden: true,
                      field: "TIS_N", width: 30, title: "Id pais", template: "<a  id='single_2' href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}' style='color:gray;'>${TIS_N}</a>"
                  },
                  {
                      field: "NAME_TER", width: 25, title: "País", template: "<a id='single_2'  href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}' style='color:gray;'>${NAME_TER}</a>"
                  },
                  {
                      field: "TAXN_NAME", width: 50, title: "Nombre", template: "<a id='single_2'  href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}'style='color:gray;'>${TAXN_NAME}</a>"
                  },
                  {
                      field: "TEXT_DESCRIPTION", width: 100, title: "Descripción", template: "<a id='single_2'  href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}'style='color:gray;'>${TEXT_DESCRIPTION}</a>"
                  },
                  {
                      field: "TAXN_POS", width: 20, title: "Longitud", template: "<a id='single_2'  href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}' style='color:gray;'>${TAXN_POS}</a>"
                  },
                  { field: "ACTIVO", width: 12, title: "Estado", template: "<a  id='single_2' href='../IDENTIFICADORESFISCALES/Edit/${OWNER},${TAXT_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
                  
                  
              ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el documentos de Identificación?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        TAXT_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../IDENTIFICADORESFISCALES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../IDENTIFICADORESFISCALES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}
