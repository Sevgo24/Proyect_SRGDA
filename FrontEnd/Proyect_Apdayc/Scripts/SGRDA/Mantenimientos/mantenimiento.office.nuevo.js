/************************** INICIO CONSTANTES****************************************/
var K_WIDTH_OBS = 580;
var K_HEIGHT_OBS = 295;
var K_WIDTH_DEP = 670;
var K_HEIGHT_DEP = 440;
var K_WIDTH_DOC = 610;
var K_HEIGHT_DOC = 250;
var K_WIDTH_PAR = 480;
var K_HEIGHT_PAR = 260;
var K_WIDTH_NUM = 400;
var K_HEIGHT_NUM = 180;

var K_SIZE_PAGE = 8;
var K_ID_DIR = "divDir";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_DIV_ADM = "ADM";

var idDep = 0;
var id = 0;

var K_DIV_VALIDAR = {
    DIV_CAB: "divCabeceraOfi",
    DIV_DIRECCION: "divDireccionOfi"
};

var K_DIV_MESSAGE = {
    DIV_OFICINA: "divMensajeError",
    DIV_TAB_POPUP_DOCUMENTO: "avisoDocumento",
    DIV_DIRECCION: "avisoMV"
};

var K_DIV_POPUP = {
    DOCUMENTO: "mvDocumento"
};

