/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;

$(function () {
    $("#txtBusqueda").focus();
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            $('#listado').data('kendoGrid').dataSource.query({ parametro: $("#txtBusqueda").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
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
                    url: "../FORMASDEPAGO/usp_listar_FormasPagoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtBusqueda").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "RECPAYMENTTYPE", total: 'TotalVirtual' }
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
                  title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${PAY_ID}'/>"
              },
              {
                  hidden: true,
                  //field: "RowNumber", width: 9, title: "Id", template: "<a  href='../FORMASDEPAGO/Edit/${PAY_ID}'style='color:gray;'>${RowNumber}</a>"
                  field: "RowNumber", width: 9, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${PAY_ID}') style='color:gray !important;'>${RowNumber}</a></font>"
              },
              {
                  hidden: true,
                  //field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a  href='../FORMASDEPAGO/Edit/${PAY_ID}'style='color:gray;'>${OWNER}</a>"
                  field: "OWNER", width: 9, title: "<font size=2px>PROPIETARIO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${PAY_ID}') style='color:gray !important;'>${OWNER}</a></font>"
              },
              {
                  //field: "PAY_ID", width: 5, title: "Id", template: "<a  href='../FORMASDEPAGO/Edit/${PAY_ID}' style='color:gray;'>${PAY_ID}</a>"
                  field: "PAY_ID", width: 9, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${PAY_ID}') style='color:gray !important;'>${PAY_ID}</a></font>"
              },
              {
                  //field: "DESCRIPTION", width: 80, title: "Forma  de Pago", template: "<a  href='../FORMASDEPAGO/Edit/${PAY_ID}'style='color:gray;'>${DESCRIPTION}</a>"
                  field: "DESCRIPTION", width: 140, title: "<font size=2px>Forma  de Pago</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${PAY_ID}') style='color:gray !important;'>${DESCRIPTION}</a></font>"
              },

              {
                  //field: "ACTIVO", width: 8, title: "Estado", template: "<a  href='../FORMASDEPAGO/Edit/${PAY_ID}'style='color:gray !important;;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>"
                  field: "ACTIVO", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${PAY_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>"
              }
            ]
    });
}

function editar(idSel) {
    location.href = "../FORMASDEPAGO/Edit?set=" + idSel;
}

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar forma de pago?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        PAY_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../FORMASDEPAGO/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../FORMASDEPAGO/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}