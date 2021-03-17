/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 155;
var K_WIDTH_FOR = 500;
var K_HEIGHT_FOR = 300;
var K_WIDTH_VAL = 500;
var K_HEIGHT_VAL = 400;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_CARACTERISTICA_MENSAJE = "Se permite como maximo 5 caracteristicas.";
var K_CARACTERISTICA_ELIMIAR_MENSAJE = "La caracteristica sera eliminada con todos los valores. ¿Desea eliminar?";
var K_MENSAJE_NUM_CARACTERISTICA = "Se debe ingresar como minimo 1 caracteristica para grabar la plantilla.";
var K_DESCRIPCION_VALIDACION_MENSAJE = "La descripción ya existe, ingrese uno nuevo."
/************************** INICIO CARGA********************************************/
$(function () {

    kendo.culture('es-PE');
    //--------------------------------------------------------------------------------- 
    $("#txtDescripcion").focus();
    var id = (GetQueryStringParams("id"));
    $('#txtFecha').kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#numVariable").val(0);
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    $("#hidAccionMvVal").val("0");
    $("#hidEdicionVal").val("0");
    $("#hidEdicionValCar").val("0");
    $('#txtDecimalesRegla').on("keypress", function (e) { return solonumeros(e); });
    $('#txtDecimalesMinimo').on("keypress", function (e) { return solonumeros(e); });
    $("#imgCaracteristicaRemove").remove();
    //--------------------------------------------------------------------------------- 
    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Regla de Cálculo – Nuevo");
        $("#hidId").val(0);
        $("#hidOpcionEdit").val(0);
        $("#txtId").val(0);
        $("#trId").hide();
        loadTipoMoneda('ddlMoneda', 0);
        loadPeriodocidad('ddlPeriodocidad', 0);
        loadPlantillaTarifa('dllPlantilla', 0);
        loadFormatoFormula('ddlFormatoRegla', 0);
        loadFormatoFormula('ddlFormatoMinimo', 0);
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Regla de Cálculo - Modificar");
        $("#hidId").val(id);
        $("#hidOpcionEdit").val(1);
        $("#txtId").val(id);
        $("#trId").show();
        ObtenerDatos(id);
    }

    //--------------------------POPUP -----------------------------------------------
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

    //Caracteristica
    $("#mvObservacion").dialog({
        autoOpen: false, width: K_WIDTH, height: K_HEIGHT,
        buttons: {
            "Agregar": addCaracteristica,
            "Cancelar": function () {
                $("#mvObservacion").dialog("close");
                $("#hidAccionMvObs").val("0");
                $("#hidEdicionObs").val("0");
            }
        }, modal: true
    });
    $(".addCaracteristica").on("click", function () {
        var cantidadRegistros = parseInt($('#numVariable').val());
        if (cantidadRegistros < 5) {
            $("#chkTramo").prop('checked', false);
            loadTarifaCaracteristica('ddlTipoCaracteristica', 0);
            $('#mvObservacion').css('overflow', 'hidden');
            $("#mvObservacion").dialog("open");
        } else {
            alert(K_CARACTERISTICA_MENSAJE);
        }
    });
    $(".addLimpiar").on("click", function () {
        addLimpiarValorMatriz();
    });


    //--------------------------POPUP -----------------------------------------    
    $("#mvValor").dialog({
        autoOpen: false, width: K_WIDTH_VAL, height: K_HEIGHT_VAL,
        buttons: {
            "Grabar": addValor,
            "Cancelar": function () {
                $("#mvValor").dialog("close");
                $("#hidAccionMvVal").val("0");
                $("#hidEdicionVal").val("0");
                $("#hidEdicionValCar").val("0");
            }
        }, modal: true
    });

    $("#tabs").tabs();
    $("#tabs2").tabs();
    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnRegresar").on("click", function () {
        location.href = "../TarifaRegla/";
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#dllPlantilla").change(function () {
        var id = $("#dllPlantilla").val();
        ObtenerDatosPlantilla(id);
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

    loadDataCaracteristica(0);
    loadDataValor(0);

    jQuery('#txtmatriz').keydown(function (event) {
        $('.txtmatriz').on("keypress", function (e) { return solonumeros(e); });
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

    $("#numA").on("click", function () { agregarFormula('txtFormulaPopUp', 'numA'); });
    $("#numB").on("click", function () { agregarFormula('txtFormulaPopUp', 'numB'); });
    $("#numC").on("click", function () { agregarFormula('txtFormulaPopUp', 'numC'); });
    $("#numD").on("click", function () { agregarFormula('txtFormulaPopUp', 'numD'); });
    $("#numE").on("click", function () { agregarFormula('txtFormulaPopUp', 'numE'); });
    $("#numR").on("click", function () { agregarFormula('txtFormulaPopUp', 'numR'); });

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
});

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
    var strformula = $('#' + control).val();
    var srrVal = $('#' + valor).val();
    $('#' + control).val(strformula + srrVal);
}

