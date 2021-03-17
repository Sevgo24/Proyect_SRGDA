var mvInitDetalleFormaPago = function (parametro) {
    var idContenedor = parametro.container;
    var btnEvento = parametro.idButtonToSearch;
    var idModalView = parametro.idDivMV;

    var elemento = '<div id="' + parametro.idDivMV + '"> ';
    elemento += '<input type="hidden"  id="hidIdViewFp" value="' + parametro.idDivMV + '"/>';
    elemento += '<input type="hidden"  id="hidIdEventFp" value="' + parametro.event + ' "/>';

    elemento += '<table border=0 style=" width:100%; border:0px;">';
    //elemento += '<tr>';
    //elemento += '<td colspan="4"><div id="gridFactura"></div></td>';
    //elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td>';
    elemento += '<table border="0" style="border-collapse: collapse;">';
    elemento += '<tr>';
    //elemento += '<td> <a href=# onclick="return GrabarRecibo({});"><img id="btnGrabarRecibo" src="../Images/botones/2save.png" width=20px border=0></a> </td>';
    //elemento += '<td> <lable id="lblRecibo" style="cursor:pointer;">  Grabar Recibo de Ingreso</lable> </td>';
    elemento += '</tr>';
    elemento += '</table>';
    elemento += '</td>';

    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '</tr>';
    elemento += '<tr>';
    elemento += '<td></td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '<table border=0  style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td><div class="contenedor"><table border=0 style=" width:100%; "><tr>';
    elemento += '<td style="width:80px">Forma de pago </td> <td style="width:10px"><select id="ddlFormaPago" style="width: 190px" class="requeridoLst"/></td>';
    elemento += '<td style="width:40px">Valor </td> <td><input type="text" id="txtValor" style="width:60px" class="requerido"></td>';
    elemento += '<input type="hidden" id="hidRecibo">';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:80px">Banco </td> <td style="width:10px"><select id="ddlBanco" style="width: 190px"/></td>';
    elemento += '<td style="width:40px">Sucursal </td> <td style="width:10px"><select id="ddlSucursal" style="width: 190px"/></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:80px">Fecha depósito </td> <td><input type="text" id="txtFecha" class="requerido"></td>';
    elemento += '<td style="width:40px">Referencia </td> <td><input type="text" id="txtReferencia" style="width:350px" maxlength="30"></td>';
    elemento += '</tr>';

    elemento += '<tr>';
    elemento += '<td style="width:80px">Cuenta </td> <td style="width:10px"><select id="ddlCuenta" style="width: 190px"/></td>';
    elemento += '</tr>';

    elemento += '</tr>';
    elemento += '</table></div>';
    elemento += '</td>';
    elemento += '</tr>';


    //elemento += '<td>';
    //elemento += '<div> <img src="../Images/botones/add.png" class="addFormaPago" height="30"></div>';
    //elemento += '<div style=" margin-top:-30px; margin-left:30px;"> <a href="#" class="addFormaPago">Agregar </a></div> <hr />';
    //elemento += '</td>';


    //elemento += '<td>';
    //elemento += '<table border="0" style="border-collapse: collapse;">';
    //elemento += '<tr>';
    //elemento += '<td> <img src="../Images/botones/add.png" class="addFormaPago" height="30"> </td>';
    //elemento += '<td> <label id="btnAgregar" style="cursor:pointer;" class="addFormaPago"> Agregar</label> </td>';
    //elemento += '</tr>';
    //elemento += '</table>';
    //elemento += '</td>';


    //elemento += '<tr>';
    ////&nbsp;&nbsp;<button id="btnLimpiar"> <img src="../Images/botones/refresh.png"  width="16px"> Limpiar </button>
    //elemento += '<td colspan="4"><button id="btnAgregar" class="boton">Guardar <img src="../Images/botones/save.png"  width="24px">  </button>';
    //elemento += '</td>';
    //elemento += '</tr>';


    //elemento += '<table border=0  style=" width:100%; border:0px; id="FiltroTabla"">';
    //elemento += '<thead><tr>';
    //elemento += '<th class=' + clase + 'style="width:30px">Forma de pago</th>';
    //elemento += '<th class=' + clase + 'style="width:30px">Entidad</th>';
    //elemento += '<th class=' + clase + 'style="width:30px">Fecha Operación</th>';
    //elemento += '<th class=' + clase + 'style="width:50px">Voucher / N Ope. - Tarj</th>';
    //elemento += '<th class=' + clase + 'style="width:30px">Moneda</th>';
    //elemento += '<th class=' + clase + 'style="width:30px">Importe Rec.</th>';
    //elemento += '<th class=' + clase + 'style="width:30px">TC</th>';
    //elemento += '<th class=' + clase + 'style="width:10px"></th>';
    //elemento += '</table>';

    //elemento += '<tr>';
    //elemento += '<td colspan="4"><div id="gridDetallePago"></div></td>';
    //elemento += '</tr>';

    elemento += '</table>';

    elemento += '<table border=0  style=" width:100%; border:0px;">';
    elemento += '<tr>';
    elemento += '<td style="height: 30px; text-align:center" colspan="20">';
    elemento += '<center>';
    elemento += '<div id="avisoMetodoPago" style=" width: 100% ; vertical-align:  middle; "></div>';
    elemento += '</center>';
    elemento += '</td>';
    elemento += '</tr>';
    elemento += '</table>';

    elemento += '</div>';

    //$("#" + parametro.container).append(elemento);
    //$("#mvDetalleFormaPago").dialog({
    //    modal: true,
    //    autoOpen: false,
    //    width: 820,
    //    height: 400,
    //    title: "Detalle de Formas de Pago"
    //});

    $("#" + parametro.container).append(elemento);
    //$("#" + parametro.idButtonToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); });
    //if (parametro.idLabelToSearch != undefined) { $("#" + parametro.idLabelToSearch).on("click", function () { $("#" + parametro.idDivMV).dialog("open"); }); }

    $("#" + parametro.idDivMV).dialog({
        modal: true,
        autoOpen: false,
        width: 750,
        height: 300,
        title: "Métodos de pago.",
        buttons: { "Agregar": Agregar, "Cancelar": function () { $("#mvDetalleFormaPago").dialog("close"); } }
    });

    LoadMetodoPago('ddlFormaPago', 0);
    loadBancos('ddlBanco', 0);
    $("#ddlBanco").change(function () {
        var codigo = $("#ddlBanco").val();
        LoadrSucursalesBanco('ddlSucursal', codigo, '0');
    });

    $("#ddlSucursal").change(function () {
        //var banco = $("#ddlBanco").val();
        var sucursal = $("#ddlSucursal").val();
        //var socio = $("#hidCodigoBPS").val();
        //loadCuentaBancaria('ddlCuenta', banco, sucursal, socio, '0')
        loadCuentaBancaria('ddlCuenta', sucursal, '0')
    });

    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    //$(".addFormaPago").on("click", function () {
    //    //limpiar();
    //    GrabraMetodoPago();
    //});


};

