/************************** INICIO CONSTANTES****************************************/
var LimiteFacturaSelect = 300;
/************************** INICIO CARGA********************************************/
$(function () {

    //POP UP BUSCADOR DE SOCIOS
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarAgente({ container: "ContenedormvBuscarAgenteComercial", idButtonToSearch: "btnBuscarAGE", idDivMV: "mvBuscarAgente", event: "reloadEventoAgente", idLabelToSearch: "lbAgente" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRU", idDivMV: "MvBuscarGrupoFacturacion", event: "reloadEventoGrupoFact", idLabelToSearch: "lbGrupo" });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });

    $("#mvLicDivision").dialog({
        closeOnEscape: false,
        autoOpen: false,
        width: K_WIDTH_DIV,
        height: K_HEIGHT_DIV,
        buttons: {
            "Agregar": function () { GetQueryStringParams("set") == undefined ? InsertaDivisionObligatorio() : addDivisionLic(); },
            "Cancelar": function () { GetQueryStringParams("set") == undefined ? alert("Seleccione una Division") : $("#mvLicDivision").dialog("close"); }
        }, modal: true
    });

    $("#mvLicDivisionAgenteObligatorio").dialog({
        closeOnEscape: false,
        autoOpen: false,
        width: K_WIDTH_DIV_OBLI,
        height: K_HEIGHT_DIV_OBLI,
        buttons: {
            "Aceptar": function () { ValidaAgentesAgregados(); } ,
            "Regresar": function () { $("#mvLicDivision").dialog("open"); $("#mvLicDivisionAgenteObligatorio").dialog("close"); }
        }, modal: true
    });


    //BOTONES
    $("#btnBuscar").on("click", function () {
        ConsultarLicenciasTrasladar();
    });

    $("#btnLimpiar").on("click", function () {
        LimpiarTodo();
    });

    $("#btnSeleccionar").on("click", function () {
        SeleccionarLicencia();
    });

    $("#btnElegirDivision").on("click", function () {
        //agregando el POP UP 
        $("#mvLicDivisionAgenteObligatorio").dialog("open");
        //Confirmar('Se trasladaran  N° LICENCIAS A LA DIVISION SELECCIONADA , (ESTA SEGURO(A) DE CONTINUAR ?',
        //function () { Actualizar(); },
        //function () { },
        //'Confirmar'
    });
    loadDivisionTipo('ddTipoDivision', '0');
    loadTipoGrupo('dllGruModalidad', '0');
    $("#ddTipoDivision").on("change", function () {
        var tipo = $("#ddTipoDivision").val();
        loadDivisionXTipo('ddDivision', tipo, 'TODOS');
    });


    $("#btnActualizar").on("click", function (){

        var cant = CuantasLicenciasSeleccionadas();
        var cant2 = CuantosAgentesSeleccionados();
        if (cant > 0 && cant2>0) {
            Confirmar('Se trasladaran ' + cant + ' LICENCIAS A LA DIVISION SELECCIONADA , (ESTA SEGURO(A) DE CONTINUAR ?',
            function () { Actualizar(); },
            function () { },
            'Confirmar')
        } else {
            if (cant == 0) {
                alert("Seleccione Al menos una Licencia para trasladar");
            }
            if (cant2 == 0) {
                alert("Seleccione Al menos una Agente realizar el traslado");
            }
            
        }
    });


    DivisionObligatorio();
   
    $('#txtFecCreaInicialTras').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinalTras').kendoDatePicker({ format: "dd/MM/yyyy" });

        
    //Limpiar Controles
    LimpiarTodo();
});

//RECUPERA EL NOMBRE DEL SOCIO
var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            }
        }

    });

};

//Editar
function EditarLicHija(est_id) {
}

