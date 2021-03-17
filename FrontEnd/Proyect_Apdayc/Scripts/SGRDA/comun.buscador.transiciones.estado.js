var mvInitBuscarTransiciones = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewT" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventT" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
   
    elemento += '<tr>';
    elemento += '<td>Ciclo de Aprobación :</td>';
    elemento += '<td><select id="ddlCiclo" /></td>';
    elemento += '<td>Evento :</td>';
    elemento += '<td><select id="ddlEventos" /></td>';
    elemento += '</tr>';


    elemento += '<tr>';   
    elemento += '<td>Estado Origen :</td>';
    elemento += '<td><select id="ddlOrigen" /></td>';
    elemento += '<td>Estado Destino :</td>';
    elemento += '<td><select id="ddlDestino" /></td>';
    elemento += '<td>Estado :</td>';
    elemento += '<td><select id="ddlEstado" /></td>';
    elemento += '</tr>';


    elemento += '</tr>'; 

    elemento += '<tr>';
    elemento += '<td colspan="7"><center><button id="btnBuscarT"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarT"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridT"></div>';
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
        height: 540,
        title: "Búsqueda General de transiciones de estado."
    });
    $("#btnBuscarT").on("click", function () {
        $('#gridT').data('kendoGrid').dataSource.query({ idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEventos").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });
    $("#btnLimpiarT").on("click", function () {
        limpiarBusqueda();
    });

    loadEstadosMaestro("ddlEstado");
    LoadModuloNombre('ddlCliente', 0);
    LoadCicloAprobacion('ddlCiclo');
    LoadEventos('ddlEventos');
    loadEstadoWF('ddlOrigen');
    loadEstadoWF('ddlDestino');

    loadDataTransiciones();
};

var getTran = function (id) {
    var hidIdidModalViewT = $("#hidIdidModalViewT").val();
    var fnc = $("#hidIdEventT").val();
    $("#" + hidIdidModalViewT).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusqueda() {
    $("#ddlCiclo").val(0);
    $("#ddlEventos").val(0);
    $("#ddlOrigen").val(0);
    $("#ddlDestino").val(0);
    $("#ddlEstado").val(1);
}

function loadDataTransiciones() {
    $("#gridT").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: '../Transiciones/Listar',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    return $.extend({}, options, { idCiclo: $("#ddlCiclo").val(), idEvento: $("#ddlEventos").val(), idEstadoIni: $("#ddlOrigen").val(), idEstadoFin: $("#ddlDestino").val(), st: $("#ddlEstado").val() })
                }
            },
            schema: { data: 'ListarTransiciones', total: 'TotalVirtual' }
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
				//{ title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${WRKF_TID}'/>" },
				{ field: "WRKF_TID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${WRKF_TID}</a>" },
				{ field: "WRKF_NAME", width: 10, title: "Ciclo", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${WRKF_NAME}</a>" },
				{ field: "WRKF_ENAME", width: 10, title: "Evento", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${WRKF_ENAME}</a>" },
                { field: "ESTADO_INI", width: 10, title: "Estado Desde", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${ESTADO_INI}</a>" },
                { field: "ESTADO_FIN", width: 10, title: "Estado Hasta", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${ESTADO_FIN}</a>" },
                { field: "LOG_USER_CREAT", width: 6, title: "Usuario Crea.", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
                { field: "ESTADO", width: 4, title: "Estado", template: "<a id='single_2'  href=javascript:getTran('${WRKF_TID}') style='color:gray !important;'>${ESTADO}</a>" },

			]
    });
}

