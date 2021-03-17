/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";


/*INICIO CONSTANTES*/

$(function () {

    $("#btnNuevo").on("click", function () {
        location.href = "../ClaseCreacion/Nuevo";
    });

    $("#btnBuscar").on("click", function () { loadData(); limpiar(); });
    $("#btnLimpiar").on("click", function () {
        limpiar(); loadData();
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
            loadData();
            alert("Estados actualizado correctamente.");
        }
    });
    loadEstadosMaestro("ddlEstado");
    loadData();
});

function loadData() {

    var busq = $("#txtDescripcion").val();
    var estado = $("#ddlEstado").val();
    var tot = 2; //$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../ClaseCreacion/Listar_PageJson_Clase_Creacion", dataType: "json", data: { parametro: busq, st: estado }
                }
            },
            schema: { data: "ClaseCreacion", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 9, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CLASS_COD}'/>"
               },
            { field: "RowNumber", width: 9, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${RowNumber}</a>" },
            { field: "CLASS_COD", width: 20, title: "<font size=2px>DESCRIP.CORTA</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${CLASS_COD}</a>" },
            { field: "CLASS_DESC", width: 20, title: "<font size=2px>DESCRIPCIÓN</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${CLASS_DESC}</a></font>" },
            { field: "Activo", width: 5, title: "<font size=2px>Estado </font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:5F5F5F;text-decoration:none;font-size:11px'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
           ]
    });
}

function limpiar() {

    $("#txtDescripcion").val("");
    $("#estado").val(0);
}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function editar(idSel) {
    location.href = "../ClaseCreacion/Edit?set=" + idSel;
}

function eliminar(idSel) {
    // $('#ddlRol option').remove();
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../ClaseCreacion/Eliminar',
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