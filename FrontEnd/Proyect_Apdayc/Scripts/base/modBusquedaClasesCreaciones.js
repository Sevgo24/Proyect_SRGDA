$(function () {
    $("#listado").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_15,
            transport: {
                read: {
                    type: "POST",
                    url: "../CLASESDECREACIONES/usp_listar_ClaseCreacionesJson",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            clasecreacion: $("#CLASS_COD").val(),
                            tipodocumento: $("#cb1").val(),
                            clase: $("#COD_PARENT_CLASS").val()
                        })
                }
            },
            schema: { data: "REFCREATIONCLASS", total: 'TotalVirtual' }
        },
        sortable: K_ESTADO_ORDEN,
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
                  field: "CLASS_COD", width: 50, title: "Clase de Creación", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;'>${CLASS_COD}</a>"
              },
              {
                  field: "CLASS_DESC", width: 50, title: "Tipo Documento", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}' style='color:5F5F5F;text-decoration:none;'>${CLASS_DESC}</a>"
              },
              {
                  field: "COD_PARENT_CLASS", width: 150, title: "Clase", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;'>${COD_PARENT_CLASS}</a>"
              },
              {
                  type: "date",
                  field: "LOG_DATE_CREAT",
                  editable: "false",
                  width: 150, title: "Creación",
                  template: "<font color='green'><a id='single_2' href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;font-size:16px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>",
              }
              ]
    });
});


$(function () {
    $("#btnBusqueda").click(function () {

        function avoidRefresh(e) {
            e.preventDefault();
        }

        $(".alert-link").hide();
        $("#message").hide();

        $("#listado").kendoGrid({
            dataSource: {
                type: "json",
                serverPaging: true,
                pageSize: K_SIZE_PAGE,
                transport: {
                    read: {
                        type: "POST",
                        url: "../CLASESDECREACIONES/usp_listar_ClaseCreacionesJson",
                        dataType: "json"
                    },
                    parameterMap: function (options, operation) {
                        if (operation == 'read')
                            return $.extend({}, options, {
                                clasecreacion: $("#CLASS_COD").val(),
                                tipodocumento: $("#cb1").val(),
                                clase: $("#COD_PARENT_CLASS").val()
                            })
                    }
                },
                schema: { data: "REFCREATIONCLASS", total: 'TotalVirtual' }
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
                      field: "CLASS_COD", width: 50, title: "CLASE CREACION", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;'>${CLASS_COD}</a>"
                  },
                  {
                      field: "CLASS_DESC", width: 50, title: "TIPO DOCUMENTO", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}' style='color:5F5F5F;text-decoration:none;'>${CLASS_DESC}</a>"
                  },
                  {
                      field: "COD_PARENT_CLASS", width: 150, title: "CLASE", template: "<a  href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;'>${COD_PARENT_CLASS}</a>"
                  },
                  {
                      type: "date",
                      field: "LOG_DATE_CREAT",
                      editable: "false",
                      width: 150, title: "CREACION",
                      template: "<font color='green'><a id='single_2' href='../CLASESDECREACIONES/Edit/${CLASS_COD}'style='color:5F5F5F;text-decoration:none;font-size:16px'>" + '#=(LOG_DATE_CREAT==null)?"":kendo.toString(kendo.parseDate(LOG_DATE_CREAT,"MM/dd/yyyy"),"MM/dd/yyyy") #' + "</a></font>"
                  }
                  ]
        });
    });
});

