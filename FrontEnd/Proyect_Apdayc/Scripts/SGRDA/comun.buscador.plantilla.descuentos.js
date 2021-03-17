var mvInitBuscarPlantillaDescuento = function (parametro) {

    //las variables que declare antes
    var idcontenedor=parametro.container;
    var btnEvento=parametro.idButtonToSearch;
    var idModalView=parametro.idDivMV;
    var valida=parametro.valida;

    var elemento = '<div id="' + parametro.idDivMV + '">';
    elemento += '<input type="hidden"  id="hidIdidModalView" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEvent" value="' + parametro.event + '"/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';


    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';

    elemento += '</tr>';




    elemento += '<td>Descripcion de la Plantilla</td><td colspan="3"><input type="text" id="txtDescPlantilla" size="70"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '</tr>';
    //elemento += '<td>Fecha de Inicio</td><td colspan="3"><input type="text" id="txtInicio" size="70"></td>';
    elemento += '</tr>';



    elemento += '<tr>';
    elemento += '<td colspan="4"><center><button id="btnBuscarPlanillaDesc"> <img src="../Images/botones/buscar2.png"  width="16px"> Buscar </button>&nbsp;&nbsp;<button id="btnLimpiarSocioBPS"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button></center>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridDesc"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';

    elemento += '<style>';
    elemento += ' .ui-autocomplete {        max-height: 200px;        overflow-y: auto;        overflow-x: hidden;    }';
    elemento += '  html .ui-autocomplete {        height: 200px;    }';
    elemento += ' ul.ui-autocomplete {         z-index: 1100;    } ';
    elemento += ' </style> ';

    $("#" + parametro.container).append(elemento);

    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    //if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 810,
        height: 530,
        title: "Búsqueda General de Plantillas de Descuento."
    });

    var _cuentaBusSoc = 1; /*addon dbs  20150831- Primera carga los datos */
    $("#btnBuscarPlanillaDesc").on("click", function () {
        if (_cuentaBusSoc == 1) {
            loadDataFound();
        }
        _cuentaBusSoc++;
    });

    $("#btnLimpiarSocioBPS").on("click", function () {
        //limpiarBusqueda();

        // $("#ddlTipoIdBPS").val();
        $("#txtDescPlantilla").val("");

        $('#gridDesc').html('');
        _cuentaBusSoc = 1;
    });
};


var loadDataFound = function () {
    $("#gridDesc").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: 5,
            transport: {
                read: {
                    url: "../Descuento/ListarPlantillaDescuentos",
                    dataType: "json"
                    //data: param
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            desc: $("#txtDescPlantilla").val(),
                            fecha: $("#txtInicio").val()
                        });
                }
            },
            schema: { data: "ListaDescuentosPlantilla", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: true,
        pageable: true,
        selectable: true,
        filterable: false,
        columns:
           [
            { field: "TEMP_ID_DSC", width: 5, title: "ID", template: "<a id='single_2'  href=javascript:getBPS('${TEMP_ID_DSC}') style='color:gray !important;'>${TEMP_ID_DSC}</a>" },
            { field: "TEMP_DESC", width: 10, title: "Descripcion Establecimiento", template: "<a id='single_2'  href=javascript:getBPS('${TEMP_ID_DSC}')  style='color:gray !important;'>${TEMP_DESC}</a>" },
           ]
    });
};

var getBPS = function (id) {
    //alert(id);
    var hidIdidModalView = $("#hidIdidModalView").val();//val=mvBuscarSocio
    var fnc = $("#hidIdEvent").val();//fnc=reloadEvento;
    $("#" + hidIdidModalView).dialog("close");
    eval(fnc + " ('" + id + "');");
};

//Recupera la data
//DESC - BUSQ. GENERAL
var reloadEvento = function (idSel) {
    $("#lblPlantilla").val(idSel);
    //generales.js
    obtenerNombreDescPlantillaX($("#lblPlantilla").val(), 'lblPlantilla');
};

