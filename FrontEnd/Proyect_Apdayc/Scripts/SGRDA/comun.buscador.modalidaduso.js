var defaultTipoCreacion = 'MW';
var defaultOrigeModalidad = 'NAC';
var defaultTipoSociedad = 'AUT';
var mvInitModalidadUso = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewMod" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventMod" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; ">'
    //elemento += '<tr >'
    elemento += '<tr style="display:none">'    
    elemento += '<td style="width:50px">Tipo de Creaciones </td><td style="width:10px"><select id="ddlTipCreacion" style="width: 190px"/></td>';
    elemento += '<td style="width:50px">Tipo de Derecho </td><td style="width:10px"><select id="ddlTipDerecho" style="width: 190px"/></td>';
    elemento += '<input type="hidden" id="hidMOD_ID">';
    elemento += '</tr>';

    //elemento += '<tr>';
    elemento += '<tr style="display:none">'
    elemento += '<td style="width:50px">Origen de Mod. Uso </td> <td style="width:10px"><select id="ddlOriModalidad"/></td>';
    elemento += '<td style="width:50px">Nivel de Incidencia  </td> <td style="width:10px"><select id="ddlNivIncidencia"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:50px">Grupo Modalidad </td> <td style="width:10px"><select id="dllGruModalidad"/></td>';
    //elemento += '<td style="width:130px">Tipo de Uso de Obra  </td style="width:10px"> <td><select id="ddlTipUsoRepertorio"/></td>';
    elemento += '<td style="width:130px;display:none">Tipo de Uso de Obra  </td style="width:10px"> <td style="display:none"><select id="ddlTipUsoRepertorio"/></td>';
    elemento += '</tr>';

    //elemento += '<tr>';
    elemento += '<tr style="display:none">'
    elemento += '<td style="width:50px">Tipo Sociedad </td> <td style="width:10px"><select id="ddlTipSociedad"/></td>';
    elemento += '<td style="width:50px">Mod. de Uso de Repertorio </td> <td style="width:10px"><select id="ddlModUsoRepertorio"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:50px">Modalidad </td> <td><input type="text" id="txtDescripcionMod" placeholder="Ingrese la descripción." style=" width:300px;"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarModalidad"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarMod"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridModalidaduso"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { initLoadCombosModUso(); $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { initLoadCombosModUso(); $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 900,
        height: 500,
        title: "Búsqueda General de Modalidad Uso."
    });

    $("#ddlTipCreacion").change(function () {
        var codigo = $("#ddlTipCreacion").val();
        loadTipoDerecho('ddlTipDerecho', codigo, '0');
    });

    var _cuentaBusModUso = 1; /*addon dbs  20150831- Primera carga los datos */
    //$("#btnBuscarModalidad").on("click", function () { loadDataModalidaduso(); });

    $("#btnBuscarModalidad").on("click", function () {
        $('#gridModalidaduso').data('kendoGrid').dataSource.query({
            MOD_DEC: $('#txtDescripcionMod').val(),
            MOD_ORIG: $('#ddlOriModalidad').val(),
            MOD_SOC: $('#ddlTipSociedad').val(),
            CLASS_COD: $('#ddlTipCreacion').val(),
            MOG_ID: $('#dllGruModalidad').val(),

            RIGHT_COD: $('#ddlTipDerecho').val(),
            MOD_INCID: $('#ddlNivIncidencia').val(),
            MOD_USAGE: $('#ddlTipUsoRepertorio').val(),
            MOD_REPER: $('#ddlModUsoRepertorio').val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_10
        });
    });

    $("#txtDescripcionMod").keypress(function (e) {
        if (e.which == 13) {
            $('#gridModalidaduso').data('kendoGrid').dataSource.query({
                MOD_DEC: $('#txtDescripcionMod').val(),
                MOD_ORIG: $('#ddlOriModalidad').val(),
                MOD_SOC: $('#ddlTipSociedad').val(),
                CLASS_COD: $('#ddlTipCreacion').val(),
                MOG_ID: $('#dllGruModalidad').val(),

                RIGHT_COD: $('#ddlTipDerecho').val(),
                MOD_INCID: $('#ddlNivIncidencia').val(),
                MOD_USAGE: $('#ddlTipUsoRepertorio').val(),
                MOD_REPER: $('#ddlModUsoRepertorio').val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_10
            });
        }
    });

    $("#btnLimpiarMod").on("click", function () {
        $("#txtDescripcionMod").val('');
        limpiarBusqueda();
    });

    loadDataModalidaduso();
};

