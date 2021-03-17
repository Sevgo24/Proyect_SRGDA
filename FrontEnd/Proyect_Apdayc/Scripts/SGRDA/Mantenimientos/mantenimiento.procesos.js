$(function () {
    limpiar();
    loadTipoproceso("ddlTipoproceso");    
    loadModulo("ddlModulo");
    loadEstadosMaestro("ddlEstado");
    LoadCicloAprobacion('ddlCiclo');
    LoadModuloNombre("ddlCliente");
    loadData();

    $("#txtDescripcion").focus();
    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../Procesos/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
});

function editar(idSel) {
    document.location.href = '../Procesos/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../Procesos/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "ListaProceso", total: 'TotalVirtual' }
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
            { title: 'Eliminar', width: 40, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${PROC_ID}'/>" },
            { field: "PROC_ID",  width: 35, title: "Id", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_ID}</a>" },
            { field: "PROC_NAME", width: 100, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_NAME}</a></font>" },
            { field: "PROC_TYPE", hidden: true, width: 50, title: "Tipo Id", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_TYPE}</a></font>" },
            { field: "PROC_TDESC", width: 100, title: "Tipo Procesos", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_TDESC}</a>" },
            { field: "WRKF_NAME", width: 100, title: "Workflow", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${WRKF_NAME}</a>" },
            { field: "MOD_DESC", width: 100, title: "Módulo", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${MOD_DESC}</a>" },
            { field: "LOG_USER_CREAT", width: 100, title: "Usuario Crea.", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
             //{ field: "MOG_ID", hidden: true, width: 50, title: "Modalidad Id", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${MOG_ID}</a></font>" },
            //{ field: "MOG_DESC", width: 100, title: "Modalidad", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${MOG_DESC}</a></font>" },
            //{ field: "PROC_MOD", hidden: true, width: 50, title: "Modulo Id", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_MOD}</a></font>" },
            //{ field: "MOD_DESC", width: 100, title: "Modulo", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${MOD_DESC}</a>" },           
            
            //{ field: "PROC_JOBS", width: 100, title: "Nro Ejecuciones", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${PROC_JOBS}</a>" },
            { field: "ESTADO", width: 50, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${PROC_ID}') style='color:gray !important;'>${ESTADO}</a></font>" },
           ]
    });
}

function limpiar() {
    $("#txtDescripcion").val('');
    $("#ddlTipoproceso").val(0);
    $("#ddlCiclo").val(0);
    $("#ddlCliente").val(0);
    $("#ddlEstado").val(1);
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
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), tipo: $("#ddlTipoproceso").val(), ciclo: $("#ddlCiclo").val(), cliente: $("#ddlCliente").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../Procesos/Eliminar',
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