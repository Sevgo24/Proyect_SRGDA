
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtCiclo").val(), etiqueta: $("#txtEtiqeta").val(), idCliente: $("#ddlCliente").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtCiclo").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtCiclo").val(), etiqueta: $("#txtEtiqeta").val(), idCliente: $("#ddlCliente").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtCiclo").val(), etiqueta: $("#txtEtiqeta").val(), idCliente: $("#ddlCliente").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                                                      
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../CicloAprobacion/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    loadEstadosMaestro("ddlEstado");
    LoadModuloNombre('ddlCliente', 0);
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
                    url: '../CicloAprobacion/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { nombre: $("#txtCiclo").val(), etiqueta: $("#txtEtiqeta").val(), idCliente: $("#ddlCliente").val(), estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListaFlujo', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 2, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_ID}'/>" },
				{ field: "WRKF_ID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${WRKF_ID}</a>" },
				{ field: "WRKF_NAME", width: 10, title: "Nombre", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${WRKF_NAME}</a>" },
				{ field: "WRKF_LABEL", width: 10, title: "Etiqueta", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${WRKF_LABEL}</a>" },
                { field: "PROC_MOD", width: 10,hidden:true, title: "Id Cliente", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${PROC_MOD}</a>" },
                { field: "MOD_DESC", width: 10, title: "Módulo", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${MOD_DESC}</a>" },
                { field: "LOG_USER_CREAT", width: 7, title: "Usuario Crea.", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_ID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../CicloAprobacion/nuevo?id=' + idSel;
}              

function limpiar() {
    $("#txtCiclo").val('');
    $("#txtEtiqeta").val('');
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
            delOrigen(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un registro.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtCiclo").val(), etiqueta: $("#txtEtiqeta").val(), idCliente: $("#ddlCliente").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../CicloAprobacion/eliminar',
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


