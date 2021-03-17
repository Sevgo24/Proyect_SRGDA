var modUso;

function initPopupsReporteArtista() {
    kendo.culture('es-PE');
    /*inicalizar popups*/

    $("#mvAdvertencia").dialog({
        close: function (event) {
            if (event.which) { returnPage(); }
        }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    });
    // $("#MVReporteArtista").dialog({ title: "SGRDA ", autoOpen: false, width: 650, height: 300, modal: false });
    $("#MVReporteArtista").dialog({ autoOpen: false, width: 300, height: 300, buttons: { "Grabar": addInterpreteRep, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });

    //Views/Autorizacion/Dialogs/MVObrasArtista
    $("#MVObrasArtista").dialog({ autoOpen: false, width: 300, height: 300, buttons: { "Grabar": addInterpreteRep, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });

    //$("#MVReporteArtista").dialog({ autoOpen: false, width: 450, height: 375, buttons: { "Grabar": addReporteDeta, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#ddlshow").on("change", function () {
        var codshow = $("#ddlshow").val();
        loadTipoArtistaLicencia('ddlinterprete', '0', codshow);
    });


    //XXXXXXXXXXXXXXXXXXXX POP UP DE OBRAS POR ARTISTA
    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    // $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    // var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    // $('#txtFecFinal').data('kendoDatePicker').enable(true);
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    $('#txtNumReferenciaReporte').val('');
    $('#txtNumReferenciaReporte').on("keypress", function (e) { return solonumeros(e); });

    loadPlaneamientoOpcionxLicencia('ddlAnioPeroidoLicencia', '0', $("#hidLicId").val(), 1);
    loadPlaneamientoOpcionxLicencia('ddlMesPeroidoLicencia', '0', $("#hidLicId").val(), 2);

}


$(function () {

    initPopupsReporteArtista();
    //DROWDOWNLIST.JS
    //$(".addAutorizacion").on("click", function () { limpiarAutorizacion(); $("#mvAutorizacion").dialog("option", "title", "Agregar Autorización"); $("#mvAutorizacion").dialog("open"); });

});

function initLoadAddReporte(paramSet, obj) {
    modUso = $(K_HID_KEYS.MODALIDAD_USO).val();
    /*
    if (modUso == "TEM") {
        //alert("TEM");
        Visibility("lblAutorizacionRep", true);
        Visibility("lblShowRep", true);
        Visibility("lblArtistaRep", true);
        Visibility("ddlAutorizacionRep", true);
        Visibility("ddlShowRep", true);
        Visibility("ddlArtistaRep", true);
        Visibility("ddlReporteFecPlan", false);
        Visibility("lblReporteFecPlan", false);

        //------------------------------cambio para planillas
        Visibility("title", false);
        Visibility("ddlSerie", false);
        Visibility("lbl", false);
        Visibility("txtCorrelativo", false);
        $("#ddlSerie").removeClass("requeridoLst");
        //----------------------------------------------------
        //es nuevo
        if (obj == undefined) {
            $('#ddlAutorizacionRep option').remove();
            loadAutorizaciones("ddlAutorizacionRep", $("#hidLicId").val(), 0);
        }

        $('#ddlShowRep option').remove();
        $('#ddlArtistaRep option').remove();
        $('#ddlShowRep').append($("<option />", { value: 0, text: "-SELECCIONE-" }));
        $('#ddlArtistaRep').append($("<option />", { value: 0, text: "-SELECCIONE-" }));

    }
    else if (modUso == "PER") {
    */
    Visibility("lblAutorizacionRep", false);
    Visibility("lblShowRep", false);
    Visibility("lblArtistaRep", false);
    Visibility("ddlAutorizacionRep", false);
    Visibility("ddlShowRep", false);
    Visibility("ddlArtistaRep", false);
    Visibility("ddlReporteFecPlan", true);
    Visibility("lblReporteFecPlan", true);
    loadFechaPlanificacion('ddlReporteFecPlan', paramSet, $("#hidLicId").val());
    //------------------------------cambio para planillas
    $("#ddlAutorizacionRep").removeClass("requeridoLst");
    $("#ddlShowRep").removeClass("requeridoLst");
    $("#ddlArtistaRep").removeClass("requeridoLst");
    //----------------------------------------------------
    //}


    //es edicion
    if (obj != undefined) {
        LoadTipoReporte("ddlTipoRep", obj.CodigoTipoRep);
        loadComboTerritorio(obj.CodigoTerritorio);
    } else {
        LoadTipoReporte("ddlTipoRep", 0);
        loadComboTerritorio(0);
    }
    //loadComboTerritorio(0);   
    $('#ddlTerritorio').attr('disabled', 'disabled');
}

