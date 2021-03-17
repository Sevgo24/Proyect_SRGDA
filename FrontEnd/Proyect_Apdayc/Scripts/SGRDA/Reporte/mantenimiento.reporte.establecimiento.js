/************************** INICIO CARGA********************************************/
var INEI = 1
$(function () {

	kendo.culture('es-PE');
	//-------------------------- EVENTO BOTONES ---------------------
	$("#btnPdf").on("click", function () {
		//var estadoRequeridos = ValidarRequeridos();
		//$('#externo').attr("src", ExportarReportef('PDF'));
		ExportarReportef('PDF');
	});
	$("#btnExcel").on("click", function () {
		//var estadoRequeridos = ValidarRequeridos();
		ExportarReportef2('EXCEL');
	});

	$('#divEstado').val(0);
	//--------------------------FUNCIONES DE  EL DROWDOWN LIST
	//valida si es ADMIN O CONTABILIDAD
	//var oculta = validarOficinaReporte();

	var oculta = validarOficinaReporte();
	var ocultacombo = validarOficinaReportedl();
	//oculta segun sea el caso
	if (ocultacombo == false) {
		$('#tddllOficina').hide();
		$('#tddllOficina2').hide();
		$('#dllOficina').prop('disabled', true);

		if (oculta == true) {
			$('#trRubro').show();
			//$("input[name=valida]").prop('enabled',true);
		} else {
			$('#trRubro').hide();
			//$("input[name=valida]").prop('disabled', true);

		}
	}
	//Si ocultacombo sera =1 Siempre que el usuario que ingreso sea Admin o Contabilidad
	if (ocultacombo == 1) {
		//Llena Combo con la funcion creada en el comun.drowdownlist
		loadComboOficina('dllOficina', '0');
		$('#dllOficina').prop('enabled', true);
		$('#dllOficina').show();
	}
	else {
		//deshabilita el Select
		$('#tddllOficina').hide();
		$('#tddllOficina2').hide();
		$('#dllOficina').prop('disabled', true);
	}

	$("#dllOficina").change(function () {
		if ($(this).val() == "26") {
			if (ocultacombo == 1) {
				$('#dllOficina').show();
				$('#trRubro').show();
			}
		} else {
			S
			$('#trRubro').hide();
			//$("input[name=valida]").prop('disabled', true);
		}
	});

	loadTipoDivision('ddlDivision', 'GEO', INEI);
	$("#ddlDivision").on("change", function () {
		var idDivision = $(this).val();
		loadSubTipoDivisiones(idDivision, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');
	});

	loadSubTipoDivisiones(INEI, 'lblSubTipo1', 'lblSubTipo2', 'lblSubTipo3', 'hidSubTipo1', 'hidSubTipo2', 'hidSubTipo3', 'ddlSubTipo1', 'ddlSubTipo2', 'ddlSubTipo3');
	
	loadTipoGrupo('dllGruModalidad', '0');
	$("#ddlSubTipo1").on("change", function () {
		var idDivision = $('#ddlDivision').val();
		var subtipo = $('#hidSubTipo2').val();
		var belog = $(this).val();
		loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo2', 0);
	});

	$("#ddlSubTipo2").on("change", function () {
		var idDivision = $('#ddlDivision').val();
		var subtipo = $('#hidSubTipo3').val();
		var belog = $(this).val();
		loadValoresXsubtipo_Division(idDivision, subtipo, belog, 'ddlSubTipo3', 0);
	});

	$("#hidCodigoGrupoEmpresarial").val(0);
	$("#hidGrupoFacturacion").val(0);
	//mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
	mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
	mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });
	mvInitLicencia({ container: "ContenedormvLicencia", idButtonToSearch: "btnBuscarLic", idDivMV: "mvBuscarLicencia", event: "reloadEventoLicencia", idLabelToSearch: "lblLicencia" });
	//mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });

    //mvInitModalidadUso({ container: "ContenedormvModalidad", idButtonToSearch: "btnBuscarMod", idDivMV: "mvModalidad", event: "reloadEventoMod", idLabelToSearch: "lblModalidad" });
	$("#hidLicencia").val(0);
	$("#hidEstablecimiento").val(0);
	$("#hidResponsable").val(0);
});

