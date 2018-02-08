module Pages {
  import CrossfitterService = General.CrossfitterService;

  export class ManageWorkoutController {

    createWorkoutController: CreateWorkoutController;
    chooseExistingWorkoutController: ChooseExistingWorkoutController;
    service;
    logWorkoutController;
    isAnyContainersVisible;
    wantToPlanWorkout;
    isCreateNewWorkoutPressed;

    constructor(public parameters: {pathToApp: string}) {
      this.createWorkoutController = new CreateWorkoutController(parameters);
      this.chooseExistingWorkoutController = new ChooseExistingWorkoutController(parameters);
      this.service = new CrossfitterService(parameters.pathToApp);

      this.logWorkoutController = ko.observable();
      this.isAnyContainersVisible = ko.observable(false);
      this.wantToPlanWorkout = ko.observable(false);
      this.isCreateNewWorkoutPressed = ko.observable(false);
      this.chooseExistingWorkoutController.workoutToDisplay.subscribe((newValue) => {
        if (!newValue) {
          return;
        }
        let lightLogModel = {
          selectedWorkoutType: newValue.selectedWorkoutType(),
          simpleRoutines: newValue.simpleRoutines(),
          selectedWorkout: this.chooseExistingWorkoutController.selectedWorkout,
          logWorkoutText: "Log workout"
        };

        this.createLogController(lightLogModel);
      });

      this.createWorkoutController.simpleRoutines.subscribe((newValue) => {
        if (!newValue || newValue.length === 0) {
          return;
        }
        var lightLogModel = {
          selectedWorkoutType: this.createWorkoutController.selectedWorkoutType(),
          simpleRoutines: this.createWorkoutController.simpleRoutines(),
          logWorkoutText: "Create and log workout"
        };
        this.createLogController(lightLogModel);
      });




      ko.computed( () => {
        this.isAnyContainersVisible(this.logWorkoutController() != null && (this.chooseExistingWorkoutController.selectedWorkout() || this.createWorkoutController.hasAnyRoutines()));
      });

    }

    logWorkoutContainerClick =  () => {
      this.wantToPlanWorkout(false);
    };

    logFunction =  () => {
      var canLogWorkout = this.logWorkoutController().canLogWorkout();

      if (this.isCreateNewWorkoutPressed()) {
        var canCreateWorkout = this.createWorkoutController.canCreateCreateWorkout();
        if (canCreateWorkout && canLogWorkout) {
          var model = {
            newWorkoutViewModel: this.createWorkoutController.getCreateWorkoutModel(),
            logWorkoutViewModel: this.logWorkoutController().toJSON()
          };
          this.service.createAndLogWorkout(model);
        }
      } else {
        if (canLogWorkout) {
          this.service.logWorkout(this.logWorkoutController().toJSON());
        }
      }
    };


    createLogController =  (lightLogModel) => {
      lightLogModel.crossfitterWorkoutId =  null;
      this.logWorkoutController(new LogWorkoutController(lightLogModel, this.logFunction));
    };

    manageWorkoutClick = (isCreateNewWorkout) => {
      this.chooseExistingWorkoutController.clearState();
      this.createWorkoutController.clearState();

      this.isCreateNewWorkoutPressed(isCreateNewWorkout);
      this.logWorkoutController(null);
    };

  }
}

    

