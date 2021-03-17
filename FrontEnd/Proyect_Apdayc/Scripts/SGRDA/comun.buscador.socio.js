

var mvInitBuscarSocio = function (parametro) {
    //var idContenedor = parametro.container;
    //var btnEvento = parametro.idButtonToSearch;
    //var idModalView = parametro.idDivMV;


    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEvent" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Tipo Persona</td><td><select id="ddlTipoPersonaBPS" /></td>';

    //elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPS" /> <input type="text" id="txtNumeroBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPS" /> <input type="text" id="txtNumeroBPS"></td>';
    elemento += '</tr>';


    elemento += '<td>Nombre / Razon Social</td><td colspan="3"><input type="text" id="txtRazonSocialBPS" size="70" autofocus> <input type="hidden"  id="hidRazonSocialBPS" value=0 ></td>';
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
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPS" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarSocioBPS"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPS"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPS"></div>';
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
    var _cuentaBusSoc = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarSocioBPS").on("click", function () {
        if (_cuentaBusSoc == 1) {
            loadDataFound();
        } else {
            $('#gridBPS').data('kendoGrid').dataSource.query({
                tipoPersona: $("#ddlTipoPersonaBPS").val(),
                tipo: $("#ddlTipoIdBPS").val() == "" ? 0 : $("#ddlTipoIdBPS").val(),
                nro_tipo: $("#txtNumeroBPS").val(),
                nombre: $("#txtRazonSocialBPS").val(),
                derecho: $("#chkUsuarioDerecho").prop("checked"),
                asociacion: $("#chkAsociacion").prop("checked"),
                grupo: $("#chkGrupo").prop("checked"),
                recaudador: $("#chkRecaudador").prop("checked"),
                proveedor: $("#chkProveedor").prop("checked"),
                empleado: $("#chkEmpleado").prop("checked"),
                ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                estado: $("#ddlEstadoBPS").val() == 2 ? 0 : 1,
                page: 1, pageSize: 5
            });
        }
        _cuentaBusSoc++;
    });
    $("#btnLimpiarSocioBPS").on("click", function () {
        $("#txtNumeroBPS").val("");
        $("#txtRazonSocialBPS").val("");
        $("#txtUbigeoBPS").val("");
        $("#ddlEstadoBPS").val(1)
        $("#hidUbigeoBPS").val(0);
        $("#gridBPS").data('kendoGrid').dataSource.data([]);
    });

    loadTipoDocumento("ddlTipoIdBPS", 0);
    loadTipoPersona("ddlTipoPersonaBPS", 0);
    loadEstados("ddlEstadoBPS", 0);
    //initAutoCompletarUbigeoB("txtUbigeoBPS", "hidUbigeoBPS", "604");
    //initAutoCompletarRazonSocial("txtRazonSocialBPS", "hidRazonSocialBPS");   

    //loadDataFound();

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

  
    $("#txtRazonSocialBPS").keypress(function (e) {
        if (e.which == 13) {
            if (_cuentaBusSoc == 1) {
                loadDataFound();
            } else {
                $('#gridBPS').data('kendoGrid').dataSource.query({
                    tipoPersona: $("#ddlTipoPersonaBPS").val(),
                    tipo: $("#ddlTipoIdBPS").val() == "" ? 0 : $("#ddlTipoIdBPS").val(),
                    nro_tipo: $("#txtNumeroBPS").val(),
                    nombre: $("#txtRazonSocialBPS").val(),
                    derecho: $("#chkUsuarioDerecho").prop("checked"),
                    asociacion: $("#chkAsociacion").prop("checked"),
                    grupo: $("#chkGrupo").prop("checked"),
                    recaudador: $("#chkRecaudador").prop("checked"),
                    proveedor: $("#chkProveedor").prop("checked"),
                    empleado: $("#chkEmpleado").prop("checked"),
                    ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                    estado: $("#ddlEstadoBPS").val() == 2 ? 0 : 1,
                    page: 1, pageSize: 5
                });
            }
            _cuentaBusSoc++;
        }
    });
       
    
};

var getBPS = function (id) {
    var hidIdidModalView = $("#hidIdidModalView").val();
    var fnc = $("#hidIdEvent").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
    //generales.js/ObtieneNombreEntidad
    if (typeof K_TITULO.EDITAR == "undefined") {
        ObtieneNombreEntidad(id, 'lbResponsable', true);//obtiene el nombre del socio(entidad)
    } else {
        ValidaUsuarioMoroso(id);
        ObtieneNombreEntidad(id, 'lbResponsable', true);//obtiene el nombre del socio(entidad)
        //alert("Validar Usuario Moroso");
    }
    //SI no funciona el evento get pbs
    // obtenerNombreSocioX($("#lbResponsable").val(), 'lbResponsable');
};

var limpiarBusqueda = function () {
    
};

var loadDataFound = function () {
    $("#gridBPS").kendoGrid({
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
                            tipoPersona: $("#ddlTipoPersonaBPS").val(),
                            tipo: $("#ddlTipoIdBPS").val() == "" ? 0 : $("#ddlTipoIdBPS").val(),
                            nro_tipo: $("#txtNumeroBPS").val(),
                            nombre: $("#txtRazonSocialBPS").val(),


                            derecho: $("#chkUsuarioDerecho").prop("checked"),
                            asociacion: $("#chkAsociacion").prop("checked"),
                            grupo: $("#chkGrupo").prop("checked"),
                            recaudador: $("#chkRecaudador").prop("checked"),
                            proveedor: $("#chkProveedor").prop("checked"),
                            empleado: $("#chkEmpleado").prop("checked"),


                            ubigeo: $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val(),
                            //estado: $("#ddlEstadoBPS").val() == "A" ? 1 : 0
                            estado: $("#ddlEstadoBPS").val() == 2 ? 0 : 1
                            
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
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },
  
            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPS('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPS('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPS('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPS('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPS('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPS('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPS('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPS('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPS('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPS('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPS('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPS('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPS('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPS('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPS('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });


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
};

