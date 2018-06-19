var contextMenuIndex = 0;
$(function () {
    //加载顶部一级菜单
    $.post("/Home/GetMenu", function (data) {
        var menuHtml = "";
        for (var i = 0; i < data.length; i++) {
            menuHtml = menuHtml +
                '<td class="topmenu" style="height:50px"><a onclick="loadSubmenu(\'' + data[i].id + '\')" id="' + data[i].id + '" title="' + data[i].text + '" class="l-btn-text bannerMenu ' + '" href="#"><lable class="' + data[i].iconCls + '"></lable><br /><span>' + data[i].text + '</span></a></td>';
        }
        $(".webname").after(menuHtml);
        loadSubmenu(data[0].id);
    }, "json");

    //为tabs绑定右键菜单
    $("#mainTab").tabs({
        onContextMenu: function (e, title, index) {
            contextMenuIndex = index;
            //阻止默认右键菜单
            e.preventDefault();
            $("#tab_menu").menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        },
        onSelect: function (title, index) {
            var refresh = window.frames[title].window.refreshPage;
            if (refresh != null)
                refresh();
        }
    });

    //刷新
    $('#tab_menu-tabrefresh').click(function () {
        var url = $(".tabs-panels .panel").eq(contextMenuIndex).find("iframe").attr("src");
        $(".tabs-panels .panel").eq(contextMenuIndex).find("iframe").attr("src", url);
    });

    //关闭
    $('#tab_menu-tabclose').click(function () {
        if (contextMenuIndex != 0)
            $("#mainTab").tabs('close', contextMenuIndex);
    });
    //关闭所有
    $('#tab_menu-tabcloseall').click(function () {
        $("#mainTab").tabs('select', 0);
        $('.tabs-title').each(function (i, e) {
            if (i != 0)
                $("#mainTab").tabs('close', $(e).text());
        });
    });
    //关闭其他tabs
    $('#tab_menu-tabcloseother').click(function () {
        $("#mainTab").tabs('select', contextMenuIndex);
        $('.tabs-title').each(function (i, e) {
            if (i != 0 && i != contextMenuIndex)
                $("#mainTab").tabs('close', $(e).text());
        });
    });

    $("#notice").click(function () {
        addTab("我的信息", "fa fa-envelope-o", "/Notice/Index", true);
    })

    serverHub = $.connection.ServerHub;
    serverHub.client.updateNotices = function () {
        updateNotReadNotices();
    };

    $.connection.hub.start().done(function () {
        serverHub.server.addOnlineUser($("#receiver").val());
    })

    updateNotReadNotices();
    addTab("主页", "fa fa-home", "/Home/HomePage", false);
    loadReturnUrl();
});

//加载左边二级菜单
function loadSubmenu(parentId) {
    //更改样式
    $(".bannerMenu").removeClass("selected");
    $("#" + parentId).addClass("selected");

    $.post("/Home/GetMenu", {
        "parentId": parentId
    }, function (data) {
        //清空侧边栏
        var rcount = $('#RightAccordion .panel').length;
        for (var i = 0; i < rcount; i++) {
            $('#RightAccordion').accordion("remove", 0);
        }
        var fristTitle;
        $.each(data, function (i, e) {
            if (i == 0)
                fristTitle = e.text;
            var id = e.id;
            $('#RightAccordion').accordion('add', {
                title: e.text,
                content: `<ul id="accordion${id}" class="tree"></ul>`,
                selected: true,
                iconCls: e.iconCls
            });

            $.post("/Home/GetMenu", {
                "parentId": id
            }, function (data) {
                $("#accordion" + id).tree({
                    data: data,
                    onClick: function (node) {
                        addTab(node.text, node.iconCls, node.attributes, true);
                    }
                });
            }, 'json');

            $('#RightAccordion').accordion('select', fristTitle);
        });
    }, "json");

    $('[data-toggle="popover"]').popover();
};

//添加tab
function addTab(subtitle, icon, url, closableFlag) {
    if (!$("#mainTab").tabs('exists', subtitle)) {
        $("#mainTab").tabs('add', {
            title: subtitle,
            content: '<iframe name="' + subtitle + '" frameborder="0" src="' + url + '" scrolling="auto" style="width:100%; height:99.5%;overflow:hidden"></iframe>',
            closable: closableFlag,
            icon: icon
        });
    } else {
        $("#mainTab").tabs('select', subtitle);
    }
}

//查看个人资料
function getUserInfoPage() {
    addTab("个人资料查看", "fa fa-user-o", "/SystemUser/ShowUserInfo", true);
}

//退出登录
function logOut() {
    $.messager.confirm('提醒', '您确定要退出登录吗？', function (result) {
        if (result) {
            $.post("/Account/LogOut");
            window.location.href = "/Account/Index";
        }
    });
}

//加载返回的Url地址
function loadReturnUrl() {
    var url = getQueryStringByName("ReturnUrl");
    if (url == null)
        return;
    url = url.replace(/%2f/g, '/');
    if (url == "/" || url == "/Home/HomePage" || "/Home/Index")
        return;
    addTab("跳转页面", "fa fa-save", url, true)
}

//重命名
function changeUserName(name) {
    $("#userName").text(name);
}

//加载未读消息
function updateNotReadNotices() {
    $.post("/Notice/GetNotReadNotices", function (result) {
        $(".notice").css("display", result != 0 ? "block" : "none");
    })
}

function addNotice(receiver) {
    serverHub.server.addNotice(receiver);
}