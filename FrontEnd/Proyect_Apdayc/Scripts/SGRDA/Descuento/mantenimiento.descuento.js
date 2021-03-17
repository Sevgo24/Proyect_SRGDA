function initPopupsDescuentoLicencia() {

    kendo.culture('es-PE');

    $("#mvAdvertencia").dialog({
        close: function (event) {
            if (event.which) { returnPage(); }
        }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    });

    $("#mvActualizaDescuento").dialog({ autoOpen: false, width: 300, height: 300, buttons: { "Grabar": ModificaDesc, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });


    $("#ddlTipoDescuento").change(function () {
        //comun.dropdown.js
        //    alert($("#ddlTipoDescuento").val());
        if ($("#ddlTipoDescuento").val() != 11) {
            loadTipoDescuentoxTipoDescuento("ddlDescuento", 0, $("#ddlTipoDescuento").val());

        }
    });
}

$(function () {

  
    $("#btnGrabarDescto").css("display", "none");

    //Para Evitar Que se ahga bucle siempre poner  el codigo Change en el function
        $("#ddlTipoDescuento").change(function () {
        //comun.dropdown.js
    //    alert($("#ddlTipoDescuento").val());
        if ($("#ddlTipoDescuento").val() != 11) {
            loadTipoDescuentoxTipoDescuento("ddlDescuento", 0, $("#ddlTipoDescuento").val());

        }
    });
    //$("#ddlPerPlanFacDesc").change(function () {
        
    //    var validar = ValidarPeriodoDescuento($("#ddlPerPlanFacDesc").val());

    //});
       

});

//VARIABLE PARA OBTENER EL DESCUENTO TOTAL SI ES QUE LO HUBIERA:
var total = 0;
function loadDataDescuentos(idLic, idTarifa, idLicPlan) {
    
   // var RES = ValidarPeriodoDescuento(idLicPlan);

    //Aqui Entra Luego de seleccionar El Combo Periodos
    //vALIDA SI el Periodo esta Abierto o Cerrado
//    if (RES != 1) {

        msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, "");
        $.ajax({
            data: { idLicencia: idLic, idTarifa: idTarifa, idLicPlan: idLicPlan }, type: 'POST', url: '../TarifaTest/ObtenerValorTestTarifa',
            success: function (responseTarifa) {
                var datoTarifa = responseTarifa;
                validarRedirect(datoTarifa);/*add sysseg*/
                if (datoTarifa.result == 1) {

                    var entidad = datoTarifa.data.Data;

                    $("#divBtnAddDscto").css("display", "inline");
                    listarDescuento(entidad.ValorFormula, idLicPlan, idLic);
                } else if (datoTarifa.result == 0) {
                    $("#divBtnAddDscto").css("display", "none");
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, "Los valores del tab característica no son válidos para obtener el valor de la tarifa.");
                    listarDescuento(0, 0, idLic);

                }
            }
        });
  //  }
}



function loadDataGridTmpDescuento(Controller, idGrilla, idLic,ValorTestTarifa) {
    $.ajax({
        data: { idLic: idLic, valorTestTarifa: ValorTestTarifa },
        type: 'POST',
        url: Controller,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var entidad = dato.data.Data;
                $(idGrilla).html( dato.message);
               
               
                ///luego de cargar la grilla 1 ..inicia la carga de la grilla 2 Impuestos
                var idEstab = $(K_HID_KEYS.ESTABLECIMIENTO).val();
                if (idLic == -1) idEstab = -1;

                var montoTotalTarifa = idEstab == -1 ? 0.00 : ValorTestTarifa;

                loadDataImpuesto(idEstab, entidad.TotalDescuento, entidad.TotalCargo, montoTotalTarifa);

            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }

        }
    });
}
 



