
var mvInitBuscarCorrelativo = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewCOR" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventCOR" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor">'
    elemento += '<table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Descripción</td>';
    elemento += '<td><input type="text"   id="txtDescripcionCOR" style="width:300px" /></td>';
    elemento += '</tr>';
        
    elemento += '<tr>';
    elemento += '<td>Serie </td><td colspan="3"><input type="text"   id="txtSerieCOR" style="width:75px" /></td>';

    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoCOR" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarCOR"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarCOR"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridCOR"></div>';
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
        width:750,
        height: 470,
        title: "Búsqueda General de Correlativos"
    });

    loadEstadosMaestro("ddlEstadoCOR");

    $("#txtDescripcionCOR").keypress(function (e) {
        if (e.which == 13) {
            $('#gridCOR').data('kendoGrid').dataSource.query({
                dato: $("#txtDescripcionCOR").val(),
                st: $("#ddlEstadoCOR").val(),
                serie: $("#txtSerieCOR").val(),
                tipo: $("#ddlTipoFacturacionIndividual").val() == undefined ? "1" : $("#ddlTipoFacturacionIndividual").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#txtSerieCOR").keypress(function (e) {
        if (e.which == 13) {
            $('#gridCOR').data('kendoGrid').dataSource.query({
                dato: $("#txtDescripcionCOR").val(),
                st: $("#ddlEstadoCOR").val(),
                serie: $("#txtSerieCOR").val(),
                tipo: $("#ddlTipoFacturacionIndividual").val() == undefined ? "1" : $("#ddlTipoFacturacionIndividual").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnLimpiarCOR").on("click", function () {
        limpiarBusquedaCOR();
    });

    $("#btnBuscarCOR").on("click", function () {
        $('#gridCOR').data('kendoGrid').dataSource.query({
            dato: $("#txtDescripcionCOR").val(),
            st: $("#ddlEstadoCOR").val(),
            serie: $("#txtSerieCOR").val(),
            tipo: $("#ddlTipoFacturacionIndividual").val() == undefined ? "1" : $("#ddlTipoFacturacionIndividual").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });
    loadDataFoundCOR();
};

var getCOR = function (id) {
    var hidIdidModalView = $("#hidIdidModalViewCOR").val();
    var fnc = $("#hidIdEventCOR").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaCOR = function () {
    $("#txtDescripcionCOR").val("");
    $("#txtSerieCOR").val("");
    $("#ddlEstadoCOR").val(1);    
};

var loadDataFoundCOR = function () {
    $("#gridCOR").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../CORRELATIVOS/usp_listar_CorrelativosJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            dato: $("#txtDescripcionCOR").val(),
                            st: $("#ddlEstadoCOR").val(),
                            serie: $("#txtSerieCOR").val()
                            ,tipo: $("#ddlTipoFacturacionIndividual").val() == undefined ? "1" : $("#ddlTipoFacturacionIndividual").val()
                        })
                }
            },
            schema: { data: "RECNUMBERING", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
        [
            {
                hidden: true,
                field: "OWNER", width: 100, title: "PROPIETARIO", template: "<a id='single_2'  href=javascript:getCOR('${NMR_ID}') style='color:gray;'>${OWNER}</a>"
            }, {

                field: "NMR_ID", width: 12, title: "Id", template: "<a  id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>${NMR_ID}</a>"
            }, {
                hidden: true,
                field: "NMR_TYPE", width: 50, title: "Tipo", template: "<a  id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>${NMR_TYPE}</a>"
            }, {
                field: "NMR_TDESC", width: 35, title: "Tipo", template: "<a  id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>${NMR_TDESC}</a>"
            }, {
                field: "NMR_SERIAL", width: 15, title: "Serial", template: "<a  id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>${NMR_SERIAL}</a>"
            }, {
                field: "NMR_NAME", width: 70, title: "Nombre", template: "<a  id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>${NMR_NAME}</a>"
            }, {
                field: "NMR_FORM", width: 20, title: "Inicio rango", template: "<a id='single_2'  href=javascript:getCOR('${NMR_ID}')   style='color:gray;'>${NMR_FORM}</a>"
            }, {                
                field: "NMR_TO", width: 20, title: "Fin Rango", template: "<a id='single_2'  href=javascript:getCOR('${NMR_ID}') style='color:gray !important;'>${NMR_TO}</a></font>"
            },
            { field: "ACTIVO", width: 20, title: "Estado", template: "<a id='single_2'  href=javascript:getCOR('${NMR_ID}')  style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });

};