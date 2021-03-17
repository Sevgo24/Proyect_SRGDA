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
        location.href = "../ModuloSistema/Nuevo";
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
                    url: "../ModuloSistema/Listar_PageJson_Modulo_Sistema",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { parametro: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "Modulo", total: 'TotalVirtual' }
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
                   title: 'Eliminar', width: 11, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${PROC_MOD}'/>"
               },
               { field: "PROC_MOD", width: 9, title: "Id", template: "<a  href='../ModuloSistema/Nuevo?set=${PROC_MOD}'style='color:gray !important;'>${PROC_MOD}</a>" },
               { field: "MOD_DESC", width: 80, title: "Descripción", template: "<a  href='../ModuloSistema/Nuevo?set=${PROC_MOD}'style='color:gray !important;'>${MOD_DESC}</a>" },
               { field: "MOD_CLABEL", width: 120, title: "Etiqueta del Módulo", template: "<a  href='../ModuloSistema/Nuevo?set=${PROC_MOD}'style='color:gray !important;'>${MOD_CLABEL}</a>" },
               { field: "Activo", width: 12, title: "Estado", template: "<a  href='../ModuloSistema/Nuevo?set=${PROC_MOD}' style='color:gray !important;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
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

//function editar(idSel) {
//    location.href = "../ModuloSistema/Nuevo?set=" + idSel;
//}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../ModuloSistema/Eliminar',
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