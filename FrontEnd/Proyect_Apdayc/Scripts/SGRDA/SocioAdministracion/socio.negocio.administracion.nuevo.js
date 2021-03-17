
/*INICIO CONSTANTES*/
var K_WIDTH = 1000;
var K_HEIGHT = 400;
var K_WIDTH_OBS = 700;
var K_HEIGHT_OBS = 350;
var K_SIZE_PAGE = 8;
// var K_TipoPersonaItem = [{ Text: 'Juridica', Value: 'J' }, { Text: 'Natural', Value: 'N' }]
var K_ID_POPUP_DIR = "mvDireccion";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_DIV_MESSAGE = {
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento"
};
var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};
var K_HID_KEYS = {
    SOCIO: '#hidBpsId'
};

//varaible para que la Programacion Descuento Reconozca que el descuento se esta haciendo por Socio y no Por Licencia
var K_RECONOCE_SOCIO = {
    SOCIO: 1
};

/*INICIO CONSTANTES*/

$(function () {



    $("#loading").dialog({
        autoOpen: false,
        width: 300,
        height: 100,
        modal: true,
        open: function (event, ui) {
            $(this).parent().children('.ui-dialog-titlebar').hide();
        }
    });
    $("#tabs").tabs();
    var eventoKP = "keypress";
    $('#txtFono').on(eventoKP, function (e) { return solonumeros(e); });
    /*Inicializador de PopUp de direcciones*/
    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvObs").val("0");
    $("#hidAccionMvPar").val("0");
    $("#hidAccionMvDoc").val("0");
    $("#hidAccionMvTel").val("0");
    $("#hidAccionMvMail").val("0");
    $("#hidAccionMvRedes").val("0");

    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        $("#divTituloPerfil").html("Persona - Entidad / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoPersona("ddlTipoPersona", 0);
        loadTipoDocumento("ddlTipoDocumentoVal", 0);

    } else {
        $("#divTituloPerfil").html("Persona - Entidad / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);

    }
    initFormDireccion({
        id: K_ID_POPUP_DIR,
        parentControl: "divDireccion",
        width: 850,
        height: 400,
        evento: addDireccion,
        modal: true
    });




    /*ComboBox de Popups*/
    loadTipoDoc("ddlTipoDocumento", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);
    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoCorreo("ddlTipoMail", 0);
    loadTipoTelefono("ddlTipoFono", 0);
    loadTipoRedes("ddlTipoRedes", 0);


    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addObservacion, "Cancelar": function () { $("#mvObservacion").dialog("close"); } }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addDocumento, "Cancelar": function () { $("#mvDocumento").dialog("close"); } }, modal: true });
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addParametro, "Cancelar": function () { $("#mvParametro").dialog("close"); } }, modal: true });
    $("#mvTelefono").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addTelefono, "Cancelar": function () { $("#mvTelefono").dialog("close"); } }, modal: true });
    $("#mvCorreo").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addCorreo, "Cancelar": function () { $("#mvCorreo").dialog("close"); } }, modal: true });
    $("#mvRedes").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addRedes, "Cancelar": function () { $("#mvRedes").dialog("close"); } }, modal: true });
    $("#mvListarDireccion").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": addDuplicarDireccion, "Cancelar": function () { $("#mvListarDireccion").dialog("close"); } }, modal: true });
    //Agregando el AbrirPopup
    $("#mvDescuento").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": addDescuento, "Cancelar": function () { $("#mvDescuento").dialog("close"); } }, modal: true });
    //agregando la busqueda de socio empresarial,es como una funcion ....
    //mvInitBuscarSocioEmpresarial({ container: "mvContenedorSocioEmpresarial", idButtonToSearch: "btnGrupEmp", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lblGrupoEmpresarial" });
    // mal*mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnGrupEmp", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lblResponsable" });
    $("#mvValidarDni").dialog({ autoOpen: false, width: 450, height: 265, modal: true });
    $(".addListarDireccion").on("click", function () { ListarDireccionxPerfil(codeEdit); $("#mvListarDireccion").dialog("open"); });
    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $(".addTelefono").on("click", function () { limpiarTelefono(); $("#mvTelefono").dialog("open"); });
    $(".addCorreo").on("click", function () { limpiarCorreo(); $("#mvCorreo").dialog("open"); });
    $(".addRedes").on("click", function () { limpiarRedes(); $("#mvRedes").dialog("open"); });
    //Agregar Para Hacer el Desceunto por Clientes
    $(".addDescuento").on("click", function () { initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento"); $("#mvDescuento").dialog("open") /*limpiarDescuento(); initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento");*/ });
    //por si aca
    //initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento");


    $("#btnValidarDocumento").on("click", function () {
        if ($("#ddlTipoDocumentoVal option:selected").text() != "NINGUNO") {
            validarLongitudNumDoc();
        }
    }).button({
        icons: { secondary: "ui-icon-gear" }
    });

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });


    $("#txtNroValidacion").on("keypress", function (e) {
        var key = (e ? e.keyCode || e.which : window.event.keyCode);
        if (key == 13) {
            validarDocPopup();
        }
    });

    $("#ddlTipoDocumentoVal,#ddlTipoPersona").on("change", function () {
        $("#hidExitoValNumero").val("0");
        getValorConfigNumDoc($("#ddlTipoDocumentoVal").val());
        msgErrorB("divResultValidarDoc", "");
        msgError("");


        setTipoDocumento();

    });
    //$("#ddlTipoPersona").on("change", function () {
    //    $("#hidExitoValNumero").val("0");
    //    getValorConfigNumDoc($("#ddlTipoDocumentoVal").val());
    //    msgErrorB("divResultValidarDoc", "");
    //    msgError("");


    //    setTipoDocumento();

    //});

    $("#txtNroDocumento").on("keypress", function (e) {
        var key = (e ? e.keyCode || e.which : window.event.keyCode);
        msgErrorB("divResultValidarDoc", "");
        if (key == 13) {
            validarLongitudNumDoc();
        }
    });
    $("#btnGrabar").on("click", function () { insertarSocio(); }).button();
    $("#btnRegresar").on("click", function () { location.href = "../SocioAdministracion/"; }).button();
    $("#btnNuevo").on("click", function () { location.href = "Nuevo"; }).button();

    //getValorConfigNumDoc($("#ddlTipoDocumentoVal").val());
    /*Inicio de Carga inicial de tabs*/
    loadDataDireccion();
    loadDataObservacion();
    loadDataDocumento();
    loadDataParametro();
    loadDataTelefono();
    loadDataCorreo();
    loadDataRedes();
    /*FIN de Carga inicial de tabs*/


    $("#txtPaterno").attr("disabled", "disabled");
    $("#txtMaterno").attr("disabled", "disabled");
    $("#txtNombres").attr("disabled", "disabled");
    $("#tabs").css("display", "inline");

    //Probando controles de el TAB "DESCUENTOS"
    $("#divplanifi").hide();

    //$("#divBtnAddDscto").on("click",function(){ alert("POP UP DESCUENTOS")});
    //Descuento por Socios 
    $("#ddlTipoDescuento").on("change", function () {
        if (ObtieneIdTipoDscto() != $(this).val()) {
            $("#ddlDescuento").show();
            $("#txtDescuentoEspecial").hide();
            $("#txtDescuentoEspecial").removeClass('requerido');
            $("#txtDescuentoEspecial").css('border-color', 'gray');
            $("#txtValorDscto").hide();
            $("#txtValorDscto").removeClass('requerido');
            $("#txtValorDscto").css('border-color', 'gray');
            $("#ddlDescuento").addClass('requeridoLst');
            //mantenimiento.descuento.js/limpiarDescuento()
            limpiarDescuento();
            loadDescuentoXTarifa("ddlDescuento", $(this).val(), 0, $(K_HID_KEYS.TARIFA).val());
        } else {
            $("#ddlDescuento").hide();
            $("#txtDescuentoEspecial").show();
            $("#txtDescuentoEspecial").addClass('requerido');
            $("#txtValorDscto").show();
            $("#txtValorDscto").val('');
            $("#txtValorDscto").addClass('requerido');
            $("#ddlDescuento").removeClass('requeridoLst');
            $("#txtDescuentoEspecial").val('');
            $('#txtValorDscto').on("keypress", function (e) { return solonumeros(e); });
            $('#lblPerDscto').html(0);
            //$('#lblValorDscto').html(entidad.DISC_VALUE);
            $('#lblSignoDscto').html('-');
            //cssValCaract k-formato-numerico
        }

    });
    $("#ddlDescuento").on("change", function () { limpiarDescuento(); obtenerDescuento($(this).val()); });
    //$("#ddlTemporalidad").on("change", function () { limpiarTarifa(); obtenerTarifa($(this).val()); });

    //LOAD DESCUENTOS POR SOCIO
    if ($("#hidBpsId").val()) {
        var bpsid = $("#hidBpsId").val();
        //mantenimiento.descuento.js
        ConsultaDescuentosSocio(bpsid);
    }

    //Oculta el Tr de los text de la pantalla
    if (K_RECONOCE_SOCIO.SOCIO == 1) {

        //Ocultar Los 
        $("#trMostrarText").hide();
        $("#btnCalculoTTDescuento").hide();
    }

    //descuentos grupo empresarial

    $("#chkGE").change(function () {
        if ($("#chkGE").is(':checked')) {
            $("#btnGrupEmp").hide();
            $("#lblGrupoEmpresarial").text("GRUPO EMPRESARIAL");
        } else {

            $("#btnGrupEmp").show();
            $("#lblGrupoEmpresarial").prop("disabled", false);
            $("#lblGrupoEmpresarial").text("Seleccione");
        }
    });

});

