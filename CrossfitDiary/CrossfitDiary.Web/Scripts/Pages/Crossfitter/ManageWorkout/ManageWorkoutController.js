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
        function ManageWorkoutController(parameters) {
            var _this = this;
            this.parameters = parameters;
            //    private logFunction = () => {
            //      const canLogWorkout = this.logWorkoutController().canLogWorkout();
            //
            //      if (this.isCreateNewWorkoutPressed()) {
            //        const canCreateWorkout = this.createWorkoutController.canCreateCreateWorkout();
            //        if (canCreateWorkout && canLogWorkout) {
            //          const model = {
            //            newWorkoutViewModel: this.createWorkoutController.getCreateWorkoutModel(),
            //            logWorkoutViewModel: this.logWorkoutController().toJSON()
            //          };
            //          this._service.createAndLogWorkout(model);
            //        }
            //      } else {
            //        if (canLogWorkout) {
            //          this._service.logWorkout(this.logWorkoutController().toJSON());
            //        }
            //      }
            //    };
            //    private createLogController = (lightLogModel) => {
            //      lightLogModel.crossfitterWorkoutId = null;
            //      this.logWorkoutController(new LogWorkoutController(lightLogModel, this.logFunction));
            //    };
            this.manageWorkoutClick = function (isCreateNewWorkout) {
                //      this.chooseExistingWorkoutController.clearState();
                //      this.createWorkoutController.clearState();
                _this.isCreateNewWorkoutPressed(isCreateNewWorkout);
                _this.logWorkoutController(null);
            };
            this._service = new CrossfitterService(parameters.pathToApp);
            this.createWorkoutController = new Pages.CreateWorkoutController(parameters, this._service);
            this.chooseExistingWorkoutController = new Pages.ChooseExistingWorkoutController(parameters, this._service);
            this.logWorkoutController = ko.observable();
            this.isAnyContainersVisible = ko.observable(false);
            this.isCreateNewWorkoutPressed = ko.observable(false);
            //      this.chooseExistingWorkoutController.workoutToDisplay.subscribe((newValue) => {
            //        if (!newValue) {
            //          return;
            //        }
            //        const lightLogModel = {
            //          selectedWorkoutType: newValue.selectedWorkoutType(),
            //          simpleRoutines: newValue.simpleRoutines(),
            //          selectedWorkout: this.chooseExistingWorkoutController.selectedWorkout,
            //          logWorkoutText: "Log workout"
            //        };
            //        this.createLogController(lightLogModel);
            //      });
            //      this.createWorkoutController.simpleRoutines.subscribe((newValue) => {
            //        if (!newValue || newValue.length === 0) {
            //          return;
            //        }
            //        var lightLogModel = {
            //          selectedWorkoutType: this.createWorkoutController.selectedWorkoutType(),
            //          simpleRoutines: this.createWorkoutController.simpleRoutines(),
            //          logWorkoutText: "Create and log workout"
            //        };
            //        this.createLogController(lightLogModel);
            //      });
            //      ko.computed(() => {
            //        this.isAnyContainersVisible(this.logWorkoutController() != null &&
            //          (this.chooseExistingWorkoutController.selectedWorkout() || this.createWorkoutController.hasAnyRoutines()));
            //      });
        }
        return ManageWorkoutController;
    }());
    Pages.ManageWorkoutController = ManageWorkoutController;
})(Pages || (Pages = {}));
//# sourceMappingURL=ManageWorkoutController.js.map