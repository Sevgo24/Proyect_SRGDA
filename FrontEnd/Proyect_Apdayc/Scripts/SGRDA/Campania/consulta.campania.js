var K_WIDTH = 700;
var K_HEIGHT = 400;

$(function () {
    loadCentrosContactos('ddlCentroContacto', 0);
    loadTipoCampania('ddlTipoCampania', 0);

    //loadCampaniaContacto('ddlEstadoCampania', 0);
    $('#ddlEstadoCampania').append($("<option />", { value: '', text: '--SELECCIONE--' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'INI', text: 'INICIADA' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'OPE', text: 'OPERACION' }));
    $('#ddlEstadoCampania').append($("<option />", { value: 'CER', text: 'CERRADA' }));

    loadEntidades('ddlPerfilCliente', 0);
    loadEstadosMaestro('ddlEstado', 1);

    kendo.culture('es-PE');
    $('#txtFechaIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFechaFin').kendoDatePicker({ format: "dd/MM/yyyy" });

    InicializarFechas();
    loadDataCampania();

    $("#btnBuscar").on("click", function () {
        cargargrid();
    });

    $("#btnLimpiar").on("click", function (e) {
        limpiar();
        cargargrid();
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../CampaniaCallCenter/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });

    mvInitBuscarSocioCampania({ container: "ContenedormvBuscarSocioCampania", idButtonToSearch: "Abrir", idDivMV: "mvBuscarSocioCampania", event: "reloadEvento", idLabelToSearch: "lbGrupo" });

    $("#mvAsignados").dialog({ autoOpen: false, width: K_WIDTH, height: K_HEIGHT, buttons: { "Cancel": function () { $("#mvAsignados").dialog("close"); } }, modal: true });

    $("#tabs").tabs();
});

function BuscarObjeto(id) {
    $("#hidCodigo").val(id);
    $("#mvBuscarSocioCampania").dialog("open");
};

function VerAsignados(id) {
    $("#hidCodigo").val(id);
    $("#mvAsignados").dialog("open");
    loadDataAsignados(id);
}

function loadDataAsignados(Id) {
    loadDataGridDocumentosTmp('ListarClientesAsignadosCampania', "#gridAsignados", Id);
}

function loadDataGridDocumentosTmp(Controller, idGrilla, Id) {
    $.ajax({
        data: { Id: Id },
        type: 'POST', url: Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response; validarRedirect(dato);
            $(idGrilla).html(dato.message);
        }
    });
}


function cargargrid() {
    $('#gridCampaniaCallCenter').data('kendoGrid').dataSource.query({
        contacto: $("#ddlCentroContacto").val(),
        tipoCamp: $("#ddlTipoCampania").val(),
        estadoCamp: $("#ddlEstadoCampania").val() == "0" ? "" : $("#ddlEstadoCampania").val(),
        nombre: $("#txtNombre").val(),
        perfilCliente: $("#ddlPerfilCliente").val(),
        fechaIni: $("#txtFechaIni").val(),
        fechaFin: $("#txtFechaFin").val(),
        st: $("#ddlEstado").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}

function editar(idSel) {
    document.location.href = '../CampaniaCallCenter/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
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
    $("#ddlCentroContacto").val(0),
    $("#ddlTipoCampania").val(0),
    $("#ddlEstadoCampania").val(0),
    $("#txtNombre").val(""),
    $("#ddlPerfilCliente").val(0),
    $("#txtFechaIni").val(""),
    $("#txtFechaFin").val("")
    InicializarFechas();
}

function loadDataCampania() {
    $("#gridCampaniaCallCenter").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../CampaniaCallCenter/ListaPageCampaniaCallCenter",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            contacto: $("#ddlCentroContacto").val(),
                            tipoCamp: $("#ddlTipoCampania").val(),
                            estadoCamp: $("#ddlEstadoCampania").val() == "0" ? "" : $("#ddlEstadoCampania").val(),
                            nombre: $("#txtNombre").val(),
                            perfilCliente: $("#ddlPerfilCliente").val(),
                            fechaIni: $("#txtFechaIni").val(),
                            fechaFin: $("#txtFechaFin").val(),
                            st: $("#ddlEstado").val()
                        })
                }
            },
            schema: { data: "ListaCampaniaCall", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns: [
                    { title: 'Eliminar', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${CONC_CID}'/>" },
				    { field: "CONC_CID", width: 5, title: "Id", template: "<a id='single_2'  href=javascript:editar('${CONC_CID}') style='color:gray !important;'>${CONC_CID}</a>" },
				    { field: "CONC_CNAME", width: 20, title: "Nombre", template: "<a id='single_2'  href=javascript:editar('${CONC_CID}') style='color:gray !important;'>${CONC_CNAME}</a>" },
				    { field: "CONC_CTNAME", width: 30, title: "Tipo Campaña", template: "<a id='single_2'  href=javascript:editar('${CONC_CID}') style='color:gray !important;'>${CONC_CTNAME}</a>" },
				    { field: "ENT_DESC", width: 20, title: "Perfil Cliente", template: "<a id='single_2'  href=javascript:editar('${CONC_CID}') style='color:gray !important;'>${ENT_DESC}</a>" },
                    { field: "CONC_CDINI", type: "date", width: 10, title: "Fecha Inicial", template: "<a id='single_2' href=javascript:editar('${CONC_CID}') style='color:gray !important;'>" + '#=(CONC_CDINI==null)?"":kendo.toString(kendo.parseDate(CONC_CDINI,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
                    { field: "CONC_CDEND", type: "date", width: 10, title: "Fecha Final", template: "<a id='single_2' href=javascript:editar('${CONC_CID}') style='color:gray !important;'>" + '#=(CONC_CDEND==null)?"":kendo.toString(kendo.parseDate(CONC_CDEND,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a></font>" },
                    { field: "ESTADO", width: 8, title: "<font size=2px>Estado</font>", template: "<a id='single_2'  href=javascript:editar('${CONC_CID}') style='color:gray;text-decoration:none;font-size:11px'>${ESTADO}</a>" },

                    { title: "<center>Asignar</center>", width: 8, template: "<center><a id='single_2' href=javascript:BuscarObjeto('${CONC_CID}') style='color:Blue !important;'> <img src='../Images/botones/new32.png'  width='20px'> <label id='lblBuscar'></label>Buscar</a></center>" },

                    { title: "<center>Ver</center>", width: 8, template: "<center><a id='single_2' href=javascript:VerAsignados('${CONC_CID}') style='color:Blue !important;'> <img src='../Images/iconos/file.png'  width='20px'> <label id='lblVer'></label>Ver</a></center>" },
        ]
    });
};

function eliminar() {
    var values = [];
    $('.k-grid-content tbody tr').each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == 'checked') {
            var id = $row.find('.kendo-chk').attr('value');
            values.push(id);
            FuncionEliminar(id);
        }
    });
    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
        cargargrid();
    }
}

function FuncionEliminar(id) {
    $.ajax({
        url: '../CampaniaCallCenter/Eliminar',
        type: 'POST',
        data: { Id: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result != 1) {
                alert(dato.messages);
            }
        }
    });
}