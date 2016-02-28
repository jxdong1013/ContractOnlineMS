function selectonekey(keyString, key) {
    
    $('#btnOneQuery').html(keyString + " <span class='caret'></span>");

    $('#pkey').val(key);

    //alert(key);
    //alert($('#pkey').val());


}

function s() {
    alert($('#btnOneQuery').text());
}