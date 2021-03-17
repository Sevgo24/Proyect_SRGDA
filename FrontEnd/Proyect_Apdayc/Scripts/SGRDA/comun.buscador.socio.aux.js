var mvInitBuscarSocioAux = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewaux" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventaux" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:20%">Tipo Persona</td><td><select id="ddlTipoPersonaBPSaux" /></td>';

    //elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPSaux" /> <input type="text" id="txtNumeroBPSaux"></td>';
    elemento += '</tr>';

    //  elemento += '<td>Tipo de Doc.</td><td><select id="ddlTipoIdBPSaux" /> <input type="text" id="txtNumeroBPSaux"></td>';
    elemento += '<tr>';
    elemento += '<td>Tipo de Documento</td>';
    elemento += '<td><select id="ddlTipoIdBPSaux" /> <input type="text" id="txtNumeroBPSaux"></td>';
    elemento += '</tr>';

    elemento += '<td>Nombre / Razón Social</td><td style="width:50%" colspan="3"><input type="text" id="txtRazonSocialBPSaux" size="64" ></td>';
    elemento += '<td><img src="../Images/botones/add.png" id="btnAgregarResponsable" height="30" style="cursor: pointer;" alt="Registrar Socio" title="Registrar Socio"/></td>';


    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Ubigeo</td><td colspan="3"><input type="text" id="txtUbigeoBPSaux" size="64"><input type="hidden" id="hidUbigeoBPSaux"></td>';
    elemento += '</tr>';



    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '<td colspan="6">';
    elemento += '<div class="checkboxes">';
    elemento += '<label for="chkUsuarioDerecho2"><input type="checkbox"  id="chkUsuarioDerecho2" checked="checked"/><span>Derecho</span></label>&nbsp;';
    elemento += '<label for="chkRecaudador2"><input type="checkbox"   id="chkRecaudador2" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    elemento += '<label for="chkEmpleado2"><input type="checkbox"  id="chkEmpleado2" checked="checked"/><span>Empleado</span> </label>';
    elemento += '<label for="chkAsociacion2"><input type="checkbox"   id="chkAsociacion2" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    elemento += '<label for="chkGrupo2"><input type="checkbox"  id="chkGrupo2" checked="checked"/><span>Grupo</span></label>&nbsp;';
    elemento += '<label for="chkProveedor2"><input type="checkbox"  id="chkProveedor2" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    elemento += '<label for="chkFiltro2" id="lblFiltro"><input type="checkbox" id="chkFiltro2" checked="checked"/><span>Seleccionar todo</span></label>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';



    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoBPSaux" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="7"><center><button id="btnBuscarSocioBPSaux"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPSaux"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridBPSaux"></div>';
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
        height: 540,
        title: "Búsqueda General de Socio."
    });

    $("#btnBuscarSocioBPSaux").on("click", function () {
        //$('#gridBPSaux').data('kendoGrid').dataSource.query({
        //    tipoPersona: $("#ddlTipoPersonaBPSaux").val(),
        //    tipo: $("#ddlTipoIdBPSaux").val() == "" ? 0 : $("#ddlTipoIdBPSaux").val(),
        //    nro_tipo: $("#txtNumeroBPSaux").val(),
        //    nombre: $("#txtRazonSocialBPSaux").val(),

        //    derecho: $("#chkUsuarioDerecho2").prop("checked"),
        //    asociacion: $("#chkAsociacion2").prop("checked"),
        //    grupo: $("#chkGrupo2").prop("checked"),
        //    recaudador: $("#chkRecaudador2").prop("checked"),
        //    proveedor: $("#chkProveedor2").prop("checked"),
        //    empleado: $("#chkEmpleado2").prop("checked"),

        //    ubigeo: $("#hidUbigeoBPSaux").val() == "" ? 0 : $("#hidUbigeoBPSaux").val(),
        //    //estado: $("#ddlEstadoBPSaux").val() == "A" ? 1 : 0,
        //    estado: $("#ddlEstadoBPSaux").val() == 2 ? 0 : 1,
        //    page: 1, pageSize: K_PAGINACION.LISTAR_5
        //});
        loadDataFoundSocio();
    });


    $("#txtRazonSocialBPSaux").keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundSocio(e);
        }
    });

    $("#txtUbigeoBPSaux").keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundSocio(e);
        }
    });

    $("#btnLimpiarSocioBPSaux").on("click", function () {
        limpiarBusqueda();
    });

    loadTipoDocumento("ddlTipoIdBPSaux", 0);
    loadTipoPersona("ddlTipoPersonaBPSaux", 0);
    loadEstados("ddlEstadoBPSaux", 0);
    initAutoCompletarUbigeoB("txtUbigeoBPSaux", "hidUbigeoBPSaux", "604");
    //loadDataFoundSocio();
    
    $("#chkFiltro2").on("click", function () {
        var chk = $("#chkFiltro2").prop("checked");
        var x = true;
        if (chk == 0) {
            x = false;
        }
        $("#chkUsuarioDerecho2").prop("checked", x);
        $("#chkAsociacion2").prop("checked", x);
        $("#chkGrupo2").prop("checked", x);
        $("#chkRecaudador2").prop("checked", x);
        $("#chkProveedor2").prop("checked", x);
        $("#chkEmpleado2").prop("checked", x);
    });
};

