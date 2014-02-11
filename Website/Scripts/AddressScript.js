//Nathan
$(document).ready(function () {
    $('#AddressTable').css('display', 'none');
    $('input[id="MainContent_RadioButtonList1_0"]').change(function (event) {
        $('#AddressTable').css('display', 'block');
    });
    $('input[id="MainContent_RadioButtonList1_1"]').change(function (event) {
        $('#AddressTable').css('display', 'none');
    });
});
