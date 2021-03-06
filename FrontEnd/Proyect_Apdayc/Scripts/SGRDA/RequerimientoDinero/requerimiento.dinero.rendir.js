/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 165;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_ESTADO = { Abierto: 1, Pendiente: 2, Atendido: 3, Entregado: 4, Rendido: 5, Anulado: 6 };
var K_MSJ_MONTO = "El monto de aporabación excede el solicitado.";
/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    $("#hidAccionMvDet").val("0");
    $("#lbResponsable").hide();
    $("#hidResponsable").val(0);
    $("#txtNombre").prop('disabled', true);
    $("#spnSolicitado").val('0.00');
    $("#spnAprobado").val('0.00');
    $("#spnGastado").val('0.00');
    $('#txtRequerimiento').attr('readonly', true);
    $("#tdAdd").hide();
    //--------------------------------------------------------------------------------- 
    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Requerimiento Dinero - Rendir");
        $("#hidId").val(0);
        $("#hidOpcionEdit").val(0);
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Requerimiento Dinero - Rendir");
        $("#hidId").val(id);
        $("#hidOpcionEdit").val(1);
        $("#btnBuscarBS").hide();
        ObtenerDatos(id);
    }

    //--------------------------POPUP -----------------------------------------------
    $("#mvDetalle").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        overflow: false,
        buttons: {
            "Agregar": addDetalleRendir,
            "Cancelar": function () { $("#mvDetalle").dialog("close"); }
        },
        modal: true
    });
    $("#mvDetalle").css({ overflow: 'hidden' })
    $(".addDetalle").on("click", function () {
        $("#hidAccionMvDet").val("0");
        $("#hidEdicionDet").val(0);
        $("#txtGastoDes").css('border', '1px solid gray');
        $("#txtMonto").css('border', '1px solid gray');
        $("#txtMonto").val('');
        $("#txtGastoDes").val('');
        $("#mvDetalle").dialog("open");
    });
    $("#tabs").tabs();

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnRegresar").on("click", function () {
        location.href = "../RequerimientoDinero/ConsultaRendir";
    }).button();

    $("#btnRendir").on("click", function () {
        grabar(K_ESTADO.Rendido);
    }).button();

    $('#txtMonto').keypress(function (event) {
        if (event.which != 8) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }

            var text = $(this).val();
            if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2)) {
                event.preventDefault();
            }
        }
    });

    //-------------------------- CARGAR DETALLE --------------------------------------------
    var estado = $("#hidEstadoReq").val();
    loadDataDetalle(estado);
    loadDataDetalle(estado);
    loadDataDetalle(estado);
    loadDataDetalle(estado);
    //--------------------------BUSQUEDA GENERAL  -----------------------------------------------
    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento"
    });


});

//-------------------------------------  FUNCIONES  ------------------------------------------------
function grabar(estado) {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        insertar(estado);
    }
};

