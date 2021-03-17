
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtNombre").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtEtiqueta").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Agent/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    $("#repPDF").on("click", function (e) {
        reporte();
    });

    $("#repEXCEL").on("click", function (e) {
        reporte();
    });
    loadEstadosMaestro("ddlEstado");
    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

//****************************  FUNCIONES ****************************
function reporte(formato) {
    var agente = {
        WRKF_AGNAME: $('#txtNombre').val(),
        WRKF_AGLABEL: $('#txtEtiqueta').val(),
        ID_ESTADO: $('#ddlEstado').val()
    }

    $.ajax({
        url: "../Agent/Reporte",
        type: "POST",
        data: agente,
        success: function (response) {
            //reporteDownload(formato);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });}


function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: '../Agent/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListarAgentes', total: 'TotalVirtual' }
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
				{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_AGID}'/>" },
				{ field: "WRKF_AGID  ", width: 2, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_AGID}') style='color:gray !important;'>${WRKF_AGID}</a>" },
				{ field: "WRKF_AGNAME", width: 14, title: "<font size=2px>Descipción</font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_AGID}') style='color:gray !important;'>${WRKF_AGNAME}</a>" },
				{ field: "WRKF_AGLABEL", width: 8, title: "<font size=2px>Etiqueta</font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_AGID}') style='color:gray !important;'>${WRKF_AGLABEL}</a>" },
                { field: "LOG_USER_CREAT", width: 5, title: "<font size=2px>Usuario Crea.</font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_AGID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },                
                { field: "LOG_DATE_CREAT", type: "date", width: 5, title: "<font size=2px>Fecha Crea.</font>", template: "<a id='single_2' href=javascript:editar('${WRKF_AGID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
                { field: "ESTADO", width: 4, title: "<font size=2px>Estado </font>", template: "<a id='single_2'  href=javascript:editar('${WRKF_AGID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

function editar(idSel) {
    document.location.href = '../Agent/nuevo?id=' + idSel;
}

function limpiar() {
    $("#txtNombre").val('');
    $("#txtNombre").focus();
    $("#txtEtiqueta").val('');
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
        alert("Estados actualizados correctamente.");
        $('#grid').data('kendoGrid').dataSource.query({ nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };
    $.ajax({
        url: '../Agent/eliminar',
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


