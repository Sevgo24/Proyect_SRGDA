$(function () {
    //loadDivisionTipo('ddTipoDivision', '0');
    //loadTipoLicencia('ddTipoLicencia', '0');
    //loadEstadoWF({
    //    control: 'ddEstadoLicencia',
    //    valSel: 0,
    //    idFiltro: 0,
    //    addItemAll: true
    //});
    //loadTemporalidad('ddTemporalidad', '0', 'TODOS');
    //loadTarifaAsociada('ddTarifaAsociada', '0', 'TODOS', 0);
    //loadMonedaRecaudacion('ddMoneda', '0', 'TODOS');
    $("#tdMesCierre").hide();
    $("#tdComboMesCierre").hide();

    loadComboAnio('ddlAnioCierre', '0');
    loadComboRango('ddlComboRango', '0');
    $("#ddlAnioCierre").change(function () {
        if ($("#ddlAnioCierre").val() > 0) {
            $("#tdMesCierre").show();
            $("#tdComboMesCierre").show();
            loadComboMesXAnio('ddlMesCierre', '0');
        }
        else {
            $("#tdMesCierre").hide();
            $("#tdComboMesCierre").hide();
        }
    });

    //FECHA AUTORIZACION
    //$('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    //$('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    //var fechaActual = new Date();
    //$("#txtFecInicial").data("kendoDatePicker").value(fechaActual);
    //var d = $("#txtFecInicial").data("kendoDatePicker").value();

    //var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    //$("#txtFecFinal").data("kendoDatePicker").value(fechaFin);
    //var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    var oculta = validarOficinaReporte();
    var ocultacombo = validarOficinaReportedl();


    if (ocultacombo == false) {
        $('#tddllOficina').hide();
        $('#tddllOficina2').hide();
        $('#dllOficina').prop('disabled', true);
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

    //$("#ddTipoDivision").on("change", function () {
    //    var tipo = $("#ddTipoDivision").val();
    //    loadDivisionXTipo('ddDivision', tipo, 'TODOS');
    //});

    $("#txtCodLicencia").on("keypress", function (event) {
        if (event.which == 13) {
            loadData();
        }
    });

    $("#txtNomLicencia").on("keypress", function (event) {
        if (event.which == 13) {
            loadData(event);
        }
    });

    //$("#btnNuevo").on("click", function () {
    //    location.href = "../Licencia/nuevo";
    //});
    $("#btnLimpiar").on("click", function () {
        //limpiar();
    });

    //$("#btnEliminar").on("click", function () {
    //    var values = [];
    //    $(".k-grid-content tbody tr").each(function () {
    //        var $row = $(this);
    //        var checked = $row.find('.kendo-chk').attr('checked');
    //        if (checked == "checked") {
    //            var codigo = $row.find('.kendo-chk').attr('value');
    //            values.push(codigo);
    //            EliminarLicenciaDifMult(codigo);
    //            //eliminar(codigo);
    //        }
    //    });
    //    if (values.length == 0) {
    //        alert("Seleccione usuario para eliminar.");
    //    } else {
    //        loadData();

    //    }
    //    loadData();
    //});

    $("#btnBuscar").on("click", function () {
        var anio = $("#ddlAnioCierre").val();
        var mes = $("#ddlMesCierre").val() == null ? "0" : $("#ddlMesCierre").val();
        var ID_OFICINA = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();;
        if (anio > 0) {
            if (mes > 0) {
                if (ID_OFICINA > 0) {
                    loadData();
                    ExportarReportef('PDF');
                   
                } else {
                    alert('Por favor seleccione OFICINA')
                }
            } else {
                alert('Por favor seleccione MES')
            }
        } else {
            alert('Por favor seleccione AÑO')
        }
        
    });

    $("#btnExcel").on("click", function () {
        //var estadoRequeridos = ValidarRequeridos();
        ExportarReportef2('EXCEL');
    });
    //loadData();

    //mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    //mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });
    //mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });
    //mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
    //mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRU", idDivMV: "MvBuscarGrupoFacturacion", event: "reloadEventoGrupoFact", idLabelToSearch: "lbGrupo" });
    //mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });


    //$("#txtNomLicencia").focus();
    //limpiar();
    document.getElementById("txtNomLicencia").focus();

    var K_WIDTH_OBS  = 700;
    var K_HEIGHT_OBS = 350;


    //------------------    POPUPS
    //$("#mvTablaComisiones").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: { "Agregar": addRangoComision, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#mvTablaComisiones").dialog({ autoOpen: false, width: K_WIDTH_OBS, height: K_HEIGHT_OBS, buttons: {  "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
    $("#btnTablaComision").on("click", function () { TablaRangoComision(); $("#mvTablaComisiones").dialog("open"); });

});

function loadData(e) {


    var anio = $("#ddlAnioCierre").val();
    var mes = $("#ddlMesCierre").val() == null ?"0":$("#ddlMesCierre").val();

    //var fecha_inicio = $("#txtFecInicial").val();
    //var fecha_fin = $("#txtFecFinal").val();
    var ID_OFICINA = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();;

    var TIPO_PAGO = '-1';
    var LIC_ID = $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val();

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }
    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: 10,
        transport: {
            read: {
                url: "../MatrizEmpadronamiento/USP_MatrizEmpadronamiento_LISTARPAGEJSON",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {
                        anio: anio,
                        mes: mes,
                        ID_OFICINA: ID_OFICINA,
                        TIPO_PAGO: TIPO_PAGO, 
                        LIC_ID: LIC_ID

                    })
            }
        },
        schema: { data: "ListaMatrizEmpadronamiento", total: 'TotalVirtual' }
    })


    var gridLic = $("#grid").kendoGrid({
        dataSource: data_sourceLic,
        groupable: false,
        sortable: true,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns:
           [
                //{
                //    title: '', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
                //},
                //href=javascript:verDetalleDocumento('${LIC_ID}')
             { field: "LIC_ID", width: 4, title: "COD. Lic.", template: "<a id='single_2' href=javascript:verNombreLicencia('${LIC_NAME}.replace(' ','/')')   style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
             { field: "FECHA_CREACION", width: 7, title: "Fec. Creacion ", template: "<a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'></a>${FECHA_CREACION}</font>" },
             { field: "LIC_NAME", width: 17, title: "Nombre Licencia", template: "<a id='single_2'  href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_NAME}</a>" },
             { field: "EST_ID", width: 4, title: "Cod. Est.", template: "<a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${EST_ID}</a>" },
             { field: "ESTABLECIMIENTO", width: 14, title: "Establecimiento ", template: "<font color='green'><a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${ESTABLECIMIENTO} </a></font>" },
             { field: "FECHA_EMISION", width: 7, title: "Fec.Emision", template: "<font color='green'><a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_EMISION} </a></font>" },
             { field: "DOCUMENTO", width: 7, title: "Documento", template: "<font color='green'><a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${DOCUMENTO} </a></font>" },
             { field: "PERIODO", width: 5, title: "Periodo", template: "<font color='green'><a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${PERIODO} </a></font>" },
             { field: "FECHA_CONFIRMACION", width: 6, title: "F.Confir.", template: "<font color='green'><a id='single_2' href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_CONFIRMACION} </a></font>" },
             { field: "INVL_NET", width: 6, title: "Facturado", template: "<font color='green'><a  href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${INVL_NET} </a></font>" },
             { field: "INVL_COLLECTN", width: 6, title: "Recaudado", template: "<font color='green'><a  href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${INVL_COLLECTN} </a></font>" },
             { field: "NODO", width: 10, title: "Oficina", template: "<font color='green'><a  href=javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${NODO} </a></font>" },
             { field: "PAGOS", width: 4, title: "Pagos", template: "<font color='green'><a  href}javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${PAGOS} </a></font>" },
             { field: "PAGO_EMPADRONAMIENTO", width:5, title: "Comision", template: "<font color='green'><a  href}javascript:verDetalleDocumento('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${PAGO_EMPADRONAMIENTO} </a></font>" },

             //{ field: "LIC_ID", width: 5, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerLicenciVentanaNueva(${LIC_ID});'  border='0' title='Ver Licnecia En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho
           ]
    });
}

