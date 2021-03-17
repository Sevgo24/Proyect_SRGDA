var K_WIDTH_OBS2_LIC = 600;
var K_HEIGHT_OBS2_LIC = 325;
var K_INACTIVA_LICENCIA = 1;

$(function () {
    loadDivisionTipo('ddTipoDivision', '0');
    loadTipoLicencia('ddTipoLicencia', '0');
    loadEstadoWF({
        control: 'ddEstadoLicencia',
        valSel: 0,
        idFiltro: 0,
        addItemAll: true
    });
    loadTemporalidad('ddTemporalidad', '0', 'TODOS');
    loadTarifaAsociada('ddTarifaAsociada', '0', 'TODOS', 0);
    loadMonedaRecaudacion('ddMoneda', '0', 'TODOS');
    loadTipoInactivacionLicencia('ddltipoInact', 0);
    //FECHA AUTORIZACION
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    $("#txtFecInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $("#ddTipoDivision").on("change", function () {
        var tipo = $("#ddTipoDivision").val();
        loadDivisionXTipo('ddDivision', tipo, 'TODOS');
    });

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

    $("#btnNuevo").on("click", function () {
        location.href = "../Licencia/nuevo";
    });
    $("#btnLimpiar").on("click", function () {
        limpiar();
    });

    $("#btnEliminar").on("click", function () {
        var cantidad = ValidarLicenciassMarcados();
        if (cantidad == 1) {

            var values = [];
            $(".k-grid-content tbody tr").each(function () {
                var $row = $(this);
                var checked = $row.find('.kendo-chk').attr('checked');
                if (checked == "checked") {
                    var codigo = $row.find('.kendo-chk').attr('value');
                    //values.push(codigo);
                    $("#hidLicReq").val(codigo);
                    ObtieneDatosRequerimiento();
                    $("#mvSolicitudRequeLic").dialog("open");
                    //EliminarLicenciaDifMult(codigo);
                    //eliminar(codigo);
                }
            });
           
            loadData();
        } else if (cantidad == 0) {

            alert("Seleccione al menos una Licencia.");
        } else {
            alert("Seleccione solo una Licencia");
        }
    });

    $("#btnBuscar").on("click", function () {
        loadData();
    });

    //loadData();

    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    mvInitModalidadUso({ container: "ContenedormvBuscarModalidad", idButtonToSearch: "btnBuscarMOD", idDivMV: "mvBuscarModalidad", event: "reloadEventoModalidad", idLabelToSearch: "lbModalidad" });
    mvInitEstablecimiento({ container: "ContenedormvEstablecimiento", idButtonToSearch: "btnBuscarEstablecimiento", idDivMV: "mvEstablecimiento", event: "reloadEventoEst", idLabelToSearch: "lblEstablecimiento" });
    mvInitBuscarSocioEmpresarial({ container: "ContenedorMvSocioEmpresarial", idButtonToSearch: "btnBuscarGrupoEmpresarial", idDivMV: "MvSocioEmpresarial", event: "reloadEventoSocEmp", idLabelToSearch: "lblGrupoEmpresarial" });
    mvInitBuscarGrupoF({ container: "ContenedormvBuscarGrupoFacuracion", idButtonToSearch: "btnBuscarGRU", idDivMV: "MvBuscarGrupoFacturacion", event: "reloadEventoGrupoFact", idLabelToSearch: "lbGrupo" });
    mvInitArtista({ container: "ContenedormvArtista", idButtonToSearch: "btnBuscarArtista", idDivMV: "mvArtistas", event: "reloadEventoArt", idLabelToSearch: "lblArtista" });

    
    //$("#txtNomLicencia").focus();
    limpiar();
    document.getElementById("txtNomLicencia").focus();

    $("#ddltiporequerimiento").change(function () {
        if ($("#ddltiporequerimiento").val() == 1) {

            $("#trlicenciaTipoInact").show();
        }
        else {
            $("#trlicenciaTipoInact").val(0);
            $("#trlicenciaTipoInact").hide();


        }
    });

});


