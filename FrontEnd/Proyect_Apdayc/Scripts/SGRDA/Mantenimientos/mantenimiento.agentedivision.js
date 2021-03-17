/************************** INICIO CONSTANTES****************************************/
//var k_TIPO_DIVISIONES = { ADMINISTRATIVO: "ADM", GEO: "GEO" }
/************************** INICIO CARGA********************************************/
$(function () {
    $('#ddlDivision').append($("<option />", { value: 0, text: '--SELECCIONE--' }));
    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });
    $("#btnNuevo").on("click", function (e) {
        document.location.href = '../AgenteDivision/Nuevo';
    });
    //------------------------------------------------------------------------------    
    $("#btnBuscar").on("click", function (e) {        
        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({
            idOficina: $("#hidOficina").val(),
            iddivision: $("#ddlDivision").val(),            
            agenteRecaudo: $("#txtAgeRecaudador").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnLimpiar").on("click", function (e) {
        $("#hidOficina").val(0);
        $("#lbOficina").html('Seleccione una agencia de recaudo.');
        loadComboDivisionesXOficina('ddlDivision', 0, 0);
        $("#txtAgeRecaudador").val(''),

        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({
            idOficina: $("#hidOficina").val(),
            iddivision: $("#ddlDivision").val(),
            agenteRecaudo: $("#txtAgeRecaudador").val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#txtAgeRecaudador").keypress(function (e) {
        if (e.which == 13) {
            $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({
                idOficina: $("#hidOficina").val(),
                iddivision: $("#ddlDivision").val(),
                agenteRecaudo: $("#txtAgeRecaudador").val(),
                page: 1,
                pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    //loadData();
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    loadData();
    loadComboDivisionesXOficina('ddlDivision', 0, 0);
});

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../AgenteDivision/ListarAgenteRecaudo",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        idOficina: $("#hidOficina").val(),
                        iddivision: $("#ddlDivision").val(),
                        agenteRecaudo: $("#txtAgeRecaudador").val(),
                    })
            }
        },
        schema: { data: "ListarAgenteRecaudo", total: 'TotalVirtual' }
    });

    $("#gridAgenteRecaudo").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
                { field: "COLL_OFF_ID", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${COLL_OFF_ID}</a>" },
                { field: "OFF_NAME", width: 10, title: "Agencia Recaudo", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_NAME}</a>" },
                { field: "DIVISION", width: 10, title: "División", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DIVISION}</a>" },
                { field: "RECAUDADOR", width: 15, title: "Recaudador", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RECAUDADOR}</a>" },
                { field: "ROL", width: 6, title: "Rol", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ROL}</a>" },
                { field: "F_INICIAL", width: 6, title: "F. Inicial", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_INICIAL}</a>" },
                { field: "F_FINAL", width: 6, title: "F. Final", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_FINAL}</a>" },
        ]
    });
};


// OFICINA - BUSQ. GENERAL
var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
};

function obtenerNombreConsultaOficina(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/obtenerNombreOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
                loadComboDivisionesXOficina('ddlDivision', idSel, 0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


//****************************  FUNCIONES ****************************
//function loadData() {
//    var sharedDataSource = new kendo.data.DataSource({
//        serverPaging: true,
//        pageSize: K_PAGINACION.LISTAR_15,
//        type: "json",
//        transport: {
//            read: {
//                type: "POST",
//                url: "../AgenteDivision/ListarAgenteRecaudo",
//                dataType: "json"
//            },
//            parameterMap: function (options, operation) {
//                if (operation == 'read')
//                    return $.extend({}, options, {
//                        dato: 'fer'
//                    })
//            }
//        },
//        schema: { data: "ListarAgenteRecaudo", total: 'TotalVirtual' }
//    });

//    $("#gridAgenteRecaudo").kendoGrid({
//        dataSource: sharedDataSource,
//        groupable: false,
//        sortable: K_ESTADO_ORDEN,
//        pageable: {
//            messages: {
//                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
//                empty: "No se encontraron registros"
//            }
//        },
//        selectable: "multiple row",
//        columns: [
//                { field: "COLL_OFF_ID", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${COLL_OFF_ID}</a>" },
//                { field: "RECAUDADOR", width: 15, title: "Recaudador", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RECAUDADOR}</a>" },
//                { field: "ROL", width: 6, title: "Rol", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ROL}</a>" },
//                { field: "OFF_NAME", width: 10, title: "Agencia Recaudo", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${OFF_NAME}</a>" },
//                { field: "DIVISION", width: 10, title: "División", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DIVISION}</a>" },
//                { field: "F_INICIAL", width: 6, title: "F. Inicial", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_INICIAL}</a>" },
//                { field: "F_FINAL", width: 6, title: "F. Final", template: "<a id='single_2'  href=javascript:editar('${COLL_OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${F_FINAL}</a>" },
//        ]
//    });
//};



function editar(idSelDiv) {
    document.location.href = '../AgenteDivision/Nuevo?id=' + idSelDiv;
    $("#hidOpcionEdit").val(1);
}

function eliminar() {
    var idDiv = $("#ddlDivision").val();
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var dad_id = $row.find('.kendo-chk').attr('value');
            values.push(dad_id);
            delDivision(dad_id);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un agente.");
    } else {
        alert("Division administrativa eliminada correctamente.");
        //loadData();
    }
}

function delDivision(divID) {
    $.ajax({
        url: "../AgenteDivision/Eliminar",
        data: { idDad: divID },
        type: "POST",
        success: function () {
        }
    });
}

