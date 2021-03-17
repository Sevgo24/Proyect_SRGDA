var K_DIV_POPUP_ESCALA_RANGO = {
    DIV: "mvEscalaRangos",
    ANCHO: 540, ALTO: 230,
};
var K_DIV_POPUP_TIPO = { PORCENTAJE: "P", VALOR: 'V', };
var K_TIPO_ACUMULACION = { MODALIDAD: "M", TOTALES: 'T', };

$(function () {
    $('#txtRangoInicial').on("keypress", function (e) { return solonumeros(e); });
    $('#txtRangoFinal').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValor').on("keypress", function (e) { return solonumeros(e); });


    var Id = (GetQueryStringParams("id"));//REC_ID (V=>N)- MREC_ID(V=>A)
    //alert(Id);
    if (Id === undefined) {
        limpiarPuPup();
    }
    else {
        ObtenerDatos(Id);
    }

    // ** POPUP - RANGO ** // 
    $("#" + K_DIV_POPUP_ESCALA_RANGO.DIV).dialog({
        autoOpen: false, width: K_DIV_POPUP_ESCALA_RANGO.ANCHO, height: K_DIV_POPUP_ESCALA_RANGO.ALTO,
        buttons: {
            "Agregar": addDetalleEscalaRango,
            "Cancelar": function () { $("#" + K_DIV_POPUP_ESCALA_RANGO.DIV).dialog("close"); }
        },
        modal: true
    });
    $(".addEscalaRango").on("click", function () { AbrirPoPupEscalaRango(); });

    // ** BOTONES ** //
    $("#btnDescartar").on("click", function () {
        location.href = "../EscalaComisiones/";

    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../EscalaComisiones/Nuevo";
    }).button();

    $("#btnGrabar").on("click", function () {
        GrabarComision();
    }).button();

    limpiarPuPup();


});

