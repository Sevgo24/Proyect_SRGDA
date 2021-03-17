

var mvInitBuscarSocioEntidad = function (parametro) {
    //var idContenedor = parametro.container;
    //var btnEvento = parametro.idButtonToSearch;
    //var idModalView = parametro.idDivMV;


    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewEntidad" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventEntidad" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPSEntidad" /></td>';

    //elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPS" /> <input type="text" id="txtNumeroBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPSEntidad" /> <input type="text" id="txtNumeroBPSEntidad"></td>';
    elemento += '</tr>';


    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPSEntidad" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>';


    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPSEntidad" size="70"><input type="hidden" id="hidUbigeoBPSEntidad"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '<td colspan="6">';
    elemento += '<div class="checkboxes">';
    elemento += '<label for="chkUsuarioDerechoEntidad"><input type="checkbox"  id="chkUsuarioDerechoEntidad" checked="checked"/><span>Derecho</span></label>&nbsp;';
    elemento += '<label for="chkRecaudadorEntidad"><input type="checkbox"   id="chkRecaudadorEntidad" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    elemento += '<label for="chkEmpleadoEntidad"><input type="checkbox"  id="chkEmpleadoEntidad" checked="checked"/><span>Empleado</span> </label>';
    elemento += '<label for="chkAsociacionEntidad"><input type="checkbox"   id="chkAsociacionEntidad" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    elemento += '<label for="chkGrupoEntidad"><input type="checkbox"  id="chkGrupoEntidad" checked="checked"/><span>Grupo</span></label>&nbsp;';
    elemento += '<label for="chkProveedorEntidad"><input type="checkbox"  id="chkProveedorEntidad" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    elemento += '<label for="chkFiltroEntidad" id="lblFiltroEntidad"><input type="checkbox" id="chkFiltroEntidad" checked="checked"/><span>Seleccionar todo</span></label>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSEntidad" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPSEntidad"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPSEntidad"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPSEntidad"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';

    $("#" + parametro.container).append(elemento);
    if (parametro.idButtonToSearch.indexOf(".") != -1) {
        $( parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    } else {
            $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
            if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 810,
        height: 530,
        title: "Búsqueda General de Socio."
    });
    var _cuentaBusSocEnt = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarSocioBPSEntidad").on("click", function () {
         
        if (_cuentaBusSocEnt == 1) {
            loadDataFoundEntidad();
        } else {
            $('#gridBPSEntidad').data('kendoGrid').dataSource.query({
                tipoPersona: $("#ddlTipoPersonaBPSEntidad").val(),
                tipo: $("#ddlTipoIdBPSEntidad").val() == "" ? 0 : $("#ddlTipoIdBPSEntidad").val(),
                nro_tipo: $("#txtNumeroBPSEntidad").val(),
                nombre: $("#txtRazonSocialBPSEntidad").val(),

                derecho: $("#chkUsuarioDerechoEntidad").prop("checked"),
                asociacion: $("#chkAsociacionEntidad").prop("checked"),
                grupo: $("#chkGrupoEntidad").prop("checked"),
                recaudador: $("#chkRecaudadorEntidad").prop("checked"),
                proveedor: $("#chkProveedorEntidad").prop("checked"),
                empleado: $("#chkEmpleadoEntidad").prop("checked"),

                ubigeo: $("#hidUbigeoBPSEntidad").val() == "" ? 0 : $("#hidUbigeoBPSEntidad").val(),
                //estado: $("#ddlEstadoBPSEntidad").val() == "A" ? 1 : 0,
                estado: $("#ddlEstadoBPSEntidad").val() == 2 ? 0 : 1,
                page: 1, pageSize: 5
            });
        }
        _cuentaBusSocEnt++;
    });

    $("#btnLimpiarSocioBPSEntidad").on("click", function () {
        limpiarBusquedaEntidad();
    });

    loadTipoDocumento("ddlTipoIdBPSEntidad", 0);
    loadTipoPersona("ddlTipoPersonaBPSEntidad", 0);
    loadEstados("ddlEstadoBPSEntidad", 0);
    initAutoCompletarUbigeoB("txtUbigeoBPSEntidad", "hidUbigeoBPSEntidad", "604");

    //loadDataFoundEntidad();

    $("#chkFiltroEntidad").on("click", function () {
        var chk = $("#chkFiltroEntidad").prop("checked");
        var x = true;
        if (chk == 0) {
            x = false;
        }
        $("#chkUsuarioDerechoEntidad").prop("checked", x);
        $("#chkAsociacionEntidad").prop("checked", x);
        $("#chkGrupoEntidad").prop("checked", x);
        $("#chkRecaudadorEntidad").prop("checked", x);
        $("#chkProveedorEntidad").prop("checked", x);
        $("#chkEmpleadoEntidad").prop("checked", x);
    });
};

