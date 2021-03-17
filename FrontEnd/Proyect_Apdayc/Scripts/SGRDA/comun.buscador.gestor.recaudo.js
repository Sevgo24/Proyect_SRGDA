
var mvInitBuscarAgenteRecaudo = function (parametro) {

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden" id="hidIdidModalViewAgenteR" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden" id="hidIdEventAgenteR" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPSAgenteR" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPSAgenteR" /> <input type="text" id="txtNumeroBPSAgenteR"></td>';
    elemento += '</tr>';

    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPSAgenteR" size="70" autofocus> <input type="hidden"  id="hidRazonSocialBPSAgenteR" value=0 ></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPSAgenteR" size="70"><input type="hidden" id="hidUbigeoBPSAgenteR"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSAgenteR" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPSAgenteR"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPSAgenteR"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPSAgenteR"></div>';
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
        width: 810,
        height: 530,
        title: "Búsqueda General de Agentes de Recaudo."
    });
    var _cuentaBusSoc = 1; /*addon dbs  20150831- Primera carga los datos */

    $("#btnBuscarSocioBPSAgenteR").on("click", function () {
        if (_cuentaBusSoc == 1) {
            loadDataFoundAgenteR();
        } else {
            $('#gridBPSAgenteR').data('kendoGrid').dataSource.query({
                tipoPersona: $("#ddlTipoPersonaBPSAgenteR").val(),
                tipo: $("#ddlTipoIdBPSAgenteR").val() == "" ? 0 : $("#ddlTipoIdBPSAgenteR").val(),
                nro_tipo: $("#txtNumeroBPSAgenteR").val(),
                nombre: $("#txtRazonSocialBPSAgenteR").val(),
                derecho: false,
                asociacion: false,
                grupo: false,
                recaudador: true,
                proveedor: false,
                empleado: false,
                ubigeo: $("#hidUbigeoBPSAgenteR").val() == "" ? 0 : $("#hidUbigeoBPSAgenteR").val(),
                estado: $("#ddlEstadoBPSAgenteR").val() == 2 ? 0 : 1,
                page: 1, pageSize: 5
            });
        }
        _cuentaBusSoc++;
    });
    $("#btnLimpiarSocioBPSAgenteR").on("click", function () {
        $("#txtNumeroBPSAgenteR").val("");
        $("#txtRazonSocialBPSAgenteR").val("");
        $("#txtUbigeoBPSAgenteR").val("");
        $("#ddlEstadoBPSAgenteR").val(1)
        $("#hidUbigeoBPSAgenteR").val(0);
        $("#gridBPSAgenteR").data('kendoGrid').dataSource.data([]);
    });

    loadTipoPersona("ddlTipoPersonaBPSAgenteR", parametro.tipoPersona);
    if (parametro.tipoPersona == 'J')
        loadTipoDocumento("ddlTipoIdBPSAgenteR", 1);
    else if (parametro.tipoPersona == 'N')
        loadTipoDocumento("ddlTipoIdBPSAgenteR", 2);
    else
        loadTipoDocumento("ddlTipoIdBPSAgenteR", 0);

    $("#ddlTipoPersonaBPSAgenteR").change(function () {
        var tipoPersona = $("#ddlTipoPersonaBPSAgenteR").val();
        if (tipoPersona == 'J')
            loadTipoDocumento("ddlTipoIdBPSAgenteR", 1);
        else if (tipoPersona == 'N')
            loadTipoDocumento("ddlTipoIdBPSAgenteR", 2);
        else
            loadTipoDocumento("ddlTipoIdBPSAgenteR", 0);
    });

    loadEstados("ddlEstadoBPSAgenteR", 0);


    $("#txtRazonSocialBPSAgenteR").keypress(function (e) {
        if (e.which == 13) {
            if (_cuentaBusSoc == 1) {
                loadDataFoundAgenteR();
            } else {
                $('#gridBPSAgenteR').data('kendoGrid').dataSource.query({
                    tipoPersona: $("#ddlTipoPersonaBPSAgenteR").val(),
                    tipo: $("#ddlTipoIdBPSAgenteR").val() == "" ? 0 : $("#ddlTipoIdBPSAgenteR").val(),
                    nro_tipo: $("#txtNumeroBPSAgenteR").val(),
                    nombre: $("#txtRazonSocialBPSAgenteR").val(),
                    derecho: false,
                    asociacion: false,
                    grupo: false,
                    recaudador: true,
                    proveedor: false,
                    empleado: false,
                    ubigeo: $("#hidUbigeoBPSAgenteR").val() == "" ? 0 : $("#hidUbigeoBPSAgenteR").val(),
                    estado: $("#ddlEstadoBPSAgenteR").val() == 2 ? 0 : 1,
                    page: 1, pageSize: 5
                });
            }
            _cuentaBusSoc++;
        }
    });

};

var getBPSAgenteR = function (id) {
    var hidIdidModalView = $("#hidIdidModalViewAgenteR").val();
    var fnc = $("#hidIdEventAgenteR").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};


var loadDataFoundAgenteR = function () {
    $("#gridBPSAgenteR").kendoGrid({
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
                            tipoPersona: $("#ddlTipoPersonaBPSAgenteR").val(),
                            tipo: $("#ddlTipoIdBPSAgenteR").val() == "" ? 0 : $("#ddlTipoIdBPSAgenteR").val(),
                            nro_tipo: $("#txtNumeroBPSAgenteR").val(),
                            nombre: $("#txtRazonSocialBPSAgenteR").val(),

                            derecho: false,
                            asociacion: false,
                            grupo: false,
                            recaudador: true,
                            proveedor: false,
                            empleado: false,

                            ubigeo: $("#hidUbigeoBPSAgenteR").val() == "" ? 0 : $("#hidUbigeoBPSAgenteR").val(),
                            //estado: $("#ddlEstadoBPS").val() == "A" ? 1 : 0
                            estado: $("#ddlEstadoBPSAgenteR").val() == 2 ? 0 : 1

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
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            //{ title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSAgenteR('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            //{ title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            //{ title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            //{ title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSAgenteR('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSAgenteR('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSAgenteR('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            //{ title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSAgenteR('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSAgenteR('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            //{ title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSAgenteR('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSAgenteR('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            //{ field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSAgenteR('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },

           ]
    });


};