//function GrabraMetodoPago() {
//    if (ValidarRequeridos()) {
//        var IdAdd = 0;
//        var entidad = {
//            //REC_ID:   ("#hidRecibo$").val(),
//            REC_ID: 1, // solo para pruebas aun falta el insertar recibo
//            REC_PWID: $("#ddlFormaPago").val(),
//            REC_PVALUE: $("#txtValor").val(),
//            REC_DATEDEPOSITE: $("#txtFecha").val(),
//            //REC_CONFIRMED: "S", //Crear función para obtener el estado de la confirmación según el método de pago     
//            BNK_ID: $("#ddlBanco").val(),
//            BRCH_ID: $("#ddlSucursal").val(),
//            BACC_NUMBER: $("#ddlCuenta").val(),
//            REC_REFERENCE: $("#txtReferencia").val()
//        };
//        $.ajax({
//            url: 'Cobro/GrabarDetalleMetodoPago',
//            type: 'POST',
//            data: entidad,
//            beforeSend: function () { },
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato);
//                if (dato.result == 1) {
//                    alert(dato.message);
//                    loadDataDetalloMetodoPago(1);
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//    }
//}

//function AgregarFormaPago() {
//    var IdAdd = 0;
//    var entidad = {
//        //Id: IdAdd,
//        IdMetodoPago: $("#ddlFormaPago").val(),
//        ValorIgreso: $("#txtValor").val(),
//        ConfirmacionIngreso: "S", //crear funcion para obtener el estado de la confirmacion segun el metodo de pago
//        FechaConfirmacion:  $("#txtFecha").val(),
//        UsuarioConfirmacionIngreso: "",
//        CodigoConfirmacionIngreso: ""
//    };
//    if (ValidarRequeridosET()) {
//        $.ajax({
//            url: '..Cobro/AddFormaPago',
//            type: 'POST',
//            data: entidad,
//            beforeSend: function () { },
//            success: function (response) {
//                var dato = response;
//                validarRedirect(dato);
//                if (dato.result == 1) {
//                    //loadDataAsociado();
//                } else if (dato.result == 0) {
//                    alert(dato.message);
//                }
//            }
//        });
//    }
//}