/************************** INICIO CARGA********************************************/
$(function () {
    limpiar();
    $("#tabs").tabs();
    $("#tabVis").hide();
    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvObs").val("0");
    $("#hidAccionMvPar").val("0");
    $("#hidAccionMvDoc").val("0");
    $("#hidAccionMvNum").val("0");
    $("#hidAccionMvOfi").val("0");
    $('#hidIdOficinaDiv').val('');
    //CONTACTO
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitOficina({ container: "ContenedormvOficina", idButtonToSearch: "btnBuscaOficina", idDivMV: "mvBuscarOficina", event: "reloadEventoOficina", idLabelToSearch: "lbOficina" });
    //---------------------------------------------------------------------------------
    id = (GetQueryStringParams("id"));
    var edit = 0;
    if (id === undefined) {
        $("#hidOpcionEdit").val(0);
        $('#divTituloPerfil').html("Oficinas de Recaudo - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidOpcionEdit").val(0);
        $("#hidOfiId").val(0);
        nuevo();
        loadComboTipoVia(0);
        loadComboTipoEtapa(0);
        loadComboDireccion(0);
        loadComboTerritorio(0);
        loadComboTipoDptoPiso(0);
        loadComboTipoUrb(0);
        loadComboCodPostal(0);
        initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
        //-------------------------- CARGA LISTAS - TABS ---------------------------------   
        loadDataParametro();
        loadDataObservacion();
        loadDataDocumento();
        loadDataOfi();
        loadDataAsociado();
        loadDataDireccionHistorial();
        loadDataDivisionAdm();
    } else {
        $('#divTituloPerfil').html("Oficinas de Recaudo - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidOpcionEdit").val(1);
        $("#hidAccionMvOfi").val(1);
        $("#hidOfiId").val(id);
        ObtenerDatos(id);
    }
    //--------------------------------------------------------------------------------

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#txtOficina").focus();
    $("#chkPrincipal").change(function () {
        if ($('#chkPrincipal').is(':checked')) {
            $("#ddlOfiDependencia").prop('disabled', true);
            $('#ddlOfiDependencia option').remove();
        } else {
            $("#ddlOfiDependencia").prop('disabled', false);
            loadComboOficina('ddlOfiDependencia', idDep);
        }
    });
    //--------------------------------------------------------------------------------

    //-------------------------- CARGA - DROPDOWNLIST -TABS -------------------------
    loadTipoDoc("ddlTipoDocumento", 0);
    loadTipoParametro("ddlTipoParametro", 0);
    loadTipoObservacion("ddlTipoObservacion", 0);
    //--------------------------------------------------------------------------------

    //-------------------------- CARGA TABS -----------------------------------------    
    //------OBSERVACION
    loadComboTipoObs();
    $("#mvObservacion").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Grabar": addObservacion, "Cancelar": function () { $("#mvObservacion").dialog("close"); $('#ddlTipoObservacion').css({ 'border': '1px solid gray' }); $('#txtObservacion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addObservacion").on("click", function () { limpiarObservacion(); $("#mvObservacion").dialog("open"); });
    $("#tabObs").on("click", function () {
        loadDataObservacion();
    });

    //------DEPENDENCIA
    //$("#ModalNeoDep").dialog({
    //    autoOpen: false,
    //    width: K_WIDTH_DEP,
    //    height: K_HEIGHT_DEP,
    //    buttons: {
    //        "Agregar": agregarDep,
    //        "Cancel": function () { $("#ModalNeoDep").dialog("close"); }
    //    },
    //    modal: true,
    //    open: function (event, ui) {
    //        $('#ModalNeoDep').css('overflow', 'hidden');
    //    }
    //});

    //$("#addHijo").on("click", function () {
    //    $("#ModalNeoDep").dialog("open");
    //    limpiarDep();
    //    loadDataDep();
    //});

    //$("#btnLimpiarDep").on("click", function () {
    //    limpiarDep();
    //});

    //$("#btnBuscarDep").on("click", function () { loadDataDep(); });

    //$("#txtOfiSearchDep").keypress(function (e) {
    //    if (e.which == 13) {
    //        loadDataDep();
    //    }
    //});

    //$("#tabVis").on("click", function () {
    //    loadDataOfi();
    //});
    //---------------

    //------DOCUMENTO
    $("#mvDocumento").dialog({ autoOpen: false, width: K_WIDTH_DOC, height: K_HEIGHT_DOC, buttons: { "Grabar": addDocumento, "Cancelar": function () { $("#mvDocumento").dialog("close"); $('#txtFecha').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addDocumento").on("click", function () { limpiarDocumento(); $("#mvDocumento").dialog("open"); });
    $("#btnValidarDocumento").on("click", function () {
        validarLongitudNumDoc();
    });

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });


    $("#tabDoc").on("click", function () {
        loadDataDocumento();
    });
    //---------------

    //------PARAMETRO
    $("#mvParametro").dialog({ autoOpen: false, width: K_WIDTH_PAR, height: K_HEIGHT_PAR, buttons: { "Grabar": addParametro, "Cancelar": function () { $("#mvParametro").dialog("close"); $('#txtconipcion').css({ 'border': '1px solid gray' }); } }, modal: true });
    $(".addParametro").on("click", function () { limpiarParametro(); $("#mvParametro").dialog("open"); });
    $("#tabPar").on("click", function () {
        loadDataParametro();
    });
    //----------------



    //------OBSERVACIÒN
    $("#addObs").on("click", function () {
        $("#txtOfiObs").val('');
        $("#ModalNeoObs").dialog({ title: "Registrar Observación" });
        $("#ModalNeoObs").dialog("open");
    });

    $("#ModalNeoObs").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS,
        height: K_HEIGHT_OBS,
        buttons: {
            "Agregar": grabarObs,
            "Cancel": function () { $("#ModalNeoObs").dialog("close"); }
        },
        modal: true
    });

    $("#delObs").on("click", function () {
        eliminarObs();
    });
    //------

    //------Contactos
    $("#mvEntidad").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: 280, buttons: { "Grabar": AddAsociado, "Cancelar": function () { $("#mvEntidad").dialog("close"); } }, modal: true });
    $(".addEntidad").on("click", function () { limpiarAsociado(); $("#mvEntidad").dialog("open"); });
    loadTipoDocumento("ddlTipoDocumento1", 0);
    loadRol("ddlRol", 0);
    $("#tabCon").on("click", function () {
        loadDataAsociado();
    });
    //--------------------------------------------------------------------------------
    $("#tabDir").on("click", function () {
        loadDataDireccionHistorial();
    });

    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../office/';
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../office/Nuevo';
    }).button();

    $("#btnGrabarOfi").on("click", function () {
        grabarOficina();
    }).button();

    $("#btnBuscarSocio").on("click", function () {
        buscarSocio();
    });
    //---------------------------------------------------------------------------------


    //$('#fuFiles').uploadify({
    //    'uploader': '../Scripts/Extensiones/_scripts/uploadify.swf',
    //    'script': 'Upload',
    //    'cancelImg': '../Scripts/Extensiones/_scripts/cancel.png',
    //    'auto': false,
    //    'multi': false,
    //    'buttonText': 'Adjuntar Archivo',
    //    'queueSizeLimit': 1,
    //    'simUploadLimit': 2,
    //    'sizeLimit': 800 * 1024,
    //    'fileDesc': 'Tipo de imágenes permitidas (.JPG, .GIF, .PNG, .PDF)',
    //    'fileExt': '*.jpg;*.jpeg;*.gif;*.png;*.pdf',

    //    'onError': function (a, b, c, d) {
    //        if (d.status == 404)
    //            alert("Could not find upload script. Use a path relative to: " + "<?= getcwd() ?>");
    //        else if (d.type === "HTTP")
    //            alert("error " + d.type + ": " + d.status);
    //        else if (d.type === "File Size")
    //            alert(c.name + " " + d.type + " Limit: " + Math.round(d.info / (1024 * 1024)) + "MB");
    //        else
    //            alert("error " + d.type + ": " + d.text);
    //    }
    //});

    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    // -- AGENTE DE RECAUDO
    mvInitBuscarAgenteRecaudo({ container: "ContenedormvBuscarGestor", idButtonToSearch: "btnAgenteRecaudo", idDivMV: "mvBuscarAgenteRecaudo", event: "BuscarGestor", idLabelToSearch: "lblAgenteRecaudo", tipoPersona: "J" });
    $(".addGestor").on("click", function () {
        $("#mvBuscarAgenteRecaudo").dialog("open");
    });

    // XX DIVISIONES ADMINISTRATIVAS XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    mvInitDivisiones({ container: "ContenedormvBuscarGestor", idButtonToSearch: "Abrir", idDivMV: "mvBuscarDivision", event: "addDivision", idLabelToSearch: "lblDivision", tipoDiv: 'ADM', bloqueoTipoDiv: '1' });
    $(".addDivision").on("click", function () {
        $("#mvBuscarDivision").dialog("open");
    });

    // -- GRUPO MODALIDAD POR DIVISIÓN
    mvInitBuscarGrupoModalidad({ container: "ContenedormvBuscarGrupoModalidad", idButtonToSearch: "Abrir", idDivMV: "mvBuscarGrupoModalidad", event: "addGrupoModalidad", idLabelToSearch: "lblGrupoModalidad" });

    // -- NUMERACIÓN POR DIVISIÓN (SERIE) ----------
    mvInitBuscarNumerador({ container: "ContenedormvBuscarNumeracion", idButtonToSearch: "Abrir", idDivMV: "mvBuscarNumerador", event: "addNumerador", idLabelToSearch: "lblNumerador" });

});

