

var mvInitSubDivisiones = function (parametro) {

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdSubDivisionView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventSubDivision" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';
      


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; ">';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Division : </td>'
    elemento += '<td style="width:30px"><select id="ddlDivision" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Subdivisión : </td>'
    elemento += '<td style="width:30px"><select id="ddlSubdivisionSub" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Dependencia : </td>'
    elemento += '<td style="width:30px"><select id="ddlDependenciaSub" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:30px">Nombre : </td>';
    elemento += '<td style="width:30px"><input type="text" id="txtNombreSub" placeholder="Ingrese Nombre" name="txtNombreSubDivision" style="width:250px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSubDiviion"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSubDivision"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
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
        title: "Búsqueda General de Oficinas."
    });

    loadDivisionXtipo('ddlDivision', 'ADM', 0);
    loadValSubFiltros(0);

    $("#ddlDivision").change(function () {
        var division = $("#ddlDivision").val();
        loadValSubFiltros(division);
    });
    
    $("#btnLimpiarSubDivision").on("click", function () {
        limpiar();
    });

    $("#btnBuscarSubDiviion").on("click", function () {        
        $('#gridVal').data('kendoGrid').dataSource.query({
            id: $("#ddlDivision").val(),
            subId: $("#ddlSubdivisionSub").val(),
            depId: $("#ddlDependenciaSub").val(),
            nombre: $("#txtNombreSub").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_10
        });
    });

    loadDataValores();
};

function loadValSubFiltros(id) {
    var newOption = "<option value='" + "0" + "'>-- SELECCIONE --</option>";
    loadSubdivision('ddlSubdivisionSub', id, 0);
    $("#ddlSubdivisionSub > option").remove();
    $("#ddlSubdivisionSub").append(newOption);

    loadValoresDep('ddlDependenciaSub', id, 0);
    $("#ddlDependenciaSub > option").remove();
    $("#ddlDependenciaSub").append(newOption);

    $("#ddlSubdivisionSub").change(function () {
        var subdivision = $("#ddlSubdivisionSub").val();
        loadValoresDep('ddlDependenciaSub', id, subdivision);
        $("#ddlDependenciaSub > option").remove();
        $("#ddlDependenciaSub").append(newOption);
    });
}


function limpiar() {    
    $("#ddlDivision").val(0);
    loadValSubFiltros(0);
    $("#txtNombreSub").val('');
}

var getidSubDivision = function (id) {
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
            pageSize: K_PAGINACION.LISTAR_10,
            transport: {
                read: {
                    url: "../Divisiones/ListarValores",
                    dataType: "json",
                    data: { id: 0, subId: 0, depId: 0, nombre: '' }
                }
            },
            schema: { data: "REFDIVISIONES_VALUES", total: 'TotalVirtual' }
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
                { field: "DADV_ID", hidden: false, width: 2, title: "Id" },
				{ field: "DAD_BELONGS", hidden: true, width: 10, title: "DAD_BELONGS" },
                { field: "SUBDIVISION", width: 4, title: "Subdivisión", template: "<a id='single_2'  href=javascript:getidSubDivision('${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>${SUBDIVISION}</a>" },
                { field: "DEPENDENCIA", width: 4, title: "Dependencia", template: "<a id='single_2'  href=javascript:getidSubDivision('${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DEPENDENCIA}</a>" },
                { field: "NOMBRE",      width: 4, title: "Nombre", template: "<a id='single_2'  href=javascript:getidSubDivision('${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>${NOMBRE}</a>" },
			]
    });
}
