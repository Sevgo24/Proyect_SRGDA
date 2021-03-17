//var K_RUTA_SITE_BASE = "http://192.168.252.105/SGRDA";
var K_RUTA_SITE_BASE = "http://localhost:9035/"; /*ruta para la controlladora API UPLOAD*/

///updaload para el tab  documentos de licencias
function InitUpload(ctrlFileUpload, id) {

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);
    var ajaxRequest = $.ajax({
        type: "POST",
        async:false,
        url: K_RUTA_SITE_BASE + "/api/FileUpload/UploadTabLicencia",
        contentType: false,
        processData: false,
        data: data,
        success: function (responseData, textStatus) {
            if (textStatus == 'success') {
                if (responseData != null) {
                    if (responseData.Key) {
                        $.ajax({
                            url: '../General/ActualizarNombreDoc',
                            type: 'POST',
                            //async: false,
                            data: { nombre: responseData.Value ,idDoc: id},
                            success: function (response) {
                                var dato = response;
                                validarRedirect(dato); /*add sysseg*/
                                if (dato.result == 1) {
                                    loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
                                } else if (dato.result == 0) {
                                    alert(dato.message);
                                }
                            }
                        });
                        $("#" + ctrlFileUpload).val('');
                    } else {
                        alert(responseData.Value);
                    }
                }
            } else {
                alert(responseData.Value);
            }
        }
    });

    //ajaxRequest.done(function (responseData, textStatus) {
    //    if (textStatus == 'success') {
    //        if (responseData != null) {
    //            if (responseData.Key) {
    //                $.ajax({                        
    //                    url: '../General/ActualizarNombreDoc',
    //                    type: 'POST',
    //                    //async: false,
    //                    data: { idDoc: id, nombre: responseData.Value },
    //                    success: function (response) {
    //                        var dato = response;
    //                        validarRedirect(dato); /*add sysseg*/
    //                        if (dato.result == 1) {
    //                            loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
    //                        } else if (dato.result == 0) {
    //                            alert(dato.message);
    //                        }
    //                    }
    //                });
    //                $("#" + ctrlFileUpload).val('');
    //            } else {
    //                alert(responseData.Value);
    //            }
    //        }
    //    } else {
    //        alert(responseData.Value);
    //    }
    //});

}


///updaload para el tab  documentos de licencias
function InitUploadTrace(ctrlFileUpload, documento, callBack) {

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }

    data.append("hidKey", documento.Id);
    data.append("hidIdLic", documento.codLic);
    data.append("hidIdDoc", documento.Id);
    data.append("hidTipoDOc", documento.TipoDocumento);
    data.append("hidArchivo", documento.Archivo);
    data.append("hidUser", documento.UsuarioActual);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: K_RUTA_SITE_BASE + "/api/FileUploadTracer/UploadTabProcesoLicencia",
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    $("#" + ctrlFileUpload).val('');
                } else {
                    alert(responseData.Value);
                }
            }
        } else {
            alert(responseData.Value);
        }
        callBack(responseData);
    });
}

function InitUploadObjeto(ctrlFileUpload, objeto, callBack) {
    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", objeto.Id);
    data.append("hidCodigo", objeto.Id);
    data.append("hidCodigoInterno", objeto.CodigoInterno);
    data.append("hidDescripcion", objeto.Descripcion);
    data.append("hidTipo", objeto.Tipo);
    data.append("hidArchivo", objeto.Archivo);
    data.append("hidAsunto", objeto.Asunto);
    data.append("hidUser", objeto.UsuarioActual);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: K_RUTA_SITE_BASE + "/api/FileUploadObjeto/UploadDocumentoObjeto",
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    $("#" + ctrlFileUpload).val('');
                } else {
                    alert(responseData.Value);
                }
            }
        } else {
            alert(responseData.Value);
        }
        callBack(responseData);
    });
}

