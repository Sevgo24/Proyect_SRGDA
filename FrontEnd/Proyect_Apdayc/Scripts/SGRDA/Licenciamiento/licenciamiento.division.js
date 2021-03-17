/************************** INICIO CONSTANTES****************************************/
var K_WIDTH_DIV = 380;
var K_HEIGHT_DIV = 150;
var K_WIDTH_DIV_ROL = 380;
var K_HEIGHT_DIV_ROL = 150;

var K_WIDTH_DIV_OBLI = 500;
var K_HEIGHT_DIV_OBLI = 300;

// XX DIVISIONES ADMINISTRATIVAS XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
function InitPoPupDivision() {
    //PoPup - División
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

    $("#mvLicDivisionAgenteObligatorio").dialog("widget").find(".ui-dialog-titlebar-close").hide();

    if (GetQueryStringParams("set") == undefined)
    $("#mvLicDivision").dialog("widget").find(".ui-dialog-titlebar-close").hide();
     
    //Boton Agregar división
    $(".addDivisionDDL").on("click", function () {
        var idModalidad = $('#hidModalidad').val();
        var idEstablecimiento = $('#hidEstablecimiento').val();
        validarUbigeoDivision(idModalidad, idEstablecimiento, 'ddlLicDivision', 0);
        var idLicencia = $('#hidLicId').val();
    });

    var idLicencia = $('#hidLicId').val();
    loadDataDivisionLic(idLicencia);
    loadRol('ddlLicDivisionRol', 0);

    LimpiarAgentObli();

    $("#btnBuscarAgenteObli").on("click", function () {

        ListarAgentesObligatorios($("#ddlLicDivision").val());

    });

    // -- Contenedor Agente
    //mvInitBuscarAgenteRecaudo({ container: "ContenedormvBuscarRecaudador", idButtonToSearch: "bntAgenteRecaudo", idDivMV: "mvBuscarAgenteRecaudo", event: "addAgenteRecaudo", idLabelToSearch: "lblAgenteRecaudo", tipoPersona: "N" });

    // XX DIVISIONES ADMINISTRATIVAS XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    //mvInitDivisiones({ container: "ContenedormvBuscarGestor", idButtonToSearch: "Abrir", idDivMV: "mvBuscarDivision", event: "addAgenteRecaudo", idLabelToSearch: "lblAgenteRecaudo", tipoDiv: 'ADM', bloqueoTipoDiv: '1' });
    mvInitAgenteDivision({ container: "ContenedormvBuscarRecaudador", idButtonToSearch: "bntAgenteRecaudo", idDivMV: "mvBuscarAgenteRecaudo", event: "addAgenteRecaudo", idLabelToSearch: "lblAgenteRecaudo" });

    //mvInitValidarObligatorio({ container: "ContenedormvAgenteRecaudoObliga", idButtonToSearch: "bntAgenteRecaudo", idDivMV: "mvBuscarAgenteRecaudoObliga", event: "addAgenteRecaudo", idLabelToSearch: "lblAgenteRecaudo" });

    //$(".addDivision").on("click", function () {
    //    $("#mvBuscarDivision").dialog("open");
    //});

}

function validarUbigeoDivision(idModalidad,idEstablecimiento, control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: "../LicenciaDivision/ValidacionDivision",
        type: 'POST',
        data: { idModalidad: idModalidad, idEstablecimiento: idEstablecimiento },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
                $("#mvLicDivision").dialog("open");// Abrir PoPup
            } else {
                alert(dato.message);
            }
        }
    });    
}

