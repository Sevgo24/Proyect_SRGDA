$(function () {

    $("#btnGrabarCaract").button({ icons: { secondary: "ui-icon-disk" } });
});
function loadDataCaracteristica(codLic, dfecha, codLicPlan) {
 
        loadDataGridCaract('ListarCaracteristica', "#gridCaracteristica", codLic, dfecha, codLicPlan);



}

function loadDataGridCaract(Controller, idGrilla, codLic, dfecha, codLicPlan) {

  
	$.ajax({
	    data: { codigoLic: codLic, fecha: dfecha, codigoLicPlan: codLicPlan },
		type: 'POST', url:  Controller,
		beforeSend: function () { },
		success: function (response) {
		    var dato = response;
		    validarRedirect(dato); /*add sysseg*/
		    if (dato.result == 1) {
		        $(idGrilla).html(dato.message);

		        //$('.k-formato-numerico').each(function (i, elem) {
		        //    $(elem).priceFormat({
		        //        clearPrefix: true,
		        //        prefix: '',
		        //        limit: 11,
		        //        centsLimit: 4
		        //    });
		        //});

		        $('.cssTabLicGridCheck').each(function (i, elem) {
		            $(elem).on("click", function () {
		                if ($(elem).prop('checked')) $("#txtComentChar_" + i).removeAttr("disabled");
		                else $("#txtComentChar_" + i).attr("disabled","disabled");
		            });
		        });


		    } else if (dato.result == 0) {
		        alert(dato.message);
		    }
		  
		}
	});
}

function InsertarCaracteristica(idLicPlan,idFechaRec ) {
    var K_DIV_MESSAGE = { DIV_TAB_CARACT: "avisoTabCaracteristica" };

    var resp = ValidaLicenciaAutorizacion(2, idLicPlan); //accion de validar autorizacion /mantenimiento.autorizacion/

    if (resp) {
        if (confirm("Sólo si ya existen caracteristicas para la fecha de hoy se actualizarán los valores. Caso contrario se crearán caracteristicas con nueva fecha.\n¿Estás seguro de actualizar los valores de las caracteristicas?  ")) {
            //aqui se esta rayando********************
            var listCaracteristica = new Array();
            var indice = 0;
            var idLic = 0;
            $('.cssValCaract ').each(function (idx, control) {
                //console.log(idx +".... "+$("#checkFlagChar_" + idx).prop('checked') + ' .. ' + $("#txtComentChar_" + indice).val());
                //Valor: $(control).val(),
                listCaracteristica.push({
                    CodigoCaracteristica: $('#hidIdCaract_' + idx).val(),
                    CodigoLic: $('#hidIdLic_' + idx).val(),
                    Tipo: $('#hidTipo_' + idx).val(),
                    Owner: '',
                    FechaCaracteristicaLic: '2014-10-10',
                    DescCarateristica: '',
                    CodigoLicPlan: idLicPlan,
                    ValorString: $(control).val(),
                    EsCaractAlterada: $("#checkFlagChar_" + idx).prop('checked') ? true : false,
                    CaractAlteradaDesc: $("#txtComentChar_" + idx).val()

                });
                //alert(listCaracteristica);
                idLic = $('#hidIdLic_' + idx).val();
                indice = indice + 1;
            });



            if (indice != 0) {
                var postData = JSON.stringify(listCaracteristica);
                $.ajax({
                    data: { caracteristicas: postData },
                    type: 'POST',
                    url: 'InsertarLicenciaCaract',
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);/*add sysseg*/
                        if (dato.result == 1) {

                            ActualizaLicenciaMonto(idLicPlan);// ACTUALIZA MONTOS LICENCIA
                            ActualizaDesctLicenciaCalc();//actualizar descuentos licencia 
                            loadFechaCaractLic("ddlFechasSearchCarac", [idLic, idLicPlan], function (exito) {
                                loadDataCaracteristica(idLic, idFechaRec, idLicPlan);
                            });


                            /*BEGIN - CODIGO PARA LA ACTUALIZACION DE TAB DESCUENTOS DESPUES DE ACTUALIZAR CARACTERISTICAS*/
                            var disabled = $("#tabs").tabs("option", "disabled");
                            var refreshDiscount = true;
                            $.each(disabled, function (key, value) { if (value == 2) { refreshDiscount = false; } });
                            //actualizamos los descuentos con el nuevo test de tarifa y siempre que el combo fechas de plan este cargado
                            if (refreshDiscount == true && $("#ddlPerPlanFacDesc option[value=" + idLicPlan + "]").length != 0) {
                                $("#ddlPerPlanFacDesc").val(idLicPlan).change();
                                // loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), idLicPlan);
                            }
                            /*END - CODIGO PARA LA ACTUALIZACION DE TAB DESCUENTOS DESPUES DE ACTUALIZAR CARACTERISTICAS*/
                            //console.log(K_DIV_MESSAGE.DIV_TAB_CARACT +" ..OK "+ dato.message);
                            msgOkB(K_DIV_MESSAGE.DIV_TAB_CARACT, dato.message);


                        } else if (dato.result == 0) {
                            msgErrorB(K_DIV_MESSAGE.DIV_TAB_CARACT, dato.message);
                            // console.log(K_DIV_MESSAGE.DIV_TAB_CARACT + " ...Error.. " + dato.message);
                        }
                    }
                });
            } else {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_CARACT, "No existen características para registar a licencia.");
                //console.log(K_DIV_MESSAGE.DIV_TAB_CARACT + " ... " + "No existen características para registar a licencia.");
            }
        }
    } else {
        alert("No se Insertaron/Actualizaron las caracteristicas, Faltan Datos de La Autorizacion de Licencia | El Periodo Se encuentra Cerrado ");
    }
}

