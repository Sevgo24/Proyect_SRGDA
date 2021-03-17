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

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TIPOSDIRECCIONES/Nuevo";
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../TIPOSDIRECCIONES/Nuevo?id=' + idSel;
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
                    url: "../TIPOSDIRECCIONES/usp_listar_TiposDireccionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "REFADDRESSTYPE", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        sortable: true,
        selectable: true,
        columns:
        [
         { title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${ADDT_ID}'/>" },
         { field: "OWNER", hidden: true, width: 10, title: "PROPIETARIO", template: "<a id='single_2'  href=javascript:editar('${ADDT_ID}') style='color:gray !important;'>${OWNER}</a></font>" },
         { field: "ADDT_ID", width: 10, title: "Id", template: "<a id='single_2'  href=javascript:editar('${ADDT_ID}') style='color:gray !important;'>${ADDT_ID}</a></font>" },
         { field: "DESCRIPTION", width: 60, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${ADDT_ID}') style='color:gray !important;'>${DESCRIPTION}</a></font>" },
         { field: "ADDT_OBSERV", width: 100, title: "Observación", template: "<a id='single_2'  href=javascript:editar('${ADDT_ID}') style='color:gray !important;'>${ADDT_OBSERV}</a></font>" },
         { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${ADDT_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }

        ]
    });
}

function limpiar() {
    $("#txtBusqueda").val("");
    $("#estado").val(1);
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
        url: '../TIPOSDIRECCIONES/Eliminar',
        type: 'POST',
        data: { Id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}