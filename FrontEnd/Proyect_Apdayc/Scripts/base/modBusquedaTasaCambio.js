

$(function () {
    var fecha = new Date();
    var ano = fecha.getFullYear();
    var mes = fecha.getMonth()+1;

    $("#cb1").val(mes);
    $("#cb2").val(ano);

    loadData();

    $("#btnBusqueda").click(function () {       

        $('#listado').data('kendoGrid').dataSource.query({ mes: $("#cb1").val(), anio: $("#cb2").val(), moneda: $("#cb3").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
       
    });

});

function loadData(){
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TASASCAMBIO/usp_listar_TasaCambioJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { mes: $("#cb1").val(), anio:  $("#cb2").val(), moneda: $("#cb3").val() })
                }
            },
            schema: { data: "CURRENCYVALUES", total: 'TotalVirtual' }
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
                type: "date",
                editable: "false",
                field: "CUR_DATE", width: 50, title: "FECHA",
                template: "<a id='single_2' href='../TASASCAMBIO/Edit/${kendo.toString(kendo.parseDate(CUR_DATE,'dd-MM-yyyy'),'dd-MM-yyyy')}'style='color:gray !important;'>" + '#=(CUR_DATE==null)?"":kendo.toString(kendo.parseDate(CUR_DATE,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a>"
            },
            {
                hidden: true,                
                field: "CUR_ALPHA", width: 20, title: "<font size=2px>MONEDA</font>", template: "<a id='single_2' href='../TASASCAMBIO/Edit/${kendo.toString(kendo.parseDate(CUR_DATE,'dd-MM-yyyy'),'dd-MM-yyyy')}' style='color:gray !important;'>${CUR_ALPHA}</a>"
            },
            {
                field: "CUR_VALUE",
                width: 90,
                title: "VALOR",
                template: "<a id='single_2' href='../TASASCAMBIO/Edit/${kendo.toString(kendo.parseDate(CUR_DATE,'dd-MM-yyyy'),'dd-MM-yyyy')}'style='color:gray !important;'>${CUR_VALUE}</a>"
            }
        ]
    });

}
