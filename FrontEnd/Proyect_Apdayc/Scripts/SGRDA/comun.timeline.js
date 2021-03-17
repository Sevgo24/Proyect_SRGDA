var getTimeLine = function (idRef) {
    $.ajax({
        url: '../Trace/ListarLogTraces',
        data: { codigo: idRef },
        dataType: 'json',
        type: 'POST',
        success: function (response) {
            var dato = response;
            validarRedirect(dato); /*add sysseg*/
            if (dato.result == 1) {
                var json = [];
                var objeto = dato.data.Data;
                var json2 = objeto.rangos;
                $.each(json2, function (index, value) {
                    json.push({
                        title: value.title,
                        description: value.description,
                        startDate: (new Date(value.startDate)),
                        endDate: value.endDate
                    });
                });

                $("#timeline").timeCube({
                    data: json,
                    granularity: "year",
                    startDate: new Date(objeto.desde),
                    endDate: new Date(objeto.hasta),
                    nextButton: $("#next-link"),
                    previousButton: $("#prev-link"),
                    showDate: true,
                    transitionAngle: 60,
                    transitionSpacing: 80,
                });

            } else if (dato.result == 0) {
                alert(dato.message);
            }
        }
    });


};