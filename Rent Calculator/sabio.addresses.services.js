if (!sabio.addresses) {
    sabio.addresses = { services: {} };
}

sabio.addresses.services.insert = function (data, onSuccess, onError) {

    var url = "/api/addresses/"

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)
}

sabio.addresses.services.getById = function (id, onSuccess, onError) {

    var url = "/api/addresses/" + id

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)

}

sabio.addresses.services.getAll = function (onSuccess, onError) {

    var url = "/api/addresses/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)
}

sabio.addresses.services.getByDistance = function (data,onSuccess, onError) {

    var url = "/api/addresses/distance"

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data:data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)

}

sabio.addresses.services.delete = function (id, onSuccess, onError) {

    var url = "/api/addresses/" + id

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "DELETE",

        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)

}

sabio.addresses.services.update = function (id, data, onSuccess, onError) {

    var url = "/api/addresses/" + id

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "PUT",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings)

}

sabio.addresses.services.searchAddress = function (address, onSuccess, onError) {

    var url = "/api/addresses/search/" + address

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false,

    };

    $.ajax(url, settings)

}


sabio.addresses.services.getAllWizard = function (onSuccess, onError) {

    var url = "/api/addresses/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false,

    };

    $.ajax(url, settings)
}



///promise call  .then


//jquery ajax  go to for reference

