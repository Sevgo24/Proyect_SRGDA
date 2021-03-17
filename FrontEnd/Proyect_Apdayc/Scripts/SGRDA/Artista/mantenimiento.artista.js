var ARTISTA ={
    EQUIPO_SONIDO: "200393",
    NOM_EQUIPO_SONIDO: "EQUIPO DE SONIDO"
}
var K_WIDTH_OBS2_DOC = 600;
var K_HEIGHT_OBS2_DOC = 325;
var K_ES_SOLICITUD_DOCUMENTO_PENDIENTE_CANCELADO = 7;
var K_ES_MODIF_DOC_MANUAL = 3;
var K_ES_MODIF_DOC_PEND_CANC = 4;
var K_MODIFICAR_DOCUMENTO_MANUAL = 6;


$(function () {

    //$("#mvAdvertencia").dialog({
    //    close: function (event) {
    //        if (event.which) { returnPage(); }
    //    }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    //});
    $("#mvActualizaSGS").dialog({ autoOpen: false, width: 300, height: 300, buttons: { "Grabar": addSGS, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $('#txtcodlic').on("keypress", function (e) { return solonumeros(e); });

   



    var _cuenta = 1; /*addon dbs  20150831- Primera carga los datos */
    limpiar();
    loadData();

    $("#btnBuscar").on("click", function () {

        loadData();
    });
    $("#btnLimpiar").on("click", function () {

        limpiar();
        loadData();
    });

});

function limpiar() {
    $("#txtnombreartista").val("");
    $("#hidcodart").val("");
    $("#txtcodlic").val("");
    $("#txtshow").val("");
    //loadData();

}

function insertarArtista(){

    var inactiva = ValidarLicenciaValorizada();
    //var Observacion = $("#txtObservacion").text();
    var Observacion = document.getElementById("txtObservacion").value;

    var tipo=0;
    if (!inactiva){
         tipo=1;
    }else{
         tipo=2;
    }
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_ARTISTA, K_DIV_POPUP.ARTISTA)) {
            var artista = {
                CodigoShow: $("#hidShowSel").val(),
                CodigoArtista: $("#hidArtistaSel").val() != "" ? $("#hidArtistaSel").val() : "0",
                Principal: $("#chkPrincipal").prop("checked") == true ? "1" : "0",
                NombreArtista: $("#lblArtista").text() != "" ? $("#lblArtista").text() : "",
                Tipo:tipo,
                Observacion: Observacion
            };
            //alert($("#hidArtistaSel").val());
            $.ajax({
                url: '../Artista/Insertar',
                type: 'POST',
                data: artista,
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato); /*add sysseg*/
                    if (dato.result == 1) {
                        listarArtista(artista.CodigoShow);
                        ValidaModalidad($("#hidLicId").val(), dato.Code);
                        $("#" + K_DIV_POPUP.ARTISTA).dialog("close");
                        if (inactiva) {
                            $("#mvObservacionArtista").dialog("close");
                        }
                        
                        //alert(dato.message);
                    } else if (dato.result == 2) {
                        $("#" + K_DIV_POPUP.ARTISTA).dialog("close");                
                       alert("EL ARTISTA SELECCIONADO YA SE ENCUENTRA REGISTRADO EN EL SHOW ,SELECCIONE OTRO ARTISTA");                     
                        
                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_ARTISTA, dato.message);
                    }
                }
            });
        }
    }

function listarArtista(idShow) {
	$.ajax({
		url: '../Artista/ListarArtistaHtml',
		type: 'POST',
		data: { CodigoShow: idShow },
		beforeSend: function () { },
		success: function (response) {
			var dato = response;
			validarRedirect(dato); /*add sysseg*/
			if (dato.result == 1) {
				$("#divArtista_" + idShow).html(dato.message);
			} else if (dato.result == 0) {
				alert(dato.message);
			}
		}
	});
}

function addArtista(idShow) {
    $("#txtObservacionArtista").val('');
    var inactiva = ValidarLicenciaValorizada();
    if (!inactiva) {
    var resp = ValidaModGrabada(idShow);
    if (resp) {
        limpiarArtista();
        obtenerNombreShow(idShow, "lblShowSel");
        $("#hidAccionMvArtista").val(K_ACCION.Nuevo);
        $("#hidShowSel").val(idShow); //hidShowSel        
        $("#" + K_DIV_POPUP.ARTISTA).dialog("option", "title", "Registro de Nuevo Artista.");
        $("#" + K_DIV_POPUP.ARTISTA).dialog("open");
    } else {
        alert("Insertando Automaticamente MEDIO MECANICO");
    }
    } else {
        alert("Se enviara una solicitud para agregar el artista debido a que la licencia ya se encuentra Valorizada.");
        var resp = ValidaModGrabada(idShow);
        if (resp) {
            limpiarArtista();
            obtenerNombreShow(idShow, "lblShowSel");
           
            $("#mvObservacionArtista").dialog({
                autoOpen: true,
                width: 300,
                height: 300,
                buttons: {
                    "Registrar": function () {                       
                        $("#hidAccionMvArtista").val(K_ACCION.Nuevo);
                        $("#hidShowSel").val(idShow); //hidShowSel
                        $("#" + K_DIV_POPUP.ARTISTA).dialog("option", "title", "Registro de Nuevo Artista.");
                        $("#" + K_DIV_POPUP.ARTISTA).dialog("open");
                        $("#mvObservacionArtista").dialog("close");
                    },
                    "Cancelar": function () {
                        $("#mvObservacionArtista").dialog("close");
                    }
                },
                modal: true
            });
            $("#mvObservacionArtista").dialog("open");
        } else {
            alert("Insertando Automaticamente MEDIO MECANICO");
        }
    }
}

