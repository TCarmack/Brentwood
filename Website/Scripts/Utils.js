//Jack
$(document).ready(function () {
    //Ensures only numbers are input
    $('input[id$="PhoneNumberTextbox"]').keydown(function (event) {
        if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
            event.preventDefault();
        }
    });
});