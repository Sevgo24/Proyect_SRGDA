/*INICIO CONSTANTES*/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/
$(function () {
    $("#tabs").tabs();

    /*Inicializador de PopUp de Detalle de Gastos*/
    $("#hidAccionMvDet").val("0");

    $('#txtFechaSolicitud').kendoDatePicker({ format: "dd/MM/yyyy" });

    var id = GetQueryStringParams("set");

    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTituloPerfil").html("Requerimiento - Dinero / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoDocumento("ddlTipoDocumento", 0);

    } else {
        $("#divTituloPerfil").html("Requerimiento - Dinero / Actualizar");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(id);
        ObtenerDatos(id);

    }

    $("#mvDetalle").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        buttons: {
            //"Agregar": grabar,
            "Cancelar": function () { $("#mvDetalle").dialog("close"); $('#txtMonto').css({ 'border': '1px solid gray' }); }
        },
        modal: true
    });

    /*Inicio de Carga inicial de tabs*/
    loadDataDetalle();
    /*FIN de Carga inicial de tabs*/

    $("#btnGrabar").on("click", function () {
        //grabar();
    });

    $("#btnRegresar").on("click", function () {
        location.href = "../RequerimientoDinero/Index";
    });

    $("#tabs-1").on("click", function () {
        loadDataDetalle();
    });

    $("#addDetalle").on("click", function () {
        $("#mvDetalle").dialog({});
        $("#mvDetalle").dialog("open");
        limpiar();
    });

    $("#ddlTipoGasto").on("change", function () {
        $('#ddlGrupoGasto option').remove();
        var tipo = $("#ddlTipoGasto").val();
        loadGrupoGasto(tipo);
    });

    $("#ddlGrupoGasto").on("change", function () {
        $('#ddlGasto option').remove();
        var tipo = $("#ddlGrupoGasto").val();
        loadGasto(tipo);
    });

    loadTipoDocumento('ddlTipoDocumento', 0);
    loadTipoGasto();
});

function loadDataDetalle() {
    loadDataGridTmp('../RequerimientoDinero/ListarDetalle', "#gridDetalle");

    //$("#gridDetalle").kendoGrid({
    //    dataSource: {
    //        type: "json",
    //        serverPaging: true,
    //        pageSize: 5,
    //        transport: {
    //            read: {
    //                url: "../DetalleGasto/Listar_PageJson_DetalleGasto", dataType: "json", data: { id: id }
    //            }
    //        },
    //        schema: { data: "DetalleGasto", total: 'TotalVirtual' }
    //    },
    //    groupable: false,
    //    sortable: true,
    //    pageable: true,
    //    selectable: true,
    //    columns:
    //       [
    //        { field: "RowNumber", width: 3, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${RowNumber}</a>" },
    //        { field: "EXP_DESC", width: 24, title: "<font size=2px>GASTO</font>", template: "<a id='single_2'  href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_DESC}</a>" },
    //        { field: "EXP_VAL_PRE", width: 10, title: "<font size=2px>S/.SOLICITADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_PRE}</a></font>" },
    //        { field: "EXP_VAL_APR", width: 8, title: "<font size=2px>S/.APROBADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_APR}</a></font>" },
    //        { field: "EXP_VAL_CON", width: 8, title: "<font size=2px>S/.GASTADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_CON}</a></font>" },
    //        { field: "ESTADO", width: 13, title: "<font size=2px>ESTADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ESTADO}</a></font>" },
    //        { title: '', width: 4, template: "<img src='../Images/iconos/delete.png' value='${RowNumber}' />" }
    //       ]
    //});
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadTipoGasto() {

    $.ajax({
        url: "../General/ListarTipoGasto",
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlTipoGasto').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                var tipo = $("#ddlTipoGasto").val();
                loadGrupoGasto(tipo);
            }
            else {
                alert(dato.message);
            }
        }
    });
}

function loadGrupoGasto(tipo) {

    $('#ddlGrupoGasto').append($("<option />", { value: 0, text: '--Seleccione--' }));
    $.ajax({
        url: "../General/ListarGrupoGasto",
        data: { tipo: tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlGrupoGasto').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                var tipo = $("#ddlGrupoGasto").val();
                loadGasto(tipo);
            }
            else {
                $("#ddlGrupoGasto").attr("disabled", "disabled");
                alert(dato.message);
            }
        }
    });
}

