
var mvInitBuscarCorrelativoRecibo = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewCORrec" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventCORrec" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor">'
    elemento += '<table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Descripción</td>';
    elemento += '<td><input type="text"   id="txtDescripcionCORrec" style="width:300px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Serie </td><td colspan="3"><input type="text"   id="txtSerieCORrec" style="width:75px" /></td>';


    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoCORrec" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarCORrec"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarCORrec"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridCORrec"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';

    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width:750,
        height: 530,
        title: "Búsqueda General de Correlativos Recibos"
    });

    loadEstadosMaestro("ddlEstadoCORrec");
    $("#ddlEstadoGRUrec").val(1);

    $("#txtDescripcionCORrec").keypress(function (e) {
        if (e.which == 13) {
            $('#gridCORrec').data('kendoGrid').dataSource.query({
                dato: $("#txtDescripcionCORrec").val(),
                st: $("#ddlEstadoCORrec").val(),
                serie: $("#txtSerieCORrec").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#txtSerieCORrec").keypress(function (e) {
        if (e.which == 13) {
            $('#gridCORrec').data('kendoGrid').dataSource.query({
                dato: $("#txtDescripcionCORrec").val(),
                st: $("#ddlEstadoCORrec").val(),
                serie: $("#txtSerieCORrec").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnLimpiarCORrec").on("click", function () {
        limpiarBusquedaCORrec();
    });

    $("#btnBuscarCORrec").on("click", function () {
        var grid = $('#gridCORrec').html();
        if (grid == '') {
            loadDataFoundCORrec();
        } else {
            $('#gridCORrec').data('kendoGrid').dataSource.query({
                dato: $("#txtDescripcionCORrec").val(),
                st: $("#ddlEstadoCORrec").val(),
                serie: $("#txtSerieCORrec").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });
    //loadDataFoundCORrec();
};

var getCORrec = function (id) {
    var hidIdidModalView = $("#hidIdidModalViewCORrec").val();
    var fnc = $("#hidIdEventCORrec").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaCORrec = function () {
    $("#txtDescripcionCORrec").val("");
    $("#txtSerieCORrec").val("");
    $("#ddlEstadoCORrec").val(1);
};

var loadDataFoundCORrec = function () {
    $("#gridCORrec").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../CORRELATIVOS/ListarCorrelativosReciboJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            dato: $("#txtDescripcionCORrec").val(),
                            st: $("#ddlEstadoGRUrec").val(),
                            serie: $("#txtSerieCORrec").val()
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
                field: "NMR_ID", width: 12, title: "Id", template: "<a  id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>${NMR_ID}</a>"
            },
            {
                hidden: true,
                field: "NMR_TYPE", width: 50, title: "Tipo", template: "<a  id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>${NMR_TYPE}</a>"
            },
            {
                field: "NMR_TDESC", width: 35, title: "Tipo", template: "<a  id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>${NMR_TDESC}</a>"
            },
            {
                field: "NMR_SERIAL", width: 15, title: "Serial", template: "<a  id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>${NMR_SERIAL}</a>"
            },
            {
                field: "NMR_NAME", width: 70, title: "Nombre", template: "<a  id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>${NMR_NAME}</a>"
            },
            {
                field: "NMR_FORM", width: 20, title: "Ini. rang", template: "<a id='single_2'  href=javascript:getCORrec('${NMR_ID}')   style='color:gray;'>${NMR_FORM}</a>"
            },
            {
                field: "NMR_TO", width: 20, title: "Fin Rang", template: "<a id='single_2'  href=javascript:getCORrec('${NMR_ID}') style='color:gray !important;'>${NMR_TO}</a></font>"
            },
            {
                field: "NMR_NOW", width: 20, title: "Act. rag", template: "<a id='single_2'  href=javascript:getCORrec('${NMR_ID}') style='color:gray !important;'>${NMR_NOW}</a></font>"
            },
            { field: "ACTIVO", width: 20, title: "Estado", template: "<a id='single_2'  href=javascript:getCORrec('${NMR_ID}')  style='color:gray;'>#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    });

};