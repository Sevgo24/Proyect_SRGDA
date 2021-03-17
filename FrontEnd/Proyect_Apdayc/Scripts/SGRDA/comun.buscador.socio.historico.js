

var mvInitBuscarSocioHistorico = function (parametro) {
    //var idContenedor = parametro.container;
    //var btnEvento = parametro.idButtonToSearch;
    //var idModalView = parametro.idDivMV;


    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalView" value="' + parametro.idDivMV + '"/>';
    //elemento += '<input type="hidden"  id="hidIdEvent" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPSHistorico" /></td>';

    //elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPS" /> <input type="text" id="txtNumeroBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPSHistorico" /> <input type="text" id="txtNumeroBPSHistorico"></td>';
    elemento += '</tr>';


    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPSHistorico" size="70" autofocus> <input type="hidden"  id="hidRazonSocialBPSHistorico" value=0 ></td>';
    elemento += '</tr>';

    elemento += '<tr>';


    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPS" size="70"><input type="hidden" id="hidUbigeoBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '<td colspan="6">';
    elemento += '<div class="checkboxes">';
    elemento += '<label for="chkUsuarioDerecho"><input type="checkbox"  id="chkUsuarioDerecho" checked="checked"/><span>Derecho</span></label>&nbsp;';
    elemento += '<label for="chkRecaudador"><input type="checkbox"   id="chkRecaudador" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    elemento += '<label for="chkEmpleado"><input type="checkbox"  id="chkEmpleado" checked="checked"/><span>Empleado</span> </label>';
    elemento += '<label for="chkAsociacion"><input type="checkbox"   id="chkAsociacion" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    elemento += '<label for="chkGrupo"><input type="checkbox"  id="chkGrupo" checked="checked"/><span>Grupo</span></label>&nbsp;';
    elemento += '<label for="chkProveedor"><input type="checkbox"  id="chkProveedor" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    elemento += '<label for="chkFiltro" id="lblFiltro"><input type="checkbox" id="chkFiltro" checked="checked"/><span>Seleccionar todo</span></label>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSHistorico" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPSHistorico"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPS"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPSHistorico"></div>';
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
        title: "Búsqueda General de Socio.1"
    });
    var _cuentaBusSocHistorico = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarSocioBPSHistorico").on("click", function () {
        if (_cuentaBusSocHistorico == 1) {
            loadDataFoundHistorico();
        } else {
            $('#gridBPSHistorico').data('kendoGrid').dataSource.query({
                tipoPersona: $("#ddlTipoPersonaBPSHistorico").val(),
                tipo: $("#ddlTipoIdBPSHistorico").val() == "" ? 0 : $("#ddlTipoIdBPSHistorico").val(),
                nro_tipo: $("#txtNumeroBPSHistorico").val(),
                nombre: $("#txtRazonSocialBPSHistorico").val(),
                derecho: $("#chkUsuarioDerecho").prop("checked"),
                asociacion: $("#chkAsociacion").prop("checked"),
                grupo: $("#chkGrupo").prop("checked"),
                recaudador: $("#chkRecaudador").prop("checked"),
                proveedor: $("#chkProveedor").prop("checked"),
                empleado: $("#chkEmpleado").prop("checked"),
                ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                estado: $("#ddlEstadoBPSHistorico").val() == 2 ? 0 : 1,
                page: 1, pageSize: 5
            });
        }
        _cuentaBusSocHistorico++;
    });
    $("#btnLimpiarSocioBPS").on("click", function () {
        $("#txtNumeroBPSHistorico").val("");
        $("#txtRazonSocialBPSHistorico").val("");
        $("#txtUbigeoBPS").val("");
        $("#ddlEstadoBPSHistorico").val(1)
        $("#hidUbigeoBPS").val(0);
        $("#gridBPSHistorico").data('kendoGrid').dataSource.data([]);
    });

    loadTipoDocumento("ddlTipoIdBPSHistorico", 0);
    loadTipoPersona("ddlTipoPersonaBPSHistorico", 0);
    loadEstados("ddlEstadoBPSHistorico", 0);
    //initAutoCompletarUbigeoB("txtUbigeoBPS", "hidUbigeoBPS", "604");
    //initAutoCompletarRazonSocial("txtRazonSocialBPSHistorico", "hidRazonSocialBPSHistorico");   

    //loadDataFoundHistorico();

    $("#chkFiltro").on("click", function () {
        var chk = $("#chkFiltro").prop("checked");
        var x = true;
        if (chk == 0) {
            x = false;
        }
        $("#chkUsuarioDerecho").prop("checked", x);
        $("#chkAsociacion").prop("checked", x);
        $("#chkGrupo").prop("checked", x);
        $("#chkRecaudador").prop("checked", x);
        $("#chkProveedor").prop("checked", x);
        $("#chkEmpleado").prop("checked", x);
    });


    $("#txtRazonSocialBPSHistorico").keypress(function (e) {
        if (e.which == 13) {
            if (_cuentaBusSocHistorico == 1) {
                loadDataFoundHistorico();
            } else {
                $('#gridBPSHistorico').data('kendoGrid').dataSource.query({
                    tipoPersona: $("#ddlTipoPersonaBPSHistorico").val(),
                    tipo: $("#ddlTipoIdBPSHistorico").val() == "" ? 0 : $("#ddlTipoIdBPSHistorico").val(),
                    nro_tipo: $("#txtNumeroBPSHistorico").val(),
                    nombre: $("#txtRazonSocialBPSHistorico").val(),
                    derecho: $("#chkUsuarioDerecho").prop("checked"),
                    asociacion: $("#chkAsociacion").prop("checked"),
                    grupo: $("#chkGrupo").prop("checked"),
                    recaudador: $("#chkRecaudador").prop("checked"),
                    proveedor: $("#chkProveedor").prop("checked"),
                    empleado: $("#chkEmpleado").prop("checked"),
                    ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                    estado: $("#ddlEstadoBPSHistorico").val() == 2 ? 0 : 1,
                    page: 1, pageSize: 5
                });
            }
            _cuentaBusSocHistorico++;
        }
    });


};

