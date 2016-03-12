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


function selectall() {
    
    var $that = $('#ckbAll');
   
    if ($that.is(':checked')) {       
        $("#tablelist").find('table:gt(0) :checkbox').each(function () {
            //$that.parents('.content').find('table:gt(0) :checkbox').each(function () {
            if (!$(this).is(':disabled')) {
                $(this).get(0).checked = true;
            }
        });
    } else {  
        $("#tablelist").find('table:gt(0) :checkbox').removeAttr('checked');
        //$that.parents('.content').find('table:gt(0) :checkbox').removeAttr('checked');
    }

}

function deleterecord(){
    var checkedRows = $('#tablelist').find('table:gt(0) :checked');
    if (checkedRows.length < 1) {
        alert('请选择需要删除的记录！');
        return;
    }
    var isok = confirm("您确定要删除选中的记录吗?");
    if (isok) {
        var urlstring = $('#hdDeleteContract').val();
        //alert(urlstring);
        var contractids = "";
        $.each(checkedRows, function (i, v) {
            //contractids.push($(v).siblings(':hidden[name="hdcontractid"]').val());
            var temp = $(v).siblings(':hidden[name="hdcontractid"]').val();
            if (temp != undefined) {
                if (contractids != "") contractids += ",";
                contractids += temp;
            }
        });

        //alert( "{contractids:["+ contractids+"]}");
        //var dataString = JSON.stringify({ "contractids": contractids });
        var dataString = "{contractids:[" + contractids + "]}";

        jQuery.ajax({
            type: 'post',
            url: urlstring,
            data: dataString,
            contentType: 'application/json',
            success: function (re) {
                //alert("ok");
                window.location.reload(true);
            },
            error: function (msg, status) {
                alert(msg);
            }
        });
    } else {
        //alert("false");
    }
}