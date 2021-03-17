/*INICIO CONSTANTES*/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_PER = 400;
var K_HEIGHT_PER = 250;
//var K_HEIGHT_PER = 450;
var eventoKP = "keypress";

/************************** INICIO CARGA********************************************/
$(function () {
    var id = GetQueryStringParams("id");    
	$('#txtNroOrden').on(eventoKP, function (e) { return solonumeros(e); });
	$('#txtNrodias').on(eventoKP, function (e) { return solonumeros(e); });
	$("#hidAccionMvPe").val("0");	
	$("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });
	
    //---------------------------------------------------------------------------------
	if (id != null) {
		$("#divTitulo").html("Periodicidad Tarifa / Actualización");
		$("#hidAccionMvPe").val("1");
		$("#hidId").val(id);
		$("#hidOpcionEdit").val(1);
		ObtenerDatos(id);
	}
	else {
	    $("#hidOpcionEdit").val(0);
	    $("#hidId").val(0);
	    $("#divTitulo").html("Periodicidad Tarifa / Nuevo");
	    loadDataPeriodo();
	}

    //-------------------------- CARGA TABS -----------------------------------------    
	$("#tabs").tabs();
    //------ PERIODO		
	$("#mvPeriodo").dialog({
	    autoOpen: false,
	    width: K_WIDTH_PER,
	    height: K_HEIGHT_PER,
	    buttons: {
	        "Agregar": addPeriodo,
	        "Cancel": function () { $("#mvPeriodo").dialog("close"); $('#txtNroOrden').css({ 'border': '1px solid gray' }); }
	    }, modal: true
	});
	$(".addPeriodo").on("click", function () { $("#mvPeriodo").dialog({}); $("#hidEdicionPe").val($("#hidId").val()); $("#hidAccionMvPe").val("0"); $("#hidNroordenPeriodoAnt").val("0"); $("#mvPeriodo").dialog("open"); limpiarPeriodo(); });

    //-------------------------- EVENTO CONTROLES ------------------------------------    
    $("#btnVolver").on("click", function () {
        document.location.href = '../PERIODICIDADTARIFA/';
	}).button();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../PERIODICIDADTARIFA/Create';
	}).button();

	$("#btnGrabar").on("click", function () {
	    grabar();
	}).button();

	$("#txtNombre").focus();	
});

//****************************  FUNCIONES ****************************
function limpiarPeriodo() {
	$('#txtNroOrden').css({ 'border': '1px solid gray' });
	$('#txtNombrePeriodo').css({ 'border': '1px solid gray' });
	$('#txtNrodias').css({ 'border': '1px solid gray' });
	$('#txtFecha').css({ 'border': '1px solid gray' });
	$("#txtNroOrden").val("");
	$("#txtNombrePeriodo").val("");
	$("#txtNrodias").val("");
	$("#txtFecha").val("");	
}

function loadDataPeriodo() {
	loadDataGridTmp('ListarPeriodos', "#gridPeriodo");
}

