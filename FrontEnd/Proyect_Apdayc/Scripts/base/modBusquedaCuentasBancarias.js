/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

$(function () {

    function avoidRefresh(e) {
        e.preventDefault();
    }

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        }
    });

    $("#btnBusqueda").on("click", function () {
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar();
        $('#listado').data('kendoGrid').dataSource.query({ dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadEstadosMaestro("ddlEstado");
    loadData();
    $("#txtBusqueda").focus();
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
                    url: "../CUENTASBANCARIAS/usp_listar_CuentasBancariasJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECBPSBANKSACC", total: 'TotalVirtual' }
        },
        sortable: true,
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
                  title: 'Eliminar', width: 10, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${BNK_ID}'/>"
              },
              {
                  hidden: true,
                  field: "BPS_ID", width: 70, title: "SOCIO NEGOCIO", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:gray;'>${BPS_ID}</a>"
              },
              {
                  hidden: true,
                  field: "OWNER", width: 50, title: "PROPIETARIO", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${OWNER}</a>"
              },
              {
                  field: "BNK_ID", width: 9, title: "Id", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${BNK_ID}</a>"
              },
              {
                  field: "BNK_NAME", width: 30, title: "Banco", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${BNK_NAME}</a>"
              },
              {
                  hidden: true,
                  field: "BRCH_ID", width: 40, title: "IdSucursal", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${BRCH_ID}</a>"
              },
              //{
              //    field: "BRCH_NAME", width: 30, title: "Sucursal", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${BRCH_NAME}</a>"
              //},
              {
                  //hidden: true,
                  field: "BACC_NUMBER", width: 50, title: "Cuenta Bancaria", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>${BACC_NUMBER}</a>"
              },
              {
                  field: "BACC_TYPE", width: 20, title: "Dig. Control", template: "<a  href='../CUENTASBANCARIAS/Edit/${OWNER},${BNK_ID},${BRCH_ID}'style='color:gray;'>${BACC_TYPE}</a>"
              },
              { field: "ACTIVO", width: 12, title: "Estado", template: "<a  href='../CORRELATIVOS/Edit/${OWNER},${BNK_ID},${BRCH_ID}' style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
              ]
    })
}


$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar esta cuenta bancaria?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        BNK_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../CUENTASBANCARIAS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../CUENTASBANCARIAS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}
