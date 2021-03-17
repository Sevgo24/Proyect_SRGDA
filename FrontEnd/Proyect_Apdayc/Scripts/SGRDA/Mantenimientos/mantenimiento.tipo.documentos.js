$(function () {
    $("#txtBusqueda").focus();

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

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../DOCUMENTOSTIPO/Nuevo";
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../DOCUMENTOSTIPO/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

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
                { title: 'Eliminar', width: 8, template: "<input type='checkbox' class='kendo-chk' id='vehicle' name='vehicle' value='${DOC_TYPE}'/>" },
                { field: "DOC_TYPE", width: 7, title: "Id", template: "<a  id='single_2'  href=javascript:editar('${DOC_TYPE}') style='color:gray !important;'>${DOC_TYPE}</a>" },
                { field: "OWNER", hidden: true, width: 150, title: "<font size=2px>Propietario</font>", template: "<a  id='single_2'  href=javascript:editar('${DOC_TYPE}') style='color:gray !important;'>${OWNER}</a>" },
                { field: "DOC_DESC", width: 50, title: "Descripción", template: "<a  id='single_2'  href=javascript:editar('${DOC_TYPE}') style='color:gray !important;'>${DOC_DESC}</a>" },
                { field: "DOC_OBSERV", width: 100, title: "Observación", template: "<a  id='single_2'  href=javascript:editar('${DOC_TYPE}') style='color:gray !important;'>${DOC_OBSERV}</a>" },
                { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${DOC_TYPE}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" },
            ]
    });
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            FuncionEliminar(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

    }
}

function FuncionEliminar(id) {
    $.ajax({
        url: '../DOCUMENTOSTIPO/Eliminar',
        type: 'POST',
        data: { Id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
                $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#txtBusqueda").val('');
    $("#estado").val(1);
}