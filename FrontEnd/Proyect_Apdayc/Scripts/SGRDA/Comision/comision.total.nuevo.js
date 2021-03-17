var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

$(function myfunction() {

    var eventoKP = "keypress";
    $('#txtOrden').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtValorInicial').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtValorFinal').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtValorComision').on(eventoKP, function (e) { return solonumeros(e); });


    $("#hidAccionMvRep").val("0");

    var id = GetQueryStringParams("id");
    //---------------------------------------------------------------
    if (id === undefined) {
        $("#divTitulo").html("Comisiones por totales / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        //nuevo();

    } else {
        $("#divTitulo").html("Comisiones por totales / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidAccionMvRep").val(1);
        $("#hidIdPrograma").val(id);
        ObtenerDatos(id);
    }

    $("#btnDescartar").on("click", function () {
        location.href = "../ComisionTotales/";
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#txtFechaUltimo").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#FechaRepIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#FechaRepFin").kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#tabs").tabs();
    $(".addRepresentante").on("click", function () { LimpiarRepresentante(); $("#mvRepresentante").dialog("open"); });
    $("#mvRepresentante").dialog({ autoOpen: false, width: 400, height: 230, buttons: { "Agregar": AddRepresentante, "Cancelar": function () { $("#mvRepresentante").dialog("close"); } }, modal: true });

    $("#mvRango").dialog({ autoOpen: false, width: 400, height: 250, buttons: { "Agregar": addRango, "Cancelar": function () { $("#mvRango").dialog("close"); } }, modal: true });

    mvInitBuscarSocioAux({ container: "ContenedormvBuscarSocioAux", idButtonToSearch: "btnBuscarBSaux", idDivMV: "mvBuscarSocioAux", event: "reloadEventoAux", idLabelToSearch: "lbResponsableAux" });
    loadFormatoComision('ddlFormatoComision', 0);
    loadTemporalidades("ddlTemporalidad", 0);

    loadDataRepresentante(0);

});

var reloadEventoAux = function (idSel) {
    $("#hidResponsableAux").val(idSel);
    var estado = validarRolAgenteRecaudo(idSel);
    if (estado)
        obtenerNombreSocio($("#hidResponsableAux").val());
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsableAux").html(dato.valor);
            }
        }
    });
};

