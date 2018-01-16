var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var CrossfitterService = (function (_super) {
    __extends(CrossfitterService, _super);
    function CrossfitterService(pathToApp) {
        var _this = _super.call(this) || this;
        _this.pathToApp = pathToApp;
        _this.createWorkout = function (model) {
            return _this.post(_this.pathToApp + "api/createWorkout", model)
                .finally(function () {
                window.location.href = "/Home";
            });
        };
        _this.logWorkout = function (model) {
            return _this.post(_this.pathToApp + "api/logWorkout", model)
                .finally(function () {
                window.location.href = "/Home";
            });
        };
        _this.createAndLogWorkout = function (model) {
            return _this.post(_this.pathToApp + "api/createAndLogNewWorkout", model)
                .finally(function () {
                window.location.href = "/Home";
            });
        };
        _this.getAvailableWorkouts = function () {
            return _this.get(_this.pathToApp + "api/getAvailableWorkouts");
        };
        _this.getExercises = function () {
            return _this.get(_this.pathToApp + "api/getExercises");
        };
        return _this;
    }
    return CrossfitterService;
}(BaseService));
//# sourceMappingURL=CrossfitterService.js.map