function ListarDireccionxPerfil(codeEdit) {
    $.ajax({
        url: '../SocioAdministracion/ListarDireccionxPerfil',
        type: 'POST',
        data: { id: codeEdit },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    loadDataListarDireccion();
                }
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataListarDireccion() {
    loadDataGridTmp('ListarDireccionAll', "#gridListarDireccion");
}


function setTipoDocumento() {
    $("#txtNroDocumento").val("");
    $("#hidExitoValNumero").val("0");
    var itemChange = $("#ddlTipoDocumentoVal option:selected").text();
    $("#txtNroDocumento").attr("class", "requerido");

    //if (itemChange === "DNI" ) {
    if (itemChange != "RUC" && itemChange != "NINGUNO" && itemChange != "DOC.TRIB.NO.DOM.SIN.RUC") {
        $("#txtRazon").attr("disabled", "disabled");
        $("#txtMaterno").removeAttr("disabled");
        $("#txtPaterno").removeAttr("disabled");
        $("#txtNombres").removeAttr("disabled");
        $("#txtRazon").val("");

        $("#txtRazon").removeAttr("class", "requerido");
        $("#txtMaterno").attr("class", "requerido");
        $("#txtPaterno").attr("class", "requerido");
        $("#txtNombres").attr("class", "requerido");

        $("#txtNroDocumento").prop("disabled", false);




    } else if (itemChange === "RUC") {
        $("#txtMaterno").attr("disabled", "disabled");
        $("#txtPaterno").attr("disabled", "disabled");
        $("#txtNombres").attr("disabled", "disabled");

        $("#txtRazon").removeAttr("disabled");

        $("#txtRazon").attr("class", "requerido");
        $("#txtMaterno").removeAttr("class", "requerido");
        $("#txtPaterno").removeAttr("class", "requerido");
        $("#txtNombres").removeAttr("class", "requerido");

        $("#txtMaterno").val("");
        $("#txtPaterno").val("");
        $("#txtNombres").val("");

        $("#txtNroDocumento").prop("disabled", false);

    } else if (itemChange === "DOC.TRIB.NO.DOM.SIN.RUC") {
        $("#txtMaterno").attr("disabled", "disabled");
        $("#txtPaterno").attr("disabled", "disabled");
        $("#txtNombres").attr("disabled", "disabled");

        $("#txtRazon").removeAttr("disabled");

        $("#txtRazon").attr("class", "requerido");
        $("#txtMaterno").removeAttr("class", "requerido");
        $("#txtPaterno").removeAttr("class", "requerido");
        $("#txtNombres").removeAttr("class", "requerido");

        $("#txtMaterno").val("");
        $("#txtPaterno").val("");
        $("#txtNombres").val("");

        $("#txtNroDocumento").prop("disabled", false);
        $("#hidExitoValNumero").val("1");
    } else if (itemChange === "NINGUNO") {

        $("#txtNroDocumento").removeAttr("class", "requerido");
        $("#txtNroDocumento").css({ 'border': '1px solid gray' });
        $("#txtNroDocumento").prop("disabled", true);
        $("#txtNroDocumento").val("");
        $("#hidExitoValNumero").val("1");
        item = $("#ddlTipoPersona").val();
        if (item === "N") {
            $("#txtRazon").attr("disabled", "disabled");
            $("#txtMaterno").removeAttr("disabled");
            $("#txtPaterno").removeAttr("disabled");
            $("#txtNombres").removeAttr("disabled");
            $("#txtRazon").val("");

            $("#txtRazon").removeAttr("class", "requerido");
            $("#txtMaterno").attr("class", "requerido");
            $("#txtPaterno").attr("class", "requerido");
            $("#txtNombres").attr("class", "requerido");

        } else if (item === "J") {
            $("#txtMaterno").attr("disabled", "disabled");
            $("#txtPaterno").attr("disabled", "disabled");
            $("#txtNombres").attr("disabled", "disabled");

            $("#txtRazon").removeAttr("disabled");

            $("#txtRazon").attr("class", "requerido");
            $("#txtMaterno").removeAttr("class", "requerido");
            $("#txtPaterno").removeAttr("class", "requerido");
            $("#txtNombres").removeAttr("class", "requerido");

            $("#txtMaterno").val("");
            $("#txtPaterno").val("");
            $("#txtNombres").val("");
        }
    }
    $("#txtRazon").css({ 'border': '1px solid gray' });
    $("#txtMaterno").css({ 'border': '1px solid gray' });
    $("#txtPaterno").css({ 'border': '1px solid gray' });
    $("#txtNombres").css({ 'border': '1px solid gray' });

}
function validarLongitudNumDoc() {
    msgError("");
    var exito = false;
    var tipoDoc = $("#ddlTipoDocumentoVal option:selected").val();
    var tipoDocDesc = $("#ddlTipoDocumentoVal option:selected").text();

    getValorConfigNumDoc(tipoDoc);
    var numValidar = $("#hidCantNumValidar").val();

    if (tipoDocDesc === "DOC.TRIB.NO.DOM.SIN.RUC") {
        $("#hidExitoValNumero").val("1");
    } else {
        if ($.trim($("#txtNroDocumento").val()) != "") {
            if ($("#txtNroDocumento").val().length != numValidar) {
                msgErrorB("divResultValidarDoc", "Longitud del número de documento debe ser " + numValidar + " digitos.");
            } else {
                exito = true;
                msgErrorB("divResultValidarDoc", "");
            }
            if (exito) {
                modalValidarNumero();
            }
        } else {
            msgErrorB("divResultValidarDoc", "Ingrese número de documento.");
        }

    }
}

function modalValidarNumero() {
    $("#hidExitoValNumero").val(0);
    msgOkB("divErrorValidarDoc", "");
    msgOkB("divResultValidarDoc", "");
    //es ruc
    var tipoDocText = $("#ddlTipoDocumentoVal option:selected").text();
    if (tipoDocText === "RUC") {
        var validar = { ruc: $("#txtNroDocumento").val(), TipoDocumento: $("#ddlTipoDocumentoVal").val() }
        $("#divGetRuc").css({ 'display': 'inline' });
        $("#divGetRuc").html("<img src='../Images/otros/loading.GIF' width='18' />");          //   Verificando RUC..
        $.ajax({
            url: 'ValidarRuc',
            data: validar,
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                $("#divGetRuc").css({ 'display': 'none' });
                if (dato.result == 1) {
                    // $("#hidExitoValNumero").val(dato.result);
                    $("#txtRazon").val(dato.valor);
                } else if (dato.result == 0) {
                    msgErrorB("divResultValidarDoc", dato.message);
                    $("#txtRazon").val("");
                    $("#txtNroDocumento").focus();
                }
                $("#hidExitoValNumero").val(dato.result);

            }
        });
        //si es dni
    } else if (tipoDocText === "DNI") {
        $("#txtNroValidacion").val("");
        $("#mvValidarDni").dialog("open");
    } else {
        $("#hidExitoValNumero").val(1);
    }
}

function validarDocPopup() {
    $("#hidExitoValNumero").val(0);
    msgOkB("divErrorValidarDoc", "");
    msgOkB("divResultValidarDoc", "");

    var tipoDoc = $("#ddlTipoDocumentoVal option:selected").text();
    var tipoPer = $("#ddlTipoPersona option:selected").text();


    var numero = $("#txtNroDocumento").val();
    var identificador = $("#txtNroValidacion").val();
    if (numero.length == 8 && tipoDoc == 'DNI') {
        $.ajax({
            url: '../General/ValidacionDNI',
            data: { num: numero, id: identificador },
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    $("#hidExitoValNumero").val(1);
                    msgOkB("divErrorValidarDoc", dato.message);
                    msgOkB("divResultValidarDoc", dato.message);
                    $("#mvValidarDni").dialog("close");
                } if (dato.result == 2) {
                    $("#hidExitoValNumero").val(0);
                    msgErrorB("divErrorValidarDoc", dato.message);
                    msgErrorB("divResultValidarDoc", dato.message);
                } else {
                    msgErrorB("divErrorValidarDoc", dato.message);
                }
            }
        });
    } else {
        msgErrorB("divErrorValidarDoc", "El numero a validar tiene que ser igual a 8 digitos");
    }
    //if (numero.length == 11 && tipoDoc == 'RUC') {
    //    var numRuc = $("#txtNroDocumento").val();
    //    var tipoDoc = $("#ddlTipoDocumentoVal").val();
    //    alert(numRuc + "-" + tipoDoc);
    //    $.ajax({
    //        url: 'ValidarRuc',
    //        data: { ruc: numRuc , TipoDocumento: tipoDoc },
    //        type: 'POST',
    //        success: function (response) {
    //            var dato = $.parseJSON(response);
    //            $("#hidExitoValNumero").val(dato.result);
    //            alert(dato.message);
    //        }
    //    });
    //}
}

