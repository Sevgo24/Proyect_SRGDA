
$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();
    $("#txtBusqueda").focus();

    var busq = "";
    var tot = $("#TotalVirtual").val();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../TIPOSSOCIEDADESDERECHO/usp_listar_TipoderechoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "REFSOCIETYTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 20, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${SOC_TYPE}'/>"
              },
              {
                  hidden:true,
                  field: "SOC_TYPE", width: 50, title: "Tipo de Derecho", template: "<a  href='../TIPOSSOCIEDADESDERECHO/Edit/${SOC_TYPE}'style='color:5F5F5F;text-decoration:none;'>${SOC_TYPE}</a>"
              },
              {
                  field: "SOC_DESC", width: 150, title: "Descripción", template: "<a  href='../TIPOSSOCIEDADESDERECHO/Edit/${SOC_TYPE}'style='color:5F5F5F;text-decoration:none;'>${SOC_DESC}</a>"
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
                pageSize: 5,
                transport: {
                    read: {
                        type: "POST",
                        url: "../TIPOSSOCIEDADESDERECHO/usp_listar_TipoderechoJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "REFSOCIETYTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 20, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${SOC_TYPE}'/>"
              },
              {
                  hidden:true,
                  field: "SOC_TYPE", width: 50, title: "Tipo de Derecho", template: "<a  href='../TIPOSSOCIEDADESDERECHO/Edit/${SOC_TYPE}'style='color:5F5F5F;text-decoration:none;'>${SOC_TYPE}</a>"
              },
              {
                  field: "SOC_DESC", width: 150, title: "Descripción", template: "<a  href='../TIPOSSOCIEDADESDERECHO/Edit/${SOC_TYPE}'style='color:5F5F5F;text-decoration:none;'>${SOC_DESC}</a>"
              }
            ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo de derecho?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        SOC_TYPE: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOSSOCIEDADESDERECHO/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOSSOCIEDADESDERECHO/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
