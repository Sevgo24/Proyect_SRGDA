var K_ITEM = {
    CHOOSE: '--SELECCIONE--', ALL: '--TODOS--'
};
$(function () {
    $("input[name=valida]").hide();
    document.getElementById("frmFormulario").reset();
    LoadBancoPagos('ddlBancos', 0, 'Todos');
    loadDataArchivos_Cargados();
    //mvInitBuscarSocio({ container: "ContenedormvBuscarSocio", idButtonToSearch: "btnBuscarBS", idDivMV: "mvBuscarSocio", event: "reloadEvento", idLabelToSearch: "lbResponsable" });
    $("#frmFormulario").hide();
    document.getElementById("LabelBanco").innerHTML = "";
    $("#btnCargarSistema").hide();
    $("#btnCargarSistema").on("click", function () {
        var id = $("#ddlBancos").val();
        VAlidarArchivoxId(id);
        //CargarPagos();
    });
    $("#btnloadDataArchivos").on("click", function () {
        var id = $("#ddlBancos").val();
        loadDataArchivos_Cargados();
        //CargarPagos();
    });
    $("#ddlBancos").change(function () {
    if ($("#ddlBancos").val() > 0)
        {       
       $("#frmFormulario").show();
    }
    else {
        $("#frmFormulario").hide();
    }
       });

    $("#ddlBancos").change(function () {
    if ($("#ddlBancos").val() > 0)
        {       
        $("#frmFormulario").show();
        document.getElementById("frmFormulario").reset();
        $("#grid").html('');
        document.getElementById("LabelBanco").innerHTML = "";
        document.getElementById("lbBanco").innerHTML = "";

    }
    else {
        $("#frmFormulario").hide();
    }
       });
 });

function Limpiar() {
    document.getElementById("frmFormulario").reset();
}