function actualizarMonto(control, id) {
    //alert("Ingreso?");
    $.ajax({
        data: { idLicDes: id, valorAplicable: $("#" + control.id).val() },
        type: 'POST',
        url: "../Descuento/UpdAplicable",
        beforeSend: function () {  },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                
                //loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(),$('#ddlPerPlanFacDesc  option:selected').val()); 
                ActualizaDesctLicenciaCalc();
                loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }
        }
    });
}
//Aqui Es Donde esta entrando para poder hacer la invocacion de El Pop Up
function initLoadAddDescuento(idPopup,idContenedorCtrl,idContenedorMsg) {
    
  
    msgErrorB(idContenedorMsg, "");
    limpiarDescuento();
  //  if (ValidarObligatorio(idContenedorMsg, idContenedorCtrl)) {
       
        //COMUN.DROPDOWNLIST.JS
        loadTipoDescuento("ddlTipoDescuento", 0);
        $('#ddlDescuento  option').remove();
        $('#ddlDescuento').append($("<option />", { value: '0', text: K_ITEM.CHOOSE }));
        $("#" + idPopup).dialog("open");

        $("#ddlDescuento").show();
        $("#txtDescuentoEspecial").hide();
        $("#txtDescuentoEspecial").removeClass('requerido');
        $("#txtDescuentoEspecial").css('border-color', 'gray');
        $("#txtValorDscto").hide();
        $("#txtValorDscto").removeClass('requerido');
        $("#txtValorDscto").css('border-color', 'gray');
        $("#ddlDescuento").addClass('requeridoLst');


        //aqui
   // }


 }
function obtenerDescuento(idDscto) {

    $.ajax({
        data: { id: idDscto },
        type: 'POST',
        url: "../Descuentos/Obtiene",
        beforeSend: function () {  },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var entidad = dato.data.Data;
                $('#lblPerDscto').html(entidad.DISC_PERC);
                $('#lblValorDscto').html(entidad.DISC_VALUE);
                $('#lblSignoDscto').html(entidad.DISC_SIGN);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}
function delAddDescuento(id) {
    msgOkB(K_DIV_MESSAGE.DIV_TAB_DSCTO, "");
    if(confirm("Está seguro de eliminar el descuento?")){
        $.ajax({
            data: { idLicDes: id },
            type: 'POST', url: "../Descuento/Eliminar",
            beforeSend: function () {  },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    //loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), $('#ddlPerPlanFacDesc  option:selected').val());
                    ActualizaDesctLicenciaCalc();
                    loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
                } else if (dato.result == 0) {
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
                }
            }
        });
    }
}
function addDescuento() {
    //var RES = ValidarPeriodoDescuento($("#ddlPerPlanFacDesc").val());
    var RES = 0;
    if (RES != 1) {
        if (ValidarObligatorio(K_DIV_MESSAGE.DIV_TAB_POPUP_DESCUENTO, K_DIV_POPUP.DESCUENTO)) {

            var idTipoDescuento = $('#ddlTipoDescuento  option:selected').val();
            var idDescuento = 0;

            //alert("Agregar Descuento Financiero o Volumen :" + idDescuento + "Tipo" + ObtieneIdTipoDscto());
            if (idTipoDescuento == ObtieneIdTipoDscto()) {
                //ObtieneIdTipoDscto Retorna 11 Id de el Descuento Especial
                //Aqui Ingresa Cuando es Descuento Especial
                var monto;
                if ($("#ddlSignoDescuento").val() == "P") {

                    monto = $('#txtPerDscto').val();

                } else if($("#ddlSignoDescuento").val() == "V"){
                    
                    monto = $('#txtValorDscto').val();
                }

                idDescuento = addDescuentoEspecial($('#ddlTipoDescuento  option:selected').val(), $('#txtDescuentoEspecial').val(),monto, $('#txtDescOb').val());
                //alert(idDescuento);
            } else {
                var monto = 0;
                if ($('#lblValorDscto').text() != 0) {
                    monto = $('#lblValorDscto').text();
                } else {
                    monto = $('#lblPerDscto').text();
                }

                idDescuento = addDescuentoFinancieroVolumen($('#ddlTipoDescuento  option:selected').val(), $('#ddlDescuento option:selected').text(), monto,$('#txtDescOb').val());
            }

            if (idDescuento != 0) {
                $.ajax({
                    data: {
                        idTipo: $('#ddlTipoDescuento  option:selected').val(),
                        id: idDescuento,
                        Tipo: $('#ddlTipoDescuento  option:selected').text(),
                        idLic: $(K_HID_KEYS.LICENCIA).val(),
                        OBSERVACION: $('#txtDescOb').val()
                    },
                    type: 'POST', url: "../Descuento/addItem",
                    beforeSend: function () { },
                    success: function (response) {
                        var dato = response;
                        validarRedirect(dato);/*add sysseg*/
                        if (dato.result == 1) {
                            //alert("ingreso Correctamente");
                            // loadDataDescuentos($(K_HID_KEYS.LICENCIA).val(), $(K_HID_KEYS.TARIFA).val(), $('#ddlPerPlanFacDesc  option:selected').val());
                            ActualizaDesctLicenciaCalc();
                            loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
                        } else if (dato.result == 0) {
                            msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
                        }
                    }
                });
                $("#" + K_DIV_POPUP.DESCUENTO).dialog("close");
            }

        }
    } else {

        $("#" + K_DIV_POPUP.DESCUENTO).dialog("close");
    }

}
function limpiarDescuento() {
    $('#lblPerDscto').html("");
    $('#lblValorDscto').html("");
    $('#lblSignoDscto').html("");
    $("#ddlSignoDescuento").removeClass('requerido');
    $("#ddlSignoDescuento").hide();
    $("#tdSignoDescuentoDes").hide();
    LimpiarRequeridos(K_DIV_MESSAGE.DIV_TAB_POPUP_DESCUENTO, K_DIV_POPUP.DESCUENTO);
}

