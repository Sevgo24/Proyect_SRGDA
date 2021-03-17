var K_WIDTH = 1000;
var K_HEIGHT = 400;
var K_WIDTH_OBS = 580;
var K_HEIGHT_OBS = 280;
var K_SIZE_PAGE = 8;
var K_ID_POPUP_DIR = "mvDireccion";
var K_WIDTH_CAR = 380;
var K_HEIGHT_CAR = 210;
var K_WIDTH_PAR = 580;
var K_HEIGHT_PAR = 280;
var K_WIDTH_DOC = 580;
var K_HEIGHT_DOC = 280;
var K_WIDTH_INS = 560;
var K_HEIGHT_INS = 400;
var K_WIDTH_DIV = 350;
var K_HEIGHT_DIV = 190;
var K_WIDTH_DIF = 450;
var K_HEIGHT_DIF = 160;
var K_ID_DIR = "divDir";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_DIV_VALIDAR = {
    DIV_CAB: "divCabeceraEst"
};
var K_DIV_MESSAGE = {
    DIV_ESTABLECIMIENTO: "divMensajeError",
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento"
};

var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};

//KEYS DE VALIDACION
var K_VALIDA = {
    CARACTERISTICA_PREDEFINIDA : 0
};
//VARIABLE QUE GUARDA EL VALOR DE EL CODIGO BPS PARA Poder validar mas adelante.

