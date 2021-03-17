/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 155;
var K_WIDTH_DES = 500;
var K_HEIGHT_DES = 210;
var K_WIDTH_FOR = 500;
var K_HEIGHT_FOR = 300;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_CARACTERISTICA_MENSAJE = "Se permite como maximo 5 caracteristicas.";
var K_CARACTERISTICA_ELIMIAR_MENSAJE = "La caracteristica sera eliminada con todos los valores. ¿Desea eliminar?";
var K_MENSAJE_NUM_ELEMENTO = "Se debe ingresar como minimo 1 elemento.";
var K_DESCRIPCION_VALIDACION_MENSAJE = "La descripción ya existe, ingrese uno nuevo."
var K_PERIODOCIDAD_VALIDACION = "Existe una tarifa con la misma modalidad y periodocidad."
/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');

    //--------------------------------------------------------------------------------- 
    $("#txtDescripcion").focus();
    $("#txtFechaCreacion").hide();
    $("#numR").hide();
    $("#hidModalidad").val('');
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitBuscarTarifa({ container: "ContenedormvBuscarTarifa", idButtonToSearch: "btnBuscarTarifa", idDivMV: "mvBuscarTarifa", event: "reloadEventoTarifa", idLabelToSearch: "lbTarifa" });

    var id = (GetQueryStringParams("id"));
    $('#txtFecha').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaCreacion').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaVencimiento').kendoDatePicker({ format: "dd/MM/yyyy" });
    limpiar();
    $('#txtDecimalesRegla').on("keypress", function (e) { return solonumeros(e); });
    $('#txtDecimalesMinimo').on("keypress", function (e) { return solonumeros(e); });
    $("#imgCaracteristicaRemove").remove();
    loadTarifaTipos('ddlTipo', '0');
    loadFuncion('ddlElemento', 0);
    ObtenerVUM();
    $("#hidEdicionDes").val('0');
    $("#hidEdicionObs").val('0');    
    //--------------------------------------------------------------------------------- 
    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Tarifa Mantenimiento – Nuevo");
        $("#hidId").val(0);
        $("#hidIdOrigen").val(0);
        $("#hidOpcionEdit").val(0);
        $("#txtId").val(0);
        $("#trId").hide();
        loadTipoMoneda('ddlMoneda', 0);
        loadPeriodocidad('ddlPeriodocidad', 0);
        loadFormatoFormula('ddlFormatoRegla', 0);
        loadFormatoFormula('ddlFormatoMinimo', 0);
        loadCuentaContable('ddlCuentaContable', 0);
        $("#txtDecimalesRegla").prop("disabled", true);
        $("#txtDecimalesMinimo").prop("disabled", true);
        loadDataRegla();
        loadDataDescuento();
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Tarifa Mantenimiento - Modificar");
        $("#hidId").val(id);
        $("#hidOpcionEdit").val(1);
        $("#txtId").val(id);
        $("#trId").show();
        ObtenerDatos(id);
    }

    //--------------------------POPUP - REGLA -----------------------------------------------
    $("#mvRegla").dialog({
        autoOpen: false, width: K_WIDTH, height: K_HEIGHT,
        buttons: {
            "Agregar": addReglaAsoc,
            "Cancelar": function () {
                $("#mvRegla").dialog("close");
                $("#hidAccionMvObs").val("0");
                $("#hidEdicionObs").val("0");
            }
        }, modal: true
    });
    $(".addRegla").on("click", function () {
        var cantidadRegistros = parseInt($('#numVariable').val());
        if (cantidadRegistros < 5) {
            $("html, body").animate({ scrollTop: $(window).height() }, 600);
            loadTarifaTipos('ddlTipo', '0');
            loadFuncion('ddlElemento', 0);
            $('#mvRegla').css('overflow', 'hidden');
            $("#mvRegla").dialog("open");
        } else {
            alert(K_CARACTERISTICA_MENSAJE);
        }
    });

    //--------------------------POPUP - DESCUENTOS -----------------------------------------------
    $("#mvDescuento").dialog({
        autoOpen: false, width: K_WIDTH_DES, height: K_HEIGHT_DES,
        buttons: {
            "Agregar": addDescuento,
            "Cancelar": function () {
                $("#mvDescuento").dialog("close");
                $("#hidAccionMvDes").val("0");
                $("#hidEdicionDes").val("0");
            }
        }, modal: true
    });
    $(".addDescuento").on("click", function () {
        $("#txtFormato").val('');
        $("#txtValor").val('');
        loadTipoDescuento('ddlTipoDescuento', '0');
        loadDescuento('ddlDescuento', 0, 0);
        $('#mvDescuento').css('overflow', 'hidden');
        $("#mvDescuento").dialog("open");
    });

    $("#ddlTipoDescuento").change(function () {
        eliminaropcionEspecial();//david
        var idtipo = $("#ddlTipoDescuento").val();
        loadDescuento('ddlDescuento', idtipo, 0);
        if (idtipo == 0) {
            $("#txtFormato").val('');
            $("#txtValor").val('');
        } else {
            $('#ddlTipoDescuento').css({ 'border': '1px solid gray' });
        }
    });

    $("#ddlDescuento").change(function () {
        var idDesc = $("#ddlDescuento").val();
        ObtenerDatosDescuento(idDesc);
        if (idDesc == 0) {
            $("#txtFormato").val('');
            $("#txtValor").val('');
        } else {
            $('#ddlDescuento').css({ 'border': '1px solid gray' });
        }
    });

    //Formula
    $("#mvFormula").dialog({
        autoOpen: false, width: K_WIDTH_FOR, height: K_HEIGHT_FOR,
        buttons: {
            "Generar Formula": generarFormula,
            "Cancelar": function () {
                $("#mvFormula").dialog("close");
            }
        }, modal: true
    });
    $("#tabs").tabs();
    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnRegresar").on("click", function () {
        location.href = "../Tarifa/";
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#ddlTipo").change(function () {
        var idtipo = $("#ddlTipo").val();
        if (idtipo == 'R')
            loadRegla('ddlElemento', 0);
        else
            loadFuncion('ddlElemento', 0);
    });

    $("#ddlFormatoRegla").change(function () {
        $("#txtDecimalesRegla").css('border', '1px solid gray');
        var codigo = $("#ddlFormatoRegla").val();
        if (codigo == 'N') {
            $("#txtDecimalesRegla").prop("disabled", false);
            $("#txtDecimalesRegla").addClass("requerido");
        }
        else if (codigo == 'P') {
            $("#txtDecimalesRegla").prop("disabled", true);
            $("#txtDecimalesRegla").val("");
            $("#txtDecimalesRegla").removeClass("requerido");
        }
    });

    $("#ddlFormatoMinimo").change(function () {
        $("#txtDecimalesMinimo").css('border', '1px solid gray');
        var codigo = $("#ddlFormatoMinimo").val();
        if (codigo == 'N') {
            $("#txtDecimalesMinimo").prop("disabled", false);
            $("#txtDecimalesMinimo").addClass("requerido");

        }
        else if (codigo == 'P') {
            $("#txtDecimalesMinimo").prop("disabled", true);
            $("#txtDecimalesMinimo").val("");
            $("#txtDecimalesMinimo").removeClass("requerido");
        }
    });

    //-------------------------- EVENTO CONTROLES | FORMULA ------------------------------------        
    $('#txtFormulaPopUp').on("keypress", function (e) { return soloEspacioBack(e); });
    $("#btnAgregarValor").on("click", function () { agregarFormula('txtFormulaPopUp', 'ddlValor'); });
    $("#btnLimpiarPopup").on("click", function () { $("#txtFormulaPopUp").val(''); });

    $("#num1").on("click", function () { agregarFormula('txtFormulaPopUp', 'num1'); });
    $("#num2").on("click", function () { agregarFormula('txtFormulaPopUp', 'num2'); });
    $("#num3").on("click", function () { agregarFormula('txtFormulaPopUp', 'num3'); });
    $("#num4").on("click", function () { agregarFormula('txtFormulaPopUp', 'num4'); });
    $("#num5").on("click", function () { agregarFormula('txtFormulaPopUp', 'num5'); });

    $("#num6").on("click", function () { agregarFormula('txtFormulaPopUp', 'num6'); });
    $("#num7").on("click", function () { agregarFormula('txtFormulaPopUp', 'num7'); });
    $("#num8").on("click", function () { agregarFormula('txtFormulaPopUp', 'num8'); });
    $("#num9").on("click", function () { agregarFormula('txtFormulaPopUp', 'num9'); });
    $("#num0").on("click", function () { agregarFormula('txtFormulaPopUp', 'num0'); });

    $("#numPabierto").on("click", function () { agregarFormula('txtFormulaPopUp', 'numPabierto'); });
    $("#numPcerrado").on("click", function () { agregarFormula('txtFormulaPopUp', 'numPcerrado'); });
    $("#numPunto").on("click", function () { agregarFormula('txtFormulaPopUp', 'numPunto'); });

    $("#numSuma").on("click", function () { agregarFormula('txtFormulaPopUp', 'numSuma'); });
    $("#numResta").on("click", function () { agregarFormula('txtFormulaPopUp', 'numResta'); });
    $("#numMultiplicador").on("click", function () { agregarFormula('txtFormulaPopUp', 'numMultiplicador'); });
    $("#numDivisor").on("click", function () { agregarFormula('txtFormulaPopUp', 'numDivisor'); });

    $("#numT").on("click", function () { agregarFormula('txtFormulaPopUp', 'numT'); });
    $("#numW").on("click", function () { agregarFormula('txtFormulaPopUp', 'numW'); });
    $("#numX").on("click", function () { agregarFormula('txtFormulaPopUp', 'numX'); });
    $("#numY").on("click", function () { agregarFormula('txtFormulaPopUp', 'numY'); });
    $("#numZ").on("click", function () { agregarFormula('txtFormulaPopUp', 'numZ'); });
    $("#numR").on("click", function () { agregarFormula('txtFormulaPopUp', 'numR'); });
    $("#numV").on("click", function () { agregarFormula('txtFormulaPopUp', 'numV'); });

    $("#numBack").on("click", function () {
        var string = $('#txtFormulaPopUp').val();
        var str = string.substring(0, string.length - 1);
        $('#txtFormulaPopUp').val(str);
    });
    $("#btnRegla").on("click", function () {
        var cantidadRegistros = parseInt($('#numVariable').val());
        if (cantidadRegistros > 0) {
            var regla = $("#txtFormulaRegla").val();
            $("#txtFormulaPopUp").val(regla);
            loadValorePopup();
            $('#mvFormula').css('overflow', 'hidden');
            $("#mvFormula").dialog("open");
            $("#hidFormula").val('R');
        }
    });
    $("#btnMinimo").on("click", function () {
        var cantidadRegistros = parseInt($('#numVariable').val());
        if (cantidadRegistros > 0) {
            var minimo = $("#txtFormulaMinimo").val();
            $("#txtFormulaPopUp").val(minimo);
            loadValorePopup();
            $('#mvFormula').css('overflow', 'hidden');
            $("#mvFormula").dialog("open");
            $("#hidFormula").val('M');
        }
    });
    controlesValorHabDes(true);
    //--------------------------------------------------------
    //*********************Quitando el Desc Especial**********
    //$("#ddlTipoDescuento option[value='11']").hide();
    $("#ddlTipoDescuento").on("click", function () {
        $("#ddlTipoDescuento").find("option[value='11']").remove();
        if ($(this).val() == 12)
            $("#trvalor").hide();
        else
            $("#trvalor").show();
    });
    //No mostrar El txt valor y desahilitarlo
    //------------------------------------------------------
    //*****************Busca Tarifa Anterior*
    $("#btnBuscarTarifa").on("click", function () {
        limpiarBusquedaTari();
        loadDataFoundTari();
    });
});

