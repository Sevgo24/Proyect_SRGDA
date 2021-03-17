var K_DIV_CONTENDOR = {
    DIV_TAB_FACT: "divTabFacturacion",
    DIV_TAB_DSCTO: "divPeriodoDescuentoLst",
    DIV_TAB_CARACT: "divTabCaract"
};
var K_DIV_MESSAGE = {
    DIV_LICENCIA: "divResultadoCab",
    DIV_TAB_PROCESO: "divResultadoTabProc",
    DIV_TAB_FACT: "divResultadoFac",
    DIV_TAB_POPUP_FACTURACION: "avisoPlanificacion",
    DIV_TAB_POPUP_AUTORIZACION: "avisoAutorizacion",
    DIV_TAB_POPUP_SHOW: "avisoPopupShow",
    DIV_TAB_POPUP_ARTISTA: "avisoPopupArtista",
    DIV_TAB_POPUP_GARANTIA: "avisoGarantia",
    DIV_TAB_POPUP_DEVOLVER_GARANTIA: "avisoDevolverGarantia",
    DIV_TAB_POPUP_LOCALIDAD: "avisoLocalidad",
    DIV_TAB_POPUP_REPORTE: "avisoReporte",
    DIV_TAB_POPUP_REPORTE_DETA: "avisoReporteDeta",
    DIV_TAB_POPUP_ENTIDAD: "avisoEntidad",
    DIV_TAB_POPUP_DESCUENTO: "avisoDescuento",
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento",
    DIV_TAB_POPUP_DOCUMENTO_IN: "avisoProcUpload",
    DIV_TAB_DSCTO: "avisoTabDescuento",
    DIV_TAB_CARACT: "avisoTabCaracteristica"
};
var K_DIV_POPUP = {
    NUEVO_AUTORIZACION: "mvAutorizacion",
    NUEVO_GARANTIA: "mvGarantia",
    DEVOLVER_GARANTIA: "mvGarantiaDevol",
    SHOWS: "mvShow",
    ARTISTA: "mvArtista",
    LOCALIDAD: "mvLocalidad",
    REPORTE: "mvReporte",
    REPORTE_DETA: "mvReporteDeta",
    NUEVO_ENTIDAD: "mvEntidad",

    DESCUENTO: "mvDescuento",
    DOCUMENTO: "mvDocumento",
    DOCUMENTO_IN: "mvDocumentoTraces",
    FACTURAS: "mvFacturas"

};
/*HIDDEN QUE CONTIENEN LOS IDS CLAVES PARA TODO EL FORMULARIO DE LICENCIAMIENTO*/
var K_HID_KEYS = {
    MODALIDAD: "#hidModalidad",
    ESTABLECIMIENTO: "#hidEstablecimiento",
    LICENCIA: "#hidLicId",
    SOCIO: "#hidResponsable",
    WORKFLOW: "#hidWorkflow",
    TARIFA: "#hidCodigoTarifa",
    MODALIDAD_USO: "#hidModalidadUso",
    LICENCIA_MASTER: "#hidLicMaster"
};
var K_DIV_VALIDAR = { DIV_CAB: "divCabecera" };
var K_TIPO_LIC = { MULTIPLE: 3, SIMNPLE: 2 };
var K_MENSAJES = {
    TAB_FACTURACION: "Configuración de Facturación registrada correctamente"
};
var K_MENSAJES_ERROR = {
    TAB_POPUP_FACTURACION: "Debe ingresar todos los campos"
};
var K_TITULO = {
    EDITAR: "Licencia / Actualizar",
    NUEVO: "Licencia / Nuevo"
};

var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

var K_WIDTH = 1000;
var K_HEIGHT = 400;
var K_WIDTH_OBS = 700;
var K_HEIGHT_OBS = 280;
var K_SIZE_PAGE = 8;

var K_TIPO__ENVIO_FACT = 6;
var K_FORMA_ENTREGA_FACT = 5;

//varaible para que la Programacion Descuento Reconozca que el descuento se esta haciendo por Socio y no Por Licencia
var K_RECONOCE_SOCIO = {
    SOCIO: 0
};
var K_Activar_Alfresco;

//$(document).ready(function () {
//    console.log("ready!");
//});

