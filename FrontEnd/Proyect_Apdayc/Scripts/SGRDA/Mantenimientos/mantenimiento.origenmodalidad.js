
/************************** INICIO CARGA********************************************/
$(function () {
    $("#txtOrigen").focus();

    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOrigen").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtOrigen").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOrigen").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOrigen").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../OrigenModalidad/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadEstadosMaestro("ddlEstado");
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
                    url: '../OrigenModalidad/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtOrigen").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListaOrigenModalidad', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOD_ORIG}'/>" },
				{ field: "MOD_ORIG", width: 10, title: "ID", template: "<a id='single_2'  href=javascript:editar('${MOD_ORIG}') style='color:gray !important;'>${MOD_ORIG}</a>" },
				{ field: "MOD_ODESC", width: 80, title: "Origen", template: "<a id='single_2'  href=javascript:editar('${MOD_ORIG}') style='color:gray !important;'>${MOD_ODESC}</a>" },
				//{ field: "ESTADO", width: 10, hidden: true, title: "<font size=2px>Estado</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ORIG}') style='color:gray !important;'>${ESTADO}</a>" },
                {field: "ESTADO", width: 12, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ORIG}') style='color:gray !important;'>${ESTADO}</a></font>"}

			]
    });
}

function editar(idSel) {
    document.location.href = '../OrigenModalidad/nuevo?id=' + idSel;
}

function limpiar() {
    $("#txtOrigen").val('');
    $("#chkEnds").prop('checked', false);
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
        alert("Seleccione un Origen.");
    } else {
        var chkends = '';
        if ($('#chkEnds').is(':checked')) {
            chkends = true;
        } else {
            chkends = false;
        }
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOrigen").val(), ends: chkends, page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../OrigenModalidad/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Origen(es) eliminado(s) correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


