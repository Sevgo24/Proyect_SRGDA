/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 200;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_ESTADO = { Abierto: 1, Pendiente: 2, Atendido:3, Entregado:4,Rendido:5,Anulado:6 };
/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    $("#hidAccionMvDet").val("0");
    $("#lbResponsable").hide();
    $('#txtFechaSolicitud').kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#hidResponsable").val(0);
    $("#txtNombre").prop('disabled', true);
    $("#spnSolicitado").val('0.00');
    $("#spnAprobado").val('0.00');
    $("#spnGastado").val('0.00');
    //--------------------------------------------------------------------------------- 
    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Requerimiento Dinero - Nuevo");
        $("#hidId").val(0);
        $("#hidOpcionEdit").val(0);
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Requerimiento Dinero - Modificar");
        $("#hidId").val(id);
        $("#hidOpcionEdit").val(1);
        $("#btnBuscarBS").hide();
        ObtenerDatos(id);
    }

    $("#mvDetalle").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        overflow: false,
        buttons: {
            "Agregar": addDetalle,
            "Cancelar": function () { $("#mvDetalle").dialog("close"); }
        },
        modal: true
    });
    $("#mvDetalle").css({ overflow: 'hidden' })
    $(".addDetalle").on("click", function () {
        $("#hidAccionMvDet").val("0");
        $("#hidEdicionDet").val(0);
        loadTipoGasto("ddlTipo", 0);
        $('#ddlGrupo option').remove();
        $("#ddlGrupo").prepend("<option selected='selected' value='0'>SELECCIONAR</option>");
        $('#ddlGasto option').remove();
        $("#ddlGasto").prepend("<option selected='selected' value='0'>SELECCIONAR</option>");
        $("#ddlTipo").css('border', '1px solid gray');
        $("#ddlGrupo").css('border', '1px solid gray');
        $("#ddlGasto").css('border', '1px solid gray');
        $("#txtMonto").css('border', '1px solid gray');
        $("#txtMonto").val('');
        $("#mvDetalle").dialog("open");
    });
    $("#tabs").tabs();
    //-------------------------- CARGA - DROPDOWNLIST -TABS -------------------------
    $("#ddlTipo").change(function () {
        var tipo = $("#ddlTipo").val();
        loadGrupoGasto('ddlGrupo', tipo);
    });

    $("#ddlGrupo").change(function () {
        var grupo = $("#ddlGrupo").val();
        loadGasto('ddlGasto', grupo);
    });

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnRegresar").on("click", function () {
        location.href = "../RequerimientoDinero/ConsultaSolicitud";
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar(K_ESTADO.Abierto);
    }).button();

    $("#btnAprobar").on("click", function () {
        grabar(K_ESTADO.Pendiente);
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

    //$("#txtMonto").blur(function () {
    //    var price = $("#foo").val();
    //    var validatePrice = function (price) {
    //        return /^(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(price);
    //    }
    //    //alert(validatePrice(price)); // False
    //});

    //--------------------------BUSQUEDA GENERAL  -----------------------------------------------
    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento"
    });

    //-------------------------- CARGAR DETALLE --------------------------------------------
    loadDataDetalle();
   
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
    
    var req = {
        MNR_ID: id,
        BPS_ID: idBps,
        STT_ID: estado,
        MNR_DESC: $("#txtRequerimiento").val(),
        MNR_DATE: $("#txtFechaSolicitud").val(),
        MNR_VALUE_PRE: subTotSolicitado
    };

    $.ajax({
        url: '../RequerimientoDinero/Insertar',
        data: req,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../RequerimientoDinero/ConsultaSolicitud';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiar() {
    $("#ddlGrupoGasto").val(0);
    $("#ddlGasto").val(0);
    $("#ddlGasto").attr("disabled", "disabled");
    $("#txtMonto").val("");
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
                    var valFecha = formatJSONDate(req.MNR_DATE);
                    d1.value(valFecha);

                    $("#txtNombre").val(req.BPS_NAME);
                    $("#hidResponsable").val(req.BPS_ID);
                    $("#txtRequerimiento").val(req.MNR_DESC);
                    $("#spnEstado").html(req.ESTADO);
                    $("#spnSolicitado").html(req.MNR_VALUE_PRE.toFixed(2));
                    $("#spnAprobado").html(req.MNR_VALUE_APR.toFixed(2));
                    $("#spnGastado").html(req.MNR_VALUE_CON.toFixed(2));
                    loadDataDetalle();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - DOCUMENTO -------------------------------------  
function loadDataDetalle() {
    loadDataGridTmp('../RequerimientoDinero/ListarDetalle', "#gridDetalle");
}

function addDetalle() {
    var estadoValidacion = validarDetalle();
    if (estadoValidacion == true) {
        var IdAdd = 0;
        if ($("#hidAccionMvDet").val() === "1") IdAdd = $("#hidEdicionDet").val();

        var detalle = {
            Id: IdAdd,
            ReqGasto_Id: $("#hidId").val(),
            Tipo_Id: $("#ddlTipo").val(),
            Grupo_Id: $("#ddlGrupo").val(),
            Gasto_Id: $("#ddlGasto").val(),
            Gasto_Des: $("#ddlGasto option:selected").text(),
            Monto_Solicitado: $("#txtMonto").val()
        };


        $.ajax({
            url: '../RequerimientoDinero/AddDetalle',
            type: 'POST',
            data: detalle,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDetalle();                   
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvDetalle").dialog("close");
    }
    
}

function delAddDetalle(idDel) {
    $.ajax({
        url: '../RequerimientoDinero/DellAddDetalle',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDetalle();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddDetalle(idUpd) {

    $.ajax({
        url: '../RequerimientoDinero/ObtieneDetalleTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDet").val("1");
                    $("#hidEdicionDet").val(doc.Id);
                    loadTipoGasto("ddlTipo", doc.Tipo_Id);
                    loadGrupoGasto('ddlGrupo', doc.Tipo_Id, doc.Grupo_Id);
                    loadGasto('ddlGasto', doc.Grupo_Id, doc.Gasto_Id);
                    $("#txtMonto").val(doc.Monto_Solicitado.toFixed(2));
                    $("#mvDetalle").dialog("open");
                } else {
                    alert("No se pudo obtener el detalle del gasto para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-- VALIDACIONES
function validarDetalle() {
    var estado = true;
    estado = ddlValidar('ddlTipo', estado);
    estado = ddlValidar('ddlGrupo', estado);
    estado = ddlValidar('ddlGasto', estado);
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

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
            calcularMontoSolicitado();
        }
    });
}

function calcularMontoSolicitado() {

    var totalSolicitado = 0;
    $('#tblUsuario tr').each(function () {
        var solicitado = parseFloat($(this).find("td").eq(3).html());
        if(!isNaN(solicitado)) {
            totalSolicitado = parseFloat(totalSolicitado) + parseFloat(solicitado);
            $("#spnSolicitado").html(totalSolicitado.toFixed(2));
        }
    });
}