$(function () {
    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvObs").val("0");
    $("#hidAccionMvPar").val("0");
    $("#hidAccionMvDoc").val("0");
    $("#hidAccionMvCar").val("0");
    $("#hidAccionMvEnt").val("0");
    $("#hidAccionMvDiv").val("0");
    $("#hidAccionMvDif").val("0");

    //AGREGADO NUEVO - 24/10/2017
    $("#hidAccionMvTel").val("0");
    $("#hidAccionMvMail").val("0");
    $("#hidAccionMvRedes").val("0");
    $("#hidAccionMvEnt").val("0");
    //--------------------------------------

    $('#txtPaterno').attr('disabled', 'disabled');
    $('#txtMaterno').attr('disabled', 'disabled');
    $('#txtNombres').attr('disabled', 'disabled');
    $('#ddlSubtipoestablecimiento').append($("<option />", { value: 0, text: '--SELECCIONE--' }));
    initAutoCompletarRazonSocial("txtRazonSocial", "hidCodigoBPS");

    var id = GetQueryStringParams("id");

    if (id === undefined) {
        $("#divTitulo").html("Establecimientos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoestablecimiento('ddlTipoestablecimiento', 0);
        loadTipoIdentificacion(0);
    } else {
        $("#divTitulo").html("Establecimientos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidCodigoEST").val(id);
        ObtenerDatos(id);
    }

    /*ComboBox de Popups*/
    loadTipoDocumento("ddlTipoDocumentoRes", 0);
    loadTipoPersona("ddlTipoPersonaRes", 0);
    loadTipoDoc('ddlTipoDocumento', 0);
    loadTipoParametro('ddlTipoParametro', 0);
    loadTipoObservacion('ddlTipoObservacion', 0);
    loadTipoDocumento("ddlTipoDoc", 0);
    loadCaracteristica(0);
    loadRol("ddlRol", 0);
    loadDivisionTipo('ddlDivisionTipo', 0);
    loadMediosDifusion('ddlDifusion', 0);

    loadTipoCorreo("ddlTipoMail", 0); //AGREGADO NUEVO - 24/10/2017
    loadTipoTelefono("ddlTipoFono", 0); //AGREGADO NUEVO - 24/10/2017
    loadTipoRedes("ddlTipoRedes", 0);  //AGREGADO NUEVO - 24/10/2017
    //--------------------------------------------


    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaInspeccion").datepicker({ dateFormat: 'dd/mm/yy' });
    //Direccion
    initFormDireccion({ id: K_ID_POPUP_DIR, parentControl: "divDireccion", width: 850, height: 400, evento: addDireccion, modal: true });

    $("#addCaracteristica").on("click", function () {
        $("#mvCaracteristica").dialog({});
        $("#mvCaracteristica").dialog("open");
        limpiarCaracteristica();
    });

    $("#addParametro").on("click", function () {
        $("#mvParametro").dialog({});
        $("#mvParametro").dialog("open");
        limpiarParametro();
    });

    $("#addObservacion").on("click", function () {
        $("#mvObservacion").dialog({});
        $("#mvObservacion").dialog("open");
        limpiarObservacion();
    });

    $("#txtNroIdentificacion").attr("disabled", "disabled");
    $("#btnBuscar").attr("disabled", "disabled");

    $("#ddlTipoestablecimiento").on("change", function () {
        var codigo = $("#ddlTipoestablecimiento").val();
        var subcodigo = $("#ddlSubtipoestablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo);
    });

    $("#ddlSubtipoestablecimiento").on("change", function () {
        var codigo = $("#ddlTipoestablecimiento").val();
        var subcodigo = $("#ddlSubtipoestablecimiento").val();
        //alert("est:" + codigo + ",subtipo:" + subcodigo);
        //if (K_ACCION=K_ACCION_ACTUAL) {
        //    alert("Insertara")
        //} else {
        //    alert("Modificara");
        //}
       K_VALIDA.CARACTERISTICA_PREDEFINIDA= ObtenerCaracteristicasXSubtipo(codigo, subcodigo);
    });

    $("#tabs").tabs();
    $("#mvCaracteristica").dialog({ autoOpen: false, width: K_WIDTH_CAR, height: K_HEIGHT_CAR, buttons: { "Agregar": addCaracteristica, "Cancel": function () { $("#mvCaracteristica").dialog("close"); $('#txtvalor').css({ 'border': '1px solid gray' }); } }, modal: true });
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_PAR, height: K_HEIGHT_PAR, buttons: { "Agregar": addParametro, "Cancel": function () { $("#mvParametro").dialog("close"); $('#txtDescripcion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addObservacion, "Cancel": function () { $("#mvObservacion").dialog("close"); $('#txtObservacion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_DOC, height: K_HEIGHT_DOC, buttons: { "Agregar": addDocumento, "Cancel": function () { $("#mvDocumento").dialog("close"); $('#txtFecha').css({ 'border': '1px solid gray' }); } }, modal: true });
    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    //$("#mvDivision").dialog({ autoOpen: false, width: K_WIDTH_DIV, height: K_HEIGHT_DIV, buttons: { "Agregar": AddDivision, "Cancelar": function () { $("#mvDivision").dialog("close"); } }, modal: true });
    $("#mvDifusion").dialog({ autoOpen: false, width: K_WIDTH_DIF, height: K_HEIGHT_DIF, buttons: { "Agregar": AddDifusion, "Cancelar": function () { $("#mvDifusion").dialog("close"); } }, modal: true });
    $("#mvAgregarResponsable").dialog({ autoOpen: false, width: 500, height: 330, buttons: { "Grabar": GrabarReponsable, "Cancelar": function () { $("#mvAgregarResponsable").dialog("close"); } }, modal: true });
    //
    $("#mvListarDireccion").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": AddDireccionesAll, "Cancelar": function () { $("#mvListarDireccion").dialog("close"); } }, modal: true });

    //AGREGADO NUEVO - 24/10/2017
    $("#mvTelefono").dialog({ autoOpen: false, width: 550, height: 280, buttons: { "Agregar": addTelefono, "Cancelar": function () { $("#mvTelefono").dialog("close"); } }, modal: true });
    $("#mvCorreo").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addCorreo, "Cancelar": function () { $("#mvCorreo").dialog("close"); } }, modal: true });
    $("#mvRedes").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addRedes, "Cancelar": function () { $("#mvRedes").dialog("close"); } }, modal: true });
    $("#mvEntidad").dialog({ autoOpen: false, width: 800, height: 260, buttons: { "Agregar": AddAsociado, "Cancelar": function () { $("#mvEntidad").dialog("close"); } }, modal: true });
    //------------------------------------

    $("#mvGeoreferenciacion").dialog({
        autoOpen: false,
        width: 1000,
        height: 1000,
        buttons: {
            "Grabar": function () { $("#mvGeoreferenciacion").dialog("close"); }
        },
        modal: true
    });



    $(".addListarDireccion").on("click", function () { ListarDireccionxPerfil($("#hidCodigoBPS").val()); $("#mvListarDireccion").dialog("open"); });
    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $(".addCaracteristica").on("click", function () { limpiarCaracteristica(); $("#mvCaracteristica").dialog("open"); });
    //$(".addEntidad").on("click", function () { limpiarAsociado(); $("#mvBuscarSocio").dialog("open"); });
    //$(".addDivision").on("click", function () { limpiarDivision(); $("#mvDivision").dialog("open"); });
    $(".addDifusion").on("click", function () { limpiarDifusion(); $("#mvDifusion").dialog("open"); });

    ////AGREGADO NUEVO - 24/10/2017
    $(".addTelefono").on("click", function () { limpiarTelefono(); $("#mvTelefono").dialog("open"); });
    $(".addCorreo").on("click", function () { limpiarCorreo(); $("#mvCorreo").dialog("open"); });
    $(".addRedes").on("click", function () { limpiarRedes(); $("#mvRedes").dialog("open"); });
    $(".addEntidad").on("click", function () { limpiarAsociado(); $("#mvEntidad").dialog("open"); });
    $(".addGeoreferenciacion").on("click", function () { $("#mvGeoreferenciacion").dialog("open"); });

    //mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarSocioAux({ container: "ContenedormvBuscarResponsable", idButtonToSearch: "btnBuscarResponsable", idDivMV: "mvBuscarSocioResponsable", event: "reloadEventoResponsable", idLabelToSearch: "lbResponsableSocio" });

    $("#ddlDivisionTipo").on("change", function () {
        var tipo = $(this).val();
        loadTipoDivision('ddlDivision', tipo);//lista diviones por tipo de division
    })

    $("#ddlDivision").on("change", function () {
        var tipo = $(this).val();
        loadSubdivision('ddlSubDivision', tipo);//lista sub diviones por division
    })

    $("#ddlSubDivision").on("change", function () {
        var subtipo = $(this).val();
        loadDivisionValor('ddlValorSubDivision', subtipo);
    })

    $("#ddlValorSubDivision").on("change", function () {
        var id = $(this).val();
        $("#hidDadvid").val(id);
    })

    $("#ddlSubdivision").on("change", function () {
        loadDependecia($(this).val(), '');
    })

    var eventoKP = "keypress";
    $('#txtNroDifusion').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtNumeroDocRes').on(eventoKP, function (e) { return solonumeros(e); });

    $('#txtvalor').on(eventoKP, function (e) { return solonumeros(e); });

    $("#btnGrabar").on("click", function () {
        var estado = addvalidacionAsociado();
        if (estado) {

            //Solo valida que las caracteristicas predefinidas no sean 0
            var Respuesta = ValidarCaractersiticasPredefinidas();
            //alert(Respuesta);
          //  if (Respuesta == 1) {
                grabar();
           // }
        };
    }).button();

    $("#btnNuevo").on("click", function () {
        location.href = "../Establecimiento/Create";
    }).button();

    $("#btnBuscar").on("click", function () {
        consultarSocio();
    });

    $("#btnBuscarSocio").on("click", function () {
        buscarSocio();
    });

    $("#ddlTipoPersonaRes").on("change", function () {
        if ($("#ddlTipoPersonaRes option:selected").text() == 'Natural') {
            if ($("#ddlTipoDocumentoRes option:selected").text() == 'NINGUNO') {
                $('#txtPaterno').removeAttr('disabled');
                $('#txtMaterno').removeAttr('disabled');
                $('#txtNombres').removeAttr('disabled');
                $('#txtRazonSocialRes').attr('disabled', 'disabled');
            }
        }
        else {
            $('#txtPaterno').attr('disabled', 'disabled');
            $('#txtMaterno').attr('disabled', 'disabled');
            $('#txtNombres').attr('disabled', 'disabled');
            $('#txtRazonSocialRes').removeAttr('disabled');
        }

        if ($("#ddlTipoPersonaRes option:selected").text() == 'Jurídica') {
            if ($("#ddlTipoDocumentoRes option:selected").text() == 'DNI') {
                $('#txtPaterno').removeAttr('disabled');
                $('#txtMaterno').removeAttr('disabled');
                $('#txtNombres').removeAttr('disabled');
                $('#txtRazonSocialRes').attr('disabled', 'disabled');
            }
        }
    });

    $("#ddlTipoDocumentoRes").on("change", function () {
        $('#txtNumeroDocRes').val('');
        $("#txtRazonSocialRes").val("");
        $("#txtPaterno").val("");
        $("#txtMaterno").val("");
        $("#txtNombres").val("");

        $('#txtNumeroDocRes').css({ 'border': '1px solid gray' });

        $('#txtPaterno').css({ 'border': '1px solid gray' });
        $('#txtMaterno').css({ 'border': '1px solid gray' });
        $('#txtNombres').css({ 'border': '1px solid gray' });
        $('#txtRazonSocialRes').css({ 'border': '1px solid gray' });
        msgErrorB("divResultValidarDoc", "");

        if ($("#ddlTipoDocumentoRes option:selected").text() == 'NINGUNO') {
            $('#txtNumeroDocRes').attr('disabled', 'disabled');
        }
        else {
            $('#txtNumeroDocRes').removeAttr('disabled');
        }

        if ($("#ddlTipoDocumentoRes option:selected").text() == 'DNI') {
            $('#txtPaterno').removeAttr('disabled');
            $('#txtMaterno').removeAttr('disabled');
            $('#txtNombres').removeAttr('disabled');
            $('#txtRazonSocialRes').attr('disabled', 'disabled');
        }
        else {
            $('#txtPaterno').attr('disabled', 'disabled');
            $('#txtMaterno').attr('disabled', 'disabled');
            $('#txtNombres').attr('disabled', 'disabled');
            $('#txtRazonSocialRes').removeAttr('disabled');
        }
        if ($("#ddlTipoDocumentoRes option:selected").text() == 'NINGUNO' && $("#ddlTipoPersonaRes option:selected").text() == 'Natural') {
            $('#txtPaterno').removeAttr('disabled');
            $('#txtMaterno').removeAttr('disabled');
            $('#txtNombres').removeAttr('disabled');
            $('#txtRazonSocialRes').attr('disabled', 'disabled');
        }
    });

    $('#txtNumeroBPSaux').on(eventoKP, function (e) { return solonumeros(e); });

    $("#btnAgregarResponsable").on("click", function () {
        $("#mvAgregarResponsable").dialog({});
        $("#mvAgregarResponsable").dialog("open");
        msgErrorB("divResultValidarDoc", "");
        LimpiarResponsable();
    });

    $("#btnVolver").on("click", function () {
        var estid = $("#hidCodigoEST").val();
        if (estid == "")
            estid = 0;
        var respuestaEstModif = validarEstablecimientoModificado(estid);//mantenimiento.administracion.socio

        if (respuestaEstModif == "1")// regresa a la pantalla de administracion
        {
            location.href = "../AdministracionEstablecimiento/";
        } else {
            location.href = "../Establecimiento/";
        }



    }).button();

    $(document).ready(function () {
        $("#txtNroIdentificacion").keydown(function (event) {
            if (event.shiftKey) {
                event.preventDefault();
            }

            if (event.keyCode == 46 || event.keyCode == 8) {
            }
            else {
                if (event.keyCode < 95) {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        event.preventDefault();
                    }
                }
                else {
                    if (event.keyCode < 96 || event.keyCode > 105) {
                        event.preventDefault();
                    }
                }
            }
        });
    });

    $("#txtDocumento").on("keypress", function (e) {
        return solonumeros(e);
    });

    //$('a[rel="external"]').click(function () {
    //    this.target = "_blank";
    //});

    //Validar Si El establecimiento se encuentra En una Licencia

    ValidarEstablecimientoModif($("#hidCodigoEST").val());

    //Recupera el Codigo
    var validaBPSID = $("#hidCodigoBPS").val();
});

