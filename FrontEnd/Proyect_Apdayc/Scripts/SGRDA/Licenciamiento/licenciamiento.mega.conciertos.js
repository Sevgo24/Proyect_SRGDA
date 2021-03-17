/*Fin Acciones del tab LOCALIDAD*/
//*** LOCALIDADES *************************************************************************
var MV_DIV_LOCALIDADES = 'divLocalidadesMatriz';
var DIV_MSG_AVISO_LOCALIDADES = "avisoLocalidades";
var MSG_AVISO_ELI_AFORO = '¿Desea eliminar el aforo?';
var MSG_AVISO_LIQUIDACION_AFORO = 'Complete en seleccionar el tipo de liquidación.';
var MSG_AVISO_VALIDAR_NUEVO = 'Complete los datos del anterior registro.';

//var K_DIV_CONTENDOR =
//    {
//        DIV_TAB_PRELIQUID: "divPreLiquid"
//    }
//var K_DIV_MESSAGE =
//    {
//        DIV_TAB_PRELIQUID: "avisoTabPreliquid"
//    }
//var K_DIV_POP_UP =
//    {
//        PRELIQUID: "MvPreLiquidar",
//        LIQUID: "MvLiquidar"
//    };


function initPopups2() {
    kendo.culture('es-PE');
    /*inicalizar popups*/

    $("#mvAdvertencia").dialog({
        close: function (event) {
            if (event.which) { returnPage(); }
        }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    });
    $("#MvPreLiquidar").dialog({ title: "SGRDA :: SELECCIONE AFORO ", autoOpen: false, width: 650, height: 300, modal: false });
    $("#MvLiquidar").dialog({ title: "SGRDA :: SELECCIONE AFORO ", autoOpen: false, width: 650, height: 300, modal: false });
    //$("#" + K_DIV_POP_UP.PRELIQUID).dialog({ autoOpen: false, width: 400, height: 280,/* buttons: { "Seleccionar": ListarPreLiquid, "Cancelar": function () { $(this).dialog("close"); } },*/ modal: true });


}
$(function () {
   
    initPopups2();
    //dropdownlist
    loadTipoAforo('ddllistaaforo', 0);
    loadTipoAforo('ddllistaaforoLiquid', 0);
    $(".verLocalidadAforo").on("click", function () { loadDataLocalidadAforo($(K_HID_KEYS.LICENCIA).val()); });
    $(".verLocalidades").on("click", function () { loadDataLocalidades($(K_HID_KEYS.LICENCIA).val()); });
    $(".addLocalidades").on("click", function () { addLocalidades(); });
  
    $("#btnPreLiquidacion").on("click", function () { $("#MvPreLiquidar").dialog("open"); }).button();

    $("#btnLiquidacion").on("click", function () { $("#MvLiquidar").dialog("open"); }).button();

    //cambio de list
    $('#ddllistaaforo').change(function () {
        var CAP_ID = $("#ddllistaaforo").val();
        var lic_id = $(K_HID_KEYS.LICENCIA).val();
        
        //$("#txtmontopreliquid").val(CAP_ID + '-' + lic_id);
        calculaMontoTipoAforo(CAP_ID, lic_id, '1');

    });

    $('#ddllistaaforoLiquid').change(function () {
        var CAP_ID = $("#ddllistaaforoLiquid").val();
        var lic_id = $(K_HID_KEYS.LICENCIA).val();

        //$("#txtmontopreliquid").val(CAP_ID + '-' + lic_id);
        calculaMontoTipoAforo(CAP_ID, lic_id, '2');

    });

    //grabar 
    $("#btngrabarpreliqui").on("click", function () { grabarAforoLicencia('P', '1'); }).button();
    $("#btngrabarliqui").on("click", function () { grabarAforoLicencia('L', '2'); }).button();

    //SI YA GRABO UNA PRELIQUIDACION O LIQUIDACION DEBE PODER LISTARLA
    
    $("#btnPreLiquidacion").on("click", function () { ObtenerLicenciaConteo('P'); }).button();//PRELIQUIDACION
    $("#btnLiquidacion").on("click", function () { ObtenerLicenciaConteo('L'); }).button();//LIQUIDACION
});

// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
// == Localidad == 1° Bandeja
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
function loadDataLocalidades(codLic) {
    loadDataGridTmpLocalidades('../LicenciaMegaConcierto/ListarLocalidades', "#gridLocalidades", codLic);
}

function loadDataGridTmpLocalidades(Controller, idGrilla, codLic) {
    $.ajax({
        data: { codigoLic: codLic },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
            //cargarTipoLocalidades(); //refactorizar
            addValSoloNumeroLocalidad();
        }
    });
}

function cambiosDatosLocales(id) {
    ActualizarLocalidades(id);
    var idLocalidad = $('#lblTipoLocalidad' + id).html();
    var bruto = $('#txtBruto' + id).val();
    var impuesto = $('#txtImpuesto' + id).val();
    var neto = $('#lblNeto' + id).html();
    //cambiosDatosValoresLocalidadMatriz(id, idLocalidad, bruto, impuesto, neto);
}

//function cambiosDatosValoresLocalidadMatriz(id, idLocalidad, valBruto, valImp, valNeto) {
//    $('#tblLocalidadAforo tr').each(function () {
//        var id = $(this).find(".Id").html();
//        var idLocalidadAforo = $(this).find(".IdLocalidad").html();

//        if (idLocalidadAforo != null && idLocalidadAforo == idLocalidad) {
//            $('#lblValBruto' + id).html(valBruto);
//            $('#lblValImp' + id).html(valImp);
//            $('#lblValNeto' + id).html(valNeto);

//            var tickets = $('#txtTickets' + id).val();
//            var bruto = tickets * valBruto;
//            var impuesto = tickets * valImp;
//            var neto = tickets * valNeto;

//            $('#lblBruto' + id).html(bruto.toFixed(2));
//            $('#lblImp' + id).html(impuesto.toFixed(2));
//            $('#lblNeto' + id).html(neto.toFixed(2));

//        }
//    });
//}

function cargarTipoLocalidades() {
    $('#tblLocalidades tr').each(function () {
        var Id = $(this).find(".Id").html();
        //alert(Id);
        if (!isNaN(Id)) {
            if (Id != null) {
                var localidadId = $('#lblTipoLocalidad' + Id).html();
                loadLocalidad('cboTipoLocalidad' + Id, '-- SELECCIONE --', localidadId);
            }
        }
    });
}

function addValSoloNumeroLocalidad() {
    $('#tblLocalidades tr').each(function () {
        var id = $(this).find(".Id").html();
        if (!isNaN(id)) {
            $('#txtPreVenta' + id).on("keypress", function (e) { return solonumeros(e); });
            $('#txtBruto' + id).on("keypress", function (e) { return solonumeros(e); });
            $('#txtImpuesto' + id).on("keypress", function (e) { return solonumeros(e); });
            $('#lblNeto' + id).on("keypress", function (e) { return solonumeros(e); });
        }
    });
}

