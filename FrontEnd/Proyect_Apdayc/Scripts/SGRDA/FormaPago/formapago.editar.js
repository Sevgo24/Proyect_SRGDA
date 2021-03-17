/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    var codeEdit = (GetQueryStringParams("set"));
    $('#txtFechaFija').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT01').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT02').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT03').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT04').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT05').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT06').on("keypress", function (e) { return solonumeros(e); });

    if (codeEdit === undefined) {
        $("#divTituloPerfil").html("Forma de Pago - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);

    } else {
        $("#divTituloPerfil").html("Forma de Pago - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
        DeshabilitaControles();
    }

    $("#btnEditar").on("click", function () {
        HabilitaControles();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../FORMASDEPAGO/Nuevo";
    });

    $("#btnDescartar").on("click", function () {
        DeshabilitaControles();
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../FORMASDEPAGO/Index";
    });

    $("#chkFecha").on("click", function () {
        var chk = $("#chkFecha").prop("checked");
        if (chk == true) {
            $("#txtFechaFija").val("");
            $("#txtFechaFija").removeAttr('disabled');
            $("#txtVT01").val("");
            $("#txtVT01").attr("disabled", "disabled");
            $("#txtVT02").val("");
            $("#txtVT02").attr("disabled", "disabled");
            $("#txtVT03").val("");
            $("#txtVT03").attr("disabled", "disabled");
            $("#txtVT04").val("");
            $("#txtVT04").attr("disabled", "disabled");
            $("#txtVT05").val("");
            $("#txtVT05").attr("disabled", "disabled");
            $("#txtVT06").val("");
            $("#txtVT06").attr("disabled", "disabled");
        }
        else {
            $("#txtFechaFija").val("");
            $("#txtFechaFija").attr("disabled", "disabled");
            $("#txtFechaFija").val("");
            $("#txtVT01").removeAttr('disabled');
            $("#txtVT02").removeAttr('disabled');
            $("#txtVT03").removeAttr('disabled');
            $("#txtVT04").removeAttr('disabled');
            $("#txtVT05").removeAttr('disabled');
            $("#txtVT06").removeAttr('disabled');
        }
    });
});


function limpiar() {
    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");
}

function limpiarValidacion() {
    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
}

function HabilitaControles() {
    $("#txtSigla").removeAttr('disabled');
    $("#txtDescripcion").removeAttr('disabled');
    $("#btnEditar").hide();
    $("#btnGrabar").show();
    $("#btnNuevo").hide();
    $("#btnDescartar").show();
    $("#chkFecha").removeAttr('disabled');

    var chk = $("#chkFecha").prop("checked");
    if (chk == true) {
        $("#txtFechaFija").removeAttr('disabled');
        $("#txtVT01").attr("disabled", "disabled");
        $("#txtVT02").attr("disabled", "disabled");
        $("#txtVT03").attr("disabled", "disabled");
        $("#txtVT04").attr("disabled", "disabled");
        $("#txtVT05").attr("disabled", "disabled");
        $("#txtVT06").attr("disabled", "disabled");
    }
    else {
        $("#txtFechaFija").attr("disabled", "disabled");
        $("#txtVT01").removeAttr('disabled');
        $("#txtVT02").removeAttr('disabled');
        $("#txtVT03").removeAttr('disabled');
        $("#txtVT04").removeAttr('disabled');
        $("#txtVT05").removeAttr('disabled');
        $("#txtVT06").removeAttr('disabled');
    }
    $("#chkPago").removeAttr('disabled');
    $("#chkRB").removeAttr('disabled');
    $("#chkRA").removeAttr('disabled');
    $("#chkTR").removeAttr('disabled');
}

