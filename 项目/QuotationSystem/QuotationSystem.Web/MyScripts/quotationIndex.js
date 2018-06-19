var level = $("#level").val();
var preIndex = undefined;
$(function () {
    $(document).keydown(function (e) {
        if (e.which == 13) {
            var selected = $("#orderDetailList").datagrid('getSelected');
            if (selected != null) {
                $("#orderDetailList").datagrid('endEdit', $("#orderDetailList").datagrid('getRowIndex', selected))
            }
        }
    });

    $("#orderDetailList").datagrid({
        width: SetGridWidthSub(30),
        height: SetGridHeightSub(60),
        fitColumns: true,
        sortName: 'productId',
        sortOrder: 'desc',
        idField: 'productId',
        pagination: false,
        rownumbers: false,
        striped: true,
        showFooter: true,
        autoRowHeight: true,
        singleSelect: true,
        columns: [[
                    { field: 'operation', title: '操作', width: 10, hidden: !!$("#readOnly").val() },
                    { field: 'productId', title: '产品编号', width: 20 },
                    { field: 'productName', title: '产品名字', width: 20, },
                    { field: 'productUnit', title: '产品单位', width: 20 },
                    { field: 'productCost', title: '产品成本', width: 20, hidden: level > 0 },
                    { field: 'productPrice', title: '产品售价', width: 20 },
                    {
                        field: 'productCount', title: '数量', width: 20, editor: {
                            type: 'numberbox',
                            options: {
                                min: 1,
                                required: true
                            }
                        }
                    }
        ]],
        onClickRow: function (index) {
            if (!!!$("#readOnly").val()) {
                if (preIndex != index) {
                    $("#orderDetailList").datagrid("endEdit", preIndex);
                    preIndex = index;
                }

                $("#orderDetailList").datagrid("selectRow", index).datagrid("beginEdit", index);
            }
        },
        onAfterEdit: function () {
            updateFooter();
        }
    });

    datagridResize("#orderDetailList", 30, 60);

    $("#btnAppend").click(function () {
        showWindow();
    });

    $("#btnRemove").click(function () {
        remove();
    });

    $("#btnSave").click(function () {
        var rows = $("#orderDetailList").datagrid('getData').total;
        if (rows == 0) {
            window.parent.showTip("没有选择产品！", 3000)
            return
        }
        if (!validateDatagrid(rows)) {
            window.parent.showTip("数量栏不能为空！", 3000)
            return
        }

        $("#orderDetailList").datagrid('acceptChanges');
        updateFooter();

        var id = $("#id").val();
        $.post("/Quotation/UpdateQuotationData", { orderId: id == "" ? 0 : id, orderDetails: $("#orderDetailList").datagrid('getData').rows }, function (data) {
            if (data.result) {
                window.parent.showTip("操作成功！", 3000)
                clearDatagrid();
                window.parent.datagridReload("#orderList")
                window.top.addNotice(0);
            }
            else
                window.parent.showTip("操作失败！", 3000)
        });
    });

    if ($("#id").val() != "") {
        initDatagrid();
    }
    else {
        initFooter();
    }
});

function initDatagrid() {
    $.post("/Quotation/GetOrderDetails", { id: $("#id").val() }, function (data) {
        for (var i = 0; i < data.rows.length; i++) {
            data.rows[i].operation = "<a href='javascript:showWindow()' style='color:green;'><span class='fa fa-plus'></span><a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href='javascript:remove()' style='color:red'><span class='fa fa-minus'></span><a>"
        }
        updateFooter(data);
    })
}

function append(value) {
    console.log(value)
    var index = $('#orderDetailList').datagrid('getRowIndex', $('#orderDetailList').datagrid('getSelected'));
    $('#orderDetailList').datagrid('insertRow', {
        index: index + 1,
        row: {
            operation: "<a href='javascript:showWindow()' style='color:green;'><span class='fa fa-plus'></span><a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href='javascript:remove()' style='color:red'><span class='fa fa-minus'></span><a>",
            productId: value.id,
            productName: value.name,
            productCategory: value.category,
            productUnit: value.unit,
            productCost: value.cost,
            productPrice: value.price,
            productCount: 1
        }
    });

    $('#orderDetailList').datagrid('selectRow', index + 1)
            .datagrid('beginEdit', index + 1);

    updateFooter();
}

function remove() {
    var index = $('#orderDetailList').datagrid('getRowIndex', $('#orderDetailList').datagrid('getSelected'));
    $("#orderDetailList").datagrid('cancelEdit', index).datagrid(
        'deleteRow', index);
    preIndex = undefined;
    updateFooter();
}

function initFooter() {
    var data = $("#orderDetailList").datagrid('getData');
    data.footer = [{ productId: '总计', productCost: 0, productPrice: 0, productCount: 0 }];
    $("#orderDetailList").datagrid('loadData', data);
}

function updateFooter(data) {
    if (data == null)
        data = $("#orderDetailList").datagrid('getData');

    var costs = 0, prices = 0, count = 0;
    data.rows.forEach(value=> {
        costs += value.productCost * value.productCount;
        prices += value.productPrice * value.productCount;
        count += Number.parseInt(value.productCount);
    })

    data.footer = [{ productId: '总计', productCost: costs.toFixed(2), productPrice: prices.toFixed(2), productCount: count }];
    $("#orderDetailList").datagrid('loadData', data);
}

function validateDatagrid(rows) {
    for (var i = 0; i < rows; i++)
        if (!$("#orderDetailList").datagrid('validateRow', i))
            return false;
    return true;
}

function clearDatagrid() {
    var data = $("#orderDetailList").datagrid('getData');
    data.total = 0;
    data.rows = [];
    data.footer = [{ productId: '总计', productCost: 0, productPrice: 0, productCount: 0 }];
    $("#orderDetailList").datagrid('loadData', data);
}

//获得选择的产品
function setCheckedList(list) {
    list.forEach(value=> {
        if (!isAlreadyHave(value.id))
            append(value)
    })
}

//判断是否已经添加
function isAlreadyHave(id) {
    var data = $("#orderDetailList").datagrid('getData');
    for (var i = 0; i < data.rows.length; i++) {
        if (data.rows[i].productId == id)
            return true;
    }

    return false;
}

function showWindow() {
    var height = 600, width = 900;

    openWindow("#modalwindow", '/SystemProduct/Index?readOnly=true', height, width, 'fa fa-pencil', '添加产品');
}