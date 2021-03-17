/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 440;

/*INICIO CONSTANTES*/
function loadData() {

    var busq = $("#txtNombreSearch").val();
    var busq11 = $("#txtNombreSearch").val();
    var busq2 = $("#txtNombreSearch").val();
    var tot = 2;//$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "Socio/USP_SOCIOS_LISTARPAGE", dataType: "json", data: { tipo: busq, cod_tipo: busq1, nombre: busq2 }
                }
            },
            schema: { data: "Socio_Negocio", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: '', width: 4, template: "<input type='checkbox' id='id' name='id' value='${OWNER},${BPS_ID}'/>"
               },
            { field: "RowNumber", width: 5, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${OWNER },${BPS_ID }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${RowNumber}</a>" },
            { field: "ENT_TYPE", width: 5, title: "<font size=2px>TIPO PERS</font>", template: "<a id='single_2'  href=javascript:editar('${OWNER },${BPS_ID }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ENT_TYPE}</a>" },
            { field: "TAX_ID", width: 20, title: "<font size=2px>TIPO PERS</font>", template: "<a id='single_2' href=javascript:editar('${OWNER },${BPS_ID }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${TAX_ID}</a>" },
            { field: "ROL_VNOMBRE_ROL", width: 20, title: "<font size=2px>NOMBRES/RAZON SOCIAL</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${OWNER },${BPS_ID }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROL_VNOMBRE_ROL}</a></font>" },
            { title: 'SOCIO', width: 4, template: "<input type='checkbox' id='USUARIO' name='id' value='${OWNER},${BPS_ID}'/>" },
            { title: 'U', width: 4, template: "<input type='checkbox' id='USUARIO' name='id' value='${OWNER},${BPS_ID}'/>" },
            { title: 'GE', width: 4, template: "<input type='checkbox' id='RECAUDADOR' name='id' value='${OWNER},${BPS_ID}'/>" },
            { title: 'R', width: 4, template: "<input type='checkbox' id='ASOCIACION' name='id' value='${OWNER},${BPS_ID}'/>" },
            { title: 'A', width: 4, template: "<input type='checkbox' id='GRUPO EMP.' name='id' value='${OWNER},${BPS_ID}'/>" },
            { title: 'E', width: 4, template: "<input type='checkbox' id='PROVEEDOR' name='id' value='${OWNER},${BPS_ID}'/>" },
           ]
    });
}