var getBPSEntidad = function (id) {
    var hidIdidModalView = $("#hidIdidModalViewEntidad").val();
    var fnc = $("#hidIdEventEntidad").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaEntidad = function () {

    
    // $("#ddlTipoIdBPS").val();
    $("#txtNumeroBPSEntidad").val("");
    $("#txtRazonSocialBPSEntidad").val("");
    //$("#txtUbigeoBPSEntidad").val("");

    //$("#ddlTipoPersonaBPS").val();
    $("#ddlEstadoBPSEntidad").val(1) 
    $("#hidUbigeoBPSEntidad").val(0);

    $('#gridBPSEntidad').data('kendoGrid').dataSource.query({
        tipoPersona: $("#ddlTipoPersonaBPSEntidad").val(),
        tipo: $("#ddlTipoIdBPSEntidad").val() == "" ? 0 : $("#ddlTipoIdBPSEntidad").val(),
        nro_tipo: $("#txtNumeroBPSEntidad").val(),
        nombre: $("#txtRazonSocialBPSEntidad").val(),
        derecho: $("#chkUsuarioDerechoEntidad").prop("checked"),
        asociacion: $("#chkAsociacionEntidad").prop("checked"),
        grupo: $("#chkGrupoEntidad").prop("checked"),
        recaudador: $("#chkRecaudadorEntidad").prop("checked"),
        proveedor: $("#chkProveedorEntidad").prop("checked"),
        empleado: $("#chkEmpleadoEntidad").prop("checked"),
        ubigeo: $("#hidUbigeoBPSEntidad").val() == "" ? 0 : $("#hidUbigeoBPSEntidad").val(),
        //estado: $("#ddlEstadoBPSEntidad").val() == "A" ? 1 : 0,
        estado: $("#ddlEstadoBPSEntidad").val() == 2 ? 0 : 1,
        page: 1, pageSize: 5
    });
};

var loadDataFoundEntidad = function () {

    //var busq;
    //if ($("#ddlTipoIdBPS").val() == null)
    //    busq = 1
    //else
    //    busq = $("#ddlTipoIdBPS").val();
    //var busq1 = $("#txtNumeroBPS").val();
    //var busq2 = $("#txtRazonSocialBPS").val();
    //var tipoEntidad = $("#ddlTipoPersonaBPS").val();
    //var est = $("#ddlEstadoBPS").val() == "A" ? 1 : 0;
    //var valUbigeo = $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val();
    //var param = {
    //    tipoPersona: tipoEntidad,
    //    tipo: busq,
    //    nro_tipo: busq1,
    //    nombre: busq2,
    //    ubigeo: valUbigeo,
    //    estado: est
    //};
    $("#gridBPSEntidad").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Socio/BuscarSocio",
                    dataType: "json"
                    //data: param
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            tipoPersona: $("#ddlTipoPersonaBPSEntidad").val(),
                            tipo: $("#ddlTipoIdBPSEntidad").val() == "" ? 0 : $("#ddlTipoIdBPSEntidad").val(),
                            nro_tipo: $("#txtNumeroBPSEntidad").val(),
                            nombre: $("#txtRazonSocialBPSEntidad").val(),


                            derecho: $("#chkUsuarioDerechoEntidad").prop("checked"),
                            asociacion: $("#chkAsociacionEntidad").prop("checked"),
                            grupo: $("#chkGrupoEntidad").prop("checked"),
                            recaudador: $("#chkRecaudadorEntidad").prop("checked"),
                            proveedor: $("#chkProveedorEntidad").prop("checked"),
                            empleado: $("#chkEmpleadoEntidad").prop("checked"),


                            ubigeo: $("#hidUbigeoBPSEntidad").val() == "" ? 0 : $("#hidUbigeoBPSEntidad").val(),
                            //estado: $("#ddlEstadoBPSEntidad").val() == "A" ? 1 : 0
                            estado: $("#ddlEstadoBPSEntidad").val() == 2 ? 0 : 1
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
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },

            //{ field: "BPS_USER", width: 12, title: "Usu Derecho", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_USER}</a>" },
            //{ field: "BPS_COLLECTOR", width: 12, title: "Aget Recaudo", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_COLLECTOR}</a>" },
            //{ field: "BPS_ASSOCIATION", width: 12, title: "Asociación", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_ASSOCIATION}</a>" },
            //{ field: "BPS_EMPLOYEE", width: 12, title: "Empleado", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_EMPLOYEE}</a>" },
            //{ field: "BPS_GROUP", width: 12, title: "Gr Económico", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_GROUP}</a>" },
            //{ field: "BPS_SUPPLIER", width: 12, title: "Proveedor", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_SUPPLIER}</a>" },

            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSEntidad('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSEntidad('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSEntidad('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSEntidad('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSEntidad('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSEntidad('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSEntidad('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSEntidad('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEntidad('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });
};