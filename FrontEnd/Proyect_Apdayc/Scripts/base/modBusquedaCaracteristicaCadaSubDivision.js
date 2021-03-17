/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;

$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_SIZE_PAGE,
            transport: {
                read: {
                    type: "POST",
                    url: "../CARACTERISTICASASIGNADASSUBDIVISION/usp_listar_CaracteriticaAsigSubDivisionJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "REFDIVSTYPECHAR", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
            [//,${DAD_TYPE},${DAD_STYPE}
                 {
                     title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DAC_ID},${DAD_TYPE},${DAD_STYPE}'/>"
                 },
                 {
                     hidden: true,
                     field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                 },
                 {
                     field: "DAC_ID", width: 10,
                     hidden: true,
                     title: "IDENT.CARACTERISTICA", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAC_ID}</a>"
                 },
                 {
                     field: "DAD_TYPE", width: 10,
                     hidden: true,
                     title: "TIPO DIVISIÓN", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAD_TYPE}</a>"
                 },
                 {
                     field: "DAD_STYPE", width: 10,
                     hidden: true,
                     title: "SUBTIPO CARACTERISTICA", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_STYPE}</a>"
                 },
                 {
                     field: "DAD_TNAME", width: 30, title: "Tipo de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_TNAME}</a>"
                 },
                 {
                     field: "DAD_SNAME", width: 10, title: "Sub-Tipo de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_SNAME}</a>"
                 },
                 {
                     field: "DESCRIPTION", width: 30, title: "Caracteristica de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
                 },
            ]
    });
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
                pageSize: K_SIZE_PAGE,
                transport: {
                    read: {
                        type: "POST",
                        url: "../CARACTERISTICASASIGNADASSUBDIVISION/usp_listar_CaracteriticaAsigSubDivisionJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "REFDIVSTYPECHAR", total: 'TotalVirtual' }
            },
            sortable: true,
            pageable: {
                messages: {
                    display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                    empty: "No se encontraron registros"
                }
            },
            columns:
                 [//,${DAD_TYPE},${DAD_STYPE}
                 {
                     title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DAC_ID},${DAD_TYPE},${DAD_STYPE}'/>"
                 },
                 {
                     hidden: true,
                     field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                 },
                 {
                     field: "DAC_ID", width: 10,
                     hidden: true,
                     title: "IDENT.CARACTERISTICA", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAC_ID}</a>"
                 },
                 {
                     field: "DAD_TYPE", width: 10,
                     hidden: true,
                     title: "TIPO DIVISIÓN", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}' style='color:5F5F5F;text-decoration:none;'>${DAD_TYPE}</a>"
                 },
                 {
                     field: "DAD_STYPE", width: 10,
                     hidden: true,
                     title: "SUBTIPO CARACTERISTICA", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_STYPE}</a>"
                 },
                 {
                     field: "DAD_TNAME", width: 30, title: "Tipo de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_TNAME}</a>"
                 },
                 {
                     field: "DAD_SNAME", width: 10, title: "Sub-Tipo de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DAD_SNAME}</a>"
                 },
                 {
                     field: "DESCRIPTION", width: 30, title: "Caracteristica de División", template: "<a  href='../CARACTERISTICASASIGNADASSUBDIVISION/Edit/${DAC_ID},${DAC_ID},${DAD_TYPE},${DAD_STYPE}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
                 },
                 ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la caracateristica sub división asignada?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();

                    array.push
                        ({
                            DAC_ID: itemId                          
                        });
                });

                $.ajax({
                    type: 'POST',
                    url: "../CARACTERISTICASASIGNADASSUBDIVISION/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../CARACTERISTICASASIGNADASSUBDIVISION/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});