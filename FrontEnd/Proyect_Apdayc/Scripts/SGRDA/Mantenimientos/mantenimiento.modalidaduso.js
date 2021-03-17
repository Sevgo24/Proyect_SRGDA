var defaultTipoCreacion = 'MW';
var defaultOrigeModalidad = 'NAC';
var defaultTipoSociedad = 'AUT';
/************************** INICIO CARGA ********************************************/
$(function () {
    //-------------------------- CARGA DLL ---------------------------------     
    cargaDLL();    
    //-------------------------- EVENTO CONTROLES ------------------------------------ 
    $("#btnBuscar").on("click", function () {
        $('#grid').data('kendoGrid').dataSource.query({
            MOD_DEC: $('#txtDescripcionMod').val(),
            MOD_ORIG: $('#ddlOriModalidad').val(),
            MOD_SOC: $('#ddlTipSociedad').val(),
            CLASS_COD: $('#ddlTipCreacion').val(),
            MOG_ID: $('#dllGruModalidad').val(),
            RIGHT_COD: $('#ddlTipDerecho').val(),
            MOD_INCID: $('#ddlNivIncidencia').val(),
            MOD_USAGE: $('#ddlTipUsoRepertorio').val(),
            MOD_REPER: $('#ddlModUsoRepertorio').val(),
            page: 1,
            pageSize: K_PAGINACION.LISTAR_10
        });      
    });

    $("#txtDescripcion").keypress(function (e) {
        if (e.which == 13) {
            $('#grid').data('kendoGrid').dataSource.query({
                MOD_DEC: $('#txtDescripcionMod').val(),
                MOD_ORIG: $('#ddlOriModalidad').val(),
                MOD_SOC: $('#ddlTipSociedad').val(),
                CLASS_COD: $('#ddlTipCreacion').val(),
                MOG_ID: $('#dllGruModalidad').val(),
                RIGHT_COD: $('#ddlTipDerecho').val(),
                MOD_INCID: $('#ddlNivIncidencia').val(),
                MOD_USAGE: $('#ddlTipUsoRepertorio').val(),
                MOD_REPER: $('#ddlModUsoRepertorio').val(),
                page: 1,
                pageSize: K_PAGINACION.LISTAR_10
            });
        }
    });

    loadData();

    $("#btnNuevo").on("click", function () {
        document.location.href = '../ModalidadUso/Nuevo';
    });

    $("#btnEliminar").on("click", function (e) {
        eliminar();
    });

    $("#ddlTipCre").change(function () {
        var codigo = $("#ddlTipCre").val();
        loadTipoDerecho('ddlTipDer', codigo, '0');
    });

    $("#repPDF").on("click", function (e) {
        reporte();
    });

    $("#repEXCEL").on("click", function (e) {
        reporte();
    });

    //-------------------------------------------------------------
    ///copiar consulta general modalidaduso
    mvInitModalidadUso({
        container: "ContenedormvModUso",
        idButtonToSearch: "btnBusca",
        idDivMV: "mvModalidadUso",
        event: "reloadEventoModuso"
    });
    
});

///copiar consulta general modalidaduso
var reloadEventoModuso = function (idSel) {
    //capturando id
    alert("Selecciono ID:   " + idSel);
    $("#hidMOD_ID").val(idSel);
};
//-------------------------------------

