
var mvInitBuscarObjects = function (parametro) {
    var elemento = '<div id="' + "mvBuscarObject" + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewO" value="' + "mvBuscarObject" + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventO" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor">'
    elemento += '<table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Nombre </td><td><input type="text" id="txtNombreOb" maxlength="100" style="width:300px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Código Interno</td>';
    elemento += '<td> <input type="text" id="txtCodInternoOb" maxlength="40" />  </td>';
    elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Tipo de Objeto</td>';
    elemento += '<td> <select id="ddlTipoOb" />  </td>';
    elemento += '</tr>';
    
    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoOb" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarObjects"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarObjects"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridObjects"></div>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table></div>';

    $("#" + parametro.container).append(elemento);
    $("#mvBuscarObject").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        height: 530,
        title: "Búsqueda General de Workflow de Objetos."
    });

    $("#btnBuscarObjects").on("click", function () {
        $('#gridObjects').data('kendoGrid').dataSource.query({ nombre: $("#txtNombreOb").val(), codInterno: $("#txtCodInternoOb").val(), idTipoObjeto: $("#ddlTipoOb").val(), estado: $("#ddlEstadoOb").val(), page: 1, pageSize: K_PAGINACION.LISTAR_5 });
    });
    $("#btnLimpiarObjects").on("click", function () {
        limpiarObjects();
        $('#gridObjects').data('kendoGrid').dataSource.query({ nombre: $("#txtNombreOb").val(), codInterno: $("#txtCodInternoOb").val(), idTipoObjeto: $("#ddlTipoOb").val(), estado: $("#ddlEstadoOb").val(), page: 1, pageSize: K_PAGINACION.LISTAR_5 });
    });
    loadEstadosMaestro("ddlEstadoOb");
    LoadTipoObjeto("ddlTipoOb");

    var newOption0 = "<option value='" + "0" + "'>--SELECCIONE--</option>";
    var newOption1 = "<option value='" + "A" + "'>AUTOMATICO</option>";
    var newOption2 = "<option value='" + "M" + "'>MANUAL</option>";
    $('#dllAccion').append(newOption0);
    $('#dllAccion').append(newOption1);
    $('#dllAccion').append(newOption2);

    loadDataFoundObjects();
};

var getIdOb = function (id) {
    var hidIdidModalViewO = $("#hidIdidModalViewO").val();
    var fnc = $("#hidIdEventO").val();
    $("#" + hidIdidModalViewO).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarObjects = function () {
    $("#txtNombreOb").val('');
    $("#txtCodInternoOb").val('');
    $("#ddlEstadoOb").val(1);
    $("#ddlTipoOb").val(0);
};

function loadDataFoundObjects() {
    $("#gridObjects").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_5,
            transport: {
                read: {
                    type: "POST",
                    url: '../Objects/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { nombre: $("#txtNombreOb").val(), codInterno: $("#txtCodInternoOb").val(), idTipoObjeto: $("#ddlTipoOb").val(), estado: $("#ddlEstadoOb").val() })
                }
            },
            schema: { data: 'ListarObject', total: 'TotalVirtual' }
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
				//{ title: 'Eliminar', width: 2, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_OID}'/>" },
				{ field: "WRKF_OID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${WRKF_OID}</a>" },
				{ field: "WRKF_ODESC", width: 10, title: "Descripción", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${WRKF_ODESC}</a>" },
				{ field: "WRKF_OINTID", width: 5, title: "Código Interno", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${WRKF_OINTID}</a>" },
                { field: "WRKF_OTDESC", width: 10, title: "Tipo Objeto", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${WRKF_OTDESC}</a>" },
                { field: "LOG_USER_CREAT", width: 7, title: "Usuario", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:getIdOb('${WRKF_OID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}
