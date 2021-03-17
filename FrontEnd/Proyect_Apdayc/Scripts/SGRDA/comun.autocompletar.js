
function initAutoCompletarRecaudador(control, controlHidden) {
    $("#" + control).autocomplete({
        source: "../AgenteDivision/ACBuscarRecaudador",
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
}

function initAutoCompletarRazonSocial(control, controlHidden) {
    $("#" + control).autocomplete({
        source: "../Socio/ACBuscarSocio",
        select: function (event, ui) {
             $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
}

function initAutoCompletarEstablecimiento(control, controlHidden) {
    $("#" + control).autocomplete({
        source: "../Establecimiento/ACBuscarEstablecimiento",
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
}

function initAutoCompletarAgenterecaudador(control, controlHidden) {
    $("#" + control).autocomplete({
        source: "../TrasladoAgentesRecaudo/ACBuscarAgenterecaudador",
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
}


function initAutoCompletarUbigeo(control, controlHidden) {
    
   $("#" + control).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../General/ACBuscarUbigeo",
                datatype: "json",
                data: {
                    term: request.term,
                    tisn: $('#ddlTerritorio').val()
                },
                success: function (data) {
                    response($.map(data, function(item) {
                        return {
                            label: item.value,
                            value: item.Descripcion,
                            Codigo: item.Codigo
                        }
                    }))
                }
            })
        },
        select: function (event, ui) {
             $("#" + controlHidden).val(ui.item.Codigo);
         }
    });

}
function initAutoCompletarUbigeoB(control, controlHidden,valTerritorio) {

    $("#" + control).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../General/ACBuscarUbigeo",
                datatype: "json",
                data: {
                    term: request.term,
                    tisn: valTerritorio
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.value,
                            value: item.Descripcion,
                            Codigo: item.Codigo
                        }
                    }))
                }
            })
        },
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });

}
function initAutoCompletarGrupoEmpresarial(control, controlHidden) {
    
    $("#" + control).autocomplete({
        source: "../General/ACBuscarGrupoEmpresarial",
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
    
}

function initAutoCompletarAgenciaXbanco(control, controlHidden, idBanco) {
    $("#" + control).autocomplete({
        source: "../General/ACSucursalesBancarias?idBanco=" + idBanco,
        select: function (event, ui) {
            $("#" + controlHidden).val(ui.item.Codigo);
        }
    });
}
