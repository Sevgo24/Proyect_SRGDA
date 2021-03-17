
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
                    url: "../BLOQUEOS/usp_listar_bloqueosJson", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "RECBLOCKS", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: true,
        selectable: "multiple row",
        columns:
               [
               {
                   title: 'Eliminar', width: 40, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BLOCK_ID}'/>"
               },
               {
                   hidden: true,
                   field: "BLOCK_ID", width: 60, title: "CODIGO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_ID}</a>"
               },
               {
                   hidden: true,
                   field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
               },
               {
                   field: "BLOCK_DESC", width: 150, title: "DESCRIPCION", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_DESC}</a>"
               },
               {
                   field: "BLOCK_PULL", width: 60, title: "ARRASTRE BLOQUEO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_PULL}</a>"
               },
               {
                   field: "BLOCK_AUT", width: 60, title: "AUTORIZACION", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_AUT}</a>"
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
                pageSize: 5,
                transport: {
                    read: {
                        url: "../BLOQUEOS/usp_listar_bloqueosJson", dataType: "json", data: { dato: busq }
                    }
                },
                schema: { data: "RECBLOCKS", total: 'TotalVirtual' }
            },
            sortable: true,
            pageable: true,
            selectable: "multiple row",
            columns:
               [
               {
                   title: 'Eliminar', width: 40, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BLOCK_ID}'/>"
               },
               {
                   hidden: true,
                   field: "BLOCK_ID", width: 60, title: "CODIGO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_ID}</a>"
               },
               {
                   hidden: true,
                   field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
               },
               {
                   field: "BLOCK_DESC", width: 150, title: "DESCRIPCION", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_DESC}</a>"
               },
               {
                   field: "BLOCK_PULL", width: 60, title: "ARRASTRE BLOQUEO", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_PULL}</a>"
               },
               {
                   field: "BLOCK_AUT", width: 60, title: "AUTORIZACION", template: "<a  href='/BLOQUEOS/Edit/${OWNER},${BLOCK_ID}'style='color:5F5F5F;text-decoration:none;'>${BLOCK_AUT}</a>"
               }
               ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el bloqueo?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        BLOCK_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../BLOQUEOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "BLOQUEOS";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
