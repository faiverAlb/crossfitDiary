var Pages;
(function (Pages) {
    var WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
    var ChooseExistingWorkoutController = (function () {
        /* Computeds */
        function ChooseExistingWorkoutController(service) {
            var _this = this;
            this.service = service;
            this.loadAvailableWorkouts = function () {
                _this.service.getAvailableWorkouts()
                    .then(function (availableWorkouts) {
                    _this._availableWorkouts(availableWorkouts);
                });
            };
            this.clearState = function () {
                _this.selectedWorkout(null);
            };
            this._availableWorkouts = ko.observableArray([]);
            this.selectedWorkout = ko.observable(null);
            this.workoutToDisplay = ko.observable(null);
            ko.computed(function () {
                var workout = _this.selectedWorkout();
                if (!workout) {
                    return;
                }
                _this.workoutToDisplay(new WorkoutViewModelObservable(workout, true));
            });
            this.loadAvailableWorkouts();
        }
        return ChooseExistingWorkoutController;
    }());
    Pages.ChooseExistingWorkoutController = ChooseExistingWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ChooseExistingWorkoutController.js.map