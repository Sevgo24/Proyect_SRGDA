

var mvInitOficina = function (parametro) {

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdOficinaView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventOficina" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:30px">Oficina </td><td style="width:30px"><input type="text" id="txtOficinaSearch" placeholder="Ingrese Oficina" name="txtOficinaSearch" style="width:250px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Estado </td><td style="width:30px"><select id="ddlEstadoOficina" style="width: 90px"/></td>';
    elemento += '<input type="hidden" id="hidMOD_ID">';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarOficina"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarOficina"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridOficina"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 790,
        height: 560,
        title: "Búsqueda General de Oficinas."
    });

    loadEstadosMaestro("ddlEstadoOficina");
    
    $("#txtOficinaSearch").keypress(function (e) {
        if (e.which == 13) {
            $('#gridOficina').data('kendoGrid').dataSource.query({
                dato: $("#txtOficinaSearch").val(),
                estado: $("#ddlEstadoOficina").val(),
                page: 1,
                pageSize: K_PAGINACION.LISTAR_10
            });
        }
    });

    $("#btnBuscarOficina").on("click", function ()
    {        
        $('#gridOficina').data('kendoGrid').dataSource.query({
            dato: $("#txtOficinaSearch").val(), 
            estado: $("#ddlEstadoOficina").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_10
        });
    });

    $("#btnLimpiarOficina").on("click", function ()
    {
        limpiarBusquedaOficina();

        $('#gridOficina').data('kendoGrid').dataSource.query({
            dato: $("#txtOficinaSearch").val(),
            estado: $("#ddlEstadoOficina").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_10
        });
    });

    loadDataOficina();
};

var getidOficina = function (id) {
    var hidIdOficinaView = $("#hidIdOficinaView").val();
    var fnc = $("#hidIdEventOficina").val();
    $("#" + hidIdOficinaView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusquedaOficina() {
    $("#ddlEstadoOficina").val(1);
    $("#txtOficinaSearch").val("");
}

function loadDataOficina() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_10,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../Office/Listar",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { dato: $("#txtOficinaSearch").val(), estado: $("#ddlEstadoOficina").val() })
            }
        },
        schema: { data: "BEREC_OFFICE", total: 'TotalVirtual' }
    });

    $("#gridOficina").kendoGrid({
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
        columns:
               [
				{ field: "OFF_ID", width: 4, title: "Id", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_ID}</a>" },
				{ field: "SOFF_ID", hidden: true, width: 20, title: "SOFF_ID", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${SOFF_ID}</a>" },
				{ field: "ADD_ID", hidden: true, width: 20, title: "ADD_ID", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADD_ID}</a>" },
				{ field: "OFF_NAME", width: 20, title: "Oficina", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_NAME}</a>" },
                { field: "ADDRESS", width: 20, title: "Dirección", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADDRESS}</a>" },
                { field: "HQ_IND", width: 10, title: "Indicador", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${HQ_IND}</a>" },
                { field: "ENDSDES", width: 8, title: "Estado", template: "<a id='single_2'  href=javascript:getidOficina('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ENDSDES}</a>" },
               ]
    });
};

