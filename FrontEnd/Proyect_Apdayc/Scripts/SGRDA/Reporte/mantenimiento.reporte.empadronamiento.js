var K_ITEM = { CHOOSE: '--SELECCIONE--', ALL: '--TODOS--' };
$(function () {

    kendo.culture('es-PE');
    $("#tdMesCierre").hide();
    $("#tdCantMesCierre").hide();
    //$('#txtID_SOCIO').on("keypress", function (e) { return solonumeros(e); });
    //$('#txtId_MODALIDAD').on("keypress", function (e) { return solonumeros(e); });
    loadComboAnio('ddlAnioCierre', '0');
    $("#ddlAnioCierre").change(function () {
        if ($("#ddlAnioCierre").val() > 0) {
            $("#tdMesCierre").show();
            loadComboMesXAnio('ddlMesCierre', '0');
        }
        else {
            $("#tdMesCierre").hide();
        }
    });
   

    $("#divEstado").val(1);

    //loadTipoGrupo('ddlGrupoModalidad', '0');

    //$("input[name=valida]").hide();

    //-------------------------- EVENTO BOTONES ---------------------

    $('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);

        //if (oculta == true) {
        //    $('#trRubro').show();
        //    //$("input[name=valida]").prop('enabled',true);
        //} else {
        //    $('#trRubro').hide();
        //    //$("input[name=valida]").prop('disabled', true);

        //}
    }

    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();

    }
    else {
        //deshabilita el Select
        $('#tddllOficina').hide();
        $('#tddllSelect').hide();
        $('#dllOficina').prop('disabled', true);
    }

    //$("#dllOficina").change(function () {
    //	if ($(this).val() == "26") {
    //		if (ocultacombo == 1) {
    //			$('#dllOficina').show();
    //			$('#trRubro').show();
    //		}
    //	} else {
    //		$('#trRubro').hide();
    //		//$("input[name=valida]").prop('disabled', true);
    //	}
    //});
    //$("#btnRefrescar").on("click", function () {
    //    limpiarReporte();
    //});
    //mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    //mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });

    //mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });
    //limpiarReporte();
});
//function limpiarReporte() {
//    $("#txtID_SOCIO").val("");
//    $("#hidModalidad").val("");
//    $("#txtId_MODALIDAD").val("");
//    $("#dllOficina").val("");
//    $("#lbResponsable").html('Seleccione un socio.');
//    $("#hidEdicionEnt").val(0);
//    $("#lbModalidad").html("Todos");

//    $("#divEstado").val(0);
//    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
//    $("#txtFecFinal").data("kendoDatePicker").value(new Date());

//}
function ExportarReportef(tipo) {
    //var ini = $("#txtFecInicial").val();
    //var fin = $("#txtFecFinal").val();
    var anio = $("#ddlAnioCierre").val();
    var mes = $("#ddlMesCierre").val();
    //var validafecha = validate_fechaMayorQue(ini, fin);

    //var ID_SOCIO = $("#hidEdicionEnt").val() == "" ? 0 : $("#hidEdicionEnt").val();
    //var Id_Modalidad = $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val();
    //validacion de fechas 
    var url = "";
    //var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    //var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var estado = $("#divEstado").val();

    //var desc_artista = $("#txtDescArtista").val();
    //var cod_artista_sgs = $("#hidArtistaSel").val() == "" ? -1 : $("#hidArtistaSel").val();
    //var selectedValue = $('#ddtipo').val();
    //if (selectedValue === 'Ra') {
    //    var usu = $("#txtSociedad").val();
    //    var num = "";
    //} else if (selectedValue === 'Ru') {
    //    var usu = "";
    //    var num = $("#txtSociedad").val();
    //}
    //
    var vacio = "";
    //if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../REPORTE_DE_EMPRADRONAMIENTO/ReporteTipo',
            type: 'POST',
            data: { MES: mes, ANIO: anio, formato: tipo, ID_OFICINA: idoficina },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../REPORTE_DE_EMPRADRONAMIENTO/ReporteDeEmpadronamiento?" +
          "MES=" + mes + "&" +
          "ANIO=" + anio + "&" +
          "formato=" + tipo + "&" +
          "ID_OFICINA=" + idoficina + "&" +
          "nombreoficina=" + nombreoficina;
                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
    //} else {
    //    $("#contenedor").hide();
    //}
}

function ExportarReportef2(tipo) {

    var anio = $("#ddlAnioCierre").val();
    var mes = $("#ddlMesCierre").val();
    //var validafecha = validate_fechaMayorQue(ini, fin);
    var idoficina = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    var vacio = "";

    var nombreoficina = $("#dllOficina option:selected").text();

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);

    //var ID_SOCIO = $("#hidEdicionEnt").val() == "" ? 0 : $("#hidEdicionEnt").val();
    //var Id_Modalidad = $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val();
    //var selectedValue = $('#ddtipo').val();
    $("#contenedor").show();
    var load = '../Images/otros/loading.GIF';
    $('#externo').attr("src", load);
    var url = "../REPORTE_DE_EMPRADRONAMIENTO/ReporteDeEmpadronamiento?" +
        "MES=" + mes + "&" +
          "ANIO=" + anio + "&" +
          "formato=" + tipo + "&" +
          "ID_OFICINA=" + idoficina + "&" +
          "nombreoficina=" + nombreoficina;
    //if (validafecha == 1) {
    //$("#contenedor").show();
    //$("#contenedor").show();
        $.ajax({
            url: '../REPORTE_DE_EMPRADRONAMIENTO/ReporteTipo',
            type: 'POST',
            data: { MES: mes, ANIO: anio, formato: tipo, ID_OFICINA: idoficina },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    window.open(url);;
                    $("#contenedor").hide();
                    $('#externo').attr("src", vacio);

                } else if (dato.result == 0) {
                    $("#contenedor").hide();
                    $('#externo').attr("src", vacio);

                    url = alert(dato.message);
                }
            }
        });

    //} else {
    //    $("#contenedor").hide();
    //}
}


//var reloadEventoArt = function (idArtSel) {
//    //msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
//    $("#hidArtistaSel").val(idArtSel);
//    obtenerNombreArtista(idArtSel, "lblArtista");
//};
//var reloadEvento = function (idSel) {
//    $("#lbResponsable").val(idSel);
//    $("#hidEdicionEnt").val(idSel);
//    obtenerNombreSocio($("#lbResponsable").val(), 'lbResponsable');
//};

//function obtenerNombreSocio(idSel, control) {
//    $.ajax({
//        data: { codigoBps: idSel },
//        url: '../General/ObtenerNombreSocio',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                $("#" + control).html(dato.valor);
//            }
//        }
//    });
//}
//var reloadEventoModalidad = function (idSel) {
//    $("#hidModalidad").val(idSel);
//    obtenerNombreModalidad(idSel, "lbModalidad");
//    //obtenerNombreTarifaLabels(idSel, "lblTarifaDesc", "lblTemporalidadDesc");

//};


function loadComboAnio(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../REPORTE_DE_EMPRADRONAMIENTO/ListarAniosCierre',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, AnioCierre) {
                    if (AnioCierre.Value == valSel)
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadComboMesXAnio(control, valSel) {
    var anio = $("#ddlAnioCierre").val();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../REPORTE_DE_EMPRADRONAMIENTO/ListarMesesCierre',
        type: 'POST',
        data: { anio: anio },
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, AnioCierre) {
                    if (AnioCierre.Value == valSel)
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}