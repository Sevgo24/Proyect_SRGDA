$(function () {

    $("#mvModificaEmision").dialog({ autoOpen: false, width: 400, height: 300, buttons: { "Grabar": Editar, "Cancelar": function () { $(this).dialog("close");} }, modal: true });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });

    $("#btnBuscar").on("click", function () {
        ListarEmisionMensual();
    });

    $("#btnLimpiar").on("click", function () {
        LimpiarEmisionMensual();
    });

    $("#btnNuevo").on("click", function () {
        LimpiarPopUp();
        Registrar();
        $("#mvModificaEmision").dialog("open");

    });


    LimpiarEmisionMensual();
    $('#txtdia').on("keypress", function (e) { return solonumeros(e); });
    $('#txtdiaof').on("keypress", function (e) { return solonumeros(e); });
    //$('#txtrangoini').kendoDatePicker({ format: "MM" });


    $("#txtrangoini").kendoTimePicker({ format: "HH:mm", timeFormat: "HH:mm" });
    $("#txtrangofin").kendoTimePicker({ format: "HH:mm", timeFormat: "HH:mm" });


    $("#txthoraini").kendoTimePicker({ format: "HH:mm", timeFormat: "HH:mm" });
    $("#txthorafin").kendoTimePicker({ format: "HH:mm", timeFormat: "HH:mm" });
    //$('#txtrangofin').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());

    //$("#txtrangoini").data("kendoDatePicker").value(fechaActual);
    //var d = $("#txtrangoini").data("kendoDatePicker").value();



    //$("#txtrangofin").data("kendoDatePicker").value(fechaFin);
    //var d2 = $("#txtrangofin").data("kendoDatePicker").value();



});

function ListarEmisionMensual() {

    var oficina = $("#txtnomoff").val() ==""? "":$("#txtnomoff").val();
    var dia = $("#txtdia").val() == "" ? "0" : $("#txtdia").val();
    var fecha_ini = $("#txtrangoini").val() == "" ? "" : $("#txtrangoini").val();
    var fecha_fin = $("#txtrangofin").val() == "" ? "" : $("#txtrangofin").val();
    var estado = $("#ddlestado").val(); 
    
    
    $.ajax({
        data: { NOM_OFF: oficina, DIA: dia, FECHA_INICIO: fecha_ini, FECHA_FIN: fecha_fin, ESTADO: estado },
        url: '../EmisionMensual/ListarEmisionMensual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //$("#lbOficina").html(dato.valor);
                //alert("Listando EMISION MENSUAL");
                $("#grid").html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}



function LimpiarEmisionMensual() {
    $("#txtnomoff").val('');
    $("#txtdia").val('');
    $("#txtrangoini").val('00:00');
    $("#txtrangofin").val('23:30');
    $("#ddlestado").prop('selectedIndex', 1);

    ListarEmisionMensual();
}
function LimpiarPopUp() {
    $("#hidValue").val('');
    $("#txtnomof").val('');
    $("#txtdiaof").val('');
    $("#txthoraini").val('');
    $("#txthorafin").val('');
    $("#lblnombreOficina").html('');
    $("#lbOficina").html('Seleccione Oficina');
    $("#hidOficina").val('');
}


function InactivarEmision(ID) {
    //alert("Inactivando :" + ID);

    Confirmar('ESTA SEGURO DE ACTIVAR/INACTIVAR LA VALIDACCION MENSUAL SELECCIONADA',
    function () {

        var resp = ValidaEmisionMensual(ID);

        if (resp == 1) {
            $.ajax({
                data: { ID: ID },
                url: '../EmisionMensual/InactivarEmisionMensual',
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        alert("SE ACTIVO/INACTIVO LA VALIDACION SELECCIONADA");
                        ListarEmisionMensual();
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        }
    },
    function () { },
    'Confirmar');

}

function ModificarEmision(ID) 
{
    //alert("Modificando :" + ID);
    $("#mvModificaEmision").dialog("open");
    $("#hidValue").val(ID);
    //$("#hidLicId").val(lic_id);
    //$('#txtmontoDesc').on("keypress", function (e) { return solonumeros(e); });
    //ObtieneDatosDescuento();
    ObtenerEmisionIDVALUE(ID);
}



function ObtenerEmisionIDVALUE(ID) {


    $.ajax({
        data: {ID: ID },
        url: '../EmisionMensual/ObtieneEmisionMensualIDValue',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var emi = dato.data.Data;
                $("#lblnombreOficina").html(emi.DESC_VALUE);
                $("#txtnomof").val(emi.DESC_VALUE);
                $("#txtdiaof").val(emi.DIA);
                $("#txthoraini").val(emi.RANGO_INICIAL);
                $("#txthorafin").val(emi.RANGO_FINAL);
                reloadEventoOficina(emi.OFF_ID);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}



function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
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
        }


    });

}


function Editar() {
    //alert("MODIFICANDO");
    var ID = $("#hidValue").val() == "" ? 0 : $("#hidValue").val();
    var oficina = $("#txtnomof").val();
    var dia = $("#txtdiaof").val() ;
    var fecha_ini = $("#txthoraini").val();
    var fecha_fin = $("#txthorafin").val();
    var off_id = $("#hidOficina").val();
    var resp = ValidaEmisionMensual(ID);

    if (oficina != "" && dia != "" && fecha_ini != "" && fecha_fin != "" && off_id!="") {
        if (resp==1) {
            $.ajax({
                data: { ID: ID, NOMBRE_OFI: oficina, DIA: dia, RANGO_INI: fecha_ini, RANGO_FIN: fecha_fin, OFF_ID: off_id },
                url: '../EmisionMensual/EditarEmisionMensual',
                type: 'POST',
                async: false,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {

                        $("#mvModificaEmision").dialog("close");
                        alert(dato.message);
                        ListarEmisionMensual();

                    } else if (dato.result == 0) {
                        alert(dato.message);
                        $("#mvModificaEmision").dialog("close");
                    }
                }
            });
        }
    } else {
        alert("TODOS LOS CAMPOS DEBEN SER RELLENADOS");
    }
}



function ValidaEmisionMensual(ID) {

    var resp;
    ObtenerEmisionIDVALUE(ID);
    var dia = $("#txtdiaof").val() == "" ? 0 : $("#txtdiaof").val();
    var fecha_ini = $("#txthoraini").val();
    var fecha_fin = $("#txthorafin").val();

    $.ajax({
        data: { ID: ID, DIA: dia, FECHA_INI: fecha_ini, FECHA_FIN: fecha_fin },
        url: '../EmisionMensual/ValidaEmisionMensual',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result==1) {
                resp = dato.result;

            } else  if (dato.result==2){
                alert(dato.message);
            } else {
                alert(dato.message);
            }
        }
    });

    return resp;
}


function Registrar() {
    alert("Registrando");
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
            if (dato.result == 1) {
                $("#lbOficina").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}