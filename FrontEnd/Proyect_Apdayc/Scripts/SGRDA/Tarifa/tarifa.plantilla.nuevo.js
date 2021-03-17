/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 155;
var K_WIDTH_VAL = 500;
var K_HEIGHT_VAL = 190;
var K_ID_POPUP_DIR = "mvDetalle";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_CARACTERISTICA_MENSAJE = "Se permite como maximo 5 caracteristicas.";
var K_CARACTERISTICA_ELIMIAR_MENSAJE = "La caracteristica sera eliminada con todos los valores. ¿Desea eliminar?";
var K_MENSAJE_NUM_CARACTERISTICA = "Se debe ingresar como minimo 1 caracteristica para grabar la plantilla.";
var K_DESCRIPCION_VALIDACION_MENSAJE = "La descripción ya existe, ingrese uno nuevo."
var K_ESTADO = { Abierto: 1, Pendiente: 2, Atendido: 3, Entregado: 4, Rendido: 5, Anulado: 6 };
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
    $("#hidValorCar").val(0);
    $('#txtValDesde').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValHasta').on("keypress", function (e) { return solonumeros(e); });
    //--------------------------------------------------------------------------------- 
    var id = (GetQueryStringParams("id"));
    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Tarifa Plantilla - Nuevo");
        $("#hidId").val(0);
        $("#hidOpcionEdit").val(0);
        $("#txtId").val(0);
        $("#trId").hide();
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Tarifa Plantilla - Modificar");
        $("#hidId").val(id);
        $("#hidOpcionEdit").val(1);
        $("#txtId").val(id);
        $("#trId").show();
        ObtenerDatos(id);
    }

    //--------------------------POPUP -----------------------------------------------
    $("#mvObservacion").dialog({
        autoOpen: false, width: K_WIDTH, height: K_HEIGHT,
        buttons: {
            "Grabar": addCaracteristica,
            "Cancelar": function () {
                $("#mvObservacion").dialog("close");
                $("#hidAccionMvObs").val("0");
                $("#hidEdicionObs").val("0");
                $("#hidValorCar").val(0);
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

    //--------------------------POPUP -----------------------------------------------
    
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
    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnRegresar").on("click", function () {
        location.href = "../TarifaPlantilla/";
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    loadDataCaracteristica(0);
});

function grabar() {
    var estado = true;
    var numCaracteristicas = $("#numVariable").val();
    var estadoRequeridos = ValidarRequeridos();
    var estadoDescripcion = validarDescripcion();

    if (numCaracteristicas == '0' || numCaracteristicas == '') {
        estado = false;
        $("#numVariable").css('border', '1px solid red');
        msgErrorDiv("divResultNumCar", K_MENSAJE_NUM_CARACTERISTICA);
    } else {
        $("#numVariable").css('border', '1px solid gray');
        msgErrorDiv("divResultNumCar", "");
    }

    if (estadoRequeridos && estado && estadoDescripcion) {
        msgError("");
        insertar();
    } else {
        msgError("Debe ingresar los campos requeridos ");
    }
};

function insertar() {
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

    var plantilla = {
        TEMP_ID: id,
        STARTS: $("#txtFecha").val(),
        TEMP_DESC: $("#txtDescripcion").val(),
        TEMP_NVAR: $("#numVariable").val()
    };

    alert(plantilla.TEMP_DESC);
    $.ajax({
        url: '../TarifaPlantilla/Insertar',
        data: plantilla,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert(dato.message);
                document.location.href = '../TarifaPlantilla/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../TarifaPlantilla/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    var d1 = $("#txtFecha").data("kendoDatePicker");
                    var valFecha = formatJSONDate(req.STARTS);
                    d1.value(valFecha);

                    $("#txtDescripcion").val(req.TEMP_DESC);
                    $("#numVariable").val(req.TEMP_NVAR);
                    loadDataCaracteristica(0);
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
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - CARACTERISTICA -------------------------------------  
function loadDataCaracteristica(id) {
    loadDataGridTmp('ListarCaracteristica', "#gridCaracteristica", id);
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
            Tramo: tramo,
            IdCaracteristicaAnt: $("#hidValorCar").val()
        };

        $.ajax({
            url: '../TarifaPlantilla/AddCaracteristica',
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
        $("#mvObservacion").dialog("close");
        $("#hidAccionMvObs").val("0");
        $("#hidEdicionObs").val("0");
        $("#hidEdicionObs").val("0");
        $("#hidValorCar").val(0);
        $('#ddlTipoCaracteristica').css({ 'border': '1px solid gray' });
    }
}

function delAddCaracteristica(idDel) {
    Confirmar(K_CARACTERISTICA_ELIMIAR_MENSAJE,
                       function () {

                           $.ajax({
                               url: '../TarifaPlantilla/DellAddCaracteristica',
                               type: 'POST',
                               data: { id: idDel },
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
                           return false;

                       },
                       function () {
                       },
                       'Confirmar'
                   );
}

function updAddCaracteristica(idUpd) {
    $.ajax({
        url: '../TarifaPlantilla/ObtieneCaracteristicaTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidValorCar").val(0);
                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(doc.Id);
                    $("#hidValorCar").val(doc.IdCaracteristica);
                    loadTarifaCaracteristica('ddlTipoCaracteristica', doc.IdCaracteristica);

                    if (doc.Tramo == 1)
                        $("#chkTramo").prop('checked', true);
                    else
                        $("#chkTramo").prop('checked', false);

                    $("#mvObservacion").dialog("open");
                } else {
                    alert("No se pudo obtener la caracteristica para editar.");
                }
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
            Descripcion: $("#txtDescVal").val().toUpperCase()
        };

        $.ajax({
            url: '../TarifaPlantilla/AddValor',
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
        url: '../TarifaPlantilla/DellAddValor',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var doc = dato.data.Data;
                loadDataCaracteristica(doc.IdCaracteristica); 1
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
        url: '../TarifaPlantilla/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: plantilla,
        async: false,
        success: function (response) {
            var dato = response;

            if (dato.result == 1) {
                if (dato.Code == 1) {
                    estado = true;
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return estado;
}

