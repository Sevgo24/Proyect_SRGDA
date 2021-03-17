/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 250;
var K_ID_POPUP_DIR = "";
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";

/*INICIO CONSTANTES*/

$(function () {
    $("#hidResponsable").val(0);
    $("#hidOpcionEdit").val(0);

    $("#btnNuevo").on("click", function () {
        location.href = "../GrupoFacturacion/Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({ UserDer: $("#hidResponsable").val(), GrupoMod: $("#ddlGrupoModalidad").val(), parametro: $("#txtGrupoFacturacion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnLimpiar").on("click", function () {
        limpiar();
        $('#grid').data('kendoGrid').dataSource.query({ UserDer: $("#hidResponsable").val(), GrupoMod: $("#ddlGrupoModalidad").val(), parametro: $("#txtGrupoFacturacion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    });

    $("#btnEliminar").on("click", function () {
        var values = [];
        $(".k-grid-content tbody tr").each(function () {
            var $row = $(this);
            var checked = $row.find('.kendo-chk').attr('checked');
            if (checked == "checked") {
                var codigoTipo = $row.find('.kendo-chk').attr('value');
                values.push(codigoTipo);
                eliminar(codigoTipo);
            }
        });
        if (values.length == 0) {
            alert("Seleccione un tipo para eliminar.");
        } else {
            $('#grid').data('kendoGrid').dataSource.query({ UserDer: $("#hidResponsable").val(), GrupoMod: $("#ddlGrupoModalidad").val(), parametro: $("#txtGrupoFacturacion").val(), st: $("#ddlEstado").val(), page: 1, pageSize: K_PAGINACION.LISTAR_15 });
     
        }
    });

    mvInitBuscarSocio({
        container: "ContenedormvBuscarSocio",
        idButtonToSearch: "btnBuscarBS",
        idDivMV: "mvBuscarSocio",
        event: "reloadEvento",
        idLabelToSearch: "lbResponsable"
    });

    //initAutoCompletarRazonSocial("txtUsuario", "hidCodigoBPS");

    loadTipoGrupo('ddlGrupoModalidad', '0');
    loadEstadosMaestro("ddlEstado");
    loadData();
});

var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    obtenerNombreSocio($("#hidResponsable").val());
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
};

function loadData() {

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../GrupoFacturacion/Listar_PageJson_Grupo_Facturacion",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            UserDer: $("#hidResponsable").val(),
                            GrupoMod: $("#ddlGrupoModalidad").val(),
                            parametro: $("#txtGrupoFacturacion").val(),
                            st: $("#ddlEstado").val()
                        })
                }
            },
            schema: { data: "GrupoFacturacion", total: 'TotalVirtual' }
        },
        groupable: false,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        sortable: K_ESTADO_ORDEN,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 12, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${INVG_ID}'/>"
               },
            { field: "INVG_ID", width: 7, title: "Id", template: "<a id='single_2'  href=javascript:editar('${INVG_ID}') style='color:gray !important;'>${INVG_ID}</a>" },
            { field: "BPS_NAME", width: 70, title: "Usuario Derecho", template: "<a id='single_2'  href=javascript:editar('${INVG_ID}') style='color:gray !important;'>${BPS_NAME}</a></font>" },
            { field: "GRUPO", width: 70, title: "Grupo de Modalidad", template: "<a id='single_2'  href=javascript:editar('${INVG_ID}') style='color:gray !important;'>${GRUPO}</a></font>" },
            { field: "INVG_DESC", width: 70, title: "Grupo de Facturación", template: "<a id='single_2'  href=javascript:editar('${INVG_ID}') style='color:gray !important;'>${INVG_DESC}</a></font>" },
            { field: "Activo", width: 12, title: "Estado", template: "<a id='single_2'  href=javascript:editar('${INVG_ID}') style='color:gray !important;'>#if(Activo == 'A'){# Activo #}else{# Inactivo#}#  </a></font>" }
           ]
    });
}

function editar(idSel) {
    location.href = "../GrupoFacturacion/Nuevo?set=" + idSel;
}

function eliminar(idSel) {
    var codigosDel = { codigo: idSel };

    $.ajax({
        url: '../GrupoFacturacion/Eliminar',
        type: 'POST',
        data: codigosDel,
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

function limpiar() {
    $("#hidResponsable").val(0);
    $("#lbResponsable").html("Seleccione");
    loadTipoGrupo('ddlGrupoModalidad', '0');
    $("#txtGrupoFacturacion").val("");
    $("#ddlEstado").val(1);
}
