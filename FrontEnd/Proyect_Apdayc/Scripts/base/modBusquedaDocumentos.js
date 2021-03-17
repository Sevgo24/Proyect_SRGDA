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
                    url: "../DOCUMENTOS/usp_listar_documentosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                }
            },
            schema: { data: "RECDOCUMENTSGRAL", total: 'TotalVirtual' }
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
                    title: 'Eliminar', width: 5, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DOC_ID}'/>"
                },
                {
                    hidden: true,
                    field: "DOC_ID", width: 15, title: "ID", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;'>${DOC_ID}</a>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 15, title: "PROPIETARIO", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                },
                {
                    field: "DOC_DESC", width: 30, title: "Tipo Documento", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;'>${DOC_DESC}</a>"
                },
                {
                    type: "date",
                    field: "DOC_DATE",
                    width: 15,
                    title: "Inlusión",
                    template: "<font color='green'><a id='single_2' href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;font-size:11px'>" + '#=(DOC_DATE==null)?"":kendo.toString(kendo.parseDate(DOC_DATE,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>",
                },
                {
                    field: "DOC_VERSION", width: 5, title: "versión", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}' style='color:5F5F5F;text-decoration:none;' >${DOC_VERSION}</a>"
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
                pageSize: K_SIZE_PAGE,
                transport: {
                    read: {
                        type: "POST",
                        url: "../DOCUMENTOS/usp_listar_documentosJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                    }
                },
                schema: { data: "RECDOCUMENTSGRAL", total: 'TotalVirtual' }
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
                    title: 'Eliminar', width: 5, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DOC_ID}'/>"
                },
                {
                    hidden: true,
                    field: "DOC_ID", width: 15, title: "ID", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;'>${DOC_ID}</a>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 15, title: "PROPIETARIO", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                },
                {
                    field: "DOC_DESC", width: 30, title: "Tipo Documento", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;'>${DOC_DESC}</a>"
                },
                {
                    type: "date",
                    field: "DOC_DATE",
                    width: 15,
                    title: "Inlusión",
                    template: "<font color='green'><a id='single_2' href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}'style='color:5F5F5F;text-decoration:none;font-size:11px'>" + '#=(DOC_DATE==null)?"":kendo.toString(kendo.parseDate(DOC_DATE,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>",
                },
                {
                    field: "DOC_VERSION", width: 5, title: "versión", template: "<a  href='../DOCUMENTOS/Edit/${OWNER},${DOC_ID}' style='color:5F5F5F;text-decoration:none;' >${DOC_VERSION}</a>"
                }
              ]
        })
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar documento?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        DOC_ID: itemId
                    });
                });

                var dato=JSON.stringify(array );
                $.ajax({
                    type: 'POST',
                    url: "../DOCUMENTOS/Eliminar",
                    data: dato,
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../DOCUMENTOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});