function loadComboAnio(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../MatrizEmpadronamiento/ListarAnios',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, AnioCierre) {
                    if (AnioCierre.Value == valSel)
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}
function loadComboMesXAnio(control, valSel) {
    var anio = $("#ddlAnioCierre").val();
    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../MatrizEmpadronamiento/ListarMeses',
        type: 'POST',
        data: { anio: anio },
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, AnioCierre) {
                    if (AnioCierre.Value == valSel)
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: AnioCierre.Value, text: AnioCierre.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function verDetalleDocumento(id) {
    var url = '../DetalleEmpadronamiento/Index?id=' + id;
    window.open(url, "_blank");
}
function verNombreLicencia(nombre){
    alert(nombre);
}

function addRangoComision(){
    var idRango = $("#ddlComboRango").val();

    var monto_desde = $("#txtMontoDesde").val();
    var monto_hasta = $("#txtMontoHasta").val();
    var porcentaje = $("#txtPorcentaje").val();
    //alert(idRango + '-' + montoDesde + '-' + montoHasta+'-'+  porcentaje)

    if (idRango != 0) {
        $.ajax({
            url: '../MatrizEmpadronamiento/InsertarRangoComision',
            type: 'POST',
            data: { idRango: idRango, monto_desde: monto_desde, monto_hasta: monto_hasta, porcentaje: porcentaje },
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                //validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    TablaRangoComision();
                    loadComboRango('ddlComboRango', '0');
                } else if (dato.result == 0) {

                    alert(dato.message);
                }
            }
        });
    } else {
        alert('Sin codigo de tipo rango');
    }

  }

