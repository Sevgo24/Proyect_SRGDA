/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#txtDescripcion").focus();

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoDerecho/Nuevo";
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13)
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

});

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../TipoDerecho/Listar_PageJson_TipoDerecho",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "TipoDerecho", total: 'TotalVirtual' }
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
               {
                   title: 'Eliminar', width: 6, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${RIGHT_COD}'/>"
               },
            { field: "RowNumber", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${RIGHT_COD}') style='color:gray !important;'>${RowNumber}</a>" },
            { field: "RIGHT_COD", width: 13, title: "Descrip.Corta", template: "<a id='single_2'  href=javascript:editar('${RIGHT_COD}') style='color:gray !important;'>${RIGHT_COD}</a>" },
            { field: "RIGHT_DESC", width: 70, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${RIGHT_COD}') style='color:gray !important;'>${RIGHT_DESC}</a></font>" },
            { field: "Activo", width: 8, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${RIGHT_COD}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
           ]
    });
}

function limpiar() {
    $("#txtDescripcion").val("");
}

function limpiarValidacion() {
    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function editar(idSel) {
    location.href = "../TipoDerecho/Edit?set=" + idSel;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../TipoDerecho/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}