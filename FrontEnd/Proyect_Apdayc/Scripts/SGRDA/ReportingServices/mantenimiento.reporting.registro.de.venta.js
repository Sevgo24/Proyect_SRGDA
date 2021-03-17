/************************** INICIO CARGA********************************************/
var Rubros = "";
var oficina = "";
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

    //$("input[name=valida]").hide();
    RecuperarOficinaId();
    //-------------------------- EVENTO BOTONES ---------------------

    //$('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        obtenerModalidadesSeleccionadas();
        ExportarReportef('PDF');
    });

    //$("#btnExcel").on("click", function () {
    //    var estadoRequeridos = ValidarRequeridos();
    //    obtenerModalidadesSeleccionadas();
    //    ExportarReportef2('EXCEL');
    //});
    //--------------------------FUNCIONES DE  EL DROWDOWN LIST
    //valida si es ADMIN O CONTABILIDAD
    //var oculta = validarOficinaReporte();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();
    //oculta segun sea el caso
    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);

        if (oculta == true) {
            //$('#trRubro').show();
            //$("input[name=valida]").prop('enabled',true);
        } else {
            //$('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);

        }
    }
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    }
    else {
        //deshabilita el Select
        $('#tddllOficina').hide();
        $('#dllOficina').prop('disabled', true);
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    }

    $("#dllOficina").change(function () {
        if ($(this).val() == "26") {
            if (ocultacombo == 1) {
                $('#dllOficina').show();
                //$('#trRubro').show();
            }
        } else {
            //$('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        }
    });

    $("#dllOficina").on("change", function () {
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);

    });
});

function BuscarModalidadesXOficina(dato) {
    var soc = dato;
    $.ajax({
        url: '../RegistroVenta/ConsultaModalidadXOficina',
        type: 'POST',
        data: {
            IdOficina: soc
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //$('#TddRubro').show();
                $("#gridModalidad").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridModalidad").html('');
                //alert(dato.message);
            }
        }
    });
}

function check1() {
    if ($('#checkALL').prop('checked')) {
        var dato = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    } else {
        $(".Check").removeAttr("checked");
    }
}

function checkValue() {
    for (var i = 0; i < 30; i++) {
        //if ($('#checkValue' + i + '').prop('checked')) {
        //    obtenerModalidadesSeleccionadas();
        //    ListarModalidadDet();
        //} else {
        //    obtenerModalidadesSeleccionadas();
        //    ListarModalidadDet();
        //}
        if ($('#checkValue' + i + '').prop('checked') == false) {
            document.getElementById("checkALL").checked = false;
        }
    }
}

function obtenerModalidadesSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblModalidadXOficina input[type=checkbox]').each(function () {
        if (this.checked) {
            var id = $(this).val();
            ReglaValor[contador] = { MOG_ID: id };
            if (contador == 0) {
                Rubros = id;
            } else if (contador != 0) {
                Rubros += "," + id;
            }
            contador += 1;
        }
    });
    //alert(ReglaValor)
    //$('#tblModalidadXOficina tr').each(function () {
    //        var IdNro = $(this).find(".IDModOri").html();
    //        var idEst = $(this).find(".IDModOri").html();     
    //        if (IdNro != null) {              
    //            if ($('#chkModOrigen' + idEst).is(':checked')) {
    //                ReglaValor[contador] = {
    //                    MOG_ID: idEst,
    //                };
    //                contador += 1;
    //            }
    //        }
    //    //});
    //});
    //alert(contador);

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    //if (contador > 0) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../RegistroVenta/ModalidadesSeleccionadasTemporalesOriginal',
        data: ReglaValor,
        //codigo malo..-:"
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else if (dato.result == 0) {

                alert(dato.message);
            }
        }
    });
    //} else {
    //    alert("Debe selecionar antes de continuar.");
    //}
}
function getRadioButtonSelectedValue(ctrl) {
    for (i = 0; i < ctrl.length; i++)
        if (ctrl[i].checked) return ctrl[i].value;
}

function ExportarReportef(tipo) {

    var TipoReporte = getRadioButtonSelectedValue(document.frmTIPO.tipo);
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
    //obtiene el valor del combo
    //var idoficina
    //idoficina = $("#dllOficina").val();
    //var nombreoficina = $("#dllOficina option:selected").text();

    var oficina_id = "";
    var OficinaAdmin = $("#lblOficinaTit").text();
    var NombreUsuario = $("#lblUsuarioTit").text();
    var nombreoficina = $("#dllOficina option:selected").text() == "" ? OficinaAdmin : $("#dllOficina option:selected").text();
    if (nombreoficina == '--SELECCIONE--') {
        nombreoficina = 'TODAS LAS OFICINAS'
    }
    nombreoficina = nombreoficina.replace('&', 'Y');
    if (oficina == "-1") {
        oficina_id = $("#dllOficina").val() == null ? 0 : $("#dllOficina").val();
    } else {
        oficina_id = oficina;
    }


    //Valida Seleccion del combo

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var validaselecciondelCombo = 1;
    //if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
    //    rubro = vacio;
    //}

    alert(Rubros)
    //nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        if (TipoReporte == "D") {
            url = "http://ws2016-002/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fRegistroVenta%2fRSRegistroVentaFinalV1&" +
         "F1=" + ini + "&" +
         "F2=" + fin + "&" +
         "OFICINA=" + oficina_id + "&" +
         "territorio=" + 0 + "&" +
         "PARAMETROS=" + Rubros + "&" +
         "Rubros=" + Rubros + "&" +
          "OficinaNombre=" + nombreoficina + "&" +
          "Usuario=" + NombreUsuario + "&" +
           "rs:Command=Render&rc:Parameters=false&rc:Zoom=Page Width ";
        } else {
            url = "http://ws2016-002/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fRegistroVenta%2fRSRegistroVentaFinalV1&" +
         "F1=" + ini + "&" +
         "F2=" + fin + "&" +
         "OFICINA=" + oficina_id + "&" +
         "territorio=" + 0 + "&" +
         "PARAMETROS=" + Rubros + "&" +
         "Rubros=" + Rubros + "&" +
          "OficinaNombre=" + nombreoficina + "&" +
          "Usuario=" + NombreUsuario + "&" +
           "rs:Command=Render&rc:Parameters=false&rc:Zoom=Page Width ";
        }
      
        alert(url)
        $("#contenedor").show();
        $('#externo').attr("src", url);
    } else if (dato.result == 0) {
        $('#externo').attr("src", vacio);
        $("#contenedor").hide();
        url = alert(dato.message);
    }

}

function ExportarReportef2(tipo) {
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
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del comb

    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    var validaselecciondelCombo = 1;
    //if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
    //    rubro = vacio;
    //}
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1 && validaselecciondelCombo == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../RegistroVenta/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina, formato: tipo },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../RegistroVenta/ReporteRegistroVenta?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                       + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina;
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

function RecuperarOficinaId() {
    $.ajax({
        url: '../Reporte_FacturasCanceladas/RecuperarOficina',
        data: {},
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                oficina = dato.valor;
            } else {
                alert(dato.message);
            }
        }
    })
}