function FValidarRucVacio() {
	if ($("#txtRuc").val() != "" || $("#txtlicencia").val() != "") {
		return 1;
	} else {
		alert("Ingrese Ruc o Licencia");
		return 0;
	}
}


var reloadEvento = function (idSel) {
    $("#hidResponsable").val(idSel);
    ObtenerNombreSocio(idSel, "lbResponsable");
    //ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
};
//var reloadEvento = function (idSel) {

//	//alert("Selecciono ID:   " + idSel);
//	$("#hidResponsable").val(idSel);
//	var socio = $("#hidResponsable").val();
//	$.ajax({
//		data: { codigoBps: idSel },
//		url: '../General/ObtenerNombreSocio',
//		type: 'POST',
//		beforeSend: function () { },
//		success: function (response) {
//			var dato = response;
//			if (dato.result == 1) {
//				//alert(dato.valor);
//				$("#lblResponsable").html(dato.valor);
//			}
//		}

//	});

//};

function ObtenerNombreSocio(id, idLabelSetting) {
    $.ajax({
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        data: { codigoBps: id },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                $("#" + idLabelSetting).html(dato.valor);

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            // alert(thrownError);
        }
    });
}


var reloadEventoEst = function (idSel) {
	$("#hidEstablecimiento").val(idSel);
	ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
	//ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
};


function ObtenerNombreEstablecimiento(id, idLabelSetting) {
	$.ajax({
		url: '../General/ObtenerNombreEstablecimiento',
		type: 'POST',
		data: { codigoEst: id },
		success: function (response) {
			var dato = response;
			validarRedirect(dato); /*add sysseg*/
			if (dato.result == 1) {
				$("#" + idLabelSetting).html(dato.valor);

			} else if (dato.result == 0) {
				alert(dato.message);
			}
		},
		error: function (xhr, ajaxOptions, thrownError) {
			alert(xhr.status);
			// alert(thrownError);
		}
	});
}

var reloadEventoLicencia = function (idSel) {
	$("#hidLicencia").val(idSel);
	ObtenerNombreLicencia($("#hidLicencia").val());
};

function ObtenerNombreLicencia(idSel) {
	$.ajax({
		data: { codigoLic: idSel },
		url: '../General/ObtenerNombreLicencia',
		type: 'POST',
		beforeSend: function () { },
		success: function (response) {
			var dato = response;
			if (dato.result == 1) {
				$("#lblLicencia").html(dato.valor);
			}
		}
	});
};


function ExportarReportef(tipo) {

	var url = "";
	var vacio = $('input:radio[name=valida]:checked').val();
	var rubro = null;
	//validar COMBOS
	var ocultacombo = validarOficinaReportedl();
	var oculta = validarOficinaReporte();
	//obtiene el valor del combo
	var idoficina
	idoficina = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();
	var nombreoficina = $("#dllOficina option:selected").text();
	//Valida Seleccion del combo
	//var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
	//valida ingreso de RUC
	var ValidarRucVacio = FValidarRucVacio();
	if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
		rubro = vacio;
	}
	var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
	var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();
	var MOG_ID =  $("#dllGruModalidad").val() == "" ? "0" : $("#dllGruModalidad").val();

	var DEPARTAMENTO = $("#ddlSubTipo1").val();
	var PROVINCIA = $("#ddlSubTipo2").val();
	var DISTRITO = $("#ddlSubTipo3").val();
	//var ruc = $("#txtRuc").val() == null ? "0" : $("#txtRuc").val();

	//Si la validacion de fecha es Igual a 1 entonces :
	if (MOG_ID !="0") {
		$("#contenedor").show();
		$.ajax({
			url: '../ReporteEstablecimiento/ReporteTipo',
			type: 'POST',
			data: {
			    MOG_ID: MOG_ID, id_socio: SOCIO, id_departamento: DEPARTAMENTO, id_provincia: PROVINCIA, id_distrito: DISTRITO,
				id_est: EST_ID, formato: tipo
			},
			beforeSend: function () {
				var load = '../Images/otros/loading.GIF';
				$('#externo').attr("src", load);
			},
			success: function (response) {
				var dato = response;
				validarRedirect(dato); /*add sysseg*/
				if (dato.result == 1) {
					url = "../ReporteEstablecimiento/ReporteEstablecimiento?" +
          "MOG_ID=" + MOG_ID + "&" +
          "id_socio=" + SOCIO + "&" +
           "id_departamento=" + DEPARTAMENTO + "&" +
            "id_provincia=" + PROVINCIA + "&" +
            "id_distrito=" + DISTRITO + "&" +
            "id_est=" + EST_ID + "&" +
            "formato=" + tipo;
					$("#contenedor").show();
					$('#externo').attr("src", url);
				} else if (dato.result == 0) {
					$('#externo').attr("src", vacio);
					$("#contenedor").hide();
					url = alert(dato.message);
				}
			}
		});
	} else {
        alert('Por favor seleccione una Modalidad.')
		$("#contenedor").hide();
	}
}


