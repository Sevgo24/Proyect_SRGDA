var mvInitEstablecimientoSocioEmpre = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;
    var Valida = parametro.valida;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewEstSocMult" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventEstSocMul" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<input type="hidden" id="hidCodigoBPS"> <input type="hidden" id="hidCodigoEST"> <input type="hidden" id="hidCodigoDir">';

    elemento += '<tr>';
    elemento += '<td>Socio :</td><td colspan="3"><input type="text" id="txtNombreSocionegocioEmpr" style="width: 380px"></td>';														//id = "txtNombreSocionegocio"

    elemento += '<tr>';
    elemento += '<td ><center><button id="btnBuscarEstSocMul"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp</td>';
    elemento += '<td ><button id="btnLimpiarEst"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table></div>';

    elemento += '<table style=" width:100%; border:1px;">';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td valign="top" width="250px"><div id="gridEstablecimientoOriginal" width="100px"></div>';
    elemento += '</td>';
    elemento += '<td align="center"><button id="btnElegirEstablecimiento"> <img src="../Images/botones/more.png"  width="16px"></button>';
    elemento += '</td>';
    elemento += '<td valign="top"  width="250px">';
    elemento += '<div id="gridEstablecimientoFinal" width="100px"><div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td align="center"></td><td align="center"><button  id="btnNoelegirrEstablecimiento"> <img src="../Images/botones/menos.png"  width="16px"></button>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="3"><center><button id="btnGrabarEstEmp">GRABAR<img src="../Images/botones/save.png"  width="16px"></button></center></td>';
    elemento += '</tr>';

    elemento += '</table>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';



    //if (PermitirSeleccionarBotones == 0) {

    $("#" + idContenedor).append(elemento);

    $("#" + parametro.idButtonToSearch).on("click", function () { initLoadCombosEstab(); $("#" + parametro.idDivMV).dialog("open"); });
    //if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { initLoadCombosEstab(); $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 710,
        height: 590,
        title: "Búsqueda de Establecimientos de Socios "
    });
    //} else {
    //    CerrarPopUp();
    //}

    //inicializando variables de busqueda de establecimiento
    var _cuentaBusEsta = 0;
    $("#hidCodigoBPS").val(0);
    // initAutoCompletarGrupoEmpresarial("txtNombreSocionegocioEmpr", "hidCodigoBPS");
    initAutoCompletarRazonSocial("txtNombreSocionegocioEmpr", "hidCodigoBPS");

    if ($("#txtNombreSocionegocio").val() == "")
        $("#hidCodigoBPS").val(0);

    //EVENTO QUE BUSCA SOCIO
    $("#btnBuscarEstSocMul").on("click", function () {
        //	//if (_cuentaBusEsta == 1) {
        var soc = $("#hidCodigoBPS").val();
        $("#gridEstablecimientoFinal").html('');
        $("#gridEstablecimientoOriginal").html('');
        var dato = 0;
        BuscarEstablecimientoSocioEmpresarial(dato);
    });

    //Evento que busca socio antes de abrir el popup*
    $("#btnBuscarEstablecimientoMult").on("click", function () {
        if (_cuentaBusEsta == 0)
        {
            var dato = $("#hidResponsable").val();
            //alert("Mostrar ="+dato);
            BuscarEstablecimientoSocioEmpresarial(dato);
            _cuentaBusEsta++;
        }
    });


    //hasta aqui 
    //}

    $("#btnElegirEstablecimiento").on("click", function () {
        //	//if (_cuentaBusEsta == 1) {
        var soc = $("#hidCodigoBPS").val();
        obtenerEstablecimientosSeleccionadas();
        //} else {
        //	var soc = $("#hidCodigoBPS").val();
        //	$('#gridEstablecimiento').data('kendoGrid').dataSource.query({
        //		Socio: $("#hidCodigoBPS").val(),
        //		page: 1,
        //		pageSize:4
        //	});
        //}
    });
    $("#btnNoelegirrEstablecimiento").on("click", function () {
        //Limpiar la Data de gridcontenedorListaDestino para poder eliminar

        //	//if (_cuentaBusEsta == 1) {
        var soc = $("#hidCodigoBPS").val();
        Confirmar('Si los Establecimientos Seleccionados ya estaban registrados, las Licencias Asociadas seran Quitadas de la Cadena ,Seguro(a) ?',
            function () { RegresarEstablecimientosSeleccionadas(); },
            function () { },
            'Confirmar'
            );


        //RegresarEstablecimientosSeleccionadas();
    });
    //
    $("#btnGrabarEstEmp").on("click", function () {
        var nombreSocioNeg = $("#txtNombreSocionegocioEmpr").val();
        var codigoSocioNeg = $("#hidCodigoBPS").val();
        //$("#mvInitEstablecimientoSocioEmpre").dialog("close");
        //alert(codigoSocioNeg);
        CerrarPopUp();
        if (nombreSocioNeg != "" && codigoSocioNeg != "") {
            RecuperaSocioEmpPopUp(nombreSocioNeg, codigoSocioNeg);
        }


    });



    $("#btnLimpiarEst").on("click", function () {
        limpiarBusquedaEst();

        var soc = $("#hidCodigoBPS").val();
        $('#gridEstablecimiento').data('kendoGrid').dataSource.query({
            Tipoestablecimiento: $("#ddlTipoest").val(),
            SubTipoestableimiento: $("#ddlSubtipoest").val(),
            Nombreestablecimiento: $("#txtEstablecimiento").val(),
            Socio: $("#hidCodigoBPS").val(),
            Tipodivision: $("#ddlDivTipo").val(),
            Division: $("#ddlDiv").val(),
            //estado: $("#ddlEstadoEst").val() == "A" ? 1 : 0,
            estado: $("#ddlEstadoEst").val() == 2 ? 0 : 1,
            page: 1, pageSize: 4
        });
    });

    ////OBTIENE ESTABLECIMIENTOS PARA PODER EDITARLOS
    ////Solo debe buscar la Primera Vez Luego no Deberia Permitir
    ////Cont variable para contar solo deberia Entrar una Vez Al darle de nuevo buscar solo abrira la ventana (y)
    //var cont = 0;
    //if ($("#txtCodigo").val() != null) {
    //    //alert("Entro");
    //    //Recuperando El Codigo Para Listar Establecimientos 

    //    $("#btnBuscarEstablecimientoMult").on("click", function () {
    //        var dato = $("#hidResponsable").val();
    //        if (cont < 1) {
    //            BuscarEstablecimientoSocioEmpresarial(dato);
    //            cont++;
    //        }

    //    });
    //}




}

