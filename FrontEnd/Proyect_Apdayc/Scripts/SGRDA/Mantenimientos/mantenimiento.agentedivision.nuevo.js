/************************** INICIO CONSTANTES****************************************/
var k_TIPO_DIVISIONES = { ADMINISTRATIVO: "ADM", GEO: "GEO" }
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_OBS = 580;
var K_HEIGHT_OBS = 295;
var K_WIDTH_DOC = 610;
var K_HEIGHT_DOC = 250;
var K_WIDTH_PAR = 480;
var K_HEIGHT_PAR = 260;
var K_ID_POPUP_DIR = "mvDireccion";

/************************** INICIO CARGA********************************************/
$(function () {
    kendo.culture('es-PE');
    var id = (GetQueryStringParams("id"));

    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    loadRol('ddlRol', 0);
    $('#ddlDivision').append($("<option />", { value: '0', text: '--SELECCIONE--' }));
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#hidIdAgenteRecaudo').val(0);
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $('#divTituloPerfil').html("Agentes de Recaudado - Nuevo");
        $("#txtFecInicial").data("kendoDatePicker").value(new Date());
        var d = $("#txtFecInicial").data("kendoDatePicker").value();
        $("#txtFecFinal").data("kendoDatePicker").value(new Date());
        var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();
        $('#txtFecInicial').data('kendoDatePicker').enable(true);
        $('#txtFecFinal').data('kendoDatePicker').enable(false);
        $('#txtFecFinal').val('');

        // -- AGENTE DE RECAUDO
        mvInitBuscarAgenteRecaudo({ container: "ContenedormvBuscarRecaudador", idButtonToSearch: "bntAgenteRecaudo", idDivMV: "mvBuscarAgenteRecaudo", event: "BuscarRecaudador", idLabelToSearch: "lblAgenteRecaudo", tipoPersona: "N" });
        $(".addGestor").on("click", function () {
            $("#mvBuscarAgenteRecaudo").dialog("open");
        });
    } else {
        $('#hidIdAgenteRecaudo').val(id);
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $('#divTituloPerfil').html("Agentes de Recaudado - Actualización");
        $('#txtFecInicial').data('kendoDatePicker').enable(true);
        $('#txtFecFinal').data('kendoDatePicker').enable(true);
        $('#bntAgenteRecaudo').hide();
        obtenerDatos(id);
    }
    //---------------------------------------------------------------------------------

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    //$("#ddlDivision").change(function () {
    //    var index = $("#ddlDivision option:selected").index();
    //    if (index != 0) {
    //        $('#aviso').html('');
    //        $("#ddlDivision").css({ 'border': '1px solid gray' });
    //    }
    //});

    //$("#ddlTipoIdentificacion").change(function () {
    //    $("#hidExitoValNumero").val("0");
    //    msgErrorB("divResultValidarDoc", "");
    //    var index = $("#ddlTipoIdentificacion option:selected").index();
    //    if (index == 0) {
    //        DesHabControles(true);
    //    } else {
    //        DesHabControles(false);
    //        getValorConfigNumDoc($("#ddlTipoIdentificacion").val());
    //    }
    //});

    //$("#txtNroDocumento").on("keypress", function (e) {
    //    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    //    if (key == 13)
    //        consultarDocumento();
    //});
    $("#tabs").tabs();

    //-------------------------- EVENTO BOTONES ------------------------------------    
    $("#btnVolver").on("click", function () {
        document.location.href = '../AgenteDivision/';
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../AgenteDivision/Nuevo';
    }).button();

    $("#btnGrabar").on("click", function () {
        insertar();
    }).button();

    //-------------------------- CARGA - DROPDOWNLIST -TABS -------------------------
    //loadTipoDoc("ddlTipoDocumento", 0);
    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);
    //--------------------------------------------------------------------------------

    //-------------------------- CARGA TABS -----------------------------------------    
    //------OBSERVACION
    //loadComboTipoObs();
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Grabar": addObservacion, "Cancelar": function () { $("#mvObservacion").dialog("close"); $('#ddlTipoObservacion').css({ 'border': '1px solid gray' }); $('#txtObservacion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $("#tabObs").on("click", function () {
        loadDataObservacion();
    });
    //------PARAMETRO
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_PAR, height: K_HEIGHT_PAR, buttons: { "Grabar": addParametro, "Cancelar": function () { $("#mvParametro").dialog("close"); $('#txtconipcion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $("#tabPar").on("click", function () {
        loadDataParametro();
    });

    // -- BUSQUEDA GENERAL OFICINA
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });

    // -- DIRECCIÓN
    initFormDireccion({
        id: K_ID_POPUP_DIR,
        parentControl: "divDireccion",
        width: 850,
        height: 400,
        evento: addDireccion,
        modal: true
    });

    //$("#mvListarDireccion").dialog({ autoOpen: false, width: 650, height: 250, buttons: { "Agregar": addDuplicarDireccion, "Cancelar": function () { $("#mvListarDireccion").dialog("close"); } }, modal: true });
    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });


});
//-------------------------- TAB - DIRECCIÓN   ---------------------------------- 
function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
}

