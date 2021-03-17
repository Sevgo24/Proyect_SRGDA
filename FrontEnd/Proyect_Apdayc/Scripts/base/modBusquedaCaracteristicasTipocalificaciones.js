
$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();
    $("#btnBusqueda").focus();
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
                    url: "../CARACTERISTICASTIPOSCALIFICADORES/usp_listar_caracteristicastipocalificacionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECQUALIFYCHAR", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 15, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${QUC_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  hidden: true,
                  field: "QUC_ID", width: 50, title: "CARACTERISTICA", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${QUC_ID}</a>"
              },
              {
                  field: "DESCRIPTION", width: 150, title: "Descripción", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
              },
              {
                  field: "DESCTIPO", width: 150, title: "Tipo de Calificador", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCTIPO}</a>"
              }
              ]
    })
});


/// <reference path="../kendo.web-vsdoc.js" />
$(function () {
    $("#btnBusqueda").click(function () {

        $(".alert-link").hide();
        $("#message").hide();

        var busq = $("#txtBusqueda").val();
        var tot = $("#TotalVirtual").val();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: K_PAGINACION.LISTAR_15,
                transport: {
                    read: {
                        type: "POST",
                        url: "../CARACTERISTICASTIPOSCALIFICADORES/usp_listar_caracteristicastipocalificacionesJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "RECQUALIFYTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 15, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${QUC_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  hidden: true,
                  field: "QUC_ID", width: 50, title: "CARACTERISTICA", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${QUC_ID}</a>"
              },
              {
                  field: "DESCRIPTION", width: 150, title: "Descripción", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
              },
              {
                  field: "DESCTIPO", width: 150, title: "Tipo de Calificador", template: "<a  href='../CARACTERISTICASTIPOSCALIFICADORES/Edit/${OWNER},${QUC_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCTIPO}</a>"
              }
               ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la caracteristica tipo de calificación?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        QUC_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../CARACTERISTICASTIPOSCALIFICADORES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../CARACTERISTICASTIPOSCALIFICADORES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