function addLocalidades() {
    if (validarNuevoLocalidad()) {
        var idLic = $(K_HID_KEYS.LICENCIA).val()
        var Localidad = {
            LIC_ID: idLic,
            SEC_DESC: '',
            SEC_TICKETS: 0,
            SEC_VALUE: 0,
            SEC_GROSS: 0,
            SEC_TAXES: 0,
            SEC_NET: 0,
            SEC_COLOR: ''
        };

        $.ajax({
            url: '../LicenciaMegaConcierto/AddLocalidades',
            data: Localidad,
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    loadDataLocalidades(idLic);
                    loadDataLocalidadAforo(idLic);
                    //      eliminarMatriz(idLic);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
}

function ActualizarLocalidades(id) {
    $('#tblLocalidades tr').each(function () {
        var idAct = $(this).find(".Id").html();
        if (!isNaN(idAct)) {
            if (idAct == id) {//es igual

                var localidadId = $('#cboTipoLocalidad' + idAct).val();
                var preVta = 0;
                var desc = $("#txtSecDesc" + idAct).val();
                var igv = parseFloat(calularigv(id)) + 1; /// 1.18
                var preBruto = $('#txtBruto' + idAct).val();
                var preImp = $('#txtImpuesto' + idAct).val();
                var preNeto = preBruto / igv;
                preNeto = preNeto.toFixed(6);
                var color = $('#txtColor' + idAct).val();

                var idLic = $(K_HID_KEYS.LICENCIA).val();
                var Localidad = {
                    SEC_ID: idAct,
                    SEC_DESC: desc,
                    //   SEC_TICKETS : ticket,
                    SEC_VALUE: preVta,
                    SEC_GROSS: preBruto,
                    SEC_TAXES: preImp,
                    SEC_NET: preNeto,
                    SEC_COLOR: color
                };

                $.ajax({
                    url: '../LicenciaMegaConcierto/ActualizarLocalidad',
                    data: Localidad,
                    type: 'POST',
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato); /*add sysseg*/
                        if (dato.result == 1) {
                            loadDataLocalidades(idLic);
                        } else if (dato.result == 0) {
                            alert(dato.message);
                        }
                    }
                });

            }
        }
    });
}

function changeCboTipoLocalidad(id) {
    var idOriCbo = $('#lblTipoLocalidad' + id).html();
    var idValSelCbo = $('#cboTipoLocalidad' + id).val();
    var estadoExiste = validarExistLocalidad(idValSelCbo, id);

    if (estadoExiste) {
        $('#cboTipoLocalidad' + id).val(idOriCbo);
        alert('La localidad seleccionada ya existe.');
    }
    else {
        var localidad = $('#cboTipoLocalidad' + id + ' option:selected').text();
        $('#lblTipoLocalidad' + id).html(idValSelCbo);
        ActualizarLocalidades(id);
        //  cambiosDatosCboLocalidadMatriz(id, localidad);
    }
}

function validarExistLocalidad(idvalselcbo, idcbo) {
    var estado = false;
    $('#tblLocalidades tr').each(function () {
        var id = $(this).find(".Id").html();
        if (id != idcbo) {
            if (!isNaN(id)) {
                if (id != null) {
                    var cbota = $('#cboTipoLocalidad' + id).val();
                    if (cbota == idvalselcbo)
                        estado = true;
                }
            }
        }
    });
    return estado;
}

function eliminarLocalidad(id) {
    $.ajax({
        url: '../LicenciaMegaConcierto/EliminarLocalidad',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var idLic = $(K_HID_KEYS.LICENCIA).val();
                loadDataLocalidades(idLic);
                loadDataLocalidadAforo(idLic);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function validarNuevoLocalidad() {
    var result = false;
    var cont = 0;
    $('#tblLocalidades tr').each(function () {
        var id = $(this).find(".Id").html();
        if (id != null) {
            var cod = $('#lblTipoLocalidad' + id).html();
            if (cod == 0) {
                cont += 1;
            }
        }
    });

    if (cont > 0)
        alert(MSG_AVISO_VALIDAR_NUEVO);
    else
        result = true;
    return result;
}

function calcularMontosLocalidad(id) {

    var preVenta = 0;
    var igv = parseFloat(calularigv(id)) + 1; /// 1.18
    var valBruto = parseFloat($('#txtBruto' + id).val());
    //alert(igv);
    var valImp = valBruto - (valBruto / igv);
    $('#txtImpuesto' + id).val(valImp);
    var neto = valBruto - valImp;
    $('#lblNeto' + id).html(neto.toFixed(2));
    cambiosDatosLocales(id);
}


// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
// == Aforo == 2° bandeja
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
function loadDataLocalidadAforo(codLic) {
    loadDataGridTmpLocalidadAforo('../LicenciaMegaConcierto/ListarLocalidadAforo', "#gridAforo", codLic);
}

function loadDataGridTmpLocalidadAforo(Controller, idGrilla, codLic) {
    $.ajax({
        data: { codigoLic: codLic },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
            cargarTipoAforo();  // refactorizar
            addValSoloNumeroAforo();
        }
    });
}

function cambiosDatosAforo(idLocalidadAforo) {
 //   var idLocalidadAforo = $("#idLocalidad" + idLocalidadAforo).html();

    //$('#tblLocalidades tr').each(function () {
    //    var id = $(this).find(".Id").html();
    //    alert(id);
    //    var idLocalidad = $(this).find(".IdLocalidad").html();
    //    alert(idLocalidad);

    //    var neto2 = parseFloat($('#idLocalidad' + id).html());
    //    alert(neto2);


    //    if (idLocalidad != null && idLocalidad == idLocalidadAforo) {
    //        var neto = parseFloat($('#lblValNeto' + id).html());
    //        alert(neto);
    //        //$('#lblNeto' + id).html(neto.toFixed(2));
    //    }

    //});

    ActualizarLocalidadAforo(idLocalidadAforo);
}

//Tipo Aforo - Dropdownlista
function changeCboTA(id) {
    var idOriCbo = $('#lblAforo' + id).html();
    var idValSelCbo = $('#cboTA' + id).val();
    var estadoExiste = validarExistTipoAforo(idValSelCbo, id);
    if (estadoExiste) {
        $('#cboTA' + id).val(idOriCbo);
        alert('El aforo seleccionado ya existe.');
    }
    else {
        $('#lblAforo' + id).html(idValSelCbo);
        var aforo = $('#cboTA' + id + ' option:selected').text();
        ActualizarLocalidadAforo(id);
        cambiosDatosAforoMatriz(id, aforo);
    }
}

function changeLiquidar(id) {
    ActualizarLocalidadAforo(id);
}

function validarExistTipoAforo(idValSelCbo, idCbo) {
    var estado = false;
    $('#tblLocalidadAforo tr').each(function () {
        var Id = $(this).find(".Id").html();
        if (Id != idCbo) {
            if (!isNaN(Id)) {
                if (Id != null) {
                    var cboTa = $('#cboTA' + Id).val();
                    if (cboTa == idValSelCbo)
                        estado = true;
                }
            }
        }
    });
    return estado;
}

function cargarTipoAforo() {
    $('#tblLocalidadAforo tr').each(function () {
        var Id = $(this).find(".Id").html();
        if (!isNaN(Id)) {
            if (Id != null) {
                var AforoId = $('#lblAforo' + Id).html();
                loadTipoAforo('cboTA' + Id, '-- SELECCIONE --', AforoId);
            }
        }
    });
}

function addValSoloNumeroAforo() {
    $('#tblLocalidadAforo tr').each(function () {
        var id = $(this).find(".Id").html();
        if (!isNaN(id)) {
            $('#txtCapTickets' + id).soloNumEnteros();
            $('#txtCapTicketsV' + id).soloNumEnteros();
        }
    });
}

function validarRadioButtonLiquidar() {
    var result = false;
    var cont = 0;
    var acu = 0;

    $('#tblLocalidadAforo tr').each(function () {
        var id = $(this).find(".Id").html();
        if (id != null) {
            cont += 1;
            if ($('#chkPLiquidar' + id).is(':checked'))
                acu += 1;
            if ($('#chkLiquidar' + id).is(':checked'))
                acu += 1;
        }
    });

    if (cont == acu)
        result = true;
    else
        alert(MSG_AVISO_LIQUIDACION_AFORO);

    return result;
}

function addLocalidadAforo() {
    if (validarNuevoAforo()) {
        var idLic = $(K_HID_KEYS.LICENCIA).val()
        var aforo = {
            LIC_ID: idLic,
            CAP_ID: 0,
            CAP_IPRE: false,
            CAP_ILIQ: false,
            CAP_TICKETS: 0,
            CAP_TICKETSV: 0
        };

        $.ajax({
            url: 'AddLocalidadAforo',
            data: aforo,
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    loadDataLocalidadAforo(idLic);
                    eliminarMatriz(idLic);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
}

function ActualizarLocalidadAforo(id) {

    //alert();
    //$('#tblLocalidadAforo tr').each(function () {
    //    var idAct = $(this).find(".Id").html();
    //    if (!isNaN(idAct)) {
    //        if (idAct == id) {//es igual

                //var capTickets = $('#txtCapTickets' + id).val();
                //var capTicketsV = $('#txtCapTicketsV' + id).val();
                var txtPreLiqTickets = $('#txtPreLiqTickets' + id).val();
                var idLic = $(K_HID_KEYS.LICENCIA).val();
                var aforo = {
                    ACOUNT_ID: id,
                    LIC_ID: idLic,
                  //   CAP_ID: TA,
                //    CAP_IPRE: preLiq,
                 //   CAP_ILIQ: Liq,
                    TICKET_PRE: txtPreLiqTickets,
                  //  CAP_TICKETSV: capTicketsV
                };

                $.ajax({
                    url: '../LicenciaMegaConcierto/ActualizarLocalidadAforo',
                    data: aforo,
                    type: 'POST',
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato); /*add sysseg*/
                        if (dato.result == 1) {
                            loadDataLocalidadAforo(idLic);
                        } else if (dato.result == 0) {
                            alert(dato.message);
                        }
                    }
                });

    //        }
    //    }
    //});


}

function eliminarAforo(id) {
    Confirmar(MSG_AVISO_ELI_AFORO,
               function () {
                   delAforo(id);
               },
               function () {
               },
               'Confirmar'
           )
}

function delAforo(id) {
    $.ajax({
        url: '../Licencia/EliminarLocalidadAforo',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var idLic = $(K_HID_KEYS.LICENCIA).val();
                loadDataLocalidadAforo(idLic);
                eliminarMatriz(idLic);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function validarNuevoAforo() {
    var result = false;
    var cont = 0;
    $('#tblLocalidadAforo tr').each(function () {
        var id = $(this).find(".Id").html();
        if (id != null) {
            var cod = $('#lblAforo' + id).html();
            if (cod == 0) {
                cont += 1;
            }
        }
    });

    if (cont > 0)
        alert(MSG_AVISO_VALIDAR_NUEVO);
    else
        result = true;
    return result;
}

/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX */

function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            Si: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            No: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}

jQuery.fn.soloNumEnteros = function () {

    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                // numbers   
                key >= 48 && key <= 57 ||
                // Numeric keypad
                key >= 96 && key <= 105 ||
                // comma, period and minus, . on keypad
                //key == 190 || key == 188 || key == 109 || key == 110 ||
                // Backspace and Tab and Enter
               key == 8 || key == 9 || key == 13 ||
                // Home and End
               key == 35 || key == 36 ||
                // left and right arrows
               key == 37 || key == 39 ||
                // Del and Ins
               key == 46 || key == 45)
                return true;

            return false;
        });
    });
}

function ListarPreLiquid() {
}

function initLoadPreLiquid(idPopup, idContenedorCtrl, idContenedorMsg) {
    //msgErrorB(idContenedorMsg, "");
    //limpiarDescuento();
    ////  if (ValidarObligatorio(idContenedorMsg, idContenedorCtrl)) {

    ////COMUN.DROPDOWNLIST.JS
    //loadTipoDescuento("ddlTipoDescuento", 0);
    //$('#ddlDescuento  option').remove();
    //$('#ddlDescuento').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $("#" + idPopup).dialog("open");

    //$("#ddlDescuento").show();
    //$("#txtDescuentoEspecial").hide();
    //$("#txtDescuentoEspecial").removeClass('requerido');
    //$("#txtDescuentoEspecial").css('border-color', 'gray');
    //$("#txtValorDscto").hide();
    //$("#txtValorDscto").removeClass('requerido');
    //$("#txtValorDscto").css('border-color', 'gray');
    //$("#ddlDescuento").addClass('requeridoLst');


    //aqui
    // }


}
function calculaMontoTipoAforo(cap_id,lic_id,control) {

    
    $.ajax({
        url: '../LicenciaMegaConcierto/CalculaMontoLiquidarAforo',
        data: { CAP_ID: cap_id, lic_id: lic_id },
        type:'POST',
        success: function (response) {
        var dato = response;
        validarRedirect(dato); /*add sysseg*/
        if (dato.result == 1) {
            //alert(control);
            if(control=='1')
                $("#txtmontopreliquid").val(dato.valor);
            else
                $("#txtmontoliquid").val(dato.valor);

        } else if (dato.result == 0) {
            alert(dato.message);
        }
    }

    });
}

function grabarAforoLicencia(tipo, control) {
    //alert(control);
    if (control == '1') {
        var capid = $("#ddllistaaforo").val();//tipo de aforo
        var total = $("#txtmontopreliquid").val();
        //alert(capid + '-' + total);
    } else {
        var capid = $("#ddllistaaforoLiquid").val();//tipo de aforo
        var total = $("#txtmontoliquid").val();
        //alert(capid + '-' + total);
    }
 
    var licencia = $(K_HID_KEYS.LICENCIA).val();//licencia
    var tipo = tipo;
   

    $.ajax({
        url: '../LicenciaMegaConcierto/insertaAforoLic',
        data: { licid: licencia, capid: capid, cap_iprelq: tipo, total: total },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                alert('GRABO CORRECTAMENTE');
                $("#MvPreLiquidar").dialog("close");
                $("#MvLiquidar").dialog("close");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }

    });
}

