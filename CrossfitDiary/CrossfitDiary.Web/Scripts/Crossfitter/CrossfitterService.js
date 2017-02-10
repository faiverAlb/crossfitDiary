var CrossfitterService = (function () {
    function CrossfitterService(pathToApp) {
        var _this = this;
        this.pathToApp = pathToApp;
        this.createWorkout = function (model) {
            return Q.Promise(function (resolve, reject) {
                return $.ajax({
                    url: _this.pathToApp + "api/createWorkout",
                    method: 'POST',
                    success: function (data) { return resolve(data); },
                    error: function (error) { return reject(error); },
                    data: model
                });
            }).finally(function () {
                window.location.href = "/Home";
            });
        };
        this.logWorkout = function (model) {
            return Q.Promise(function (resolve, reject) {
                return $.ajax({
                    url: _this.pathToApp + "api/logWorkout",
                    method: "POST",
                    success: function (data) { return resolve(data); },
                    error: function (error) { return reject(error); },
                    data: model
                });
            }).finally(function () {
                window.location.href = "/Home";
            });
        };
        this.createAndLogWorkout = function (model) {
            return Q.Promise(function (resolve, reject) {
                return $.ajax({
                    url: _this.pathToApp + "api/createAndLogNewWorkout",
                    method: "POST",
                    success: function (data) { return resolve(data); },
                    error: function (error) { return reject(error); },
                    data: model
                });
            }).finally(function () {
                window.location.href = "/Home";
            });
        };
        this.getAvailableWorkouts = function () {
            return Q.Promise(function (resolve, reject) {
                return $.ajax({
                    url: _this.pathToApp + "api/getAvailableWorkouts",
                    method: "GET",
                    success: function (data) { return resolve(data); },
                    error: function (error) { return reject(error); }
                });
            }).finally(function () {
            });
        };
    }
    return CrossfitterService;
}());
//# sourceMappingURL=CrossfitterService.js.map