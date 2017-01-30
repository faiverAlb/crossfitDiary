var modalLoading;
var CrossfitterService = (function () {
    function CrossfitterService(pathToApp) {
        this.pathToApp = pathToApp;
        this.createWorkout = function (model) {
            modalLoading.init(true);
            return $.ajax({
                type: 'POST',
                url: this.pathToApp + "api/createWorkout",
                data: model,
                async: false
            }).done(function () {
                modalLoading.init(false);
                window.location.href = "/Home";
            });
        };
        this.logWorkout = function (model) {
            modalLoading.init(true);
            return $.post({
                url: this.pathToApp + "api/logWorkout",
                data: model
            }).done(function () {
                modalLoading.init(false);
                window.location.href = "/Home";
            });
        };
        this.createAndLogWorkout = function (model) {
            modalLoading.init(true);
            return $.post({
                url: this.pathToApp + "api/createAndLogNewWorkout",
                data: model
            }).done(function () {
                modalLoading.init(false);
                window.location.href = "/Home";
            });
        };
    }
    return CrossfitterService;
}());
//# sourceMappingURL=CrossfitterService.js.map