//--------------------cambio para planillas
function imprimir(idreport, idLic, idPerFac) {
    $("#hidIdLicPlId").val(idPerFac);

    //var estadoFac = validarFactura_notacredito_anulada(idLic, idPerFac);
    //alert(estadoFac);
    //if (estadoFac) {
    //    var estado;
    //estado = validarImpresionPlanila(idreport);
    //if (estado) {
    //    alert("La planilla ya ha sido impresa.");
    //}
    //else {
    //    $("#hidIdReport").val(idreport);
    //    $("#hidIdLic").val(idLic);
    //    ObtenerSeriePlanilla($("#hidIdLic").val(), $("#hidIdReport").val());
    //}
    //}
    //else {
    //    alert("No se puede imprimir la planilla. Porque tiene, nota de crédito o esta anulada.")
    //}
    $("#hidIdReport").val(idreport);
    $("#hidIdLic").val(idLic);
    ObtenerSeriePlanilla($("#hidIdLic").val(), $("#hidIdReport").val());
}

function ObtenerSeriePlanilla(idlic, idreport) {
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
                    $("#hidIdSerie").val(en.IdSerie);
                    ComprobandoCorrelativoPlanilla('PL', idreport);
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

function ComprobandoCorrelativoPlanilla(type, idReport) {
    $.ajax({
        url: '../General/ObtenerCorrelativoXtipo',
        type: 'GET',
        data: { tipo: type },
        async: true,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                var idModalidad = $("#hidModalidad").val();
                var wrkfoId = GetWrkfoId(idModalidad);
                //var estado = ActualizarNroImpresionPlanilla(idReport);
                //if (estado) {
                //abrir_ventana('../Formatos/GenerarFormato/?idObj=' + wrkfoId + "&idTrace=" + dato.Code + "&idRef=" + $("#hidIdLic").val() + "&idSerie=" + $("#hidIdSerie").val() + "&idReportPlanilla=" + $("#hidIdReport").val(), "popupPDF", 600, 550);
                //GenerarFormatoPlanilla


                //abrir_ventana('../Formatos/GenerarFormatoPlanilla/?idObj=' + wrkfoId + "&idTrace=" + dato.Code + "&idRef=" + $("#hidIdLic").val() + "&idSerie=" + $("#hidIdSerie").val() + "&idReportPlanilla=" + $("#hidIdReport").val(), "popupPDF", 600, 550);
                
                //alert('$(K_HID_KEYS.LICENCIA).val() '+ $(K_HID_KEYS.LICENCIA).val());
                //alert('$(K_HID_KEYS.MODALIDAD_USO).val() ' + $(K_HID_KEYS.MODALIDAD_USO).val());
                //alert('$("#ddlAnioPeroidoLicencia").val() ' + $("#ddlAnioPeroidoLicencia").val());
                //alert(' $("#ddlMesPeroidoLicencia").val() ' + $("#ddlMesPeroidoLicencia").val());
                generarPlanillaPDF(wrkfoId, dato.Code, $("#hidIdLic").val(), $("#hidIdSerie").val(), $("#hidIdReport").val(), function (response) {
                    //if (response == true) ObtenerLicencia(traces.ref1Wrkf);
                    loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                });

                //}
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//function ActualizarNroImpresionPlanilla(id) {
//    var estado = false;
//    $.ajax({
//        url: '../Reporte/ActualizarNroImpresionPlanilla',
//        data: { Id: id },
//        type: 'POST',
//        dataType: 'JSON',
//        async: false,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                estado = true;
//            } else if (dato.result == 0) {
//                estado = false;
//            }
//        }
//    });
//    return estado;
//}

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

function validarImpresionPlanila(idreport) {
    var estado = false;
    $.ajax({
        url: '../Reporte/ObtenerEstadoImpresionPLanilla',
        type: 'GET',
        dataType: 'JSON',
        data: { idReport: idreport },
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                estado = true;
            } else if (dato.result == 0) {
                estado = false;
            }
        }
    });
    return estado;
}

function validarFactura_notacredito_anulada(idlic, idperfac) {
    var estado = false;
    $.ajax({
        url: '../Reporte/ObtenerFactuarNotaCreditoAnulada',
        type: 'GET',
        dataType: 'JSON',
        data: { idLic: idlic, idPerFac: idperfac },
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                estado = true;
            } else if (dato.result == 0) {
                estado = false;
            }
        }
    });
    return estado;
}