function ListarDireccionxPerfil(codeEdit) {
    $.ajax({
        url: '../Establecimiento/ListarDireccionxPerfil',
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


function addvalidacionAsociado() {
    var result = true;
    $('#tbAsociado tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        if (!isNaN(id)) {
            //alert(id + " " + $('#ddlAplicable_' + id).val());
            if ($('#ddlAplicable_' + id).val() == 0) {
                $('#ddlAplicable_' + id).css({ 'border': '1px solid red' });
                result = false;
            }
            else {
                $('#ddlAplicable_' + id).css({ 'border': '1px solid gray' });
            }
        }
    });
    return result;
}

function LimpiarResponsable() {
    $("#ddlTipoPersonaRes").val(0);
    $("#ddlTipoPersonaRes").val(0);
    $("#ddlTipoDocumentoRes").val(0);
    $("#txtNumeroDocRes").val("");
    $("#txtRazonSocialRes").val("");
    $("#txtPaterno").val("");
    $("#txtMaterno").val("");
    $("#txtNombres").val("");
    $('#txtPaterno').css({ 'border': '1px solid gray' });
    $('#txtMaterno').css({ 'border': '1px solid gray' });
    $('#txtNombres').css({ 'border': '1px solid gray' });
    $('#txtRazonSocialRes').css({ 'border': '1px solid gray' });
}

function validarRazonSocial() {

    msgError("");
    var itemChange = $("#ddlTipoDocumentoRes option:selected").text();
    var exito = true;

    if (itemChange === "RUC") {
        var elem = $.trim($("#txtRazonSocialRes").val());
        if (elem === '') {
            $("#txtRazonSocialRes").css({ 'border': '1px solid red' });
            exito = false;

        } else {
            $("#txtRazonSocialRes").css({ 'border': '1px solid gray' });
        }
        if (exito == false) msgError("Debe ingresar los campos requeridos dni ");

        //|| (itemChange === "NINGUNO" && $("#ddlTipoPersonaRes option:selected").text() == 'Natural')

    } else if (itemChange === "DNI") {
        var elemNom = $.trim($("#txtNombres").val());
        var elemMa = $.trim($("#txtMaterno").val());
        var elemPa = $.trim($("#txtPaterno").val());

        if (elemNom === '') {
            $("#txtNombres").css({ 'border': '1px solid red' });
            exito = false;
        }
        else {
            $("#txtNombres").css({ 'border': '1px solid gray' });
        }

        if (!exito && elemMa === '') {
            $("#txtMaterno").css({ 'border': '1px solid red' });
            exito = false;
        }
        else {
            $("#txtMaterno").css({ 'border': '1px solid gray' });
        }

        if (!exito && elemPa === '') {
            $("#txtPaterno").css({ 'border': '1px solid red' });
            exito = false;
        }
        else {
            $("#txtPaterno").css({ 'border': '1px solid gray' });
        }


        if (exito == false) msgError("Debe ingresar los campos requeridos ruc ");
    }
    return exito;
}

function GrabarReponsable() {
    var estado = validarLongitudNumDoc();
    if (estado) {
        if (validarRazonSocial()) {

            var idBps = 0;
            var socio = {
                Codigo: idBps,
                TipoEntidad: $("#ddlTipoPersonaRes").val(),
                TipoPersona: $("#ddlTipoPersonaRes").val(),
                TipoDocumento: $("#ddlTipoDocumentoRes").val(),
                NumDocumento: $("#txtNumeroDocRes").val(),
                RazonSocial: $("#txtRazonSocialRes").val(),
                EsUsuDerecho: 0,
                EsGrupoEmp: 0,
                EsRecaudador: 0,
                EsEmpleador: 0,
                EsProveedor: 0,
                EsAsociacion: 0,
                Nombres: $("#txtPaterno").val(),
                Paterno: $("#txtMaterno").val(),
                Materno: $("#txtNombres").val()
            };

            $.ajax({
                url: '../Socio/Insertar',
                data: socio,
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        //alert(dato.message);
                        msgErrorB("divResultValidarDoc", dato.message);
                        //loadDataFoundSocio();
                        $("#mvAgregarResponsable").dialog("close");
                    } else if (dato.result == 0) {
                        //alert(dato.message);
                        msgErrorB("divResultValidarDoc", dato.message);
                    }
                }
            });
        }
    }
};