function initPopups() {

    kendo.culture('es-PE');
    /*inicalizar popups*/

    $("#mvAdvertencia").dialog({
        close: function (event) {
            if (event.which) { returnPage(); }
        }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    });
    $("#mvDocumentoTraces").dialog({ autoOpen: false, width: 500, height: 260, buttons: { "Continuar >>": addDocumentoTrace }, modal: true });
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Grabar": ActivarAlfresco, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    //$("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: {  "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Grabar": addParametro, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Grabar": addObservacion, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvGrupoFacturacion").dialog({ autoOpen: false, width: 260, height: 160, buttons: { "Grabar": addGrupoFacturacion, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvPlanificacion").dialog({ autoOpen: false, width: 300, height: 200, buttons: { "Agregar": InsertaPlaneamientoActual, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvAutorizacion").dialog({ autoOpen: false, width: 500, height: 300, buttons: { "Grabar": addAutorizacion, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog({ autoOpen: false, width: 600, height: 280, buttons: { "Grabar": addGarantia, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.SHOWS).dialog({ autoOpen: false, width: 500, height: 400, buttons: { "Grabar": insertarShow, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.ARTISTA).dialog({ autoOpen: false, width: 500, height: 240, buttons: { "Grabar": insertarArtista, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.REPORTE).dialog({ autoOpen: false, width: 460, height: 300, buttons: { "Grabar": addReporte, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.REPORTE_DETA).dialog({ autoOpen: false, width: 450, height: 375, buttons: { "Grabar": addReporteDeta, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#" + K_DIV_POPUP.DESCUENTO).dialog({ autoOpen: false, width: 400, height: 280, buttons: { "Agregar": addDescuento, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });

    $("#" + K_DIV_POPUP.DEVOLVER_GARANTIA).dialog({ autoOpen: false, width: 270, height: 170, buttons: { "Grabar": devolverGarantia, "Cancelar": function () { $(this).dialog("close"); } }, modal: false });

    $("#mvEjecutarProceso").dialog({ title: "SGRDA :: Visor de Formatos.", autoOpen: false, width: 850, height: 650, modal: true });
    $("#mvImagen").dialog({ title: "SGRDA :: Visor de documentos.", autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    $("#mvWord").dialog({ title: "SGRDA :: Descargando Word...", autoOpen: false, width: 250, height: 80, modal: true });
    $("#mvTimeLine").dialog({ title: "SGRDA :: Time Line Acciones.", autoOpen: false, width: 650, height: 300, modal: false });
    $("#mvFacturas").dialog({ title: "SGRDA :: Facturas ", autoOpen: false, width: 990, height: 350, modal: false });
    //--------------------cambio para planillas
    ListarSerieXtipo("ddlSerie", 'PL', 0);
    //------------------------------------------

}

$(function () {

    initPopups();
    InitPoPupDivision();
    initPopupsReporteArtista();
    initPopupsDescuentoLicencia();
    $("#tabs").tabs();

    $("#ddlArtistaAlfresco").hide();
    $("#ddlArtistaAlfresco_td").hide();
    // 
    $("#btnGrabar").show();
    $("#btnGrabarFacturacion").show();
    $("#btnGrabarCaract").show();

    $.fn.disableTab = function (tabIndex, hide) {
        // Get the array of disabled tabs, if any
        var disabledTabs = this.tabs("option", "disabled");
        if ($.isArray(disabledTabs)) {
            var pos = $.inArray(tabIndex, disabledTabs);
            if (pos < 0) {
                disabledTabs.push(tabIndex);
            }
        }
        else {
            disabledTabs = [tabIndex];
        }
        this.tabs("option", "disabled", disabledTabs);
        if (hide === true) {
            $(this).find('li:eq(' + tabIndex + ')').addClass('ui-state-hidden');
        }
        // Enable chaining
        return this;
    };

    $.fn.enableTab = function (tabIndex) {

        // Remove the ui-state-hidden class if it exists
        $(this).find('li:eq(' + tabIndex + ')').removeClass('ui-state-hidden');
        // Use the built-in enable function
        this.tabs("enable", tabIndex);
        // Enable chaining
        return this;
    };


    $.ajaxSetup({
        error: function (jqXHR, exception) {
            if (jqXHR.status === 0) {
                // alert('Not connect: Verify Network.');
            } else if (jqXHR.status == 404) {
                // alert('Requested page not found [404]');
            } else if (jqXHR.status == 500) {
                //alert('Internal Server Error [500].');
            } else if (exception === 'parsererror') {
                alert('Requested JSON parse failed.');
            } else if (exception === 'timeout') {
                alert('Time out error.');
            } else if (exception === 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('Uncaught Error: ' + jqXHR.responseText);
            }
            //  $("#loading").dialog("close");
        },
        complete: function () {
            // $("#loading").dialog("close");
        },
        success: function () {
            //  $("#loading").dialog("open");
        }
    });

    $("#divTituloLic").html(K_TITULO.NUEVO);
    $("#btnBuscarCaract").css({ "display": "none" });

    InitTemporalidadTarifa();


    var codeEdit = (GetQueryStringParams("set"));
    if (codeEdit === undefined) {
        $("#divTituloLic").html(K_TITULO.NUEVO);
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        loadTipoLicencia('ddlTipoLicencia', '0');
        loadMonedaRecaudacion('ddlMoneda', '0');

        LoadTipoPago('ddlTipoPago', 0);
        $('#ddlEstadoLicencia option').remove();
        $('#ddlEstadoLicencia').append($("<option />", { value: 0, text: "--SELECCIONE--" }));
        $("#btnRefrescar").attr("disabled", "disabled");
        $("#btnTimeLine").attr("disabled", "disabled");
        $("#btnVisualizaPopUp").css("display", "inline");
        $("#btnverEstablecimiento").attr("disabled", "disabled"); //inactivando el boton 
        //agregando el POP UP 
        DivisionObligatorio();

    } else {
        $("#divTituloLic").html(K_TITULO.EDITAR);
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $(K_HID_KEYS.LICENCIA).val(codeEdit);
        ObtenerLicencia(codeEdit);
        var idLicenciaDiv = $('#hidLicId').val();
        loadDataDivisionLic(idLicenciaDiv);
    }

    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnAgregarCadena", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarResponsable", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lblResponsable" });
    mvInitBuscarSocioHistorico({ container: "ContenedormvBuscarSocioHistorico", idButtonToSearch: "btnHistorico", idDivMV: "mvBuscarSocioHistorico", idLabelToSearch: "lblResponsable" });
    //DAVID NOTA: LLAMAR A LA FUNCION PARA LLAMAR AL POP UP buscador.establecimiento o establecimientoSocioEmpresarial
    //mvInitEstablecimientoSocioEmpre({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });
    // mvInitEstablecimientoSocioEmpre({ container: "ContenedormvEstablecimientoSocEmp", idButtonToSearch: "btnBuscarEstablecimientoMult", idDivMV: "mvEstablecimientoSocEmp", idLabelToSearch: "lblEstablecimiento" });
    mvInitEstablecimientoSocioEmpre({ container: "ContenedormvEstablecimientoSocEmp", idButtonToSearch: "btnBuscarEstablecimientoMult", idDivMV: "mvEstablecimientoSocEmp", idLabelToSearch: "lblEstablecimiento" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEstablecimiento", idLabelToSearch: "lblEstablecimiento" });
    //
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });
    mvInitBuscarSocioEntidad({ container: "ContenedormvBuscarEntidad", idButtonToSearch: ".addEntidad", idDivMV: "mvBuscarEntidad", event: "reloadEventoEntidad" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRU", idDivMV: "MvBuscarGrupoFacturacion", event: "reloadEventoGrupoFact", idLabelToSearch: "lbGrupo" });
    /*INICIO ACCCIONES PRINCIPALES GENERALES*/
    //david
    $("#btnBuscarEstablecimiento").on("click", function () { BuscaEstablecimiento(); }).button();
    $("#btnverEstablecimiento").on("click", function () { VerEstablecimientoLicencia(); }).button();
    $("#btnAgregarCadena").on("click", function () { AgregarCadena(); }).button();


    //$("#btnBuscarEstablecimientoMult").on("click", function () { BuscaEstablecimiento(); }).button();
    //$("#btnGrabar").on("click", function () { insertar(); }).button();

    $("#btnHistorico").on("click", function () { /*MandarHistorico(codeEdit)*/ }).button();
    $("#btnGrabar").on("click", function () { ValidarInsert(); }).button();
    $("#btnRefrescar").on("click", function () { ObtenerLicencia($(K_HID_KEYS.LICENCIA).val()); }).button();
    $("#btnVisualizaPopUp").on("click", function () { $("#mvLicDivisionAgenteObligatorio").dialog("open"); }).button();

    $("#btnNuevo").on("click", function () { document.location.href = "../Licencia/Nuevo"; }).button();
    $("#btnRegresar").on("click", function () { document.location.href = "../Licencia/Index"; }).button();
    $("#btnTimeLine").on("click", function () { $("#mvTimeLine").dialog("open"); }).button();
    $("#btnTimeLine").hide();
    /*FIN ACCCIONES PRINCIPALES GENERALES*/

    $('#txtCodigoLicMultiple').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValorGar').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValApl').on("keypress", function (e) { return solonumeros(e); });
    $('#txtValDev').on("keypress", function (e) { return solonumeros(e); });
    $('#txtNumeroGar').on("keypress", function (e) { return solonumeros(e); });

    $('#txtTicketLoc').on("keypress", function (e) { return solonumeros(e); });
    $('#txtPrcVentaLoc').on("keypress", function (e) { return solonumeros(e); });
    $('#txtImporteBLoc').on("keypress", function (e) { return solonumeros(e); });
    $('#txtImpuestoLoc').on("keypress", function (e) { return solonumeros(e); });
    $('#txtImporteNLoc').on("keypress", function (e) { return solonumeros(e); });


    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $(".addGarantia").on("click", function () { limpiarGarantia(); $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("option", "title", "Agregar Garantía"); $("#" + K_DIV_POPUP.NUEVO_GARANTIA).dialog("open"); });
    $(".addGrupoFacturacion").on("click", function () { limpiarGrupoFacturacion(); $("#mvGrupoFacturacion").dialog("open"); });
    $(".addPlanificacion").on("click", function () { limpiarPlanificacion(); $("#mvPlanificacion").dialog("open"); });
    $(".addAutorizacion").on("click", function () { limpiarAutorizacion(); $("#mvAutorizacion").dialog("option", "title", "Agregar Autorización"); $("#mvAutorizacion").dialog("open"); });
    $(".addReporte").on("click", function () { limpiarReporte(); initLoadAddReporte(0); $("#" + K_DIV_POPUP.REPORTE).dialog("option", "title", "Agregar Reporte"); $("#" + K_DIV_POPUP.REPORTE).dialog("open"); });
    $(".addDescuento").on("click", function () { limpiarDescuento(); initLoadAddDescuento(K_DIV_POPUP.DESCUENTO, K_DIV_CONTENDOR.DIV_TAB_DSCTO, K_DIV_MESSAGE.DIV_TAB_DSCTO); });
    $(".addLocalidadAforo").on("click", function () { addLocalidadAforo(); });
    //$(".addLocalidades").on("click", function () { addLocalidades(); });

    /*Listener cargar grillas tabs*/
    $(".verAutorizacion").on("click", function () { loadDataAutorizacion($(K_HID_KEYS.LICENCIA).val()); });
    $(".verReporte").on("click", function () { loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val()); });
    $(".verEntidad").on("click", function () { loadDataEntidades($(K_HID_KEYS.LICENCIA).val()); });
    $(".verGarantia").on("click", function () { loadDataGarantia($(K_HID_KEYS.LICENCIA).val()); });
    $(".verObservacion").on("click", function () { loadDataObservacion($(K_HID_KEYS.LICENCIA).val()); });
    $(".verDocumento").on("click", function () { LoadDocumentoAlfresco_Lyrics(); });
    $(".verParametro").on("click", function () { loadDataParametro($(K_HID_KEYS.LICENCIA).val()); });
    $(".verMatriz").on("click", function () { loadDataMatrizLocalidad($(K_HID_KEYS.LICENCIA).val()); });
    $(".verPlanificacion").on("click", function () { loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), $("#ddlAniPlaniamiento").val()); msgErrorB(K_DIV_MESSAGE.DIV_TAB_FACT, ""); });

    //$(".verLocalidadAforo").on("click", function () { loadDataLocalidadAforo($(K_HID_KEYS.LICENCIA).val()); });
    //$(".verLocalidades").on("click", function () { loadDataLocalidades($(K_HID_KEYS.LICENCIA).val()); });
    loadTipoDocAlfresco("ddlTipoDocumento", 0);
    loadTipoDocAlfresco("ddlOrigenDoc", 0);
    //ARTISTA
    $("#ddlTipoDocumento").change(function () {
        if ($("#ddlTipoDocumento").val() == 2) {
            $("#ddlArtistaAlfresco_td").show();
            $("#ddlArtistaAlfresco").show();
            loadArtista_X_Licencia("ddlArtistaAlfresco", 0, $(K_HID_KEYS.LICENCIA).val());
        }
        else {
            $("#ddlArtistaAlfresco").hide();
            $("#ddlArtistaAlfresco_td").hide();

        }
    });


    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);

    /*eventos tab facturacion*/
    $("#ddlAniPlaniamiento").on("change", function () { loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), $("#ddlAniPlaniamiento").val()); msgErrorB(K_DIV_MESSAGE.DIV_TAB_FACT, ""); });
    $("#btnGrabarFacturacion").on("click", function () { InsertarFacturacion(); }).button({ icons: { secondary: "ui-icon-disk" } });
    $("#btnGrabarCaract").on("click", function () {
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_CARACT, K_DIV_CONTENDOR.DIV_TAB_CARACT)) {
            InsertarCaracteristica($("#ddlPerPlanFacCarac option:selected").val(), $("#ddlFechasSearchCarac  option:selected").val());
        }
    }).button({ icons: { secondary: "ui-icon-disk" } });

    $("#btnGrabarCaractDsc").on("click", function () {
        // if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_CARACT, K_DIV_CONTENDOR.DIV_TAB_CARACT)) {
        InsertarCaracteristicaDsc($("#ddlPerPlanFacCarac option:selected").val(), $("#ddlFechasSearchCarac  option:selected").val());
        // } else {
        //     alert("Debe seleccionar un periodo del TAB CARACTERISTICA");
        // }
    }).button({ icons: { secondary: "ui-icon-disk" } });


    $("#btnFacturaManual").on("click", function () {
        var Lic_Id=$(K_HID_KEYS.LICENCIA).val();
        var ValidaFacturacion = ValidarFacturacion(Lic_Id);
        if (ValidaFacturacion == 0) {
            ShowPopUpFacturacionManual();
        } else {
            alert("Usted no cuenta con permisos para Facturar.")
        }
       
    }).button();
    //$("#btnFacturaManual").hide();
    /*fin eventos tab facturacion*/
    InicializarTab();
    $('.k-fecha').each(function (i, elem) { $(elem).kendoDatePicker({ format: "dd/MM/yyyy" }); });
    $("#ddlAutorizacionRep").on("change", function () { loadShow("ddlShowRep", $(this).val(), 0); });
    $("#ddlShowRep").on("change", function () { loadArtista("ddlArtistaRep", $(this).val(), 0); });
    $('#txtDocumento').on("keypress", function (e) { return solonumeros(e); });
    loadTipoDocumento("ddlTipoDocumento1", 0);

    //var K_ID_DESCUENTO_ESPECIAL2 = ObtieneIdTipoDscto();
    $("#btnBuscarSocio").on("click", function () { buscarSocio(); });
    //Descuento -- al elegir TIPO ESPECIAL SE DEBE HABLITAR LOS TXT Y LA posibilidad de Ingresar
    $("#ddlSignoDescuento").hide();
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
    $("#ddlTemporalidad").on("change", function () { limpiarTarifa(); obtenerTarifa($(this).val()); });

    // $("#txtCodigoLicMultiple").attr("disabled", "disabled");

    //cambiando los botones 
    $("#ddlTipoLicencia").on("change", function () {
        var testSel = $("#ddlTipoLicencia option:selected").text();

        if (testSel.indexOf("MULTIPLE") != -1 || testSel.indexOf("MÚLTIPLE") != -1 || testSel.indexOf("multiple") != -1 || testSel.indexOf("múltiple") != -1) {
            $("#txtCodigoLicMultiple").removeAttr("disabled");
            //AutogeneraCOdigo (function creada en este mismo Documento)
            var codigo = AutogeneraCOdigoLicMuilti();
            $("#txtCodigoLicMultiple").val(codigo);
            $("#txtCodigoLicMultiple").focus();
            $("#txtCodigoLicMultiple").prop("readonly", true);
            $("#tdlblestselec").show();
        } else {
            $("#txtCodigoLicMultiple").attr("disabled", "disabled");
            $("#txtCodigoLicMultiple").val("");
            //borrando el gridview
            $("#gridcontenedorListaDestino").html('');
            $("#tdlblestselec").hide();
        }
    });
    //$("#ddlOrigenDoc").on("change", function () { loadDataDocumento($(K_HID_KEYS.LICENCIA).val()); });
    //var $("#ddlOrigenDoc").

    $("#ddlOrigenDoc").on("change", function () { loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val()); });
    $("input:radio[name=rdbDuracion]").click(function () {
        var value = $(this).val();
        var tiempo = $("#txtDuracion").val();
        var segt = calcularSegundos(value, tiempo);
        $("#txtTotalSeg").val(segt);
        $("#txtDuracion").focus();
    });
    $("#txtDuracion").on("blur", function () {
        var tiempo = $("#txtDuracion").val();
        var value = $("input:radio[name=rdbDuracion]:checked").val();
        var segt = calcularSegundos(value, tiempo);
        $("#txtTotalSeg").val(segt);
    });
    $("#txtFecEmisionRepDeta").kendoDateTimePicker({ format: "dd/MM/yyyy  HH:mm:ss" });
    $("#btnMatriz").on("click", function () {
        estadoRadioButton = validarRadioButtonLiquidar();
        LimpiarRequeridos(DIV_MSG_AVISO_LOCALIDADES, MV_DIV_LOCALIDADES);
        if (estadoRadioButton && ValidarRequeridosMV(DIV_MSG_AVISO_LOCALIDADES, MV_DIV_LOCALIDADES)) {
            obtenerMatrizValor();
        } else {
            var index = $('#tabs a[href="#tabs-5"]').parent().index();
            $("#tabs").tabs("option", "active", index);
        }
    });

    $("#ddlPerPlanFacCarac").on("change", function () {
        if ($(this).val() != "0") {
            QuitarBordeObligatorio(K_DIV_CONTENDOR.DIV_TAB_CARACT, K_DIV_MESSAGE.DIV_TAB_CARACT);
        }
        else {
            msgOkB(K_DIV_MESSAGE.DIV_TAB_CARACT, "");
        }
        loadFechaCaractLic("ddlFechasSearchCarac", [$(K_HID_KEYS.LICENCIA).val(), $(this).val()], function (exito) {
            loadDataCaracteristica($(K_HID_KEYS.LICENCIA).val(), $("#ddlFechasSearchCarac").val(), $("#ddlPerPlanFacCarac").val());
        });
    });
    $("#ddlFechasSearchCarac").on("change", function () {
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_CARACT, K_DIV_CONTENDOR.DIV_TAB_CARACT)) { }
        loadDataCaracteristica($(K_HID_KEYS.LICENCIA).val(), $("#ddlFechasSearchCarac").val(), $("#ddlPerPlanFacCarac").val());
    });
    $("#ddlPerPlanFacDesc").on("change", function () {
        if ($(this).val() != "0") { QuitarBordeObligatorio(K_DIV_CONTENDOR.DIV_TAB_DSCTO, K_DIV_MESSAGE.DIV_TAB_DSCTO); }
        //mantenimiento.descuento.js/loadDataDescuentos
        //   loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), $(this).val());
    });
    //$("#divBtnAddDscto").css("display", "none");
    $("#divplanifi").css("display", "none");
    $("#btnCalculoTTDescuento").css("display", "none");
    $("#trMostrarText").hide();
    //Ocultando los Botones.
    $("#ddlTipoLicencia").change(function () {
        if ($(this).val() == "2") {
            document.getElementById('btnBuscarEstablecimientoMult').style.display = 'none';
            document.getElementById('btnBuscarEstablecimiento').style.display = 'inline';
            $("#lblEstablecimiento").html('SELECCIONE');
            $("#lblResponsable").html('SELECCIONE');

        } else if ($(this).val() == "3") {
            document.getElementById('btnBuscarEstablecimiento').style.display = 'none';

            if ($("#hidModalidad").val() != 0 && $("#hidModalidad").val() != undefined && $("#hidModalidad").val() != null) {
                document.getElementById('btnBuscarEstablecimientoMult').style.display = 'inline';
                $("#lblEstablecimiento").html('SELECCIONE');
                $("#lblResponsable").html('SELECCIONE');
            }
            else
                $("#lblEstablecimiento").html('SELECCIONE PRIMERO LA MODALIDAD');
        }
    });

    //Recuperando El Codigo Para Listar Establecimientos
    //$("#btnBuscarEstablecimientoMult").on("click", function () {
    //    var dato = $("#hidResponsable").val();
    //    //alert("Mostrar ="+dato);
    //    BuscarEstablecimientoSocioEmpresarial(dato);
    //});

    //Al iniciar el ChekcBox debe estar marcado
    $("#chkMostarEstab").prop("checked", true);
    //Muestra y oculta los Establecimientos Seleccionados
    $("#chkMostarEstab").change(function () {
        if ($("#chkMostarEstab").is(':checked')) {
            $("#gridcontenedorListaDestino").show();
            //cambia el color de el texto "ESTABLECIMIENTOS SELECCIONADOS"
            $("#lblestselec").css("color", "#000000");
        } else {
            $("#gridcontenedorListaDestino").hide();
            //CAMBIA EL COLOR DE EL TEXTO "ESTABLECIMIENTOS SELECCIONADOS"
            $("#lblestselec").css("color", "#ff0000");

        }
    });

    //Ocultar El txtCodigoLicMultiple
    $("#txtCodigoLicMultiple").hide();



    //Listando los Descuentos sin Necesidad de Obtener planificacion
    loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
    loadDataGridTmpDescuentoPlantilla('../Descuento/ListarCaracteristicaDescuentos', "#gridDescuentoTarifa", $("#hidLicId").val());

    //LoadComboTarifa
    loadComboTarifa(0, 0, 0, 'hidCodigoTarifa');
    $("#lblTarifaDesc").change(function () {
        $("#hidCodigoTarifa").val($(this).val());
        loadComboTarifa($("#hidModalidad").val(), $("#hidCodigoTemporalidad").val(), $(this).val(), 'hidCodigoTarifa');

    });
    //limpiar planificacion
    $("#ddlAniPlaniamiento").change(function () {
        if ($(this).val() == "0") {
            limpiarPlanificacion();
        }
    });


    $("#TROcultaTipoFact").hide(); // ocullta Tr DE ELECCION DE TIPO DE ENVIO DE FACTURA (YA NO SE UTILIZA)
    $("#btnRefrescar").hide();
    $("#btnNuevo").hide();


});



//--------------------------------------------------



function calcularSegundos(value, tiempo) {
    var tottal = 0;
    if (tiempo == "") tiempo = 0;
    if (value == "min") {
        tottal = (tiempo * 60);
    } else {
        tottal = tiempo;
    }
    return tottal;
}
function ValidarPeriodoRepetido() {


    if ($("#txtFechaPlanificacion").val() != "") {
        var fecha = $("#txtFechaPlanificacion").val();
        var anio = fecha.substr(6, 4);
        var mes = fecha.substr(3, 2);
        //Crear un If Para Recuperar los Codigos de La licencias Hijas
        var valida = ValidarLicenciaMultipleHija();

        $.ajax({
            data: { codLic: $(K_HID_KEYS.LICENCIA).val(), anio: anio },
            type: 'POST',
            url: 'ValidarPeriodoRepetido',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    // alert(dato.message+'...000');
                    $("#ddlAniPlaniamiento").prop('selectedIndex', 0);
                    loadDataNuevaPlanificacion(anio, mes);

                } else if (dato.result == 0) {
                    if (valida != 0) {

                        //    //SI es Licencia Hija No debe Permitir agregar Planificacion
                        //Confirmar('Desea Agregar Planificacion?', function () {
                        $("#ddlAniPlaniamiento").prop('selectedIndex', 0);

                        loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), $("#ddlAniPlaniamiento").val());
                        $("#txtFechaPlanificacion").val("");
                        //  $(this).dialog("close");
                        //},
                        //function () { },
                        //'Confirmar');
                        //    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_FACTURACION,"Solo se Puede Agregar Planificacion desde la Licencia Maestra");
                    } else {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_FACTURACION, dato.message);
                    }
                    //alert(dato.message);
                }
            }
        });

    } else {
        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_FACTURACION, K_MENSAJES_ERROR.TAB_POPUP_FACTURACION);
    }
}
//"divPeriodoDescuentoLst" por queee???
function InsertarFacturacion() {
    var resp = ValidarPeriodoRepetidoLicenciasMultiples();
    if (resp == 1) {
        var esInsert = $("#ddlAniPlaniamiento").val() == '0' ? true : false;
        var idTemp = $("#hidCodigoTemporalidad").val();
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_FACT, K_DIV_CONTENDOR.DIV_TAB_FACT)) {
            $.ajax({
                data: {
                    codLic: $(K_HID_KEYS.LICENCIA).val(),
                    docReq: $("#chkIndDocReq").prop("checked") ? "1" : "0",
                    actReq: $("#chkIndDetaFact").prop("checked") ? "1" : "0",
                    plaReq: $("#chkPlanilla").prop("checked") ? "1" : "0",
                    desVis: $("#chkIndDscVisible").prop("checked") ? "1" : "0",
                    EmiMen: $("#chkLicEmiMensual").prop("checked") ? "1" : "0",
                    Envio: K_TIPO__ENVIO_FACT,
                    facGruop: $("#hidGrupoFacturacion").val(),
                    facForm: K_FORMA_ENTREGA_FACT,
                    esInsert: esInsert,
                    idTemp: idTemp
                },
                type: 'POST',
                url: 'InsertarLicenciaFactura',
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        if (esInsert) {
                            loadAnioPlaneamiento('ddlAniPlaniamiento', dato.valor, '--SELECCIONE--', $(K_HID_KEYS.LICENCIA).val());
                            loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), dato.valor);
                            loadDataGridTmpDescuentoPlantilla('../Descuento/ListarCaracteristicaDescuentos', "#gridDescuentoTarifa", $("#hidLicId").val());//Que liste automaticamente las caracteristicas de descuento
                        }

                        /*BEGIN - RECARGAR LISTAS DE PLANIFICACION DE TABS CARACTERISTICAS Y DESCUENTOS SI ESTAN ACTIVAS*/
                        var disabled = $("#tabs").tabs("option", "disabled");
                        var refreshTabDiscount = true;
                        var refreshTabCharac = true;
                        $.each(disabled, function (key, value) {
                            if (value == 2) { refreshTabDiscount = false; }
                            if (value == 1) { refreshTabCharac = false; }
                        });
                        if (refreshTabDiscount) loadPeriodoPlanFactura("ddlPerPlanFacCarac", $(K_HID_KEYS.LICENCIA).val(), $("#ddlPerPlanFacCarac").val());
                        if (refreshTabCharac) loadPeriodoPlanFactura("ddlPerPlanFacDesc", $(K_HID_KEYS.LICENCIA).val(), $("#ddlPerPlanFacDesc").val());
                        /*END - RECARGAR LISTAS DE PLANIFICACION DE TABS CARACTERISTICAS Y DESCUENTOS SI ESTAN ACTIVAS*/

                        msgOkB(K_DIV_MESSAGE.DIV_TAB_FACT, K_MENSAJES.TAB_FACTURACION);
                        $("#ddlTemporalidad").attr("disabled", "disabled");
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_FACT, dato.message);
                    }
                }
            });
        }
    }
}

