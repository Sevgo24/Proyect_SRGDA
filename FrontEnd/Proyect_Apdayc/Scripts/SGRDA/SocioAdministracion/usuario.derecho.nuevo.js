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
var K_MESSAGES = {
    VALIDACION_PERMISO: "Ud no tiene permiso para ejecutar la acción."
};
//varaible para que la Programacion Descuento Reconozca que el descuento se esta haciendo por Socio y no Por Licencia
var K_RECONOCE_SOCIO = {
    SOCIO: 1
};
/*INICIO CONSTANTES*/

$(function () {
    /*Inicializador de PopUp de direcciones*/
    var eventoKP = "keypress";
    $('#txtDsctoPer').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtDsctoSol').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtFono').on(eventoKP, function (e) { return solonumeros(e); });

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    $('#ddlTipoDocumentoVal').attr("disabled", "disabled");
    $('#txtNroDocumento').attr("disabled", "disabled");
    $('#txtRazon').attr("disabled", "disabled");

    //$('#btnGrabar').attr("disabled", "disabled");
    $('#btnStatus').css("display", "none");
    //Quitar Grupo Empresarial
    //$("#btnEliminarGrupoEmp").on("click", function () { EliminarGrupoEmpresarial($("#hidBpsId").val(), $("#hidCodigoGrupoEmpresarial").val()); });
    $("#btnEliminarGrupoEmp").on("click", function () { ValidarEliminacionGrupoEmpresarial();});


    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvObs").val("0");
    $("#hidAccionMvPar").val("0");
    $("#hidAccionMvDoc").val("0");
    $("#hidAccionMvTel").val("0");
    $("#hidAccionMvMail").val("0");
    $("#hidAccionMvEnt").val("0");
    $("#hidAccionMvRedes").val("0");


    loadTipoDoc("ddlTipoDocumento", 0);
    loadTipoDocumento("ddlTipoDocumento1", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);
    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoCorreo("ddlTipoMail", 0);
    loadTipoTelefono("ddlTipoFono", 0);
    loadRol("ddlRol", 0);
    loadTipoRedes("ddlTipoRedes", 0);



    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        alert("No se envio parámetro esperado");
    } else {
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatosSocio(codeEdit);
        //ValidatePermiso(codeEdit);
    }

    initFormDireccion({
        id: K_ID_POPUP_DIR,
        parentControl: "divDireccion",
        width: 850,
        height: 400,
        evento: addDireccion,
        modal: true
    });
    //Busqueda de Grupo Empresarial
    mvInitBuscarSocioEmpresarial({ container: "mvContenedorSocioEmpresarial", idButtonToSearch: "btnGrupEmp", idDivMV: "mvBuscarSocio", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
    // initAutoCompletarGrupoEmpresarial("txtGrupoEmpresarial", "hidCodigoGrupoEmpresarial");
    /*ComboBox de Popups*/



   
    $("#tabs").tabs();
    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addObservacion, "Cancelar": function () { $("#mvObservacion").dialog("close"); } }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addDocumento, "Cancelar": function () { $("#mvDocumento").dialog("close"); } }, modal: true });
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addParametro, "Cancelar": function () { $("#mvParametro").dialog("close"); } }, modal: true });
    $("#mvTelefono").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addTelefono, "Cancelar": function () { $("#mvTelefono").dialog("close"); } }, modal: true });
    $("#mvCorreo").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addCorreo, "Cancelar": function () { $("#mvCorreo").dialog("close"); } }, modal: true });
    $("#mvEntidad").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: 260, buttons: { "Agregar": AddAsociado, "Cancelar": function () { $("#mvEntidad").dialog("close"); } }, modal: true });
    $("#mvRedes").dialog({ autoOpen: false, width: 550, height: 250, buttons: { "Agregar": addRedes, "Cancelar": function () { $("#mvRedes").dialog("close"); } }, modal: true });
    //
    $("#mvListarDireccion").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": AddDireccionesAll, "Cancelar": function () { $("#mvListarDireccion").dialog("close"); } }, modal: true });
    //Agregando el AbrirPopup
    $("#mvDescuento").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": addDescuento, "Cancelar": function () { $("#mvDescuento").dialog("close"); } }, modal: true });


    $("#mvValidarDni").dialog({ autoOpen: false, width: 650, height: 260, modal: true });
    $(".addListarDireccion").on("click", function () { ListarDireccionxPerfil(codeEdit); $("#mvListarDireccion").dialog("open"); });
    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#hidEstado").val(0); $("#mvObservacion").dialog("open"); });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $(".addTelefono").on("click", function () { limpiarTelefono(); $("#mvTelefono").dialog("open"); });
    $(".addCorreo").on("click", function () { limpiarCorreo(); $("#mvCorreo").dialog("open"); });
    $(".addEntidad").on("click", function () { limpiarAsociado(); $("#mvEntidad").dialog("open"); });
    $(".addRedes").on("click", function () { limpiarRedes(); $("#mvRedes").dialog("open"); });
    //Agregar Para Hacer el Desceunto por Clientes
    $(".addDescuento").on("click", function () { initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento"); $("#mvDescuento").dialog("open") /*limpiarDescuento(); initLoadAddDescuento("mvDescuento", "divPeriodoDescuentoLst", "avisoTabDescuento");*/ });
    
 
    $("#btnGrabar").on("click", function () { insertar(0); }).button();
    $("#btnRegresar").on("click", function () { location.href = "../SocioAdministracion/"; }).button();
    $("#btnStatus").on("click", function () {
        limpiarObservacion();
        $("#hidEstado").val(1);
        $("#mvObservacion").dialog("open");
    }).button();

    //Probando controles de el TAB "DESCUENTOS"
    $("#divplanifi").hide();
    
    $("#txtDocumento").on("keypress", function (e) {
        return solonumeros(e);
    });
    $("#btnBuscarSocio").on("click", function () {
        buscarSocio();
    });
 
    //ObtenerDatosSocio($("#hidBpsId").val());


    //descuentos
    //Descuento por Socios 
    $("#ddlTipoDescuento").on("change", function () {
        if (ObtieneIdTipoDscto() != $(this).val()) {
            $("#ddlSignoDescuento").removeClass('requerido');
            $("#ddlSignoDescuento").hide();
            $("#ddlDescuento").show();
            $("#txtDescuentoEspecial").hide();
            $("#txtDescuentoEspecial").removeClass('requerido');
            $("#txtDescuentoEspecial").css('border-color', 'gray');
            $("#txtValorDscto").hide();
            $("#txtValorDscto").removeClass('requerido');
            $("#txtValorDscto").css('border-color', 'gray');
            $("#ddlDescuento").addClass('requeridoLst');


            $("#tdSignoDescuentoDes").hide();
            $("#txtPerDscto").removeClass('requerido');
            $("#txtPerDscto").hide();

            $("#lblValorDscto").show();
            //mantenimiento.descuento.js/limpiarDescuento()
            limpiarDescuento();
            // loadDescuentoXTarifa("ddlDescuento", $(this).val(), 0, $(K_HID_KEYS.TARIFA).val());
        } else {
            $("#ddlSignoDescuento").addClass('requerido');
            $("#ddlSignoDescuento").show();
            $("#ddlSignoDescuento").val("V");

            $("#ddlDescuento").hide();
            $("#txtDescuentoEspecial").show();
            $("#txtDescuentoEspecial").addClass('requerido');
            $("#txtValorDscto").show();
            $("#txtValorDscto").val('');
            $("#txtValorDscto").addClass('requerido');
            $("#ddlDescuento").removeClass('requeridoLst');
            $("#txtDescuentoEspecial").val('');
            $('#txtValorDscto').on("keypress", function (e) { return solonumeros(e); });
            //$('#lblPerDscto').html(0);
            //$('#lblValorDscto').html(entidad.DISC_VALUE);
            $('#lblSignoDscto').html('-');

            $("#tdSignoDescuentoDes").show();

            //$("#txtPerDscto").addClass('requerido');
            //$("#txtPerDscto").show();

            $("#lblValorDscto").hide();
            //cssValCaract k-formato-numerico
        }

    });

    //oculta CAJA DE TEXTOS
    $("#ddlSignoDescuento").on("change", function () {
        if ($(this).val() == "P") {

            $("#txtPerDscto").show();
            $("#txtValorDscto").hide();
            $("#txtValorDscto").removeClass('requerido');

    //        $("#lblValorDscto").hide();
    //        $("#lblPerDscto").hide();
        } else {

            $("#txtValorDscto").show();
            $("#txtPerDscto").hide();
            $("#txtPerDscto").removeClass('requerido');

    //        $("#lblValorDscto").show();
    //        $("#lblPerDscto").show();
        }
    });
    $("#ddlDescuento").on("change", function () { limpiarDescuento(); obtenerDescuento($(this).val()); });


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

function buscarSocio() {
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    var doc = $("#ddlTipoDocumento1").val();
    var nro = $("#txtDocumento").val();

    $.ajax({
        data: { tipo: doc, nro_tipo: nro },
        url: '../SocioAdministracion/ObtenerSocioDocumento',
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

function modalValidarNumero() {
    msgOkB("divErrorValidarDoc", "");
    msgOkB("divResultValidarDoc", "");
    $("#txtNroValidacion").val("");
    $("#mvValidarDni").dialog("open");
}

function validarDocPopup() {
    $("#hidExitoValNumero").val(0);
    msgOkB("divErrorValidarDoc", "");
    msgOkB("divResultValidarDoc", "");

    var tipoDoc = $("#ddlTipoDocumentoVal option:selected").text();
    var tipoPer = $("#ddlTipoPersona option:selected").text();
 
    if (tipoDoc == "DNI") {

        var numero = $("#txtNroDocumento").val();
        var identificador = $("#txtNroValidacion").val();
        if (numero.length == 8) {
            $.ajax({
                url: '../General/ValidacionDNI',
                data: { num: numero, id: identificador },
                type: 'POST',
                success: function (response) {
                    var dato = response;
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
    } else {

        $("#hidExitoValNumero").val(0);
        alert("PENDIENTE LOGICA DE VALIDACION DE RUC EN LA SUNAT.");
        $("#hidExitoValNumero").val(1);
        msgOkB("divErrorValidarDoc", "RUC Válido"); 
        msgOkB("divResultValidarDoc", "RUC Válido");
        $("#mvValidarDni").dialog("close");
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
function insertar(estado) {
 

    $("#file_upload").removeClass("requerido");
    $("#txtFecha").removeClass("requerido");
    $("#txtDescuentoEspecial").removeClass('requerido');
    $("#txtValorDscto").removeClass('requerido');

    msgError("");

    //$.ajax({
    //    url: '../Seguridad/EnableUpdate',
    //    data: { idBps: $("#hidBpsId").val() },
    //    type: 'POST',
    //    beforeSend: function () { },
    //    success: function (response) {
    //        var datoA = response;
    //        validarRedirect(datoA); /*add sysseg*/
    //        if (datoA.result == 1) {
    //            /*validacion si tiene permiso*/
    //            if (datoA.valor=="1") {
 
    if (ValidarRequeridos()) {
        
                            var usuario = {
                                Codigo:$("#hidBpsId").val(),
                                TipoPago: $("#ddlFormaPago").val(),
                                GrupoEmpresarial: $("#hidCodigoGrupoEmpresarial").val() == "" ? "0" : $("#hidCodigoGrupoEmpresarial").val(),
                                DescuentoPer: $("#txtDsctoPer").val(),
                                DescuentoMonto: $("#txtDsctoSol").val(),
                                DescuentoMotivo: $("#txtMotivo").val(),
                                CuentaContable: $("#txtContable").val(),
                                Partida:$("#txtPartida").val(),
                                Zona: $("#txtZona").val(),
                                Sede:$("#txtSede").val()
                          };
                            $.ajax({
                                url: 'Insertar',
                                data: usuario,
                                type: 'POST',
                                success: function (response) {
                                    var dato = response;
                                    validarRedirect(dato); /*add sysseg*/
                                    if (dato.result == 1) {
                                        if (estado == 1) {

                                            actualizarEstado();
                                          
                                            //Si se Selecciono Items para Inactivar lo hace 

                                        } else {
                                            //Agregar Descuentos del grupo empresarial si es que se agrego
                                            
                                         //   alert($("#hidCodigoGrupoEmpresarial").val());

                                           if ($("#hidBpsId").val() != "" )
                                            {
                                                //funcion
                                               if($("#hidCodigoGrupoEmpresarial").val() == "" )
                                                $("#hidCodigoGrupoEmpresarial").val("0");

                                               //alert($("#hidCodigoGrupoEmpresarial").val());
                                                InactivarDescuentos($("#hidBpsId").val(), $("#hidCodigoGrupoEmpresarial").val());

                                                //Una vez se Inserte el Socio ,se insertan sus descuentos 
                                                //socio.negocio.nuevo/InsertarDescuentos
                                                InsertarDescuentos(dato.Code);// <== activar 

                                           //     InsertarDesctAutom($("#hidBpsId").val(), $("#hidCodigoGrupoEmpresarial").val());//ac
                                            }


                                            alert(dato.message);
                                            location.href = "../Socio/";
                                        }
                                    } else if (dato.result == 0) {
                                        msgError(dato.message);
                                    }
                                }
                            });
                   }  
    //            } else{
    //                msgError(datoA.message);
    //            }
    //        } else if (dato.result == 0) {
    //            msgError(datoA.message);
    //        }
    //    }
    //});

   
    return false;

}

 

 
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
            TipoUrba:$("#ddlUrbanizacion").val(),
            Urbanizacion:$("#txtUrb").val(),
            Numero:$("#txtNro").val(),
            Manzana:$("#txtMz").val(),
            Lote:$("#txtLote").val(),
            TipoDepa:$("#ddlDepartamento").val(),
            NroPiso:$("#txtNroPiso").val(),
            TipoAvenida:$("#ddlAvenida").val(),
            Avenida:$("#txtAvenida").val(),
            TipoEtapa:$("#ddlEtapa").val(),
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
                        InitUploadTabDocDerecho("file_upload", dato.Code);
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

function AddAsociado() {

    var IdAdd = 0;
    if ($("#hidAccionMvEnt").val() == "1") IdAdd = $("#hidEdicionEnt").val();
    var entidad = {
        Id: IdAdd,
        Codigo:$("#hidBpsId").val(),
        CodigoAsociado: $("#txtSocioAsociado").val(),
        NombreAsociado:$("#txtNombre").val(),
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


    var nroPiso="";
    var nroAv="";
    var nroEtp="";
    var nro="";
    var nroMZ="";
    var nroLote = "";


    if($.trim($("#txtNro").val())!=""){
        nro= "  Nro "+ $("#txtNro").val();
    }
    if($.trim($("#txtMz").val())!=""){
        nroMZ= "  Mz "+ $("#txtMz").val();
    }
    if($.trim($("#txtLote").val())!=""){
        nroLote= "  Lote "+ $("#txtLote").val();
    }
    if ($.trim($("#txtNroPiso").val()) != "") {
        nroPiso = " " + $("#ddlDepartamento option:selected").text() + " " + $("#txtNroPiso").val();
    }
    if($.trim($("#txtNroPiso").val())!=""){
        nroAv = " " + $("#ddlAvenida option:selected").text() + " " + $("#txtAvenida").val();
    }
    if ($.trim($("#txtEtapa").val()) != "") {
        nroEtp = " " + $("#ddlEtapa option:selected").text() + " " + $("#txtEtapa").val();
    }
    var razon = $("#ddlUrbanizacion option:selected").text() + " " + $("#txtUrb").val()  + nro + nroMZ + nroLote + nroPiso + nroAv + nroEtp;
    
    return razon;
}

function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
}

function loadDataGridTmp(Controller, idGrilla) {

    //if (bpsId != undefined) {
    //    $.ajax({ type: 'POST', url: Controller, data: { bpsId: bpsId }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
    //} else {
        $.ajax({ type: 'POST', url: Controller, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
   // }
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
function loadDataAsociado() {
    loadDataGridTmp('ListarAsociado', "#gridAsociado");
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
//DAVID
//Funcion que permite listar el duplicado de direccion
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
        url: '../DerechoAdministracion/ObtenerDireccionesAll',
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
function delAddAsociado(idDel) {
    $.ajax({
        url: 'DellAddAsociado',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadDataAsociado();
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
                    $("#txtReferencia").val( direccion.Referencia);
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


function updAddTelefono(idUpd) {
    limpiarTelefono();

    $.ajax({
        url: 'ObtieneTelefonoTmp',
        data: { Id: idUpd},
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
function limpiarAsociado() {
    $("#hidAccionMvEnt").val("0");
    $("#hidEdicionEnt").val("");
    $("#ddlTipoDocumento1").val("");
    $("#txtDocumento").val("");
    $("#txtSocioAsociado").val("");
    $("#ddlRol").val("");
    $("#txtNombre").val("");
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    msgErrorET("", "txtSocioAsociado");
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
   // $("#btnGrabar").attr("disabled", "disabled");
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
                    $("#txtRazon").val(socio.Nombres + " " + socio.Paterno +  "  " + socio.Materno);
                }

                //carga datos del usuario de derecho
                $.ajax({
                    url: 'ObtenerUsuarioDerecho',
                    data: { codigo: bpsId },
                    type: 'POST',
                    success: function (response) {
                        var dato = response;
                        if (dato.result === 1) {
                            var usuario = dato.data.Data;
                            if (usuario != null) {

                                
                                $("#divTituloPerfil").html("Usuario de Derecho / Actualización");
                                K_ACCION_ACTUAL = K_ACCION.Modificacion;
                                loadTipoPago("ddlFormaPago", usuario.TipoPago);
                                //loadGrupoEmpresarial("ddlGrupoEmpresarial", usuario.GrupoEmpresarial,"Ninguno");
                                //txtGrupoEmpresarial
                               // ObtieneNombreEntidad(usuario.GrupoEmpresarial, 'lblGrupoEmpresarial', false);
                                $("#hidCodigoGrupoEmpresarial").val(usuario.GrupoEmpresarial);
                                //obtenerNOmbresociox en este docum.
                                obtenerNombreSocioX(usuario.GrupoEmpresarial,'lblGrupoEmpresarial');
                                
                                $("#txtDsctoPer").val(usuario.DescuentoPer);
                                $("#txtDsctoSol").val(usuario.DescuentoMonto);
                                $("#txtMotivo").val(usuario.DescuentoMotivo);
                                $("#txtContable").val(usuario.CuentaContable);

                                //cambio 20150521
                                $("#txtPartida").val(usuario.Partida);
                                $("#txtZona").val(usuario.Zona);
                                $("#txtSede").val(usuario.Sede);
                              
                                if (usuario.Activo) {
                                    $("#spEstado").html("Inactivar");
                                    $("#lblEstado").text("Activo");
                                } else {
                                    $("#spEstado").html("Activar");
                                    $("#lblEstado").text("Inactivo");
                                }
                                $('#btnStatus').css("display", "inline");
                                $('#btnGrabar').removeAttr("disabled"); 
                            } 
                        } else {
                            if (dato.Code == K_TIPO_ERROR.CODE_ERROR_NO_DATA) {

                                $("#divTituloPerfil").html("Usuario de Derecho / Nuevo");
                                K_ACCION_ACTUAL = K_ACCION.Nuevo;
                                loadTipoPago("ddlFormaPago", 0);
                                //loadGrupoEmpresarial("ddlGrupoEmpresarial", 0,"Ninguno");
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
                        loadDataAsociado();
                        loadDataRedes();
                        
                        loadDataLicencia();
                        loadDataEstablecimientos();
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
    var exito=true;

    if (itemChange === "RUC") {
        var elem=$.trim($("#txtRazon").val());
        if(elem ===''){
            $("#txtRazon").css({ 'border': '1px solid red' });
            exito=false;

        } else {
            $("#txtRazon").css({ 'border': '1px solid gray' });
        }
        if (exito==false) msgError("Debe ingresar los campos requeridos dni ");

    } else {
        var elemNom=$.trim($("#txtNombres").val());
        var elemMa=$.trim($("#txtMaterno").val());
        var elemPa=$.trim($("#txtPaterno").val());
        if(elemNom ===''){
            $("#txtNombres").css({ 'border': '1px solid red' });
            exito=false;
        } else {
            $("#txtNombres").css({ 'border': '1px solid gray' });
        }
        if(exito && elemMa ===''){
            $("#txtMaterno").css({ 'border': '1px solid red' });
            exito=false;
        } else {
            $("#txtMaterno").css({ 'border': '1px solid gray' });
        }
        if(exito && elemPa ===''){
            $("#txtPaterno").css({ 'border': '1px solid red' });
            exito=false;
        } else {
            $("#txtPaterno").css({ 'border': '1px solid gray' });
        }


        if (exito==false) msgError("Debe ingresar los campos requeridos ruc ");
       
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
        url: '../Seguridad/EnableUpdate',
        data: { idBps: idBps  },
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
                    $("#gridAsociado a").each(function (index, element) { $(element).show(); });
                    $("#gridRedes a").each(function (index, element) { $(element).show(); });
                     $("#btnStatus ,#btnGrabar").button({ disabled: false });
                } else {
                    $(".cssTabReadOnly").each(function (index, element) { $(element).hide(); });
                    $("#gridDireccion a").each(function (index, element) { $(element).hide(); });
                    $("#gridDireccion radio").each(function (index, element) { $(element).attr("disabled","disabled"); });
                    $("#gridObservacion a").each(function (index, element) { $(element).hide(); });
                    $("#gridDocumento a").each(function (index, element) { $(element).hide(); });
                    $("#gridParametro a").each(function (index, element) { $(element).hide(); });
                    $("#gridTelefono a").each(function (index, element) { $(element).hide(); });
                    $("#gridCorreo a").each(function (index, element) { $(element).hide(); });
                    $("#gridAsociado a").each(function (index, element) { $(element).hide(); });
                    $("#gridRedes a").each(function (index, element) { $(element).hide(); });

                    $("#btnStatus ,#btnGrabar").button({ disabled: true });
                 
                
                    msgError(dato.message);
                    
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
//obtiene nombre de grupo empresarial
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

//Validar Eliminacion del Grupo Empresarial
function ValidarEliminacionGrupoEmpresarial(){
    var bpsid=$("#hidBpsId").val();
    var bpsidgroup=$("#hidCodigoGrupoEmpresarial").val();
    Confirmar('Esta Seguro de desasociar del Grupo Empresarial ?,Se perderan los beneficios del mismo',
       function () { EliminarGrupoEmpresarial(bpsid, bpsidgroup); },
       function () { },
       'CONFIRMAR'
       );

}


//Quitar Grupo Empresarial
function EliminarGrupoEmpresarial(bpsid, bpsgroup) {
    //LImpiado el hid y el label
    $.ajax({
        data: { bpsid: bpsid, bpsgroup: bpsgroup },
        url: '../Descuento/InactivaDescuentosGrupoEmp',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $("#hidCodigoGrupoEmpresarial").val("0");
                $("#lblGrupoEmpresarial").html('SELECCIONE');

            }
        }
    });



}

//Insertar Automaticamente Descuentos al agregar Socio si es que posee licencia

function InsertarDesctAutom(bpsid, bpsidgroup) {
    //alert(bpsidgroup);
    $.ajax({
        data: { bpsid: bpsid, bpsidgroup: bpsidgroup },
        url: '../Descuento/InsertarAutomDescLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                alert("Agregado Correctamente");
            }
        }

    });

}

//Mensaje de Confirmacion 

//MENSAJE DE CONFIRMAR 
function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogText,
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

//DESCUENTOS 
function InsertarDescuentos(codigo) {

    var BpsId = $("#hidBpsId").val();

    if (BpsId == "" || BpsId == null || BpsId == undefined)
        BpsId = codigo;

    $.ajax({
        url: '../Descuento/InsertarDescuentosSocios',
        data: { BpsId: BpsId},
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //Mandar al Segundo insert en la tabla REC_DISCOUNTS_BPS
                //insertarSocio

            } else if (dato.result == 0) {
                //alert(dato.message);
            }
        }
    });


}

//Fubcion que es llama por el boton "btnGrabar" de este documento
function InactivarDescuentos(BPSID,BPSID_GRUPO) {
    //alert("Inactivando");
    $.ajax({
        url: '../Descuento/InactivarDescuentoSocioGrabar',
        data: { BPSID: BPSID, BPSID_GRUPO: BPSID_GRUPO },
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //Mandar al Segundo insert en la tabla REC_DISCOUNTS_BPS

            } else if (dato.result == 0) {
                //alert(dato.message);
            }
        }

    });


}


//licencia

function loadDataLicencia() {
    var BPSID = $("#hidBpsId").val() == "" ? 0 : $("#hidBpsId").val();
    $("#gridLicencia").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 15,
            transport: {
                read: {
                    url: "../DerechoAdministracion/ObtenerLicenciasPorSocio", dataType: "json", data: { BPSID: BPSID }
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
            { field: "MONEDA", width: 25, title: "Moneda", template: "<a id='single_2' style='color:gray !important;'>${MONEDA}</a>" },
            { field: "TIPOPAGO", width: 25, title: "Tipo pago", template: "<a id='single_2' style='color:gray !important;'>${TIPOPAGO}</a>" },
            { field: "INVG_DESC", width: 30, title: "Grup. facturación", template: "<a id='single_2' style='color:gray !important;'>${INVG_DESC}</a>" },
            { field: "LOG_USER_CREAT", width: 20, title: "Usu. Crea", template: "<a id='single_2' style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
            { field: "LOG_DATE_CREAT", type: "date", width: 20, title: "Fecha Crea", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            { field: "LOG_USER_UPDAT", width: 20, title: "Usu. Modi", template: "<a id='single_2'  style='color:gray !important;'${LOG_USER_UPDAT}</a>" },
            { field: "LOG_DATE_UPDATE", type: "date", width: 20, title: "Fecha Modi", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_UPDATE==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_UPDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            { title: "<center>Ver</center>", width: 20, template: "<center><a id='single_2' href=javascript:verlicencia('${LIC_ID}') style='color:Blue !important;'> <img src='../Images/botones/new32.png'  width='20px'> <label id='lblVer'></label>Licencia</a></center>" },
            //{ title: "Ver", width: 15, template: "<a id='single_2' style='color:Blue !important;' rel='external' href='../Licencia/Nuevo?set=${LIC_ID} /target='_blank''>Ver Licencia</a>" },
           ]
    });
}

//verlicencias
function verlicencia(id) {
    //location.href = "../Licencia/Nuevo?set=" + id;
    window.open("../Licencia/Nuevo?set=" + id);
}

//establecimientos 

function loadDataEstablecimientos() {
    var BPSID = $("#hidBpsId").val() == "" ? 0 : $("#hidBpsId").val();
    $("#gridEstablecimiento").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 15,
            transport: {
                read: {
                    url: "../DerechoAdministracion/ObtenerEstablecimientosPorSocio", dataType: "json", data: { BPSID: BPSID }
                }
            },
            schema: { data: "Establecimiento", total: 'TotalVirtual' }
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
            { field: "EST_ID", width: 10, title: "Id", template: "<a id='single_2' style='color:gray !important;'>${EST_ID}</a>" },

            { field: "EST_NAME", width: 50, title: "Establecimiento", template: "<a id='single_2' style='color:gray !important;'>${EST_NAME}</a>" },
            { field: "LOG_USER_CREAT", width: 20, title: "Usu. Crea", template: "<a id='single_2' style='color:gray !important;'>${LOG_USER_CREAT}</a>" },
            { field: "LOG_DATE_CREAT", type: "date", width: 20, title: "Fecha Crea", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            { field: "LOG_USER_UPDAT", width: 20, title: "Usu. Modi", template: "<a id='single_2'  style='color:gray !important;'${LOG_USER_UPDAT}</a>" },
            { field: "LOG_DATE_UPDATE", type: "date", width: 20, title: "Fecha Modi", template: "<a id='single_2' style='color:gray !important;'>" + '#=(LOG_DATE_UPDATE==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_UPDATE,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>" },
            { title: "<center>Ver</center>", width: 20, template: "<center><a id='single_2' href=javascript:verEstablecimiento('${EST_ID}') style='color:Blue !important;'> <img src='../Images/botones/new32.png'  width='20px'> <label id='lblVer'></label>Establecimiento</a></center>" },
            //{ title: "Ver", width: 15, template: "<a id='single_2' style='color:Blue !important;' rel='external' href='../Licencia/Nuevo?set=${LIC_ID} /target='_blank''>Ver Licencia</a>" },
           ]
    });
}

function verEstablecimiento(id) {
    //location.href = "../Licencia/Nuevo?set=" + id;
    window.open("../EstablecimientoAdministracion/Nuevo?id=" + id);
}