function limpiar() {
    $("#txtCodLicencia").val("");
    $("#hidModalidad").val("");
    $("#hidEstablecimiento").val("");
    $("#hidResponsable").val("");
    $("#hidCodigoGrupoEmpresarial").val("");
    $("#txtNomLicencia").val("");
    $("#lbModalidad").html("Todos");
    $("#lblEstablecimiento").html("Todos");
    $("#lbResponsable").html("Todos");
    $("#txtCodMult").val("");
    $("#lblGrupoEmpresarial").html("SELECCIONE UN GRUPO COMERCIAL");
    $("#hidGrupoFacturacion").val("");
    $("#lbGrupo").html("SELECCIONE GRUPO");
    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    $("#chkConFecha").prop('checked', false);
    $("#txtDescArtista").val("");
    $("#lblArtista").html("SELECCIONE ARTISTA");
    $("#hidArtistaSel").val("");
    $("#ddEstadoLic").prop('selectedIndex', 1);
    $("#hidLicReq").val("");

    //loadDivisionTipo('ddTipoDivision', '0');
    //loadTipoLicencia('ddTipoLicencia', '0');
    //loadEstadoWF({
    //    control: 'ddEstadoLicencia',
    //    valSel: 0,
    //    idFiltro: 0,
    //    addItemAll: true
    //});
    //loadTemporalidad('ddTemporalidad', '0', 'TODOS');
    //loadMonedaRecaudacion('ddMoneda', '0', 'TODOS');

    $("#ddEstadoLicencia").prop('selectedIndex', 0);
    $("#ddTemporalidad").prop('selectedIndex', 0);
    $("#ddMoneda").prop('selectedIndex', 0);
    $("#ddTipoDivision").prop('selectedIndex', 0);
    $("#ddTipoLicencia").prop('selectedIndex', 0);


    $("#mvSolicitudRequeLic").dialog({
        autoOpen: false,
        width: K_WIDTH_OBS2_LIC,
        height: K_HEIGHT_OBS2_LIC,
        buttons: {
            "Grabar": RegistrarRequerimiento,
            "Cancelar": function () {
                //$('#ddltipoAprobacion').val(0);
                $("#mvSolicitudRequeLic").dialog("close");
                //$('#txtAprobacionDesc').css({ 'border': '1px solid gray' });
            }
        },
        modal: true
    });

    //loadGrid();

    loadTipoRquerimiento('ddltiporequerimiento', 1,1);

};

function eliminar(idBPS) {

    var codigosDel = { codigo: idBPS };
    $.ajax({
        url: '../Licencia/Eliminar',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result != 1) {
                alert(dato.message);
            }
        }
    });
}

var reloadEvento = function (idSel) {
    //alert("Selecciono ID:   " + idSel);
    $("#hidResponsable").val(idSel);
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                //alert(dato.valor);
                $("#lblResponsable").html(dato.valor);
            }
        }

    });

};
var reloadEventoModalidad = function (idSel) {
    $("#hidModalidad").val(idSel);
    obtenerNombreModalidad(idSel, "lbModalidad");
    //obtenerNombreTarifaLabels(idSel, "lblTarifaDesc", "lblTemporalidadDesc");

};
var reloadEventoEst = function (idSel) {    
    $("#hidEstablecimiento").val(idSel);
    ObtenerNombreEstablecimiento(idSel, "lblEstablecimiento");
    //ObtenerRespXEstablecimiento(idSel, "lblResponsable", "hidResponsable");
};

