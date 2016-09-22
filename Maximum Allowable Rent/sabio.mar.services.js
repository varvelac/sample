if (!sabio.mar) {
    sabio.mar = { services: {} };
}
sabio.mar.services.upload = function (data, onSuccess, onError) {

			var url = "/api/mar/bulk"

            var settings = {
                
                contentType: "application/json",
                type: "POST",
                dataType: "json",
                success: onSuccess,
                error: onError,
                cache: false,
                data: JSON.stringify(data),
  
            };

            $.ajax(url, settings)
}

sabio.mar.services.getFirst = function (onSuccess, onError) {

    var url = "https://data.smgov.net/resource/g9ru-7m92.json"

    var settings = {
        type: "GET",
        success: onSuccess,
        error: onError,
        data: {

            "$$app_token": "08jsdAC5mAYszocBmKPXeRwX4"
        }

    };


    $.ajax(url, settings)
}


sabio.mar.services.update = function (date, onSuccess, onError) {

    var url = "https://data.smgov.net/resource/g9ru-7m92.json?$where=:updated_at >" + "'"+ date + "'" 

    var settings = {
        type: "GET",
        success: onSuccess,
        error: onError,
        data: {
 
            "$$app_token": "08jsdAC5mAYszocBmKPXeRwX4"
        }

    };


    $.ajax(url, settings)
}

