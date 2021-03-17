/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var defaultTipoCreacion = 'MW';
var defaultOrigeModalidad = 'NAC';
var defaultTipoSociedad = 'AUT';

/************************** INICIO CARGA ********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));

    if (id === undefined) {
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        cargaDLL();
    } else {
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidCod").val(id);
        obtenerDatos(id);
    }
  
    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnDescartar").on("click", function () {
        document.location.href = '../ModalidadUso/';
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../ModalidadUso/Nuevo';
    }).button();

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#ddlTipCre").change(function () {
        var codigo = $("#ddlTipCre").val();
        loadTipoDerecho('ddlTipDer', codigo, '0');
    });
    
    var eventoKP = "keypress";
    $('#txtComision').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtDtoAdm').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtDtoSoc').on(eventoKP, function (e) { return solonumeros(e); });
    $('#txtDtoAdi').on(eventoKP, function (e) { return solonumeros(e); });
    //---------------------------------------------------------------------------------
});

/************************** FUNCIONES ********************************************/
function grabar() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {        
        insertar();
    }
}

function insertar() {
    var id=0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidCod").val();
    var vMOD_COM = $('#txtComision').val() == '' ? -1 : $('#txtComision').val();
    var vMOD_DISCA = $('#txtDtoAdm').val() == '' ? -1 : $('#txtDtoAdm').val();
    var vMOD_DISCS = $('#txtDtoSoc').val() == '' ? -1 : $('#txtDtoSoc').val();
    var vMOD_DISCC = $('#txtDtoAdi').val() == '' ? -1 : $('#txtDtoAdi').val();
    
    
    var modalidad = {
        MOD_ID: id,
        MOD_DEC: $('#txtDes').val(),
        MOD_ORIG: $('#ddlOriMod').val(),
        MOD_SOC: $('#ddlTipSoc').val(),
        CLASS_COD: $('#ddlTipCre').val(),
        MOG_ID: $('#dllGruMod').val(),
        RIGHT_COD: $('#ddlTipDer').val(),
        MOD_INCID: $('#ddlNivInc').val(),
        MOD_USAGE: $('#ddlTipUsoRep').val(),
        MOD_REPER: $('#ddlModUsoRep').val(),
        MOD_COM: vMOD_COM,
        MOD_DISCA: vMOD_DISCA,
        MOD_DISCS: vMOD_DISCS,
        MOD_DISCC: vMOD_DISCC,
        WRFK_ID: $('#ddlFlujo').val(),
        MOD_OBS: $('#txtObs').val()
    };

    $.ajax({
        url: '../ModalidadUso/Insertar',
        data: modalidad,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../ModalidadUso/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function obtenerDatos(codigo) {
    $.ajax({
        url: "../ModalidadUso/Obtener",
        type: "GET",
        data: { id: codigo },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var modalidad = dato.data.Data;
                $('#txtDes').val(modalidad.MOD_DEC);
                $('#txtObs').val(modalidad.MOD_OBS);
                loadTipoModUso('ddlOriMod', modalidad.MOD_ORIG);
                loadTipoSociedad('ddlTipSoc', modalidad.MOD_SOC);                
                loadTipoGrupo('dllGruMod', modalidad.MOG_ID);                
                loadTipoCreacion('ddlTipCre',modalidad.CLASS_COD);
                ////loadTipoCreacion('ddlTipCre', modalidad.RIGHT_COD,modalidad.CLASS_COD);
                loadTipoDerecho('ddlTipDer',modalidad.CLASS_COD, modalidad.RIGHT_COD);
                loadTipoIncidencia('ddlNivInc', modalidad.MOD_INCID);                
                loadTipoRepertorio('ddlModUsoRep', modalidad.MOD_REPER);
                loadTipoObra('ddlTipUsoRep', modalidad.MOD_USAGE);
                $('#txtComision').val(modalidad.MOD_COM);
                $('#txtDtoAdm').val(modalidad.MOD_DISCA);
                $('#txtDtoSoc').val(modalidad.MOD_DISCS);
                $('#txtDtoAdi').val(modalidad.MOD_DISCC);
                LoadCicloAprobacion('ddlFlujo', modalidad.WRFK_ID)
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

function cargaDLL()
{    
    loadTipoDerecho('ddlTipDer', defaultTipoCreacion, '0');//loadTipoDerecho('ddlTipDer','0', '0');
    loadTipoModUso('ddlOriMod', defaultOrigeModalidad);//loadTipoModUso('ddlOriMod', '0');
    loadTipoGrupo('dllGruMod', '0');
    loadTipoSociedad('ddlTipSoc', defaultTipoSociedad); //loadTipoSociedad('ddlTipSoc', '0');
    loadTipoCreacion('ddlTipCre', defaultTipoCreacion)//loadTipoCreacion('ddlTipCre', '0');
    loadTipoIncidencia('ddlNivInc', '0');
    loadTipoObra('ddlTipUsoRep', '0');
    loadTipoRepertorio('ddlModUsoRep', '0');
    LoadCicloAprobacion('ddlFlujo', 0);

    
}