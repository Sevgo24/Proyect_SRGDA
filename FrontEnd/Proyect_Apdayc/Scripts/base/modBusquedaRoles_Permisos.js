
$(function () {

    $("#btnBusqueda").click(function () {

        function avoidRefresh(e) {
            e.preventDefault();
        }

        $(".alert-link").hide();
        $("#message").hide();

        var busq1 = $("#cb1").val();
        var busq2 = $("#cb2").val();
        var tot = $("#TotalVirtual").val();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: 10,

                transport: {
                    read: {
                        url: "../ROLES_PERMISOS/usp_listar_RolesPermisosJson", dataType: "json", data: { rol: busq1, modulo: busq2 }
                    }
                },
                schema: { data: "ROL_PERMISOS", total: 'TotalVirtual'}
            },
            sortable: true,
            pageable: {
                messages: {
                    display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                    empty: "No se encontraron registros"
                }
            },
            selectable: "multiple row",
            columns:
            [
                {
                    field: "MODU_ICODIGO_MODULO", width: 10, title: "ID",
                    template: "<a id='single_2' href='../ROLES_PERMISOS/Edit/${MODU_ICODIGO_MODULO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${MODU_ICODIGO_MODULO}</a>"
                },
                {
                    field: "ROL_ICODIGO_ROL", width: 20, title: "ROL",
                    template: "<a id='single_2' href='../ROLES_PERMISOS/Edit/${MODU_ICODIGO_MODULO}' style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROL_ICODIGO_ROL}</a>"
                },
                {
                    field: "MODU_VNOMBRE_MODULO", width: 40, title: "MODULO",
                    template: "<a id='single_2' href='../ROLES_PERMISOS/Edit/${MODU_ICODIGO_MODULO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${MODU_VNOMBRE_MODULO}</a></font>"
                },
                {
                    field: "ROMO_CACTIVO", width: 15, title: "ACTIVO",
                    template: "<a id='single_2' href='../ROLES_PERMISOS/Edit/${MODU_ICODIGO_MODULO}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${ROMO_CACTIVO}</a></font>"
                }
            ]
        });
    }

    );
});
