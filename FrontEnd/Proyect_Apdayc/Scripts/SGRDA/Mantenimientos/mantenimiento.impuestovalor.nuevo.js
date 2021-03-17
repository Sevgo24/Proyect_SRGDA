/************************** INICIO CONSTANTES****************************************/
var K_MENSAJE = {
    DuplicadoCodigo: "El código ya existe, ingrese uno nuevo.",
    DuplicadoDescripcion: "La descripción ya existe, ingrese uno nuevo."
};
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_WIDTH_VAL = 420;
var K_HEIGHT_VAL = 225;
var K_TIPO_DIV = 'ADM';
/************************** INICIO CARGA ********************************************/
$(function () {
    kendo.culture('es-PE');
    $("#hidAccionMvVal").val("0");
	var id = (GetQueryStringParams("id"));
	$("#tabs").tabs();
	$("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" })
	$("#txtFechaValor").kendoDatePicker({ format: "dd/MM/yyyy" });
	
	 //---------------------------------------------------------------------------------
    if (id === undefined) {
        $('#divTituloPerfil').html("Impuestos - Nuevo");
        K_ACCION_ACTUAL = K_ACCION.Nuevo;
        loadComboTerritorio(0);
        $("#hidId").val(0);
        loadDataValores();
    } else {
    	$('#divTituloPerfil').html("Impuestos - Actualización");
        K_ACCION_ACTUAL = K_ACCION.Modificacion;
        $("#hidId").val(id);
        obtenerDatos(id);        
    }

	//-------------------------- CARGA TABS -----------------------------------------    
	//------VALORES    
	$("#mvValores").dialog({
		autoOpen: false,
		width: K_WIDTH_VAL,
		height: K_HEIGHT_VAL,
		buttons: {
		    "Agregar": addValor,
			"Cancel": function () {
				$("#mvValores").dialog("close");
			}
		},
		modal: true
	});
	$(".addvalores").on("click", function () {
	    limpiarValor();
	    var ter = $("#ddlTerritorio").val();
	    cargarDivisionXTipo('ddlDivisionFiscal', K_TIPO_DIV,0);

	    $("#hidEdicionVal").val(0);
	    $("#hidAccionMvVal").val(0);	    
		$("#mvValores").dialog("open");
	});
	//-------------------------- EVENTO CONTROLES -----------------------------  
	$("#btnDescartar").on("click", function () {
		document.location.href = '../ImpuestoValor/';
	}).button();

	$("#btnNuevo").on("click", function () {
		document.location.href = '../ImpuestoValor/Nuevo';
	}).button();

	$("#btnGrabar").on("click", function () {
		grabar();
	}).button();

	$("#tabVal").on("click", function () {
	    loadDataValores();
	});

	$('#txtValor').keypress(function (event) {
	    if (event.which != 8) {
	        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
	            event.preventDefault();
	        }

	        var text = $(this).val();
	        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2)) {
	            event.preventDefault();
	        }
	    }
	});

	$('#txtValorPorcentaje').keypress(function (event) {
	    if (event.which != 8) {
	        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
	            event.preventDefault();
	        }

	        var text = $(this).val();
	        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2)) {
	            event.preventDefault();
	        }
	    }
	});

	$('#txtValorMonto').keypress(function (event) {
	    if (event.which != 8) {
	        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
	            event.preventDefault();
	        }

	        var text = $(this).val();
	        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2)) {
	            event.preventDefault();
	        }
	    }
	});
	loadImpuestoValor("ddlValor");
});

//-------------------------- FUNCIONES ----------------------------------------- 
function validarDescripcion() {
    var descripcion = $("#txtNombre").val();
    if (descripcion != '') {
        var estadoDuplicado = validarDuplicado();
        if (!estadoDuplicado) {
            $("#txtNombre").css('border', '1px solid gray');
            msgErrorDiv("divResultValidacionDes", "");
            return true;
        } else {
            $("#txtNombre").css('border', '1px solid red');
            msgErrorDiv("divResultValidacionDes", K_MENSAJE.DuplicadoDescripcion);
            return false;
        }
    }
}

function validarDuplicado() {
    var estado = false;
    var id = '0';
    if (K_ACCION_ACTUAL == K_ACCION.Modificacion) id = $("#hidId").val();
    var impuesto = {
        TAX_ID: id,
        DESCRIPTION: $("#txtNombre").val()
    };

    $.ajax({
        url: '../ImpuestoValor/ObtenerXDescripcion',
        type: 'POST',
        dataType: 'JSON',
        data: impuesto,
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                estado = true;
            } else {
                estado = false;            
            }
        }
    });
    return estado;
}

function grabar() {
	var estadoRequeridos = ValidarRequeridos();
	var estadoDescripcion = validarDescripcion();
	if (estadoRequeridos && estadoDescripcion) {
		insertar();
	}
};