function loadDataImpuesto(idEstab,dTotalDscto,dTotalCargo,dTotalTarifa) {
    $.ajax({
        data: { codigoEstab: idEstab }, type: 'POST', url: "../Licencia/ListarImpuesto",
        beforeSend: function () {  },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var entidad = dato.data.Data;
                $("#gridImpuesto").html(dato.message);
                calcTest(dTotalTarifa, dTotalDscto, dTotalCargo, entidad.totalImpPer, entidad.totalImpVal);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function calcTest(ttar, tdes, tcarg, timpPer, timpVal) {

    
    var licid = $("#txtCodigo").val();

    //Retorna el Valor de DESCEUNTO TOTAL
    //ya no es Necesario Por que el Valor tiene que calcularse y no amndarse
    //var retornodesc = MostrarDescuentoTotalSocio(licid);

    //alert("entro " + retornodesc);

    //alert("descpues");
    //MostrarDescuentoTotalSocio(licid);

                $.ajax({
                    data: { montoTarifa: ttar, montoDescuento: tdes, licid: licid, montoCargo: tcarg, totImpuestoPer: timpPer, totImpuestoVal: timpVal }, type: 'POST', url: "../Descuento/CalcularTestDescuento",
                    success: function (response) {
                        validarRedirect(response);/*add sysseg*/
                        if (response.result == 1) {

                            var entidad = response.data.Data;

                            $("#txtTarifaDesc").val(entidad.TarifaTotFormato);
                            $("#txtCargoDesc").val(entidad.CargosTotFormato);
                            $("#txtImpuestoDesc").val(entidad.ImpuestosTotFormato);
                            $("#txtDescuentoDesc").val(entidad.DsctosTotFormato);
                            $("#txtTotalDesc").val(entidad.TotalDsctoFormato);
                            $("#txdtDescuentoSoc").val(entidad.DsctoTotSocioFormato);


                        } else if (response.result == 0) {
                            alert(response.message);
                        }
                    }
                });            

}

function redondeo(numero, decimales) {
    var flotante = parseFloat(numero);
    var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
    return resultado;
}


var listarDescuento = function (valorFormula, idLicPlan, idLic) {

    if (idLicPlan == "0") idLic = -1;
    $.ajax({
        data: { idLic: idLic, idLicPlan: idLicPlan }, type: 'POST', url: '../General/ExisteCaracteristicasXLic',
        success: function (response) {
            validarRedirect(response);/*add sysseg*/
            if (response.result == 1) {
                var existeCaract = response.Code;
                if (existeCaract == "0") {
                    idLic = -1;
                }
                if (idLicPlan != "0" && existeCaract == "0") {
                    $("#divBtnAddDscto").css("display", "none");
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, "No existen Caracteristicas asociadas al periodo seleccionado para el cálculo de la tarifa.");
                }
                loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", idLic, valorFormula);
            } else if (response.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }
        }
    });
}

//
function addDescuentoEspecial(tipoDescuento, descripcion, valor, observacion) {
    //AQUI INGRESA CUANDO ES DESCUENTO ESPECIAL 2 PARTE
    var idDescuento = 0;
    var tipo = tipoDescuento;
    var desc = descripcion;
    var signo = '-';

    if ($("#ddlSignoDescuento").val() == "P") {
        var valor1 = "";
        var valor2 = valor;
       
    } else if ($("#ddlSignoDescuento").val() == "V") {
        var valor1 = valor;
        var valor2 = "";
        
    }
   // var valor1 = valor;
   // var valor2 = "";


    var cuenta = 2;//Cuenta Contable
    var estadoDescuento = "S";// Indicar de Descuento
    var OBSERVACION = observacion;
    var DescuentosSoc = [];


    //Descuento para Insertar En licencia
    var Descuentos = {
        DISC_ID: 0,
        DISC_TYPE: tipo,
        DISC_NAME: desc,
        DISC_SIGN: signo,
        DISC_PERC: valor2,
        DISC_VALUE: valor1,
        DISC_ACC: cuenta,
        DISC_AUT: estadoDescuento,
        OBSERVACION: observacion
    };
    //Evaluando si Es Descuento Por Socio O por Licencia
    //1= Es Socio
    //0=N oes Socio
    //K_RECONOCE_SOCIO VARIABLE CREADA EN socio.negocio.nuevo Se utiliza para reconocer que es descuento por socio y no por licencia
    if (K_RECONOCE_SOCIO.SOCIO == 1) {

      DescuentosSoc[0] = {
            Tipo: tipo,
            Descuento: desc,
            esSuma: signo,
            DISC_VALUE: valor1,
            DISC_ACC: cuenta,
            DISC_AUT: estadoDescuento,
            DISC_PERC: valor2,
            OBSERVACION: OBSERVACION
            //DISC_AUT: estadoDescuento,
        };
      var DescuentosSoc = JSON.stringify({ 'DescuentosSoc': DescuentosSoc });

      ArmaDescuentoTemporal(DescuentosSoc);

    } else {
        //aQUI SI DEBE DE INSERTAR POR QUE ES UN DESCUENTO ESPECIAL
        var retorna = InsertaDescuento(Descuentos);

        return  retorna;

    }
   
}

function addTarifaDescuento(idTarifa, idDescuento) {
    
    var idTarifaDescuento = 0;
    var tarifaDescuento = {
        RATE_ID: idTarifa,
        DISC_ID: idDescuento
    };


    $.ajax({
        url: "../Descuentos/InsertarTarifaDescuento",
        data: tarifaDescuento,
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                idTarifaDescuento = dato.Code;
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
    return idTarifaDescuento;
};

//ArmaDescuentoSocioTemporal
function ArmaDescuentoTemporal(DescuentosSoc) {
    //alert("Insertando Descuentos en El Socio");
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Descuento/ArmaDescuentoSocioTmp',
        data: DescuentosSoc/*: DescuentosSoc*/,
        success: function (response) {
            var dato = response;/*add sysseg*/
            if (dato.result == 1) {
                //alert("iNGRESO?=");
                //alert(dato.Code);
                if (dato.Code == 0)

                    alert("El Descuento Ingresado YA Existe !");

                $("#gridDescuentoSoc").html(dato.message);
                $("#mvDescuento").dialog("close");
                //loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", idLic, valorFormula);
            }
        }
    });
    //addDescuentoSocio(iDSocio, idDescuento);

    return idDescuento;
}

//Consulta SI hay Descuentos por SOcio (Si hay los llena en el temporal y redirecciona a El listado
function ConsultaDescuentosSocio(bpsid) {

    $.ajax({
        data: { bpsid: bpsid },
        type: 'POST',
        url: "../Descuento/ConsultaDescuentosSocio",
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {

                LoadDescuentosSocio();
            }
        }
    });
}
//Lista o Arma tabla para el Listado De descuentos de SOCIO
function LoadDescuentosSocio() {
   
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Descuento/ArmaDescuentoSocioTmp',
        //data: DescuentosSoc/*: DescuentosSoc*/,
        success: function (response) {
            var dato = response;/*add sysseg*/
            if (dato.result == 1) {
                //alert("iNGRESO?=");

                $("#gridDescuentoSoc").html(dato.message);
                $("#mvDescuento").dialog("close");
                //loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", idLic, valorFormula);
            }
        }
    });

}
//Descativando Licencias Temporalmente Por Descuentos por Socio
function DescativandoDescuentosSocio(id) {

    //alert("llego" + Orden);
    //Redireccionar a Armar el descuento 
    $.ajax({
        data: { id: id },
        type: 'POST',
        url: "../Descuento/DellAddDescuentoSocio",
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {

                LoadDescuentosSocio();

            }
        }
    });

}

