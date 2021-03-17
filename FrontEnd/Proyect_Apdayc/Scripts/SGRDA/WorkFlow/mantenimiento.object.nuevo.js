/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_TIPO = { Nuevo: "I", Modificacion: "U" };
/************************** INICIO CARGA********************************************/
$(function () {
    var id = (GetQueryStringParams("id"));
    $("#txtopath").hide();
    $("#txtNombre").focus();
    $('#txtCodInterno').on("keypress", function (e) { return solonumeros(e); });
    //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Workflow - Objeto / Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        $("#hidId").val(0);
        $("#trId").hide();
        LoadTipoObjeto("ddlTipo");
        $("#VerImagen").hide();
        $("#email").hide();
        $("#asunto").hide();
    } else {
        $('#divTituloPerfil').html("Workflow - Objeto / Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#txtCodigo").prop('disabled', true);
        $("#hidId").val(id);
        obtenerDatos(id);
    }

    $("#mvImagen").dialog({ autoOpen: false, width: 800, height: 550, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });

    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnDescartar").on("click", function () {
        document.location.href = '../Objects/';
    });

    $("#btnNuevo").on("click", function () {
        document.location.href = '../Objects/Nuevo';
    });

    $("#btnGrabar").on("click", function () {
        grabar();
    });

    $("#ddlTipo").on("change", function () {
        $("#txtAsunto").removeAttr("class", "requerido");
        var tipo = $("#ddlTipo").val();
        $("#hidIsDocIn").val("");
        $.ajax({
            url: "../Objects/SeleccionarTipo",
            type: "GET",
            data: { id: tipo },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                //Correo electrónico a Usuarios y Terceros
                if (dato.result == 3) {
                    $("#asunto").show();
                    $("#trinterno").hide();
                    $("#txtAsunto").attr("class", "requerido");
                    $("#txtCodInterno").removeAttr("class", "requerido");
                    $("#ruta").show();
                    $("#fuFilesUploader").attr("class", "VerUP");
                    $("#VerImagen").hide();

                    //hidTipoObjetoPref
                }
                    // Documento de Entrada
                else if (dato.result == 4) {
                    $("#ruta").hide();
                    $("#fuFilesUploader").attr("class", "ocultarUP");
                    $("#asunto").hide();
                    $("#trinterno").hide();
                    $("#txtCodInterno").removeAttr("class", "requerido");
                    $("#txtAsunto").removeAttr("class", "requerido");
                    $("#hidIsDocIn").val("1");
                    //hidTipoObjetoPref
                }
                    //Otros
                else if (dato.result == 5) {
                    $("#txtAsunto").val("");
                    $("#txtCodInterno").attr("class", "requerido");
                    $("#txtAsunto").removeAttr("class", "requerido");
                    $("#asunto").hide();
                    $("#trinterno").show();
                    $("#ruta").show();
                    $("#fuFilesUploader").attr("class", "VerUP");
                    $("#VerImagen").hide();
                }
            }
        });
    });

    $("#VerImagen").on("click", function () {
        var id = (GetQueryStringParams("id"));
        verImagen(id);
    });

    $('#fuFiles').uploadify({
        'uploader': '../Scripts/Extensiones/_scripts/uploadify.swf',
        'script': 'Upload',
        'cancelImg': '../Scripts/Extensiones/_scripts/cancel.png',
        'auto': false,
        'multi': false,
        'buttonText': 'Adjuntar archivo',
        'queueSizeLimit': 1,
        'simUploadLimit': 2,
        'sizeLimit': 800 * 1024,
        'fileDesc': 'Tipo de imágenes permitidas (.DOC, .DOCX, .TXT)',
        'fileExt': '*.doc;*.docx;*.txt',

        'onError': function (a, b, c, d) {
            if (d.status == 404)
                alert("Could not find upload script. Use a path relative to: " + "<?= getcwd() ?>");
            else if (d.type === "HTTP")
                alert("error " + d.type + ": " + d.status);
            else if (d.type === "File Size")
                alert(c.name + " " + d.type + " Limit: " + Math.round(d.info / (1024 * 1024)) + "MB");
            else
                alert("error " + d.type + ": " + d.text);
        }
    });
});

//****************************  FUNCIONES ****************************
function grabar() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) 
        insertarObjeto();      
};

function insertarObjeto() {
    var archivo = $("#txtopath").val();
    var cuenta = 0;
    if ($("#hidIsDocIn").val() != "1") {
        cuenta = $('#fuFiles').uploadifySettings('queueSize');
    }

    var tipo = $("#ddlTipo").val();
    var asunto = $("#txtAsunto").val();
    var id = 0;

    if (archivo == "") { archivo = "file" }
    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

    var objeto = {
        Id: id,
        CodigoInterno: $("#txtCodInterno").val(),
        Descripcion: $("#txtNombre").val(),
        Tipo: tipo,
        Archivo: archivo,
        Asunto: asunto
    };

    InitUploadObjeto("file_upload", objeto, function (responseUpload) {
        if (responseUpload.Key) {
            alert('Se registro correctamente.');
            location.href = "../Objects/Index";
        }
    });
};

