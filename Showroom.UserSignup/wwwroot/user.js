$(document).ready(function () {
    $("#add-user-form").on("submit", function (e) {
        e.preventDefault();
        $('.form-control').removeClass('is-invalid');
        var user = objectifyForm($('#add-user-form :input'));
        $.ajax({
            type: "post",
            url: "https://localhost:5001/api/user",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(user),
            success: function (data, textStatus, jqXHR) {
                $("#add-user-form")[0].reset();
            }
        });
    });
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    var errors = jqxhr.responseJSON.errors;
    for (var error in errors) {
        $('#' + error).addClass('is-invalid');
        $('#Invalid' + error).text(errors[error]);
    }
});

function objectifyForm(formArray) {
    var returnArray = { };
    for (var i = 0; i < formArray.length; i++) {
        if (formArray[i]['nodeName'] == 'INPUT') {
            returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
    }
    return returnArray;
}