//var reloadEvento = function (idSel) {
//    $("#hidAsocioado").val(idSel);
//    $.ajax({
//        data: { codigoBps: idSel },
//        url: '../General/ObtenerNombreSocio',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                $("#hidNombreAsoc").val(dato.valor);
//                AddAsociado($("#hidAsocioado").val(), $("#hidNombreAsoc").val());

               
//            }
//        }
//    });
//};

var reloadEventoResponsable = function (idSel) {
    $("#hidCodigoBPS").val(idSel);
    validaBPSID = $("#hidCodigoBPS").val();
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsableSocio").html(dato.valor);
                //Recuperando id del socio
                
            }
        }
    });
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsableSocio").html(dato.valor);
            }
        }
    });
}

function validarRol(control, id) {
    $.ajax({
        data: { idRol: control.value, idAsociado: id },
        url: '../Establecimiento/ValidacionPerfil',
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

            } else {
                alert(dato.message);
                loadRol(control.id, 0);
            }
        }
    });
}

//function UpdRol(id, idAsoc) {

//    $('#ddlAplicable_' + id).css({ 'border': '1px solid gray' });

//    var rol = {
//        idRol: $("#ddlAplicable_" + id).val(),
//        idAsociado: idAsoc
//    };
//    $.ajax({
//        data: rol,
//        url: 'UpdRol',
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//            } else {
//                $('#ddlAplicable_' + id).val(0);
//                alert(dato.message);
//            }
//        }
//    });
//}

//function validarGrabarAsociados() {
//    var estado = false;
//    $.ajax({
//        url: '../Establecimiento/validacionGrabarAsociados',
//        type: 'GET',
//        async: false,
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 0) {
//                estado = false;
//                alert(dato.message);
//            } else {
//                estado = true;
//            }
//        }
//    });
//    return estado;
//}

//function loadDataAsociado() {
//    loadDataGridAsociaodsTmp('ListarAsociado', "#gridAsociado");
//}

//function loadDataGridAsociaodsTmp(Controller, idGrilla) {
//    $.ajax({
//        type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) {
//            var dato = response; $(idGrilla).html(dato.message);
//            addRoles();
//        }
//    });
//}

function addRoles() {
    $('#tbAsociado tr').each(function () {
        var id = parseFloat($(this).find("td").eq(0).html());
        var idRol = parseFloat($(this).find("td").eq(6).html());
        if (!isNaN(id)) {
            if (idRol == undefined)
                idRol = 0;
            loadRol("ddlAplicable_" + id, idRol);
        }
    });
}

//function AddAsociado(idAsociado, Nombre) {
//    var IdAdd = 0;
//    if ($("#hidAccionMvEnt").val() == "1") IdAdd = $("#hidEdicionEnt").val();
//    var entidad = {
//        Id: IdAdd,
//        Codigo: $("#hidCodigoBPS").val(),
//        CodigoAsociado: idAsociado,
//        NombreAsociado: Nombre,
//        RolTipo: "",
//        RolTipoDesc: "",
//        NroDocAsociado: $("#txtDocumento").val()
//    };
//    if (ValidarRequeridosET()) {
//        $.ajax({
//            url: 'AddAsociado',
//            type: 'POST',
//            data: entidad,
//            beforeSend: function () { },
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato);
//                if (dato.result == 1) {
//                    loadDataAsociado();
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//        $("#avisoMVEntidad").html('');
//        $("#mvEntidad").dialog("close");
//    }
//}

//function obtenerMatrizAsociado() {
//    var Asociado = [];
//    var contador = 0;
//    $('#tbAsociado tr').each(function () {
//        var id = parseFloat($(this).find("td").eq(0).html());
//        if (!isNaN(id)) {
//            Asociado[contador] = {
//                //Id :
//                Id: $('#txtId_' + id).val(),
//                CodigoAsociado: $('#txtCodigoAsociado_' + id).val(),
//                NombreAsociado: $('#txtNombreAsociado_' + id).val(),
//                RolTipo: $('#ddlAplicable_' + id).val(),
//                RolTipoDesc: $('#ddlAplicable_' + id + " option:selected").text()
//            };
//            contador += 1;
//        }
//    });

//    var Asociado = JSON.stringify({ 'Asociado': Asociado });

//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'POST',
//        url: '../Establecimiento/ObtenerMatrizAsociados',
//        data: Asociado,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {

//            }
//            else {
//                alert(dato.message);
//            }
//        },
//        failure: function (response) {
//            $('#result').html(response);
//        }
//    });
//}

function Salir() {
    c = confirm('¿Desea salir, se perderán los cambios?');
    if (c == true) {
        location.href = "../Establecimiento";
    }
    else {
        return false;
    }
}

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

function validarLongitudNumDoc() {
    msgError("");
    var exito = false;
    var tipoDoc = $("#ddlTipoDocumentoRes").val();
    var tipoDocDesc = $("#ddlTipoDocumentoRes option:selected").text();
    var numValidar = "";
    getValorConfigNumDoc(tipoDoc);

    numValidar = $("#hidCantNumValidar").val();

    if (tipoDocDesc != "NINGUNO") {
        if ($.trim($("#txtNumeroDocRes").val()) != "") {
            if (tipoDocDesc == "DNI") {
                if ($("#txtNumeroDocRes").val().length != 8) {
                    msgErrorB("divResultValidarDoc", "Longitud del DNI debe contener " + "8" + " dígitos.");
                } else {
                    exito = true;
                    msgErrorB("divResultValidarDoc", "");
                }
            } else if (tipoDocDesc == "RUC") {
                if ($("#txtNumeroDocRes").val().length != 11) {
                    msgErrorB("divResultValidarDoc", "Longitud del RUC debe contener " + "11" + " dígitos.");
                } else {
                    exito = true;
                    msgErrorB("divResultValidarDoc", "");
                }
            }
            if (exito) {
                return true;
            } else {
                return false;
            }
        }
        else {
            $("#txtNumeroDocRes").css({ 'border': '1px solid gray' });
            msgErrorB("divResultValidarDoc", "Ingrese número de documento.");
        }
    }
    else {
        exito = true;
    }
}

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
//david Aqui es donde se Deberian de Recuperar las Caracteristicas Definidas Automaticamente
function ObtenerCaracteristicasXSubtipo(id, subid) {
    var respuesta = 0;
    $.ajax({
        url: '../Establecimiento/ObtenerCaracteristicasXSubtipo',
        data: { idTipoEstablecimiento: id, idSubtipoEstablecimiento: subid },
        async :false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            respuesta = 1;
            loadDataCaracteristica();
        }
    });
    return respuesta;
}

