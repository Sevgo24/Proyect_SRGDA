var K_ID_POPUP_DIR = "mvDireccion";
var K_ID_DIR = "divDir";
var K_WIDTH_CON = 400;
var K_HEIGHT_CON = 250;

$(function () {
    $("#hidAccionMvDir").val("0");
    $("#hidAccionMvCon").val("0");
    var id = GetQueryStringParams("id");

    $("#tabs").tabs();

    /*Pestaña Dirección*/
    initFormDireccionNMV({
        parentControl: K_ID_DIR
    });

    initFormDireccion({
        id: K_ID_POPUP_DIR,
        parentControl: "divDireccion",
        width: 850,
        height: 300,
        evento: addDireccion,
        modal: true
    });

    $("#mvContacto").dialog({
        autoOpen: false,
        width: K_WIDTH_CON,
        height: K_HEIGHT_CON,
        buttons: {
            "Agregar": addContacto,
            "Cancel": function () { $("#mvContacto").dialog("close"); $('#txtContacto').css({ 'border': '1px solid gray' }); }
        }, modal: true
    });

    $(".addDireccion").on("click", function () { limpiarDireccion(); $("#" + K_ID_POPUP_DIR).dialog("open"); });
    $(".addContacto").on("click", function () { $("#mvContacto").dialog({});  $("#mvContacto").dialog("open"); limpiarContacto(); });



    $("#ddlTipoIdentificacion").on("change", function () {
        $("#hidExitoValNumero").val("0");
        msgErrorB("divResultValidarDoc", "");

        if ($(this).val() == 0) {
            $("#txtNroIdentificacion").val("");
        }
        else {
            $("#txtContacto").val("");
            getValorConfigNumDoc($("#ddlTipoIdentificacion").val());
        }
    });

    $("#ddlRol").on("change", function () {
        var codigo = $("#ddlRol").val();
        $("#hidRolid").val(codigo);
    });

    $("#ddlTipoIdentificacion").on("change", function () {
        var codigo = $("#ddlTipoIdentificacion").val();
        $("#hidIdentidicacion").val(codigo);
    });

    //$(document).ready(function () {
    //    $("#txtNroIdentificacion").keydown(function (event) {
    //        if (event.shiftKey) {
    //            event.preventDefault();
    //        }

    //        if (event.keyCode == 46 || event.keyCode == 8) {
    //        }
    //        else {
    //            if (event.keyCode < 95) {
    //                if (event.keyCode < 48 || event.keyCode > 57) {
    //                    event.preventDefault();
    //                }
    //            }
    //            else {
    //                if (event.keyCode < 96 || event.keyCode > 105) {
    //                    event.preventDefault();
    //                }
    //            }
    //        }
    //    });
    //});      

    loadTipoIdentificacion(0);
    loadRolesCargos('ddlRol');

    $("#txtNroIdentificacion").on("keypress", function (e) {
        var key = (e ? e.keyCode || e.which : window.event.keyCode);
        if (key == 13) {
            consultarSocio();
        }
    });

    $("#btnBuscarContacto").on("click", function () {
        consultarSocio();
    });    

    $("#btnGrabar").on("click", function () {
        grabar();
    }).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../SucursalesBancarias/Create';
    }).button();

    $("#btnVolver").on("click", function () {
        document.location.href = '../SucursalesBancarias/';
    }).button();

    //---------------------------------------------------------------
    if (id != null) {
        $("#divTituloPerfil").html("Sucursales bancarias - Actualización");
        //K_ACCION_ACTUAL = K_ACCION.Modificacion;
        //$("#hidOpcionEdit").sval(0);
        $("#hidAccionMvCon").val("1");
        ObtenerDatos(id);
        //ObtenerSocio();
        //nuevo();
    }
    else {
        $("#divTituloPerfil").html("Sucursales bancarias - Nuevo");
        //    K_ACCION_ACTUAL = K_ACCION.Modificacion;
        //$("#hidOpcionEdit").val(0);
        //    $("#hidCodigoEST").val(id);
        //    ObtenerDatos(id);
    }
    //---------------------------------------------------------------

    $("#tabs-2").on("click", function () {
        loadDataContacto();
    });

    loadDataDireccion();
    loadDataContacto();

    $("#hidCodigoBanco").val(3);

    //$("#ddlBancos").on("change", function () {
    //    var codigo = $("#ddlBancos").val();
    //    $("#hidCodigoBanco").val(codigo);
    //    ObtenerLongitudCodigoSucursal($("#hidCodigoBanco").val());
    //});
});

