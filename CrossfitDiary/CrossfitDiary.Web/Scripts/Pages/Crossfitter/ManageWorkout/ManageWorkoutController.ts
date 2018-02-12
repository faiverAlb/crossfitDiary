module Pages {
  import CrossfitterService = General.CrossfitterService;
  import BasicParameters = General.BasicParameters;

  declare var ko;
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
  export class ManageWorkoutController {
    createWorkoutController: CreateWorkoutController;
//    chooseExistingWorkoutController: ChooseExistingWorkoutController;
    _service: CrossfitterService;
    logWorkoutController: KnockoutObservable<LogWorkoutController>;
    isAnyContainersVisible: KnockoutObservable<boolean>;
    isCreateNewWorkoutPressed: KnockoutObservable<boolean>;

    constructor(public parameters: BasicParameters) {
      this._service = new CrossfitterService(parameters.pathToApp);

      this.createWorkoutController = new CreateWorkoutController(parameters, this._service);
//      this.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);
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

    private manageWorkoutClick = (isCreateNewWorkout: boolean) => {
//      this.chooseExistingWorkoutController.clearState();
//      this.createWorkoutController.clearState();

      this.isCreateNewWorkoutPressed(isCreateNewWorkout);
      this.logWorkoutController(null);
    };

  }
}