function TablaRangoComision() {
    
    $.ajax({
        url: '../MatrizEmpadronamiento/Listar_TablaComision',
        type: 'POST',
        data: { },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#gridTablaComision").html(dato.message);
            } else if (dato.result == 0) {
                $("#gridTablaComision").html('');
                alert(dato.message);
            }
        }
    });
}

function eliminarRangoComision(id) {

    Confirmar(' ¿Confirmas eliminar el rango de comision?',
       function () {
           $.ajax({
               url: '../MatrizEmpadronamiento/EliminarRangoComision',
               type: 'POST',
               data: { id: id },
               beforeSend: function () { },
               success: function (response) {
                   var dato = response;
                   //validarRedirect(dato);
                   if (dato.result == 1) {
                       alert(dato.message);
                       TablaRangoComision();
                       loadComboRango('ddlComboRango', '0');
                   } else if (dato.result == 0) {

                       alert(dato.message);
                   }
               }
           });
           'Confirmar'
       })
}

function validaNumericos(txt) {
    var inputtxt = document.getElementById(txt);
    var valor = inputtxt.value;
    for (i = 0; i < valor.length; i++) {
        var code = valor.charCodeAt(i);
        if (code <= 48 || code >= 57) {
            inputtxt.value = "";
            return;
        }
    }

}