/*inicio upload para los socios y los distintos roles*/
function InitUploadTabDocSocio(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadSocio/UploadTabDocSocio";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
function InitUploadTabDocDerecho(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadDerecho/UploadTabDocDerecho";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
function InitUploadTabDocRecaudador(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadRecaudador/UploadTabDocRecaudador";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
function InitUploadTabDocAsociacion(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadAsociacion/UploadTabDocAsociacion";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
function InitUploadTabDocEmpleado(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadEmpleado/UploadTabDocEmpleado";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
function InitUploadTabDocProveedor(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadProveedor/UploadTabDocProveedor";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
/*fin upload para los socios y los distintos roles*/

function InitUploadTabDocEstablecimiento(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadEstablecimiento/UploadTabDocEstablecimiento";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    UploadTabDocTMP(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile);
}

function InitUploadTabDocOficina(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadOficina/UploadTabDocOficina";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    UploadTabDocTMP(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile);
}

function InitUploadTabDocCampania(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadEstablecimiento/UploadTabDocCampania";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    UploadTabDocTMP(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile);
}

function InitUploadTabDocContactoCallCenter(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadContactoCallCenter/UploadTabDocContactoCallCenter";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    UploadTabDocTMP_Id(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile);
}

///funcion generica exclusiva para almacenar docuemtnos temporalmente
function UploadTabDocTMP(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile) {

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);
    var ajaxRequest = $.ajax({
        type: "POST",
        url: urlControllerUpload,
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    $.ajax({
                        url: urlControllerSetNameFile,
                        type: 'POST',
                        data: { idDoc: id, nombre: responseData.Value },
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato); /*add sysseg*/
                            if (dato.result == 1) {
                                loadDataDocumento();
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });
                    $("#" + ctrlFileUpload).val('');
                } else {
                    alert(responseData.Value);
                }
            }
        } else {
            alert(responseData.Value);
        }
    });
}


///funcion generica exclusiva para almacenar docuemtnos temporalmente enviando Id para filtrar documentos
function UploadTabDocTMP_Id(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile) {

    var IdContactoLlamada = $("#hidContactoLlamada").val();

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);
    var ajaxRequest = $.ajax({
        type: "POST",
        url: urlControllerUpload,
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    $.ajax({
                        url: urlControllerSetNameFile,
                        type: 'POST',
                        data: { idDoc: id, nombre: responseData.Value },
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato); /*add sysseg*/
                            if (dato.result == 1) {
                                loadDataDocumento(IdContactoLlamada);
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });
                    $("#" + ctrlFileUpload).val('');
                } else {
                    alert(responseData.Value);
                }
            }
        } else {
            alert(responseData.Value);
        }
    });
}


///funcion generica exclusiva para almacenar docuemtnos temporalmente
function UploadTabDocTMP2(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUp) {
    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);
    var ajaxRequest = $.ajax({
        type: "POST",
        url: urlControllerUpload,
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    /*subio ok*/
                    $.ajax({
                        url: urlControllerSetNameFile,
                        type: 'POST',
                        data: { idDoc: id, nombre: responseData.Value },
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato); /*add sysseg*/
                            if (dato.result == 1) {
                                loadDataDocumento();
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });
                    $("#" + ctrlFileUpload).val('');
                } else {
                    /*subio No ok*/
                    if (urlControllerErrorUp != undefined) {
                        $.ajax({
                            url: urlControllerErrorUp,
                            type: 'POST',
                            data: { id: id, },
                            success: function (response) {
                                var dato = response;
                                //  if (dato.result == 1) { alert("ok eliminado..por error al subir."); }
                                loadDataDocumento();
                            }
                        });
                    }
                    alert(responseData.Value);
                }
            }
        } else {
            if (urlControllerErrorUp != undefined) {
                $.ajax({
                    url: urlControllerErrorUp,
                    type: 'POST',
                    data: { id: id, },
                    success: function (response) {
                        var dato = response;
                        // if (dato.result == 1) { alert("ok eliminado..por error al subir."); }
                        loadDataDocumento();
                    }
                });
            }
            alert(responseData.Value);

        }
    });
}

