/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 440;

/*INICIO CONSTANTES*/

$(function () {
    $("#txtNombreSearch").focus();

    $("#ModalNeoUsu").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        buttons: {
            "Grabar": function () { },
            "Cancelar": function () { $("#ModalNeoUsu").dialog("close"); }
        },
        modal: true
    });
    $("#btnNuevo").on("click", function () {
        $("#txtCodigo").hide();
        $("#txtCodigo").val("");
        limpiar();
        $("#ModalNeoUsu").dialog({ title: "Nuevo Usuario" });
        $("#ModalNeoUsu").dialog("open")
    });
    //$("#btnFiltrar").on("click", function () { limpiar(); filtrar(); });
    //$("#btnPLimpiar").on("click", function () {
    //    $("#txtNombres").val("");
    //    limpiar(); loadFiltrar();
    //});

    loadRol(0);
    loadData();
});

function loadData() {

    var busq = $("#txtNombreSearch").val();
    var tot = 2;//$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Roles_Usuarios/usp_listar_RolesUsuariosPage", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "Rol_Usuario", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
               {
                   title: '', width: 4, template: "<input type='checkbox' id='id' name='id' value='${USUA_ICODIGO_USUARIO },${ROL_ICODIGO_ROL }'/>"
               },
            {
                hidden:true,
                field: "USUA_ICODIGO_USUARIO", width: 5, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>"
            },
            { field: "NOMBRE_COMPLETO", width: 20, title: "<font size=2px>Nombre completo</font>", template: "<a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${NOMBRE_COMPLETO}</a>" },
            { field: "ROL_VNOMBRE_ROL", width: 20, title: "<font size=2px>Rol</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROL_VNOMBRE_ROL}</a></font>" },
            { field: "USRO_ACTIVO", width: 10, title: "<font size=2px>Activo</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USRO_ACTIVO}</a></font>" }
           ]
    });
}

var filtrar = function () {
    var busq = $("#txtNombreSearch").val();
    var tot = 2;//$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Roles_Usuarios/usp_listar_RolesUsuariosPage", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "Rol_Usuario", total: 'TotalVirtual' }
        },
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
               {
                   title: '', width: 4, template: "<input type='checkbox' id='id' name='id' value='${USUA_ICODIGO_USUARIO },${ROL_ICODIGO_ROL }'/>"
               },
            {
                hidden: true,
                field: "USUA_ICODIGO_USUARIO", width: 5, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>"
            },
            { field: "NOMBRE_COMPLETO", width: 20, title: "<font size=2px>Nombre completo</font>", template: "<a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${NOMBRE_COMPLETO}</a>" },
            { field: "ROL_VNOMBRE_ROL", width: 20, title: "<font size=2px>Rol</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROL_VNOMBRE_ROL}</a></font>" },
            { field: "USRO_ACTIVO", width: 10, title: "<font size=2px>Activo</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USRO_ACTIVO}</a></font>" }
           ]
    });
};

function limpiar() {

    $("#txtNombres").val("");
    $("#txCodigo").val("");
}

function loadRol(idRol) {
    $.ajax({
        url: '../ROLES/Listar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Codigo == idRol)
                        $('#ddlRol').append($("<option />", { value: valor.Codigo, text: valor.Nombre, selected: true }));
                    else
                        $('#ddlRol').append($("<option />", { value: valor.Codigo, text: valor.Nombre }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}