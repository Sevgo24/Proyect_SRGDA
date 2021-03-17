
var mvInitLicencia = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidId" value="' + parametro.event + ' "/>';

    elemento += '<div id="ContenedormvSocio"></div>';
    elemento += '<div id="ContenedormvMod"></div>';
    elemento += '<div id="ContenedormvEstaux"></div>';

    elemento += '<table border=0  style=" width:100%; border:1px;">';
    elemento += '<tr>';
    elemento += '<td>'; 
    elemento += '<div class="contenedor">';
    elemento += '<table border=0 style=" width:100%; ">';
    elemento += '<tr>';
    elemento += '<td style="width:120px;"> Codigo de Licencia: </td> <td><input type="text" id="txtCodLicencia" size="10"/></td>';
    elemento += '<td> Nombre de la Licencia: </td> <td><input type="text" id="txtNomLicencia" size="15" /></td>';
    elemento += '<td> Moneda: </td> <td><select id="ddMoneda"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Establecimiento: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarEstablecimientoAux" style="cursor:pointer;" alt="Búsqueda de Establecimiento" title="Búsqueda de Establecimiento"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdEstablecimiento"/> <lable id="lblEstAux" style="cursor:pointer;" alt="Búsqueda de Establecimiento" title="Búsqueda de Establecimiento">Todos</lable> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';

    elemento += '<td> Responsable: </td>';
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarBSP" style="cursor:pointer;" alt="Búsqueda de Responsable" title="Búsqueda de Responsable"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdResponsable"/> <lable id="lbRes" style="cursor:pointer;" alt="Búsqueda de Responsable" title="Búsqueda de Responsable">Todos</lable> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';

    elemento += '<td> Modalidad: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarModalidaduso" style="cursor:pointer;" alt="Búsqueda de Modalidad" title="Búsqueda de Modalidad"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdModalidad"/> <lable id="lbMod" style="cursor:pointer;" alt="Búsqueda de Modalidad" title="Búsqueda de Modalidad">Todos</lable> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Tipo de Licencia: </td> <td><select id="ddTipoLicencia"/></td>';
    elemento += '<td > Tarifa Asociada: </td> <td><select id="ddTarifaAsociada" style="width: 120px"/></td>';
    elemento += '<td > Temporalidad: </td> <td><select id="ddTemporalidad"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td > Tipo División: </td> <td><select id="ddTipDiv"/></td>';
    elemento += '<td > División: </td> <td><select id="ddDiv"/></td>';
    elemento += '<td > Estado de Licencia: </td> <td><select id="ddEstadoLicencia"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="6"><center><button id="btnBuscarLicencia"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarLic"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table>';
    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td align="center"><div id="gridlicencias"></div></td>';
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
        height: 560,
        title: "Búsqueda General de Licencia."
    });

    loadDivisionTipo('ddTipDiv', '0');
    loadTipoLicencia('ddTipoLicencia', '0');
    loadEstadoIniPorTipoLic('ddEstadoLicencia', 0, '0', 'TODOS');
    loadTemporalidad('ddTemporalidad', '0', 'TODOS');
    loadTarifaAsociada('ddTarifaAsociada', '0', 'TODOS', 0);
    loadMonedaRecaudacion('ddMoneda', '0', 'TODOS');
    //$('#txtNomLicencia').focus();
    

    $("#ddTipDiv").on("change", function () {
        var tipo = $("#ddTipDiv").val();
        loadDivisionXTipo('ddDiv', tipo, 'TODOS');
    });
    $("#ddTipoLicencia").on("change", function () {
        var idTipoLic = $(this).val();
        loadEstadoIniPorTipoLic("ddEstadoLicencia", '0', idTipoLic, 'TODOS');
    });
    
    $('#txtCodLicencia').keypress(function (e) {
        if (e.which == 13) {
            loadGridLicencias();
        }
    });

    $('#txtNomLicencia').keypress(function (e) {
        if (e.which == 13) {
            loadGridLicencias();
        }
    });
    
    $("#btnBuscarLicencia").on("click", function (e) {
        //loadGridLicencias();
        loadGridLicencias();
    });

    $("#btnLimpiarLic").on("click", function () {
        limpiarLicencias();
    });

    //loadDataLicencia();
    //$("#txtNomLicencia").focus();
    mvInitEstablecimientoAux({ container: "ContenedormvEstaux", idButtonToSearch: "btnBuscarEstablecimientoAux", idDivMV: "mvEstablecimientoAux", event: "reloadEventoEstablecimientoAux", idLabelToSearch: "lblEstAux" });
    mvInitBuscarSocioAux({ container: "ContenedormvSocio", idButtonToSearch: "btnBuscarBSP", idDivMV: "mvBuscarSoci", event: "reloadEventoSocio", idLabelToSearch: "lbRes" });
    mvInitModalidadUsoaux({ container: "ContenedormvMod", idButtonToSearch: "btnBuscarModalidaduso", idDivMV: "mvMod", event: "reloadEventoModalidad", idLabelToSearch: "lbMod" });
};