function loadData(e) {

    var fecha_inicio = $("#txtFecInicial").val();
    var fecha_fin = $("#txtFecFinal").val();

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }

    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        transport: {
            read: {
                url: "../Licencia/USP_LICENCIAMIENTO_LISTARPAGEJSON",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {

                        LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
                        LIC_TYPE: $("#ddTipoLicencia").val(),
                        LICS_ID: $("#ddEstadoLicencia").val(),
                        CUR_ALPHA: $("#ddMoneda").val(),
                        MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
                        EST_ID: $("#hidEstablecimiento").val() == "" ? 0 : $("#hidEstablecimiento").val(),
                        BPS_ID: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
                        LIC_NAME: $("#txtNomLicencia").val(),
                        LIC_TEMP: $("#ddTemporalidad").val(),
                        RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
                        LICMAS: $("#txtCodMult").val() == "" ? 0 : $("#txtCodMult").val(),
                        BPS_GROUP: $("#hidCodigoGrupoEmpresarial").val() == "" ? 0 : $("#hidCodigoGrupoEmpresarial").val(),
                        BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
                        conFecha: $("#chkConFecha").is(':checked') == true ? 1 : 0,
                        finiauto: $("#txtFecInicial").val(),
                        ffinauto: $("#txtFecFinal").val(),
                        desc_artista: $("#txtDescArtista").val(),
                        cod_artista_sgs: $("#hidArtistaSel").val() == "" ? -1 : $("#hidArtistaSel").val(),
                        estadoLic: $("#ddEstadoLic").val()
                    })
            }
        },
        schema: { data: "ListaLicencias", total: 'TotalVirtual' }
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
                {
                    title: '', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
                },
             { field: "LIC_ID", width: 7, title: "Código", template: "<a id='single_2'  href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
             { field: "LIC_TYPE", width: 15, title: "Tipo ", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_TDESC} </a></font>" },
             { field: "LIC_NAME", width: 28, title: "Nombre", template: "<a id='single_2'  href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_NAME}</a>" },
             //{ field: "LIC_MASTER", width: 7, title: "Lic. Multiple", template: "<a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:6px'>#if(LIC_MASTER != '0'){# ${LIC_MASTER} #} #  </a>" },
             { field: "BPS_ID", width: 20, title: "Responsable ", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${BPS_NAME} </a></font>" },
             { field: "LIC_AUT", width: 15, title: "Autorizacion Inicio", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_AUT_START} </a></font>" },
             { field: "LIC_AUT_END", width: 15, title: "Autorizacion Fin", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_AUT_END} </a></font>" },
             { field: "MOD_ID", width: 20, title: "Modalidad", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${MOD_DEC} </a></font>" },
             { field: "LICS_ID", width: 10, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${WRKF_SLABEL} </a></font>" },
             { field: "EST_ID", width: 18, title: "Establecimiento", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${EST_NAME} </a></font>" },
             { field: "LIC_ID", width: 5, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerLicenciVentanaNueva(${LIC_ID});'  border='0' title='Ver Licnecia En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho
            ]
    });
}

//function loadData() {

//    var fecha_inicio = $("#txtFecInicial").val();
//    var fecha_fin = $("#txtFecFinal").val();
//    //alert(fecha_inicio + '-' + fecha_fin);
//    var grilla = $("#grid").kendoGrid({
//        dataSource: {
//            type: "json",
//            serverPaging: true,
//            pageSize: K_PAGINACION.LISTAR_15,
//            transport: {
//                read: {
//                    url: "../Licencia/USP_LICENCIAMIENTO_LISTARPAGEJSON",
//                    dataType: "json"
//                },
//                parameterMap: function (options, operation) {
//                    if (operation == 'read')
//                        return $.extend({}, options, {

//                            LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
//                            LIC_TYPE: $("#ddTipoLicencia").val(),
//                            LICS_ID: $("#ddEstadoLicencia").val(),
//                            CUR_ALPHA: $("#ddMoneda").val(),
//                            MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
//                            EST_ID: $("#hidEstablecimiento").val() == "" ? 0 : $("#hidEstablecimiento").val(),
//                            BPS_ID: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
//                            LIC_NAME: $("#txtNomLicencia").val(),
//                            LIC_TEMP: $("#ddTemporalidad").val(),
//                            RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
//                            LICMAS: $("#txtCodMult").val() == "" ? 0 : $("#txtCodMult").val(),
//                            BPS_GROUP: $("#hidCodigoGrupoEmpresarial").val() == "" ? 0 : $("#hidCodigoGrupoEmpresarial").val(),
//                            BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
//                            conFecha: $("#chkConFecha").is(':checked') == true ? 1 : 0,
//                            finiauto: $("#txtFecInicial").val(),
//                            ffinauto: $("#txtFecFinal").val(),
//                            desc_artista: $("#txtDescArtista").val(),
//                            cod_artista_sgs: $("#hidArtistaSel").val() == "" ? -1 : $("#hidArtistaSel").val(),
//                        })
//                }
//            },
//            schema: { data: "ListaLicencias", total: 'TotalVirtual' }
//        },
//        groupable: false,
//        sortable: true,
//        pageable: {
//            messages: {
//                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
//                empty: "No se encontraron registros"
//            }
//        },
//        selectable: true,
//        columns:
//    [
//        {
//            title: '', width: 5, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${LIC_ID}'/>"
//        },
//     { field: "LIC_ID", width: 7, title: "Código", template: "<a id='single_2'  href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
//     { field: "LIC_TYPE", width: 15, title: "Tipo ", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_TDESC} </a></font>" },
//     { field: "LIC_NAME", width: 28, title: "Nombre", template: "<a id='single_2'  href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_NAME}</a>" },
//     //{ field: "LIC_MASTER", width: 7, title: "Lic. Multiple", template: "<a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:6px'>#if(LIC_MASTER != '0'){# ${LIC_MASTER} #} #  </a>" },
//     { field: "BPS_ID", width: 20, title: "Responsable ", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${BPS_NAME} </a></font>" },
//     { field: "LIC_AUT", width: 15, title: "Autorizacion Inicio", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_AUT_START} </a></font>" },
//     { field: "LIC_AUT_END", width: 15, title: "Autorizacion Fin", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${LIC_AUT_END} </a></font>" },
//     { field: "MOD_ID", width: 20, title: "Modalidad", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${MOD_DEC} </a></font>" },
//     { field: "LICS_ID", width: 10, title: "Estado", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${WRKF_SLABEL} </a></font>" },
//     { field: "EST_ID", width: 18, title: "Establecimiento", template: "<font color='green'><a id='single_2' href=javascript:editar('${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'> ${EST_NAME} </a></font>" },
//     { field: "LIC_ID", width: 5, title: 'Ver',  headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerLicenciVentanaNueva(${LIC_ID});'  border='0' title='Ver Licnecia En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho
//    ]

//    }).data("kendoGrid");


//}

function loadModalidades(control, valSel, etiqueta) {
    $('#' + control + ' option').remove();

    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: 'SELECCIONE' }));
    }

    $.ajax({

        url: '../General/ListaModalidades',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadEstablecimiento(control, valSel, etiqueta) {
    $('#' + control + ' option').remove();
    if (etiqueta != undefined) {
        $('#' + control).append($("<option />", { value: 0, text: etiqueta }));
    } else {
        $('#' + control).append($("<option />", { value: 0, text: 'SELECCIONE' }));
    }
    $.ajax({
        url: '../General/ListaEstablecimiento',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, valor) {
                    if (valor.Value == valSel)
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: valor.Value, text: valor.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function editar(idSel) {
    location.href = "../Licencia/Nuevo?set=" + idSel;
}

function loadGrid() {

    var fecha_inicio = $("#txtFecInicial").val();
    var fecha_fin = $("#txtFecFinal").val();
    //alert(fecha_inicio + '-' + fecha_fin);

    $('#grid').data('kendoGrid').dataSource.query({
        LIC_ID: $("#txtCodLicencia").val() == "" ? 0 : $("#txtCodLicencia").val(),
        LIC_TYPE: $("#ddTipoLicencia").val(),
        LICS_ID: $("#ddEstadoLicencia").val(),
        CUR_ALPHA: $("#ddMoneda").val(),
        MOD_ID: $("#hidModalidad").val() == "" ? 0 : $("#hidModalidad").val(),
        EST_ID: $("#hidEstablecimiento").val() == "" ? 0 : $("#hidEstablecimiento").val(),
        BPS_ID: $("#hidResponsable").val() == "" ? 0 : $("#hidResponsable").val(),
        LIC_NAME: $("#txtNomLicencia").val(),
        LIC_TEMP: $("#ddTemporalidad").val(),
        RATE_ID: $("#ddTarifaAsociada").val() == "" ? 0 : $("#ddTarifaAsociada").val(),
        LICMAS: $("#txtCodMult").val() == "" ? 0 : $("#txtCodMult").val(),
        BPS_GROUP_FACT: $("#hidGrupoFacturacion").val() == "" ? 0 : $("#hidGrupoFacturacion").val(),
        conFecha: $("#chkConFecha").is(':checked') == true ? 1 : 0,
        finiauto: $("#txtFecInicial").val(),
        ffinauto: $("#txtFecFinal").val(),
        desc_artista: $("#txtDescArtista").val(),
        estadoLic:$("#ddEstadoLic").val(),
        page: 1,
        pageSize: K_PAGINACION.LISTAR_15
    });
}

//Valida Si es Licencia Multiple
function EliminarLicenciaDifMult(codigo){
    $.ajax({
        url: '../Licencia/ValidarLicenciasMultiplesHijas',
        type: 'GET',
        data: { CodLic: codigo },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                alert("No se Puede Eliminar Una Licencia Multiple Desde Esta Ventana");
            } else {
                //eliminar(codigo);
                //alert("1");
                ValidaSiEsLicenciMultiple(codigo);
            }
        }
    });
}

//Valida Si es Licencia Multiple Padre Para Elimanr en Cadena Las licencias

function ValidaSiEsLicenciMultiple(codigo) {
    $.ajax({
        url: '../Licencia/ValidarLicenciaMultiplesPadre',
        type: 'GET',
        data: { CodLic: codigo },
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                Confirmar('Esta Apunto de Eliminar Licencias Multiples ,Esta Seguro que desea Continuar?',
                     //eliminarLicPadreHija(codigo)
                     function () {
                         eliminarLicPadreHija(codigo)
                     },
                     function () { }, 'Confirmar');
                //eliminarLicPadreHija(codigo);
                //alert("Estados actualizado correctamente.");
               //Eliminar Licencia Padre
            } else {
                eliminar(codigo);
                //alert("Estados actualizado correctamente.");
            }
        }

    });

}

//Eliminar Licencias Padre y Hija
function eliminarLicPadreHija(codigo) {

    var codigosDel = { codigo: codigo };
    $.ajax({
        url: '../Licencia/EliminarLicPadreyHija',
        type: 'POST',
        data: codigosDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result != 1) {
                alert(dato.message);

            } else {
                loadData();
                alert("Estados actualizado correctamente");
            }
        }
    });
}


