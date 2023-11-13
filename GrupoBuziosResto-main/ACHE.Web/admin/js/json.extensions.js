(function () {
    JSON.serializeElements = function (elements) {
        var json = {};
        for (var idx = 0; idx < elements.length; idx++) {
            var name = elements[idx].name;
            if (json[name]) {
                json[name] = json[name] + ',' + elements[idx].value;
            } else {
                json[name] = elements[idx].value;
            }
        }
        return json;
    }
} ());