/************************** FUNCIONES ********************************************/
function loadData() {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_10,        
            transport: {
                read: {
                    type: "POST",
                    url: "../ModalidadUso/Listar",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            MOD_DEC: $('#txtDescripcion').val(),
                            MOD_ORIG: $('#ddlOriMod').val(),
                            MOD_SOC: $('#ddlTipSoc').val(),
                            CLASS_COD: $('#ddlTipCre').val(),
                            MOG_ID: $('#dllGruMod').val(),

                            RIGHT_COD: $('#ddlTipDer').val(),
                            MOD_INCID: $('#ddlNivInc').val(),
                            MOD_USAGE: $('#ddlTipUsoRep').val(),
                            MOD_REPER: $('#ddlModUsoRep').val()
                        })
                }
            },
            schema: { data: "ListarModalidad", total: 'TotalVirtual' }
        },
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        navigatable: true,
        scrollable: true,

        columns:
			[
				{ title: 'Eliminar', width: 60, template: "<input type='checkbox' id='chkSel' class='kendo-chk' name='chkSel' value='${MOD_ID}'/>" },
                { field: "MOD_ID", width: 50, title: "Id", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ID}</a>" },				
                { field: "SAP_CODIGO", width: 70, title: "Cod. Sap", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${SAP_CODIGO}</a>" },
                { field: "MODALIDAD", title: "Grupo Modalidad", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MODALIDAD}</a>" },
                { field: "MOD_DEC", width: 330, title: "Descripción", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_DEC}</a>" },
                { field: "TIPO_DERECHO", title: "Tipo de Derecho", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_DERECHO}</a>" },
                { field: "TIPO_CREACION", title: "Tipo de Creación", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_CREACION}</a>" },

                { field: "MOD_INCID", width: 70, title: "Nivel de </br>Incidencia", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_INCID}</a>" },
                //{ field: "INCIDENCIA", title: "Nivel de Incidencia", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${INCIDENCIA}</a>" },                

                { field: "MOD_ORIG", width: 70, title: "Origen de </br>Mod. Uso", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_ORIG}</a>" },
                //{ field: "ORIGEN", title: "Origen de Mod. Uso", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${ORIGEN}</a>" },                               

                { field: "MOD_USAGE", width: 70, title: "Tipo Uso</br>de Obra", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_USAGE}</a>" },
                //{ field: "TIPO_OBRA", title: "Tipo de Uso de Obra", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_OBRA}</a>" },
                

			    { field: "MOG_SOC", width: 70, title: "Tipo</br> Sociedad", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOG_SOC}</a>" },
                //{ field: "TIPO_SOCIEDAD", title: "Tipo Sociedad", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${TIPO_SOCIEDAD}</a>" },

                { field: "MOD_REPER", width: 80, title: "Modo Uso</br>de Repertorio", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${MOD_REPER}</a>" },
                //{ field: "USO_REPERTORIO", title: "Id Modo de Uso de Repertorio", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:grey;text-decoration:none;font-size:11px'>${USO_REPERTORIO}</a>" },
                //{ field: "MOD_COM", title: "<font size=2px>Comisión (%)</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${MOD_COM}</a>" },
                //{ field: "MOD_DISCA",  title: "<font size=2px>Dcot. Admin. (%)</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${MOD_DISCA}</a>" },
                //{ field: "MOD_DISCS",  title: "<font size=2px>Dcto. Social (%)</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${MOD_DISCS}</a>" },
                //{ field: "MOD_DISCC",  title: "<font size=2px>Dcto. Adicional (%)</font>", template: "<a id='single_2'  href=javascript:editar('${MOD_ID}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${MOD_DISCC}</a>" }
			]
    });
}

function editar(idSel) {
    document.location.href = '../ModalidadUso/nuevo?id=' + idSel;
}

function eliminar() {
    var values = [];

    $(".k-grid-content tbody tr").each(function () {
        var $row = $(this);
        var checked = $row.find('.kendo-chk').attr('checked');
        if (checked == "checked") {
            var codigo = $row.find('.kendo-chk').attr('value');
            values.push(codigo);
            delData(codigo);
        }
    });

    if (values.length == 0) {
        alert("Seleccione una modalidad.");
    } else {
        var modalidad = {
            MOD_ORIG: $('#ddlOriMod').val(),
            MOD_SOC: $('#ddlTipSoc').val(),
            CLASS_COD: $('#ddlTipCre').val(),
            MOG_ID: $('#dllGruMod').val(),
            RIGHT_COD: $('#ddlTipDer').val(),
            MOD_INCID: $('#ddlNivInc').val(),
            MOD_USAGE: $('#ddlTipUsoRep').val(),
            MOD_REPER: $('#ddlModUsoRep').val()
        }
        $('#grid').data('kendoGrid').dataSource.query({ modalidad: modalidad, page: 1, pageSize: K_PAGINACION.LISTAR_15 });
        
    }
}

