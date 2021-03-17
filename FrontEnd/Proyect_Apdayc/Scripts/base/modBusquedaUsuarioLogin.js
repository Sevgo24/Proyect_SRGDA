/// <reference path="../kendo.web-vsdoc.js" />
//$(function () {
//    $("#btnAceptar").click(function () {

//        $(".alert-link").hide();
//        $("#message").hide();

//        var user = $("#txtUsuario").val();
//        var pwd = $("#txtPassword").val();

//        $("#Ingresar").kendoGrid({
//            dataSource: {
//                type: "json",
//                serverPaging: true,
//                pageSize: 5,

//                transport: {
//                    read: {
//                        url: "USUARIOS/USUARIOS_spBuscarLogin", dataType: "json", data: { usuario_red: user, usuario_pwd: pwd }
//                    }
//                },
//                schema: { data: "USUARIO", total: 'TotalVirtual' }
//            },
//            groupable: true,
//            sortable: true,
//            pageable: true,
//            columns: [{
//                field: "USUA_ICODIGO_USUARIO", width: 10, title: "<font size=2px>ID</font>", template: "<a id='single_2' href='/USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>"
//            },
//                { field: "USUA_VNOMBRE_USUARIO", width: 20, title: "<font size=2px>NOMBRE</font>", template: "<a id='single_2' href='/USUARIOS/Edit/${USUA_ICODIGO_USUARIO}' style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VNOMBRE_USUARIO}</a>" },
//                { field: "USUA_VAPELLIDO_PATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO P</font>", template: "<font color='green'><a id='single_2' href='/USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_PATERNO_USUARIO}</a></font>" },
//                { field: "USUA_VAPELLIDO_MATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO M</font>", template: "<font color='green'><a id='single_2' href='/USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_MATERNO_USUARIO}</a></font>" },
//                { field: "USUA_VUSUARIO_RED_USUARIO", width: 10, title: "<font size=2px>USUARIO RED</font>", template: "<font color='green'><a id='single_2' href='/USUARIOS/Edit/${USUA_ICODIGO_USUARIO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VUSUARIO_RED_USUARIO}</a></font>" }
//            ]
//        });
//    }

//    );
//});

$(function () {
    $("#btnAceptar").click(function () {
        var user = $("#user").val();
        var pwd = $("#pwd").val();

        updateResult(id, name);
        return false;
    });

    function updateResult(id, name) {
        var url = '@Url.Action("USUARIOS_spBuscarLogin", "USUARIO_LOGIN", new { user="param-user", pwd="param-pwd" })';

        url = url.replace("param-id", id)
                 .replace("param-name", name);
        $("#Ingresar").load(url);
    }
});