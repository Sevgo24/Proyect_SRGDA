/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 440;
var K_SIZE_PAGE = 12;

/*INICIO CONSTANTES*/

$(function () {
    $("#hidOpcionEdit").val(0);

    var eventoKP = "keypress";
    $('#txtNombreSearch').on(eventoKP, function (e) { return solonumeros(e); });

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

    initAutoCompletarRazonSocial("txtRazon", "hidCodigoBPS");

    $("#btnNuevo").on("click", function () {
        location.href = "../Socio/Nuevo";
    });

    $("#btnBuscar").on("click", function () {
        //loadData();
        loadGrid();        
    });

    $("#btnLimpiar").on("click", function () {
        $("#txtNombreSearch").val("");
        $("#txtRazon").val("");
        limpiar();
    });

    $("#ddlTipoDocumento").on("change", function () {
        if ($("#ddlTipoDocumento").val() == 1) {
            $("#divRazon").html("Razon Social:");
        } else {
            $("#divRazon").html("Nombres y Apellidos:");
        }
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
        }
        else {
            //loadData();
            loadGrid();
            alert("Estados actualizado correctamente.");
        }
        //loadData();
        loadGrid();
    });

    $("#chkFiltro").on("click", function () {
        var chk = $("#chkFiltro").prop("checked");
        var x = true;
        if (chk == 0) {
            x = false;
        }
        $("#chkUsuarioDerecho").prop("checked",x);
        $("#chkAsociacion").prop("checked",x);
        $("#chkGrupo").prop("checked",x);
        $("#chkRecaudador").prop("checked",x);
        $("#chkProveedor").prop("checked",x);
        $("#chkEmpleado").prop("checked",x);
    });

    loadTipoDocumento();

});