function ObtenerLicenciaConteo(tipo) {
    var licencia = $(K_HID_KEYS.LICENCIA).val();//licencia

    $.ajax({
        url: '../LicenciaMegaConcierto/listarLicenciaConteo',
        data: { licid: licencia, tipo: tipo },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var licenciaconteo = dato.data.Data;
                    if (licenciaconteo.CAP_IPRELQ == 'P') {
                        $("#lblcalculopreliquid").val(parseFloat(licenciaconteo.TOTAL_IPRELQ));
                        $("#lblDescripcionpreliquid").val(licenciaconteo.CAP_DESC);
                    } else if (licenciaconteo.CAP_IPRELQ == 'L') {
                        $("#lblDescripcionliquid").val(licenciaconteo.CAP_DESC);
                        $("#lblcalculoliquid").val(parseFloat(licenciaconteo.TOTAL_IPRELQ));
                    }
            } else if (dato.result == 0) {
                //alert(dato.message);
                $("#lblDescripcionpreliquid").val('AUN NO SE HA SELECCIONADO');
                $("#lblDescripcionliquid").val('AUN NO SE HA SELECCIONADO');

                $("#lblcalculoliquid").val('0');
                $("#lblcalculopreliquid").val('0');
            }
        }

    });
}


//calcula igv para realizar la resta respectiva con el monto bruto
function calularigv(id) {
    var division = '-1';
    var igv = null;
    $.ajax({
        url: '../LicenciaMegaConcierto/ObtenerIGV',
        async: false,
        data: { DIVISION: division },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                igv = dato.valor;
            } else {
            }
        }
    });
    return igv;
}
//GENERAR AUTORIZACION MEGACONCIERTO

