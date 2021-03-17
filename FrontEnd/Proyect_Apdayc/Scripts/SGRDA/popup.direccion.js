/*INICIO CODIGO PARA LA CARGA DE POPUP DIRECCION*/
var K_CLASE_DIRECCION = {
                            URBANIZACION: "URB",
                            INTERIOR: "INT",
                            ETAPA: "ETP",
                            VIA: "VIA"
                        };


function loadComboTipoDireccion(control,tipo, valSel ) {
    $('#'+control+' option').remove();
    $.ajax({
        url: '../General/ListarRutas',
        data: { tipo:tipo },
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, entidad) {
                    if (entidad.Value == valSel)
                        $('#'+control).append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
                    else
                        $('#'+control).append($("<option />", { value: entidad.Value, text: entidad.Text }));
                });
            } else {
                alert("Error al cargar lista desplegble TIPO"+tipo+" .. "+dato.message);
            }
        }
    });

}
function loadComboTipoUrb(idTipoEdit) {
    loadComboTipoDireccion("ddlUrbanizacion", K_CLASE_DIRECCION.URBANIZACION, idTipoEdit);
}
function loadComboTipoDptoPiso(idTipoEdit) {
    loadComboTipoDireccion("ddlDepartamento", K_CLASE_DIRECCION.INTERIOR, idTipoEdit);
}
function loadComboTipoVia(idTipoEdit) {
    loadComboTipoDireccion("ddlAvenida", K_CLASE_DIRECCION.VIA, idTipoEdit);
}
function loadComboTipoEtapa(idTipoEdit) {
    loadComboTipoDireccion("ddlEtapa", K_CLASE_DIRECCION.ETAPA, idTipoEdit);
}

function loadComboCodPostal(idTipoEdit) {
    $('#ddlCodPostal option').remove();
    var dato = [{ Text: '-', Value: '0' }, { Text: '7899', Value: '1' }, { Text: '3434', Value: '2' }];
    $.each(dato, function (indice, entidad) {
        if (entidad.Value == idTipoEdit)
            $('#ddlCodPostal').append($("<option />", { value: entidad.Value, text: entidad.Text, selected: true }));
        else
            $('#ddlCodPostal').append($("<option />", { value: entidad.Value, text: entidad.Text }));
    });
}


function loadComboTerritorio(idTipoEdit) {
    $('#ddlTerritorio option').remove();
    $.ajax({
        url: '../General/ListarTerritorio',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, territorio) {
                    if (idTipoEdit!=0 && territorio.Value == idTipoEdit)
                        $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text, selected: true }));
                    else {
                        if ( idTipoEdit==0 && territorio.Text == "PERU") {
                            $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text, selected: true }));
                        } else {
                            $('#ddlTerritorio').append($("<option />", { value: territorio.Value, text: territorio.Text }));
                        }
                    }
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function loadComboDireccion(idTipoEdit) {
    $('#ddlTipoDireccion option').remove();
    $.ajax({
        url: '../General/ListarTipoDireccion',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var datos = dato.data.Data;
                $.each(datos, function (indice, tipoDireccion) {
                    if (tipoDireccion.Value == idTipoEdit)
                        $('#ddlTipoDireccion').append($("<option />", { value: tipoDireccion.Value, text: tipoDireccion.Text, selected: true }));
                    else
                        $('#ddlTipoDireccion').append($("<option />", { value: tipoDireccion.Value, text: tipoDireccion.Text }));
                });
            } else {
                alert(dato.message);
            }
        }
    });
}

