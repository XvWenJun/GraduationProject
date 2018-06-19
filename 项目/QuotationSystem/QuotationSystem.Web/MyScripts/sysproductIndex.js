var preNodes = [];
var oldParent;
$(function () {
    var readOnly = !!$("#readOnly").val();
    var level = $("#level").val();
    var canEditCategoies = !!$("#editCategories").val();
    $("#tree").tree({
        url: '/SystemProduct/GetCategories',
        method: 'post',
        animate: true,
        dnd: !readOnly && canEditCategoies,
        checkbox: true,
        onLoadSuccess: function (node, data) {
            // collapseAll();
        },

        onContextMenu: function (e, node) {
            e.preventDefault();
            $(this).tree('select', node.target);
            $("#context").menu('show', {
                left: e.pageX,
                top: e.pageY
            })
        },

        onDblClick: function (node) {
            if (!readOnly && canEditCategoies)
                $(this).tree("beginEdit", node.target)
        },

        onBeforeEdit: function (node) {
            preNodes.push({ id: node.id, text: node.text });
        },

        onAfterEdit: function (node) {
            var preNode = findAndDeleteNode(node.id)
            if (node.text == "") {
                $("#tree").tree('update', {
                    target: node.target,
                    text: preNode.text
                });
            }
            else if (preNode.text != node.text) {
                $.post("/SystemProduct/EditCategoryText", { id: node.id, text: node.text }, function (data) {
                    if (data.result)
                        showTip("修改成功", 500);
                    else {
                        $("#tree").tree('update', {
                            target: node.target,
                            text: preNode.text
                        });
                        showTip("修改失败", 500);
                    }
                });
            }
        },

        onCancelEdit: function (node) {
            findAndDeleteNode(node.id)
        },

        onStartDrag: function (node) {
            var parent = $("#tree").tree('getParent', node.target);
            oldParent = parent ? parent.id : 0;
        },

        onDrop: function (target, source, point) {
            var parent = point == "append" ? $("#tree").tree('getNode', target) : $("#tree").tree('getParent', target);
            var newParent = parent ? parent.id : 0;
            if (oldParent != newParent)
                $.post("/SystemProduct/MoveCategory", { id: source.id, newParent: newParent }, function (data) {
                    if (data.result)
                        showTip("移动成功", 500);
                    else
                        showTip("移动失败，请刷新重试", 500);
                });
        },

        onCheck: function (node, checked) {
            doSearch(null, null);
        }
    });

    /*-------------------------------------------------------------------------------------------*/

    $("#productList").datagrid({
        url: '/SystemProduct/GetProcutList',
        methord: 'post',
        width: SetGridWidthSub(230),
        height: SetGridHeightSub(110),
        fitColumns: true,
        sortName: 'id',
        sortOrder: 'desc',
        idField: 'id',
        pageSize: 10,
        pageList: [5, 10, 15, 20],
        pagination: true,
        rownumbers: false,
        striped: true,
        singleSelect: level > 0 && !readOnly,
        autoRowHeight: true,
        checkOnSelect: true,
        selectOnCheck: true,
        columns: [[
                    { field: 'checkbox', width: 10, checkbox: level <= 0 || readOnly, hidden: level > 0 && !readOnly },
                    { field: 'id', title: '产品编号', width: 10 },
                    { field: 'name', title: '产品名字', width: 10 },
                    {
                        field: 'img', title: '产品图片', width: 6, align: 'center', hidden: readOnly, formatter: function (value, row) {
                            return `<a class="fancybox" title="${row.name}" href="${value ? value : "/Content/images/nopic.gif"}"><img width="50px" height="50px" src="${value ? value : "/Content/images/nopic.gif"}" /></a>`;
                        },
                    },
                    { field: 'unit', title: '产品单位', width: 10 },
                    { field: 'describe', title: '产品描述', width: 30 },
                    { field: 'cost', title: '成本', hidden: level > 0, width: 10 },
                    { field: 'price', title: '售价', width: 10 },
        ]],
        onLoadSuccess: function (data) {
            $("a.fancybox").fancybox({
                'titleShow': false
            });
        }
    });

    datagridResize('#productList', 230, 110);

    $("#Create").click(function () {
        openWindow("#modalwindow", `/SystemProduct/ShowProductInfoEditView`, 550, 720, 'fa fa-list', `创建产品`, true, '/SystemProduct/DeleteProduct')
    })

    $("#Edit").click(function () {
        var row = getSelectRow("#productList");
        if (row == null)
            return
        openWindow("#modalwindow", `/SystemProduct/ShowProductInfoEditView?id=${row.id}`, 550, 720, 'fa fa-pencil', `编辑产品：${row.name}`)
    })

    $("#Detail").click(function () {
        var row = getSelectRow("#productList");
        if (row == null)
            return
        openWindow("#modalwindow", `/SystemProduct/ShowProductInfo?id=${row.id}`, 550, 720, 'fa fa-list', `${row.name}的资料`)
    })
    $("#Delete").click(function () {
        var rows = $("#productList").datagrid("getSelections");
        if (rows.length == 0) {
            showTip("请选择要操作的对象", 3000)
            return
        }
        $.post("/SystemProduct/DeleteProductList", { idList: rows.map(val=>val.id) }, function (data) {
            if (data.result) {
                showTip("删除成功!", 3000);
                datagridReload("#productList");
                $("#productList").datagrid('unselectAll');
            }
            else
                showTip("删除失败!", 3000);
        })
    })
    $("#btnOk").click(function () {
        var checkedList = $("#productList").datagrid("getChecked");
        if (checkedList.length == 0) {
            window.parent.showTip("请选择要生成报价单的产品", 3000);
            return
        }
        window.parent.setCheckedList(checkedList);
    })

    $("#EditProductCategories").combotree({
        url: '/SystemProduct/GetCategories',
        onChange: function (newValue, oldValue) {
            if (newValue > 0) {
                var rows = $("#productList").datagrid("getSelections");
                if (rows.length == 0) {
                    showTip("请选择要操作的对象", 3000)
                    return
                }

                $.post("/SystemProduct/EditProductListCategory", { idList: rows.map(val=>val.id), category: newValue }, function (data) {
                    if (data.result) {
                        showTip("修改分类成功!", 3000);
                        datagridReload("#productList");
                        $("#productList").datagrid('unselectAll');
                    }
                    else
                        showTip("修改分类失败!", 3000);
                })
            }
        }
    })

    $("#Import").click(function () {
        openWindow("#modalwindow", `/SystemProduct/ShowFileUpload`, 335, 620, 'fa fa-file-excel-o', `上传产品资料`)
    });
});

