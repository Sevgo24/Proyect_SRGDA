
/************************** INICIO CARGA********************************************/
$(function () {
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
            idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
            idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
            estado: $("#ddlEstado").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#txtNombre").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({
                nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
                idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
                idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
                estado: $("#ddlEstado").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
    });

    $("#txtEtiqueta").keypress(function (e) {
        if (e.which == 13)
            $('#grid').data('kendoGrid').dataSource.query({
                nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
                idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
                idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
                estado: $("#ddlEstado").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
            idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
            idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
            estado: $("#ddlEstado").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Action/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    loadEstadosMaestro("ddlEstado");
    LoadTipoAccion('ddlTipo');
    LoadTipoDato('ddlDato');
    LoadProceso('ddlProceso');

    $("#txtAccion").attr("disabled", "disabled");

    var newOption0 = "<option value='" + "0" + "'>--SELECCIONE--</option>";
    var newOption1 = "<option value='" + "A" + "'>AUTOMATICO</option>";
    var newOption2 = "<option value='" + "M" + "'>MANUAL</option>";
    $('#dllAccion').append(newOption0);
    $('#dllAccion').append(newOption1);
    $('#dllAccion').append(newOption2);
    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});

//****************************  FUNCIONES ****************************
function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: '../Action/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, {
                        nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
                        idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
                        idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
                        estado: $("#ddlEstado").val()
                    })
                }
            },
            schema: { data: 'ListarAcciones', total: 'TotalVirtual' }
        },
        groupable: false,
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
				{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_AID}'/>" },
				{ field: "WRKF_AID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${WRKF_AID}</a>" },
				{ field: "WRKF_ANAME", width: 22, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${WRKF_ANAME}</a>" },
				{ field: "WRKF_ALABEL", width: 9, title: "Etqueta", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${WRKF_ALABEL}</a>" },
                { field: "WRKF_ATNAME", width: 7, title: "Tipo Acción", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${WRKF_ATNAME}</a>" },
                { field: "WRKF_DTNAME", width: 7, title: "Tipo Dato", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${WRKF_DTNAME}</a>" },
                //{ field: "PROC_NAME", width: 10, title: "Procedimiento", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${PROC_NAME}</a>" },
                { field: "TIPO_ACCION", width: 5, title: "Acción Automatica", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${TIPO_ACCION}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${WRKF_AID}') style='color:gray !important;'>${ESTADO}</a>" },
                { title: 'Perfil', width: 3, headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/botones/user.png'  width='16' onclick='Abrir(${WRKF_AID});' border='0' title='Ver perfil.'  style=' cursor: pointer; cursor: hand;'>" },
			]
    });
}

function Abrir(id) {
    $.ajax({
        url: '../Action/ObtenerAgente',
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtAccion").val(tipo.WRKF_ANAME);
                    loadDataAgente();
                    $("#mvPerfil").dialog({ autoOpen: false, width: 430, height: 220, modal: true });
                    $("#mvPerfil").dialog("open");
                }
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
            else if (dato.result == 2) {
                alert(dato.message);
            }
        }
    });
}

function editar(idSel) {
    document.location.href = '../Action/Nuevo?set=' + idSel;
}

function limpiar() {
    $("#txtNombre").val('');
    $("#txtEtiqueta").val('');
    $("#ddlTipo").val(0);
    $("#ddlProceso").val(0);
    $("#dllAccion").val(0);
    $("#ddlDato").val(0);
    $("#ddlEstado").val(1);
}

function eliminar() {
    var values = [];
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            delOrigen(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un registro.");
    } else {
        alert("Estados actualizado correctamente.");
        $('#grid').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
            idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
            idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion").val(),
            estado: $("#ddlEstado").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    }
}

function delOrigen(idOri) {
    var codigoDel = { id: idOri };

    $.ajax({
        url: '../Action/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataAgente() {
    loadDataGridTmp('VerDetalle', "#gridAgente");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