function initFormDireccion(modalParam) {
    var shtml = ' <div title="Agregar Direccion" id="'+modalParam.id+'">';
    shtml += '   <table width="100%" border="0" id="FiltroTablaPop"> ';
    shtml += ' <tr> ';
    shtml += '  <td> Tipo de Dir., :</td> ';
    shtml += ' <td>  <input type="hidden" id="hidAccionMvDir"><input type="hidden" id="hidEdicionDir" />';
    shtml += ' <select id="ddlTipoDireccion"> ';
    shtml += '      </select> ';
    shtml += '      </td> ';
    shtml += '      <td> Territorio :</td> ';
    shtml += '       <td> ';
    shtml += '            <select name="ddlTerritorio" id="ddlTerritorio"> ';
    shtml += '            </select> ';
    shtml += '         </td> ';
    shtml += '         <td rowspan="3"> ';
    shtml += '         </td> ';
    shtml += '     </tr> ';
    shtml += '     <tr> ';
    shtml += '        <td rowspan="2" colspan="2"> ';
    shtml += '             <table border=0 style=" width:100%;"> ';
    shtml += '             <tr> ';
    shtml += '                  <td> ';
    shtml += '                   <select  id="ddlUrbanizacion"> ';
    shtml += '                   </select> ';
    shtml += '                 </td> ';
    shtml += '                 <td><input type="text" id="txtUrb" placeholder="Nombre Urb/Resd" class="__requeridoMV" name="txtUrb" maxlength="40" size="35"/> ';
    shtml += '                  </td> ';
    shtml += '               </tr> ';
    shtml += '               <tr> ';
    shtml += '                       <td colspan="6"> ';
    shtml += '                           <input type="text" id="txtNro" placeholder="Nro."   maxlength="10" size="5"/> ';
    shtml += '                         <input type="text" id="txtMz" placeholder="Mz."   maxlength="10" size="5"/> ';
    shtml += '                         <input type="text" id="txtLote" placeholder="Lote"     maxlength="10" size="5"/> ';
    shtml += '                         <select name="ddlDepartamento" id="ddlDepartamento"></select> ';
    shtml += '                        <input type="text" id="txtNroPiso" placeholder="Dpt"  maxlength="50" size="13"/> ';
    shtml += '                    </td> ';
    shtml += '            </tr> ';
    shtml += '            <tr> ';
    shtml += '               <td> ';
    shtml += '                   <select name="ddlAvenida" id="ddlAvenida"></select> ';
    shtml += '              </td> ';
    shtml += '                  <td><input type="text" id="txtAvenida" placeholder="Nombre Av/Jr/Calle"     maxlength="50" size="35"/></td> ';
    shtml += '           </tr> ';
    shtml += '          <tr> ';
    shtml += '            <td> ';
    shtml += '               <select name="ddlEtapa" id="ddlEtapa"></select> ';
    shtml += '          </td> ';
    shtml += '              <td><input type="text" id="txtEtapa" placeholder="Nombre Etp/Sec/Zna"    maxlength="80" size="35"/></td> ';
    shtml += '      </tr> ';
    shtml += '      <tr> ';
    shtml += '          <td>Código Postal:</td> ';
    shtml += '          <td> ';
    shtml += '             <select name="ddlCodPostal" id="ddlCodPostal"></select>';
    shtml += '         </td>';
    shtml += '     </tr>';
    shtml += '   </table>';
    shtml += '  </td>';
    shtml += '   <td> Ubigeo :</td>';
    shtml += '  <td>';
    shtml += '      <input type="text" id="txtUbigeo"   class="requeridoMV"    size="50"/>';
    shtml += '      <input type="hidden" id="hidCodigoUbigeo" />';
    shtml += '  </td>';
    shtml += ' </tr>';
    shtml += '  <tr>';
    shtml += '     <td> Ref. :</td>';
    shtml += '     <td><input type="text" id="txtReferencia"  class="__requeridoMV"   maxlength="15000" size="50"/></td>';
    shtml += '  </tr>';
    shtml += '  <tr>';
    shtml += '      <td colspan="5">';
    shtml += '          </hr>';
    shtml += '      </td>';
    shtml += '  </tr>';
    shtml += ' <tr><td  colspan="5">';
    shtml += ' <center><div id="avisoMV" style=" width: 100% ; vertical-align:  middle; "></div></center>';
    shtml += ' </td></tr>';
    shtml += ' </table> ';
    shtml += ' </div> ';

 
     $("#" + modalParam.parentControl).append(shtml);
  


    /*SE VERIFICA SE SE ENVIO LOS DATOS PARA LA EDICION*/
    if (modalParam.data != undefined) {
        var entidad = modalParam.data;
        loadComboDireccion(entidad.idTipoDireccion);
        loadComboTerritorio(entidad.idTipoTerritorio);
        loadComboTipoEtapa(entidad.idTipoEtapa);
        loadComboTipoVia(entidad.idTipoVisa);
        loadComboTipoDptoPiso(entidad.idTipoDptoPiso);
        loadComboTipoUrb(entidad.idTipoUrb);
        loadComboCodPostal(entidad.idCodPostal);
    } else {
        loadComboDireccion(0);
        loadComboTerritorio(0);
        loadComboTipoEtapa(0);
        loadComboTipoVia(0);
        loadComboTipoDptoPiso(0);
        loadComboTipoUrb(0);
        loadComboCodPostal(0);
        initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
        //initAutoCompletarRazonSocial("txtUbigeo", "hidCodigoUbigeo");
    }
   

    $("#"+modalParam.id).dialog({
        autoOpen: false,
        width: modalParam.width,
        height: modalParam.height,
        buttons: {
            "Grabar": modalParam.evento,
            "Cancelar": function () { $("#" + modalParam.id).dialog("close"); }
        }, modal: modalParam.modal
    });

    //$('#txtNro').on("keypress", function (e) { return solonumeros(e); });
}
function initFormDireccionNMV(modalParam) {
    var shtml = ' <div title="Agregar Direccion">';
    shtml += '   <table width="70%" border="0" > ';
    shtml += ' <tr> ';
    shtml += '  <td> Tipo de Dirección :</td> ';
    shtml += ' <td> <input type="hidden" id="hidAccionMvDir"><input type="hidden" id="hidEdicionDir" />';
    shtml += ' <select id="ddlTipoDireccion"> ';
    shtml += '      </select> ';
    shtml += '      </td> ';
    shtml += '      <td> Territorio :</td> ';
    shtml += '       <td> ';
    shtml += '            <select name="ddlTerritorio" id="ddlTerritorio" style="width: 88%">';
    shtml += '            </select> ';
    shtml += '         </td> ';
    shtml += '         <td rowspan="3"> ';
    shtml += '         </td> ';
    shtml += '     </tr> ';
    shtml += '     <tr> ';
    shtml += '        <td rowspan="2" colspan="2"> ';
    shtml += '             <table border=0 style=" width:100%;"> ';
    shtml += '             <tr> ';
    shtml += '                  <td> ';
    shtml += '                   <select  id="ddlUrbanizacion"> ';
    shtml += '                   </select> ';
    shtml += '                 </td> ';
    shtml += '                 <td><input type="text" id="txtUrb" placeholder="Nombre Urb/Resd" class="requeridoMV" name="txtUrb" maxlength="40" size="37"/> ';
    shtml += '                  </td> ';
    shtml += '               </tr> ';
    shtml += '               <tr> ';
    shtml += '                       <td colspan="6"> ';
    shtml += '                           <input type="text" id="txtNro" placeholder="Nro."   maxlength="10" size="5"/> ';
    shtml += '                         <input type="text" id="txtMz" placeholder="Mz."   maxlength="10" size="5"/> ';
    shtml += '                         <input type="text" id="txtLote" placeholder="Lote"     maxlength="10" size="5"/> ';
    shtml += '                         <select name="ddlDepartamento" id="ddlDepartamento"></select> ';
    shtml += '                        <input type="text" id="txtNroPiso" placeholder="Dpt"  maxlength="10" size="5"/> ';
    shtml += '                    </td> ';
    shtml += '            </tr> ';
    shtml += '            <tr> ';
    shtml += '               <td> ';
    shtml += '                   <select name="ddlAvenida" id="ddlAvenida"></select> ';
    shtml += '              </td> ';
    shtml += '                  <td><input type="text" id="txtAvenida" placeholder="Nombre Av/Jr/Calle"     maxlength="80" size="37"/></td> ';
    shtml += '           </tr> ';
    shtml += '          <tr> ';
    shtml += '            <td> ';
    shtml += '               <select name="ddlEtapa" id="ddlEtapa"></select> ';
    shtml += '          </td> ';
    shtml += '              <td><input type="text" id="txtEtapa" placeholder="Nombre Etp/Sec/Zna"    maxlength="80" size="37"/></td> ';
    shtml += '      </tr> ';
    shtml += '      <tr> ';
    shtml += '          <td>Código Postal:</td> ';
    shtml += '          <td> ';
    shtml += '             <select name="ddlCodPostal" id="ddlCodPostal"></select>';
    shtml += '         </td>';
    shtml += '     </tr>';
    shtml += '   </table>';
    shtml += '  </td>';
    shtml += '   <td> Ubigeo :</td>';
    shtml += '  <td>';
    shtml += '      <input type="text" id="txtUbigeo"   class="requeridoMV"    size="50"/>';
    shtml += '      <input type="hidden" id="hidCodigoUbigeo" />';
    shtml += '  </td>';
    shtml += ' </tr>';
    shtml += '  <tr>';
    shtml += '     <td> Ref. :</td>';
    shtml += '     <td><input type="text" id="txtReferencia"  class="requeridoMV"   maxlength="40" size="50"/></td>';
    shtml += '  </tr>';
    shtml += '  <tr>';
    shtml += '      <td colspan="5">';
    shtml += '          </hr>';
    shtml += '      </td>';
    shtml += '  </tr>';
    shtml += ' <tr><td  colspan="5">';
    shtml += ' <center><div id="avisoMV" style=" width: 100% ; vertical-align:  middle; "></div></center>';
    shtml += ' </td></tr>';
    shtml += ' </table> ';
    shtml += ' </div> ';


    $("#" + modalParam.parentControl).append(shtml);
    




    /*SE VERIFICA SE SE ENVIO LOS DATOS PARA LA EDICION*/
    //if (modalParam.data != undefined) {
    //    var entidad = modalParam.data;
    //    loadComboDireccion(entidad.idTipoDireccion);
    //    loadComboTerritorio(entidad.idTipoTerritorio);
    //    loadComboTipoEtapa(entidad.idTipoEtapa);
    //    loadComboTipoVia(entidad.idTipoVisa);
    //    loadComboTipoDptoPiso(entidad.idTipoDptoPiso);
    //    loadComboTipoUrb(entidad.idTipoUrb);
    //} else {
        loadComboDireccion(0);
        loadComboTerritorio(0);
        loadComboTipoEtapa(0);
        loadComboTipoVia(0);
        loadComboTipoDptoPiso(0);
        loadComboTipoUrb(0);
        loadComboCodPostal(0);
        initAutoCompletarUbigeo("txtUbigeo", "hidCodigoUbigeo");
       
    //}


    //$("#" + modalParam.id).dialog({
    //    autoOpen: false,
    //    width: modalParam.width,
    //    height: modalParam.height,
    //    buttons: {
    //        "Grabar": modalParam.evento,
    //        "Cancelar": function () { $("#" + modalParam.id).dialog("close"); }
    //    }, modal: modalParam.modal
    //});

}
/*FIN CODIGO PARA LA CARGA DE POPUP DIRECCION*/