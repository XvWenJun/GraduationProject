var editIndex = undefined;
$(function () {
    $(document).keydown(function (e) {
        if (e.which == 13) {
            var selected = $("#ruleList").datagrid('getSelected');
            if (selected != null) {
                $("#ruleList").datagrid('endEdit', $("#ruleList").datagrid('getRowIndex', selected))
            }
        }
    });

    $("#ruleList").datagrid({
        url: '/Quotation/GetLevelChangeRules',
        methord: 'post',
        width: SetGridWidthSub(30),
        height: SetGridHeightSub(60),
        sortName: 'id',
        sortOrder: 'desc',
        idField: 'id',
        rownumbers: false,
        striped: true,
        singleSelect: true,
        fitColumns: true,
        columns: [[
                    { field: 'id', title: '编号', width: 5, hidden: true },
                    {
                        field: 'priceLevel', title: '售价等级', width: 20, editor: {
                            type: 'combobox',
                            options: {
                                valueField: 'value',
                                textField: 'text',
                                url: "/Quotation/GetPriceList",
                                method: "post",
                                required: true
                            }
                        }
                    },
                    {
                        field: 'levelChangeCondition', title: '升降级条件', width: 20, formatter: function (value) {
                            return `以 <strong>${value}</strong> 为条件`;
                        }, editor: {
                            type: 'combobox',
                            options: {
                                valueField: 'label',
                                textField: 'value',
                                data: [{
                                    label: '销售额',
                                    value: '销售额'
                                }, {
                                    label: '利润',
                                    value: '利润'
                                }],
                                required: true
                            }
                        }
                    },
                    {
                        field: 'upgradeQuantity', title: '升级数额', width: 20, formatter: function (value) {
                            return `${value} ￥`
                        }, editor: {
                            type: "numberbox",
                            options: {
                                min: 0,
                                precision: 2,
                                suffix: ' ￥',
                                required: true
                            }
                        }
                    },
                    {
                        field: 'degradeQuantity', title: '降级数额', width: 20, formatter: function (value) {
                            return `${value} ￥`
                        }, editor: {
                            type: "numberbox",
                            options: {
                                min: 0,
                                precision: 2,
                                suffix: ' ￥',
                                required: true
                            }
                        }
                    },
                    {
                        field: 'count', title: '生效次数', width: 10, editor: {
                            type: "numberbox",
                            options: {
                                min: 1,
                                required: true
                            }
                        }
                    }

        ]],
        onClickRow: function (index) {
            if (editIndex != index) {
                if (endEditing()) {
                    $("#ruleList").datagrid('selectRow', index).datagrid('beginEdit', index);
                    editIndex = index
                }
                else {
                    $('#ruleList').datagrid('selectRow', editIndex);
                }
            }
        }
    });
    datagridResize('#ruleList', 30, 60);

    $("#Edit").click(function () {
        var rows = $("#ruleList").datagrid('getData');
        if (!validateDatagrid(rows.total)) {
            showTip("您的修改存在错误，请确认！", 2000);
            return;
        }

        $.post("/Quotation/EditPriceLevelChangeRules", { rules: rows.rows }, function (data) {
            if (data.result) {
                showTip("修改成功！", 1000);
                $("#ruleList").datagrid('reload');
            }
            else {
                showTip("修改失败，请重试！", 1000);
                $("#ruleList").datagrid('rejectChanges');
            }
        })
    })

    $("#Reset").click(function () {
        $("#ruleList").datagrid('rejectChanges')
    })

    $("#Create").click(function () {
        if (endEditing()) {
            $("#ruleList").datagrid('appendRow', {})
            editIndex = $("#ruleList").datagrid("getRows").length - 1;
            $("#ruleList").datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);
        }
    })

    $("#Delete").click(function () {
        if (editIndex == undefined) {
            showTip("请选择要删除的项！", 1000);
            return
        }
        $('#ruleList').datagrid('cancelEdit', editIndex)
					.datagrid('deleteRow', editIndex);
        editIndex = undefined;
    })

    function endEditing() {
        console.log(editIndex)
        if (editIndex == undefined)
            return true
        if ($("#ruleList").datagrid("validateRow", editIndex)) {
            $('#ruleList').datagrid('endEdit', editIndex);
            editIndex = undefined
            return true
        }
        else {
            showTip("您的修改存在错误，请确认！", 1000);
            return false
        }
    }
    function validateDatagrid(rowCount) {
        for (var i = 0; i < rowCount; i++)
            if (!$("#ruleList").datagrid('validateRow', i))
                return false;
        return true;
    }
})

function refreshPage() {
    $("#ruleList").datagrid("reload");
}