var getidLic = function (id) {
    var hidIdidModalViewLic = $("#hidIdidView").val();
    var fnc = $("#hidId").val();
    $("#" + hidIdidModalViewLic).dialog("close");

    if ($("#btnAgregarCadena").length) { // si existe el boton agregar la licencia a la cadena
        AgregarLicenciaCadenaSeleccionada(id);
    } else {
        eval(fnc + " ('" + id + "');"); // si no obtener el nombre de la licencia
    }
};


var reloadEventoSocio = function (idSel) {
    $("#hidIdResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbRes").html(dato.valor);
            }
        }
    });
};

var reloadEventoModalidad = function (idSel) {
    $("#hidIdModalidad").val(idSel);
    obtenerNombreModalidad(idSel, "lbMod");
};

var reloadEventoEstablecimientoAux = function (idSel) {
    $("#hidIdEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstAux");
};

function limpiarLicencias() {
    $("#txtCodLicencia").val("");
    $("#hidIdModalidad").val("");
    $("#hidIdEstablecimiento").val("");
    $("#hidIdResponsable").val("");
    $("#txtNomLicencia").val("");
    $("#lbMod").html("Todos");
    $("#lblEstAux").html("Todos");
    $("#lbRes").html("Todos");
    loadDivisionTipo('ddTipoDivision', '0');
    loadTipoLicencia('ddTipoLicencia', '0');
    loadEstadoIniPorTipoLic('ddEstadoLicencia', 0, '0', 'TODOS');
    loadTemporalidad('ddTemporalidad', '0', 'TODOS');
    loadMonedaRecaudacion('ddMoneda', '0', 'TODOS');
    //loadGridLicencias();
};