//Mostrar El descuento Total que tiene por Socio
function MostrarDescuentoTotalSocio(licid) {
    var retornototal = 0;
    $.ajax({
        data: { licid: licid },
        type: 'POST',
        url: "../Descuento/ObtieneDescuentoTotalSocio",
        async:false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {

                retornototal = dato.valor;
               

            }else{
                retornototal = 0;
            }


        }
    });

    return retornototal;
}

function addDescuentoFinancieroVolumen(tipoDescuento, descripcion, valor,observacion) {
    //alert("VALOR =" + valor);
    //            alert($("#ddlDescuento").val());
    var idDescuento = 0;
    if ($("#ddlDescuento").val() != 0)
        idDescuento = $("#ddlDescuento").val();       
    
    var tipo = tipoDescuento;
    var desc = descripcion;
    var signo = '-';
    var valor1 = "";
    var valor2 = valor;
    var cuenta = 2;//Cuenta Contable
    var estadoDescuento = "S";// Indicar de Descuento
    var observa = observacion;
    //var DescuentosSoc = [];
    //DescuentosSoc[0] = {
    //    Tipo: tipo,
    //    Descuento: desc,
    //    esSuma: signo,
    //    DISC_VALUE: valor1,
    //    DISC_ACC: cuenta,
    //    DISC_AUT: estadoDescuento,
    //    DISC_PERC: valor2
    //    //DISC_AUT: estadoDescuento,
    //};
    //Si es Descuento que viene de la pantalla SOCIO 

    if (K_RECONOCE_SOCIO.SOCIO == 1) {
        var DescuentosSoc = [];
        DescuentosSoc[0] = {
            DISC_ID:idDescuento,
            Tipo: tipo,
            Descuento: desc,
            esSuma: signo,
            DISC_VALUE: valor1,
            DISC_ACC: cuenta,
            DISC_AUT: estadoDescuento,
            DISC_PERC: valor2,
            OBSERVACION: observa
            //DISC_AUT: estadoDescuento,
        };
       
        var DescuentosSoc = JSON.stringify({ 'DescuentosSoc': DescuentosSoc });
        ArmaDescuentoTemporal(DescuentosSoc);
    } else {
        
        var Descuentos = {
            DISC_ID: 0,
            DISC_TYPE: tipo,
            DISC_NAME: desc,
            DISC_SIGN: signo,
            DISC_PERC: valor2,
            DISC_VALUE: valor1,
            DISC_ACC: cuenta,
            DISC_AUT: estadoDescuento,
            OBSERVACION: observa
        };
        //Cuando es TIpo Financiero o Volumen no se debe insertar un unevo registro en la TABLA REC_DISCOUNTS
        //Solo se debe utilizar el DISC_ID que viene del ddlDescuento y solo se debera insertar en la tabla REC_LIC_DISCOUNTS
        //var retorna=  InsertaDescuento(Descuentos);
        if ($("#ddlDescuento").val() != 0) {
            var retorna = $("#ddlDescuento").val();
            
        }
        return retorna;
    }
}

