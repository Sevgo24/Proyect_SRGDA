
$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), serie: '', tipo: '0', page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), serie: '' ,tipo:'0', page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        $("#txtBusqueda").focus();
    });

    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(),serie:'',tipo:'0', page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#txtBusqueda").focus();
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
                    url: "../CORRELATIVOS/usp_listar_CorrelativosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(),serie:'',tipo:'0'})
                }
            },
            schema: { data: "RECNUMBERING", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
        [
            {
                title: 'Eliminar', width: 12, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${NMR_ID}'/>"
            }, {
                hidden: true,
                field: "OWNER", width: 100, title: "PROPIETARIO", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${OWNER}</a>"
            }, {
                
                field: "NMR_ID", width: 9, title: "Id", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}' style='color:gray;'>${NMR_ID}</a>"
            }, {
                hidden: true,
                field: "NMR_TYPE", width: 25, title: "Tipo", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_TYPE}</a>"
            }, {
                field: "NMR_TDESC", width: 20, title: "Tipo", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_TDESC}</a>"
            }, {
                field: "NMR_SERIAL", width: 15, title: "Serial", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_SERIAL}</a>"
            }, {
                field: "NMR_NAME", width: 70, title: "Nombre", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_NAME}</a>"
            }, {
                field: "NMR_FORM", width: 20, title: "Inicio rango", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_FORM}</a>"
            }, {
                field: "NMR_TO", width: 20, title: "Fin Rango", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}'style='color:gray;'>${NMR_TO}</a>"
            },
            { field: "ACTIVO", width: 12, title: "Estado", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${NMR_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar correlativo?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        NMR_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../CORRELATIVOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../CORRELATIVOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}
