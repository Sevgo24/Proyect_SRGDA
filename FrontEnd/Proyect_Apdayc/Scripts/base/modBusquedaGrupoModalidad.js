/*INICIO CONSTANTES*/
var K_SIZE_PAGE = 15;


$(function () {
    
    function avoidRefresh(e) {
        e.preventDefault();
    }

    $(".alert-link").hide();
    $("#message").hide();

    $("#btnBusqueda").on("click", function () { loadData(); });

    $("#btnLimpiar").on("click", function () {
        $("#txtBusqueda").val("");
        limpiar(); loadData();
        $("#txtBusqueda").focus();
    });
    loadEstadosMaestro("ddlEstado");
    loadData();

    $("#txtBusqueda").keypress(function (e) {
        if (e.which == 13) {
            loadData();
        }
    });

    $("#txtBusqueda").focus();
});

function loadData() {
    var busq = $("#txtBusqueda").val();
    var estado = $("#ddlEstado").val();
    var tot = $("#TotalVirtual").val();
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../GRUPOMODALIDAD/usp_listar_GrupoModalidadJson", dataType: "json", data: { dato: busq, st: estado }
                }
            },
            schema: { data: "RECMODGROUP", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [{
            title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${MOG_ID}'/>"
        }, {
            field: "MOG_ID", width: 9, title: "Id", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}'style='color:gray;text-decoration:none;'>${MOG_ID}</a>"
        }, {
            hidden: true,
            field: "OWNER", width: 10, title: "Propietario", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}' style='color:gray;text-decoration:none;'>${OWNER}</a>"
        }, {
            field: "MOG_DESC", width: 80, title: "Descripción", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}'style='color:gray;text-decoration:none;'>${MOG_DESC}</a>"
        },
        {
            hidden: true,
            field: "LOG_USER_CREAT", width: 30, title: "Creación", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}' style='color:gray;text-decoration:none;' >${LOG_USER_CREAT}</a>"
        },
        {
            hidden: true,
            field: "LOG_USER_UPDAT", width: 10, title: "Modificación", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}' style='color:gray;text-decoration:none;' >${LOG_USER_UPDAT}</a>"
        },
        { field: "Activo", width: 12, title: "Estado", template: "<a  href='../GRUPOMODALIDAD/Edit?set=${MOG_ID}' style='color:gray;text-decoration:none;'>#if(Activo == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" }
        ]
    })
}


$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar la modalidad?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        MOG_ID: itemId
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../GRUPOMODALIDAD/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});

function limpiar() {
    $("#txtBusqueda").val("");
}

