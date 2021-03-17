
$(function () {
    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    // -- BUSQUEDA GENERAL OFICINA
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    $("#mvEjecutarProceso").dialog({ title: "SGRDA :: Visor de Formatos.", autoOpen: false, width: 850, height: 650, modal: true });
    ObtenerOficinaConsultaDocumeno();
    // --- LOAD GRUPO MODALIDAD
    loadTipoGrupo('dllGruModalidad', '0');
    $('#btnGenerarPlanillasMasivo').hide();
    //loadTipoGrupo

    $("#btnGenerarPlanillasMasivo").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        //$("#grid").html('');
        GenerarMasiva();
        $('#btnGenerarPlanillasMasivo').prop('disabled', false);
    });

    $("#btnBuscar").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        $("#grid").html('');
        BuscarPLanillas();
        var idLicencia = $('#txtNumLic').val() == '' ? 0 : $('#txtNumLic').val();
        if (idLicencia != 0) {
            $('#btnGenerarPlanillasMasivo').show();
        }

    });

    $("#btnGenerarPlanillas").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        //$("#grid").html('');
        //alert();
        GenerarPLanillas();
    });

    //COMBO HABILIAT
    //btnGenerarPlanillas

});


//********* FUNCIONES *******************
function GenerarPLanillas() {

    var idOficina = $('#hidOficina').val() == '' ? 0 : $('#hidOficina').val();
    var idDivision = $('#ddlDivision').val() == null ? 0 : $('#ddlDivision').val();
    var idGrupoModalidad = $('#dllGruModalidad').val();
    var idLicencia = $('#txtNumLic').val() == '' ? 0 : $('#txtNumLic').val();
    var idSocio = 0;
    var valFecIni = $('#txtFecInicial').val();
    var valFecFin = $('#txtFecFinal').val();
    var estadoPlanilla = $('#ddlEstadoPlanilla').val();






    $.ajax({
        url: '../Planilla/GenerarPlanillas',
        type: 'POST',
        data: {
            ID_OFICINA: idOficina,
            ID_DIVISION: idDivision,
            GRUPO_MODALIDAD: idGrupoModalidad,
            LIC_ID: idLicencia,
            ID_SOCIO: idSocio,
            FEC_INI: valFecIni,
            FEC_FIN: valFecFin,
            ESTADO: estadoPlanilla
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                if (dato.TotalFacturas > 0) {
                    BuscarPLanillas();
                    alert("Total de planillas generadas: " + dato.TotalFacturas);
                } else {
                    alert("No existen PLanillas pendientes de generar.");
                }
            } else if (dato.result == 0) {
                $("#grid").html('');
                $("#CantidadRegistros").html("");
            }
        }
    });
}

function BuscarPLanillas() {
    var idOficina = $('#hidOficina').val() == '' ? 0 : $('#hidOficina').val();
    var idDivision = $('#ddlDivision').val() == null ? 0 : $('#ddlDivision').val();
    var idGrupoModalidad = $('#dllGruModalidad').val();
    var idLicencia = $('#txtNumLic').val() == '' ? 0 : $('#txtNumLic').val();
    var idSocio = 0;
    var valFecIni = $('#txtFecInicial').val();
    var valFecFin = $('#txtFecFinal').val();
    var estadoPlanilla = $('#ddlEstadoPlanilla').val();

    $.ajax({
        url: '../Planilla/ObtenerCabecera',
        type: 'POST',
        data: {
            ID_OFICINA: idOficina,
            ID_DIVISION: idDivision,
            GRUPO_MODALIDAD: idGrupoModalidad,
            LIC_ID: idLicencia,
            ID_SOCIO: idSocio,
            FEC_INI: valFecIni,
            FEC_FIN: valFecFin,
            ESTADO: estadoPlanilla
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#grid").html(dato.message);
                $("#CantidadRegistros").html(dato.valor);
            } else if (dato.result == 0) {
                $("#grid").html('');
                $("#CantidadRegistros").html("");
                alert(dato.message);
            }
        }
    });

}

