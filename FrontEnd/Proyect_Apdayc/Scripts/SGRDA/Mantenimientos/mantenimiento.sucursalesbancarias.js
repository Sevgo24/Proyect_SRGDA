$(function () {

    var cod = "";
    $("#idBanco").val(cod);
    $("#ddlBancos").on("change", function () {
        var codigo = $("#ddlBancos").val();
        $("#idBanco").val(codigo);
    });

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), id: $("#ddlBancos").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnsuprimir").on("click", function () {
        eliminar();
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), id: $("#ddlBancos").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13)
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), id: $("#ddlBancos").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    
    loadBancos('ddlBancos', 0, 'Todos');
    loadEstadosMaestro("ddlEstado");
    loadData();

});

function editar(idSel) {
    document.location.href = '../SUCURSALESBANCARIAS/Edit?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../SUCURSALESBANCARIAS/usp_listar_SucursalesBancariasJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), idBanco: $("#ddlBancos").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECBANKSBRANCH", total: 'TotalVirtual' }
        },
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
              title: 'Eliminar', width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${BNK_ID},${BRCH_ID}'/>"
          },
          {
              hidden: true,
              field: "OWNER", width: 30, title: "PROPIETARIO", template: "<a id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${OWNER}</a>"
          },
          {
              hidden: true,
              field: "BNK_ID", width: 5, title: "Id", template: "<a id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${BNK_ID}</a>"
          },
          {
              field: "BRCH_ID", width: 15, title: "Id", template: "<a id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${BRCH_ID}</a>"
          },
          {
              field: "BNK_NAME", width: 30, title: "Banco", template: "<a  id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${BNK_NAME}</a>"
          },
          {
              field: "BRCH_NAME", width: 30, title: "Sucursal", template: "<a  id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${BRCH_NAME}</a>"
          },
          {
              hidden: true,
              field: "ADD_ID", width: 50, title: "COD. DIRECCION", template: "<a  id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${ADD_ID}</a>"
          },
          {
              field: "ADDRESS", width: 50, title: "Dirección", template: "<a  id='single_2' href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>${ADDRESS}</a>"
          },
          { field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${BRCH_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
          ]
    });
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
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), id: $("#ddlBancos").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        alert("Estados actualizado correctamente.");
    }
}

function FuncionEliminar(id) {
    var id = { Codigo: id };
    $.ajax({
        url: '../SUCURSALESBANCARIAS/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
        }
    });
}

function limpiar() {
    $("#ddlBancos").val(0);
    $("#idBanco").val("");
    $("#txtBusqueda").val('');
    $("#txtBusqueda").focus();
}