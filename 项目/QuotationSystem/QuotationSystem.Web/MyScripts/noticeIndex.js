$(function () {
    $("#noticeList").datagrid({
        url: '/Notice/GetNoticeList',
        method: 'post',
        width: SetGridWidthSub(30),
        height: SetGridHeightSub(60),
        fitColumns: true,
        idField: 'id',
        sortName: 'datetime',
        sortOrder: 'desc',
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 15, 20],
        rownumbers: false,
        striped: true,
        autoRowHeight: true,
        singleSelect: true,
        columns: [[
                    { field: 'id', title: '消息编号', width: 10 },
                    { field: 'title', title: '标题', width: 15 },
                    {
                        field: 'message', title: '内容', width: 25, formatter: function (value) {
                            return value.split('；')[0] + "......"
                        }
                    },
                    {
                        field: 'state', title: '状态', width: 10, align: 'center', formatter: function (value) {
                            return value ? '<span class="label label-info ">已 读</span>' : '<span class="label label-success">未 读</span>';
                        }
                    },
                    {
                        field: 'datetime', title: '时间', width: 20, formatter: function (value) {
                            return new Date(Number.parseInt(value.match(/\d+/g)[0])).Format("yyyy年MM月dd日 hh:mm:ss")
                        }
                    }
        ]],
    });

    datagridResize("#noticeList", 30, 60);

    $("#Detail").click(function () {
        var row = getSelectRow("#noticeList");
        if (row == null)
            return

        if (!row.state)
            $.post("/Notice/ReadNoticeById", { id: row.id }, function (data) {
                if (data.result) {
                    datagridReload("#noticeList");
                    window.parent.updateNotReadNotices();
                }
            })

        openNoticeWindow("#modalwindow", 400, 300, 'fa fa-info-circle', row)
    })

    $("#Delete").click(function () {
        var row = getSelectRow("#noticeList");
        if (row == null)
            return

        $.post("/Notice/DeleteNoticeById", { id: row.id }, function (data) {
            if (data.result) {
                showTip("删除成功!", 2000);
                datagridReload("#noticeList");
                $("#noticeList").datagrid('unselectAll');
                window.parent.updateNotReadNotices();
            }
            else
                showTip("删除失败!", 2000);
        })
    })

    $("#allRead").click(function () {
        $.post("/Notice/ReadAllNotices", function (data) {
            if (data.result) {
                showTip("修改成功!", 2000);
                datagridReload("#noticeList");
                $("#noticeList").datagrid('unselectAll');
                window.parent.updateNotReadNotices();
            }
            else
                showTip("修改失败!", 2000);
        })
    })

    $("#deleteRead").click(function () {
        $.post("/Notice/DeleteReadNotices", function (data) {
            if (data.result) {
                showTip("删除成功!", 2000);
                datagridReload("#noticeList");
                $("#noticeList").datagrid('unselectAll');
            }
            else
                showTip("删除失败!", 2000);
        })
    })
})
function searchInfo(query, condition) {
    $("#noticeList").datagrid('load', { condition, query });
    }

function refreshPage() {
    $("#noticeList").datagrid("reload");
    }