//Inserta Descuentos Si vienen de  AddEspecial o AddDescuentosFinancierosVolumen
function InsertaDescuento(Descuentos) {

    $.ajax({
        url: "../Descuentos/Insertar",
        data: Descuentos,
        async: false,
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                idDescuento = dato.Code;
                if (idDescuento != 0) {
                    var idTarifa = $('#hidCodigoTarifa').val();
                    //addTarifaDescuento(idTarifa, idDescuento);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });

    return idDescuento;
}

//Validar Si  El periodo esta cerrado al seleccionar un item del select 
function ValidarPeriodoDescuento(CodPeriodo) {

    var valida = 0;
    $.ajax({
        data: { CodPeriodo: CodPeriodo },
        type: 'POST',
        url: "../Descuento/ValidarPeriodoDescuento",
        async: false,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                //alert("eNTRO");
               // $("#gridDescuento").html("");
                valida = 1;
                 msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            } else {
              //  $("#divBtnAddDscto").show();
                //$("#gridDescuento").show();
            }
        }


    });
    return valida;
}


///
function loadDataGridTmpDescuentoPlantilla(Controller, idGrilla, idLic) {
    $.ajax({
        data: { codigoLic: idLic },
        type: 'POST',
        url: Controller,
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                //var entidad = dato.data.Data;
                $(idGrilla).html(dato.message);
                ///luego de cargar la grilla 1 ..inicia la carga de la grilla 2 Impuestos
             //   var idEstab = $(K_HID_KEYS.ESTABLECIMIENTO).val();
             //   if (idLic == -1) idEstab = -1;
            //    var montoTotalTarifa = idEstab == -1 ? 0.00 : ValorTestTarifa;
              //  loadDataImpuesto(idEstab, entidad.TotalDescuento, entidad.TotalCargo, montoTotalTarifa);
            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }

        }
    });
}


