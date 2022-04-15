$(document).ready(function () {

    $("#form1").validate({
        rules: {
            userId: {
                required: true
            },
            password: {
                required: true
            }
        },
        messages: {
            userId: {
                required: "<br/>Please provide a User ID"
            },
            password: {
                required: "<br/>Please provide a password"
            }
        },
        submitHandler: function () {
            //ajaxian here
            document.forms["form1"].action = loginAction;
            document.forms["form1"].submit();
        }
    });

});