function insertar() {
	var id = '';
	if (K_ACCION_ACTUAL === K_ACCION.Modificacion) 
		id = $("#hidId").val();	
	else
		id = 0;

	var impuesto = {
		TAX_ID: id,
		TIS_N:	$("#ddlTerritorio").val(),
		TAX_COD: $("#txtAbrev").val(),
		DESCRIPTION: $("#txtNombre").val(),
		START: $("#txtFecha").val(),
		TAX_CACC:	$("#ddlCuenta").val()
	};

	$.ajax({
		url: '../ImpuestoValor/Insertar',
		data: impuesto,
		type: 'POST',
		beforeSend: function () { },
		success: function (response) {
		    var dato = response;
		    validarRedirect(dato);
			if (dato.result == 1) {
				alert(dato.message);
				document.location.href = '../ImpuestoValor/';
			} else if (dato.result == 0) {
			    alert(dato.message);
			}
		}
	});
	return false;
}

function obtenerDatos(vId) {
	$.ajax({
		url: "../ImpuestoValor/Obtener",
		type: "GET",
		data: { id: vId },
		success: function (response) {
		    var dato = response;
		    validarRedirect(dato);
			if (dato.result === 1) {
				var obj = dato.data.Data;
				$("#txtNombre").val(obj.DESCRIPTION);
				loadComboTerritorio(obj.TIS_N);
				var fecha = kendo.toString(kendo.parseDate(obj.FECHA_VIGENCIA, "dd/MM/yyyy"), "dd/MM/yyyy");
				$("#txtFecha").val(fecha);
				$("#ddlCuenta").val(obj.TAX_CACC);
				$("#txtAbrev").val(obj.TAX_COD);
				loadDataValores();
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

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

//-------------------------- TAB - VALORES ---------------------------------- 
function loadDataValores() {
    loadDataGridTmp('ListarValor', "#gridVal");
}

function addValor() {
    var idvalor = $("#ddlValor option:selected").val();
    var valor = $("#txtValor").val();
    var valor1 = "";
    var valor2 = "";

    var estadoValidacion = validacion();
    if (estadoValidacion)
    {
        if (idvalor == 1) { valor1 = valor; } else { valor2 = valor; }

        alert(valor);

        var IdAdd = 0;
        if ($("#hidAccionMvVal").val() === "1") IdAdd = $("#hidEdicionVal").val();
        var entidad = {
            Id: IdAdd,
            IdDivision: $("#ddlDivisionFiscal option:selected").val(),
            Division: $("#ddlDivisionFiscal option:selected").text(),
            ValorPorcentaje: valor2,
            ValorMonto: valor1,
            FechaVigencia: $("#txtFechaValor").val()
        };

        $.ajax({
            url: '../ImpuestoValor/AddValor',
            type: 'POST',
            data: entidad,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    loadDataValores();
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            }
        });
        $("#mvValores").dialog("close");
    }
}

function delAddvalor(idDel) {
    $.ajax({
        url: '../ImpuestoValor/DellAddValor',
        type: 'POST',
        data: { id: idDel },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                loadDataValores();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return false;
}

function updAddValor(idUpd) {
    limpiarValor();    
    $.ajax({
        url: '../ImpuestoValor/ObtieneValorTmp',
        data: { idDir: idUpd },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result === 1) {
                var obs = dato.data.Data;
                if (obs != null) {
                    $("#hidAccionMvVal").val("1");
                    $("#hidEdicionVal").val(obs.Id);
                    var ter = $("#ddlTerritorio").val();
                    cargarDivisionXTipo('ddlDivisionFiscal', K_TIPO_DIV, obs.IdDivision);

                    if (obs.ValorMonto != "") {
                        loadImpuestoValor("ddlValor", 1);
                        $("#txtValor").val(obs.ValorMonto);
                    } else {
                        loadImpuestoValor("ddlValor", 2);
                        $("#txtValor").val(obs.ValorPorcentaje);
                    }
                    $("#txtFechaValor").val(obs.FechaVigencia);
                    $("#mvValores").dialog("open");
                } else {
                    alert("No se pudo obtener el valor para editar.");
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function limpiarValor() {    
    $("#ddlDivisionFiscal").val(0);
    $("#ddlValor").val(0);
    $("#txtValor").val("");
    $("#txtFechaValor").val("");
    $("#hidAccionMvObs").val("0");
    $("#hidEdicionObs").val("0");
    $('#ddlDivisionFiscal').css({ 'border': '1px solid gray' });
    $('#ddlValor').css({ 'border': '1px solid gray' });
    $('#txtValor').css({ 'border': '1px solid gray' });
    $('#txtFechaValor').css({ 'border': '1px solid gray' });
}

function validacion() {
    var estado = true;

    if ($("#ddlDivisionFiscal").val() == 0) {
        $('#ddlDivisionFiscal').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#ddlDivisionFiscal').css({ 'border': '1px solid gray' });
    }

    if ($("#ddlValor").val() == 0) {
        $('#ddlValor').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#ddlValor').css({ 'border': '1px solid gray' });
    }

    if ($("#txtValor").val() == '') {
        $('#txtValor').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#txtValor').css({ 'border': '1px solid gray' });
    }   

    if ($("#txtFechaValor").val() == 0) {
        $('#txtFechaValor').css({ 'border': '1px solid red' });
        estado = false;
    } else {
        $('#txtFechaValor').css({ 'border': '1px solid gray' });
    }
    return estado;
}
