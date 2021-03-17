$(function () {
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#txtBusqueda").focus();

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        $("#txtBusqueda").focus();
    });

    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnsuprimir").on("click", function () {
        eliminar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
});

function loadData() {
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../Incidenciaobra/usp_listar_incidenciaObraJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECUSESTYPE", total: 'TotalVirtual' }
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
                  //title: 'Eliminar', width: 20, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${MOD_INCID}'/>"
                  title: 'Eliminar', width: 30, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOD_INCID}'/>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 30, title: "PROPIETARIO", template: "<a  href='../Incidenciaobra/Edit/${MOD_INCID}' style='color:gray;text-decoration:none;'>${OWNER}</a>"
              },
              {
                  field: "MOD_INCID", width: 20, title: "Id", template: "<a  href='../Incidenciaobra/Edit/${MOD_INCID}'style='color:gray;text-decoration:none;'>${MOD_INCID}</a>"
              },              
              {
                  field: "MOD_IDESC", width: 100, title: "Descripción", template: "<a  href='../Incidenciaobra/Edit/${MOD_INCID}'style='color:gray;text-decoration:none;'>${MOD_IDESC}</a>"
              },              
              {
                  field: "MOD_IDET", width: 200, title: "Detalle", template: "<a  href='../Incidenciaobra/Edit/${MOD_INCID}'style='color:gray;text-decoration:none;'>${MOD_IDET}</a>"
              },
              {
                  field: "Activo", width: 40, title: "Estado", template: "<a  href='../Incidenciaobra/Edit/${MOD_INCID}' style='color:gray;text-decoration:none;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>"
              }
              ]
    })
}

function FuncionEliminar(id) {
    var id = { cod : id };
    $.ajax({
        url: '../Incidenciaobra/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
        }
    });
}

function eliminar() {
    var values = [];
    //bootbox.confirm("desea eliminar incidencia de la obra?", function (result) {
        //if (result == true) {
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
                alert("Estados actualizado correctamente.");
            }
        //}
    //});
}

function limpiar() {
    $("#txtBusqueda").val("");
}