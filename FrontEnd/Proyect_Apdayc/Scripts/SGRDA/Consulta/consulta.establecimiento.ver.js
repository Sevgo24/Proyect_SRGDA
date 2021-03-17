
$(function () {
    $("#mvImagen").dialog({ autoOpen: false, width: 700, height: 500, buttons: { "Cancelar": function () { $("#mvImagen").dialog("close"); } }, modal: true });
    id = (GetQueryStringParams("id"));
    if (id !=undefined) {
        obtenerDatos(id);
    }
});

function obtenerDatos(idEst) {
    $.ajax({
        url: '../ConsultaEstablecimiento/ObtenerEstablecimiento',
        data: { idEst: idEst },
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var establecimiento = dato.data.Data;
                $('#txtId').val(establecimiento.EST_ID);
                $('#txtNombre').val(establecimiento.EST_NAME);
                $('#txtBPS').val(establecimiento.BPS_NAME);
                $('#txtTipoEst').val(establecimiento.EST_TYPE);
                $('#txtSubTipoEst').val(establecimiento.EST_SUBTYPE);
                $('#txtDivision').val(establecimiento.DIVISION);
                $('#txtDirVigente').val(establecimiento.ADDRESS);
                $('#txtUbigeo').val(establecimiento.UBIGEO);
                loadDataDireccion();
                loadDataCaracteristica();
                loadDataParametro();
                loadDataAsociado();
                loadDataLicencia();
                loadDataObservacion();
                loadDataDocumento();
                loadDataDifusion();
                
            } if (dato.result == 0) {
                alert(dato.message);
            }
        }

    });
}

function loadDataGridTmp(Controller, idGrilla) {
    $.ajax({ type: 'POST', url: Controller, beforeSend: function () { }, success: function (response) { var dato = response; $(idGrilla).html(dato.message); } });
}

function loadDataDireccion() {
    loadDataGridTmp('ListarDireccion', "#gridDireccion");
}

function loadDataCaracteristica() {
    loadDataGridTmp('ListarCaracteristica', "#gridCaracteristica");
}

function loadDataParametro() {
    loadDataGridTmp('ListarParametro', "#gridParametro");
}

function loadDataAsociado() {
    loadDataGridTmp('ListarAsociado', "#gridAsociado");
}
//load licencia
function loadDataLicencia() {
    loadDataGridTmp('ListarLicencia', "#gridLicencias");
}
function loadDataObservacion() {
    loadDataGridTmp('ListarObservacion', "#gridObservacion");
}

function loadDataDocumento() {
    loadDataGridTmp('ListarDocumento', "#gridDocumentos");
}

function loadDataDifusion() {
    loadDataGridTmp('ListarDifusion', "#gridDifusion");
}

function verImagen(url) {
    $("#mvImagen").dialog("open");    
    $("#ifContenedor").attr("src", url);
    return false;
}