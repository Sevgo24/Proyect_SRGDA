$(function () {
    loadTipoObservacion("ddlTipoObservacion", 0);
    loadEstadosMaestro("ddlEstado", 0);

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#gridTipoCampania').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoObservacion").val(), dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    });

    $("#btnBuscar").on("click", function () {
        $('#gridTipoCampania').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoObservacion").val(), dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../TipoCampania/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    $("#txtDescripcion").on("keypress", function (e) {
        var key = (e ? e.keyCode || e.which : window.event.keyCode);
        if (key == 13) {
            $('#gridTipoCampania').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoObservacion").val(), dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
        }
    });

    $("#ddlEstado").val(1);
    loadData(); 
});

function redirecto(formato) {
    var en = {
        tipo: $("#ddlTipoObservacion").val(),
        desc: $("#txtDescripcion").val(),
        st: $("#ddlEstado").val()
    };
    window.open('../TipoCampania/DownloadReport?format=' + formato + '&tipo=' + en.tipo + '&desc=' + en.desc + '&st=' + en.st );
    return true;
};

function loadData() {
    $("#gridTipoCampania").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TipoCampania/ListarTipoCampania",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoObservacion").val(), dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "ListaCampTipo", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            { title: 'Eliminar', width: 15, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CONC_CTID}'/>" },
            { field: "CONC_CTID", width: 10, title: "Id", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_CTID}') style='color:gray !important;'>${CONC_CTID}</a></font>" },
            { field: "CONC_CTNAME", width: 120, title: "Descripción", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_CTID}') style='color:gray !important;'>${CONC_CTNAME}</a></font>" },
            { field: "OBS_DESC", width: 100, title: "Tipo Observación", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_CTID}') style='color:gray !important;'>${OBS_DESC}</a></font>" },
            { field: "ESTADO", width: 20, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_CTID}') style='color:gray !important;'>${ESTADO}</a></font>" }
           ]
    });
};

function editar(idSel) {
    document.location.href = '../TipoCampania/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
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
        $('#gridTipoCampania').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoObservacion").val(), dato: $("#txtDescripcion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../TipoCampania/Eliminar',
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

function limpiar() {
    $("#ddlTipoObservacion").val(0);
    $("#txtDescripcion").val('');
    $("#ddlEstado").val(0);
}