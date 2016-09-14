if (!RentApp.upload) {
    RentApp.upload = { services: {} };
}
RentApp.upload.services.upload = function (data, onSuccess, onError) {

    var url = "/api/upload/"

    var settings = {
        data: data,
        contentType: false,
        type: "POST",
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false,
        processData: false,
    };

    $.ajax(url, settings)
}

RentApp.upload.services.uploadAvatar = function (data, onSuccess, onError) {

    var url = "/api/upload/avatar"

    var settings = {
        data: data,
        contentType: false,
        type: "POST",
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false,
        processData: false,
    };

    $.ajax(url, settings)
}