function InsertarCaracteristicaDsc(idLicPlan, idFechaRec) {
    var K_DIV_MESSAGE = { DIV_TAB_CARACT: "avisoTabDescuento" };
    if (confirm("Sólo si ya existen caracteristicas para la fecha de hoy se actualizarán los valores. Caso contrario se crearán caracteristicas con nueva fecha.\n¿Estás seguro de actualizar los valores de las caracteristicas?  ")) {
        //aqui se esta rayando********************
        var listCaracteristicadsc = new Array();
        var indice = 0;
        var idLic = 0;
        $('.cssValCaractDsc ').each(function (idx, control) {
            //console.log(idx +".... "+$("#checkFlagChar_" + idx).prop('checked') + ' .. ' + $("#txtComentChar_" + indice).val());
            //Valor: $(control).val(),
            listCaracteristicadsc.push({
                CodigoCaracteristica: $('#hidIdCaractDsc_' + idx).val(),
                CodigoLic: $('#hidIdLic_' + idx).val(),
                Tipo: $('#hidTipo_' + idx).val(),
                Owner: '',
                FechaCaracteristicaLic: '2014-10-10',
                DescCarateristica: '',
                CodigoLicPlan: idLicPlan,
                ValorString: $(control).val(),
                EsCaractAlterada: $("#checkFlagChar_" + idx).prop('checked') ? true : false,
                CaractAlteradaDesc: $("#txtComentChar_" + idx).val()

            });
            //alert(listCaracteristica);
            idLic = $('#hidIdLic_' + idx).val();
            indice = indice + 1;
        });



        if (indice != 0) {
            var postData = JSON.stringify(listCaracteristicadsc);
            $.ajax({
                data: { caracteristicas: postData },
                type: 'POST',
                url: 'InsertarLicenciaCaract',
                beforeSend: function () { },
                success: function (response) {
                    var dato = response;
                    validarRedirect(dato);/*add sysseg*/
                    if (dato.result == 1) {
                        loadFechaCaractLic("ddlFechasSearchCarac", [idLic, idLicPlan], function (exito) {
                            loadDataCaracteristica(idLic, idFechaRec, idLicPlan);
                        });


                        /*BEGIN - CODIGO PARA LA ACTUALIZACION DE TAB DESCUENTOS DESPUES DE ACTUALIZAR CARACTERISTICAS*/
                        var disabled = $("#tabs").tabs("option", "disabled");
                        var refreshDiscount = true;
                        $.each(disabled, function (key, value) { if (value == 2) { refreshDiscount = false; } });
                        //actualizamos los descuentos con el nuevo test de tarifa y siempre que el combo fechas de plan este cargado
                        if (refreshDiscount == true && $("#ddlPerPlanFacDesc option[value=" + idLicPlan + "]").length != 0) {
                            $("#ddlPerPlanFacDesc").val(idLicPlan).change();
                            // loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), idLicPlan);
                        }
                        /*END - CODIGO PARA LA ACTUALIZACION DE TAB DESCUENTOS DESPUES DE ACTUALIZAR CARACTERISTICAS*/
                        //console.log(K_DIV_MESSAGE.DIV_TAB_CARACT +" ..OK "+ dato.message);
                        msgOkB(K_DIV_MESSAGE.DIV_TAB_CARACT, dato.message);


                    } else if (dato.result == 0) {
                        msgErrorB(K_DIV_MESSAGE.DIV_TAB_CARACT, dato.message);
                        // console.log(K_DIV_MESSAGE.DIV_TAB_CARACT + " ...Error.. " + dato.message);
                    }
                }
            });
        } else {
            msgErrorB(K_DIV_MESSAGE.DIV_TAB_CARACT, "No existen Descuentos De Tarifa para Registrar a esta Licencia");
            //console.log(K_DIV_MESSAGE.DIV_TAB_CARACT + " ... " + "No existen características para registar a licencia.");
        }
    }
}
 