function loadTipoDocumento() {

    //$('#ddlTipoDocumento option').remove();
    $.ajax({
        url: '../General/ListarTipoDocumento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    $('#ddlTipoDocumento').append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                var doc = $("#ddlTipoDocumento").val();
                if (doc == 1) { $('#divRazon').html("Razón Social"); } else { $('#divRazon').html("Nombre"); }
                loadData();
            }
            else {
                alert(dato.message);
            }
        }
    });
}
var grabar = function () {

    if (ValidarRequeridos()) {
        $("#ModalNeoUsu").dialog("close");
        var estado = $("#selEstado").val() == 1 ? true : false;
        var usuario = {
            USUA_VNOMBRE_USUARIO: $("#txtNombre").val(),
            USUA_VAPELLIDO_PATERNO_USUARIO: $("#txtPaterno").val(),
            USUA_VAPELLIDO_MATERNO_USUARIO: $("#txtMaterno").val(),
            USUA_VUSUARIO_RED_USUARIO: $("#txtRed").val(),
            USUA_VPASSWORD_USUARIO: $("#txtPass").val(),
            ROL_ICODIGO_ROL: $("#ddlRol").val(),
            USUA_CACTIVO_USUARIO: estado,
            USUA_ICODIGO_USUARIO: $("#txtCodigo").val()
        };

        $.ajax({
            url: 'USUARIO/Insertar',
            data: usuario,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    //loadData();                    
                    loadGrid();
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
    var busq = $("#ddlTipoDocumento").val();
    //var busq1 = $("#txtNombreSearch").val();
    //var busq2 = $("#txtRazon").val();
    //var p1 = $("#chkUsuarioDerecho").prop("checked");
    //var p2 = $("#chkAsociacion").prop("checked");
    //var p3 = $("#chkGrupo").prop("checked");
    //var p4 = $("#chkRecaudador").prop("checked");
    //var p5 = $("#chkProveedor").prop("checked");
    //var p6 = $("#chkEmpleado").prop("checked");
    //var est = $("#divEstado").val();
    if (busq == 1) { $('#divRazon').html("Razón Social:"); } else { $('#divRazon').html("Nombres y Apellidos:"); }

    var grilla = $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    url: "../Socio/USP_SOCIOS_LISTARPAGEJSON",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            tipo: $("#ddlTipoDocumento").val(),
                            nro_tipo: $("#txtNombreSearch").val(),
                            nombre: $("#txtRazon").val(),
                            derecho: $("#chkUsuarioDerecho").prop("checked"),
                            asociacion: $("#chkAsociacion").prop("checked"),
                            grupo: $("#chkGrupo").prop("checked"),
                            recaudador: $("#chkRecaudador").prop("checked"),
                            proveedor: $("#chkProveedor").prop("checked"),
                            empleado: $("#chkEmpleado").prop("checked"),
                            estado: $("#divEstado").val()
                        })
                }
            },
            schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
        },
        groupable: false,
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
            title: 'Eliminar', width: 3, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${BPS_ID}'/>"
        },
     { field: "BPS_ID", width: 3, title: "Id", template: "<a id='single_2'  href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${BPS_ID}</a>" },
     { field: "ENT_TYPE_NOMBRE", width: 5, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
     { field: "TAXN_NAME", width: 5, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
     { field: "TAX_ID", width: 5, title: "Número", template: "<a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
     { field: "BPS_NAME", width: 20, title: "Razon Social", template: "<font color='green'><a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a></font>" },
     { field: "ACTIVO", width: 3, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;' >#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" },
     { title: 'U', width: 1.5, headerAttributes: {style: 'text-align: center'},  template: "#if(BPS_USER == '1'){#   <img src='../Images/botones/green.png'  width='16'  onclick='goUD(${BPS_ID});'  border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'>  #} else { if(BPS_USER == '2'){#   <img src='../Images/botones/yellow.png'  width='16'  onclick='goUD(${BPS_ID});'  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'>#}else{#  <img src='../Images/botones/red.png' width='16' onclick='goUD(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Usuario Derecho
     { title: 'R', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){#  <img src='../Images/botones/green.png'  width='16' onclick='goRE(${BPS_ID});' border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_COLLECTOR == '2'){#  <img src='../Images/botones/yellow.png'  width='16' onclick='goRE(${BPS_ID});' border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goRE(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Recaudador
     { title: 'E', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){#  <img src='../Images/botones/green.png'  width='16'  onclick='goEM(${BPS_ID});' border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'>  #}else {  if(BPS_EMPLOYEE == '2') {#  <img src='../Images/botones/yellow.png' width='16' onclick='goEM(${BPS_ID});' title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{ # <img src='../Images/botones/red.png' width='16' onclick='goEM(${BPS_ID});' title='ir a configurar perfil.' style=' cursor: pointer; cursor: hand;'> #}}#" },//Empleado
     { title: 'A', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#  <img src='../Images/botones/green.png' border='0' onclick='goAS(${BPS_ID});' title='Perfil asociado con estado activo.' width='16'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_ASSOCIATION == '2'){#  <img src='../Images/botones/yellow.png' border='0' onclick='goAS(${BPS_ID});' title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goAS(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Asociacion
     
     { title: 'P', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){#  <img src='../Images/botones/green.png'  width='16' onclick='goPV(${BPS_ID});' border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_SUPPLIER == '2'){#  <img src='../Images/botones/yellow.png'  width='16' onclick='goPV(${BPS_ID});' border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goPV(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Proveedor
     { title: 'G.E', width: 2, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <img src='../Images/botones/green.png'  width='16'  border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else {#  <img src='../Images/botones/red.png' width='16' title='ir a configurar perfil.'>  #}#" },//Grupo Empresarial
    ]

    }).data("kendoGrid");

    //grilla.thead.kendoTooltip({
    //    filter: "th",
    //    content: function (e) {
    //        var target = e.target; // element for which the tooltip is shown
    //        return $(target).text();
    //    }
    //});
}

var agregar = function () {
    location.href = "Socio/Nuevo";
};

var modificar = function () {
    alert("edit");
};
//  altRowTemplate: kendo.template($("#altRowTemplate").html())

function editar(idSel) {
    location.href = "../Socio/Nuevo?set=" + idSel;
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

function eliminar(idBPS) {
    var codigosDel = { codigo: idBPS };    
    $.ajax({
        url: '../Socio/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //loadData();
                loadGrid();
            }
            // alert(dato.message);
        }
    });
}
function limpiarValidacion() {
    msgError("", "txtNombre");
    msgError("", "txtPaterno");
    msgError("", "txtMaterno");
    msgError("", "txtRed");
    msgError("", "txtPass");
    //msgError("","selEstado");
}

function limpiar() {
    $("#txtNombreSearch").val("");
    $("#txtRazon").val("");
}

function goUD(id) {
    location.href = "../Derecho/Nuevo?set=" + id;
}

function goRE(id) {
    location.href = "../Recaudador/Nuevo?set=" + id;
}
function goAS(id) {
    location.href = "../Asociacion/Nuevo?set=" + id;
}

function goEM(id) {
    location.href = "../Empleado/Nuevo?set=" + id;
}

function goPV(id) {
    location.href = "../Proveedor/Nuevo?set=" + id;
}

function loadGrid() {
    $('#grid').data('kendoGrid').dataSource.query({
        tipo: $("#ddlTipoDocumento").val(),
        nro_tipo: $("#txtNombreSearch").val(),
        nombre: $("#txtRazon").val(),
        derecho: $("#chkUsuarioDerecho").prop("checked"),
        asociacion: $("#chkAsociacion").prop("checked"),
        grupo: $("#chkGrupo").prop("checked"),
        recaudador: $("#chkRecaudador").prop("checked"),
        proveedor: $("#chkProveedor").prop("checked"),
        empleado: $("#chkEmpleado").prop("checked"),
        estado: $("#divEstado").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}