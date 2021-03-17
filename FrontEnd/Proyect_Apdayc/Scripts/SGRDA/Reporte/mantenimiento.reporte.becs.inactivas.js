/************************** INICIO CARGA********************************************/
$(function () {

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);

    //-------------------------- EVENTO BOTONES ---------------------
    $("#btnPdf").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();
        var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
        var LIC_ID = $("#hidLicencia").val() == null || "" ? "0" : $("#hidLicencia").val();

        if (SOCIO != 0 || EST_ID != 0 || LIC_ID!=0) {
            ExportarReportef('PDF');
        } else {
            alert("SELECCIONE SOCIO, ESTABLECIMIENTO O LICENCIA.");
        }
    });
    $("#btnExcel").on("click", function () {
        var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();
        var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
        var LIC_ID = $("#hidLicencia").val() == null || "" ? "0" : $("#hidLicencia").val();

        if (SOCIO != 0 || EST_ID != 0 || LIC_ID != 0) {
            ExportarReportef('EXCEL');
        }else {
            alert("SELECCIONE SOCIO, ESTABLECIMIENTO O LICENCIA.");

        }
        //var estadoRequeridos = ValidarRequeridos();       
    });

    $('#divEstado').val(0);
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);

        if (oculta == true) {
            $('#trRubro').show();
            //$("input[name=valida]").prop('enabled',true);
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);

        }
    }
    //Si ocultacombo sera =1 Siempre que el usuario que ingreso sea Admin o Contabilidad
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
    }
    else {
        //deshabilita el Select
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);
    }

    $("#dllOficina").change(function () {
        if ($(this).val() == "26") {
            if (ocultacombo == 1) {
                $('#dllOficina').show();
                $('#trRubro').show();
            }
        } else {
            S
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        }
    });
    $("#hidCodigoGrupoEmpresarial").val(0);
    $("#hidGrupoFacturacion").val(0);
    //mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });
    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });

    //mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    $("#hidLicencia").val(0);
    $("#hidEstablecimiento").val(0);
    $("#hidResponsable").val(0);
});

function FValidarRucVacio() {
    if ($("#txtRuc").val() != "" || $("#txtlicencia").val() != "") {
        return 1;
    } else {
        alert("Ingrese Ruc o Licencia");
        return 0;
    }
}

var reloadEvento = function (idSel) {

    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    var socio = $("#hidResponsable").val();
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

var reloadEventoEst = function (idSel) {
    $("#hidEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
    //ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
};

function ObtenerNombreEstablecimiento(id, idLabelSetting) {
    $.ajax({
        url: '../General/ObtenerNombreEstablecimiento',
        type: 'POST',
        data: { codigoEst: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#" + idLabelSetting).html(dato.valor);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }
    });
}

var reloadEventoLicencia = function (idSel) {
    $("#hidLicencia").val(idSel);
    ObtenerNombreLicencia($("#hidLicencia").val());
};

function ObtenerNombreLicencia(idSel) {
    $.ajax({
        data: { codigoLic: idSel },
        url: '../General/ObtenerNombreLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lblLicencia").html(dato.valor);
            }
        }
    });
};


function ExportarReportef(tipo) {
    //var TipoReporte = getRadioButtonSelectedValue(document.frmTIPO.tipo);
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //var ESTADO = $("#divEstado").val();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //valida ingreso de RUC
    var ValidarRucVacio = FValidarRucVacio();
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    var LIC_ID = $("#hidLicencia").val() == null || "" ? "0" : $("#hidLicencia").val();

    //var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
    var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();


    var INV_ID = $("#Cod_Factura").val() == "" || null ? 0 : $("#Cod_Factura").val();
    var Serie = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();    
    var nro = $("#TxT_Nro").val() == "" || null ? 0 : $("#TxT_Nro").val();
    var Bec_id = $("#Cod_Bec").val() == "" || null ? 0 : $("#Cod_Bec").val();
    //var ruc = $("#txtRuc").val() == null ? "0" : $("#txtRuc").val();

    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 ) {
        $("#contenedor").show();
        $.ajax({
            url: '../Reporte_BEC_Inactivas/ReporteTipo',
            type: 'POST',
            data: {
                LIC_ID: LIC_ID, BPS_ID: SOCIO, INV_ID: INV_ID, Serie: Serie, nro: nro, Bec_id: Bec_id,
                Fini_Rechazo: ini, Ffin_Rechazo: fin, oficina_id: idoficina, formato: tipo
            },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                
                if (dato.result == 1) {

                    url = "../Reporte_BEC_Inactivas/ReporteBecInactivas?" +
          
             "formato=" + tipo;

                    if (tipo = 'PDF') {
                        $("#contenedor").show();
                        $('#externo').attr("src", url);
                    }else{
                        window.open(url);
                        $("#contenedor").hide();
                    }                  
                }
                else if (dato.result == 0) {
                    if (tipo = 'PDF') {
                        $('#externo').attr("src", vacio);
                        $("#contenedor").hide();
                        url = alert(dato.message);
                    } else {
                        $("#contenedor").hide();
                        url = alert(dato.message);
                    }
                                           
            }
            }
        });
    } else {

        $("#contenedor").hide();
    }
}


