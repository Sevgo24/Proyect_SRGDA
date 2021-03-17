var K_variables = {
    OkSimbolo: '<span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"></span>',
    AlertaSimbolo: '<span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>',
    SinSimbolo: '',
    Si: 1,
    No: 0,
    Cero: 0,
    MenosUno: '-1',
    CeroLetra: '0',
    TextoVacio: "",
    MinimoHeight: 75,
    blindDuration: 1000,
    hideDuration: 1000,
    Uno: 1
}

$(function () {

    ListarSolicitud();

    $("#btnBuscar").on("click", function () {
        $("#grid").html('');
        ListarSolicitud();
    });

    $("#btnAprobarArtistas").on("click", function () {
        obtenerSolicitusSeleccionadasAprobadas();
    });

    $("#btnRechazarArtistas").on("click", function () {
        obtenerSolicitusSeleccionadasRechazadas();
    });

})

function clickCheck() {
    var state = $("#idCheck").is(':checked');

    if (state == 1) {
        $(".Check").attr('checked', true);
        //var cantidad = CantidadFacturasSeleccionadas();
    
            $(".Check").attr('checked', false);
            $("#idCheck").prop('checked', true);

            ValidadCantidadFactSelecionadas();
    } else {
        $('#tblAdministracionArtistas tr').each(function () {
            var id = $(this).find(".IDCODUNI").html();//ID FACTURA

            //var ident = $(this).find(".IDENTIFICADORCell").html();
            if (id != null) {
                $('#chkEstOrigen' + id).prop('checked', false);
            }
        });
    }
  
}

function ValidadCantidadFactSelecionadas() {
    var cantidadSelect = 0;
    var ReglaValor = [];
    var cantidad = 1;
    $('#tblAdministracionArtistas tr').each(function () {
        var id = $(this).find(".IDCODUNI").html();//ID FACTURA
        
        //var ident = $(this).find(".IDENTIFICADORCell").html();
        if ( id != null) {           
            $('#chkEstOrigen' + id).prop('checked', true);
                cantidad += 1;            
        }
    });
    return cantidad;
}


function ListarSolicitud() {
    $.ajax({
        data: {
            Lic_Id: $("#txtcodlic").val() == "" ? 0 : $("#txtcodlic").val()
                },
        url: '../AdministracionArtista/Listar',
        type: 'POST',
        async:false,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#grid").html(dato.message);
                $("#lblCantCobros").html(dato.Code);
            } else {
                VentanaAviso(dato.message, "OBSERVACIONES", K_variables.AlertaSimbolo);
            }
        }
    });
}


function obtenerSolicitusSeleccionadasAprobadas(tipo) {
    var ReglaValor = [];
    var contador = 0;

    $('#tblAdministracionArtistas tr').each(function () {
        var id = $(this).find(".IDCODUNI").html();
        //var tipo = $(this).find(".TipCell").html();
        var actual = 0;
        if ( id != null) {
            if ($('#chkEstOrigen' + id).is(':checked')) {
                ReglaValor[contador] = {
                   COD_UNICO: id,
                };
                contador += 1;
            }
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {

      
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '../AdministracionArtista/AprobarSolicitudes',
                data: ReglaValor,
                success: function (response) {
                    var dato = response;
                    //validarRedirect(dato);
                    if (dato.result == 1) {
                        //var idMoneda = $('#ddlMoneda').val();                    
                        alert(dato.message);
                        ListarSolicitud(0);
                        //BuscarFacturasBorrador(0);
                    } else if (dato.result == 0) {
                        alert(dato.message);
                    }
                },
                failure: function (response) {
                    alert("No se logro enviar las factura(s).");
                }
            });
        }    
    else {
        alert("Debe selecionar una solicitud.");
    }

}


function obtenerSolicitusSeleccionadasRechazadas(tipo) {
    var ReglaValor = [];
    var contador = 0;

    $('#tblAdministracionArtistas tr').each(function () {
        var id = $(this).find(".IDCODUNI").html();
        //var tipo = $(this).find(".TipCell").html();
        var actual = 0;
        if (id != null) {
            if ($('#chkEstOrigen' + id).is(':checked')) {
                ReglaValor[contador] = {
                    COD_UNICO: id,
                };
                contador += 1;
            }
        }
    });
    var ReglaValor = JSON.stringify({ 'ReglaValor': ReglaValor });

    if (contador > 0) {


        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../AdministracionArtista/RechazarSolicitudes',
            data: ReglaValor,
            success: function (response) {
                var dato = response;
                //validarRedirect(dato);
                if (dato.result == 1) {
                    //var idMoneda = $('#ddlMoneda').val();                    
                    alert(dato.message);
                    ListarSolicitud(0);
                    //BuscarFacturasBorrador(0);
                } else if (dato.result == 0) {
                    alert(dato.message);
                }
            },
            failure: function (response) {
                alert("No se logro enviar las factura(s).");
            }
        });
    }
    else {
        alert("Debe selecionar una solicitud.");
    }

}
