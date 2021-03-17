var K_ITEM = { CHOOSE: '--SELECCIONE--', ALL: '--TODOS--' };

function loadTipoDocumento(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    }
    $.ajax({
        url: '../General/ListarTipoDocumento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
                if (control === "ddlTipoDocumentoVal") {
                    getValorConfigNumDoc($("#ddlTipoDocumentoVal").val());
                    if ($("#ddlTipoDocumentoVal option:selected").text() == "NINGUNO" || $("#ddlTipoDocumentoVal option:selected").text() == "ninguno") {
                        $("#txtNroDocumento").removeAttr("class", "requerido");
                    }
                }

            } else {
                alert(dato.message);
            }
        }
    });
}

function loadActividadEcon(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarActividadEcon',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoPersona(control, valSel) {
    var K_TipoPersonaItems = [{ Text: 'Jurídica', Value: 'J' }, { Text: 'Natural', Value: 'N' }];
    $('#' + control + ' option').remove();
    $.each(K_TipoPersonaItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadTipoObservacion(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoObservacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoParametro(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoParametro',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoestablecimiento(control, valSel) {
    //$('#ddlTipoestablecimiento option').remove();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoEstablecimiento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

//function loadTipoestablecimiento(control, valSel) {
//    //$('#ddlTipoestablecimiento option').remove();
//    $('#' + control + ' option').remove();
//    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
//    $.ajax({
//        url: '../General/ListarTipoEstablecimiento',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;

//                $.each(datos, function (indice, valor) {
//                    if (valor.Value == tipo)
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
//                    else
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
//                });
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}

function loadSubTipoestablecimiento(tipo) {
    $('#ddlSubtipoestablecimiento option').remove();
    $('#ddlSubtipoestablecimiento').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarSubTipoEstablecimiento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#ddlSubtipoestablecimiento').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlSubtipoestablecimiento').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

//function loadSubTipoestablecimientoPorTipo(tipo) {
//    $('#ddlSubtipoestablecimiento option').remove();
//    $('#ddlSubtipoestablecimiento').append($("<option />", { value: 0, text:K_ITEM.CHOOSE }));
//    $.ajax({
//        data: { IdTipo: tipo },
//        url: '../General/ListarSubtipoEstablecimientoPorTipo',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;
//                $.each(datos, function (indice, valor) {
//                    if (valor.Value == tipo) {
//                        $('#ddlSubtipoestablecimiento').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
//                    }
//                    else
//                        $('#ddlSubtipoestablecimiento').append($("<option />", { value: valor.Value, text: valor.Text }));
//                });
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}   

function loadSubTipoestablecimientoPorTipo(control, tipo, valSel) {
    //$('#ddlSubtipoestablecimiento option').remove();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        data: { IdTipo: tipo },
        url: '../General/ListarSubtipoEstablecimientoPorTipo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadDivisionesadministrativas(tipo) {
    $('#ddlDivisionAdministrativa option').remove();
    $('#ddlDivisionAdministrativa').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
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
                        $('#ddlDivisionAdministrativa').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlDivisionAdministrativa').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoIdentificacion(tipo) {
    $('#ddlTipoIdentificacion option').remove();
    $('#ddlTipoIdentificacion').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaIdentificadores',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#ddlTipoIdentificacion').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlTipoIdentificacion').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadDivisionesFiscales(tipo, territorio) {
    var vTer = 604;
    if (territorio != undefined)
        vTer = territorio;

    $('#ddlDivisionFiscal option').remove();
    $('#ddlDivisionFiscal').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDivisionesFiscales',
        data: { ter: vTer },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#ddlDivisionFiscal').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlDivisionFiscal').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCaracteristica(tipo) {
    $('#ddlCaracteristica option').remove();
    $('#ddlCaracteristica').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCartacteristica',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#ddlCaracteristica').append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#ddlCaracteristica').append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoDoc(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoDoc',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });


            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoCorreo(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoCorreo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadTipoTelefono(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoTelefono',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadTipoRedes(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoRedes',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadComboNivelAgente(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaNivelesDependencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadComboOficina(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../general/ListarOfiActivas',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, oficina) {
                    if (oficina.Value == valSel)
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadComboOficinaComercial(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../general/ListarOfiActivas',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, oficina) {
                    if (oficina.Value == valSel)
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                    else {

                        if (valSel == 0 && dato.valor == "0" && dato.Code > 0 && oficina.Value == dato.Code) {
                            $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                        } else {
                            $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text }));
                        }
                    }
                });
              
                if (dato.valor == "0") {
                    $('#' + control).attr("disabled", "disabled");
                }

            } else {
                alert(dato.message);
            }
        }
    });
}