function ExportarReportef2(tipo) {
    //var TipoReporte = getRadioButtonSelectedValue(document.frmTIPO.tipo);
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //validacion de fechas 
    var validafecha = validate_fechaMayorQue(ini, fin);
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //var ESTADO = $("#divEstado").val();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //valida ingreso de RUC
    var ValidarRucVacio = FValidarRucVacio();
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    var LIC_ID = $("#hidLicencia").val() == null || "" ? "0" : $("#hidLicencia").val();

    //var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
    var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();


    var INV_ID = $("#Cod_Factura").val() == "" || null ? 0 : $("#Cod_Factura").val();
    var Serie = $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val();
    var nro = $("#TxT_Nro").val() == "" || null ? 0 : $("#TxT_Nro").val();
    var Bec_id = $("#Cod_Bec").val() == "" || null ? 0 : $("#Cod_Bec").val();

    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && ValidarRucVacio == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../Reporte_BEC_Inactivas/ReporteTipo',
            type: 'POST',
            data: {
                fini: ini, ffin: fin, BPS_ID: SOCIO, EST_ID: EST_ID, idoficina: idoficina, nombreoficina: nombreoficina
                , LIC_ID: LIC_ID, ESTADO: ESTADO, TipoReporte: TipoReporte
            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../Reporte_BEC_Inactivas/ReporteEstadoCuenta?" +
          "fini=" + ini + "&" +
          "ffin=" + fin + "&" +
          "BPS_ID=" + SOCIO + "&" +
           "EST_ID=" + EST_ID + "&" +
            "formato=" + tipo + "&" +
            "idoficina=" + idoficina + "&" +
            "nombreoficina=" + nombreoficina + "&" +
            "ESTADO=" + ESTADO + "&" +
            "LIC_ID=" + LIC_ID + "&" +
            "TipoReporte=" + TipoReporte;

                    window.open(url);
                    $("#contenedor").hide();
                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    } else {

        $("#contenedor").hide();
    }
}
function getRadioButtonSelectedValue(ctrl) {
    for (i = 0; i < ctrl.length; i++)
        if (ctrl[i].checked) return ctrl[i].value;
}
//var reloadEventoMod = function (idModSel) {
//    $("#lblModalidad").css('color', 'black');
//    $("#hidModalidad").val(idModSel);
//    obtenerNombreModalidad(idModSel, "lblModalidad");
//    ObtenerDatosModalidad($("#hidModalidad").val());
//    var modalidad = $("#lblModalidad").val();
//    $("#txtModalidad").val(modalidad);
//    var estado = validarPeriodocidad();
//};
var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidSerie").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
