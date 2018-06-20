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
    var ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;
    var ExerciseViewModel = Models.ExerciseViewModel;
    var PersonExerciseRecord = Models.PersonExerciseRecord;
    var CrossfitterService = (function (_super) {
        __extends(CrossfitterService, _super);
        function CrossfitterService(pathToApp, isDataLoading) {
            var _this = _super.call(this) || this;
            _this.pathToApp = pathToApp;
            _this.isDataLoading = isDataLoading;
            _this.createAndLogWorkout = function (model) {
                _this.isDataLoading(true);
                return _this.post(_this.pathToApp + "api/createAndLogNewWorkout", model);
            };
            _this.getAllCrossfittersWorkouts = function (userId, exerciseId, page, pageSize) {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp +
                    ("api/getAllCrossfittersWorkouts?userId=" + userId + "&exerciseId=" + exerciseId + "&page=" + page + "&pageSize=" + pageSize))
                    .then(function (jsonData) {
                    return jsonData.map(function (x) { return new ToLogWorkoutViewModel().deserialize(x); });
                });
            };
            _this.getExercises = function () {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp + "api/getExercises")
                    .then(function (jsonData) {
                    return jsonData.map(function (x) { return new ExerciseViewModel().deserialize(x); });
                })
                    .finally(function () { _this.isDataLoading(false); });
            };
            _this.getStatisticalExercises = function () {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp + "api/getStatisticalExercises")
                    .then(function (jsonData) {
                    return jsonData.map(function (x) { return new ExerciseViewModel().deserialize(x); });
                })
                    .finally(function () { _this.isDataLoading(false); });
            };
            _this.getPersonExerciseMaximumWeight = function (exerciseId) {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp + ("api/exercises/" + exerciseId + "/personMaximum"))
                    .then(function (jsonData) {
                    return jsonData.map(function (x) { return new PersonExerciseRecord().deserialize(x); });
                })
                    .finally(function () { _this.isDataLoading(false); });
            };
            _this.getAllPersonsExerciseMaximumWeights = function (exerciseId) {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp + ("api/exercises/" + exerciseId + "/allPersonsMaximums"))
                    .then(function (jsonData) {
                    return jsonData.map(function (x) { return new PersonExerciseRecord().deserialize(x); });
                })
                    .finally(function () { _this.isDataLoading(false); });
            };
            _this.removeWorkout = function (crossfitterWorkoutId) {
                _this.isDataLoading(true);
                return _this.delete("api/removeWorkout/" + crossfitterWorkoutId).finally(function () { _this.isDataLoading(false); });
            };
            _this.getPersonLoggingInfo = function (preselectedCrossfitterWorkoutId) {
                _this.isDataLoading(true);
                return _this.get(_this.pathToApp + ("api/getPersonLoggingInfo/" + preselectedCrossfitterWorkoutId))
                    .then(function (jsonData) {
                    return new ToLogWorkoutViewModel().deserialize(jsonData);
                })
                    .finally(function () {
                    _this.isDataLoading(false);
                });
            };
            return _this;
        }
        return CrossfitterService;
    }(General.BaseService));
    General.CrossfitterService = CrossfitterService;
})(General || (General = {}));
//# sourceMappingURL=CrossfitterService.js.map