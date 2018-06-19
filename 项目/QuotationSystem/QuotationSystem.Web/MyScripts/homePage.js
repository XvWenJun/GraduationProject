$(function () {
    loadOrderData(true);
});

function loadOrderData(showanimation) {
    var myChart1 = echarts.init(document.getElementById('echart1'));
    var myChart2 = echarts.init(document.getElementById('echart2'));
    $.post("/Home/GetPageData", function (result) {
        if ($("#agentId").val() != 0) {
            $("#orderTotal").html(result.totalCount);
        }

        $("#orderPassTotalCount").html(result.passCount);

        $("#priceTotal").html(result.curdata.reduce((sum, value) =>sum + value.totalPrice, 0).toFixed(2));
        if ($("#agentId").val() == 0) {
            $("#profitTotal").html((result.curdata.reduce((sum, value) =>sum + value.totalPrice, 0) - result.curdata.reduce((sum, value) =>sum + value.totalCost, 0)).toFixed(2));
        }

        var option1 = {
            title: {
                text: '本月销售情况汇总',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br />{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: result.curdata.map(value=>value.categoryText)
            },
            series: [
                {
                    name: '销售汇总',
                    type: 'pie',
                    animation: showanimation,
                    radius: '70%',
                    center: ['50%', '60%'],
                    data: result.curdata.map(val=> { return { value: val.totalPrice.toFixed(2), name: val.categoryText } }),
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };

        xdata = result.curdata.map(value=> { return { id: value.categoryId, text: value.categoryText } });
        result.lastdata.forEach(value=> {
            var entry = xdata.find(val=>val.id == value.categoryId);
            if (entry == null) {
                xdata.push({ id: value.categoryId, text: value.categoryText })
            }
        })

        var curdata = [], lastdata = [];

        xdata.forEach(value => {
            var curentry = result.curdata.find(val=>val.categoryId == value.id);
            if (curentry != null)
                curdata.push(curentry.totalPrice.toFixed(2))
            else
                curdata.push("0.00")

            var lastentry = result.lastdata.find(val=>val.categoryId == value.id);
            if (lastentry != null)
                lastdata.push(lastentry.totalPrice.toFixed(2))
            else
                lastdata.push("0.00")
        })

        option2 = {
            title: {
                text: '本月与上月销售情况比较',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['本月', '上月'],
                left: 'left'
            },
            xAxis: {
                type: 'category',
                boundaryGap: true,
                data: xdata.map(value=>value.text)
            },
            yAxis: {
                type: 'value',
                axisLabel: {
                    formatter: '{value} ￥'
                }
            },
            series: [
                {
                    name: '本月',
                    type: 'line',
                    animation: showanimation,
                    data: curdata,
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' }
                        ]
                    }
                },
                {
                    name: '上月',
                    type: 'line',
                    animation: showanimation,
                    data: lastdata,
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' },

                        ]
                    }
                }
            ]
        };

        myChart1.setOption(option1)
        myChart2.setOption(option2)
    })
}
function refreshPage() {
    loadOrderData(false)
}