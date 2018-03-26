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
var Pages;
(function (Pages) {
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var BaseController = General.BaseController;
    var ChooseExistingWorkoutController = (function (_super) {
        __extends(ChooseExistingWorkoutController, _super);
        /* Computeds */
        function ChooseExistingWorkoutController(service, errorMessager) {
            var _this = _super.call(this) || this;
            _this.service = service;
            _this.errorMessager = errorMessager;
            _this.loadAvailableWorkouts = function () {
                _this.service.getAvailableWorkouts()
                    .then(function (availableWorkouts) {
                    _this._availableWorkouts(availableWorkouts);
                })
                    .fail(function (response) {
                    _this.errorMessager.addMessage(response.responseText, false);
                });
            };
            _this.clearState = function () {
                _this.selectedWorkout(null);
            };
            _this._availableWorkouts = ko.observableArray([]);
            _this.selectedWorkout = ko.observable(null);
            _this.workoutToDisplay = ko.observable(null);
            ko.computed(function () {
                var workout = _this.selectedWorkout();
                if (!workout) {
                    return;
                }
                _this.workoutToDisplay(new WorkoutViewModelObservable(workout, true));
            });
            _this.loadAvailableWorkouts();
            return _this;
        }
        return ChooseExistingWorkoutController;
    }(BaseController));
    Pages.ChooseExistingWorkoutController = ChooseExistingWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ChooseExistingWorkoutController.js.map