function insertar(estado) {
    var id = 0;
    var idBps = $("#hidResponsable").val();
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();
    var subTotSolicitado = parseFloat($("#spnSolicitado").html());
    var subTotAprobado = parseFloat($("#spnAprobado").html());
    var subTotGastado = parseFloat($("#spnGastado").html());

    alert(idBps);
    var req = {
        MNR_ID: id,
        BPS_ID: idBps,
        STT_ID: estado,
        MNR_DESC: $("#txtRequerimiento").val(),
        MNR_DATE: $("#txtFechaSolicitud").val(),
        MNR_VALUE_PRE: subTotSolicitado,
        MNR_VALUE_APR: subTotAprobado,
        MNR_VALUE_CON: subTotGastado
    };

    $.ajax({
        url: '../RequerimientoDinero/InsertarRendir',
        data: req,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../RequerimientoDinero/ConsultaRendir';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiar() {
    $("#txtGastoDes").val('');
    $("#txtMonto").val("");
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../RequerimientoDinero/ObtenerRendir",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    $("#txtNombre").val(req.BPS_NAME);
                    $("#hidResponsable").val(req.BPS_ID);
                    $("#txtRequerimiento").val(req.MNR_DESC);
                    $("#spnEstado").html(req.ESTADO);
                    $("#spnSolicitado").html(req.MNR_VALUE_PRE.toFixed(2));
                    $("#spnAprobado").html(req.MNR_VALUE_APR.toFixed(2));
                    $("#spnGastado").html(req.MNR_VALUE_CON.toFixed(2));
                    $("#txtFechaSolicitud").val(req.FECHA);
                    $("#hidEstadoReq").val(req.STT_ID);
                    
                    if (req.STT_ID == K_ESTADO.Rendido) 
                        $("#btnRendir").hide();

                    var estado = $("#hidEstadoReq").val();
                    loadDataDetalle(estado);
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - DOCUMENTO -------------------------------------  
function loadDataDetalle(estado) {
    loadDataGridTmp('../RequerimientoDinero/ListarDetalleRendir', "#gridDetalle", estado);
}

function addDetalleRendir() {
    var estadoValidacion = validarDetalle();

    //var montoSolicitado = parseFloat($("#hidMonto").val());
    //var montoAprobado = parseFloat($("#txtMonto").val());
    //var estadoMonto = validarMonto(montoSolicitado, montoAprobado);

    if (estadoValidacion == true) {
        var IdAdd = 0;
        if ($("#hidAccionMvDet").val() === "1") IdAdd = $("#hidEdicionDet").val();

        var detalle = {
            Id: IdAdd,
            ReqGasto_Id: $("#hidId").val(),
            Monto_Gastado: $("#txtMonto").val(),
            Gasto_Des: $("#txtGastoDes").val()
        };

        $.ajax({
            url: '../RequerimientoDinero/AddDetalleRendir',
            type: 'POST',
            data: detalle,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    var estado = $("#hidEstadoReq").val();
                    loadDataDetalle(estado);
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#hidMonto").val(0);
        $("#avisoMonto").text('');
        $("#mvDetalle").dialog("close");
    } 
}

function delAddDetalleRendir(idDel) {
    $.ajax({
        url: '../RequerimientoDinero/DellAddDetalleRendir',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var estado = $("#hidEstadoReq").val();
                loadDataDetalle(estado);
            }
        }
    });
    return false;
}

function updAddDetalleRendir(idUpd) {

    $.ajax({
        url: '../RequerimientoDinero/ObtieneDetalleTmpRendir',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDet").val("1");
                    $("#hidEdicionDet").val(doc.Id);
                    $("#txtGastoDes").val(doc.Gasto_Des);
                    $("#txtMonto").val(doc.Monto_Gastado.toFixed(2));
                    $("#hidMonto").val(doc.Monto_Solicitado);
                    $("#mvDetalle").dialog("open");
                } else {
                    alert("No se pudo obtener el detalle del gasto para editar.");
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

//-- VALIDACIONES
function validarMonto(solicitado, aprobado) {
    var estado = false;
    if (aprobado <= solicitado)
        estado = true;
    return estado;
}

function validarDetalle() {
    var estado = true;
    if ($("#txtMonto").val().trim() == '') {
        $("#txtMonto").css('border', '1px solid red');
        estado = false;
    }
    else
        $("#txtMonto").css('border', '1px solid gray');
    return estado;
}

function ddlValidar(control, estado) {
    if ($("#" + control).val() == 0) {
        $("#" + control).css('border', '1px solid red');
        estado = false;
    } else {
        $("#" + control).css('border', '1px solid gray');
    }
    return estado;
}

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $("#lbResponsable").html(dato.valor);
                $("#txtNombre").val(dato.valor);
            }
        }

    });

};

function loadDataGridTmp(Controller, idGrilla, estado) {
    $.ajax({
        type: 'POST', url: Controller, data: { estadoReq: estado }, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
            calcularMontoAprobado();
        }
    });
}

function calcularMontoAprobado() {

    var totalSolicitado = 0;
    $('#tblUsuario tr').each(function () {
        var solicitado = parseFloat($(this).find("td").eq(7).html());
        if (!isNaN(solicitado)) {
            totalSolicitado = parseFloat(totalSolicitado) + parseFloat(solicitado);
            $("#spnGastado").html(totalSolicitado.toFixed(2));
        }
    });
}