//Lista los establecimientos por Socio
function ConsultarLicenciasTrasladar() {
    var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();
    var grupo = $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val();
    var ofi = $("#hidOficina").val() == "" ? 0 : $("#hidOficina").val();
    var licencia = $("#txtCodLic").val() == "" ? 0 : $("#txtCodLic").val();
    var nombre_licencia = $("#txtNomLic").val() == "" ? 0 : $("#txtNomLic").val();
    var cadena = $("#txtCodLicMaster").val() == "" ? -1 : $("#txtCodLicMaster").val();
    var divsub1 = $("#ddlSubTipo1").val();
    var divsub2 = $("#ddlSubTipo2").val();
    var divsub3 = $("#ddlSubTipo3").val();
    var division = $("#ddDivision").val();
    var GrupoMod = $("#dllGruModalidad").val();
    var Agente = $("#hidEdicionEntAGE").val() == "" ? 0 : $("#hidEdicionEntAGE").val();
    var CON_FECHA_CREACION = $("#chkConFechaCreaTras").is(':checked') ? 1 : 0;
    var FECHA_CREA_INICIAL = $("#txtFecCreaInicialTras").val();
    var FECHA_CREA_FINAL = $("#txtFecCreaFinalTras").val();

    $.ajax({
        data: {
            BPS_ID: BPS_ID, LIC_ID: licencia, NOM_LIC: nombre_licencia, LIC_MASTER: cadena, ID_GROUP: grupo, OFF_ID: ofi,
            DIV1: divsub1, DIV2: divsub2, DIV3: divsub3, DIVISION: division, MOD_GROUP: GrupoMod, AGE_ID: Agente, CON_FECHA_CREACION: CON_FECHA_CREACION,
            FECHA_CREA_INICIAL: FECHA_CREA_INICIAL, FECHA_CREA_FINAL: FECHA_CREA_FINAL
        },
        url: '../TrasladarLicenciaDivision/ConsultaLiencenciasTrasladar',
        type: 'POST',
       // async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                //alert(dato.valor);
                //$("#trbuscar").show();
                ListarLicenciaOrigen();
                //$("#grid").html(dato.message);
                //ListarEstablecimientoCambio();
               // $("#grid2").html(dato.message);
                //$("#tractualizar").show();
            }
        }

    });
}

//LImpia controles
function LimpiarTodo() {

    $("#lbResponsable").html('Seleccione Un Responsable');
    $("#hidResponsable").val("");
    $("#txtCodLic").val("");
    $("#txtNomLic").val("");
    $("#txtCodLicMaster").val("");
    
    $("#grid").html('');
    $("#trseleccionar").hide();
    $("#tractualizar").hide();
    $("#Licdiv").hide();
    $("#trListaLicencias").hide();
    $("#trlistLicenciaActualizar").hide();
    $("#lbGrupo").html('SELECICONE UN GRUPO COMERCIAL');
    $("#hidGrupoFacturacion").val("");
    $("#ddlDivision").select()
    $("#ddlDivision").prop('selectedIndex', 0);
    $("#ddTipoDivision").prop('selectedIndex', 0);
    $("#ddDivision").prop('selectedIndex', 0);
    $("#ddlSubTipo1").prop('selectedIndex', 0);
    $("#ddlSubTipo2").prop('selectedIndex', 0);
    $("#ddlSubTipo3").prop('selectedIndex', 0);
    $("#lbOficina").html('Seleccione Oficina');
    $("#lbOficina").html('Seleccione Oficina');
    $("#hidOficina").val("");
    $("#chkFactPend").prop("checked", false);
    $("#chkFactHistorico").prop("checked", false);
    $("#dllGruModalidad").prop('selectedIndex', 10);//LOCALES
    $("#hidEdicionEntAGE").val("");
    $("#chkConFechaCreaTras").prop("checked", false);
    $("#txtFecCreaInicialTras").val("");
    $("#txtFecCreaFinalTras").val("");
    

}

function ListarLicenciaOrigen(){
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();

    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../TrasladarLicenciaDivision/ListarLicenciasTrasladoOrigen',
        type: 'POST',
        //async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                //$("#trbuscar").show();
                $("#grid").html(dato.message);
                // $("#grid2").html(dato.message);
                $("#trListaLicencias").show();
                $("#trseleccionar").show();
            }
        }

    });
}

function ListarLicenciaDestino() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();

    $.ajax({
        //data: { BPS_ID: BPS_ID },
        url: '../TrasladarLicenciaDivision/ListarLicenciasTrasladoDestino',
        type: 'POST',
     //   async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $("#grid2").html(dato.message);
                $("#tractualizar").show();
                $("#trlistLicenciaActualizar").show();
            }
        }

    });
}

