/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "../CaracteristicaPredefinida/Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipo").val(), subtipo: $("#ddlSubTipo").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipo").val(), subtipo: $("#ddlSubTipo").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipo").val(), subtipo: $("#ddlSubTipo").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            
        }
    });

    $("#ddlTipo").on("change", function () {
        var codigo = $("#ddlTipo").val();
        loadSubTipoestablecimientoPorTipo('ddlSubTipo', codigo);
    });

    loadTipoestablecimiento('ddlTipo', 0);
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
                    type: "POST",
                    url: "../CaracteristicaPredefinida/Listar_PageJson_CaracteristicaPredefinida",
                    dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, { tipo: $("#ddlTipo").val(), subtipo: $("#ddlSubTipo").val(), st: $("#ddlEstado").val() })
                    }
            },
            schema: { data: "CaracteristicaPredefinida", total: 'TotalVirtual' }
        },
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
                   title: 'Eliminar', width: 8, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CHAR_TYPES_ID}'/>"
               },
            { field: "CHAR_TYPES_ID", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}') style='color:gray !important;'>${CHAR_TYPES_ID}</a>" },
            { field: "TIPO", width: 30, title: "Tipo de Establecimiento", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>${TIPO}</a></font>" },
            { field: "SUBTIPO", width: 40, title: "SubTipo de Establecimiento", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>${SUBTIPO}</a></font>" },
            { field: "CHAR_SHORT", width: 30, title: "Características", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>${CHAR_SHORT}</a></font>" },
            { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" },

           { field: "EST_ID", hidden: true, width: 30, title: "Características", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>${EST_ID}</a></font>" },
           { field: "SUBE_ID", hidden: true, width: 30, title: ">Características", template: "<a id='single_2'  href=javascript:editar('${EST_ID}','${SUBE_ID}','${CHAR_TYPES_ID}')  style='color:gray !important;'>${SUBE_ID}</a></font>" },
           ]
    });
}

function editar(idEst,idSub,idCar) {
    location.href = "../CaracteristicaPredefinida/Nuevo?idEst=" + idEst + "&idSub=" + idSub + "&idCar=" + idCar;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../CaracteristicaPredefinida/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $('#grid').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipo").val(), subtipo: $("#ddlSubTipo").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#ddlTipo").val(0);
    $("#ddlSubTipo").val(0);
    $("#ddlEstado").val(1);
}