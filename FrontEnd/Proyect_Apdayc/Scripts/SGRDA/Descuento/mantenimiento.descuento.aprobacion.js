var K_PAGINACION = {
    LISTAR_15: 15
};


function initPopupsDescuentoLicencia() {

    kendo.culture('es-PE');

    $("#mvAdvertencia").dialog({
        close: function (event) {
            if (event.which) { returnPage(); }
        }, closeOnEscape: true, autoOpen: false, width: 500, height: 100, modal: true
    });

    $("#mvActualizaDescuento").dialog({ autoOpen: false, width: 400, height: 300, buttons: { "Grabar": ModificaDesc, "Cancelar": function () { $(this).dialog("close"); } }, modal: true });
}

$(function () {
    initPopupsDescuentoLicencia();

    //FECHA CREACION DE DESCUENTO
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    var fechaActual = new Date();
    $("#txtFecInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();


    loadComboOficina('dllOficina', '0');
    //BOTON BUSCAR

    $("#btnBuscar").on("click",function (){
        LoadData();
    });

    $("#btnLimpiar").on("click", function () {
        Limpiar();
    });

    Limpiar();
    LoadData();

    //MOSTRAR OCULTAR TEXTO DE RESPUESTA

    //$("#txtRespuestaObs").css("display", "none");

    $("#ddlestado_desc").on("change", function () {
        if ($(this).val() == 2) {
           // $("#trocultaRespObs").css("display", "inline");
            $("#trocultaRespObs").show();
            $("#txtRespuestaObs").prop('readonly', false);
        }
        else
        {
            $("#trocultaRespObs").hide();
            $("#txtRespuestaObs").prop('readonly', true);
        }

    });

});

function LoadData() {

    //var codigoLic = $("#txtcodlic").val();
    //var codigoLic = $("#txtnomlic").val();
    //var codigoLic = $("#txtnomdesc").val();
    //var fecha_inicio = $("#txtFecInicial").val();
    //var fecha_fin = $("#txtFecFinal").val();
    //conFecha: $("#chkConFecha").is(':checked') == true ? 1 : 0

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }


    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: K_PAGINACION.LISTAR_15,
        transport: {
            read: {
                url: "../DescuentoAprobacion/LISTAR_DESCUENTOS_APROBACIONES_JSONPAGE",
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {

                        LIC_ID: $("#txtcodlic").val() == "" ? 0 : $("#txtcodlic").val(),
                        NOM_LIC: $("#txtnomlic").val(),
                        OFI_ID: $("#dllOficina").val() == 0 ? 0 : $("#dllOficina").val(),
                        EST_DESC: $("#ddlestado").val() == 0 ? 0 : $("#ddlestado").val(),
                        NOM_DESC: $("#txtnomdesc").val(),
                        CON_FECHA: $("#chkfecdesc").is(':checked') == true ? 1 : 0,
                        fecha_inicio: $("#txtFecInicial").val(),
                        fecha_fin: $("#txtFecFinal").val()
                    })
            }
        },
        schema: { data: "Descuentos", total: 'TotalVirtual' }
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
             { field: "LIC_ID", width: 7, title: "LICENCIA", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_ID}</a>" },
             //{ field: "LIC_DISC_ID", width: 7, title: "Código_Descuento_Licencia", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LIC_DISC_ID}</a>" },
             { field: "DISC_ID", width: 7, title: "Código_Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DISC_ID}</a>" },
             { field: "DISC_NAME", width: 7, title: "Nombre_Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DISC_NAME}</a>" },
              { field: "FORMATO", width: 7, title: "Formato_Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${FORMATO}</a>" },
             { field: "VALOR", width: 7, title: "Valor_Descuento", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${VALOR}</a>" },
             { field: "ORIGEN", width: 7, title: "ORIGEN", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${DISC_ORG}</a>" },
             { field: "LOG_DATE_CREACION", width: 7, title: "FECHA_CREACION", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LOG_DATE_CREACION}</a>" },
             { field: "LOG_USER_CREAT", width: 7, title: "USUARIO_CREACION", template: "<a id='single_2'  href=javascript:editar('${DISC_ID}','${LIC_ID}') style='color:gray;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a>" },
             { field: "LIC_ID", width: 5, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerLicenciVentanaNueva(${LIC_ID});'  border='0' title='Ver Licnecia En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },//Usuario Derecho
            ]
    });
}