function EditarDescuento(lic_disc) {
    $("#mvActualizaDescuento").dialog("open");
    $("#hidLicDisc").val(lic_disc);
    $('#txtmontoDesc').on("keypress", function (e) { return solonumeros(e); });
    ObtieneDatosDescuento();

}


function ObtieneDatosDescuento() {
    //$('#txtmontoDesc').on("keypress", function (e) { return solonumeros(e); });
    var codigo = $("#hidLicDisc").val();
    $.ajax({
        data: { LIC_DISC_ID: codigo },
        type: 'POST',
        url: "../Descuento/ObtenerDescuento",
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var descuento = dato.data.Data;

                $("#txtNomDesc").val(descuento.DISC_NAME);
                $("#txtmontoDesc").val(descuento.DISC_VALUE);
                $("#hidLicDiscEstado").val(descuento.DISC_ESTADO);
                $("#hidiLicDiscType").val(descuento.DISC_TYPE);
                $("#txtObsResp").val(descuento.DISC_RESP_OBSERVACION);
                if (descuento.DISC_ESTADO == 1 || descuento.DISC_TYPE != ObtieneIdTipoDscto()) {
                    $('#txtNomDesc').prop('readonly', true);
                    $('#txtmontoDesc').prop('readonly', true);

                } else {
                    $('#txtNomDesc').prop('readonly', false);
                    $('#txtmontoDesc').prop('readonly', false);
                }

            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }

        }
    });
    
}


function ModificaDesc() {

    //alert("Modificando ");

    var lic_disc_id = $("#hidLicDisc").val();
    var nombre_desc = $("#txtNomDesc").val();
    var monto_desc = $("#txtmontoDesc").val();
    var disc_type = $("#hidiLicDiscType").val();
    if ($("#hidLicDiscEstado").val() != 1 && $("#hidiLicDiscType").val()== ObtieneIdTipoDscto()) {
        $.ajax({
            data: { LIC_DISC_ID: lic_disc_id, NomDesc: nombre_desc, MontoDesc: monto_desc},
            type: 'POST',
            url: "../Descuento/ActualizaDescuento",
            success: function (response) {
                var dato = response;
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    alert("Descuento Actualizado Correctamente -Esperando Confirmacion");
                    ActualizaDesctLicenciaCalc();
                    loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
                    $("#mvActualizaDescuento").dialog("close");
                } else if (dato.result == 0) {
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
                   $("#mvActualizaDescuento").dialog("close");
                }

            }
        });
    }else
    {
        alert("EL DESCUENTO NO PUEDE SER MODIFICADO POR ESTAR APROBADO O NO SER UN DESCUENTO ESPECIAL");
    }


}


function ActualizaDesctLicenciaCalc() {

    //alert("Modificando ");

    var codigo = $("#hidLicId").val();

        $.ajax({
            data: { CodigoLicencia: codigo },
            type: 'POST',
            async: false,
            url: "../Descuento/ActualizarMontoCalculadoDescuentoLicencia",
            success: function (response) {
                var dato = response;
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                 //   alert("Descuento Actualizado Correctamente -Esperando Confirmacion");
                    //loadDataGridTmpDescuento('../Descuento/ListarDescuentos', "#gridDescuento", $("#hidLicId").val(), 0);
                    //$("#mvActualizaDescuento").dialog("close");
                } else if (dato.result == 0) {
                    //msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
                    //$("#mvActualizaDescuento").dialog("close");
                }

            }
        });

}
