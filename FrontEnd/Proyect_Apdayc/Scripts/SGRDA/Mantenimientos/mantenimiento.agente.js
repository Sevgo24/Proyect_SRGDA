/************************** INICIO CARGA********************************************/
$(function () {
    //---------------------------------------------------------------------------------
    $.ajaxSetup({ cache: false });

    $(".openDialog").live("click", function (e) {
        e.preventDefault();

        $("<div></div>")
            .addClass("dialog")
            .attr("id", $(this)
            .attr("data-dialog-id"))
            .appendTo("body")
            .dialog({
                title: $(this).attr("data-dialog-title"),
                close: function () { $(this).remove(); },
                modal: true,
                height: 480,
                width: 400,
                left: 0

            })
        .load(this.href);
    });

    $(".close").live("click", function (e) {
        e.preventDefault();
        $(this).closest(".dialog").dialog("close");
    });

    //-------------------------- EVENTO BOTONES ------------------------------------   
    $("#btnBuscar").on("click", function (e) {
        $('#gridAgente').data('kendoGrid').dataSource.query({ dato: $("#txtNivel").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#gridAgente').data('kendoGrid').dataSource.page(1);
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../agente/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });

    //-------------------------- EVENTO CONTROLES ------------------------------------   
    $("#txtNivel").keypress(function (e) {
        if (e.which == 13) {
            $('#gridAgente').data('kendoGrid').dataSource.query({ dato: $("#txtNivel").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });
    $("#txtNivel").focus();
    //-------------------------- CARGA LISTA ------------------------------------   
    loadEstadosMaestro("ddlEstado");
    loadData();

});

//****************************  FUNCIONES ****************************
function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../Agente/ListarAgentes",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtNivel").val(), st: $("#ddlEstado").val() })
            }
        },
        schema: { data: "ListaAgente", total: 'TotalVirtual' }
    });

    $("#gridAgente").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns: [
                { title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LEVEL_ID}'/>" },
				{ field: "LEVEL_ID", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${LEVEL_ID}') style='color:gray !important;'>${LEVEL_ID}</a>" },
				{ field: "DESCRIPTION", width: 40, title: "Nivel", template: "<a id='single_2'  href=javascript:editar('${LEVEL_ID}') style='color:gray !important;'>${DESCRIPTION}</a>" },
				{ field: "LEVEL_DEP", hidden: true, width: 10, title: "LEVEL_DEP", template: "<a id='single_2'  href=javascript:editar('${LEVEL_ID}') style='color:gray !important;'>${LEVEL_DEP}</a>" },
				{ field: "DEPENDENCIA", width: 40, title: "Dependencia", template: "<a id='single_2'  href=javascript:editar('${LEVEL_ID}') style='color:gray !important;'>${DEPENDENCIA}</a>" },
				{ field: "ESTADO", width: 9, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${LEVEL_ID}') style='color:gray !important;'>#if(ESTADO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });
};



function editar(idSel) {
    document.location.href = '../Agente/nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
    limpiar();
}

function limpiar() {
    $("#txtNivel").val('');
    $("#txtNivel").focus();
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
            delNivel(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un nivel.");
    } else {
        $('#gridAgente').data('kendoGrid').dataSource.query({ dato: $("#txtNivel").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        alert("Nivel(es) eliminado(s) correctamente.");
        loadData();
    }
}

function delNivel(id) {
    var codigoDel = { idNivel: id };
    $.ajax({
        url: '../Agente/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}