$(function () {
    var level = '';
    var levelName = '';
    var rightId = '';
    var rightText = '';
    $("#levelList").datagrid({
        title: '用户模块',
        url: '/SystemRight/GetLevelList',
        method: 'post',
        width: 300,
        height: SetGridHeightSub(60),
        idField: 'level',
        pagination: false,
        rownumbers: false,
        striped: true,
        singleSelect: true,
        fitColumns: true,
        scrollbarSize: 0,
        columns: [[
                    { field: 'id', width: 10, title: '<strong>用户编号</strong>', align: 'center' },
                     { field: 'name', width: 30, title: '<strong>用户等级</strong>', align: 'center' },
        ]],
        onClickRow: function (index, data) {
            level = data.id;
            levelName = data.name;
            if (checkSelect())
                updateDutyListTile();
        }
    });

    datagridResizeFixWidth('#levelList', 300, 60);

    $("#rightList").datagrid({
        title: '权限分类模块',
        url: '/SystemRight/GetRightList',
        method: 'post',
        width: 300,
        height: SetGridHeightSub(60),
        idField: 'id',
        pagination: false,
        rownumbers: false,
        striped: true,
        singleSelect: true,
        fitColumns: true,
        scrollbarSize: 0,
        columns: [[
                    { field: 'id', title: '权限Id', align: 'left', hidden: true },
                    { field: 'text', width: 300, title: '<strong>权限分类</strong>', align: 'center' },
        ]],
        onClickRow: function (index, data) {
            rightId = data.id;
            rightText = data.text;
            if (checkSelect())
                updateDutyListTile();
        }
    });

    datagridResizeFixWidth('#rightList', 300, 60);

    $("#dutyList").datagrid({
        title: '授权操作',
        method: 'post',
        width: 600,
        height: SetGridHeightSub(60),
        idField: 'id',
        pagination: false,
        rownumbers: false,
        striped: true,
        singleSelect: true,
        fitColumns: true,
        scrollbarSize: 0,
        columns: [[
                    { field: 'id', width: 100, title: '权限编号', align: 'center' },
                    { field: 'name', width: 150, title: '权限管理', align: 'left' },
                    { field: 'keyCode', width: 150, title: '权限操作码', align: 'left' },
                    {
                        field: 'havePermission', width: 200, title: `<a href='#' class="fa  fa-check-square-o color-green"  onclick="selectAll()">全选择</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='#'  class="fa  fa-square-o color-gray" onclick="selectNone()" >全取消</a>`, align: 'center',
                        formatter: function (value) {
                            return `<input type="checkbox" value="${value}" ${value ? "checked='checked'" : ""}>`
                        }
                    },

        ]],
        onClickRow: function (index, data) {
        }
    });

    datagridResizeFixWidth('#dutyList', 600, 60);

    $("#Edit").click(function () {
        if (!checkSelect())
            return
        var updateRows = 0;
        var rows = $("#dutyList").datagrid("getRows");

        rows.forEach((value, index) => {
            var checkState = $("td[field='havePermission']").find("input[type='checkbox']").eq(index).prop('checked');
            if (checkState != value.havePermission) {
                value.havePermission = checkState;
                updateRows++
            }
        })
        if (updateRows == 0) {
            showTip("没有修改可以提交！", 1500);
        }
        else {
            $.post("/SystemRight/EditUserDuty", { level: level, dutyList: rows }, function (data) {
                if (data.result)
                    showTip("修改成功", 1500)

                else
                    showTip("修改失败", 1500)

                $("#dutyList").datagrid('reload');
            })
        }
    })

    loadLevel();

    /**************function**************/

    function updateDutyListTile() {
        $("#dutyList").datagrid({
            title: `角色组：${levelName || '??'} >> 权限组：${rightText || '??'}`,
            url: `/SystemRight/GetDutyList?level=${level}&id=${rightId}`,
        })
    }

    function checkSelect() {
        if (level == '') {
            showTip("请选择用户模块", 1500);
            return false;
        }

        else if (rightId == '') {
            showTip("请选择权限分类模块", 1500);
            return false;
        }
        return true;
    }
})

function selectAll() {
    $("td[field='havePermission']").find("input[type='checkbox']").prop('checked', true);
}

function selectNone() {
    $("td[field='havePermission']").find("input[type='checkbox']").prop('checked', false);
}

function refreshPage() {
    $("#levelList").datagrid("reload");
    $("#rightList").datagrid("reload");
    $("#dutyList").datagrid("reload");
}