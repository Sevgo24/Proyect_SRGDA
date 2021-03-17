var K_COLOR_MOD = '#52a7b0';
$(function () {


    mvInitBuscarSocioAdministracion({ container: "ContenedormvBuscarSocioAdministracion", idButtonToSearch: "btnElegirSocio", idDivMV: "mvBuscarSocioAdministracion", idLabelToSearch: "lblResponsable" });

    loadDivisionTipo('ddTipoDivision', '0');
    $("#ddTipoDivision").on("change", function () {
        var tipo = $("#ddTipoDivision").val();
        loadDivisionXTipo('ddDivision', tipo, 'TODOS');
    });


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

    LimpiarTodo();
    //---------------------- BOTONES ---------------------------------------

    //botones ***********
    $("#btnBuscar").on("click", function () {
        ConsultarEstablecimiento();
    });
    $("#btnMarcaEstPrinci").on("click", function () {
        SeleccionarEstablecimientoPrincipal();
    });
    $("#btnMarcaEstInactivar").on("click", function () {
        SeleccionarEstablecimientosInactivar();
    });
    $("#btnLimpiar").on("click", function () {
        LimpiarTodo();
    });


    $("#btnAgruparEstablecimientos").on("click", function () {

        Confirmar('SE REALIZARA LA AGRUPACION DE LOS ESTABLECIMIENTOS SELECCIONADOS , (ESTA SEGURO(A) DE CONTINUAR ?',
            function () { AgruparEstablecimientos(); },
            function () { },
            'Confirmar');
    });
    $("#btnNuevo").on("click", function () {
        location.href = "../Establecimiento/Create";
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
        ConsultarEstablecimiento();
    });
    //---------------------------------------------------------------------



});


function LimpiarTodo() {

}

function ConsultarEstablecimiento() {
    var codest = $("#txtcodest").val() == "" ? 0 : $("#txtcodest").val();
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
    var fechacreafin = $("#txtFecCreaFinal").val();
    var confechamodi = $("#chkConFechaMod").is(':checked') == true ? 1 : 0;
    var fechamodcrea = $("#txtFecModInicial").val();
    var fechamodfin = $("#txtFecModFinal").val();
    var division=  $("#ddDivision").val() == "" ? 0 : $("#ddDivision").val();
    var departamento=  $("#ddlSubTipo1").val() == "" ? 0 : $("#ddlSubTipo1").val();
    var provincia = $("#ddlSubTipo2").val() == "" ? 0 : $("#ddlSubTipo2").val();
    var distrito = $("#ddlSubTipo3").val() == "" ? 0 : $("#ddlSubTipo3").val();

    //$("#chkConFechaMod").is(':checked') == true ? 1 : 0
    $.ajax({
        data: {
            EST_ID: codest, BPS_NAME: raz, BPS_FIRST_NAME: nombre, BPS_FATH_SURNAME: paterno, BPS_MOTH_SURNAME: materno, TAX_ID: ruc,
            LOG_USER_UPDAT: usumodif, CON_FECHA_CREA: confechacrea, FECHA_INI_CREA: fechacreaini, FECHA_FIN_CREA: fechacreafin,
            CON_FECHA_UPD: confechamodi, FECHA_INI_UPD: fechamodcrea, FECHA_FIN_UPD: fechamodfin, LIC_ID: codlic, EST_NAME: nomest,
            DIV_ID: division, DEP_ID: departamento, PROV_ID: provincia, DIST_ID: distrito
            
        },
        url: '../AdministracionEstablecimiento/ConsultaEstablecimientosAdministracion',
        type: 'POST',
        // async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {

                ListarEstablecimiento();

            } else {
                alert(dato.message);
            }
        }

    });
}



function ListarEstablecimiento() {
    //alert("listando");
    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionEstablecimiento/ListaEstablecimientosAdministracion',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#trListaEstable").show();
                $("#btnMarcaEstPrinci").show();
                $("#btnMarcaEstInactivar").show();
                $("#btnElegirSocio").show();
                $("#grid").html(dato.message);
                // $("#grid2").html(dato.message);
                //$("#trListaLicencias").show();
                //$("#trseleccionar").show();
            }
        }

    });
}



function SeleccionarEstablecimientoPrincipal() {
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
                    EST_ID: idEst,
                    EST_NAME: NomEst
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
                    EST_ID: idEst,
                    EST_NAME: NomEst
                };
                contador += 1;
                contador2 += 1;
            }
        }
    });
    var cant = ValidaSeleccionados();
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
            url: '../AdministracionEstablecimiento/EstablecimientosSeleccionadosPrincipal',
            data: ReglaValor,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ListarEstablecimiento();
                    ListarEstablecimientoPrincipal();

                }
            }

        });
    } else {
        alert("Seleccione al menos un SOCIO  Licencia para Continuar");
    }
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
            } else {
                contador2 += 1;
            }
        }
    });
    return contador2;
}


