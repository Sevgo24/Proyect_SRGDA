var K_variables = {
    OkSimbolo: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    AlertaSimbolo: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    AlertMarqueCheck: 'Por favor marque el check de emision mensual para Facturar',
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
    Tres: 3,
    Cinco: 5,
    Dos: 2,
    Seis: 6,
    Once: 11,
    Vacio: "",
    MenosDos: -2
}


$(function () {

    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    $("#btnBuscarEmision").on("click", function () {
        ListarEmisionMensual();
    });

    $("#btnLimpiarEmision").on("click", function () {
        LimpiarEmision();
    });
    $("#btnCalcular").on("click", function () {
        Recalcular();
    });
    $("#btnFacturar").on("click", function () {
        if ($("#chkConFechaCrea").is(':checked'))
            AutoriazarGenerarEmision();
        else
            VentanaAviso(K_variables.AlertMarqueCheck, "OBSERVACIONES", K_variables.AlertaSimbolo);
    });

    $('#txtFecCreaInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    var fechaActual = new Date();
    $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);

    VentanaAviso("EN EL SIGUIENTE MODULO SE REALIZAN LAS EMISIONES MENSUALES ", "INFORMACION", K_variables.OkSimbolo);
    $('#chkConFechaCrea').click(function () {
        if ($(this).is(':checked')) {
            var datepicker = $("#txtFecCreaInicial").data("kendoDatePicker");
            var fechaActual = new Date();
            $("#txtFecCreaInicial").data("kendoDatePicker").value(fechaActual);
            $('#txtFecCreaInicial').data('kendoDatePicker').enable(false);
            $("#ddEstadoLic").prop('selectedIndex', K_variables.Uno);
            $("#ddEstadoLic").attr("disabled", "disabled");
        } else {
            $('#txtFecCreaInicial').data('kendoDatePicker').enable(true);
            $("#ddEstadoLic").removeAttr("disabled");
            $("#ddEstadoLic").prop('selectedIndex', K_variables.Cero);
        }
    });

    LimpiarEmision();
});

function ListarEmisionMensual() {

    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = FechaElegida.substring(K_variables.Tres, K_variables.Cinco);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año

    var oficina = $("#hidOficina").val() == K_variables.Vacio ? K_variables.Cero : $("#hidOficina").val();

    var estado = $("#ddEstadoLic").val();

    var licencia = $("#txtcodlic").val() == K_variables.Vacio ? K_variables.Cero : $("#txtcodlic").val();

    var socio = $("#hidResponsable").val() == K_variables.Vacio ? K_variables.Cero : $("#hidResponsable").val();

    $.ajax({
        data: {
            Oficina: oficina, Mes: mes, Anio: anio, Estado: estado, CodigoLicencia: licencia, CodigoSocio: socio
            //NumeroOperacion: $("#txtnumope").val() == K_variables.TextoVacio ? K_variables.TextoVacio : $("#txtnumope").val(),
        },
        url: '../AdministracionEmisionMensual/ListaEmisionMensual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                $("#grid").html(dato.message);
                $("#lblCantfacturas").html(dato.TotalFacturas);
                $("#lblMontfacturas").html(dato.valor);
                $("#lblCantFactValidas").html(dato.TotalFacturasValidas);


            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}

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

var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
};

function obtenerNombreConsultaOficina(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/obtenerNombreOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == K_variables.Si) {
                $("#lbOficina").html(dato.valor);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}

function ConfirmarAccion(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }, show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }


    });

}


