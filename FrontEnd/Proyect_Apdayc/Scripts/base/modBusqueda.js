
/// <reference path="../kendo.web-vsdoc.js" />
$(function () {
    $("#btnBusqueda").click(function () {
        $("#listado").kendoGrid({
            dataSource: {
                 type: "json",
                 serverPaging: true,
                 pageSize: 5,
                 transport: {
                     read: {
                         type: "POST",
                         url: "../REC_BANKS_GRAL/USP_LISTAR_REC_BANKS_GRALJSON",
                         dataType: "json"
                     },
                     parameterMap: function (options, operation) {
                         if (operation == 'read')
                             return $.extend({}, options, { dato: $("#txtBusqueda").val() })
                     }
                 },
                 schema: { data: "REC_BA", total: 'TotalVirtual' }
             },

            groupable: true,
            sortable: true,
            pageable: true,
           

            columns: [{
                field: "OWNER", width: 50, title: "<font size=2px>OWNER</font>", template: "<a id='single_2' href='../ROLES/Edit/${OWNER}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${OWNER}</a>"
            }, {
                field: "BNK_ID", width: 50, title: "<font size=2px>ID</font>", template: "<a id='single_2' href='http://farm3.static.flickr.com/2489/4234944202_0fe7930011.jpg' style='color:5F5F5F;text-decoration:none;font-size:11px'>${BNK_ID}</a>"
            }, {
                field: "BNK_NAME", width: 90, title: "<font size=2px>NAME</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${BNK_NAME}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${BNK_NAME}</a></font>"
            }, {
                field: "BNK_C_BRANCH", width: 90, title: "<font size=2px>BRANCH</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${BNK_C_BRANCH}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${BNK_C_BRANCH}</a></font>"
            }, {
                field: "BNK_C_DC", width: 90, title: "<font size=2px>DC</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${BNK_C_DC}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${BNK_C_DC}</a></font>"
            }, {
                field: "BNK_C_ACCOUNT", width: 50, title: "<font size=2px>ACCOUNT</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${BNK_C_ACCOUNT}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${BNK_C_ACCOUNT}</a></font>"
           }, { //
            //    field: "LOG_DATE_CREAT", width: 90, title: "FCREATE", type: "date", template: "<a id='example2' href='/ROLES/Edit/${LOG_DATE_CREAT}'style='color:5F5F5F;text-decoration:none;font-size:12px'>${LOG_DATE_CREAT}</a>", format: "{0:dd/MM/yyyy}"
            //}, {
            //    field: "LOG_DATE_UPDATE", width: 90, title: "FUPDATE", type: "date", format: "{0:dd/MM/yyyy}", template: "<font color='green'><a id='example2' href='/ROLES/Edit/${LOG_DATE_UPDATE}'style='color:5F5F5F;text-decoration:none;font-size:12px;'>${LOG_DATE_UPDATE}</a></font>"
            //}, {
               field: "LOG_USER_CREAT", width: 90, title: "<font size=2px>USERC</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${LOG_USER_CREAT}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${LOG_USER_CREAT}</a></font>"
            }, {
                field: "LOG_USER_UPDATE", width: 90, title: "<font size=2px>USERU</font>", template: "<font color='green'><a id='single_2' href='../ROLES/Edit/${LOG_USER_UPDATE}'style='color:5F5F5F;text-decoration:none;font-size:11px'>${LOG_USER_UPDATE}</a></font>"
            }]
        });
        /*});*/
    }

    );
});


