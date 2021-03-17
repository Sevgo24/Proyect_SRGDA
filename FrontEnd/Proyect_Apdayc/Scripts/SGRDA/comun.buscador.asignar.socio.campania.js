var Usuario = 0;
var Recaudador = 0;
var Asociado = 0;
var GrupoEmpresarial = 0;
var Empleado = 0;
var Proveedor = 0;

var mvInitBuscarSocioCampania = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + "mvBuscarSocioCampania" + '"> ';
    elemento += '<input type="hidden"  id="hidIdidView" value="' + "mvBuscarSocioCampania" + '"/>';
    elemento += '<input type="hidden"  id="hidId" value="' + parametro.event + ' "/>';

    elemento += '<div id="ContenedormvModalidad"></div>';
    elemento += '<div id="ContenedormvBuscarGrupoFacuracion"></div>';
    elemento += '<div id="ContenedormvEstaux"></div>';
    elemento += '<div id="ContenedormvBuscarSocio"></div>';
    elemento += '<div id="ContenedormvBuscarCorrelativo"></div>';

    elemento += '<table border=0  style=" width:100%;">';
    elemento += '<tr>';
    elemento += '<td>';
    elemento += '<table border=0 style="width:100%; ">';
    elemento += '<tr>';
    elemento += '<td>';
    elemento += '<div id="tabs">';
    elemento += '<ul>';
    elemento += '<li><a href="#tabs-1">LICENCIA</a></li>';
    elemento += '<li><a href="#tabs-2">ESTABLECIMIENTO</a></li>';
    elemento += '<li><a href="#tabs-3">SOCIO DE NEGOCIO</a></li>';
    elemento += '</ul>';

    //---------------------------------------------------LICENCIA------------------------------------------------
    elemento += '<div id="tabs-1">';
    elemento += '<div class="contenedor">';
    elemento += '<table style="width: 100%;" border="0">';
    elemento += '<input id="hidCodigo" type="hidden" />';

    elemento += '<tr>';
    elemento += '<td style="width:110px;"> Tipo Licencia: </td>'
    elemento += '<td> <select id="ddlTipoLicencia" ></select> </td>'
    elemento += '<td style="width:110px;"> Grupo Modalidad: </td>'
    elemento += '<td> <select id="ddlGrupoModalidad" ></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Modalidad Uso: </td>';
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarModalidaduso" style="cursor:pointer;" alt="Búsqueda de Modalidad de Uso" title="Búsqueda de Modalidad de Uso"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdModalidad" value="0"/> <label id="lbMod" style="cursor:pointer;" alt="Búsqueda de Modalidad de Uso" title="Búsqueda de Modalidad de Uso">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Grupo Facturación: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarGRUPO" style="cursor:pointer;" alt="Búsqueda de Grupo Facturación" title="Búsqueda de Grupo Facturación"/> </td>';
    elemento += '<td> <input type="hidden" id="hidGrupo"/> <label id="lbGrupo" style="cursor:pointer;" alt="Búsqueda de Grupo Facturación" title="Búsqueda de Grupo Facturación">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td> Temporalidad: </td>'
    elemento += '<td> <select id="ddlTemporalidad" ></select> </td>'
    elemento += '<td> Estado Licencia: </td>'
    elemento += '<td> <select id="ddlEstadoLicencia" ></select> </td>'
    elemento += '<td style="width:70px;"> Serie Fact: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarCorrelativo" style="cursor:pointer;" alt="Búsqueda de Serie" title="Búsqueda de Serie"/> </td>';
    elemento += '<td> <input type="hidden" id="hidCorrelativo"/> <label id="lbCorrelativo" style="cursor:pointer;" alt="Búsqueda de Serie" title="Búsqueda de Serie">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table>';
    elemento += '</div>';
    elemento += '</div>';
    //-------------------------------------------ESTABLECIMIENTO---------------------------------------------------


    //-------------------------------------------------------------------------------------------------------------
    elemento += '<div id="tabs-2">';
    elemento += '<div class="contenedor">';
    elemento += '<table style="width: 100%;" border="0">';

    elemento += '<tr>';
    elemento += '<td style="width:110px;"> Tipo Establecimiento: </td>'
    elemento += '<td style="width:100px;"> <select id="ddlTipoEstablecimiento" ></select> </td>'
    elemento += '<td style="width:150px;"> Sub Tipo Establecimiento: </td>'
    elemento += '<td> <select id="ddlSubTipoEstablecimiento" ></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:70px;"> Establecimiento: </td>'
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    elemento += '<td> <img src="../Images/botones/buscar.png" id="btnBuscarEstablecimientoAux" style="cursor:pointer;" alt="Búsqueda de Establecimiento" title="Búsqueda de Serie"/> </td>';
    elemento += '<td> <input type="hidden" id="hidIdEstablecimiento"/> <label id="lblEstAux" style="cursor:pointer;" alt="Búsqueda de Establecimiento" title="Búsqueda de Establecimiento">Todos</label> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table>';
    elemento += '</div>';
    elemento += '</div>';
    //-------------------------------------------------------------------------------------------------------------


    //---------------------------------------SOCIO DE NEGOCIO------------------------------------------------------
    elemento += '<div id="tabs-3">';
    elemento += '<div class="contenedor">';
    elemento += '<table style="width: 100%;" border="0">';
    elemento += '<tr>';
    elemento += '<td style="width:110px;"> Tipo Persona: </td>'
    elemento += '<td style="width:150px;"> <select id="ddlTipoPersonaBPS" ></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:120px;"> Tipo de Documento: </td>'
    elemento += '<td> <select id="ddlTipoIdBPS" style="width:177px;"></select> </td>'
    elemento += '<td> <input type="text" id="txtNumdoc" style="width:100px;"/></select> </td>'
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:130px;">Nombre / Razón Social:</td><td colspan="3"><input type="text" id="txtRazonSocialBPS" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Ubigeo:</td><td colspan="3"><input type="text" id="txtUbigeoBPS" size="70"><input type="hidden" id="hidUbigeoBPS"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    //elemento += '<td colspan="6">';
    //elemento += '<div class="checkboxes">';
    //elemento += '<label for="chkUsuarioDerecho"><input type="checkbox"  id="chkUsuarioDerecho" checked="checked"/><span>Derecho</span></label>&nbsp;';
    //elemento += '<label for="chkRecaudador"><input type="checkbox"   id="chkRecaudador" checked="checked"/><span>Recaudador</span></label>&nbsp;';
    //elemento += '<label for="chkEmpleado"><input type="checkbox"  id="chkEmpleado" checked="checked"/><span>Empleado</span> </label>';
    //elemento += '<label for="chkAsociacion"><input type="checkbox"   id="chkAsociacion" checked="checked"/><span>Asociacion</span> </label>&nbsp;';
    //elemento += '<label for="chkGrupo"><input type="checkbox"  id="chkGrupo" checked="checked"/><span>Grupo</span></label>&nbsp;';
    //elemento += '<label for="chkProveedor"><input type="checkbox"  id="chkProveedor" checked="checked"/><span>Proveedor</span></label>&nbsp;';
    //elemento += '<label for="chkFiltro" id="lblFiltro"><input type="checkbox" id="chkFiltro" checked="checked"/><span>Seleccionar todo</span></label>';
    //elemento += '</div>';
    //elemento += '</td>';

    elemento += '<td>Perfil</td>';
    elemento += '<td><select id="ddlPerfil" style="width:177px;"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>Estado:</td><td colspan="3"><select id="ddlEstadoBPS" /></td>';
    elemento += '</tr>';


    elemento += '</table>';
    elemento += '</div>';
    elemento += '</div>';
    //-------------------------------------------------------------------------------------------------------------

    elemento += '</div>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table>';



    elemento += '<table style="width: 100%;" border="0" >';
    elemento += '<tr>';
    elemento += '<td><center><button id="btnBuscarS"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarS"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center></td>';
    elemento += '<td style="text-align: right; width:9%"><button id="btnAsignar" > <img src="../Images/botones/finalizar.png"  width="16px" style="text-align:left"> Asignar </button></td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '<table>';
    elemento += '<tr>';
    elemento += '<td colspan="6" align="center"><div id="gridConsulta"></div></td>';
    elemento += '</tr>';


    elemento += '</table>';

    //elemento += '<table>';
    //elemento += '<tr>';
    //elemento += '<td colspan="20" ><button id="btnAsignar" > <img src="../Images/botones/finalizar.png"  width="16px" style="text-align:left"> Buscar </button></td>';
    //elemento += '</tr>';
    //elemento += '</table>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';

    $("#" + parametro.container).append(elemento);
    $("#mvBuscarSocioCampania").dialog({
        modal: true,
        autoOpen: false,
        width: 980,
        height: 500,
        title: "Búsqueda General Asignar socios a campaña."
    });

    loadTipoDocumento("ddlTipoIdBPS", 0);
    loadTipoPersona("ddlTipoPersonaBPS", 0);
    loadEstados("ddlEstadoBPS", 0);
    initAutoCompletarUbigeoB("txtUbigeoBPS", "hidUbigeoBPS", "604");


    //var tipoDivisionADM = 'ADM'
    //loadMonedas('ddlMoneda', '0');
    //loadTipoLicencia('dllTipoLicencia', '0');
    //loadTipoPago('ddlFormapago', '0');
    //$('#ddlFormapago').append($("<option />", { value: '0', text: '--SELECCIONE--' }));
    //loadDivisionXTipo('ddlDivisionAdministrativa', tipoDivisionADM);
    //$('#txtNumFact').on("keypress", function (e) { return solonumeros(e); });
    //$('#txtNumLic').on("keypress", function (e) { return solonumeros(e); });
    //$('#txtIdFact').on("keypress", function (e) { return solonumeros(e); });

    //limpiar();
    //kendo.culture('es-PE');
    //$('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$("#txtFecInicial").data("kendoDatePicker").value(new Date());
    //var d = $("#txtFecInicial").data("kendoDatePicker").value();
    //$("#txtFecFinal").data("kendoDatePicker").value(new Date());
    //var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();


    mvInitModalidadUsoaux({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarModalidaduso", idDivMV: "mvMod", event: "reloadEventoModalidad", idLabelToSearch: "lbMod" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRUPO", idDivMV: "mvBuscarGrupo", event: "reloadEventoGrupo", idLabelToSearch: "lbGrupo" });
    mvInitEstablecimiento({ container: "ContenedormvEstaux", idButtonToSearch: "btnBuscarEstablecimientoAux", idDivMV: "mvEstablecimientoAux", event: "reloadEventoEstablecimientoAux", idLabelToSearch: "lblEstAux" });
    mvInitBuscarCorrelativo({ container: "ContenedormvBuscarCorrelativo", idButtonToSearch: "btnBuscarCorrelativo", idDivMV: "mvBuscarCorrelativo", event: "reloadEventoCorrelativo", idLabelToSearch: "lbCorrelativo" });

    //TIPOS DE ESTABLECIMIENTO
    loadTipoestablecimiento('ddlTipoEstablecimiento', 0);
    //SUBTIPO DE ESTABLECIMIENTO
    $('#ddlSubTipoEstablecimiento').append($("<option />", { value: 0, text: '--SELECCIONE--' }));

    $("#ddlTipoEstablecimiento").on("change", function () {
        var codigo = $("#ddlTipoEstablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubTipoEstablecimiento', codigo);
    });

    //PERFIL
    //$('#' + 'ddlPerfil').append($("<option />", { value: 0, text: '--SELECCIONE--' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 1, text: 'USUARIO' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 2, text: 'RECAUDO' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 3, text: 'ASOCIACION' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 4, text: 'GRUPO EMPRESARIAL' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 5, text: 'EMPLEADO' }));
    //$('#' + 'ddlPerfil').append($("<option />", { value: 6, text: 'PROVEEDOR' }));

    loadTipoLicencia('ddlTipoLicencia', '0');
    loadGrupoModalidad('ddlGrupoModalidad', '0');

    //$("#ddlGrupoModalidad").on("change", function () {
    //    var id = $("#ddlGrupoModalidad").val();
    //    loadTemporalidad('ddlTemporalidad', id, 0);
    //});

    loadTemporalidades('ddlTemporalidad', '0', '--SELECCIONE--');
    LoadWorkFlowEstado('ddlEstadoLicencia', '0');
    //loadTemporalidad('ddlTemporalidad', '0', 'TODOS');
    //loadEstadoWF({
    //    control: 'ddlEstadoLicencia',
    //    valSel: 0,
    //    idFiltro: 0,
    //    addItemAll: true
    //});

    $("#btnBuscarS").on("click", function () {
        ObtenerDatosConsulta();
    });

    $("#btnLimpiarS").on("click", function () {
        btnLimpiarS();
    });

    $("#ddlPerfil").on("change", function () {
        if ($("#ddlPerfil").val() == 0) {
            Usuario = 0;
            Recaudador = 0;
            Asociado = 0;
            GrupoEmpresarial = 0;
            Empleado = 0;
            Proveedor = 0;
        }
        //usuario
        if ($("#ddlPerfil").val() == 1) {
            Usuario = 1;
            Recaudador = 0;
            Asociado = 0;
            GrupoEmpresarial = 0;
            Empleado = 0;
            Proveedor = 0;
        }
        //Recaudador
        if ($("#ddlPerfil").val() == 2) {
            Usuario = 0;
            Recaudador = 1;
            Asociado = 0;
            GrupoEmpresarial = 0;
            Empleado = 0;
            Proveedor = 0;
        }
        //Asociado
        if ($("#ddlPerfil").val() == 3) {
            Usuario = 0;
            Recaudador = 0;
            Asociado = 1;
            GrupoEmpresarial = 0;
            Empleado = 0;
            Proveedor = 0;
        }
        //GrupoEmpresarial
        if ($("#ddlPerfil").val() == 4) {
            Usuario = 0;
            Recaudador = 0;
            Asociado = 0;
            GrupoEmpresarial = 1;
            Empleado = 0;
            Proveedor = 0;
        }
        //Empleado
        if ($("#ddlPerfil").val() == 5) {
            Usuario = 0;
            Recaudador = 0;
            Asociado = 0;
            GrupoEmpresarial = 0;
            Empleado = 1;
            Proveedor = 0;
        }
        //Proveedor
        if ($("#ddlPerfil").val() == 6) {
            Usuario = 0;
            Recaudador = 0;
            Asociado = 0;
            GrupoEmpresarial = 0;
            Empleado = 0;
            Proveedor = 1;
        }
    });

    $("#btnAsignar").on('click', function () {
        obtenerSociosAsignarCampania();
    });

    $("#btnAsignar").hide();
};

