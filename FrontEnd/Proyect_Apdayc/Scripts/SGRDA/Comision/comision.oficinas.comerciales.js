$(function () {
    kendo.culture('es-PE');
    $("#txtFechaIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });

    InicializarFechas();

    loadTipoDerecho('ddlDerecho', '0', '0');
    loadTipoModUso('ddlOrigen', '0');
    loadTipoGrupo('ddlGrupoModalidad', '0');
    loadTipoSociedad('ddlSociedad', '0');
    loadTipoCreacion('ddlClasesCreacion', '0');
    loadTipoIncidencia('ddlIncidencia', '0');
    loadTipoObra('ddlFrecuenciaUso', '0');
    loadTipoRepertorio('ddlRepertorio', '0');
    loadAgente('ddlNivelAgente', 0);
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadEstadosMaestro("ddlEstado");
    loadOrigenComision('ddlOrigenComision', 0);
    loadOficinasComerciales('ddlOficinaComercial', 0);

    $("#btnNuevo").on("click", function () {
        location.href = "../ComisionOficinasComerciales/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        CargaGrid();
    });

    $("#btnBuscar").on("click", function () {
        CargaGrid();
    });

    loadData();
});

function editar(idSel, idSel2, idSel3) {
    document.location.href = '../ComisionOficinasComerciales/Nuevo?id=' + idSel + '&idNivAgent=' + idSel2 + '&idOficina=' + idSel3;
    $("#hidOpcionEdit").val(1);
}

var reloadEventoMod = function (idModSel) {
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    ObtenerDatosModalidad($("#hidModalidad").val());
};

function ObtenerDatosModalidad(id) {
    $.ajax({
        url: "../ComisionOficinasComerciales/ObtieneDatosModalidad",
        type: 'GET',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    loadTipoCreacion('ddlClasesCreacion', en.CLASS_COD);
                    loadTipoDerecho('ddlDerecho', en.CLASS_COD, en.RIGHT_COD);
                    loadTipoModUso('ddlOrigen', en.MOD_ORIG);
                    loadTipoGrupo('ddlGrupoModalidad', en.MOG_ID);
                    loadTipoSociedad('ddlSociedad', en.MOD_SOC);
                    loadTipoIncidencia('ddlIncidencia', en.MOD_INCID);
                    loadTipoObra('ddlFrecuenciaUso', en.MOD_USAGE);
                    loadTipoRepertorio('ddlRepertorio', en.MOD_REPER);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../ComisionOficinasComerciales/ListaComisionesOficina",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            Origen: $("#ddlOrigen").val(),
                            Sociedad: $("#ddlSociedad").val(),
                            Clases: $("#ddlClasesCreacion").val(),
                            Grupo: $("#ddlGrupoModalidad").val(),
                            Derecho: $("#ddlDerecho").val(),
                            Incidencia: $("#ddlIncidencia").val(),
                            Frecuencia: $("#ddlFrecuenciaUso").val(),
                            Repertorio: $("#ddlRepertorio").val(),
                            TipoComision: $("#ddlTipoComision").val(),
                            OrigenComision: $("#ddlOrigenComision").val(),
                            NivelAgente: $("#ddlNivelAgente").val(),
                            Oficina: $("#ddlOficinaComercial").val(),
                            FechaIni: $("#txtFechaIni").val(),
                            FechaFin: $("#txtFechaFin").val(),
                            st: $("#ddlEstado").val()
                        });
                }
            },
            schema: { data: "ListaComisionOficina", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        columns:
           [
            { title: 'Eliminar', width: 60, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOD_ID}'/>" },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "<font size=2px>PROPIETARIO</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${OWNER}</a>"
            },
            { field: "MOD_ID", width: 30, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${MOD_ID}</a>" },
            { field: "LEVEL_ID", hidden: true, width: 10, title: "IdLvel", template: "<label for='lbls' id='chkSeloff' class='kendo-chk-off' name='chkSeloff' value='${LEVEL_ID}'/>${LEVEL_ID}</label> " },
            { field: "OFF_ID", hidden: true, width: 10, title: "IdOfi", template: "<label for='lbls' id='chkSelofficina' class='kendo-chk-officina' name='chkSeloff' value='${OFF_ID}'/>${OFF_ID}</label> " },

            { field: "COM_DESC", width: 70, title: "<font size=2px>Tipo comisión</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${COM_DESC}</a></font>" },
            { field: "OFF_NAME", width: 100, title: "<font size=2px>Oficina</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${OFF_NAME}</a></font>" },
            { field: "DESCRIPTION", width: 100, title: "<font size=2px>Nivel agente</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${DESCRIPTION}</a></font>" },
            { field: "MOD_DEC", width: 120, title: "<font size=2px>Producto</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${MOD_DEC}</a></font>" },
            { field: "Formato", width: 40, title: "<font size=2px>Formato</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${Formato}</a></font>" },
            { field: "Valor", width: 60, title: "<font size=2px>Valor comisión</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray !important;'>${Valor}</a></font>" },
            { field: "COM_START", type: "date", width: 80, title: "<font size=2px>Fecha de vigencia</font>", template: "<a id='single_2' href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(COM_START==null)?"":kendo.toString(kendo.parseDate(COM_START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "ESTADO", width: 50, title: "<font size=2px>Estado</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${LEVEL_ID}','${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a>" },
           ]
    });
}

