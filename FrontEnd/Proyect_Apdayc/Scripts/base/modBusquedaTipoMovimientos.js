/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;


$(function () {
   
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
        $("#txtBusqueda").focus();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    loadEstadosMaestro("ddlEstado");    
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
                    url: "../TIPOSMOVIMIENTO/usp_listar_tipoMovimientoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECMOVTYPE", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
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
                title: 'Eliminar', width: 7, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${OWNER},${MOV_TYPE}'/>"
            },
            {
                field: "MOV_TYPE", width: 5, title: "Id", template: "<a  href='../TIPOSMOVIMIENTO/Edit/${OWNER},${MOV_TYPE}'style='color:5F5F5F;text-decoration:none;'>${MOV_TYPE}</a>"
            },
            {
                hidden: true,
                field: "OWNER", width: 50, title: "Propietario", template: "<a  href='../TIPOSMOVIMIENTO/Edit/${OWNER},${MOV_TYPE}' style='color:5F5F5F;text-decoration:none;'>${OWNER}</a>"
            },
            {
                field: "MOV_DESC", width: 70, title: "Descripción", template: "<a  href='../TIPOSMOVIMIENTO/Edit/${OWNER},${MOV_TYPE}'style='color:5F5F5F;text-decoration:none;'>${MOV_DESC}</a>"
            },
            {
                field: "MOV_SIGN ", width: 5, title: "Signo", template: "<a  href='../TIPOSMOVIMIENTO/Edit/${OWNER},${MOV_TYPE}'style='color:5F5F5F;text-decoration:none;'>${MOV_SIGN}</a>"
            },
            { field: "Activo", width: 6, title: "Estado", template: "<a  href='../TIPOSMOVIMIENTO/Edit/${OWNER},${MOV_TYPE}' style='color:5F5F5F;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
            ]
    })
}


$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el tipo de movimiento?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        MOV_TYPE: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../TIPOSMOVIMIENTO/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../TIPOSMOVIMIENTO/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});