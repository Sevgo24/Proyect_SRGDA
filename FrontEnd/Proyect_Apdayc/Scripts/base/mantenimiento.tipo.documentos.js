/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;
$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").focus();
    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
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
                    url: "../DOCUMENTOSTIPO/usp_listar_DocumentosTipoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECDOCUMENTTYPE", total: 'TotalVirtual' }
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
                    title: 'Eliminar', width: 8, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${DOC_TYPE}'/>"
                },
                {
                    field: "DOC_TYPE", width: 7, title: "Id", template: "<a  href='../DOCUMENTOSTIPO/Edit/${OWNER},${DOC_TYPE}'style='color:gray !important;'>${DOC_TYPE}</a>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 150, title: "<font size=2px>Propietario</font>", template: "<a  href='../DOCUMENTOSTIPO/Edit/${OWNER},${DOC_TYPE}' style='color:gray !important;'>${OWNER}</a>"
                },
                {
                    field: "DOC_DESC", width: 50, title: "Descripción", template: "<a  href='../DOCUMENTOSTIPO/Edit/${OWNER},${DOC_TYPE}'style='color:gray !important;'>${DOC_DESC}</a>"
                },
                {
                    field: "DOC_OBSERV", width: 100, title: "Observación", template: "<a  href='../DOCUMENTOSTIPO/Edit/${OWNER},${DOC_TYPE}'style='color:gray !important;'>${DOC_OBSERV}</a>"
                },
                { field: "ACTIVO", width: 12, title: "Estado", template: "<a  href='../DOCUMENTOSTIPO/Edit/${OWNER},${DOC_TYPE}'style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
            ]
    });
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar documento?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        DOC_TYPE: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../DOCUMENTOSTIPO/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../DOCUMENTOSTIPO/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val('');
}