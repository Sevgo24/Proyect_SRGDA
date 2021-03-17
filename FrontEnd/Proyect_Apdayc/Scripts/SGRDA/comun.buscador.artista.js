var val;
var _cuentaBusArt = 1; /*addon dbs  20150831- Primera carga los datos */
var mvInitArtista = function (parametro) {
    //var idContenedor = parametro.container;
    //var btnEvento = parametro.idButtonToSearch;
    //var idModalView = parametro.idDivMV;
    var busquedaIni = 0;
    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdidModalViewArt" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventArt" value="' + parametro.event + ' "/>';
    elemento += '<table border=0  style=" width:100%; border:1px;">';

    elemento += '<tr>';
    //elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td><div ><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:80px">Interprete : </td><td style="width:30px"><input type="text" id="txtNombreArtistaFilter" style="width:250px"/></td>';
    elemento += '<td colspan="4"><center><button id="btnBuscarArtistaFilter" title="Buscar interprete"> <img src="../Images/botones/buscar2.png" title="Buscar interprete" width="16px"></button>&nbsp;&nbsp;<button id="btnLimpiarConsulta"  title="Limpiar interprete"> <img src="../Images/botones/refresh.png"  width="16px" title="limpiar">  </button>&nbsp;&nbsp;<button id="btnAddConsGralArtista"  title="Agregar interprete"> <img src="../Images/botones/pencil.png"  width="16px" title="Agregar interprete">  </button></center>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td colspan="4"><div id="gridArtista"></div>';
    elemento += '</td>';
    elemento += '</tr>';

    elemento += '</table></div>';


    $("#" + parametro.container).append(elemento);
    $("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }
    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 600,
        height: 420,
        title: "Búsqueda General de Interpretes"
    });

    $("#btnBuscarArtistaFilter").on("click", function () {
        __searchArtista();
    });

    $("#btnAddConsGralArtista").on("click", function () {
        if ($("#txtNombreArtistaFilter").val() != "") {
            if (confirm("Está seguro de registrar el artista?")) {
                var nombreartista = $("#txtNombreArtistaFilter").val();
                $("#lblArtista").html(nombreartista);
                $("#hidArtistaSel").val("0");
                var hidIdidModalViewArt = $("#hidIdidModalViewArt").val();
                $("#" + hidIdidModalViewArt).dialog("close");
                //var entidad = {
                //    Nombre: " ",
                //    IpNombre: " ",
                //    PrimerNombre: " ",
                //    NombreCompleto: $("#txtNombreArtistaFilter").val(),
                //};

                //$.ajax({
                //    url: '../Artista/Insertar',
                //    data: entidad,
                //    type: 'POST',
                //    success: function (response) {
                //        var dato = response;
                //        validarRedirect(dato); /*add sysseg*/
                //        if (dato.result == 1) {
                //            __searchArtista();
                //        } else if (dato.result == 5) {
                //            alert("Ya existe el artista que desea registrar.");
                //        } else if (dato.result == 0) {
                //            alert(dato.message);
                //        }
                //    }
                //});
            }
        } else {
            alert("Ingrese el nombre completo del artista.");
        }
    });

    $("#btnLimpiarConsulta").on("click", function () { limpiarBusqueda(); });

    $("#txtNombreArtistaFilter").on("keypress", function (evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == "13") {
            __searchArtista();
        }
    });
    $("#txtNombreArtistaFilter").focus();
    //loadDataArtista();
};

var getidArt = function (id) {

    var hidIdidModalViewArt = $("#hidIdidModalViewArt").val();
    var fnc = $("#hidIdEventArt").val();
    $("#" + hidIdidModalViewArt).dialog("close");
    eval(fnc + " ('" + id + "');");
};

function limpiarBusqueda() {
    $("#txtNombreArtistaFilter").val("");
    $("#txtNombreArtistaFilter").focus();
}

function loadDataArtista() {
    if ($("#txtNombreArtistaFilter").val() == "")
        val = 1;
    else
        val = 0;
    //alert($("#txtNombreArtistaFilter").val());
    $("#gridArtista").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            pageSize: K_PAGINACION.LISTAR_10,
            transport: {
                read: {
                    type: "POST",
                    url: "../Artista/ListarOracle",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation == 'read')
                        return $.extend({}, options, {
                            flag: val,
                            nombre: $("#txtNombreArtistaFilter").val()
                        })
                }
            },
            schema: { data: "listaArtista", total: 'TotalVirtual' }
        },
        groupable: false,
        sortable: K_ESTADO_ORDEN,
        pageable: {
            messages: {
                display: "<span style='text-align:left;' >{0} - {1} de {2} registros</span>",
                empty: "No se encontraron registros"
            }
        },
        selectable: true,
        columns: [
                    {
                        field: "COD_ARTIST_SQ",
                        hidden: false,
                        width: 70, title: "Código", template: "<a id='single_2'  href=javascript:getidArt('${COD_ARTIST_SQ}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${COD_ARTIST_SQ}</a>"
                    },
                    { field: "ART_COMPLETE", title: "Interprete", template: "<a id='single_2'  href=javascript:getidArt('${COD_ARTIST_SQ}') style='color:5F5F5F;text-decoration:none;font-size:11px'>${ART_COMPLETE}</a>" }

        ]
    });

}

function __searchArtista() {

    if ($("#txtNombreArtistaFilter").val() == "") val = 1; else val = 0;
    if (_cuentaBusArt == 1) {
        loadDataArtista();
    } else {
        //alert(val +"-"+"nombre:"+ $("#txtNombreArtistaFilter").val() +"-"+ 1 + K_PAGINACION.LISTAR_10)
        $('#gridArtista').data('kendoGrid').dataSource.query({ flag: val, nombre: $("#txtNombreArtistaFilter").val(), page: 1, pageSize: K_PAGINACION.LISTAR_10 });
    }
    _cuentaBusArt = 2;


}