function QuitarCaracteristicasXSubtipo(id) {
    $.ajax({
        url: '../Establecimiento/QuitarCaracteristicasXSubtipo',
        data: { idTipoEstablecimiento: id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            loadDataCaracteristica();
        }
    });
}

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Establecimiento/Obtiene',
        data: { id: idSel },
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var establecimiento = dato.data.Data;
                if (establecimiento != null) {
                    $("#hidCodigoEST").val(establecimiento.Codigo);
                    $("#hidCodigoBPS").val(establecimiento.CodigoSocio);
                    $("#txtNroIdentificacion").val(establecimiento.NumeroIdentificacionfiscal);
                    $("#txtRazonSocial").val(establecimiento.SocioNombre);
                    $("#txtEstablecimiento").val(establecimiento.Nombre);
                    $("#CodigoUbigeoLAT").val(establecimiento.Latitud);
                    $("#CodigoUbigeoLON").val(establecimiento.Longitud);
                    loadTipoIdentificacion(establecimiento.CodigoTipoidentificacionfiscal);
                    //$('#ddlTipoestablecimiento option').remove();
                    //cambio david
                    loadTipoestablecimiento('ddlTipoestablecimiento', establecimiento.TipoEstablecimiento);
                    //loadTipoestablecimiento('ddlTipoestablecimiento', 2);
                    //$('#ddlTipoestablecimiento').val(establecimiento.TipoEstablecimiento);
                    //nombre de los sub tipos ListarSubtipoEstablecimientoPorTipo
                    //asi debe estar loadSubTipoestablecimientoPorTipo
                   // loadSubTipoestablecimiento(establecimiento.SubTipoestablecimiento);
                    //david cambio
                    loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', establecimiento.TipoEstablecimiento, establecimiento.SubTipoestablecimiento);

                    loadDivisionesadministrativas(establecimiento.Codigodivision);
                    loadDivisionesFiscales(establecimiento.CodigoDivisionfiscal);
                    obtenerNombreSocio($("#hidCodigoBPS").val());

                    $("#btnBuscarResponsable").css("display", "none");
                    loadDataDireccion();
                    loadDataObservacion();
                    loadDataDocumento();
                    loadDataParametro();
                    loadDataCaracteristica();
                    loadDataAsociado();
                    loadDataDivisiones();
                    loadDataDifusion();
                    loadDataLicencia();

                    loadDataTelefono();
                    loadDataCorreo();
                    loadDataRedes();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function grabar() {
    msgOkB(K_DIV_MESSAGE.DIV_ESTABLECIMIENTO, "");
    document.getElementById('lbResponsableSocio').style.color = "black";
    $('#txtObservacion').css({ 'border': '1px solid gray' });
    var estid = $("#hidCodigoEST").val();
    var respuestaEstModif = validarEstablecimientoModificado(estid);//mantenimiento.administracion.socio
    var estadoIdSocio = 0;
    var IdSocio = $("#hidCodigoBPS").val();
    if (IdSocio == 0 || IdSocio == "" || IdSocio == undefined)
        estadoIdSocio = false;
    else
        estadoIdSocio = true;

    if (!estadoIdSocio)
        document.getElementById('lbResponsableSocio').style.color = "red";
    else
        document.getElementById('lbResponsableSocio').style.color = "black";

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_ESTABLECIMIENTO, K_DIV_VALIDAR.DIV_CAB)) {
        if (estadoIdSocio) {
            //var estado = validarGrabarAsociados();
            //if (estado) {
            if ($("#CodigoUbigeoLAT").val() != '' & $("#CodigoUbigeoLAT").val()!='') {
                if (respuestaEstModif != "3") {
                    var idEst = 0;
                    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) idEst = $("#hidCodigoEST").val();
                    var idtipoestablecimiento = $("#ddlTipoestablecimiento").val();
                    var idsubtipoestablecimiento = $("#ddlSubtipoestablecimiento").val();
                    var establecimiento = {
                        EST_ID: idEst,
                        EST_NAME: $("#txtEstablecimiento").val(),
                        ESTT_ID: idtipoestablecimiento,
                        DAD_ID: 1,
                        SUBE_ID: idsubtipoestablecimiento,
                        DIF_ID: 1, //por pruebas.... este campo se borrara
                        BPS_ID: $("#hidCodigoBPS").val(),
                        ROL_ID: $("#ddlRol").val(),
                        LATITUD: $("#CodigoUbigeoLAT").val() == "" ? 0 : $("#CodigoUbigeoLAT").val(),
                        LONGITUD: $("#CodigoUbigeoLON").val() == "" ? 0 : $("#CodigoUbigeoLON").val()
                        
                    };
                    $.ajax({
                        url: '../Establecimiento/Insertar',
                        data: establecimiento,
                        type: 'POST',
                        beforeSend: function () { },
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato);
                            if (dato.result == 1) {
                                alert(dato.message);
                                location.href = "../Establecimiento/";
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });
                } else {
                    alert("EL ESTABLECIMIENTO NO PUEDE SER MODIFICADO DEBIDO A QUE YA FUE VALIDADO");
                }
            } else {
                alert("POR FAVOR DE INGRESAR LA DIRECCION EN LA PESTAÑA GEOREFERENCIACION | GOOGLE MAPS");
            }
            //};
        }
        else {
            msgErrorB(K_DIV_MESSAGE.DIV_ESTABLECIMIENTO, "Debe ingresar los campos requeridos");
        }
    };
};

