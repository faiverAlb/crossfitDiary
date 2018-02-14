var Pages;
(function (Pages) {
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var ChooseExistingWorkoutController = (function () {
        function ChooseExistingWorkoutController(_service) {
            var _this = this;
            this._service = _service;
            this.loadAvailableWorkouts = function () {
                _this._service.getAvailableWorkouts()
                    .then(function (availableWorkouts) {
                    _this._availableWorkouts(availableWorkouts);
                });
            };
            this.clearState = function () {
                _this._selectedWorkout(null);
            };
            this._availableWorkouts = ko.observableArray([]);
            this._selectedWorkout = ko.observable(null);
            this._workoutToDisplay = ko.observable(null);
            ko.computed(function () {
                var workout = _this._selectedWorkout();
                if (!workout) {
                    return;
                }
                _this._workoutToDisplay(new WorkoutViewModelObservable(workout, true));
            });
            this.loadAvailableWorkouts();
        }
        return ChooseExistingWorkoutController;
    }());
    Pages.ChooseExistingWorkoutController = ChooseExistingWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ChooseExistingWorkoutController.js.map