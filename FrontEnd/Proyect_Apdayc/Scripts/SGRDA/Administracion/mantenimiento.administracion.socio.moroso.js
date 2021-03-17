var K_WIDTH_OBS2 = 250;
var K_HEIGHT_OBS2 = 300;
var K_variables = {
    OkSimbolo: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    AlertaSimbolo: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    SinSimbolo: '',
    Si: 1,
    No: 0,
    Cero: 0,
    MenosUno: '-1',
    CeroLetra: '0',
    TextoVacio: "",
    MinimoHeight: 75,
    blindDuration: 1000,
    hideDuration: 1000,
    Uno: 1,
    MsgElegirSocio:"Por favor elija un Socio "
}
var K_TITULO = {
}

$(function () {

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecCreaFinal').kendoDatePicker({ format: "dd/MM/yyyy" });


    var fechaActual = new Date();


    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecCreaInicial").data("kendoDatePicker").value();

    $("#txtFecCreaFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecCreaFinal").data("kendoDatePicker").value();


    $("#mvAgregarSocioMoroso").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": GrabarSocioMoroso,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvAgregarSocioMoroso").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });
    $("#mvModificaSocio").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2,
        height: K_HEIGHT_OBS2,
        buttons: {
            "Grabar": ActualizarEstadoAprobacionSocio,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvModificaSocio").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio2", idButtonToSearch: "btnBuscarRegBS", idDivMV: "mvBuscarSocio", event: "reloadEventoMoroso", idLabelToSearch: "lbResponsableMoroso" });

    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });

    

    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX BOTONES
    $("#btnBuscarSocioMoroso").on("click", function () {
        $("#contenedor").hide();
        ListarSociosMorosos();
    });
    $("#btnNuevoSocioMoroso").on("click", function () {
        LimpiarPopUp();
        $("#mvAgregarSocioMoroso").dialog("open");
    });

    $("#btnLimpiar").on("click", function () {
        LimpiarSocioMoroso();
    });

    $("#btnPdf").on("click", function () {

        $("#contenedor").show();

        ExportarReporteSocioM('PDF');
    });

    $("#btnExcel").on("click", function () {

        ExportarReporteSocioM('EXCEL');
    });

    LimpiarSocioMoroso();
    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZA LA CONSULTA Y REGISTROS   DE USUARIOS RENUENTES ", "INFORMACION", K_variables.OkSimbolo);
});


function VentanaAviso(dialogText, dialogTitle, SIMBOLO) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + SIMBOLO + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: K_variables.MinimoHeight,
        show: {
            effect: "blind",
            duration: K_variables.blindDuration
        },
        hide: {
            effect: "explode",
            duration: K_variables.hideDuration
        }


    });
}
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
            if (dato.result == K_variables.Si) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

};

function ListarSociosMorosos() {

    $.ajax({
        data: {
            CodisoSocio: $("#hidResponsable").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidResponsable").val(),
            ConFecha: $("#chkConFechaCrea").is(':checked') == true ? 1 : 0, // Modificara por una Lista.
            FechaInicial: $("#txtFecCreaInicial").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaInicial").val(),
            FechaFinal: $("#txtFecCreaFinal").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtFecCreaFinal").val(),
            Tipo: $("#dlltiposocio").val(),
            Estado: $("#ddlestadosocio").val()


        },
        url: '../AdministracionSocioMoroso/ListarSociosMoroso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#grid").html(dato.message);
                //$("#lblCantCobros").html(dato.Code);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}

function GrabarSocioMoroso() {

    if ($("#hidResponsableMoroso").val() > K_variables.Cero) {
        $.ajax({
            data: {
                CodigoSocio: $("#hidResponsableMoroso").val(),
                Descripcion: $("#txtDescUsuMoroso").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtDescUsuMoroso").val(), // Modificara por una Lista.
                CodigoLicencia: $("#hidLicencia").val() == K_variables.TextoVacio ? K_variables.Cero : $("#hidLicencia").val(), // Modificara por una Lista.
                
            },
            url: '../AdministracionSocioMoroso/InsertarSocioMoroso',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == K_variables.Si) {
                    $("#mvAgregarSocioMoroso").dialog("close");

                    VentanaAviso(dato.message, "RESULTADOS", K_variables.OkSimbolo);
                    ListarSociosMorosos();
                } else {
                    VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
                }
            }
        });
    } else {
        VentanaAviso(K_variables.MsgElegirSocio, "OBSERVACIONES", K_variables.AlertaSimbolo);
    }
}
var reloadEventoMoroso = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    $("#hidResponsableMoroso").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                //alert(dato.valor);
                $("#lbResponsableMoroso").html(dato.valor);

                //$("#hidResponsable").val('');
                $("#lbResponsable").html('');
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

};


