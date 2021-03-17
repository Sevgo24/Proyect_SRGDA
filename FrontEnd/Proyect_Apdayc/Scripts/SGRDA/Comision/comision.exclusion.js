$(function () {
    kendo.culture('es-PE');
    $("#txtFechaIni").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy" });
    mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
    mvInitBuscarTarifa({ container: "ContenedormvBuscarTarifa", idButtonToSearch: "btnBuscarTarifa", idDivMV: "mvBuscarTarifa", event: "reloadEventoTarifa", idLabelToSearch: "lbTarifa" });

    InicializarFechas();
    loadTipoDerecho('ddlDerecho', '0', '0');
    loadTipoModUso('ddlOrigen', '0');
    loadTipoGrupo('ddlGrupoModalidad', '0');
    loadTipoSociedad('ddlSociedad', '0');
    loadTipoCreacion('ddlClasesCreacion', '0');
    loadTipoIncidencia('ddlIncidencia', '0');
    loadTipoObra('ddlFrecuenciaUso', '0');
    loadTipoRepertorio('ddlRepertorio', '0');
    loadDivisionesadministrativas('');
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadTarifaMantenimiento('ddlTarifa', 0);
    loadEstadosMaestro("ddlEstado");
    loadOrigenComision('ddlOrigenComision', 0);

    $("#btnNuevo").on("click", function () {
        location.href = "../ComisionExclusion/Nuevo";
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

    $("#ddlTemporalidad").on("change", function () {
        var index = document.getElementById("ddlTemporalidad").selectedIndex;
        if (index > 0) {
            var codigo = $("#ddlTemporalidad").val();
            ObtenerTarifaTemporalidad($("#hidModalidad").val(), codigo);
        }
        else
            $("#lbTarifa").html("Seleccione");
    });

    loadData();
});

function editar(idSel, idSel2) {
    document.location.href = '../ComisionExclusion/Nuevo?id=' + idSel + '&idDivAdm=' + idSel2;
    $("#hidOpcionEdit").val(1);
}

var reloadEventoMod = function (idModSel) {
    $("#hididPeriodicidad").val(0);
    $("#hididTarifa").val(0);
    $("#lbTarifa").html("Seleccione");
    $("#hidModalidad").val(idModSel);
    obtenerNombreModalidad(idModSel, "lblModalidad");
    ObtenerDatosModalidad($("#hidModalidad").val());
    loadTemporalidad('ddlTemporalidad', $("#hidModalidad").val(), 0);
};

var reloadEventoTarifa = function (idSel) {
    $("#hididTarifa").val(idSel);
    obtenerNombreConsultaTarifa($("#hididTarifa").val());
};

function obtenerNombreConsultaTarifa(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/ObtenerNombreTarifa',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbTarifa").html(dato.valor);
            }
        }
    });
}

function ObtenerDatosModalidad(id) {
    $.ajax({
        url: "../ComisionExclusion/ObtieneDatosModalidad",
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

function ObtenerTarifaTemporalidad(idMod, idTemp) {
    $.ajax({
        url: "../ComisionExclusion/ObtieneTarifaTemporalidad",
        type: 'GET',
        data: { idModalidad: idMod, idTemporalidad: idTemp },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var en = dato.data.Data;
                if (en != null) {
                    $("#hididPeriodicidad").val(en.RAT_FID);
                    $("#hididTarifa").val(en.RATE_ID);
                    $("#lbTarifa").html(en.NAME);
                }
            } else if (dato.result == 0) {
                $("#lbTarifa").html("");
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
                    url: "../ComisionExclusion/ListaComisionesExDiv",
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
                            Tarifa: $("#hididTarifa").val() == "" ? 0 : $("#hididTarifa").val(),
                            TipoComision: $("#ddlTipoComision").val(),
                            OrigenComision: $("#ddlOrigenComision").val(),
                            Division: $("#ddlDivisionAdministrativa").val(),
                            FechaIni: $("#txtFechaIni").val(),
                            FechaFin: $("#txtFechaFin").val(),
                            st: $("#ddlEstado").val()
                        });
                }
            },
            schema: { data: "ListaComisionEx", total: 'TotalVirtual' }
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
                field: "OWNER", width: 10, title: "<font size=2px>PROPIETARIO</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${OWNER}</a>"
            },
            {
                hidden: true,
                field: "RowNumber", width: 50, title: "<font size=2px>IdRowNumber</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${RowNumber}</a>"
            },
            { field: "MOD_ID", width: 50, title: "<font size=2px>Id</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${MOD_ID}</a>" },
            
            { field: "DADV_ID",hidden: true, width: 10, title: "Id", template: "<label for='lbls' id='chkSeloff' class='kendo-chk-off' name='chkSeloff' value='${DADV_ID}'/>${DADV_ID}</label> " },

            { field: "COM_DESC", width: 70, title: "<font size=2px>Tipo comisión</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${COM_DESC}</a></font>" },
            { field: "DAD_NAME", width: 100, title: "<font size=2px>División Adm</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${DAD_NAME}</a></font>" },
            { field: "MOD_DEC", width: 120, title: "<font size=2px>Producto</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${MOD_DEC}</a></font>" },
            { field: "Formato", width: 40, title: "<font size=2px>Formato</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${Formato}</a></font>" },
            { field: "Valor", width: 60, title: "<font size=2px>Valor comisión</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray !important;'>${Valor}</a></font>" },
            { field: "COM_START", type: "date", width: 100, title: "<font size=2px>Fecha de vigencia</font>", template: "<a id='single_2' href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(COM_START==null)?"":kendo.toString(kendo.parseDate(COM_START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
            { field: "ESTADO", width: 60, title: "<font size=2px>Estado</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}','${DADV_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a>" },
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
    loadDivisionesadministrativas('');
    loadFormatoComision('ddlFormatoComision', 0);
    loadTipoComision('ddlTipoComision', 0);
    loadTarifaMantenimiento('ddlTarifa', 0);
    loadTemporalidad('ddlTemporalidad', 0);
    $("#lblModalidad").html("Seleccione");
    $("#hidModalidad").val(0); 
    $("#hididPeriodicidad").val(0);
    $("#hididTarifa").val(0);
    $("#lbTarifa").html("Seleccione");
    InicializarFechas();
}

function eliminar() {
    var values = [];
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var idMod = $row.find('.kendo-chk').attr('value');
            var idDiv = $row.find('.kendo-chk-off').attr('value');
            values.push(idMod);
            FuncionEliminar(idMod, idDiv);
        }
    });
    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        CargaGrid();
    }
}

function FuncionEliminar(id, id2) {
    $.ajax({
        url: '../ComisionExclusion/Eliminar',
        type: 'POST',
        data: { Id: id, idDivAdm: id2 },
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
        Tarifa: $("#hididTarifa").val() == "" ? 0 : $("#hididTarifa").val(),
        TipoComision: $("#ddlTarifa").val(),
        OrigenComision: $("#ddlOrigenComision").val(),
        Division: $("#ddlDivisionAdministrativa").val(),
        FechaIni: $("#txtFechaIni").val(),
        FechaFin: $("#txtFechaFin").val(),
        st: $("#ddlEstado").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}