function InicializarFechas() {

    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;

    var ultimoDia = new Date(fullDate.getFullYear(), fullDate.getMonth() + 1, 0);
    var anoIni = parseInt(fullDate.getFullYear());

    var currentDateIni = "01" + "/" + twoDigitMonth + "/" + (anoIni - 1).toString();
    var currentDateFin = ultimoDia.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();

    $('#txtFechaIni').val(currentDateIni);
    $('#txtFechaFin').val(currentDateFin);
}

function limpiar() {
    loadTipoDerecho('ddlDerecho', '0', '0');
    loadTipoModUso('ddlOrigen', '0');
    loadTipoGrupo('ddlGrupoModalidad', '0');
    loadTipoSociedad('ddlSociedad', '0');
    loadTipoCreacion('ddlClasesCreacion', '0');
    loadTipoIncidencia('ddlIncidencia', '0');
    loadTipoObra('ddlFrecuenciaUso', '0');
    loadTipoRepertorio('ddlRepertorio', '0');
    loadAgente('ddlNivelAgente', 0);
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadOficinasComerciales('ddlOficinaComercial', 0);
    $("#lblModalidad").html("Seleccione");
    $("#hidModalidad").val(0);
    InicializarFechas();
}

function eliminar() {
    var values = [];
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idMod = $row.find('.kendo-chk').attr('value');
            var idNivAg = $row.find('.kendo-chk-off').attr('value');
            var idOfi = $row.find('.kendo-chk-officina').attr('value');
            values.push(idMod);
            FuncionEliminar(idMod, idNivAg, idOfi);
        }
    });
    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        CargaGrid();
    }
}

function FuncionEliminar(id, id2, id3) {
    $.ajax({
        url: '../ComisionOficinasComerciales/Eliminar',
        type: 'POST',
        data: { Id: id, idNivAgent: id2, idOficina: id3 },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function CargaGrid() {
    $('#grid').data('kendoGrid').dataSource.query({
        Origen: $("#ddlOrigen").val(),
        Sociedad: $("#ddlSociedad").val(),
        Clases: $("#ddlClasesCreacion").val(),
        Grupo: $("#ddlGrupoModalidad").val(),
        Derecho: $("#ddlDerecho").val(),
        Incidencia: $("#ddlIncidencia").val(),
        Frecuencia: $("#ddlFrecuenciaUso").val(),
        Repertorio: $("#ddlRepertorio").val(),
        TipoComision: $("#ddlTipoComision").val(),
        OrigenComision: $("#ddlOrigenComision").val(),
        NivelAgente: $("#ddlNivelAgente").val(),
        Oficina: $("#ddlOficinaComercial").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val(),
        st: $("#ddlEstado").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}