function addGrupoFacturacion() {
    if ($("#txtGrupoDescripcion").val() == "") {
        $("#avisoGruop").html("<font color='red'>* Campos Obligatorios<font>");
    } else {
        var grupoFacturacion = {
            GrupoDescripcion: $("#txtGrupoDescripcion").val(),
            idMod: $("#hidModalidad").val(),
            idBps: $("#hidResponsable").val()
        }
        $.ajax({
            data: grupoFacturacion,
            type: 'POST',
            url: 'AddGrupoFacturacion',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    var codFac = $("#hidGrupoFacturacion").val();
                    loadGrupoFacturacion('ddlgrupoFact', codFac, grupoFacturacion.idBps, codFac);

                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvGrupoFacturacion").dialog("close");
    }
}
function addObservacion() {

    var IdAdd = 0;

    if (ValidarRequeridosOBS()) {
        if ($("#hidAccionMvObs").val() === "1") {
            IdAdd = $("#hidEdicionObs").val();
            var observacion = {
                codLic: $(K_HID_KEYS.LICENCIA).val(),
                Id: IdAdd,
                TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
                Observacion: $("#txtObservacion").val()
            };
            $.ajax({
                url: 'UpdObservacion',
                type: 'POST',
                data: observacion,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        loadDataObservacion($(K_HID_KEYS.LICENCIA).val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        } else {
            var observacion = {
                codLic: $(K_HID_KEYS.LICENCIA).val(),
                Id: IdAdd,
                TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
                Observacion: $("#txtObservacion").val()
            };
            $.ajax({
                url: 'AddObservacion',
                type: 'POST',
                data: observacion,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        loadDataObservacion($(K_HID_KEYS.LICENCIA).val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        }

        $("#mvObservacion").dialog("close");
    }
}

function addDocumentoLyrics() {

    var IdAdd = 0;
    if ($("#hidAccionMvDoc").val() === "1") {
        IdAdd = $("#hidEdicionDoc").val();
    }


    if (IdAdd > 0) {
        $("#file_upload").removeClass("requerido");
    } else {
        $("#file_upload").addClass("requerido");
    }

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, K_DIV_POPUP.DOCUMENTO)) {
        var documento = {
            codLic: $(K_HID_KEYS.LICENCIA).val(),
            Id: IdAdd,
            TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
            FechaRecepcion: $("#txtFecha").val(),
            Archivo: $("#hidNombreFile").val()
        };

        if (IdAdd > 0) {
            $.ajax({
                data: documento,
                url: 'UpdDocumento',
                type: 'POST',
                success: function (response) {
                    var dato = response;
                    if (dato.result == 1) {
                        if ($("#file_upload").val() != "") {
                            InitUpload("file_upload", IdAdd);
                        }
                        loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                    } else {
                        alert(dato.message);
                    }
                }
            });
        } else {
            //es nuevo y seleccionó nueva imagen
            if (IdAdd == 0) {
                documento.Archivo = "";
                $.ajax({
                    data: documento,
                    url: 'AddDocumento',
                    type: 'POST',
                    async: false,
                    success: function (response) {
                        var dato = response;
                        if (dato.result == 1) {
                            InitUpload("file_upload", dato.Code);
                            loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
                        } else {
                            alert(dato.message);
                        }
                    }
                });

            }

        }
        loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
        $("#mvDocumento").dialog("close");
    }

}

function addParametro() {

    var IdAdd = 0;
    if (ValidarRequeridosPM()) {
        if ($("#hidAccionMvPar").val() === "1") {
            IdAdd = $("#hidEdicionPar").val();
            var entidad = {
                codLic: $(K_HID_KEYS.LICENCIA).val(),
                Id: IdAdd,
                TipoParametro: $("#ddlTipoParametro option:selected").val(),
                Descripcion: $("#txtDescripcionPar").val()
            };
            $.ajax({
                url: 'UpdParametro',
                type: 'POST',
                data: entidad,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        loadDataParametro($(K_HID_KEYS.LICENCIA).val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        } else {
            var entidad = {
                codLic: $(K_HID_KEYS.LICENCIA).val(),
                Id: IdAdd,
                TipoParametro: $("#ddlTipoParametro option:selected").val(),
                Descripcion: $("#txtDescripcionPar").val()
            };
            $.ajax({
                url: 'AddParametro',
                type: 'POST',
                data: entidad,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        loadDataParametro($(K_HID_KEYS.LICENCIA).val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        }
        $("#mvParametro").dialog("close");
    }


}


function updAddObservacion(idUpd) {
    limpiarObservacion();

    $.ajax({
        url: 'ObtieneObservacion',
        data: { idLic: $(K_HID_KEYS.LICENCIA).val(), idObs: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {

                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(obs.Id);
                    $("#ddlTipoObservacion").val(obs.TipoObservacion);
                    $("#txtObservacion").val(obs.Observacion);
                    $("#mvObservacion").dialog("open");
                } else {
                    alert("No se pudo obtener la dirección para editar.");
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
        url: 'ObtieneDocumento',
        data: { idLic: $(K_HID_KEYS.LICENCIA).val(), idDoc: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {


                    $("#hidAccionMvDoc").val("1");
                    $("#hidEdicionDoc").val(doc.Id);
                    $("#hidNombreFile").val(doc.Archivo);
                    loadTipoDoc("ddlTipoDocumento", doc.TipoDocumento);

                    var datepicker = $("#txtFecha").data("kendoDatePicker");
                    datepicker.value(doc.FechaRecepcion);
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

function updAddParametro(idUpd) {
    limpiarParametro();

    $.ajax({
        url: 'ObtieneParametro',
        data: { idLic: $(K_HID_KEYS.LICENCIA).val(), idPar: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvPar").val("1");
                    $("#hidEdicionPar").val(param.Id);
                    $("#ddlTipoParametro").val(param.TipoParametro);
                    $("#txtDescripcionPar").val(param.Descripcion);
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

function delAddObservacion(idDel, activo) {
    $.ajax({
        url: 'DellAddObservacion',
        type: 'POST',
        data: { id: idDel, esActivo: (activo == 1 ? true : false) },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataObservacion($(K_HID_KEYS.LICENCIA).val());
            }
        }
    });
    return false;
}

function delAddDocumento(idDel, activo) {

    $.ajax({
        url: 'DellAddDocumento',
        type: 'POST',
        data: { id: idDel, esActivo: (activo == 1 ? true : false) },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function delAddParametro(idDel, activo) {
    $.ajax({
        url: 'DellAddParametro',
        type: 'POST',
        data: { id: idDel, esActivo: (activo == 1 ? true : false) },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                loadDataParametro($(K_HID_KEYS.LICENCIA).val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarObservacion() {
    $("#txtObservacion").val("");
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    msgErrorOBS("", "txtObservacion");
}
function limpiarDocumento() {
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#file_upload").css({ 'border': '1px solid gray' });
    $("#txtFecha").css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#hidAccionMvDoc").val("0");
    $("#hidEdicionDoc").val("0");
    $("#hidNombreFile").val("");
}
function limpiarParametro() {
    $("#txtDescripcion").val("");
    $("#hidAccionMvPar").val("0");
    $("#hidEdicionPar").val("0");
    msgErrorPM("", "txtDescripcion");
}
function limpiarGrupoFacturacion() {
    $("#txtGrupoDescripcion").val("");
    $("#avisoGruop").html("");
}
function limpiarPlanificacion() {
    $("#txtFechaPlanificacion").val("");
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_FACT, ""); //K_DIV_MESSAGE.DIV_TAB_FACTURACION
    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_FACTURACION, "");
}

function loadDataComision(codLic) {
    loadDataGridTmp('ListarComision', "#gridComision", codLic);
}
function loadDataLocalidad(codLic) {
    loadDataGridTmp('ListarLocalidad', "#gridLocalidad", codLic);
}

function loadDataDocumento(codLic) {
    //loadDataGridTmp('ListarDocumento', "#gridDocumento", codLic);
    var TipoDocumento = $("#ddlOrigenDoc").val();
    $.ajax({
        data: { codigoLic: codLic, TipoDocumento: TipoDocumento },
        type: 'POST', url: 'ListarDocumento',
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato); /*add sysseg*/
            $("#gridDocumento").html(dato.message);
        }
    });
}

function loadDataDocumentoLyrics(codLic) {
    //loadDataGridTmp('ListarDocumento', "#gridDocumento", codLic);
    var TipoDocumento = $("#ddlOrigenDoc").val();
    //alert(TipoDocumento);
    $.ajax({
        data: { codigoLic: codLic, tipoOrigen: TipoDocumento },
        type: 'POST', url: 'ListarDocumentoLyrics',
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato); /*add sysseg*/
            $("#gridDocumento").html(dato.message);
        }
    });
}
function loadDataParametro(codLic) {
    loadDataGridTmp('ListarParametro', "#gridParametro", codLic);
}
function loadDataObservacion(codLic) {
    loadDataGridTmp('ListarObservacion', "#gridObservacion", codLic);
}
function loadDataGridTmp(Controller, idGrilla, codLic) {
    $.ajax({
        data: { codigoLic: codLic },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
        }
    });
}

function loadDataPlaneamiento(codLic, anio) {
    var idtemp = $("#hidCodigoTemporalidad").val();
    var idTarifa = $("#hidCodigoTarifa").val();
    //Nueva planificacion sin perder los cambios

    var fecha = $("#txtFechaPlanificacion").val();
    // var anio = fecha.substr(6, 4);
    var mes = fecha.substr(3, 2);

    if (mes == "")
        mes = 0;

    if (anio == 0)
        anio = fecha.substr(6, 4);
    //   alert($("#ddlAniPlaniamiento").val());
    $.ajax({
        data: { idLic: codLic, idTemp: idtemp, anio: anio, tipoPagoPadre: $("#ddlTipoPago").val(), idTarifa: idTarifa, mes: mes, idTemp2: $("#ddlTemporalidad").val() },
        type: 'POST',
        url: 'ListarPlaneamiento',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#mvPlanificacion").dialog("close");//Cerrando el pop up planificacion
                $("#gridPlaneamiento").html(dato.message);
                darFormato();
            } else if (dato.result == 0) {
                $("#gridPlaneamiento").html(dato.message);
                //alert(dato.message);
            }
        }
    });

}
function loadDataNuevaPlanificacion(anio, mes) {


    $.ajax({
        data: { idTemp: $("#ddlTemporalidad").val(), anio: anio, fecha: $("#txtFechaPlanificacion").val(), mes: mes, tipoPagoPadre: $("#ddlTipoPago").val() },
        type: 'POST',
        url: 'ListarNuevaPlanificacion',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $("#gridPlaneamiento").html(dato.message);
            darFormato();
        }
    });
    $("#mvPlanificacion").dialog("close");
};



var reloadEvento = function (idSel) {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    $("#hidResponsable").val(idSel);
    ObtieneNombreEntidad(idSel, "lblResponsable");
};

var da = function (idSel) {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    QuitarBordeObligatorio(K_DIV_VALIDAR.DIV_CAB, K_DIV_MESSAGE.DIV_LICENCIA);
    $.ajax({
        url: '../Seguridad/EnableInsLicXEstableSel',
        data: { idEst: idSel },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                if (dato.valor == "1") {
                    $("#hidEstablecimiento").val(idSel);
                    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
                    ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
                } else {

                    $("#hidEstablecimiento").val(0);
                    $("#lblEstablecimiento").html("Seleccione");
                    $("#lblResponsable").html("Seleccione");
                    $("#hidResponsable").val(0);

                    msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                }
            } else if (dato.result == 0) {
                console.log("ENTRO QUI dato.result == 0");
                msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
            }
        }
    });

};


var reloadEventoMod = function (idModSel) {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidadB({
        idModalidad: idModSel,
        fncCallBack: function (result) {
            if (!(result.error)) {
                $("#lblModalidad").html(result.modalidadText);
                $("#hidWorkflow").val(result.idWorkFlow);
                obtenerEstadoInicialWF(result.idWorkFlow, function (iniState) {
                    loadEstadoWF({
                        control: 'ddlEstadoLicencia',
                        valSel: iniState,
                        idFiltro: result.idWorkFlow,
                        addItemAll: false
                    });
                });
                //Habilitar el Boton btnBuscarEstablecimientoMult
                if ($("#ddlTipoLicencia").val() == 3) {
                    document.getElementById('btnBuscarEstablecimientoMult').style.display = 'inline';
                    $("#lblEstablecimiento").text('SELECCIONE..');
                }
            } else {
                alert(result.message);
            }
        }
    });
    InitTemporalidadTarifa();
    loadTemporalidad("ddlTemporalidad", idModSel, 0);

    // alert($("#hidWorkflow").val());




};
var reloadEventoEntidad = function (idSel) {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    var entidad = {
        idEntidad: 0,
        idLic: $(K_HID_KEYS.LICENCIA).val(),
        idBps: idSel
    };
    $.ajax({
        url: '../Entidad/Insertar',
        data: entidad,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                alert(dato.message);
                loadDataEntidades($(K_HID_KEYS.LICENCIA).val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

};



/*insertar datos licencia*/
function insertar() {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_LICENCIA, K_DIV_VALIDAR.DIV_CAB)) {
        if ($(K_HID_KEYS.WORKFLOW).val() == "") {
            msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, "No se puede registrar la licencia. La modalidad seleccionada no tiene asociado un WorkFlow.");
        } else {
            var licenciaCab = {
                codLicencia: $("#txtCodigo").val() == "" ? 0 : $("#txtCodigo").val(),
                tipoLicencia: $("#ddlTipoLicencia").val(),
                codEstado: $("#ddlEstadoLicencia").val(),
                codMoneda: $("#ddlMoneda").val(),
                nombreLicencia: $("#txtNombre").val(),
                descLicencia: $("#txtDescripcion").val(),
                codModalidad: $("#hidModalidad").val(),
                codEstablecimiento: $("#hidEstablecimiento").val(),
                codUsuDerecho: $("#hidResponsable").val(),
                codLicenciaPadre: $("#txtCodigoLicMultiple").val(),
                FormaEntregaFact: $("#ddlEntregaFactura").val(),
                IndUpdCaracteristicas: $("#chkIndUpdCaracteristicas").prop("checked"),
                IndDscVisible: $("#chkIndDscVisible").prop("checked"),
                IndReqReporte: $("#chkIndReqReporte").prop("checked"),
                codTarifa: $("#hidCodigoTarifa").val(),
                codTemporalidad: $("#ddlTemporalidad option:selected").val(),
                codTipoPago: $("#ddlTipoPago").val()
            };

            $.ajax({

                url: '../Licencia/Insertar',
                type: 'POST',
                async: false,
                data: licenciaCab,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        $("#txtCodigo").val(dato.valor);
                        $(K_HID_KEYS.LICENCIA).val(dato.valor);
                        $("#ddlEstadoLicencia").attr("disabled", "disabled");
                        $("#ddlTipoLicencia").attr("disabled", "disabled");
                        listarTabs(licenciaCab.codEstado, $(K_HID_KEYS.WORKFLOW).val());
                        $("#divTituloLic").html(K_TITULO.EDITAR);
                        msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                        DivisionObligatorioInsert(dato.valor);
                        ActualizaLicenciaPermisoFacturacion(dato.valor);
                        ObtenerLicencia(dato.valor);

                        //if (licenciaCab.tipoLicencia == 2) {
                        //    ObtenerLicencia(dato.valor);
                        //    alert("Mostrar El utlimo Registrado");
                        //} else {

                        //    alert("Mostrar La Licencia Padre");
                        //    ObtenerLicencia(dato.valor);
                        //}
                        /*cargar dropdownlist*/
                        //loadFormatoFacturacion('ddlFormaEntrega', licenciaCab.FormaEntregaFact);
                        //loadGrupoFacturacion('ddlgrupoFact',0,  $(K_HID_KEYS.SOCIO).val(),0);
                        //loadEnvioFacturacion('ddlEnvioFactura', 0);
                    } else if (dato.result == 2) {
                        msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                    }
                }
            });
        }
    }
}




function listarTabs(idEstado, idWorkFlow) {
    // alert(idWorkFlow);

    for (var i = 1; i < 14; i++) { $('#tabs').disableTab(i); }


    $.ajax({
        url: '../Licencia/ListarTabs',
        type: 'POST',
        data: { codigoEstado: idEstado, codigoWF: idWorkFlow },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var tabs = dato.data.Data;
                //borrar esto v
                //Aqui Cambiar la Condicion Poner (tabs!="" && valida)
                var valida = ValidarLicenciaMultipleHija();
                //borrar esto ^
                //if (tabs != "" && valida==0) {
                if (tabs != ""/* && valida == 0*/) {

                    //DESCOMENTAR LUEGO DE COMPLETAR LA PROGRAMACION DE TODOS LOS TABS
                    $.each(tabs, function (index, tab) {
                        /* activacion de tabs segun estado */
                        $('#tabs').enableTab(tab);
                        if (tab == 1) {
                            ListarProcesoHtml("divProceso", idEstado, idWorkFlow, $(K_HID_KEYS.LICENCIA).val());
                            loadTipoDoc("ddlTipoDocumentoTrace", 0);
                        }
                        if (tab == 2) {
                            loadPeriodoPlanFactura("ddlPerPlanFacCarac", $(K_HID_KEYS.LICENCIA).val(), 0);
                            $('#ddlFechasSearchCarac').append($("<option />", { value: -1, text: "SELECCIONE PERIODO" }));
                        }
                        if (tab == 3) {
                            loadPeriodoPlanFactura("ddlPerPlanFacDesc", $(K_HID_KEYS.LICENCIA).val(), 0);
                            //loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), $('#ddlPerPlanFacDesc  option:selected').val());
                        }
                        if (tab == 4) {
                            //loadDataAutorizacion($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 5) {
                            //loadDataLocalidadAforo($(K_HID_KEYS.LICENCIA).val());
                            //loadDataLocalidades($(K_HID_KEYS.LICENCIA).val());
                            //loadDataMatrizLocalidad($(K_HID_KEYS.LICENCIA).val()); //refactorizar
                        }
                        if (tab == 6) {
                            //loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), 0);
                        }
                        if (tab == 7) {
                            loadDataComision($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 8) {
                            // loadDataReporte($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 9) {
                            //loadDataEntidades($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 10) {
                            //loadDataGarantia($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 11) {
                            //loadDataObservacion($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 12) {
                            //loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 13) {
                            //loadDataParametro($(K_HID_KEYS.LICENCIA).val());
                        }
                        if (tab == 14) {
                            loadDataAuditoria($(K_HID_KEYS.LICENCIA).val());
                        }
                    });
                    //SI es licencia Padre lista establecimientos
                    if ($("#txtCodigoLicMultiple").val() != "" && $("#ddlTipoLicencia").val() == 3) {
                        var dato = $("#hidResponsable").val();
                        //habilitando txtCodigoLicMultiple
                        $("#txtCodigoLicMultiple").show();
                        BuscarEstablecimientoSocioEmpresarial(dato);//comun.buscador.establecimientoSocioEmpresarial.js
                    }
                }
                //borrar esto v
                //else {
                //    $("#chkMostarEstab").prop("checked", false);
                //    $("#chkMostarEstab").prop("disabled",true);
                //    $('#tabs').enableTab(1);

                $('#tabs').enableTab(2);

                var index = $('#tabs a[href="#tabs-15"]').parent().index();
                $("#tabs").tabs("option", "active", index);
                //    //lista planeamiento en TAB DESCUENTO
                //    loadPeriodoPlanFactura("ddlPerPlanFacDesc", $(K_HID_KEYS.LICENCIA).val(), 0);

                //    $('#tabs').enableTab(5);
                //    loadPeriodoPlanFactura("ddlPerPlanFacCarac", $(K_HID_KEYS.LICENCIA).val(), 0);
                //    $('#ddlFechasSearchCarac').append($("<option />", { value: -1, text: "SELECCIONE PERIODO" }));
                //}
                //borrar esto ^

            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
            }
        }
    });
}


function loadDataEntidades(id) { }
function InicializarTab() {
    $('#tabs').tabs("disable");
}
function ObtenerLicencia(codigo) {

    $.ajax({
        url: '../Licencia/ObtenerLicenciaXCodigo',
        data: { idLicencia: codigo },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result <= 3) {
                var licencia = dato.data.Data;
                if (licencia.tipoLicencia == K_TIPO_LIC.SIMNPLE) {
                    $("#tdlblestselec").hide(); //Label 
                }
                else if (licencia.tipoLicencia == K_TIPO_LIC.MULTIPLE) {
                    $("#tdlblestselec").show();
                    $("#btnAgregarCadena").hide();
                }

                $("#txtCodigo").val(licencia.codLicencia);
                $("#txtCodigoLicMultiple").val(licencia.codMultiple == 0 ? "" : licencia.codMultiple);
                $("#hidLicMaster").val(licencia.codMultiple);
                if (licencia.codMultiple > 0) { $("#txtCodigoLicMultiple").removeAttr("disabled"); }
                loadTipoLicencia('ddlTipoLicencia', licencia.tipoLicencia);

                loadEstadoWF({
                    control: 'ddlEstadoLicencia',
                    valSel: licencia.codEstado,
                    idFiltro: licencia.codWorkFlow,
                    addItemAll: false
                });
                loadMonedaRecaudacion('ddlMoneda', licencia.codMoneda);
                //loadFormatoFacturacion('ddlFormaEntrega', licencia.FormaEntregaFact);
                //loadGrupoFacturacion('ddlgrupoFact', licencia.GrupoFacturacion, licencia.codUsuDerecho, licencia.GrupoFacturacion);
                ObtenerGrupoFacturacion(licencia.GrupoFacturacion);
                $("#hidGrupoFacturacion").val(licencia.GrupoFacturacion);
                //loadEnvioFacturacion('ddlEnvioFactura', licencia.TipoEnvioFact);
                loadAnioPlaneamiento('ddlAniPlaniamiento', 0, '--SELECCIONE--', codigo);
                $("#txtNombre").val(licencia.nombreLicencia);
                $("#txtDescripcion").val(licencia.descLicencia);
                LoadTipoPago('ddlTipoPago', licencia.codTipoPago);
                if (licencia.IndUpdCaracteristicas == "1") { $("#chkIndDetaFact").attr('checked', true); } else { $("#chkIndDetaFact").attr('checked', false); }
                if (licencia.IndUpdPlanilla == "1") { $("#chkPlanilla").attr('checked', true); } else { $("#chkPlanilla").attr('checked', false); }
                if (licencia.IndDscVisible == "1") { $("#chkIndDscVisible").attr('checked', true); }
                if (licencia.IndReqReporte == "1") { $("#chkIndDocReq").attr('checked', true); }
                if (licencia.IndEmiMensual == "1") { $("#chkLicEmiMensual").attr('checked', true); } else { $("#chkLicEmiMensual").attr('checked', false); }
                $("#hidModalidad").val(licencia.codModalidad);
                $("#hidEstablecimiento").val(licencia.codEstablecimiento);
                $("#hidResponsable").val(licencia.codUsuDerecho);
                ObtieneNombreEntidad(licencia.codUsuDerecho, "lblResponsable");
                ObtenerNombreEstablecimiento(licencia.codEstablecimiento, "lblEstablecimiento");
                obtenerNombreModalidad(licencia.codModalidad, "lblModalidad", "hidWorkflow");
                loadTemporalidad("ddlTemporalidad", licencia.codModalidad, licencia.codTemporalidad);
                $("#hidCodigoTemporalidad").val(licencia.codTemporalidad);
                $("#hidCodigoTarifa").val(licencia.codTarifa);
                //loadComboTarifa(licencia.codModalidad, licencia.codTemporalidad, $("#hidCodigoTarifa").val(), "hidCodigoTarifa");
                var respModalidadPermanente = ValidaLicenciaLocalPermanente(codigo);

                if (respModalidadPermanente != 1)
                    $("#lblTarifaDesc").attr("disabled", "disabled");

                LoadComboObtenerNombreTarifa(licencia.codModalidad, $("#hidCodigoTemporalidad").val(), "lblTarifaDesc", licencia.codTarifa);

                if (licencia.hasPlanning) {
                    $("#ddlTemporalidad").attr("disabled", "disabled");
                }
                $("#ddlEstadoLicencia").attr("disabled", "disabled");
                $("#ddlTipoLicencia").attr("disabled", "disabled");
                $("#ddlTemporalidad").attr("disabled", "disabled");//¿licencia.hasPlanning?
                $("#btnRefrescar").removeAttr("disabled");
                $("#btnTimeLine").removeAttr("disabled");
                $("#btnVisualizaPopUp").css("display", "none");
                $("#btnBuscarMod").css("display", "none");

                $("#btnBuscarResponsable").css("display", "none");
                $("#btnBuscarEstablecimiento").css("display", "none");

                $("#lblModalidad").off("click");
                $("#lblModalidad").prop({ "alt": "Modalidad de Licencia", "title": "Modalidad de Licencia" });
                $(K_HID_KEYS.MODALIDAD_USO).val(licencia.codModUso);
                $("#hidModalidadUso").val(licencia.codModUso);
                $(K_HID_KEYS.TARIFA).val(licencia.codTarifa);
                // David : De donde Viene el Codigo De Workflow

                listarTabs(licencia.codEstado, licencia.codWorkFlow);

                getTimeLine(licencia.codLicencia);

                //Validando Licencia Multiple Hija
                ValidarLicenciaMultipleHija();
                ValidaUsuarioMoroso(licencia.codUsuDerecho);
                ValidaUsuarioCorrTelef(licencia.codUsuDerecho, licencia.codLicencia);

                //Mostrar Boton de Listar  
                if ($("#txtCodigoLicMultiple").prop('disabled', true) && $("#txtCodigoLicMultiple").val() == "") {
                    //alert("entro=" + $("#txtCodigoLicMultiple").val());
                    document.getElementById('btnBuscarEstablecimiento').style.display = 'inline';
                    document.getElementById('btnBuscarEstablecimientoMult').style.display = 'none';
                } else {
                    document.getElementById('btnBuscarEstablecimiento').style.display = 'none';
                    document.getElementById('btnBuscarEstablecimientoMult').style.display = 'inline';
                }
                $("#btnBuscarEstablecimiento").css("display", "none"); // ocultando boton de busqueda 
                //alert("sin limpiar js")

                if (dato.result == 0 || dato.result == 3) {
                    //$("#btnGrabar").attr("disabled", "disabled");
                    //$("#lblMensajeDialog").html(dato.message);
                    //$("#mvAdvertencia").dialog("open");
                    msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                    $("#btnGrabar").hide();
                    $("#btnAgregarCadena").hide();
                    $("#btnHistorico").hide();
                    $("#btnGrabarFacturacion").hide();
                    $("#btnGrabarCaractDsc").hide();
                    $("#btnGrabarCaract").hide();
                    $("#cssTabBtnAgregar").hide();
                }

            } else if (dato.result == 4) {
                $("#lblMensajeDialog").html("Esta licencia pertenece a otra oficina. No puede acceder.");
                $("#mvAdvertencia").dialog("open");
            }
        }
    });
}
function irProceso(idModulo) {
    $.ajax({
        data: { idModulo: idModulo },
        url: '../General/EjecutarProceso',
        type: 'POST',
        success: function (response) {
            validarRedirect(dato); /*add sysseg*/
            var dato = response;
            if (dato.result == 1) {
                alert(dato.message);
            }
        }
    });
}

function bloquearLista(control, indiceddl) {

    var estado = $("#" + control.id).prop("checked");
    if (estado) {
        $("#lstBloqueo_" + indiceddl).removeAttr('disabled');
    } else {
        $("#lstBloqueo_" + indiceddl).attr('disabled', 'disabled');
    }
}

function UpdPlanificacion(id) {
    var plan = {
        id: $("#hidCodigoLP_" + id).val(),
        idBlock: $("#lstBloqueo_" + id).val(),
        idTipoPago: $("#ddlTipoPago_" + id).val(),
        fecha: $("#txtFechaBloqueo_" + id).val()
    };
    $.ajax({
        data: plan,
        url: 'UpdPlanificacion',
        type: 'POST',
        success: function (respon) {

        }
    });
}

function darFormato() {
    var i = $("#tbPlaniamiento tbody tr").length;
    for (var x = 1; x <= i; x++) {
        var control = "lstBloqueo_" + x;
        var control1 = "ddlTipoPago_" + x;
        var valSel = "hidBloqueo_" + x;
        var valSel1 = $("#" + control1).val();//"hidTipoPago_" + x;
        var fecha = "txtFechaBloqueo_" + x;
        if (valSel1 == null) valSel1 = $("#ddlTipoPago").val();
        $("#" + fecha).kendoDatePicker({ format: "dd/MM/yyyy" });

        // loadBloqueo(control, valSel);
        // LoadTipoPago(control1, valSel1);
    }
}



function limpiarTarifa() {
    $("#hidCodigoTarifa").val(0);
    $("#lblTarifaDesc").html("Seleccione Temporalidad");
}

function obtenerTarifa(idTemp) {
    var idModSel = $("#hidModalidad").val();
    $("#hidCodigoTemporalidad").val(idTemp);
    //obtenerNombreTarifaLabels(idModSel, idTemp, "lblTarifaDesc", "hidCodigoTarifa");
    loadComboTarifa(idModSel, idTemp, 0, "hidCodigoTarifa");
}
function InitTemporalidadTarifa() {
    $("#hidCodigoTemporalidad").val(0);
    $("#hidCodigoTarifa").val(0);
    $("#lblTarifaDesc").html("Seleccione Temporalidad");
    $("#ddlTemporalidad").append($("<option />", { value: 0, text: 'SELECCIONE MODALIDAD' }));

}


/* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX */
/*Inicio Acciones del tab LOCALIDAD*/
function limpiarLocalidad() {
    loadLocalidad('ddlLocalidad', 'Seleccione');
    loadTipoAforo('ddlAforo', 'Seleccione');
    $("#txtFundacionLoc").val("");
    $("#txtColorLoc").val("");
    $("#txtTicketLoc").val("");
    $("#txtPrcVentaLoc").val("");
    $("#txtImporteBLoc").val("");
    $("#txtImpuestoLoc").val("");
    $("#txtImporteNLoc").val("");

    $("#hidAccionMvLoc").val("0");
    $("#hidEdicionLoc").val("0");
    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_LOCALIDAD, K_DIV_POPUP.LOCALIDAD);
}
///*Fin Acciones del tab LOCALIDAD*/

////*** LOCALIDADES *************************************************************************
//var MV_DIV_LOCALIDADES = 'divLocalidadesMatriz';
//var DIV_MSG_AVISO_LOCALIDADES = "avisoLocalidades";
//var MSG_AVISO_ELI_AFORO = '¿Desea eliminar el aforo?';
//var MSG_AVISO_LIQUIDACION_AFORO = 'Complete en seleccionar el tipo de liquidación.';
//var MSG_AVISO_VALIDAR_NUEVO = 'Complete los datos del anterior registro.';

////*** Aforo
//function loadDataLocalidadAforo(codLic) {
//    loadDataGridTmpLocalidadAforo('ListarLocalidadAforo', "#gridAforo", codLic);
//}

//function loadDataGridTmpLocalidadAforo(Controller, idGrilla, codLic) {
//    $.ajax({
//        data: { codigoLic: codLic },
//        type: 'POST', url: Controller,
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            $(idGrilla).html(dato.message);
//            cargarTipoAforo();  // refactorizar
//            addValSoloNumeroAforo();
//        }
//    });
//}

//function cambiosDatosAforo(id) {
//    ActualizarLocalidadAforo(id);
//}

////Tipo Aforo - Dropdownlista
//function changeCboTA(id) {
//    var idOriCbo = $('#lblAforo' + id).html();
//    var idValSelCbo = $('#cboTA' + id).val();
//    var estadoExiste = validarExistTipoAforo(idValSelCbo, id);
//    if (estadoExiste) {
//        $('#cboTA' + id).val(idOriCbo);
//        alert('El aforo seleccionado ya existe.');
//    }
//    else {
//        $('#lblAforo' + id).html(idValSelCbo);
//        var aforo = $('#cboTA' + id + ' option:selected').text();
//        ActualizarLocalidadAforo(id);
//        cambiosDatosAforoMatriz(id, aforo);
//    }
//}

//function changeLiquidar(id) {
//    ActualizarLocalidadAforo(id);
//}

//function validarExistTipoAforo(idValSelCbo, idCbo) {
//    var estado = false;
//    $('#tblLocalidadAforo tr').each(function () {
//        var Id = $(this).find(".Id").html();
//        if (Id != idCbo) {
//            if (!isNaN(Id)) {
//                if (Id != null) {
//                    var cboTa = $('#cboTA' + Id).val();
//                    if (cboTa == idValSelCbo)
//                        estado = true;
//                }
//            }
//        }
//    });
//    return estado;
//}

//function cargarTipoAforo() {
//    $('#tblLocalidadAforo tr').each(function () {
//        var Id = $(this).find(".Id").html();
//        if (!isNaN(Id)) {
//            if (Id != null) {
//                var AforoId = $('#lblAforo' + Id).html();
//                loadTipoAforo('cboTA' + Id, '-- SELECCIONE --', AforoId);
//            }
//        }
//    });
//}

//function addValSoloNumeroAforo() {
//    $('#tblLocalidadAforo tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (!isNaN(id)) {
//            $('#txtCapTickets' + id).soloNumEnteros();
//            $('#txtCapTicketsV' + id).soloNumEnteros();
//        }
//    });
//}

//function validarRadioButtonLiquidar() {
//    var result = false;
//    var cont = 0;
//    var acu = 0;

//    $('#tblLocalidadAforo tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (id != null) {
//            cont += 1;
//            if ($('#chkPLiquidar' + id).is(':checked'))
//                acu += 1;
//            if ($('#chkLiquidar' + id).is(':checked'))
//                acu += 1;
//        }
//    });

//    if (cont == acu)
//        result = true;
//    else
//        alert(MSG_AVISO_LIQUIDACION_AFORO);

//    return result;
//}

//function addLocalidadAforo() {
//    if (validarNuevoAforo()) {
//        var idLic = $(K_HID_KEYS.LICENCIA).val()
//        var aforo = {
//            LIC_ID: idLic,
//            CAP_ID: 0,
//            CAP_IPRE: false,
//            CAP_ILIQ: false,
//            CAP_TICKETS: 0,
//            CAP_TICKETSV: 0
//        };

//        $.ajax({
//            url: 'AddLocalidadAforo',
//            data: aforo,
//            type: 'POST',
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato); /*add sysseg*/
//                if (dato.result == 1) {
//                    loadDataLocalidadAforo(idLic);
//                    eliminarMatriz(idLic);
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//    }
//}

//function ActualizarLocalidadAforo(id) {
//    $('#tblLocalidadAforo tr').each(function () {
//        var idAct = $(this).find(".Id").html();
//        if (!isNaN(idAct)) {
//            if (idAct == id) {//es igual
//                var preLiq = '';
//                var Liq = '';

//                var TA = $('#cboTA' + idAct).val();
//                if ($('#chkPLiquidar' + idAct).is(":checked"))
//                    preLiq = true
//                else
//                    preLiq = false;

//                if ($('#chkLiquidar' + idAct).is(":checked"))
//                    Liq = true;
//                else
//                    Liq = false;
//                var capTickets = $('#txtCapTickets' + id).val();
//                var capTicketsV = $('#txtCapTicketsV' + id).val();

//                var idLic = $(K_HID_KEYS.LICENCIA).val();
//                var aforo = {
//                    CAP_LIC_ID: id,
//                    LIC_ID: idLic,
//                    CAP_ID: TA,
//                    CAP_IPRE: preLiq,
//                    CAP_ILIQ: Liq,
//                    CAP_TICKETS: capTickets,
//                    CAP_TICKETSV: capTicketsV
//                };

//                $.ajax({
//                    url: 'ActualizarLocalidadAforo',
//                    data: aforo,
//                    type: 'POST',
//                    success: function (response) {
//                        var dato = response;
//                        validarRedirect(dato); /*add sysseg*/
//                        if (dato.result == 1) {
//                            loadDataLocalidadAforo(idLic);
//                        } else if (dato.result == 0) {
//                            alert(dato.message);
//                        }
//                    }
//                });

//            }
//        }
//    });
//}

//function eliminarAforo(id) {
//    Confirmar(MSG_AVISO_ELI_AFORO,
//               function () {
//                   delAforo(id);
//               },
//               function () {
//               },
//               'Confirmar'
//           )
//}

//function delAforo(id) {
//    $.ajax({
//        url: '../Licencia/EliminarLocalidadAforo',
//        data: { id: id },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            if (dato.result == 1) {
//                var idLic = $(K_HID_KEYS.LICENCIA).val();
//                loadDataLocalidadAforo(idLic);
//                eliminarMatriz(idLic);
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

//jQuery.fn.soloNumEnteros = function () {

//    return this.each(function () {
//        $(this).keydown(function (e) {
//            var key = e.which || e.keyCode;

//            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
//                // numbers   
//                key >= 48 && key <= 57 ||
//                // Numeric keypad
//                key >= 96 && key <= 105 ||
//                // comma, period and minus, . on keypad
//                //key == 190 || key == 188 || key == 109 || key == 110 ||
//                // Backspace and Tab and Enter
//               key == 8 || key == 9 || key == 13 ||
//                // Home and End
//               key == 35 || key == 36 ||
//                // left and right arrows
//               key == 37 || key == 39 ||
//                // Del and Ins
//               key == 46 || key == 45)
//                return true;

//            return false;
//        });
//    });
//}

//function validarNuevoAforo() {
//    var result = false;
//    var cont = 0;
//    $('#tblLocalidadAforo tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (id != null) {
//            var cod = $('#lblAforo' + id).html();
//            if (cod == 0) {
//                cont += 1;
//            }
//        }
//    });

//    if (cont > 0)
//        alert(MSG_AVISO_VALIDAR_NUEVO);
//    else
//        result = true;
//    return result;
//}

//// == Localidad ==
//function loadDataLocalidades(codLic) {
//    loadDataGridTmpLocalidades('ListarLocalidades', "#gridLocalidades", codLic);
//}

//function loadDataGridTmpLocalidades(Controller, idGrilla, codLic) {
//    $.ajax({
//        data: { codigoLic: codLic },
//        type: 'POST', url: Controller,
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            $(idGrilla).html(dato.message);
//            cargarTipoLocalidades(); //refactorizar
//            addValSoloNumeroLocalidad();
//        }
//    });
//}

//function cambiosDatosLocales(id) {
//    ActualizarLocalidades(id);
//    var bruto = $('#txtBruto' + id).val();
//    var impuesto = $('#txtImpuesto' + id).val();
//    var neto = $('#txtNeto' + id).val();
//    cambiosDatosValoresLocalidadMatriz(id, bruto, impuesto, neto);

//}

//function cambiosDatosValoresLocalidadMatriz(id, valBruto, valImp, valNeto) {
//    $('#tblMatriz tr').each(function () {
//        var idMatriz = $(this).find(".Id").html();
//        var idLocalidad = $(this).find(".LIC_SEC_ID").html();

//        if (idLocalidad != null && idLocalidad == id) {
//            $('#lblValBruto' + idMatriz).html(valBruto);
//            $('#lblValImp' + idMatriz).html(valImp);
//            $('#lblValNeto' + idMatriz).html(valNeto);

//            var tickets = $('#txtTickets' + idMatriz).val();
//            var bruto = tickets * valBruto;
//            var impuesto = tickets * valImp;
//            var neto = tickets * valNeto;

//            $('#lblBruto' + idMatriz).html(bruto.toFixed(2));
//            $('#lblImp' + idMatriz).html(impuesto.toFixed(2));
//            $('#lblNeto' + idMatriz).html(neto.toFixed(2));
//        }
//    });
//}

//function cargarTipoLocalidades() {
//    $('#tblLocalidades tr').each(function () {
//        var Id = $(this).find(".Id").html();
//        //alert(Id);
//        if (!isNaN(Id)) {
//            if (Id != null) {
//                var localidadId = $('#lblLocalidad' + Id).html();
//                loadLocalidad('cboLocalidad' + Id, '-- SELECCIONE --', localidadId);
//            }
//        }
//    });
//}

//function addValSoloNumeroLocalidad() {
//    $('#tblLocalidades tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (!isNaN(id)) {
//            $('#txtPreVenta' + id).on("keypress", function (e) { return solonumeros(e); });
//            $('#txtBruto' + id).on("keypress", function (e) { return solonumeros(e); });
//            $('#txtImpuesto' + id).on("keypress", function (e) { return solonumeros(e); });
//            $('#txtNeto' + id).on("keypress", function (e) { return solonumeros(e); });
//        }
//    });
//}

//function addLocalidades() {
//    if (validarNuevoLocalidad()) {
//        var idLic = $(K_HID_KEYS.LICENCIA).val()
//        var Localidad = {
//            LIC_ID: idLic,
//            SEC_ID: 0,
//            SEC_VALUE: 0,
//            SEC_GROSS: 0,
//            SEC_TAXES: 0,
//            SEC_NET: 0,
//            SEC_COLOR: ''
//        };

//        $.ajax({
//            url: 'AddLocalidades',
//            data: Localidad,
//            type: 'POST',
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato); /*add sysseg*/
//                if (dato.result == 1) {
//                    loadDataLocalidades(idLic);
//                    eliminarMatriz(idLic);
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//    }
//}

//function ActualizarLocalidades(id) {
//    $('#tblLocalidades tr').each(function () {
//        var idAct = $(this).find(".Id").html();
//        if (!isNaN(idAct)) {
//            if (idAct == id) {//es igual

//                var localidadId = $('#cboLocalidad' + idAct).val();
//                var preVta = $('#txtPreVenta' + idAct).val();
//                var preBruto = $('#txtBruto' + idAct).val();
//                var preImp = $('#txtImpuesto' + idAct).val();
//                var preNeto = $('#txtNeto' + idAct).val();
//                var color = $('#txtColor' + idAct).val();

//                var idLic = $(K_HID_KEYS.LICENCIA).val();
//                var Localidad = {
//                    LIC_SEC_ID: idAct,
//                    SEC_ID: localidadId,
//                    SEC_VALUE: preVta,
//                    SEC_GROSS: preBruto,
//                    SEC_TAXES: preImp,
//                    SEC_NET: preNeto,
//                    SEC_COLOR: color
//                };

//                $.ajax({
//                    url: 'ActualizarLocalidad',
//                    data: Localidad,
//                    type: 'POST',
//                    success: function (response) {
//                        var dato = response;
//                        validarRedirect(dato); /*add sysseg*/
//                        if (dato.result == 1) {
//                            loadDataLocalidades(idLic);
//                        } else if (dato.result == 0) {
//                            alert(dato.message);
//                        }
//                    }
//                });

//            }
//        }
//    });
//}

//function changeCboLocalidad(id) {
//    var idOriCbo = $('#lblLocalidad' + id).html();
//    var idValSelCbo = $('#cboLocalidad' + id).val();
//    var estadoExiste = validarExistLocalidad(idValSelCbo, id);

//    if (estadoExiste) {
//        $('#cboLocalidad' + id).val(idOriCbo);
//        alert('La localidad seleccionada ya existe.');
//    }
//    else {
//        var localidad = $('#cboLocalidad' + id + ' option:selected').text();
//        $('#lblLocalidad' + id).html(idValSelCbo);
//        ActualizarLocalidades(id);
//        cambiosDatosCboLocalidadMatriz(id, localidad);
//    }
//}

//function validarExistLocalidad(idvalselcbo, idcbo) {
//    var estado = false;
//    $('#tblLocalidades tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (id != idcbo) {
//            if (!isNaN(id)) {
//                if (id != null) {
//                    var cbota = $('#cboLocalidad' + id).val();
//                    if (cbota == idvalselcbo)
//                        estado = true;
//                }
//            }
//        }
//    });
//    return estado;
//}

//function eliminarLocalidad(id) {
//    $.ajax({
//        url: '../Licencia/EliminarLocalidad',
//        data: { id: id },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            if (dato.result == 1) {
//                var idLic = $(K_HID_KEYS.LICENCIA).val();
//                loadDataLocalidades(idLic);
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

//function validarNuevoLocalidad() {
//    var result = false;
//    var cont = 0;
//    $('#tblLocalidades tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (id != null) {
//            var cod = $('#lblLocalidad' + id).html();
//            if (cod == 0) {
//                cont += 1;
//            }
//        }
//    });

//    if (cont > 0)
//        alert(MSG_AVISO_VALIDAR_NUEVO);
//    else
//        result = true;
//    return result;
//}

//// xxxxxxxxxxx Matriz xxxxxxxxxxxxxx
//function loadDataMatrizLocalidad(codLic) {
//    loadDataGridTmpMatrizLocalidad('ListarMatrizLocalidad', "#gridMatriz", codLic);
//}

//function loadDataGridTmpMatrizLocalidad(Controller, idGrilla, codLic) {
//    $.ajax({
//        data: { codigoLic: codLic },
//        type: 'POST', url: Controller,
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            $(idGrilla).html(dato.message);
//            addValSoloNumeroMatriz();
//            $("#btnMatriz").css("display", "inline");
//        }
//    });
//}

//function addValSoloNumeroMatriz() {
//    $('#tblMatriz tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (!isNaN(id)) {
//            $('#txtTickets' + id).on("keypress", function (e) {
//                return solonumeros(e);
//            });
//        }
//    });
//}

//function calcularMontosMatriz(id) {
//    var tickets = $('#txtTickets' + id).val();

//    var valBruto = $('#lblValBruto' + id).html();
//    var valImp = $('#lblValImp' + id).html();
//    var valNeto = $('#lblValNeto' + id).html();

//    var bruto = tickets * valBruto;
//    var impuesto = tickets * valImp;
//    var neto = tickets * valNeto;

//    $('#lblBruto' + id).html(bruto.toFixed(2));
//    $('#lblImp' + id).html(impuesto.toFixed(2));
//    $('#lblNeto' + id).html(neto.toFixed(2));
//}


//function obtenerMatrizValor() {
//    var idLic = $(K_HID_KEYS.LICENCIA).val();
//    var ReglaValor = [];
//    var contador = 0;
//    $('#tblMatriz tr').each(function () {
//        var id = $(this).find(".Id").html();
//        if (id != null) {
//            ReglaValor[contador] = {
//                Nro: id,
//                LIC_ID: idLic,
//                CAP_LIC_ID: $(this).find(".CAP_LIC_ID").html(),
//                LIC_SEC_ID: $(this).find(".LIC_SEC_ID").html(),
//                SEC_TICKETS: $('#txtTickets' + id).val(),
//                SEC_VALUE: id,
//                SEC_GROSS: $('#lblBruto' + id).html(),
//                SEC_TAXES: $('#lblImp' + id).html(),
//                SEC_NET: $('#lblNeto' + id).html()
//            };
//            contador += 1;
//        }
//    });

//    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'POST',
//        url: '../Licencia/ObtenerMatrizValor',
//        data: ReglaValor,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            if (dato.result == 1) {
//                alert('Se registro la matriz de localidades.');
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        },
//        failure: function (response) {
//            $('#result').html(response);
//        }
//    });
//}

//function cambiosDatosAforoMatriz(id, aforo) {
//    $('#tblMatriz tr').each(function () {
//        var idAforo = $(this).find(".CAP_LIC_ID").html();

//        if (idAforo != null && idAforo == id) {
//            $(this).find(".AFORO").html(aforo);
//        }
//    });
//}

//function cambiosDatosAforoMatriz(id, aforo) {
//    $('#tblMatriz tr').each(function () {
//        var idAforo = $(this).find(".CAP_LIC_ID").html();
//        if (idAforo != null && idAforo == id) {
//            $(this).find(".AFORO").html(aforo);
//        }
//    });
//}

//function cambiosDatosCboLocalidadMatriz(id, localidad) {
//    $('#tblMatriz tr').each(function () {
//        var idLocalidad = $(this).find(".LIC_SEC_ID").html();
//        if (idLocalidad != null && idLocalidad == id) {
//            $(this).find(".LOCALIDAD").html(localidad);
//        }
//    });
//}

//function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
//    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
//        draggable: false,
//        modal: true,
//        resizable: false,
//        ewidth: 'auto',
//        title: dialogTitle,
//        minHeight: 75,
//        buttons: {
//            Si: function () {
//                if (typeof (okFunc) == 'function') {
//                    setTimeout(okFunc, 50);
//                }
//                $(this).dialog('destroy');
//            },
//            No: function () {
//                if (typeof (cancelFunc) == 'function') {
//                    setTimeout(cancelFunc, 50);
//                }
//                $(this).dialog('destroy');
//            }
//        }
//    });
//}

//function eliminarMatriz(id) {
//    $.ajax({
//        url: '../Licencia/EliminarMatrizLocalidades',
//        data: { id: id },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato); /*add sysseg*/
//            if (dato.result == 1) {
//                loadDataMatrizLocalidad(id);
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

///* XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX */

var returnPage = function () {
    //alert("Ud No tiene acceso a ver esta licencia");
    document.location.href = '../Licencia/';
};

// ******* FACTURACION MANUAL ************
var ShowPopUpFacturacionManual = function () {
    var idLic = $(K_HID_KEYS.LICENCIA).val();
    var Moneda = $("#ddlMoneda").val();
    var url = "../FacturacionManual/Index?idLic=" + idLic + "&Moneda=" + Moneda;
    //var poPup = '';
    //var width = 1000;
    //var height = 500;
    //var x = screen.width / 2 - width / 2;
    //var y = screen.height / 2 - height / 2;
    //var prop = 'location=1,menubar=false,resizable=1,scrollbars=1,width=' + width + ',height=' + height + ',left=' + x + ',top=' + y;
    //poPup = window.open(url, "Facturación Manual", prop);
    window.open(url, "_blank");
};

function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}

function verWord(url) {
    $("#mvWord").dialog("open");
    $("#ifContenedorWord").attr("src", url);
    setTimeout(function () { $("#mvWord").dialog("close"); }, 2000);
    return false;
}

/*cambios tab FACTURACION*/
function verFactura(idLic, idPlan) {
    $('#' + K_DIV_POPUP.FACTURAS).dialog("open");
    //BuscarFacturasXLic(idLic, idPlan);
    LICENCIA_DETALLE_FACTURA_X_PERIODO(idPlan);
    //alert('Realizar la búsqueda por consulta facturas');
    return false;
}

function LICENCIA_DETALLE_FACTURA_X_PERIODO(idPeriodo) {

    $.ajax({
        url: '../ConsultaDocumentoDetalle/VerDetalleFacturaLicencia',
        type: 'POST',
        data: {
            idPeriodo: idPeriodo
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#grid").html(dato.message);
                //CD_Licencia(idFactura);
                //$("#CantidadRegistros").html(dato.TotalFacturas);
            } else if (dato.result == 0) {
                $("#grid").html('');
                //$("#CantidadRegistros").html("");
                alert(dato.message);
            }
        }
    });

}

function verDetalleDocumento(id) {
    var url = '../ConsultaDocumentoDetalle/Index?id=' + id;
    window.open(url, "_blank");
}

function BuscarFacturasXLic(idLic, idPlan) {
    $("#grid").html("Obteniendo información...<img src='../Images/otros/loading.GIF' width=20 />");
    var serie = "0";
    var numFact = 0;
    var idTipoLic = 0;
    var idBps = 0;
    var idGF = 0;
    var codMoneda = 'PEN';
    var ini = '10-10-2015';
    var fin = '10-10-2015';
    var idFact = 0;
    var numLic = idLic;
    var noImpresas = 0;
    var noAnuladas = 0;
    var tipLicencia = 0;
    var idBpsAgen = 0;
    var conFecha = 0;
    var Impresas = 0;
    var Anuladas = 0;
    var tipoDoc = 0;
    var idOficina = 0;
    var valorDivision = 0;
    var estado = 0;
    var idBpsGroup = 0;

    $.ajax({
        url: '../FacturacionConsulta/ListarConsulta',
        type: 'POST',
        data: {
            numSerial: serie,
            numFact: numFact,
            idSoc: idBps,
            grupoFact: idGF,
            moneda: codMoneda,
            idLic: numLic,
            Fini: ini,
            Ffin: fin,
            idFact: idFact,
            impresas: Impresas,
            anuladas: Anuladas,
            licTipo: tipLicencia,
            agenteBpsId: idBpsAgen,
            conFecha: conFecha,
            tipoDoc: tipoDoc,
            idOficina: idOficina,
            valorDivision: valorDivision,
            estado: estado,
            idPlan: idPlan,
            idBpsGroup: idBpsGroup
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataFacturaConsulta(idPlan);
            } else if (dato.result == 0) {
                $("#grid").html('');
                alert(dato.message);
            }
        }
    });
}

function loadDataFacturaConsulta(estado) {
    $.ajax({
        type: 'POST',
        data: { estado: estado == undefined ? 0 : estado },
        url: '../FacturacionConsulta/ListarFactConsulta',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#grid").html(dato.message);
            } else if (dato.result == 0) {
                $("#grid").html('');
                alert(dato.message);
            }
        }
    });

}


function verDetaFactura(id) {
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

        mostrarDetalleFactura(id);
    }
    return false;
}

function verDetaLic(id, lic) {
    var cod = (id + '-' + lic);
    if ($("#expandLic" + cod).attr('src') == '../Images/botones/less.png') {
        $("#expandLic" + cod).attr('src', '../Images/botones/more.png');
        $("#expandLic" + cod).attr('title', 'ver detalle.');
        $("#expandLic" + cod).attr('alt', 'ver detalle.');
        $("#divLic" + cod).css("display", "none");

    } else {
        $("#expandLic" + cod).attr('src', '../Images/botones/less.png');
        $("#expandLic" + cod).attr('title', 'ocultar detalle.');
        $("#expandLic" + cod).attr('alt', 'ocultar detalle.');
        $("#divLic" + cod).css("display", "inline");
    }
    return false;
}

function mostrarDetalleFactura(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../FacturacionConsulta/mostrarDetalleFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#div" + idSel).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function verReporte(cod) {
    var url = "../FacturacionConsulta/ReporteManual?id=" + cod;
    var poPup = '';
    poPup = window.open(url, "Reporte Consulta", "menubar=false,resizable=false,width=750,height=550");
}

function obtenerId(id, fa) { verReporte(id); }
/*cambios tab FACTURACION*/

function RecuperaSocioEmpPopUp(NombreSoc, CodioSoc) {
    //alert("llego ?");
    $("#lblResponsable").html(NombreSoc);
    //Borrar Esto si esta Mal
    $("#hidResponsable").html(CodioSoc);
}

//Limpiar
//DAVID
function ValidarInsert() {
    //  var resp =ValidaDivisionInsert();//licenciamiento.division
    if ($("#ddlTipoLicencia").val() == K_TIPO_LIC.SIMNPLE) {
        //alert("inserta normal");
        // if (resp)
        insertar();

    } else if ($("#ddlTipoLicencia").val() == K_TIPO_LIC.MULTIPLE) {
        //   if (resp)
        insertarMultiple();

    } else {
        alert("Seleccione Tipo de Licencia");
    }

}

function insertarMultiple() {

    //alert("Insertar Licencias Multiples");

    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_LICENCIA, K_DIV_VALIDAR.DIV_CAB)) {
        if ($(K_HID_KEYS.WORKFLOW).val() == "") {
            msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, "No se puede registrar la licencia. La modalidad seleccionada no tiene asociado un WorkFlow.");
        } else {
            var licenciaCab = {
                codLicencia: $("#txtCodigo").val() == "" ? 0 : $("#txtCodigo").val(),
                tipoLicencia: $("#ddlTipoLicencia").val(),
                codEstado: $("#ddlEstadoLicencia").val(),
                codMoneda: $("#ddlMoneda").val(),
                nombreLicencia: $("#txtNombre").val(),
                descLicencia: $("#txtDescripcion").val(),
                codModalidad: $("#hidModalidad").val(),
                //Al insertar Debe de tener Cod Establecimiento 0 si es padre 
                codEstablecimiento: 11387,
                codUsuDerecho: $("#hidResponsable").val(),
                codLicenciaPadre: $("#txtCodigoLicMultiple").val(),
                FormaEntregaFact: $("#ddlEntregaFactura").val(),
                IndUpdCaracteristicas: $("#chkIndUpdCaracteristicas").prop("checked"),
                IndDscVisible: $("#chkIndDscVisible").prop("checked"),
                IndReqReporte: $("#chkIndReqReporte").prop("checked"),
                codTarifa: $("#hidCodigoTarifa").val(),
                codTemporalidad: $("#ddlTemporalidad option:selected").val(),
                codTipoPago: $("#ddlTipoPago").val()
            };

            $.ajax({

                url: '../Licencia/Insertar',
                type: 'POST',
                data: licenciaCab,
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        $("#txtCodigo").val(dato.valor);
                        $(K_HID_KEYS.LICENCIA).val(dato.valor);
                        $("#ddlEstadoLicencia").attr("disabled", "disabled");
                        $("#ddlTipoLicencia").attr("disabled", "disabled");
                        listarTabs(licenciaCab.codEstado, $(K_HID_KEYS.WORKFLOW).val());
                        $("#divTituloLic").html(K_TITULO.EDITAR);
                        msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                        DivisionObligatorioInsert(dato.valor);//liecnciamiento.division
                        ActualizaLicenciaPermisoFacturacion(dato.valor);
                        ObtenerLicencia(dato.valor);

                        //if (licenciaCab.tipoLicencia == 2) {
                        //    alert("Mostrar El utlimo Registrado");
                        //} else {

                        //    alert("Mostrar La Licencia Padre");
                        //    ObtenerLicencia(licenciaCab.codLicencia);
                        //}

                        /*cargar dropdownlist*/
                        //loadFormatoFacturacion('ddlFormaEntrega', licenciaCab.FormaEntregaFact);
                        //loadGrupoFacturacion('ddlgrupoFact',0,  $(K_HID_KEYS.SOCIO).val(),0);
                        //loadEnvioFacturacion('ddlEnvioFactura', 0);
                    } else if (dato.result == 2) {
                        msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);
                    }
                }
            });
        }
    }

}

