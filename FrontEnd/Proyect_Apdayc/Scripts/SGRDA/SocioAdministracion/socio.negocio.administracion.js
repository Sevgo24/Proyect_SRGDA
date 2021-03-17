/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 440;
var K_SIZE_PAGE = 12;

var K_WIDTH_SOC = 900;
var K_HEIGHT_SOC = 400;

/*INICIO CONSTANTES*/

$(function () {
    $("#hidOpcionEdit").val(0);

    var eventoKP = "keypress";
    $('#txtNombreSearch').on(eventoKP, function (e) { return solonumeros(e); });

    $("#mvAgruparSocio").dialog({ autoOpen: false, width: K_WIDTH_SOC, height: K_HEIGHT_SOC, buttons: { "Agrupar": addAgrupar, "Cancelar": function () { $(this).dialog("close"); } }, modal: true});

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
    initAutoCompletarRazonSocial("txtRazSocial", "hidCodBPS");

    $("#btnNuevo").on("click", function () {
        location.href = "../SocioAdministracion/Nuevo";
    });

    $("#btnBuscarAgrSocio").on("click", function () {
        $("#gridSocioOriginal").html('');
        $("#gridSocioFinal").html('');
        var ruc = $("#txtRuc").val();
        var razsocial = $("#txtRazSocial").val();
        BuscarSocioxRuc(ruc, razsocial);
    });

    $("#btnBuscar").on("click", function () {
        //loadData();
        loadGrid();
    });

    $("#btnAgruparSocio").on("click", function () {
        limpiarRuc();
        $("#mvAgruparSocio").dialog("open");
        var ruc = $("#txtRuc").val();
        var razsocial = $("#txtRazSocial").val();
        BuscarSocioxRuc(ruc, razsocial);
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
        $("#chkUsuarioDerecho").prop("checked", x);
        $("#chkAsociacion").prop("checked", x);
        $("#chkGrupo").prop("checked", x);
        $("#chkRecaudador").prop("checked", x);
        $("#chkProveedor").prop("checked", x);
        $("#chkEmpleado").prop("checked", x);
    });

    $("#btnElegirSocio").on("click", function () {
        obtenerSociosSeleccionadas();
    });

    $("#btnNoelegirSocio").on("click", function () {
        Confirmar('Está seguro de quitar el socio de negocio ?',
            function () { RegresarSociosSeleccionadas(); },
            function () { },
            'Confirmar'
        );
    });

    loadTipoDocumento();

});

function addAgrupar() {
    Confirmar('Los Establecimientos de los socios de negocios dados de baja' + '\n' + 'serán trasladados al socio de negocio activo, desea continuar?',
    function () {
        var ReglaValor = [];
        var contador = 0;

        $('#tblSocioNegocioA tr').each(function () {
            var IdNro = $(this).find(".IDSocioOri").html();

            if (!isNaN(IdNro) && IdNro != null) {
                var bps_id = $(this).find(".IDSocioOri").html();
                if ($('#chkSocioOrigen' + bps_id).is(':checked')) {
                    ReglaValor[contador] = {
                        BPS_ID: bps_id
                    };
                    contador += 1;
                }
            }
        });

        var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
        if (contador > 0) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '../SocioAdministracion/AgruparSocio',
                data: ReglaValor,
                success: function (response) {
                    var dato = response;
                    if (dato.result == 1) {
                        LoadListarSocioB();
                        loadDataSocioA();
                        alert(dato.message);
                    } else if (dato.result == 0) {
                        $("#gridSocioFinal").html('');
                        alert(dato.message);
                    }
                }
            });
        } else {
            alert("Debe selecionar antes de continuar.");
        }
    },
    function () { },
    'Confirmar'
    );
}

function limpiarRuc() {
    $("#txtRuc").val("");
    $("#txtRazSocial").val("");
}

