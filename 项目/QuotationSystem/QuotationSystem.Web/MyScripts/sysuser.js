$(function () {
    clearErrState("#oldPwd")
    clearErrState("#newPwd1")
    clearErrState("#newPwd2")

    $("#editPwd").click(function () {
        clearWindows();
        $("#editPwdWindow").window("open");
    });

    $("#save").click(function () {
        if (emptyFocusMithMsg("#oldPwd", "#errMsg", "请输入旧密码!"))
            return
        if (lengthErrFocusMithMsg("#oldPwd", "#errMsg", "旧密码过短!", 6))
            return

        if (emptyFocusMithMsg("#newPwd1", "#errMsg", "请输入新密码!"))
            return

        if (lengthErrFocusMithMsg("#newPwd1", "#errMsg", "新密码过短!", 6))
            return

        if (emptyFocusMithMsg("#newPwd2", "#errMsg", "请输入确认密码!"))
            return

        if ($("#newPwd2").val().trim() != $("#newPwd1").val().trim()) {
            $("#newPwd2").addClass("input-validation-error").focus();
            $("#errMsg").html("<span class='text-danger'>两次密码不匹配!</span>")
            return;
        }

        $("#errMsg").html("<span class='fa fa-spinner fa-spin text-info'></span>&nbsp;&nbsp;<span>改密中...</span>")
        $.post("/SystemUser/EditPassword", { oldPwd: $("#oldPwd").val(), newPwd: $("#newPwd1").val() },
            function (data) {
                if (!data.result) {
                    $("#errMsg").html(`<span class='fa fa-warning text-warning'>&nbsp;&nbsp;${data.msg}</span>`)
                }
                else {
                    $("#editPwdWindow").window("close");
                    showTip(data.msg, 3000)
                }
            })
    })
    $("#close").click(function () {
        $("#editPwdWindow").window("close");
    })

    $("#btnEdit").click(function () {
        window.parent.addTab("个人资料修改", "fa fa-pencil", "/SystemUser/ShowUserInfoEdit",true)
    })
})

function clearWindows() {
    $("#oldPwd").val("");
    $("#newPwd1").val("");
    $("#newPwd2").val("");
    $("#errMsg").html("");
}