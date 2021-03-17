
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {                                                        
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtEtiqueta").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../State/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    loadEstadosMaestro("ddlEstado");
    LoadTipoEstado("ddlTipo"); 
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
                    url: '../State/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListarEstados', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_SID}'/>" },
				{ field: "WRKF_SID  ", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${WRKF_SID}</a>" },
				{ field: "WRKF_SNAME", width: 14, title: "Descipción", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${WRKF_SNAME}</a>" },
				{ field: "WRKF_SLABEL", width: 8, title: "Etiqueta", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${WRKF_SLABEL}</a>" },
                { field: "WRKF_STID", hidden: true, width: 8, title: "Cod. Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${WRKF_STID}</a>" },
                { field: "WRKF_STNAME", width: 10, title: "Tipo Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${WRKF_STNAME}</a>" },
                { field: "LOG_USER_CREAT", width: 6, title: "Usuario Crea.", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                //{ field: "LOG_USER_CREAT", width: 7, title: "<font size=2px>Usuario </font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_SID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../State/nuevo?id=' + idSel;
}

function limpiar() {    
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
    $("#txtEtiqueta").val('');
    $("#ddlEstado").val(1);
    $("#ddlTipo").val(0);
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
        alert("Estados actualizados correctamente.");
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtDescripcion").val(), etiqueta: $("#txtEtiqueta").val(), idTipoEstado: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };
    $.ajax({
        url: '../State/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


