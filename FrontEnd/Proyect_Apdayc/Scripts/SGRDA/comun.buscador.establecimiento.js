
var mvInitEstablecimiento = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;


    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewEst" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventEst" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:70px">Tipo    </td><td style="width:15px"><select id="ddlTipoest" style="width: 170px"/></td>';
    elemento += '<td style="width:13%">Sub tipo</td><td style="width:15px"><select id="ddlSubtipoest" style="width: 170px"/></td>';
    elemento += '<input type="hidden" id="hidCodigoBPS"> <input type="hidden" id="hidCodigoEST"> <input type="hidden" id="hidCodigoDir">';
    elemento += '</tr>';


    elemento += '<td>Nombre</td><td colspan="3"><input type="text" id="txtEstablecimiento" style="width: 380px"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Socio</td><td colspan="3"><input type="text" id="txtNombreSocionegocio" style="width: 380px"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Tipo división</td><td><select id="ddlDivTipo" style="width: 170px"/></td>';
    elemento += '<td>División</td><td><select id="ddlDiv" style="width: 170px"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado</td> <td><select id="ddlEstadoEst"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarEst"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarEst"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridEstablecimiento"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';

    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { initLoadCombosEstab(); $("#" + parametro.idDivMV).dialog("open"); });
    //if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { initLoadCombosEstab(); $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 710,
        height: 590,
        title: "Búsqueda General de Establecimiento."
    });

    $('#ddlSubtipoest').append($("<option />", { value: 0, text: '--SELECCIONE--' }));
    $('#ddlDiv').append($("<option />", { value: 0, text: '--SELECCIONE--' }));



    $("#ddlTipoest").on("change", function () {
        var codigo = $("#ddlTipoest").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoest', codigo);
    });


    $("#ddlDivTipo").on("change", function () {
        var tipo = $(this).val();
        loadTipoDivision('ddlDiv', tipo);
    })

    //inicializando variables de busqueda de establecimiento
    $("#hidCodigoBPS").val(0);
    $("#txtEstablecimiento").val("");
    $("#ddlTipoest").val(0);
    $("#ddlSubtipoest").val(0);

    initAutoCompletarRazonSocial("txtNombreSocionegocio", "hidCodigoBPS");

    if ($("#txtNombreSocionegocio").val() == "")
        $("#hidCodigoBPS").val(0);

    var _cuentaBusEsta = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarEst").on("click", function () {
        loadDataFoundEst();
        //if (_cuentaBusEsta == 1) {
        //    loadDataFoundEst();
        //} else {
        //    var soc = $("#hidCodigoBPS").val();
        //    $('#gridEstablecimiento').data('kendoGrid').dataSource.query({
        //        Tipoestablecimiento: $("#ddlTipoest").val(),
        //        SubTipoestableimiento: $("#ddlSubtipoest").val(),
        //        Nombreestablecimiento: $("#txtEstablecimiento").val(),
        //        Socio: $("#hidCodigoBPS").val(),
        //        Tipodivision: $("#ddlDivTipo").val(),
        //        Division: $("#ddlDiv").val(),
        //        //estado: $("#ddlEstadoEst").val() == "A" ? 1 : 0,
        //        estado: $("#ddlEstadoEst").val() == 2 ? 0 : 1,
        //        page: 1, pageSize: 4
        //    });
        //}
        //_cuentaBusEsta++;        
    });

    $("#btnLimpiarEst").on("click", function () {
        limpiarBusquedaEst();
    });

    $("#txtEstablecimiento").keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundEst(e);
        }
    });

    $("#txtNombreSocionegocio").keypress(function (e) {
        if (e.which == 13) {
            loadDataFoundEst(e);
        }
    });
    
    //loadDataFoundEst();
    //------------------------------------------------------   
};

var getBPSEst = function (id) {
    var hidIdidModalViewEst = $("#hidIdidModalViewEst").val();
    var fnc = $("#hidIdEventEst").val();
    //alert(fnc);
    $("#" + hidIdidModalViewEst).dialog("close");
    eval(fnc + " ('" + id + "');");
    //alert(fnc + "('" + id + "');");

};

var limpiarBusquedaEst = function () {
    $("#ddlTipoest").val(0);
    $("#ddlSubtipoest").val(0);
    $("#txtEstablecimiento").val("");
    //$("#txtDireccion").val("0"); //en pruebas, luego modificar hidCodigoDir al terminar el popup de direcciones
    $("#txtNombreSocionegocio").val("");
    $("#hidCodigoBPS").val(0);
    $("#ddlDivTipo").val("");
    $("#ddlDiv").val(0);
    $("#ddlEstadoEst").val(0);
    $("#hidCodigoBPS").val(0);
    $("#hidCodigoEST").val(0);
};