var getBPSaux = function (id) {
    var hidIdidModalViewaux = $("#hidIdidModalViewaux").val();
    var fnc = $("#hidIdEventaux").val();
    $("#" + hidIdidModalViewaux).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusqueda = function () {
    $("#txtNumeroBPSaux").val("");
    $("#txtRazonSocialBPSaux").val("");
    $("#txtUbigeoBPSaux").val("");
    $("#hidUbigeoBPSaux").val(0);
    $("#ddlEstadoBPSaux").val(1);

    //$('#gridBPSaux').data('kendoGrid').dataSource.query({
    //    tipoPersona: $("#ddlTipoPersonaBPSaux").val(),
    //    tipo: $("#ddlTipoIdBPSaux").val() == "" ? 0 : $("#ddlTipoIdBPSaux").val(),
    //    nro_tipo: $("#txtNumeroBPSaux").val(),
    //    nombre: $("#txtRazonSocialBPSaux").val(),
    //    derecho: $("#chkUsuarioDerecho2").prop("checked"),
    //    asociacion: $("#chkAsociacion2").prop("checked"),
    //    grupo: $("#chkGrupo2").prop("checked"),
    //    recaudador: $("#chkRecaudador2").prop("checked"),
    //    proveedor: $("#chkProveedor2").prop("checked"),
    //    empleado: $("#chkEmpleado2").prop("checked"),
    //    ubigeo: $("#hidUbigeoBPSaux").val() == "" ? 0 : $("#hidUbigeoBPSaux").val(),
    //    //estado: $("#ddlEstadoBPSaux").val() == "A" ? 1 : 0,
    //    estado: $("#ddlEstadoBPSaux").val() == 2 ? 0 : 1,
    //    page: 1, pageSize: K_PAGINACION.LISTAR_5
    //});
};

function loadDataFoundSocio (e) {
    if ($("#gridBPSaux").data("kendoGrid") != undefined) {
        $("#gridBPSaux").empty();
    }

    
    var data_sourceBPSaux = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_5,
        transport: {
            read: {
                url: "../Socio/BuscarSocio",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        tipoPersona: $("#ddlTipoPersonaBPSaux").val(),
                        tipo: $("#ddlTipoIdBPSaux").val() == "" ? 0 : $("#ddlTipoIdBPSaux").val(),
                        nro_tipo: $("#txtNumeroBPSaux").val(),
                        nombre: $("#txtRazonSocialBPSaux").val(),

                        derecho: $("#chkUsuarioDerecho2").prop("checked"),
                        asociacion: $("#chkAsociacion2").prop("checked"),
                        grupo: $("#chkGrupo2").prop("checked"),
                        recaudador: $("#chkRecaudador2").prop("checked"),
                        proveedor: $("#chkProveedor2").prop("checked"),
                        empleado: $("#chkEmpleado2").prop("checked"),


                        ubigeo: $("#hidUbigeoBPSaux").val() == "" ? 0 : $("#hidUbigeoBPSaux").val(),
                        //estado: $("#ddlEstadoBPSaux").val() == "A" ? 1 : 0,
                        estado: $("#ddlEstadoBPSaux").val() == 2 ? 0 : 1
                    });
            }
        },
        schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
    })

    
    var gridBPSaux = $("#gridlicencias").kendoGrid({
    });


    $("#gridBPSaux").kendoGrid({
        dataSource: data_sourceBPSaux,
        groupable: false,
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },


            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSaux('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSaux('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },

            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]       
    });
};




