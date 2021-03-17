var mvInitBuscarSocioEmpresarial = function (parametro) {

    //las variables que declare antes
    //var idcontenedor=parametro.container;
    //var btnEvento=parametro.idButtonToSearch;
    //var idModalView=parametro.idDivMV;
    //var valida=parametro.valida;

    var elemento = '<div id="' + parametro.idDivMV + '">';
    elemento += '<input type="hidden"  id="hidIdidModalViewSocEmpr" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventSocEmpr" value="' + parametro.event + '"/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPSSocEMP" /></td>';

    //elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPS" /> <input type="text" id="txtNumeroBPS"></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPSSOCEMP" /> <input type="text" id="txtNumeroBPSSocEMP"></td>';
    elemento += '</tr>';

    elemento += '<tr>'; //BORRAR SI SALE ERROR
    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPSSOCEMP" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>'; //BORRAR SI SALE ERROR
    elemento += '<td>Nombre Comercial</td><td colspan="3"><input type="text" id="txtNombreComercialBPS" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>';


    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPS" size="70"><input type="hidden" id="hidUbigeoBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '<td colspan="6">';
    elemento += '<div class="checkboxes" id="idtdcheckboxes">';
    elemento += '<label for="chkUsuarioDerechoSOCEMP"><input type="checkbox"  id="chkUsuarioDerechoSOCEMP" checked="checked"/><span>Derecho</span></label>&nbsp;';
    elemento += '<label for="chkRecaudadorSOCEMP"><input type="checkbox"   id="chkRecaudadorSOCEMP" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    elemento += '<label for="chkEmpleadoSOCEMP"><input type="checkbox"  id="chkEmpleadoSOCEMP" checked="checked"/><span>Empleado</span> </label>';
    elemento += '<label for="chkAsociacionSOCEMP"><input type="checkbox"   id="chkAsociacionSOCEMP" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    elemento += '<label for="chkGrupoSOCEMP"><input type="checkbox"  id="chkGrupoSOCEMP" checked="checked"/><span>Grupo</span></label>&nbsp;';
    elemento += '<label for="chkProveedorSOCEMP"><input type="checkbox"  id="chkProveedorSOCEMP" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    elemento += '<label for="chkFiltroSOCEMP" id="lblFiltro"><input type="checkbox" id="chkFiltroSOCEMP" checked="checked"/><span>Seleccionar todo</span></label>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSSOCEMP" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPSEmpr"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPSSOCEMP"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPSEmp"></div>';
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
    //if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 810,
        height: 530,
        title: "Búsqueda General de Socio Empresarial."
    });
    var _cuentaBusSoceMP = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarSocioBPSEmpr").on("click", function () {
        if (_cuentaBusSoceMP == 1) {
            loadDataFoundSOCEMP();
        } else {
            $('#gridBPSEmp').data('kendoGrid').dataSource.query({
                tipoPersona: $("#ddlTipoPersonaBPSSocEMP").val(),
                tipo: $("#ddlTipoIdBPSSOCEMP").val() == "" ? 0 : $("#ddlTipoIdBPSSOCEMP").val(),
                nro_tipo: $("#txtNumeroBPSSocEMP").val(),
                nombre: $("#txtRazonSocialBPSSOCEMP").val(),
                derecho: $("#chkUsuarioDerechoSOCEMP").prop("checked"),
                asociacion: $("#chkAsociacionSOCEMP").prop("checked"),
                grupo: $("#chkGrupoSOCEMP").prop("checked"),
                recaudador: $("#chkRecaudadorSOCEMP").prop("checked"),
                proveedor: $("#chkProveedorSOCEMP").prop("checked"),
                empleado: $("#chkEmpleadoSOCEMP").prop("checked"),
                ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                estado: $("#ddlEstadoBPSSOCEMP").val() == 2 ? 0 : 1,
                nombreComercial: $("#txtNombreComercialBPS").val(),
                page: 1, pageSize: 5
            });
        }
        _cuentaBusSoceMP++;
    });
    $("#btnLimpiarSocioBPSSOCEMP").on("click", function () {
        //limpiarBusqueda();

        // $("#ddlTipoIdBPS").val();
        $("#txtNumeroBPSSocEMP").val("");
        $("#txtRazonSocialBPSSOCEMP").val("");
        $("#txtUbigeoBPS").val("");
        //$("#ddlTipoPersonaBPS").val();
        $("#ddlEstadoBPSSOCEMP").val(1)
        $("#hidUbigeoBPS").val(0);

        $('#gridBPSEmp').data('kendoGrid').dataSource.query({
            tipoPersona: $("#ddlTipoPersonaBPSSocEMP").val(),
            tipo: $("#ddlTipoIdBPSSOCEMP").val() == "" ? 0 : $("#ddlTipoIdBPSSOCEMP").val(),
            nro_tipo: $("#txtNumeroBPSSocEMP").val(),
            nombre: $("#txtRazonSocialBPSSOCEMP").val(),
            derecho: $("#chkUsuarioDerechoSOCEMP").prop("checked"),
            asociacion: $("#chkAsociacionSOCEMP").prop("checked"),
            grupo: $("#chkGrupoSOCEMP").prop("checked"),
            recaudador: $("#chkRecaudadorSOCEMP").prop("checked"),
            proveedor: $("#chkProveedorSOCEMP").prop("checked"),
            empleado: $("#chkEmpleadoSOCEMP").prop("checked"),
            ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
            //estado: $("#ddlEstadoBPS").val() == "A" ? 1 : 0,
            estado: $("#ddlEstadoBPSSOCEMP").val() == 2 ? 0 : 1,
            nombreComercial: $("#txtNombreComercialBPS").val(),
            page: 1, pageSize: 5
        });
    });

    loadTipoDocumento("ddlTipoIdBPSSOCEMP", 0);
    loadTipoPersona("ddlTipoPersonaBPSSocEMP", 0);
    loadEstados("ddlEstadoBPSSOCEMP", 0);
    initAutoCompletarUbigeoB("txtUbigeoBPS", "hidUbigeoBPS", "604");

    //loadDataFound();

    $("#chkFiltroSOCEMP").on("click", function () {
        var chk = $("#chkFiltroSOCEMP").prop("checked");
        var x = true;
        if (chk == 0) {
            x = false;
        }
        $("#chkUsuarioDerechoSOCEMP").prop("checked", x);
        $("#chkAsociacionSOCEMP").prop("checked", x);
        $("#chkGrupoSOCEMP").prop("checked", x);
        $("#chkRecaudadorSOCEMP").prop("checked", x);
        $("#chkProveedorSOCEMP").prop("checked", x);
        $("#chkEmpleadoSOCEMP").prop("checked", x);
    });


    $("#txtRazonSocialBPSSOCEMP").keypress(function (e) {
        if (e.which == 13) {
            if (_cuentaBusSoceMP == 1) {
                loadDataFoundSOCEMP();
            } else {
                $('#gridBPSEmp').data('kendoGrid').dataSource.query({
                    tipoPersona: $("#ddlTipoPersonaBPSSocEMP").val(),
                    tipo: $("#ddlTipoIdBPSSOCEMP").val() == "" ? 0 : $("#ddlTipoIdBPSSOCEMP").val(),
                    nro_tipo: $("#txtNumeroBPSSocEMP").val(),
                    nombre: $("#txtRazonSocialBPSSOCEMP").val(),
                    derecho: $("#chkUsuarioDerechoSOCEMP").prop("checked"),
                    asociacion: $("#chkAsociacionSOCEMP").prop("checked"),
                    grupo: $("#chkGrupoSOCEMP").prop("checked"),
                    recaudador: $("#chkRecaudadorSOCEMP").prop("checked"),
                    proveedor: $("#chkProveedorSOCEMP").prop("checked"),
                    empleado: $("#chkEmpleadoSOCEMP").prop("checked"),
                    ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                    estado: $("#ddlEstadoBPSSOCEMP").val() == 2 ? 0 : 1,
                    page: 1, pageSize: 5
                });
            }
            _cuentaBusSoceMP++;
        }
    });
    //Ocultar los Check Box Que no se utilizaran
    $("#chkUsuarioDerechoSOCEMP").hide();
    $("#chkUsuarioDerechoSOCEMP").prop("checked", false);
    $("#chkRecaudadorSOCEMP").prop("checked", false);
    $("#chkRecaudadorSOCEMP").hide();
    $("#chkEmpleadoSOCEMP").prop("checked", false);
    $("#chkEmpleadoSOCEMP").hide();
    $("#chkAsociacionSOCEMP").prop("checked", false);
    $("#chkAsociacionSOCEMP").hide();
    $("#chkProveedorSOCEMP").prop("checked", false);
    $("#chkProveedorSOCEMP").hide();
    $("#chkEmpleadoSOCEMP").hide();
    $("#chkEmpleadoSOCEMP").prop("checked", false);
    $("#chkFiltroSOCEMP").hide();
    $("#chkFiltroSOCEMP").prop("checked", false);
    $("#idtdcheckboxes").hide();
};