//Valida Si el COdigo de Licencia Es Licencia Hija Si lo es AL iniciar El load Deshabilita Todos los controles
function ValidarLicenciaMultipleHija() {
    var CodLic = $("#txtCodigo").val();
    var ValidaGlobal = 1;
    var PermitirSeleccionarBotones = 0;
    //alert("Llego" + CodLic);
    $.ajax({
        url: '../Licencia/ValidarLicenciasMultiplesHijas',
        type: 'GET',
        data: { CodLic: CodLic },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $("#btnBuscarEstablecimientoMult").hide();
                $("#btnBuscarResponsable").hide();
                //$("#txtNombre").prop("disabled", true);
                $("#txtDescripcion").prop("disabled", true);
                $("#ddlTemporalidad").prop("disabled", true);
                $("#lblEstablecimiento").prop("disabled", true);
                $("#ddlTipoPago").prop("disabled", true);
                //$("#btnGrabar").hide();
                //$('btnBuscarEstablecimientoMult').prop('onclick', null).off('click');
                $("#ddlMoneda").prop("disabled", true);
                $("#lblResponsable").prop("disabled", true);
                $("#trBotones").prop("disabled", true);

                //Permite Visualizar Los Tabs
                PermitirSeleccionarBotones = 1;

                //Inhabilitando Botones de Tab Facturacion 
                // $("#btnGrabarFacturacion").hide();
                //$("#btnFacturaManual").hide();
                //$("#ddlFormaEntrega").prop("disabled", true);
                //$("#ddlgrupoFact").prop("disabled", true);
                // $("#ddlEnvioFactura").prop("disabled", true);
                $("#chkIndDocReq").prop("disabled", true);
                //$("#chkIndDscVisible").prop("disabled", true);
                $("#chkIndDetaFact").prop("disabled", true);
                $("#gridcontenedorListaDestino").hide();
                $("#txtCodigoLicMultiple").show();


            } else {

                PermitirSeleccionarBotones = 0;
                //alert("No es Licencia Hija");
            }
        }


    });

    return PermitirSeleccionarBotones;
}

