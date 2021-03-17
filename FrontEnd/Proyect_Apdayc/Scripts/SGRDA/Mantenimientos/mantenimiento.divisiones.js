/************************** INICIO CONSTANTES****************************************/
var K_MENSAJE_ELIMIMAR = '¿La subdivision(es) tienen dependencias, desea eliminar?';

/************************** INICIO CARGA********************************************/
$(function () {
    $("#txtNombre").focus();
    loadTipoDivisiones('ddlDivision', 0);

    $("#txtNombre").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadEstadosMaestro("dllEstado");
    
    //-------------------------- EVENTO BOTONES ------------------------------------   
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function (e) {
        document.location.href = '../Divisiones/nuevo';
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtNombre").val('');
        $("#ddlDivision").val(0);
        $("#dllEstado").val(1);
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnEliminar").on("click", function (e) {
        validarEliminar();        
    });

    loadData();
});


//****************************  FUNCIONES ****************************
function loadData() {

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../Divisiones/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val() })
                }
            },
            schema: { data: "REFDIVISIONES", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
				{ title: 'Eliminar', width: 2, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${DAD_ID}'/>" },
                { field: "DAD_ID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DAD_ID}</a>" },
                { field: "DAD_TYPE", width: 3, hidden: true, title: "Tipo Div", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DAD_TYPE}</a>" },
				{ field: "DAD_TNAME", width: 7, title: "Tipo", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DAD_TNAME}</a>" },
                { field: "DAD_NAME", width: 10, title: "División", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DAD_NAME}</a>" },

                { field: "DIV_DESCRIPTION", width: 20, title: "<font size=2px>Descripción</font>", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DIV_DESCRIPTION}</a>" },

                { field: "TIS_N", width: 10, hidden: true, title: "<font size=2px>Id territorio</font>", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${TIS_N}</a>" },
				{ field: "NAME_TER", width: 4, hidden: true, title: "<font size=2px>Territorio</font>", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${NAME_TER}</a>" },                
                { field: "DAD_CODE", width: 5, hidden: true, title: "<font size=2px>Ident. Corta</font>", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${DAD_CODE}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${ESTADO}</a>" },
			]
    });
}

function editar(idSel) {
    document.location.href = '../Divisiones/Editar?id=' + idSel;
    $("#hidOpcionEdit").val(1);
    limpiar();
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            delDivision(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una división.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        alert("Division(es) eliminado(s) correctamente.");
    }
}

function delDivision(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../Divisiones/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}

function validarEliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una división.");
    } else {      
        Confirmar(K_MENSAJE_ELIMIMAR,
                     function () {
                         eliminar();
                         $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlDivision").val(), nombre: $("#txtNombre").val(), estado: $("#dllEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                     },
                     function () {
                     },
                     'Confirmar'
                 );
    }

   
}


function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            OK: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Cancel: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}