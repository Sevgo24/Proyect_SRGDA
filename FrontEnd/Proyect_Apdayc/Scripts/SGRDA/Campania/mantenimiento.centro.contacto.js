$(function () {
    loadComboOficina("ddlOficinaComercial", 0);
    loadEstadosMaestro("ddlEstado", 0);

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#gridCentroContacto').data('kendoGrid').dataSource.query({ tipo: $("#ddlOficinaComercial").val(), dato: $("#txtNombre").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    });

    $("#btnBuscar").on("click", function () {
        $('#gridCentroContacto').data('kendoGrid').dataSource.query({ tipo: $("#ddlOficinaComercial").val(), dato: $("#txtNombre").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../CentroContacto/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    $("#txtNombre").on("keypress", function (e) {
        var key = (e ? e.keyCode || e.which : window.event.keyCode);
        if (key == 13) {
            $('#gridCentroContacto').data('kendoGrid').dataSource.query({ tipo: $("#ddlOficinaComercial").val(), dato: $("#txtNombre").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
        }
    });

    $("#ddlEstado").val(1);
    loadData();
});

function redirecto(formato) {
    var en = {
        Idoficina: $("#ddlOficinaComercial").val(),
        Nombre: $("#txtNombre").val(),
        st: $("#ddlEstado").val()
    };
    window.open('../CentroContacto/DownloadReport?format=' + formato + '&Idoficina=' + en.Idoficina + '&Nombre=' + en.Nombre + '&st=' + en.st);
    return true;
};

function loadData() {
    $("#gridCentroContacto").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../CentroContacto/ListarCentroContacto",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlOficinaComercial").val(), dato: $("#txtNombre").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: "ListaCentroCont", total: 'TotalVirtual' }
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
            { title: 'Eliminar', width: 15, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CONC_ID}'/>" },
            { field: "CONC_ID", width: 15, title: "Id", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_ID}') style='color:gray !important;'>${CONC_ID}</a></font>" },
            { field: "CONC_NAME", width: 60, title: "Nombre", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_ID}') style='color:gray !important;'>${CONC_NAME}</a></font>" },
            { field: "OFF_NAME", width: 60, title: "Oficina Comercial", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_ID}') style='color:gray !important;'>${OFF_NAME}</a></font>" },
            { field: "CONC_DESC", width: 200, title: "Descripción", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_ID}') style='color:gray !important;'>${CONC_DESC}</a></font>" },
            { field: "ESTADO", width: 20, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${CONC_ID}') style='color:gray !important;'>${ESTADO}</a></font>" }
           ]
    });
};

function editar(idSel) {
    document.location.href = '../CentroContacto/Nuevo?id=' + idSel;
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
        $('#gridCentroContacto').data('kendoGrid').dataSource.query({ tipo: $("#ddlOficinaComercial").val(), dato: $("#txtNombre").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../CentroContacto/Eliminar',
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
    $("#ddlOficinaComercial").val(0);
    $("#txtNombre").val('');
    $("#ddlEstado").val(0);
}