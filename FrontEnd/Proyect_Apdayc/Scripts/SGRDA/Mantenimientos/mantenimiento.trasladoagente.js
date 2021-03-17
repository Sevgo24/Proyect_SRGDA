var idOficina = 0;
var idAgente = 0;

$(function () {

    $("#hidCodigoBPS").val(0);
    mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });

    //$("#txtNroIdentificacion").attr("disabled", "disabled");

    //$("#ddlTipoIdentificacion").on("change", function () {
    //    $("#hidExitoValNumero").val("0");
    //    msgErrorB("divResultValidarDoc", "");

    //    if ($(this).val() == 0) {
    //        $("#txtNroIdentificacion").attr("disabled", "disabled");
    //        $("#txtNombre").removeAttr("disabled");
    //        $("#txtNroIdentificacion").val("");
    //    }
    //    else {
    //        $("#txtNroIdentificacion").removeAttr("disabled");
    //        $("#txtNombre").attr("disabled", "disabled");
    //        $("#txtNombre").val("");
    //        getValorConfigNumDoc($("#ddlTipoIdentificacion").val());
    //    }
    //});

    //$("#btnBuscar").on("click", function () {

    //    if ($("#ddlTipoIdentificacion option:selected").index() != 0) {
    //        var resultado = consultarDocumento();
    //        if (resultado) {

    //            //var idsocioBPS = $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val();
    //            var idsocioBPS = $("#hidCodigoBPS").val();
    //            alert(idsocioBPS);

    //            $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({ agente: idsocioBPS, page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    //            msgErrorB("divValidacion", "");
    //        }
    //    }
    //    else {
    //        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({ agente: idsocioBPS, page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    //        msgErrorB("divValidacion", "");
    //    }
    //});


    $("#btnBuscar").on("click", function () {
        //var idsocioBPS = $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val();
        //alert(idsocioBPS);


        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({ agente: $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
        msgErrorB("divValidacion", "");
    });


    $("#btnLimpiar").on("click", function () {
        $("#hidCodigoBPS").val(0);
        $("#lbResponsable").html("Seleccione");
        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({ agente: $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val(), page: 1, pageSize: K_PAGINACION.LISTAR_20 });
        msgErrorB("divValidacion", "");
    });

    $("#btnNuevo").on("click", function () {
        if ($("#hidCodigoBPS").val() != "0") {
            nuevo($("#hidCodigoBPS").val());
            msgErrorB("divValidacion", "");
        }
        else {
            msgErrorB("divValidacion", "Busque al agente recaudador.");
        }
    });

    //loadTipoIdentificacion(0);
    //initAutoCompletarAgenterecaudador("txtNombre", "hidCodigoBPS");

    //$("#txtNroIdentificacion").on("keypress", function (e) {
    //    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    //    if (key == 13) {
    //        consultarDocumento();
    //    }
    //});

    //$("#txtNombre").on("keypress", function (e) {
    //    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    //    if (key == 13) {
    //        var idsocioBPS = $("#hidCodigoBPS").val();
    //        $('#gridAgenteRecaudo').data('kendoGrid').dataSource.query({ agente: idsocioBPS, page: 1, pageSize: K_PAGINACION.LISTAR_15 });
    //    }
    //});

    //$(document).ready(function () {
    //    $("#txtNroIdentificacion").keydown(function (event) {
    //        if (event.shiftKey) {
    //            event.preventDefault();
    //        }

    //        if (event.keyCode == 46 || event.keyCode == 8) {
    //        }
    //        else {
    //            if (event.keyCode < 95) {
    //                if (event.keyCode < 48 || event.keyCode > 57) {
    //                    event.preventDefault();
    //                }
    //            }
    //            else {
    //                if (event.keyCode < 96 || event.keyCode > 105) {
    //                    event.preventDefault();
    //                }
    //            }
    //        }
    //    });
    //});

    loadDataTrasladoAgentes();
});

var reloadEvento = function (idSel) {
    //alert(idSel);
    $("#hidCodigoBPS").val(idSel);
    //var estado = ObtenerNivelAgente($("#hidCodigoBPS").val());
    //if (estado) {
    obtenerNombreSocio($("#hidCodigoBPS").val());
    msgErrorB("divValidacion", "");
    //};
};

function obtenerNombreSocio(idSel) {
    $.ajax({
        data: { codigoBps: idSel },
        url: '../General/ObtenerNombreSocio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbResponsable").html(dato.valor);
            }
        }
    });
}

function consultarDocumento() {
    var resultado = false;
    var estado = validarLongitudNumDoc();
    if (estado) {
        resultado = true;
        ObtenerAgenterecaudador();
    }
    if (resultado) {
        return true;
    } else {
        return false;
    }
}

function nuevo(idSel) {
    document.location.href = '../TrasladoAgentesRecaudo/Nuevo?id=' + idSel;
}

function editar(idSel, idSel2) {
    idOficina = idSel;
    idAgente = idSel2;

    var estado = ValidarTrasladoAgente();
    if (estado) {
        $("#hidOpcionEdit").val(1);
        document.location.href = '../TrasladoAgentesRecaudo/Editar?id=' + idSel + '&idAgente=' + idSel2;
    }
}

