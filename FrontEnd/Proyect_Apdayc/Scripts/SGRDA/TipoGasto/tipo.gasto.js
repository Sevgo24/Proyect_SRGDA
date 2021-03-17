/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
       
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
                    type: 'POST',
                    url: "../TipoGasto/Listar_PageJson_TipoGasto",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "TipoGasto", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${EXP_TYPE}'/>"
               },
            { field: "RowNumber", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${EXP_TYPE}') style='color:gray !important;'>${RowNumber}</a>" },
            { field: "EXP_TYPE", width: 10, title: "Descrip.Corta", template: "<a id='single_2'  href=javascript:editar('${EXP_TYPE}') style='color:gray !important;'>${EXP_TYPE}</a>" },
            { field: "EXPT_DESC", width: 60, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${EXP_TYPE}') style='color:gray !important;'>${EXPT_DESC}</a>" },
            { field: "Activo", width: 12, title: "Estado ", template: "<a id='single_2'  href=javascript:editar('${EXP_TYPE}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a>" }
           ]
    });
}

function limpiar() {

    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");

    //limpiarValidacion();
}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function editar(idSel) {
    location.href = "../TipoGasto/Edit?set=" + idSel;
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../TipoGasto/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}