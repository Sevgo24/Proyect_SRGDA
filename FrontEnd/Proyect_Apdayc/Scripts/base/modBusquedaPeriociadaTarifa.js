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
                    url: "../PERIODICIDADTARIFA/usp_listar_periocidadtarifaJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECRATEFREQUENCY", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 7, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${RAT_FID}'/>"
              },
              {
                  field: "RAT_FID", width: 6, title: "Id", template: "<a  href='../PERIODICIDADTARIFA/Edit/${OWNER},${RAT_FID}'style='color:5F5F5F;text-decoration:none;'>${RAT_FID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../PERIODICIDADTARIFA/Edit/${OWNER},${RAT_FID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "RAT_FDESC", width: 90, title: "Descripción", template: "<a  href='../PERIODICIDADTARIFA/Edit/${OWNER},${RAT_FID}'style='color:5F5F5F;text-decoration:none;'>${RAT_FDESC}</a>"
              },
              { field: "Activo", width: 8, title: "<font size=2px>Estado </font>", template: "<a  href='../PERIODICIDADTARIFA/Edit/${OWNER},${RAT_FID}' style='color:5F5F5F;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
              ]
    })
};



$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la periocidad de la tarifa?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        RAT_FID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../PERIODICIDADTARIFA/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../PERIODICIDADTARIFA/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
