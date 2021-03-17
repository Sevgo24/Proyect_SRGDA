/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
var K_TIPO_ERROR = {
    CODE_ERROR_PERSONALIZADO: 999,
    CODE_ERROR_SESION_TIME_OUT: 998,
    CODE_ERROR_NO_DATA: 997
}

var K_PAGINACION = {
    GENERAL:8,
    LISTAR_20: 20,
    LISTAR_15: 15,
    LISTAR_5: 5,
    LISTAR_7: 7,
    LISTAR_8: 8,
    LISTAR_10: 10
}

var K_ESTADO_ORDEN = false;

function validarRedirect(retorno) { if (retorno.isRedirect) { alertify.error("La sesion actual ha expirado."); setTimeout(function () { document.location.href = retorno.redirectUrl; }, 2000); return false;} }
function validarSesion() { $.ajax({ url: '../Login/ValidarSesion', type: 'POST', success: function (response) { var retorno = response; validarRedirect(retorno); } }); }
//alertify.error

///recorre todos los elementos con la clase requerido del css y detiene la ejecucion si el elemento
/// se encuentra vacio. dbs
var ValidarRequeridos = function () {

    var error = 0;
    $('.requerido').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    $('.requeridoLst').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgError("Debe ingresar los campos requeridos ");
        // $('#aviso').html('<br /><span style="color:red;">Debe ingresar los campos requeridos </span> <br />');
        return false;
    } else {
        msgError("");
        //$('#aviso').html('<br /><span style="color:red;"></span> <br />');
        return true;
    }
};
/*valida los controles que tengan la clase requerido o requeridoLst y muestra el mensaje el Id del div como parametro */
var ValidarRequeridos = function (contenedor) {
    var error = 0;
    $('.requerido').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    $('.requeridoLst').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorB(contenedor, "Debe ingresar los campos requeridos ");
        // $('#aviso').html('<br /><span style="color:red;">Debe ingresar los campos requeridos </span> <br />');
        return false;
    } else {
        msgErrorB(contenedor, "");
        //$('#aviso').html('<br /><span style="color:red;"></span> <br />');
        return true;
    }
};

/*valida los controles que tengan la clase requerido o requeridoLst y muestra el mensaje el Id del div como parametro */
var ValidarObligatorio = function (contenedor, divForm) {
    var error = 0;
    $('#' + divForm + ' .requerido').each(function (i, elem) {
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    $('#' + divForm + ' .requeridoLst').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });

    if (error > 0) {
        msgErrorB(contenedor, "Debe ingresar los campos requeridos ");
        return false;
    } else {
        msgErrorB(contenedor, "");
        return true;
    }
};




var ValidarRequeridosMV = function () {
    var error = 0;
    $('.requeridoMV').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    $('.requeridoLstMV').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorMV("Debe ingresar los campos requeridos ");
        // $('#aviso').html('<br /><span style="color:red;">Debe ingresar los campos requeridos </span> <br />');
        return false;
    } else {
        msgErrorMV("");
        //$('#aviso').html('<br /><span style="color:red;"></span> <br />');
        return true;
    }
};

var ValidarRequeridosOBS = function () {
    var error = 0;
    $('.requeridoOBS').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorOBS("Debe ingresar los campos requeridos ");
        return false;
    } else {
        msgErrorOBS("");
        return true;
    }
};
var ValidarRequeridosPM = function () {
    var error = 0;
    $('.requeridoPM').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorPM("Debe ingresar los campos requeridos ");
        return false;
    } else {
        msgErrorPM("");
        return true;
    }
};
var ValidarRequeridosTL = function () {
    var error = 0;
    $('.requeridoTL').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorTL("Debe ingresar los campos requeridos ");
        return false;
    } else {
        msgErrorTL("");
        return true;
    }
};
var ValidarRequeridosET = function () {
    var error = 0;
    $('.requeridoET').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($.trim($(elem).val()) == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    $('.requeridoLstET').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        // event.preventDefault();
        msgErrorET("Debe ingresar los campos requeridos ");
        return false;
    } else {
        msgErrorET("");
        return true;
    }
};
var ValidarRequeridosEdit = function () {
    var error = 0;
    $('.requeridoEdit').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        msgErrorEdit("Debe ingresar los campos requeridos ");
        //$('#avisoEdit').html('<br /><span style="color:red;">Debe ingresar los campos requeridos </span> <br />');
        return false;
    } else {
        msgErrorEdit("");
        //$('#avisoEdit').html('<br /><span style="color:red;"></span> <br />');
        return true;
    }
};