function EditarLicHija(codigoEst) {

    var licmaster = $("#hidLicMaster").val();
    if (licmaster != "" && licmaster != null) {
        $.ajax({

            url: '../Licencia/RecuperaCodigoLicHijxCodEst',
            type: 'POST',
            data: { CodEst: codigoEst, licmaster: licmaster },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    //alert(dato.valor);
                    window.open('../Licencia/Nuevo?set=' + dato.valor, '_blank');
                } else {
                    alert("Aun No se ha Generado una Licencia para este Establecimiento");
                }
            }
        });
    } else {
        alert("Aun No se ha Generado una Licencia para este Establecimiento");
    }
    //Si funciona $("#lblestselec").css("color", "#000000");

    //window.open('../Licencia/Nuevo?set=' + codigoEst, '_blank');
}


//Autogenera COdigo de Licencia Multiple
function AutogeneraCOdigoLicMuilti() {
    var codigo = 0;
    $.ajax({
        url: "../Licencia/AutogeneraCOdigo",
        type: 'POST',
        async: false,
        success: function (response) {

            var dato = response;
            if (dato.result == 1) {

                codigo = dato.Code;
            } else
                codigo = 0;
        }
    });

    return codigo;
}

//MENSAJE DE CONFIRMAR 
function Confirmar(dialogText, OkFunction, CancelFunction, TitleDialog) {

    $('div style ="padding:10px;max-width:500px;word-wrap:break-word;"' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: true,
        ewidth: 'auto',
        title: dialogText,
        minHeight: 75,
        buttons: {
            SI: function () {
                if (typeof (OkFunction) == 'function') {

                    setTimeout(OkFunction, 50)
                }
                $(this).dialog('Destroy');
            },
            NO: function () {
                if (typeof (CancelFunction) == 'function') {
                    setTimeout(CancelFunction, 50);
                }
                $(this).dialog('Destroy');

            }

        }

    });
}