function Limpiar() {
    $("#txtcodlic").val("");
    $("#txtnomlic").val("");
    $("#txtnomdesc").val("");
    $("#chkfecdesc").prop('checked', false);


    var fechaActual = new Date();
    $("#txtFecInicial").data("kendoDatePicker").value(fechaActual);
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, fechaActual.getDate());
    $("#txtFecFinal").data("kendoDatePicker").value(fechaFin);
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();
    $("#ddlestado").val(0);
}

function editar(lic_disc,lic_id) {
    $("#mvActualizaDescuento").dialog("open");
    $("#hidLicDisc").val(lic_disc);
    $("#hidLicId").val(lic_id);
    $('#txtmontoDesc').on("keypress", function (e) { return solonumeros(e); });
    ObtieneDatosDescuento();

}

function ModificaDesc() {

    var lic_disc_id = $("#hidLicDisc").val();
    var nombre_desc = $("#txtNomDesc").val();
    var monto_desc = $("#txtmontoDesc").val();
    var estado = $("#ddlestado_desc").val();
    var rest = $("#txtRespuestaObs").val() == "" ? "APROBADO" : $("#txtRespuestaObs").val();
    var lic_id = $("#hidLicId").val();

    if ($("#hidLicDiscEstado").val() != 1) {
        $.ajax({
            data: { DISC_ID: lic_disc_id, estado: estado, observ_respuesta: rest, LIC_ID:lic_id},
            type: 'POST',
            url: "../DescuentoAprobacion/ActualizaDescuentoPanel",
            success: function (response) {
                var dato = response;
                validarRedirect(dato);/*add sysseg*/
                if (dato.result == 1) {
                    alert("Descuento Actualizado Correctamente");
                    LoadData();
                    $("#mvActualizaDescuento").dialog("close");
                } else if (dato.result == 0) {
                    msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
                    $("#mvActualizaDescuento").dialog("close");
                }

            }
        });
    } else {
        alert("EL DESCUENTO SE ENCUENTRA APROBADO POR LO QUE NO PUEDE SER MODIFICADO");
    }


}


function ObtieneDatosDescuento() {
    //$('#txtmontoDesc').on("keypress", function (e) { return solonumeros(e); });
    var codigo = $("#hidLicDisc").val();
    var codLicencia = $("#hidLicId").val();

    $.ajax({
        data: { DISC_ID: codigo, LIC_ID: codLicencia },
        type: 'POST',
        url: "../DescuentoAprobacion/ObtenerDescuentoPanel",
        success: function (response) {
            var dato = response;
            validarRedirect(dato);/*add sysseg*/
            if (dato.result == 1) {
                var descuento = dato.data.Data;

                $("#txtRespuestaObs").val("");
                $("#txtNomDesc").val(descuento.DISC_NAME);
                $("#txtmontoDesc").val(descuento.DISC_VALUE);
                $("#hidLicDiscEstado").val(descuento.DISC_ESTADO);
                $("#txtObsResp").val(descuento.OBSERVACION);
                $("#lblformato").text(descuento.FORMATO);
                $("#ddlestado_desc").val(descuento.DISC_ESTADO);

                //if (descuento.DISC_ESTADO == 1) {
                    $('#txtNomDesc').prop('readonly', true);
                    $('#txtmontoDesc').prop('readonly', true);
                    $("#txtObsResp").prop('readonly', true);
                    $("#txtorigen").prop('readonly', true);
              //  } else {
                  //$('#txtNomDesc').prop('readonly', false);
                 // $('#txtmontoDesc').prop('readonly', false);
                 // $("#txtObsResp").prop('readonly', false);
                //}


                    if ($("#ddlestado_desc").val() == 2) {
                        // $("#trocultaRespObs").css("display", "inline");
                        $("#trocultaRespObs").show();
                        $("#txtRespuestaObs").prop('readonly', false);
                    }
                    else {
                        $("#trocultaRespObs").hide();
                        $("#txtRespuestaObs").prop('readonly', true);
                    }

            } else if (dato.result == 0) {
                msgErrorB(K_DIV_MESSAGE.DIV_TAB_DSCTO, dato.message);
            }

        }
    });

}

function VerLicenciVentanaNueva(lic_id) {

    window.open('../Licencia/Nuevo?set=' + lic_id, '_blank');
}