//var loadDataFoundSocio = function () {
//    $("#gridBPSaux").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: K_PAGINACION.LISTAR_5,
//            transport: {
//                read: {
//                    url: "../Socio/BuscarSocio",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {
//                            tipoPersona: $("#ddlTipoPersonaBPSaux").val(),
//                            tipo: $("#ddlTipoIdBPSaux").val() == "" ? 0 : $("#ddlTipoIdBPSaux").val(),
//                            nro_tipo: $("#txtNumeroBPSaux").val(),
//                            nombre: $("#txtRazonSocialBPSaux").val(),

//                            derecho: $("#chkUsuarioDerecho2").prop("checked"),
//                            asociacion: $("#chkAsociacion2").prop("checked"),
//                            grupo: $("#chkGrupo2").prop("checked"),
//                            recaudador: $("#chkRecaudador2").prop("checked"),
//                            proveedor: $("#chkProveedor2").prop("checked"),
//                            empleado: $("#chkEmpleado2").prop("checked"),


//                            ubigeo: $("#hidUbigeoBPSaux").val() == "" ? 0 : $("#hidUbigeoBPSaux").val(),
//                            //estado: $("#ddlEstadoBPSaux").val() == "A" ? 1 : 0,
//                            estado: $("#ddlEstadoBPSaux").val() == 2 ? 0 : 1
//                        });
//                }
//            },
//            schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: true,
//        selectable: true,
//        columns:
//           [
//            { field: "BPS_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
//            { field: "ENT_TYPE_NOMBRE", width: 10, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
//            { field: "TAXN_NAME", width: 8, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
//            { field: "TAX_ID", width: 10, title: "Número", template: "<font color='green'><a id='single_2' href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
//            { field: "BPS_NAME", width: 15, title: "Razón Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },

  
//            { title: 'U', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#  <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a> #} else { if(BPS_USER == '2'){#   <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/yellow.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}')  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
//            { title: 'R', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_COLLECTOR == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> </a> #}else{#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
//            { title: 'E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>  #}else {  if(BPS_EMPLOYEE == '2') {#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/yellow.png' width='16' href=javascript:getBPSaux('${BPS_ID}') title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{ # <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}') style=' cursor: pointer; cursor: hand;'>  </a>#}}#" },
//            { title: 'A', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#<a  href=javascript:getBPSaux('${BPS_ID}') >  <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' style=' cursor: pointer; cursor: hand;'>  </a>  #} else { if(BPS_ASSOCIATION == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/yellow.png' border='0' href=javascript:getBPSaux('${BPS_ID}') title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'></a> #}else{# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a>  #}}#" },
           
//            { title: 'P', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/green.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else { if(BPS_SUPPLIER == '2'){# <a  href=javascript:getBPSaux('${BPS_ID}') > <img src='../Images/botones/yellow.png'  width='16' href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'></a> #}else{#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16' href=javascript:getBPSaux('${BPS_ID}')  style=' cursor: pointer; cursor: hand;'> </a> #}}#" },
//            { title: 'G.E', width: 4, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/green.png'  width='16'  href=javascript:getBPSaux('${BPS_ID}') border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'> </a>   #} else {#  <a  href=javascript:getBPSaux('${BPS_ID}') ><img src='../Images/botones/red.png' width='16'> </a> #}#" },
//            { field: "ACTIVO", width: 7, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSaux('${BPS_ID}') style='color:gray !important;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
//           ]
//    });
//};