$(document).ready(function () {
    //sobreescribimos el metodo submit para que envie la solicitud por ajax
    $("#frmFormulario").submit(function (e) { 
        //esto evita que se haga la petición común, es decir evita que se refresque la pagina
        e.preventDefault();    
        //FormData es necesario para el envio de archivo, 
        //y de la siguiente manera capturamos todos los elementos del formulario
        var archivoTXT = new FormData($(this)[0])       
        LeerArchivo(archivoTXT);
        //alert('entre');        
        //realizamos la petición ajax con la función de jquery
        //$.ajax({
        //    type: "POST",
        //    url: '../CargarArchivoPlano/Cargar',
        //    data: archivoTXT,
        //    contentType: false, //importante enviar este parametro en false
        //    processData: false, //importante enviar este parametro en false
        //    success: function (data) {               
        //        alert("Se capturo el archivo con éxito")
        //    }       
        //})
        //var id = $("#ddlBancos").val();
        //VAlidarBancoxId(id);
    })


})
function LeerArchivo(archivoTXT) {
 
    $.ajax({
        type: "POST",
        url: '../CargarArchivoPlano/Cargar',
        data: archivoTXT,
        contentType: false, //importante enviar este parametro en false
        processData: false, //importante enviar este parametro en false
        success: function (data) {
            var id = $("#ddlBancos").val();
            VAlidarBancoxId(id);
        }
    })   
   
}
function loadData() {
   
    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }
        var data_sourceLic = new kendo.data.DataSource({
            type: "json",
            serverPaging: true,
            pageSize:15,
            transport: {
                read: {
                    url: '../CargarArchivoPlano/USP_ARCHIVO_LISTARPAGEJSON',
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {                       

                        })
                }
            },
            schema: { data: "ListaArchivosPlanos", total: 'TotalVirtual' }
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
                 { field: "NOMBRE_CLIENTE", width: 09, title: "CLIENTE", template: "<a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'>${NOMBRE_CLIENTE}</a>" },
                 { field: "RUC", width: 4, title: "RUC", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${RUC} </a></font>" },
                 { field: "TD", width: 1, title: "TD", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${TD} </a></font>" },
                 { field: "SERIE", width: 2, title: "SERIE", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${SERIE} </a></font>" },
                 { field: "NRO", width: 2, title: "NRO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${NRO} </a></font>" },
                 { field: "IMPORTE_ORIGEN", width: 5, title: "IMP. ORIGEN", template: "<a id='single_2'   style='color:gray;text-decoration:none;font-size:11px'>${IMPORTE_ORIGEN}</a>" },
                 { field: "IMPORTE_DEPOSITADO", width: 5, title: "IMP.DEPOSITADO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${IMPORTE_DEPOSITADO} </a></font>" },
                 { field: "NORO_ID", width: 2, title: "OFICINA", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${NORO_ID} </a></font>" },
                 { field: "NRO_MOVIMIENTO", width: 4, title: "N° MOVIMIENTO", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${NRO_MOVIMIENTO} </a></font>" },
                 { field: "FECHA_PAGO", width: 3, title: "FECHA PAGO", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_PAGO} </a></font>" },
                 //{ field: "TIPO_VALOR", width: 3, title: "TIPO VALOR", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${TIPO_VALOR} </a></font>" },
                 //{ field: "CANAL_ENTRADA", width: 2, title: "CANAL ENTRADA", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${CANAL_ENTRADA} </a></font>" },

                ]
        });
        $("#btnCargarSistema").show();
  
}
function loadDataArchivos_Cargados() {

    if ($("#grid").data("kendoGrid") != undefined) {
        $("#grid").empty();
    }
    var data_sourceLic = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        pageSize: 15,
        transport: {
            read: {
                url: '../CargarArchivoPlano/USP_ARCHIVOS_CARGADOS_LISTARPAGEJSON',
                dataType: "json"
            },
            parameterMap: function (options, operation) {
                if (operation == 'read')
                    return $.extend({}, options, {

                    })
            }
        },
        schema: { data: "ListaFileBanco", total: 'TotalVirtual' }
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
             { field: "FILE_COBRO_ID", width: 1, title: "ID", template: "<a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'>${FILE_COBRO_ID}</a>" },
             { field: "LOG_USER_CREAT", width: 6, title: "Usuario", template: "<a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a>" },
             { field: "LOG_DATE_CREAT", width: 3, title: "Fecha Consulta", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${LOG_DATE_CREAT} </a></font>" },
             { field: "DESC_FILE", width: 6, title: "Nombre Archivo", template: "<a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'>${DESC_FILE}</a>" },
             { field: "TOTAL_CABECERAS", width: 2, title: "Cant. Archivo", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${TOTAL_CABECERAS} </a></font>" },
             { field: "TOTAL", width: 2, title: "Total", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${TOTAL} </a></font>" },
             { field: "TOTAL_CARGADO", width: 2, title: "Cant. Cargada", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${TOTAL_CARGADO} </a></font>" },
             { field: "MONTO_CARGADO", width: 2, title: "MONTO CARGADO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${MONTO_CARGADO} </a></font>" },
             { field: "FILE_COBRO_ID", width: 2, title: 'Ver', headerAttributes: { style: 'text-align: center' }, template: "<img src='../Images/iconos/report_deta.png'  width='16'  onclick='VerReporteCobro(${FILE_COBRO_ID});'  border='0' title='Ver Reporte En nueva Ventana.'  style=' cursor: pointer; cursor: hand;'>" },
             //{ field: "NRO", width: 2, title: "NRO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${NRO} </a></font>" },
             //{ field: "IMPORTE_ORIGEN", width: 5, title: "IMP. ORIGEN", template: "<a id='single_2'   style='color:gray;text-decoration:none;font-size:11px'>${IMPORTE_ORIGEN}</a>" },
             //{ field: "IMPORTE_DEPOSITADO", width: 5, title: "IMP.DEPOSITADO", template: "<font color='green'><a id='single_2' style='color:gray;text-decoration:none;font-size:11px'> ${IMPORTE_DEPOSITADO} </a></font>" },
             //{ field: "NORO_ID", width: 2, title: "OFICINA", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${NORO_ID} </a></font>" },
             //{ field: "NRO_MOVIMIENTO", width: 4, title: "N° MOVIMIENTO", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${NRO_MOVIMIENTO} </a></font>" },
             //{ field: "FECHA_PAGO", width: 3, title: "FECHA PAGO", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${FECHA_PAGO} </a></font>" },
             //{ field: "TIPO_VALOR", width: 3, title: "TIPO VALOR", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${TIPO_VALOR} </a></font>" },
             //{ field: "CANAL_ENTRADA", width: 2, title: "CANAL ENTRADA", template: "<font color='green'><a id='single_2'  style='color:gray;text-decoration:none;font-size:11px'> ${CANAL_ENTRADA} </a></font>" },

            ]
    });   
}
function VerReporteCobro(FILE_COBRO_ID) {
    //Si la validacion de fecha es Igual a 1 entonces :        
    var tipo = "PDF"
        $.ajax({
            url: '../CargarArchivoPlano/ReporteTipo',
            type: 'POST',
            data: { id: FILE_COBRO_ID},
            beforeSend: function () {
                var load = '../Images/otros/loading.GIF';
                //$('#externo').attr("src", load);
            },
            success: function (response) {
                var dato = response;
                //validarRedirect(dato);
                if (dato.result == 1) {
                    url = "../CargarArchivoPlano/ReporteBanco?" + "id=" + FILE_COBRO_ID + "&" + "formato=" + tipo ;
                    //$("#contenedor").show();
                    window.open(url);
                    //$('#externo').attr("src", url);
                } else if (dato.result == 0) {
                    //$('#externo').attr("src", vacio);
                    //$("#contenedor").hide();
                    url = alert(dato.message);
                }
            }
        });
   

}

function LoadBancoPagos(control, valSel, etiqueta) {

    $('#' + control + ' option').remove();
    $('#' + control).append($("<option />", { value: 0, text: K_ITEM.CHOOSE }));
    //if (etiqueta != undefined) { $('#' + control).append($("<option />", { value: 0, text: etiqueta })); };
    $.ajax({
        url: '../CargarArchivoPlano/ListaBancosPagos',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#' + control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function VAlidarBancoxId(id) {
    $.ajax({
        url: "../CargarArchivoPlano/ValidarBanco",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //validarRedirect(dato);
            if (dato.result == 1) {
                var lbl = dato.valor;
                var bankOrigen = "BANCO ORIGEN :&nbsp;&nbsp;";
                document.getElementById("LabelBanco").innerHTML = bankOrigen;
                document.getElementById("lbBanco").innerHTML = lbl.bold();
                loadData();
            } else if (dato.result == 0) {              
                
                alert(dato.message);
                $("#grid").html('');
            }
        }
    });
}

function VAlidarArchivoxId(id) {
    var mensaje = confirm("ESTA SEGURO QUE DESEA SUBIR EL ARCHIVO.");
    if(mensaje){
    $.ajax({
        url: "../CargarArchivoPlano/ValidarArchivo",
        type: 'POST',
        data: { id: id },
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //validarRedirect(dato);
            if (dato.result == 1) {
               
                    CargarPagos();
             
            } else if (dato.result == 0) {
                alert(dato.message);
                //$("#grid").html('');
            }
        }
    });
    } else {
        alert('ACCION CANCELADA.')
    }
}
function CargarPagos() {
   
    $.ajax({
        url: "../CargarArchivoPlano/CargarCobrosDeBanco",
        type: 'POST',
        data: {},
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            //validarRedirect(dato);
            if (dato.result == 1) {
                alert(dato.message);
            } else if (dato.result == 0) {

                alert(dato.message);
                //$("#grid").html('');
            }
        }
    });
  
}


