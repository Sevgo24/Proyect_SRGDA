$(function () {
    $("#txtDescripcion").focus();

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) 
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(1), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoenvioFactura/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(1), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadEstadosMaestro("ddlEstado");

    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

function editar(idSel) {
    document.location.href = '../TipoenvioFactura/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
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
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TipoenvioFactura/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "TipoenvioFactura", total: 'TotalVirtual' }
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
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_SEND}'/>"
            },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "Propietario", template: "<a id='single_2'  href=javascript:editar('${LIC_SEND}') style='color:gray !important;'>${OWNER}</a>"
            },
            {
                field: "LIC_SEND", width: 7, title: "Id", template: "<font color='green'><a id='single_2'  href=javascript:editar('${LIC_SEND}') style='color:gray !important;'>${LIC_SEND}</a></font>"
            },
            {
                field: "LIC_FSEND", width: 100, title: "Descripción", template: "<font color='green'><a id='single_2'  href=javascript:editar('${LIC_SEND}') style='color:gray !important;'>${LIC_FSEND}</a></font>"
            },            
            { field: "Activo", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${LIC_SEND}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIO#}#  </a></font>" }
           ]
    });
}

function FuncionEliminar(id) {
    var id = { codigo: id };
    $.ajax({
        url: '../TipoenvioFactura/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Registro eliminado correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
}