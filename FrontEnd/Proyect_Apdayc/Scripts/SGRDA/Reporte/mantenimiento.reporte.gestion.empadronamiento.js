$(function () {

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtID_SOCIO').on("keypress", function (e) { return solonumeros(e); });
    $('#txtId_MODALIDAD').on("keypress", function (e) { return solonumeros(e); });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);

    //$("#divEstado").val(1);

    loadTipoGrupo('ddlGrupoModalidad', '0');

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
    $("#btnRefrescar").on("click", function () {
        limpiarReporte();
    });
    //mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    //mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });

    //mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });
    limpiarReporte();
});
function limpiarReporte() {
    //$("#txtID_SOCIO").val("");
    //$("#hidModalidad").val("");
    //$("#txtId_MODALIDAD").val("");
    $("#dllOficina").val("");
    //$("#lbResponsable").html('Seleccione un socio.');
    //$("#hidEdicionEnt").val(0);
    //$("#lbModalidad").html("Todos");
    //$("#ddlGrupoModalidad").val(0);
    //$("#divEstado").val(0);
    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecFinal").data("kendoDatePicker").value(new Date());

}
function ExportarReportef(tipo) {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var validafecha = validate_fechaMayorQue(ini, fin);

    //var ID_SOCIO = $("#hidEdicionEnt").val() == "" ? 0 : $("#hidEdicionEnt").val();
    //var GrupoModalidad = $("#ddlGrupoModalidad").val() == 0 ? "0" : $("#ddlGrupoModalidad").val();
    //var Id_Modalidad = $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val();
    //validacion de fechas 
    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    //var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //var estado = $("#divEstado").val();
    var TIPO_PAGO = $("#ddTIPO_PAGO").val();
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
    var url = "";
    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../Reporte_Gestion_Empadronamiento/ReporteTipo',
            type: 'POST',
            data: { finicio: ini, ffin: fin, formato: tipo, ID_OFICINA: idoficina, TIPO_PAGO: TIPO_PAGO },
            //ID_SOCIO: ID_SOCIO, ID_MODALIDAD: GrupoModalidad, Estado: estado
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../Reporte_Gestion_Empadronamiento/ReporteDeEmpadronamiento?" +
          "finicio=" + ini + "&" +
          "ffin=" + fin + "&" +
          "formato=" + tipo + "&" +
          //"ID_SOCIO=" + ID_SOCIO + "&" +
          //"ID_MODALIDAD=" + Id_Modalidad + "&" +
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
    } else {
        $("#contenedor").hide();
    }
}

function ExportarReportef2(tipo) {

    //var estado = $("#divEstado").val();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    var validafecha = validate_fechaMayorQue(ini, fin);
    var idoficina = $("#dllOficina").val();
    //var GrupoModalidad = $("#ddlGrupoModalidad").val() == 0 ? "0" : $("#ddlGrupoModalidad").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    var TIPO_PAGO = $("#ddTIPO_PAGO").val();

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);

    //var ID_SOCIO = $("#hidEdicionEnt").val() == "" ? 0 : $("#hidEdicionEnt").val();
    //var Id_Modalidad = $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val();
    //var selectedValue = $('#ddtipo').val();
    var url = "../Reporte_Gestion_Empadronamiento/ReporteDeEmpadronamiento?" +
          "finicio=" + ini + "&" +
          "ffin=" + fin + "&" +
          "formato=" + tipo + "&" +
          //"ID_SOCIO=" + ID_SOCIO + "&" +
          //"ID_MODALIDAD=" + Id_Modalidad + "&" +
          "ID_OFICINA=" + idoficina + "&" +
          "nombreoficina=" + nombreoficina;
    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../Reporte_Gestion_Empadronamiento/ReporteTipo',
            type: 'POST',
            data: { finicio: ini, ffin: fin, formato: tipo, ID_OFICINA: idoficina, TIPO_PAGO: TIPO_PAGO },
            //ID_SOCIO: ID_SOCIO, ID_MODALIDAD: GrupoModalidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    window.open(url);;
                    $("#contenedor").show();
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


var reloadEventoArt = function (idArtSel) {
    //msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    $("#hidArtistaSel").val(idArtSel);
    obtenerNombreArtista(idArtSel, "lblArtista");
};

var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    obtenerNombreSocio($("#lbResponsable").val(), 'lbResponsable');
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
var reloadEventoModalidad = function (idSel) {
    $("#hidModalidad").val(idSel);
    obtenerNombreModalidad(idSel, "lbModalidad");
    //obtenerNombreTarifaLabels(idSel, "lblTarifaDesc", "lblTemporalidadDesc");

};