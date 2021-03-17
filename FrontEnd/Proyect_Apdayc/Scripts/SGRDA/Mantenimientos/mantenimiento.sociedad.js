
/************************** INICIO CARGA********************************************/
$(function () {
    $("#txtSociedad").focus();
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtSociedad").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtSociedad").keypress(function (e) {
        if (e.which == 13) 
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtSociedad").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });    
 
    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtSociedad").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Sociedad/Nuevo';
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
                    url: '../Sociedad/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtSociedad").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListaSociedad', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOG_SOC}'/>" },
				{ field: "MOG_SOC", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${MOG_SOC}') style='color:gray !important;'>${MOG_SOC}</a>" },
				{ field: "MOG_SDESC", width: 80, title: "Sociedad", template: "<a id='single_2'  href=javascript:editar('${MOG_SOC}') style='color:gray !important;'>${MOG_SDESC}</a>" },
				{ field: "ESTADO", width: 10, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${MOG_SOC}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../Sociedad/nuevo?id=' + idSel;
}

function limpiar() {
    $("#txtSociedad").val('');
    $("#chkEnds").prop('checked', false);
    $("#txtSociedad").focus();
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
        alert("Seleccione una sociedad.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtSociedad").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../Sociedad/eliminar',
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


