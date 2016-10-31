var postData = { 'dirName': $('#createDirMask input') }
$.ajax({
    url: "",
    type: "POST",
    contentType: 'application/json; charset=UTF-8',
    dataType: "json",
    data: JSON.stringify(postData),
    beforeSend: function () {
        layer.load(1, {
            shade: [0.1, '#fff'] //0.1透明度的白色背景
        });
    },
    complete: function () {
        layer.closeAll('loading');
    },
    success: function (result) {
        alert('ok');
    },
    error: function (e) {
        console.log(e);
    }
});