function loadNivelAgente(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control + ' option').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaNivelesDependencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadComboOficinaDestino(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control + ' option').append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../general/ListarOfiActivas',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, oficina) {
                    if (oficina.Value == valSel)
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoPago(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoPago',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadGrupoEmpresarial(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    if (etiqueta != undefined) { $('#' + control).append($("<option />", { value: 0, text: etiqueta })); };
    $.ajax({
        url: '../General/ListaGrupoEmpresarial',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadMonedaRecaudacion(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }

    $.ajax({
        url: '../general/ListarMonedas',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, oficina) {
                    if (oficina.Value == valSel)
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

/*
DESCRIPCION : OBTIENE LAS DIVISIONES POR TIPO DE DIVISION SELECCIONADO
PARAMETROS  : 
            CONTROL: ID DEL HTML SELECT DONDE VAR CARGAR LA LISTA
            IDTIPO: ID DEL VALOR QUE FILTRARA LAS DIVISIONES
            VALSEL: VALOR QUE SE ENVIA EN CASO SE REQUIERA ESTABLECER UN VALOR DE LA LISTA COMO PREDETERMINADO.
AUTOR       :    KLESCANO (OBSOLETO, EN DESUSO)
FECHA       :    2014-09-30
*/
function loadTipoDivision(control, idTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDivision',
        type: 'POST',
        data: { tipo: idTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
/*
DESCRIPCION : OBTIENE LAS DIVISIONES POR TIPO DE DIVISION SELECCIONADO
PARAMETROS  : 
            CONTROL: ID DEL HTML SELECT DONDE VAR CARGAR LA LISTA
            IDTIPO: ID DEL VALOR QUE FILTRARA LAS DIVISIONES
            VALSEL: VALOR QUE SE ENVIA EN CASO SE REQUIERA ESTABLECER UN VALOR DE LA LISTA COMO PREDETERMINADO.
AUTOR       :    DBALVIS
FECHA       :    2014-09-30
*/
function loadDivisionXTipo(control, idTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDivision',
        type: 'POST',
        data: { tipo: idTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}



function loadTarifaMantenimiento(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTarifaMantenimento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadRol(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarRoles',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadBancos(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    if (etiqueta != undefined) { $('#' + control).append($("<option />", { value: 0, text: etiqueta })); };
    $.ajax({
        url: '../General/ListarBancos',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadRolesCargos(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarRolesCargos',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoDivisiones(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDivisiones',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadSubdivisionDep(control, idSub, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: 'Sin Dependencia' }));
    $.ajax({
        url: '../General/ListarSubdivisionDep',
        data: { id: idSub },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadSubdivision(control, idSub, valSel) {
    $('#' + control + ' option').remove();
    //$('#' + control).append($("<option />", { value: 0, text: 'Sin Dependencia' }));
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarSubdivision',
        data: { id: idSub },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadValoresDep(control, idSub, idSubdivision, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: 'Sin Dependencia' }));
    $.ajax({
        url: '../General/ListarValoresDep',
        data: { id: idSub, subdivision: idSubdivision },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadTipoIncidencia(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoIncidencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoSociedad(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoSociedad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoCreacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoCreacion',
        type: 'POST',

        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoDerecho(control, idCodigo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDerecho',
        data: { id: idCodigo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoGrupo(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoGrupo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoModUso(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoModUso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoObra(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoObra',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoRepertorio(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoRepertorio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoGasto(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoGasto',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });

            } else {
                alert(dato.message);
            }
        }
    });
}

function loadGrupoGasto(control, tipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarGrupoGasto',
        data: { tipo: tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoIdent(control, tipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaIdentificadores',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == tipo)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstados(control, valSel) {
    var K_TipoEstadoItems = [{ Text: 'Activo', Value: 1 }, { Text: 'Inactivo', Value: 2 }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadImpuestoValor(control, valSel) {
    var K_ValorImpuestoItems = [{ Text: '--SELECCIONE--', Value: 0 }, { Text: 'Monto(S/.)', Value: 1 }, { Text: 'Porcentaje(%)', Value: 2 }];
    $('#' + control + ' option').remove();
    $.each(K_ValorImpuestoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadEstadosMaestro(control, valSel) {
    var K_TipoEstadoItems = [{ Text: 'Activo', Value: 1 }, { Text: 'Todos', Value: 0 }, { Text: 'Inactivo', Value: 2 }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadCargoRecaudador(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCargosOficina',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadDivisionTipo(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDivionesTipo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

//function loadDivisionTipo(control, valSel) {

//    $('#' + control + ' option').remove();
//    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
//    $.ajax({
//        url: '../General/ListarDivionesTipo',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;
//                $.each(datos, function (indice, entidad) {
//                    if (entidad.Value == valSel)
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
//                    else
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
//                });
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}

function loadDivisionValor(control, idSubTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDivionesValor',
        data: { id: idSubTipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadValoresXSubdivision(control, idSub, idSubdivision, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: 'Sin Dependencia' }));
    //$('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListarValoresXSubdivision',
        data: { id: idSub, subdivision: idSubdivision },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoCaracteristicas(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoCaracteristicas',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadMediosDifusion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarMediosDifusion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoCalificador(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoCalificador',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });

            } else {
                alert(dato.message);
            }
        }
    });
}


function loadFiscales(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaSoloFiscales',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoLicencia(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadEstadoLicencia(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadoLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadEstadoIniPorTipoLic(control, valSel, idTipoLic, etiqueta) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }
    $.ajax({
        url: '../General/ListarEstadoLicencia',
        data: { idTipo: idTipoLic },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadDivision(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDivisiones',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadTemporalidades(control, valSel, etiqueta) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }
    $.ajax({
        url: '../General/ListaTemporalidad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadEstadoLicencia(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadoLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadDivisionXTipo(control, valSel, etiqueta) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }
    var division = {
        tipo: valSel
    };
    $.ajax({
        data: division,
        url: '../General/ListaDivisionesXTipo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadSocio(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    var division = {
        tipo: valSel
    };
    $.ajax({
        data: division,
        url: '../General/ListaSocios',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadDerechoTipo(control, valSel, etiqueta) {
    $('#' + control + ' option').remove();
    //$('#' + control).append($("<option />", { value: 0, text: '<--Seleccione-->' }));
    if (etiqueta != undefined) { $('#' + control).append($("<option />", { value: '0', text: etiqueta })); };
    $.ajax({
        url: '../General/ListarDerecho',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadTarifaAsociada(control, valSel, etiqueta, idTarifa) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }
    $.ajax({
        url: '../General/ListaTarifaAsociada',
        data: { codTarifa: idTarifa },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadGasto(control, vTipo, valSel, etiqueta) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }
    $.ajax({
        url: '../General/ListarDefinicionGasto',
        type: 'POST',
        data: { tipo: vTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadosLicencia(control, valSel) {
    var K_TipoEstadoItems = [{ Text: '--SELECCIONE--', Value: 0 }, { Text: 'INICIAL', Value: 1 }, { Text: 'INTERMEDIO', Value: 2 }, { Text: 'FINAL', Value: 3 }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadFormatoFacturacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarFormatoFacturacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadGrupoFacturacion(control, valSel, idSocio, idGrupoFac) {
    // alert(valSel + '...' + idSocio + '...' + idGrupoFac);
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        data: { idSocio: idSocio, idGrupoFac: idGrupoFac },
        url: '../General/ListarGrupoFacturacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEnvioFacturacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEnvioFacturacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadTipoDescuento(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDescuento',
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}



function loadFormatoFacturacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarFormatoFactura',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTarifaCaracteristica(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTarifaCartacteristica',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadAnioPlaneamiento(control, valSel, message, CodigoLic) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: message }));
    $.ajax({
        url: '../General/ListarAnioPlaneamiento',
        type: 'POST',
        data: { licId: CodigoLic },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}




function loadPeriodocidad(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarPeriodicidad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadPlantillaTarifa(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarPlantillaTarifa',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadFormatoFormula(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $('#' + control).append($("<option />", { value: 'N', text: 'NUMERICO' }));
    $('#' + control).append($("<option />", { value: 'P', text: 'PORCENTAJE' }));

    if (valSel != 0)
        $('#' + control).val(valSel);
}



function loadLicenciaTab(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarLicenciaTab',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoMoneda(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    }

    $.ajax({
        url: '../general/ListarTipoMoneda',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, oficina) {
                    if (oficina.Value == valSel)
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: oficina.Value, text: oficina.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadBloqueo(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarBloqueo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoproceso(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoproceso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadGrupoModalidad(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarGrupoModalidad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadModulo(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarModulo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadosLicencia(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadosLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadSigno(control, valSel) {
    var K_TipoEstadoItems = [{ Text: 'Positivo(+)', Value: 0 }, { Text: 'Negativo(-)', Value: 1 }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadPorcentaje(control, valSel) {
    var K_TipoEstadoItems = [{ Text: 'Porcentaje', Value: 1 }, { Text: 'Monto', Value: 2 }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}


function loadRegla(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarRegla',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTarifaTipos(control, valSel) {
    var K_TipoEstadoItems = [{ Text: '--SELECCIONE--', Value: '0' }, { Text: 'FUNCION', Value: 'F' }, { Text: 'REGLA', Value: 'R' }];
    $('#' + control + ' option').remove();
    $.each(K_TipoEstadoItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadFuncion(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarFuncion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadMonedas(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: '-- SELECCIONE --' }));
    $.ajax({
        url: '../General/ListarMonedas',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCuentaContable(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCuentaContable',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadLocalidad(control, message, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: message }));
    $.ajax({
        url: '../General/ListarLocalidad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoAforo(control, message, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: message }));
    $.ajax({
        url: '../General/ListarTipoAforo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoLocalidad(control, message, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: message }));
    $.ajax({
        url: '../General/ListarTipoLocalidad',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoLocalidadxLic(control,idlic,valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: message }));
    $.ajax({
        data: { idLic: idLic },
        url: '../General/ListarLocalidadxLic',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

///Sólo para caracteristicas y licencia
function loadFechaCaractLic(control, valParam, callback) {
   // alert(callback);
    var retorno = 0;
    $.ajax({
        url: '../General/ListarFechaCaracteristicasLic',
        type: 'POST',
        data: { idLic: valParam[0], idLicPlan:valParam[1] },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var datos = dato.data.Data;
                retorno = 1;

                if ($.isEmptyObject(datos)) {
                    $('#' + control + ' option').remove();
                    $('#' + control).append($("<option />", { value: 0, text: "NO TIENE CARACTERISTICAS" }));
                } else {
                    //if (message == undefined) { message = 'ACTIVOS'; }

                    $('#' + control + ' option').remove();
                    $('#' + control).append($("<option />", { value: 0, text: "ACTIVOS" }));
                    $.each(datos, function (indice, entidad) {
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                    });
                }
                if (callback != undefined) { callback(retorno); }
                

            } else if (dato.result == 0) {
                $('#' + control + ' option').remove();
                $('#' + control).append($("<option />", { value: -1, text: "--" }));
                alert(dato.message);
                retorno = 0;
            }
        }
    });
    return retorno;
}


function loadDescuento(control, idTipo, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDescuento',
        async: false,
        type: 'POST',
        data: { idTipo: idTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadComboTerritorio(idTipoEdit) {
    $('#ddlTerritorio option').remove();
    $.ajax({
        url: '../General/ListarTerritorio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, territorio) {
                    if (idTipoEdit != 0 && territorio.Value == idTipoEdit)
                        $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text, selected: true }));
                    else {
                        if (idTipoEdit == 0 && territorio.Text == "PERU") {
                            $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text, selected: true }));
                        } else {
                            $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text }));
                        }
                    }
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadFechaPlanificacion(control, valSel, idLic) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarFechaPlanificacion',
        type: 'POST',
        data: { idLic: idLic },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadAutorizaciones(control, idTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../Autorizacion/ListItems',
        type: 'POST',
        data: { codigoLic: idTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    var datos = dato.data.Data;
                    $.each(datos, function (indice, entidad) {
                        if (entidad.Value == valSel)
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                        else
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                    });
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadShow(control, idAut, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../Show/ListItems',
        type: 'POST',
        data: { idAutorizacion: idAut },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    var datos = dato.data.Data;
                    $.each(datos, function (indice, entidad) {
                        if (entidad.Value == valSel)
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                        else
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                    });
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadArtista(control, idShow, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../Artista/ListItems',
        type: 'POST',
        data: { idShow: idShow },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    var datos = dato.data.Data;
                    $.each(datos, function (indice, entidad) {
                        if (entidad.Value == valSel)
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                        else
                            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                    });
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadFormatoComision(control, valSel) {
    var K_TipoPersonaItems = [{ Text: K_ITEM.CHOOSE, Value: 0 }, { Text: 'Porcentaje (%)', Value: 'P' }, { Text: 'Monto (S/.)', Value: 'M' }];
    $('#' + control + ' option').remove();
    $.each(K_TipoPersonaItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function LoadAgente(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarAgente',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTarifa(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTarifas',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadTipoComision(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoComision',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadOrigenComision(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarOrigenComision',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;

                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTarifaModalidad(control, idMod, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTarifaModalidad',
        data: { idMod: idMod },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadTemporalidad(control, idMod, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTemporalidadPorModalidad',
        data: { idModalidad: idMod },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadOficinasComerciales(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarOficinasComerciales',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function LoadrSucursalesBanco(control, tipo, val) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        data: { IdBanco: tipo },
        url: '../General/ListarSucursalesBancariasSegunBanco',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == val)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadoWF(control, valSel, idCicloApro) {
    var idCA = 0;
    if (idCicloApro != 0)
        idCA = idCicloApro
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    //if (idCA != 0) {
    $.ajax({
        url: '../State/ListaItemEstado',
        data: { idCiclo: idCA },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
    //}
}

function LoadModuloNombre(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarNombreModulo',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoObjeto(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoObjeto',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoAgente(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoAgente',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadCicloAprobacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCicloAprobacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadEventos(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEventos',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoAccion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoAccion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoDato(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDato',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadProceso(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarProceso',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoPago(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoPago',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEventosWorkf(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEventosWorkf',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoEstado(control, Id, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoEstado',
        data: { IdCicloAprob: Id },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadTipoEstadoWrkf(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadoXTipoWrkf',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadoXTipo(control, idTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadoXTipo',
        data: { Id: idTipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadoXWorkFlow(control, IdWork, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarEstadorPorWorkFlow',
        data: { Id: IdWork },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


/*
lista los estados de un WORKFLOW
PARAMETROS:
control =ID DEL CONTROL
valSel = ID A SETEAR EL CONRTOL
idFiltro= PARAMENTRO DEL WF PARA EL FILTRO
addItemAll= indicador boolean para 
*/
function loadEstadoWF(param, valSel) {
    $('#' + param.control + ' option').remove();
    $('#' + param.control).append($("<option />", { value: 0, text: param.addItemAll == true ? K_ITEM.ALL : K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaEstadosWF', 
        data: { idWorkFlow: param.idFiltro != undefined ? param.idFiltro : 0 },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == param.valSel)
                        $('#' + param.control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + param.control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadWFEstado(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaEstadosWF', data: { idWorkFlow: valSel },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadWorkFlowEstado(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaWorkFlowEstado',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadMetodoPago(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarMetodoPago',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCuentaBancaria(control, IdSucursal, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCuentaBancaria',
        data: { sucbnkId: IdSucursal },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadSerie(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaSerie',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ListarSerieXtipo(control, type, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarSerieXtipo',
        data: {tipo : type},
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function LoadParametroTransicion(control, Tipo, ref, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarParametroTransicion',
        data: { idTipo: Tipo, referencia: ref },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

/*
CARGA DESCUENTOS ASOCIADOS A UNA TARIFA
*/
function loadDescuentoXTarifa(control, idTipo, valSel, idTarifa) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDescuentoXTarifa',
        type: 'POST',
        data: { idTipo: idTipo, idTarifa: idTarifa },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}


function LoadTipoReporte(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoReporte',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadTipoFactura(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoFactura',
        //data: { IdBanco: IdBanco },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel) 
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
//    $('#' + control + ' option').remove();
//    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
//    $.ajax({
//        url: '../General/ListarTipoFactura',
//        type: 'POST',
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;
//                $.each(datos, function (indice, entidad) {
//                    if (entidad.Value == valSel)
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
//                    else
//                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
//                });
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}

function loadFortmatoFacturacion(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarFortmatoFacturacion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCentrosContactos(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDropCentroContacto',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoCampania(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDropTipoCampania',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCampaniaContacto(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDropCampaniaContacto',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEntidades(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($('<option />', { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDropEntidades',
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadDivisionXtipo(control, tipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($('<option />', { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarDivisioneXtipo',
        type: 'POST',
        data: {tipo:tipo},
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($('<option />', { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstadoFactura(control, valSel) {
    var K_TipoPersonaItems = [{ Text: '--SELECCIONE--', Value: 0 },
                              { Text: 'CANCELADA PARCIAL', Value: 1 },
                              { Text: 'CANCELADO', Value: 2 },
                              { Text: 'PENDIENTE DE PAGO', Value: 3 },
                              { Text: 'ANULADA', Value: 4 },
                              //{ Text: 'SOLICITUD NC', Value: 5 },
                              //{ Text: 'SOLICITUD QUIEBRA', Value: 6 }
                              { Text: 'PROV. COBRANZA DUDOSA', Value: 11 },
                              { Text: 'CASTIGO', Value: 12 }
                             ];
    $('#' + control + ' option').remove();
    $.each(K_TipoPersonaItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function cargarDivisionXTipo(control, idTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarTipoDivision',
        type: 'POST',
        data: { tipo: idTipo },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadPeriodoPlanFactura(control, idLic, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaPerPlanFact',
        type: 'POST',
        data: { idLic: idLic },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadCentroContacto(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCentroContacto',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadCampaniaXtipo(control, tipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCampaniaPorTipo',
        data: { idTipoCampania: tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadLoteAgenteXCampania(control, idCampania, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarLoteAgenteXcampania',
        data: { IdCampania: idCampania },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadTipoPagofactura(control, valSel) {
    var K_TipoFacturaItems = [{ Text: 'TOTAL', Value: 'T' },
                              { Text: 'PARCIAL', Value: 'P' }
    ];
    $('#' + control + ' option').remove();
    $.each(K_TipoFacturaItems, function (indice, entidad) {
        if (entidad.Value == valSel)
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}

function loadCuentaBancariaXbanco(control, IdBanco, valSel, tipo_Moneda) {
 

    var tipoMoneda = '';
    if (tipo_Moneda == undefined)    
        tipoMoneda = '0';    
    else {
        if (tipo_Moneda == '44')
            tipoMoneda = 'DOL';
        else if (tipo_Moneda == 'PEN')
            tipoMoneda = 'SOL';
        else
            tipoMoneda = '0';
    }

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarCuentaBancariaXbanco',
        data: { IdBanco: IdBanco, moneda: tipoMoneda },
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadValoresConfiguracion(control,tipo, subTipo, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: -1, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarValoresConfiguracion',
        data: { tipo: tipo, subTipo: subTipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}



// OFICINA
function loadComboTipoNum(control, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaTipoNumerador',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, obs) {
                    if (indice.Value == valSel) {
                        $('#' + control).append($("<option />", { value: obs.Value, text: obs.Text, selected: true }));
                    }
                    else {
                        $('#' + control).append($("<option />", { value: obs.Value, text: obs.Text }));
                    }
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadComboDivisionesXOficina(control, idOficina, valSel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaDivisionesXOficina',
        type: 'POST',
        data: { IdOficina: idOficina },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

//function loadComboDivisionesXOficina(control, idOficina, valSel) {
//    $('#' + control + ' option').remove();
//    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
//    $.ajax({
//        url: '../General/ListaDivisionesXOficina',
//        type: 'POST',
//        data: { IdOficina: idOficina },
//        beforeSend: function () { },
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                var datos = dato.data.Data;

//                $.each(datos, function (indice, obs) {                    
//                    alert(obs.Value);
//                    alert(obs.Text);
//                    alert(indice.Value);
//                    //if (indice.Value == valSel) {
//                    if (indice.Value == indice.Value) {
//                        $('#' + control).append($("<option />", { value: obs.Value, text: obs.Text, selected: true }));
//                    }
//                    else {
//                        $('#' + control).append($("<option />", { value: obs.Value, text: obs.Text }));
//                    }
//                });

//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}




//DESCUENTOS******************************
function loadTipoDescuentoxTipoDescuento(control, valSel, disctype) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListarDescuentosxTipoDesc',
        data: { disctype: disctype },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.DISC_ID == valSel)
                        $('#' + control).append($("<option />", { value: valor.DISC_ID, text: valor.DISC_NAME, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.DISC_ID, text: valor.DISC_NAME }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

//**************************************** MEGA CONCIERTOS
function loadTipoAforo(control, valsel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../LicenciaMegaConcierto/ListarTipoAforo',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    //if (valor.CAP_ID == valSel)
                    //    $('#' + control).append($("<option />", { value: valor.CAP_ID, text: valor.CAP_DESC, selected: true }));
                    //else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

}
function loadComboTarifa(idModalidad, idTemp, valSel,hidCodigoTarifa) {
    $('#lblTarifaDesc' + ' option').remove();
    $('#lblTarifaDesc').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ObtenerTarifaAsociada',
        data: { codModalidad: idModalidad, codTemp: idTemp },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                 
                    if (valor.RATE_ID == valSel)
                    {
                        $('#lblTarifaDesc').append($("<option />", { value: valor.RATE_ID, text: valor.RATE_DESC, selected: true }));
                        //$('#'+hidCodigoTarifa).val(valor.RATE_ID);
                    } else
                        $('#lblTarifaDesc').append($("<option />", { value: valor.RATE_ID, text: valor.RATE_DESC }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function LoadComboObtenerNombreTarifa(idModalidad, idLabelSetting, idTextSetting, hidCodigoTarifa) {
    //$("#" + idLabelSetting).html("No hay Tarifa");
    //if (idTextSetting != undefined) {
    //    $("#" + idTextSetting).val(0);
    //}
    $('#lblTarifaDesc' + ' option').remove();
    $('#lblTarifaDesc').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ObtenerNombreTarifa',
        data: { Id: hidCodigoTarifa },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
               // var datos = dato.data.Data;
             //   $.each(datos, function (indice, valor) {
                    $('#' + idTextSetting).append($("<option />", { value: hidCodigoTarifa, text: dato.valor, selected: true }));
             //   });
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function loadTipoNotaCredito(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListaTipoNotaCredito',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadTipoShowLicencia(control, valsel,codlic) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../LicenciaMegaConcierto/ListarShowxLicencia',
        data: { codlic: codlic },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    //if (valor.CAP_ID == valSel)
                    //    $('#' + control).append($("<option />", { value: valor.CAP_ID, text: valor.CAP_DESC, selected: true }));
                    //else
                    $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

}


function loadTipoArtistaLicencia(control, valsel, codshow) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../Artista/ListarArtistaxShow',
        data: { codshow: codshow },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    //if (valor.CAP_ID == valSel)
                    //    $('#' + control).append($("<option />", { value: valor.CAP_ID, text: valor.CAP_DESC, selected: true }));
                    //else
                    $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });
}

//---
//**************************************** MEGA CONCIERTOS
function loadListaTipoFacturacionManual(control, valsel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListaTipoFacturacionManual',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valsel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                    $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

}


function loadSubTipoDivisiones(idDivision, controlLabel1, controlLabel2, controlLabel3, controhid1, controlhid2, controlhid3, ddl1, ddl2, ddl3) {

    $.ajax({
        url: '../General/ListarSubTipoDivisiones',
        type: 'POST',
        data: { idDivision: idDivision },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                var nro = 0;
                $.each(datos, function (indice, entidad) {
                    nro += 1;
                    //alert(entidad.Value); alert(entidad.Text);
                    if (nro === 1) {
                        $("#" + controlLabel1).html(entidad.Text);
                        $("#" + controhid1).val(entidad.Value);
                    } else if (nro == 2) {
                        $("#" + controlLabel2).html(entidad.Text);
                        $("#" + controlhid2).val(entidad.Value);
                    } else if (nro == 3) {
                        $("#" + controlLabel3).html(entidad.Text);
                        $("#" + controlhid3).val(entidad.Value);
                    }
                });

                $('#' + ddl1 + ' option').remove();
                $('#' + ddl1).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

                $('#' + ddl2 + ' option').remove();
                $('#' + ddl2).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

                $('#' + ddl3 + ' option').remove();
                $('#' + ddl3).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));


                var subtipo1 = $("#" + controhid1).val();
                //loadValoresXsubtipo_Division(INEI, subtipo1, 0, 'ddlSubTipo1', 0);
                loadValoresXsubtipo_Division(idDivision, subtipo1, 0, ddl1, 0);

            } else {
                alert(dato.message);
            }
        }
    });
}


function loadValoresXsubtipo_Division(idDivision, idSubTipo, idBelong, control, valsel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListarValoresXsubtipo_Division',
        type: 'POST',
        data:{idDivision:idDivision, idSubTipo:idSubTipo, idBelong:idBelong},
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valsel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

}



function loadListaContableDesplegable(control, valsel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListaContableDesplegable',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valsel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

    //$('#' + control).append($("<option />", { value: '-10', text: "VIGENTES" }));

}

function loadSubTipoParametro(control, idTipoParametro, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarSubTipoParametro',
        data: { idTipoParametro: idTipoParametro },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadTipoReporteDistribucion(control, idTipoParametro, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../ReporteDistribucion/ListarTipoReporte',
        data: { idTipoParametro: idTipoParametro },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadListarFiltroOrdenConsultaDocumento(control, valsel) {
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));

    $.ajax({
        url: '../General/ListarFiltroOrdenConsultaDocumento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valsel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }

    });

    //$('#' + control).append($("<option />", { value: '-10', text: "VIGENTES" }));

}


function loadTipoRquerimiento(control, valSel,tipo) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        async: false,
        url: '../AdministracionModuloRequerimientos/ListarTipoRequerimiento',
        data: { tipo: tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}


function loadTipoDocAlfresco(control, valSel) {

    $('#' + control + ' option').remove();
    $.ajax({
        url: '../General/ListarTipoDocAlfresco',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { name: entidad.Value_Alfresco, value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { name: entidad.Value_Alfresco, value: entidad.Value, text: entidad.Text }));
                });


            } else {
                alert(dato.message);
            }
        }
    });
}

function loadArtista_X_Licencia(control, valSel, Cod_lic) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/Lista_Artista_X_Licencia',
        data: { Cod_lic: Cod_lic },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });


            } else {
                alert(dato.message);
            }
        }
    });
}

function loadPlaneamientoOpcionxLicencia(control, valSel, CodigoLicencia,Opcion) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../General/ListarPlaneamientoxLicenciaOpcion',
        data: { CodigoLicencia: CodigoLicencia, Opcion: Opcion },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });


            } else {
                alert(dato.message);
            }
        }
    });
}

function loadTipoInactivacionLicencia(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../Licencia/ListaTipoInactivacionLicencia',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });


            } else {
                alert(dato.message);
            }
        }
    });
}