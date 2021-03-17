var codigo = "";
$(function () {
    $("#hidCodigoBPS").val(0);
    $("#hidCodigoEST").val(0);

    $("#btnNuevo").on("click", function () {
        location.href = "../Inspection/Nuevo";
    });

    mvInitEstablecimiento({
        container: "ContenedormvEstablecimiento",
        idButtonToSearch: "btnBuscarEstablecimiento",
        idDivMV: "mvEstablecimiento",
        event: "reloadEventoEst"
    });

    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento"
    });

    loadTipoestablecimiento('ddlTipoestablecimiento', 0);
    loadDivisionTipo('ddlDivisionTipo', 0);

    $("#ddlTipoestablecimiento").on("change", function () {
        var codigo = $("#ddlTipoestablecimiento").val();
        loadSubTipoestablecimientoPorTipo('ddlSubtipoestablecimiento', codigo);
    });

    $("#ddlDivisionTipo").on("change", function () {
        var tipo = $(this).val();
        loadTipoDivision('ddlDivision', tipo);
    });

    $("#ddlDivisionTipo").val("");

    $("#btnBuscar").on("click", function () {
        if ($("#txtCodigo").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigo").val();
        $('#grid').data('kendoGrid').dataSource.query({
            insId: codigo,
            estId: $("#hidCodigoEST").val(),
            tipoest: $("#ddlTipoestablecimiento").val(),
            subtipoest: $("#ddlSubtipoestablecimiento").val(),
            socio: $("#hidCodigoBPS").val(),
            tipodiv: $("#ddlDivisionTipo").val(),
            division: $("#ddlDivision").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        if ($("#txtCodigo").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigo").val();
        $('#grid').data('kendoGrid').dataSource.query({
            insId: codigo,
            estId: $("#hidCodigoEST").val(),
            tipoest: $("#ddlTipoestablecimiento").val(),
            subtipoest: $("#ddlSubtipoestablecimiento").val(),
            socio: $("#hidCodigoBPS").val(),
            tipodiv: $("#ddlDivisionTipo").val(),
            division: $("#ddlDivision").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    });

    $("#btnEliminar").on("click", function () {
        eliminar();
    });
    loadData();
});

var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidCodigoBPS").val(idSel);
    //aplicar logica que va realizar luego de seleccionar el ID del BPS
    ObtenerNombreSocio(idSel);
    //ObtieneNombreEntidad(idSel, "lblsocio");
};

var reloadEventoEst = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidCodigoEST").val(idSel);
    //aplicar logica que va realizar luego de seleccionar el ID del Establecimiento
    ObtenerNombreEst(idSel);
    //ObtenerNombreEstablecimiento(idSel, "lblestablecimiento");
};

function ObtenerNombreSocio(id) {
    $.ajax({
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        data: { codigoBps: id },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lblsocio").html(dato.valor);
            } else {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ObtenerNombreEst(id) {
    $.ajax({
        url: '../General/ObtenerNombreEstablecimiento',
        type: 'POST',
        data: { codigoEst: id },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lblNombreestablecimiento").html(dato.valor);
            } else {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function editar(idSel) {
    document.location.href = '../Inspection/Nuevo?id=' + idSel;
    $("#hidOpcionEdit").val(1);
}

function loadData() {
    if ($("#txtCodigo").val() == "")
        codigo = 0;
    else
        codigo = $("#txtCodigo").val();
    $("#grid").kendoGrid({

        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../Inspection/ListarInpection",
                    dataType: "json"
                    //data: { insId: codigo, estId: Idestablecimiento, tipoest: Tipoestablecmiento, subtipoest: SubTipoestablecmiento, socio: Idsocio, tipodiv: idTipoDiv, division: idDivi }
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            insId: codigo,
                            estId: $("#hidCodigoEST").val(),
                            tipoest: $("#ddlTipoestablecimiento").val(),
                            subtipoest: $("#ddlSubtipoestablecimiento").val(),
                            socio: $("#hidCodigoBPS").val(),
                            tipodiv: $("#ddlDivisionTipo").val(),
                            division: $("#ddlDivision").val()
                        })
                }
            },
            schema: { data: "ListaInspection", total: 'TotalVirtual' }
        },

        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns:
        [
            {
                title: 'Eliminar', width: 7, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${INSP_ID}'/>"
            },
            {
                hidden: true,
                field: "OWNER", width: 4, title: "<font size=2px>PROPIETARIO</font>", template: "<a id='single_2'  href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${OWNER}</a>"
            },
            {
                field: "INSP_ID", width: 6, title: "<font size=2px>Id</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${INSP_ID}</a></font>"
            },
            {
                field: "INSP_DOC", width: 30, title: "<font size=2px>Documento</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${INSP_DOC}</a></font>"
            },
            {
                field: "INSP_OBS", width: 30, title: "<font size=2px>Observación</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${INSP_OBS}</a></font>"
            },
            {
                type: "date",
                editable: "false",
                field: "INSP_DATE", width: 8, title: "FECHA",
                template: "<font color='green'><a id='single_2' href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>" + '#=(INSP_DATE==null)?"":kendo.toString(kendo.parseDate(INSP_DATE,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>"
            },
            {
                field: "LOG_USER_CREAT", width: 7, title: "<font size=2px>usuario</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${INSP_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a></font>"
            }
        ]
    });
}

function limpiar() {
    $("#txtCodigo").val("");
    $("#hidCodigoEST").val(0);
    $("#hidCodigoBPS").val(0);
    $("#ddlTipoestablecimiento").val(0);
    $("#ddlSubtipoestablecimiento").val(0);
    $("#ddlDivisionTipo").val("");
    $("#ddlDivision").val(0);
    $("#lblsocio").html("");
    $("#lblNombreestablecimiento").html("");
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
        if ($("#txtCodigo").val() == "")
            codigo = 0;
        else
            codigo = $("#txtCodigo").val();
        $('#grid').data('kendoGrid').dataSource.query({
            insId: codigo,
            estId: $("#hidCodigoEST").val(),
            tipoest: $("#ddlTipoestablecimiento").val(),
            subtipoest: $("#ddlSubtipoestablecimiento").val(),
            socio: $("#hidCodigoBPS").val(),
            tipodiv: $("#ddlDivisionTipo").val(),
            division: $("#ddlDivision").val(),
            page: 1, pageSize: K_PAGINACION.LISTAR_15
        });
    }
}

function FuncionEliminar(id) {
    var id = { idinspection: id };
    $.ajax({
        url: '../Inspection/Eliminar',
        type: 'POST',
        data: id,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}