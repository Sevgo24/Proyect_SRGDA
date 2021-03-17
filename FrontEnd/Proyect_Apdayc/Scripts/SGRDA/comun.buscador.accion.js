
var mvInitBuscarAccion = function (parametro) {
    var elemento = '<div id="' + "mvBuscarAccion" + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalView" value="' + "mvBuscarAccion" + '"/>';
    elemento += '<input type="hidden"  id="hidIdEvent" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Descripción</td><td><input type="text" id="txtNombre" maxlength="100" style="width:300px" /></td>';
    elemento += '<td>Etiqueta</td>';
    elemento += '<td><input type="text" id="txtEtiqueta" maxlength="40" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Dato</td>';
    elemento += '<td><select id="ddlDato" ></select></td>';
    elemento += '<td>Tipo de Acción</td>';
    elemento += '<td><select id="ddlTipo" ></select></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Proceso</td>';
    elemento += '<td><select id="ddlProceso" ></select></td>';
    elemento += '<td>Acción Automática</td>';
    elemento += '<td><select id="dllAccion2" ></select></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado</td><td><select id="ddlEstadoAc"></select></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarAccion"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarAccion"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridAccion"></div>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table></div>';

    $("#" + parametro.container).append(elemento);
    $("#mvBuscarAccion").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        height: 600,
        title: "Búsqueda General de Acción."
    });

    $("#btnBuscarAccion").on("click", function () {
        $('#gridAccion').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
            idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
            idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion2").val(),
            estado: $("#ddlEstadoAc").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_5
        });
    });
    $("#btnLimpiarAccion").on("click", function () {
        limpiar();
        $('#gridAccion').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombre").val(), etiqueta: $("#txtEtiqueta").val(),
            idTipoAccion: $("#ddlTipo").val(), idTipoDato: $("#ddlDato").val(),
            idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion2").val(),
            estado: $("#ddlEstadoAc").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_5
        });
    });
    loadEstadosMaestro("ddlEstadoAc");
    //loadEstados("ddlEstadoAc", 0);
    LoadTipoAccion('ddlTipo');
    LoadTipoDato('ddlDato');
    LoadProceso('ddlProceso');

    var newOption0 = "<option value='" + "0" + "'>--SELECCIONE--</option>";
    var newOption1 = "<option value='" + "A" + "'>AUTOMATICO</option>";
    var newOption2 = "<option value='" + "M" + "'>MANUAL</option>";
    $('#dllAccion2').append(newOption0);
    $('#dllAccion2').append(newOption1);
    $('#dllAccion2').append(newOption2);

    loadDataA();
};

var getId = function (id) {
    var hidIdidModalView = $("#hidIdidModalView").val();
    var fnc = $("#hidIdEvent").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiar() {
    $("#txtNombre").val('');
    $("#txtEtiqueta").val('');
    $("#ddlTipo").val(0);
    $("#ddlProceso").val(0);
    $("#dllAccion2").val(0);
    $("#ddlDato").val(0);
    $("#ddlEstadoAc").val(1);
}

function loadDataA() {
    $("#gridAccion").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_5,
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
                        idProceso: $("#ddlProceso").val(), idAuto: $("#dllAccion2").val(),
                        estado: $("#ddlEstadoAc").val()
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
				//{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_AID}'/>" },
				{ field: "WRKF_AID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${WRKF_AID}</a>" },
				{ field: "WRKF_ANAME", width: 10, title: "Descripción", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${WRKF_ANAME}</a>" },
				{ field: "WRKF_ALABEL", width: 8, title: "Etiqueta", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${WRKF_ALABEL}</a>" },
                { field: "WRKF_ATNAME", width: 8, title: "Tipo Acción", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${WRKF_ATNAME}</a>" },
                { field: "WRKF_DTNAME", width: 8, title: "Tipo Dato", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${WRKF_DTNAME}</a>" },
                { field: "PROC_NAME", width: 8, title: "Procedimiento", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${PROC_NAME}</a>" },
                { field: "TIPO_ACCION", width: 10, title: "Acción Automatica", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${TIPO_ACCION}</a>" },
                { field: "ESTADO", width: 6, title: "Estado", template: "<a id='single_2'  href=javascript:getId('${WRKF_AID}') style='color:gray !important;'>${ESTADO}</a>" },
			]
    });
}
