
var mvInitBuscarGrupoModalidad = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdGrupoModVMView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventGrupoModVM" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; ">';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Ingrese Descripción : </td>'
    elemento += '<td style="width:30px"><input type="text" id="txtNombreGrupoModVM" placeholder="Ingrese Nombre" name="txtNombreGrupoModVM" style="width:250px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarGrupoModVM"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarGrupoModVM"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridGrupoModVM"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 710,
        height: 490,
        title: "Búsqueda General Grupo Modalidad."
    });

    //if (parametro.bloqueoTipoDiv == '1') $("#ddlTipoDivision").prop('disabled', true);

    //loadTipoDivisiones('ddlTipoDivision', parametro.tipoDiv);

    //$("#btnLimpiarSubDivision").on("click", function () {
    //    limpiar();
    //});

    //$("#btnBuscarDiviion").on("click", function () {
    //    $('#gridVal').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDivision").val(), nombre: $("#txtNombreDiv").val(), estado: 0, page: 1, pageSize: K_PAGINACION.LISTAR_5 });
    //});


    //$("#txtNombreDiv").keypress(function (e) {
    //    if (e.which == 13) {
    //        $('#gridVal').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDivision").val(), nombre: $("#txtNombreDiv").val(), estado: 0, page: 1, pageSize: K_PAGINACION.LISTAR_5 });
    //    }
    //});
    //$("#txtNombreDiv").focus();
    //loadDataValores();

    //$("#txtNombreGrupoModVM").keypress(function (e) {
    //    if (e.which == 13) {
    //        loadDataGrupoModVM();
    //    }
    //});

    $("#txtNombreGrupoModVM").keypress(function (e) {
        if (e.which == 13) {
            $('#gridGrupoModVM').data('kendoGrid').dataSource.query({ dato: $("#txtNombreGrupoModVM").val(), st: 1, page: 1, pageSize: K_PAGINACION.LISTAR_10 });
        }
    });

    loadDataGrupoModVM();
};

var getIdGrupoModVM = function (id) {
    var hidIdOficinaView = $("#hidIdGrupoModVMView").val();
    var fnc = $("#hidIdEventGrupoModVM").val();
    $("#" + hidIdOficinaView).dialog("close");
    eval(fnc + " ('" + id + "');");
};


function loadDataGrupoModVM() {
    $("#gridGrupoModVM").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_10,
            transport: {
                read: {
                    type: "POST",
                    url: "../GRUPOMODALIDAD/usp_listar_GrupoModalidadJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { dato: $("#txtNombreGrupoModVM").val(), st: 1 })
                }
            },
            schema: { data: "RECMODGROUP", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
			    { field: "MOG_ID",   width: 10, title: "Id", template: "<a id='single_2'  href=javascript:getIdGrupoModVM('${MOG_ID}') style='color:gray !important;'>${MOG_ID}</a>" },
                { field: "MOG_DESC", width: 90, title: "Grupo de Modalidad", template: "<a id='single_2'  href=javascript:getIdGrupoModVM('${MOG_ID}') style='color:gray !important;'>${MOG_DESC}</a>" },
			]
    });
}

