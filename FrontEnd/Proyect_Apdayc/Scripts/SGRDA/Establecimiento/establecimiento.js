/*INICIO CONSTANTES*/
var K_ID_DIR = "divDir";
var INEI = 1
var idsocio = $("#txtRazonSocial").val();

var K_WIDTH_EST= 1200;
var K_HEIGHT_EST = 400;

$(function () {
    /*Pestaña Dirección*/
    //initFormDireccionNMV({
    //    parentControl: K_ID_DIR
    //});
    $("#hidCodigoBPS").val(0);
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    //mvInitEstablecimientoSocio({ container: "ContenedormvEstablecimientoSocio", idButtonToSearch: "btnBuscarEstablecimientoSocio", idDivMV: "mvEstablecimientoSocio"});

    $("#mvAgruparLocal").dialog({ autoOpen: false, width: K_WIDTH_EST, height: K_HEIGHT_EST, buttons: { "Agrupar": addAgrupar, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });

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

    $("#btnAgruparLocales").on("click", function () {
        limpiarRuc();
        $("#mvAgruparLocal").dialog("open");
        var ruc = $("#txtRuc").val();
        BuscarSocioxRuc(ruc);
    });

    $("#btnBuscarEstSocio").on("click", function () {
        $("#gridEstOriginal").html('');
        $("#gridEstFinal").html('');
        var ruc = $("#txtRuc").val();
        BuscarSocioxRuc(ruc);
    });

    $("#btnElegirEst").on("click", function () {
        obtenerEstablecimientoSeleccionado();
    });

    $("#btnNoelegirEst").on("click", function () {
        Confirmar('Está seguro de quitar el establecimiento ?',
            function () { RegresarEstablecimientoSeleccionado(); },
            function () { },
            'Confirmar'
        );
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../EstablecimientoAdministracion/Nuevo";
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


        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk-est').attr('checked');
            if (checked == "checked") {
                var codigoEst = $row.find('.kendo-chk-est').attr('value');
                alert(codigoEst);
                values.push(codigoEst);
                eliminar(codigoEst);
            }
        });
        if (values.length == 0) {
            alert("Seleccione para eliminar.");
        }
        loadDataPorEstablecimientoPrincipal();
    

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

});

function limpiarRuc() {
    $("#txtRuc").val("");
}

function addAgrupar() {
Confirmar('Las Licencias de los Establecimientos dados de baja' + '\n' + 'serán trasladados al establecimiento activo, desea continuar?',
    function () {
        var ReglaValor = [];
        var contador = 0;

        $('#tblEstablecimientoA tr').each(function () {
            var IdNro = $(this).find(".IDEstOri").html();

            if (!isNaN(IdNro) && IdNro != null) {
                var est_id = $(this).find(".IDEstOri").html();
                if ($('#chkEstOrigen' + est_id).is(':checked')) {
                    ReglaValor[contador] = {
                        EST_ID: est_id
                    };
                    contador += 1;
                }
            }
        });

        var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
        if (contador > 0) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '../EstablecimientoAdministracion/AgruparEstablecimiento',
                data: ReglaValor,
                success: function (response) {
                    var dato = response;
                    if (dato.result == 1) {
                        LoadListarEstB();
                        loadDataEstB();
                        alert(dato.message);
                    } else if (dato.result == 0) {
                        $("#gridEstFinal").html('');
                        alert(dato.message);
                    }
                }
            });
        } else {
            alert("Debe selecionar antes de continuar.");
        }
    },
    function () { },
    'Confirmar'
    );
};

function BuscarSocioxRuc(ruc) {
    $.ajax({
        url: '../EstablecimientoAdministracion/ConsultaRucSocio',
        type: 'POST',
        data: {
            ruc: ruc
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataEstA();
                loadDataEstB();
            } else if (dato.result == 0) {
                $("#gridEstOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function loadDataEstA() {
    loadDataGridTmp('../EstablecimientoAdministracion/ListarEstablecimientoA', "#gridEstOriginal");
}

function loadDataEstB() {
    loadDataGridTmp('../EstablecimientoAdministracion/ListarEstablecimientoB', "#gridEstFinal");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', data: {}, url: Controller,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                $(idGrilla).html('');
                alert(dato.message);
            }
        }
    });
}

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
    document.location.href = '../EstablecimientoAdministracion/Nuevo?id=' + idSel;
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
}


function eliminar(idEst) {
    var id = { idEstablecimiento: idEst };
    $.ajax({
        url: '../EstablecimientoAdministracion/Eliminar',
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
                url: "../EstablecimientoAdministracion/Listar_Establecimiento_Principal",
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

    window.open('../EstablecimientoAdministracion/Nuevo?id=' + est_id, '_blank');
}


function BuscarEstablecimientoSocioEmpresarial(ruc) {
    $.ajax({
        url: '../EstablecimientoAdministracion/ConsultaEstablecimientoSocioEmpresarial',
        type: 'POST',
        data: {
            ruc: ruc
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                LoadListarEstablecimientoSocioEmpresarialSeg();
                ConsultarEstSocEmprGrab(soc, LicMas);
            } else if (dato.result == 0) {
                $("#gridEstablecimientoOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function obtenerEstablecimientoSeleccionado() {
    var ReglaValor = [];
    var contador = 0;

    $('#tblEstablecimientoA tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNro) && IdNro != null) {
            var idEst = $(this).find(".IDEstOri").html();
            var NomEstOri = $(this).find(".NomEstOri").html();
            var NomUbgOri = $(this).find(".NomUbgOri").html();

            if ($('#chkEstOrigen' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idEst,
                    Nombre: NomEstOri,
                    Ubigeo: NomUbgOri
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../EstablecimientoAdministracion/EstArmaTemporalesOriginal',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    LoadListarEstB();
                    loadDataEstA();
                } else if (dato.result == 0) {
                    $("#gridEstFinal").html('');
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
    }
}

function LoadListarEstB() {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../EstablecimientoAdministracion/ListarEstablecimientoB',
        data: {},
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#gridEstFinal").html(dato.message);
                //Lista los socios en la segunda grilla
                loadDataEstB();
            } else if (dato.result == 0) {
                $("#gridEstFinal").html('');
                $("#gridEstOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function RegresarEstablecimientoSeleccionado() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblEstablecimientoB tr').each(function () {
        var IdNro = $(this).find(".IDEstDes").html();

        if (!isNaN(IdNro) && IdNro != null) {
            var idEst = $(this).find(".IDEstDes").html();
            var NomEstDes = $(this).find(".NomEstDes").html();
            var NomUbgDes = $(this).find(".NomUbgDes").html();

            if ($('#chkEstDes' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idEst,
                    Nombre: NomEstDes,
                    Ubigeo: NomUbgDes
                };
                contador += 1;

            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../EstablecimientoAdministracion/EstArmaTemporalesDestino',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataEstB();
                    loadDataEstA();
                } else if (dato.result == 0) {
                    $("#gridEstFinal").html('');
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
        //Vuelve a listar 
        loadDataEstB();
    }
}

function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 150,
        title: 'Agrupar',
        minHeight: 70,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}