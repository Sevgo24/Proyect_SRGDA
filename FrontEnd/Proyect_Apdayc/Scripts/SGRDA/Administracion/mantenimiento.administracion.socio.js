var k_MENSAJES_ADMINISTRACION = {
    MODIFICADO: "EL SOCIO HA SIDO MODIFICADO POR OTRO USUARIO DATA QUALITY"
};

$(function () {
    //alert("WELCOME");
    //FECHA AUTORIZACION
    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecModInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecModFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecCreaInicial").data("kendoDatePicker").value();

    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecCreaFinal").data("kendoDatePicker").value();

    $("#txtFecModInicial").data("kendoDatePicker").value(fechaActual);
    var d2 = $("#txtFecModInicial").data("kendoDatePicker").value();

    $("#txtFecModFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN2 = $("#txtFecModFinal").data("kendoDatePicker").value();

    //limpiar
    LimpiarTodo();
    //botones ***********
    $("#btnBuscar").on("click", function () {
        ConsultarSocio();
    });
    $("#btnMarcaSocioPrinci").on("click", function () {
        SeleccionarSocioPrincipal();
    });
    $("#btnMarcaSocioInactivar").on("click", function () {
        SeleccionarSociosInactivar();
    });

    $("#btnLimpiar").on("click", function () {
        LimpiarTodo();
    });


    $("#btnEliminar").on("click", function () {
        var values = [];
        $('#tblLicencias tr').each(function () {
            var IdNro = $(this).find(".IDEstOri").html();

            if (!isNaN(IdNro) && IdNro != null) {
                var idEst = $(this).find(".IDEstOri").html();

                if ($('#chkEstOrigen' + idEst).is(':checked')) {

                    values.push(IdNro);
                    eliminar(IdNro);
                }
            }
        });
        if (values.length == 0) {
            alert("Seleccione usuario para eliminar.");
        }
        else {
            //loadData();
            //loadGrid();
            alert("Estados actualizado correctamente.");
        }
        //loadData();
        ConsultarSocio();
    });

    $("#btnAgruparSOcios").on("click", function () {

        Confirmar('SE REALIZARA LA AGRUPACION DE LOS SOCIOS SELECCIONADOS , (ESTA SEGURO(A) DE CONTINUAR ?',
            function () { AgruparSocios(); },
            function () { },
            'Confirmar');
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../Socio/Nuevo";
    });


    //OCULTAR
    $("#trListaSocio").hide();
    $("#trListaSociosInactivar").hide();
    $("#trListaSocioPrincipal").hide();
    $("#btnMarcaSocioPrinci").hide();
    $("#btnMarcaSocioInactivar").hide();
    $("#btnDesmarcar").hide();
    $("#trAgruparSsocios").hide();
    //$("#btnAgruparSOcios").hide();
    
});

function LimpiarTodo() {

    //validarUsuarioOficina();

    $("#txtcodsoc").val("");
    $("#txtraz").val("");
    $("#txtcodlic").val("");
    $("#txtnomsoc").val("");
    $("#txtapepat").val("");
    $("#txtapemat").val("");
    $("#txtnomest").val("");
    $("#txtruc").val("");
    $("#dllusumodif").prop('selectedIndex', 0);
    $("#chkConFechaCrea").prop("checked", false);
    $("#chkConFechaMod").prop("checked", false);
    $("#txtFecCreaInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecCreaFinal").data("kendoDatePicker").value(new Date());
    $("#txtFecModInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecModFinal").data("kendoDatePicker").value(new Date());

    $("#trListaSocio").hide();
    $("#grid").html('');
    $("#btnMarcaSocioPrinci").hide();
    $("#btnMarcaSocioInactivar").hide();
}

function ConsultarSocio() {
    var codsoc = $("#txtcodsoc").val() == "" ? 0 : $("#txtcodsoc").val();
    var ruc = $("#txtruc").val() == "" ? "" : $("#txtruc").val();
    var raz = $("#txtraz").val() == "" ? "" : $("#txtraz").val();
    var codlic = $("#txtcodlic").val() == "" ? 0 : $("#txtcodlic").val();
    var nombre = $("#txtnomsoc").val() == "" ? "" : $("#txtnomsoc").val();
    var paterno = $("#txtapepat").val() == "" ? "" : $("#txtapepat").val();
    var materno = $("#txtapemat").val() == "" ? "" : $("#txtapemat").val();
    var nomest = $("#txtnomest").val() == "" ? "" : $("#txtnomest").val();
    var usumodif = $("#dllusumodif").text() == "Seleccione" ? "" : $("#dllusumodif").text();
    var confechacrea = $("#chkConFechaCrea").is(':checked') == true ? 1 : 0;
    var fechacreaini = $("#txtFecCreaInicial").val();
    var fechacreafin= $("#txtFecCreaFinal").val();
    var confechamodi = $("#chkConFechaMod").is(':checked') == true ? 1 : 0;
    var fechamodcrea= $("#txtFecModInicial").val();
    var fechamodfin= $("#txtFecModFinal").val();

  $("#chkConFechaMod").is(':checked') == true ? 1 : 0
    $.ajax({
        data: {
            BPS_ID: codsoc, BPS_NAME: raz, BPS_FIRST_NAME: nombre, BPS_FATH_SURNAME: paterno, BPS_MOTH_SURNAME: materno, TAX_ID: ruc,
            LOG_USER_UPDAT: usumodif, CON_FECHA_CREA: confechacrea, FECHA_INI_CREA: fechacreaini, FECHA_FIN_CREA: fechacreafin,
            CON_FECHA_UPD: confechamodi, FECHA_INI_UPD: fechamodcrea, FECHA_FIN_UPD: fechamodfin, LIC_ID: codlic, EST_NAME: nomest
        },
        url: '../AdministracionSocio/ConsultaSociosADmin',
        type: 'POST',
        // async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {

                ListarSocio();

            }
        }

    });
}