//function editar(idSel, idSel2) {
//    document.location.href = '../ComisionProducto/Nuevo?id=' + idSel + '&idNivAgent=' + idSel2;
//    $("#hidOpcionEdit").val(1);
//}


function ObtenerAgenterecaudador() {

    var idtipo = $("#ddlTipoIdentificacion").val();
    var doc = $("#txtNroIdentificacion").val();

    $.ajax({
        url: '../General/BuscarAgenterecaudadorTipoDocumento',
        type: 'POST',
        data: { idTipoDocumento: idtipo, nroDocumento: doc },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $("#hidCodigoBPS").val(dato.Code);
                $("#txtNombre").val(dato.valor);

                alert($("#hidCodigoBPS").val());

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

function ValidarTrasladoAgente() {

    var result = false;
    var id = idOficina;

    $.ajax({
        url: '../General/ValidarTrasladoAgente',
        type: 'POST',
        data: { oficinaId: id },
        async: false,
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                result = true;
            } else {
                result = false;
                alert(dato.message);
            }
        }
    });
    return result;
}

function loadDataTrasladoAgentes() {
    //var idsocioBPS = $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val();
    //var idsocioBPS = $("#hidCodigoBPS").val();
    //var idsocioBPS = $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val();
    $("#gridAgenteRecaudo").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_20,
            transport: {
                read: {
                    type: "POST",
                    url: "../TrasladoAgentesRecaudo/usp_listar_TrasladoAgentesRecaudoJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, { agente: $("#hidCodigoBPS").val() == "" ? 0 : $("#hidCodigoBPS").val() })
                }
            },
            schema: { data: "Traslado", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
            {
                hidden: true,
                field: "OWNER", width: 10, title: "PROPIETARIO", template: "<a id='single_2'  href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${OWNER}</a>"
            },
            {
                hidden: true,
                field: "BPS_ID", width: 10, title: "IdAgente", template: "<a id='single_2' href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${BPS_ID}</a>"
            },
            {
                field: "BPS_NAME", width: 100, title: "Agente", template: "<a id='single_2' href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${BPS_NAME}</a>"
            },
            {
                hidden: true,
                field: "OFF_ID", width: 20, title: "Id", template: "<a id='single_2' href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${OFF_ID}</a>"
            },
            {
                field: "OFF_NAME", width: 150, title: "Oficina", template: "<font color='green'><a id='single_2'  href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${OFF_NAME}</a>"
            },
            {
                field: "START",
                width: 30,
                type: "date",
                title: "Inicio",
                template: "<font color='green'><a id='single_2' href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>" + '#=(START==null)?"":kendo.toString(kendo.parseDate(START,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>"
            },
            {
                field: "ENDS",
                width: 30,
                type: "date",
                title: "Fin",
                template: "<font color='green'><a id='single_2' href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>" + '#=(ENDS==null)?"":kendo.toString(kendo.parseDate(ENDS,"dd/MM/yyyy"),"dd/MM/yyyy") #' + "</a>"

            },
            {
                field: "DESCRIPTION", width: 60, title: "Nivel", template: "<font color='green'><a id='single_2'  href=javascript:editar('${OFF_ID}','${BPS_ID}') style='color:gray'>${DESCRIPTION}</a>"
            },
           ]
    });
}

//function validarLongitudNumDoc() {
//    msgError("");
//    var exito = false;
//    var tipoDoc = $("#ddlTipoIdentificacion option:selected").val();
//    var tipoDocDesc = $("#ddlTipoIdentificacion option:selected").text();

//    getValorConfigNumDoc(tipoDoc);
//    var numValidar = $("#hidCantNumValidar").val();

//    if ($.trim($("#txtNroIdentificacion").val()) != "") {
//        if (tipoDocDesc == "DNI") {
//            if ($("#txtNroIdentificacion").val().length != numValidar) {
//                msgErrorB("divResultValidarDoc", "Longitud del DNI debe contener " + numValidar + " digitos.");
//            } else {
//                exito = true;
//                msgErrorB("divResultValidarDoc", "");
//            }
//        } else {
//            if ($("#txtNroIdentificacion").val().length != numValidar) {
//                msgErrorB("divResultValidarDoc", "Longitud del RUC debe contener " + numValidar + " digitos.");
//            } else {
//                exito = true;
//                msgErrorB("divResultValidarDoc", "");
//            }
//        }
//        if (exito) {
//            return true;
//        } else {
//            return false;
//        }
//    } else {
//        msgErrorB("divResultValidarDoc", "Ingrese número de documento.");
//    }
//}

//function getValorConfigNumDoc(itipo) {
//    $.ajax({
//        url: '../General/GetConfigTipoDocumento',
//        data: { tipo: itipo },
//        type: 'POST',
//        success: function (response) {
//            var dato = response;
//            if (dato.result == 1) {
//                $("#hidCantNumValidar").val(dato.valor);
//            } else {
//                alert(dato.message);
//            }
//        }
//    });
//}