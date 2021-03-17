/*INICIO CONSTANTES*/
var K_WIDTH = 1000;
var K_HEIGHT = 450;
var K_WIDTH_DEP = 670;
var K_HEIGHT_DEP = 490;
var K_WIDTH_OBS = 550;
var K_HEIGHT_OBS = 372;
var K_SIZE_PAGE = 8;


/************************** INICIO CARGA********************************************/
$(function () {    
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
                height: 550,
                width: 400,
                left: 0

            })
        .load(this.href);
    });
    $(".close").live("click", function (e) {
        e.preventDefault();
        $(this).closest(".dialog").dialog("close");
    });

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#txtOficinaSearch").focus();
    $("#txtOficinaSearch").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOficinaSearch").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });
    //-------------------------- EVENTO BOTONES ------------------------------------    

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.page(1);
    });

    $("#btnImprimir").on("click", function (e) {
        window.open('../Office/ReporteTreeview');        
    });

    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtOficinaSearch").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });    

    $("#btnEliminar").on("click", function () {
        var values = [];
        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk').attr('checked');
            if (checked == "checked") {
                var codigoUsu = $row.find('.kendo-chk').attr('value');
                values.push(codigoUsu);
                eliminar(codigoUsu);
            }
        });

        if (values.length == 0) {
            alert("Seleccione oficina para eliminar.");
        } else {
            loadData();
            alert("Estado(s) actualizado(s) correctamente.");
        }
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../office/nuevo';
        $("#hidOpcionEdit").val(1);
    });

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
                url: "../Office/Listar",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtOficinaSearch").val(), estado: $("#ddlEstado").val() })
            }
        },
        schema: { data: "BEREC_OFFICE", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
                { title: 'Eliminar', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${OFF_ID}'/>" },
				{ field: "OFF_ID", width: 4, title: "Id", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_ID}</a>" },
				{ field: "SOFF_ID", hidden: true, width: 20, title: "SOFF_ID", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${SOFF_ID}</a>" },
				{ field: "ADD_ID", hidden: true, width: 20, title: "ADD_ID", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADD_ID}</a>" },
				{ field: "OFF_NAME", width: 20, title: "Oficina", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_NAME}</a>" },
                { field: "ADDRESS", width: 20, title: "Dirección", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADDRESS}</a>" },
                { field: "HQ_IND", width: 10, title: "Indicador", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${HQ_IND}</a>" },
                { field: "ENDSDES", width: 8, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ENDSDES}</a>" },
        ]
    });
};

function eliminar(idUsuario) {
    var codigosDel = { codigo: idUsuario };
    $.ajax({
        url: '../Office/Eliminar',
        type: 'POST',
        data: codigosDel,
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

function editar(idSel) {
    document.location.href = '../Office/nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
    limpiar();
}

function limpiar() {
    $("#txtOficinaSearch").val("");
    $("#ddlEstado").val(1);
    $("#txtOficinaSearch").focus();
    $("#chkEnds").prop('checked', false);
}

var agregar = function () {
    var values = [];
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk-dep').attr('checked');
        if (checked == "checked") {
            var codigoUsu = $row.find('.kendo-chk-dep').attr('value');
            values.push(codigoUsu);
            actualizarDep(codigoUsu);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una oficina para agregar.");
    } else {
        loadDataDep();
        alert("Oficina(s) agregada(s) correctamente.");
    }
}

function actualizarDep(id) {
    var soffId = $("#txtOfiCodigo").val();
    var codigosDel = { idOff: id, soff: soffId };
    $.ajax({
        url: '../Office/ActualizarDep',
        type: 'POST',
        data: codigosDel,
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

