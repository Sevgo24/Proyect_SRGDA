
// con . porque es clase
$(function () {

    $(".modalVista").fancybox({
        type: 'ajax',
        transitionIn: 'none',
        transitionOut: 'none',
        keys: {
            close: null
        },
        'closeOnEscape': false,
        'helpers': {
            'overlay': {
                'closeClick':false
            }
        }
    });
});