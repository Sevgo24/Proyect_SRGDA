
var mvInitDivisiones = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdSubDivisionView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventSubDivision" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; ">';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Tipo División : </td>'
    elemento += '<td style="width:30px"><select id="ddlTipoDivision" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Nombre : </td>';
    elemento += '<td style="width:30px"><input type="text" id="txtNombreDiv" placeholder="Ingrese Nombre" name="txtNombreDiv" style="width:250px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarDiviion"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSubDivision"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridVal"></div>';
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
        title: "Búsqueda General de Divisiones."
    });

    if(parametro.bloqueoTipoDiv == '1')     $("#ddlTipoDivision").prop('disabled', true);

    loadTipoDivisiones('ddlTipoDivision', parametro.tipoDiv);

    $("#btnLimpiarSubDivision").on("click", function () {
        limpiar();
    });

    $("#btnBuscarDiviion").on("click", function () {
        $('#gridVal').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDivision").val(), nombre: $("#txtNombreDiv").val(), estado: 0, page: 1, pageSize: K_PAGINACION.LISTAR_5 });
    });


    $("#txtNombreDiv").keypress(function (e) {
        if (e.which == 13) {
            $('#gridVal').data('kendoGrid').dataSource.query({ tipo: $("#ddlTipoDivision").val(), nombre: $("#txtNombreDiv").val(), estado: 0, page: 1, pageSize: K_PAGINACION.LISTAR_5 });
        }
    });
    $("#txtNombreDiv").focus();
    loadDataValores();
    
};

function limpiar() {
    $("#txtNombreDiv").val('').focus();
}

var getidDivision = function (id) {
    var hidIdOficinaView = $("#hidIdSubDivisionView").val();
    var fnc = $("#hidIdEventSubDivision").val();
    $("#" + hidIdOficinaView).dialog("close");
    eval(fnc + " ('" + id + "');");
};


function loadDataValores() {
    $("#gridVal").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_5,
            transport: {
                read: {
                    type: "POST",
                    url: "../Divisiones/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { tipo: $("#ddlTipoDivision").val(), nombre: $("#txtNombreDiv").val(), estado: 1 })
                }
            },
            schema: { data: "REFDIVISIONES", total: 'TotalVirtual' }
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
                { field: "DAD_ID", width: 2, title: "Id", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DAD_ID}</a>" },
                { field: "DAD_TYPE", width: 3, hidden: true, title: "Tipo Div", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DAD_TYPE}</a>" },
				{ field: "DAD_TNAME", width: 7, hidden:true, title: "Tipo", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DAD_TNAME}</a>" },
                { field: "DAD_NAME", width: 10, title: "División", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DAD_NAME}</a>" },
                { field: "DIV_DESCRIPTION", width: 20, title: "Descripción", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DIV_DESCRIPTION}</a>" },
                { field: "TIS_N", width: 10, hidden: true, title: "<font size=2px>Id territorio</font>", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${TIS_N}</a>" },
				{ field: "NAME_TER", width: 4, hidden: true, title: "<font size=2px>Territorio</font>", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${NAME_TER}</a>" },
                { field: "DAD_CODE", width: 5, hidden: true, title: "<font size=2px>Ident. Corta</font>", template: "<a id='single_2'  href=javascript:getidDivision('${DAD_ID}') style='color:gray !important;'>${DAD_CODE}</a>" },
                //{ field: "ESTADO", width: 3, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${DAD_ID}') style='color:gray !important;'>${ESTADO}</a>" },
			]
    });
}
