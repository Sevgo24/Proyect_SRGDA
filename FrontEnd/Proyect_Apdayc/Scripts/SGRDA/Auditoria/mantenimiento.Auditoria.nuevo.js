 
function loadDataAuditoria(IdLicencia) {
    loadDataGridAuditoria('ListarAuditoria', "#gridAuditoria", IdLicencia);
}

function loadDataGridAuditoria(Controller, idGrilla, idSel) {
    $.ajax({
        data: { IdLicencia: idSel },
        type: 'POST', url: "../Auditoria/" + Controller,
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            $(idGrilla).html(dato.message);
        }
    });
}