function insertarSocio() {

    msgError("");
    msgErrorB("divResultValidarDoc", "");
    var exito = true;

    var tipoDoc = $("#ddlTipoDocumentoVal option:selected").val();

    if ($("#txtNroDocumento").val().trim() === '') {
        $("#txtNroDocumento").css({ 'border': '1px solid red' });
        exito = false;
    } else {
        $("#txtNroDocumento").css({ 'border': '1px solid gray' });
    }

    if (tipoDoc == 13 && $("#txtNroDocumento").val().length > 12) {
        $("#txtNroDocumento").css({ 'border': '1px solid red' });
       alert("La Longitud del número de documento excede los 12 digitos.");
       exito = false;
    }

    //ValidarRequeridos() (&&
    if (exito && validarRazonSocial()) {

        if ($("#hidExitoValNumero").val() == "1") {
            var idBps = 0;
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) idBps = $("#hidBpsId").val();

            var socio = {
                Codigo: idBps,
                TipoEntidad: $("#ddlTipoPersona").val(),
                TipoPersona: $("#ddlTipoPersona").val(),
                TipoDocumento: $("#ddlTipoDocumentoVal").val(),
                NumDocumento: $("#txtNroDocumento").val(),
                RazonSocial: $("#txtRazon").val(),
                EsUsuDerecho: 0,
                EsGrupoEmp: $("#chkGE").prop("checked"),
                EsRecaudador: 0,
                EsEmpleador: 0,
                EsProveedor: 0,
                EsAsociacion: 0,
                Nombres: $("#txtNombres").val(),
                Paterno: $("#txtPaterno").val(),
                Materno: $("#txtMaterno").val(),
                NombreComercial: $("#txtNomComercial").val()
            };
            //, $("#chkRecaudador").prop("checked"),
            // $("#chkEmpleado").prop("checked")
            //  $("#chkProveedor").prop("checked")
            //  $("#chkAsociacion").prop("checked")
            //  $("#chkUsuDerecho").prop("checked")
            $.ajax({
                url: 'Insertar',
                data: socio,
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        alert(dato.message);
                        location.href = "../SocioAdministracion/";
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        } else {

            msgError("La validación del Número de Documento no ha sido realizada con éxito.");

        }
    }
    return false;

}



//function editar(idSel) {
//    $("#hidOpcionEdit").val(1);
//    limpiar();
//    $.ajax({
//        url: 'USUARIO/Obtiene',
//        type: 'POST',
//        data: { id: idSel },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var usuario = dato.data.Data;
//                if (usuario != null) {
//                    $("#txtNombre").val(usuario.USUA_VNOMBRE_USUARIO);
//                    $("#txtPaterno").val(usuario.USUA_VAPELLIDO_PATERNO_USUARIO);
//                    $("#txtMaterno").val(usuario.USUA_VAPELLIDO_MATERNO_USUARIO);
//                    $("#txtRed").val(usuario.USUA_VUSUARIO_RED_USUARIO);
//                    $("#txtPass").val(usuario.USUA_VPASSWORD_USUARIO);
//                    $("#txtCodigo").val(usuario.USUA_ICODIGO_USUARIO);
//                    loadRol(usuario.ROL_ICODIGO_ROL);

//                    if (usuario.USUA_CACTIVO_USUARIO) {
//                        $("#selEstado option").filter(function () {
//                            return $(this).val() == 1;
//                        }).attr('selected', true);
//                    } else {

//                        $("#selEstado option").filter(function () {
//                            return $(this).val() == 0;
//                        }).attr('selected', true);
//                    }
//                }

//            } else {
//                alert(dato.message);
//            }
//        }
//    });


//    $("#ModalNeoUsu").dialog({ title: "Actualizar Usuario" });
//    $("#ModalNeoUsu").dialog("open");


//}

function limpiarValidacion() {

    msgError("", "txtNombre");
    msgError("", "txtPaterno");
    msgError("", "txtMaterno");
    msgError("", "txtRed");
    msgError("", "txtPass");
    //msgError("","selEstado");




}

function limpiar() {

    $("#txtNombre").val("");
    $("#txCodigo").val("");
    $("#txtPaterno").val("");
    $("#txtMaterno").val("");
    $("#txtRed").val("");
    $("#txtPass").val("");
    limpiarValidacion();





}


/*Inicio de eventos de grillas temporales */
function addDireccion() {

    var IdAdd = 0;
    if ($("#hidAccionMvDir").val() == "1") IdAdd = $("#hidEdicionDir").val()
    if (ValidarRequeridosMV()) {
        var direccion = {
            Id: IdAdd,
            TipoDireccion: $("#ddlTipoDireccion").val(),
            RazonSocial: "",
            Territorio: $("#ddlTerritorio").val(),
            CodigoUbigeo: $("#hidCodigoUbigeo").val(),
            Referencia: $("#txtReferencia").val(),
            CodigoPostal: $("#ddlCodPostal").val(),
            TipoUrba: $("#ddlUrbanizacion").val(),
            Urbanizacion: $("#txtUrb").val(),
            Numero: $("#txtNro").val(),
            Manzana: $("#txtMz").val(),
            Lote: $("#txtLote").val(),
            TipoDepa: $("#ddlDepartamento").val(),
            NroPiso: $("#txtNroPiso").val(),
            TipoAvenida: $("#ddlAvenida").val(),
            Avenida: $("#txtAvenida").val(),
            TipoEtapa: $("#ddlEtapa").val(),
            Etapa: $("#txtEtapa").val(),
            TipoDireccionDesc: $("#ddlTipoDireccion option:selected").text(),
            TipoTerritorioDes: $("#ddlTerritorio option:selected").text(),
            TipoUrbDes: $("#ddlUrbanizacion option:selected").text(),
            TipoDepaDes: $("#ddlDepartamento option:selected").text(),
            TipoAvenidaDes: $("#ddlAvenida option:selected").text(),
            TipoEtapaDes: $("#ddlEtapa option:selected").text(),
            DescripcionUbigeo: $("#txtUbigeo").val()
        };
        $.ajax({
            url: 'AddDireccion',
            type: 'POST',
            data: direccion,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDireccion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });

        $("#mvDireccion").dialog("close");
    }
}

function addObservacion() {
    var IdAdd = 0;
    if ($("#hidAccionMvObs").val() === "1") IdAdd = $("#hidEdicionObs").val();
    if (ValidarRequeridosOBS()) {
        var observacion = {
            Id: IdAdd,
            TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
            Observacion: $("#txtObservacion").val(),
            TipoObservacionDesc: $("#ddlTipoObservacion option:selected").text(),
        };

        $.ajax({
            url: 'AddObservacion',
            type: 'POST',
            data: observacion,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataObservacion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvObservacion").dialog("close");
    }
}

function addDocumento() {
    var IdAdd = 0;
    if ($("#hidAccionMvDoc").val() === "1") {
        IdAdd = $("#hidEdicionDoc").val();
    }
    $("#txtFecha").addClass("requerido");
    if (IdAdd > 0) {
        $("#file_upload").removeClass("requerido");
    } else {
        $("#file_upload").addClass("requerido");
    }
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, K_DIV_POPUP.DOCUMENTO)) {
        var documento = {
            Id: IdAdd,
            TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
            TipoDocumentoDesc: $("#ddlTipoDocumento option:selected").text(),
            FechaRecepcion: $("#txtFecha").val(),
            Archivo: $("#hidNombreFile").val()
        };
        $.ajax({
            data: documento,
            url: 'AddDocumento',
            type: 'POST',
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    if ($("#file_upload").val() != "") {
                        InitUploadTabDocSocio("file_upload", dato.Code);
                    }
                    loadDataDocumento();
                } else {
                    alert(dato.message);
                }
            }
        });
        loadDataDocumento();
        $("#mvDocumento").dialog("close");
    }
}