function SeleccionarLicencia() {
    //var BPS_ID = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();
    //alert("Selecciono");
    var ReglaValor = [];
    var contador = 0;
    //var contador2 = 0;
    // LICENCIAS ORIGEN
    $('#tblLicencias tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();
       //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstOri").html();

            var NomEst = $(this).find(".NomEstOri").html();
            //alert(NomEst);
            if ($('#chkEstOrigen' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    codLicencia: idEst,
                    nombreLicencia: NomEst
                };
                contador += 1;
            }
        }
    });

    //LICENCIA DESTINO
    $('#tblLicenciasFin tr').each(function () {
        var IdNro = $(this).find(".IDEstFin").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstFin").html();

            var NomEst = $(this).find(".IDNomEstFin").html();
            //alert(NomEst);
            if ($('#chkEstFin' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    codLicencia: idEst,
                    nombreLicencia: NomEst
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    //alert(ReglaValor);
    if (contador > 0 || contador2>0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../TrasladarLicenciaDivision/LicenciasSeleccionadas',
            data:  ReglaValor ,
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {

                    ListarLicenciaOrigen();
                    ListarLicenciaDestino();

                }
            }

        });
    } else {
        alert("Seleccione al menos una Licencia para Continuar");
    }
}

function Actualizar() {

    var DIVID = $("#ddlLicDivision").val();
    var FACPEN = $("#chkFactPend").is(':checked') ? 1 : 0;
    var FACHIS = $("#chkFactHistorico").is(':checked') ? 1 : 0;

    //if ($("#chkFactPend").is(':checked')) 
    //    FACPEN = 1;
    
    //if ($("#chkFactHistorico").is(':checked')) 
    //    FACHIS = 1;
    

    var contadorl = 0;
    var contador = 0;

    //alert(DIVID);
    var ReglaValor = [];
    var ReglaValorL = [];

    $('#tblAgentesxDivisionObli tr').each(function () {
        var IdNumero = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNumero) && IdNumero != null) {
            var idAgente = $(this).find(".IDEstOri").html();
            var NomAgente = $(this).find(".NomEstOri").html();

            if ($('#chkEstOrigen' + idAgente).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idAgente
                    //nombreLicencia: NomAgente
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });


    $('#tblLicenciasFin tr').each(function () {
        var IdNro = $(this).find(".IDEstFin").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstFin").html();

            var NomEst = $(this).find(".IDNomEstFin").html();
            //alert(NomEst);
            if ($('#chkEstFin' + idEst).is(':checked')) {
                ReglaValorL[contadorl] = {
                    codLicencia: idEst
                };
                contadorl += 1;
            }
        }
    });

    var ReglaValorL = JSON.stringify({ 'ReglaValorL': ReglaValorL });
    //alert(ReglaValorL);

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TrasladarLicenciaDivision/RecuperaLicenciasTrasladar',
        data: ReglaValorL, //data:  ReglaValorL },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

            }
        }

    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TrasladarLicenciaDivision/RecuperaAgentesTrasladar',
        data: ReglaValor, //data:  ReglaValorL },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

            }
        }

    });
        
    $.ajax({

        type: 'POST',
        url: '../TrasladarLicenciaDivision/ActualizarLicenciasDivision',
        data: { DIV_ID: DIVID, FACPEND: FACPEN, FACHISTO: FACHIS }, //data:  ReglaValorL },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result ==1) {
                alert("Se Actualizaron Las Licencias Correctamente ");
                LimpiarTodo();

            } else {
                alert(dato.message);
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

function DivisionObligatorio() {
    $("#mvLicDivision").dialog("open");// Abrir PoPup
    var idModalidad = 0;
    var idEstablecimiento = 0;
    validarUbigeoDivision(idModalidad, idEstablecimiento, 'ddlLicDivision', 0);
}

function CuantasLicenciasSeleccionadas(){
    var contador=0;
    $('#tblLicenciasFin tr').each(function () {
        var IdNro = $(this).find(".IDEstFin").html();
        //alert(IdNro);
        if (!isNaN(IdNro) && IdNro != null) {
            //alert("Encontro IDCELLORI");
            var idEst = $(this).find(".IDEstFin").html();

            var NomEst = $(this).find(".IDNomEstFin").html();
            //alert(NomEst);
            if ($('#chkEstFin' + idEst).is(':checked')) {
                contador += 1;
            }
        }
    });
    return  contador;
}
function CuantosAgentesSeleccionados() {
    var contador = 0;
    $('#tblAgentesxDivisionObli tr').each(function () {
        var IdNumero = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNumero) && IdNumero != null) {
            var idAgente = $(this).find(".IDEstOri").html();
            var NomAgente = $(this).find(".NomEstOri").html();

            if ($('#chkEstOrigen' + idAgente).is(':checked')) {
                contador += 1;
            }
        }
    });
    return contador;
}

