var mvInitModalidadUsoauxiliar = function (parametro) {

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewModauxiliar" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventModauxiliar" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:130px">Tipo de Creaciones </td><td style="width:10px"><select id="ddlTipCreacionauxiliar" style="width: 190px"/></td>';
    elemento += '<td style="width:130px">Tipo de Derecho </td><td style="width:30px"><select id="ddlTipDerechoauxiliar" style="width: 190px"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px">Origen de Mod. Uso </td> <td style="width:10px"><select id="ddlOriModalidadauxiliar"/></td>';
    elemento += '<td style="width:130px">Nivel de Incidencia  </td> <td style="width:10px"><select id="ddlNivIncidenciaauxiliar"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px">Grupo Modalidad </td> <td style="width:10px"><select id="dllGruModalidadauxiliar"/></td>';
    elemento += '<td style="width:130px">Tipo de Uso de Obra  </td style="width:10px"> <td><select id="ddlTipUsoRepertorioauxiliar"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px">Tipo Sociedad </td> <td style="width:10px"><select id="ddlTipSociedadauxiliar"/></td>';
    elemento += '<td style="width:130px">Mod. de Uso de Repertorio </td> <td style="width:10px"><select id="ddlModUsoRepertorioauxiliar"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px">Descripción </td> <td><input type="text" id="txtDescripcion" placeholder="Ingrese la descripción." style=" width:300px;"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarModalidadauxiliar"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarauxiliar"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridModalidadusoauxiliar"></div>';
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

    loadTipoCreacion('ddlTipCreacionauxiliar', '0');

    $("#ddlTipCreacionauxiliar").change(function () {
        var codigo = $("#ddlTipCreacionauxiliar").val();
        loadTipoDerecho('ddlTipDerechoauxiliar', codigo, '0');
    });

    loadTipoDerecho('ddlTipDerechoauxiliar', '0', '0');
    loadTipoModUso('ddlOriModalidadauxiliar', '0');
    loadTipoGrupo('dllGruModalidadauxiliar', '0');
    loadTipoSociedad('ddlTipSociedadauxiliar', '0');
    loadTipoIncidencia('ddlNivIncidenciaauxiliar', '0');
    loadTipoObra('ddlTipUsoRepertorioauxiliar', '0');
    loadTipoRepertorio('ddlModUsoRepertorioauxiliar', '0');

    $("#btnBuscarModalidadauxiliar").on("click", function () { loadDataModalidadusoAuxiliar(); });
    $("#btnLimpiarauxiliar").on("click", function () { limpiarBusquedaModAuxiliar(); });

    var modalidadauxiliar;
};