// **  FUNCIONES | BOTONES ** //
function GrabarComision() {
    var msj = ''
    var estadoRequeridos = ValidarRequeridos();
    var estadpRbtAcumulado = $('[name="rbtTipoAcumulacion"]:checked').length;
    if (estadpRbtAcumulado <= 0) alert('Seleccione tipo de acumulación.');
    
    if (estadoRequeridos && estadpRbtAcumulado) {
        var ComisionEscala = {
            SET_ID: $("#txtId").val(),
            SET_DESC: $("#txtEscalaDescripcion").val(),
            SET_ACC: $('#chkAcumulaEscala').is(":checked") ? '1' : '0',
            SET_MOT: $("input[type='radio'].rbtTipoAcumulacion:checked").val(),
            SET_NTRANI: $("#txttranInicial").val(),
            SET_ITRANF: $("#txttranfinal").val(),
        }

        $.ajax({
            url: '../EscalaComisiones/Insertar',
            data: ComisionEscala,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    document.location.href = '../EscalaComisiones/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;

    } else {
        alert('Complete la información requerida.');
    }
}

function ObtenerDatos(id) {
    $.ajax({
        url: '../EscalaComisiones/Obtener',
        data: { Id: id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var ent = dato.data.Data;
                $("#txtId").val(ent.SET_ID);                
                $("#txtEscalaDescripcion").val(ent.SET_DESC);
                if (ent.SET_ACC == '1')
                    $("#chkAcumulaEscala").prop('checked', true);
                else
                    $("#chkAcumulaEscala").prop('checked', false);
                
                if (ent.SET_MOT == K_TIPO_ACUMULACION.MODALIDAD) {
                    $("#rbtModalidad").prop('checked', true); $("#rbtTotal").prop('checked', false);
                } else if (ent.SET_MOT == K_TIPO_ACUMULACION.TOTALES) {
                    $("#rbtModalidad").prop('checked', false); $("#rbtTotal").prop('checked', true);
                }
                $("#txttranInicial").val(ent.SET_NTRANI);
                $("#txttranfinal").val(ent.SET_ITRANF);
                loadDataEscalaRango(1);//Listar detalle
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;

}

// ** FUNCIONES ** //
function loadDataEscalaRango(accionActual) {
    loadDataEscalaRangoGridTmp('ListarEscalaRango', "#gridEscalaRango", accionActual);
}

function AbrirPoPupEscalaRango() {
    limpiarPuPup();
    $('#hidIdEscalaRango').val(0),
    $.ajax({
        url: '../EscalaComisiones/ObtieneUltimoOrden',
        type: 'GET',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var ent = dato.data.Data;
                $('#txtOrden').val(ent.Orden);
            } else {
                alert(dato.message);
            }
        }
    })
    $("#" + K_DIV_POPUP_ESCALA_RANGO.DIV).dialog("open");
}

var addDetalleEscalaRango = function () {
    if (validarPoPup() == 1) {
        var escala = {
            Id: $('#hidIdEscalaRango').val(),
            Orden: $('#txtOrden').val(),
            Desde: $('#txtRangoInicial').val(),
            Hasta: $('#txtRangoFinal').val(),
            TipoId: $("input[type='radio'].escalaValor:checked").val(),
            Valor: $('#txtValor').val(),
        }

        $.ajax({
            url: '../EscalaComisiones/AddEscalaRango',
            type: 'POST',
            data: escala,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataEscalaRango(1);
                    $("#" + K_DIV_POPUP_ESCALA_RANGO.DIV).dialog("close");
                } else if (dato.result == 0) {
                    //alert(dato.message);
                }
            }
        });
    }
};

function delAddEscalaRango(idDel) {
    $.ajax({
        url: '../EscalaComisiones/delAddEscalaRango',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataEscalaRango(1);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddEscalaRango(id) {
    limpiarPuPup();
    $.ajax({
        url: '../EscalaComisiones/ObtieneEscalaRangoTmp',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var ent = dato.data.Data;
                if (ent != null) {
                    $('#hidIdEscalaRango').val(ent.Id),
                    $('#txtOrden').val(ent.Orden);
                    $('#txtRangoInicial').val(ent.Hasta);
                    $('#txtRangoFinal').val(ent.Desde);
                    $('#txtValor').val(ent.Valor);
                    if (ent.TipoId == K_DIV_POPUP_TIPO.VALOR)
                        $('#rbtValor').prop('checked', true);
                    else
                        $('#rbtPorcentaje1').prop('checked', true);
                    $("#" + K_DIV_POPUP_ESCALA_RANGO.DIV).dialog("open");

                } else {
                    alert("No se pudo obtener la observación para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataEscalaRangoGridTmp(Controller, idGrilla, accionActual) {
    $.ajax({
        type: 'POST',
        data: { accion: accionActual },
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        },
        complete: function () {
        }
    });
}

function limpiarPuPup() {
    $('#txtOrden').val('');
    $('#txtRangoInicial').val('');
    $('#txtRangoFinal').val('');
    $('#txtValor').val('');
    $('#rbtValor').prop('checked', false); $('#rbtPorcentaje1').prop('checked', false);
};

function validarPoPup() {
    var estado = 1;
    var msj = '';
    var orden = $('#txtOrden').val();
    var RInicial = $('#txtRangoInicial').val();
    var RFinal = $('#txtRangoFinal').val();
    var Valor = $('#txtValor').val();

    if (orden == '') { estado = 0; msj += 'Ingrese el orden.'; }
    if (RInicial == '') { estado = 0; msj += '\r\nIngrese el rango inicial. '; }
    if (RFinal == '') { estado = 0; msj += '\r\nIngrese el rango final. '; }
    if ($('[name="escalaValor"]:checked').length <= 0) { estado = 0; msj += '\r\nSeleccione un  tipo de valor.'; }
    if (Valor == '') { estado = 0; msj += '\r\nIngrese el valor. '; }

    if (estado == 0) alert(msj);
    return estado;
}




