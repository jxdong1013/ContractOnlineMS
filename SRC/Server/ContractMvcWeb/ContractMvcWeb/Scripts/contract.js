 
$('body').on('click', '#selectFile', function () {
    $('#uploadFile').click();
})
    .on('submit', '#uploadForm', function () {


    //if (formValidator && formValidator.form()) {
        $(this).ajaxSubmit(function (d) {
        //    try {
        //        if ( typeof d === 'string') {
        //            d = JSON.parse(d);
        //        }
        //    } catch (e) {
        //        d = {
        //            status: 1,
        //            msg: '操作失败',
        //            errorMsg: '操作失败'
        //        };
        //    }
            $('#errorTable tr:visible').remove();
            $('#uploadForm')[0].reset();
            $('#displayName').html('');
            if (d.status === 0) {
                $('#successMsg').html(d.msg);
                $('.success_box').show();
                $('.error_box').hide();
                $('.error_details').hide();
                $('.info_return').show();
            } else {
                if (d.result) {
                    $.each(d.result, function (i, v) {
                        var $curTr = $('.rowTpl').clone().removeClass('rowTpl hide');
                        $curTr.find('.errIndex').html(v.priority);
                        $curTr.find('.errStdCode').html(v.icdCode);
                        $curTr.find('.errDiseaseName').html(v.diseaseName);
                        $curTr.find('.errMsg').html(v.errors.join(''));
                        $('#errorTable').append($curTr);
                    });
                }
                $('#errorMsg').html(d.errorMsg);
                $('.success_box').hide();
                $('.error_box').show();
                $('.error_details').show();
                $('.info_return').show();
            }
        });
    //} else {
    //    $('#uploadForm').find('.error:first').focus();
    //}
    return false;
})
.on('change', '#uploadFile', function (e) {
    $('#fileName').val($(this).val());
    $('#displayName').html($(this).val().split('\\').pop());
}).on('click', '#ckbAll', function () {
    var $that = $(this);
    alert('ddd');
    if ($that.is(':checked')) {
        $("#tablelist").find('table:gt(0) :checkbox').each(function(){ 
        //$that.parents('.content').find('table:gt(0) :checkbox').each(function () {
            if (!$(this).is(':disabled')) {
                $(this).get(0).checked = true;
            }
        });
    } else {
        $("#tablelist").find('table:gt(0) :checkbox').removeAttr('checked');
        //$that.parents('.content').find('table:gt(0) :checkbox').removeAttr('checked');
    }
}).on('click', '.content :checkbox[name="checkRow"]', function (e) {
    var rowCount = $('.content :checkbox[name="checkRow"]:not(:disabled)').length,
        checkedCount = $('.content :checked[name="checkRow"]:not(:disabled)').length;
    if (rowCount > checkedCount) {
        $('#ckbAll').removeAttr('checked');
    } else {
        $('#ckbAll').get(0).checked = true;
    }
}).on('click', '#btnDelete', function () {
    var checkedRows = $('.content').find('table:gt(0) :checked');
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
        var dataString = "{contractids:[" + contractids+"]}";

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
});