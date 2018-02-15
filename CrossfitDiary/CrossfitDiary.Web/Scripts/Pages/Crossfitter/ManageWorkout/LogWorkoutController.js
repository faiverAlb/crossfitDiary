var Pages;
(function (Pages) {
    var ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;
    var LogWorkoutController = (function () {
        /* Computeds */
        function LogWorkoutController(workoutToUse, createAndLogWorkout) {
            var _this = this;
            this.workoutToUse = workoutToUse;
            this.createAndLogWorkout = createAndLogWorkout;
            this._logWorkout = function () {
                var hasErrors = false;
                if (_this.workoutToUse.errors().length > 0) {
                    _this.workoutToUse.errors.showAllMessages();
                    hasErrors = true;
                }
                if (_this._logToCreate().errors().length > 0) {
                    _this._logToCreate().errors.showAllMessages();
                    hasErrors = true;
                }
                if (hasErrors) {
                    return;
                }
            };
            this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType));
            this._logWorkoutText = "Log workout";
            if (createAndLogWorkout) {
                this._logWorkoutText = "Create and log workout";
            }
        }
        return LogWorkoutController;
    }());
    Pages.LogWorkoutController = LogWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=LogWorkoutController.js.map