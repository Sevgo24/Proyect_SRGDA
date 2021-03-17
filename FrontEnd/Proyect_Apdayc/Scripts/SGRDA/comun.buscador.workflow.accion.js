
var mvInitBuscarSocio = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEvent" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor">'
        elemento += '<table border=0 style=" width:100%; "><tr>';
        elemento += '<td>Nombre </td><td><input type="text" id="txtNombreAC" maxlength="100" style="width:300px" /></td>';
        elemento += '</tr>';

        elemento += '<tr>';
        elemento += '<td>Etiqueta</td>';
        elemento += '<td> <input type="text" id="txtEtiquetaAC" maxlength="40" />  </td>';
        elemento += '</tr>';


        elemento += '<tr>';
        elemento += '<td>Tipo de Acción</td>';
        elemento += '<td> <select id="ddlTipoAC" />  </td>';
        elemento += '<td>Tipo de Dato</td>';
        elemento += '<td> <select id="ddlDatoAC" />  </td>';
        elemento += '</tr>';

        elemento += '<tr>';
        elemento += '<td>Proceso</td>';
        elemento += '<td> <select id="ddlProcesoAC" />  </td>';
        elemento += '<td>Acción Automatica</td>';
        elemento += '<td> <select id="ddlAccionAC" />';
        elemento += '</td>';
        elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoAC" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPS"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPS"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPS"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';

    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        height: 530,
        title: "Búsqueda General de Acciones de los Progresos."
    });

    loadEstadosMaestro("ddlEstadoAC");
    LoadTipoAccion('ddlTipoAC');
    LoadTipoDato('ddlDatoAC');
    LoadProceso('ddlProcesoAC');
    var newOption0 = "<option value='" + "0" + "'>--SELECCIONE--</option>";
    var newOption1 = "<option value='" + "A" + "'>AUTOMATICO</option>";
    var newOption2 = "<option value='" + "M" + "'>MANUAL</option>";
    $('#ddlAccionAC').append(newOption0);
    $('#ddlAccionAC').append(newOption1);
    $('#ddlAccionAC').append(newOption2);

    
    $("#btnBuscarSocioBPS").on("click", function () {

        $('#gridBPS').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombreAC").val(), etiqueta: $("#txtEtiquetaAC").val(),
            idTipoAccion: $("#ddlTipoAC").val(), idTipoDato: $("#ddlDatoAC").val(),
            idProceso: $("#ddlProcesoAC").val(), idAuto: $("#ddlAccionAC").val(),
            estado: $("#ddlEstadoAC").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnLimpiarSocioBPS").on("click", function () {
        limpiarBusqueda();
        $('#gridBPS').data('kendoGrid').dataSource.query({
            nombre: $("#txtNombreAC").val(), etiqueta: $("#txtEtiquetaAC").val(),
            idTipoAccion: $("#ddlTipoAC").val(), idTipoDato: $("#ddlDatoAC").val(),
            idProceso: $("#ddlProcesoAC").val(), idAuto: $("#ddlAccionAC").val(),
            estado: $("#ddlEstadoAC").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });
         
    $("#txtNombreAC").keypress(function (e) {
        if (e.which == 13)
            $('#gridBPS').data('kendoGrid').dataSource.query({
                nombre: $("#txtNombreAC").val(), etiqueta: $("#txtEtiquetaAC").val(),
                idTipoAccion: $("#ddlTipoAC").val(), idTipoDato: $("#ddlDatoAC").val(),
                idProceso: $("#ddlProcesoAC").val(), idAuto: $("#ddlAccionAC").val(),
                estado: $("#ddlEstadoAC").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
    });

    $("#txtEtiquetaAC").keypress(function (e) {
        if (e.which == 13)
            $('#gridBPS').data('kendoGrid').dataSource.query({
                nombre: $("#txtNombreAC").val(), etiqueta: $("#txtEtiquetaAC").val(),
                idTipoAccion: $("#ddlTipoAC").val(), idTipoDato: $("#ddlDatoAC").val(),
                idProceso: $("#ddlProcesoAC").val(), idAuto: $("#ddlAccionAC").val(),
                estado: $("#ddlEstadoAC").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
    });

    loadDataFound();
};

var getBPS = function (id) {
    var hidIdidModalView = $("#hidIdidModalView").val();
    var fnc = $("#hidIdEvent").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusqueda = function () {
    $("#txtNombreAC").val("");
    $("#txtEtiquetaAC").val("");
    $("#ddlEstadoAC").val(1);
    $("#ddlTipoAC").val(0);
    $("#ddlDatoAC").val(0);
    $("#ddlProcesoAC").val(0);
    $("#ddlAccionAC").val(0);
};
var loadDataFound = function () {
    $("#gridBPS").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: '../Action/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, {
                        nombre: $("#txtNombreAC").val(), etiqueta: $("#txtEtiquetaAC").val(),
                        idTipoAccion: $("#ddlTipoAC").val(), idTipoDato: $("#ddlDatoAC").val(),
                        idProceso: $("#ddlProcesoAC").val(), idAuto: $("#ddlAccionAC").val(),
                        estado: $("#ddlEstadoAC").val()
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
				{ field: "WRKF_AID", width: 4, title: "Id", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${WRKF_AID}</a>" },
				{ field: "WRKF_ANAME", width: 10, title: "Descripción", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${WRKF_ANAME}</a>" },
				{ field: "WRKF_ALABEL", width: 10, title: "Etqueta", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${WRKF_ALABEL}</a>" },
                { field: "WRKF_ATNAME", width: 10, title: "Tipo Acción", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${WRKF_ATNAME}</a>" },
                { field: "WRKF_DTNAME", width: 10, title: "Tipo Dato", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${WRKF_DTNAME}</a>" },
                { field: "PROC_NAME", width: 10, title: "Procedimiento", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${PROC_NAME}</a>" },
                { field: "TIPO_ACCION", width: 10, title: "Acción Automatica", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${TIPO_ACCION}</a>" },
                //{ field: "ESTADO", width: 4, title: "Estado", template: "<a id='single_2'  href=javascript:getBPS('${WRKF_AID}') style='color:gray !important;'>${ESTADO}</a>" },
			]
    });


};