/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

$(function () {
    $("#repPDF").on("click", function (e) {
        reporte();
    });

    $("#repEXCEL").on("click", function (e) {
        reporte();
    });

    loadComboTerritorio(-1);
    $("#ddlTerritorio").append($("<option>Some Text</option>").val(-1).html("<--Seleccione-->"));

    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), territorio: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), territorio: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function loadData() { 
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../IMPUESTOS/usp_listar_ImpuestoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), territorio: $("#ddlTerritorio").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECTAXES", total: 'TotalVirtual' }
        },
        sortable: false,
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
                    title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${TAX_ID}'/>"
                }, {
                    
                    field: "TAX_ID", width: 10, title: "Id", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}'style='color:5F5F5F;text-decoration:none;'>${TAX_ID}</a>"
                }, {
                    hidden: true,
                    field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
                }, {
                    hidden: true,
                    field: "TIS_N", width: 15, title: "Id", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}'style='color:5F5F5F;text-decoration:none;'>${TIS_N}</a>"
                },
                {
                    hidden: true,
                    field: "TAX_COD", width: 15, title: "Cod impuesto", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;' >${TAX_COD}</a>"
                }, {
                    field: "DESCRIPTION", width: 50, title: "Descripción", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;' >${DESCRIPTION}</a>"
                },
                {
                    field: "TAX_CACC", width: 10, title: "Cuenta", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;' >${TAX_CACC}</a>"
                },
                
                {
                    field: "NAME_TER", width: 20, title: "Territorio", template: "<a  href='../IMPUESTOS/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;' >${NAME_TER}</a>"
                },
                { field: "Activo", width: 12, title: "<font size=2px>Estado </font>", template: "<a  href='../MONEDA/Edit/${TAX_ID}' style='color:5F5F5F;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
            ]
    })

    
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar impuesto?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        TAX_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../IMPUESTOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../IMPUESTOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});



function reporte() {
    var vBusqueda=$("#txtBusqueda").val();
    var vTerritorio=$("#ddlTerritorio").val();
    $.ajax({
        url: "../Impuestos/Reporte",
        type: "GET",
        data: { busqueda: vBusqueda, territorio:vTerritorio },
        success: function (response) {
          
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}