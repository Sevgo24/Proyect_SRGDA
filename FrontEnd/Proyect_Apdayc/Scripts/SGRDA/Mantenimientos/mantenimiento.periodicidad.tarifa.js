$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        $("#txtBusqueda").focus();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnsuprimir").on("click", function () {
        eliminar();
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
});

function editar(idSel) {
    document.location.href = '../PERIODICIDADTARIFA/Edit?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}


function loadData() {
    $("#txtBusqueda").focus();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../PERIODICIDADTARIFA/usp_listar_periocidadtarifaJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECRATEFREQUENCY", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns:
              [
              {
                  title: 'Eliminar', width: 7, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${RAT_FID}'/>"
              },
              {
                  field: "RAT_FID", width: 6, title: "Id", template: "<a id='single_2' href=javascript:editar('${RAT_FID}') style='color:grey;text-decoration:none;'>${RAT_FID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 10, title: "Propietario", template: "<a  id='single_2' href=javascript:editar('${RAT_FID}') style='color:grey;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "RAT_FDESC", width: 90, title: "Descripción", template: "<a  id='single_2' href=javascript:editar('${RAT_FID}') style='color:grey;text-decoration:none;'>${RAT_FDESC}</a>"
              },
              { field: "Activo", width: 8, title: "Estado", template: "<a  id='single_2' href=javascript:editar('${RAT_FID}') style='color:grey;text-decoration:none;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
              ]
    })
};



function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            FuncionEliminar(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../PERIODICIDADTARIFA/Eliminar',
        type: 'POST',
        data: id,
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