function limpiarArtista() {
	msgErrorB(K_DIV_MESSAGE.DIV_TAB_POPUP_ARTISTA, "");
	$("#hidAccionMvArtista").val("");
	$("#hidIdArtistaEditar").val("");
	$("#hidIdShowArtista").val("");
	//$("#txtShowNombre").val("");
	//$("#txtShowFecIni").val("");
	//$("#txtShowFecFin").val("");
	//$("#txtShowObserv").val("");
	//$("#txtOrden").val("0");

}

function verArtistas(id) {
	if ($("#expandArtista" + id).attr('src') == '../Images/iconos/minus.png') {
		$("#expandArtista" + id).attr('src', '../Images/iconos/plus.png');
		$("#expandArtista" + id).attr('title', 'Ver Artistas del Shows. ');
		$("#expandArtista" + id).attr('alt', 'Ver Artistas del Shows. ');
		$("#divArtista_" + id).css("display", "none");
		$("#tdArtista_" + id).css("background", "transparent");

	} else {
		$("#expandArtista" + id).attr('src', '../Images/iconos/minus.png');
		$("#expandArtista" + id).attr('title', 'Ocultar Artistas.');
		$("#expandArtista" + id).attr('alt', 'Ocultar Artistas.');
		listarArtista(id);
		$("#divArtista_" + id).css("display", "inline");
		$("#tdArtista_" + id).css("background", "#dbdbde");
	}

}

