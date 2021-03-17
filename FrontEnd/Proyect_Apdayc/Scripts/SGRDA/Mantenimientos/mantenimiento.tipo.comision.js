$(function () {
    $("#txtDescripcion").focus();
    loadEstadosMaestro("ddlEstado");

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoComision/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TipoComision/ListarTipocomision",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "listaComision", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            { title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${COMT_ID}'/>" },
            { field: "COMT_ID", width: 10, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COMT_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${COMT_ID}</a></font>" },
            { field: "COM_DESC", width: 100, title: "<font size=2px>Repertorio</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COMT_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${COM_DESC}</a></font>" },
            { field: "ESTADO", width: 10, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${COMT_ID}') style='color:gray !important;'>${ESTADO}</a></font>" }
           ]
    });
}

function editar(idSel) {
    document.location.href = '../TipoComision/Nuevo?id=' + idSel;
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

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../TipoComision/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
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