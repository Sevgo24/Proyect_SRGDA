
/************************** INICIO CARGA********************************************/
$(function () {
	//-------------------------- EVENTO BOTONES ------------------------------------    
	$("#btnBuscar").on("click", function (e) {
	    $('#grid').data('kendoGrid').dataSource.query({ idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEvento").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
	});

	$("#txtCliente").keypress(function (e) {
		if (e.which == 13)
		    $('#grid').data('kendoGrid').dataSource.query({ idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEvento").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
	});

	$("#btnLimpiar").on("click", function (e) {
	    limpiar();	                                                    
	    $('#grid').data('kendoGrid').dataSource.query({ idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEvento").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
	});

	$("#btnNuevo").on("click", function () {
	    document.location.href = '../Transiciones/Nuevo';
	});

	$("#btnEliminar").on("click", function (e) {
		eliminar();
	});
	loadEstadosMaestro("ddlEstado");
	LoadModuloNombre('ddlCliente', 0);
	LoadCicloAprobacion('ddlCiclo');
	LoadEventos('ddlEvento');
	loadEstadoWF('ddlOrigen',0,1);
	loadEstadoWF('ddlDestino', 0, 1);

	$('#ddlOrigen').prop('disabled', true).css('border', '1px solid gray');;
	$('#ddlDestino').prop('disabled', true).css('border', '1px solid gray');;

    $("#ddlCiclo").change(function () {
        var id = $('option:selected', this).val();
        
        if (id == 0)
        {
            $('#ddlOrigen').prop('disabled', true).val(0);
            $('#ddlDestino').prop('disabled', true).val(0);
        } else {
            
            loadEstadoWF('ddlOrigen', 0, id);
            loadEstadoWF('ddlDestino', 0, id);
            $('#ddlOrigen').prop('disabled', false);
            $('#ddlDestino').prop('disabled', false);
        }    

    });

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
					url: '../Transiciones/Listar',
					dataType: "json"
				},
				parameterMap: function (options, operation) {
				    return $.extend({}, options, { idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEvento").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val() })
				}
			},
			schema: { data: 'ListarTransiciones', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_TID}'/>" },
				{ field: "WRKF_TID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${WRKF_TID}</a>" },
				{ field: "WRKF_NAME", width: 10, title: "Ciclo", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${WRKF_NAME}</a>" },
				{ field: "WRKF_ENAME", width: 10, title: "Evento", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${WRKF_ENAME}</a>" },
                { field: "ESTADO_INI", width: 10, title: "Estado Desde", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${ESTADO_INI}</a>" },
                { field: "ESTADO_FIN", width: 10, title: "Estado Hasta", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${ESTADO_FIN}</a>" },
                { field: "LOG_USER_CREAT", width: 6, title: "Usuario Crea.", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 4, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_TID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
	});
}

function editar(idSel) {
    document.location.href = '../Transiciones/nuevo?id=' + idSel;
}

function limpiar() {
    $("#ddlCiclo").val(0);
    $("#ddlEvento").val(0);
    $("#ddlOrigen").val(0);
    $("#ddlDestino").val(0);
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
	    alert("Estados actualizado correctamente.");
	    $('#grid').data('kendoGrid').dataSource.query({ idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEvento").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
	}
}

function delOrigen(idOri) {
	var codigoDel = { id: idOri };

	$.ajax({
	    url: '../Transiciones/eliminar',
		type: 'POST',
		data: codigoDel,
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


