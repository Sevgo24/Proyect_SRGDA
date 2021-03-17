
var mvInitBuscarGrupoF = function (parametro) {
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewGRU" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventGRU" value="' + parametro.event + ' "/>';
    elemento += '<div id="ContenedormvSocioAux"></div>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<input type="hidden" id="hidCodigoGRU" />';
    elemento += '<tr>';
    elemento += '<td><div class="contenedor">'
    elemento += '<table border=0 style=" width:100%; "><tr>';
    elemento += '<td>Usuario de Derecho</td>';
    

    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarBSAux" style="cursor:pointer;" alt="Búsqueda de Usuario Derecho" title="Búsqueda de Usuario Derecho"/> </td>';
    elemento += '<td> <input type="hidden" id="hidSocioAux" value="0" /> <lable id="lbResponsableAux" style="cursor:pointer;" alt="Búsqueda de Usuario Derecho" title="Búsqueda de Usuario Derecho">Todos</lable> </td>';
    
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';


    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Grupo Modalidad de Uso</td>';
    elemento += '<td> <select id="ddlGrupoModalidadGRU" />  </td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Grupo de Facturación</td>';
    elemento += '<td><input type="text" id="txtGrupoFacturacionGRU" style="width:300px" /></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado</td><td colspan="3"><select id="ddlEstadoGRU" /></td>';

    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarGRUPO"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarGRU"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridGRU"></div>';
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
        width: 800,
        height: 530,
        title: "Búsqueda General de Grupos de Facturación"
    });

    loadEstadosMaestro("ddlEstadoGRU");
    loadTipoGrupo('ddlGrupoModalidadGRU', '0');
    initAutoCompletarRazonSocial("txtUsuarioGRU", "hidCodigoGRU");

    $("#txtUsuarioGRU").keypress(function (e) {
        if (e.which == 13) {
            $('#gridGRU').data('kendoGrid').dataSource.query({
                UserDer: $("#hidCodigoGRU").val(),
                GrupoMod: $("#ddlGrupoModalidadGRU").val(),
                parametro: $("#txtGrupoFacturacionGRU").val(),
                st: $("#ddlEstadoGRU").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#txtGrupoFacturacionGRU").keypress(function (e) {
        if (e.which == 13) {
            $('#gridGRU').data('kendoGrid').dataSource.query({
                UserDer: $("#hidCodigoGRU").val(),
                GrupoMod: $("#ddlGrupoModalidadGRU").val(),
                parametro: $("#txtGrupoFacturacionGRU").val(),
                st: $("#ddlEstadoGRU").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnLimpiarGRU").on("click", function () {
        limpiarBusquedaGRU();
    });

    $("#btnBuscarGRUPO").on("click", function () {
        $('#gridGRU').data('kendoGrid').dataSource.query({
            UserDer: $("#hidSocioAux").val(),
            GrupoMod: $("#ddlGrupoModalidadGRU").val(),
            parametro: $("#txtGrupoFacturacionGRU").val(),
            st: $("#ddlEstadoGRU").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    limpiarBusquedaGRU();
    
    mvInitBuscarSocioAux({ container: "ContenedormvSocioAux", idButtonToSearch: "btnBuscarBSAux", idDivMV: "mvBuscarSocioAux", event: "reloadEventoSociox", idLabelToSearch: "lbResponsableAux" });
};


var reloadEventoSociox = function (idSel) {
    $("#hidSocioAux").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsableAux").html(dato.valor);
            }
        }
    });
};

var reloadEventoGrupoFact = function (id) {
    $("#hidGrupoFacturacion").val(id);
    $.ajax({
        data: { id: id },
        url: '../GrupoFacturacion/Obtiene',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $("#lbGrupo").html(datos.INVG_DESC);
            }
        }
    });
};


var getGRU = function (id) {
    var hidIdidModalView = $("#hidIdidModalViewGRU").val();
    var fnc = $("#hidIdEventGRU").val();
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

var limpiarBusquedaGRU = function () {   
    $("#ddlGrupoModalidadGRU").val(0);
    $("#txtGrupoFacturacionGRU").val("");
    $("#ddlEstadoGRU").val(1);
    $("#hidCodigoGRU").val('0');
    $("#hidSocioAux").val('0');
    $("#lbResponsableAux").html('Todos');
    loadDataFoundGRU();
};

var loadDataFoundGRU = function () {

    $("#gridGRU").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../GrupoFacturacion/Listar_PageJson_Grupo_Facturacion",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            UserDer: $("#hidSocioAux").val(),
                            GrupoMod: $("#ddlGrupoModalidadGRU").val(),
                            parametro: $("#txtGrupoFacturacionGRU").val(),
                            st: $("#ddlEstadoGRU").val()
                        })
                }
            },
            schema: { data: "GrupoFacturacion", total: 'TotalVirtual' }
        },
        groupable: false,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        sortable: K_ESTADO_ORDEN,
        selectable: true,
        columns:
           [
            { field: "INVG_ID", width: 10, title: "Id", template: "<a id='single_2'  href=javascript:getGRU('${INVG_ID}') style='color:gray !important;'>${INVG_ID}</a>" },            
            { field: "INVG_DESC", width: 50, title: "Grupo de Facturación", template: "<a id='single_2'  href=javascript:getGRU('${INVG_ID}') style='color:gray !important;'>${INVG_DESC}</a></font>" },
            { field: "GRUPO", width: 50, title: "Grupo de Modalidad", template: "<a id='single_2'  href=javascript:getGRU('${INVG_ID}') style='color:gray !important;'>${GRUPO}</a></font>" },            
            { field: "BPS_NAME", width: 70, title: "Usuario Derecho", template: "<a id='single_2'  href=javascript:getGRU('${INVG_ID}') style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "Activo", width: 20, title: "Estado", template: "<a id='single_2'  href=javascript:getGRU('${INVG_ID}') style='color:gray !important;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
           ]
    })

};

function ObtenerGrupoFacturacion(id) {
    reloadEventoGrupoFact(id);
}