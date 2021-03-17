
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                                                       
    });

    $("#txtNombre").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtCodInterno").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Objects/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    loadEstadosMaestro("ddlEstado");
    LoadTipoObjeto("ddlTipo");
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
                    url: '../Objects/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val() , estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListarObject', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 2, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_OID}'/>" },
				{ field: "WRKF_OID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${WRKF_OID}</a>" },
				{ field: "WRKF_ODESC", width: 14, title: "Descipción", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${WRKF_ODESC}</a>" },
				{ field: "WRKF_OINTID", width: 3, title: "Código Interno", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${WRKF_OINTID}</a>" },
                { field: "WRKF_OTDESC", width: 8, title: "Tipo Objeto", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${WRKF_OTDESC}</a>" },
                { field: "LOG_USER_CREAT", width: 7, title: "Usuario", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_OID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../Objects/nuevo?id=' + idSel;
}

function limpiar() {
    $("#txtNombre").val('');
    $("#txtCodInterno").val('');
    $("#txtCliente").focus();
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
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), codInterno: $("#txtCodInterno").val(), idTipoObjeto: $("#ddlTipo").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../Objects/eliminar',
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


