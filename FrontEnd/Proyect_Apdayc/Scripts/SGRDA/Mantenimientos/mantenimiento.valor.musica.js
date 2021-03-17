//var varflag = 0;
$(function () {
    kendo.culture('es-PE');
    $("#txtFechaini").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFechafin").kendoDatePicker({ format: "dd/MM/yyyy" });
    loadEstadosMaestro("ddlEstado");
    limpiar();

    $("#btnBuscar").on("click", function () {
        var vfini = $('#txtFechaini').data('kendoDatePicker').value();
        var vffin = $('#txtFechafin').data('kendoDatePicker').value();
        $('#txtFechaini').data('kendoDatePicker').value(vfini);
        $('#txtFechafin').data('kendoDatePicker').value(vffin);

        //if ($("#txtFechaini").val() == "" && $("#txtFechafin").val() == "")
        //    varflag = 0;
        //else
        //    varflag = 0;
        //alert(varflag + " " + $("#txtFechaini").val() + " " + $("#txtFechafin").val());

        $('#grid').data('kendoGrid').dataSource.query({ fechaini: vfini, fechafin: vffin, st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnNuevo").on("click", function () {
        location.href = "../ValorMusica/Nuevo";
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
        
        var vfini = $('#txtFechaini').data('kendoDatePicker').value();
        var vffin = $('#txtFechafin').data('kendoDatePicker').value();
        $('#txtFechaini').data('kendoDatePicker').value(vfini);
        $('#txtFechafin').data('kendoDatePicker').value(vffin);
        $('#grid').data('kendoGrid').dataSource.query({ fechaini: vfini, fechafin: vffin, st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    loadData();

    mvInitArtista({
        container: "ContenedormvArtista",
        idButtonToSearch: "btnConsulta",
        idDivMV: "mvEstablecimiento",
        event: "reloadEvento"
    });
});

var reloadEvento = function (idSel) {
    alert("Selecciono ID:   " + idSel);
    $("#hidCodigoArt").val(idSel);
};

function limpiar() {
    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
    //var twoDigitDate = 1;

    //var anoIni = parseInt(fullDate.getFullYear());
    var mesFin = parseInt(fullDate.getMonth());
    //var currentDateIni = twoDigitDate + "/" + twoDigitMonth + "/" + (anoIni - 1).toString();
    //var currentDateFin = twoDigitDate + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
    var currentDateIni = "01" + "/" + (mesFin + 1) + "/" + fullDate.getFullYear();
    var currentDateFin = twoDigitDate + "/" + (mesFin + 1) + "/" + fullDate.getFullYear();

    $('#txtFechaini').val(currentDateIni);
    $('#txtFechafin').val(currentDateFin);
    $("#ddlEstado").val(1);
}

function editar(idSel) {
    document.location.href = '../ValorMusica/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {    
    var vfini = $('#txtFechaini').data('kendoDatePicker').value();
    var vffin = $('#txtFechafin').data('kendoDatePicker').value();
    $('#txtFechaini').data('kendoDatePicker').value(vfini);
    $('#txtFechafin').data('kendoDatePicker').value(vffin);

    //if ($("#txtFechaini").val() == "" && $("#txtFechafin").val() == "")
    //    varflag = 0;
    //else
    //    varflag = 0;
    //alert(varflag + " " + $("#txtFechaini").val() + " " + $("#txtFechafin").val());
    
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../ValorMusica/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                                                        fechaini: $("#txtFechaini").val(),
                                                        fechafin: $("#txtFechafin").val(),
                                                        st: $("#ddlEstado").val()
                        })
                }
            },
            schema: { data: "listaValorMusica", total: 'TotalVirtual' }
        },
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
            { title: 'Eliminar', width: 50, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${VUM_ID}'/>" },
            { field: "VUM_ID", width: 50, title: "Id", template: "<a id='single_2'  style='color:gray !important;'>${VUM_ID}</a></font>" },
            { field: "VUM_VAL", width: 50, title: "Valor unitario", template: "<a id='single_2'   style='color:gray !important;'>${VUM_VAL}</a></font>" },
            {
                field: "START", width: 80, title: "Fecha inicio",
                type: "date",
                template: "<font color='green'><a id='single_2'  style='color:gray !important;'>" + '#=(START==null)?"":kendo.toString(kendo.parseDate(START,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>"
            },
            {
                field: "ENDS", width: 80, title: "Fecha fin",
                type: "date",
                template: "<font color='green'><a id='single_2'  style='color:gray !important;'>" + '#=(ENDS==null)?"":kendo.toString(kendo.parseDate(ENDS,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>"
            },
            { field: "ESTADO", width: 50, title: "Estado", template: "<a id='single_2' style='color:gray !important;'>${ESTADO}</a></font>" },
           ]
    });
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            FuncionEliminar(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione para eliminar.");
    } else {
       
        var vfini = $('#txtFechaini').data('kendoDatePicker').value();
        var vffin = $('#txtFechafin').data('kendoDatePicker').value();
        $('#txtFechaini').data('kendoDatePicker').value(vfini);
        $('#txtFechafin').data('kendoDatePicker').value(vffin);
        $('#grid').data('kendoGrid').dataSource.query({ fechaini: vfini, fechafin: vffin, st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });

       
    }
}

function FuncionEliminar(id) {
    var id = { Id: id };
    $.ajax({
        url: '../ValorMusica/Eliminar',
        type: 'POST',
        data: id,
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