//function imprimirautorizacion(plid) {
//    var idRef = $(K_HID_KEYS.LICENCIA).val();
//    var idObj = 20160;
//    var idTrace = 0;
//    //alert('licencia ' + idlic + ' periodo ' + lic_pl_id);
//    //GenerarFormatoJson
//    $.ajax({
//        url: '../Formatos/GenerarFormatoJson',
//        async: false,
//        data: { idObj: idObj, idTrace: idTrace, idRef: idRef },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            if (dato.result == 1 || dato.result == 0) {
//                verPlanillaPDF(dato.result, dato.message);
//                if (dato.result == 1) retorno = true;
//            }
//            //callback(retorno);
//        }
//    });

//}

////VER PLANILLA
//var verPlanillaPDF = function (result, message) {
//    $("#lblResultGenPDF").html('');
//    if (result == 1) {
//        var url = message;
//        $("#lblResultGenPDF").attr("display", "none");
//        $("#ifContenedorFormato").attr("display", "inline");
//        $("#ifContenedorFormato").attr("src", url);
//        //$("#ifContenedorFormato").attr("src", 'C:/inetpub/wwwroot/Documentos/20151223144111_Omiso248H.pdf');
//    } else {
//        $("#lblResultGenPDF").attr("display", "inline");
//        $("#ifContenedorFormato").attr("display", "none");
//        $('#ifContenedorFormato').attr('src', '');
//        $("#lblResultGenPDF").html(message);
//    }
//    $("#mvEjecutarProceso").dialog("open");
//};