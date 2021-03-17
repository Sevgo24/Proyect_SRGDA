
$(function () {
    loadEstadosMaestro("ddlEstado");

    $("#txtDescripcion").focus();

    $("#btnBuscar").on("click", function () {
        CargarGrid();
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13)
        { CargarGrid(); }
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        CargarGrid();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../ComisionOrigen/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

function editar(idSel) {
    document.location.href = '../ComisionOrigen/Nuevo?id=' + idSel;
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
    };
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
                    url: "../ComisionOrigen/ListarOrigenComision",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "ListaOrigenComision", total: 'TotalVirtual' }
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
            {
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${COM_ORG}'/>"
            },
            {

                field: "COM_ORG", width: 10, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ORG}') style='color:gray !important;'>${COM_ORG}</a></font>"
            },
            {
                field: "COM_DESC", width: 100, title: "<font size=2px>Descripción</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ORG}') style='color:gray !important;'>${COM_DESC}</a></font>"
            },
            {
                field: "ESTADO", width: 10, title: "<font size=2px>Estado</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${COM_ORG}') style='color:gray !important;'>${ESTADO}</a></font>"
            }
           ]
    });
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../ComisionOrigen/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
                CargarGrid();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#ddlEstado").val(1);
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
}

function CargarGrid() {
    $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
}