function AddDireccionesAll() {
    var Direcciones = [];
    var contador = 0;
    $('#tbDireccionAll tr').each(function () {
        var id = parseFloat($(this).find("td").eq(2).html());
        var checked = $(this).find('.chksel').attr('checked');
        if (checked == "checked") {
            if (!isNaN(id)) {
                Direcciones[contador] = {
                    Id: $('#txtId_' + id).val()
                };
                contador += 1;
            }
        }
    });

    var Direcciones = JSON.stringify({ 'Direcciones': Direcciones });

    if (contador == 0) {
        alert("Seleccione uno o mas Direcciones");
        return;
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Establecimiento/ObtenerDireccionesAll',
        data: Direcciones,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#mvListarDireccion").dialog("close");
                loadDataDireccion();
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

//-------------------------------------------
//Primero llega aqui y valida luego pasa a AddDir-
function addDireccion() {

    var ubigeo = $("#hidCodigoUbigeo").val();
    if (ubigeo == 0 || ubigeo == '') {
        alert('Seleccione un Ubigeo antes de registrar.');
    } else {
        var IdAdd = 0;
        if ($("#hidAccionMvDir").val() == "1") IdAdd = $("#hidEdicionDir").val()
        $("#avisoMV").val("");

        if (ValidarRequeridosMV()) {
            if (validarUbigeoXOficia(ubigeo)) {
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
                    TipoTerritorioDesc: $("#ddlTerritorio option:selected").text(),
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
    }
}


function addObservacion() {
    if ($("#txtObservacion").val() == '') {
        $('#txtObservacion').css({ 'border': '1px solid red' });
    } else {

        var IdAdd = 0;
        if ($("#hidAccionMvObs").val() === "1") IdAdd = $("#hidEdicionObs").val();
        var observacion = {
            Id: IdAdd,
            TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
            Observacion: $("#txtObservacion").val(),
            TipoObservacionDesc: $("#ddlTipoObservacion option:selected").text()
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
        $('#txtObservacion').css({ 'border': '1px solid gray' });
        $("#mvObservacion").dialog("close");
    }
}

function addDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
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
            url: 'AddDocumento',
            type: 'POST',
            data: documento,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    if ($("#file_upload").val() != "") {
                        InitUploadTabDocEstablecimiento("file_upload", dato.Code);
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
    //if (validarRedSocial(entidad.Link)) {
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
    //} else {
    //    $("#avisoMVRedes").html('<br/><span style="color:red;">Error: La Url es incorrecta.</span><br/>');
    //}
}

function AddAsociado() {

    var IdAdd = 0;
    if ($("#hidAccionMvEnt").val() == "1") IdAdd = $("#hidEdicionEnt").val();
    var entidad = {
        Id: IdAdd,
        Codigo: $("#hidBpsId").val(),
        CodigoAsociado: $("#txtSocioAsociado").val(),
        NombreAsociado: $("#txtNombre").val(),
        RolTipo: $("#ddlRol option:selected").val(),
        RolTipoDesc: $("#ddlRol option:selected").text(),
        NroDocAsociado: $("#txtDocumento").val()
    };
    if (ValidarRequeridosET()) {
        $.ajax({
            url: 'AddAsociado',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    loadDataAsociado();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#avisoMVEntidad").html('');
        $("#mvEntidad").dialog("close");
    }
}

function limpiarDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#hidNombreFile").val("");
    $('#file_upload').css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#file_upload").val("");
}
//GUIARME DE AQUI------------------------------------------

function addParametro() {
    if ($("#txtDescripcion").val() == '') {
        $('#txtDescripcion').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        if ($("#hidAccionMvPar").val() === "1") IdAdd = $("#hidEdicionPar").val();

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
        $('#txtDescripcion').css({ 'border': '1px solid gray' });
    }
}

var addCaracteristica = function () {
    if ($("#txtvalor").val() == '') {
        $('#txtvalor').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        if ($("#hidAccionMvCar").val() === "1") IdAdd = $("#hidEdicionCar").val();

        var entidad = {
            Id: IdAdd,
            Idcaracteristica: $("#ddlCaracteristica").val(),
            caracteristica: $("#ddlCaracteristica option:selected").text(),
            TipoEstablecimiento: $("#ddlTipoestablecimiento option:selected").val(),
            IdSubTipoEstablecimiento: $("#ddlSubtipoestablecimiento option:selected").val(),
            Valor: $("#txtvalor").val()
        };

        $.ajax({
            url: 'AddCaracteristica',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataCaracteristica();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvCaracteristica").dialog("close");
        $('#txtvalor').css({ 'border': '1px solid gray' });
        $("#hidEdicionCar").val("");

        //Si Trajo Caracteristicas Predefinida Mostrar Valores

        //if (K_VALIDA.CARACTERISTICA_PREDEFINIDA == 1) {
        //    CalularTarifa();
        //}
    }
}

//function AddDivision() {
//    if ($("#ddlDivisionTipo").val() == '0' || $("#ddlDivision").val() == '0' || $("#ddlSubDivision").val() == '0' || $("#ddlValorSubDivision").val() == '0') {
//        $('#ddlDivisionTipo').css({ 'border': '1px solid red' });
//        $('#ddlDivision').css({ 'border': '1px solid red' });
//        $('#ddlSubDivision').css({ 'border': '1px solid red' });
//        $('#ddlValorSubDivision').css({ 'border': '1px solid red' });
//    } else {
//        var IdAdd = 0;
//        if ($("#hidAccionMvDiv").val() === "1") IdAdd = $("#hidEdicionDiv").val();

//        var entidad = {
//            Id: IdAdd,
//            idEstablecimiento: $("#hidCodigoEST").val(),
//            idTipoDivision: $("#ddlDivisionTipo").val(),
//            idDivision: $("#ddlDivision").val(),
//            idSubTipoDivision: $("#ddlSubDivision").val(),
//            idDivisionValor: $("#ddlValorSubDivision").val(),
//            auxidDivisionValor: $("#ddlValorSubDivision").val(),
//            TipoDivision: $("#ddlDivisionTipo option:selected").text(),
//            Division: $("#ddlDivision option:selected").text(),
//            SubTipoDivision: $("#ddlSubDivision option:selected").text(),
//            DivisionValor: $("#ddlValorSubDivision option:selected").text()
//        };
//        $.ajax({
//            url: 'AddDivision',
//            type: 'POST',
//            data: entidad,
//            beforeSend: function () { },
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato);
//                if (dato.result == 1) {
//                    loadDataDivisiones();
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//        $("#mvDivision").dialog("close");
//        $('#ddlDivisionTipo').css({ 'border': '1px solid gray' });
//        $('#ddlDivision').css({ 'border': '1px solid gray' });
//        $('#ddlSubDivision').css({ 'border': '1px solid gray' });
//        $('#ddlValorSubDivision').css({ 'border': '1px solid gray' });
//    }
//}

function AddDifusion() {
    if ($("#ddlDifusion").val() == '0' || $("#txtNroDifusion").val() == '') {
        $('#ddlDifusion').css({ 'border': '1px solid red' });
        $('#txtNroDifusion').css({ 'border': '1px solid red' });
    }
    else {
        var IdAdd = 0;
        if ($("#hidAccionMvDif").val() === "1") IdAdd = $("#hidEdicionDif").val();

        var entidad = {
            Id: IdAdd,
            idEstablecimiento: $("#hidCodigoEST").val(),
            idDifusion: $("#ddlDifusion").val(),
            Difusion: $("#ddlDifusion option:selected").text(),
            NroDifusion: $("#txtNroDifusion").val(),
            almacenamiento: $("#chkalmacenamiento").prop("checked")
        };
        $.ajax({
            url: 'AddDifusion',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDifusion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvDifusion").dialog("close");
        $('#ddlDifusion').css({ 'border': '1px solid gray' });
        $('#txtNroDifusion').css({ 'border': '1px solid gray' });
    }
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}
//Manda aqui
function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
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

function loadDataCaracteristica() {
    loadDataGridTmp('ListarCaracteristica', "#gridCaracteristica");
}

function loadDataDivisiones() {
    loadDataGridTmp('ListarDivisiones', "#gridDivisiones");
}

function loadDataDifusion() {
    loadDataGridTmp('ListarDifusion', "#gridDifusion");
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

function loadDataAsociado() {
    loadDataGridTmp('ListarAsociado', "#gridAsociado");
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

function delAddCaracteristica(idDel) {
    $.ajax({
        url: 'DellAddCaracteristica',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataCaracteristica();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
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

function delAddInscepccion(idDel) {
    $.ajax({
        url: 'DellAddInscepccion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataInscepccion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddAsociado(idDel) {
    $.ajax({
        url: 'DellAddAsociado',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataAsociado();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

//function delAddDivision(idDel) {
//    $.ajax({
//        url: 'DellAddDivision',
//        type: 'POST',
//        data: { id: idDel },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                loadDataDivisiones();
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//    return false;
//}

function delAddDifusion(idDel) {
    $.ajax({
        url: 'DellAddDifusion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDifusion();
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

var ObtenerSocio = function () {
    var idtipo = $("#ddlTipoIdentificacion").val();
    var doc = $("#txtNroIdentificacion").val();

    $.ajax({
        url: '../General/BuscarsocioTipoDocumento',
        type: 'POST',
        data: { idTipoDocumento: idtipo, nroDocumento: doc },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#hidCodigoBPS").val(dato.Code);
                $("#txtRazonSocial").val(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
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
                    alert("No se pudo obtener la observación para editar.");
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
                    $("#ddlTipoDocumento").val(doc.TipoDocumento);

                    $("#txtFecha").val(doc.FechaRecepcion);

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

function updAddInscepccion(idUpd) {
    limpiarInscepccion();

    $.ajax({
        url: 'ObtieneInscepccionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var ins = dato.data.Data;
                if (ins != null) {
                    $("#hidAccionMvIns").val("1");
                    $("#hidEdicionIns").val(ins.Id);
                    //$("#txtFileinscepccion").val('ins.Documento'); 
                    $("#txtFechaInspeccion").val(ins.FechaInspeccion);
                    $("#txtHora").val(ins.HoraInspeccion);
                    $("#txtObservacioninspeccion").val(ins.Observacion);
                    $("#mvInspections").dialog("open");
                } else {
                    alert("No se pudo obtener el documento para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function updAddCaracteristica(idUpd) {
    limpiarCaracteristica();

    $.ajax({
        url: 'ObtieneCaracteristicaTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var car = dato.data.Data;
                if (car != null) {
                    $("#hidAccionMvCar").val("1");
                    $("#hidEdicionCar").val(car.Id);

                    $("#ddlCaracteristica").val(car.Idcaracteristica);
                    $("#txtvalor").val(car.Valor);
                    $("#mvCaracteristica").dialog("open");
                    //$("#ddlCaracteristica").attr("disabled", "disabled");
                } else {
                    alert("No se pudo obtener la caracteristica para editar.");
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

function updAddAsociado(idUpd) {
    limpiarAsociado();

    $.ajax({
        url: 'ObtieneAsociadoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var aso = dato.data.Data;
                if (aso != null) {
                    $("#hidAccionMvEnt").val("1");
                    $("#hidEdicionEnt").val(aso.Id);
                    $("#txtNombre").val(aso.NombreAsociado);
                    $("#ddlRol").val(aso.RolTipo);
                    $("#txtSocioAsociado").val(aso.CodigoAsociado);
                    $("#txtDocumento").val(aso.NroDocAsociado);
                    $("#mvEntidad").dialog("open");
                } else {
                    alert("No se pudo obtener al asociado para editar.");
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

//function updAddDivision(idUpd) {
//    limpiarDivision();
//    $.ajax({
//        url: 'ObtieneDivisionTmp',
//        data: { idDir: idUpd },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result === 1) {
//                var div = dato.data.Data;
//                if (div != null) {
//                    $("#hidAccionMvEnt").val("1");
//                    $("#hidEdicionEnt").val(div.Id);
//                    $("#ddlDivisionTipo").val(div.idTipoDivision);
//                    loadTipoDivision('ddlDivision', div.idTipoDivision, div.idDivision)
//                    loadSubdivision('ddlSubDivision', div.idDivision, div.idSubTipoDivision);
//                    loadDivisionValor('ddlValorSubDivision', div.idSubTipoDivision, div.idDivisionValor);
//                    $("#mvDivision").dialog("open");
//                } else {
//                    alert("No se pudo obtener la division para editar.");
//                }
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

function updAddDifusion(idUpd) {
    limpiarDifusion();

    $.ajax({
        url: 'ObtieneDifusionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var aso = dato.data.Data;
                if (aso != null) {
                    $("#hidAccionMvDif").val("1");
                    $("#hidEdicionDif").val(aso.Id);
                    $("#ddlDifusion").val(aso.idDifusion);
                    $("#txtNroDifusion").val(aso.NroDifusion);
                    $("#chkalmacenamiento").attr('checked', aso.almacenamiento);
                    $("#mvDifusion").dialog("open");
                } else {
                    alert("No se pudo obtener el medio de difusion para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiar() {
    $("#ddlTipoIdentificacion").val(0);
    $("#txtNroIdentificacion").val("");
    $("#txtRazonSocial").val("");
    $("#ddlTipoestablecimiento").val(0);
    $("#ddlSubtipoestablecimiento").val(0);
    $("#txtEstablecimiento").val("");
    $("#txtRazonSocial").val("");
}

function limpiarObservacion() {
    $("#txtObservacion").val("");
    $("#hidAccionMvObs").val(0);
    $("#hidEdicionObs").val(0);
}

function limpiarDireccion() {
    $("#txtUrb").val("");
    $("#hidAccionMvDir").val("0");
    $("#hidEdicionDir").val("0");
    $("#txtNro").val("");
    $("#txtMz").val("");
    $("#ddlTipoDireccion").val("");
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

function limpiarParametro() {
    $("#txtDescripcion").val("");
    $("#hidAccionMvPar").val(0);
    $("#hidEdicionPar").val(0);

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

function limpiarInscepccion() {
    $("#txtFileinscepccion").val("");
    $("#txtFechaInspeccion").val("");
    $("#txtHora").val("");
    $("#txtObservacioninspeccion").val("");
    $("#hidAccionMvDoc").val(0);
    $("#hidEdicionDoc").val(0);
}

function limpiarCaracteristica() {
    $("#ddlCaracteristica").val(0);
    $("#txtvalor").val("");
    $("#hidAccionMvPar").val(0);
    $("#hidEdicionPar").val(0);
}

function limpiarAsociado() {
    $("#hidAccionMvEnt").val("0");
    $("#hidEdicionEnt").val("");
    $("#ddlTipoDoc").val("");
    $("#txtDocumento").val("");
    $("#txtSocioAsociado").val("");
    $("#ddlRol").val("");
    $("#txtNombre").val("");
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    msgErrorET("", "txtSocioAsociado");
}

function limpiarDivision() {
    $("#hidAccionMvDiv").val("0");
    $("#hidEdicionDiv").val(0);
    $("#ddlDivisionTipo").val(0);
    $("#ddlDivision").val(0);
    $("#ddlSubDivision").val(0);
    $("#ddlValorSubDivision").val(0);
}

function limpiarDifusion() {
    $("#hidAccionMvDif").val("0");
    $("#hidEdicionDif").val(0);
    $("#ddlDifusion").val(0);
    $("#txtNroDifusion").val('');
    document.getElementById("chkalmacenamiento").checked = false;
}

function actualizarDirPrincipal(idDir) {
    $.ajax({
        url: 'SetDirPrincipal',
        data: { idDir: idDir },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            //alert(dato.message);
            if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function actualizarContacto(idCon, idAso) {
    $.ajax({
        url: 'SetConPrincipal',
        data: { idCon: idCon, idAso: idAso },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 0) {
                loadDataAsociado();
                alert(dato.message);
            }
        }
    });
}

function loadDataLicencia() {
    var estid = $("#hidCodigoEST").val() == "" ? 0 : $("#hidCodigoEST").val();
    $("#gridLicencia").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 15,
            transport: {
                read: {
                    url: "../Establecimiento/usp_listar_LicenciasJson", dataType: "json", data: { EstablecimientoId: estid }
                }
            },
            schema: { data: "ListaLicencias", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
            { field: "LIC_ID", width: 10, title: "Id", template: "<a id='single_2' style='color:gray !important;'>${LIC_ID}</a>" },

            { field: "LIC_NAME", width: 50, title: "Licencia", template: "<a id='single_2' style='color:gray !important;'>${LIC_NAME}</a>" },
            //{ field: "tipo licencia", width: 25, title: "Tipo Licencia", template: "<a id='single_2' style='color:gray !important;'>${LIC_TDESC}</a>" },
            { field: "TIPO", width: 25, title: "TIPO", template: "<a id='single_2' style='color:gray !important;'>${RAT_FDESC}</a>" },
            { field: "MODALIDAD", width: 25, title: "Modalidad", template: "<a id='single_2' style='color:gray !important;'>${MOD_DEC}</a>" },
            { field: "USUARIO", width: 30, title: "USUARIO", template: "<a id='single_2' style='color:gray !important;'>${BPS_NAME}</a>" },
            { field: "AUTORIZACION INICIO", width: 20, title: "AUTORIZACION INICIO", template: "<a id='single_2' style='color:gray !important;'>${LIC_AUT_START}</a>" },
            { field: "AUTORIZACION FIN", width: 20, title: "AUTORIZACION FIN", template: "<a id='single_2' style='color:gray !important;'>${LIC_AUT_END}</a>" },
            //{ field: "LOG_DATE_CREAT", type: "date", width: 20, title: "Fecha Crea", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            //{ field: "LOG_USER_UPDAT", width: 20, title: "Usu. Modi", template: "<a id='single_2'  style='color:gray !important;'${LOG_USER_UPDAT}</a>" },
            //{ field: "LOG_DATE_UPDATE", type: "date", width: 20, title: "Fecha Modi", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_UPDATE==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_UPDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            { title: "<center>Ver</center>", width: 20, template: "<center><a id='single_2' href=javascript:verlicencia('${LIC_ID}') style='color:Blue !important;'> <img src='../Images/botones/new32.png'  width='20px'> <label id='lblVer'></label>Licencia</a></center>" },
            //{ title: "Ver", width: 15, template: "<a id='single_2' style='color:Blue !important;' rel='external' href='../Licencia/Nuevo?set=${LIC_ID} /target='_blank''>Ver Licencia</a>" },
           ]
    });
}

function verlicencia(id) {
    //location.href = "../Licencia/Nuevo?set=" + id;
    window.open("../Licencia/Nuevo?set=" + id);
}

function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}

function ValidarEstablecimientoModif(ESTID) {

    var respuesta = 0;
    $.ajax({

        url: '../Establecimiento/ValidarEstablecimientoModif',
        data: { ESTID: ESTID },
        //async:false PARA QUE se tenga que el jquery termine de ejecutar y no se saltee la programacion
       async:false,
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#btnBuscarResponsable").show();
                respuesta = 1;
                //alert(respuesta);
            } else {
                respuesta = 0;
                $("#btnBuscarResponsable").hide();
                //$("#lbResponsableSocio").removeAttr("onclick");
            }
        }

    });

    return respuesta;

}

//Validaque  Las Caracteristicas Predefinidas de un Establecimiento no sean nulas o de valor "0" al Grabar
function ValidarCaractersiticasPredefinidas() {

    //Realizar La validacion aqui 
    var Respuesta = 0;
    $.ajax({

        url: '../Establecimiento/ValidaCaracteristicasPredefinidas',
        //async:false PARA QUE se tenga que el jquery termine de ejecutar y no se saltee la programacion
        async: false,
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                Respuesta = 1;

            } else {
                msgErrorB(K_DIV_MESSAGE.DIV_ESTABLECIMIENTO, "Las Caracteristicas Predefinidas no tienen valores Validos,no pueden ser 0");
            }
        }

    });

    return Respuesta;
}

//Calula la Tarifa de el Establecimiento Segun la Cantidad de sus Caracteristicas.

function CalularTarifa() {
    //obtener este valor ,no pasarlo en duro
    var calvum = 2.5;

    $.ajax({
        url: "../TarifaTest/Calcular",
        type: 'POST',
        data: { VUMcalcular: calvum},
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                   
                    if (minimo >= formula)
                        valorTarifa = minimo.toFixed(2);
                    else
                        valorTarifa = formula.toFixed(2);
                    var val = formatoMoneda(valorTarifa);
                    $('#txtCalcTarif').val(val);
                }
            } else if (dato.result == 0) {
                $('#txtCalcTarif').val('');
                //alert(K_MENSAJE_SIN_RESULTADO);
                msgErrorB(K_DIV_MESSAGE.DIV_ESTABLECIMIENTO, "Los Valores Ingresados en las Caracteristicas no devuelven un valor de Tarifa Valido");
            }
        }
    });
}

function buscarSocio() {
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    var doc = $("#ddlTipoDoc").val();
    var nro = $("#txtDocumento").val();

    $.ajax({
        data: { tipo: doc, nro_tipo: nro },
        url: '../Socio/ObtenerSocioDocumento',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $("#txtSocioAsociado").val(datos.Codigo);
                var nombre = datos.Nombres + " " + datos.Paterno + " " + datos.Materno;
                if (doc == 1) {
                    nombre = datos.RazonSocial;
                }
                $("#txtNombre").val(nombre);
            } else {
                msgErrorET("No se encontro resultados en la busqueda", "txtDocumento");
            }

        }
    });
}

function actualizarAsocPrincipal(idAsoc) {

    $.ajax({
        url: 'SetAsocPrincipal',
        data: { idAsoc: idAsoc },
        type: 'POST',
        success: function (response) {
            var dato = response;
            //  alert(dato.message);
            if (!(dato.result == 1)) {
                alert(dato.message);
            }
        }
    });

}