function loadComboRango(control, valSel) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    $.ajax({
        url: '../MatrizEmpadronamiento/ComboRangoComision',
        type: 'POST',
        beforeSend: function (idDependencia) { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, Rango) {
                    if (Rango.Value == valSel)
                        $('#' + control).append($("<option />", { value: Rango.Value, text: Rango.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: Rango.Value, text: Rango.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function Confirmar(dialogText, okFunc, cancelFunc, dialogTitle) {
    $('<div style="padding: 10px; max-width: 500px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
        draggable: false,
        modal: true,
        resizable: false,
        ewidth: 'auto',
        title: dialogTitle,
        minHeight: 75,
        buttons: {
            Si: function () {
                if (typeof (okFunc) == 'function') {
                    setTimeout(okFunc, 50);
                }
                $(this).dialog('destroy');
            },
            No: function () {
                if (typeof (cancelFunc) == 'function') {
                    setTimeout(cancelFunc, 50);
                }
                $(this).dialog('destroy');
            }
        }
    });
}

function ExportarReportef(tipo) {
    var anio = $("#ddlAnioCierre").val();
    var mes_nombre = $("#ddlMesCierre option:selected").text();

    //var validafecha = validate_fechaMayorQue(ini, fin);
    var idoficina = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    var vacio = "";

    var nombreoficina = $("#dllOficina option:selected").text();

    var mes = $("#ddlMesCierre").val() == null ? "0" : $("#ddlMesCierre").val();

    //var fecha_inicio = $("#txtFecInicial").val();
    //var fecha_fin = $("#txtFecFinal").val();
    var ID_OFICINA = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();;

    var TIPO_PAGO = '-1';
    var LIC_ID = $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val();

    var nombreoficina = $("#dllOficina option:selected").text();
    //if (validafecha == 1) {
    $("#contenedor").show();
    $.ajax({
        url: '../MatrizEmpadronamiento/ReporteTipo',
        type: 'POST',
        data: {
            anio: anio,
            mes: mes,
            ID_OFICINA: ID_OFICINA,
            LIC_ID: LIC_ID
        },
        beforeSend: function () {
            var load = '../Images/otros/loading.GIF';
            $('#externo').attr("src", load);
        },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                url = "../MatrizEmpadronamiento/ReporteDeEmpadronamiento?" +
      "MES=" + mes_nombre + "&" +
      "ANIO=" + anio + "&" +
      "formato=" + tipo + "&" +
      "ID_OFICINA=" + idoficina + "&" +
      "nombreoficina=" + nombreoficina;
                $("#contenedor").show();
                $('#externo').attr("src", url);
            } else if (dato.result == 0) {
                $('#externo').attr("src", vacio);
                $("#contenedor").hide();
                url = alert(dato.message);
            }
        }
    });
    //} else {
    //    $("#contenedor").hide();
    //}
}

function ExportarReportef2(tipo) {

    var anio = $("#ddlAnioCierre").val();
    var mes_nombre = $("#ddlMesCierre option:selected").text();

    //var validafecha = validate_fechaMayorQue(ini, fin);
    var idoficina = $("#dllOficina").val() == null ? 1 : $("#dllOficina").val();
    var vacio = "";

    var nombreoficina = $("#dllOficina option:selected").text();

    var mes = $("#ddlMesCierre").val() == null ? "0" : $("#ddlMesCierre").val();

    //var fecha_inicio = $("#txtFecInicial").val();
    //var fecha_fin = $("#txtFecFinal").val();
    var ID_OFICINA = $("#dllOficina").val() == null ? "0" : $("#dllOficina").val();;

    var TIPO_PAGO = '-1';
    var LIC_ID = $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val();


    //$("#contenedor").show();
    //var load = '../Images/otros/loading.GIF';
    //$('#externo').attr("src", load);
    var url = "../MatrizEmpadronamiento/ReporteDeEmpadronamiento?" +
        "MES=" + mes_nombre + "&" +
          "ANIO=" + anio + "&" +
          "formato=" + tipo + "&" +
          "ID_OFICINA=" + idoficina + "&" +
          "nombreoficina=" + nombreoficina;


    $.ajax({
        url: '../MatrizEmpadronamiento/ReporteTipo',
        type: 'POST',
        data: {
            anio: anio,
            mes: mes,
            ID_OFICINA: ID_OFICINA,
            LIC_ID: LIC_ID
        },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                window.open(url);
                //$("#contenedor").hide();
                //$('#externo').attr("src", vacio);

            } else if (dato.result == 0) {
            //    $("#contenedor").hide();
            //    $('#externo').attr("src", vacio);
                url = alert(dato.message);
            }
        }
    });

    //} else {
    //    $("#contenedor").hide();
    //}
}