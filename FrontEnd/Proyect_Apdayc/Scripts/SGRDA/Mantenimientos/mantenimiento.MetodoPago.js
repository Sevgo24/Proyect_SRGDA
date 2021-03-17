$(function () {
    $("#txtDescripcion").focus();

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#gridMetodo').data('kendoGrid').dataSource.query({ parametro: $("#txtDescripcion").val(), confirmacion: $("#chk").is(':checked'), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnBuscar").on("click", function () {
        $('#gridMetodo').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), confirmacion: $("#chk").is(':checked'), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtDescripcion").val("");
        limpiar();
        $('#gridMetodo').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), confirmacion: $("#chk").is(':checked'), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../MetodoPago/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
});

function limpiar() {
    $("#txtDescripcion").val("");
    $("#chk").prop('checked', false);
    $("#ddlEstado").val(0);
}

function editar(idSel) {
    location.href = '../MetodoPago/Nuevo?id=' + idSel;
}

function loadData() {
    $("#gridMetodo").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../MetodoPago/ListarMetodoPago",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtDescripcion").val(), confirmacion: $("#chk").is(':checked'), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "ListaMetodoPago", total: 'TotalVirtual' }
        },
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
                  title: 'Eliminar', width: 9, template: "<input type='checkbox' class='kendo-chk' id='vehicle' name='vehicle' value='${REC_PWID}'/>"
              },
              {
                  field: "REC_PWID", width: 7, title: "Id", template: "<font color='green'><a id='single_2' href=javascript:editar('${REC_PWID}') style='color:gray !important;'>${REC_PWID}</a></font>"
              },
              {
                  field: "Automaticamente", width: 9, title: "Conf. Auto", template: "<font color='green'><a id='single_2' href=javascript:editar('${REC_PWID}') style='color:gray !important;'>${Automaticamente}</a></font>"
              },
              {
                  field: "DESCRIPTION", width: 140, title: "Método de Pago", template: "<font color='green'><a id='single_2' href=javascript:editar('${REC_PWID}') style='color:gray !important;'>${REC_PWDESC}</a></font>"
              },
              {
                  field: "ESTADO", width: 10, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${REC_PWID}') style='color:gray !important;'>${ESTADO}</a></font>"
              }
            ]
    });
}

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
        $('#gridMetodo').data('kendoGrid').dataSource.query({ dato: $("#txtDescripcion").val(), confirmacion: $("#chk").is(':checked'), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../MetodoPago/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}