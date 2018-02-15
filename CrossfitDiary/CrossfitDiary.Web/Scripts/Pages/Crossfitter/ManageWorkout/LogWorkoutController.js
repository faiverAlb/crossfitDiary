var Pages;
(function (Pages) {
    var ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;
    var LogWorkoutController = (function () {
        /* Computeds */
        function LogWorkoutController(workoutToUse, isCreateAndLogWorkout, service) {
            var _this = this;
            this.workoutToUse = workoutToUse;
            this.isCreateAndLogWorkout = isCreateAndLogWorkout;
            this.service = service;
            this._logWorkout = function () {
                if (_this.checkAndShowErrors()) {
                    return;
                }
                if (_this.isCreateAndLogWorkout) {
                    _this.createAndLogWorkout();
                }
                else {
                    _this.logExistingWorkout();
                }
            };
            this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType, workoutToUse.getId()));
            this._logWorkoutText = "Log workout";
            if (isCreateAndLogWorkout) {
                this._logWorkoutText = "Create and log workout";
            }
        }
        LogWorkoutController.prototype.checkAndShowErrors = function () {
            var hasErrors = false;
            if (this.workoutToUse.errors().length > 0) {
                this.workoutToUse.errors.showAllMessages();
                hasErrors = true;
            }
            if (this._logToCreate().errors().length > 0) {
                this._logToCreate().errors.showAllMessages();
                hasErrors = true;
            }
            return hasErrors;
        };
        LogWorkoutController.prototype.createAndLogWorkout = function () {
            var workoutModel = this.workoutToUse.toPlainObject();
            var model = {
                newWorkoutViewModel: workoutModel,
                logWorkoutViewModel: this._logToCreate().toPlainObject()
            };
            this.service.createAndLogWorkout(model)
                .then(function () {
                window.location.href = "/Home";
            })
                .fail(function (error) {
                console.log(error);
            });
        };
        LogWorkoutController.prototype.logExistingWorkout = function () {
            this.service.logWorkout(this._logToCreate().toPlainObject())
                .then(function () {
                window.location.href = "/Home";
            })
                .fail(function (error) {
                console.log(error);
            });
        };
        return LogWorkoutController;
    }());
    Pages.LogWorkoutController = LogWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=LogWorkoutController.js.map