function ObtenerDatosConsulta() {
    //------------------------------------------------------------
    var TipoLicencia = $("#ddlTipoLicencia").val() == "" ? 0 : $("#ddlTipoLicencia").val();
    var ModalidadUso = $("#hidIdModalidad").val() == "" ? 0 : $("#hidIdModalidad").val();
    var GrupoModalidad = $("#ddlGrupoModalidad").val() == 0 ? "" : $("#ddlGrupoModalidad").val();
    var GrupoFacturacion = $("#hidGrupo").val() == "" ? 0 : $("#hidGrupo").val();
    var Temporalidad = $("#ddlTemporalidad").val() == "" ? 0 : $("#ddlTemporalidad").val();
    //var EstadoLicencia = $("#ddlEstadoLicencia").val(); averiguar 
    var SerieFactura = $("#hidCorrelativo").val() == "" ? 0 : $("#hidCorrelativo").val();
    //------------------------------------------------------------    
    var TipoEstablecimiento = $("#ddlTipoEstablecimiento").val() == "" ? 0 : $("#ddlTipoEstablecimiento").val();
    var SubTipoEstablecimiento = $("#ddlSubTipoEstablecimiento").val() == "" ? 0 : $("#ddlSubTipoEstablecimiento").val();
    var Establecimiento = $("#hidIdEstablecimiento").val() == "" ? 0 : $("#hidIdEstablecimiento").val();
    //------------------------------------------------------------  
    var TipoPersona = $("#ddlTipoPersonaBPS").val();
    var TipoDocumento = $("#ddlTipoIdBPS").val();
    var NumeroDocumento = $("#txtNumdoc").val();
    var NombreRazonSocial = $("#txtRazonSocialBPS").val();
    var Ubigeo = $("#hidUbigeoBPS").val() == "" ? 0 : $("#hidUbigeoBPS").val();
    var EstadoBPS = $("#ddlEstadoBPS").val() == 2 ? 0 : $("#ddlEstadoBPS").val();

    //------------------------------------------------------------

    $.ajax({
        //url: '..CampaniaCallCenter/ListarAsignarSocioCampaniaCab',
        url: '../CampaniaCallCenter/ObtieneListaAsignarSocioCampaña',
        type: 'POST',
        data: {
            idTipoLic: TipoLicencia,
            idMod: ModalidadUso,
            idGrupoMod: GrupoModalidad,
            idGrupoFac: GrupoFacturacion,
            idTemp: Temporalidad,
            idSerie: SerieFactura,
            idEst: Establecimiento,
            idSubtipoEst: SubTipoEstablecimiento,
            idTipoEst: TipoEstablecimiento,
            idTipoPersona: TipoPersona,
            idTipoDoc: TipoDocumento,
            numeroDoc: NumeroDocumento,
            socio: NombreRazonSocial,
            idUbigeo: Ubigeo,
            usuario: Usuario,
            recaudador: Recaudador,
            asociado: Asociado,
            grupo: GrupoEmpresarial,
            empleado: Empleado,
            proveedor: Proveedor,
            Estado: EstadoBPS
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataCabecera();
                $("#btnAsignar").show();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

var reloadEventoEstablecimientoAux = function (idSel) {
    $("#hidIdEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstAux");
};

var reloadEventoModalidad = function (idModSel) {
    $("#lblModalidad").css('color', 'black');
    $("#hidIdModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lbMod");
    //ObtenerDatosModalidad($("#hidIdModalidad").val());
    //var modalidad = $("#lblModalidad").val();
    //$("#txtModalidad").val(modalidad);
    //var estado = validarPeriodocidad();
};

var reloadEventoGrupo = function (idSel) {
    $("#hidGrupo").val(idSel);
    //$("#hidEdicionEntGRU").val(idSel);
    obtenerNombreGrupo($("#hidGrupo").val(), 'lbGrupo');
};

function obtenerNombreGrupo(id, control) {
    $.ajax({
        url: "../GrupoFacturacion/Obtiene",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var tipo = dato.data.Data;
                if (tipo != null) {
                    $('#lbGrupo').html(tipo.INVG_DESC);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

var reloadEventoCorrelativo = function (idSel) {
    $("#hidCorrelativo").val(idSel);
    obtenerNombreCorrelativo($("#hidCorrelativo").val());
};

function obtenerNombreCorrelativo(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativo").html(cor.NMR_SERIAL);
                //$("#hidSerie").val(cor.NMR_SERIAL);
                $("#hidCorrelativo").val(cor.NMR_ID);
                $("#hidActual").val(cor.NMR_NOW);
                $("#lbCorrelativo").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function btnLimpiarS() {
    $("#ddlTipoLicencia").val(0);
    $("#ddlGrupoModalidad").val(0);
    $("#lbMod").html("Todos");
    $("#hidIdModalidad").val(0);
    $("#lbGrupo").html("Todos");
    $("#hidGrupo").val(0);
    $("#ddlTemporalidad").val(0);
    $("#ddlEstadoLicencia").val(0);
    $("#lbCorrelativo").html("Todos");
    $("#hidCorrelativo").val(0);

    $("#ddlTipoEmisora").val(0);
    $("#ddlSubTipoEstablecimiento").val(0);
    $("#lblEstAux").html("Todos");
    $("#hidIdEstablecimiento").val(0);

    $("#ddlTipoPersonaBPS").val(0);
    $("#ddlTipoIdBPS").val(0);
    $("#txtNumdoc").val("");
    $("#txtRazonSocialBPS").val("");
    $("#txtUbigeoBPS").val("");
    $("#ddlPerfil").val(0);
    $("#ddlEstadoBPS").val(0);
}

function loadDataCabecera() {
    loadDataGridTmp('ListarAsignarSocioCampaniaCab', "#gridConsulta");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function verDeta(id) {
    if ($("#expand" + id).attr('src') == '../Images/botones/less.png') {
        $("#expand" + id).attr('src', '../Images/botones/more.png');
        $("#expand" + id).attr('title', 'ver detalle.');
        $("#expand" + id).attr('alt', 'ver detalle.');
        $("#div" + id).css("display", "none");

    } else {
        $("#expand" + id).attr('src', '../Images/botones/less.png');
        $("#expand" + id).attr('title', 'ocultar detalle.');
        $("#expand" + id).attr('alt', 'ocultar detalle.');
        $("#div" + id).css("display", "inline");
    }
    return false;
}

function verSubDeta(id) {
    if ($("#expandSub" + id).attr('src') == '../Images/botones/less.png') {
        $("#expandSub" + id).attr('src', '../Images/botones/more.png');
        $("#expandSub" + id).attr('title', 'ver sub detalle.');
        $("#expandSub" + id).attr('alt', 'ver sub detalle.');
        $("#div" + id).css("display", "none");

    } else {
        $("#expandSub" + id).attr('src', '../Images/botones/less.png');
        $("#expandSub" + id).attr('title', 'ocultar sub detalle.');
        $("#expandSub" + id).attr('alt', 'ocultar sub detalle.');
        $("#div" + id).css("display", "inline");
    }
    return false;
}

function obtenerSociosAsignarCampania() {
    var Asignar = [];
    var contador = 0;
    $('#tblAsignarCampania tr').each(function () {
        var id = parseFloat($(this).find("td").eq(3).html());
        var cab = $(this).find(".IdClass").html();
        var checked = $(this).find('.chk').attr('checked');

        if (checked == "checked") {
            if (!isNaN(cab)) {
                if (cab != null) {
                    Asignar[contador] = {
                        idSocio: $('#txtId_' + id).val(),
                        idCampania: $("#hidCodigo").val()
                    };
                    contador += 1;
                }
            }
        }
    });

    var Asignar = JSON.stringify({ 'Asignar': Asignar });

    if (contador == 0)
    {
        alert("Seleccione uno o mas socios para asignar.");
        return;
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../CampaniaCallCenter/ObtenerSocioAsignarCampania',
        data: Asignar,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
            }
            else {
                alert(dato.message);
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
