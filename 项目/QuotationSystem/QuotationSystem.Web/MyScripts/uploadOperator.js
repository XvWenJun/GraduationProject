function initImageUploader(picker, url, id, img, reload) {
    var uploader = WebUploader.create({
        auto: true,
        swf: "/Scripts/webuploader/Uploader.swf",
        server: url,
        pick: picker,
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,bmp,png',
            mimeTypes: 'image/*'
        },
        fileNumLimit: 0,
        fileSingleSizeLimit: 2048000,
        resize: false,
        compress: false,
        formData: {
            imgId: id
        }
    })

    uploader.on('fileQueued', function (file) {
        uploader.makeThumb(file, function (error, src) {
            if (error) {
                return;
            }
            $(img).attr('src', src);
        }, 1, 1);
    });

    //uploader.on('uploadProgress', function (file, percentage) {
    //});

    // 文件上传成功
    uploader.on('uploadSuccess', function (file) {
        window.parent.showTip("上传成功！", 300)
        if (reload != null) {
            reload();
        }
    });

    // 文件上传失败
    uploader.on('uploadError', function (file) {
        window.parent.showTip("上传失败！", 300)
    });

    //// 完成上传完了，成功或者失败
    //uploader.on('uploadComplete', function (file) {
    //})
}

function initFileUploader(picker, url, id, msg) {
    var uploader = WebUploader.create({
        auto: false,
        swf: "/Scripts/webuploader/Uploader.swf",
        server: url,
        pick: {
            id: picker,
            multiple: false,
            title: '点击上传excel文件'
        },
        accept: {
            title: 'Files',
            extensions: 'xlsx,xls'
        },

        fileSingleSizeLimit: 4096000,
        resize: false,
        compress: false,
        formData: {
            fileId: id
        }
    })

    uploader.on('fileQueued', function (file) {
        $(msg).text(file.name)
    });

    // 文件上传成功
    uploader.on('uploadSuccess', function (file, response) {
        window.parent.showTip(response > 0 ? `上传成功，已经写入${response}条数据` : `未能成功写入数据,可能原因是文件格式错误，或者已存在商品`, 2000);
        if (response > 0)
        {
            window.parent.refreshData();
        }
    });

    // 文件上传失败
    uploader.on('uploadError', function (file) {
        window.parent.showTip("上传失败！", 2000)
    });

    return uploader;
}