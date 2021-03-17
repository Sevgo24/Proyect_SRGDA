

/************************** INICIO CARGA ********************************************/
$(function () {
    $("#txtDescripcion").focus();
    loadComboTerritorio(-1);
    $("#ddlTerritorio").append($("<option>Some Text</option>").val(-1).html("<--Seleccione-->"));
    
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), territorio: $("#ddlTerritorio").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), territorio: $("#ddlTerritorio").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), territorio: $("#ddlTerritorio").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../ImpuestoValor/Nuevo';
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
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: '../ImpuestoValor/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), territorio: $("#ddlTerritorio").val(), estado: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'RECTAXES', total: 'TotalVirtual' }
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
                 { title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${TAX_ID}'/>" },
                 { field: "TAX_ID", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${TAX_ID}</a>" },
                 { field: "TAX_COD", width: 5, title: "Desc. Corta", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${TAX_COD}</a>" },
                 { field: "DESCRIPTION", width: 13, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DESCRIPTION}</a>" },
                 { field: "TAX_CACC", width: 9, title: "Cuenta", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${TAX_CACC}</a>" },                 
                 { field: "START", type: "date", width: 7, title: "Fecha de Vigencia", template: "<a id='single_2' href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(START==null)?"":kendo.toString(kendo.parseDate(START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
                 { field: "NAME_TER", width: 9, title: "Territorio", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${NAME_TER}</a>" },
                 { field: "ESTADO", width: 5, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${TAX_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a>" },                 
		     ]
    });
}

function editar(idSel) {
    document.location.href = '../ImpuestoValor/nuevo?id=' + idSel;
}

function limpiar() {
    $("#txtDescripcion").val('');
    $("#txtDescripcion").focus();
    $("#ddlTerritorio").val(0);
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
            delImpuesto(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un impuesto.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), territorio: $("#ddlTerritorio").val(), estado: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function delImpuesto(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../ImpuestoValor/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Impuesto(s) eliminado(s) correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}



