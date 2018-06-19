//获取querystring
function getQueryStringByName(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return r[2];
    }
    return null
}

//当输入框值改变时清除错误样式
function clearErrState(name) {
    $(name).change(function () {
        if ($(name).val().trim().length > 0)
            $(name).removeClass("input-validation-error");
    })
}

//当输入框值空验证
function emptyFocus(name) {
    if ($(name).val() == undefined || $(name).val().trim() == "") {
        $(name).addClass("input-validation-error").focus();
        return true;
    }
    return false;
}

//输入框值空验证带错误消息
function emptyFocusMithMsg(name, e, msg) {
    if ($(name).val().trim() == "") {
        $(name).addClass("input-validation-error").focus();
        $(e).html(`<span class='text-danger'>${msg}</span>`)
        return true;
    }
    return false;
}
//输入框值长度验证带错误消息
function lengthErrFocusMithMsg(name, e, msg, length) {
    if ($(name).val().trim().length < length) {
        $(name).addClass("input-validation-error").focus();
        $(e).html(`<span class='text-danger'>${msg}</span>`)
        return true;
    }
    return false;
}

//手机号验证
function telErrFocus(name) {
    var reg = /^1[3|4|5|8][0-9]\d{8}$/
    if (!reg.test($(name).val())) {
        $(name).addClass("input-validation-error").focus();
        return true;
    }
    return false;
}

//提示框
function showTip(msg, timeout) {
    $.messager.show({
        iconCls: 'fa fa-info-circle',
        title: '提示',
        msg: msg,
        timeout: timeout,
        showType: 'slide'
    });
}

//datagrid resize
function datagridResize(e, width, height) {
    $(window).resize(function () {
        $(e).datagrid('resize', {
            width: SetGridWidthSub(width),
            height: SetGridHeightSub(height)
        });
    });
}

function datagridResizeFixWidth(e, width, height) {
    $(window).resize(function () {
        $(e).datagrid('resize', {
            width: width,
            height: SetGridHeightSub(height)
        });
    });
}

//datagrid得到当前选中的行
function getSelectRow(e) {
    var row = $(e).datagrid('getSelected');
    if (row == null) {
        showTip("请先选中要操作的对象", 2000);
        return;
    }
    return row;
}

//datagrid openwindw
function openWindow(window, url, height, width, iconCls, title, isAddWindow, deleteUrl) {
    $(window).html(`<iframe frameborder="0" src="${url}" scrolling="auto" style="width:100%; height:98%;overflow:hidden"></iframe>`)
    $(window).window({
        height: height,
        width: width,
        iconCls: iconCls,
        title: title
    });

    if (isAddWindow) {
        $(window).window({
            onBeforeClose: function () {
                if ($('iframe').contents().find("#isNotAdd").val() == "true") {
                    $.post(deleteUrl, { id: $('iframe').contents().find("#id").val() })
                }
            }
        })
    }
    $(window).window('open');
}

function openNoticeWindow(window, width, height, iconCls, data) {
    $(window).html(` <div class="container" style="padding: 15px; height:250px;position:relative; "> <h1 class="text-center" style="font-size:18px;">${data.title}</h1><hr style = "margin-bottom:10px;margin-top:10px;" /><div style=" font-size:14px;">${data.message}</div><div class="text-right" style="position: absolute;bottom: 10px;right: 30px;"><a class="btn btn-link" onclick="noticeJump('${window}','${data.url}')">--> 跳转查看</a> </div></div>`)
    $(window).window({
        height: height,
        width: width,
        iconCls: iconCls,
        title: "公告详情",
    });

    $(window).window('open');
}

function noticeJump(win, url) {
    if (win != null) {
        $(win).window('close');
    }
    if (url == "/Quotation/ShowQuotationInfo")
        window.parent.addTab('报价单操作', 'fa fa-align-justify', url, true)
    else if (url == "/SystemProduct/Index")
        window.parent.addTab('产品操作', 'fa fa-info', url, true)
    else if (url == "/Quotation/ShowQuotationEchart")
        window.parent.addTab('报价单统计', 'fa fa-area-chart', url, true)
}

//datagrid reload
function datagridReload(e) {
    $(e).datagrid('reload');
}

function windowClose(e) {
    $(e).window('close');
}

//格式化日期
Date.prototype.Format = function (fmt) {
    var o = {
        "y+": this.getFullYear(),
        "M+": this.getMonth() + 1,                 //月份
        "d+": this.getDate(),                    //日
        "h+": this.getHours(),                   //小时
        "m+": this.getMinutes(),                 //分
        "s+": this.getSeconds(),                 //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S+": this.getMilliseconds()             //毫秒
    };
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            if (k == "y+") {
                fmt = fmt.replace(RegExp.$1, ("" + o[k]).substr(4 - RegExp.$1.length));
            }
            else if (k == "S+") {
                var lens = RegExp.$1.length;
                lens = lens == 1 ? 3 : lens;
                fmt = fmt.replace(RegExp.$1, ("00" + o[k]).substr(("" + o[k]).length - 1, lens));
            }
            else {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
    }
    return fmt;
}