function ListarSocio() {
    //alert("listando");
    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionSocio/ListarSociosAdministracion',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#trListaSocio").show();
                $("#btnMarcaSocioPrinci").show();
                $("#btnMarcaSocioInactivar").show();
                $("#grid").html(dato.message);
                // $("#grid2").html(dato.message);
                //$("#trListaLicencias").show();
                //$("#trseleccionar").show();
            }
        }

    });
}

function MODIFICAR(BPS_ID) {

    var resp = validar(BPS_ID);
    var mensaje = k_MENSAJES_ADMINISTRACION.MODIFICADO;

    if (resp == 1) {

        window.open('../Socio/Nuevo?set=' + BPS_ID, '_blank');

    } else {
        alert(mensaje);
    }
}

function MODUSU(BPS_ID) {
    var resp = validar(BPS_ID);
    var mensaje = k_MENSAJES_ADMINISTRACION.MODIFICADO;

    if (resp == 1) {

        window.open('../Derecho/Nuevo?set=' + BPS_ID, '_blank');

    } else {
        alert(mensaje);
    }
}

function SeleccionarSocioPrincipal() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();
    //alert("Selecciono");
    var ReglaValor = [];
    var contador = 0;
    var contador2 = 0;
    // LICENCIAS ORIGEN
    $('#tblLicencias tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstOri").html();

            var NomEst = $(this).find(".IDNomEstOri").html();
            //alert(NomEst);
            if ($('#chkEstOrigen' + idEst).is(':checked')) {
                //alert(idEst);
                ReglaValor[contador] = {
                    BPS_ID: idEst,
                    SOCIO: NomEst
                };
                contador += 1;
            }
        }
    });
    //SOCIO PRINCIPAL
    $('#tblLicenciasPrin tr').each(function () {
        var IdNro = $(this).find(".IDEstFin").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstFin").html();

            var NomEst = $(this).find(".IDNomEstFin").html();
            //alert(NomEst);
            if ($('#chkEstFin' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    BPS_ID: idEst,
                    SOCIO: NomEst
                };
                contador += 1;
                contador2 += 1;
            }
        }
    });
    var cant =ValidaSeleccionados();
    contador2 = contador2;  // VALIDA SELECCIONADOS HACE UN RECUENTO DE LA TABLA PRINCIPAL PARA SABER SI AUN HAY
    //alert(contador + '-' + contador2)
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    //alert(ReglaValor);
    if ((contador2 + contador > 0) && (((contador - contador2) == 1 && contador2 == 1) || (contador == 1 && contador2 == 0 && cant == 0) || ((contador - contador2) == 0 && contador2 == 1))) { // SOLO PUEDE HABER UN SOCIO PRINCIPAL
        //alert("PASA VALIDACION");
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../AdministracionSocio/SociosSeleccionadosPrincipal',
            data: ReglaValor,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ListarSocio();
                    ListarSocioPrincipal();
                    //ListarLicenciaDestino();

                }
            }

        });
    } else {
        alert("Seleccione al menos un SOCIO  Licencia para Continuar");
    }
}