function loadGridLicencias(e) {
    var d = new Date();
    var vffni = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();
    var vffin = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();

    // Si existe vacío que la cuadrícula
    if ($("#gridlicencias").data("kendoGrid") != undefined) {
        $("#gridlicencias").empty();
    }

    var data_source = new kendo.data.DataSource({
        //batch: true,
        //pageSize: 50,
        //serverFiltering: true,
        //serverPaging: true,
        //serverSorting: true,  
        
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_5,
        transport: {
            read: {
                type: "POST",
                url: "../Licencia/USP_LICENCIAMIENTO_LISTARPAGEJSON",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
                        LIC_TYPE: $("#ddTipoLicencia").val() == "" ? 0 : $("#ddTipoLicencia").val(),
                        LICS_ID: $("#ddEstadoLicencia").val(),
                        CUR_ALPHA: $("#ddMoneda").val(),
                        MOD_ID: $("#hidIdModalidad").val() == "" ? 0 : $("#hidIdModalidad").val(),
                        EST_ID: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
                        BPS_ID: $("#hidIdResponsable").val() == "" ? 0 : $("#hidIdResponsable").val(),
                        LIC_NAME: $("#txtNomLicencia").val(),
                        LIC_TEMP: $("#ddTemporalidad").val(),
                        RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
                        LICMAS: 0,
                        BPS_GROUP: $("#hidCodigoGrupoEmpresarial").val() == "" || $("#hidCodigoGrupoEmpresarial").val() ==undefined ? 0 : $("#hidCodigoGrupoEmpresarial").val(),
                        BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
                        conFecha: 0,
                        finiauto: vffni,
                        ffinauto: vffin,
                        desc_artista: "",
                        cod_artista_sgs: "-1",
                        estadoLic:0
                    })
            }
        },
        schema: { data: "ListaLicencias", total: 'TotalVirtual' }
       
    })


    var grid = $("#gridlicencias").kendoGrid({
        dataSource: data_source,
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
        {
            hidden: true,
            title: "Eliminar", width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
        },
        { field: "LIC_NAME", width: 13, title: "Nombre", template: "<a id='single_2'  href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LIC_NAME}</a>" },
        { field: "LIC_DESC", width: 15, title: "Descripción", hidden:true, template: "<a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LIC_DESC}</a>" },        
        { field: "LIC_TYPE", width: 12, title: "Tipo ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${LIC_TDESC} </a>" },
        { field: "LICS_ID", width: 10, title: "Estado ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${WRKF_SLABEL} </a>" },
        { field: "MOD_ID", width: 20, title: "Modalidad ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${MOD_DEC} </a>" },
        { field: "EST_ID", width: 18, title: "Establecimiento ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${EST_NAME} </a>" },
        { field: "BPS_ID", width: 18, title: "Responsable ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${BPS_NAME} </a>" },
    ]
            
    });
}


//function loadDataLicencia() {
//    var d = new Date();
//    var vffni = d.getFullYear() + "/" + (d.getMonth()+1) + "/" + d.getDate();
//    var vffin = d.getFullYear() + "/" + (d.getMonth()+1) + "/" + d.getDate();

//    $("#gridlicencias").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: K_PAGINACION.LISTAR_5,
//            transport: {
//                read: {
//                    type: "POST",
//                    url: "../Licencia/USP_LICENCIAMIENTO_LISTARPAGEJSON",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {
//                            LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
//                            LIC_TYPE: $("#ddTipoLicencia").val() == "" ? 0 : $("#ddTipoLicencia").val(),
//                            LICS_ID: $("#ddEstadoLicencia").val(),
//                            CUR_ALPHA: $("#ddMoneda").val(),
//                            MOD_ID: $("#hidIdModalidad").val() == "" ? 0 : $("#hidIdModalidad").val(),
//                            EST_ID: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
//                            BPS_ID: $("#hidIdResponsable").val() == "" ? 0 : $("#hidIdResponsable").val(),
//                            LIC_NAME: $("#txtNomLicencia").val(),
//                            LIC_TEMP: $("#ddTemporalidad").val(),
//                            RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
//                            LICMAS: 0,
//                            BPS_GROUP: $("#hidCodigoGrupoEmpresarial").val() == "" ? 0 : $("#hidCodigoGrupoEmpresarial").val(),
//                            BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
//                            conFecha: 0,
//                            finiauto: vffni,
//                            ffinauto: vffin,
//                            desc_artista: "",
//                            cod_artista_sgs:"-1"
//                        })
//                }
//            },
//            schema: { data: "ListaLicencias", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: {
//            messages: {
//                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
//                empty: "No se encontraron registros"
//            }
//        },
//        selectable: true,
//        columns:
//    [
//     {
//         hidden: true,
//         title: "Eliminar", width: 10, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
//     },
//     { field: "LIC_NAME", width: 13, title: "Nombre", template: "<a id='single_2'  href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LIC_NAME}</a>" },
//     { field: "LIC_DESC", width: 15, title: "Descripción", hidden:true, template: "<a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LIC_DESC}</a>" },
//     //{ field: "LIC_MASTER", width: 5, title: "Lic. Multiple", hidden: true, template: "<a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LIC_MASTER}</a>" },
//     { field: "LIC_TYPE", width: 12, title: "Tipo ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${LIC_TDESC} </a>" },
//     { field: "LICS_ID", width: 10, title: "Estado ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${WRKF_SLABEL} </a>" },
//     { field: "MOD_ID", width: 20, title: "Modalidad ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${MOD_DEC} </a>" },
//     { field: "EST_ID", width: 18, title: "Establecimiento ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${EST_NAME} </a>" },
//     { field: "BPS_ID", width: 18, title: "Responsable ", template: "<font color='green'><a id='single_2' href=javascript:getidLic('${LIC_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'> ${BPS_NAME} </a>" },
//    ]
//    });
//}

