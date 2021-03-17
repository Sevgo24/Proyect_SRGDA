$(function () {
    $("#pagination-slider").paginate({
        count: Math.ceil($("#hdfTotal").val() / 5),
        start: $("#hdfPagina").val(),
        display: 3,
        border: false,
        text_color: "#888",
        background_color: "#EEE",
        text_hover_color:"black",
        background_hover_color: "CFCFCF",
        first_text: "First",
        last_text: "Last    ",
        onChange: function (e) {
            $("#hdfPagina").val(e);
            $("form:first").submit();
        }
    }); 
});