var getBPSHistorico = function (id) {
    var hidIdidModalView = $("#hidIdidModalView").val();
    //var fnc = $("#hidIdEvent").val();
    $("#" + hidIdidModalView).dialog("close");

    // eval(fnc + " ('" + id + "');");
    //generales.js/ObtieneNombreEntidad
    //ObtieneNombreEntidad(id, 'lbResponsable', true);//obtiene el nombre del socio(entidad)



    $("#hidResponsableHistorico").val(id);

    var lic_id = $("#hidLicId").val();

    //var socio_nuevo = $("#lbResponsable").text();

    //alert(socio_nuevo);

    Confirmar('Desea enviar al Historico la licencia ' + lic_id + ' y asignar al socio Seleccionado', //+ socio_nuevo,
       function () {
           MandarHistorico()
       },
           function () { }, ' ENVIAR AL HISTORICO ?');

};

var limpiarBusqueda = function () {

};

var loadDataFoundHistorico = function () {
    $("#gridBPSHistorico").kendoGrid({
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
                            tipoPersona: $("#ddlTipoPersonaBPSHistorico").val(),
                            tipo: $("#ddlTipoIdBPSHistorico").val() == "" ? 0 : $("#ddlTipoIdBPSHistorico").val(),
                            nro_tipo: $("#txtNumeroBPSHistorico").val(),
                            nombre: $("#txtRazonSocialBPSHistorico").val(),


                            derecho: $("#chkUsuarioDerecho").prop("checked"),
                            asociacion: $("#chkAsociacion").prop("checked"),
                            grupo: $("#chkGrupo").prop("checked"),
                            recaudador: $("#chkRecaudador").prop("checked"),
                            proveedor: $("#chkProveedor").prop("checked"),
                            empleado: $("#chkEmpleado").prop("checked"),


                            ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                            //estado: $("#ddlEstadoBPS").val() == "A" ? 1 : 0
                            estado: $("#ddlEstadoBPSHistorico").val() == 2 ? 0 : 1

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
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },

            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSHistorico('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSHistoricoHistorico('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSHistorico('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSHistorico('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSHistorico('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSHistorico('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSHistoricoHistorico('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSHistorico('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSHistoricoHistorico('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSHistorico('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });


    //var busq;
    //if ($("#ddlTipoIdBPSHistorico").val() == null)
    //    busq = 1
    //else
    //    busq = $("#ddlTipoIdBPS").val();
    //var busq1 = $("#txtNumeroBPSHistorico").val();
    //var busq2 = $("#txtRazonSocialBPSHistorico").val();
    //var tipoEntidad = $("#ddlTipoPersonaBPSHistorico").val();
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
};

