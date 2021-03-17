
$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#txtBusqueda").focus();

    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 20,
            transport: {
                read: {
                    type: "POST",
                    url: "../SUCURSALESBANCARIAS/usp_listar_SucursalesBancariasJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECBANKSBRANCH", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${BNK_ID},${BRCH_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 30, title: "PROPIETARIO", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  hidden: true,
                  field: "BNK_ID", width: 30, title: "INDENT. BANCO", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BNK_ID}</a>"
              },
              {
                  hidden: true,
                  field: "BRCH_ID", width: 30, title: "INDENT. SUCURSAL", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BRCH_ID}</a>"
              },
              {
                  field: "BNK_NAME", width: 40, title: "Banco", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BNK_NAME}</a>"
              },
              {
                  field: "BRCH_NAME", width: 40, title: "Sucursal", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BRCH_NAME}</a>"
              },
              {
                  hidden: true,
                  field: "ADD_ID", width: 50, title: "COD. DIRECCION", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:5F5F5F;text-decoration:none;'>${ADD_ID}</a>"
              }
              ,
              {
                  field: "ADDRESS", width: 50, title: "Dirección", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:5F5F5F;text-decoration:none;'>${ADDRESS}</a>"
              }
              ]
    })
});


/// <reference path="../kendo.web-vsdoc.js" />
$(function () {
    $("#btnBusqueda").click(function () {

        $(".alert-link").hide();
        $("#message").hide();
        //var idbanco = $("#idBanco").val();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: 20,
                transport: {
                    read: {
                        type: "POST",
                        url: "../SUCURSALESBANCARIAS/usp_listar_SucursalesBancariasJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val(), id: $("#idBanco").val() })
                    }
                },
                schema: { data: "RECBANKSBRANCH", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${BNK_ID},${BRCH_ID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 30, title: "PROPIETARIO", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  hidden: true,
                  field: "BNK_ID", width: 30, title: "INDENT. BANCO", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BNK_ID}</a>"
              },
              {
                  hidden: true,
                  field: "BRCH_ID", width: 30, title: "INDENT. SUCURSAL", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BRCH_ID}</a>"
              },
              {
                  field: "BNK_NAME", width: 40, title: "Banco", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BNK_NAME}</a>"
              },
              {
                  field: "BRCH_NAME", width: 40, title: "Sucursal", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:5F5F5F;text-decoration:none;'>${BRCH_NAME}</a>"
              },
              {
                  hidden: true,
                  field: "ADD_ID", width: 50, title: "COD. DIRECCION", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:5F5F5F;text-decoration:none;'>${ADD_ID}</a>"
              }
              ,
              {
                  field: "ADDRESS", width: 50, title: "Dirección", template: "<a  href='../SUCURSALESBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:5F5F5F;text-decoration:none;'>${ADDRESS}</a>"
              }
              ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar esta sucursal bancaria?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        OWNER: itemId
                        
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../SUCURSALESBANCARIAS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../SUCURSALESBANCARIAS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
