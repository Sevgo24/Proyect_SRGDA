var defaultTipoCreacion = 'MW';
var defaultOrigeModalidad = 'NAC';
var defaultTipoSociedad = 'AUT';

var mvInitModalidadUsoaux = function (parametro) {
    //var idContenedor = parametro.container;
    //var btnEvento = parametro.idButtonToSearch;
    //var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewModaux" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventModaux" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:180px">Tipo de Creaciones </td><td style="width:30px"><select id="ddlTipCreacionaux" style="width: 190px"/></td>';
    elemento += '<td style="width:180px">Tipo de Derecho </td><td style="width:30px"><select id="ddlTipDerechoaux" style="width: 190px"/></td>';
    elemento += '<input type="hidden" id="hidMOD_IDaux">';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:180px">Origen de Mod. Uso </td> <td style="width:30px"><select id="ddlOriModalidadaux"/></td>';
    elemento += '<td style="width:180px">Nivel de Incidencia  </td> <td style="width:30px"><select id="ddlNivIncidenciaaux"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:180px">Grupo Modalidad </td> <td style="width:30px"><select id="dllGruModalidadaux"/></td>';
    elemento += '<td style="width:180px">Tipo de Uso de Obra  </td style="width:30px"> <td><select id="ddlTipUsoRepertorioaux"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:180px">Tipo Sociedad </td> <td style="width:30px"><select id="ddlTipSociedadaux"/></td>';
    elemento += '<td style="width:180px">Mod. de Uso de Repertorio </td> <td style="width:30px"><select id="ddlModUsoRepertorioaux"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px">Descripción </td> <td  colspan="4"><input type="text" id="txtDescripcionModAux" placeholder="Ingrese la descripción." style=" width:300px;"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarModalidadaux"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiaraux"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridModalidadusoaux"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 900,
        height: 500,
        title: "Búsqueda General de Modalidad Uso."
    });

    loadTipoCreacion('ddlTipCreacionaux', defaultTipoCreacion);

    $("#ddlTipCreacionaux").change(function () {
        var codigo = $("#ddlTipCreacionaux").val();
        loadTipoDerecho('ddlTipDerechoaux', codigo, '0');
    });

    loadTipoDerecho('ddlTipDerechoaux', defaultTipoCreacion, '0');
    loadTipoModUso('ddlOriModalidadaux', defaultOrigeModalidad);
    loadTipoGrupo('dllGruModalidadaux', '0');
    loadTipoSociedad('ddlTipSociedadaux', defaultTipoSociedad);
    loadTipoIncidencia('ddlNivIncidenciaaux', '0');
    loadTipoObra('ddlTipUsoRepertorioaux', '0');
    loadTipoRepertorio('ddlModUsoRepertorioaux', '0');

    $("#btnBuscarModalidadaux").on("click", function () { loadDataModalidadusoAux(); });
    $("#btnLimpiaraux").on("click", function () { limpiarBusquedaModAux(); });

    var modalidadaux;
};

var getidModAux = function (id) {
    var hidIdidModalViewModaux = $("#hidIdidModalViewModaux").val();
    var fnc = $("#hidIdEventModaux").val();
    $("#" + hidIdidModalViewModaux).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusquedaModAux() {
    loadTipoCreacion('ddlTipCreacionaux', defaultTipoCreacion);
    loadTipoDerecho('ddlTipDerechoaux', defaultTipoCreacion, '0');//loadTipoDerecho('ddlTipDer','0', '0');
    loadTipoModUso('ddlOriModalidadaux', defaultOrigeModalidad);
    loadTipoGrupo('dllGruModalidadaux', '0');
    loadTipoSociedad('ddlTipSociedadaux', defaultTipoSociedad);
    loadTipoIncidencia('ddlNivIncidenciaaux', '0');
    loadTipoObra('ddlTipUsoRepertorioaux', '0');
    loadTipoRepertorio('ddlModUsoRepertorioaux', '0');
    $('#txtDescripcionModAux').val('');
}

function loadDataModalidadusoAux() {

    modalidadaux = {
        MOD_DEC: $('#txtDescripcionModAux').val(),
        MOD_ORIG: $('#ddlOriModalidadaux').val(),
        MOD_SOC: $('#ddlTipSociedadaux').val(),
        CLASS_COD: $('#ddlTipCreacionaux').val(),
        MOG_ID: $('#dllGruModalidadaux').val(),
        RIGHT_COD: $('#ddlTipDerechoaux').val(),
        MOD_INCID: $('#ddlNivIncidenciaaux').val(),
        MOD_USAGE: $('#ddlTipUsoRepertorioaux').val(),
        MOD_REPER: $('#ddlModUsoRepertorioaux').val()
    }

    $("#gridModalidadusoaux").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            //pageSize: K_PAGINACION.LISTAR_20,
            pageSize: K_PAGINACION.LISTAR_5,
            transport: {
                read: {
                    url: "../ModalidadUso/Listar", dataType: "json", data: modalidadaux
                }
            },
            schema: { data: "ListarModalidad", total: 'TotalVirtual' }
        },
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        navigatable: true,
        scrollable: true,
        columns:
			[
               { field: "MOD_ID", width: 35, title: "Id", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ID}</a>" },
                { field: "MODALIDAD", width: 80, title: "Grupo Modalidad", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MODALIDAD}</a>" },
                { field: "MOD_DEC", width: 120, title: "Descripción", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_DEC}</a>" },
                { field: "TIPO_DERECHO", width: 80, title: "Tipo de Derecho", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_DERECHO}</a>" },
                { field: "TIPO_CREACION", width: 80, title: "Tipo de Creación", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_CREACION}</a>" },
                { field: "MOD_INCID", width: 70, title: "Nivel de </br>Incidencia", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_INCID}</a>" },
                { field: "MOD_ORIG", width: 75, title: "Origen de </br>Mod. Uso", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ORIG}</a>" },
                { field: "MOD_USAGE", width: 75, title: "Tipo </br>Uso de Obra", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_USAGE}</a>" },
			    { field: "MOG_SOC", width: 70, title: "Tipo</br> Sociedad", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOG_SOC}</a>" },
                { field: "MOD_REPER", width: 75, title: "Modo Uso</br>de Repertorio", template: "<a id='single_2'  href=javascript:getidModAux('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_REPER}</a>" },
			]
    });
}