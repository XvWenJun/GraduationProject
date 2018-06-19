$(function () {
    $("#userList").datagrid({
        url: '/SystemUser/GetUserList',
        methord: 'post',
        width: SetGridWidthSub(30),
        height: SetGridHeightSub(60),
        fitColumns: true,
        sortName: 'id',
        sortOrder: 'desc',
        idField: 'id',
        pageSize: 10,
        pageList: [5, 10, 15, 20],
        pagination: true,
        rownumbers: true,
        striped: true,
        singleSelect: true,
        columns: [[
                    { field: 'id', title: '账号', width: 30 },
                    { field: 'name', title: '用户名', width: 30 },
                    {
                        field: 'active', title: '账号状态', width: 20, align: 'center', formatter: function (value) {
                            return value == "启用" ? '<span class="label label-success">启用</span>' : '<span class="label label-error">禁用</span>';
                        },
                    },
                    { field: 'company', title: '所属公司', width: 80 },
                    { field: 'tel', title: '手机号码', width: 40 },
                    { field: 'level', title: '角色', width: 40 }
        ]]
    });
    datagridResize('#userList', 30, 60);

    $("#Create").click(function () {
        openWindow("#modalwindow", `/SystemUser/ShowUserInfoEdit?create=true`, 600, 650, 'fa fa-list', `创建用户`, true, "/SystemUser/DeleteUser")
    })

    $("#Edit").click(function () {
        var row = getSelectRow("#userList");
        if (row == null)
            return
        openWindow("#modalwindow", `/SystemUser/ShowUserInfoEdit?id=${row.id}`, 600, 650, 'fa fa-pencil', `编辑${row.name}的个人资料`)
    })

    $("#Detail").click(function () {
        var row = getSelectRow("#userList");
        if (row == null)
            return
        openWindow("#modalwindow", `/SystemUser/ShowUserInfo?id=${row.id}`, 500, 650, 'fa fa-list', `${row.name}的个人资料`)
    })
    $("#Delete").click(function () {
        var row = getSelectRow("#userList");
        if (row == null)
            return
        $.post("/SystemUser/DeleteUser", { id: row.id }, function (data) {
            if (data.result) {
                showTip("删除成功!", 3000);
                datagridReload("#userList");
                $("#userList").datagrid('unselectAll');
            }
            else
                showTip("删除失败!", 3000);
        })
    })
})

//查询条件
function searchInfo(query, condition) {
    $("#userList").datagrid('load', { condition: condition, query: query });
}

function refreshPage() {
    $("#userList").datagrid("reload");
}