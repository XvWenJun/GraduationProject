$(function () {
    $("#orderList").datagrid({
        url: '/Quotation/GetOrderList',
        method: 'post',
        width: SetGridWidthSub(30),
        height: SetGridHeightSub(60),
        fitColumns: true,
        sortName: 'date',
        sortOrder: 'asc',
        idField: 'id',
        pagination: true,
        rownumbers: false,
        striped: true,
        autoRowHeight: true,
        singleSelect: true,
        columns: [[
                    { field: 'id', title: '订单号', width: 10 },
                    { field: 'agentId', title: '提交代理商编号', width: 20 },
                    { field: 'agentName', title: '提交代理商名字', width: 20, },
                    {
                        field: 'state', title: '订单状态', width: 20, align: 'center', formatter: function (value) {
                            return value == "待审核" ? '<span class="label label-success">待审核</span>' : (value == "审核通过" ? '<span class="label label-info">审核通过</span>' : '<span class="label label-error">审核失败</span>');
                        }
                    },
                    {
                        field: 'date', title: '提交时间', width: 20, formatter: function (value) {
                            return new Date(Number.parseInt(value.match(/\d+/g)[0])).Format("yyyy年MM月dd日")
                        }
                    },
                    {
                        field: 'staffId', title: '订单审核人编号', width: 20, formatter: function (value) {
                            return value == 0 ? "" : value;
                        }
                    },
                    { field: 'staffName', title: '订单审核人名字', width: 20 }
        ]],
    });

    datagridResize("#orderList", 30, 60);

    $("#date").datebox({
        closeText: "关闭",
        okText: "确认",
        onSelect: function (date) {
            doSearch(null, null);
        }
    });

    $("#Create").click(function () {
        openWindow("#modalwindow", `/Quotation/Index`, 650, 1100, 'fa fa-server', `添加订单`);
    });

    $("#Detail").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        openWindow("#modalwindow", `/Quotation/Index?id=${row.id}`, 600, 900, 'fa fa-server', `订单${row.id}的详情`)
    });

    $("#Edit").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        else if (row.state == "审核通过") {
            showTip("已经通过审核，无法修改");
            return
        }
        openWindow("#modalwindow", `/Quotation/Index?id=${row.id}&edit=true`, 650, 1100, 'fa fa-server', `订单${row.id}的详情`)
    });

    $("#Delete").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        else if (row.state == "审核通过") {
            showTip("已经通过审核，无法删除");
            return
        }
        $.post("/Quotation/DeleteQuotationByOrderId", { orderId: row.id }, function (data) {
            if (data.result) {
                showTip("删除成功!", 3000);
                datagridReload("#orderList");
                $("#orderList").datagrid('unselectAll');
            }
            else
                showTip("删除失败!", 3000);
        })
    });

    $("#CheckPass").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        else if (row.state != '待审核') {
            showTip("该订单已经审核过了，无法修改！", 3000);
            return
        }
        editOrderState(row.id, row.agentId, "审核通过")
    });

    $("#CheckFail").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        else if (row.state != '待审核') {
            showTip("该订单已经审核过了，无法修改！", 3000);
            return
        }
        editOrderState(row.id, row.agentId, "审核失败")
    });

    $("#Download").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null) {
            $("#Download").attr('href', `#`);
            return
        }
           
        else if (row.state != "审核通过") {
            showTip("审核未通过，无法下载！", 3000);
            $("#Download").attr('href', `#`);
            return
        }

        $("#Download").attr('href', `/Quotation/ExportWord?orderId=${row.id}`);
    });
})

function searchInfo(query, condition) {
    doSearch(condition, query);
}

function editOrderState(id, agentId, state) {
    $.post("/Quotation/EditOrderState", { id, state}, function (data) {
        if (data.result) {
            showTip("修改成功", 3000);
            $("#orderList").datagrid('reload');
            window.parent.addNotice(agentId);
    }
    else {
            showTip("修改失败", 3000);
    }
    })
    }

function doSearch(condition, query) {
    if (condition == null) {
        query = $("#search").searchbox('getValue');
        condition = $("#search").searchbox('getName');
    }
    var date = $('#date').datebox('getValue');

    $("#orderList").datagrid('load', { condition, query, date});
    }

function refreshPage() {
    $("#orderList").datagrid("reload");
    }