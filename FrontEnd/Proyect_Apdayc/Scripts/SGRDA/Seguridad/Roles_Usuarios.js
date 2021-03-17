/*INICIO CONSTANTES*/
var K_WIDTH = 630;
var K_HEIGHT = 440;

/*INICIO CONSTANTES*/

function loadData() {

    var busq = $("#txtNombreSearch").val();
    var tot = 2;//$("#TotalVirtual").val();

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "Roles_Usuarios/usp_listar_RolesUsuariosPage", dataType: "json", data: { dato: busq }
                }
            },
            schema: { data: "Rol_Usuario", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        columns:
           [
               {
                   title: '', width: 9, template: "<input type='checkbox' id='id' name='id' value='${USUA_ICODIGO_USUARIO },${ROL_ICODIGO_ROL }'/>"
               },
               { field: "USUA_ICODIGO_USUARIO", width: 10, title: "<font size=2px>ID</font>", template: "<a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${USUA_ICODIGO_USUARIO}</a>" },
            { field: "NOMBRE_COMPLETO", width: 20, title: "<font size=2px>NOMBRE COMPLETO</font>", template: "<a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${NOMBRE_COMPLETO}</a>" },
            { field: "ROL_VNOMBRE_ROL", width: 20, title: "<font size=2px>ROL P</font>", template: "<font color='green'><a id='single_2' href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROL_VNOMBRE_ROL}</a></font>" },
            { field: "activo_usuario", width: 10, title: "<font size=2px>ACTIVO</font>", template: "<font color='green'><a id='single_2'  href=javascript:editar('${USUA_ICODIGO_USUARIO},${ROL_ICODIGO_ROL }') style='color:5F5F5F;text-decoration:none;font-size:11px'>${activo_usuario}</a></font>" }
           ]
    });
}