function loadDataFoundEst (e) {
    //alert($("#ddlTipoest").val());
    var tipo = $("#ddlTipoest").val() === null ? 0 : $("#ddlTipoest").val();
    var estado = $("#ddlEstadoEst").val() === null ? 0 : $("#ddlEstadoEst").val();
    var soc = $("#hidCodigoBPS").val();

    if ($("#gridEstablecimiento").data("kendoGrid") != undefined) {
        $("#gridEstablecimiento").empty();
    }


    //$("#gridEstablecimiento").kendoGrid({
    //dataSource: {
    var data_sourceEstBG = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        //pageSize: K_PAGINACION.LISTAR_15,
        //pageSize: 4,
        pageSize: K_PAGINACION.LISTAR_5,
        transport: {
            read: {
                url: "../Establecimiento/ConsultaGeneralEstablecimiento",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        //Tipoestablecimiento: $("#ddlTipoest").val(),
                        //Tipoestablecimiento: 0,
                        Tipoestablecimiento: tipo,
                        SubTipoestableimiento: $("#ddlSubtipoest").val(),
                        Nombreestablecimiento: $("#txtEstablecimiento").val(),
                        Socio: $("#hidCodigoBPS").val(),
                        Tipodivision: $("#ddlDivTipo").val(),
                        Division: $("#ddlDiv").val(),
                        estado: estado
                        //estado: $("#ddlEstadoEst").val() == 2 ? 0 : 1
                    })
            }
        },
        schema: { data: "Establecimiento", total: 'TotalVirtual' }
        //},
    })

    var gridEstBG = $("#gridEstablecimiento").kendoGrid({
        dataSource: data_sourceEstBG,
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            { field: "EST_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_ID}</a>" },
            { field: "EST_TYPE", width: 10, title: "Tipo", template: "<a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_TYPE}</a>" },
            { field: "EST_SUBTYPE", width: 10, title: "SubTipo", template: "<a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_SUBTYPE}</a>" },
            { field: "EST_NAME", width: 12, title: "Nombre", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_NAME}</a>" },
            { field: "ADDRESS", width: 12, title: "Dirección", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${ADDRESS}</a>" },
            { field: "BPS_NAME", width: 12, title: "Socio", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${BPS_NAME}</a>" },
            //{ field: "BPS_NAME", width: 20, title: "Razon Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEst('${BPS_ID}') style='color:gray'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },
            //{ field: "DAD_TNAME", width: 12, title: "Tipo division", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${DAD_TNAME}</a>" },
            //{ field: "DAD_NAME", width: 12, title: "Division", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${DAD_NAME}</a>" },
            { field: "Activo", width: 8, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
           ]
    });

    //});
};


//var loadDataFoundEst = function () {
//    //alert($("#ddlTipoest").val());
//    var tipo = $("#ddlTipoest").val() === null ? 0 : $("#ddlTipoest").val();
//    var estado = $("#ddlEstadoEst").val() === null ? 0 : $("#ddlEstadoEst").val();

//    var soc = $("#hidCodigoBPS").val();
//    $("#gridEstablecimiento").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            //pageSize: K_PAGINACION.LISTAR_15,
//            pageSize: 4,
//            transport: {
//                read: {
//                    url: "../Establecimiento/ConsultaGeneralEstablecimiento",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {
//                            //Tipoestablecimiento: $("#ddlTipoest").val(),
//                            //Tipoestablecimiento: 0,
//                            Tipoestablecimiento: tipo,
//                            SubTipoestableimiento: $("#ddlSubtipoest").val(),
//                            Nombreestablecimiento: $("#txtEstablecimiento").val(),
//                            Socio: $("#hidCodigoBPS").val(),
//                            Tipodivision: $("#ddlDivTipo").val(),
//                            Division: $("#ddlDiv").val(),
//                            estado: estado
//                            //estado: $("#ddlEstadoEst").val() == 2 ? 0 : 1
//                        })
//                }
//            },
//            schema: { data: "Establecimiento", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: true,
//        selectable: true,
//        //filterable: {
//        //    extra: false,
//        //    operators: {
//        //        string: {
//        //            contains: "Contiene"
//        //        }
//        //    }
//        //},
//        columns:
//           [
//            { field: "EST_ID", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_ID}</a>" },
//            { field: "EST_TYPE", width: 10, title: "Tipo", template: "<a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_TYPE}</a>" },
//            { field: "EST_SUBTYPE", width: 10, title: "SubTipo", template: "<a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_SUBTYPE}</a>" },
//            { field: "EST_NAME", width: 12, title: "Nombre", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${EST_NAME}</a>" },
//            { field: "ADDRESS", width: 12, title: "Dirección", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${ADDRESS}</a>" },
//            { field: "BPS_NAME", width: 12, title: "Socio", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${BPS_NAME}</a>" },
//            //{ field: "BPS_NAME", width: 20, title: "Razon Social", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEst('${BPS_ID}') style='color:gray'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a>" },
//            //{ field: "DAD_TNAME", width: 12, title: "Tipo division", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${DAD_TNAME}</a>" },
//            //{ field: "DAD_NAME", width: 12, title: "Division", template: "<font color='green'><a id='single_2' href=javascript:getBPSEst('${EST_ID}') style='color:gray'>${DAD_NAME}</a>" },
//            { field: "Activo", width: 8, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:getBPSEst('${EST_ID}') style='color:gray'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}# </a>" },
//           ]
//    });
//};



//invocado al momento de abrir popup de busqueda
var initLoadCombosEstab = function () {
    loadTipoestablecimiento('ddlTipoest', 0);
    loadEstados("ddlEstadoEst", 0);
    loadDivisionTipo('ddlDivTipo', 0);
}