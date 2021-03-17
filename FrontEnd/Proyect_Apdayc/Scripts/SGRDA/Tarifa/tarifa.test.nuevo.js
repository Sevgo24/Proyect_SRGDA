/************************** INICIO CONSTANTES****************************************/
var K_ACCION = { Nuevo: "I", Modificacion: "U" };
var K_ACCION_ACTUAL = "I";
var K_MENSAJE_SIN_RESULTADO = "Con los datos ingresados no se obtuvieron valores.";
var KP = "keypress";
/************************** INICIO CARGA********************************************/
$(function () {
    Limpiar();
    $("#txtCodigo").focus();
    mvInitBuscarTarifa({ container: "ContenedormvBuscarTarifa", idButtonToSearch: "btnBuscarTarifa", idDivMV: "mvBuscarTarifa", event: "reloadEventoTarifa", idLabelToSearch: "lbTarifa" });
    //ObtenerVUM();
    //-------------------------- EVENTO CONTROLES -----------------------------------  
    $("#btnCalcular").on("click", function () {
        Calcular()
    });
    $("#btnBuscarTarifa").on("click", function () {
        limpiarBusquedaTari();
        loadDataFoundTari();
    });

});


//****************************  FUNCIONES ****************************
Number.prototype.roundTo = function (num) {
    var resto = this % num;
    if (resto <= (num / 2)) {
        return this - resto;
    } else {
        return this + num - resto;
    }
}

var reloadEventoTarifa = function (idSel) {
    $("#hidId").val(idSel);
    obtenerNombreConsultaTarifa($("#hidId").val());
    ObtenerDatos($("#hidId").val());
};

function obtenerNombreConsultaTarifa(idSel) {
    $.ajax({
        data: { Id: idSel },
        url: '../General/ObtenerNombreTarifa',
        type: 'POST',
        beforeSend: function () { },
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                $("#lbTarifa").html(dato.valor);
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function Calcular() {
    var estadoRequeridos = ValidarRequeridos();
    if (estadoRequeridos) {
        ObtenerCaracteristicaValor();
    }
}

function ObtenerDatos(id) {
    $.ajax({
        url: "../TarifaTest/Obtener",
        data: { id: id },
        type: 'POST',
        success: function (response) {
            var dato = response;
            if (dato.result == 1) {
                var req = dato.data.Data;
                validarRedirect(req);
                if (req != null) {
                    $("#txtTarifa").val(req.RATE_DESC);
                    $("#txtProducto").val(req.MOD_DEC);
                    $("#txtPeriodocidad").val(req.RAT_FDESC);
                    $("#txtVum").val(req.VUM);
                    loadDataCaracteristica();
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function ObtenerVUM() {
    $.ajax({
        url: "../TarifaTest/ObtieneVUM",
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    $("#txtVum").val(req.VUM_VAL);
                }
            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });
}

function CalcularTarifa() {
    $.ajax({
        url: "../TarifaTest/Calcular",
        type: 'POST',
        data: { VUMcalcular: $('#txtVum').val() },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                var req = dato.data.Data;
                if (req != null) {
                    var tipoFormula = req.FormulaTipo;
                    var tipoMinimo = req.MinimoTipo;
                    var decFormula = req.FormulaDec;
                    var decMinimo = req.MinimoDec;
                    var formula = parseFloat(req.ValorFormula);
                    var minimo = parseFloat(req.ValorMinimo);

                    $('#txtValor').val(formula.toFixed(decFormula));
                    $('#txtMinimo').val(minimo.toFixed(decMinimo));
                }
            } else if (dato.result == 0) {
                $('#txtValor').val(0);
                $('#txtMinimo').val(0);
                alert(K_MENSAJE_SIN_RESULTADO);
            }
        }
    });
}
//-------------------------- GRILLA - CARACTERISTICA -------------------------------------  
function loadDataCaracteristica() {
    loadDataCaracteristicaGridTmp('ListarCaracteristica', "#gridCaracteristica");
}

function loadDataCaracteristicaGridTmp(Controller, idGrilla) {
    $.ajax({
        type: 'POST', url: Controller, beforeSend: function () { },
        success: function (response) {
            var dato = response;
            validarRedirect(dato);
            if (dato.result == 1) {
                $(idGrilla).html(dato.message);
                var cantidadRegistros = dato.Code;
                $('#txtNumVariable').val(cantidadRegistros);
                addvalidacionSoloNumeroValor();
            } else if (dato.result == 0) {
                alert(dato.message);
            }

        }
    });
}

function addvalidacionSoloNumeroValor() {
    $('#tblCaracteristica tr').each(function () {
        var id = $(this).find("td").eq(0).html();
        if (id != null)
            $('#txt' + id).on("keypress", function (e) { return solonumeros(e); });
    });
}

function ObtenerCaracteristicaValor() {
    //alert("Obtiene Caracteristica");
    var CaracteristicaValor = [];
    var contador = 0;
    $('#tblCaracteristica tr').each(function () {

        var letra = $(this).find("td").eq(0).html();
        if (letra != null) {
            CaracteristicaValor[contador] = {
                Letra: letra,
                Valor: quitarformatoMoneda ($('#txt' + letra).val())
            };
            contador += 1;
        }
    });

    var CaracteristicaValor = JSON.stringify({ 'CaracteristicaValor': CaracteristicaValor });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../TarifaTest/ObtenerCaracteristicaValor',
        data: CaracteristicaValor,
        success: function () {
            //var dato = response;
            //validarRedirect(dato);
            //if (dato.result == 1) {
                CalcularTarifa();
            //} else if (dato.result == 0) {
            //    alert(dato.message);
            //}
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function quitarformatoMoneda(valorTarifa) {
    var valor = ''
    valor = valorTarifa.replace(',', '').replace(',', '').replace(',', '').replace(',', '');
    return valor;
}

function Limpiar() {
    $("#txtTarifa").val('');
    $("#hidCodigo").val(0);
    $("#txtProducto").val('');
    $("#txtPeriodocidad").val('');
    $("#txtNumVariable").val('');
    $("#txtVum").val('');
    $("#txtValor").val('');
    $("#txtMinimo").val('');
}

var format = function (num, dec) {
    var str = num.toString().replace("S/. ", ""), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(".") > 0) {
        parts = str.split(".");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] != ",") {
            output.push(str[j]);
            if (i % 3 == 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return ("S/. " + formatted + ((parts) ? "." + parts[1].substr(0, dec) : ""));
};
