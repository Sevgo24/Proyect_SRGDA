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

    //$("input[name=valida]").hide();

    //-------------------------- EVENTO BOTONES ---------------------

    $('#trRubro').hide();
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        //Buscar2();
        obtenerModalidadesSeleccionadas();
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        obtenerModalidadesSeleccionadas();
        ExportarReportef2('EXCEL');
    });
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
            $('#trRubro').show();
            //$("input[name=valida]").prop('enabled',true);
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);

        }
    }
    if (ocultacombo == 1) {
        //Llena Combo con la funcion creada en el comun.drowdownlist
        loadComboOficina('dllOficina', '0');
        $('#dllOficina').prop('enabled', true);
        $('#dllOficina').show();
    }
    else {
        //deshabilita el Select
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
                $('#trRubro').show();
            }
        } else {
            $('#trRubro').hide();
            //$("input[name=valida]").prop('disabled', true);
        }
    });
    $("#dllOficina").on("change", function () {
        var dato = $("#dllOficina").val();
        BuscarModalidadesXOficina(dato);
    });
    $('#divEstado').val(-1);
});
function BuscarModalidadesXOficina(dato) {
    var soc = dato;
    $.ajax({
        url: '../RegistroVentaNC/ConsultaModalidadXOficina',
        type: 'POST',
        data: {
            IdOficina: soc

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridModalidad").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridModalidad").html('');
                alert(dato.message);
            }
        }
    });

    //alert(soc);

    // }

}

function obtenerModalidadesSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblModalidadXOficina input[type=checkbox]').each(function () {
        if (this.checked) {
            var id = $(this).val();
            ReglaValor[contador] = { MOG_ID: id };
            contador += 1;
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../RegistroVentaNC/ModalidadesSeleccionadasTemporalesOriginal',
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
}
function ExportarReportef(tipo) {
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
    var estado = $('#divEstado').val();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
    //    rubro = vacio;
    //}
    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1) {
        $("#contenedor").show();

        $.ajax({
            url: '../RegistroVentaNC/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina,ESTADO:estado },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../RegistroVentaNC/ReporteRegistroVenta?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                        + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                    + "&" + "EStADO=" + estado;

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

    var estado = $('#divEstado').val();

    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo

    nombreoficina = nombreoficina.replace('&', 'Y');
    //Si la validacion de fecha es Igual a 1 entonces :
    if (validafecha == 1) {
        $("#contenedor").show();
        $.ajax({
            url: '../RegistroVentaNC/ReporteTipo',
            type: 'POST',
            data: { fini: ini, ffin: fin, rubro: rubro, idoficina: idoficina, nombreoficina: nombreoficina, ESTADO: estado },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../RegistroVentaNC/ReporteRegistroVenta?" + "fini=" + ini + "&" + "ffin=" + fin + "&"
                       + "formato=" + tipo + "&" + "rubro=" + rubro + "&" + "idoficina=" + idoficina + "&" + "nombreoficina=" + nombreoficina
                    + "&" + "EStADO=" + estado;
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
