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
var K_TIPODOC = { RUC: 1, DNI: 2, NINGUNO: 3 };
var K_DIV_MESSAGE = {
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento"
};
var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};
/*INICIO CONSTANTES*/

$(function () {
    /*Inicializador de PopUp de direcciones*/
    var eventoKP = 'keypress';
    $('#ddlTipoDocumentoVal').attr("disabled", "disabled");
    $('#txtNroDocumento').attr("disabled", "disabled");
    $('#txtRazon').attr("disabled", "disabled");
    $('#txtFono').on(eventoKP, function (e) { return solonumeros(e); });

    $('#txtFLiquidacionGastos,#txtFLiquidacionComision,#txtFecha').kendoDatePicker({
        format: "dd/MM/yyyy"
    });
    //txtFInicioContrato txtFLiquidacionContrato
    $("#txtFInicioContrato").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFLiquidacionContrato").kendoDatePicker({ format: "dd/MM/yyyy" });
    

   // $('#btnGrabar').attr("disabled", "disabled");
    $('#btnStatus').css("display", "none");

    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvObs").val("0");
    $("#hidAccionMvPar").val("0");
    $("#hidAccionMvDoc").val("0");
    $("#hidAccionMvTel").val("0");
    $("#hidAccionMvMail").val("0");
    $("#hidAccionMvRedes").val("0");

    loadTipoDoc("ddlTipoDocumento", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);
    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoCorreo("ddlTipoMail", 0);
    loadTipoTelefono("ddlTipoFono", 0);
    loadTipoRedes("ddlTipoRedes", 0);

    $("#ddlOficinaComercial,#ddlMonedaRecaudacion").on("change", function () {
        msgError("", "ddlOficinaComercial");
        msgError("", "ddlMonedaRecaudacion");
    });

    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        alert("No se envio parámetro esperado");
    } else {
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatosSocio(codeEdit);
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




    $("#tabs").tabs();
    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addObservacion, "Cancelar": function () { $("#mvObservacion").dialog("close"); } }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addDocumento, "Cancelar": function () { $("#mvDocumento").dialog("close"); } }, modal: true });
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addParametro, "Cancelar": function () { $("#mvParametro").dialog("close"); } }, modal: true });
    $("#mvTelefono").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addTelefono, "Cancelar": function () { $("#mvTelefono").dialog("close"); } }, modal: true });
    $("#mvCorreo").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addCorreo, "Cancelar": function () { $("#mvCorreo").dialog("close"); } }, modal: true });
    $("#mvRedes").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addRedes, "Cancelar": function () { $("#mvRedes").dialog("close"); } }, modal: true });
    $("#mvListarDireccion").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": addRedes, "Cancelar": function () { $("#mvListarDireccion").dialog("close"); } }, modal: true });

    $("#mvValidarDni").dialog({ autoOpen: false, width: 450, height: 265, modal: true });
    $(".addListarDireccion").on("click", function () { ListarDireccionxPerfil(codeEdit); $("#mvListarDireccion").dialog("open"); });
    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#hidEstado").val(0); $("#mvObservacion").dialog("open"); });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $(".addTelefono").on("click", function () { limpiarTelefono(); $("#mvTelefono").dialog("open"); });
    $(".addCorreo").on("click", function () { limpiarCorreo(); $("#mvCorreo").dialog("open"); });
    $(".addRedes").on("click", function () { limpiarRedes(); $("#mvRedes").dialog("open"); });

    
    $("#btnGrabar").on("click", function () {
        insertar(0);
    }).button();
    $("#btnRegresar").on("click", function () {
        location.href = "../SocioAdministracion/";
    }).button();
    $("#btnNuevo").on("click", function () {
        location.href = "../RecaudadorAdministracion/Nuevo";
    }).button();
    $("#btnStatus").on("click", function () {
        limpiarObservacion();
        $("#hidEstado").val(1);
        $("#mvObservacion").dialog("open");
    }).button();
 
});