var reloadEventoEstablecimiento = function (idSel) {
    $("#hidEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
    // ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");

    //var idModalidad = $('#hidModalidad').val();
    //var idEstablecimiento = $('#hidEstablecimiento').val();
    //validarUbigeoDivision(idModalidad, idSel, 'ddlLicDivision', 0);
};

function ValidarPeriodoRepetidoLicenciasMultiples() {

    var respuesta = 1;
    if ($("#txtFechaPlanificacion").val() != "") { //|| $("#ddlAniPlaniamiento").val() > 0
        var fecha = $("#txtFechaPlanificacion").val();
        var anio = fecha.substr(6, 4);
        var mes = fecha.substr(3, 2);
        //Crear un If Para Recuperar los Codigos de La licencias Hijas
        var valida = ValidarLicenciaMultiplesPadre();
        if (valida == 1) {
            $.ajax({
                data: { codLic: $(K_HID_KEYS.LICENCIA).val(), anio: anio },
                async: false,
                type: 'POST',
                url: 'ValidarPeriodoRepetido',
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {//si es 0 es por que no tiene la planificacion "OK"
                        respuesta = 1;
                    }
                    else {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_FACT, dato.message);
                        respuesta = 0;
                    }
                    //alert(dato.message);
                }
            });
        }

    }
    //else {
    //    respuesta = 1;
    //    //msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_FACTURACION, K_MENSAJES_ERROR.TAB_POPUP_FACTURACION);
    //}
    //alert(respuesta);
    return respuesta;
}