function addDireccion() {
    var IdAdd = 0;
    if ($("#hidAccionMvDir").val() == "1") IdAdd = $("#hidEdicionDir").val()
    if (ValidarRequeridosMV()) {
        var direccion = {
            Id: IdAdd,
            TipoDireccion: $("#ddlTipoDireccion").val(),
            RazonSocial: "",
            Territorio: $("#ddlTerritorio").val(),
            CodigoUbigeo: $("#hidCodigoUbigeo").val(),
            Referencia: $("#txtReferencia").val(),
            CodigoPostal: $("#ddlCodPostal").val(),
            TipoUrba: $("#ddlUrbanizacion").val(),
            Urbanizacion: $("#txtUrb").val(),
            Numero: $("#txtNro").val(),
            Manzana: $("#txtMz").val(),
            Lote: $("#txtLote").val(),
            TipoDepa: $("#ddlDepartamento").val(),
            NroPiso: $("#txtNroPiso").val(),
            TipoAvenida: $("#ddlAvenida").val(),
            Avenida: $("#txtAvenida").val(),
            TipoEtapa: $("#ddlEtapa").val(),
            Etapa: $("#txtEtapa").val(),
            TipoDireccionDesc: $("#ddlTipoDireccion option:selected").text(),
            TipoTerritorioDes: $("#ddlTerritorio option:selected").text(),
            TipoUrbDes: $("#ddlUrbanizacion option:selected").text(),
            TipoDepaDes: $("#ddlDepartamento option:selected").text(),
            TipoAvenidaDes: $("#ddlAvenida option:selected").text(),
            TipoEtapaDes: $("#ddlEtapa option:selected").text(),
            DescripcionUbigeo: $("#txtUbigeo").val()
        };
        $.ajax({
            url: 'AddDireccion',
            type: 'POST',
            data: direccion,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataDireccion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });

        $("#mvDireccion").dialog("close");
    }
}

