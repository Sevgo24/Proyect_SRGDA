var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

var K_ENTIDAD = {
    idMappings: 0,
    checkBox: "N",
    idAccion: 0,
    prioridad: 0
};

var K_ResultadoAjax = { Exito: 1, Error: 0 };

$(function () {
    var id = (GetQueryStringParams("id"));
    mvInitBuscarAccion({ container: "ContenedormvBuscarAccion", idButtonToSearch: "Abrir", idDivMV: "mvBuscarAccion", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitBuscarObjects({ container: "ContenedormvBuscarObjects", idButtonToSearch: "Abrir", idDivMV: "mvBuscarObject", event: "reloadEventoObjects", idLabelToSearch: "lbResponsable" });
    $("#tabs").tabs();

    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Mapa de Acciones de la Transición - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidWrkf").val(0);
        $("#hidEstado").val(0);
        LoadCicloAprobacion('dllWorkFlow', 0);
        $('#dllEstado').append($("<option />", { value: 0, text: "--SELECCIONE--" }));
        //loadEstadoXWorkFlow('dllEstado', 0, 0)
    }

    $('#dllWorkFlow').on('change', function () {
        $("#hidWrkf").val($('#dllWorkFlow').val());
        loadEstadoXWorkFlow('dllEstado', $('#dllWorkFlow').val(), 0);
        $('#dllEstado').val(0);
        if ($('#dllWorkFlow').val() == 0) {
            obtenerDatos(0, 0);
            loadEstadoXWorkFlow('dllEstado', 0, 0)
        }
        ObtenerProcMod($("#hidWrkf").val());
    });

    $('#dllEstado').on('change', function () {
        var idSel = $('#dllEstado').val();
        $("#hidAccionMvT").val(idSel);
        $("#hidEdicionT").val(idSel);
        $("#hidEstado").val(idSel);
        obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
    });

    $("#btnDescartar").on("click", function () {
        document.location.href = '../Mapping/Nuevo';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Mapping/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        if ($('#dllWorkFlow').val() != 0) {
            Actualizar();
        }
        else
            alert("Seleccione WorkFlow");
    });

    $(".addActionMappings").on("click", function () {
        if ($('#dllWorkFlow').val() != 0) {
            grabar();
        }
        else
            alert("Seleccione WorkFlow");
    });

    $("#mvWorkflowParametros").dialog({ autoOpen: false, width: 700, height: 300, buttons: { "Guardar": GuardarParametro, "Cancel": function () { $("#mvWorkflowParametros").dialog("close"); $('#txtObjeto').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addParametro").on("click", function () { agregarParametro(); });
    loadDataObjetosParametros();

    $("#mvWorkflowParametrosTransicion").dialog({
        autoOpen: false, width: 320, height: 200, buttons: {
            "Guardar": GuardarParametroTran, "Cancel": function () {
                $("#mvWorkflowParametrosTransicion").dialog("close");
                $('#ddlTabla').css({ 'border': '1px solid gray' });
                $('#ddlWhere').css({ 'border': '1px solid gray' });
                $('#ddlUpdate').css({ 'border': '1px solid gray' });
            }
        }, modal: true
    });
    $(".addParametroTra").on("click", function () { agregarParametroTra(); });

    LoadParametroTransicion('ddlTabla', 1, '', 0);
    LoadParametroTransicion('ddlWhere', 2, 'REC_LICENSES_GRAL', 0);
    LoadParametroTransicion('ddlUpdate', 3, 'REC_LICENSES_GRAL', 0);
  
    $("#ddlTabla").on("change", function () {
        UpdParametroTran(2, $("#ddlTabla").val(),1);    
    });

    $("#ddlWhere").on("change", function () {
        UpdParametroTran(4, $("#ddlWhere").val(),3);      
    });

    $("#ddlUpdate").on("change", function () {
        UpdParametroTran(1, $("#ddlUpdate").val(),2);    
    });
    
});


function UpdParametroTran(tipo, value, orden) {
    
       $.ajax({
        data: { wrkfdtid: tipo, valor: value,orden:orden },
        url: 'UpdParametroTran',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
            } else {
                if (tipo == 2)
                    AddTransicionParametro(1, "TABLA", $("#ddlTabla").val(), $("#hidAccionMapping").val(), tipo, $("#hidProcMod").val(), orden);
                else if (tipo == 4)
                    AddTransicionParametro(2, "CAMPO", $("#ddlWhere").val(), $("#hidAccionMapping").val(), tipo, $("#hidProcMod").val(), orden);
                else if (tipo == 1)
                    AddTransicionParametro(3, "CAMPO", $("#ddlUpdate").val(), $("#hidAccionMapping").val(), tipo, $("#hidProcMod").val(), orden);
            }
        }
    });
}

function GuardarParametroTran() {
    if ($("#ddlTabla").val() == 0 || $("#ddlWhere").val() == 0 || $("#ddlUpdate").val() == 0) {
        $('#ddlTabla').css({ 'border': '1px solid red' });
        $('#ddlWhere').css({ 'border': '1px solid red' });
        $('#ddlUpdate').css({ 'border': '1px solid red' });
    }
    else {
        $('#ddlTabla').css({ 'border': '1px solid gray' });
        $('#ddlWhere').css({ 'border': '1px solid gray' });
        $('#ddlUpdate').css({ 'border': '1px solid gray' });
        
        $.ajax({
            url: '../Mapping/InsertarParametroTran',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    limpiarTran();
                    alert(dato.message);
                    $("#mvWorkflowParametrosTransicion").dialog("close");
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
}

function limpiarTran() {
    $('#ddlTabla').val(0);
    $('#ddlWhere').val(0);
    $('#ddlUpdate').val(0);
}

var formatoCorreo = false;
function GuardarParametro() {
    addvalidacionFormatoCorreo()
    if (formatoCorreo) {
        grabarParametroCorreo();
    }
    else {
        alert("Formato de correo incorrecto.");
    }
}

function grabarParametroCorreo() {
    if (ValidarRequeridos()) {
        ObtenerObjetoParametro();
        $.ajax({
            url: '../Mapping/InsertarParametroCorreo',
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    $("#mvWorkflowParametros").dialog("close");
                    alert(dato.message);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};

function AgregarParametro(oId, mId) {
    $("#hidObjects").val(oId);
    $("#hidAccionMapping").val(mId);
    ObtenerObjeto(oId);
    ObtenerParametros(oId);
    $("#mvWorkflowParametros").dialog("open");
}

function agregarParametroTra(idTran, mId) {
    $("#hidTransicion").val(idTran);
    $("#hidAccionMapping").val(mId);
    ObtenerParametrosTransicion(mId, 2);
    ObtenerParametrosTransicion(mId, 1);
    ObtenerParametrosTransicion(mId, 4);
    $("#mvWorkflowParametrosTransicion").dialog("open");
}

function AddTransicionParametro(id, nom, val, amId, wrkdId, pmod,orden) {
    var entidad = {
        Id: 0,
        nombre: nom,
        valor: val,
        orden: orden,
        accionMappingId: amId,
        wrkfdtid: wrkdId,
        wrkfptid: "VARIABLE",
        procmod: pmod
    };

    $.ajax({
        url: '../Mapping/AddTransicionParametro',
        type: 'POST',
        data: entidad,
        sucess: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerParametrosTransicion(id, wrkid) {
    $.ajax({
        url: '../Mapping/ObtenerParametroTransicion',
        data: { idAccionMapping: id, wrkfdtid: wrkid },
        type: 'GET',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var parametro = dato.data.Data;
                if (parametro != null) {
                    if (wrkid == 2) {
                        ObtenerTemporalParamentrosTransicion(parametro.WRKF_PID, parametro.WRKF_PNAME, parametro.WRKF_PVALUE, parametro.WRKF_PORDER, parametro.WRKF_AMID, parametro.WRKF_DTID, parametro.WRKF_PTID, parametro.PROC_MOD);
                        LoadParametroTransicion('ddlTabla', 1, '', parametro.WRKF_PVALUE);
                    }
                    else if (wrkid == 4) {
                        ObtenerTemporalParamentrosTransicion(parametro.WRKF_PID, parametro.WRKF_PNAME, parametro.WRKF_PVALUE, parametro.WRKF_PORDER, parametro.WRKF_AMID, parametro.WRKF_DTID, parametro.WRKF_PTID, parametro.PROC_MOD);
                        LoadParametroTransicion('ddlWhere', 2, 'REC_LICENSES_GRAL', parametro.WRKF_PVALUE);
                    }
                    else if (wrkid == 1) {
                        ObtenerTemporalParamentrosTransicion(parametro.WRKF_PID, parametro.WRKF_PNAME, parametro.WRKF_PVALUE, parametro.WRKF_PORDER, parametro.WRKF_AMID, parametro.WRKF_DTID, parametro.WRKF_PTID, parametro.PROC_MOD);
                        LoadParametroTransicion('ddlUpdate', 3, 'REC_LICENSES_GRAL', parametro.WRKF_PVALUE);
                    }
                }
            } else if (dato.result == 0) {
                $("#ddlTabla").val(0);
                $("#ddlWhere").val(0);
                $("#ddlUpdate").val(0);
            }
        }
    });
};

function ObtenerTemporalParamentrosTransicion(id, nom, val, ord, amId, wrkdId, wrkpId, pmod) {
    var entidad = {
        codigo: id,
        nombre: nom,
        valor: val,
        orden: ord,
        accionMappingId: amId,
        wrkfdtid: wrkdId,
        wrkfptid: wrkpId,
        procmod: pmod
    };
    $.ajax({
        url: '../Mapping/ObtenerTemporalParamentrosTransicion',
        type: 'POST',
        data: entidad,
        sucess: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function addvalidacionFormatoCorreo() {
    $('#TblParametros tr').each(function () {
        var id = parseFloat($(this).find("td").eq(1).html());
        //alert("id " + id);
        if (!isNaN(id)) {
            var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
            if (!regex.test($('#txtvalor_' + id).val().trim())) {
                $('#txtvalor_' + id).css({ 'border': '1px solid red' });
                formatoCorreo = false;
                //alert(formatoCorreo);
            }
            else {
                $('#txtvalor_' + id).css({ 'border': '1px solid gray' });
                formatoCorreo = true;
                //alert(formatoCorreo);
            }
        }
    });
}

function agregarParametro() {
    ObtenerObjetoParametro();
    var IdAdd = 0;
    var entidad = {
        Id: IdAdd,
        codigo: 0,
        nombre: "CORREO",
        valor: "",
        orden: 0,
        accionMappingId: $("#hidAccionMapping").val(),
        wrkfdtid: 3,
        wrkfptid: "VARIABLE",
        objetoId: $("#hidObjects").val(),
        procmod: $("#hidProcMod").val()
    };
    $.ajax({
        url: 'AddParametro',
        type: 'POST',
        data: entidad,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataObjetosParametros();
            } else if (dato.result == 0) { }
        }
    });
}

function ObtenerObjetoParametro() {
    var objetoParametroCorreo = [];
    var contador = 0;
    $('#TblParametros tr').each(function () {
        var id = parseFloat($(this).find("td").eq(1).html());
        if (!isNaN(id)) {
            objetoParametroCorreo[contador] = {
                Id: id,
                valor: $('#txtvalor_' + id).val()
            };
            contador += 1;
        }
    });

    var objetoParametroCorreo = JSON.stringify({ 'objetoParametroCorreo': objetoParametroCorreo });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Mapping/ObtenerObjetoParametro',
        data: objetoParametroCorreo,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) { }
            else { }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function ObtenerParametros(id) {
    $.ajax({
        url: '../Mapping/obtenerParametroCorreoXobjeto',
        data: { idObj: id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    loadDataObjetosParametros();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function ObtenerObjeto(id) {
    $.ajax({
        url: '../Mapping/ObtenerDescripcionObjeto',
        data: { idObj: id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#txtObjeto").val(en);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function ObtenerProcMod(id) {
    $.ajax({
        url: '../Mapping/ObtenerProcMod',
        data: { wrkfid: id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hidProcMod").val(en);
                    //alert(en);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function grabar() {
    var entidad = {
        WRKF_TID: $("#hidEstado").val(),
        WRKF_ID: $("#dllWorkFlow").val(),
        WRKF_SID: $("#dllEstado").val()
    };
    $.ajax({
        url: '../Mapping/Insertar',
        data: entidad,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                ////obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function Actualizar() {
    var entidad = {
        WRKF_ID: $("#hidWrkf").val(),
        WRKF_SID: $("#hidEstado").val()
    };
    $.ajax({
        url: '../Mapping/ActualizarMappings',
        data: entidad,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                //obtenerNombreTransicion($("#hidEstado").val());
                alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function moverOpcion(orden, opc) {
    var id = $("#hidEstado").val();
    //alert(id + " " + orden + " " + opc);

    var workflow = $("#dllWorkFlow").val();
    var estado = $("#dllEstado").val();

    //alert($("#dllWorkFlow").val() + " " + $("#dllEstado").val());

    $.ajax({
        url: '../Mapping/ActualizarOrden',
        //data: { IdTransicion: id, orden: orden, opcion: opc, Idwrk: workflow, Idst: estado },
        data: { orden: orden, opcion: opc, Idwrk: workflow, Idst: estado },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == K_ResultadoAjax.Exito) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
            } else if (dato.result == K_ResultadoAjax.Error) {
                alertify.error(dato.message);
            }
        }
    });

    return false;
}

function obtenerDatos(idW, idSel) {
    $("#hidOpcionEdit").val(1);
    $.ajax({
        url: '../Mapping/obtener',
        data: { Idwrk: idW, Idst: idSel },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    loadDataAcctionMappingsValores();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function validaAccionAccion(id) {
    var estado = false;
    $.ajax({
        data: { Id: id },
        url: '../Mapping/ValidarAccion',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            }
            else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

function validaAccionObjeto(id) {
    var estado = false;
    $.ajax({
        data: { Id: id },
        url: '../Mapping/ValidarObjeto',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            }
            else {
                estado = false;
                alert(dato.message);
            }
        }
    });
    return estado;
}

var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    $("#hidAccion").val(idSel);
    ActualizarAcc(idSel);
};

var reloadEventoObjects = function (idSel) {
    $("#hidObjects").val(idSel)
    ActualizarObj(idSel)
};

function ActualizarObj(id) {
    var estado = validaAccionObjeto(id);
    if (estado) {
        $.ajax({
            url: '../Mapping/ActualizarObjeto',
            data: { Objeto: id, idMappings: K_ENTIDAD.idMappings },
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                    //obtenerNombreTransicion($("#hidEstado").val());
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    };
}

function QuitarObjeto(id, idmap) {
    if (id != 0) {
        if (confirm('¿Esta seguro de eliminar este objeto?')) {
            $.ajax({
                url: '../Mapping/QuitarObjeto',
                data: { Objeto: id, idMappings: idmap },
                type: 'POST',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);
                    if (dato.result == 1) {
                        obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                        //obtenerNombreTransicion($("#hidEstado").val());
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                }
            });
        }
    }
}

function ActualizarAcc(id) {
    var estado = validaAccionAccion(id);
    if (estado) {
        $.ajax({
            url: '../Mapping/ActualizarAccion',
            data: { Accion: id, idMappings: K_ENTIDAD.idMappings },
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                    //obtenerNombreTransicion($("#hidEstado").val());
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    };
}

function nombreTransicion(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../General/ObtenerNombreTransicion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbTransicion").html(dato.valor);
            }
        }
    });
}

function BuscarAccion(idmap, check) {
    K_ENTIDAD.idMappings = idmap;
    $("#mvBuscarAccion").dialog("open");
};

function BuscarObjeto(idmap, check) {
    K_ENTIDAD.idMappings = idmap;
    $("#mvBuscarObject").dialog("open");
};

function ActualizarV(checkbox, idMap) {
    if (checkbox.checked)
        K_ENTIDAD.checkBox = "Y";
    else
        K_ENTIDAD.checkBox = "N";

    $.ajax({
        url: '../Mapping/ActualizarVisible',
        data: { Visible: K_ENTIDAD.checkBox, idMappings: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function ActualizarO(checkbox, idMap) {
    if (checkbox.checked)
        K_ENTIDAD.checkBox = "Y";
    else
        K_ENTIDAD.checkBox = "N";

    $.ajax({
        url: '../Mapping/ActualizarObligatorio',
        data: { Obligatorio: K_ENTIDAD.checkBox, idMappings: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};

function ActualizarPri(control, idMap) {
    $.ajax({
        url: '../Mapping/ActualizarPrioridad',
        data: { Prioridad: control.value, idMappings: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ActualizarPre(control, idMap, orden) {
    $.ajax({
        url: '../Mapping/ActualizarPrerrequisito',
        data: { Prerrequisito: control.value, idMappings: idMap, NroOrden: orden },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                alert(dato.message);
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ActualizarNext(control, idMap, orden) {
    $.ajax({
        url: '../Mapping/ActualizarNextA',
        data: { Next: control.value, idMappings: idMap, NroOrden: orden },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                alert(dato.message);
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ActualizarEvento(control, idMap) {
    $.ajax({
        url: '../Mapping/ActualizarEvento',
        data: { Evento: control.value, idMappings: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ActualizarTransicion(control, idMap) {
    $.ajax({
        url: '../Mapping/ActualizarTransicion',
        data: { Transicion: control.value, idMappings: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                EliminarParametrosTransicion(idMap);
                //obtenerNombreTransicion($("#hidEstado").val());
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function EliminarParametrosTransicion(idMap) {
    $.ajax({
        url: '../Mapping/EliminarParametrosTransicion',
        data: { idMapping: idMap },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function EliminarMapping(idMap, orden) {
    //alert("IdMap : " + idMap + " Wrkf : " + $("#hidWrkf").val() + " Estado : " + $("#hidEstado").val());
    if (confirm('¿Esta seguro de eliminar?')) {
        $.ajax({
            url: '../Mapping/EliminarAccion',
            data: { idMappings: idMap, Orden: orden, Wrkf: $("#hidWrkf").val(), Estado: $("#hidEstado").val() },
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    obtenerDatos($("#hidWrkf").val(), $("#hidEstado").val());
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
    };
}

function loadDataAcctionMappingsValores() {
    loadDataGridTmp('ListarAcctionMappingsValores', "#gridAcctionMappings");
}

function loadDataObjetosParametros() {
    loadDataGridTmp('ListarObjetosParametros', "#gridObjetoParametro");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) {
            var dato = response; $(idGrilla).html(dato.message);
        }
    });
}

function Eliminar(idDel) {
    $.ajax({
        url: 'DellParametro',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataObjetosParametros();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