function Insertar() {
    var objeto = {
        WRKF_OID: id,
        WRKF_OINTID: $("#txtCodInterno").val(),
        WRKF_ODESC: $("#txtNombre").val(),
        WRKF_OTID: tipo,
        WRKF_OPATH: archivo,
        WRKF_OSUBJECT: asunto
    };

    $.ajax({
        url: '../Objects/Insertar',
        type: 'POST',
        data: objeto,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
                location.href = "../Objects/Index";
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

}
function obtenerDatos(idOrigen) {
    $("#hidIsDocIn").val("");
    $.ajax({
        url: "../Objects/Obtener",
        type: "GET",
        data: { id: idOrigen },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            //Correo electrónico a Usuarios y Terceros
            if (dato.result === 3) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_OID);
                $("#txtNombre").val(obj.WRKF_ODESC);
                $("#txtCodInterno").val(obj.WRKF_OINTID);
                LoadTipoObjeto("ddlTipo", obj.WRKF_OTID);
                $("#txtopath").val(obj.WRKF_OPATH);
                $("#File").show();
                $("#asunto").show();
                $("#trinterno").hide();
                $("#txtAsunto").val(obj.WRKF_OSUBJECT);
                $("#txtAsunto").attr("class", "requerido");
                if (obj.WRKF_OPATH == null || obj.WRKF_OPATH == "") $("#VerImagen").hide(); else { $("#VerImagen").show(); }
            }
                // Documento de Entrada
            else if (dato.result === 4) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_OID);
                $("#txtNombre").val(obj.WRKF_ODESC);
                $("#trinterno").hide();
                $("#txtCodInterno").val("");
                LoadTipoObjeto("ddlTipo", obj.WRKF_OTID);
                $("#txtopath").val(obj.WRKF_OPATH);
                $("#File").show();
                $("#asunto").hide();
                $("#hidIsDocIn").val("1");
                if (obj.WRKF_OPATH == null || obj.WRKF_OPATH == "") $("#VerImagen").hide(); else { $("#VerImagen").show(); }
            }
                //Otros
            else if (dato.result == 5) {
                var obj = dato.data.Data;
                $("#txtCodigo").val(obj.WRKF_OID);
                $("#txtNombre").val(obj.WRKF_ODESC);
                $("#txtCodInterno").val(obj.WRKF_OINTID);
                $("#txtCodInterno").attr("class", "requerido");
                LoadTipoObjeto("ddlTipo", obj.WRKF_OTID);
                $("#txtopath").val(obj.WRKF_OPATH);
                $("#asunto").hide();
                $("#File").show();
                if (obj.WRKF_OPATH == null || obj.WRKF_OPATH == "") $("#VerImagen").hide(); else { $("#VerImagen").show(); }
            }
            else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}


function verImagen(url) {
    $("#mvImagen").dialog("open");
    $("#ifContenedor").attr("src", url);
    return false;
}


//function insertar() {

//    var archivo = $("#txtopath").val();
//    var cuenta = 0;
//    if ($("#hidIsDocIn").val() != "1") {
//        cuenta = $('#fuFiles').uploadifySettings('queueSize');
//    }
//    var tipo = $("#ddlTipo").val();
//    var asunto = $("#txtAsunto").val();
//    var id = 0;

//    if (archivo == "") { archivo = "file" }
//    if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();

//    var objeto = {
//        WRKF_OID: id,
//        WRKF_OINTID: $("#txtCodInterno").val(),
//        WRKF_ODESC: $("#txtNombre").val(),
//        WRKF_OTID: tipo,
//        WRKF_OPATH: archivo,
//        WRKF_OSUBJECT: asunto
//    };

//    if (id == 0) {
//        if (cuenta > 0) {
//            $('#fuFiles').uploadifySettings('scriptData', objeto);
//            $('#fuFiles').uploadifyUpload();
//            alert("Registro guardado correctamente.");
//            return document.location.href = '../Objects/';
//        } else {
//            $.ajax({
//                url: '../Objects/Insertar',
//                type: 'POST',
//                data: objeto,
//                beforeSend: function () { },
//                success: function (response) {
//                    var dato = response;
//                    validarRedirect(dato);
//                    if (dato.result == 1) {
//                        alert(dato.message);
//                        location.href = "../Objects/Index";
//                    } else if (dato.result == 0) {
//                        alert(dato.message);
//                    }
//                }
//            });
//        }
//    }
//    else {
//        if (cuenta > 0) {
//            $('#fuFiles').uploadifySettings('scriptData', objeto);
//            $('#fuFiles').uploadifyUpload();
//            alert("Registro guardado correctamente.");
//            return document.location.href = '../Objects/';
//        } else {
//            $.ajax({
//                url: '../Objects/Insertar',
//                type: 'POST',
//                data: objeto,
//                beforeSend: function () { },
//                success: function (response) {
//                    var dato = response;
//                    validarRedirect(dato);
//                    if (dato.result == 1) {
//                        alert(dato.message);
//                        location.href = "../Objects/Index";
//                    } else if (dato.result == 0) {
//                        alert(dato.message);
//                    }
//                }
//            });
//        }
//    }
//};