function delData(codigo) {
    var codigoDel = { id: codigo };

    $.ajax({
        url: '../ModalidadUso/eliminar',
        type: 'POST',
        data: codigoDel,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                alert("Estados actualizado correctamente.");
                loadData();
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

//function habChk(val) {
//    $('#chkTipDer').prop('checked', val).hide();
//    $('#chkOriMod').prop('checked', val).hide();
//    $('#chkGruMod').prop('checked', val).hide();
//    $('#chkTipSoc').prop('checked', val).hide();
//    $('#chkNivInc').prop('checked', val).hide();
//    $('#chkTipUsoRep').prop('checked', val).hide();
//    $('#chkModUsoRep').prop('checked', val).hide();
//}

//function habDesDll(controlChk, controlDdl) {    
//    $("#"+controlChk).change(function () {
//        if ($('#' + controlChk).is(':checked')) {           
//            $("#"+controlDdl).prop('disabled', false);
//            $("#"+controlDdl).css('color', '#333333').css('border', '1px solid gray').css('background-color', 'white');
//        } else {
//            $("#" + controlDdl).val('0');
//            $("#"+controlDdl).prop('disabled', true);
//            $("#"+controlDdl).css('color', '#333333').css('border', '1px solid gray').css('background-color', 'rgb(235, 235, 228)');
//        }
//    });
//}

function reporte() {
    //var vFormat = "PDF";

    var modalidad = {
        MOD_ORIG: $('#ddlOriMod').val(),
        MOD_SOC: $('#ddlTipSoc').val(),
        CLASS_COD: $('#ddlTipCre').val(),
        MOG_ID: $('#dllGruMod').val(),

        RIGHT_COD: $('#ddlTipDer').val(),
        MOD_INCID: $('#ddlNivInc').val(),
        MOD_USAGE: $('#ddlTipUsoRep').val(),
        MOD_REPER: $('#ddlModUsoRep').val()
    }

    $.ajax({
        url: "../ModalidadUso/DownloadReport",
        type: "GET",
        data: modalidad,
        success: function (response) {
            //var dato = response;
            //if (dato.result === 1) {
            //    var modalidad = dato.data.Data;
            //    $('#txtDes').val(modalidad.MOD_DEC);
            //    loadTipoModUso('ddlOriMod', modalidad.MOD_ORIG);
            //    loadTipoSociedad('ddlTipSoc', modalidad.MOD_SOC);
            //    loadTipoGrupo('dllGruMod', modalidad.MOG_ID);
            //    loadTipoCreacion('ddlTipCre', modalidad.CLASS_COD);
            //    ////loadTipoCreacion('ddlTipCre', modalidad.RIGHT_COD,modalidad.CLASS_COD);
            //    loadTipoDerecho('ddlTipDer', modalidad.CLASS_COD, modalidad.RIGHT_COD);
            //    loadTipoIncidencia('ddlNivInc', modalidad.MOD_INCID);
            //    loadTipoRepertorio('ddlModUsoRep', modalidad.MOD_REPER);
            //    loadTipoObra('ddlTipUsoRep', modalidad.MOD_USAGE);
            //    $('#txtComision').val(modalidad.MOD_COM);
            //    $('#txtDtoAdm').val(modalidad.MOD_DISCA);
            //    $('#txtDtoSoc').val(modalidad.MOD_DISCS);
            //    $('#txtDtoAdi').val(modalidad.MOD_DISCC);
            //    //RATE_ID: '0';                
            //} else {
            //    alert(dato.message);
            //}
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function cargaDLL() {
    //loadTipoDerecho('ddlTipDer', defaultTipoCreacion, '0'); 
    loadTipoDerecho('ddlTipDer', '0', '0');

    //loadTipoModUso('ddlOriMod', defaultOrigeModalidad);
    loadTipoModUso('ddlOriMod', '0');
    loadTipoGrupo('dllGruMod', '0');

    //loadTipoSociedad('ddlTipSoc', defaultTipoSociedad);
    loadTipoSociedad('ddlTipSoc', '0');

    //loadTipoCreacion('ddlTipCre', defaultTipoCreacion);
    loadTipoCreacion('ddlTipCre', '0');

    loadTipoIncidencia('ddlNivInc', '0');
    loadTipoObra('ddlTipUsoRep', '0');
    loadTipoRepertorio('ddlModUsoRep', '0');
}

function reporte() {
    var modalidad = {
        MOD_ORIG: $('#ddlOriMod').val(),
        MOD_SOC: $('#ddlTipSoc').val(),
        CLASS_COD: $('#ddlTipCre').val(),
        MOG_ID: $('#dllGruMod').val(),

        RIGHT_COD: $('#ddlTipDer').val(),
        MOD_INCID: $('#ddlNivInc').val(),
        MOD_USAGE: $('#ddlTipUsoRep').val(),
        MOD_REPER: $('#ddlModUsoRep').val()
    }

    $.ajax({
        url: "../ModalidadUso/Reporte",
        type: "GET",
        data: modalidad,
        success: function (response) {

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
