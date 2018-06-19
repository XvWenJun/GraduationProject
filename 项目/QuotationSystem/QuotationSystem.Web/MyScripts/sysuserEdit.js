var userName;
$(function () {
    var self = !!$("#self").val();
    var province = $("#province").val();
    var city = $("#city").val();
    var region = $("#region").val();
    getAreas("#province", 1);
    if (province != "")
        getChildAreas("#city", province, 1, city);
    if (city != "")
        getChildAreas("#region", city, 2, region);

    $("#province").change(function () {
        getChildAreas("#city", $("#province").val(), 1);
        $("#region").html("");
    })

    $("#city").change(function () {
        getChildAreas("#region", $("#city").val(), 2);
    })

    if(!self)
    $("#levelVal").combobox({
        valueField: 'id',
        textField: 'name',
        url: "/SystemRight/GetLevelList",
        onChange: function (newValue, oldValue) {
            $("#level").val(newValue)
            if (newValue > 0) {
                $(this).next('span').removeClass("input-validation-error").find('input').css("background-color", "#fff").focus();
            }
        }
    });

    var level=$("#level").val();
    if(level!=0 &&!self)
    $("#levelVal").combobox("setValue",level)
    clearErrState(":input")
    clearErrState("select")

    $("#btnSave").click(function () {

        if (!self&&comboboxEmptyFocus("#levelVal"))
            return
        if (emptyFocus("#name"))
            return

        if (telErrFocus("#tel"))
            return
        if (emptyFocus("#company"))
            return

        if (emptyFocus("#province"))
            return

        if (emptyFocus("#city"))
            return
        if (emptyFocus("#region"))
            return

        if (emptyFocus("#area"))
            return

        var type = $("#type").val();
        if (type == "create")
            postData("/SystemUser/EditUserInfo", $("form").serialize(), `修改成功，新用户id:<strong>   ${$("#id").val()} </strong> !`, 5000);
        else {
            userName = $("#name").val();
            postData("/SystemUser/EditUserInfo", $("form").serialize())
        }
    })

    initImageUploader("#picker", "/SystemUser/UploadAvatar", $("#id").val(), "#avatar")

    $("#switchbutton").switchbutton({
        checked: $("#active").val() == "启用",
        onChange: function (checked) {
            $("#active").val(checked ? "启用" : "禁用");
        }
    })

   

    $("#resetPwd").click(function () {
        postData("/SystemUser/ResetPassword", { id: $("#id").val() })
    })
})

function getAreas(ele, level) {
    $.post("/SystemUser/GetAreas", { level: level, name: "" },
        function (data) {
            var name = $(ele).val();
            $(ele).html(packageData(data));
            $(ele).val(name);
        })
}

function getChildAreas(children, name, level, val) {
    $.post("/SystemUser/GetAreas", { level, name },
       function (data) {
           $(children).html(packageData(data));
           $(children).val(val || "");
       })
}

function packageData(data) {
    var html = "";
    data.forEach((value, index) => {
        html += `<option value="${value}">${value}</option>`
    })
    return html;
}

function postData(url, data, msg, timeout) {
    $.post(url, data, function (data) {
        if (data.result) {
            window.parent.showTip(msg || "修改成功！", timeout || 3000)
            var update = $("#updateName").val();
            if (!!update) {
                window.parent.changeUserName(userName);
                $.post("/Account/ChangeUserCookie", { userName});
            }
            else {
                window.parent.datagridReload("#userList");
                $("#isNotAdd").val("false")
            }
        }
        else {
            window.parent.showTip("修改失败，请重试！", 3000)
        }
    })
}

function comboboxEmptyFocus(name) {
    var value = $(name).combobox('getValue')
    if (value.trim() == "") {
        $(name).next('span').addClass("input-validation-error").find('input').css("background-color", "#fee").focus();
        return true;
    }
    return false;
}