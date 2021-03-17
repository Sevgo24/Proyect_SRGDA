/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;

$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }
    loadEstadosMaestro("ddlEstado");
    $(".alert-link").hide();
    $("#message").hide();

    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../RangoMorosidad/Listar_PageJson_RangoMorosidad",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RangoMorosidad", total: 'TotalVirtual' }
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
                 title: 'Eliminar', width: 15, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${RANGE_COD}'/>"
             },
             {
                 hidden: true,
                 field: "RANGE_COD", width: 10, title: "CODIGO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}'style='color:5F5F5F;text-decoration:none;'>${RANGE_COD}</a>"
             },
             {
                 hidden: true,
                 field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
             },
             {
                 field: "DESCRIPTION", width: 80, title: "Descripción", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
             },
             {
                 field: "RANGE_FROM", width: 20, title: "Inicio Rango", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${RANGE_FROM}</a>"
             },
             {
                 field: "RANGE_TO", width: 20, title: "Fin Rango", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${RANGE_TO}</a>"
             }
             ]
    })
});

$(function () {
    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        $("#txtBusqueda").focus();
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
                        url: "../RangoMorosidad/Listar_PageJson_RangoMorosidad",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                    }
                },
                schema: { data: "RangoMorosidad", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 30, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${RANGE_COD}'/>"
            },
            {
                field: "RANGE_COD", width: 60, title: "CODIGO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}'style='color:5F5F5F;text-decoration:none;'>${RANGE_COD}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 40, title: "PROPIETARIO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "DESCRIPTION", width: 110, title: "DESCRIPCION", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}'style='color:5F5F5F;text-decoration:none;'>${DESCRIPTION}</a>"
            },
            {
                field: "RANGE_FROM", width: 80, title: "INICIO RANGO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${RANGE_FROM}</a>"
            },
            {
                field: "RANGE_TO", width: 80, title: "FIN RANGO", template: "<a  href='../RANGOSMOROSIDAD/Edit/${OWNER},${RANGE_COD}' style='color:5F5F5F;text-decoration:none;'>${RANGE_TO}</a>"
            }
            ]
        });
    });
});

$(function () {
    $("#btnsuprimir").click(function () {
        
        bootbox.confirm("Desea eliminar el rango de morosidad?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        RANGE_COD: itemId
                    });
                    
                });

                var dato = JSON.stringify(array);
                $.ajax({
                    type: 'POST',
                    url: "../RangoMorosidad/Eliminar",
                    data: dato,
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../RANGOSMOROSIDAD/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}