function validarBusqueda() {
    var estado = 1;
    var idOficina = $('#hidOficina').val();
    var idDivision = $('#ddlDivision').val();
    var idGrupoModalidad = $('#dllGruModalidad').val();
    var idLicencia = $('#txtNumLic').val();
    var valFecIni = $('#txtFecInicial').val();
    var valFecFin = $('#txtFecFinal').val();



}




// OFICINA - BUSQ. GENERAL
var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());

}

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
                $("#lbOficina").css({ 'color': 'black' });
                loadComboDivisionesXOficina('ddlDivision', idSel, 0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

// PLANILLA
//--------------------cambio para planillas
function imprimir(idreport, idLic, idPerFac, Mod_Id) {
    //alert('imprimir');
    //$("#hidIdLicPlId").val(idPerFac);
    //$("#hidIdReport").val(idreport);
    //$("#hidIdLic").val(idLic);
    //ObtenerSeriePlanilla($("#hidIdLic").val(), $("#hidIdReport").val());
    ObtenerSeriePlanilla(idLic, idreport, Mod_Id);


}

function ObtenerSeriePlanilla(idlic, idreport, Mod_Id) {
    $.ajax({
        url: '../Reporte/ObtenerSeriePlanilla',
        data: { idLic: idlic, IdReport: idreport },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var en = dato.data.Data;
                if (en != null) {
                    ComprobandoCorrelativoPlanilla('PL', idreport, en.IdSerie, idlic, Mod_Id);
                } else {
                    alert("No se pudo obtener la serie de la planilla.");
                    return;
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ComprobandoCorrelativoPlanilla(type, idReport, idSerie, idlic, Mod_Id) {
    //alert('ComprobandoCorrelativoPlanilla');
    //alert('type ' + type);
    //alert('idReport ' + idReport);
    //alert('idSerie ' + idSerie);
    //alert('idlic ' + idlic);

    $.ajax({
        url: '../General/ObtenerCorrelativoXtipo',
        type: 'GET',
        data: { tipo: type },
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var wrkfoId = GetWrkfoId(Mod_Id);
                //var estado = ActualizarNroImpresionPlanilla(idReport);
                //if (estado) {
                //abrir_ventana('../Formatos/GenerarFormato/?idObj=' + wrkfoId + "&idTrace=" + dato.Code + "&idRef=" + $("#hidIdLic").val() + "&idSerie=" + $("#hidIdSerie").val() + "&idReportPlanilla=" + $("#hidIdReport").val(), "popupPDF", 600, 550);
                //GenerarFormatoPlanilla


                //abrir_ventana('../Formatos/GenerarFormatoPlanilla/?idObj=' + wrkfoId + "&idTrace=" + dato.Code + "&idRef=" + $("#hidIdLic").val() + "&idSerie=" + $("#hidIdSerie").val() + "&idReportPlanilla=" + $("#hidIdReport").val(), "popupPDF", 600, 550);

                ////generarPlanillaPDF(wrkfoId, dato.Code, $("#hidIdLic").val(), $("#hidIdSerie").val(), $("#hidIdReport").val(), function (response) {
                ////    //if (response == true) ObtenerLicencia(traces.ref1Wrkf);
                ////    loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                ////});
                //alert(wrkfoId + '_wrkfoId');
                generarPlanillaPDF(wrkfoId, dato.Code, idlic, idSerie, idReport
                    //, function (response) {
                    //loadDataReporte(idlic, $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                    //}
                );


            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}




function GetWrkfoId(idModalidad) {
    var id = "";
    $.ajax({
        url: '../Reporte/ObtenerWrkfoId',
        type: 'GET',
        dataType: 'JSON',
        data: { idModalidad: idModalidad },
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                id = dato.valor;
                //alert(id);        
            } else if (dato.result == 0) { }
        }
    });
    return id;
}


function loadDataReporte(codLic, idMod, paramOpcional, anio, mes) {
    loadDataGridReportes('ListarReporte', "#gridReporte", codLic, idMod, paramOpcional, anio, mes);
}

function loadDataGridReportes(Controller, idGrilla, idSel, idMod, paramOpcional, anio, mes) {

    $.ajax({
        data: { paramId: idSel, ModUso: idMod, Anio: anio, Mes: mes },
        type: 'POST', url: "../Reporte/" + Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
            if (paramOpcional != undefined) RefreshDetalle(paramOpcional.idCabecera);
            //$('#gridReporte tr> *:nth-child(5)').first().text('Num. Factura');
            //$('#gridReporte tr > *:nth-child(6)').hide();            
        }
    });
}



//var generarPlanillaPDF = function (idObj, idTrace, idRef, idSerie, idPlanilla, callback) {
//    var retorno = false;
//    $.ajax({
//        data: { idObj: idObj, idTrace: idTrace, idRef: idRef, idSerie: idSerie, idReportPlanilla: idPlanilla },
//        url: "../Formatos/GenerarFormatoPlanillaJson",
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1 || dato.result == 0) {
//                verPlanillaPDF(dato.result, dato.message);
//                if (dato.result == 1) retorno = true;
//            }
//            callback(retorno);
//        }
//    });
//};


var generarPlanillaPDF = function (idObj, idTrace, idRef, idSerie, idPlanilla) {
    var retorno = false;
    $.ajax({
        data: { idObj: idObj, idTrace: idTrace, idRef: idRef, idSerie: idSerie, idReportPlanilla: idPlanilla },
        url: "../Formatos/GenerarFormatoPlanillaJson",
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1 || dato.result == 0) {
                verPlanillaPDF(dato.result, dato.message);
                if (dato.result == 1) retorno = true;
            }
        }
    });
};
var verPlanillaPDF = function (result, message) {
    $("#lblResultGenPDF").html('');
    if (result == 1) {
        var url = message;
        $("#lblResultGenPDF").attr("display", "none");
        $("#ifContenedorFormato").attr("display", "inline");
        $("#ifContenedorFormato").attr("src", url);
        //$("#ifContenedorFormato").attr("src", 'C:/inetpub/wwwroot/Documentos/20151223144111_Omiso248H.pdf');
    } else {
        $("#lblResultGenPDF").attr("display", "inline");
        $("#ifContenedorFormato").attr("display", "none");
        $('#ifContenedorFormato').attr('src', '');
        $("#lblResultGenPDF").html(message);
    }
    $("#mvEjecutarProceso").dialog("open");
};




function GenerarMasiva() {
    $('#btnGenerarPlanillasMasivo').prop('disabled', true)
    var id = "";
    $.ajax({
        url: '../Planilla/ConcatenarPDF',
        type: 'GET',
        dataType: 'JSON',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                id = dato.valor;
                verPlanillaPDF(dato.result, dato.message)

                //alert(id);        
            } else if (dato.result == 0) { }
        }
    });
    return id;
}


//function RecuperarTExto(Numero) {
//    var id = "";
//    $.ajax({
//        url: '../Planilla/ConvertiraTexto',
//        type: 'GET',
//        dataType: 'JSON',
//        data: { Numero: Numero },
//        async: false,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                id = dato.valor;
//                //alert(id);        
//            } else if (dato.result == 0) { }
//        }
//    });
//    return id;
////}

function ObtenerOficinaConsultaDocumeno() {
    $.ajax({
        //data: { Id: idSel },
        url: '../ConsultaDocumento/ObtenerOficinaConsultaDocumeno',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#hidOficina").val(dato.Code);
                $("#lbOficina").html(dato.valor);
                if (dato.Code == 10154 || dato.Code == 10081) {
                    $("#btnBuscaOficina").show();
                    $("#btnBuscaOficina").prop('disabled', false);
                    $('#Oficina').prop('disabled', false);
                }
                else {
                    $("#btnBuscaOficina").hide();
                    $("#btnBuscaOficina").prop('disabled', true);
                    $('#lbOficina').prop('disabled', true);
                    $('#Oficina').prop('disabled', true);
                }



            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}