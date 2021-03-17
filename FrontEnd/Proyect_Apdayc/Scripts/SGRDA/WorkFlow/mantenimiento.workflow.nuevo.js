/************************** INICIO CONSTANTES****************************************/
var K_ITEM = { CHOOSE: '--SELECCIONE--', ALL: '--TODOS--' };
var K_WIDTH_ESTADO = 350;
var K_HEIGHT_ESTADO = 130;
var K_WIDTH_TRAN = 350;
var K_HEIGHT_TRAN = 200;
var K_WIDTH_TAB = 350;
var K_HEIGHT_TAB = 160;
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    $("#tabs").tabs();
    $("#tabs").tabs({ disabled: [1,2] });
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Ciclo de Aprobación / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        //$("#hidOpcionEdit").val(0);
        $("#hidId").val(0);
        $("#trId").hide();
        LoadModuloNombre('ddlCliente', 0);
    } else {
        $('#divTituloPerfil').html("Workflow - Ciclo de Aprobación / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        //$("#hidOpcionEdit").val(1);
        $("#hidId").val(id);
        obtenerDatos(id);
    }
    //-------------------------- CARGA TABS -----------------------------------------    
    //------ESTADO
    $("#mvEstado").dialog({
        autoOpen: false,
        width: K_WIDTH_ESTADO,
        height: K_HEIGHT_ESTADO,
        buttons: {
            "Grabar": addEstado,
            "Cancelar": function () {
                $("#mvEstado").dialog("close");
                //$('#txtObservacion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    $(".addEstado").on("click", function () {
        $("#hidAccionMvEstado").val(0);
        $("#hidEdicionEstado").val(0);
        LoadWorkFlowEstado('ddlEstado', 0);
        $("#mvEstado").dialog("open");
    });

    //------TRANSICION
    
    $("#mvTransiciones").dialog({
        autoOpen: false,
        width: K_WIDTH_TRAN,
        height: K_HEIGHT_TRAN,
        buttons: {
            "Grabar": addTransicion,
            "Cancelar": function () {
                $("#mvTransiciones").dialog("close");
                //$('#txtObservacion').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });
    $(".addtransicion").on("click", function () {
        loadEstadoTemporal('ddlDestino');
        loadEstadoTemporal('ddlOrigen');
        LoadEventos('ddlEvento');
        $("#mvTransiciones").dialog("open");
    });

    //------TAB
    $("#mvTab").dialog({
        autoOpen: false,
        width: K_WIDTH_TAB,
        height: K_HEIGHT_TAB,
        buttons: {
            "Agregar": addTab,
            "Cancel": function () {
                $("#mvTab").dialog("close");
            }
        }, modal: true
    });

    $(".addTab").on("click", function () {
        $("#hidAccionMvTab").val(0);
        $("#hidEdicionTab").val(0);
        loadLicenciaTab("ddlTab", '0');
        LoadWorkFlowEstado("ddlPEstado", 0);
        $("#mvTab").dialog({});
        $("#mvTab").dialog("open");
        limpiarTab();
    });

    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../CicloAprobacion/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../CicloAprobacion/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabarFlujo();
    });

    $("#txtCodigo").keypress(function (e) {
        if (e.which == 13) {
            $("#txtSociedad").focus();
        }
    });
    //---------------------------------------------------------------------------------
    
    loadDataEstado();
});

function limpiarTab() {
    $("#ddlTab").val(0);
    $("#ddlPEstado").val(0);
}

function loadDataTransicion() {
    loadDataGridTmp('ListarTransiciones', "#gridTransicion");
}

function loadDataTab() {
    loadDataGridTmp('ListarTabs', "#gridTab");
}

//****************************  FUNCIONES ****************************
function grabarFlujo() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos)
        insertar();
};

function insertar() {
    var id = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion)
        id = $("#hidId").val();
    var flujo = {
        WRKF_ID: id,
        WRKF_NAME: $("#txtNombre").val(),
        WRKF_LABEL: $("#txtEtiqueta").val(),
        PROC_MOD: $("#ddlCliente").val()
    };
    $.ajax({
        url: '../CicloAprobacion/Insertar',
        data: flujo,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../CicloAprobacion/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(idOrigen) {
    $.ajax({
        url: "../CicloAprobacion/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_ID);
                $("#txtNombre").val(obj.WRKF_NAME);
                $("#txtEtiqueta").val(obj.WRKF_LABEL);
                LoadModuloNombre('ddlCliente', obj.PROC_MOD);
                loadDataEstado();
                //loadDataTransicion();
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

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

//-------------------------- TAB - ESTADO ---------------------------------- 
function loadDataEstado() {
    //loadDataGridTmp('ListarEstado', "#gridEstado");
    $.ajax({
        type: "POST",
        url: "../CicloAprobacion/ListarEstado",
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                $("#gridEstado").html(dato.message);
                $("#tabs").tabs({ disabled: false });
                loadDataTransicion();
                loadDataTab();
            } else if (dato.result == 2) {
                $("#gridEstado").html(dato.message);
                loadDataTransicion();
                loadDataTab();
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function addEstado() {  
    if ($("#ddlEstado").val() == 0) {
        $('#ddlEstado').css({ 'border': '1px solid red' });
    } else {
        var entidad = {
            Id_Estado: $("#ddlEstado option:selected").val(),
            Id_Estado_origen: $("#hidEdicionEstado").val(),
            DesEstado: $("#ddlEstado option:selected").text()
        };
        $.ajax({
            url: '../CicloAprobacion/AddEstado',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataEstado();
                    //loadDataTab();
                    $("#tabs").tabs({ disabled: false });
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $('#ddlEstado').css({ 'border': '1px solid gray' });
        $("#mvEstado").dialog("close");
    }
}

function actualizarDirPrincipal(idDir) {
    $.ajax({
        url: '../CicloAprobacion/SetDirPrincipal',
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

function delAddEstado(idDel) {
    $.ajax({
        url: '../CicloAprobacion/DellAddEstado',
        type: 'POST',
        data: { idEstado: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#tabs").tabs({ disabled: [1] });
                loadDataEstado();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddEstado(idUpd) {
    $("#hidAccionMvEstado").val(1);
    $("#hidEdicionEstado").val(0);
    //limpiarObservacion();
    $.ajax({
        url: '../CicloAprobacion/ObtieneEstadoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {
                    $("#hidEdicionEstado").val(obs.Id_Estado_origen);
                    LoadWorkFlowEstado('ddlEstado', obs.Id_Estado);
                    $("#mvEstado").dialog("open");
                } else {
                    alert("No se pudo obtener la observación para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//-------------------------- TAB - TRANSICION ---------------------------------- 
function loadEstadoTemporal(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../CicloAprobacion/ListaEstadosTemporal',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function addTransicion() {
    $('#ddlOrigen').css({ 'border': '1px solid gray' });
    $('#ddlDestino').css({ 'border': '1px solid gray' });
    $('#ddlEvento').css({ 'border': '1px solid gray' });

    var origen = $("#ddlOrigen").val();
    var destino = $("#ddlDestino").val();
    var evento = $("#ddlEvento").val();
    var estado = true;
    if (origen == 0) {
        $('#ddlOrigen').css({ 'border': '1px solid red' });
        estado = false;
    }
    if (destino == 0) {
        $('#ddlDestino').css({ 'border': '1px solid red' });
        estado = false;
    }
    if (evento == 0) {
        $('#ddlEvento').css({ 'border': '1px solid red' });
        estado = false;
    }
    if (origen == destino) {
        $('#ddlOrigen').css({  'border': '1px solid red' });
        $('#ddlDestino').css({ 'border': '1px solid red' });
        estado = false;
    }

    if(estado)
    {
        var entidad = {
            IdEstadoInicial: $("#ddlOrigen option:selected").val(),
            EstadoInicial: $("#ddlOrigen option:selected").text(),
            IdEstadoFinal: $("#ddlDestino option:selected").val(),
            EstadoFinal: $("#ddlDestino option:selected").text(),
            IdEvento: $("#ddlEvento option:selected").val(),
            Evento: $("#ddlEvento option:selected").text()
        };
        $.ajax({
            url: '../CicloAprobacion/AddTransicion',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataTransicion();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $('#ddlOrigen').css({ 'border': '1px solid gray' });
        $('#ddlDestino').css({ 'border': '1px solid gray' });
        $("#mvTransiciones").dialog("close");
    }
}

function delAddTransicion(idDel) {
    $.ajax({
        url: '../CicloAprobacion/DellAddTransicion',
        type: 'POST',
        data: { idTransicion: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataTransicion();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function addTab() {
    if ($("#ddlTab").val() == 0 || $("#ddlPEstado").val() == 0) {
        $('#ddlTab').css({ 'border': '1px solid red' });
        $('#ddlPEstado').css({ 'border': '1px solid red' });
    }
    else {
        var IdAdd = 0;
        if ($("#hidAccionMvTab").val() === "1") IdAdd = $("#hidIdSequencia").val();
        var entidad = {
            sequencia: IdAdd,
            IdTab: $("#ddlTab").val(),
            IdEstado: $("#ddlPEstado").val(),
            antIdTab: $("#hidTabanterior").val(),
            Nombre: $("#ddlTab option:selected").text(),
            NombreEst: $("#ddlPEstado option:selected").text()
        };
        $.ajax({
            url: 'AddTab',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataTab();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvTab").dialog("close");
        $('#ddlTab').css({ 'border': '1px solid gray' });
        $('#ddlPEstado').css({ 'border': '1px solid gray' });
    }
}

function delAddTab(idDel) {
    $.ajax({
        url: 'DellAddTab',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataTab();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    return false;
}

function updAddTab(idUpd) {
    limpiarTab();
    $.ajax({
        url: 'ObtieneLicenciaEstadoTabTmp',
        data: { Id: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvTab").val("1");
                    $("#hidIdSequencia").val(param.sequencia);
                    $("#hidTabanterior").val(param.IdTab);
                    $("#hidEdicionTab").val(param.Id);
                    LoadWorkFlowEstado("ddlPEstado", param.IdEstado);
                    loadLicenciaTab("ddlTab", param.IdTab);
                    $("#mvTab").dialog("open");
                } else {
                    alert("No se pudo obtener los datos para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}