//function loadGridLicencias() {
//    var d = new Date();
//    var vffni = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();
//    var vffin = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();

//    $('#gridlicencias').data('kendoGrid').dataSource.query({
//        LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
//        LIC_TYPE: $("#ddTipoLicencia").val() == "" ? 0 : $("#ddTipoLicencia").val(),
//        LICS_ID: $("#ddEstadoLicencia").val(),
//        CUR_ALPHA: $("#ddMoneda").val(),
//        MOD_ID: $("#hidIdModalidad").val() == "" ? 0 : $("#hidIdModalidad").val(),
//        EST_ID: $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val(),
//        BPS_ID: $("#hidIdResponsable").val() == "" ? 0 : $("#hidIdResponsable").val(),
//        LIC_NAME: $("#txtNomLicencia").val(),
//        LIC_TEMP: $("#ddTemporalidad").val(),
//        RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
//        LICMAS: 0,
//        BPS_GROUP: $("#hidCodigoGrupoEmpresarial").val() == "" ? 0 : $("#hidCodigoGrupoEmpresarial").val(),
//        BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
//        conFecha: 0,
//        finiauto: vffni,
//        ffinauto: vffin,
//        desc_artista: "",
//        cod_artista_sgs:"-1",
//        //$("#txtCodMult").val() == "" ? 0 : $("#txtCodMult").val()
//        page: 1,
//        pageSize: K_PAGINACION.LISTAR_5
//    });
//}



