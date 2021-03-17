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
        location.href = "../ClaseCreacion/Nuevo";
    });

    loadDerechoTipo('ddlDerecho', 0, 'Todos');
    loadEstadosMaestro("ddlEstado");
    loadData();
    $("#btnBuscar").on("click", function () { loadData(); limpiar(); });
    $("#btnLimpiar").on("click", function () {
        limpiar(); loadData();
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            loadData();
        }
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
        }
    });

});

function loadData() {

    //var busq1 = $("#ddlDerecho").val();
    var busq2 = $("#txtDescripcion").val();
    var estado = $("#ddlEstado").val();
    var tot = 2; //$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../ClaseCreacion/Listar_PageJson_Clase_Creacion",
                    dataType: "json"
                    //data: { clas: busq2, st: estado }
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { clas: busq2, st: estado })
                }
            },
            schema: { data: "ClaseCreacion", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: false,
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
                   title: 'Eliminar', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CLASS_COD}'/>"
               },
            //{ field: "RowNumber", width: 5, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:gray !important;'>${RowNumber}</a>" },
            //{ field: "RIGHT_DESC", hidden: true, width: 60, title: "<font size=2px>Tipo Derecho</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:gray !important;'>${RIGHT_DESC}</a></font>" },
            { field: "CLASS_COD", width: 6, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:gray !important;'>${CLASS_COD}</a>" },
            { field: "CLASS_DESC", width: 90, title: "<font size=2px>Clase Creación</font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:gray !important;'>${CLASS_DESC}</a></font>" },
            { field: "Activo", width: 8, title: "<font size=2px>Estado </font>", template: "<a id='single_2'  href=javascript:editar('${CLASS_COD}') style='color:gray !important;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
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

function editar(cod) {
    location.href = "../ClaseCreacion/Edit?set=" + cod;
}

function eliminar(cod) {
    var codigosDel = { codigo: cod };
    $.ajax({
        url: '../ClaseCreacion/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadData();
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}