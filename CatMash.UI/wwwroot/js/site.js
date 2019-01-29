function postAjax(url, data, containerId, functionSuccess, functionFail) {
    var container = $('#' + containerId);
    if (container !== null)
        container.html('<img src="/Images/loading.gif" alt="loading" />');

    var settings = {
        type: 'POST',
        url: url,
        data: data,
        headers: {
            'X-PJAX': 'true'
        },
        success: function (response) {
            if (functionSuccess !== null && $.isFunction(functionSuccess)) {
                functionSuccess(response);
            } else if (container !== null) {
                container.html(response);
            }
        },
        error: function (response) {
            var status = response.status;
            if (status !== null && status === 401) {
                // Unauthorized or bad login
                window.location.replace('/Account/SignOut');
                return;
            }

            if (functionFail !== null && $.isFunction(functionFail)) {
                functionFail(response);
            } else if (container !== null) {
                container.html(response.responseText);
            }
        }
    };
    if (data instanceof FormData) {
        settings.processData = false;
        settings.contentType = false;
    }

    $.ajax(settings);
}