function BuscarSocioxRuc(ruc, razsocial) {
    $.ajax({
        url: '../SocioAdministracion/ConsultaRucSocio',
        type: 'POST',
        data: {
            ruc: ruc,
            razsocial: razsocial
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataSocioA();
                loadDataSocioB();
            } else if (dato.result == 0) {
                $("#gridSocioOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function loadDataSocioA() {
    loadDataGridTmp('../SocioAdministracion/ListarSocioA', "#gridSocioOriginal");
}

function loadDataSocioB() {
    loadDataGridTmp('../SocioAdministracion/ListarSocioB', "#gridSocioFinal");
}


function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', data: {}, url: Controller,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
            } else if (dato.result == 0) {
                $("#gridSocioOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function loadTipoDocumento() {
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
                    url: "../SocioAdministracion/USP_SOCIOS_LISTARPAGEJSON",
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
     { field: "ENT_TYPE_NOMBRE", width: 4, title: "Tipo Persona", template: "<a id='single_2'  href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${ENT_TYPE_NOMBRE}</a>" },
     { field: "TAXN_NAME", width: 3, title: "Tipo Doc.", template: "<a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${TAXN_NAME}</a>" },
     { field: "TAX_ID", width: 5, title: "Número", template: "<a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${TAX_ID}</a>" },
     { field: "BPS_NAME", width: 20, title: "Razon Social", template: "<font color='green'><a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;'>${BPS_NAME} ${BPS_FIRST_NAME} ${BPS_FATH_SURNAME} ${BPS_MOTH_SURNAME}</a></font>" },
     { field: "ACTIVO", width: 3, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${BPS_ID}') style='color:gray !important;' >#if(ACTIVO == 'A'){# ACTIVO #}else{# INACTIVO#}#  </a></font>" },
     { title: 'U', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_USER == '1'){#   <img src='../Images/botones/green.png'  width='16'  onclick='goUD(${BPS_ID});'  border='0' title='Perfil usuario con estado activo.'  style=' cursor: pointer; cursor: hand;'>  #} else { if(BPS_USER == '2'){#   <img src='../Images/botones/yellow.png'  width='16'  onclick='goUD(${BPS_ID});'  border='0' title='Perfil usuario con estado inactivo.'  style=' cursor: pointer; cursor: hand;'>#}else{#  <img src='../Images/botones/red.png' width='16' onclick='goUD(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Usuario Derecho
     { title: 'R', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_COLLECTOR == '1'){#  <img src='../Images/botones/green.png'  width='16' onclick='goRE(${BPS_ID});' border='0' title='Perfil recaudador con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_COLLECTOR == '2'){#  <img src='../Images/botones/yellow.png'  width='16' onclick='goRE(${BPS_ID});' border='0' title='Perfil recaudador con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goRE(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Recaudador
     { title: 'E', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_EMPLOYEE == '1'){#  <img src='../Images/botones/green.png'  width='16'  onclick='goEM(${BPS_ID});' border='0' title='Perfil empleado con estado activo.'  style=' cursor: pointer; cursor: hand;'>  #}else {  if(BPS_EMPLOYEE == '2') {#  <img src='../Images/botones/yellow.png' width='16' onclick='goEM(${BPS_ID});' title='Perfil empleado con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{ # <img src='../Images/botones/red.png' width='16' onclick='goEM(${BPS_ID});' title='ir a configurar perfil.' style=' cursor: pointer; cursor: hand;'> #}}#" },//Empleado
     { title: 'A', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_ASSOCIATION == '1'){#  <img src='../Images/botones/green.png' border='0' onclick='goAS(${BPS_ID});' title='Perfil asociado con estado activo.' width='16'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_ASSOCIATION == '2'){#  <img src='../Images/botones/yellow.png' border='0' onclick='goAS(${BPS_ID});' title='Perfil asociado con estado inactivo.' width='16'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goAS(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Asociacion
     { title: 'P', width: 1.5, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_SUPPLIER == '1'){#  <img src='../Images/botones/green.png'  width='16' onclick='goPV(${BPS_ID});' border='0' title='Perfil proveedor con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else { if(BPS_SUPPLIER == '2'){#  <img src='../Images/botones/yellow.png'  width='16' onclick='goPV(${BPS_ID});' border='0' title='Perfil proveedor con estado inactivo.'  style=' cursor: pointer; cursor: hand;'> #}else{#  <img src='../Images/botones/red.png' width='16' onclick='goPV(${BPS_ID});' title='ir a configurar perfil.'  style=' cursor: pointer; cursor: hand;'>  #}}#" },//Proveedor
     { title: 'G.E', width: 2, headerAttributes: { style: 'text-align: center' }, template: "#if(BPS_GROUP == '1'){#  <img src='../Images/botones/green.png'  width='16'  border='0' title='Perfil grupo empresarial con estado activo.'  style=' cursor: pointer; cursor: hand;'>   #} else {#  <img src='../Images/botones/red.png' width='16' title='ir a configurar perfil.'>  #}#" },//Grupo Empresarial
     { field: "BPS_ID", width: 1.5, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerSocioVentanaNueva(${BPS_ID});'  border='0' title='Ver Socio de Negocio En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },
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

function VerSocioVentanaNueva(bps_id) {
    window.open('../SocioAdministracion/Nuevo?set=' + bps_id, '_blank');
}

var agregar = function () {
    location.href = "SocioAdministracion/Nuevo";
};

var modificar = function () {
    alert("edit");
};
//  altRowTemplate: kendo.template($("#altRowTemplate").html())

function editar(idSel) {
    location.href = "../SocioAdministracion/Nuevo?set=" + idSel;
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
        url: '../SocioAdministracion/Eliminar',
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
    //location.href = "../DerechoAdministracion/Nuevo?set=" + id;
    window.open('../DerechoAdministracion/Nuevo?set=' + id, '_blank');
}

function goRE(id) {
    //location.href = "../RecaudadorAdministracion/Nuevo?set=" + id;
    window.open('../RecaudadorAdministracion/Nuevo?set=' + id, '_blank');
}
function goAS(id) {
    //location.href = "../AsociacionAdministracion/Nuevo?set=" + id;
    window.open('../AsociacionAdministracion/Nuevo?set=' + id, '_blank');
}

function goEM(id) {
    //location.href = "../EmpleadoAdministracion/Nuevo?set=" + id;
    window.open('../EmpleadoAdministracion/Nuevo?set=' + id, '_blank');
}

function goPV(id) {
    //location.href = "../ProveedorAdministracion/Nuevo?set=" + id;
    window.open('../ProveedorAdministracion/Nuevo?set=' + id, '_blank');
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

function obtenerSociosSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;

    $('#tblSocioNegocioA tr').each(function () {
        var IdNro = $(this).find(".IDSocioOri").html();

        if (!isNaN(IdNro) && IdNro != null) {
            var idEst = $(this).find(".IDSocioOri").html();
            var NomEst = $(this).find(".NomSocioOri").html();

            if ($('#chkSocioOrigen' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idEst,
                    Nombre: NomEst
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../SocioAdministracion/SocioArmaTemporalesOriginal',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    LoadListarSocioB();
                    loadDataSocioA();
                } else if (dato.result == 0) {
                    $("#gridSocioFinal").html('');
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
    }
}

function LoadListarSocioB() {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../SocioAdministracion/ListarSocioB',
        data: {},
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#gridSocioFinal").html(dato.message);
                //Lista los socios en la segunda grilla
                loadDataSocioB();
            } else if (dato.result == 0) {
                $("#gridSocioFinal").html('');
                $("#gridSocioOriginal").html('');
                alert(dato.message);
            }
        }
    });
}

function RegresarSociosSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    $('#tblSocioNegocioB tr').each(function () {
        var IdNro = $(this).find(".IDSocioDes").html();

        if (!isNaN(IdNro) && IdNro != null) {
            var idSoc = $(this).find(".IDSocioDes").html();
            var NomSoc = $(this).find(".NomSocioDes").html();

            if ($('#chkSocioDes' + idSoc).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idSoc,
                    Nombre: NomSoc
                };
                contador += 1;

            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../SocioAdministracion/SocioArmaTemporalesDestino',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataSocioB();
                    loadDataSocioA();
                } else if (dato.result == 0) {
                    $("#gridSocioFinal").html('');
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
        //Vuelve a listar 
        loadDataSocioB();
    }
}

function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: 'Agrupar',
        minHeight: 75,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}