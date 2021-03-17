/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;

$(function () {
    $("#txtBusqueda").focus();
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
            pageSize: K_SIZE_PAGE,
            transport: {
                read: {
                    type: "POST",
                    url: "../CALIFICADORESSOCIOSNEGOCIOS/usp_listar_calificadoressociosnegocioJson",
                    dataType: "json"
                    //data: { dato: busq }
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECBPSQUALY", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BPS_ID}'/>"
            },
            {
                hidden: true,
                field: "BPS_ID", width: 10, title: "Id", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}'style='color:5F5F5F;text-decoration:none;'>${BPS_ID}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "CARACTERISTICA", width: 70, title: "Caracteristica", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}'style='color:5F5F5F;text-decoration:none;'>${CARACTERISTICA}</a>"
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
                pageSize: K_SIZE_PAGE,
                transport: {
                    read: {
                        type: "POST",
                        url: "../CALIFICADORESSOCIOSNEGOCIOS/usp_listar_calificadoressociosnegocioJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "RECBPSQUALY", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BPS_ID}'/>"
            },
            {
                hidden: true,
                field: "BPS_ID", width: 10, title: "Id", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}'style='color:5F5F5F;text-decoration:none;'>${BPS_ID}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "CARACTERISTICA", width: 70, title: "Caracteristica", template: "<a  href='/CALIFICADORESSOCIOSNEGOCIOS/Edit/${OWNER},${BPS_ID}'style='color:5F5F5F;text-decoration:none;'>${CARACTERISTICA}</a>"
            }
             ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el calificador socio del negocio?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        BPS_ID: itemId
                    });
                });

                var dato = JSON.stringify(array);
                $.ajax({
                    type: 'POST',
                    url: "../CALIFICADORESSOCIOSNEGOCIOS/Eliminar",
                    data: dato,
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "CALIFICADORESSOCIOSNEGOCIOS";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
