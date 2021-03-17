
$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $("#txtBusqueda").focus();

    $(".alert-link").hide();
    $("#message").hide();

    var busq = "";
    var tot = $("#TotalVirtual").val();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TIPOAFORO/usp_listar_TipoaforoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECCAPACITYTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${CAP_ID}'/>"
              },
              {
                  hidden: true,
                  field: "CAP_ID", width: 50, title: "Id", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}'style='color:5F5F5F;text-decoration:none;'>${CAP_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "CAP_DESC", width: 150, title: "Descripción", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}'style='color:5F5F5F;text-decoration:none;'>${CAP_DESC}</a>"
              }
              ]
    })
});


/// <reference path="../kendo.web-vsdoc.js" />
$(function () {
    $("#btnBusqueda").click(function () {

        $(".alert-link").hide();
        $("#message").hide();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: 20,
                transport: {
                    read: {
                        type: "POST",
                        url: "../TIPOAFORO/usp_listar_TipoaforoJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "RECCAPACITYTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${CAP_ID}'/>"
              },
              {
                  hidden: true,
                  field: "CAP_ID", width: 50, title: "Id", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}'style='color:5F5F5F;text-decoration:none;'>${CAP_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "CAP_DESC", width: 150, title: "Descripción", template: "<a  href='../TIPOAFORO/Edit/${OWNER},${CAP_ID}'style='color:5F5F5F;text-decoration:none;'>${CAP_DESC}</a>"
              }
              ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo aforo?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        CAP_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOAFORO/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOAFORO/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
