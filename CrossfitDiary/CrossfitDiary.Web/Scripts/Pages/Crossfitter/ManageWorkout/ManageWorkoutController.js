var Pages;
(function (Pages) {
    var CrossfitterService = General.CrossfitterService;
    var ManageWorkoutController = (function () {
        function ManageWorkoutController(parameters) {
            var _this = this;
            this.parameters = parameters;
            this.logWorkoutContainerClick = function () {
                _this.wantToPlanWorkout(false);
            };
            this.logFunction = function () {
                var canLogWorkout = _this.logWorkoutController().canLogWorkout();
                if (_this.isCreateNewWorkoutPressed()) {
                    var canCreateWorkout = _this.createWorkoutController.canCreateCreateWorkout();
                    if (canCreateWorkout && canLogWorkout) {
                        var model = {
                            newWorkoutViewModel: _this.createWorkoutController.getCreateWorkoutModel(),
                            logWorkoutViewModel: _this.logWorkoutController().toJSON()
                        };
                        _this.service.createAndLogWorkout(model);
                    }
                }
                else {
                    if (canLogWorkout) {
                        _this.service.logWorkout(_this.logWorkoutController().toJSON());
                    }
                }
            };
            this.createLogController = function (lightLogModel) {
                lightLogModel.crossfitterWorkoutId = null;
                _this.logWorkoutController(new Pages.LogWorkoutController(lightLogModel, _this.logFunction));
            };
            this.manageWorkoutClick = function (isCreateNewWorkout) {
                _this.chooseExistingWorkoutController.clearState();
                _this.createWorkoutController.clearState();
                _this.isCreateNewWorkoutPressed(isCreateNewWorkout);
                _this.logWorkoutController(null);
            };
            this.createWorkoutController = new Pages.CreateWorkoutController(parameters);
            this.chooseExistingWorkoutController = new Pages.ChooseExistingWorkoutController(parameters);
            this.service = new CrossfitterService(parameters.pathToApp);
            this.logWorkoutController = ko.observable();
            this.isAnyContainersVisible = ko.observable(false);
            this.wantToPlanWorkout = ko.observable(false);
            this.isCreateNewWorkoutPressed = ko.observable(false);
            this.chooseExistingWorkoutController.workoutToDisplay.subscribe(function (newValue) {
                if (!newValue) {
                    return;
                }
                var lightLogModel = {
                    selectedWorkoutType: newValue.selectedWorkoutType(),
                    simpleRoutines: newValue.simpleRoutines(),
                    selectedWorkout: _this.chooseExistingWorkoutController.selectedWorkout,
                    logWorkoutText: "Log workout"
                };
                _this.createLogController(lightLogModel);
            });
            this.createWorkoutController.simpleRoutines.subscribe(function (newValue) {
                if (!newValue || newValue.length === 0) {
                    return;
                }
                var lightLogModel = {
                    selectedWorkoutType: _this.createWorkoutController.selectedWorkoutType(),
                    simpleRoutines: _this.createWorkoutController.simpleRoutines(),
                    logWorkoutText: "Create and log workout"
                };
                _this.createLogController(lightLogModel);
            });
            ko.computed(function () {
                _this.isAnyContainersVisible(_this.logWorkoutController() != null && (_this.chooseExistingWorkoutController.selectedWorkout() || _this.createWorkoutController.hasAnyRoutines()));
            });
        }
        return ManageWorkoutController;
    }());
    Pages.ManageWorkoutController = ManageWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ManageWorkoutController.js.map