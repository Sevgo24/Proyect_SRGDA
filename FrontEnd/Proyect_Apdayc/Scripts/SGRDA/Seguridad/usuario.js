/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT=440;

/*INICIO CONSTANTES*/

$(function () {
    $("#hidOpcionEdit").val(0);
    $("#ModalNeoUsu").dialog({
        autoOpen: false,
        width: K_WIDTH,
        height: K_HEIGHT,
        buttons: {
            "Grabar": grabar,
            "Cancelar": function () { $("#ModalNeoUsu").dialog("close"); }
        },
        modal: true
    });
    $("#btnNuevo").on("click", function () {
        $("#txtCodigo").val("");
        limpiar();
        $("#hidOpcionEdit").val(0);
        $("#ModalNeoUsu").dialog({ title: "Nuevo Usuario" });
        $("#ModalNeoUsu").dialog("open")
    });
    $("#btnBuscar").on("click", function () { limpiar(); loadData(); });
    $("#btnLimpiar").on("click", function () {
        $("#txtNombreSearch").val("");
        $("#txtLoginSearch").val("");
        limpiar(); loadData();
    });

    $("#btnEliminar").on("click", function () {
        var values = [];
                $(".k-grid-content tbody tr").each(function () {
                    var $row = $(this);
                    var checked = $row.find('.kendo-chk').attr('checked');
                    if (checked == "checked") {
                        var codigoUsu = $row.find('.kendo-chk').attr('value');
                        values.push(codigoUsu);
                        eliminar(codigoUsu);
                   }
                });
                if (values.length == 0) {
                    alert("Seleccione usuario para eliminar.");
                } else {
                    loadData();
                }
    });
    
    loadRol(0);
    loadData();
});

var grabar = function () {

    if (ValidarRequeridos()) {
        $("#ModalNeoUsu").dialog("close");
        var estado = $("#selEstado").val() == 1 ? true : false;
        var usuario = {
            USUA_VNOMBRE_USUARIO : $("#txtNombre").val(),
            USUA_VAPELLIDO_PATERNO_USUARIO : $("#txtPaterno").val(),
            USUA_VAPELLIDO_MATERNO_USUARIO : $("#txtMaterno").val(),
            USUA_VUSUARIO_RED_USUARIO : $("#txtRed").val(),
            USUA_VPASSWORD_USUARIO : $("#txtPass").val(),
            ROL_ICODIGO_ROL : $("#ddlRol").val(),
            USUA_CACTIVO_USUARIO: estado,
            USUA_ICODIGO_USUARIO: $("#txtCodigo").val()
        };
      
        $.ajax({
            url: 'USUARIO/Insertar',
            data : usuario,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadData();
                    //msgOkB("aviso", dato.message);
                    alert(dato.message);
                } else {
                    //msgErrorB("aviso", dato.message);
                    alert(dato.message);
                }
            }
        });
    }
    return false;
    
};


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
                    url: "../USUARIOS/usp_listar_UsuariosJson", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "USUARIO", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: 'Eliminar', width: 9, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${USUA_ICODIGO_USUARIO}'/>"
               },
            { field: "USUA_ICODIGO_USUARIO", width: 10, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>" },
            { field: "USUA_VNOMBRE_USUARIO", width: 20, title: "<font size=2px>NOMBRE</font>", template: "<a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VNOMBRE_USUARIO}</a>" },
            { field: "USUA_VAPELLIDO_PATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO P</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_PATERNO_USUARIO}</a></font>" },
            { field: "USUA_VAPELLIDO_MATERNO_USUARIO", width: 20, title: "<font size=2px>APELLIDO M</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VAPELLIDO_MATERNO_USUARIO}</a></font>" },
            { field: "USUA_VUSUARIO_RED_USUARIO", width: 10, title: "<font size=2px>USUARIO RED</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_VUSUARIO_RED_USUARIO}</a></font>" },
           { field: "USUA_CACTIVO_USUARIO", width: 10, title: "<font size=2px>ACTIVO</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_CACTIVO_USUARIO}</a></font>" }
           ]
    });
}

function editar(idSel) {
    $("#hidOpcionEdit").val(1);
    limpiar();
    $.ajax({
        url: 'USUARIO/Obtiene',
        type: 'POST',
        data: { id: idSel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var usuario = dato.data.Data;
                if (usuario != null) {
                    $("#txtNombre").val(usuario.USUA_VNOMBRE_USUARIO);
                    $("#txtPaterno").val(usuario.USUA_VAPELLIDO_PATERNO_USUARIO);
                    $("#txtMaterno").val(usuario.USUA_VAPELLIDO_MATERNO_USUARIO);
                    $("#txtRed").val(usuario.USUA_VUSUARIO_RED_USUARIO);
                    $("#txtPass").val(usuario.USUA_VPASSWORD_USUARIO);
                    $("#txtCodigo").val(usuario.USUA_ICODIGO_USUARIO);
                    loadRol(usuario.ROL_ICODIGO_ROL);

                    if (usuario.USUA_CACTIVO_USUARIO) {
                        $("#selEstado option").filter(function () {
                            return $(this).val() == 1;
                        }).attr('selected', true);
                    } else {
                       
                        $("#selEstado option").filter(function () {
                            return $(this).val() == 0;
                        }).attr('selected', true);
                    }


                }
                
            } else {
                alert(dato.message);
            }
        }
    });


    $("#ModalNeoUsu").dialog({ title: "Actualizar Usuario" });
    $("#ModalNeoUsu").dialog("open");


}

function loadRol(idRol) {
    $('#ddlRol option').remove();
    $.ajax({
        url: 'ROLES/Listar',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Codigo == idRol) 
                        $('#ddlRol').append($("<option />", { value: valor.Codigo, text: valor.Nombre,selected:true}));
                    else
                        $('#ddlRol').append($("<option />", { value: valor.Codigo, text: valor.Nombre }));
                });
            } else {
                 alert(dato.message);
            }
        }
    });
}

function eliminar(idUsuario) {
    var codigosDel = { codigo: idUsuario };   
    $.ajax({
        url: 'Usuario/Eliminar',
        type: 'POST',
        data: codigosDel,
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
function limpiarValidacion() {

    msgError("","txtNombre");
    msgError("","txtPaterno");
    msgError("","txtMaterno");
    msgError("","txtRed");
    msgError("","txtPass");
    //msgError("","selEstado");

   


}

function limpiar() {
     
    $("#txtNombre").val("");
    $("#txCodigo").val("");
    $("#txtPaterno").val("");
    $("#txtMaterno").val("");
    $("#txtRed").val("");
    $("#txtPass").val("");
    limpiarValidacion();
 




}