//------------------------------------------

function addReporte() {
    //alert($("#ddlSerie").val() + " " + $("#txtCorrelativo").val());

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE, K_DIV_POPUP.REPORTE)) {
        var entidad = {
            DescPlanilla: $("#txtDescPlanilla").val(),
            CodigoTerritorio: $("#ddlterritorio").val(),
            CodigoPerFacturacion: $("#ddlReporteFecPlan").val(),
            CodigoLicencia: $("#hidLicId").val(),
            idReporte: $("#hidEdicionRep").val(),
            idModalidad: $("#hidModalidad").val(),
            idShow: $("#ddlShowRep").val(),
            idArtista: $("#ddlArtistaRep").val(),
            idBps: $("#hidResponsable").val(),
            CodigoEstablecimiento: $("#hidEstablecimiento").val(),
            CodigoTipoRep: $("#ddlTipoRep").val(),
            CodigoAutorizacion: $("#ddlAutorizacionRep").val(),
            FecDesde: $("#ddlReporteFecPlan option:selected").text(),
            FecHasta: null,

            //------------------------------cambio para planillas
            ModUso: modUso,
            IdSerie: $("#ddlSerie").val(),
            //----------------------------------------------------
            NumReporteReferencia: $("#txtNumReferenciaReporte").val()
        };
        //alert(modUso);
        $.ajax({
            url: '../Reporte/Insertar',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    alert(dato.message);
                    loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });

        $("#" + K_DIV_POPUP.REPORTE).dialog("close");
    }
}

//-------------------------------------------------------------------------------
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
//-------------------------------------------------------------------------------



//----------------DETALLE REPORTE------------------------------------------------
function loadDataReporteDeta(idCab, idContendor) {
    loadDataGridReportesDetalle('ListarReporteDetalle', idContendor, idCab);
}

function loadDataGridReportesDetalle(Controller, idGrilla, idSel, paramOpcional) {
    $.ajax({
        data: { paramId: idSel },
        type: 'POST', url: "../Reporte/" + Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
            if (paramOpcional != undefined) RefreshDetalle(paramOpcional.idCabecera);
        }
    });
}

function limpiarReporte() {
    $("#txtDescPlanilla").val("");
    $("#hidAccionMvRep").val("0");
    $("#hidEdicionRep").val("0");
    $("#txtNumReferenciaReporte").val("");
    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE, K_DIV_POPUP.REPORTE);
}