//****************************   TAB - DIVISION ADMIN.  *********************************************
var addDivision = function (idSel) {
    $.ajax({
        url: '../Office/AddDivisionAdm',
        type: 'POST',
        data: { Id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};
function loadDataDivisionAdm() {
    loadDataGridTmp('ListarDivisionAdm', "#gridDivisionAdm");
}
function delDivisionAdm(idDel) {
    $.ajax({
        url: '../office/delAddDivisionAdm',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

//****************************   TAB - GRUPO MODALIDAD  *********************************************
function AbrirPoPupAddGrupoModalidad(idDivOficina) {
    $('#hidIdOficinaDiv').val('');
    $("#mvBuscarGrupoModalidad").dialog("open");
    $('#hidIdOficinaDiv').val(idDivOficina);
}
var addGrupoModalidad = function (idSel) {
    $.ajax({
        url: '../Office/AddGrupoModalidad',
        type: 'POST',
        data: { idGrupoModalidad: idSel, IdOficinaDiv: $('#hidIdOficinaDiv').val() },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};
function delGrupoModalidad(idDel) {
    $.ajax({
        url: '../office/DellAddGrupoModalidad',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

//****************************   TAB - NUMERADORES *********************************************
function AbrirPoPupAddNumeradores(idDivOficina) {
    $('#hidIdOficinaDiv').val('');
    $("#mvBuscarNumerador").dialog("open");
    $('#hidIdOficinaDiv').val(idDivOficina);
}
var addNumerador = function (idSel) {
    $.ajax({
        url: '../Office/AddNumeracion',
        type: 'POST',
        data: { IdOficinaDiv: $('#hidIdOficinaDiv').val(), idCorrelativo: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
};
function delNumerador(idDel) {
    $.ajax({
        url: '../office/DellAddNumeracion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//----------------------------  AGENTE DE RECAUDO  -----------------------------------------
var BuscarGestor = function (idSel) {
    $("#lblAgenteRecaudo").val(idSel);
    $("#hidIdAgenteRecaudo").val(idSel);
    obtenerNombreSocio(idSel, 'lblAgenteRecaudo');
};
function obtenerNombreSocio(idSel, control) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#" + control).html(dato.valor);
            }
        }
    });
}
function grabarOficina() {

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_OFICINA, K_DIV_VALIDAR.DIV_CAB)) {
        var estado = ValidarRequeridosMV();
        if (estado) {
            if (K_ACCION_ACTUAL == K_ACCION.Nuevo) {
                var estadoDuplicado = validarDuplicado();
                if (!estadoDuplicado) {
                    addDireccion();//insertarOficina();
                } else {
                    Confirmar('La oficina ya existe, ¿Desea registrar?',
                            function () {
                                addDireccion();//insertarOficina();
                            },
                            function () {
                            },
                            'Confirmar'
                        );
                }
            } else {
                addDireccion();//insertarOficina();
            }

        } else {
            var index = $('#tabs a[href="#tabs-1"]').parent().index();
            $("#tabs").tabs("option", "active", index);
        }
    }
}

//----------------------------  CONTACTO  -----------------------------------------
var reloadEvento = function (idSel) {
    $("#lbResponsable").val(idSel);
    $("#hidEdicionEnt").val(idSel);
    obtenerNombreSocio(idSel, 'lbResponsable');
};
//---------------------------- DIRECCION  --------------------------------------------------
function addDireccion() {
    var IdAdd = 0;
    if ($("#hidAccionMvDir").val() == "1") IdAdd = $("#hidEdicionDir").val()

    var direccion = {
        Id: IdAdd,
        TipoDireccion: $("#ddlTipoDireccion").val(),
        RazonSocial: obtenerRazonSocial(),
        Territorio: $("#ddlTerritorio").val(),
        CodigoUbigeo: $("#hidCodigoUbigeo").val(),
        Referencia: $("#txtReferencia").val(),
        CodigoPostal: $("#ddlCodPostal").val(),
        TipoUrb: $("#ddlUrbanizacion").val(),
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
        TipoTerritorioDesc: $("#ddlTerritorio option:selected").text(),
        TipoUrbaDesc: $("#ddlUrbanizacion option:selected").text(),
        TipoDepaDesc: $("#ddlDepartamento option:selected").text(),
        TipoAvenidaDesc: $("#ddlAvenida option:selected").text(),
        TipoEtapaDesc: $("#ddlEtapa option:selected").text()
    };

    $.ajax({
        url: '../Office/AddDireccion',
        type: 'POST',
        data: direccion,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                insertarOficina();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function updAddDireccion(idUpd) {
    limpiarDireccion();

    $.ajax({
        url: '../Office/ObtieneDireccionTmp',
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
                    loadComboDireccion(direccion.TipoDireccion);
                    loadComboTerritorio(direccion.Territorio);
                    $("#txtReferencia").val(direccion.Referencia);
                    loadComboCodPostal(direccion.CodigoPostal);
                    loadComboTipoUrb(direccion.TipoUrb);
                    $("#txtLote").val(direccion.Lote);
                    loadComboTipoDptoPiso(direccion.TipoDepa);
                    $("#txtNroPiso").val(direccion.NroPiso);
                    loadComboTipoVia(direccion.TipoAvenida);
                    $("#txtAvenida").val(direccion.Avenida);
                    loadComboTipoEtapa(direccion.TipoEtapa);
                    $("#txtEtapa").val(direccion.Etapa);
                    $("#hidCodigoUbigeo").val(direccion.CodigoUbigeo);
                    $("#txtUbigeo").val(direccion.DescripcionUbigeo);
                    initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
                } else {
                    alert("No se pudo obtener la direccion para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerRazonSocial() {
    var nroPiso = "";
    var nroAv = "";
    var nroEtp = "";
    var nro = "";
    var nroMZ = "";
    var nroLote = "";

    if ($.trim($("#txtNro").val()) != "") {
        nro = "  Nro " + $("#txtNro").val();
    }
    if ($.trim($("#txtMz").val()) != "") {
        nroMZ = "  Mz " + $("#txtMz").val();
    }
    if ($.trim($("#txtLote").val()) != "") {
        nroLote = "  Lote " + $("#txtLote").val();
    }
    if ($.trim($("#txtNroPiso").val()) != "") {
        nroPiso = " " + $("#ddlDepartamento option:selected").text() + " " + $("#txtNroPiso").val();
    }
    if ($.trim($("#txtNroPiso").val()) != "") {
        nroAv = " " + $("#ddlAvenida option:selected").text() + " " + $("#txtAvenida").val();
    }
    if ($.trim($("#txtEtapa").val()) != "") {
        nroEtp = " " + $("#ddlEtapa option:selected").text() + " " + $("#txtEtapa").val();
    }
    var razon = $("#ddlUrbanizacion option:selected").text() + " " + $("#txtUrb").val() + nro + nroMZ + nroLote + nroPiso + nroAv + nroEtp;

    return razon;
}

function limpiarDireccion() {
    $("#hidAccionMvDir").val("0");
    $("#hidEdicionDir").val("0");
}

//---------------------------------------------
function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST',
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        },

        //complete: function () {
        //    $(idGrilla).html(dato.message);
        //}

    });
}

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

//-------------------------- TAB - Contacto -----------------------------------  
function loadDataAsociado() {
    loadDataGridTmp('ListarAsociado', "#gridAsociado");
}

function AddAsociado() {

    var IdAdd = 0;
    IdAdd = $("#hidEdicionEnt").val();

    var entidad = {
        Id: IdAdd,
        Codigo: $("#hidBpsId").val(),
        nombre: $("#lbResponsable").html(),

        rol_id: $("#ddlRol option:selected").val(),
        rol_descripcion: $("#ddlRol option:selected").text(),
        tipo_documento: $("#ddlTipoDocumento1 option:selected").val(),
        numero_documento: $("#txtDocumento").val()
    };

    if (ValidarRequeridosET() && IdAdd !== 0) {
        $.ajax({
            url: '../office/AddAsociado',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataAsociado();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#avisoMVEntidad").html('');
        $("#mvEntidad").dialog("close");
    }


}

function buscarSocio() {
    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    var doc = $("#ddlTipoDocumento1").val();
    var nro = $("#txtDocumento").val();

    $.ajax({
        data: { tipo: doc, nro_tipo: nro },
        //url: '../SOCIO/ObtenerSocioDocumento',
        url: '../OFFICE/ObtenerSocioDocumento',
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $("#hidEdicionEnt").val(datos.Codigo);
                $("#txtSocioAsociado").val(datos.Codigo);
                var nombre = datos.Nombres + " " + datos.Paterno + " " + datos.Materno;
                if (doc == 1) {
                    nombre = datos.RazonSocial;
                }
                $("#txtNombre").val(nombre);
                //} else {
                //    $("#txtNombre").val('');
                //    msgErrorET("No se encontro resultados en la busqueda", "txtDocumento");
                //}
            } else if (dato.result == 0) {
                $("#txtNombre").val('');
                alert(dato.message);
            }
        }
    });
}

function delAddAsociado(idDel) {
    $.ajax({
        url: '../office/DellAddAsociado',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataAsociado();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddAsociado(idUpd) {
    limpiarAsociado();

    $.ajax({
        url: '../office/ObtieneAsociadoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var aso = dato.data.Data;
                if (aso != null) {
                    $("#hidAccionMvEnt").val("1");
                    $("#hidEdicionEnt").val(aso.Id);
                    $("#ddlTipoDocumento1").val(aso.tipo_documento),
                    $("#txtDocumento").val(aso.numero_documento);
                    $("#txtNombre").val(aso.nombre),
                    $("#lbResponsable").html(aso.nombre),
                    $("#ddlRol").val(aso.rol_id);
                    $("#txtSocioAsociado").val(aso.Id);
                    $("#mvEntidad").dialog("open");
                } else {
                    alert("No se pudo obtener al asociado para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiarAsociado() {
    $("#hidAccionMvEnt").val("0");
    $("#hidEdicionEnt").val("");
    $("#ddlTipoDocumento1").val("");
    $("#txtDocumento").val("");
    $("#txtSocioAsociado").val("");
    $("#ddlRol").val("");
    $("#txtNombre").val("");
    $("#lbResponsable").html("Seleccione");

    msgErrorET("", "txtDocumento");
    msgErrorET("", "ddlRol");
    msgErrorET("", "txtSocioAsociado");
}

//-------------------------- TAB - DOCUMENTO -------------------------------------  
function loadDataDocumento() {
    loadDataGridTmp('ListarDocumento', "#gridDocumento");
}

//function addDocumento() {
//    var cuenta = $('#fuFiles').uploadifySettings('queueSize');
//    var IdAdd = 0;
//    if ($("#hidAccionMvDoc").val() === "1") IdAdd = $("#hidEdicionDoc").val();

//    var documento = {
//        Id: IdAdd,
//        TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
//        TipoDocumentoDesc: $("#ddlTipoDocumento option:selected").text(),
//        FechaRecepcion: $("#txtFecha").val(),
//        Archivo: "file"
//    };

//    /*ES EDICION DE DOCUMENTOS*/
//    if ($("#hidAccionMvDoc").val() === "1") {
//        /*SI SELECCIONO DOCUMENTO PARA CAMBIAR AL ITEM SELECCIONADO
//        * SE REALIZA EL UPLOAD Y ACTUALIZA LOS DATOS 
//        */
//        if (cuenta > 0) {
//            $('#fuFiles').uploadifySettings('scriptData', documento);
//            $('#fuFiles').uploadifyUpload();
//            loadDataDocumento();
//            loadDataDocumento();
//        } else {
//            /*CASO CONTRARIO ACTUALIZA SOLO LA IMFORMACION MAS NO EL DOCUMENTO ADJUNTO*/
//            $.ajax({
//                url: 'AddDocumento',
//                type: 'POST',
//                data: documento,
//                beforeSend: function () { },
//                success: function (response) {
//                    var dato = response;
//                    validarRedirect(dato);
//                    if (dato.result == 1) {
//                        loadDataDocumento();
//                        loadDataDocumento();
//                    } else if (dato.result == 0) {
//                        alert(dato.message);
//                    }
//                }
//            });
//        }
//    } else {
//        /* LA ACCION PROVIENEN DEL NUEVO DOCUMENTO*/
//        if (cuenta > 0) {
//            $('#fuFiles').uploadifySettings('scriptData', documento);
//            $('#fuFiles').uploadifyUpload();
//            loadDataDocumento();
//            loadDataDocumento();
//        } else {
//            alert("Seleccione un archivo para adjuntar al registro de documento.");
//            return false;
//        }
//    }
//    $("#mvDocumento").dialog("close");
//}

function addDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    var IdAdd = 0;
    if ($("#hidAccionMvDoc").val() === "1") {
        IdAdd = $("#hidEdicionDoc").val();
    }

    $("#txtFecha").addClass("requerido");
    if (IdAdd > 0) {
        $("#file_upload").removeClass("requerido");
    } else {
        $("#file_upload").addClass("requerido");
    }

    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, K_DIV_POPUP.DOCUMENTO)) {
        var documento = {
            Id: IdAdd,
            TipoDocumento: $("#ddlTipoDocumento option:selected").val(),
            TipoDocumentoDesc: $("#ddlTipoDocumento option:selected").text(),
            FechaRecepcion: $("#txtFecha").val(),
            Archivo: $("#hidNombreFile").val()
        };

        $.ajax({
            url: 'AddDocumento',
            type: 'POST',
            data: documento,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    if ($("#file_upload").val() != "") {
                        InitUploadTabDocOficina("file_upload", dato.Code);
                    }
                    loadDataDocumento();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvDocumento").dialog("close");
    }
    //else {
    //    msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "Debe ingresar los campos requeridos");
    //}
}

function limpiarDocumento() {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO, "");
    $("#hidNombreFile").val("");
    $('#file_upload').css({ 'border': '1px solid gray' });
    $("#txtFecha").val("");
    $("#file_upload").val("");

}

function delAddDocumento(idDel) {
    $.ajax({
        url: '../Office/DellAddDocumento',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataDocumento();
                loadDataDocumento();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddDocumento(idUpd) {
    limpiarDocumento();
    $.ajax({
        url: '../Office/ObtieneDocumentoTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var doc = dato.data.Data;
                if (doc != null) {
                    $("#hidAccionMvDoc").val("1");
                    $("#hidEdicionDoc").val(doc.Id);
                    $("#ddlTipoDocumento").val(doc.TipoDocumento);
                    $("#txtFecha").val(doc.FechaRecepcion);
                    $("#mvDocumento").dialog("open");
                } else {
                    alert("No se pudo obtener el documento para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
//---------------------------------------------------------------------------------- 

//-------------------------- TAB - PARAMETRO -------------------------------------  
function loadDataParametro() {
    loadDataGridTmp('ListarParametro', "#gridParametro");
}

function addParametro() {
    if ($("#txtDescripcion").val() == '') {
        $('#txtDescripcion').css({ 'border': '1px solid red' });
    } else {
        var IdAdd = 0;
        if ($("#hidAccionMvPar").val() === "1") IdAdd = $("#hidEdicionPar").val();
        var entidad = {
            Id: IdAdd,
            TipoParametro: $("#ddlTipoParametro option:selected").val(),
            Descripcion: $("#txtDescripcion").val(),
            TipoParametroDesc: $("#ddlTipoParametro option:selected").text()
        };
        $.ajax({
            url: '../Office/AddParametro',
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
        $('#txtDescripcion').css({ 'border': '1px solid gray' });
    }
}

function delAddParametro(idDel) {
    $.ajax({
        url: '../Office/DellAddParametro',
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
        url: '../Office/ObtieneParametroTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvPar").val("1");
                    $("#hidEdicionPar").val(param.Id);
                    $("#ddlTipoParametro").val(param.TipoParametro);
                    $("#txtDescripcion").val(param.Descripcion);
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
    $("#txtDescripcion").val("");
    $("#hidAccionMvPar").val("0");
    $("#hidEdicionPar").val("0");

}
//---------------------------------------------------------------------------------- 


//-------------------------- TAB - DEPENDENCIA ---------------------------------- 
function loadDataOfi() {
    loadDataGridTmp('ListarOficina', "#gridOfi");
}

function addOficina(id, nom) {
    var IdAdd = 0;
    if ($("#hidAccionMvOfi").val() === "1") IdAdd = $("#hidOfiId").val();

    var oficina = {
        OFF_ID: id,
        OFF_NAME: nom,
        SOFF_ID: IdAdd
    };

    $.ajax({
        url: '../Office/AddOficina',
        type: 'POST',
        data: oficina,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //loadDataOfi();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function delAddOficina(idDel) {
    $.ajax({
        url: '../office/DellAddOficina',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataOfi();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarOficina() {
    $("#txtObservacion").val("");
    $("#hidEdicionOfi").val("0");
    $("#hidAccionMvOfi").val("0");
}
//--------------------------------------------------------------------------------

//-------------------------- TAB - OBSERVACION ---------------------------------- 
function loadComboTipoObs() {
    $('#ddlOfiTipoObs option').remove();
    $.ajax({
        url: '../General/ListarTipoObservacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, obs) {
                    $('#ddlOfiTipoObs').append($("<option />", { value: obs.Value, text: obs.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadDataObservacion() {
    loadDataGridTmp('ListarObservacion', "#gridObservacion");
}

function addObservacion() {
    var estado = true;
    if ($("#ddlTipoObservacion").val() == 0) {
        $('#ddlTipoObservacion').css({ 'border': '1px solid red' });
        estado = false;
    }

    if ($("#txtObservacion").val() == '') {
        $('#txtObservacion').css({ 'border': '1px solid red' });
        estado = false;
    }

    if (estado) {
        var IdAdd = 0;
        if ($("#hidAccionMvObs").val() === "1") IdAdd = $("#hidEdicionObs").val();
        var observacion = {
            Id: IdAdd,
            TipoObservacion: $("#ddlTipoObservacion option:selected").val(),
            Observacion: $("#txtObservacion").val(),
            TipoObservacionDesc: $("#ddlTipoObservacion option:selected").text()
        };

        $.ajax({
            //url: '../ImpuestoValor/AddValor',
            url: '../Office/AddObservacion',
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
        $('#txtObservacion').css({ 'border': '1px solid gray' });
        $("#mvObservacion").dialog("close");
    }
}

function delAddObservacion(idDel) {
    $.ajax({
        url: '../office/DellAddObservacion',
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
        url: '../office/ObtieneObservacionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {

                    $("#hidAccionMvObs").val("1");
                    $("#hidEdicionObs").val(obs.Id);
                    $("#ddlTipoObservacion").val(obs.TipoObservacion);
                    $("#txtObservacion").val(obs.Observacion);
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
    $("#txtObservacion").val("");
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    $("#ddlTipoObservacion").val(0);
}

var grabarObs = function () {
    var idOff = id;
    var msjOfi = $("#txtOfiObs").val();
    var idTipo = $("#ddlOfiTipoObs").val();

    $.ajax({
        url: '../OFFICE/InsertarObs',
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

//-------------------------- FUNCIONES - GENERALES ----------------------------- 
function ContadorPrincipal() {
    $.ajax({
        url: '../OFFICE/ContadorPrincipal',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 0) {
                $("#chkPrincipal").show();
                $("#lblTituloPrincipal").show();

            } else {
                $("#chkPrincipal").hide();
                $("#lblTituloPrincipal").hide();
            }
        }
    })
};

function limpiar() {
    $("#txtOficinaSearch").val("");
    $("#txtOfiCodigo").val("");
    $("#txtOficina").val("");
    $("#chkPrincipal").prop('checked', false);
    $("ddlOfiDependencia").val('');
    $("#chkEnds").prop('checked', false);
}

function nuevo() {
    ContadorPrincipal();
    loadComboOficina('ddlOfiDependencia', 0);
}

function ObtenerDatos(oficinaId) {
    $.ajax({
        url: '../office/ObtieneOficina',
        data: { id: oficinaId },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var office = dato.data.Data;

                $("#txtOfiCodigo").val(office.OFF_ID);
                $("#txtOficina").val(office.OFF_NAME);
                $("#ddlTipo").val(office.OFF_TYPE);
                $("#txtCC").val(office.OFF_CC);
                $("#hidIdAgenteRecaudo").val(office.BPS_ID);
                $("#lblAgenteRecaudo").html(office.SOCIO);

                //loadTipoDivision('ddlOfiDivision', K_DIV_ADM, office.DAD_ID);//lista diviones por tipo de division
                //alert(office.HQ_IND);

                if (office.HQ_IND == "Y" || office.HQ_IND == "S") {
                    $('#chkPrincipal').prop('checked', true);
                    $("#ddlOfiDependencia").prop('disabled', true);
                    $('#chkPrincipal').show();
                    $('#lblTituloPrincipal').show();

                } else {
                    $('#chkPrincipal').prop('checked', false);
                    $("#ddlOfiDependencia").prop('disabled', false);
                    $('#lblTituloPrincipal').hide();
                    $('#chkPrincipal').hide();
                    idDep = office.SOFF_ID;
                    loadComboOficina('ddlOfiDependencia', idDep);
                };
                updAddDireccion(office.ADD_ID);
                //-------------------------- CARGA LISTAS - TABS ---------------------------------   
                loadDataParametro();
                loadDataObservacion();
                loadDataDocumento();
                loadDataOfi();
                loadDataAsociado();
                loadDataDireccionHistorial();
                loadDataDivisionAdm();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
//Grabar Oficina
function insertarOficina() {
    var idOfi = 0;
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) idOfi = $("#hidOfiId").val();

    var principal = "";
    var idDir = $("#hidEdicionDir").val();

    if ($('#chkPrincipal').is(':checked')) {
        principal = "Y";
    } else {
        principal = "N";
    }

    var oficina = {
        OFF_ID: idOfi,
        OFF_NAME: $("#txtOficina").val(),
        HQ_IND: principal,
        SOFF_ID: $("#ddlOfiDependencia").val(),
        OFF_TYPE: $("#ddlTipo").val(),
        OFF_CC: $("#txtCC").val(),
        ADD_ID: idDir,
        BPS_ID: $("#hidIdAgenteRecaudo").val(),
        OFF_ID_PRE: $("#hidOficina").val()
    };

    $.ajax({
        url: '../OFFICE/InsertarOficina',
        data: oficina,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                idDep = 0;
                alert(dato.message);
                document.location.href = '../office/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function validarDuplicado(descripcion) {
    var estado = false;
    var oficina = { OFF_NAME: $("#txtOficina").val() };

    $.ajax({
        url: '../Office/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: oficina,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
            }
        }
    });
    return estado;
}

function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            OK: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            Cancel: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}

//------------------------------------------------------------------------------
function loadDataDireccionHistorial() {
    loadDataGridTmp('ListarDireccionHistorial', "#divDir");
}

function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}

//-------------------------- TAB - DEPENDENCIA ---------------------------------- 
//function loadDataDep() {
//    var busq = $("#txtOfiSearchDep").val();
//    var id_Off = $("#hidOfiId").val();

//    $("#gridDep").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: 5,
//            transport: {
//                read: {
//                    url: "../OFFICE/ListarDep", dataType: "json", data: { dato: busq, offId: id_Off }
//                }
//            },
//            schema: { data: "BEREC_OFFICE", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: {
//            messages: {
//                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>"
//            }
//        },
//        selectable: true,
//        columns:
//            [
//                { width: 2, title: '', template: "<input type='checkbox' id='chkSelDep' class='kendo-chk-dep' name='chkSelDep'  value='${OFF_ID}'/>" },
//                { field: "OFF_ID", hidden: true, width: 3, title: "ID" },
//                { field: "HQ_IND", hidden: true, width: 10, title: "INDICADOR" },
//                { field: "SOFF_ID", hidden: true, width: 10, title: "SOFF_ID" },
//                { field: "ADD_ID", hidden: true, width: 10, title: "ADD_ID" },
//                { field: "OFF_NAME", width: 10, title: "Oficina", template: "<label for='lbls' id='chkSeloff' class='kendo-chk-off' name='chkSeloff' value='${OFF_NAME}'/>${OFF_NAME}</label> " },
//                { field: "ADDRESS", width: 10, title: "Dirección" },
//            ]
//    });
//}

//function limpiarDep() {
//    $("#txtOfiSearchDep").val("");
//}

//function eliminar(idUsuario) {
//    var codigosDel = { codigo: idUsuario };
//    $.ajax({
//        url: '../OFFICE/Eliminar',
//        type: 'POST',
//        data: codigosDel,
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}

//function agregar() {
//    var values = [];
//    var valuesOffName = [];

//    $(".k-grid-content tbody tr").each(function () {
//        var $row = $(this);
//        var checked = $row.find('.kendo-chk-dep').attr('checked');
//        if (checked == "checked") {
//            var codigoUsu = $row.find('.kendo-chk-dep').attr('value');
//            var codigoUsu = $row.find('.kendo-chk-dep').attr('value');
//            values.push(codigoUsu);
//            actualizarDep(codigoUsu);
//        }
//    });

//    if (values.length == 0) {
//        alert("Seleccione una oficina para agregar.");
//    } else {
//        loadDataDep();
//        alert("Oficina(s) agregada(s) correctamente.");
//    }
//}

//var agregarDep = function () {
//    var values = [];
//    var valuesOffName = [];

//    $(".k-grid-content tbody tr").each(function () {
//        var $row = $(this);
//        var checked = $row.find('.kendo-chk-dep').attr('checked');
//        if (checked == "checked") {
//            var idOff = $row.find('.kendo-chk-dep').attr('value');
//            var nomOff = $row.find('.kendo-chk-off').attr('value');
//            values.push(idOff);
//            valuesOffName.push(nomOff);
//            addOficina(idOff, nomOff);
//        }
//    });

//    if (values.length == 0) {
//        alert("Seleccione una oficina para agregar.");
//    } else {
//        $("#ModalNeoDep").dialog("close");
//        loadDataOfi();
//    }
//}

//function actualizarDep(id) {
//    var soffId = $("#hidOfiId").val();
//    var codigosDel = { idOff: id, soff: soffId };

//    $.ajax({
//        url: '../Office/ActualizarDep',
//        type: 'POST',
//        data: codigosDel,
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            validarRedirect(dato);
//            if (dato.result == 1) {
//            } else if (dato.result == 0) {
//                alert(dato.message);
//            }
//        }
//    });
//}


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