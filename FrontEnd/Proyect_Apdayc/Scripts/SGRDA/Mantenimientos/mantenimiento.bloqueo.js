/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvBloqueo";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "../Bloqueo/Nuevo";
    });

    $("#txtDescripcion").focus();
    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
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
            alert("Seleccione un registro para eliminar.");
        } else {
            $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            alert("Estados actualizado correctamente.");
        }
    });
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../Bloqueo/Listar_PageJson_Bloqueos",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
            }
        },
        schema: { data: "Bloqueos", total: 'TotalVirtual' }
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
        selectable: true,
        columns: [
               {
                   title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${BLOCK_ID}'/>"
               },
            { field: "BLOCK_ID", width: 6, title: "Id", template: "<a id='single_2'  href=javascript:editar('${BLOCK_ID}') style='color:gray !important;'>${BLOCK_ID}</a>" },
            { field: "BLOCK_DESC", width: 70, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${BLOCK_ID}') style='color:gray !important;'>${BLOCK_DESC}</a></font>" },
            { field: "BLOCK_PULL", width: 7, title: "Bloqueo", template: "<a id='single_2'  href=javascript:editar('${BLOCK_ID}') style='color:gray !important;'>${BLOCK_PULL}</a></font>" },
            { field: "BLOCK_AUT", width: 9, title: "Autorización", template: "<a id='single_2'  href=javascript:editar('${BLOCK_ID}') style='color:gray !important;'>${BLOCK_AUT}</a></font>" },
            { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${BLOCK_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });
};

function editar(idSel) {
    location.href = "../Bloqueo/Nuevo?set=" + idSel;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../Bloqueo/Eliminar',
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
    $("#ddlEstado").val(1);
    $("#txtDescripcion").focus();
}