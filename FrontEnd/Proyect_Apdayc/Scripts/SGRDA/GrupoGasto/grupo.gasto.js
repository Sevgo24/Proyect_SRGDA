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

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            alert("Seleccione un grupo para eliminar.");
        } else {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            alert("grupo actualizado correctamente.");
        }
    });
    loadEstadosMaestro("ddlEstado");
    loadTipoGasto();
    loadData();
});

function loadTipoGasto() {

    $.ajax({
        url: "../General/ListarTipoGasto",
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlTipoGasto').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                var tipo = $("#ddlTipoGasto").val();
                $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            }
            else {
                alert(dato.message);
            }
        }
    });
}

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../GrupoGasto/Listar_PageJson_GrupoGasto",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoGasto").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "GrupoGasto", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${EXPG_ID}'/>"
               },
            { field: "RowNumber", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${EXPG_ID}') style='color:gray !important;'>${RowNumber}</a>" },
            { field: "EXPT_DESC", width: 15, title: "Tipo de Gasto", template: "<a id='single_2' href=javascript:editar('${EXPG_ID}') style='color:gray !important;'>${EXPT_DESC}</a>" },
            { field: "EXPG_DESC", width: 60, title: "Grupo de Gasto", template: "<font color='green'><a id='single_2' href=javascript:editar('${EXPG_ID}') style='color:gray !important;'>${EXPG_DESC}</a>" },
            { field: "Activo", width: 12, title: "Estado ", template: "<font color='green'><a id='single_2' href=javascript:editar('${EXPG_ID}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a>" }
           ]
    });
}

function limpiar() {
    $("#ddlPGrupoGasto").val(0);
    $("#txtDescripcion").val("");
}

function editar(idSel) {
    location.href = "../GrupoGasto/Edit?set=" + idSel;
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../GrupoGasto/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadData();
            }
        }
    });
}