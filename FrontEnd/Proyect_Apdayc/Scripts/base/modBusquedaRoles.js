$(function () {
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $("#txtBusqueda").focus();

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
                    url: "../ROLES/usp_listar_RolesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "ROL", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 30, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${ROL_ICODIGO_ROL}'/>"
            },
            {
                hidden: true,
                field: "ROL_ICODIGO_ROL", width: 50, title: "Codigo", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}'style='color:5F5F5F;text-decoration:none;'>${ROL_ICODIGO_ROL}</a>"
            },
            {
                field: "ROL_VNOMBRE_ROL", width: 150, title: "Rol", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}' style='color:5F5F5F;text-decoration:none;'>${ROL_VNOMBRE_ROL}</a>"
            },
            {
                field: "ROL_VDESCRIPCION_ROL", width: 300, title: "Descripción", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}'style='color:5F5F5F;text-decoration:none;'>${ROL_VDESCRIPCION_ROL}</a>"
            },
            {
                field: "ROL_CACTIVO_ROL", width: 50, title: "Estado", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}' style='color:5F5F5F;text-decoration:none;' >${ROL_CACTIVO_ROL}</a>"
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
                        url: "../ROLES/usp_listar_RolesJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "ROL", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 30, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${ROL_ICODIGO_ROL}'/>"
            },
            {
                hidden: true,
                field: "ROL_ICODIGO_ROL", width: 50, title: "Codigo", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}'style='color:5F5F5F;text-decoration:none;'>${ROL_ICODIGO_ROL}</a>"
            },
            {
                field: "ROL_VNOMBRE_ROL", width: 150, title: "Rol", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}' style='color:5F5F5F;text-decoration:none;'>${ROL_VNOMBRE_ROL}</a>"
            },
            {
                field: "ROL_VDESCRIPCION_ROL", width: 300, title: "Descripción", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}'style='color:5F5F5F;text-decoration:none;'>${ROL_VDESCRIPCION_ROL}</a>"
            },
            {
                field: "ROL_CACTIVO_ROL", width: 50, title: "Estado", template: "<a  href='../ROLES/Edit/${ROL_ICODIGO_ROL}' style='color:5F5F5F;text-decoration:none;' >${ROL_CACTIVO_ROL}</a>"
            }
        ]
        })
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar rol?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        ROL_ICODIGO_ROL: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../ROLES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../ROLES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});



