
$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    var busq = "";
    var tot = $("#TotalVirtual").val();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    type: "POST",
                    url: "../NIVELAGENTESRECAUDADORES/usp_listar_nivelesAgenteRecaudadoresJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECCOLLLEVEL", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: true,
        selectable: "multiple row",
        columns:
             [
             {
                 title: 'Eliminar', width: 30, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${LEVEL_ID}'/>"
             },
             {
                 field: "LEVEL_ID", width: 70, title: "NIV. AGENTE COMERCIAL", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}'style='color:5F5F5F;text-decoration:none;'>${LEVEL_ID}</a>"
             },
             {
                 hidden: true,
                 field: "OWNER", width: 40, title: "PROPIETARIO", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
             },
             {
                 field: "NMR_ID", width: 80, title: "NUMERADOR ASIGNADO", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${NMR_ID}</a>"
             },
             {
                 field: "LEVEL_DEP", width: 100, title: "NIV S. AGENTE COMERCIAL", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${LEVEL_DEP}</a>"
             },
             {
                 field: "DESCRIPTION", width: 110, title: "DESCRIPCION", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
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
                        url: "../NIVELAGENTESRECAUDADORES/usp_listar_nivelesAgenteRecaudadoresJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "RECCOLLLEVEL", total: 'TotalVirtual' }
            },
            sortable: true,
            pageable: true,
            selectable: "multiple row",
            columns:
             [
             {
                 title: 'Eliminar', width: 30, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${LEVEL_ID}'/>"
             },
             {
                 field: "LEVEL_ID", width: 70, title: "NIV. AGENTE COMERCIAL", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}'style='color:5F5F5F;text-decoration:none;'>${LEVEL_ID}</a>"
             },
             {
                 hidden: true,
                 field: "OWNER", width: 40, title: "PROPIETARIO", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
             },
             {
                 field: "NMR_ID", width: 80, title: "NUMERADOR ASIGNADO", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${NMR_ID}</a>"
             },
             {
                 field: "LEVEL_DEP", width: 100, title: "NIV S. AGENTE COMERCIAL", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}' style='color:5F5F5F;text-decoration:none;'>${LEVEL_DEP}</a>"
             },
             {
                 field: "DESCRIPTION", width: 110, title: "DESCRIPCION", template: "<a  href='/NIVELAGENTESRECAUDADORES/Edit/${OWNER},${LEVEL_ID}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
             }
             ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el identificador fiscal?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        LEVEL_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../NIVELAGENTESRECAUDADORES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "NIVELAGENTESRECAUDADORES";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
