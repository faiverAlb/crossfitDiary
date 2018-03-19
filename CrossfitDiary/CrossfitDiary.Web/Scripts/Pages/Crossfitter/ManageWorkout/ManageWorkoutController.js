var Pages;
(function (Pages) {
    var CrossfitterService = General.CrossfitterService;
    ko.validation.init({
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',
        decorateInputElement: true,
        insertMessages: false,
        grouping: {
            deep: true,
            live: true,
            observable: true
        }
    });
    var ManageWorkoutController = (function () {
        /* Computeds */
        function ManageWorkoutController(parameters) {
            var _this = this;
            this.parameters = parameters;
            this.manageWorkoutClick = function (isCreateNewWorkout) {
                _this._chooseExistingWorkoutController.clearState();
                _this._createWorkoutController.clearState();
                _this._isCreateNewWorkoutPressed(isCreateNewWorkout);
                _this._logWorkoutController(null);
            };
            this._service = new CrossfitterService(parameters.pathToApp);
            this._createWorkoutController = new Pages.CreateWorkoutController(this._service);
            this._chooseExistingWorkoutController = new Pages.ChooseExistingWorkoutController(this._service);
            this._logWorkoutController = ko.observable(null);
            this._canSeeLoggingContainer = ko.observable(false);
            this._createWorkoutController.selectedWorkoutType.subscribe(function (selectedWorkoutType) {
                _this._canSeeLoggingContainer(false);
                if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
                    return;
                }
                _this._canSeeLoggingContainer(true);
                _this._logWorkoutController(new Pages.LogWorkoutController(_this._createWorkoutController.workoutToCreate(), true, _this._service));
            });
            this._isCreateNewWorkoutPressed = ko.observable(false);
            this._chooseExistingWorkoutController.selectedWorkout.subscribe(function (selectedWorkout) {
                _this._canSeeLoggingContainer(false);
                if (selectedWorkout == undefined || selectedWorkout == null) {
                    return;
                }
                _this._logWorkoutController(new Pages.LogWorkoutController(_this._chooseExistingWorkoutController.workoutToDisplay(), false, _this._service));
                _this._canSeeLoggingContainer(true);
            });
        }
        return ManageWorkoutController;
    }());
    Pages.ManageWorkoutController = ManageWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ManageWorkoutController.js.map