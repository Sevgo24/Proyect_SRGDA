
$(function () {
    $("#hidResponsable").val(0);
    kendo.culture('es-PE');
    $("#txtFechaUltimoLanzamiento").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    FechaActual();
    loadData();

    $("#btnBuscar").on("click", function () {
        CargaGrid(); 
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../ComisionTotales/Nuevo";
    });
});

function editar(idSel) {
    document.location.href = '../ComisionTotales/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}


function CargaGrid() {
    $('#gridComisionesTotales').data('kendoGrid').dataSource.query({
        ProgramaId: $("#txtPrograma").val() == "" ? 0 : $("#txtPrograma").val(),
        Ultfecha: $("#txtFechaUltimoLanzamiento").val(),
        IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        //st: $("#ddlEstado").val() == "" ? 0 : $("#ddlEstado").val(),
        st: 0,
        page: 1,
        pageSize: K_PAGINACION.LISTAR_20
    });
}

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    var estado = validarRolAgenteRecaudo(idSel);
    if (estado)
        obtenerNombreSocio($("#hidResponsable").val());
};

function validarRolAgenteRecaudo(id) {
    var estado = false;
    $.ajax({
        data: { idAsociado: id },
        url: '../ComisionTotales/ValidacionPerfilAgenteRecaudo',
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
};

function FechaActual() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    today = mm + '/' + dd + '/' + yyyy;
    $('#txtFechaUltimoLanzamiento').val(today);
}

function loadData() {
    $("#gridComisionesTotales").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../ComisionTotales/ListaComisionesTotales",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            ProgramaId: $("#txtPrograma").val() == "" ? 0 : $("#txtPrograma").val(),
                            Ultfecha: $("#txtFechaUltimoLanzamiento").val(),
                            IdRepresentante: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
                            //st: $("#ddlEstado").val() == "" ? 0 : $("#ddlEstado").val()
                            st: 0
                        });
                }
            },
            schema: { data: "ListaComisionTotales", total: 'TotalVirtual' }
        },
        sortable: true,
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
                title: 'Eliminar', width: 60, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${PRG_ID}'/>"
            },
            { field: "PRG_ID", width: 50, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${PRG_ID}') style='color:gray !important;'>${PRG_ID}</a>" },
            { field: "PRG_DESC", width: 120, title: "<font size=2px>Programa</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${PRG_ID}') style='color:gray !important;'>${PRG_DESC}</a></font>" },
            { field: "START", type: "date", width: 100, title: "<font size=2px>Fecha Inicial</font>", template: "<a id='single_2' href=javascript:editar('${PRG_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(START==null)?"":kendo.toString(kendo.parseDate(START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "ENDS", type: "date", width: 100, title: "<font size=2px>Fecha Final</font>", template: "<a id='single_2' href=javascript:editar('${PRG_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(ENDS==null)?"":kendo.toString(kendo.parseDate(ENDS,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            {
                hidden: true,
                field: "BPS_ID", width: 120, title: "<font size=2px>IdRepresentante</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${PRG_ID}') style='color:gray !important;'>${BPS_ID}</a></font>"
            },
            { field: "BPS_NAME", width: 40, title: "<font size=2px>Representante</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${PRG_ID}') style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "RAT_FDESC", width: 60, title: "<font size=2px>Periodicidad</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${PRG_ID}') style='color:gray !important;'>${RAT_FDESC}</a></font>" },
            //{ field: "ESTADO", width: 60, title: "<font size=2px>Estado</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a>" },
           ]
    });
}