function ListarDireccionxPerfil(codeEdit) {
    $.ajax({
        url: '../DerechoAdministracion/ListarDireccionxPerfil',
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

function startChange() {
    var startDate = start.value(),
        endDate = end.value();

    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate());
        end.min(startDate);
    } else if (endDate) {
        start.max(new Date(endDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
}

function endChange() {
    var endDate = end.value(),
        startDate = start.value();
    if (endDate) {
        endDate = new Date(endDate);
        endDate.setDate(endDate.getDate());
        start.max(endDate);
    } else if (startDate) {
        end.min(new Date(startDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
}

function actualizarEstado() {

    //if (confirm("¿Estás seguro de cambiar el estado del usuario de derecho?")) {
        var codigo = $("#hidBpsId").val();
        $.ajax({
            url: 'ActualizarEstado',
            data: { codigo: codigo },
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    if ($("#spEstado").html() == "Inactivar") {
                        $("#spEstado").html("Activar");
                        $("#lblEstado").text("Inactivo");
                    } else {
                        $("#spEstado").html("Inactivar");
                        $("#lblEstado").text("Activo");
                    }

                    alert(dato.message);
                } else {
                    msgError(dato.message);
                }
            }
        });
        var codeEdit = (GetQueryStringParams("set"));
        ObtenerDatosSocio(codeEdit);
    //}
}

//function modalValidarNumero() {
//    msgOkB("divErrorValidarDoc", "");
//    msgOkB("divResultValidarDoc", "");
//    $("#txtNroValidacion").val("");
//    $("#mvValidarDni").dialog("open");
//}

//function validarDocPopup() {
//    $("#hidExitoValNumero").val(0);
//    msgOkB("divErrorValidarDoc", "");
//    msgOkB("divResultValidarDoc", "");

//    var tipoDoc = $("#ddlTipoDocumentoVal option:selected").text();
//    var tipoPer = $("#ddlTipoPersona option:selected").text();

//    if (tipoDoc == "DNI") {

//        var numero = $("#txtNroDocumento").val();
//        var identificador = $("#txtNroValidacion").val();
//        if (numero.length == 8) {
//            $.ajax({
//                url: '../General/ValidacionDNI',
//                data: { num: numero, id: identificador },
//                type: 'POST',
//                success: function (response) {
//                    var dato = response;
//                    if (dato.result == 1) {
//                        $("#hidExitoValNumero").val(1);
//                        msgOkB("divErrorValidarDoc", dato.message);
//                        msgOkB("divResultValidarDoc", dato.message);
//                        $("#mvValidarDni").dialog("close");
//                    } if (dato.result == 2) {
//                        $("#hidExitoValNumero").val(0);
//                        msgErrorB("divErrorValidarDoc", dato.message);
//                        msgErrorB("divResultValidarDoc", dato.message);
//                    } else {
//                        msgErrorB("divErrorValidarDoc", dato.message);
//                    }
//                }
//            });
//        } else {
//            msgErrorB("divErrorValidarDoc", "El numero a validar tiene que ser igual a 8 digitos");
//        }
//    } else {

//        $("#hidExitoValNumero").val(0);
//        alert("PENDIENTE LOGICA DE VALIDACION DE RUC EN LA SUNAT.");
//        $("#hidExitoValNumero").val(1);
//        msgOkB("divErrorValidarDoc", "RUC Válido");
//        msgOkB("divResultValidarDoc", "RUC Válido");
//        $("#mvValidarDni").dialog("close");
//    }


//}

function insertar(estado) {
    $("#file_upload").removeClass("requerido");
    $("#txtFecha").removeClass("requerido");
    msgError("");
    
    if (ValidarRequeridos()) {
        //var idBps = 0;
        //if (K_ACCION_ACTUAL === K_ACCION.Modificacion) idBps=$("#hidBpsId").val();

        var usuario = {
            Codigo: $("#hidBpsId").val(),
            OficinaComercial: $("#ddlOficinaComercial").val(),
            MonedaRecaudacion: $("#ddlMonedaRecaudacion").val(),
            InicioContrato: $("#txtFInicioContrato").val(),
            LiquidacionContrato: $("#txtFLiquidacionContrato").val(),
            LiquidacionGastos: $("#txtFLiquidacionGastos").val(),
            LiquidacionComision: $("#txtFLiquidacionComision").val(),
            CuentaCorriente: $("#txtCCorriente").val(),
            Nivel: $("#ddlCargoRecaudador").val()
        }; 

            $.ajax({
                url: 'Insertar',
                data: usuario,
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    if (dato.result == 1) {
                        if (estado == 1) {
                            actualizarEstado();
                        } else {
                            alert(dato.message);
                            location.href = "../SocioAdminisracion/";
                        }
                    } else {
                        msgError(dato.message);

                    }
                }
            });
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
                if (dato.result == 1) {
                    loadDataDireccion();
                } else {
                    alert(dato.message);
                }
            }
        });

        $("#mvDireccion").dialog("close");
    }
}

function addObservacion() {
    var estado = $("#hidEstado").val();
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
                if (dato.result == 1) {
                    loadDataObservacion();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvObservacion").dialog("close");
        if (estado == 1) {
            insertar(1);
        }
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
                        InitUploadTabDocRecaudador("file_upload", dato.Code);
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
                if (dato.result == 1) {
                    loadDataParametro();
                } else {
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
                if (dato.result == 1) {
                    loadDataTelefono();
                } else {
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
                if (dato.result == 1) {
                    loadDataCorreo();
                } else {
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
function loadDataOficina() {
    loadDataGridTmp('ListarHistorico', "#gridOficina");
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
            if (dato.result == 1) {
                loadDataDireccion();
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
            if (dato.result == 1) {
                loadDataObservacion();
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
            if (dato.result == 1) {
                loadDataDocumento();
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
            if (dato.result == 1) {
                loadDataParametro();
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
            if (dato.result == 1) {
                loadDataCorreo();
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
            if (dato.result == 1) {
                loadDataTelefono();
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


function updAddDireccion(idUpd) {
    limpiarDireccion();

    $.ajax({
        url: 'ObtieneDireccionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
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
            } else {
                alert(dato.message);
            }
        }
    });

}

function updAddObservacion(idUpd) {
    limpiarObservacion();
    $("#hidEstado").val(0);
    $.ajax({
        url: 'ObtieneObservacionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
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
            } else {
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
            } else {
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
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDoc").val("1");
                    $("#hidEdicionDoc").val(doc.Id);
                    $("#ddlTipoDocumento").val(doc.TipoDocumento);
                    $("#txtFecha").val(doc.Fecha);
                    $("#mvDocumento").dialog("open");
                } else {
                    alert("No se pudo obtener el documento para editar.");
                }
            } else {
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
            } else {
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
            } else {
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
    $("#file_upload").css({ 'border': '1px solid gray' });
    $("#txtFecha").css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#hidAccionMvDoc").val("0");
    $("#hidEdicionDoc").val("0");
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



function ObtenerDatosSocio(bpsId) {
    $.ajax({
        url: 'ObtenerSocio',
        data: { codigo: bpsId },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var socio = dato.data.Data;
                loadTipoPersona("ddlTipoPersona", socio.TipoPersona);
                loadTipoDocumento("ddlTipoDocumentoVal", socio.TipoDocumento);
                $("#txtNroDocumento").val(socio.NumDocumento);
                if (socio.TipoDocumento != K_TIPODOC.DNI) {
                    $("#txtRazon").val(socio.RazonSocial);
                } else {
                    $("#txtRazon").val(socio.Nombres + " " + socio.Paterno + "  " + socio.Materno);
                }

                //carga datos del Recaudador
                $.ajax({
                    url: 'ObtenerUsuarioRecaudador',
                    data: { codigo: bpsId },
                    type: 'POST',
                    success: function (response) {
                        var dato = response;
                        if (dato.result === 1) {
                            var usuario = dato.data.Data;
                            if (usuario != null) {
                               // alert("sss");
                                var d1 = $("#txtFInicioContrato").data("kendoDatePicker");
                                var d2 = $("#txtFLiquidacionContrato").data("kendoDatePicker");
                                var d3 = $("#txtFLiquidacionGastos").data("kendoDatePicker");
                                var d4 = $("#txtFLiquidacionComision").data("kendoDatePicker");

                                $("#divTituloPerfil").html("Recaudador / Actualización");
                                K_ACCION_ACTUAL = K_ACCION.Modificacion;
                                loadComboOficinaComercial("ddlOficinaComercial", usuario.OficinaComercial);
                                loadMonedaRecaudacion("ddlMonedaRecaudacion", usuario.MonedaRecaudacion);
                                loadCargoRecaudador("ddlCargoRecaudador",usuario.Nivel);

                                var valFechaIni = formatJSONDate(usuario.InicioContrato);
                                var valFechaLC = formatJSONDate(usuario.LiquidacionContrato);
                                var valFechaLG = formatJSONDate(usuario.LiquidacionGastos);
                                var valFechaLCo = formatJSONDate(usuario.LiquidacionComision);
                                
                                //var start = $('#txtFInicioContrato').kendoDatePicker({
                                //    value: valFechaIni,
                                //    change: startChange,
                                //    format: "dd/MM/yyyy"
                                //}).data("kendoDatePicker");
                                //var end = $('#txtFLiquidacionContrato').kendoDatePicker({
                                //    value: valFechaLC,
                                //    change: endChange,
                                //    format: "dd/MM/yyyy"
                                //}).data("kendoDatePicker");

                                //start.max(end.value());
                                //end.min(start.value());

                               d1.value(valFechaIni);
                               d2.value(valFechaLC);
                               d3.value(valFechaLG);
                               d4.value(valFechaLCo);
                            
                               $("#txtCCorriente").val(usuario.CuentaCorriente);

                               if (usuario.Activo) {
                                   $("#spEstado").html("Inactivar");
                                   $("#lblEstado").text("Activo");
                               } else {
                                   $("#spEstado").html("Activar");
                                   $("#lblEstado").text("Inactivo");
                               }
                               $('#btnStatus').css("display", "inline");

                            }
                        } else {
                            if (dato.Code == K_TIPO_ERROR.CODE_ERROR_NO_DATA) {

                                $("#divTituloPerfil").html("Recaudador / Nuevo");
                                K_ACCION_ACTUAL = K_ACCION.Nuevo;
                                loadComboOficinaComercial("ddlOficinaComercial", 0);
                                loadMonedaRecaudacion("ddlMonedaRecaudacion", 0);
                                loadCargoRecaudador("ddlCargoRecaudador", 0);
                                
                                //var today = new Date();
                                //var d1 = today.getDate() + '/' + today.getMonth() + '/' + today.getFullYear();
                                //var d2 = (today.getDate() + 1) + '/' + today.getMonth() + '/' + today.getFullYear();

                                //var start = $('#txtFInicioContrato').kendoDatePicker({
                                //    value: d1,
                                //    change: startChange,
                                //    format: "dd/MM/yyyy"
                                //}).data("kendoDatePicker");
                                //var end = $('#txtFLiquidacionContrato').kendoDatePicker({
                                //    value: d2,
                                //    change: endChange,
                                //    format: "dd/MM/yyyy"
                                //}).data("kendoDatePicker");

                                //start.max(end.value());
                                //end.min(start.value());
                            }
                            else {
                                alert(dato.message);
                            }
                        }
                        /*Inicio de Carga inicial de tabs*/
                        loadDataDireccion();
                        loadDataObservacion();
                        loadDataDocumento();
                        loadDataParametro();
                        loadDataTelefono();
                        loadDataCorreo();
                        loadDataOficina();
                        loadDataRedes();
                        $('#btnGrabar').removeAttr("disabled");
                        /*FIN de Carga inicial de tabs*/
                        ValidatePermiso(bpsId);
                    }
                });

            } else {
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
            //  alert(dato.message);
            if (!(dato.result == 1)) {
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

    } else {
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



function getValorConfigNumDoc(itipo) {

}

function ValidatePermiso(idBps) {
    msgError("");
    $.ajax({
        url: '../Seguridad/EnableUpdateUsuRec',
        type: 'POST',
        data: { idBps: idBps },
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
                  //  $("#gridAsociado a").each(function (index, element) { $(element).show(); });
                    $("#gridRedes a").each(function (index, element) { $(element).show(); });
                    $("#btnStatus ,#btnGrabar").button({ disabled: false });
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

                    $("#btnStatus ,#btnGrabar").button({ disabled: true });

                    msgError(dato.message);

                }
            } else if (dato.result == 0) {
                alert(dato.message);
                $("#btnStatus").attr("disabled", "disabled");
                $("#btnGrabar").attr("disabled", "disabled");

            }
        }
    });
}