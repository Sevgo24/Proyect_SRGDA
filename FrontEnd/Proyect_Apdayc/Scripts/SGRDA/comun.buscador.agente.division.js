
var mvInitAgenteDivision = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdSubDivisionViewAR" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventSubDivisionAR" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; ">';

    //elemento += '<tr>';
    //elemento += '<td style="width:30px">División Administrativa : <input type="hidden"  id="hidIdDivisionAdmAR" > </td>'
    //elemento += '<td style="width:30px"> <label id="lblIdDivisionAdmAR"></label> </td>';
    //elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Nombre :  <input type="hidden" id="hidIdDivisionAdmAR" > </td>';
    elemento += '<td style="width:30px"><input type="text" id="txtRecaudadorAR" placeholder="Ingrese Recaudador" name="txtRecaudadorAR" style="width:250px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarDiviionAR"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarDivisionAR"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridDivisionAR"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 770,
        height: 490,
        title: "Búsqueda Agente de Recaudo."
    });
    
    $("#btnBuscarDiviionAR").on("click", function () {
        $('#gridDivisionAR').data('kendoGrid').dataSource.query({
            idOficina: 0,
            iddivision: $("#hidIdDivisionAdmAR").val(),
            agenteRecaudo: $("#txtRecaudadorAR").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_5
        });
    });

    $("#txtRecaudadorAR").keypress(function (e) {
        if (e.which == 13) {
            $('#gridDivisionAR').data('kendoGrid').dataSource.query({
                idOficina: 0,
                iddivision: $("#hidIdDivisionAdmAR").val(),
                agenteRecaudo: $("#txtRecaudadorAR").val(),
                page: 1,
                pageSize: K_PAGINACION.LISTAR_5
            });
        }
    });

    $("#txtNombreDiv").focus();
};

function loadDataDivisionAgenteAR() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../AgenteDivision/ListarAgenteRecaudo",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        idOficina: 0,
                        iddivision: $("#hidIdDivisionAdmAR").val(),
                        agenteRecaudo: $("#txtRecaudadorAR").val(),
                    })
            }
        },
        schema: { data: "ListarAgenteRecaudo", total: 'TotalVirtual' }
    });

    $("#gridDivisionAR").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
                { field: "COLL_OFF_ID", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:getidDivisionAR('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${COLL_OFF_ID}</a>" },
                { field: "OFF_NAME", width: 10, title: "Agencia Recaudo", template: "<a id='single_2'  href=javascript:getidDivisionAR('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_NAME}</a>" },
                { field: "DIVISION", width: 10, title: "División", template: "<a id='single_2'  href=javascript:getidDivisionAR('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DIVISION}</a>" },
                { field: "RECAUDADOR", width: 15, title: "Recaudador", template: "<a id='single_2'  href=javascript:getidDivisionAR('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RECAUDADOR}</a>" },
                { field: "ROL", width: 6, title: "Rol", template: "<a id='single_2'  href=javascript:getidDivisionAR('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ROL}</a>" },
                //{ field: "F_INICIAL", width: 6, title: "F. Inicial", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_INICIAL}</a>" },
                //{ field: "F_FINAL", width: 6, title: "F. Final", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_FINAL}</a>" },
        ]
    });
};

var getidDivisionAR = function (id) {
    var hidIdOficinaView = $("#hidIdSubDivisionViewAR").val();
    var fnc = $("#hidIdEventSubDivisionAR").val();
    $("#" + hidIdOficinaView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

/*
function limpiar() {
    $("#txtNombreDiv").val('').focus();
}




*/