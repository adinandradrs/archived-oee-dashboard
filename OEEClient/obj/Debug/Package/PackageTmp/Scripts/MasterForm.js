$(document).ready(function () {
    //dom manipulation
    $("#accordion").accordion();
    $("#radioset").buttonset();
    $("#submit").button();
    $("#tabs").tabs();

    $(".form").hide();
    $("#tabs").tabs("option", "active", 1);
    $("[formatter=timepicker]").timepicker();

});

function clearSelection() {
    document.getElementById("radio1").checked = false;
    document.getElementById("radio2").checked = false;
    document.getElementById("radio3").checked = false;
}

function clearForm() {
    $('#tableForm')
        .find(':radio, :checkbox').removeAttr('checked').end()
        .find('textarea, :text, :password, select').val('')
    return false;
}