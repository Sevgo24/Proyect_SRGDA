/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/*INICIO CONSTANTES*/

$(function () {
    $("#btnNuevo").on("click", function () {
        location.href = "../Calificadores/Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoCalificador").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoCalificador").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoCalificador").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            
        }
    });

    $("#txtDescripcion").focus();
    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoCalificador").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    loadTipoCalificador('ddlTipoCalificador', 0);
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
                    url: "../Calificadores/Listar_PageJson_Calificador",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoCalificador").val(), parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "Calificador", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 7, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${QUC_ID}'/>"
               },
            { field: "QUC_ID", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${QUC_ID}') style='color:gray !important;'>${QUC_ID}</a>" },
            { field: "TIPO", width: 65, title: "Tipo de Calificador", template: "<a id='single_2' href=javascript:editar('${QUC_ID}') style='color:gray !important;'>${TIPO}</a>" },
            { field: "DESCRIPTION", width: 25, title: "Calificador", template: "<font color='green'><a id='single_2' href=javascript:editar('${QUC_ID}') style='color:gray !important;'>${DESCRIPTION}</a></font>" },
            { field: "ACTIVO", width: 9, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${QUC_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
           ]
    });
}

function limpiar() {

    $("#ddlTipoCalificador").val(0);
    $("#txtDescripcion").val("");
    $("#estado").val(0);
}

function editar(idSel) {
    location.href = "../Calificadores/Nuevo?set=" + idSel;
}

function eliminar(idSel) {

    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../Calificadores/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("dato actualizado correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}