var mvInitEstablecimientoAux = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;


    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewEstaux" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventEstaux" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:70px">Tipo    </td><td style="width:15px"><select id="ddlTipoestaux" style="width: 170px"/></td>';
    elemento += '<td style="width:70px">Sub tipo</td><td style="width:15px"><select id="ddlSubtipoestaux" style="width: 170px"/></td>';
    elemento += '<input type="hidden" id="hidCodigoBPSaux"> <input type="hidden" id="hidCodigoESTaux"> <input type="hidden" id="hidCodigoDiraux">';
    elemento += '</tr>';


    elemento += '<td>Nombre</td><td colspan="3"><input type="text" id="txtEstablecimientoaux" style="width: 380px"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Socio</td><td colspan="3"><input type="text" id="txtNombreSocionegocioaux" style="width: 380px"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo división</td><td><select id="ddlDivTipoaux" style="width: 170px"/></td>';
    elemento += '<td>División</td><td><select id="ddlDivaux" style="width: 170px"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado</td> <td><select id="ddlEstadoEstaux"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarEstaux"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarEstaux"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridEstablecimientoaux"></div>';
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
        width: 710,
        height: 500,
        title: "Búsqueda General de Establecimiento."
    });

    loadTipoestablecimiento('ddlTipoestaux', 0);
    $("#ddlTipoestaux").on("change", function () {
        var codigo = $("#ddlTipoestaux").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestaux', codigo);
    });

    loadEstados("ddlEstadoEstaux", 0);
    loadDivisionTipo('ddlDivTipoaux', 0);
    $("#ddlDivTipoaux").on("change", function () {
        var tipo = $(this).val();
        loadTipoDivision('ddlDivaux', tipo);
    });

    //inicializando variables de busqueda de establecimiento
    $("#hidCodigoBPSaux").val(0);
    $("#txtEstablecimientoaux").val("");
    $("#ddlTipoestaux").val(0);
    $("#ddlSubtipoestaux").val(0);

    initAutoCompletarRazonSocial("txtNombreSocionegocioaux", "hidCodigoBPSaux");

    if ($("#txtNombreSocionegocioaux").val() == "")
        $("#hidCodigoBPSaux").val(0);



    $("#btnLimpiarEstaux").on("click", function () {
        limpiarBusquedaEst();
        //var soc = $("#hidCodigoBPSaux").val();
        //$('#gridEstablecimientoaux').data('kendoGrid').dataSource.query({
        //    Tipoestablecimiento: $("#ddlTipoestaux").val(),
        //    SubTipoestableimiento: $("#ddlSubtipoestaux").val(),
        //    Nombreestablecimiento: $("#txtEstablecimientoaux").val(),
        //    Socio: $("#hidCodigoBPSaux").val(),
        //    Tipodivision: $("#ddlDivTipoaux").val(),
        //    Division: $("#ddlDivaux").val(),
        //    estado: $("#ddlEstadoEstaux").val() == "A" ? 1 : 0,
        //    page: 1, pageSize: K_PAGINACION.LISTAR_5
        //});
    });

    $("#btnBuscarEstaux").on("click", function (e) {
        loadDataFoundEstaux(e);
        //limpiarBusquedaEst();
        //var soc = $("#hidCodigoBPSaux").val();
        //$('#gridEstablecimientoaux').data('kendoGrid').dataSource.query({
        //    Tipoestablecimiento: $("#ddlTipoestaux").val(),
        //    SubTipoestableimiento: $("#ddlSubtipoestaux").val(),
        //    Nombreestablecimiento: $("#txtEstablecimientoaux").val(),
        //    Socio: $("#hidCodigoBPSaux").val(),
        //    Tipodivision: $("#ddlDivTipoaux").val(),
        //    Division: $("#ddlDivaux").val(),
        //    estado: $("#ddlEstadoEstaux").val() == "A" ? 1 : 0,
        //    page: 1, pageSize: K_PAGINACION.LISTAR_5
        //});        
    });

    $('#txtEstablecimientoaux').keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundEstaux(e);
        }
    });

    $('#txtNombreSocionegocioaux').keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundEstaux(e);
        }
    });

    //loadDataFoundEstaux();
    //------------------------------------------------------   
};

var getBPSEstaux = function (id) {
    var hidIdidModalViewEstaux = $("#hidIdidModalViewEstaux").val();
    var fnc = $("#hidIdEventEstaux").val();
    $("#" + hidIdidModalViewEstaux).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaEstaux = function () {
    $("#ddlTipoestaux").val(0);
    $("#ddlSubtipoestaux").val(0);
    $("#txtEstablecimientoaux").val("");
    $("#txtNombreSocionegocioaux").val("");
    $("#hidCodigoBPSaux").val(0);
    $("#ddlDivTipoaux").val("");
    $("#ddlDivaux").val(0);
    $("#ddlEstadoEstaux").val(0);
    $("#hidCodigoBPSaux").val(0);
    $("#hidCodigoESTaux").val(0);
};

function loadDataFoundEstaux(e) {
    var soc = $("#hidCodigoBPSaux").val();

    if ($("#gridEstablecimientoaux").data("kendoGrid") != undefined) {
        $("#gridEstablecimientoaux").empty();
    }
    
    var data_sourceEstaux = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_5,
        transport: {
            read: {
                url: "../Establecimiento/ConsultaGeneralEstablecimiento",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        Tipoestablecimiento: $("#ddlTipoestaux").val(),
                        SubTipoestableimiento: $("#ddlSubtipoestaux").val(),
                        Nombreestablecimiento: $("#txtEstablecimientoaux").val(),
                        Socio: $("#hidCodigoBPSaux").val(),
                        Tipodivision: $("#ddlDivTipoaux").val(),
                        Division: $("#ddlDivaux").val(),
                        estado: $("#ddlEstadoEstaux").val() == "A" ? 1 : 0
                    })
            }
        },
        schema: { data: "Establecimiento", total: 'TotalVirtual' }
    });


    var gridEstaux = $("#gridEstablecimientoaux").kendoGrid({
        dataSource: data_sourceEstaux,
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
            { field: "EST_ID", width: 5, title: " ID", template: "<a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_ID}</a>" },
            { field: "EST_TYPE", width: 10, title: "Tipo", template: "<a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_TYPE}</a>" },
            { field: "EST_SUBTYPE", width: 10, title: "SubTipo", template: "<a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_SUBTYPE}</a>" },
            { field: "EST_NAME", width: 12, title: "Nombre", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_NAME}</a>" },
            { field: "ADDRESS", width: 12, title: "Dirección", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ADDRESS}</a>" },
            { field: "BPS_NAME", width: 12, title: "Socio", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_NAME}</a>" },
            { field: "Activo", width: 10, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}# </a>" },
           ]
    });

};