//LIMPIAR LOS CONTROLES
var limpiarBusquedaEst = function () {
    //$("#txtDireccion").val("0"); //en pruebas, luego modificar hidCodigoDir al terminar el popup de direcciones
    $("#txtNombreSocionegocioEmpr").val("");
    $("#hidCodigoBPS").val(0);
    $("#hidCodigoEST").val(0);
    $("#gridEstablecimientoFinal").html('');
    $("#gridEstablecimientoOriginal").html('');
};


function BuscarEstablecimientoSocioEmpresarial(dato) {
    //alert(dato);
  //  if (dato == 0 || dato == null) {
  //      var soc = $("#hidCodigoBPS").val();
  //      var LicMas = $("#hidLicMaster").val();
  //      // Lista Datos en la lista Destino   para Modificar

  //      // COnsulta los Datos para Listar 
  //      $.ajax({
  //          url: '../Establecimiento/ConsultaEstablecimientoSocioEmpresarial',
  //          type: 'POST',
  //          data: {
  //              Socio: soc, LicMas: LicMas

  //          },
  //          beforeSend: function () { },
  //          success: function (response) {
  //              var dato = response;
  //              validarRedirect(dato);
  //              if (dato.result == 1) {
  //                  LoadListarEstablecimientoSocioEmpresarialSeg();
  //              } else if (dato.result == 0) {
  //                  $("#gridEstablecimientoOriginal").html('');
  //                  alert(dato.message);
  //              }
  //          }
  //      });
  ////  } else {
        //

        var soc = dato;
        var LicMas = $("#hidLicMaster").val();

        $.ajax({
            url: '../Establecimiento/ConsultaEstablecimientoSocioEmpresarial',
            type: 'POST',
            data: {
                Socio: soc, LicMas: LicMas

            },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    //LoadListarEstablecimientoSocioEmpresarialSeg();
                    //alert("listo 2");
                    LoadListarEstablecimientoSocioEmpresarialSeg();
                    ConsultarEstSocEmprGrab(soc, LicMas);
                } else if (dato.result == 0) {
                    $("#gridEstablecimientoOriginal").html('');
                    alert(dato.message);
                }
            }
        });

        //alert(soc);

   // }

}