var getid = function (id) {
    var hidIdidModalViewMod = $("#hidIdidModalViewMod").val();
    var fnc = $("#hidIdEventMod").val();
    $("#" + hidIdidModalViewMod).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusqueda() {
    //loadTipoDerecho('ddlTipDerecho', defaultTipoCreacion, '0');//loadTipoDerecho('ddlTipDer','0', '0');
    //loadTipoCreacion('ddlTipCreacion', defaultTipoCreacion);     
    //loadTipoModUso('ddlOriModalidad', defaultOrigeModalidad);
    //loadTipoGrupo('dllGruModalidad', '0');
    //loadTipoSociedad('ddlTipSociedad', defaultTipoSociedad); 
    //loadTipoIncidencia('ddlNivIncidencia', '0');
    //loadTipoObra('ddlTipUsoRepertorio', '0');
    //loadTipoRepertorio('ddlModUsoRepertorio', '0');
    loadTipoDerecho('ddlTipDerecho', '0', '0');//loadTipoDerecho('ddlTipDer','0', '0');
    loadTipoCreacion('ddlTipCreacion', '0');
    loadTipoModUso('ddlOriModalidad', '0');
    loadTipoGrupo('dllGruModalidad', '0');
    loadTipoSociedad('ddlTipSociedad', '0');
    loadTipoIncidencia('ddlNivIncidencia', '0');
    loadTipoObra('ddlTipUsoRepertorio', '0');
    loadTipoRepertorio('ddlModUsoRepertorio', '0');
}

function loadDataModalidaduso() {
    $("#gridModalidaduso").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_10,
            transport: {
                read: {
                    type: "POST",
                    url: "../ModalidadUso/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            MOD_DEC: $('#txtDescripcionMod').val(),
                            MOD_ORIG: $('#ddlOriModalidad').val(),
                            MOD_SOC: $('#ddlTipSociedad').val(),
                            CLASS_COD: $('#ddlTipCreacion').val(),
                            MOG_ID: $('#dllGruModalidad').val(),

                            RIGHT_COD: $('#ddlTipDerecho').val(),
                            MOD_INCID: $('#ddlNivIncidencia').val(),
                            MOD_USAGE: $('#ddlTipUsoRepertorio').val(),
                            MOD_REPER: $('#ddlModUsoRepertorio').val()
                        })
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
				{ field: "MOD_ID", width: 47, title: "Id", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ID}</a>" },
                { field: "MODALIDAD", width: 130, title: "Grupo Modalidad", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MODALIDAD}</a>" },
                { field: "MOD_DEC", width: 140, title: "Modalidad", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_DEC}</a>" },
                { field: "TIPO_DERECHO", width: 80, title: "Tipo de Derecho", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_DERECHO}</a>" },
                { field: "TIPO_CREACION", width: 80, title: "Tipo de Creación", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_CREACION}</a>" },
                { field: "MOD_INCID", width: 70, title: "Nivel de </br>Incidencia", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_INCID}</a>" },
                { field: "MOD_ORIG", width: 75, title: "Origen de </br>Mod. Uso", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ORIG}</a>" },
                { field: "MOD_USAGE", width: 75, hidden: true,  title: "Tipo </br>Uso de Obra", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_USAGE}</a>" },
			    { field: "MOG_SOC", width: 70, hidden: true,  title: "Tipo</br> Sociedad", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOG_SOC}</a>" },
                { field: "MOD_REPER", width: 75, hidden:true, title: "Modo Uso</br>de Repertorio", template: "<a id='single_2'  href=javascript:getid('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_REPER}</a>" }
			]
    });
}

var initLoadCombosModUso = function () {
    //loadTipoCreacion('ddlTipCreacion', defaultTipoCreacion);
    //loadTipoDerecho('ddlTipDerecho', defaultTipoCreacion, '0'); 
    //loadTipoModUso('ddlOriModalidad', defaultOrigeModalidad);
    //loadTipoGrupo('dllGruModalidad', '0');
    //loadTipoSociedad('ddlTipSociedad', defaultTipoSociedad);
    //loadTipoIncidencia('ddlNivIncidencia', '0');
    //loadTipoObra('ddlTipUsoRepertorio', '0');
    //loadTipoRepertorio('ddlModUsoRepertorio', '0');
    loadTipoDerecho('ddlTipDerecho', '0', '0');//loadTipoDerecho('ddlTipDer','0', '0');
    loadTipoCreacion('ddlTipCreacion', '0');
    loadTipoModUso('ddlOriModalidad', '0');
    loadTipoGrupo('dllGruModalidad', '0');
    loadTipoSociedad('ddlTipSociedad', '0');
    loadTipoIncidencia('ddlNivIncidencia', '0');
    loadTipoObra('ddlTipUsoRepertorio', '0');
    loadTipoRepertorio('ddlModUsoRepertorio', '0');
}




