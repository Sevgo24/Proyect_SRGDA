/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvTipoGasto";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "../RangoMorosidad/Nuevo";
    });

    $("#btnBuscar").on("click", function () { loadData(); limpiar(); });

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

    $("#txtDescripcion").focus();
    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
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
                    url: "../RangoMorosidad/Listar_PageJson_RangoMorosidad",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RangoMorosidad", total: 'TotalVirtual' }
        },
        groupable: false,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        sortable: false,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${RANGE_COD}'/>"
               },
            { field: "RANGE_COD", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${RANGE_COD}') style='color:gray !important;'>${RANGE_COD}</a>" },
            { field: "DESCRIPTION", width: 100, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${RANGE_COD}') style='color:gray !important;'>${DESCRIPTION}</a></font>" },
            { field: "RANGE_FROM", width: 8, title: "Inicio", template: "<a id='single_2'  href=javascript:editar('${RANGE_COD}') style='color:gray !important;'>${RANGE_FROM}</a></font>" },
            { field: "RANGE_TO", width: 8, title: "Fin", template: "<a id='single_2'  href=javascript:editar('${RANGE_COD}') style='color:gray !important;'>${RANGE_TO}</a></font>" },
            { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${RANGE_COD}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
           ]
    });
}

function editar(idSel) {
    location.href = "../RangoMorosidad/Nuevo?set=" + idSel;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../RangoMorosidad/Eliminar',
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

function limpiar() {
    $("#txtDescripcion").val("");
}