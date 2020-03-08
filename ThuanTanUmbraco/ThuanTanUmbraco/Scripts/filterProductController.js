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
        var query = "";
        var currentUrl = location.pathname;
        var colorParam = filter.getParameterByName("colors");
        var colorValue = $(e).find(".master__color").val().replace(" ", "");
        if (colorParam != null) {
            var listColors = colorParam.split(",");
            if (actionId === 0) {
                if (listColors.indexOf(colorValue) < 0) {
                    listColors.push(colorValue);
                }
                query = "?colors=" + listColors.join(encodeURIComponent(","));
            }
            else if (actionId === 1) {
                if (listColors.indexOf(colorValue) >= 0) {
                    listColors = $.grep(listColors, function (value) {
                        return value !== colorValue;
                    });
                }
                query = listColors.length !== 0 ? "?colors=" + listColors.join(encodeURIComponent(",")) : "";
            }
        } else {
            if (actionId === 0) {
                query = "?colors=" + colorValue;
            }
        }
        window.location = currentUrl + query;
    },
    getParameterByName: function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }
}
filter.init();