function ValidarLicenciaMultiplesPadre() {
    var respuesta = 0;
    $.ajax({
        data: { CodLic: $(K_HID_KEYS.LICENCIA).val() },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidarLicenciaMultiplesPadre',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                respuesta = 1;
            }
        }
    });
    return respuesta;
}


function DivisionObligatorio() {
    $("#mvLicDivision").dialog("open");// Abrir PoPup
    var idModalidad = 0;
    var idEstablecimiento = 0;
    validarUbigeoDivision(idModalidad, idEstablecimiento, 'ddlLicDivision', 0);
}


function MandarHistorico() {
    //$("#mvBuscarSocio").dialog("open");
    var licid = $("#hidLicId").val();
    $.ajax({
        data: {
            LIC_ID: licid, BPS_ID: $("#hidResponsableHistorico").val() == "" ? 0 : $("#hidResponsableHistorico").val()
        },
        async: false,
        type: 'POST',
        url: '../Licencia/MandarAlHistorico',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                alert("Se envio al historico");
                //ObtenerLicencia(dato.Code);
                document.location.href = "../Licencia/Nuevo?set=" + dato.Code;
            }
        }
    });

}

function VerEstablecimientoLicencia() {

    window.open('../Establecimiento/Create?id=' + $("#hidEstablecimiento").val(), '_blank'); //redirecciona al establecimiento
}


