module Pages {
  import CrossfitterService = General.CrossfitterService;
  import BasicParameters = General.BasicParameters;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutViewModel = Models.WorkoutViewModel;

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

    /* Сivilians */
    _createWorkoutController: CreateWorkoutController;
    _chooseExistingWorkoutController: ChooseExistingWorkoutController;
    _service: CrossfitterService;

    /* Observables */
    _logWorkoutController: KnockoutObservable<LogWorkoutController>;
    _isCreateNewWorkoutPressed: KnockoutObservable<boolean>;
    _canSeeLoggingContainer: KnockoutObservable<boolean>;

    /* Computeds */

    

    constructor(public parameters: BasicParameters) {
      this._service = new CrossfitterService(parameters.pathToApp);

      this._createWorkoutController = new CreateWorkoutController(parameters, this._service);
      this._chooseExistingWorkoutController = new ChooseExistingWorkoutController(this._service);
      this._logWorkoutController = ko.observable(null);
      this._canSeeLoggingContainer = ko.observable(false);

      this._createWorkoutController._selectedWorkoutType.subscribe((selectedWorkoutType: BaseKeyValuePairModel<number, string>) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
          return;
        }

        this._canSeeLoggingContainer(true);
        this._logWorkoutController(new LogWorkoutController(this._createWorkoutController._workoutToCreate(), true, this._service));
      });
      this._isCreateNewWorkoutPressed = ko.observable(false);

      this._chooseExistingWorkoutController._selectedWorkout.subscribe((selectedWorkout: WorkoutViewModel) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkout == undefined || selectedWorkout == null) {
          return;
        }
        this._logWorkoutController(new LogWorkoutController(this._chooseExistingWorkoutController._workoutToDisplay(), false, this._service));

        this._canSeeLoggingContainer(true);
      });


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
      this._chooseExistingWorkoutController.clearState();
      this._createWorkoutController.clearState();

      this._isCreateNewWorkoutPressed(isCreateNewWorkout);
      this._logWorkoutController(null);
    };

  }
}