function delAddReporte(idDel, esActivo) {
    var estado;
    //var inactiva = ValidarLicenciaFacturada();
    estado = validarImpresionPlanila(idDel);
    if (!inactiva) {//si es verdad entonces es por que no tiene ninguna factura cancelada
        if (!estado) {
            $.ajax({
                url: '../Reporte/Eliminar',
                type: 'POST',
                data: { id: idDel, EsActivo: esActivo },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        loadDataReporte($("#hidLicId").val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
            return false;
        }
        else {
            alert("La planilla ya ha sido impresa.");
        }
    } else {
        alert("No puede Activarse/Inactivarse una factura Relacionada a la Licencia ha sido Cancelada ");
    }
}
function updAddReporte(idUpd) {
    var estado;
    estado = validarImpresionPlanila(idUpd);

    if (!estado) {
        limpiarReporte();
        //initLoadAddReporte();
        $.ajax({
            url: '../Reporte/ObtieneReporte',
            data: { idLic: $("#hidLicId").val(), idRep: idUpd },
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result === 1) {
                    var rep = dato.data.Data;
                    if (rep != null) {
                        $("#hidAccionMvRep").val("1");
                        $("#hidEdicionRep").val(rep.idReporte);
                        $("#hidIdLicPlId").val(rep.CodigoPerFacturacion);
                        $("#txtDescPlanilla").val(rep.DescPlanilla);
                        $("#txtNumReferenciaReporte").val(rep.NumReporteReferencia);

                        ListarSerieXtipo("ddlSerie", 'PL', rep.IdSerie);
                        //$("#ddlterritorio").val(rep.CodigoTerritorio);
                        $('#ddlAutorizacionRep option').remove();
                        loadAutorizaciones("ddlAutorizacionRep", $("#hidLicId").val(), dato.valor == null ? 0 : dato.valor);
                        loadShow("ddlShowRep", dato.valor == null ? 0 : dato.valor, rep.idShow == null ? 0 : rep.idShow);
                        loadArtista("ddlArtistaRep", rep.idShow == null ? 0 : rep.idShow, rep.idArtista == null ? 0 : rep.idArtista);

                        $("#mvReporte").dialog("option", "title", "Actualizar Reporte");
                        $("#mvReporte").dialog("open");
                        initLoadAddReporte(rep.CodigoPerFacturacion, rep);



                    } else {
                        alert("No se pudo obtener el Reporte para editar.");
                    }
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    else {
        alert("La planilla ya ha sido impresa.");
    }
}


function verDetalle(id) {
    //  alert($("#expandReport" + id).attr('src'));
    if ($("#expandReport" + id).attr('src') == '../Images/iconos/minus.png') {
        $("#expandReport" + id).attr('src', '../Images/iconos/plus.png');
        $("#expandReport" + id).attr('title', 'Ver Detalle.');
        $("#expandReport" + id).attr('alt', 'Ver Detalle.');
        $("#divDetalleRep_" + id).css("display", "none");
        $("#tdDetalleRep_" + id).css("background", "transparent");

    } else {
        $("#expandReport" + id).attr('src', '../Images/iconos/minus.png');
        $("#expandReport" + id).attr('title', 'Ocultar Detalle.');
        $("#expandReport" + id).attr('alt', 'Ocultar Detalle.');
        loadDataReporteDeta(id, "#divDetalleRep_" + id);
        $("#divDetalleRep_" + id).css("display", "inline");
        $("#tdDetalleRep_" + id).css("background", "#dbdbde");

    }


}

function RefreshDetalle(id) {

    $("#expandReport" + id).attr('src', '../Images/iconos/minus.png');
    $("#expandReport" + id).attr('title', 'Ocultar Detalle.');
    $("#expandReport" + id).attr('alt', 'Ocultar Detalle.');
    loadDataReporteDeta(id, "#divDetalleRep_" + id);
    $("#divDetalleRep_" + id).css("display", "inline");
    $("#tdDetalleRep_" + id).css("background", "#dbdbde");



}

function agregarDetalle(idReporte) {


    $("#MVReporteArtista").dialog("open");
    loadTipoShowLicencia('ddlshow', '0', $("#hidLicId").val());
    //msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, "");
    //limpiarReporteDeta();
    //$("#hidIdRepCab").val(idReporte); /*set value id cabecera*/
    //loadMonedaRecaudacion("ddMonedaRepDeta", 0);
    ////$("#" + K_DIV_POPUP.REPORTE_DETA).dialog("open");

    //$("#mvArtistas").dialog("open");
    //ObtieneNombreShowXReport(idReporte, function (nombreShow) {
    //    $("#txtShowRepDeta").val(nombreShow);
    //    $("#txtShowRepDeta").attr("disabled", "disabled");
    //});

}





///*INCICIO CODIGO REFERENTE A REPORTES DETALEEE*///
function limpiarReporteDetaNeo() {
    $("#txtTituloObraRepDeta").val("");
    //  $("#txtShowRepDeta").val("");
    $("#txtAuto1RepDeta").val("");
    $("#txtAuto2RepDeta").val("");
    $("#txtTotalDetaRepDeta").val("");
    $("#txtTotalEjecRepDeta").val("");
    $("#txtFecEmisionRepDeta").val("");
    $("#txtDuracion").val("");
    $("#txtTotalSeg").val("");

    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, K_DIV_POPUP.REPORTE_DETA);
}
function limpiarReporteDeta() {
    $("#hidIdRepCab").val("");
    $("#hidAccionMvRepDeta").val("0");
    $("#hidEdicionRepDeta").val("0");
    $("#txtTituloObraRepDeta").val("");
    $("#txtShowRepDeta").val("");
    $("#txtAuto1RepDeta").val("");
    $("#txtAuto2RepDeta").val("");
    $("#txtTotalDetaRepDeta").val("");
    $("#txtTotalEjecRepDeta").val("");
    $("#txtFecEmisionRepDeta").val("");
    $("#txtTotalSeg").val("");
    $("#txtDuracion").val("");
    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, K_DIV_POPUP.REPORTE_DETA);
}
function addReporteDeta() {
    if ($("#hidIdRepCab").val() == "") {
        alert("No se ha obtenido el Id del reporte cabecera.");
        return false;
    } else {
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, K_DIV_POPUP.REPORTE_DETA)) {

            var tipoTime = $("input:radio[name=rdbDuracion]:checked").val();
            var DurSeg = null;
            var DurMin = null;

            if (tipoTime == "min") {
                DurMin = $("#txtDuracion").val();
            } else {
                DurSeg = $("#txtDuracion").val();
            }

            var entidad = {
                CodigoReporteCab: $("#hidIdRepCab").val(),
                CodigoReporteDeta: $("#hidEdicionRepDeta").val(),
                Titulo: $("#txtTituloObraRepDeta").val(),
                Show: $("#txtShowRepDeta").val(),
                AutorA: $("#txtAuto1RepDeta").val(),
                AutorB: $("#txtAuto2RepDeta").val(),
                TotalDetalle: $("#txtTotalDetaRepDeta").val(),
                TotalEjecucion: $("#txtTotalEjecRepDeta").val(),
                Fecha: $("#txtFecEmisionRepDeta").val(),
                CodigoMoneda: $("#ddMonedaRepDeta option:selected").val(),
                DuracionMin: DurMin,
                DuracionSeg: DurSeg,
                DuracionSegTotal: $("#txtTotalSeg").val()
            };

            $.ajax({
                url: '../Reporte/InsertarDeta',
                type: 'POST',
                data: entidad,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        // alert(dato.message);
                        loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());/*Actualizar la cabecera y detalle*/
                        /*SI NO ES EDICION DE UN DETALLE EL CODIGO REPORTE DETA ES 0 
                      ENTONCES NO CIERRA LA VENTANA PARA SEGUIR AGREGANDO ITEMS..
                      CASO CONTRARIO ES EDICION DE DATOS ..LA VENTANA SE CIERRA AL FINALIZAR LA ACTUALIZACION.*/
                        if (entidad.CodigoReporteDeta == 0) {
                            limpiarReporteDetaNeo();
                        } else {
                            $("#" + K_DIV_POPUP.REPORTE_DETA).dialog("close");
                        }
                        msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, dato.message);
                    } else if (dato.result == 0 || dato.result == 3) {
                        //alert(dato.message);
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_REPORTE_DETA, dato.message);
                    }




                }
            });


        }
    }
}