function InsertaPlaneamientoActual() {

    if ($("#txtFechaPlanificacion").val() != "") {
        Confirmar('SE REGISTRARA EL PLANEAMIENTO DESDE LA FECHA ' + $("#txtFechaPlanificacion").val().substr(3, 2) + '/' + $("#txtFechaPlanificacion").val().substr(6, 4) + ' ESTA SEGURO(A) DE CONTINUAR ?',
        function () {
            if ($("#txtFechaPlanificacion").val() != "") {
                var fecha = $("#txtFechaPlanificacion").val();
                var anio = fecha.substr(6, 4);
                var mes = fecha.substr(3, 2);
                var dia = fecha.substr(0, 2);
                var licid = $("#hidLicId").val();
                //Crear un If Para Recuperar los Codigos de La licencias Hijas
                //var valida = ValidarLicenciaMultipleHija();
                $.ajax({
                    data: {
                        LIC_ID: licid, ANIO: anio, MES: mes, DIA: dia
                    },
                    async: false,
                    type: 'POST',
                    url: '../Licencia/InsertaPlaneamientoActual',
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato); /*add sysseg*/
                        if (dato.result == 1) {
                            alert(dato.message);

                            loadAnioPlaneamiento('ddlAniPlaniamiento', anio, '--SELECCIONE--', $(K_HID_KEYS.LICENCIA).val());
                            loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), anio);
                            loadDataGridTmpDescuentoPlantilla('../Descuento/ListarCaracteristicaDescuentos', "#gridDescuentoTarifa", $("#hidLicId").val());//Que liste automaticamente las caracteristicas de descuento
                            loadPeriodoPlanFactura("ddlPerPlanFacCarac", $(K_HID_KEYS.LICENCIA).val(), $("#ddlPerPlanFacCarac").val());
                            //ObtenerLicencia(dato.Code);
                            //document.location.href = "../Licencia/Nuevo?set=" + dato.Code;
                        } else {
                            alert(dato.message);
                        }
                    }
                });
            }
        }

        ,
        function () { },
        'Confirmar');

    } else {
        alert('INGRESE UNA FECHA ');
    }

}

function AgregarCadena() {
    if ($("#ddTipoLicencia").length) {
        $("#ddTipoLicencia").val(K_TIPO_LIC.MULTIPLE);//multiple

        var idresp = $("#hidResponsable").val(); // de la licencia

        $("#hidIdResponsable").val(idresp); // se le asigna al responsable del POP UP
        $("#btnBuscarLicencia").hide();
        loadGridLicencias(); // evento del pop up comun.buscador.Licencia
    }
}


function ValidaLicenciaLocalPermanente(licid) {
    var resp = 0;

    $.ajax({
        data: {
            LIC_ID: licid
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidaLicenciaLocalPermanente',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                resp = dato.result;

            } else {
                //alert(dato.message);
            }
        }
    });
    return resp;
}

function ValidaPeriodoLicenciaAct(LIC_PL_ID) {

    //alert("ACTUALIZANDO");

    $.ajax({
        data: {
            LIC_PL_ID: LIC_PL_ID
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidaEstadoPeriodoLicencia',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {


                ActSituacionPeriodo(LIC_PL_ID, dato.result);

            } else {
                alert(dato.message);
            }
        }
    });
}


function ActSituacionPeriodo(LIC_PL_ID, OPCION) {

    //alert("ACTUALIZANDO");

    $.ajax({
        data: {
            LIC_PL_ID: LIC_PL_ID, opcion: OPCION
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ActualizaEstadoPeriodoLicencia',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {
                loadDataPlaneamiento($(K_HID_KEYS.LICENCIA).val(), $("#ddlAniPlaniamiento").val());
            } else {
                alert(dato.message);
            }
        }
    });
}


function ActualizaLicenciaMonto(LIC_PL_ID) {

    var LIC_ID = $("#hidLicId").val();

    $.ajax({
        data: {
            LIC_PL_ID: LIC_PL_ID, LIC_ID: LIC_ID
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ActualizaMontosLicencia',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {

            } else {
                alert(dato.message);
            }
        }
    });
}

function ActivarAlfresco() {
    $.ajax({
        type: 'POST',
        url: '../Licencia/ActivarAlfresco',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.valor == 'T') {
                addDocumento();
            } else {
                addDocumentoLyrics();
            }
        }


    })
}

function LoadDocumentoAlfresco_Lyrics() {
    $.ajax({
        type: 'POST',
        url: '../Licencia/ActivarAlfresco',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.valor == 'T') {
                loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
            } else {
                loadDataDocumentoLyrics($(K_HID_KEYS.LICENCIA).val());
            }
        }


    })
}

function addDocumento() {
    var TipoDocumento_Alfresco = $("#ddlTipoDocumento option:selected").attr('name');
    var TipoDocumento = $("#ddlTipoDocumento option:selected").val();
    var CodigoLic = $(K_HID_KEYS.LICENCIA).val();
    var Artist_ID = $("#ddlArtistaAlfresco").val() == null ? 0 : $("#ddlArtistaAlfresco").val();

    var IdAdd = 0;
    if ($("#hidAccionMvDoc").val() === "1") {
        IdAdd = $("#hidEdicionDoc").val();
    }

    if (IdAdd > 0) {
        $("#file_upload").removeClass("requerido");
    } else {
        $("#file_upload").addClass("requerido");
    }
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, K_DIV_POPUP.DOCUMENTO)) {
        //var documento = {
        //    //codLic: $(K_HID_KEYS.LICENCIA).val(),
        //    archivoTXT: archivoTXT,
        //    CodigoCarpetaAlfresco: TipoDocumento_Alfresco,
        //    //TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
        //    //FechaRecepcion: $("#txtFecha").val(),
        //    //Archivo: $("#hidNombreFile").val()
        //};
        if (TipoDocumento == 2 && Artist_ID != 0) {
            InitUploadTabDocAlfresco("file_upload", TipoDocumento_Alfresco, TipoDocumento, CodigoLic, Artist_ID);
            loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
            $("#mvDocumento").dialog("close");
        } else if (TipoDocumento != 2) {
            InitUploadTabDocAlfresco("file_upload", TipoDocumento_Alfresco, TipoDocumento, CodigoLic, Artist_ID);
            loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
            $("#mvDocumento").dialog("close");

        } else {
            alert('POR FAVOR INGRESAR UN ARTISTA.');

        }

        //alert(dato.message);
        //loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
        //$("#mvDocumento").dialog("close");  
    }
}


function ActualizaLicenciaPermisoFacturacion(LIC_ID) {

    //alert("ACTUALIZANDO");

    $.ajax({
        data: {
            LIC_ID: LIC_ID
        },
        async: false,
        type: 'POST',
        url: '../AdministracionControlLicencias/ActualizarLicenciaLocal',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {

            } else {
                alert(dato.message);
            }
        }
    });
}

function ValidaUsuarioMoroso(BPS_ID) {

    $.ajax({
        data: {
            BPS_ID: BPS_ID
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidaUsuarioMoroso',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result > 0) {
                alert(dato.message);
                $("#btnGrabar").hide();
                msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);

            } else if (dato.result < 0) {
                alert(dato.message);
            } else {
                $("#btnGrabar").show();
                $("#" + K_DIV_MESSAGE.DIV_LICENCIA).html('');
            }
        }
    });

}


function ValidaUsuarioCorrTelef(BPS_ID, LIC_ID) {

    $.ajax({
        data: {
            BPS_ID: BPS_ID, LIC_ID: LIC_ID
        },
        async: false,
        type: 'POST',
        url: '../Licencia/ValidaUsuarioCorreoTelef',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 0) {
                alert(dato.message);
                $("#btnGrabar").hide();
                $("#btnGrabarFacturacion").hide();
                $("#btnGrabarCaract").hide();
                msgErrorB(K_DIV_MESSAGE.DIV_LICENCIA, dato.message);

            }
        }
    });

}

function ValidarFacturacion(id_lic) {

    var retorno = 0;
    $.ajax({
        url: '../Licencia/ValidarFacturacion',
        type: 'POST',
        dataType: 'JSON',
        data: { LIC_ID: id_lic },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                retorno = dato.result;
                //alert(dato.message);
            } else {
                retorno = dato.result;
            }
        }
    });
    return retorno;
}