function addParametro() {
    var IdAdd = 0;
    if ($("#hidAccionMvPar").val() === "1") IdAdd = $("#hidEdicionPar").val();
    if (ValidarRequeridosPM()) {
        var entidad = {
            Id: IdAdd,
            TipoParametro: $("#ddlTipoParametro option:selected").val(),
            Descripcion: $("#txtDescripcion").val(),
            TipoParametroDesc: $("#ddlTipoParametro option:selected").text()
        };
        $.ajax({
            url: 'AddParametro',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataParametro();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvParametro").dialog("close");
    }
}

function addTelefono() {

    var IdAdd = 0;
    if ($("#hidAccionMvTel").val() == "1") IdAdd = $("#hidEdicionTel").val();
    if (ValidarRequeridosTL()) {
        var entidad = {
            Id: IdAdd,
            IdTipo: $("#ddlTipoFono option:selected").val(),
            Numero: $("#txtFono").val(),
            TipoDesc: $("#ddlTipoFono option:selected").text(),
            Observacion: $("#txtFonoObs").val()
        };
        $.ajax({
            url: 'AddTelefono',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataTelefono();
                    //} else {
                    //    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvTelefono").dialog("close");
    }

    // }
}

function addCorreo() {

    var IdAdd = 0;
    if ($("#hidAccionMvMail").val() == "1") IdAdd = $("#hidEdicionMail").val();
    var entidad = {
        Id: IdAdd,
        IdTipo: $("#ddlTipoMail option:selected").val(),
        Correo: $("#txtMail").val(),
        TipoDesc: $("#ddlTipoMail option:selected").text(),
        Observacion: $("#txtMailObs").val()
    };
    if (validarEmail(entidad.Correo)) {
        $.ajax({
            url: 'AddCorreo',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataCorreo();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#avisoMVMail").html('');
        $("#mvCorreo").dialog("close");
    } else {
        $("#avisoMVMail").html('<br/><span style="color:red;">Error: La dirección de correo es incorrecta.</span><br/>');
    }


}
function addRedes() {
    var IdAdd = 0;
    if ($("#hidAccionMvRedes").val() == "1") IdAdd = $("#hidEdicionRedes").val();
    var entidad = {
        Id: IdAdd,
        IdTipo: $("#ddlTipoRedes option:selected").val(),
        Link: $("#txtLink").val(),
        TipoDesc: $("#ddlTipoRedes option:selected").text(),
        Observacion: $("#txtRedesObs").val()
    };
    if (validarRedSocial(entidad.Link)) {
        $.ajax({
            url: 'AddRedes',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataRedes();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#avisoMVRedes").html('');
        $("#mvRedes").dialog("close");
    } else {
        $("#avisoMVRedes").html('<br/><span style="color:red;">Error: La Url es incorrecta.</span><br/>');
    }
}

function addDuplicarDireccion() {

}

function obtenerRazonSocial() {


    var nroPiso = "";
    var nroAv = "";
    var nroEtp = "";
    var nro = "";
    var nroMZ = "";
    var nroLote = "";


    if ($.trim($("#txtNro").val()) != "") {
        nro = "  Nro " + $("#txtNro").val();
    }
    if ($.trim($("#txtMz").val()) != "") {
        nroMZ = "  Mz " + $("#txtMz").val();
    }
    if ($.trim($("#txtLote").val()) != "") {
        nroLote = "  Lote " + $("#txtLote").val();
    }
    if ($.trim($("#txtNroPiso").val()) != "") {
        nroPiso = " " + $("#ddlDepartamento option:selected").text() + " " + $("#txtNroPiso").val();
    }
    if ($.trim($("#txtNroPiso").val()) != "") {
        nroAv = " " + $("#ddlAvenida option:selected").text() + " " + $("#txtAvenida").val();
    }
    if ($.trim($("#txtEtapa").val()) != "") {
        nroEtp = " " + $("#ddlEtapa option:selected").text() + " " + $("#txtEtapa").val();
    }
    var razon = $("#ddlUrbanizacion option:selected").text() + " " + $("#txtUrb").val() + nro + nroMZ + nroLote + nroPiso + nroAv + nroEtp;

    return razon;
}

function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadDataObservacion() {
    loadDataGridTmp('ListarObservacion', "#gridObservacion");
}

function loadDataDocumento() {
    loadDataGridTmp('ListarDocumento', "#gridDocumento");
}

function loadDataParametro() {
    loadDataGridTmp('ListarParametro', "#gridParametro");
}
function loadDataTelefono() {
    loadDataGridTmp('ListarTelefono', "#gridTelefono");
}
function loadDataCorreo() {
    loadDataGridTmp('ListarCorreo', "#gridCorreo");
}
function loadDataRedes() {
    loadDataGridTmp('ListarRedes', "#gridRedes");
}

function delAddDireccion(idDel) {
    $.ajax({
        url: 'DellAddDireccion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDireccion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddObservacion(idDel) {
    $.ajax({
        url: 'DellAddObservacion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataObservacion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddDocumento(idDel) {
    $.ajax({
        url: 'DellAddDocumento',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDocumento();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddParametro(idDel) {
    $.ajax({
        url: 'DellAddParametro',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataParametro();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}
function delAddCorreo(idDel) {
    $.ajax({
        url: 'DellAddCorreo',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataCorreo();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddRedes(idDel) {
    $.ajax({
        url: 'DellAddRedes',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataRedes();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddTelefono(idDel) {
    $.ajax({
        url: 'DellAddTelefono',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataTelefono();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}


function updAddDireccion(idUpd) {
    limpiarDireccion();

    $.ajax({
        url: 'ObtieneDireccionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                var direccion = dato.data.Data;
                if (direccion != null) {

                    $("#hidAccionMvDir").val("1");
                    $("#hidEdicionDir").val(direccion.Id);

                    $("#txtUrb").val(direccion.Urbanizacion);
                    $("#txtNro").val(direccion.Numero == 0 ? "" : direccion.Numero);
                    $("#txtMz").val(direccion.Manzana);


                    $("#ddlTipoDireccion").val(direccion.TipoDireccion);
                    $("#ddlTerritorio").val(direccion.Territorio);
                    $("#txtReferencia").val(direccion.Referencia);
                    $("#ddlCodPostal").val(direccion.CodigoPostal);
                    $("#ddlUrbanizacion").val(direccion.TipoUrba);

                    $("#txtLote").val(direccion.Lote);
                    $("#ddlDepartamento").val(direccion.TipoDepa);
                    $("#txtNroPiso").val(direccion.NroPiso);
                    $("#ddlAvenida").val(direccion.TipoAvenida);
                    $("#txtAvenida").val(direccion.Avenida);
                    $("#ddlEtapa").val(direccion.TipoEtapa);
                    $("#txtEtapa").val(direccion.Etapa);

                    $("#hidCodigoUbigeo").val(direccion.CodigoUbigeo);
                    $("#txtUbigeo").val(direccion.DescripcionUbigeo);




                    $("#" + K_ID_POPUP_DIR).dialog("open");
                } else {
                    alert("No se pudo obtener la direccion para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}

function updAddObservacion(idUpd) {
    limpiarObservacion();

    $.ajax({
        url: 'ObtieneObservacionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {

                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(obs.Id);
                    $("#ddlTipoObservacion").val(obs.TipoObservacion);
                    $("#txtObservacion").val(obs.Observacion);
                    $("#mvObservacion").dialog("open");
                } else {
                    alert("No se pudo obtener la direccion para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function updAddParametro(idUpd) {
    limpiarParametro();

    $.ajax({
        url: 'ObtieneParametroTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvPar").val("1");
                    $("#hidEdicionPar").val(param.Id);
                    $("#ddlTipoParametro").val(param.TipoParametro);
                    $("#txtDescripcion").val(param.Descripcion);
                    $("#mvParametro").dialog("open");
                } else {
                    alert("No se pudo obtener el parametro para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function updAddDocumento(idUpd) {
    limpiarDocumento();

    $.ajax({
        url: 'ObtieneDocumentoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {


                    $("#hidAccionMvDoc").val("1");
                    $("#hidEdicionDoc").val(doc.Id);
                    //$("#ddlTipoDocumento").val(doc.TipoDocumento);

                    loadTipoDoc("ddlTipoDocumento", doc.TipoDocumento);

                    var datepicker = $("#txtFecha").data("kendoDatePicker");
                    datepicker.value(doc.FechaRecepcion);
                    // alert(doc.FechaRecepcion);
                    $("#mvDocumento").dialog("open");

                } else {
                    alert("No se pudo obtener el documento para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function updAddTelefono(idUpd) {
    limpiarTelefono();

    $.ajax({
        url: 'ObtieneTelefonoTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvTel").val("1");
                    $("#hidEdicionTel").val(param.Id);
                    $("#ddlTipoFono").val(param.IdTipo);
                    $("#txtFono").val(param.Numero);
                    $("#txtFonoObs").val(param.Observacion);
                    $("#mvTelefono").dialog("open");
                } else {
                    alert("No se pudo obtener el para teléfono para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function updAddCorreo(idUpd) {
    limpiarCorreo();

    $.ajax({
        url: 'ObtieneCorreoTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvMail").val("1");
                    $("#hidEdicionMail").val(param.Id);
                    $("#ddlTipoMail").val(param.IdTipo);
                    $("#txtMail").val(param.Correo);
                    $("#txtMailObs").val(param.Observacion);
                    $("#mvCorreo").dialog("open");
                } else {
                    alert("No se pudo obtener el para correo para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function updAddRedes(idUpd) {
    limpiarRedes();

    $.ajax({
        url: 'ObtieneRedesSocialesTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvRedes").val("1");
                    $("#hidEdicionRedes").val(param.Id);
                    $("#ddlTipoRedes").val(param.IdTipo);
                    $("#txtLink").val(param.Link);
                    $("#txtRedesObs").val(param.Observacion);
                    $("#mvRedes").dialog("open");
                } else {
                    alert("No se pudo obtener la información para la red social para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
/*Fin de eventos de grillas temporales */


function getValorConfigNumDoc(itipo) {

    $.ajax({
        url: '../General/GetConfigTipoDocumento',
        data: { tipo: itipo },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#hidCantNumValidar").val(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}
/*Limpiar controles*/
function limpiarObservacion() {
    $("#txtObservacion").val("");
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    msgErrorOBS("", "txtObservacion");
}
function limpiarDireccion() {
    $("#txtUrb").val("");
    $("#hidAccionMvDir").val("0");
    $("#hidEdicionDir").val("0");


    $("#txtNro").val("");
    $("#txtMz").val("");

    $("#ddlTipoDireccion").val("");
    // $("#ddlTerritorio").val("");
    $("#txtReferencia").val("");
    $("#ddlCodPostal").val("");
    $("#ddlUrbanizacion").val("");

    $("#txtLote").val("");
    $("#ddlDepartamento").val("");
    $("#txtNroPiso").val("");
    $("#ddlAvenida").val("");
    $("#txtAvenida").val("");
    $("#ddlEtapa").val("");
    $("#txtEtapa").val("");

    $("#hidCodigoUbigeo").val("");
    $("#txtUbigeo").val("");

}
function limpiarDocumento() {
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#file_upload").css({ 'border': '1px solid gray' });
    $("#txtFecha").css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#hidAccionMvDoc").val("0");
    $("#hidEdicionDoc").val("0");
    //$('#fuFiles').uploadifyClearQueue();
}
function limpiarParametro() {
    $("#txtDescripcion").val("");
    $("#hidAccionMvPar").val("0");
    $("#hidEdicionPar").val("0");
    msgErrorPM("", "txtDescripcion");
}
function limpiarTelefono() {

    $("#hidAccionMvTel").val("0");
    $("#hidEdicionTel").val("");
    $("#ddlTipoFono").val("");
    $("#txtFono").val("");
    $("#txtFonoObs").val("");
    msgErrorTL("", "txtFono");
}
function limpiarCorreo() {

    $("#hidAccionMvMail").val("0");
    $("#hidEdicionMail").val("");
    $("#ddlTipoMail").val("");
    $("#txtMail").val("");
    $("#txtMailObs").val("");
    msgErrorMV("", "txtMail");
}
function limpiarRedes() {

    $("#hidAccionMvRedes").val("0");
    $("#hidEdicionRedes").val("");
    $("#ddlTipoRedes").val("");
    $("#txtLink").val("");
    $("#txtRedesObs").val("");
    //msgErrorMV("", "txtLink");
}
/*Fin Limpiar controles*/

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



function ObtenerDatos(bpsId) {
    $.ajax({
        url: 'Obtener',
        data: { codigo: bpsId },
        type: 'POST',
        success: function (response) {

            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var socio = dato.data.Data;
                loadTipoPersona("ddlTipoPersona", socio.TipoPersona);
                loadTipoDocumento("ddlTipoDocumentoVal", socio.TipoDocumento);

                $("#hidExitoValNumero").val("1");
                $("#txtNroDocumento").val(socio.NumDocumento);

                //$("#chkUsuDerecho").attr('checked', socio.EsUsuDerecho);
                $("#chkGE").attr('checked', socio.EsGrupoEmp);
                //$("#chkRecaudador").attr('checked', socio.EsRecaudador);
                //$("#chkEmpleado").attr('checked', socio.EsEmpleador);
                //$("#chkProveedor").attr('checked', socio.EsProveedor);
                //$("#chkAsociacion").attr('checked', socio.EsAsociacion);

                //alert(socio.TipoDocumento);
                if (socio.TipoDocumento == 1 || socio.TipoDocumento == 3 || socio.TipoDocumento == 17) {
                    $("#txtMaterno").attr("disabled", "disabled");
                    $("#txtPaterno").attr("disabled", "disabled");
                    $("#txtNombres").attr("disabled", "disabled");

                    $("#txtMaterno").val("");
                    $("#txtPaterno").val("");
                    $("#txtNombres").val("");
                    $("#txtRazon").val(socio.RazonSocial);
                    $("#txtNomComercial").val(socio.NombreComercial);
                } else {

                    $("#txtRazon").attr("disabled", "disabled");
                    $("#txtMaterno").removeAttr("disabled");
                    $("#txtPaterno").removeAttr("disabled");
                    $("#txtNombres").removeAttr("disabled");

                    $("#txtMaterno").val(socio.Materno);
                    $("#txtPaterno").val(socio.Paterno);
                    $("#txtNombres").val(socio.Nombres);
                    $("#txtNomComercial").val(socio.NombreComercial);
                    //$("#txtRazon").val("");
                }
                $("#lblVerificado").html(socio.Verificado);

                loadDataDireccion();
                ValidatePermiso(bpsId);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function actualizarDirPrincipal(idDir) {

    $.ajax({
        url: 'SetDirPrincipal',
        data: { idDir: idDir },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            //  alert(dato.message);
            if (!(dato.result == 1)) {
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}


function validarRazonSocial() {

    msgError("");
    var itemChange = $("#ddlTipoDocumentoVal option:selected").text();
    var exito = true;

    if (itemChange === "RUC") {
        var elem = $.trim($("#txtRazon").val());
        if (elem === '') {
            $("#txtRazon").css({ 'border': '1px solid red' });
            exito = false;

        } else {
            $("#txtRazon").css({ 'border': '1px solid gray' });
        }
        if (exito == false) msgError("Debe ingresar los campos requeridos dni ");

    } else if (itemChange === "DNI") {
        var elemNom = $.trim($("#txtNombres").val());
        var elemMa = $.trim($("#txtMaterno").val());
        var elemPa = $.trim($("#txtPaterno").val());
        if (elemNom === '') {
            $("#txtNombres").css({ 'border': '1px solid red' });
            exito = false;
        } else {
            $("#txtNombres").css({ 'border': '1px solid gray' });
        }
        if (exito && elemMa === '') {
            $("#txtMaterno").css({ 'border': '1px solid red' });
            exito = false;
        } else {
            $("#txtMaterno").css({ 'border': '1px solid gray' });
        }
        if (exito && elemPa === '') {
            $("#txtPaterno").css({ 'border': '1px solid red' });
            exito = false;
        } else {
            $("#txtPaterno").css({ 'border': '1px solid gray' });
        }


        if (exito == false) msgError("Debe ingresar los campos requeridos ruc ");

    }

    return exito;


}

function verImagen(url) {


    //alert(url);
    $("#mvImagen").dialog("open");
    //  $("#imgDocumento").attr("src", url);

    //$("#lnkDocsumento").attr("href", url);

    $("#ifContenedor").attr("src", url);


    return false;
}



function ValidatePermiso(idBps) {
    msgError("");
    $.ajax({
        url: '../Seguridad/EnableUpdate',
        data: { idBps: idBps },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                /*validacion si tiene permiso*/
                if (dato.valor == "1") {
                    $(".cssTabReadOnly").each(function (index, element) { $(element).show(); });
                    $("#gridDireccion a").each(function (index, element) { $(element).show(); });
                    $("#gridDireccion radio").each(function (index, element) { $(element).removeAttr("disabled"); });
                    $("#gridObservacion a").each(function (index, element) { $(element).show(); });
                    $("#gridDocumento a").each(function (index, element) { $(element).show(); });
                    $("#gridParametro a").each(function (index, element) { $(element).show(); });
                    $("#gridTelefono a").each(function (index, element) { $(element).show(); });
                    $("#gridCorreo a").each(function (index, element) { $(element).show(); });
                    //   $("#gridAsociado a").each(function (index, element) { $(element).show(); });
                    $("#gridRedes a").each(function (index, element) { $(element).show(); });
                    // $("#btnStatus").removeAttr("disabled");
                    $("#btnGrabar").removeAttr("disabled");
                } else {
                    $(".cssTabReadOnly").each(function (index, element) { $(element).hide(); });
                    $("#gridDireccion a").each(function (index, element) { $(element).hide(); });
                    $("#gridDireccion radio").each(function (index, element) { $(element).attr("disabled", "disabled"); });
                    $("#gridObservacion a").each(function (index, element) { $(element).hide(); });
                    $("#gridDocumento a").each(function (index, element) { $(element).hide(); });
                    $("#gridParametro a").each(function (index, element) { $(element).hide(); });
                    $("#gridTelefono a").each(function (index, element) { $(element).hide(); });
                    $("#gridCorreo a").each(function (index, element) { $(element).hide(); });
                    // $("#gridAsociado a").each(function (index, element) { $(element).hide(); });
                    $("#gridRedes a").each(function (index, element) { $(element).hide(); });
                    //   $("#btnStatus").attr("disabled", "disabled");
                    $("#btnGrabar").attr("disabled", "disabled");

                    msgError(dato.message);

                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//function addDescuento() {
//    alert("Ingreso");
//    initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento");

//}

//Insertando Los Descuentos Luego de Que se Grabe El Socio 


function InsertarDescuentos(codigo) {

    var BpsId = $("#hidBpsId").val();
    if (BpsId == "" || BpsId == null || BpsId == undefined)
        BpsId = codigo;

    $.ajax({
        url: '../Descuento/InsertarDescuentosSocios',
        data: { BpsId: BpsId },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //Mandar al Segundo insert en la tabla REC_DISCOUNTS_BPS
                //insertarSocio

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });


}

//Fubcion que es llama por el boton "btnGrabar" de este documento
function InactivarDescuentos(BPSID) {
    //alert("Inactivando");
    $.ajax({
        url: '../Descuento/InactivarDescuentoSocioGrabar',
        data: { BPSID: BPSID },
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //Mandar al Segundo insert en la tabla REC_DISCOUNTS_BPS

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }

    });


}

//function que permite valida si un Socio ya posee Grupo Empresarial

function ValidaSocioEmpresarial(bpsid) {

    $.ajax({
        url: '../SocioAdministracion/ValidaSocioEmpresarial',
        data: { bpsid: bpsid },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {

            var dato = response;
            if (dato.result > 0) {
                $("#chkGE").hide();
                $("#lblchkGE").text("PERTENECE A GRUPO EMPRESARIAL");
            } else {
                $("#chkGE").show();

            }
        }
    });

}