//****************************   TAB - DIVISION ADMIN.  *********************************************
var addDivisionLic = function () {
    var idLicencia = $('#hidLicId').val();
    var idDivision = $('#ddlLicDivision').val();

    $.ajax({
        url: '../LicenciaDivision/AddDivisionAdm',
        type: 'POST',
        data: { idLicencia: idLicencia, idDivision: idDivision },
        async :false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#mvLicDivision").dialog("close");
                loadDataDivisionLic(idLicencia);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};


function ActEstadoDivisionAdm(id, idLic, idDiv, idEstado) {
    $.ajax({
        url: '../LicenciaDivision/ActEstadoDivisionAdm',
        type: 'POST',
        data: { id: id, idLic: idLic, idDiv: idDiv, estado: idEstado },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionLic(idLic);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function loadDataDivisionLic(idLic) {
    loadDataDivLicGridTmp('../LicenciaDivision/ListarDivisionAdm', "#gridLicDivisionAdm", idLic);
}

function loadDataDivLicGridTmp(Controller, idGrilla, idLic) {
    $.ajax({
        data: { idLicencia: idLic },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
        }
    });
}

//AGENTES DE RECAUDO - (agente de servicio y de recaudo)
//****************************   TAB - GRUPO MODALIDAD  *********************************************
function AbrirPoPupAddAgente(id, idDivision) {
    $("#hidIdDivisionAdmAR").val(idDivision);
    $("#mvBuscarAgenteRecaudo").dialog("open");
    loadDataDivisionAgenteAR();
}

//PoPup - Agregar Agente Recaudo
var addAgenteRecaudo = function (idSel) {
    var idLicencia = $('#hidLicId').val();
    $.ajax({
        url: '../LicenciaDivision/AddDivisionAgenteRecaudo',
        async: false,
        type: 'POST',
        data: { idRecaudo: idSel, idLicencia: idLicencia },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionLic(idLicencia);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

    $('#ddlLicDivisionRol').val(0);
    $("#hidLicDivRolIdAgente").val(0);
    $("#hidLicDivRolIdAgente").val(idSel);    
};


function delAgenteDivision(id, idLic) {
    $.ajax({
        url: '../LicenciaDivision/delDivisionAgenteRecaudo',
        type: 'POST',
        data: { id: id, idLic: idLic },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionLic(idLic);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function actAgenteDivision(id, idLic, idEstado) {
    $.ajax({
        url: '../LicenciaDivision/actDivisionAgenteRecaudo',
        type: 'POST',
        data: { id: id, idLic: idLic, idEstado: idEstado },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionLic(idLic);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function InsertaDivisionObligatorio() {
    var DIVISION_SELECCIONADA = $("#ddlLicDivision").val();
    $("#mvLicDivision").dialog("close");
    $("#mvLicDivisionAgenteObligatorio").dialog("open");
    LimpiarAgentObli();
    ListarAgentesObligatorios(DIVISION_SELECCIONADA);


    //$("#hidIdDivisionAdmAR").val(DIVISION_SELECCIONADA),
    //loadDataDivisionAgenteAR();
    
}

function ListarAgentesObligatorios(id_div){

    var agent =$("#txtAgenteObli").val() == "" ? "" : $("#txtAgenteObli").val();
    $.ajax({
        url: '../AgenteDivision/ListarSAagentesxDivision',
    type: 'POST',
    data: { idOficina: 0, idDivision: id_div, agenteRecaudo: agent },
    beforeSend: function () { },
    success: function (response) {
        var dato = response;
        validarRedirect(dato);
        if (dato.result == 1) {
            var dato = response; validarRedirect(dato); /*add sysseg*/
            $("#gridAgenteObligatorio").html(dato.message);
        } else if (dato.result == 0) {
            alert(dato.message);
        }
    }
});


}

function DivisionObligatorioInsert(licid) { //al darle grabar licencia se ejecuta tmb esta funcion


    if (GetQueryStringParams("set") === undefined) { //soo si es una licencia nueva se debe de realizar el insert 

        addDivisionLic();// iNSERTANDO la division

        var ReglaValor = [];
        var contador = 0;
        //Recuperando los COLL_ID DE LOS AGENTES Y INSERTANDOS REUTILIZANDO LA FUNCION
        $('#tblAgentesxDivisionObli tr').each(function () {
            var IdNumero = $(this).find(".IDEstOri").html();

            if (!isNaN(IdNumero) && IdNumero != null) {
                var idAgente = $(this).find(".IDEstOri").html();
                var NomAgente = $(this).find(".NomEstOri").html();

                if ($('#chkEstOrigen' + idAgente).is(':checked')) {

                    //contador += 1;
                    //alert(idAgente);
                    addAgenteRecaudo(idAgente);
                }
            }
        });
    }

}

function ValidaAgentesAgregados() {

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
        
    if (contador ==  1) {

        $("#mvLicDivisionAgenteObligatorio").dialog("close");
    }
    else {
        alert("Seleccione solo  un Agente");

    }

}

function LimpiarAgentObli() {

    $("#txtAgenteObli").val('');
}