function ListarEstablecimientoPrincipal() {
    //alert("listando");
    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionEstablecimiento/ListarEstablecimientoAdministracionPrincipal',
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


function SeleccionarEstablecimientosInactivar() {
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
                    EST_ID: idEst,
                    EST_NAME: NomEst
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
                    EST_ID: idEst,
                    EST_NAME: NomEst
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
            url: '../AdministracionEstablecimiento/EstablecimientoSeleccionadosInactivar',
            data: ReglaValor,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ListarEstablecimiento();
                    ListarEstablecimientoInactivar();
                    //ListarLicenciaDestino();

                }
            }

        });
    } else {
        alert("Seleccione al menos un ESTABLECIMIENTO  para Continuar");
    }
}


function ListarEstablecimientoInactivar() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();

    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../AdministracionEstablecimiento/ListarEstablecimientoAdministracionInactivar',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#trListaSociosInactivar").show();
                //$("#btnAgruparSOcios").show();
                $("#trAgruparEstable").show()
                $("#grid3").html(dato.message);
            }
        }

    });
}


function validaSocioModif(est_id) {

}

function LimpiarTodo() {

    //validarUsuarioOficina();

    $("#txtcodest").val("");
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

    $("#grid").html('');
    $("#btnMarcaEstPrinci").hide();
    $("#btnMarcaEstInactivar").hide();
    $("#btnElegirSocio").hide();

    // ----------------------- INACTIVANDO TR Y BOTONES -------------------
    $("#trListaEstable").hide();
    $("#trListaSocioPrincipal").hide();
    $("#trListaSociosInactivar").hide();
    $("#trAgruparEstable").hide();
    //---------------------------------------------------------------------
}


function AgruparEstablecimientos() {

    var ACTEST = $("#chkActEst").is(':checked') ? 1 : 0;
    var ACTLIC = $("#chkActLic").is(':checked') ? 1 : 0;

    //alert(ACTEST + '-' + ACTLIC);
    $.ajax({
        type: 'POST',
        data: { ACTEST: ACTEST, ACTLIC: ACTLIC }, //data:  ReglaValorL },
        url: '../AdministracionEstablecimiento/AgruparEstablecimientos',

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
                alert("DEBE DE HABER MINIMO UN ESTABLECIMIENTO PRINCIPAL Y UN ESTABLECIMIENTO A INACTIVAR ");
            }
        }

    });

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


function eliminar(idEst) {
    var id = { idEstablecimiento: idEst };
    $.ajax({
        url: '../EstablecimientoAdministracion/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert("Estados actualizado correctamente.");
                //loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function MODIFICAR(EST_ID) {

    //var resp = validar(BPS_ID);
    //var mensaje = k_MENSAJES_ADMINISTRACION.MODIFICADO;

   // if (resp == 1) {
    $("#" + EST_ID).css('background-color', K_COLOR_MOD);
    window.open('../Establecimiento/Create?id=' + EST_ID, '_blank');

   // } else {
    //    alert(mensaje);
   // }
}

function LmpiarTodoTemporales() {


    LimpiarTodo();

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../AdministracionEstablecimiento/LimpiarTodo',
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

function validarEstablecimientoModificado(EST_ID) {
    var resp = 0;
    $.ajax({
        url: '../AdministracionEstablecimiento/ValidaEstablecimientooMOdif',
        type: 'POST',
        async: false,
        data: { EST_ID: EST_ID },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            resp = dato.result;
            //alert(resp);
        }
    });

    return resp;
}



function ModificarSocioEstSeleccionados(CodigoSocio) {
    var contador = 0;
    var ReglaValor = [];
    //alert(CodigoSocio);
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
                    EST_ID: idEst,
                    EST_NAME: NomEst
                };
                contador += 1;
            }
        }
    });


    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../AdministracionEstablecimiento/ObtieneEstablecimientosporSocioSeleccionado',
            data: ReglaValor,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ModificarEstablecimientosSocio(CodigoSocio);

                    //ListarEstablecimiento();
                    //ListarEstablecimientoPrincipal();
                    //alert(dato.message);

                } else {
                    alert("OCURRIO UN PROBLEMA AL PROCESAR ESTA SOLICITUD , COMUNIQUESE CON EL ADMINISTRADOR RESPONSABLE DEL MODULO");
                }
            }

        });
    } else {
        alert('Debe seleccionar minimo un Establecimiento');
    }
}

function ModificarEstablecimientosSocio(CodigoSocio) {

    $.ajax({
        url: '../AdministracionEstablecimiento/ModificaEstablecimientosporSocioSeleccionado',
        type: 'POST',
        async: false,
        data: { BPS_ID: CodigoSocio },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result > 0) {
                ConsultarEstablecimiento();
                alert(dato.message);
            } else {
                alert(dato.message);
            }
            //alert(resp);
        }
    });
}