function loadValorePopup() {
    controlesValorHabDes(false);
    $.ajax({
        url: '../TarifaRegla/ObtenerValorePopup',
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
    controlValorHabDes('numA', estado);
    controlValorHabDes('numB', estado);
    controlValorHabDes('numC', estado);
    controlValorHabDes('numD', estado);
    controlValorHabDes('numE', estado);
}
//--------------------------------------------------------

function grabar() {
    var estado = true;
    var numCaracteristicas = $("#numVariable").val();
    var estadoRequeridos = ValidarRequeridos();

    if (numCaracteristicas == '0' || numCaracteristicas == '') {
        estado = false;
        $("#numVariable").css('border', '1px solid red');
        msgErrorDiv("divResultNumCar", K_MENSAJE_NUM_CARACTERISTICA);
    } else {
        $("#numVariable").css('border', '1px solid gray');
        msgErrorDiv("divResultNumCar", "");
    }

    if (estadoRequeridos && estado) {
        msgError("");
        obtenerMatrizValor();
        //insertar();
    } else {
        msgError("Debe ingresar los campos requeridos ");
        var index = $('#tabs a[href="#tabs-3"]').parent().index();
        $("#tabs").tabs("option", "active", index);
    }
};

function insertar() {
    var id = 0;
    var ajuste = '0';
    var acumular = '0';
    var incrementoRegla = '0';
    var incrementoMinimo = '0';

    $("#chkAjustar").is(':checked') ? ajuste = '1' : ajuste = '0';
    $("#chkAcumular").is(':checked') ? acumular = '1' : acumular = '0';
    $("#chkReglaIncremento").is(':checked') ? incrementoRegla = '1' : incrementoRegla = '0';
    $("#chkMinimoIncremento").is(':checked') ? incrementoMinimo = '1' : incrementoMinimo = '0';

    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

    var regla = {
        CALR_ID: id,
        STARTS: $("#txtFecha").val(),
        CALR_DESC: $("#txtDescripcion").val(),
        RATE_FREQ: $("#ddlPeriodocidad").val(),
        TEMP_ID: $("#dllPlantilla").val(),
        CALR_NVAR: $("#numVariable").val(),
        CALR_ADJUST: ajuste,
        CALR_ACCUM: acumular,
        CALC_FORMULA: $("#txtFormulaRegla").val(),
        CALC_MINIMUM: $("#txtFormulaMinimo").val(),
        CALC_IFORMULA: incrementoRegla,
        CALC_IMINIMO: incrementoMinimo,
        CALR_FOR_DEC: $("#txtDecimalesRegla").val(),
        CALR_FOR_TYPE: $("#ddlFormatoRegla").val(),
        CALR_MIN_DEC: $("#txtDecimalesMinimo").val(),
        CALR_MIN_TYPE: $("#ddlFormatoMinimo").val(),
        CALR_OBSERV: $("#txtObservacion").val(),
        CUR_ALPHA: $("#ddlMoneda").val()
    };

    $.ajax({
        url: '../TarifaRegla/Insertar',
        data: regla,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../TarifaRegla/';
            } if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function ObtenerDatosPlantilla(id) {
    $.ajax({
        url: "../TarifaRegla/ObtenerPlantilla",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    $("#numVariable").val(req.TEMP_NVAR);
                    if (req.TEMP_NVAR == '0' || req.TEMP_NVAR == '') {
                        estado = false;
                        $("#numVariable").css('border', '1px solid red');
                        $("#dllPlantilla").css('border', '1px solid red');
                        msgErrorDiv("divResultNumCar", K_MENSAJE_NUM_CARACTERISTICA);
                    } else {
                        $("#numVariable").css('border', '1px solid gray');
                        $("#dllPlantilla").css('border', '1px solid gray');
                        msgErrorDiv("divResultNumCar", "");
                    }
                    loadDataCaracteristica(0);
                    loadDataValor(0);
                    loadDataMatriz(0);
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
        type: 'POST', data: { idCaracteristica: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;

            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                var cantidadRegistros = dato.Code;
                $('#numVariable').val(cantidadRegistros);
            }else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataGridValorTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idCaracteristica: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataGridMatrizTmp(Controller, idGrilla, id) {
    $.ajax({
        type: 'POST', data: { idCaracteristica: id }, url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }           
        }
    });
}

function addvalidacionSoloNumeroValor() {
    $('#tblMatriz tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            $('#txtMatrizTarifa' + id).on("keypress", function (e) { return solonumeros(e); });
            $('#txtMatrizMinimo' + id).on("keypress", function (e) { return solonumeros(e); });
        }
    });
}

function addLimpiarValorMatriz() {
    $('#tblMatriz tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            $('#txtMatrizTarifa' + id).val('');
            $('#txtMatrizMinimo' + id).val('');
        }
    });
}

//-------------------------- TAB - CARACTERISTICA -------------------------------------  
function loadDataCaracteristica(id) {
    loadDataGridTmp('ListarCaracteristica', "#gridCaracteristica", id);
}

function loadDataValor(id) {
    loadDataGridValorTmp('ListarValor', "#gridValor", id);
}

function loadDataMatriz(id) {
    loadDataGridMatrizTmp('ListarValorMatriz', "#gridMatriz", id);
}

function addCaracteristica() {
    if ($("#ddlTipoCaracteristica").val() == 0) {
        $('#ddlTipoCaracteristica').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        var tramo = 0;

        if ($("#hidAccionMvObs").val() === "1") {
            IdAdd = $("#hidEdicionObs").val();
        }
        $("#chkTramo").is(':checked') ? tramo = 1 : tramo = 0;


        var entidad = {
            Id: IdAdd,
            IdCaracteristica: $("#ddlTipoCaracteristica option:selected").val(),
            DesCaracteristica: $("#ddlTipoCaracteristica option:selected").text(),
            Tramo: tramo
        };

        $.ajax({
            url: '../TarifaRegla/AddCaracteristica',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataCaracteristica(0);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#hidAccionMvObs").val("0");
        $("#hidEdicionObs").val("0");
        $("#mvObservacion").dialog("close");
        $('#ddlTipoCaracteristica').css({ 'border': '1px solid gray' });
    }
}

function delAddCaracteristica(idDel) {
    var idPlantilla = $("#dllPlantilla").val();
    Confirmar(K_CARACTERISTICA_ELIMIAR_MENSAJE,
                       function () {

                           $.ajax({
                               url: '../TarifaRegla/DellAddCaracteristica',
                               type: 'POST',
                               data: { id: idDel, idPlantilla: idPlantilla },
                               beforeSend: function () { },
                               success: function (response) {
                                   var dato = response;
                                   if (dato.result == 1) {
                                       loadDataCaracteristica(0);
                                       loadDataValor(0);
                                       loadDataMatriz(0);
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

function updAddCaracteristica(idUpd) {
    $.ajax({
        url: '../TarifaRegla/ObtieneCaracteristicaTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(doc.Id);
                    loadTarifaCaracteristica('ddlTipoCaracteristica', doc.IdCaracteristica);

                    if (doc.Tramo == 1)
                        $("#chkTramo").prop('checked', true);
                    else
                        $("#chkTramo").prop('checked', false);

                    $("#mvObservacion").dialog("open");
                } else {
                    alert("No se pudo obtener la caracteristica para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - VALOR -------------------------------------  


function nuevoValor(id, tramo) {
    $("#txtDescVal").val('');
    $("#txtValDesde").val('');
    $("#txtValHasta").val('');
    $('#txtDescVal').css({ 'border': '1px solid gray' });
    $('#txtValDesde').css({ 'border': '1px solid gray' });
    $('#txtValHasta').css({ 'border': '1px solid gray' });
    $("#hidAccionMvVal").val("0");
    $("#hidEdicionVal").val('0');
    $("#hidEdicionValCar").val(id);

    if (tramo == 1)
        $(".trValHasta").show();
    else {
        $(".trValHasta").hide();
        $("#txtValHasta").val(-1);
    }
    $('#mvValor').css('overflow', 'hidden');
    $("#mvValor").dialog("open");
};

function addValor() {
    var estado = true;
    if ($("#txtDescVal").val() == '') {
        $('#txtDescVal').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtDescVal').css({ 'border': '1px solid gray' });

    if ($("#txtValDesde").val() == '') {
        $('#txtValDesde').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtValDesde').css({ 'border': '1px solid gray' });

    if ($("#txtValHasta").val() == '') {
        $('#txtValHasta').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtValHasta').css({ 'border': '1px solid gray' });

    if (estado) {
        var IdAdd = 0;
        vIdCaracteristica = $("#hidEdicionValCar").val();

        if ($("#hidAccionMvVal").val() === "1") {
            IdAdd = $("#hidEdicionVal").val();
        }

        var entidad = {
            Id: IdAdd,
            IdCaracteristica: vIdCaracteristica,
            Desde: $("#txtValDesde").val(),
            Hasta: $("#txtValHasta").val(),
            Descripcion: $("#txtDescVal").val()
        };

        $.ajax({
            url: '../TarifaRegla/AddValor',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataCaracteristica(vIdCaracteristica);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#hidAccionMvVal").val("0");
        $("#hidEdicionVal").val("0");
        $("#hidEdicionValCar").val("0");
        $("#mvValor").dialog("close");

    }
}

function delAddValor(idDel) {
    $.ajax({
        url: '../TarifaRegla/DellAddValor',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //alert();
            if (dato.result == 1) {
                var doc = dato.data.Data;
                loadDataCaracteristica(doc.IdCaracteristica);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddValor(idUpd, tramo) {
    $("#txtDescVal").val('');
    $("#txtValDesde").val('');
    $("#txtValHasta").val('');
    $('#txtDescVal').css({ 'border': '1px solid gray' });
    $('#txtValDesde').css({ 'border': '1px solid gray' });
    $('#txtValHasta').css({ 'border': '1px solid gray' });

    //if (tramo == 1)
    //    $(".trValHasta").show();
    //else
    //    $(".trValHasta").hide();

    $.ajax({
        url: '../TarifaPlantilla/ObtieneValorTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvVal").val("1");
                    $("#hidEdicionVal").val(doc.Id);
                    $("#hidEdicionValCar").val(doc.IdCaracteristica);
                    $("#txtDescVal").val(doc.Descripcion);
                    $("#txtValDesde").val(doc.Desde.toFixed(2));

                    if (tramo == 1) {
                        $("#txtValHasta").val(doc.Hasta.toFixed(2));
                        $(".trValHasta").show();
                    }
                    else {
                        $(".trValHasta").hide();
                        $("#txtValHasta").val(-1);
                    }

                    $('#mvValor').css('overflow', 'hidden');
                    $("#mvValor").dialog("open");
                } else {
                    alert("No se pudo obtener el valor para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    $("#hidAccionMvVal").val("0");
    $("#hidEdicionVal").val("0");
    $("#hidEdicionValCar").val("0");
}
//---------------------------------------------------------------------------------
function verDeta(id) {
    if ($("#expand" + id).attr('src') == '../Images/botones/less.png') {
        $("#expand" + id).attr('src', '../Images/botones/more.png');
        $("#expand" + id).attr('title', 'ver detalle.');
        $("#expand" + id).attr('alt', 'ver detalle.');
        $("#div" + id).css("display", "none");

    } else {
        $("#expand" + id).attr('src', '../Images/botones/less.png');
        $("#expand" + id).attr('title', 'ocultar detalle.');
        $("#expand" + id).attr('alt', 'ocultar detalle.');
        $("#div" + id).css("display", "inline");
    }
    return false;
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

function validarDescripcion() {
    var sociedad = $("#txtDescripcion").val();
    if (sociedad != '') {
        var estadoDuplicado = validarDuplicado();
        if (!estadoDuplicado) {
            $("#txtDescripcion").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDescripcion", "");
            return true;
        } else {
            $("#txtDescripcion").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDescripcion", K_DESCRIPCION_VALIDACION_MENSAJE);
            return false;
        }
    }
}

function validarDuplicado() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#hidId").val();

    var plantilla = {
        TEMP_ID: id,
        TEMP_DESC: $("#txtDescripcion").val()
    };

    $.ajax({
        url: '../TarifaRegla/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: plantilla,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
            }
        }
    });
    return estado;
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../TarifaRegla/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    $("#txtDescripcion").val(req.CALR_DESC);
                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(req.STARTS);
                    d1.value(valFecha);
                    loadTipoMoneda('ddlMoneda', req.CUR_ALPHA);
                    loadPeriodocidad('ddlPeriodocidad', req.RATE_FREQ);
                    loadPlantillaTarifa('dllPlantilla', req.TEMP_ID);

                    if (req.CantReglaAsocMant > 0)
                        $("#dllPlantilla").prop('disabled', true);
                    else
                        $("#dllPlantilla").prop('disabled', false);

                    $("#numVariable").val(req.CALR_NVAR);
                    $("#txtObservacion").val(req.CALR_OBSERV);

                    if (req.CALR_ADJUST == '1')
                        $('#chkAjustar').prop('checked', true);
                    else
                        $('#chkAjustar').prop('checked', false);

                    if (req.CALR_ACCUM == '1')
                        $('#chkAcumular').prop('checked', true);
                    else
                        $('#chkAcumular').prop('checked', false);

                    $("#txtFormulaRegla").val(req.CALC_FORMULA);
                    $("#txtFormulaMinimo").val(req.CALC_MINIMUM);


                    loadFormatoFormula('ddlFormatoRegla', req.CALR_FOR_TYPE);
                    loadFormatoFormula('ddlFormatoMinimo', req.CALR_MIN_TYPE);

                    if (req.CALR_FOR_TYPE == 'N') {
                        $("#txtDecimalesRegla").val(req.CALR_FOR_DEC);
                        $("#txtDecimalesRegla").prop("disabled", false);
                        $("#txtDecimalesRegla").addClass("requerido");
                    }
                    else if (req.CALR_FOR_TYPE == 'P') {
                        $("#txtDecimalesRegla").prop("disabled", true);
                        $("#txtDecimalesRegla").val("");
                        $("#txtDecimalesRegla").removeClass("requerido");
                    }

                    if (req.CALR_MIN_TYPE == 'N') {
                        $("#txtDecimalesMinimo").val(req.CALR_MIN_DEC);
                        $("#txtDecimalesMinimo").prop("disabled", false);
                        $("#txtDecimalesMinimo").addClass("requerido");

                    }
                    else if (req.CALR_MIN_TYPE == 'P') {
                        $("#txtDecimalesMinimo").prop("disabled", true);
                        $("#txtDecimalesMinimo").prop("");
                        $("#txtDecimalesMinimo").removeClass("requerido");
                    }

                    if (req.CALC_IFORMULA == '1')
                        $('#chkReglaIncremento').prop('checked', true);
                    else
                        $('#chkReglaIncr5emento').prop('checked', false);

                    if (req.CALC_IMINIMO == '1')
                        $('#chkMinimoIncremento').prop('checked', true);
                    else
                        $('#chkMinimoIncremento').prop('checked', false);

                    loadDataCaracteristica(0);
                    loadDataValor(0);
                    loadDataMatriz(0);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function obtenerMatrizValor() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblMatriz tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            ReglaValor[contador] = {
                OWNER: 'APD',
                CALRV_ID: id,
                VAL_FORMULA: $('#txtMatrizTarifa' + id).val(),
                VAL_MINIMUM: $('#txtMatrizMinimo' + id).val()
            };
            contador += 1;
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TarifaRegla/ObtenerMatrizValor',
        data: ReglaValor,
        success: function () {
            insertar();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

