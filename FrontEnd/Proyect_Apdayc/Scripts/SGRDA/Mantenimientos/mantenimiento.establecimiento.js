/*INICIO CONSTANTES*/
var K_ID_DIR = "divDir";
var INEI = 1
var idsocio = $("#txtRazonSocial").val();
var K_WIDTH_OBS2_EST = 600;
var K_HEIGHT_OBS2_EST = 325;

$(function () {
    /*Pestaña Dirección*/
    //initFormDireccionNMV({
    //    parentControl: K_ID_DIR
    //});
    $("#hidCodigoBPS").val(0);
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    //var x = document.getElementById("ddlDivision");
    //var option = document.createElement("option");
    //option.text = "--SELECCIONE--";
    //x.add(option);

    //load autocompletar
    initAutoCompletarRazonSocial("txtRazonSocial", "hidCodigoBPS");
    initAutoCompletarEstablecimiento("txtEstablecimiento", "hidCodigoEST");

    //carga Inicial de los tipos y subtipos de establecimiento.
    loadTipoIdentificacion('');
    loadTipoestablecimiento('ddlTipoestablecimiento', 0);

    $('#ddlSubtipoestablecimiento  option').remove();
    $('#ddlSubtipoestablecimiento').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));


    loadDivisionesadministrativas('');
    $("#ddlTipoestablecimiento").on("change", function () {
        var codigo = $("#ddlTipoestablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo);
    });



    $("#btnBuscarEst").on("click", function () {
        $("#gridEstablecimientoPrincipal").empty();
        loadDataPorEstablecimientoPrincipal();
    });

    $("#btnLimpiarEst").on("click", function () {
        $("#gridEstablecimientoPrincipal").empty();
        limpiarPrincipal();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../Establecimiento/Create";
    });

    $('#txtEstablecimiento').keypress(function (e) {
        if (e.which == 13) {
            $("#gridEstablecimientoPrincipal").empty();
            loadDataPorEstablecimientoPrincipal();
        }
    });


    $("#btnEliminar").on("click", function () {

        var $tabs = $('#tabs').tabs();
        var active = $tabs.tabs('option', 'active');
        var values = [];

        var cantidad = ValidarEstablecimientosMarcados();
        if (cantidad == 1) {
            $(".k-grid-content tbody tr").each(function () {
                var $row = $(this);
                var checked = $row.find('.kendo-chk-est').attr('checked');
                if (checked == "checked") {
                    var codigoEst = $row.find('.kendo-chk-est').attr('value');
                    //alert(codigoEst);
                    values.push(codigoEst);
                    $("#hidCodigoEstRequeq").val(codigoEst);
                    ObtieneDatosRequerimiento();
                    //eliminar(codigoEst);// ya no deberia de hacer 
                    $("#mvSolicitudRequeEst").dialog("open");
                }
            });


            loadDataPorEstablecimientoPrincipal();
        } else if (cantidad == 0) {

            alert("Seleccione al menos un establecimiento.");
        } else {
            alert("Seleccione solo un establecimiento");
        }


    });

    loadEstadosMaestro("ddlEstado2");



    //Carga Inicial
    loadTipoDivision('ddlDivision', 'GEO', INEI);//lista diviones por tipo de division   
    $("#ddlDivision").on("change", function () {        
        var idDivision = $(this).val();
        //loadTipoDivision('ddlDivision', idDivision);//lista diviones por tipo de division
        loadSubTipoDivisiones(idDivision, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');
    });
    loadSubTipoDivisiones(INEI, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');


    $("#ddlSubTipo1").on("change", function () {
        var idDivision = $('#ddlDivision').val();
        var subtipo = $('#hidSubTipo2').val();
        var belog = $(this).val();
        loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo2', 0);
    });

    $("#ddlSubTipo2").on("change", function () {
        var idDivision = $('#ddlDivision').val();
        var subtipo = $('#hidSubTipo3').val();
        var belog = $(this).val();
        loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo3', 0);
    });

    $("#mvSolicitudRequeEst").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2_EST,
        height: K_HEIGHT_OBS2_EST,
        buttons: {
            "Grabar": RegistrarRequerimiento,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvSolicitudRequeEst").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });


    //solicitud para inactivar ESTABLECIMIENTO 
    loadTipoRquerimiento('ddltiporequerimiento', 3,2);


});



var reloadEvento = function (idSel) {
    $("#hidCodigoBPS").val(idSel);
    obtenerNombreSocio($("#hidCodigoBPS").val());
};

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
}

function editar(idSel) {
    document.location.href = '../Establecimiento/Create?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function limpiarPrincipal() {
    $("#hidCodigoBPS").val(0)
    $("#lbResponsable").html("Seleccione");
    $("#ddlTipoestablecimiento").val(0);
    $("#ddlSubtipoestablecimiento").val(0);
    $("#txtEstablecimiento").val("");
    $("#hidCodigoEST").val(0);
    $("#ddlDivisionTipo").val(0);
    $("#ddlDivision").val(0);
    //otros nuevos controles
    $('#ddlSubTipo1  option').remove();
    $('#ddlSubTipo1').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $('#ddlSubTipo2  option').remove();
    $('#ddlSubTipo2').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $('#ddlSubTipo3  option').remove();
    $('#ddlSubTipo3').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $("#hidCodigoEstRequeq").val("");
}


function eliminar(idEst) {
    var id = { idEstablecimiento: idEst };
    $.ajax({
        url: '../Establecimiento/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert("Estados actualizado correctamente.");
                //loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function loadDataPorEstablecimientoPrincipal() {
    if ($("#gridEstablecimientoPrincipal").data("kendoGrid") != undefined) {
        $("#gridEstablecimientoPrincipal").empty();
    }

    var data_sourcePorEstablecimientoPrincipal = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        transport: {
            read: {
                type: "POST",
                url: "../Establecimiento/Listar_Establecimiento_Principal",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        tipoEst: $("#ddlTipoestablecimiento").val(),
                        subTipoEst: $("#ddlSubtipoestablecimiento").val(),
                        IdEstablecimiento: $("#hidCodigoEST").val(),
                        nombre: $("#txtEstablecimiento").val(),
                        st: $("#ddlEstado2").val(),
                        bpsId: $("#hidCodigoBPS").val(),

                        division: $("#ddlDivision").val(),
                        subtipo1: $("#ddlSubTipo1").val(),
                        subtipo2: $("#ddlSubTipo2").val(),
                        subtipo3: $("#ddlSubTipo3").val(),
                    })
            }
        },
        schema: { data: "Establecimiento", total: 'TotalVirtual' }
    });


    var gridPorEstablecimientoPrincipal = $("#gridEstablecimientoPrincipal").kendoGrid({
        dataSource: data_sourcePorEstablecimientoPrincipal,
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
            {
                title: 'Eliminar', width: 15, template: "<input type='checkbox' id='chkSel' class='kendo-chk-est' name='chkSel' value='${EST_ID}'/>"
            },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${OWNER}</a>"
            },
            {
                field: "EST_ID", width: 20, title: "Id", template: "<a id='single_2' href=javascript:editar('${EST_ID}') style='color:gray !important;'>${EST_ID}</a>"
            },
            {
                hidden: true,
                field: "TAXN_NAME", width: 20, title: "Tipo", template: "<font color='green'><a id='single_2' href=javascript:editar('${EST_ID}') style='color:gray !important;'>${TAXN_NAME}</a>"
            },
            {
                hidden: true,
                field: "TAX_ID", width: 20, title: "Nro identificación", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${TAX_ID}</a>"
            },
            {
                field: "EST_NAME", width: 60, title: "Establecimiento", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${EST_NAME}</a>"
            },

            {
                field: "EST_TYPE", width: 55, title: "Tipo", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${EST_TYPE}</a>"
            },
            {
                field: "EST_SUBTYPE", width: 60, title: "Subtipo", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${EST_SUBTYPE}</a>"
            },

            {
                field: "ADDRESS", width: 80, title: "Dirección", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${ADDRESS}</a>"
            },
             {
                 field: "UBIGEO", width: 80, title: "UBIGEO", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${UBIGEO}</a>"
            },
            {
                field: "BPS_NAME", width: 70, title: "Socio de Negocio", template: "<font color='green'><a id='single_2'  href=javascript:editar('${EST_ID}') style='color:gray !important;'>${BPS_NAME}</a>"
            },
            {
                field: "EST_ID", width: 10, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerEstablecimientoVentanaNueva(${EST_ID});'  border='0' title='Ver Establecimiento En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>"
            },
           ]
    });

}