//------------------------Cargar datos pestañas----------------------------------------
function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
}

function loadDataContacto() {
    loadDataGridTmp('ListarContacto', "#gridContacto");
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

//------------------------Funciones----------------------------------------

function grabar() {

    if (ValidarRequeridos()) {
        //var idbanco = $("#ddlBancos").val();
        var sucursal = {
            BNK_ID: $("#ddlBancos").val(),
            //auxBNK_ID: $("#hidCodigoBancoAntiguo").val(),
            BRCH_ID: $("#txtIdSucursal").val(),
            BRCH_NAME: $("#txtNombre").val(),
            ADD_ID: $("#hidCodigoDireccion").val()
        };
        $.ajax({
            url: 'Editar',
            data: sucursal,
            type: 'POST',
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    location.href = "../SUCURSALESBANCARIAS/";
                    alert(dato.message);
                } else {
                    msgError(dato.message);
                }
            }
        });
    }
    return false;
}

function ObtenerDatos(idSel) {
    $.ajax({
        url: '../SUCURSALESBANCARIAS/Obtiene',
        data: { id: idSel },
        type: 'POST',
        success: function (response) {
            var dato = response;

            if (dato.result == 1) {
                var sucursal = dato.data.Data;
                if (sucursal != null) {
                    $("#hidCodigoSucursal").val(sucursal.Id);
                    $("#txtIdSucursal").val(sucursal.Id);
                    loadBancos('ddlBancos', sucursal.idBanco);
                    $("#txtNombre").val(sucursal.Nombre);
                    $("#hidCodigoDireccion").val(sucursal.idDireccion);
                    $("#hidCodigoBanco").val(sucursal.idBanco);
                }
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

function consultarSocio() {
    var estado = validarLongitudNumDoc();
    if (estado) {
        var idtipo = $("#ddlTipoIdentificacion").val();
        var doc = $("#txtNroIdentificacion").val();
        ObtenerSocio(idtipo, doc);
    }
}

function validarLongitudNumDoc() {
    msgError("");
    var exito = false;
    var tipoDoc = $("#ddlTipoIdentificacion option:selected").val();
    var tipoDocDesc = $("#ddlTipoIdentificacion option:selected").text();

    getValorConfigNumDoc(tipoDoc);
    var numValidar = $("#hidCantNumValidar").val();

    if ($.trim($("#txtNroIdentificacion").val()) != "") {
        if (tipoDocDesc == "DNI") {
            if ($("#txtNroIdentificacion").val().length != numValidar) {
                msgErrorB("divResultValidarDoc", "Longitud del DNI debe contener " + "8" + " digitos.");
            } else {
                exito = true;
                msgErrorB("divResultValidarDoc", "");
            }
        } else {
            if ($("#txtNroIdentificacion").val().length != numValidar) {
                msgErrorB("divResultValidarDoc", "Longitud del RUC debe contener " + "11" + " digitos.");
            } else {
                exito = true;
                msgErrorB("divResultValidarDoc", "");
            }
        }
        if (exito) {
            return true;
        } else {
            return false;
        }
    } else {
        msgErrorB("divResultValidarDoc", "Ingrese número de documento.");
    }
}

function getValorConfigNumDoc(itipo) {
    $.ajax({
        url: '../General/GetConfigTipoDocumento',
        data: { tipo: itipo },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#hidCantNumValidar").val(dato.valor);
            } else {
                alert(dato.message);
            }
        }
    });
}

function ObtenerSocio(tipoidentificacion, nroidentificacion) {
    var idtipo = tipoidentificacion;
    var doc = nroidentificacion;

    $.ajax({
        url: '../General/BuscarsocioTipoDocumento',
        type: 'POST',
        data: { idTipoDocumento: idtipo, nroDocumento: doc },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#hidCodigoBPS").val(dato.Code);
                $("#txtContacto").val(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function limpiarDireccion() {
    $("#txtUrb").val("");
    $("#txtUbigeo").val("");
    $("#txtReferencia").val("");
    $("#hidAccionMvDir").val(0);
    $("#hidEdicionDir").val(0);
}

function limpiarContacto() {
    $("#txtContacto").val("");
    $("#txtNroIdentificacion").val("");
    $("#ddlTipoIdentificacion").val(0);
    $("#ddlRol").val(0);
    $("#hidAccionMvCon").val(0);
    $("#hidEdicionCon").val(0);
    $('#txtContacto').css({ 'border': '1px solid gray' });
}

function delAddDireccion(idDel) {
    $.ajax({
        url: 'DellAddDireccion',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadDataDireccion();
            }
        }
    });
    return false;
}

function updAddDireccion(idUpd) {
    limpiarDireccion();

    $.ajax({
        url: 'ObtieneDireccionTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                var direccion = dato.data.Data;
                if (direccion != null) {

                    $("#hidAccionMvDir").val("1");
                    $("#hidEdicionDir").val(direccion.Id);
                    $("#txtUrb").val(direccion.Urbanizacion);
                    $("#txtNro").val(direccion.Numero == 0 ? "" : direccion.Numero);
                    $("#txtMz").val(direccion.Manzana);
                    $("#ddlTipoDireccion").val(direccion.TipoDireccion);
                    $("#ddlTerritorio").val(direccion.Territorio);
                    $("#hidCodigoUbigeo").val(direccion.CodigoUbigeo);
                    $("#txtReferencia").val(direccion.Referencia);
                    $("#ddlCodPostal").val(direccion.CodigoPostal);
                    $("#ddlUrbanizacion").val(direccion.TipoUrba);
                    $("#txtLote").val(direccion.Lote);
                    $("#ddlDepartamento").val(direccion.TipoDepa);
                    $("#txtNroPiso").val(direccion.NroPiso);
                    $("#ddlAvenida").val(direccion.TipoAvenida);
                    $("#txtAvenida").val(direccion.Avenida);
                    $("#ddlEtapa").val(direccion.TipoEtapa);
                    $("#txtEtapa").val(direccion.Etapa);
                    $("#hidCodigoUbigeo").val(direccion.CodigoUbigeo);
                    $("#txtUbigeo").val(direccion.DescripcionUbigeo);

                    $("#" + K_ID_POPUP_DIR).dialog("open");
                } else {
                    alert("No se pudo obtener la direccion para editar.");
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

function delAddContacto(idDel) {
    $.ajax({
        url: 'DellAddContacto',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                loadDataContacto();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    return false;
}

function updAddContacto(idUpd) {
    limpiarContacto();

    $.ajax({
        url: 'ObtieneContactoTmp',
        data: { idCon: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result === 1) {
                var param = dato.data.Data;
                if (param != null) {
                    $("#hidAccionMvCon").val("1");
                    $("#hidEdicionCon").val(param.Id);
                    $("#txtNroIdentificacion").val(param.idDocumento);
                    $("#txtContacto").val(param.Nombre);
                    loadRolesCargos('ddlRol', param.idRol);
                    $("#hidRolid").val(param.idRol);
                    loadTipoIdentificacion(param.Numero);
                    $("#hidIdentidicacion").val(param.Numero);
                    $("#hidCodigoBPS").val(param.contacto);
                    $("#mvContacto").dialog("open");
                } else {
                    alert("No se pudo obtener el contacto para editar.");
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

//------------------------Agregar datos pestañas----------------------------------------
function addDireccion() {

    var IdAdd = 0;
    if ($("#hidAccionMvDir").val() == "1") IdAdd = $("#hidEdicionDir").val()

    $("#avisoMV").val("");

    if (ValidarRequeridosMV()) {

        var direccion = {
            Id: IdAdd,
            TipoDireccion: $("#ddlTipoDireccion").val(),
            RazonSocial: "",
            Territorio: $("#ddlTerritorio").val(),
            CodigoUbigeo: $("#hidCodigoUbigeo").val(),
            Referencia: $("#txtReferencia").val(),
            CodigoPostal: $("#ddlCodPostal").val(),
            TipoUrba: $("#ddlUrbanizacion").val(),
            Urbanizacion: $("#txtUrb").val(),
            Numero: $("#txtNro").val(),
            Manzana: $("#txtMz").val(),
            Lote: $("#txtLote").val(),
            TipoDepa: $("#ddlDepartamento").val(),
            NroPiso: $("#txtNroPiso").val(),
            TipoAvenida: $("#ddlAvenida").val(),
            Avenida: $("#txtAvenida").val(),
            TipoEtapa: $("#ddlEtapa").val(),
            Etapa: $("#txtEtapa").val(),
            TipoDireccionDesc: $("#ddlTipoDireccion option:selected").text(),
            TipoTerritorioDesc: $("#ddlTerritorio option:selected").text(),
            TipoUrbaDesc: $("#ddlUrbanizacion option:selected").text(),
            TipoDepaDesc: $("#ddlDepartamento option:selected").text(),
            TipoAvenidaDesc: $("#ddlAvenida option:selected").text(),
            TipoEtapaDesc: $("#ddlEtapa option:selected").text()
        };

        $.ajax({
            url: 'AddDireccion',
            type: 'POST',
            data: direccion,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    loadDataDireccion();
                } else {
                    alert(dato.message);
                }
            }
        });
        $("#mvDireccion").dialog("close");
    }
}

function addContacto() {

    $("#divResultValidarDoc").val("");

    if ($("#txtContacto").val() == '') {
        $('#txtContacto').css({ 'border': '1px solid red' });
    }

    if ($("#txtNroIdentificacion").val() == '') {
        $('#txtNroIdentificacion').css({ 'border': '1px solid red' });
    }

    if ($("#ddlTipoIdentificacion").val() == '0') {
        $('#ddlTipoIdentificacion').css({ 'border': '1px solid red' });
    }

    if ($("#ddlRol").val() == '0') {
        $('#ddlRol').css({ 'border': '1px solid red' });
    }

    else {

        var estado = validarLongitudNumDoc();

        if (estado) {
            var IdAdd = 0;
            if ($("#hidAccionMvCon").val() === "1") IdAdd = $("#hidEdicionCon").val();

            ObtenerSocio($("#hidIdentidicacion").val(), $("#txtNroIdentificacion").val());

            var entidad = {
                Documento: $("#ddlTipoIdentificacion option:selected").text(),
                contacto: $("#hidCodigoBPS").val(),
                Nombre: $("#txtContacto").val(),
                Rol: $("#ddlRol option:selected").text(),
                idRol: $("#hidRolid").val(),
                idBanco: $("#hidCodigoBanco").val(),
                Numero: $("#hidIdentidicacion").val(),
                idDocumento: $("#txtNroIdentificacion").val(),
                Idsucursal: $("#hidCodigoSucursal").val(),
                Id: IdAdd
            };

            $.ajax({
                url: 'AddContacto',
                type: 'POST',
                data: entidad,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    if (dato.result == 1) {
                        loadDataContacto();
                    } else {
                        alert(dato.message);
                    }
                }
            });
            $("#mvContacto").dialog("close");
            $('#txtContacto').css({ 'border': '1px solid gray' });
            $('#txtNroIdentificacion').css({ 'border': '1px solid gray' });
            $('#ddlTipoIdentificacion').css({ 'border': '1px solid gray' });
            $('#ddlRol').css({ 'border': '1px solid gray' });
        }
    }
}

function actualizarDirPrincipal(idDir) {

    $.ajax({
        url: 'SetDirPrincipal',
        data: { idDir: idDir },
        type: 'POST',
        success: function (response) {
            var dato = response;
            alert(dato.message);
        }
    });
}