function SeleccionarSociosInactivar() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();
    //alert("Selecciono");
    var ReglaValor = [];
    var contador = 0;
    var contador2 = 0;
    // LICENCIAS ORIGEN
    $('#tblLicencias tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstOri").html();

            var NomEst = $(this).find(".IDNomEstOri").html();
            //alert(NomEst);
            if ($('#chkEstOrigen' + idEst).is(':checked')) {
                //alert(idEst);
                ReglaValor[contador] = {
                    BPS_ID: idEst,
                    SOCIO: NomEst
                };
                contador += 1;
            }
        }
    });
    //SOCIO PRINCIPAL
    $('#tblLicenciasInac tr').each(function () {
        var IdNro = $(this).find(".IDEstIna").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstIna").html();

            var NomEst = $(this).find(".IDNomEstIna").html();
            //alert(NomEst);
            if ($('#chkEstIna' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    BPS_ID: idEst,
                    SOCIO: NomEst
                };
                contador += 1;
            }
        }
    });
  
    //alert(contador + '-' + contador2)
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    //alert(ReglaValor);
    if ((contador2 + contador > 0)) { // SOLO PUEDE HABER UN SOCIO PRINCIPAL
        //alert("PASA VALIDACION");
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../AdministracionSocio/SociosSeleccionadosInactivar',
            data: ReglaValor,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ListarSocio();
                    ListarSociosInactivar();
                    //ListarLicenciaDestino();

                }
            }

        });
    } else {
        alert("Seleccione al menos un SOCIO  Licencia para Continuar");
    }
}

function ListarSocioPrincipal() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();

    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionSocio/ListarSociosAdministracionPrincipal',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#trListaSocioPrincipal").show();
                $("#grid2").html(dato.message);
            }
        }

    });
}

function ListarSociosInactivar() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();

    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionSocio/ListarSociosAdministracionInactivarl',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#trListaSociosInactivar").show();
                //$("#btnAgruparSOcios").show();
                $("#trAgruparSsocios").show()
                $("#grid3").html(dato.message);
            }
        }

    });
}

function ValidaSeleccionados() {

    var contador2 = 0;

    $('#tblLicenciasPrin tr').each(function () {
        var IdNro = $(this).find(".IDEstFin").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstFin").html();

            var NomEst = $(this).find(".IDNomEstFin").html();
            //alert(NomEst);
            if ($('#chkEstFin' + idEst).is(':checked')) {

                //no hace nada
            }else{
                contador2 += 1;
            }
        }
    });
    return contador2;
}

function AgruparSocios() {

    var ACTEST = $("#chkActEst").is(':checked') ? 1 : 0;
    var ACTLIC = $("#chkActLic").is(':checked') ? 1 : 0;

    alert(ACTEST+ '-'+ ACTLIC);
    $.ajax({
        type: 'POST',
        data: { ACTEST: ACTEST, ACTLIC: ACTLIC }, //data:  ReglaValorL },
        url: '../AdministracionSocio/AgruparSocios',

        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                alert("SE AGRUPO EXITOSAMENTE");
                LimpiarTodo();
                LmpiarTodoTemporales();
                //ListarLicenciaDestino();

            } else {
                alert("DEBE DE HABER MINIMO UN SOCIO PRINCIPAL Y UN SOCIO A INACTIVAR ");
            }
        }

    });

}

function LmpiarTodoTemporales() {


    LimpiarTodo();

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../AdministracionSocio/LimpiarTodo',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $("#grid2").html('');
                $("#grid3").html('');
                $("#trListaSociosInactivar").hide();
                $("#trListaSocioPrincipal").hide();
                //ListarLicenciaDestino();

            }
        }

    });

}

function eliminar(idBPS) {

    var resp = validar(idBPS);
    var mensaje = k_MENSAJES_ADMINISTRACION.MODIFICADO;

    if (resp == 1) {
        var codigosDel = { codigo: idBPS };
        $.ajax({
            url: '../Socio/Eliminar',
            type: 'POST',
            data: codigosDel,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    //loadData();
                    ConsultarSocio();
                }
                // alert(dato.message);
            }
        });
    } else {
        alert(mensaje);
    }
}

function validaSocioModif(BPS_ID) {
    //alert("validando que socio no haya sido modificado por otro usuari QUALITY");
    var resp = 0;
    var mensaje = k_MENSAJES_ADMINISTRACION.MODIFICADO;
    
    resp = validar(BPS_ID);

    if(resp==1){
        $('#chkEstOrigen' + BPS_ID).is('checked');
    }
    else
    {
        $('#chkEstOrigen' + BPS_ID).prop("checked", false);
        alert(mensaje);
    }
}

function validar(BPS_ID) {
    var resp = 0;
    $.ajax({
        url: '../AdministracionSocio/ValidarSocioModif',
        type: 'POST',
        async: false,
        data: { BPS_ID: BPS_ID },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            resp = dato.result;
            //alert(resp);
        }
    });

    return resp;
}

function validarUsuarioOficina(BPS_ID) {
    var resp = 0;
    $.ajax({
        url: '../AdministracionSocio/ValidaUsuarioMOdif',
        type: 'POST',
        async: false,
        data: { BPS_ID: BPS_ID },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            resp = dato.result;
            //alert(resp);
        }
    });

    return resp;
}


function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
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