function delArtista(idDel, esActivo, idShow,artist_id) {
    var inactiva = ValidarLicenciaValorizada();

    if (!inactiva) {//si es verdad entonces es por que no tiene ninguna factura cancelada
        $.ajax({
            url: '../Artista/Eliminar',
            type: 'POST',
            data: { id: idDel, EsActivo: esActivo, SHOW_ID: idShow },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    listarArtista(idShow);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;

    } else {
        Solicitud_Eliminar_Y_Activar_Artista(idDel, esActivo, idShow, artist_id,1);
    }
}

function Solicitud_Eliminar_Y_Activar_Artista(idDel, esActivo, idShow, artist_id, tipo) {

    alert("No puede Inactivarse a una Licencia Valorizada. Se enviara el requerimiento automaticamente.Favor de agregar una observacion.");
    //var Observacion = $("#txtObservacion").text();
    $("#txtObservacionArtista").val('');
    //var Observacion =  $("#txtObservacion").val();
    //var Observacion = document.getElementById("txtObservacion").value;
    $("#mvObservacionArtista").dialog({
        autoOpen: true,
        width: 300,
        height: 300,
        buttons: {              
            "Registrar":
                 function () {
                     InsertarSolicitud(idDel, esActivo, idShow, tipo, artist_id)
                 }
            ,
            "Cancelar": function () {
                $("#mvObservacionArtista").dialog("close");
            }
        },
        modal: true
    });
    //$("#mvObservacionArtista").dialog("open");
    return false;
}

function InsertarSolicitud(idDel, esActivo, idShow, tipo, artist_id) {

    var Observacion = $("#txtObservacionArtista").val();
        $.ajax({
            url: '../Artista/Solicitud_Eliminar_Activar',
            type: 'POST',
            async: false,
            data: { id: idDel, EsActivo: esActivo, SHOW_ID: idShow, Tipo: tipo, Artist_ID: artist_id, Observacion: Observacion },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    $("#mvObservacionArtista").dialog("close");
                    listarArtista(idShow);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
}


function ActArtista(idDel, esActivo, idShow, artist_id) {
    var inactiva = ValidarLicenciaValorizada();
    if (!inactiva) {//si es verdad entonces es por que no tiene ninguna factura cancelada
        $.ajax({
            url: '../Artista/Eliminar',
            type: 'POST',
            data: { id: idDel, EsActivo: esActivo, SHOW_ID: idShow },
            success: function (response) {
                var dato = response;
                validarRedirect(dato); /*add sysseg*/
                if (dato.result == 1) {
                    listarArtista(idShow);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        return false;

    } else {
        Solicitud_Eliminar_Y_Activar_Artista(idDel, esActivo, idShow, artist_id, 2);
    }
}

function darPrioridad(id, idShow) {
	$.ajax({
		url: '../Artista/Prioridad',
		type: 'POST',
		data: { id: id },
		success: function (response) {
			var dato = response;
			validarRedirect(dato); /*add sysseg*/
			if (dato.result == 1) {
				listarArtista(idShow);
			} else if (dato.result == 0) {
				alert(dato.message);
			}
		}
	});
}

var reloadEventoArt = function (idArtSel) {
    msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    $("#hidArtistaSel").val(idArtSel);
    obtenerNombreArtista(idArtSel, "lblArtista");
};

function loadData() {
    var texto = $("#txtnombreartista").val() != "" ? $("#txtnombreartista").val() : "";
    var licencia = $("#txtcodlic").val() != "" ? $("#txtcodlic").val() : 0;
    var show = $("#txtshow").val() != "" ? $("#txtshow").val() : "";

    var sharedDataSource = new kendo.data.DataSource({
        serverPaging: true,
        pageSize: 15,
        type: "json",
        transport: {
            read: {
                type: "POST",
                url: "../Artista/ListarArtistaSinCodSGS",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, { nombre: texto, COD_LIC: licencia, SHOW_NAME: show })
            }
        },
        schema: { data: "listaArtista", total: 'TotalVirtual' }
    });

    $("#grid").kendoGrid({
        dataSource: sharedDataSource,
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: "multiple row",
        columns: [
                //{ title: 'Eliminar', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${ARTIST_ID}'/>" },
                { field: "CODIGO ARTISTA", width: 4, title: "CODIGO ARTISTA", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ARTIST_ID}</a>" },
                { field: "NOMBRE DEL ARTISTA", width: 4, title: "NOMBRE DEL ARTISTA", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${NAME}</a>" },
                { field: "SHOW", width: 4, title: "SHOW", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${SHOW_NAME}</a>" },
                { field: "CODIGO DE LICENCIA", width: 4, title: "CODIGO DE LICENCIA", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
                { field: "USUARIO DE CREACION", width: 4, title: "USUARIO DE CREACION", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a>" },
                { field: "FECHA DE CREACION",width: 4, title: "FECHA DE CREACION", template: "<a id='single_2'  href=javascript:editar('${ARTIST_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LOG_DATE_CREAT}</a>" },
				//{ field: "ADD_ID", hidden: true, width: 20, title: "ADD_ID", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}') style='color:gray;text-decoration:none;font-size:11px'>${ADD_ID}</a>" },
        ]
    });
};

function editar(id) {
    limpiarBuscadorPopUp();
    $("#mvActualizaSGS").dialog("open");
     obtenerNombreArt(id, "lblartSGS");
    $("#hidcodart").val(id);
}

function addSGS() {
    var codigosgs = $("#txtartSGS").val();
    var codigoartist = $("#hidcodart").val();

    $.ajax({
        url: '../Artista/ActualizaArtistaSGS',
        type: 'POST',
        data: { codsgs: codigosgs, codartist: codigoartist },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                limpiar();
                $("#mvActualizaSGS").dialog("close");
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function obtenerNombreArt(idArtita, idLabelSetting) {
    $.ajax({
        data: { id: idArtita },
        url: '../Artista/ObtenerNombreArtistaSQL',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) $("#" + idLabelSetting).html(dato.valor);
            else if (dato.result == 0) alert(dato.message);

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }

    });
}

function limpiarBuscadorPopUp() {
    $("#lblartSGS").val("");
    $("#txtartSGS").val("");
}

function ValidaModalidad(cod_lic ,codigo_artista) {
    //alert(cod_lic + ',' + codigo_artista);
    var inactiva = ValidarLicenciaValorizada();
    var lic_id = $("#hidLicId").val();
    $.ajax({
        data: { LIC_ID: lic_id, ARTIST_ID: codigo_artista },
        url: '../Artista/InsertaPlanillaxArtista',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1)
                if (!inactiva) {
                    alert("Se inserto Correctamente");
                } else {
                    alert("Se inserto Correctamentes ,el artista seleccionadao se encuentra pendiente de confirmacion.");
                }
                //alert("Se inserto Correctamente");
            //else
            //    alert("No es baile");

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }

    });

}

function ValidaModGrabada(show_id) {

    var LIC_ID = $("#hidLicId").val();
    var res = true;
    $.ajax({
        data: { LIC_ID: LIC_ID, SHOW_ID: show_id },
        url: '../Artista/ValidaModalidadVivo_Grabada',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 2) {
                res = false; //DEBE DE INSERTAR AUTOMATICAMENTE 
                $("#hidArtistaSel").val(ARTISTA.EQUIPO_SONIDO);
                $("#chkPrincipal").prop('checked',true);
                $("#lblArtista").text(ARTISTA.NOM_EQUIPO_SONIDO);
                $("#hidShowSel").val(show_id);

                insertarArtista();
            }else if(dato.result == 1)
                res = false;
            
            //else
            //    alert("No es baile");

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }

    });


    return res;
}

function ValidarLicenciaValorizada() {
    var lic_id = $("#hidLicId").val();
    var retorno = false;
    $.ajax({
        data: { LIC_ID: lic_id },
        url: "../Reporte/ValidaLicenciaFactValorizada",
        type: 'POST',
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                retorno = true;
            }
            else {
                retorno = false;
            }
        }
    });

    return retorno;
}