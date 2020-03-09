var filter = {
    init: function() {

    },
    addColorParam: function(e) {
        $(e).addClass("active");
        filter.filterColorLocation(0, e);
    },
    removeColorParam: function (e) {
        $(e).removeClass("active");
        filter.filterColorLocation(1, e);
    },
    filterColor: function (e) {
        if (!$(e).hasClass("active")) {
            filter.addColorParam(e);
        } else {
            filter.removeColorParam(e);
        }
    },
    filterColorLocation: function (actionId, e) {
        var listColors = [];
        var url = filter.renderUrl("colors");
        var value = $(e).find(".master__color").val().replace(/ /g, "");
        if (url.param != null) {
            listColors = url.param.split(",");
            if (actionId === 0) {
                if (listColors.indexOf(value) < 0) {
                    listColors.push(value);
                }
            }
            else if (actionId === 1) {
                if (listColors.indexOf(value) >= 0) {
                    listColors = $.grep(listColors, function (e) {
                        return e !== value;
                    });
                }
            }
        } else {
            listColors.push(value);
        }
        var query = listColors.length !== 0 ? url.presence + url.paramName + "=" + listColors.join(encodeURIComponent(",")) : "";
        window.location = url.url + query;
    },
    filterCapacity: function (value) {
        value = value.split("-").join(encodeURIComponent(","));
        var url = filter.renderUrl("capacity");
        if (value !== 0) {
            window.location = url.url + url.presence + url.paramName + "=" + value;
        } else {
            window.location = url.url;
        }
    },
    renderUrl: function (paramName) {
        var currentUrl = window.location.href;
        var param = filter.getParameterByName(paramName, currentUrl);
        if (param !== null) {
            currentUrl = filter.removeURLParameter(paramName, currentUrl);
        }
        var presence = window.location.href.indexOf("?") >= 0 ? "&" : "?";
        return { url: currentUrl, param: param, presence: presence, paramName: paramName }
    },
    getParameterByName: function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    },
    removeURLParameter: function (key, sourceUrl) {
        var list = [];
        var url = sourceUrl.split("?")[0];
        var query = sourceUrl.split("?")[1];
        var listQuery = query.split("&");
        for (var i = 0; i < listQuery.length; i++) {
            var item = listQuery[i].split("=");
            var obj = { key: item[0], value: item[1] };
            list.push(obj);
        }
        list = $.grep(list, function (e) {
            return e.key !== key;
        });
        return url + list.length > 0 ? "?" : "";
    }
}
filter.init();