var mvInitBuscarTarifa = function (parametro) {

    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidTarifaView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidTari" value="' + parametro.event + ' "/>';
    
    elemento += '<div id="ContenedormvModalidadauxiliar"></div>';

    elemento += '<table border=0  style=" width:100%; border:1px;">';     

    elemento += '<td>';
    elemento += '<input type="hidden" id="hidTipoUso"/>';
    elemento += '<input type="hidden" id="hidNivelIncidencia"/>';
    elemento += '<input type="hidden" id="hidSociedad"/>';
    elemento += '<input type="hidden" id="hidRepertorio"/>';
    elemento += '</td>';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Código</td><td style="width: 250px"> <input type="text" id="txtCodigoTari" style="width: 60px" maxlength="10"/></td>';

    elemento += '<td>Moneda</td><td><select id="ddlMonedaTari" /></td>';
    elemento += '</tr>';
   
    elemento += '<tr>';
    elemento += '<td> Modalidad: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarModalidadusoauxiliar" style="cursor:pointer;" alt="Búsqueda de Modalidad" title="Búsqueda de Modalidad"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdModalidadauxiliar"/> <lable id="lbModalidadauxiliar" style="cursor:pointer;" alt="Búsqueda de Modalidad" title="Búsqueda de Modalidad">Todos</lable> </td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '<td>Tipo de uso</td><td colspan="3"><select id="ddlModUsoTari" /></td>';
    elemento += '</td>';
    elemento += '</tr>';  
                                        
    elemento += '<tr>';
    elemento += '<td>Nivel de incidencia</td><td><select id="ddlNivIncTari" /></td>';

     elemento += '<td>Tipo de sociedad</td><td><select id="ddlTipSociTari" /></td>';
    elemento += '</tr>';  

    elemento += '<tr>';
    elemento += '<td>Repertorio</td><td><select id="ddlModUsoRepertTari" /></td>';

    elemento += '<td>Estado</td><td><select id="ddlEstadoTari" /></td>';
    elemento += '</tr>';
    
    elemento += '<tr>';
    elemento += '<td>Descripción </td>';
    elemento += '<td colspan=3>  <input type="text" id="txtDescripcionTarifa" style="width: 450px" maxlength="100"/> </td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="6"><center><button id="btnBuscarTari"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarTari"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td align="center"><div id="gridTari"></div></td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '</div>';

    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        height: 520,
        title: "Búsqueda General de Tarifa."
    });
    
    $("#btnBuscarTari").on("click", function () {
        if ($("#txtCodigoTari").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigoTari").val();

        if ($("#ddlEstadoTari").val() == "")
            st = 0;
        else
            st = $("#ddlEstadoTari").val();

        $('#gridTari').data('kendoGrid').dataSource.query({
            IdTarifa: codigo,
            moneda: $('#ddlMonedaTari').val(),
            moduso: $('#ddlModUsoTari').val(),
            incidencia: $('#ddlNivIncTari').val(),
            sociedad: $('#ddlTipSociTari').val(),
            repertorio: $('#ddlModUsoRepertTari').val(),
            st: st,
            descripcion: $('#txtDescripcionTarifa').val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_10
        });
    });

    $("#txtCodigoTari").keypress(function (e) {
        if (e.which == 13) {            
            if ($("#txtCodigoTari").val() == "")
                codigo = 0;
            else
                codigo = $("#txtCodigoTari").val();

            if ($("#ddlEstadoTari").val() == "")
                st = 0;
            else
                st = $("#ddlEstadoTari").val();

            $('#gridTari').data('kendoGrid').dataSource.query({
                IdTarifa: codigo,
                moneda: $('#ddlMonedaTari').val(),
                moduso: $('#ddlModUsoTari').val(),
                incidencia: $('#ddlNivIncTari').val(),
                sociedad: $('#ddlTipSociTari').val(),
                repertorio: $('#ddlModUsoRepertTari').val(),
                st: st,
                descripcion: $('#txtDescripcionTarifa').val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_10
            });
        }
    });

    $("#txtDescripcionTarifa").keypress(function (e) {
        if (e.which == 13) {
            if ($("#txtCodigoTari").val() == "")
                codigo = 0;
            else
                codigo = $("#txtCodigoTari").val();

            if ($("#ddlEstadoTari").val() == "")
                st = 0;
            else
                st = $("#ddlEstadoTari").val();

            $('#gridTari').data('kendoGrid').dataSource.query({
                IdTarifa: codigo,
                moneda: $('#ddlMonedaTari').val(),
                moduso: $('#ddlModUsoTari').val(),
                incidencia: $('#ddlNivIncTari').val(),
                sociedad: $('#ddlTipSociTari').val(),
                repertorio: $('#ddlModUsoRepertTari').val(),
                st: st,
                descripcion: $('#txtDescripcionTarifa').val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_10
            });
        }
    });

    $("#btnLimpiarTari").on("click", function () {
        limpiarBusquedaTari();

        if ($("#txtCodigoTari").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigoTari").val();

        if ($("#ddlEstadoTari").val() == "")
            st = 0;
        else
            st = $("#ddlEstadoTari").val();

        $('#gridTari').data('kendoGrid').dataSource.query({
            IdTarifa: codigo,
            moneda: $('#ddlMonedaTari').val(),
            moduso: $('#ddlModUsoTari').val(),
            incidencia: $('#ddlNivIncTari').val(),
            sociedad: $('#ddlTipSociTari').val(),
            repertorio: $('#ddlModUsoRepertTari').val(),
            IdModalidad: $('#hidIdModalidadauxiliar').val() == "" ? 0 : $('#hidIdModalidadauxiliar').val(),
            st: st,
            descripcion: $('#txtDescripcionTarifa').val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_10
        });
    });

    limpiarBusquedaTari();
    loadMonedas('ddlMonedaTari', '0')
    loadTipoObra('ddlModUsoTari', '0');
    loadTipoIncidencia('ddlNivIncTari', '0');
    loadTipoSociedad('ddlTipSociTari', '0');                                                                                                                                                                                                                                                                                                                
    loadTipoRepertorio('ddlModUsoRepertTari', '0');
    loadEstadosMaestro("ddlEstadoTari");
    loadDataFoundTari();
    mvInitModalidadUsoauxiliar({ container: "ContenedormvModalidadauxiliar", idButtonToSearch: "btnBuscarModalidadusoauxiliar", idDivMV: "mvModalidadauxiliar", event: "reloadEventoModalidadauxiliar", idLabelToSearch: "lbModalidadauxiliar" });
};

var getidTari = function (id) {
    var hidIdidTarifaView = $("#hidIdidTarifaView").val();
    var fnc = $("#hidTari").val();
    $("#" + hidIdidTarifaView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var reloadEventoModalidadauxiliar = function (idSel) {
    $("#hidIdModalidadauxiliar").val(idSel);
    obtenerNombreModalidad(idSel, "lbModalidadauxiliar");
};

function limpiarBusquedaTari() {
    $("#txtCodigoTari").val("");
    $("#lbModalidadTari").val("");
    $("#hidModalidadTari").val(0);
    $("#hidIdModalidadauxiliar").val(0);
    $("#ddlMonedaTari").val(0);
    $("#ddlModUsoTari").val(0);
    $("#ddlNivIncTari").val(0);
    $("#ddlTipSociTari").val(0);
    $("#ddlModUsoRepertTari").val(0);
    $("#ddlEstadoTari").val(1);
    $("#txtDescripcionTarifa").val('');
    $("#lbModalidadauxiliar").html("Todos");
};

function loadDataFoundTari() {

    if ($("#txtCodigoTari").val() == "")
        codigo = 0;
    else
        codigo = $("#txtCodigoTari").val();

    if ($("#ddlEstadoTari").val() == "")
        st = 0;
    else
        st = $("#ddlEstadoTari").val();

    $("#gridTari").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_10,
            transport: {
                read: {
                    url: "../Tarifa/ListarTarifa",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            IdTarifa: codigo,
                            moneda: $('#ddlMonedaTari').val(),
                            moduso: $('#ddlModUsoTari').val(),
                            incidencia: $('#ddlNivIncTari').val(),
                            sociedad: $('#ddlTipSociTari').val(),
                            repertorio: $('#ddlModUsoRepertTari').val(),
                            IdModalidad: $('#hidIdModalidadauxiliar').val() == "" ? 0 : $('#hidIdModalidadauxiliar').val(),
                            st: st,
                            descripcion: $('#txtDescripcionTarifa').val()
                        })
                }
            },
            schema: { data: "listaTarifa", total: 'TotalVirtual' }
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
				{ field: "RATE_ID", width: 50, title: "Id", template: "<a id='single_2'  href=javascript:getidTari('${RATE_ID}') style='color:gray !important;'>${RATE_ID}</a>" },
				{ field: "RATE_LDESC", title: "Nombre tarifa", template: "<a id='single_2'  href=javascript:getidTari('${RATE_ID}') style='color:gray !important;'>${RATE_LDESC}</a>" },
                {
                    field: "RATE_START",
                    width: 130,
                    type: "date",
                    title: "Fecha vigencia",
                    template: "<font color='green'><a id='single_2' href=javascript:getidTari('${RATE_ID}') style='color:gray !important;'>" + '#=(RATE_START==null)?"":kendo.toString(kendo.parseDate(RATE_START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>"
                },
                {
                    field: "ESTADO", width: 70, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getidTari('${RATE_ID}') style='color:gray !important;'>${ESTADO}</a>"
                }
			]
    });
}


