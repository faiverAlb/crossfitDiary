var BaseService = (function () {
    function BaseService() {
        this.constructPagingUrl = function (url, serverProcessedViewModel) {
            var trimmed = url.replace(/\/+$/, "");
            url = trimmed.indexOf("?") >= 0 ? url + "&" : url + "?";
            url = url + "searchingWord=" + serverProcessedViewModel.searchingWord + "&pageIndex=" + serverProcessedViewModel.pageIndex + "&pageSize=" + serverProcessedViewModel.pageSize + "&orderByAsc=" + serverProcessedViewModel.orderByAsc;
            if (serverProcessedViewModel.propertyToSortBy != null) {
                url = url + "&propertyToSortBy=" + serverProcessedViewModel.propertyToSortBy;
            }
            return url;
        };
    }
    // Make a GET call to the service
    BaseService.prototype.get = function (url) {
        return Q.Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                success: function (data) { return resolve(data); },
                method: "GET",
                error: function (e) { return reject(e); }
            });
        });
    };
    // Make a POST call to the service
    BaseService.prototype.post = function (url, model) {
        return Q.Promise(function (resolve, reject) {
            return $.ajax({
                url: url,
                data: model,
                method: "POST",
                success: function (data) { return resolve(data); },
                error: function (e) { return reject(e); }
            });
        });
    };
    //TODO: Merge with usual post
    BaseService.prototype.postExtended = function (url, model, contentType, processData) {
        return Q.Promise(function (resolve, reject) {
            return $.ajax({
                url: url,
                type: "POST",
                data: model,
                contentType: contentType,
                processData: processData,
                method: "POST",
                success: function (data) { return resolve(data); },
                error: function (e) { return reject(e); }
            });
        });
    };
    // Make a DELETE call to the service
    BaseService.prototype.delete = function (url) {
        return Q.Promise(function (resolve, reject) {
            return $.ajax({
                url: url,
                method: "DELETE",
                success: function (data) { return resolve(data); },
                error: function (e) { return reject(e); }
            });
        });
    };
    return BaseService;
}());
//# sourceMappingURL=BaseService.js.map