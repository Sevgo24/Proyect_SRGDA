var K_MENSAJE = {
    LongitudCodigo: "La longitud del código debe contener 3 digitos.",
    DuplicadoCodigo: "El código ya existe, ingrese uno nuevo.",
    DuplicadoDesAbrevValor: "La Abrev. ya existe.",
    DuplicadoDesAbrevSub: "La Abrev. ya existe."
};
var K_DIV_GEO = "GEO";
/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 10;
var K_SIZE_PAGE_V = 20;
var K_WIDTH_SUB = 400;
var K_HEIGHT_SUB = 200;
var K_WIDTH_VAL = 400;
var K_HEIGHT_VAL = 210;
var K_WIDTH_CAR = 500;
var K_HEIGHT_CAR = 210;

$(function () {
    var id = (GetQueryStringParams("id"));
    $("#tabs").tabs();
    obtenerDatos(id);

    //-------------------------- CARGA TABS -----------------------------------------    
    //------SUBDIVISION    
    $("#mvSubdivision").dialog({
        autoOpen: false,
        width: K_WIDTH_SUB,
        height: K_HEIGHT_SUB,
        buttons: {
            "Agregar": addSubdivision,
            "Cancel": function () { $("#mvSubdivision").dialog("close"); $('#txtNombreSubdivision').css({ 'border': '1px solid gray' }); $('#txtAbrevSub').css({ 'border': '1px solid gray' }); msgErrorDiv("divResultValidacionSub", ""); }
        },
        modal: true
    });
    $(".addSubdivision").on("click", function () { limpiarSubdiviones(); loadSubdivisionDep('ddlDepSubdivision', id, 0); $("#mvSubdivision").dialog("open"); });
    $(".delSubdivision").on("click", function () { eliminarSubdivision(); loadDataSubdivision(id); loadSubdivisionDep('ddlDepSubdivision', id, 0); loadValSubFiltros(id); });
    $(".arbolsubdivisiones").on("click", function () {
        window.open('../Divisiones/ReporteTreeview?id=' + id + '&tipo=' + 1);
    });
    //------VALORES    
    $("#mvValores").dialog({
        autoOpen: false,
        width: K_WIDTH_VAL,
        height: K_HEIGHT_VAL,
        buttons: {
            "Agregar": addValor,
            "Cancel": function () { $("#mvValores").dialog("close"); $('#txtAbreValor').css({ 'border': '1px solid gray' }); $('#txtNombrevalor').css({ 'border': '1px solid gray' }); msgErrorDiv("divResultValidacionDes", ""); }
        },
        modal: true
    });
    $(".addvalores").on("click", function () {
        limpiarValores(); loadSubdivision('ddlSubdivision', id, 0); $("#ddlSubdivision > option").remove(); loadValoresDep('ddlDepValor', id, 0, 0); $("#mvValores").dialog("open");
    });
    $(".delvalores").on("click", function () { eliminarValores(); loadDataValores(id); });

    $(".arbolvalores").on("click", function () {
        window.open('../Divisiones/ReporteTreeview?id=' + id + '&tipo=' + 2);
    });

    //------CARACTERISTICAS    
    $("#mvCaracteristicas").dialog({
        autoOpen: false,
        width: K_WIDTH_CAR,
        height: K_HEIGHT_CAR,
        buttons: {
            "Agregar": addCaracteristica,
            "Cancel": function () { $('#txtCaracteristica').val(''); $('#txtCaracteristica').css({ 'border': '1px solid gray' }); $('#ddlSubdivisionCar').css({ 'border': '1px solid gray' }); $('#ddlValorCar').css({ 'border': '1px solid gray' }); $('#ddlCaracteristicas').css({ 'border': '1px solid gray' }); $("#mvCaracteristicas").dialog("close"); }
        },
        modal: true
    });
    $(".addcaracteristicas").on("click", function () {
        $('#txtCaracteristica').val('');
        loadSubdivision('ddlSubdivisionCar', id, 0);
        loadValoresXSubdivision('ddlValorCar', id, 0);
        loadTipoCaracteristicas('ddlCaracteristicas', 0);
        $('#ddlSubdivisionCar option[value=0]').text('<-- Seleccionar -->');
        $('#ddlValorCar option[value=0]').text('<-- Seleccionar -->');
        $("#mvCaracteristicas").dialog("open");
    });
    $(".delcaracteristicas").on("click", function () { eliminarCaracteristica(); loadDataCaracteristicas(id); });
    //--------------------------------------------------------------------------------------------

    $("#btnDescartar").on("click", function () {
        document.location.href = '../Divisiones/';
    }).button();

    $("#btnGrabar").on("click", function () {
        var estadoRequeridos = ValidarRequeridos();
        if (estadoRequeridos)
            actualizar();
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Divisiones/Nuevo';
    }).button();

    $("#ddlSubdivision").change(function () {
        var subdivision = $("#ddlSubdivision").val();
        loadValoresDep('ddlDepValor', id, subdivision);
    });

    $("#ddlSubdivisionCar").change(function () {
        var subdivision = $("#ddlSubdivisionCar").val();
        loadValoresXSubdivision('ddlValorCar', id, subdivision);
        $('#ddlValorCar option[value=0]').text('<-- Seleccionar -->');
    });

    $("#ddlTipo").prop("disabled", true);
    $("#ddlTipo").css('color', '#333333').css('border', '1px solid gray').css('background-color', 'rgb(235, 235, 228)');

    loadDataSubdivision(id);
    loadDataValores(id);
    loadDataCaracteristicas(id);

    loadValSubFiltros(id);

    $("#btnBuscarSub").on("click", function () {
        $('#gridVal').data('kendoGrid').dataSource.query({ id: id, subId: $("#ddlSubdivisionSub").val(), depId: $("#ddlDependenciaSub").val(), nombre: $("#txtNombreSub").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

    });

    $("#btnLimpiarSub").on("click", function () {
        $("#ddlSubdivisionSub").val(0);
        $("#ddlDependenciaSub").val(0);
        $("#txtNombreSub").val('');
        $('#gridVal').data('kendoGrid').dataSource.query({ id: id, subId: 0, depId: 0, nomnre: '', page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    mvInitBuscarAgenteRecaudo({ container: "ContenedormvBuscarGestor", idButtonToSearch: "Abrir", idDivMV: "mvBuscarClienteDoc", event: "addGestor", idLabelToSearch: "lbResponsable" });

    $(".addGestor").on("click", function () {
        $("#mvBuscarClienteDoc").dialog("open");
        //$("#txtRazonSocialBPS").val('');
        //if ($("#gridBPS").data("kendoGrid").dataSource.data().length > 0)
        //    $("#gridBPS").data('kendoGrid').dataSource.data([]);
    });

    mvInitBuscarCorrelativoRecibo({ container: "ContenedormvBuscarGrupoModalidad", idButtonToSearch: "btnBuscarCorrelativoBec", idDivMV: "mvBuscarGrupoModalidad", event: "reloadEventoCorrelativoBec", idLabelToSearch: "lbCorrelativoBec" });

});

//-------------------------- GRUPO MODALIDAD ----------------------------------------- 
function AbrirPoPupAddGrupoModalidad(idBps) {
    var limpiar = true;
    //var idBpsAnterior = $("#hidIdSocioDocFact").val();
    //limpiar = (idBpsAnterior == idBps) ? false : true;
    //$("#hidIdSocioDocFact").val(idBps);
    //obtenerNombreSocioX(idBps, 'lbResponsableDocFact');
    $("#mvBuscarGrupoModalidad").dialog("open");
}

// BUSQUEDA GRUPO MODALIDAD
var reloadEventoCorrelativoBec = function (idSel) {
    $("#hidCorrelativoBec").val(idSel);
    obtenerNombreCorrelativoBec($("#hidCorrelativoBec").val());
};

function obtenerNombreCorrelativoBec(idSel) {
    $.ajax({
        data: { id: idSel },
        url: '../CORRELATIVOS/ObtenerNombre',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var cor = dato.data.Data;
                $("#lbCorrelativoBec").html(cor.NMR_SERIAL);
                $("#hidSerieBec").val(cor.NMR_SERIAL);
                $("#hidActualBec").val(cor.NMR_NOW);
                $("#lbCorrelativoBec").css('color', 'black');
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}

//-------------------------- TAB | GESTRORES  ----------------------------------------- 
var addGestor = function (idSel) {
    var IdAdd = idSel;

    $.ajax({
        url: '../Divisiones/AddAgenteRecaudo',
        type: 'POST',
        data: { Id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataClienteDoc();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

};

function loadDataClienteDoc(accionActual) {
    loadDataClienteDocGridTmp('ListarAgenteRecaudo', "#gridAgenteRecaudo");
}

function loadDataClienteDocGridTmp(Controller, idGrilla, accionActual) {
    $.ajax({
        type: 'POST',
        data: { accion: accionActual },
        url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            $(idGrilla).html(dato.message);
        },
        complete: function () {
           
        }
    });
}

//-------------------------- FUNCIONES ----------------------------------------- 
function loadValSubFiltros(id) {
    var newOption = "<option value='" + "0" + "'>-- SELECCIONE --</option>";
    loadSubdivision('ddlSubdivisionSub', id, 0);
    $("#ddlSubdivisionSub > option").remove();
    $("#ddlSubdivisionSub").append(newOption);

    loadValoresDep('ddlDependenciaSub', id, 0);
    $("#ddlDependenciaSub > option").remove();
    $("#ddlDependenciaSub").append(newOption);

    $("#ddlSubdivisionSub").change(function () {
        var subdivision = $("#ddlSubdivisionSub").val();
        loadValoresDep('ddlDependenciaSub', id, subdivision);
        $("#ddlDependenciaSub > option").remove();
        $("#ddlDependenciaSub").append(newOption);
    });
}

function actualizar() {

    var vId = $("#txtId").val();
    var vNombre = $("#txtNombre").val();
    var vTipo = $("#ddlTipo").val();
    var nombrelong = $("#txtDescripcion").val();

    $.ajax({
        url: '../Divisiones/Actualizar',
        data: { id: vId, nombre: vNombre, tipo: vTipo, descripcion: nombrelong },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                document.location.href = '../Divisiones/';
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function limpiarSubdiviones() {
    $("#txtNombreSubdivision").val('');
    $("#txtAbrevSub").val('');
    $("#ddlDepSubdivision").val(0);
}

function limpiarValores() {
    $("#txtNombrevalor").val('');
    $("#txtAbreValor").val('');
}

function obtenerDatos(idDiv) {
    $.ajax({
        url: "../Divisiones/Obtener",
        type: "GET",
        data: { id: idDiv },
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var divisiones = dato.data.Data;
                $("#txtId").val(divisiones.DAD_ID);
                $("#txtNombre").val(divisiones.DAD_NAME);
                $("#hidDadType").val(divisiones.DAD_TYPE);
                loadTipoDivisiones('ddlTipo', divisiones.DAD_TYPE);
                $("#hidDAD_TYPE").val(divisiones.DAD_TYPE);
                $("#txtDescripcion").val(divisiones.DIV_DESCRIPTION);

            } else {
                (divisiones.DAD_TYPE);
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}


//-- subdivision
function loadDataSubdivision(idDiv) {
    $("#gridSub").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_SIZE_PAGE,
            transport: {
                read: {
                    url: "../Divisiones/ListarSubdivisiones",
                    dataType: "json"
                    //data: { id: idDiv }
                },

                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { id: idDiv })
                }
            },
            schema: { data: "REFDIVSUBTYPE", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
                { title: 'Eliminar', width: 2, title: 'Eliminar', template: "<input type='checkbox' id='chkSelSub' class='kendo-chk-sub' name='chkSelSub'  value='${DAD_STYPE}'/>" },
                { field: "DAD_STYPE", hidden: true, width: 2, title: "Id" },
				{ field: "DAD_ID", hidden: true, width: 10, title: "DAD_ID" },
				{ field: "DAD_SNAME", hidden: true, width: 10, title: "DAD_SNAME" },
				{ field: "DAD_BELONGS", hidden: true, width: 10, title: "DAD_BELONGS" },
				{ field: "SUBDIVISION", width: 10, title: "Nombre" },
                { field: "DEPENDENCIA", width: 10, title: "Dependencia" },
			]
    });
}

var eliminarSubdivision = function () {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk-sub').attr('checked');
        if (checked == "checked") {
            var id = $row.find('.kendo-chk-sub').attr('value');
            values.push(id);
            eliminarSub(id);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una subdivisión para eliminar.");
    } else {
        alert("Se elimino la subdivisión.");
    }
}

function eliminarSub(idSub) {
    var codigosDel = { id: idSub };

    $.ajax({
        url: '../Divisiones/EliminarSubdivision',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}

var addSubdivision = function () {
    var estadoAbrevSubdivision = validarAbrevSubdivision();
    var validacion = validarSubdivision();
    if (validacion && estadoAbrevSubdivision) {
        var vId = $("#txtId").val();
        var vDAD_NAME = $("#txtNombreSubdivision").val();
        var vDAD_SNAME = $("#txtAbrevSub").val();
        var vDAD_BELONGS = $("#ddlDepSubdivision").val();

        $.ajax({
            url: '../Divisiones/InsertarSubdivision',
            data: { id: vId, DAD_NAME: vDAD_NAME, DAD_SNAME: vDAD_SNAME, DAD_BELONGS: vDAD_BELONGS },
            type: 'POST',
            async: true,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataSubdivision(vId);
                    $("#mvSubdivision").dialog("close");

                    loadValSubFiltros(vId);
                    alert(dato.message);
                } else {
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
}

function validarSubdivision() {
    var estado = true;
    if ($("#txtNombreSubdivision").val() == '') {
        $('#txtNombreSubdivision').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#txtNombreSubdivision').css({ 'border': '1px solid gray' });
    }

    if ($("#txtAbrevSub").val() == '') {
        $('#txtAbrevSub').css({ 'border': '1px solid red' });
        msgErrorDiv("divResultValidacionSub", "");
        estado = false;
    }
    return estado;
}

function validarAbrevSubdivision() {
    var abrev = $("#txtAbrevSub").val();
    if (abrev != '') {
        var estadoDuplicado = validarDuplicadoAbrevSubdivision();
        if (!estadoDuplicado) {
            $("#txtAbrevSub").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionSub", "");
            return true;
        } else {
            $("#txtAbrevSub").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionSub", K_MENSAJE.DuplicadoDesAbrevSub);
            return false;
        }
    }
}

function validarDuplicadoAbrevSubdivision() {
    var estado = false;
    var vId = $("#txtId").val();
    var vDAD_SNAME = $("#txtAbrevSub").val();

    $.ajax({
        url: '../Divisiones/ObtenerXDesAbrevSubdivision',
        type: 'POST',
        dataType: 'JSON',
        data: { id: vId, abrev: vDAD_SNAME },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }

    });
    return estado;
}

//----------
//-- VALORES
function loadDataValores(idDiv) {
    $("#gridVal").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_SIZE_PAGE_V,
            transport: {
                read: {
                    url: "../Divisiones/ListarValores",
                    dataType: "json",
                    data: { id: idDiv, subId: 0, depId: 0, nombre: '' }
                }

                //parameterMap: function (options, operation) {
                //    if (operation == 'read')
                //        return $.extend({}, options, { id: idDiv, subId: 0, depId: 0, nombre: '' })
                //}
            },
            schema: { data: "REFDIVISIONES_VALUES", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
                { title: 'Eliminar', width: 2, title: 'Eliminar', template: "<input type='checkbox' id='chkSelVal' class='kendo-chk-val' name='chkSelVal'  value='${DADV_ID}'/>" },
                { field: "DADV_ID", hidden: true, width: 2, title: "DADV_ID" },
				{ field: "DAD_BELONGS", hidden: true, width: 10, title: "DAD_BELONGS" },
				{ field: "DAD_BELONGS", hidden: true, width: 10, title: "DAD_BELONGS" },
				{ field: "SUBDIVISION", width: 10, title: "Subdivisión" },
                { field: "DEPENDENCIA", width: 10, title: "Dependencia" },
                { field: "NOMBRE", hidden: false, width: 10, title: "Nombre" },
			]
    });
}

var eliminarValores = function () {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk-val').attr('checked');
        if (checked == "checked") {
            var id = $row.find('.kendo-chk-val').attr('value');
            values.push(id);
            eliminarVal(id);
        }
    });

    if (values.length == 0) {
        alert("Seleccione un valor para eliminar.");
    }
}

function eliminarVal(idSub) {
    var codigosDel = { id: idSub };

    $.ajax({
        url: '../Divisiones/EliminarValores',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}

var addValor = function () {
    var estadoAbrevValor = validarAbrevValor();
    var validacion = validarValor();
    if (validacion && estadoAbrevValor) {
        var id = $("#txtId").val();
        var idDep = $("#ddlDepValor").val() == '0' ? '' : $("#ddlDepValor").val();

        var valores = {
            DAD_ID: id,
            DAD_STYPE: $("#ddlSubdivision").val(),
            DAD_VCODE: $("#txtAbreValor").val(),
            DAD_VNAME: $("#txtNombrevalor").val(),
            DAD_BELONGS: idDep
        }

        $.ajax({
            url: '../Divisiones/InsertarValores',
            data: valores,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataValores(id);
                    $("#mvValores").dialog("close");
                    alert(dato.message);
                } else {
                    alert(dato.message);
                }
            }
        });
        return false;
    }
}

function validarValor() {
    var estado = true;
    if ($("#txtNombrevalor").val() == '') {
        $('#txtNombrevalor').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#txtNombrevalor').css({ 'border': '1px solid gray' });
    }

    if ($("#txtAbreValor").val() == '') {
        $('#txtAbreValor').css({ 'border': '1px solid red' });
        msgErrorDiv("divResultValidacionDes", "");
        estado = false;
    }
    return estado;
}

function validarAbrevValor() {
    var abrevValor = $("#txtAbreValor").val();
    if (abrevValor != '') {
        var estadoDuplicado = validarDuplicadoAbrevValor();
        if (!estadoDuplicado) {
            $("#txtAbreValor").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtAbreValor").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE.DuplicadoDesAbrevValor);
            return false;
        }
    }
}

function validarDuplicadoAbrevValor() {
    var estado = false;
    var id = $("#txtId").val();
    var id_division = $("#ddlTipo").val();

    if (id_division !== K_DIV_GEO) {
        var valor = {
            DAD_ID: id,
            DAD_VCODE: $("#txtAbreValor").val()
        };

        $.ajax({
            url: '../Divisiones/ObtenerXDesAbrevValor',
            type: 'POST',
            dataType: 'JSON',
            data: valor,
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
    }
    return estado;
}

//---------- CARACTERISTICA
var addCaracteristica = function () {
    var validacion = validarCaracteristica();
    if (validacion) {
        var id = $("#txtId").val();
        var caracteristica = {
            DAD_TYPE: $("#hidDAD_TYPE").val(),
            DAD_ID: id,
            DAD_STYPE: $("#ddlSubdivisionCar").val(),
            DADV_ID: $("#ddlValorCar").val(),
            DAC_ID: $("#ddlCaracteristicas").val(),
            VALUE: $("#txtCaracteristica").val()
        }

        $.ajax({
            url: '../Divisiones/InsertarCaracteristica',
            data: caracteristica,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataCaracteristicas(id);
                    $("#mvCaracteristicas").dialog("close");
                    alert(dato.message);
                } else {
                    alert(dato.message);
                }
            }
        });
        return false;
    }

}

function loadDataCaracteristicas(idDiv) {
    $("#gridCar").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_SIZE_PAGE_V,
            transport: {
                read: {
                    url: "../Divisiones/ListarCaracteristicas",
                    dataType: "json"
                    //data: { id: idDiv }
                },

                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { id: idDiv })
                }

            },
            schema: { data: "ListaCaracteristica", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
			[
                { title: 'Eliminar', width: 2, title: 'Eliminar', template: "<input type='checkbox' id='chkSelCar' class='kendo-chk-car' name='chkSelCar'  value='${CHARVAL_ID}'/>" },
                { field: "CHARVAL_ID", hidden: true, width: 2, title: "CHARVAL_ID" },
				{ field: "SUBDIVISION", width: 10, title: "Subdivisión" },
				{ field: "VALOR", width: 10, title: "Valor de Subdivisión" },
				{ field: "CARACTERISTICA", width: 10, title: "Caracteristica" },
				{ field: "VALUE", width: 10, title: "Valor de Caracteristica" },
			]
    });
}

var eliminarCaracteristica = function () {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk-car').attr('checked');
        if (checked == "checked") {
            var id = $row.find('.kendo-chk-car').attr('value');
            values.push(id);
            eliminarCar(id);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una caracteristica para eliminar.");
    }
}

function eliminarCar(idSub) {
    var codigosDel = { id: idSub };

    $.ajax({
        url: '../Divisiones/EliminarCaracteristica',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}

function validarCaracteristica() {
    var estado = true;
    if ($("#txtCaracteristica").val() == '') {
        $('#txtCaracteristica').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#txtCaracteristica').css({ 'border': '1px solid gray' });
    }

    if ($("#ddlSubdivisionCar").val() == 0) {
        $('#ddlSubdivisionCar').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#ddlSubdivisionCar').css({ 'border': '1px solid gray' });
    }

    if ($("#ddlValorCar").val() == 0) {
        $('#ddlValorCar').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#ddlValorCar').css({ 'border': '1px solid gray' });
    }

    if ($("#ddlCaracteristicas").val() == 0) {
        $('#ddlCaracteristicas').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#ddlCaracteristicas').css({ 'border': '1px solid gray' });
    }

    return estado;
}