var codigo;
var st;
$(function () {

    $("#txtCodigo").focus();

    var eventoKP = "keypress";
    $('#txtCodigo').on(eventoKP, function (e) { return solonumeros(e); });

    loadMonedas('ddlMoneda','0')
    loadTipoObra('ddlModUso', '0');
    loadTipoIncidencia('ddlNivInc', '0');
    loadTipoSociedad('ddlTipSoci', '0');
    loadTipoRepertorio('ddlModUsoRepert', '0');
    loadEstadosMaestro("ddlEstado");
    mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });

    $("#btnBuscar").on("click", function () {
        if ($("#txtCodigo").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigo").val();

        if ($("#ddlEstado").val() == "")
            st = 0;
        else
            st = $("#ddlEstado").val();

        $('#grid').data('kendoGrid').dataSource.query({
            IdTarifa: codigo,
            moneda: $('#ddlMoneda').val(),
            moduso: $('#ddlModUso').val(),
            incidencia: $('#ddlNivInc').val(),
            sociedad: $('#ddlTipSoci').val(),
            repertorio: $('#ddlModUsoRepert').val(),
            IdModalidad: $('#hidModalidad').val() == "" ? 0 : $('#hidModalidad').val(),
            st: st,
            descripcion: $('#txtDescripcion').val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });
    
    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            if ($("#txtCodigo").val() == "")
                codigo = 0;
            else
                codigo = $("#txtCodigo").val();

            if ($("#ddlEstado").val() == "")
                st = 0;
            else
                st = $("#ddlEstado").val();

            $('#grid').data('kendoGrid').dataSource.query({
                IdTarifa: codigo,
                moneda: $('#ddlMoneda').val(),
                moduso: $('#ddlModUso').val(),
                incidencia: $('#ddlNivInc').val(),
                sociedad: $('#ddlTipSoci').val(),
                repertorio: $('#ddlModUsoRepert').val(),
                IdModalidad: $('#hidModalidad').val() == "" ? 0 : $('#hidModalidad').val(),
                st: st,
                descripcion: $('#txtDescripcion').val(),
                page: 1, pageSize: K_PAGINACION.LISTAR_15
            });
        }
    });


    $("#btnNuevo").on("click", function () {
        document.location.href = '../Tarifa/Nuevo';
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtCodigo").val("");
        $("#lbModalidad").val("");
        $("#hidModalidad").val(0);
        $("#ddlMoneda").val(0);
        $("#ddlModUso").val(0);
        $("#ddlNivInc").val(0);
        $("#ddlTipSoci").val(0);
        $("#ddlModUsoRepert").val(0);
        $("#ddlEstado").val(1);
        $("#txtDescripcion").val('');
    });

    loadData();
});

var reloadEventoModalidad = function (idSel) {
    $("#hidModalidad").val(idSel);
    //"hidTipoUso"
    //"hidNivelIncidencia"
    //"hidSociedad"
    //"hidRepertorio"
    obtenerModalidad(idSel, "lbModalidad", "ddlModUso", "ddlNivInc", "ddlTipSoci", "ddlModUsoRepert");
};

$("#btnNuevo").on("click", function () {
    location.href = "../Tarifa/Nuevo";
});

function editar(idSel) {
    document.location.href = '../Tarifa/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {

    if ($("#txtCodigo").val() == "")
        codigo = 0;
    else
        codigo = $("#txtCodigo").val();

    if ($("#ddlEstado").val() == "")
        st = 0;
    else
        st = $("#ddlEstado").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    url: "../Tarifa/ListarTarifa",
                    dataType: "json"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options,     {
                            IdTarifa: codigo,
                            moneda: $('#ddlMoneda').val(),
                            moduso: $('#ddlModUso').val(),
                            incidencia: $('#ddlNivInc').val(),
                            sociedad: $('#ddlTipSoci').val(),
                            repertorio: $('#ddlModUsoRepert').val(),
                            IdModalidad: $('#hidModalidad').val() == "" ? 0 : $('#hidModalidad').val(),                            
                            st: st,
                            descripcion: $('#txtDescripcion').val()
                        })
                }
            },
            schema: { data: "listaTarifa", total: 'TotalVirtual' }
        },
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        navigatable: true,
        scrollable: true,
        columns:
			[
				{ title: 'Eliminar', width: 100, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${RATE_ID}'/>" },
				{ field: "RATE_ID", width: 50, title: "Id", template: "<a id='single_2'  href=javascript:editar('${RATE_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RATE_ID}</a>" },
				{ field: "RATE_LDESC", title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${RATE_ID}') style='color:gray;text-decoration:none;font-size:11px'>${RATE_LDESC}</a>" },
                { field: "MOD_DEC", title: "Mondalidad", template: "<a id='single_2'  href=javascript:editar('${RATE_ID}') style='color:gray;text-decoration:none;font-size:11px'>${MOD_DEC}</a>" },
                {
                    field: "RATE_START",
                    width: 130,
                    type: "date",
                    title: "Fecha Vigencia",
                    template: "<font color='green'><a id='single_2' href=javascript:editar('${RATE_ID}') style='color:gray;text-decoration:none;font-size:11px'>" + '#=(RATE_START==null)?"":kendo.toString(kendo.parseDate(RATE_START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>"
                },
                {
                    field: "ESTADO", width: 70, title: "Estado", template: "<font color='green'><a id='single_2'  href=javascript:editar('${RATE_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a></font>"
                }
			]
    });
}

