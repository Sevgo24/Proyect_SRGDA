
function InsertarTracesProceso(aidWrkf, idWrkf, sidWrkf, ref1Wrkf, idProc, amidWrkf, poid, reqDocEntrada, showConfirm,callBackResult) {
    var resultado=true;
    var traces = {
        aidWrkf: aidWrkf,
        idWrkf: idWrkf,
        sidWrkf: sidWrkf,
        ref1Wrkf: ref1Wrkf,
        idProc: idProc,
        amidWrkf: amidWrkf,
        oid: poid
    };
    /*si el parametro callBackResult no es undefined quiere decir que es invocado despues de subie un documento de entrada.*/
    if (reqDocEntrada) {
        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO_IN, "");
        $("#file_upload_trace").css({ 'border': '1px solid gray' });
        $("#hidSerializeTrace").val(JSON.stringify(traces));
        $("#mvDocumentoTraces").dialog("open");
    } else {
        /* si no es enviado este param, entonces siempre mostrara la confirmacion como mensaje*/
        if (showConfirm == undefined) {
            var accionText = $("#tbGridProceso").find("." + amidWrkf + ":first").text();
            if (confirm("La acción a ejecutar:  '" + accionText + "' no es reversible.\nEstá seguro de ejecutar el proceso? ")) {
                ejecutar(traces);
            }
        } else {
            /* si   es enviado este param y es verdad, entonces   mostrará la confirmacion como mensaje */
            if (showConfirm) {
                var accionText = $("#tbGridProceso").find("." + amidWrkf + ":first").text();
                if (confirm("La acción a ejecutar:  '" + accionText + "' no es reversible.\nEstá seguro de ejecutar el proceso? ")) {
                    ejecutar(traces);
                }
            } else {
                /* si   es enviado este param y es false, entonces NO mostrara la confirmacion como mensaje
                   sólo no ocurre el else cuando la acccion carga previamente un documento de entrada.
                */
                ejecutar(traces, function (result) { if(callBackResult!=undefined) callBackResult(result); });
            }
        }
    }
   
}

function ejecutar(traces, callBackExito) {


    var resultado = true;
    $.ajax({
        data: traces,
        url: "../Action/EjecutarProceso",
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result > 0) {
                if (dato.result != 2) {   /* si no ha finalizado la sesion*/
                    var estado = dato.valor;
                    var objeto = dato.data.Data;
                    if (dato.result == 1) {
                        if (objeto.ObjectType == "DOCOUT") {
                            if (traces.oid != "0") {
                                generarFormato(traces.oid, dato.Code, traces.ref1Wrkf, function (response) {
                                    if (response == true)ObtenerLicencia(traces.ref1Wrkf);
                                });
                                //abrir_ventana('../Formatos/GenerarFormato/?idObj=' + traces.oid + "&idTrace=" + dato.Code + "&idRef=" + traces.ref1Wrkf, "popupPDF", 600, 550);
                            }
                        } else {
                            //$("#ddlEstadoLicencia").val(estado);
                            ObtenerLicencia(traces.ref1Wrkf);
                        }
                        resultado = true;
                    } else {
                        alert(dato.message);
                        resultado = false;
                    }
                }
            } else if (dato.result == 0) {
                alert(dato.message);
                resultado = false;
            }

            if(callBackExito!=  undefined)callBackExito(resultado);
        }
    });
    return resultado;

}
function addDocumentoTrace() {

    msgErrorB("avisoProcUpload", "");
    var IdAdd = 0;
    
    $("#file_upload_trace").addClass("requerido");
 
    if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DOCUMENTO_IN, K_DIV_POPUP.DOCUMENTO_IN)) {
        var documento = {
            codLic: $(K_HID_KEYS.LICENCIA).val(),
            Id: IdAdd,
            TipoDocumento: $("#ddlTipoDocumentoTrace option:selected").val(),
            Archivo: $("#hidNombreFileTrace").val(),
            UsuarioActual: "ADMIN"
        };
        InitUploadTrace("file_upload_trace", documento, function (responseUpload) {
            if (responseUpload.Key) {
                var param = $("#hidSerializeTrace").val();
                if (param != "") {
                    var objTrace = JSON.parse(param);
                    var showConfirm = false;
                    InsertarTracesProceso(objTrace.aidWrkf,
                        objTrace.idWrkf,
                        objTrace.sidWrkf,
                        objTrace.ref1Wrkf,
                        objTrace.idProc,
                        objTrace.amidWrkf,
                        objTrace.oid, 0, showConfirm,
                        function (resultadoEjec) {
                            /*si la ejecucion del proceso hubo algun error o validacion; eliminar archivo uploaded y registro de tabla*/
                            if (!(resultadoEjec)) {
                                $.ajax({
                                    url: 'EliminarDocumento',
                                    type: 'POST',
                                    data: { idDoc: responseUpload.Value },
                                    beforeSend: function () { },
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
                            } else {
                                /*EJECUCION DE DOC DE ENTRADA CORRECTO.. ACTUALIZAR TAB DOCUMENTOS*/
                                loadDataDocumento($(K_HID_KEYS.LICENCIA).val());
                            }

                        });

                   
                    $("#mvDocumentoTraces").dialog("close");
                    $("#file_upload_trace").removeClass("requerido");
                }
            } else {
                msgErrorB("avisoProcUpload", responseUpload.Value);
                $("#file_upload_trace").removeClass("requerido");
            }
        });
        $("#file_upload_trace").removeClass("requerido");
    }
     
}

function resultadoTrace(result) {
    alert(result);
    return false;
}
var generarFormato = function (idObj, idTrace, idRef, callback) {
    var retorno=false;
    $.ajax({
        data: { idObj: idObj, idTrace: idTrace, idRef: idRef },
        url: "../Formatos/GenerarFormatoJSON",
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1 || dato.result == 0) {                
                setPopupProcessPDF(dato.result, dato.message);
                if (dato.result == 1) retorno = true;
            }
            callback(retorno);
        }
    });
};

var setPopupProcessPDF = function (result, message) {
    $("#lblResultGenPDF").html('');
    if (result == 1) {
        var url = message;
        $("#lblResultGenPDF").attr("display", "none");
        $("#ifContenedorFormato").attr("display", "inline");
        $("#ifContenedorFormato").attr("src", url);
        //$("#ifContenedorFormato").attr("src", 'C:/inetpub/wwwroot/Documentos/20151223144111_Omiso248H.pdf');
    } else {
        $("#lblResultGenPDF").attr("display", "inline");
        $("#ifContenedorFormato").attr("display", "none");
        $('#ifContenedorFormato').attr('src', '');
        $("#lblResultGenPDF").html(message);
    }
    $("#mvEjecutarProceso").dialog("open");
};