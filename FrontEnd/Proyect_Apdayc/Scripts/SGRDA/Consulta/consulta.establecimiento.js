
/************************** INICIO CARGA********************************************/
var tipoDivisionADM = 'ADM'
$(function () {
    Limpiar();
    $('#txtId').on("keypress", function (e) { return solonumeros(e); });
    $("#hidCodigoUbigeo").val(0);
    loadDivisionXtipo('ddlDivisionAdm', tipoDivisionADM, 0);
    loadTipoestablecimiento('ddlTipoEstablecimiento', 0);
    loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', 0, 0);
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    loadComboTerritorio(0);
    initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
    
    $("#txtUbigeo").keypress(function () {
        var ubigeo = $(this).val()
        if (ubigeo == '') {
            $("#hidCodigoUbigeo").val(0);
        }
    });

    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#ddlTipoEstablecimiento").on("change", function () {
        var codigo = $("#ddlTipoEstablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo, 0);
    });

    $("#btnBuscar").on("click", function (e) {
        $('#grid').data('kendoGrid').dataSource.query({
            idEst: $("#txtId").val() == '' ? 0 : $("#txtId").val(),
            nombre: $("#txtNombre").val(),
            idSoc: $("#hidEdicionEnt").val(),
            tipo: $("#ddlTipoEstablecimiento").val(),
            subTipo: $("#ddlSubtipoestablecimiento").val(),
            idDivision: $("#ddlDivisionAdm").val(),
            ubigeo: $("#hidCodigoUbigeo").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#txtId").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({
                idEst: $("#txtId").val() == '' ? 0 : $("#txtId").val(),
                nombre: $("#txtNombre").val(),
                idSoc: $("#hidEdicionEnt").val(),
                tipo: $("#ddlTipoEstablecimiento").val(),
                subTipo: $("#ddlSubtipoestablecimiento").val(),
                idDivision: $("#ddlDivisionAdm").val(),
                ubigeo: $("#hidCodigoUbigeo").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#txtNombre").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({
                idEst: $("#txtId").val() == '' ? 0 : $("#txtId").val(),
                nombre: $("#txtNombre").val(),
                idSoc: $("#hidEdicionEnt").val(),
                tipo: $("#ddlTipoEstablecimiento").val(),
                subTipo: $("#ddlSubtipoestablecimiento").val(),
                idDivision: $("#ddlDivisionAdm").val(),
                ubigeo: $("#hidCodigoUbigeo").val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });

    $("#btnLimpiar").on("click", function (e) {
        Limpiar();
    });
    
    //-------------------------- CARGA LISTA ------------------------------------
    loadData();
});
function Limpiar() {
    $("#txtId").val('');
    $("#txtNombre").val('');
    $("#lbResponsable").html('Seleccione un usuario de derecho. ');
    $("#hidEdicionEnt").val(0);
    $("#ddlTipoEstablecimiento").val(0);
    $("#ddlSubtipoestablecimiento").val(0);
    $("#ddlDivisionAdm").val(0);
    $("#txtUbigeo").val('');
    $('grid').html('');
}

function loadData() {
    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../ConsultaEstablecimiento/ListarConsulta",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        idEst: $("#txtId").val() == '' ? '0' : $("#txtId").val(),
                        nombre: $("#txtNombre").val(),
                        idSoc: $("#hidEdicionEnt").val(),
                        tipo: $("#ddlTipoEstablecimiento").val(),
                        subTipo: $("#ddlSubtipoestablecimiento").val(),
                        idDivision: $("#ddlDivisionAdm").val(),
                        ubigeo: $("#hidCodigoUbigeo").val()
                    })
            }
        },
        schema: { data: "Establecimiento", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({
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
				{ field: "EST_ID", width: 7, title: "Id", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EST_ID}</a>" },
				{ field: "EST_NAME", width: 20, title: "Establecimiento", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EST_NAME}</a>" },
                { field: "EST_TYPE",  width: 20, title: "Tipo", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EST_TYPE}</a>" },
                { field: "EST_SUBTYPE", width: 20, title: "SubTipo", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EST_SUBTYPE}</a>" },
                { field: "BPS_NAME",  width: 20, title: "Socio", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${BPS_NAME}</a>" },
                { field: "DIVISION",  width: 20, title: "División", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DIVISION}</a>" },
                { field: "ADDRESS",  width: 20, title: "Dirección", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADDRESS}</a>" }
        ]
    });
};

function editar(idSel) {
    document.location.href = '../ConsultaEstablecimiento/Ver?id=' + idSel;
    $("#hidOpcionEdit").val(1);
    limpiar();
}

//SOCIO - BUSQ. GENERAL
var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    obtenerNombreSocio($("#lbResponsable").val(), 'lbResponsable');

};

function obtenerNombreSocio(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
            }
        }
    });
}