var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
};

function obtenerNombreConsultaOficina(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/obtenerNombreOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function clickCheckTraslado() {
    //var state = $("#idCheck").is(':checked');
    //if (state == 1) {
    //    $(".Check").attr('checked', true);
    //} else {
    //    $(".Check").attr('checked', false);
    //}

    //version actual
    var state = $("#idCheck").is(':checked');
    if (state == 1) {
        $(".Checko").attr('checked', true);
        var cantidad = CantidadFacturasSeleccionadas();
        if (cantidad > LimiteFacturaSelect) {
            $(".Checko").attr('checked', false);
            $("#idCheck").prop('checked', true);
            alert('Se seleccionaran los primeros ' + LimiteFacturaSelect + ' documentos.');
            ValidadCantidadFactSelecionadas(LimiteFacturaSelect);
        }
        else
        {
            ValidadCantidadFactSelecionadas(LimiteFacturaSelect);
        }
    } else {
        $(".Checko").attr('checked', false);

    }
}

function CantidadFacturasSeleccionadas() {

    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 0;
    $('#tblLicencias tr').each(function () {
        var id = $(this).find(".IDEstOri").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if ($('#chkEstOrigen' + id).is(':checked')) {
                cantidad += 1;
            }
        }
    });

    //alert("CANTIDADFACT SELECCIONADA :" + cantidad);
    return cantidad;
}

function ValidadCantidadFactSelecionadas(LimiteFacturaSelect) {
    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 1;
    $('#tblLicencias tr').each(function () {
        var id = $(this).find(".IDEstOri").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if (cantidad <= LimiteFacturaSelect) {
                $('#chkEstOrigen' + id).prop('checked', 'true');
                cantidad += 1;
            }
        }
    });
    return cantidad;
}



//

function clickCheckDestino() {
    //var state = $("#idCheck").is(':checked');
    //if (state == 1) {
    //    $(".Check").attr('checked', true);
    //} else {
    //    $(".Check").attr('checked', false);
    //}

    //version actual
    var state = $("#idCheckd").is(':checked');
    if (state == 1) {
        $(".Checkd").attr('checked', true);
        var cantidad = CantidadFacturasSeleccionadasD();
        if (cantidad > LimiteFacturaSelect) {
            $(".Checkd").attr('checked', false);
            $("#idCheckd").prop('checked', true);
            alert('Se seleccionaran los primeros ' + LimiteFacturaSelect + ' documentos.');
            ValidadCantidadFactSelecionadasD(LimiteFacturaSelect);
        }
        else {
            ValidadCantidadFactSelecionadasD(LimiteFacturaSelect);
        }
    } else {
        $(".Checkd").attr('checked', false);

    }
}

function CantidadFacturasSeleccionadasD() {

    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 0;
    $('#tblLicenciasFin tr').each(function () {
        var id = $(this).find(".IDEstFin").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if ($('#chkEstFin' + id).is(':checked')) {
                cantidad += 1;
            }
        }
    });

    //alert("CANTIDADFACT SELECCIONADA :" + cantidad);
    return cantidad;
}

function ValidadCantidadFactSelecionadasD(LimiteFacturaSelect) {
    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 1;
    $('#tblLicenciasFin tr').each(function () {
        var id = $(this).find(".IDEstFin").html();//ID FACTURA
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if (!isNaN(id) && id != null) {
            if (cantidad <= LimiteFacturaSelect) {
                $('#chkEstFin' + id).prop('checked', 'true');
                cantidad += 1;
            }
        }
    });
    return cantidad;
}

//AGENTE COMERCIAL - BUSQ. GENERAL
var reloadEventoAgente = function (idSel) {
    $("#lbAgente").val(idSel);
    $("#hidEdicionEntAGE").val(idSel);
    obtenerNombreSocio($("#lbAgente").val(), 'lbAgente');

};



function obtenerNombreSocio(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
            }
        }
    });
}