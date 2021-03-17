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
                    url: "../SUBTIPOSDIVISIONES/usp_listar_SubTipoDivisionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "REFDIVSUBTYPE", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
            [
                {
                    hidden: true,
                    title: 'Eliminar', width: 100, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${DAD_STYPE}'/>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 60, title: "PROPIETARIO", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                },
                {
                    field: "DAD_CODE",
                    width: 100, title: "División", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAD_CODE}</a>"
                },
                {
                    hidden: true,
                    field: "DAD_STYPE", width: 110, title: "Subtipo", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_STYPE}</a>"
                },
                {
                    field: "DAD_SNAME", width: 60, title: "Identidad corta", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_SNAME}</a>"
                },
                {
                    field: "DAD_NAME", width: 110, title: "Identidad larga", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_NAME}</a>"
                },
                {
                    field: "DAD_BELONGS", width: 110, title: "Jerarquía", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_BELONGS}</a>"
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
                        url: "../SUBTIPOSDIVISIONES/usp_listar_SubTipoDivisionesJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "REFDIVSUBTYPE", total: 'TotalVirtual' }
            },
            sortable: true,
            pageable: {
                messages: {
                    display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                    empty: "No se encontraron registros"
                }
            },
            columns:
            [
                {
                    hidden: true,
                    title: 'Eliminar', width: 100, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${DAD_STYPE}'/>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 60, title: "PROPIETARIO", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                },
                {
                    field: "DAD_CODE",
                    width: 100, title: "División", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAD_CODE}</a>"
                },
                {
                    hidden: true,
                    field: "DAD_STYPE", width: 110, title: "Subtipo", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_STYPE}</a>"
                },
                {
                    field: "DAD_SNAME", width: 60, title: "Identidad corta", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_SNAME}</a>"
                },
                {
                    field: "DAD_NAME", width: 110, title: "Identidad larga", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_NAME}</a>"
                },
                {
                    field: "DAD_BELONGS", width: 110, title: "Jerarquía", template: "<a  href='../SUBTIPOSDIVISIONES/Edit/${OWNER},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_BELONGS}</a>"
                }
            ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el sub tipo de division?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        DAD_STYPE: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../SUBTIPOSDIVISIONES/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../SUBTIPOSDIVISIONES/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});