function AgregarLicenciaCadenaSeleccionada(lic_mast) {

    var lic_id = $("#hidLicId").val();
    var lic_master_actual = $("#hidLicMaster").val();

    //alert(lic_master_actual + lic_mast);

    if (lic_master_actual == lic_mast) {
        alert("NO SE PUEDE ACTUALIZAR LA LICENCIA , LA CADENA SELECCIONADA ES IGUAL A LA ACTUAL | ELIJA OTRA CADENA ");
    } else {

        $.ajax({
            data: {
                LIC_ID: lic_id, LIC_MASTER: lic_mast
            },
            async: false,
            type: 'POST',
            url: '../Licencia/AgregarCadenaLicencia',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    alert(dato.message);
                    var hidIdidModalViewLic = $("#hidIdidView").val();
                    $("#" + hidIdidModalViewLic).dialog("close");

                    if ($("#btnGrabar").length) {
                        ObtenerLicencia(lic_id);
                    }

                } else {
                    $("#" + hidIdidModalViewLic).dialog("close");
                    alert(dato.message);
                }
            }
        });

    }
}





//var loadDataFoundEstaux = function () {
//    var soc = $("#hidCodigoBPSaux").val();
//    $("#gridEstablecimientoaux").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: K_PAGINACION.LISTAR_5,
//            transport: {
//                read: {
//                    url: "../Establecimiento/ConsultaGeneralEstablecimiento",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {
//                            Tipoestablecimiento: $("#ddlTipoestaux").val(),
//                            SubTipoestableimiento: $("#ddlSubtipoestaux").val(),
//                            Nombreestablecimiento: $("#txtEstablecimientoaux").val(),
//                            Socio: $("#hidCodigoBPSaux").val(),
//                            Tipodivision: $("#ddlDivTipoaux").val(),
//                            Division: $("#ddlDivaux").val(),
//                            estado: $("#ddlEstadoEstaux").val() == "A" ? 1 : 0
//                        })
//                }
//            },
//            schema: { data: "Establecimiento", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: true,
//        selectable: true,
//        filterable: {
//            extra: false,
//            operators: {
//                string: {
//                    contains: "Contiene"
//                }
//            }
//        },
//        columns:
//           [
//            { field: "EST_ID", width: 5, title: " ID", template: "<a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_ID}</a>" },
//            { field: "EST_TYPE", width: 10, title: "Tipo", template: "<a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_TYPE}</a>" },
//            { field: "EST_SUBTYPE", width: 10, title: "SubTipo", template: "<a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_SUBTYPE}</a>" },
//            { field: "EST_NAME", width: 12, title: "Nombre", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EST_NAME}</a>" },
//            { field: "ADDRESS", width: 12, title: "Dirección", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ADDRESS}</a>" },
//            { field: "BPS_NAME", width: 12, title: "Socio", template: "<font color='green'><a id='single_2' href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${BPS_NAME}</a>" },
//            { field: "Activo", width: 10, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEstaux('${EST_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}# </a>" },
//           ]
//    });
//};