//Confirmar Eliminacion De Lincencia Padre Hija
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


function VerLicenciVentanaNueva(lic_id) {

    window.open('../Licencia/Nuevo?set=' + lic_id, '_blank');
}

var reloadEventoArt = function (idArtSel) {
    //msgOkB(K_DIV_MESSAGE.DIV_LICENCIA, "");
    $("#hidArtistaSel").val(idArtSel);
    obtenerNombreArtista(idArtSel, "lblArtista");
};


function RegistrarRequerimiento() {

    var EST_ID = 0;
    var ID_REQ_TYPE = $("#ddltiporequerimiento").val();
    var RAZON = $("#txtAprobacionDesc").val();
    var ACTIVO = $("#ddltiporequerimiento").val() == 1 ? 0 : 1;
    var MONTO = 0;
    var FECHA = "";
    var INV_ID = 0;
    var LIC_ID = $("#hidLicReq").val();
    var BPS_ID = 0;
    var BEC_ID = 0;
    var TIP_LIC_INACT = $("#ddltipoInact").val();

    var validaNoesLicenciaLocal = ValidalocalInactivar(LIC_ID, ID_REQ_TYPE);


    if (validaNoesLicenciaLocal) {

        $.ajax({
            data: { EST_ID: EST_ID, ID_REQ_TYPE: ID_REQ_TYPE, RAZON: RAZON, ACTIVO: ACTIVO, MONTO: MONTO, FECHA: FECHA, INV_ID: INV_ID, LIC_ID: LIC_ID, BPS_ID: BPS_ID, BEC_ID: BEC_ID, TipoInactivacion: TIP_LIC_INACT },
            url: '../AdministracionModuloRequerimientos/RegistraRequerimientoGral',
            type: 'POST',
            async: false,
            beforeSend: function () { },
            success: function (response) {
                var dato = response;
                validarRedirect(dato);
                if (dato.result == 1) {
                    alert(dato.message);
                    $("#mvSolicitudRequeLic").dialog("close");
                    //$("#ddltipoAprobacion").val(entidad.TIPO);

                } else {
                    alert(dato.message);
                }
            }
        });

    }
}


function ValidarLicenciassMarcados() {

    var values = 0;
    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
    
            values++;
            //eliminar(codigo);
        }
    });
    return values;
}

function ObtieneDatosRequerimiento() {

    var est = $("#hidLicReq").val();
    $("#txtAprobacionDesc").val("");
    $("#lbllicid").html(est);
}

function ValidalocalInactivar(CodigoLicencia,CodigoRequerimiento){
    var resp = false;

    $.ajax({
        data: { CodigoLic: CodigoLicencia, COdigoReq: CodigoRequerimiento },
        url: '../Licencia/ValidaLicenciaLocalRequerimiento',
        type: 'POST',
        async: false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                    
                resp = true;

            } else {
                alert(dato.message);
            }
        }
    });

    return resp;
}