var getBPSSOCEMP = function (id) {
    var hidIdidModalViewSocEmpr = $("#hidIdidModalViewSocEmpr").val();//val=mvBuscarSocio
    var fnc = $("#hidIdEventSocEmpr").val();//fnc=reloadEvento;
    $("#" + hidIdidModalViewSocEmpr).dialog("close");
    eval(fnc + " ('" + id + "');");
};

//SOCIO - BUSQ. GENERAL
var reloadEventoSocEmp = function (idSel) {
    $("#lblGrupoEmpresarial").val(idSel);
    obtenerNombreSocioX($("#lblGrupoEmpresarial").val(), 'lblGrupoEmpresarial');

};

function obtenerNombreSocioX(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
                $("#hidCodigoGrupoEmpresarial").val(idSel);
            }
        }
    });
}

var limpiarBusqueda = function () {

};

var loadDataFoundSOCEMP = function () {
    $("#gridBPSEmp").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Socio/BuscarSocioEmpresarial",
                    dataType: "json"
                    //data: param
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            tipoPersona: $("#ddlTipoPersonaBPSSocEMP").val(),
                            tipo: $("#ddlTipoIdBPSSOCEMP").val() == "" ? 0 : $("#ddlTipoIdBPSSOCEMP").val(),
                            nro_tipo: $("#txtNumeroBPSSocEMP").val(),
                            nombre: $("#txtRazonSocialBPSSOCEMP").val(),


                            derecho: $("#chkUsuarioDerechoSOCEMP").prop("checked"),
                            asociacion: $("#chkAsociacionSOCEMP").prop("checked"),
                            grupo: $("#chkGrupoSOCEMP").prop("checked"),
                            recaudador: $("#chkRecaudadorSOCEMP").prop("checked"),
                            proveedor: $("#chkProveedorSOCEMP").prop("checked"),
                            empleado: $("#chkEmpleadoSOCEMP").prop("checked"),


                            ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                            //estado: $("#ddlEstadoBPS").val() == "A" ? 1 : 0
                            estado: $("#ddlEstadoBPSSOCEMP").val() == 2 ? 0 : 1,
                            nombreComercial: $("#txtNombreComercialBPS").val()
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
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },
            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSSOCEMP('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSSOCEMP('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSSOCEMP('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSSOCEMP('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSSOCEMP('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSSOCEMP('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSSOCEMP('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSSOCEMP('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });
};