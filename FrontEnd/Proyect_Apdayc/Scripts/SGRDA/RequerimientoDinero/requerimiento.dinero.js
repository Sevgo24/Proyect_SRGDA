/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvRequerimientoDinero";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_ESTADO = { Abierto: 1, Pendiente: 2, Atendido: 3, Entregado: 4, Rendido: 5, Anulado: 6 };

/*INICIO CONSTANTES*/
/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    loadTipoDocumento('ddlTipoDocumento', 0);    
    initAutoCompletarRazonSocial("txtRazon", "hidIdBps");
    //------------------------------------------------------------------------------
    $("#hidIdBps").val(0);
    $("#txtRazon").focus();
    $("#txtRazon").keyup(function () {
        var razon = $("#txtRazon").val();
        if (razon == '')
            $("#hidIdBps").val(0);
    });

    //-------------------------- EVENTO BOTONES ------------------------------------
    $("#btnNuevo").on("click", function () {
        location.href = "../RequerimientoDinero/Nuevo";
    });

    $("#txtRazon").keypress(function (e) {
        if (e.which == 13) {
            var vTipo = 0;
            var vEstado = 0;
            var idBps = $("#hidIdBps").val();
            var vNum = $("#txtIdentificacion").val();
            var vNombre = $("#txtRazon").val();

            if ($("#ddlTipoDocumento").val() != null)
                vTipo = $("#ddlTipoDocumento").val();

            var selected = $(".rbEstado:checked");
            if (selected.val() != undefined)
                vEstado = selected.val();
            $('#grid').data('kendoGrid').dataSource.query({
                id: idBps,
                tipo: vTipo,
                nro: vNum,
                nombre: vNombre,
                estado: vEstado,
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#txtIdentificacion").keypress(function (e) {
        if (e.which == 13) {
            var vTipo = 0;
            var vEstado = 0;
            var idBps = $("#hidIdBps").val();
            var vNum = $("#txtIdentificacion").val();
            var vNombre = $("#txtRazon").val();

            if ($("#ddlTipoDocumento").val() != null)
                vTipo = $("#ddlTipoDocumento").val();

            var selected = $(".rbEstado:checked");
            if (selected.val() != undefined)
                vEstado = selected.val();
            $('#grid').data('kendoGrid').dataSource.query({
                id: idBps,
                tipo: vTipo,
                nro: vNum,
                nombre: vNombre,
                estado: vEstado,
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnBuscar").on("click", function () {      
        var vTipo = 0;
        var vEstado = 0;
        var idBps = $("#hidIdBps").val();
        var vNum = $("#txtIdentificacion").val();
        var vNombre = $("#txtRazon").val();

        if ($("#ddlTipoDocumento").val() != null)
            vTipo = $("#ddlTipoDocumento").val();

        var selected = $(".rbEstado:checked");
        if (selected.val() != undefined)
            vEstado = selected.val();
        $('#grid').data('kendoGrid').dataSource.query({
            id: idBps,
            tipo: vTipo,
            nro: vNum,
            nombre: vNombre,
            estado: vEstado,
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        var vTipo = 0;
        var vEstado = 0;
        var idBps = $("#hidIdBps").val();
        var vNum = $("#txtIdentificacion").val();
        var vNombre = $("#txtRazon").val();

        if ($("#ddlTipoDocumento").val() != null)
            vTipo = $("#ddlTipoDocumento").val();

        var selected = $(".rbEstado:checked");
        if (selected.val() != undefined)
            vEstado = selected.val();
        $('#grid').data('kendoGrid').dataSource.query({
            id: idBps,
            tipo: vTipo,
            nro: vNum,
            nombre: vNombre,
            estado: vEstado,
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
    
});

/************************** FUNCIONES ********************************************/

function limpiar() {
    $("#hidIdBps").val(0);
    $("#txtIdentificacion").val('');
    $("#txtRazon").val('');
    $('input:radio[class=rbEstado][id=rbTodos]').prop('checked', true);
}

function loadData() {
    var vTipo = 0;
    var vEstado = 0;
    var idBps = $("#hidIdBps").val();    
    var vNum = $("#txtIdentificacion").val();
    var vNombre = $("#txtRazon").val();

    if ($("#ddlTipoDocumento").val() != null)
        vTipo = $("#ddlTipoDocumento").val();

    var selected = $(".rbEstado:checked");
    if (selected.val() != undefined)
        vEstado = selected.val();
    

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../RequerimientoDinero/Listar",
                    dataType: "json",
                    data: {
                        id: idBps,
                        tipo: vTipo,
                        nro: vNum,
                        nombre: vNombre,
                        estado: vEstado
                    }
                }
            },
            schema: { data: "RequerimientoDinero", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
            { field: "MNR_ID", width: 45, title: "Id", template: "<a id='single_2'  href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${MNR_ID}</a>" },            
            { field: "BPS_NAME", width: 180, title: "Nombre / Razón Social", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;'>${BPS_NAME}</a></font>" },

            { field: "TIPO_DOC", width: 80, title: "Tipo Doc.", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${TIPO_DOC}</a></font>" },
            { field: "NUM_DOC", width: 80, title: "N° Doc.", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${NUM_DOC}</a></font>" },

            { field: "MNR_DESC", width: 240, title: "Justificación", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${MNR_DESC}</a></font>" },
            { field: "MNR_DATE", type: "date", width: 88, title: "F. Solicitud", template: "<a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px '>" + '#=(MNR_DATE==null)?"":kendo.toString(kendo.parseDate(MNR_DATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
                        
            { field: "ESTADO", width: 80, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a></font>" },
            { field: "STT_ID", width: 80,hidden:true, title: "Estado_Id", template: "<font color='green'><a id='single_2' href=javascript:editar('${MNR_ID}','${STT_ID}') style='color:gray;text-decoration:none;font-size:11px'>${STT_ID}</a></font>" },

            { field: "MNR_VALUE_PRE", width: 87, title: "S/.Solicitado", format: "{0:c2}" },
            { field: "MNR_VALUE_APR", width: 87, title: "S/.Aprobado", format: "{0:c2}" },
            { field: "MNR_VALUE_CON", width: 83, title: "S/.Gastado", format: "{0:c2}" }            
            
           ]
    });

}

function editar(idSel, idEstado) {
    if (K_ESTADO.Abierto == idEstado) 
    {
        location.href = "../RequerimientoDinero/Nuevo?id=" + idSel;
    }

    if (K_ESTADO.Pendiente == idEstado || K_ESTADO.Atendido == idEstado)
    {
        location.href = "../RequerimientoDinero/Aprobacion?id=" + idSel;
    }

    if (K_ESTADO.Entregado == idEstado || K_ESTADO.Rendido == idEstado)
    {
        location.href = "../RequerimientoDinero/Rendir?id=" + idSel;
    }
}


