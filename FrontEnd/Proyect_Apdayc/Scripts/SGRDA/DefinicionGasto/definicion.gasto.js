/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvDefinicionGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    
    $("#btnNuevo").on("click", function () {
        location.href = "../DefinicionGasto/Nuevo";
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadTipoGasto("ddlTipoGasto", 0);
    loadGrupoGasto("ddlGrupoGasto", $("#ddlTipoGasto").val(), 0);

    
    $("#ddlTipoGasto").on("change", function () {
        $('#ddlGrupoGasto option').remove();
        var tipo = $("#ddlTipoGasto").val();
        loadGrupoGasto("ddlGrupoGasto", tipo, 0);
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        $('#ddlGrupoGasto').append($("<option />", { value: 0, text: '--Seleccione--' }));
        $("#txtDescripcion").val("");
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnEliminar").on("click", function () {
        var values = [];
        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk').attr('checked');
            if (checked == "checked") {
                var codigoTipo = $row.find('.kendo-chk').attr('value');
                values.push(codigoTipo);
                eliminar(codigoTipo);
            }
        });
        if (values.length == 0) {
            alert("Seleccione un tipo para eliminar.");
        } else {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            
        }
    });
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../DefinicionGasto/Listar_PageJson_DefinicionGasto",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "DefinicionGasto", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${EXP_ID}'/>"
               },
            { field: "RowNumber", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:editar('${EXP_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RowNumber}</a>" },
            { field: "EXPT_DESC", width: 13, title: "Tipo de Gasto", template: "<a id='single_2' href=javascript:editar('${EXP_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EXPT_DESC}</a>" },
            { field: "EXPG_DESC", width: 16, title: "Grupo de Gasto", template: "<font color='green'><a id='single_2' href=javascript:editar('${EXP_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EXPG_DESC}</a>" },
            { field: "EXP_DESC", width: 25, title: "Gasto", template: "<font color='green'><a id='single_2' href=javascript:editar('${EXP_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EXP_DESC}</a>" },
            { field: "Activo", width: 5, title: "Estado ", template: "<font color='green'><a id='single_2' href=javascript:editar('${EXP_ID}') style='color:gray;text-decoration:none;font-size:11px'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a>" }
           ]
    });
}

function limpiar() {

    $("#txtPDescripcion").val("");
    $("#txtPDesCorta").val("");
}

function editar(idSel) {
    location.href = "../DefinicionGasto/Edit?set=" + idSel;
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../DefinicionGasto/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), grupo: $("#ddlGrupoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}