function DeshabilitaControles() {
    $("#btnNuevo").show();
    $("#btnEditar").show();
    $("#btnGrabar").hide();
    $("#btnDescartar").hide();
    $("#txtSigla").attr("disabled", "disabled");
    $("#txtDescripcion").attr("disabled", "disabled");
    $("#chkFecha").attr("disabled", "disabled");
    $("#txtFechaFija").attr("disabled", "disabled");

    $("#txtVT01").attr("disabled", "disabled");
    $("#txtVT02").attr("disabled", "disabled");
    $("#txtVT03").attr("disabled", "disabled");
    $("#txtVT04").attr("disabled", "disabled");
    $("#txtVT05").attr("disabled", "disabled");
    $("#txtVT06").attr("disabled", "disabled");

    $("#chkPago").attr("disabled", "disabled");
    $("#chkRB").attr("disabled", "disabled");
    $("#chkRA").attr("disabled", "disabled");
    $("#chkTR").attr("disabled", "disabled");
}

var grabar = function () {
    var chkfecha = $("#chkFecha").prop("checked");
    var descorta = $("#txtSigla").val();
    var desc = $("#txtDescripcion").val();

    if (chkfecha == false) { $("#txtFechaFija").val(""); }

    if (descorta == "" || desc == "") {
        alert("Ingrese los datos para el registro.");
    }
    else {
        var FormaPago = {
            PAY_ID: $("#txtSigla").val(),
            DESCRIPTION: $("#txtDescripcion").val(),
            PAY_DATE_FIX: chkfecha,
            PAY_DATE_FIX_DAY: $("#txtFechaFija").val(),
            VTO1: $("#txtVT01").val(),
            VTO2: $("#txtVT02").val(),
            VTO3: $("#txtVT03").val(),
            VTO4: $("#txtVT04").val(),
            VTO5: $("#txtVT05").val(),
            VTO6: $("#txtVT06").val(),
            PAY_BANK: $("#chkPago").prop("checked"),
            PAY_BANK_RECEIPT: $("#chkRB").prop("checked"),
            PAY_AGE_RECEIPT: $("#chkRA").prop("checked"),
            PAY_TRANSFER: $("#chkTR").prop("checked")
        };
        $.ajax({
            url: "../FORMASDEPAGO/Actualizar",
            data: FormaPago,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../FORMASDEPAGO/";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
};

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    limpiar();
    $.ajax({
        url: "../FORMASDEPAGO/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtSigla").val(tipo.PAY_ID);
                    $("#txtDescripcion").val(tipo.DESCRIPTION);
                    if (tipo.PAY_DATE_FIX == false)
                        $("#chkFecha").prop('checked', false);
                    else
                        $("#chkFecha").prop('checked', true);
                    
                    if (tipo.PAY_DATE_FIX_DAY == 0) { tipo.PAY_DATE_FIX_DAY = ""; }
                    $("#txtFechaFija").val(tipo.PAY_DATE_FIX_DAY);

                    if (tipo.VTO1 == 0) { tipo.VTO1 = ""; }
                    $("#txtVT01").val(tipo.VTO1);

                    if (tipo.VTO2 == 0) { tipo.VTO2 = ""; }
                    $("#txtVT02").val(tipo.VTO2);

                    if (tipo.VTO3 == 0) { tipo.VTO3 = ""; }
                    $("#txtVT03").val(tipo.VTO3);

                    if (tipo.VTO4 == 0) { tipo.VTO4 = ""; }
                    $("#txtVT04").val(tipo.VTO4);

                    if (tipo.VTO5 == 0) { tipo.VTO5 = ""; }
                    $("#txtVT05").val(tipo.VTO5);

                    if (tipo.VTO6 == 0) { tipo.VTO6 = ""; }
                    $("#txtVT06").val(tipo.VTO6);

                    if (tipo.PAY_BANK == false)
                        $("#chkPago").prop('checked', false);
                    else
                        $("#chkPago").prop('checked', true);

                    if (tipo.PAY_BANK_RECEIPT == false)
                        $("#chkRB").prop('checked', false);
                    else
                        $("#chkRB").prop('checked', true);

                    if (tipo.PAY_AGE_RECEIPT == false)
                        $("#chkRA").prop('checked', false);
                    else
                        $("#chkRA").prop('checked', true);

                    if (tipo.PAY_TRANSFER == false)
                        $("#chkTR").prop('checked', false);
                    else
                        $("#chkTR").prop('checked', true);

                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
