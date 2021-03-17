

var mvInitBuscarAgente = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewAGENTE" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventAGENTE" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaAGENTE" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdAGENTE" /> <input type="text" id="txtNumeroAGENTE"></td>';
    elemento += '</tr>';


    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialAGENTE" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>';


    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoAGENTE" size="70"><input type="hidden" id="hidUbigeoAGENTE"></td>';
    elemento += '</tr>';

    //elemento += '<tr>';
    //elemento += '<td></td>';
    //elemento += '<td colspan="6">';
    //elemento += '<div class="checkboxes">';
    //elemento += '<label for="chkUsuarioDerechoAGENTE"><input type="checkbox"  id="chkUsuarioDerechoAGENTE" /><span>Derecho</span></label>&nbsp;';
    //elemento += '<label for="chkRecaudadorAGENTE"><input type="checkbox"   id="chkRecaudadorAGENTE" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    //elemento += '<label for="chkEmpleadoAGENTE"><input type="checkbox"  id="chkEmpleadoAGENTE" checked="checked"/><span>Empleado</span> </label>';
    //elemento += '<label for="chkAsociacionAGENTE"><input type="checkbox"   id="chkAsociacionAGENTE" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    //elemento += '<label for="chkGrupoAGENTE"><input type="checkbox"  id="chkGrupoAGENTE" checked="checked"/><span>Grupo</span></label>&nbsp;';
    //elemento += '<label for="chkProveedorAGENTE"><input type="checkbox"  id="chkProveedorAGENTE" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    //elemento += '<label for="chkFiltroAGENTE" id="lblFiltroAGENTE"><input type="checkbox" id="chkFiltroAGENTE" checked="checked"/><span>Seleccionar todo</span></label>';
    //elemento += '</div>';
    //elemento += '</td>';
    //elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoAGENTE" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioAGENTE"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioAGENTE"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridAGENTE"></div>';
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
        title: "Búsqueda General de Socio."
    });
    $("#btnBuscarSocioAGENTE").on("click", function () {
        $('#gridAGENTE').data('kendoGrid').dataSource.query({
            tipoPersona: $("#ddlTipoPersonaAGENTE").val(),
            tipo: $("#ddlTipoIdAGENTE").val() == "" ? 0 : $("#ddlTipoIdAGENTE").val(),
            nro_tipo: $("#txtNumeroAGENTE").val(),
            nombre: $("#txtRazonSocialAGENTE").val(),

            derecho: false,
            asociacion: false,
            grupo: false,
            recaudador: true,
            proveedor: false,
            empleado: false,

            ubigeo: $("#hidUbigeoAGENTE").val() == "" ? 0 : $("#hidUbigeoAGENTE").val(),
            estado: $("#ddlEstadoAGENTE").val() ,
            page: 1, pageSize: 5
        });
    });
    $("#btnLimpiarSocioAGENTE").on("click", function () {
        limpiarBusquedaAGENTE();
    });
    loadTipoDocumento("ddlTipoIdAGENTE", 0);
    loadTipoPersona("ddlTipoPersonaAGENTE", 0);
    loadEstados("ddlEstadoAGENTE", 0);
    initAutoCompletarUbigeoB("txtUbigeoAGENTE", "hidUbigeoAGENTE", "604");

    loadDataFoundAGENTE();

    //$("#chkFiltroAGENTE").on("click", function () {
    //    var chk = $("#chkFiltroAGENTE").prop("checked");
    //    var x = true;
    //    if (chk == 0) {
    //        x = false;
    //    }
    //    $("#chkUsuarioDerechoAGENTE").prop("checked", x);
    //    $("#chkAsociacionAGENTE").prop("checked", x);
    //    $("#chkGrupoAGENTE").prop("checked", x);
    //    $("#chkRecaudadorAGENTE").prop("checked", x);
    //    $("#chkProveedorAGENTE").prop("checked", x);
    //    $("#chkEmpleadoAGENTE").prop("checked", x);
    //});
};

var getAGENTE = function (id) {
    var hidididmodalview = $("#hidIdidModalViewAGENTE").val();
    var fnc = $("#hidIdEventAGENTE").val();
    $("#" + hidididmodalview).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaAGENTE = function () {
    $("#txtNumeroAGENTE").val("");
    $("#txtRazonSocialAGENTE").val("");
    $("#txtUbigeoAGENTE").val("");
    $("#hidUbigeoAGENTE").val(0);
};

var loadDataFoundAGENTE = function () {
    $("#gridAGENTE").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Socio/BuscarSocioRecaudador",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            tipoPersona: $("#ddlTipoPersonaAGENTE").val(),
                            tipo: $("#ddlTipoIdAGENTE").val() == "" ? 0 : $("#ddlTipoIdAGENTE").val(),
                            nro_tipo: $("#txtNumeroAGENTE").val(),
                            nombre: $("#txtRazonSocialAGENTE").val(),


                            derecho: false,
                            asociacion: false,
                            grupo: false,
                            recaudador: true,
                            proveedor: false,
                            empleado: false,


                            ubigeo: $("#hidUbigeoAGENTE").val() == "" ? 0 : $("#hidUbigeoAGENTE").val(),
                            estado: $("#ddlEstadoAGENTE").val()
                        });
                }
            },
            schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        filterable: false,
        columns:
           [
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 10, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 12, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 20, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },

            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getAGENTE('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getAGENTE('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getAGENTE('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getAGENTE('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getAGENTE('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getAGENTE('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getAGENTE('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getAGENTE('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getAGENTE('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getAGENTE('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getAGENTE('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getAGENTE('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getAGENTE('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getAGENTE('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getAGENTE('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getAGENTE('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });
};