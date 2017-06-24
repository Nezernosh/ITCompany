$(document).ready(function () {
    $('.button-collapse').sideNav();
    $('select').material_select();
});

function techErrorToast() {
    Materialize.toast(
        "<i class='material-icons small left'>warning</i><span class=''>Произошла техническая ошибка.<br>Пожалуйста, попробуйте ещё раз</span>",
        2000,
        'red white-text'
    );
}

$(document).on('submit', '.js-ajax-form', function (e) {
    e.preventDefault();

    var form   = $(this);
    var data   = form.serialize();
    var action = form.attr('action');
    var submit = $('button[type="submit"]', form);

    submit.prop('disabled', true);

    $.ajax({
        url: action,
        data: data,
        dataType: 'json',
        method: 'POST',
        complete: function () {
            submit.prop('disabled', false);
        },
        success: function (response) {
            $('.invalid', form).removeClass('invalid');
            if (response.status) {
                location.href = response.redirect;
            } else {
                for (var i in response.errors) {
                    var elem = $('#' + i, form);
                    if (elem.prop('tagName') === 'SELECT') {
                        elem.closest('.select-wrapper').addClass('invalid');
                    } else {
                        elem.addClass('invalid');
                    }

                    $('label[for="' + i + '"]', form).attr('data-error', response.errors[i]).addClass('active');
                }
            }
        },
        error: function () {
            techErrorToast();
        }
    });
});

$(document).on('change', '.switch input', function (e) {
    var self = $(this);
    var active = self.parent().find('.active');

    if (self.is(':checked')) {
        active.css('font-weight', 'bold');
    } else {
        active.css('font-weight', 'normal');
    }
});
