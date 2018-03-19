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
    private  _createWorkoutController: CreateWorkoutController;
    private _chooseExistingWorkoutController: ChooseExistingWorkoutController;
    private _service: CrossfitterService;

    /* Observables */
    private _logWorkoutController: KnockoutObservable<LogWorkoutController>;
    private _isCreateNewWorkoutPressed: KnockoutObservable<boolean>;
    private _canSeeLoggingContainer: KnockoutObservable<boolean>;

    /* Computeds */

    constructor(public parameters: BasicParameters) {
      this._service = new CrossfitterService(parameters.pathToApp);

      this._createWorkoutController = new CreateWorkoutController(this._service);
      this._chooseExistingWorkoutController = new ChooseExistingWorkoutController(this._service);
      this._logWorkoutController = ko.observable(null);
      this._canSeeLoggingContainer = ko.observable(false);

      this._createWorkoutController.selectedWorkoutType.subscribe((selectedWorkoutType: BaseKeyValuePairModel<number, string>) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
          return;
        }

        this._canSeeLoggingContainer(true);
        this._logWorkoutController(new LogWorkoutController(this._createWorkoutController.workoutToCreate(), true, this._service));
      });

      this._isCreateNewWorkoutPressed = ko.observable(false);

      this._chooseExistingWorkoutController.selectedWorkout.subscribe((selectedWorkout: WorkoutViewModel) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkout == undefined || selectedWorkout == null) {
          return;
        }
        this._logWorkoutController(new LogWorkoutController(this._chooseExistingWorkoutController.workoutToDisplay(), false, this._service));

        this._canSeeLoggingContainer(true);
      });
    }
    private manageWorkoutClick = (isCreateNewWorkout: boolean) => {
      this._chooseExistingWorkoutController.clearState();
      this._createWorkoutController.clearState();

      this._isCreateNewWorkoutPressed(isCreateNewWorkout);
      this._logWorkoutController(null);
    };

  }
}