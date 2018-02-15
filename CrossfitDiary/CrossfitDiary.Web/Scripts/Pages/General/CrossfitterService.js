var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var General;
(function (General) {
    var CrossfitterService = (function (_super) {
        __extends(CrossfitterService, _super);
        function CrossfitterService(pathToApp) {
            var _this = _super.call(this) || this;
            _this.pathToApp = pathToApp;
            _this.createWorkout = function (model) {
                return _this.post(_this.pathToApp + "api/createWorkout", model);
            };
            _this.logWorkout = function (model) {
                return _this.post(_this.pathToApp + "api/logWorkout", model);
            };
            _this.createAndLogWorkout = function (model) {
                return _this.post(_this.pathToApp + "api/createAndLogNewWorkout", model);
            };
            _this.getAvailableWorkouts = function () {
                return _this.get(_this.pathToApp + "api/getAvailableWorkouts");
            };
            _this.getExercises = function () {
                return _this.get(_this.pathToApp + "api/getExercises");
            };
            _this.getStatisticalExercises = function () {
                return _this.get(_this.pathToApp + "api/getStatisticalExercises");
            };
            _this.getPersonExerciseMaximumWeight = function (exerciseId) {
                return _this.get(_this.pathToApp + ("api/exercises/" + exerciseId + "/personMaximum"));
            };
            _this.getAllPersonsExerciseMaximumWeights = function (exerciseId) {
                return _this.get(_this.pathToApp + ("api/exercises/" + exerciseId + "/allPersonsMaximums"));
            };
            _this.removeWorkout = function (crossfitterWorkoutId) {
                return _this.delete("api/removeWorkout/" + crossfitterWorkoutId);
            };
            return _this;
        }
        return CrossfitterService;
    }(General.BaseService));
    General.CrossfitterService = CrossfitterService;
})(General || (General = {}));
//# sourceMappingURL=CrossfitterService.js.map