function VerEstablecimientoVentanaNueva(est_id) {

    window.open('../Establecimiento/Create?id=' + est_id, '_blank');
}

function ObtieneDatosRequerimiento() {

    var est = $("#hidCodigoEstRequeq").val();
    $("#txtAprobacionDesc").val("");
    $("#lblestid").html(est);
}

function RegistrarRequerimiento() {

    var EST_ID = $("#hidCodigoEstRequeq").val();
    var ID_REQ_TYPE = $("#ddltiporequerimiento").val();
    var RAZON = $("#txtAprobacionDesc").val();
    var ACTIVO = $("#ddltiporequerimiento").val() == 3 ? 0 : 1;
    var MONTO = 0;
    var FECHA = "";
    var INV_ID = 0;
    var LIC_ID = 0;
    var BPS_ID = 0;
    var BEC_ID = 0;
    var TIP_LIC_INACT = 0;
    $.ajax({
        data: { EST_ID: EST_ID, ID_REQ_TYPE: ID_REQ_TYPE, RAZON: RAZON, ACTIVO: ACTIVO, MONTO: MONTO, FECHA: FECHA, INV_ID: INV_ID, LIC_ID: LIC_ID, BPS_ID: BPS_ID, BEC_ID: BEC_ID, TipoInactivacion: TIP_LIC_INACT },
        url: '../AdministracionModuloRequerimientos/RegistraRequerimientoGral',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                $("#mvSolicitudRequeEst").dialog("close");
                //$("#ddltipoAprobacion").val(entidad.TIPO);

            } else {
                alert(dato.message);
            }
        }
    });

}



function ValidarEstablecimientosMarcados() {

    var values = 0;
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk-est').attr('checked');
        if (checked == "checked") {
            var codigoEst = $row.find('.kendo-chk-est').attr('value');
            values++;
        }
    });
    return values;
}

