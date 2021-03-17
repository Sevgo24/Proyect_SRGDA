$(function () {
    var cultura = $("meta[name='accept-language']").attr("content");
    //cambiando la cultura en el cliente
    Globalize.culture(cultura);
    $("#txtTexto1").val(Globalize.format(420.15, "c"));

    //$.getJSON(...){
    //llenar una variable con todos los mensajes de esta vista
    Globalize.addCultureInfo(cultura, {
        messages: {
            "TITGLREG001": "Registro de usuario.",
            "TITGLREG002": "Registrar."
        }
    });
    //}
    $("#txtTexto2").val(Globalize.localize("TITGLREG001"));
});