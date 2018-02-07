var Pages;
(function (Pages) {
    var CrossfitterService = General.CrossfitterService;
    var ChooseExistingWorkoutController = (function () {
        function ChooseExistingWorkoutController(parameters) {
            var _this = this;
            this.parameters = parameters;
            this.clearState = function () {
                _this.selectedWorkout(null);
            };
            this.toJSON = function () {
                var model = {
                    selectedWorkoutId: _this.selectedWorkout().id,
                    roundsFinished: _this.totalRoundsFinished,
                    partialRepsFinished: _this.partialRepsFinished,
                    timePassed: _this.totalTime,
                    distance: _this.distance,
                    wasFinished: _this.wasFinished,
                    isRx: _this.isRx()
                };
                return model;
            };
            this.loadAvailableWorkouts = function () {
                _this._service.getAvailableWorkouts().then(function (availableWorkouts) {
                    _this.availableWorkouts(availableWorkouts);
                    if (_this.hasPredefinedWorkout) {
                        var foundWorkout = ko.utils.arrayFirst(_this.availableWorkouts(), function (item) {
                            return item.id == _this.parameters.viewModel.crossfitterWorkout.selectedWorkoutId;
                        });
                        if (foundWorkout) {
                            _this.selectedWorkout(foundWorkout);
                        }
                    }
                });
            };
            this._service = new CrossfitterService(parameters.pathToApp);
            this.availableWorkouts = ko.observableArray();
            this.selectedWorkout = ko.observable();
            this.isReadOnlyMode = true;
            this.hasPredefinedWorkout = parameters.viewModel.crossfitterWorkout != null;
            this.workoutToDisplay = ko.observable();
            ko.computed(function () {
                var workout = _this.selectedWorkout();
                if (!workout) {
                    return;
                }
                workout.isReadOnlyMode = _this.isReadOnlyMode;
                _this.workoutToDisplay(new Pages.CrossfitterController(workout));
            });
            this.loadAvailableWorkouts();
        }
        return ChooseExistingWorkoutController;
    }());
    Pages.ChooseExistingWorkoutController = ChooseExistingWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ChooseExistingWorkoutController.js.map