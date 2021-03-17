/*INICIO CONSTANTES*/
$(function () {
    $("#hidOpcionEdit").val(0);

    $("#btnNuevo").on("click", function () {
        grabar();
        $("#hidOpcionEdit").val(0);
    });

    $("#btnSave").on("click", function () {
        grabar();
        $("#hidOpcionEdit").val(0);
    });

    $("#btnsuprimir").on("click", function () {
        var values = [];
        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk').attr('checked');
            if (checked == "checked") {
                var codigo = $row.find('.kendo-chk').attr('value');
                values.push(codigo);
                eliminar(codigo);
            }
        });
        if (values.length == 0) {
            alert("Seleccione usuario para eliminar.");
        } else {
            loadData();
            alert("Estados actualizado correctamente.");
        }
    });

    $("#ddlDivision").on("change", function () {
        loadSubDivisiones($(this).val(), '');
    })

    $("#ddlSubdivision").on("change", function () {
        loadDependecia($(this).val(), '');
    })
    
    loadDivisiones('');
    loadData();
});

function editar(idSel) {
    document.location.href = 'Division/Edit?code=' + idSel;
    $("#hidOpcionEdit").val(1);
    limpiar();
}

//var grabar = function () {
function grabar() {
    if (ValidarRequeridos()) {
        var iddivision = $("#ddlDivision").val();
        var idsubtipodivision = $("#ddlSubdivision").val();
        var dependencia = $("#ddlDependencia").val();
        var division = {
            DADV_ID: $("#txtDadid").val(),
            DAD_ID: iddivision,
            DAD_STYPE: idsubtipodivision,
            DAD_VCODE: $("#txtCodigo").val(),
            DAD_VNAME: $("#txtDescripcion").val(),
            DAD_BELONGS: dependencia
        };

        $.ajax({
            url: 'Insertar',
            data: division,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    limpiar();
                    alert(dato.message);
                } else {
                    alert(dato.message);
                }
            }
        });
    }
    return false;
};

function loadData() {

    var busq = $("#txtBusqueda").val();
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "Division/usp_listar_DivisionJson", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "Div", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
            {
                title: 'Eliminar', width: 30, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${DADV_ID}'/>"
            },
            {
                hidden: true,
                field: "OWNER", width: 10, title: "<font size=2px>PROPIETARIO</font>", template: "<a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${OWNER}</a>"
            },
            {
                hidden: true,
                field: "DADV_ID", width: 20, title: "<font size=2px>ID</font>", template: "<a id='single_2' href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DADV_ID}</a>"
            },
            {
                hidden: true,
                field: "DAD_ID", width: 20, title: "<font size=2px>ID DIVISION</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_ID}</a></font>"
            },
            { field: "DAD_CODE", width: 80, title: "<font size=2px>DIVISION</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_CODE}</a></font>" },
            {
                hidden: true,
                field: "DAD_STYPE", width: 10, title: "<font size=2px>ID SUBTIPO</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_STYPE}</a></font>"
            },
            { field: "DAD_SNAME", width: 80, title: "<font size=2px>SUBTIPO</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_SNAME}</a></font>" },
            {
                hidden: true,
                field: "DAD_VCODE", width: 10, title: "<font size=2px>ID</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_VCODE}</a></font>"
            },
            { field: "DAD_VNAME", width: 80, title: "<font size=2px>NOMBRE VALOR</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_VNAME}</a></font>" },
            { field: "DAD_BELONGS", width: 80, title: "<font size=2px>DEPENDECIA</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${DADV_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${DAD_BELONGS}</a></font>" }
           ]
    });
}

function loadDependecia(ddlPadre, valEdit) {
    $('#ddlDependencia option').remove();
    $('#ddlDependencia').append($("<option />", { value: 0, text: '<--Seleccione-->' }));
    $.ajax({
        url: '../General/ListaDependencia',
        type: 'POST',
        data: { dSubTipoDivision: ddlPadre },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valEdit)
                        $('#ddlDependencia').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlDependencia').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadSubDivisiones(ddlDivision, valEdit) {
    $('#ddlSubdivision option').remove();
    $('#ddlSubdivision').append($("<option />", { value: 0, text: '<--Seleccione-->' }));
    $.ajax({
        url: '../General/ListaSubDivisiones',
        type: 'POST',
        data: { dDivision: ddlDivision },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valEdit)
                        $('#ddlSubdivision').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlSubdivision').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadDivisiones(tipo) {
    $('#ddlDivision option').remove();
    $('#ddlDivision').append($("<option />", { value: 0, text: '<--Seleccione-->' }));
    $.ajax({
        url: '../General/ListaDivisiones',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#ddlDivision').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlDivision').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function eliminar(id) {
    var codigosDel = { codigo: id };
    $.ajax({
        url: 'Division/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
        }
    });
}

function limpiarValidacion() {
    msgError("", "txtCodigo");
    msgError("", "txtDescripcion");
}

function limpiar() {
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    limpiarValidacion();
}