function eliminaropcionEspecial() {
    $("#ddlTipoDescuento").find("option[value='DESCUENTO ESPECIAL']").remove();
    $("#ddlTipoDescuento option[value='11']").hide();
}
//
function validarPeriodocidad() {
    var estado = false;
    var cantReg = validacionCantPeriodocidadXProd();
    if (cantReg == 0) {
        msgErrorDiv("divPeriodocidadVal", '');
        estado = true;
    }
    else {
        msgErrorDiv("divPeriodocidadVal", K_PERIODOCIDAD_VALIDACION);
    }
    return estado;
}

var reloadEventoMod = function (idModSel) {
    $("#lblModalidad").css('color', 'black');
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    ObtenerDatosModalidad($("#hidModalidad").val());
    var modalidad = $("#lblModalidad").val();
    $("#txtModalidad").val(modalidad);
    var estado = validarPeriodocidad();
};

//--- FUNCIONES DE  FORMULA
function soloEspacioBack(key) {
    var estado = true;
    if (key.which === 8) { //BackSpace
        estado = true;
    } else {
        if (key.charCode < 48 || key.charCode > 57) estado = false; //Solo Letras

        if ((key.charCode < 97 || key.charCode > 122) && (key.charCode < 65 || key.charCode > 90) && (key.charCode != 45))//Solo Numeros
            estado = false;
    }
    return estado;
}

