/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    $("#chkFecha").prop("checked", true);

    //$("#txtFechaFija").keydown(function (e) {
    //    var txtFecha = $("#txtFechaFija").val();
    //    alert(txtFecha);
    //});

    $('#txtFechaFija').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT01').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT02').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT03').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT04').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT05').on("keypress", function (e) { return solonumeros(e); });
    $('#txtVT06').on("keypress", function (e) { return solonumeros(e); });

    //VALIDA EL CHECKBOX Y HABILITA LOS CUADROS DE TEXTO//
    var chk = $("#chkFecha").prop("checked");
    if (chk == true) {
        $("#txtFechaFija").val("");
        $('#txtFechaFija').removeAttr('disabled');
        $("#txtVT01").attr("disabled", "disabled");
        $("#txtVT02").attr("disabled", "disabled");
        $("#txtVT03").attr("disabled", "disabled");
        $("#txtVT04").attr("disabled", "disabled");
        $("#txtVT05").attr("disabled", "disabled");
        $("#txtVT06").attr("disabled", "disabled");
    }
    //FIN DE VALIDACIÓN//
    
    $("#chkFecha").on("click", function () {
        var chk = $("#chkFecha").prop("checked");
        if (chk == true) {
            $("#txtFechaFija").val("");
            $('#txtFechaFija').removeAttr('disabled');
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
            $('#txtFechaFija').attr("disabled", "disabled");
            $("#txtFechaFija").val("");
            $("#txtVT01").removeAttr('disabled');
            $("#txtVT02").removeAttr('disabled');
            $("#txtVT03").removeAttr('disabled');
            $("#txtVT04").removeAttr('disabled');
            $("#txtVT05").removeAttr('disabled');
            $("#txtVT06").removeAttr('disabled');
        }
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#btnVolver").on("click", function () {
        location.href = "../FORMASDEPAGO/";
    });
});

//function Key()
//{
//    var txtFecha = $("#txtFechaFija").val();
//    if ($("#txtFechaFija").val() > "31") {
//        txtFecha = "";
//        alert("Digito incorrecto");
//    }
//    else {
//        return;
//    }
//}

function limpiar() {

    $("#txtDescripcion").val("");
    $("#txtDesCorta").val("");
    $("#txtPDescripcion").val("");

}

function limpiarValidacion() {

    msgErrorB("aviso", "", "txtDesCorta");
    msgErrorB("aviso", "", "txtPDescripcion");
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
            url: "../FORMASDEPAGO/Insertar",
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