function verDetalleLicenciaSocio(idsoc, idgroupfact, oficina) {
    if ($("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('src') == '../Images/botones/less.png') {
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('src', '../Images/botones/more.png');
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('title', 'ver detalle.');
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('alt', 'ver detalle.');
        $("#div" + idsoc + '-' + idgroupfact + '-' + oficina).css("display", "none");
    } else {
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('src', '../Images/botones/less.png');
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('title', 'ocultar detalle.');
        $("#expand" + idsoc + '-' + idgroupfact + '-' + oficina).attr('alt', 'ocultar detalle.');
        $("#div" + idsoc + '-' + idgroupfact + '-' + oficina).css("display", "inline");
        mostrarDetalleLicenciaSocio(idsoc, idgroupfact, oficina);
    }
    return false;
}


function mostrarDetalleLicenciaSocio(idsoc, idgroupfact, oficina) {

    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = FechaElegida.substring(K_variables.Tres, K_variables.Cinco);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año

    $.ajax({
        data: { CodigoSocio: idsoc, CodigoGrupoFact: idgroupfact, CodigoOficina: oficina, Mes: mes, Anio: anio },
        url: '../AdministracionEmisionMensual/ListarLicenciasSociosEmision',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#div" + idsoc + '-' + idgroupfact + '-' + oficina).html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}

function verDetallePlanificacionLicencia(id) {
    if ($("#expandDoc" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandDoc" + id).attr('src', '../Images/botones/more.png');
        $("#expandDoc" + id).attr('title', 'ver detalle.');
        $("#expandDoc" + id).attr('alt', 'ver detalle.');
        $("#divDoc" + id).css("display", "none");
    } else {
        $("#expandDoc" + id).attr('src', '../Images/botones/less.png');
        $("#expandDoc" + id).attr('title', 'ocultar detalle.');
        $("#expandDoc" + id).attr('alt', 'ocultar detalle.');
        $("#divDoc" + id).css("display", "inline");

        mostrarDetalleLicenciaPlaneamiento(id);
    }
    return false;
}

function mostrarDetalleLicenciaPlaneamiento(id) {

    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = FechaElegida.substring(K_variables.Tres, K_variables.Cinco);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año

    $.ajax({
        data: { CodigoLicencia: id, Mes: mes, Anio: anio },
        url: '../AdministracionEmisionMensual/ListarPlaneamientoLicenciaEmision',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {

                $("#divDoc" + id).html(dato.message);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}


function LimpiarEmision() {

    $("#hidOficina").val("");
    $("#lbOficina").html("SELECCIONE OFICINA");
    $("#txtcodlic").val("");
    $("#grid").html('');
    $("#lblCantfacturas").html('...');
    $("#lblCantFactValidas").html('...');
    $("#lblMontfacturas").html('...');
    $("#lbResponsable").html('SELECCIONE SOCIO');
    $("#hidResponsable").val("");
    $("#ddEstadoLic").prop('selectedIndex', 0);
    $("#chkConFechaCrea").prop("checked", false);

}
function VerLicencia(lic_id) {

    window.open('../Licencia/Nuevo?set=' + lic_id, '_blank');
}


function Recalcular() {

    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = FechaElegida.substring(K_variables.Tres, K_variables.Cinco);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año

    var oficina = $("#hidOficina").val() == K_variables.Vacio ? K_variables.Cero : $("#hidOficina").val();

    var Licencia = $("#txtcodlic").val() == K_variables.Vacio ? K_variables.Cero : $("#txtcodlic").val();

    $.ajax({
        data: { CodigoLicencia: Licencia, mes: mes, year: anio, Oficina: oficina },
        url: '../AdministracionEmisionMensual/ActualizarMontoDescuentosCaracteristicasLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "INFORMACION", K_variables.OkSimbolo);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

}

function AutoriazarGenerarEmision() {



    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = ("00" + FechaElegida.substring(K_variables.Tres, K_variables.Cinco)).substr(K_variables.MenosDos, K_variables.Dos);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año





    ConfirmarAccion('SE GENERAR LA EMISION MENSUAL PARA EL MES DE ' + mes + ' del año ' + anio
    + ' SIN FILTROS , (ESTA SEGURO(A) DE CONTINUAR ?',
    function () {
        var resp = ValidarPermisionEmisionMensual();
        if (resp == K_variables.Si)
            GenerarEmision();

    },
    function () { },
    'Confirmar');



}


function GenerarEmision() {

    $("#btnFacturar").attr("disabled", true);

    var FechaElegida = $("#txtFecCreaInicial").val();

    var mes = FechaElegida.substring(K_variables.Tres, K_variables.Cinco);//mes

    var anio = FechaElegida.substring(K_variables.Seis, K_variables.Once); // año

    var oficina = $("#hidOficina").val() == K_variables.Vacio ? K_variables.Cero : $("#hidOficina").val();


    $.ajax({
        data: { Oficina: oficina, mes: mes, anio: anio },
        url: '../AdministracionEmisionMensual/GenerarFacturacionMensual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "INFORMACION", K_variables.OkSimbolo);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });
}

function ModificarPermisoLicencias(CodigoLicencia, CodigoSocio, CodigoGrupoFacturacion, CodigoOficina) {


    $.ajax({
        data: { CodigoLicencia: CodigoLicencia },
        url: '../AdministracionEmisionMensual/ActualizarEstadoLicenciaEmision',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                VentanaAviso(dato.message, "INFORMACION", K_variables.OkSimbolo);
                mostrarDetalleLicenciaSocio(CodigoSocio, CodigoGrupoFacturacion, CodigoOficina);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
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
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            }
        }

    });

};


function ValidarPermisionEmisionMensual() {

    var Retorna = K_variables.Cero;
    $.ajax({
        //data: { Oficina: oficina, mes: mes, anio: anio },
        url: '../AdministracionEmisionMensual/ValidarPermisoEmisionMensual',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == K_variables.Si) {
                //VentanaAviso(dato.message, "INFORMACION", K_variables.OkSimbolo);
                Retorna = K_variables.Si;
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }

    });

    return Retorna;
}