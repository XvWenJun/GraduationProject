$(function () {
    var uploader = initFileUploader("#fileUpload", "/SystemProduct/UploadExcelFile", $("#id").val(), "#msg")
    $("#btnOk").click(function () {
        uploader.upload();
    })

    $("#btnCancel").click(function () {
        window.parent.windowClose("#modalwindow");
    })
})