function selectonekey(keyString, key) {
    
    $('#btnOneQuery').html(keyString + " <span class='caret'></span>");

    $('#pkey').val(key);

    //alert(key);
    //alert($('#pkey').val());


}

function s() {
    alert($('#btnOneQuery').text());
}

function sort(sortkey) {
    //alert('1111');

    var key = $('#sortkey').val();
    if (key != sortkey) {
        //alert(sortkey);
        $('#sortkey').val(sortkey);
        $('#sorttype').val("asc");
    } else {
        //alert('2222');
        var type = $('#sorttype').val();
        if (type == "") {
            //alert(type);
            type = "asc";
        } else if (type == "asc") {
            //alert("sss");
            type = "desc";
        } else {
            //alert("sss");
            type = "asc";
        }
        $('#sortkey').val(sortkey);
        $('#sorttype').val(type);

        //alert( $('#sorttype').val());
    }   

    $('#btnquery').click();

}