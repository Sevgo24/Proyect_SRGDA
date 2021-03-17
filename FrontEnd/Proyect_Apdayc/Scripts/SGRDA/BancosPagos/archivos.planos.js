$(function () {

    kendo.culture('es-PE');
    $('#txtFecInicial').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFecFinal').kendoDatePicker({ format: "dd/MM/yyyy" });

    $("#txtFecInicial").data("kendoDatePicker").value(new Date());
    var d = $("#txtFecInicial").data("kendoDatePicker").value();

    $("#txtFecFinal").data("kendoDatePicker").value(new Date());
    var dFIN = $("#txtFecFinal").data("kendoDatePicker").value();

    $('#txtFecInicial').data('kendoDatePicker').enable(true);
    $('#txtFecFinal').data('kendoDatePicker').enable(true);

    //$("input[name=valida]").hide();
    $("#contenedorLoading").hide();
    //------------------------- EVENTO BOTONES ---------------------
    $("#btnArchivoPlano").on("click", function () {
        //alert('entro');   
        document.getElementById("btnArchivoPlano").disabled = true;
       
        VAlidarArchivo()
    });
    loadData();
});
function VAlidarArchivo() {
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    $.ajax({
        url: "../ArchivosPlanosBancos/ValidarData",
        type: 'POST',
        data: { fini: ini, ffin: fin },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //validarRedirect(dato);
            if (dato.result == 1) {                
                //$("#contenedorLoading").show();
                downloadFile2();
                document.getElementById("btnArchivoPlano").disabled = false;
            } else if (dato.result == 0) {
                //$("#contenedorLoading").hide();
                alert(dato.message);
                document.getElementById("btnArchivoPlano").disabled = false;
            }
        }
    });
}
function downloadFile2() {
    //$("#contenedorLoading").show();
    var ini = $("#txtFecInicial").val();
    var fin = $("#txtFecFinal").val();
    //var dato = response;
    //validarRedirect(dato);
    //if (dato.result == 1) {

            url = "../ArchivosPlanosBancos/DescargarArchivoPlanoBanco?" + "fini=" + ini + "&" + "ffin=" + fin;
       
            window.open(url);
            loadData();
                //$("#contenedorLoading").hide();
            
      



    //var load = '../Images/otros/loading.GIF';
    //$('#externo').attr("src", load);

    //    url = "../ArchivosPlanosBancos/DescargarArchivoPlanoBanco?" + "fini=" + ini + "&" + "ffin=" + fin;
    //    window.open(url);
    //    $("#contenedor").hide();
        //$("#contenedor").show();
        //$('#externo').attr("src", url);
    //} else if (dato.result == 0) {
    //    $("#contenedor").hide();
    //    url = alert(dato.message);
    //}        
}

function loadData() {

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }
    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: 15,
        transport: {
            read: {
                url: '../ArchivosPlanosBancos/USP_GENERAR_ARCHIVO_LISTARPAGEJSON',
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {

                    })
            }
        },
        schema: { data: "ListaGenerarArchivo", total: 'TotalVirtual' }
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
             { field: "LOG_USER_CREAT", width: 8, title: "USUARIO", template: "<a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a>" },
             { field: "LOG_DATE_CREAT", width: 4, title: "FECHA ARCHIVO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${LOG_DATE_CREAT} </a></font>" },
             { field: "FECHA_INI", width: 4, title: "FECHA INICIO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_INI} </a></font>" },
             { field: "FECHA_FIN", width: 4, title: "FECHA FIN", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_FIN} </a></font>" },
             { field: "CANT_ARC", width: 2, title: "CANTIDAD", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${CANT_ARC} </a></font>" },
             { field: "MONTO_TOTAL", width: 4, title: "MONTO", template: "<a id='single_2'   style='color:gray;text-decoration:none;font-size:11px'>${MONTO_TOTAL}</a>" },
             { field: "DESC_ARC", width: 8, title: "NOMBRE ARCHIVO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${DESC_ARC} </a></font>" },
           //{ field: "TIPO_VALOR", width: 3, title: "TIPO VALOR", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${TIPO_VALOR} </a></font>" },
             //{ field: "CANAL_ENTRADA", width: 2, title: "CANAL ENTRADA", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${CANAL_ENTRADA} </a></font>" },

            ]
    });
    $("#btnCargarSistema").show();
}