var generarFormula = function () {
    var tipo = $("#hidFormula").val();
    var formula = $("#txtFormulaPopUp").val();

    if (tipo == 'R')
        $('#txtFormulaRegla').val(formula);
    else
        $('#txtFormulaMinimo').val(formula);

    $("#mvFormula").dialog("close");
}

function agregarFormula(control, valor) {
    var srrVal = '';
    var strformula = $('#' + control).val();
    if (valor == 'numV')
        srrVal = 'V';
    else
        srrVal = $('#' + valor).val();
    $('#' + control).val(strformula + srrVal);
}

function loadValorePopup() {
    controlesValorHabDes(false);
    $.ajax({
        url: '../Tarifa/ObtenerValorePopup',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    controlValorHabDes('num' + entidad.Value, true);
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function controlValorHabDes(control, estado) {
    if (estado)
        $('#' + control).show();
    else
        $('#' + control).hide();
}

function controlesValorHabDes(estado) {
    controlValorHabDes('numT', estado);
    controlValorHabDes('numW', estado);
    controlValorHabDes('numX', estado);
    controlValorHabDes('numY', estado);
    controlValorHabDes('numZ', estado);
}
//--------------------------------------------------------

function limpiar() {
    $("#numVariable").val(0);
    $("#numSegmentos").val(0);
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    $("#hidAccionMvVal").val("0");
    $("#hidEdicionVal").val("0");
    $("#hidEdicionValCar").val("0");
    $("#txtDescripcion").val("");
    $("#txtObservacion").val("");
    $("#txtFormulaRegla").val("");
    $("#txtFormulaMinimo").val("");
    $("#txtFormulaMinimo").val("");
    $("#txtDecimalesRegla").val("");
    $("#txtDecimalesMinimo").val("");
    $("#txtVum").val("");
    $("#hidVum").val("");
    $('#chkRepertorioUso').attr('checked', false);
}

function grabar() {

    var estado = true;
    var estadoPeriodocidad = false;
    var numCaracteristicas = $("#numVariable").val();
    var codMod = $("#hidModalidad").val();
    var estadoRequeridos = ValidarRequeridos();

    if (codMod == 0 || codMod == '')//validar modalidad
        $("#lblModalidad").css('color', 'red');
    else
        $("#lblModalidad").css('color', 'black');

    if (numCaracteristicas == '0' || numCaracteristicas == '') {
        estado = false;
        $("#numVariable").css('border', '1px solid red');
        msgErrorDiv("divResultNumCar", K_MENSAJE_NUM_ELEMENTO);
    } else {
        $("#numVariable").css('border', '1px solid gray');
        msgErrorDiv("divResultNumCar", "");
    }

    if (estadoRequeridos)
    {
        //estadoPeriodocidad = validarPeriodocidad();
        estadoPeriodocidad = true;
    }
    if (estadoRequeridos && estado && estadoPeriodocidad) {
        msgError("");
        obtenerListaCaracteristica();
        //insertar();
    }
};

function insertar() {
    var id = 0;
    var indicadorRepertorio = '0';
    var incrementoMinimo = '0';
    var conRedondeo = 0;
    $("#chkRepertorioUso").is(':checked') ? indicadorRepertorio = '1' : indicadorRepertorio = '0';
    $("#chkRedondeo").is(':checked') ? conRedondeo = 1 : conRedondeo = 0;
    
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

    var regla = {
        RATE_ID: id,
        RATE_ID_PREC: $("#hidtarifaPadre").val(),
        RATE_DESC: $("#txtDescripcion").val(),
        MOD_ID: $("#hidModalidad").val(),
        NAME: $("#txtDescripcion").val(),
        RATE_START: $("#txtFecha").val(),
        RATE_END: $("#txtFechaVencimiento").val(),
        CUR_ALPHA: $("#ddlMoneda").val(),
        RATE_OBSERV: $("#txtObservacion").val(),
        RAT_FID: $("#ddlPeriodocidad").val(),
        RATE_ACCOUNT: $("#ddlCuentaContable").val(),
        RATE_DREPERT: indicadorRepertorio,
        RATE_NVAR: $("#numVariable").val(),
        RATE_NCAL: $("#numSegmentos").val(),
        RATE_FORMULA: $("#txtFormulaRegla").val(),
        RATE_MINIMUM: $("#txtFormulaMinimo").val(),
        RATE_FTIPO: $("#ddlFormatoRegla").val(),
        RATE_MTIPO: $("#ddlFormatoMinimo").val(),
        RATE_FDECI: $("#txtDecimalesRegla").val(),
        RATE_MDECI: $("#txtDecimalesMinimo").val(),
        LOG_DATE_CREAT: $("#txtFechaCreacion").val(),
        RATE_ID_ORIG: $("#hidIdOrigen").val(),
        RATE_REDONDEO: conRedondeo
    };
    
    $.ajax({
        url: '../Tarifa/Insertar',
        data: regla,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Tarifa/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../Tarifa/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                validarRedirect(req);
                if (req != null) {
                    $("#hidIdOrigen").val(req.RATE_ID_ORIG);
                    $("#txtDescripcion").val(req.RATE_DESC);
                    $("#hidModalidad").val(req.MOD_ID);
                    $("#lblModalidad").html(req.MOD_DEC);
                    $("#hidtarifaPadre").val(req.RATE_ID_PREC);

                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(req.RATE_START);
                    d1.value(valFecha);

                    var dVencimiento = $("#txtFechaVencimiento").data("kendoDatePicker");
                    var valFechaVencimiento = formatJSONDate(req.RATE_END);
                    dVencimiento.value(valFechaVencimiento);

                    var d2 = $("#txtFechaCreacion").data("kendoDatePicker");
                    var valFecha2 = formatJSONDate(req.LOG_DATE_CREAT);
                    d2.value(valFecha2);


                    loadTipoMoneda('ddlMoneda', req.CUR_ALPHA);
                    $("#txtObservacion").val(req.RATE_OBSERV);
                    loadPeriodocidad('ddlPeriodocidad', req.RAT_FID);
                    loadCuentaContable('ddlCuentaContable', req.RATE_ACCOUNT);
                    
                    (req.RATE_DREPERT == '1') ? $('#chkRepertorioUso').prop('checked', true) : $('#chkRepertorioUso').prop('checked', false);
                    (req.RATE_REDONDEO == 1) ? $('#chkRedondeo').prop('checked', true) : $('#chkRedondeo').prop('checked', false);

                    $("#numVariable").val(req.RATE_NVAR);
                    $("#numSegmentos").val(req.RATE_NCAL);

                    $("#txtFormulaRegla").val(req.RATE_FORMULA);
                    $("#txtFormulaMinimo").val(req.RATE_MINIMUM);

                    loadFormatoFormula('ddlFormatoRegla', req.RATE_FTIPO);
                    loadFormatoFormula('ddlFormatoMinimo', req.RATE_MTIPO);

                    if (req.RATE_FTIPO == 'N') {
                        $("#txtDecimalesRegla").val(req.RATE_FDECI);
                        $("#txtDecimalesRegla").prop("disabled", false);
                        $("#txtDecimalesRegla").addClass("requerido");
                    }
                    else if (req.RATE_FTIPO == 'P') {
                        $("#txtDecimalesRegla").prop("disabled", true);
                        $("#txtDecimalesRegla").val("");
                        $("#txtDecimalesRegla").removeClass("requerido");
                    }

                    if (req.RATE_MTIPO == 'N') {
                        $("#txtDecimalesMinimo").val(req.RATE_MDECI);
                        $("#txtDecimalesMinimo").prop("disabled", false);
                        $("#txtDecimalesMinimo").addClass("requerido");

                    }
                    else if (req.RATE_MTIPO == 'P') {
                        $("#txtDecimalesMinimo").prop("disabled", true);
                        $("#txtDecimalesMinimo").prop("");
                        $("#txtDecimalesMinimo").removeClass("requerido");
                    }

                    //loadDataRegla();
                    loadDataCaracteristica();
                    loadDataDescuento();
                    loadDataRegla();
                    
                    reloadEventoTarifa($("#hidtarifaPadre").val());
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//---------------------------------------------
function loadDataGridTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);                
                var cantidadRegistros = dato.Code;
                $('#numVariable').val(cantidadRegistros);
            } else if (dato.result == 0) {
                alert(dato.message);
            }   
        }
    });
}

function loadDataGridTmpDescuento(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataCaracteristicaGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                var cantidadRegistros = dato.Code;
                $('#numSegmentos').val(cantidadRegistros);
            } else if (dato.result == 1) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- GRILLA - CARACTERISTICA -------------------------------------  
function loadDataCaracteristica() {
    loadDataCaracteristicaGridTmp('ListarCaracteristica', "#gridCaracteristica");
}

//-------------------------- GRILLA - ELEMENTO -------------------------------------  
function loadDataRegla() {
    loadDataGridTmp('ListarReglaAsoc', "#gridRegla");
}

function addReglaAsoc() {
    var estado = true;

    if ($("#ddlTipo").val() == 0) {
        $('#ddlTipo').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#ddlTipo').css({ 'border': '1px solid gray' });

    if ($("#ddlElemento").val() == 0) {
        $('#ddlElemento').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#ddlElemento').css({ 'border': '1px solid gray' });

    if (estado) {
        var entidad = {
            IdRegla: $("#ddlElemento option:selected").val(),
            Elemento: $("#ddlElemento option:selected").text().toUpperCase(),
            TipoCalculo: $("#ddlTipo option:selected").val(),
            Tipo: $("#ddlTipo option:selected").text().toUpperCase()
        };

        $.ajax({
            url: '../Tarifa/AddReglaAsoc',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataRegla();
                    loadDataCaracteristica();
                }
                else if (dato.result == 0) {
                    alert(dato.message);
                }
                else if (dato.result == 2) {
                    alert(dato.message);
                }
            }
        });
        $("#hidAccionMvObs").val("0");
        $("#hidEdicionObs").val("0");
        $("#mvRegla").dialog("close");
        $('#ddlRegla ').css({ 'border': '1px solid gray' });
    }
}

function obtenerListaCaracteristica() {
    var CaracteristicaValor = [];
    var contador = 0;
    var estadoimp = '';
    $('#tblCaracteristica tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            $('#chkUnidadMedida' + id).is(':checked') ? estadoimp = '1' : estadoimp = '0';
            CaracteristicaValor[contador] = {
                RATE_CHAR_ID: id,
                RATE_CHAR_DESCVAR: $("#txtDescripcionLarga" + id).val(),
                RATE_CHAR_VARUNID: $('#txtUnidadMedida' + id).val(),
                RATE_CHAR_CARIDSW: estadoimp
            };
            contador += 1;
        }
    });

    var CaracteristicaValor = JSON.stringify({ 'CaracteristicaValor': CaracteristicaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Tarifa/ObtenerListaCaracteristica',
        data: CaracteristicaValor,
        success: function () {
            insertar();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function delAddReglaAsoc(idDel) {
    Confirmar(K_CARACTERISTICA_ELIMIAR_MENSAJE,
                       function () {
                           $.ajax({
                               url: '../Tarifa/DellAddReglaAsoc',
                               type: 'POST',
                               data: { id: idDel },
                               beforeSend: function () { },
                               success: function (response) {
                                   var dato = response;
                                   validarRedirect(dato);
                                   if (dato.result == 1) {
                                       loadDataRegla();
                                       loadDataCaracteristica();
                                   } else if (dato.result == 0) {
                                       alert(dato.message);
                                   }
                               }
                           });
                           return false;

                       },
                       function () {
                       },
                       'Confirmar'
                   );
}

function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            OK: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Cancel: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}

function ObtenerVUM() {
    $.ajax({
        url: "../Tarifa/ObtieneVUM",
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    $("#txtVum").val(req.VUM_VAL);
                    $("#hidVum").val(req.VUM_ID);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerDatosDescuento(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: "../Tarifa/ObtieneDescuento",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    if (tipo.DISC_VALUE != 0) {
                        $("#txtFormato").val('S/.');
                        $("#txtValor").val(tipo.DISC_VALUE);
                    } else {
                        $("#txtFormato").val('%');
                        $("#txtValor").val(tipo.DISC_PERC);
                    }
                    $("#hidSigno").val(tipo.DISC_SIGN);
                    
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - OBSERVACION ---------------------------------- 

function loadDataDescuento() {
    //loadDataGridTmp('ListarDescuento', "#gridDescuento");    
    loadDataGridTmpDescuento('ListarDescuento', "#gridDescuento");
}

var addDescuento = function () {
    var estado = true;

    if ($("#ddlTipoDescuento").val() == 0) {
        $('#ddlTipoDescuento').css({ 'border': '1px solid red' });
        estado = false;
    }

    if ($("#ddlDescuento").val() == 0) {
        $('#ddlDescuento').css({ 'border': '1px solid red' });
        estado = false;
    }

    if (estado) {
        var IdAdd = 0;
        var IdRegla = 0;
        if ($("#hidAccionMvDes").val() === "1") {
            IdAdd = $("#hidEdicionDes").val();
            IdRegla = $("#hidId").val();
        }
        var entidad = {
            Id: IdAdd,
            IdTarifa: IdRegla,
            IdTipoDesc: $("#ddlTipoDescuento").val(),
            IdDesc: $("#ddlDescuento").val(),
            TipoDescripcion: $("#ddlTipoDescuento option:selected").text(),
            Descripcion: $("#ddlDescuento option:selected").text(),
            Formato: $("#txtFormato").val(),
            Valor: $("#txtValor").val(),
            Signo: $("#hidSigno").val()
        };

        $.ajax({
            url: '../Tarifa/AddDescuento',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDescuento();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvDescuento").dialog("close");
    }
}

function delAddDescuento(idDel) {
    $.ajax({
        url: '../Tarifa/DellAddDescuento',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDescuento();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarDescuento() {
    $("#txtFormato").val("");
    $("#txtValor").val("");
    $("#hidSigno").val("");
    $("#hidAccionMvDes").val("0");
    $("#hidEdicionDes").val("0");
    $('#ddlTipoDescuento').css({ 'border': '1px solid gray' });
    $('#ddlDescuento').css({ 'border': '1px solid gray' });
}

function validacionCantPeriodocidadXProd() {
    var cantReg = 0;
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

    var tarifa = {
        RATE_ID: id,
        MOD_ID: $("#hidModalidad").val(),
        RAT_FID: $("#ddlPeriodocidad").val(),
        RATE_START: $("#txtFecha").val()
    };

    $.ajax({
        url: "../Tarifa/ObtenerCantPeriodocidadXProd",
        type: 'POST',
        data: tarifa,
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    cantReg = req;
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return cantReg;
}
//***************************Fuciones de Busqueda de Tarifa
var reloadEventoTarifa = function (idSel) {
    //$("#hidId").val(idSel);
    obtenerNombreConsultaTarifa(idSel);
    //ObtenerDatos($("#hidId").val());
};

function obtenerNombreConsultaTarifa(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/ObtenerNombreTarifa',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbTarifa").html(dato.valor);
                $("#hidtarifaPadre").val(idSel);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}