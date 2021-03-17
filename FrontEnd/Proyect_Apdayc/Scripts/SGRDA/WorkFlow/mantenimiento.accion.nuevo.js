/************************** INICIO CONSTANTES****************************************/
var K_FECHA = 22;
var K_WIDTH = 500;
var K_HEIGHT = 200;
var K_ID_POPUP_DIR = "mvAgente";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/************************** INICIO CARGA********************************************/
$(function () {
    /*Inicializador de PopUp de Agentes*/
    $("#hidAccionMvAge").val("0");
    //FIN  DEL INICIALIZADOR//

    var codeEdit = (GetQueryStringParams("set"));

    if (codeEdit === undefined) {
        $("#divTituloPerfil").html("WorkFlow - Nueva Acción de Proceso");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#btnNuevo").hide();

        //CARGA LOS COMBOS//
        LoadTipoAccion('ddlAccion', 0);
        LoadTipoDato('ddlDato', 0);
        LoadProceso('ddlProceso', 0);
        LoadTipoAgente("ddlAgente", 0);
    } else {
        $("#divTituloPerfil").html("WorkFlow - Actualizar Acción de Proceso");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidBpsId").val(codeEdit);
        ObtenerDatos(codeEdit);
    }

    //FIN DE LA CARGA

    $("#tabs").tabs();

    $("#mvAgente").dialog({ autoOpen: false, width: 400, height: 150, buttons: { "Agregar": addAgente, "Cancelar": function () { $("#mvAgente").dialog("close"); } }, modal: true });
    $(".addAgente").on("click", function () { limpiarAgente(); $("#mvAgente").dialog("open"); });


    $("#btnGrabar").on("click", function () {
        insertar();
    });
    $("#btnVolver").on("click", function () {
        location.href = "../Action/";
    });
    $("#btnNuevo").on("click", function () {
        location.href = "Nuevo";
    });
});

function ObtenerDatos(id) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Action/Obtener',
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $("#txtNombre").val(tipo.WRKF_ANAME);
                    $("#txtEtiqueta").val(tipo.WRKF_ALABEL);
                    $("#txtDescripcion").val(tipo.WRKF_ADESC);
                    LoadTipoAccion('ddlAccion', tipo.WRKF_ATID);
                    LoadTipoDato('ddlDato', tipo.WRKF_DTID);
                    LoadProceso('ddlProceso', tipo.PROC_ID);
                    if (tipo.WRKF_AAPLIC == 'A')
                        $("#chkAccion").prop('checked', true);
                    else
                        $("#chkAccion").prop('checked', false);

                    /*Inicio de Carga inicial de tabs*/
                    loadDataAgente();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadDataAgente() {
    loadDataGridTmp('ListarAgente', "#gridAgente");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function limpiarAgente() {
    $("#hidAccionMvAge").val("0");
    $("#hidEdicionAge").val("");
    LoadTipoAgente("ddlAgente", 0);
}

function addAgente() {
    var agente = $("#ddlAgente").val();
    var IdAdd = 0;
    if ($("#hidAccionAge").val() == "1") IdAdd = $("#hidEdicionAge").val();

    var entidad = {
        Id: IdAdd,
        IdAgente: $("#ddlAgente").val(),
        NombreAgente: $("#ddlAgente option:selected").text()
    };
    if (agente != 0) {
        $.ajax({
            url: 'AddAgente',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataAgente();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
                else if (dato.result == 2) {
                    alert(dato.message);
                }
            }
        });
        $("#avisoAgente").html('');
        $("#mvAgente").dialog("close");
    } else {
        $("#avisoAgente").html('<br/><span style="color:red;">Error: Seleccione un Agente.</span><br/>');
    }
}

function insertar() {
    var accion = $("#ddlAccion").val();
    var dato = $("#ddlDato").val();
    var proceso = $("#ddlProceso").val();
    var chk = $("#chkAccion").prop("checked");
    var AAPLIC = "";
    if (chk == true) {
        AAPLIC = "A";
    }
    else { AAPLIC = "M"; }

    if (accion == 0) { accion = null; }
    if (proceso == 0) { proceso = null; }

    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        if (accion == 0) {
            alert("Seleccione el Tipo de Acción");
            return;
        }
        var entidad = {
            WRKF_AID: $("#hidBpsId").val(),
            WRKF_ANAME: $("#txtNombre").val(),
            WRKF_ALABEL: $("#txtEtiqueta").val(),
            WRKF_ADESC: $("#txtDescripcion").val(),
            WRKF_ATID: accion,
            WRKF_DTID: dato,
            PROC_ID: proceso,
            WRKF_AAPLIC: AAPLIC //$("#chkAccion").prop("checked")
        };
        $.ajax({
            url: "Insertar",
            data: entidad,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    location.href = "../Action/Index";
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
}

function delAddAgente(idDel) {
    $.ajax({
        url: 'DellAddAgente',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataAgente();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddAgente(idUpd) {
    $.ajax({
        url: 'ObtieneAgenteTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionAge").val("1");
                    $("#hidEdicionAge").val(param.Id);
                    //$("#ddlAgente").val(param.IdAgente);
                    LoadTipoAgente('ddlAgente', param.IdAgente);
                    $("#mvAgente").dialog("open");
                } else {
                    alert("No se pudo obtener la información del Agente para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}