function loadDataEstablecimientoSocioEmpresarial() {
    //alert("LISTANDO..");
    loadDataGridTmp('../Establecimiento/ListarEstablecimientoSocioEmpresarial', "#gridEstablecimientoOriginal");
}


function loadDataGridTmp(Controller, idGrilla) {
    //alert("ingreso al controller");
    $.ajax({
        type: 'POST', data: {}, url: Controller,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                //$("#CantidadRegistros").html(dato.Code);
            } else if (dato.result == 0) {
                $("#gridEstablecimientoOriginal").html('');
                //$("#CantidadRegistros").html('0');
                alert(dato.message);
            }
        }
    });
}

//OBTENER LOS VALORES SELECCIONADOS DE LA LISTA

function obtenerEstablecimientosSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;

    $('#tblEstablecimientosSocEmp tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNro) && IdNro != null) {
            var idEst = $(this).find(".IDEstOri").html();
            var NomEst = $(this).find(".NomEstOri").html();

            if ($('#chkEstOrigen' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idEst,
                    Nombre: NomEst
                };
                contador += 1;
            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../Establecimiento/EstablecimientoSocEmpArmaTemporalesOriginal',
            data: ReglaValor,
            //codigo malo..-:"
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    //$("#gridEstablecimientoFinal").html(dato.message);
                    LoadListarEstablecimientoSocioEmpresarialSeg();
                    //$("#CantidadRegistros").html(dato.Code);
                } else if (dato.result == 0) {
                    $("#gridEstablecimientoFinal").html('');
                    //$("#CantidadRegistros").html('0');
                    alert(dato.message);
                }
            }
        });
    } else {
        alert("Debe selecionar antes de continuar.");
    }
}
//RegresarEstablecimientosSeleccionadas
function RegresarEstablecimientosSeleccionadas() {
    var ReglaValor = [];
    var contador = 0;
    //Borra la data  del tercer Grid para poder Eliminar
    $("#gridcontenedorListaDestino").html('');

    $('#tblEstablecimientosSocEmpSeg tr').each(function () {
        var IdNro = $(this).find(".IDEstOri").html();

        if (!isNaN(IdNro) && IdNro != null) {

            var idEst = $(this).find(".IDEstOri").html();
            var NomEst = $(this).find(".NomEstOri").html();

            if ($('#chkEstFin' + idEst).is(':checked')) {
                ReglaValor[contador] = {
                    Codigo: idEst,
                    Nombre: NomEst
                };
                //alert(ReglaValor[contador].Codigo);
                InactivarLicenciasHijas(ReglaValor[contador].Codigo);
                contador += 1;

            }
        }
    });

    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    if (contador > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../Establecimiento/EstablecimientoSocEmpArmaTemporalesDestino',
            data: ReglaValor,
            //codigo malo..-:"
            success: function (response) {
                var dato = response;
                if (dato.result == 1) {
                    //$("#gridEstablecimientoFinal").html(dato.message);
                    LoadListarEstablecimientoSocioEmpresarialSeg();
                    //$("#CantidadRegistros").html(dato.Code);
                } else if (dato.result == 0) {
                    $("#gridEstablecimientoFinal").html('');
                    //$("#CantidadRegistros").html('0');
                    alert(dato.message);
                }
            }
        });
        //Inactivar  Licencias

    } else {
        alert("Debe selecionar antes de continuar.");
        //Vuelve a listar 
        LoadListarEstablecimientoSocioEmpresarialSeg();
    }
}



