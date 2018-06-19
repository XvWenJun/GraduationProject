$(function () {
    $("#switchbutton").switchbutton({
        checked: false,
        onChange: function (checked) {
            $(".autoAdd").css("visibility", checked ? 'visible' : 'hidden')
        }
    });

    var category = $("#category").val();
    $("#combotree").combotree({
        url: '/SystemProduct/GetCategories',
        value: category > 0 ? category : "",
        onChange: function (newValue, oldValue) {
            if (newValue > 0) {
                $(this).next('span').removeClass("input-validation-error").find('input').css("background-color", "#fff").focus();
            }
        }
    })

    $("input.easyui-numberbox").numberbox({
        onChange: function (newValue, oldValue) {
            if (newValue.trim() != "")
                $(this).next('span').removeClass("input-validation-error").find('input').css("background-color", "#fff");
        }
    })

    clearErrState(":input")

    $("#btnSave").click(function () {
        if (emptyFocus("#name"))
            return
        if (combotreeEmptyFocus("#combotree"))
            return

        var checked = $("#switchbutton").switchbutton('options').checked;
        if (checked) {
            if (numberboxEmptyFocus("#autoAdd"))
                return;
        }

        if (emptyFocus("#unit"))
            return

        if (numberboxEmptyFocus("#cost"))
            return

        if (numberboxEmptyFocus("#price1"))
            return

        if (!checked) {
            if (OtherNumberboxeEmptyFocus())
                return
        }
        else {
            $(".easyui-numberbox").next('span').removeClass("input-validation-error").find('input').css("background-color", "#fff");
            calculateOtherNumberboxVal();
        }

        postData("/SystemProduct/EditProductInfo", $("form").serializeArray())
    });

    initImageUploader("#picker", "/SystemProduct/UploadAvatar", $("#id").val(), "#avatar", function () {
        if ($("#isNotAdd").val() == "false")
            window.parent.datagridReload("#productList");
    })
})

function OtherNumberboxeEmptyFocus() {
    for (var i = 2; i <= $("#priceCount").val() ; i++) {
        if (numberboxEmptyFocus(`#price${i}`))
            return true;
    }
    return false;
}

function calculateOtherNumberboxVal() {
    var price1 = $("#price1").val();
    var add = $("#autoAdd").val();
    for (var i = 2; i <= $("#priceCount").val() ; i++)
        $(`#price${i}`).numberbox('setValue', price1 * (1 + (i - 1) * add / 100));
}

function combotreeEmptyFocus(name) {
    var value = $(name).combotree('getValue')
    if (value.trim() == "") {
        $(name).next('span').addClass("input-validation-error").find('input').css("background-color", "#fee").focus();
        return true;
    }
    return false;
}

function numberboxEmptyFocus(name) {
    var value = $(name).val();
    if (value.trim() == "" || value == 0) {
        $(name).numberbox('clear');
        $(name).next('span').addClass("input-validation-error").find('input').css("background-color", "#fee").focus();
        return true;
    }
    return false;
}

function postData(url, data, msg, timeout) {
    var product = data.reduce((info, val) => {
        if(val.name.indexOf("price")==0)
        {
            if (!info.prices)
                info.prices = [];
            info.prices.push(val.value);
        }
        else {
            if (!info.product)
                info.product = {};
            info.product[val.name] = val.value;
        }
        return info;
    },{})
 
    $.post(url, { product:product }, function (data) {
        if (data.result) {
            window.parent.showTip(msg || "修改成功！", timeout || 3000)
            window.parent.datagridReload("#productList");
            $("#isNotAdd").val(false)
        }
        else {
            window.parent.showTip("修改失败，请重试！", 3000)
        }
    })
}