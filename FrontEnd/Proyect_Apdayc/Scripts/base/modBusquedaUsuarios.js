
/// <reference path="../kendo.web-vsdoc.js" />
$(function () {
    $("#btnBusqueda").click(function () {

        $(".alert-link").hide();
        $("#message").hide();

        $("#txtBusqueda").focus();

        var busq = $("#txtBusqueda").val();
        var tot = $("#TotalVirtual").val();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: 5,
                transport: {
                    read: {
                        url: "../USUARIOS/usp_listar_UsuariosJson", dataType: "json", data: { dato: busq }
                    }
                },
                schema: { data: "USUARIO", total: 'TotalVirtual' }
            },
            sortable: true,
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
                       title: 'Eliminar', width: 9, template: "<input type='checkbox' id='vehicle' name='vehicle' value='${USUA_ICODIGO_USUARIO}'/>"
                   },
                   { field: "USUA_ICODIGO_USUARIO", width: 10, title: "<font size=2px>ID</font>", template: "<font color='blue'><a id='single_2' href='../USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>" },
                { field: "USUA_VNOMBRE_USUARIO", width: 20, title: "<font size=2px>NOMBRE</font>", template: "<font color='blue'><a id='single_2' href='../USUARIOS/Edit/${USUA_ICODIGO_USUARIO}' style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VNOMBRE_USUARIO}</a>" },
                { field: "USUA_VAPELLIDO_PATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO P</font>", template: "<font color='blue'><a id='single_2' href='../USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_PATERNO_USUARIO}</a></font>" },
                { field: "USUA_VAPELLIDO_MATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO M</font>", template: "<font color='blue'><a id='single_2' href='../USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_MATERNO_USUARIO}</a></font>" },
                { field: "USUA_VUSUARIO_RED_USUARIO", width: 10, title: "<font size=2px>USUARIO RED</font>", template: "<font color='blue'><a id='single_2' href='../USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VUSUARIO_RED_USUARIO}</a></font>" }
               ]
        })
    });
});

$(function () {
    $("#btnsuprimir").click(function () {

        bootbox.confirm("Desea eliminar el usuario?", function (result) {
            if (result == true) {

                var array = [];
                var itemId;

                $('input[name=vehicle][type=checkbox]:checked').each(function (i, checkbox) {

                    itemId = $(checkbox).val();
                    array.push({
                        ROL_ICODIGO_ROL: itemId,
                    });
                });

                $.ajax({
                    type: 'POST',
                    url: "../USUARIOS/Eliminar",
                    data: JSON.stringify({ dato: array }),
                    contentType: "application/json",
                    success: function (result) {
                        window.location = "../USUARIOS/";
                    }
                });
            }
        }).find("div.modal-content").addClass("confirmWidth");
    });
});