/*carga los datos para el popup para su modificacion.*/
function updAddReporteDeta(idUpd, idRepCab) {
    limpiarReporteDeta();
    var estado;
    estado = validarImpresionPlanila(idRepCab);
    if (!estado) {
        $.ajax({
            url: '../Reporte/ObtieneReporteDeta',
            data: { idRepDeta: idUpd, idRepCab: idRepCab },
            type: 'POST',
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result === 1) {
                    var rep = dato.data.Data;
                    if (rep != null) {
                        $("#hidIdRepCab").val(idRepCab);
                        $("#hidAccionMvRepDeta").val("1");
                        $("#hidEdicionRepDeta").val(rep.CodigoReporteDeta);
                        $("#txtTituloObraRepDeta").val(rep.Titulo);
                        $("#txtShowRepDeta").val(rep.Show);
                        $("#txtShowRepDeta").attr("disabled", "disabled");
                        $("#txtAuto1RepDeta").val(rep.AutorA);
                        $("#txtAuto2RepDeta").val(rep.AutorB);
                        $("#txtTotalDetaRepDeta").val(rep.TotalDetalle);
                        $("#txtTotalEjecRepDeta").val(rep.TotalEjecucion);

                        var fechaEmi = $("#txtFecEmisionRepDeta").data("kendoDateTimePicker");
                        fechaEmi.value(rep.Fecha);
                        //alert(rep.Fecha);
                        loadMonedaRecaudacion("ddMonedaRepDeta", rep.CodigoMoneda);


                        var $radios = $('input:radio[name=rdbDuracion]');
                        if (rep.DuracionMin == null) {
                            $("#txtDuracion").val(rep.DuracionSeg);
                            if ($radios.is(':checked') === false) {
                                $radios.filter('[value=seg]').prop('checked', true);
                            }
                        } else {
                            $("#txtDuracion").val(rep.DuracionMin);
                            if ($radios.is(':checked') === false) {
                                $radios.filter('[value=min]').prop('checked', true);
                            }
                        }
                        $("#txtTotalSeg").val(rep.DuracionSegTotal);

                        $("#" + K_DIV_POPUP.REPORTE_DETA).dialog("option", "title", "Actualizar Detalle de Reporte.");
                        $("#" + K_DIV_POPUP.REPORTE_DETA).dialog("open");

                    } else {
                        alert("No se pudo obtener el Reporte para editar.");
                    }
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    else {
        alert("La planilla ya ha sido impresa.");
    }
}
function delAddReporteDeta(idDel, esActivo, idRepCab) {
    var estado;
    estado = validarImpresionPlanila(idRepCab);
    if (!estado) {
        $.ajax({
            url: '../Reporte/EliminarDeta',
            type: 'POST',
            data: { id: idDel, EsActivo: esActivo, idRepCab: idRepCab },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    alert('despues de eliminar')
                    loadDataReporte($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.MODALIDAD_USO).val(), 0, $("#ddlAnioPeroidoLicencia").val(), $("#ddlMesPeroidoLicencia").val());
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    else {
        alert("La planilla ya ha sido impresa.");
    }
    return false;
}
///*FIN  CODIGO REFERENTE A REPORTES DETALEEE*///



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


var generarPlanillaPDF = function (idObj, idTrace, idRef, idSerie, idPlanilla, callback) {
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
            callback(retorno);
        }
    });
};

function addInterpreteRep() {
    if (ValidarRequeridos()) {
        alert("Interprete Agregado correctamente");
    } else {
        alert("Interprete NO Agregado correctamente");
    }
}


function ValidarLicenciaFacturada() {
    var lic_id = $("#hidLicId").val();
    var retorno = false;
    $.ajax({
        data: { LIC_ID: lic_id },
        url: "../Reporte/ValidaLicenciaFactCancelada",
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                retorno = true;
            }
            else {
                retorno = false;
            }
        }
    });

    return retorno;
}