var getidModAuxiliar = function (id) {
    var hidIdidModalViewModauxiliar = $("#hidIdidModalViewModauxiliar").val();
    var fnc = $("#hidIdEventModauxiliar").val();
    $("#" + hidIdidModalViewModauxiliar).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusquedaModAuxiliar() {
    loadTipoCreacion('ddlTipCreacionauxiliar', '0');
    loadTipoDerecho('ddlTipDerechoauxiliar', '0', '0');
    loadTipoModUso('ddlOriModalidadauxiliar', '0');
    loadTipoGrupo('dllGruModalidadauxiliar', '0');
    loadTipoSociedad('ddlTipSociedadauxiliar', '0');
    loadTipoIncidencia('ddlNivIncidenciaauxiliar', '0');
    loadTipoObra('ddlTipUsoRepertorioauxiliar', '0');
    loadTipoRepertorio('ddlModUsoRepertorioauxiliar', '0');
}

function loadDataModalidadusoAuxiliar() {

    modalidadauxiliar = {
        MOD_DEC: $('#txtDescripcion').val(),
        MOD_ORIG: $('#ddlOriModalidadauxiliar').val(),
        MOD_SOC: $('#ddlTipSociedadauxiliar').val(),
        CLASS_COD: $('#ddlTipCreacionauxiliar').val(),
        MOG_ID: $('#dllGruModalidadauxiliar').val(),
        RIGHT_COD: $('#ddlTipDerechoauxiliar').val(),
        MOD_INCID: $('#ddlNivIncidenciaauxiliar').val(),
        MOD_USAGE: $('#ddlTipUsoRepertorioauxiliar').val(),
        MOD_REPER: $('#ddlModUsoRepertorioauxiliar').val()
    }

    $("#gridModalidadusoauxiliar").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_5,
            transport: {
                read: {
                    url: "../ModalidadUso/Listar", dataType: "json", data: modalidadauxiliar
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
				{ field: "MOD_ID", width: 50, title: "Id", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${MOD_ID}</a>" },
				{ field: "MOD_DEC", title: "Descripción", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${MOD_DEC}</a>" },
                { field: "TIPO_DERECHO", title: "Tipo de Derecho", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${TIPO_DERECHO}</a>" },
                { field: "TIPO_CREACION", title: "Tipo de Creación", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${TIPO_CREACION}</a>" },
                { field: "ORIGEN", title: "Origen de Mod. Uso", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${ORIGEN}</a>" },
                { field: "INCIDENCIA", title: "Nivel de Incidencia", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${INCIDENCIA}</a>" },
                { field: "MODALIDAD", title: "Grupo Modalidad", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${MODALIDAD}</a>" },
                { field: "TIPO_OBRA", title: "Tipo de Uso de Obra", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${TIPO_OBRA}</a>" },
                { field: "TIPO_SOCIEDAD", title: "Tipo Sociedad", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${TIPO_SOCIEDAD}</a>" },
                { field: "USO_REPERTORIO", title: "Modulo de Uso de Repertorio", template: "<a id='single_2'  href=javascript:getidModAuxiliar('${MOD_ID}') style='color:gray !important;'>${USO_REPERTORIO}</a>" },
			]
    });
}
