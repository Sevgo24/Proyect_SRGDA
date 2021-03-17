
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ cliente: $("#txtCliente").val(), etiqueta: $("#txtEtiqeta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtCliente").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ cliente: $("#txtCliente").val(), etiqueta: $("#txtEtiqeta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ cliente: $("#txtCliente").val(), etiqueta: $("#txtEtiqeta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../ModuloCliente/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    loadEstadosMaestro("ddlEstado");
    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

//****************************  FUNCIONES ****************************
function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: '../ModuloCliente/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                        return $.extend({}, options, { cliente: $("#txtCliente").val(), etiqueta: $("#txtEtiqeta").val(), estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListarModuloCliente', total: 'TotalVirtual' }
        },
        groupable: false,
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
				{ title: 'Eliminar', width: 2, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${PROC_MOD}'/>" },
				{ field: "PROC_MOD", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${PROC_MOD}') style='color:gray !important;'>${PROC_MOD}</a>" },
				{ field: "MOD_DESC", width: 10, title: "Nombre Cliente", template: "<a id='single_2'  href=javascript:editar('${PROC_MOD}') style='color:gray !important;'>${MOD_DESC}</a>" },
				{ field: "MOD_CLABEL", width: 10, title: "Etiqueta Cliente", template: "<a id='single_2'  href=javascript:editar('${PROC_MOD}') style='color:gray !important;'>${MOD_CLABEL}</a>" },
                { field: "LOG_USER_CREAT", width: 10, title: "Usuario", template: "<a id='single_2'  href=javascript:editar('${PROC_MOD}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${PROC_MOD}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../ModuloCliente/nuevo?id=' + idSel;
}              

function limpiar() {
    $("#txtCliente").val('');
    $("#txtEtiqeta").val('');
    $("#txtCliente").focus();
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
            delOrigen(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una registro.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ cliente: $("#txtCliente").val(), etiqueta: $("#txtEtiqeta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../ModuloCliente/eliminar',
        type: 'POST',
        data: codigoDel,
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