function LoadListarEstablecimientoSocioEmpresarialSeg() {
    //alert("ingreso al controller");
    //var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Establecimiento/ListarEstablecimientoSocioEmpresarialSeg',
        data: {},
        //codigo malo..-:"
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //$("#gridEstablecimientoFinal").html(dato.message);
                $("#gridEstablecimientoFinal").html(dato.message);
                //Lista Establecimientos en el contenedor que adrian dijo que se mostrara
                $("#gridcontenedorListaDestino").html(dato.message);
                loadDataEstablecimientoSocioEmpresarial();
                //$("#CantidadRegistros").html(dato.Code);
            } else if (dato.result == 0) {
                $("#gridEstablecimientoFinal").html('');
                $("#gridEstablecimientoOriginal").html('');
                $("#gridcontenedorListaDestino").html('');
                //$("#CantidadRegistros").html('0');
                alert(dato.message);
            }
        }
    });
}
function validaEstablecimiento(idEst) {
    var estado = false;
    var IdEstablecimiento = idEst;
    $.ajax({
        url: '../Establecimiento/ValidarCaracteristicasEsTablecimiento',
        type: 'GET',
        data: { IdEstablecimiento: IdEstablecimiento },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {

                $('#chkEstOrigen' + IdEstablecimiento).is('checked');
                estado = true;
            } else {
                $('#chkEstOrigen' + IdEstablecimiento).prop("checked", false);
                alert("El local no tiene características y valores registrados");
            }
        }
    });
    return estado;
}


//Cerrar POP
function CerrarPopUp() {
    //alert("cerro?");
    var hidIdidModalViewEst = $("#hidIdidModalViewEst").val() + "SocEmp";
    //alert(hidIdidModalViewEst);
    $("#" + hidIdidModalViewEst).dialog("close");
}
//Funcion que arma la lista ORGINAL Y DESTINO PARA EDITAR LICENCIAS

function ArmaListasTemporalesParaEditar() {
    ReglaValor = null;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Establecimiento/EstablecimientoSocEmpArmaTemporalesOriginal',
        data: ReglaValor,
        //codigo malo..-:"
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //$("#gridEstablecimientoFinal").html(dato.message);
                LoadListarEstablecimientoSocioEmpresarialSeg();
                //$("#CantidadRegistros").html(dato.Code);
            } else if (dato.result == 0) {
                $("#gridEstablecimientoFinal").html('');
                //$("#CantidadRegistros").html('0');
                alert(dato.message);
            }
        }
    });

}
function ConsultarEstablecimientos(soc) {
    var LicMas = $("#hidLicMaster").val();
    //alert("1");
    $.ajax({
        url: '../Establecimiento/ConsultaEstablecimientoSocioEmpresarial',
        type: 'POST',
        data: {
            Socio: soc, LicMas: LicMas

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //LoadListarEstablecimientoSocioEmpresarialSeg();
                //alert("listo 2");
            } else if (dato.result == 0) {
                $("#gridEstablecimientoOriginal").html('');
                alert(dato.message);
            }
        }
    });
}
function ConsultarEstSocEmprGrab(soc, licmas) {
    $.ajax({
        url: '../Establecimiento/ConsultaEstablecimientosSocEmprGrabados',
        type: 'POST',
        data: {
            //VARIBALE DEL CONTROLLER : VARIABLE DECLARADA EN JQUERY 
            Socio: soc,
            licmas: licmas

        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                //alert("2");
                //LoadListarEstablecimientoSocioEmpresarialSeg();
                ArmaListasTemporalesParaEditar();
            } else if (dato.result == 0) {
                $("#gridEstablecimientoOriginal").html('');
                alert(dato.message);
            }
        }
    });
}
//Inactiva las licencias de la Ventana al seleccionarlas y pasarlas a la Izquierda
function InactivarLicenciasHijas(CodLic) {
    //  var licmas = $("#txtCodigoLicMultiple").val();
    var licmaster = $("#hidLicMaster").val();
    var Socio = $("#hidResponsable").val();

    $.ajax({
        url: '../Licencia/InactivarLicenciasHiajs',
        type: 'GET',
        data: { CodLic: CodLic, licmaster: licmaster },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert("Inactivo?");
                //Esto estaba mareando Todo .;
                // BuscarEstablecimientoSocioEmpresarial(Socio);

            } else {

            }
        }
    });
}

//Mensaje de Confirmacion si Desea Eliminar Los Establecimientos Seleccionados
function Confirmar(dialogText, OkFunc, CancelFunc, dialogTitle) {
    $('<div style="padding:10px;max-width:500px;word-wrap:break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogText,
        minHeight: 75,
        buttons: {

            SI: function () {
                if (typeof (OkFunc) == 'function') {

                    setTimeout(OkFunc, 50);
                }
                $(this).dialog('destroy');
            },
            NO: function () {
                if (typeof (CancelFunc) == 'function') {

                    setTimeout(CancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }


    });


}