///updaload para el tab  documentos de Facturas
function InitUploadTabDocFactura(ctrlFileUpload, id) {
    var urlControllerUpload = K_RUTA_SITE_BASE + "/api/FileUploadFactura/UploadTabFactura";
    var urlControllerSetNameFile = "ActualizarNombreDocTmp";
    var urlControllerErrorUpload = "DellAddDocumento";
    UploadTabDocTMPFactura(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUpload);
}
///funcion generica exclusiva para almacenar docuemtnos temporalmente
function UploadTabDocTMPFactura(ctrlFileUpload, id, urlControllerUpload, urlControllerSetNameFile, urlControllerErrorUp) {

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: urlControllerUpload,
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    /*subio ok*/
                    $.ajax({
                        url: urlControllerSetNameFile,
                        type: 'POST',
                        data: { idDoc: id, nombre: responseData.Value },
                        success: function (response) {
                            var dato = response;
                            validarRedirect(dato); /*add sysseg*/
                            if (dato.result == 1) {
                                //loadDataDocumento();
                            } else if (dato.result == 0) {
                                alert(dato.message);
                            }
                        }
                    });
                    $("#" + ctrlFileUpload).val('');
                } else {
                    /*subio No ok*/
                    if (urlControllerErrorUp != undefined) {
                        $.ajax({
                            url: urlControllerErrorUp,
                            type: 'POST',
                            data: { id: id, },
                            success: function (response) {
                                var dato = response;
                                //  if (dato.result == 1) { alert("ok eliminado..por error al subir."); }
                                //loadDataDocumento();
                            }
                        });
                    }
                    alert(responseData.Value);
                }
            }
        } else {
            if (urlControllerErrorUp != undefined) {
                $.ajax({
                    url: urlControllerErrorUp,
                    type: 'POST',
                    data: { id: id, },
                    success: function (response) {
                        var dato = response;
                        // if (dato.result == 1) { alert("ok eliminado..por error al subir."); }
                        //loadDataDocumento();
                    }
                });
            }
            alert(responseData.Value);

        }
    });
}


function InitUploadTabDocAlfresco(ctrlFileUpload, TipoDocumento_Alfresco, TipoDocumento, CodigoLic, Artist_ID) {
    var urlControllerUploadAlfresco = K_RUTA_SITE_BASE + "/api/FileUploadAlfresco/UploadTabAlfresco";
    UploadTabDocTMPAlfresco(ctrlFileUpload, urlControllerUploadAlfresco, TipoDocumento_Alfresco, TipoDocumento, CodigoLic, Artist_ID);
}
function UploadTabDocTMPAlfresco(ctrlFileUpload, urlControllerUploadAlfresco, TipoDocumento_Alfresco, TipoDocumento, CodigoLic, Artist_ID) {
    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("CodigoCarpeta", TipoDocumento_Alfresco);
    data.append("CodigoTipoDoc", TipoDocumento);
    data.append("CodigoLic", CodigoLic);
    data.append("Artist_ID", Artist_ID);
    var ajaxRequest = $.ajax({
        type: "POST",
        async: false,
        url: urlControllerUploadAlfresco,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (responseData) {

        if (responseData != null) {

            alert(responseData.Value);

        }

    });
}

/// updaload para el tab  documentos de BEC
function InitUploadBEC(ctrlFileUpload, id, code) {

    var data = new FormData();
    var files = $("#" + ctrlFileUpload).get(0).files;
    if (files.length > 0) { data.append("UploadedImage", files[0]); }
    data.append("hidKey", id);
    var ajaxRequest = $.ajax({
        type: "POST",
        async: false,
        url: K_RUTA_SITE_BASE + "/api/FileUploadBec/UploadTabBEC",
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (responseData, textStatus) {
        if (textStatus == 'success') {
            if (responseData != null) {
                if (responseData.Key) {
                    $.ajax({                        
                        url: '../General/ActualizarNombreDoc',
                        async: false,
                        type: 'POST',
                        data: { nombre: responseData.Value, idDoc: code },
                        success: function (response) {
                            var dato = response;
                            //validarRedirect(dato); /*add sysseg*/
                            //if (dato.result == 1) {
                            //    loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
                            //} else if (dato.result == 0) {
                            //    alert(dato.message);
                            //}
                        }
                    });
                    $("#" + ctrlFileUpload).val('');
                } else {
                    alert(responseData.Value);
                }
            }
        } else {
            alert(responseData.Value);
        }
    });

}