$(function () {
    $("#btnBusqueda").on("click", function () {
        loadData();
        //$('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), terr: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        loadData();
        //$('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), terr: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").focus();
    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            loadData();
            //$('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), terr: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TIPOSDIVISIONES/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    loadComboTerritorio(0);
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../TIPOSDIVISIONES/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {
    var idTer = $("#ddlTerritorio").val() == null ? 604 : $("#ddlTerritorio").val();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../TIPOSDIVISIONES/usp_listar_TipoDivisionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), terr: idTer, st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "REFDIVTYPE", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
            [
                {
                    title: 'Eliminar', width: 10, template: "<input type='checkbox' class='kendo-chk' id='vehicle' name='vehicle' value='${DAD_TYPE}'/>"
                },
                {
                    hidden: true,
                    field: "OWNER", width: 10, title: "Id", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${OWNER}</a>"
                },
                {
                    field: "DAD_TYPE", width: 10, title: "Código", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${DAD_TYPE}</a>"
                },
                {
                    field: "DAD_TNAME", width: 50, title: "Descripción", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${DAD_TNAME}</a>"
                },
                {
                    hidden: true,
                    field: "TIS_N", width: 30, title: "Id Territorio", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${TIS_N}</a>"
                },
                {
                    field: "NAME_TER", width: 40, title: "Territorio", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${NAME_TER}</a>"
                },
                {
                    field: "DIVT_OBSERV", width: 70, title: "Observación", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>${DIVT_OBSERV}</a>"
                },
                { field: "Activo", width: 10, title: "Estado", template: "<a  id='single_2'  href=javascript:editar('${DAD_TYPE}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
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

        alert("Estados actualizado correctamente.");
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), terr: $("#ddlTerritorio").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

    }
}

function FuncionEliminar(id) {
    $.ajax({
        url: '../TIPOSDIVISIONES/Eliminar',
        type: 'POST',
        data: { Id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#txtBusqueda").val("");
    $("#ddlEstado").val(1);
    loadComboTerritorio(0);
}