var ValidarRequeridosLst = function () {
    var error = 0;
    $('.requeridoLst').each(function (i, elem) {
        /// alert($(elem).val() );
        if ($(elem).val() == '000' || $(elem).val() == '00000' || $(elem).val() == '0') {
            $(elem).css({ 'border': '1px solid red' });
            error++;
        } else {
            $(elem).css({ 'border': '1px solid gray' });
        }
    });
    if (error > 0) {
        msgError("Debe seleccionar la lista desplegable requerida. ");
        // $('#aviso').html('<br /><span style="color:red;">Debe ingresar los campos requeridos </span> <br />');
        return false;
    } else {
        //$('#aviso').html('<br /><span style="color:red;"></span> <br />');
        msgErrorEdit("");
        return true;
    }
};
function msgError(message, control) {
    if (message == "") {
        $('#aviso').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#aviso').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgErrorMV(message, control) {
    if (message == "") {
        $('#avisoMV').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#avisoMV').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgErrorOBS(message, control) {
    if (message == "") {
        $('#avisoOBS').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#avisoOBS').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgErrorPM(message, control) {
    if (message == "") {
        $('#avisoPM').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#avisoPM').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgErrorTL(message, control) {
    if (message == "") {
        $('#avisoTL').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#avisoTL').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgErrorET(message, control) {
    if (message == "") {
        $('#avisoMVEntidad').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#avisoMVEntidad').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgError2(message, control) {
    if (message == "") {
        $('#aviso').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid gray' });
        }
    } else {
        $('#aviso').html('<br /><img src="/Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
};
function msgOk(message) {
    if (message == "") {
        $('#aviso').html('<br /><span style="color:green;"></span> <br />');

    } else {
        $('#aviso').html('<br /><img src="/Images/botones/ok.png" width=24/>&nbsp;&nbsp;<span style="color:green; font-weight:bold;">' + message + '</span> <br />');

    }
};
function msgOkMV(message) {
    if (message == "") {
        $('#avisoMV').html('<br /><span style="color:green;"></span> <br />');

    } else {
        $('#avisoMV').html('<br /><img src="/Images/botones/ok.png" width=24/>&nbsp;&nbsp;<span style="color:green; font-weight:bold;">' + message + '</span> <br />');

    }
};
function msgErrorEdit(message, control) {
    if (message == "") {
        $('#avisoEdit').html('<br /><span style="color:red;"></span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid gray' });
        }
    } else {
        $('#avisoEdit').html('<br /><img src="Images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
}
function msgOkEdit(message) {
    if (message == "") {
        $('#avisoEdit').html('<br /><span style="color:green;"></span> <br />');
    } else {
        $('#avisoEdit').html('<br /><img src="/Images/botones/ok.png" width=24 />&nbsp;&nbsp;<span style="color:green; font-weight:bold;">' + message + '</span> <br />');

    }
}

/// <summary>
/// Validamos el número de enteros y decimales
/// </summary>
/// <param name="el">Elemento que lanza el método</param>
/// <param name="evt">Evneto</param>
/// <param name="ints">Número de enteros permitidos</param>
/// <param name="decimals">Número de decimales permitidos</param>
function validateFloatKeyPress(el, evt, ints, decimals) {
    var comaAscii = 46;
    var coma = ".";

    // El punto lo cambiamos por la coma
    /*            if (evt.keyCode == 46) {
                    evt.keyCode = comaAscii;
                }
                */

    // Valores numéricos
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != comaAscii && charCode > 31
&& (charCode < 48 || charCode > 57)) {
        return false;
    }

    // Sólo una coma
    if (charCode == comaAscii) {
        if (el.value.indexOf(coma) !== -1) {
            return false;
        }

        return true;
    }

    // Determinamos si hay decimales o no
    if (el.value.indexOf(coma) == -1) {
        // Si no hay decimales, directamente comprobamos que el número que hay ya supero el número de enteros permitidos
        if (el.value.length >= ints) {
            return false;
        }
    }
    else {

        // Damos el foco al elemento
        el.focus();

        // Para obtener la posición del cursor, obtenemos el rango de la selección vacía
        var oSel = document.selection.createRange();

        // Movemos el inicio de la selección a la posición 0
        oSel.moveStart('character', -el.value.length);

        // La posición de caret es la longitud de la selección
        iCaretPos = oSel.text.length;

        // Distancia que hay hasta la coma
        var dec = el.value.indexOf(coma);

        // Si la posición es anterior a los decimales, el cursor está en la parte entera
        if (iCaretPos <= dec) {
            // Obtenemos la longitud que hay desde la posición 0 hasta la coma, y comparamos
            if (dec >= ints) {
                return false;
            }
        }
        else { // El cursor está en la parte decimal
            // Obtenemos la longitud de decimales (longitud total menos distancia hasta la coma menos el carácter coma)
            var numDecimals = el.value.length - dec - 1;

            if (numDecimals >= decimals) {
                return false;
            }
        }
    }

    return true;
}


function solonumeros(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    if (key == 46)
        return (target.value.length > 0 && target.value.indexOf(".") == -1);
    return (key <= 12 || (key >= 48 && key <= 57) || key == 0);
}


function msgErrorB(divMsg, message) {

    if (message == "") {
        $('#' + divMsg).html('<br /><span style="color:red;"></span> <br />');
        //if (control != undefined) {

        //    if (control == "grid" || control == "grid2") {
        //        $("#" + control).css({ 'border': '1px solid transparent' });
        //    } else {
        //        $("#" + control).css({ 'border': '1px solid gray' });
        //    }
        //}

       
    } else {
        $('#' + divMsg).html('<br /><img src="../images/botones/warning.png"  />&nbsp;&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span> <br />');
        //if (control != undefined) {
        //    $("#" + control).css({ 'border': '1px solid red' });
        //}
    }
}

function msgErrorDiv(divMsg, message, control) {

    if (message == "") {
        $('#' + divMsg).html('<span style="color:red;"></span>');
        if (control != undefined) {

            if (control == "grid" || control == "grid2") {
                $("#" + control).css({ 'border': '1px solid transparent' });
            } else {
                $("#" + control).css({ 'border': '1px solid gray' });
            }
        }
    } else {
        $('#' + divMsg).html('&nbsp;<span style="color:red; font-weight:bold;">' + message + '</span>');
        if (control != undefined) {
            $("#" + control).css({ 'border': '1px solid red' });
        }
    }
}

function msgOkB(divMsg, message) {
    if (message == "") {
        $('#' + divMsg).html('<br /><span style="color:green;"></span> <br />');
    } else {
        $('#' + divMsg).html('<br /><img src="../images/botones/ok.png" width=24/>&nbsp;&nbsp;<span style="color:green; font-weight:bold;">' + message + '</span> <br />');

    }
};

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}
function formatJSONDate(jsonDate) {
    /***si viene una fecha JSON de formato /Date(1224043200000)/*****/
    //var newDate = dateFormat(jsonDate, "mm/dd/yyyy");     
    var newDate = "";
    if (jsonDate != null) {
        newDate = new Date(parseInt(jsonDate.substr(6)));
    }
    return newDate;
}
function solonumeros(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    if (key == 46)
        return (target.value.length > 0 && target.value.indexOf(".") == -1);
    return (key <= 12 || (key >= 48 && key <= 57) || key == 0);
}

function validarEmail(email) {
    //re = /^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$/;
    re = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!re.exec(email)) {
        return false;
    } else {
        return true;
    }
}

function validarRedSocial(redsocial) {
    //re = /^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$/;
    re = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
    if (!re.exec(redsocial)) {
        return false;
    } else {
        return true;
    }
}

function validarDocumento(tipo, numero) {
    var documento = {
        tipoDocumento: tipo,
        numDocumento: numero
    };
    $.ajax({
        data: documento,
        url: '../General/ValidarDocumento',
        type: 'POST',
        succes: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result != 1) {
                return true;
            } else {
                return false;
            }
        }
    });
}

///OBTIENE LA DESCRIPCION DEL LA ENTIDAD POR EL ID ENVIADO Y LO PINTA EN EL CONTROL LABEL ENVIADO COMO PARAMETRO.
function ObtieneNombreEntidad(idSel, idLabelSetting, flg_esLabel) {
    if (idSel == null) idSel=0;
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                if (flg_esLabel == undefined || flg_esLabel == true)
                    $("#" + idLabelSetting).html(dato.valor);
                else
                    $("#" + idLabelSetting).val(dato.valor);
            } else if (dato.result == 0) {
                
                if (!((flg_esLabel == undefined || flg_esLabel == false) &&  idLabelSetting=="txtGrupoEmpresarial")) {
                    alert(dato.message);
                }
            }
        }

    });
}

