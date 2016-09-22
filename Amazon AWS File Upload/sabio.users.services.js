

if (!sabio.users) {
    sabio.users = { services: {} };
}

sabio.users.services.insert = function (data, onSuccess, onError) {

    var url = "/api/users/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings);
};

sabio.users.services.getById = function (id, onAjaxSuccess, onAjaxError) {
    var url = "/api/users/" + id;
    var settings = {
        cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onAjaxSuccess
            , error: onAjaxError
            , type: "GET"
    };

    $.ajax(url, settings);

};

sabio.users.services.update = function (id, data, onSuccess, onError) {

    var url = "/api/users/" + id;

    var settings = {

        cache: false,
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "PUT"
        
    };
    $.ajax(url, settings);
};

sabio.users.services.getAll = function (onSuccess, onError) {

    var url = "/api/users/";
    var settings = {
        cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
    };

    $.ajax(url, settings);
};


sabio.users.services.delete = function (userId, onSuccess, onError) {

    var url = "/api/users/" + userId;
    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "DELETE"
    };

    $.ajax(url, settings);

};


sabio.users.services.login = function (data, onSuccess, onError) {

    var url = "/api/users/login";

    var settings = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
};

sabio.users.services.logout = function (onSuccess, onError) {

    var url = "/api/users/logout";

    var settings = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
       // data: data,
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
};

//register
sabio.users.services.Register = function (data, onSuccess, onError) {

    var url = "/api/users/register/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings);


};

//ForgotPassword
sabio.users.services.ForgotPassword = function (data, onSuccess, onError) {

    var url = "/api/users/ForgotPassword/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false

    };

    $.ajax(url, settings);

};

//ResetPassword
sabio.users.services.ResetPassword = function (data, onSuccess, onError) {

    var url = "/api/users/ResetPassword/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: data,
        dataType: "json",
        success: onSuccess,
        error: onError,
        cache: false
    };
    $.ajax(url, settings);

}

sabio.users.services.getByUserId = function (userId, onAjaxSuccess, onAjaxError) {

    var url = "/api/users/" + userId;

    var settings = {

        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onAjaxSuccess
        , error: onAjaxError
        , type: "GET"
    };

    $.ajax(url, settings);
}
sabio.users.services.getRoles = function (onSuccess, onError) {

    var url = "/api/users/roles";
    var settings = {
        cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
    };

    $.ajax(url, settings);
};

sabio.users.services.sendInvite = function (data, onSuccess, onError) {

    var url = "/api/users/invite/";

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

sabio.users.services.saveSearch = function (data, onSuccess, onError) {
    var url = "/api/users/saveSearch/"
    var settings = {
        cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "POST"
            , data: data
    };

    $.ajax(url, settings);
}

sabio.users.services.getSearchesByUserId = function (onSuccess, onError) {
    var url = "/api/users/getSearchesByUserId/"
    var settings = {
        cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
    };

    $.ajax(url, settings);
}

sabio.users.services.deleteSearch = function (id, onSuccess, onError) {

    var url = "/api/users/deleteSearch/" + id;
    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "DELETE"
    };

    $.ajax(url, settings);

};

sabio.users.services.updateSearch = function (id, data, onSuccess, onError) {

    var url = "/api/users/updateSearch/" + id;

    var settings = {

        cache: false,
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "PUT"

    };
    $.ajax(url, settings);
};