function loadGasto(tipo) {
    $('#ddlGasto').append($("<option />", { value: 0, text: '--Seleccione--' }));
    $.ajax({
        url: "../General/ListarDefinicionGasto",
        data: { tipo: tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlGasto').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                $("#ddlGasto").removeAttr("disabled");
                var tipo = $("#ddlGasto").val();
            }
            else {
                $("#ddlGasto").attr("disabled", "disabled");
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#ddlGrupoGasto").val(0);
    $("#ddlGasto").val(0);
    $("#ddlGasto").attr("disabled", "disabled");
    $("#txtMonto").val("");
}

function editar(idSel) {

    $("#hidOpcionEdit").val(1);
    limpiar();
    $.ajax({
        url: "../RequerimientoDinero/Obtiene",
        type: 'POST',
        data: { id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#ddlTipoDocumento").val(tipo.EXP_TYPE);
                    $("#txtDesCorta").val(tipo.EXPG_ID);
                    $("#txtPDescripcion").val(tipo.EXPG_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

    $("#mvGrupoGasto").dialog({ title: "Actualizar Grupo de Gasto" });
    $("#mvGrupoGasto").dialog("open");
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../RequerimientoDinero/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    var d1 = $("#txtFechaSolicitud").data("kendoDatePicker");
                    var valFechaIni = formatJSONDate(req.MNR_DATE);
                    var start = $('#txtFechaSolicitud').kendoDatePicker({
                        value: valFechaIni,
                        change: startChange,
                        format: "dd/MM/yyyy"
                    }).data("kendoDatePicker");
                    //$("#ddlTipoDocumento").val(req.TAXT_ID);
                    //alert(tipo);
                    loadTipoDocumento("ddlTipoDocumento", req.TAXT_ID);
                    $("#ddlTipoDocumento").attr("disabled", "disabled");
                    $("#txtNroDocumento").val(req.TAX_ID);
                    $("#txtNroDocumento").attr("disabled", "disabled");
                    $("#txtNombre").val(req.BPS_NAME);
                    $("#txtNombre").attr("disabled", "disabled");
                    $("#txtRequerimiento").val(req.MNR_DESC);
                    $("#estado").html(req.ESTADO);
                    $("#divsolicitado").html(req.MNR_VALUE_PRE);
                    $("#divaprobado").html(req.MNR_VALUE_APR);
                    $("#divgastado").html(req.MNR_VALUE_CON);

                    //carga la grilla del detalle
                    //$("#gridDetalle").kendoGrid({
                    //    dataSource: {
                    //        type: "json",
                    //        serverPaging: true,
                    //        pageSize: 5,
                    //        transport: {
                    //            read: {
                    //                url: "../DetalleGasto/Listar_PageJson_DetalleGasto", dataType: "json", data: { id: id }
                    //            }
                    //        },
                    //        schema: { data: "DetalleGasto", total: 'TotalVirtual' }
                    //    },
                    //    groupable: false,
                    //    sortable: true,
                    //    pageable: true,
                    //    selectable: true,
                    //    columns:
                    //       [
                    //        { field: "RowNumber", width: 3, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${RowNumber}</a>" },
                    //        { field: "EXP_DESC", width: 24, title: "<font size=2px>GASTO</font>", template: "<a id='single_2'  href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_DESC}</a>" },
                    //        { field: "EXP_VAL_PRE", width: 10, title: "<font size=2px>S/.SOLICITADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_PRE}</a></font>" },
                    //        { field: "EXP_VAL_APR", width: 8, title: "<font size=2px>S/.APROBADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_APR}</a></font>" },
                    //        { field: "EXP_VAL_CON", width: 8, title: "<font size=2px>S/.GASTADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${EXP_VAL_CON}</a></font>" },
                    //        { field: "ESTADO", width: 13, title: "<font size=2px>ESTADO</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${RowNumber}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ESTADO}</a></font>" },
                    //        { title: '', width: 4, template: "<img src='../Images/iconos/delete.png' value='${RowNumber}' />" }
                    //       ]
                    //});
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function startChange() {
    var startDate = start.value(),
        endDate = end.value();

    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate());
        end.min(startDate);
    } else if (endDate) {
        start.max(new Date(endDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
}
function endChange() {
    var endDate = end.value(),
        startDate = start.value();
    if (endDate) {
        endDate = new Date(endDate);
        endDate.setDate(endDate.getDate());
        start.max(endDate);
    } else if (startDate) {
        end.min(new Date(startDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
}