function LimpiarPopUp() {

    $("#hidResponsableMoroso").val('');
    $("#lbResponsableMoroso").html('SELECCIONE');
    $("#txtDescUsuMoroso").val('');
    
}


function InactivarSocioMoroso(IdSocio) {

    $.ajax({
        data: { CodigoSocio: IdSocio },
        url: '../AdministracionSocioMoroso/InactivarUsuarioMoroso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "RESULTADOS", K_variables.OkSimbolo);
                ListarSociosMorosos();
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}


function editarSocio(CodigoSocioMoroso) {


    $("#mvModificaSocio").dialog("open");

    ObtenerSocioMoroso(CodigoSocioMoroso);

}

function ObtenerSocioMoroso(CodigoSocioMoroso) {

    $.ajax({
        data: { ID: CodigoSocioMoroso },
        url: '../AdministracionSocioMoroso/ObtienerSocioMoroso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                var entidad = dato.data.Data;

                $("#hidIdSocioMoroso").val(entidad.CodigoSocioMoroso);
                $("#lblsocioMoroso").html(entidad.Socio);
                $("#lbllicenciasocioMoroso").html(entidad.NombreEvento);
                $("#txtDescResUsuMoroso").val(entidad.Descripcion);
                $("#ddlestadoSocioMoroso").val(entidad.Estado);

            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}

function ActualizarEstadoAprobacionSocio() {
    //var CodigoSocioMoroso = $("#hidIdSocioMoroso").val();
    //var EstadoSocioMoroso = $("#ddlestadosocio").val();
    var CodigoSocioMoroso = $("#hidIdSocioMoroso").val();
    var EstadoSocioMoroso = $("#ddlestadoSocioMoroso").val();

    if (CodigoSocioMoroso > K_variables.Cero) {

        $.ajax({
            data: { CodigoSocio: CodigoSocioMoroso, Estado :EstadoSocioMoroso},
            url: '../AdministracionSocioMoroso/ActualizarEstadoSocioMoroso',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == K_variables.Si) {
                    VentanaAviso(dato.message, "RESULTADOS", K_variables.OkSimbolo);
                    $("#mvModificaSocio").dialog("close");
                    //ListarSociosMorosos();
                } else {
                    VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
                }
            }

        });
    } else {
        alert("Por favor seleccione una Opcion a Actualizar");
    }
}

//LICENCIA - BUSQ. GENERAL
function reloadEventoLicencia(idSel) {
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
            if (dato.result == K_variables.Uno) {
                $('#hidLicencia').val(idSel);
                $('#hidMoneda').val(dato.message);
                $("#lblLicencia").html(dato.valor);
                loadMonedas('ddlMoneda', dato.message);
                $('#gridFacturaManual').html('');
            }
        }
    });
};

function LimpiarSocioMoroso() {

    $("#lbResponsable").html('SELECCIONE..');
    $("#hidResponsable").val(K_variables.CeroLetra);
    $("#dlltiposocio").prop('selectedIndex', K_variables.Cero);
    $("#ddlestadosocio").prop('selectedIndex', K_variables.Cero);
    $("#ddlestadoSocioMoroso").prop('selectedIndex', K_variables.Cero);
    $("#chkConFechaCrea").prop("checked", false);
    
}

function ExportarReporteSocioM(tipo) {

    var url = "";

    $.ajax({
        url: '../AdministracionSocioMoroso/ReporteTipo',
        type: 'POST',
        async: false,
        //data: { formato: tipo },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == K_variables.Uno) {

                //alert("ingreso");
                $("#grid").html(K_variables.TextoVacio);

                url = "../AdministracionSocioMoroso/ReporteSociosMorosos?" +
                "formato=" + tipo;

                $("#contenedor").show();
                $('#externo').attr("src", url);

                if (tipo == "EXCEL")
                    $("#contenedor").hide();


            } else if (dato.result == K_variables.Cero) {
                //alert("khe");

                //$('#externo').attr("src", vacio);
                $("#contenedor").hide();
                alert(dato.message);
            }
        }
    });

}
