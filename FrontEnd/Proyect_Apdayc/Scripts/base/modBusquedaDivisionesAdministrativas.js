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
                    url: "../DIVISIONESADMINISTRATIVAS/usp_listar_DivisionesAdministrativasJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "REFDIVISIONES", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 80, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DAD_ID}'/>"
              }, {
                  hidden: true,
                  field: "OWNER", width: 100, title: "PROPIETARIO", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              }, {
                  hidden: true,
                  field: "DAD_ID", width: 100, title: "Código", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}' style='color:5F5F5F;text-decoration:none;'>${DAD_ID}</a>"
              },
              {
                  field: "DAD_TNAME", width: 150, title: "Tipo División", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_TNAME}</a>"
              },
              {
                  field: "DAD_CODE", width: 100, title: "Identificación Corta", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_CODE}</a>"
              },
              {
                  field: "DAD_NAME", width: 300, title: "Identificación Larga", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_NAME}</a>"
              },
              {
                  field: "DAD_TYPE",
                  hidden: true,
                  width: 200, title: "ID TIPO DIVISION", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_TYPE}</a>"
              }

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
                        url: "../DIVISIONESADMINISTRATIVAS/usp_listar_DivisionesAdministrativasJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "REFDIVISIONES", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 80, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DAD_ID}'/>"
              }, {
                  hidden: true,
                  field: "OWNER", width: 100, title: "PROPIETARIO", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              }, {
                  hidden: true,
                  field: "DAD_ID", width: 100, title: "Código", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}' style='color:5F5F5F;text-decoration:none;'>${DAD_ID}</a>"
              },
              {
                  field: "DAD_TNAME", width: 200, title: "Tipo División", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_TNAME}</a>"
              },
              {
                  field: "DAD_CODE", width: 50, title: "Identificación Corta", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_CODE}</a>"
              },
              {
                  field: "DAD_NAME", width: 300, title: "Identificación Larga", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_NAME}</a>"
              },
              {
                  field: "DAD_TYPE",
                  hidden: true,
                  width: 200, title: "ID TIPO DIVISION", template: "<a  href='../DIVISIONESADMINISTRATIVAS/Edit/${OWNER},${DAD_ID}'style='color:5F5F5F;text-decoration:none;'>${DAD_TYPE}</a>"
              }
             
          ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar esta división administrativa?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        DAD_ID: itemId
                        
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../DIVISIONESADMINISTRATIVAS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../DIVISIONESADMINISTRATIVAS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});