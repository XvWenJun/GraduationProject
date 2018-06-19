$(function () {
    clearErrState("#id")
    clearErrState("#password")
    $("button").click(function () {
        if (emptyFocus("#id"))
            return
        if (emptyFocus("#password"))
            return
        $("button").attr("disabled", true);
        $("button").html('<i class="fa fa-refresh fa-spin"></i>');
        $.post("/Account/LogInValidate", { id: $("#id").val(), password: $("#password").val(), remeberMe: $("#remeber").is(":checked") },
                    function (data) {
                        $("button").removeAttr("disabled");
                        $("button").html('<span>登录</span>')
                        if (!data.result) {
                            alert(data.msg);
                            $("#id").val("").focus()
                            $("#password").val("")
                        }
                        else {
                            var url = getQueryStringByName("ReturnUrl");
                            window.location.href = "/Home/Index" + (url == null ? "" : "?ReturnUrl=" + url);
                        }
                    }, 'json');
    })
})