function ObtenerNombreEstablecimiento(id, idLabelSetting) {
    $.ajax({
        url: '../General/ObtenerNombreEstablecimiento',
        type: 'POST',
        data: { codigoEst: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#" + idLabelSetting).html(dato.valor);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }
    });
}

function ObtenerRespXEstablecimiento(idEst, idLabelResponsable, idhidResponsable) {
    $.ajax({
        url: '../General/ObtenerRespXEstablecimiento',
        type: 'POST',
        data: { codigoEst: idEst },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var entidad = dato.data.Data;

                $("#" + idLabelResponsable).html($.trim(entidad.responsable));
                $("#" + idhidResponsable).val(entidad.idSocio);
                

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }
    });
}
function obtenerNombreTarifaLabels(idModalidad, idTemp,idLabelSetting,idHidCodigoTarifa) {
    $("#" + idLabelSetting).html("No hay Tarifa");
    $.ajax({
        url: '../General/ObtenerTarifaAsociada',
        data: { codModalidad: idModalidad, codTemp: idTemp },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                if (dato.data != null) {
                    var datos = dato.data.Data;
                    $("#" + idLabelSetting).html(datos.TarifaDesc);
                    if (idHidCodigoTarifa != undefined) {
                        $("#" + idHidCodigoTarifa).val(datos.idTarifa);
                    }
                } else {

                    if (idHidCodigoTarifa != undefined) {
                        $("#" + idHidCodigoTarifa).val(0);
                    }
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}
function obtenerNombreTarifa(idModalidad, idLabelSetting, idTextSetting) {
    $("#" + idLabelSetting).html("No hay Tarifa");
    if (idTextSetting != undefined) {
        $("#" + idTextSetting).val(0);
    }
    $.ajax({
        url: '../General/ObtenerTarifaAsociada',
        data: { codModalidad: idModalidad },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $("#" + idLabelSetting).html(datos.TarifaDesc);
                if (idTextSetting != undefined) {
                    $("#" + idTextSetting).val(datos.idTarifa);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}
/*funcion para mostrar el nombre de la modalidad luego de invocar a la busqueda general
addon dbalvis 20150129
idModalidad = id de la modalidad que va obtener.
idLabelSetting =  etiqueta donde mostrará el nombre de la modalidad.
idHiddenIdWF = hidden que contendrá el codifo del Workflow al que pertenece la modalidad seleccionada.
*/
function obtenerNombreModalidad(idModalidad, idLabelSetting,idHiddenIdWF) {
    $.ajax({
        data: { id: idModalidad },
        url: '../ModalidadUso/Obtener',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#" + idLabelSetting).html(dato.valor); 
                /*addon dbalvis 20150129 - obtiene el ID del WF correspondiente a la modalidad*/
                if (idHiddenIdWF != undefined) {
                    //alert(dato.Code);
                    if (dato.Code == null || dato.Code == 0 ) {
                        alert("La modalidad no tiene workflow asociado.");
                        $("#" + idHiddenIdWF).val("");
                    } else {
                        $("#" + idHiddenIdWF).val(dato.Code);
                    }
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
        }
    });
}

/*funcion para mostrar el idmodalida, hidtipouso, hidNivelIncidencia, hidsociedad, hidrepertorio, luego de invocar a la busqueda general.
hidtipouso: id del hidden que contendra el id de tipo uso, en caso de no requerir este hidtipouso enviar vacio
hidNivelIncidencia: id del hidden que contendra el id de Nivel de incidencia, en caso de no requerir este hidNivelIncidencia enviar vacio
hidsociedad: id del hidden que contendra el id de Sociedad, en caso de no requerir este hidsociedad enviar vacio
hidrepertorio: id del hidden que contendra el id de Repertorio, en caso de no requerir este hidrepertorio enviar vacio
*/
function obtenerModalidad(idModalidad, idLabelSetting, idTipoUso, idNivelIncidencia, idSociedad, idUsoRepertorio) {
    // $("#" + idLabelSetting).html("No hay Tarifa");
    $.ajax({
        data: { id: idModalidad },
        url: '../ModalidadUso/Obtener',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var modalidad = dato.data.Data;
                $("#" + idLabelSetting).html(dato.valor);
                if (idTipoUso != "")
                    loadTipoObra(idTipoUso, modalidad.MOD_USAGE);

                if (idNivelIncidencia != "")
                    loadTipoIncidencia(idNivelIncidencia, modalidad.MOD_INCID);

                if (idSociedad != "")
                    loadTipoSociedad(idSociedad, modalidad.MOD_SOC);

                if (idUsoRepertorio != "")
                    loadTipoRepertorio(idUsoRepertorio, modalidad.MOD_REPER);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }
    });
}

function ListarProcesoHtml(control, idEstado,idWrkf,idWrkfRef) {
    $.ajax({
        data: { idEstado: idEstado, idWrkf: idWrkf, idWrkfRef: idWrkfRef },
        url: '../General/ListarProcesoHtml',
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#" + control).html(dato.message);
            }else if (dato.result == 0) {
                $("#" + control).html(dato.message);
            }
        }
    });
}

function obtenerNombreTemporalidad(idTarifa, idLabelTempSetting, idTextTempSetting) {
    $("#" + idLabelTempSetting).html("No hay Temporalidad");
    if (idTextTempSetting != undefined) {
        $("#" + idTextTempSetting).val(0);
    }
    $.ajax({
        url: '../General/ObtenerTemporalidad',
        data: { idTarifa: idTarifa },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $("#" + idLabelTempSetting).html(datos.descripcion);

                if (idTextTempSetting != undefined) {
                    $("#" + idTextTempSetting).val(datos.codigo);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerNombreArtista(idArtita, idLabelSetting) {
    $.ajax({
        data: { id: idArtita },
        url: '../Artista/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) $("#" + idLabelSetting).html(dato.valor);
            else if (dato.result == 0) alert(dato.message);

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }

    });
}
function obtenerNombreShow(idShow, idLabelSetting) {
    $.ajax({
        data: { id: idShow },
        url: '../Show/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) $("#" + idLabelSetting).html(dato.valor);
            else  if (dato.result == 0) {
                alert(dato.message);
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }

    });
}

function obtenerEstadoInicialWF(idWF, callBack) {
    var retorno=0;
    $.ajax({
        data: { id: idWF },
        url: '../General/ObtenerEstadoInicial',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                retorno =  dato.valor;
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
            callBack(retorno);
        } 
    });

    return retorno;

     
}
/*funcion mejorada sobre obtenerNombreModalidad para mostrar el nombre de la modalidad luego de invocar a la busqueda general
addon dbalvis 20150129
idModalidad = id de la modalidad que va obtener.
idLabelSetting =  etiqueta donde mostrará el nombre de la modalidad.
idHiddenIdWF = hidden que contendrá el codifo del Workflow al que pertenece la modalidad seleccionada.
*/
function obtenerNombreModalidadB(param) {
    
    var retorno = { modalidadText: "-", idWorkFlow: 0, error:false,message:"" };
    if (param != null) {
        $.ajax({
            data: { id: param.idModalidad },
            url: '../ModalidadUso/Obtener',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    retorno.modalidadText = dato.valor;
                   
                        if (!(dato.Code == null || dato.Code == 0)) {
                            retorno.idWorkFlow = dato.Code;
                        }
                   
                } else if (dato.result == 0) {
                    retorno.error = true;
                    retorno.message = dato.message;
                }
                param.fncCallBack(retorno);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
            }
        });
    } else {
        alert("funcion obtenerNombreModalidadB necesita objeto como parametro.");

    }
    return retorno;
}

var LimpiarRequeridos = function (contenedor, divForm) {
    $('#' + divForm + ' .requerido').each(function (i, elem) { $(elem).css({ 'border': '1px solid gray' }); });
    $('#' + divForm + ' .requeridoLst').each(function (i, elem) { $(elem).css({ 'border': '1px solid gray' }); });
    msgErrorB(contenedor, "");
    return true;

};

function Visibility(elemento, esVisible) {

    if (esVisible) { 
        $("#" + elemento).css("display", "inline");
    } else {
        $("#" + elemento).css("display", "none");
    }

}

function ObtieneNombreShowXReport(idReport,callBack) {
    var retorno="";
    $.ajax({
        data: { idRep: idReport },
        url: '../General/ObtienNombreShow',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                retorno = dato.valor;
            } else if (dato.result == 0) {
                alert(dato.message);
            }
            callBack(retorno);
        }
        });
    return retorno;
}

QuitarBordeObligatorio = function (contenedor, divMsg) {
    var error = 0;
    $('#' + contenedor + ' .requerido').each(function (i, elem) {
            $(elem).css({ 'border': '1px solid gray' });
    });
    $('#' + contenedor + ' .requeridoLst').each(function (i, elem) {
            $(elem).css({ 'border': '1px solid gray' });
    });

    msgErrorB(divMsg, "");
        return true;
     
};

function ObtieneIdTipoDscto() {
    var retorno = "";
    $.ajax({
        url: '../General/ObtieneCodigoTipoEspecial',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                retorno = dato.Code;
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return retorno;
}

function validacionTraslape(fecini, fecfin) {
    var estado = false;
    var startDate = $("#" + fecini).data("kendoDatePicker").value();
    var endDate = $("#" + fecfin).data("kendoDatePicker").value();

    var diff = Math.round((endDate - startDate) / 1000 / 60 / 60 / 24); //Difference in days
    if (diff >= 0)
        estado = true;
    else
        alert('La fecha de inicio no puede ser mayor a la fecha final.');
    return estado;
}

function validarUbigeoXOficia(ubigeo) {
    var estado = false;
    $.ajax({
        url: '../General/ValidarUbigeoXOficia',
        type: 'GET',
        data: { ubigeo: ubigeo },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                estado = true;
            }
            else if (dato.result == 2) {
                alert(dato.message);
            } else if (dato.result == 6) {
                alert(dato.message);
            }
        }
    });
    return estado;
}
    //VALIDA COMBO 
    function validarOficinaReportedl() {
        var estado = false;
        $.ajax({
            url: '../General/ValidarOficinaReporteDL',
            type: 'GET',
            data: {},
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    estado = true;
                }
                else if (dato.result == 2) {
                    alert(dato.message);
                } else if (dato.result == 6) {
                    alert(dato.message);
                }
            }
        });
        return estado;
    }