function delAddDireccion(idDel) {
    $.ajax({
        url: 'DellAddDireccion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDireccion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddDireccion(idUpd) {
    limpiarDireccion();

    $.ajax({
        url: 'ObtieneDireccionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {

                var direccion = dato.data.Data;
                if (direccion != null) {

                    $("#hidAccionMvDir").val("1");
                    $("#hidEdicionDir").val(direccion.Id);

                    $("#txtUrb").val(direccion.Urbanizacion);
                    $("#txtNro").val(direccion.Numero == 0 ? "" : direccion.Numero);
                    $("#txtMz").val(direccion.Manzana);


                    $("#ddlTipoDireccion").val(direccion.TipoDireccion);
                    $("#ddlTerritorio").val(direccion.Territorio);
                    $("#txtReferencia").val(direccion.Referencia);
                    $("#ddlCodPostal").val(direccion.CodigoPostal);
                    $("#ddlUrbanizacion").val(direccion.TipoUrba);

                    $("#txtLote").val(direccion.Lote);
                    $("#ddlDepartamento").val(direccion.TipoDepa);
                    $("#txtNroPiso").val(direccion.NroPiso);
                    $("#ddlAvenida").val(direccion.TipoAvenida);
                    $("#txtAvenida").val(direccion.Avenida);
                    $("#ddlEtapa").val(direccion.TipoEtapa);
                    $("#txtEtapa").val(direccion.Etapa);

                    $("#hidCodigoUbigeo").val(direccion.CodigoUbigeo);
                    $("#txtUbigeo").val(direccion.DescripcionUbigeo);




                    $("#" + K_ID_POPUP_DIR).dialog("open");
                } else {
                    alert("No se pudo obtener la direccion para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}

function limpiarDireccion() {
    $("#txtUrb").val("");
    $("#hidAccionMvDir").val("0");
    $("#hidEdicionDir").val("0");


    $("#txtNro").val("");
    $("#txtMz").val("");

    $("#ddlTipoDireccion").val("");
    // $("#ddlTerritorio").val("");
    $("#txtReferencia").val("");
    $("#ddlCodPostal").val("");
    $("#ddlUrbanizacion").val("");

    $("#txtLote").val("");
    $("#ddlDepartamento").val("");
    $("#txtNroPiso").val("");
    $("#ddlAvenida").val("");
    $("#txtAvenida").val("");
    $("#ddlEtapa").val("");
    $("#txtEtapa").val("");

    $("#hidCodigoUbigeo").val("");
    $("#txtUbigeo").val("");

}

function actualizarDirPrincipal(idDir) {

    $.ajax({
        url: 'SetDirPrincipal',
        data: { idDir: idDir },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            //  alert(dato.message);
            if (!(dato.result == 1)) {
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}

function actualizarDirPrincipal(idDir) {
    $.ajax({
        url: 'SetDirPrincipal',
        data: { idDir: idDir },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            //  alert(dato.message);
            if (!(dato.result == 1)) {
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}

function delAddDireccion(idDel) {
    $.ajax({
        url: 'DellAddDireccion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDireccion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

//-------------------------- TAB - OBSERVACION ---------------------------------- 
function addObservacion() {
    var estado = true;
    if ($("#ddlTipoObservacion").val() == 0) {
        $('#ddlTipoObservacion').css({ 'border': '1px solid red' });
        estado = false;
    }

    if ($("#txtObs").val() == '') {
        $('#txtObs').css({ 'border': '1px solid red' });
        estado = false;
    }

    if (estado) {
        var IdAdd = 0
        IdAdd = $("#hidIdObservacion").val() != 0 ? $("#hidIdObservacion").val() : 0;
        var observacion = {
            Id: IdAdd,
            TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
            Observacion: $("#txtObs").val(),
            TipoObservacionDesc: $("#ddlTipoObservacion option:selected").text()
        };

        $.ajax({
            url: '../AgenteDivision/AddObservacion',
            type: 'POST',
            data: observacion,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataObservacion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $('#txtObs').css({ 'border': '1px solid gray' });
        $("#mvObservacion").dialog("close");
    }
}

function delAddObservacion(idDel) {
    $.ajax({
        url: '../AgenteDivision/DellAddObservacion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataObservacion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddObservacion(idUpd) {
    limpiarObservacion();
    $.ajax({
        url: '../AgenteDivision/ObtieneObservacionTmp',
        data: { id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {
                    $("#hidIdObservacion").val(obs.Id);
                    $("#ddlTipoObservacion").val(obs.TipoObservacion);
                    $("#txtObs").val(obs.Observacion);
                    $("#mvObservacion").dialog("open");
                } else {
                    alert("No se pudo obtener la observación para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiarObservacion() {
    $("#txtObs").val("");
    $("#hidIdObservacion").val(0);
    $("#ddlTipoObservacion").val(0);
    $("#ddlTipoObservacion").css({ 'border': '1px solid gray' });
    $("#txtObs").css({ 'border': '1px solid gray' });
}

var grabarObs = function () {
    var idOff = id;
    var msjOfi = $("#txtObs").val();
    var idTipo = $("#ddlTipoObservacion").val();

    $.ajax({
        url: '../AgenteDivision/InsertarObs',
        data: { id: idOff, msj: msjOfi, tipo: idTipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                loadDataObs();
                $("#ModalNeoObs").dialog("close");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
};

function loadDataObservacion() {
    loadDataGridTmp('ListarObservacion', "#gridObservacion");
}

//-------------------------- TAB - PARAMETRO -------------------------------------  
function loadDataParametro() {
    loadDataGridTmp('ListarParametro', "#gridParametro");
}

function addParametro() {
    if ($("#txtDesParam").val() == '') {
        $('#txtDescripcion').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        IdAdd = $("#hidIdParametro").val() != 0 ? $("#hidIdParametro").val() : 0;
        var entidad = {
            Id: IdAdd,
            TipoParametro: $("#ddlTipoParametro option:selected").val(),
            Descripcion: $("#txtDesParam").val(),
            TipoParametroDesc: $("#ddlTipoParametro option:selected").text()
        };
        $.ajax({
            url: '../AgenteDivision/AddParametro',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataParametro();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvParametro").dialog("close");
        $('#txtDesParam').css({ 'border': '1px solid gray' });
    }
}

function delAddParametro(idDel) {
    $.ajax({
        url: '../AgenteDivision/DellAddParametro',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataParametro();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddParametro(idUpd) {
    limpiarParametro();
    $.ajax({
        url: '../AgenteDivision/ObtieneParametroTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidIdParametro").val(param.Id);
                    $("#ddlTipoParametro").val(param.TipoParametro);
                    $("#txtDesParam").val(param.Descripcion);
                    $("#mvParametro").dialog("open");
                } else {
                    alert("No se pudo obtener el parametro para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiarParametro() {
    $("#txtDesParam").val("");
    $("#hidIdParametro").val(0);
}

//---------------------------------------------------------------------------------- 
function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST',
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        },
        complete: function () {
            $(idGrilla).html(dato.message);
        }
    });
}

// **************************************************************************************************************************
function ValidarRegistro() {
    var validacion = true;
    var msj = '';
    if ($("#hidOficina").val() == '0') {
        validacion = false;
        msj += 'Seleccione una agencia de recaudo.\r\n';
        $("#lbOficina").css({ 'color': 'red' });
    }

    if ($("#ddlDivision").val() == '0') {
        validacion = false;
        msj += 'Seleccione una división administrativa.\r\n';
        $("#ddlDivision").css({ 'border': '1px solid red' });
    } else {
        $("#ddlDivision").css({ 'border': '1px solid gray' });
    }

    if ($("#hidAgenteRecaudo").val() == '0') {
        validacion = false;
        msj += 'Seleccione una agente de recaudo.\r\n';
        $("#lblAgenteRecaudo").css({ 'color': 'red' });
    }

    if ($("#ddlRol").val() == '0') {
        validacion = false;
        msj += 'Seleccione un rol.';
        $("#ddlRol").css({ 'border': '1px solid red' });
    } else {
        $("#ddlRol").css({ 'border': '1px solid gray' });
    }

    if (!validacion)
        alert(msj);

    return validacion;

}

function insertar() {
    if (ValidarRegistro()) {
        var id = $('#hidIdAgenteRecaudo').val();
        var AgenteRecaudo = {
            COLL_OFF_ID: id,
            OFF_ID: $("#hidOficina").val(),
            DAD_ID: $("#ddlDivision").val(),
            BPS_ID: $("#hidAgenteRecaudo").val(),
            ROL_ID: $("#ddlRol").val(),
            START: $("#txtFecInicial").val(),
            ENDS: $("#txtFecFinal").val()
        };

        $.ajax({
            url: "../AgenteDivision/Insertar",
            data: AgenteRecaudo,
            type: "POST",
            datatype: "JSON",
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    document.location.href = '../AgenteDivision/';
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
};

function obtenerDatos(idDivision) {
    $.ajax({
        url: "../AgenteDivision/Obtener",
        data: { id: idDivision },
        type: 'GET',
        dataType: "JSON",
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var agente = dato.data.Data;
                var idDivision = agente.DAD_ID;
                loadComboDivisionesXOficina('ddlDivision', agente.OFF_ID, idDivision);
                //loadComboDivisionesXOficina('ddlDivision', agente.OFF_ID, '47');

                $("#hidOficina").val(agente.OFF_ID);
                $("#lbOficina").html(agente.OFF_NAME);

                $("#hidAgenteRecaudo").val(agente.BPS_ID);
                $("#lblAgenteRecaudo").html(agente.RECAUDADOR);

                loadRol('ddlRol', agente.ROL_ID);
                $("#txtFecInicial").val(agente.F_INICIAL);
                $("#txtFecFinal").val(agente.F_FINAL);

                loadDataObservacion();
                loadDataDireccion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

// PoPup - BUSQ. GENERAL
// RECAUDADOR - BUSQ. GENERAL
var BuscarRecaudador = function (idSel) {
    $("#hidAgenteRecaudo").val(idSel);
    obtenerNombreRecaudador(idSel, 'lblAgenteRecaudo');
}
function obtenerNombreRecaudador(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
                $("#lblAgenteRecaudo").css({ 'color': 'black' });
            }
        }
    });
}

// OFICINA - BUSQ. GENERAL
var reloadEventoOficina = function (idSel) {
    $("#hidOficina").val(idSel);
    obtenerNombreConsultaOficina($("#hidOficina").val());
}
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
                $("#lbOficina").css({ 'color': 'black' });
                loadComboDivisionesXOficina('ddlDivision', idSel, 0);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

// --------------------------------------------------------------
//function loadDataAgente() {
//    loadDataGridTmp('ListarAgenteRecaudo', "#gridAgente");
//}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

//var reloadEvento = function (idSel) {
//    $("#hidBPSid").val(idSel);
//    //alert($("#hidBPSid").val());
//    var validacionAgente = ValidarAgenteRecaudo($("#hidBPSid").val());
//    if (validacionAgente)
//        addAgenteRecaudo($("#hidBPSid").val());
//};

//function addAgenteRecaudo(id) {
//    $.ajax({
//        url: 'AddAgenteRecaudo',
//        data: { Id: id },
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                loadDataAgente();
//            } else if (dato.result == 0) {
//                alert(dato.message);
//                result = false;
//            }
//        }
//    });
//}

//function ValidarAgenteRecaudo(id) {

//    $.ajax({
//        url: '../General/ValidarAgenteRecaudo',
//        type: 'POST',
//        data: { Id: id },
//        async: false,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                result = true;
//            } else if (dato.result == 0) {
//                alert(dato.message);
//                result = false;
//            }
//        }
//    });
//    return result;
//}

//function ValidarDivision(id) {

//    $.ajax({
//        url: '../General/ValidarDivision',
//        type: 'POST',
//        data: { Id: id },
//        async: false,
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                result = true;
//            } else if (dato.result == 0) {
//                alert(dato.message);
//                result = false;
//            }
//        }
//    });
//    return result;
//}

//****************************  FUNCIONES ****************************
//function grabarRecaudador() {
//    var validacion = ValidarRequeridos();
//    if (validacion) {
//        var id = $("#hidCodigoAgente").val();
//        if (id != '') {
//            insertar();
//        } else {
//            alert("No existe agente.");
//        }
//    }
//}



//**************************************************************************************


//function consultarDocumento() {
//    var estado = validarLongitudNumDoc();
//    if (estado)
//        ObtenerSocio();
//}

//function DesHabControles(estado) {
//    $("#txtNroDocumento").prop('disabled', estado);
//    $("#btnBuscar").prop('disabled', estado);
//    $("#txtNombre").prop('disabled', !estado);
//    if (estado) {
//        $("#txtNroDocumento").val('');
//        $("#txtNroDocumento").css({ 'border': '1px solid gray' });
//        $("#txtNroDocumento").removeClass('requerido');
//        $("#txtNombre").addClass('requerido');
//    }
//    else {
//        limpiarDatosAgente();
//        $("#txtNroDocumento").addClass('requerido');
//        $("#txtNombre").removeClass('requerido');
//        $("#txtNombre").css({ 'border': '1px solid gray' });
//    }
//}

//function ObtenerSocio() {
//    var idtipo = $("#ddlTipoIdentificacion").val();
//    var doc = $("#txtNroDocumento").val();

//    $.ajax({
//        url: '../AgenteDivision/BuscarsocioTipoDocumentoRecaudador',
//        type: 'POST',
//        data: { idTipoDocumento: idtipo, nroDocumento: doc },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                var recaudador = dato.data.Data;
//                if (recaudador != null) {
//                    $("#hidCodigoAgente").val(recaudador.BPS_ID);
//                    $("#txtNombre").val(recaudador.BPS_NAME);

//                    var division = recaudador.DAD_ID;
//                    if (division != 0) {
//                        $("#ddlDivision").val(division);
//                        $("#hidDivisionId").val(division);
//                    }
//                } else {
//                    limpiarDatosAgente();
//                    $("#ddlDivision").val(0);
//                    alert("No existe agente recaudador.");
//                }
//            } else if (dato.result == 0) {
//                limpiarDatosAgente();
//                alert(dato.message);
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status);
//            alert(thrownError);
//        }
//    });
//}

//function validarLongitudNumDoc() {
//    msgError("");
//    var exito = false;
//    var tipoDoc = $("#ddlTipoIdentificacion option:selected").val();
//    var tipoDocDesc = $("#ddlTipoIdentificacion option:selected").text();
//    limpiarDatosAgente();
//    getValorConfigNumDoc(tipoDoc);
//    var numValidar = $("#hidCantNumValidar").val();

//    if ($.trim($("#txtNroDocumento").val()) != "") {
//        if (tipoDocDesc == "DNI") {
//            if ($("#txtNroDocumento").val().length != numValidar) {
//                msgErrorB("divResultValidarDoc", "Longitud del DNI debe contener " + numValidar + " digitos.");
//            } else {
//                exito = true;
//                msgErrorB("divResultValidarDoc", "");
//            }
//        } else {
//            if ($("#txtNroDocumento").val().length != numValidar) {
//                msgErrorB("divResultValidarDoc", "Longitud del RUC debe contener " + numValidar + " digitos.");
//            } else {
//                exito = true;
//                msgErrorB("divResultValidarDoc", "");
//            }
//        }
//        if (exito) {
//            return true;
//        } else {
//            return false;
//        }
//    } else {
//        msgErrorB("divResultValidarDoc", "Ingrese número de documento.");
//    }
//}

//function getValorConfigNumDoc(itipo) {
//    $.ajax({
//        url: '../General/GetConfigTipoDocumento',
//        data: { tipo: itipo },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                $("#hidCantNumValidar").val(dato.valor);
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

//function limpiarDatosAgente() {
//    $("#hidCodigoAgente").val('');
//    $("#hidDivisionId").val('');
//    $("#txtNombre").val('');
//}

//function delAddAgenteRecaudo(idDel) {
//    $.ajax({
//        url: 'DellAgenteRecaudo',
//        type: 'POST',
//        data: { id: idDel },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//                loadDataAgente();
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//    return false;
//}


