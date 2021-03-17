/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "../Descuentos/Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDescuento").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDescuento").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDescuento").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
           
        }
    });

    $("#txtDescripcion").focus();
    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDescuento").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });
    loadTipoDescuento("ddlTipoDescuento", 0);
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
                    url: "../Descuentos/Listar_PageJson_Descuentos",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoDescuento").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "Descuentos", total: 'TotalVirtual' }
        },
        groupable: false,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        sortable: K_ESTADO_ORDEN,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 9, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${DISC_ID}'/>"
               },
            { field: "DISC_ID", width: 7, title: "Id", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>${DISC_ID}</a>" },
            { field: "TIPO", width: 30, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>${TIPO}</a></font>" },
            { field: "DISC_NAME", width: 70, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>${DISC_NAME}</a></font>" },
            { field: "DISC_PERC", width: 30, title: "Porcentaje Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>${DISC_PERC}</a></font>" },
            { field: "DISC_VALUE", width: 30, title: "Valor Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>${DISC_VALUE}</a></font>" },
            { field: "Activo", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
           ]
    });
}

function editar(idSel) {
    location.href = "../Descuentos/Nuevo?set=" + idSel;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../Descuentos/Eliminar',
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
    $("#txtDescripcion").val('');
    $("#ddlTipoDescuento").val(0)
    $("#ddlEstado").val(1)
    loadEstadosMaestro(1);
}