function searchInfo(query, condition) {
    doSearch(condition, query);
}

function doSearch(condition, query) {
    if (condition == null) {
        query = $("#search").searchbox('getValue');
        condition = $("#search").searchbox('getName');
    }
    var list = $("#tree").tree('getChecked', ['checked', 'indeterminate']).map(value=>value.id);
    $("#productList").datagrid('load', { condition, query, list });
    }

/*--------------------------------------------------------------------------------------------------*/
function findAndDeleteNode(id) {
    var preNode = preNodes.find(value=>value.id == id);
    var preNodeIndex = preNodes.find(value=>value.id == id);
    preNodes.splice(preNodeIndex, 1);
    return preNode;
}

function append(root) {
    var node = $("#tree").tree('getSelected');
    $.post("/SystemProduct/AddCategory", { parentId: root ? 0 : node.id, text: '新类别' }, function (data) {
        if (data != -1) {
            $("#tree").tree('append', {
                parent: root ? null : node.target,
                data: [{ text: '新类别', id: data }]
            });

            if (!root)
                $('#tree').tree('expand', node.target);

            var add = $('#tree').tree('find', data);
            $('#tree').tree('select', add.target);

            showTip("添加成功", 500);
        }

        else
            showTip("添加失败", 500);
    });
}

function remove() {
    $.messager.confirm("警告", "您确定要删除分类吗？", function (result) {
        if (result) {
            var node = $('#tree').tree('getSelected');
            $.post("/SystemProduct/DeleteCategories", { id: node.id }, function (data) {
                if (data.result) {
                    $('#tree').tree('remove', node.target);
                    showTip('删除成功', 500);
                }
                else
                    showTip(data.msg, 500);
            });
        }
    })
}

function expand() {
    var node = $('#tree').tree('getSelected');
    $('#tree').tree('expand', node.target);
}

function expandAll() {
    var roots = $("#tree").tree('getRoots');
    roots.forEach(root=> $('#tree').tree('expandAll', root.target))
}

function collapse() {
    var node = $('#tree').tree('getSelected');
    $('#tree').tree('collapse', node.target);
}

function collapseAll() {
    var roots = $("#tree").tree('getRoots');
    roots.forEach(root=> $('#tree').tree('collapseAll', root.target))
}

function refreshData() {
    $("#tree").tree('reload');
    $("#productList").datagrid('reload');
}

function refreshPage() {
    refreshData()
}