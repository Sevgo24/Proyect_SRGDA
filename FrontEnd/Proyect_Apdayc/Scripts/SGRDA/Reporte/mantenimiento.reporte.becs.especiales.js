var K_ITEM = { CHOOSE: '--SELECCIONE--', ALL: '--TODOS--' };
$(function () {
    Limpiar();
    $("#tdMesCierre").hide();
    $("#tdCantMesCierre").hide();
    $("#tdtxtCantMesCierre").hide();
    $("#tdRadioButton").hide(); 
    
    loadComboAnio('ddlAnioCierre', '0');
    $("#ddlAnioCierre").change(function () {
        if ($("#ddlAnioCierre").val() > 0) {
            $("#tdMesCierre").show();
            loadComboMesXAnio('ddlMesCierre', '0');
        }
        else {
            $("#tdMesCierre").hide();
            $("#tdCantMesCierre").hide();
            $("#tdtxtCantMesCierre").hide();
        }
    });

    $("#ddlMesCierre").change(function () {
        if ($("#ddlMesCierre").val() > 0) {
            //$('#txtCantMeses').on("keypress", function (e) { return solonumeros(e); });
            //$("#tdCantMesCierre").show();
            //$("#tdtxtCantMesCierre").show();
            $("#tdRadioButton").show();
            $("#tipo").prop('checked', true);
        }
        else {
            //$("#tdCantMesCierre").hide();
            //$("#tdtxtCantMesCierre").hide();
            $("#tdRadioButton").hide();
            $("#tdRadioButton2").hide();
        }
    });
    $("#btnPdf").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        //$('#externo').attr("src", ExportarReportef('PDF'));
        ExportarReportef('PDF');
    });

    $("#btnExcel").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });


});
function Limpiar(){
    //$("#txtCantMeses").val(0); 
    $("#ddlMesCierre").val(0);
    getRadioButtonSelectedValue(document.frmTIPO.tipo);
}


function loadComboAnio(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../BecEspeciales/ListarAniosCierre',
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
        url: '../BecEspeciales/ListarMesesCierre',
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

function ExportarReportef(tipo) {
    var anio = $("#ddlAnioCierre").val();
    var mes = $("#ddlMesCierre").val();
    var cant = 4;
    var tipoReporte = getRadioButtonSelectedValue(document.frmTIPO.tipo);
    var vacio = "";
    var url = "";
        $("#contenedor").show();
        $.ajax({
            url: '../BecEspeciales/ReporteTipo',
            type: 'POST',
            data: { formato: tipo, cant: cant, mes: mes, anio: anio, tipo: tipoReporte },
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                $('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    url = "../BecEspeciales/ReporteBecEspeciales?" +
          "cant=" + cant + "&" +
          "mes=" + mes + "&" +
          "formato=" + tipo + "&" +
          "anio=" + anio + "&" +
          "tipo=" + tipoReporte;
                    $("#contenedor").show();
                    $('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    $('#externo').attr("src", vacio);
                    $("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
}

function ExportarReportef2(tipo) {
    var anio = $("#ddlAnioCierre").val();
    var mes = $("#ddlMesCierre").val();
    var cant = 4;
    var tipoReporte = getRadioButtonSelectedValue(document.frmTIPO.tipo);

    if ($('#tipo').prop('checked'))
    {
        var cant = $("#tipo").val();
    }
    
    var url = "../BecEspeciales/ReporteBecEspeciales?" +
     "cant=" + cant + "&" +
          "mes=" + mes + "&" +
          "formato=" + tipo + "&" +
          "anio=" + anio + "&" +
          "tipo=" + tipoReporte;
        $("#contenedor").show();
        $.ajax({
            url: '../BecEspeciales/ReporteTipo',
            type: 'POST',
            data: { formato: tipo, cant: cant, mes: mes, anio: anio, tipo: tipoReporte },
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
}

function getRadioButtonSelectedValue(ctrl) {
    for (i = 0; i < ctrl.length; i++)
        if (ctrl[i].checked) return ctrl[i].value;
}