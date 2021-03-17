$(function () {
    loadEstadosLicencia("ddlEstadoOri");
    loadEstadosLicencia("ddlEstadoDes");
    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ estadoOri: $("#ddlEstadoOri").val(), estadoDes: $("#ddlEstadoDes").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ estadoOri: $("#ddlEstadoOri").val(), estadoDes: $("#ddlEstadoDes").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TransicionesEstado/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
        $('#grid').data('kendoGrid').dataSource.query({ estadoOri: $("#ddlEstadoOri").val(), estadoDes: $("#ddlEstadoDes").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
});

function editar(idSel,idSel2) {
    document.location.href = '../TransicionesEstado/Nuevo?idEstOri=' + idSel + '&idEstDes=' + idSel2;
      //document.location.href = '../AgenteDivision/Nuevo?id=' + idSel + '&idDiv=' + idSelDiv;
    //alert(idSel, idSel2);
    $("#hidOpcionEdit").val(1);
}

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../TransicionesEstado/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                    return $.extend({}, options, { estadoOri: $("#ddlEstadoOri").val(), estadoDes: $("#ddlEstadoDes").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "listaTranEstado", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            { title: 'Eliminar', width: 20, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LICS_ID}','${LICS_IDT}'/>" },
             { field: "LICS_ID", hidden: true, width: 50, title: "Id", template: "<a id='single_2'  href=javascript:editar('${LICS_ID}','${LICS_ID}') style='color:gray !important;'>${LICS_ID}</a></font>" },
            { field: "LICS_NAMEori", width: 100, title: "Estado Origen", template: "<a id='single_2'  href=javascript:editar('${LICS_ID}','${LICS_IDT}') style='color:gray !important;'>${LICS_NAMEori}</a></font>" },

             { field: "LICS_IDT", hidden: true, width: 50, title: "Id", template: "<a id='single_2'  href=javascript:editar('${LICS_ID}','${LICS_IDT}') style='color:gray !important;'>" + 
                "<label for='lbls' id='chkSelDadId' class='kendo-chk-est-dest' value='${LICS_IDT}'/>${LICS_IDT}</label>" +
                          "</a>" }, 

            { field: "LICS_NAMEdes", width: 20, title: "Estado Destino", template: "<a id='single_2'  href=javascript:editar('${LICS_ID}','${LICS_IDT}') style='color:gray !important;'>${LICS_NAMEdes}</a>" },
            { field: "ESTADO", width: 10, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${LICS_ID}','${LICS_IDT}') style='color:gray !important;'>${ESTADO}</a></font>" },
           ]
    });
}

function limpiar() {
    $("#ddlEstadoOri").val(0);
    $("#ddlEstadoDes").val(0);
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigoOri = $row.find('.kendo-chk').attr('value');
            var codigoDes = $row.find('.kendo-chk-est-dest').attr('value');
            values.push(codigoOri);
            FuncionEliminar(codigoOri, codigoDes);
        }
    });

    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        $('#grid').data('kendoGrid').dataSource.query({ estadoOri: $("#ddlEstadoOri").val(), estadoDes: $("#ddlEstadoDes").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

    }
}

function FuncionEliminar(idorigen, iddestino) {
    $.ajax({
        url: '../TransicionesEstado/Eliminar',
        type: 'POST',
        data: { Idori: idorigen, Iddest: iddestino },
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