function validarRolAgenteRecaudo(id) {
    var estado = false;
    $.ajax({
        data: { idAsociado: id },
        url: '../ComisionTotales/ValidacionPerfilAgenteRecaudo',
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadDataRepresentante(id) {
    loadDataGridTmp('ListarRepresentante', "#gridRepresentante", id);
}

function LimpiarRepresentante() {
    $("#hidAccionMvRep").val("0");
    $("#hidEdicionRep").val(0);
    $("#lbResponsableAux").html("Seleccione");
    $("#hidResponsableAux").val(0);
};

function AddRepresentante() {
    if ($("#FechaRepIni").val() == '' || $("#FechaRepFin").val() == '' || $("#lbResponsableAux").html() == 'Seleccione') {
        $('#FechaRepIni').css({ 'border': '1px solid red' });
        $('#FechaRepFin').css({ 'border': '1px solid red' });
        $('#lbResponsableAux').css({ 'border': '1px solid red' });
    }
    else {
        var IdAdd = 0;
        if ($("#hidAccionMvRep").val() === "1") IdAdd = $("#hidEdicionRep").val();

        var entidad = {
            sequence: IdAdd,
            Id: $("#hidResponsableAux").val(),
            Representante: $("#lbResponsableAux").html(),
            FechaInicio: $("#FechaRepIni").val(),
            FechaFin: $("#FechaRepFin").val()
        };

        if (ValidarRequeridosET()) {
            $.ajax({
                url: 'AddRepresentante',
                type: 'POST',
                data: entidad,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        loadDataRepresentante(0);
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
            $('#FechaRepIni').css({ 'border': '1px solid gray' });
            $('#FechaRepFin').css({ 'border': '1px solid gray' });
            $('#lbResponsableAux').css({ 'border': '1px solid gray' });
            $("#mvRepresentante").dialog("close");
        }
    }
};

function grabar() {
    if (ValidarRequeridos()) {
        var id = 0;
        var val = $("#hidOpcionEdit").val();
        if (val == 1) {
            if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidIdPrograma").val();
        }
        var item = {
            PRG_ID: id,
            PRG_DESC: $("#txtPrograma").val(),
            PRG_LASTL: $("#txtFechaUltimo").val(),
            RAT_FID: $("#ddlTemporalidad").val(),
            START: $("#txtFechaInicio").val(),
            ENDS: $("#txtFechaFin").val()
        };
        $.ajax({
            url: 'Insertar',
            data: item,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    location.href = "../ComisionTotales/";
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};

function ObtenerDatos(idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../ComisionTotales/Obtiene',
        data: { id: idSel },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var item = dato.data.Data;
                if (item != null) {
                    $("#hidIdPrograma").val(item.PRG_ID);
                    $("#txtPrograma").val(item.PRG_DESC);
                    loadTemporalidades('ddlTemporalidad', item.RAT_FID);
                    var d1 = $("#txtFechaUltimo").data("kendoDatePicker");
                    var valFechaUlt = formatJSONDate(item.PRG_LASTL);
                    d1.value(valFechaUlt);
                    var d2 = $("#txtFechaInicio").data("kendoDatePicker");
                    var valFechaIni = formatJSONDate(item.START);
                    d2.value(valFechaIni);
                    var d3 = $("#txtFechaFin").data("kendoDatePicker");
                    var valFechaFin = formatJSONDate(item.ENDS);
                    d3.value(valFechaFin);
                    loadDataRepresentante(0);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function delAddRepresentante(idDel) {
    $.ajax({
        url: 'DellAddRepresentante',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataRepresentante(0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarRepresentante() {
    $("#lbResponsableAux").html("Seleccione");
    $("#hidResponsableAux").val(0);
    $("#FechaRepIni").val('');
    $("#FechaRepFin").val('');
}

function updAddRepresentante(idUpd) {
    limpiarRepresentante();
    $.ajax({
        url: 'ObtieneRepresentanteTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidAccionMvRep").val("1");
                    $("#hidEdicionRep").val(en.sequence);

                    $("#hidResponsableAux").val(en.Id);
                    $("#lbResponsableAux").html(en.Representante);

                    $("#FechaRepIni").val(en.FechaInicio);
                    $("#FechaRepFin").val(en.FechaFin);

                    $("#mvRepresentante").dialog("open");
                } else {
                    alert("No se pudo obtener el representante para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function nuevoRango(id) {
    $("#txtOrden").val('');
    $("#txtValorInicial").val('');
    $("#txtValorFinal").val('');
    $("#ddlFormatoComision").val(0);
    $("#txtValorComision").val('');

    $('#txtOrden').css({ 'border': '1px solid gray' });
    $('#txtValorInicial').css({ 'border': '1px solid gray' });
    $('#txtValorFinal').css({ 'border': '1px solid gray' });
    $('#ddlFormatoComision').css({ 'border': '1px solid gray' });
    $('#txtValorComision').css({ 'border': '1px solid gray' });

    $("#hidAccionMvRan").val("0");
    $("#hidEdicionRan").val('0');
    $("#hidEdicionValRan").val(id);

    $('#mvRango').css('overflow', 'hidden');
    $("#mvRango").dialog("open");
};

function verDeta(id) {
    //alert(id);

    if ($("#expand" + id).attr('src') == '../Images/botones/less.png') {
        $("#expand" + id).attr('src', '../Images/botones/more.png');
        $("#expand" + id).attr('title', 'ver detalle.');
        $("#expand" + id).attr('alt', 'ver detalle.');
        $("#div" + id).css("display", "none");
        //alert("Oculta");
    }
    else {
        $("#expand" + id).attr('src', '../Images/botones/less.png');
        $("#expand" + id).attr('title', 'ocultar detalle.');
        $("#expand" + id).attr('alt', 'ocultar detalle.');
        $("#div" + id).css("display", "inline");
        //alert("Muestra");
    }
    return false;
}

function addRango() {
    var estado = true;
    if ($("#txtOrden").val() == '') {
        $('#txtOrden').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtOrden').css({ 'border': '1px solid gray' });

    if ($("#txtValorInicial").val() == '') {
        $('#txtValorInicial').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtValorInicial').css({ 'border': '1px solid gray' });

    if ($("#txtValorFinal").val() == '') {
        $('#txtValorFinal').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtValorFinal').css({ 'border': '1px solid gray' });

    if ($("#ddlFormatoComision").val() == '') {
        $('#ddlFormatoComision').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#ddlFormatoComision').css({ 'border': '1px solid gray' });

    if ($("#txtValorComision").val() == '') {
        $('#txtValorComision').css({ 'border': '1px solid red' });
        estado = false;
    } else
        $('#txtValorComision').css({ 'border': '1px solid gray' });

    if (estado) {
        var IdAdd = 0;
        if ($("#hidAccionMvRan").val() === "1") { IdAdd = $("#hidEdicionRan").val();
        }
        var entidad = {
            sequence: IdAdd,
            IdPrograma: $("#hidIdPrograma").val(),
            IdRepresentante: $("#hidEdicionValRan").val(),
            Orden: $("#txtOrden").val(),
            ValorInicial: $("#txtValorInicial").val(),
            ValorFinal: $("#txtValorFinal").val(),
            FormatoComision: $("#ddlFormatoComision").val(),
            ValorComisionAdicional: $("#txtValorComision").val(),
            PorcentajeAdicional: $("#txtValorComision").val()
        };

        $.ajax({
            url: '../ComisionTotales/AddRango',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataRepresentante($("#hidIdPrograma").val());
                }
            }
        });
        $("#hidAccionMvRan").val("0");
        $("#hidEdicionRan").val("0");
        $("#hidEdicionValRan").val("0");
        $("#mvRango").dialog("close");
    }
};

function updAddRango(idUpd, idrep) {
    limpiarRango();
    var seq = idUpd;
    var bpsId = idrep;

    $.ajax({
        url: 'ObtieneRangoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidAccionMvRan").val(1);
                    $("#hidEdicionRan").val(seq);

                    $("#hidEdicionValRan").val(bpsId);
                    $("#hidbpsId").val(bpsId);

                    $("#txtOrden").val(en.Orden);
                    $("#txtValorInicial").val(en.ValorInicial);
                    $("#txtValorFinal").val(en.ValorFinal);

                    if (en.FormatoComision == "Porcentaje (%)" || en.FormatoComision == "%") {
                        $("#hidValorComisionAdicional").val("");
                        $("#txtValorComision").val(en.PorcentajeAdicional);
                        $("#ddlFormatoComision").val("P");
                    }
                    else if (en.FormatoComision == "Monto (S/.)" || en.FormatoComision == "S/.") {
                        $("#hidPorcentajeAdicional").val("");
                        $("#txtValorComision").val(en.ValorComisionAdicional);
                        $("#ddlFormatoComision").val("M");
                    }

                    $("#mvRango").dialog("open");
                } else {
                    alert("No se pudo obtener el rango para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function delAddRango(idDel) {
    $.ajax({
        url: 'DellAddRango',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataRepresentante($("#hidIdPrograma").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarRango() {
    $("#txtOrden").val("");
    $("#txtValorInicial").val("");
    $("#txtValorFinal").val("");
    $("#ddlFormatoComision").val(0);
    $("#txtValorComision").val("");
}