function loadDataGridTmp(Controller, idGrilla) {
	$.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function ObtenerDatos(idSel) {
	$.ajax({
		url: '../PERIODICIDADTARIFA/Obtiene',
		data: { id: idSel },
		type: 'POST',
		success: function (response) {
			var dato = response;

			if (dato.result == 1) {
				var sucursal = dato.data.Data;
				if (sucursal != null) {
					$("#hidId").val(sucursal.Id);
					$("#txtIdPeriodo").val(sucursal.Id);
					$("#txtNombre").val(sucursal.Descripcion);
				}
				loadDataPeriodo();
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

function addPeriodo() {
	if ($("#txtNroOrden").val() == '') {
		$('#txtNroOrden').css({ 'border': '1px solid red' });
	}
	if ($("#txtNombrePeriodo").val() == '') {
		$('#txtNombrePeriodo').css({ 'border': '1px solid red' });
	}
	if ($("#txtNrodias").val() == '') {
		$('#txtNrodias').css({ 'border': '1px solid red' });
	}
	if ($("#txtFecha").val() == '') {
		$('#txtFecha').css({ 'border': '1px solid red' });
	}

	else {
	    var IdAdd = $("#hidId").val();
		//var IdAdd = 0;
		//if ($("#hidAccionMvPe").val() === "1") IdAdd = $("#hidEdicionPe").val();

		var entidad = {
			Id: IdAdd,
			NroordenPeriodo: $("#txtNroOrden").val(),
			NombrePeriodo: $("#txtNombrePeriodo").val().toUpperCase(),
			NrodiasPeriodo: $("#txtNrodias").val(),
			Fecha: $("#txtFecha").val(),
			Accion: $("#hidAccionMvPe").val(),
			NroordenPeriodoAnt:$('#hidNroordenPeriodoAnt').val()
		};

		$.ajax({
			url: 'AddPeriodo',
			type: 'POST',
			data: entidad,
			beforeSend: function () { },
			success: function (response) {
			    var dato = response;
			    validarRedirect(dato);
				if (dato.result == 1) {
				    loadDataPeriodo();
				} else if (dato.result == 0) {
				    alert(dato.message);
				}
			}
		});
		$("#mvPeriodo").dialog("close");
		$('#txtNroOrden').css({ 'border': '1px solid gray' });
		$('#txtNombrePeriodo').css({ 'border': '1px solid gray' });
		$('#txtNrodias').css({ 'border': '1px solid gray' });
		$('#txtFecha').css({ 'border': '1px solid gray' });
	}
}

function delAddPeriodo(id, idOrden) {
	$.ajax({
		url: 'DellAddPeriodo',
		type: 'POST',
		data: { id: idOrden },
		beforeSend: function () { },
		success: function (response) {
		    var dato = response;
		    validarRedirect(dato);
			if (dato.result == 1) {
				loadDataPeriodo();
			} else if (dato.result == 0) {
			    alert(dato.message);
			}
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status);
			alert(thrownError);
		}
	});
	return false;
}

function updAddPeriodo(idUpd, idorden) {
	limpiarPeriodo();

	$.ajax({
		url: 'ObtienePeriodoTmp',
		data: { idPer: idUpd, NroOrden: idorden },
		type: 'POST',
		success: function (response) {
		    var dato = response;
		    validarRedirect(dato);
			if (dato.result === 1) {
				var param = dato.data.Data;
				if (param != null) {
				    var d1 = $("#txtFecha").data("kendoDatePicker");
				    var valFecha = formatJSONDate(param.Fecha);
				    $("#hidAccionMvPe").val("1");
				    $("#hidEdicionPe").val(param.Id);
				    $("#hidNroordenPeriodoAnt").val(param.NroordenPeriodo);
					$("#txtNroOrden").val(param.NroordenPeriodo);
					$("#txtNombrePeriodo").val(param.NombrePeriodo );
					$("#txtNrodias").val(param.NrodiasPeriodo);

					
					$("#hidId").val(param.Id);
					d1.value(valFecha);
					$("#mvPeriodo").dialog("open");
				} else {
					alert("No se pudo obtener el periodo para editar.");
				}
			} else if (dato.result == 0) {
			    alert(dato.message);
			}
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

function grabar() {
	if (ValidarRequeridos()) {
		var id = 0;
		var val = $("#hidOpcionEdit").val();
		if (val == 1) {
			if (K_ACCION_ACTUAL === K_ACCION.Modificacion) id = $("#hidId").val();
		}
		else
			id = $("#txtIdPeriodo").val();

		var periodo = {
			RAT_FID: $("#txtIdPeriodo").val(),
			RAT_FDESC: $("#txtNombre").val()
		};
		$.ajax({
			url: 'Insertar',
			data: periodo,
			type: 'POST',
			beforeSend: function () { },
			success: function (response) {
			    var dato = response;
			    validarRedirect(dato);
			    if (dato.result == 1) {
			        alert(dato.message);
				    location.href = "../PERIODICIDADTARIFA/";					
				} else if (dato.result == 0) {
				    alert(dato.message);
				}
			}
		});
	}
	return false;
}