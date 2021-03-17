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
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#hidCodigoBPS").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#hidCodigoBPS").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
            alert("Seleccione un Código Postal para eliminar.");
        } else {
            $('#grid').data('kendoGrid').dataSource.query({ parametro: $("#hidCodigoBPS").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
            
        }
    });
    loadComboTerritorio(0);
    initAutoCompletarUbigeo("txtCodigo", "hidCodigoBPS");
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
                    url: "../CodigoPostal/Listar_PageJson_CodigoPostal",
                    dataType: "json"
                    //data: { parametro: busq2, st: estado }
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { parametro: $("#hidCodigoBPS").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "Codigo_Postal", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 9, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CPO_ID}'/>"
               },
            { field: "CPO_ID", width: 9, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${CPO_ID}') style='color:gray !important;'>${CPO_ID}</a>" },
            { field: "DAD_VNAME", width: 50, title: "<font size=2px>Ubigeo</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CPO_ID}') style='color:gray !important;'>${DAD_VNAME}</a></font>" },
            { field: "POSITIONS", width: 15, title: "<font size=2px>Codigo Postal</font>", template: "<a id='single_2'  href=javascript:editar('${CPO_ID}') style='color:gray !important;'>${POSITIONS}</a>" },
            { field: "Activo", width: 7, title: "<font size=2px>Estado </font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${CPO_ID}') style='color:gray !important;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
           ]
    });
}

function limpiar() {
    $("#txtCodigo").val("");
}

function editar(idSel) {
    location.href = "../CodigoPostal/Nuevo?set=" + idSel;
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../CodigoPostal/Eliminar',
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