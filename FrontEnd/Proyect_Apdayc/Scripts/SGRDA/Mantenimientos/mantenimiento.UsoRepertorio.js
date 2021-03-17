
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
        location.href = "../Usorepertorio/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../Usorepertorio/Nuevo?id=' + idSel;
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
                    url: "../Usorepertorio/ListarUsoRepertorio",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val()})
                }
            },
            schema: { data: "ListaUsoRepertorio", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        //selectable: true,s
        columns:
           [
            {
                title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOD_USAGE}'/>"
            },
            {                                                                      
                hidden: true,
                field: "OWNER", width: 10, title: "Propietario", template: "<a id='single_2'  href=javascript:editar('${MOD_USAGE}') style='color:gray !important;'>${OWNER}</a>"
            },
            {
               
                field: "MOD_USAGE", width: 10, title: "Id", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_USAGE}') style='color:gray !important;'>${MOD_USAGE}</a></font>"
            },
            {
                field: "MOD_DUSAGE", width: 120, title: "Repertorio", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_USAGE}') style='color:gray !important;'>${MOD_DUSAGE}</a></font>"
            }
            ,
            {
                field: "ESTADO", width: 12, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_USAGE}') style='color:gray !important;'>${ESTADO}</a></font>"
            }
           ]
    });
}

function FuncionEliminar(id) {
    var id = { idUsorepertorio: id };
    $.ajax({
        url: '../Usorepertorio/Eliminar',
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