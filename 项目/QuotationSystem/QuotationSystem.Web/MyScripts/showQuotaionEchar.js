$(function () {
    $("#date").combobox({
        valueField: 'id',
        textField: 'text',
        url: "/Quotation/GetOrderDateList",
        onLoadSuccess: function () {
            var arr = $(this).combobox('getData');
            if (arr.length > 0) {
                $(this).combobox('select', arr[0].id)
                $("#orderList").datagrid({
                    url: '/Quotation/GetOrderPassList',
                    queryParams: {
                        date: arr[0].id
                    }
                })
            }
            else {
                $(this).combobox('setText', "没有数据");
            }
        },
        onChange: function (newValue, oldValue) {
            $("#orderList").datagrid('reload', { date: newValue })
        }
    });

    $("#orderList").datagrid({
        methord: 'post',
        width: SetGridWidthSub(20),
        height: SetGridHeightSub(380),
        fitColumns: true,
        sortName: 'date',
        idField: 'id',
        rownumbers: false,
        striped: true,
        autoRowHeight: true,
        singleSelect: true,
        columns: [[
                    { field: 'id', title: '订单号', width: 10 },
                    { field: 'agentId', title: '提交代理商编号', width: 20 },
                    { field: 'agentName', title: '提交代理商名字', width: 20, },
                    { field: 'state', title: '订单状态', width: 20, hidden: true },
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
        onLoadSuccess: function (data) {
            loadEchartData(data.rows.map(value=>value.id))
        }
    });

    datagridResize("#orderList", 20, 380);

    $("#btnDetails").click(function () {
        var row = getSelectRow("#orderList");
        if (row == null)
            return
        openWindow("#modalwindow", `/Quotation/Index?id=${row.id}`, 600, 900, 'fa fa-server', `订单${row.id}的详情`)
    });

    var myChart = echarts.init(document.getElementById('echart'));
    function loadEchartData(data) {
        $.post("/Quotation/GetOrderDetailTotalDataList", { orderIdList: data }, function (result) {
            // 指定图表的配置项和数据
            var option = {
                title: {
                    text: `${$("#date").combobox('getText')}--订单数据汇总`,
                    subtext: `总售价：${result.reduce((sum, value) =>sum + value.totalPrice, 0).toFixed(2)}`
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'shadow'
                    }
                },
                legend: {
                    data: ['售价']
                },
                xAxis: {
                    data: result.map(value=>value.categoryText)
                },
                yAxis: {
                },
                dataZoom: [
                    {
                        type: 'inside'
                    }
                ],
                series: [{
                    name: '售价',
                    type: 'bar',
                    data: result.map(value=>value.totalPrice.toFixed(2))
                }]
            };

            if ($("#agentId").val() == 0) {
                option.title.subtext += `      总成本：${result.reduce((sum, value) =>sum + value.totalCost, 0).toFixed(2)}`
                option.legend.data.push('成本');
                option.series.push({
                    name: '成本',
                    type: 'bar',
                    data: result.map(value=>value.totalCost.toFixed(2))
                })
            }
            myChart.setOption(option)
        })
    }
})

function refreshPage() {
    $("#date").combobox('reload');
}