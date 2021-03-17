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
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../Evento/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../Evento/Nuevo?id=' + idSel;
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
                    url: "../Evento/ListarEvento",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "listaEv", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
            { title: 'Eliminar', width: 15, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_EID}'/>" },
            { field: "WRKF_EID", width: 10, title: "Id", template: "<font color='green'><a id='single_2'  href=javascript:editar('${WRKF_EID}') style='color:gray !important;'>${WRKF_EID}</a></font>" },
            { field: "WRKF_ENAME", width: 120, title: "Descripción", template: "<font color='green'><a id='single_2'  href=javascript:editar('${WRKF_EID}') style='color:gray !important;'>${WRKF_ENAME}</a></font>" },
            { field: "WRKF_ELABEL", width: 100, title: "Etiqueta", template: "<font color='green'><a id='single_2'  href=javascript:editar('${WRKF_EID}') style='color:gray !important;'>${WRKF_ELABEL}</a></font>" },
            { field: "ESTADO", width: 15, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${WRKF_EID}') style='color:gray !important;'>${ESTADO}</a></font>" },
           ]
    });
}

function FuncionEliminar(id) {
    var id = { idEvento: id };
    $.ajax({
        url: '../Evento/Eliminar',
        type: 'POST',
        data: id,
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

function limpiar() {
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
}