function ExportarReportef2(tipo) {

    var url = "";
    var vacio = $('input:radio[name=valida]:checked').val();
    var rubro = null;
    //validar COMBOS
    var ocultacombo = validarOficinaReportedl();
    var oculta = validarOficinaReporte();
    //obtiene el valor del combo
    var idoficina
    idoficina = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();
    var nombreoficina = $("#dllOficina option:selected").text();
    //Valida Seleccion del combo
    //var validaselecciondelCombo = ValidarSeleccionCombo(idoficina);
    //valida ingreso de RUC
    var ValidarRucVacio = FValidarRucVacio();
    if ((oculta == true && idoficina == null) || (ocultacombo == true && idoficina == "26")) {
        rubro = vacio;
    }
    var EST_ID = $("#hidEstablecimiento").val() == "" || null ? 0 : $("#hidEstablecimiento").val();
    var SOCIO = $("#hidResponsable").val() == "" || null ? 0 : $("#hidResponsable").val();
    var MOG_ID = $("#dllGruModalidad").val() == "" ? "0" : $("#dllGruModalidad").val();

    var DEPARTAMENTO = $("#ddlSubTipo1").val();
    var PROVINCIA = $("#ddlSubTipo2").val();
    var DISTRITO = $("#ddlSubTipo3").val();

	//var ruc = $("#txtRuc").val() == null ? "0" : $("#txtRuc").val();
	//Si la validacion de fecha es Igual a 1 entonces :
	//if (validafecha == 1) {
	    if (MOG_ID != "0") {
	        $("#contenedor").show();
	        $.ajax({
	            url: '../ReporteEstablecimiento/ReporteTipo',
	            type: 'POST',
	            data: {
	                MOG_ID: MOG_ID, id_socio: SOCIO, id_departamento: DEPARTAMENTO, id_provincia: PROVINCIA, id_distrito: DISTRITO,
	                id_est: EST_ID, formato: tipo
	            },
	            beforeSend: function () { },
	            success: function (response) {
	                var dato = response;
	                validarRedirect(dato); /*add sysseg*/
	                if (dato.result == 1) {
	                    url = "../ReporteEstablecimiento/ReporteEstablecimiento?" +
                        "MOG_ID=" + MOG_ID + "&" +
                        "id_socio=" + SOCIO + "&" +
                        "id_departamento=" + DEPARTAMENTO + "&" +
                        "id_provincia=" + PROVINCIA + "&" +
                        "id_distrito=" + DISTRITO + "&" +
                        "id_est=" + EST_ID + "&" +
                        "formato=" + tipo;

	                    window.open(url);
	                    $("#contenedor").hide();
	                } else if (dato.result == 0) {
	                    $("#contenedor").hide();
	                    url = alert(dato.message);
	                }
	            }
	        });
	    } else {
	        alert('Por favor seleccione una Modalidad.')
	        $("#contenedor").hide();
	    }
	//} else {
	//    alert('Por favor de verificar la fecha.')
	//    $("#contenedor").hide();
	//}
}