//VALIDA RADIO
    function validarOficinaReporte() {
        var estado = false;
        $.ajax({
            url: '../General/ValidarOficinaReporte',
            type: 'GET',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    estado = true;
                }
                else if (dato.result == 2) {
                    alert(dato.message);
                } else if (dato.result == 6) {
                    alert(dato.message);
                }
            }
        });
        //alert(estado);
        return estado;
    }

    //VALIDACION DE FECHAS.
    function validate_fechaMayorQue(fechaInicial, fechaFinal) {
        valuesStart = fechaInicial.split("/");
        valuesEnd = fechaFinal.split("/");

        // Verificamos que la fecha no sea posterior a la actual
        var dateStart = new Date(valuesStart[2], (valuesStart[1] - 1), valuesStart[0]);
        var dateEnd = new Date(valuesEnd[2], (valuesEnd[1] - 1), valuesEnd[0]);
        if (dateStart > dateEnd) {
            alert("La Fecha Inicial No puede ser Mayor a la Fecha Final");
            return 0;
        }
        return 1;
    }
    //Validacion de Seleccion de Oficinas
    function ValidarSeleccionCombo(idoficina) {
        if (idoficina == 0) {
            alert("Seleccione Una Oficina");
            return 0;
        } else {
            return 1;
        }
    }

    //obtener NOMBRE DEL SOCIO
    function obtenerNombreSocioX(idSel, control) {
        $.ajax({
            data: { codigoBps: idSel },
            url: '../General/ObtenerNombreSocio',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    $("#" + control).html(dato.valor);
                    $("#hidCodigoGrupoEmpresarial").val(idSel);
                }
            }
        });
    }

    //obtener NOMBRE DEL SOCIO 
    function obtenerNombreDescPlantillaX(idSel, control) {
        $.ajax({
            data: { codigoDesc: idSel },
            url: '../General/ObtenerNombreDescuentoPlantilla',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    $("#" + control).html(dato.valor);
                    $("#hidPlantillaDes").val(idSel);
                }
            }
        });
    }

   
    function validarDocumento_Cobro(id) {
        var r = 0;
        var documento = {
            INV_ID: id
        }
        $.ajax({
            data: documento,
            url: '../General/ValidarDocumentoCobro',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    r = 1;
                } else if (dato.result == 0) {
                   r=0;
                }
            }
        });
        return r;
    }
    function Valida_Fecha_Factura_Para_NC(id) {
        var r = 0;
        var documento = {
            INV_ID: id
        }
        $.ajax({
            data: documento,
            url: '../General/Valida_Fecha_Factura_Para_NC',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                r = dato.result;
            }
        });
        return r;
    }