﻿module Pages {
  import CrossfitterService = General.CrossfitterService;
  import BasicParameters = General.BasicParameters;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import BaseController = General.BaseController;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;

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
  export class ManageWorkoutController extends BaseController {

    /* Сivilians */
    private  _createWorkoutController: CreateWorkoutController;
    private _chooseExistingWorkoutController: ChooseExistingWorkoutController;
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    /* Observables */
    private _logWorkoutController: KnockoutObservable<LogWorkoutController>;
    private _isCreateNewWorkout: KnockoutObservable<boolean>;
    private _canSeeLoggingContainer: KnockoutObservable<boolean>;

    /* Computeds */

    constructor(public parameters: BasicParameters) {
      super();

      /* Сivilians */
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
      this.errorMessager = new ErrorMessageViewModel();

      this._createWorkoutController = new CreateWorkoutController(this._service, this.errorMessager);
      this._chooseExistingWorkoutController = new ChooseExistingWorkoutController(this._service, this.errorMessager);


      /* Observables */
      this._logWorkoutController = ko.observable(null);
      this._canSeeLoggingContainer = ko.observable(false);
      this._isCreateNewWorkout = ko.observable(true);

      /* Computeds */
      this._createWorkoutController.selectedWorkoutType.subscribe((selectedWorkoutType: BaseKeyValuePairModel<number, string>) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
          return;
        }

        this._canSeeLoggingContainer(true);
        this._logWorkoutController(new LogWorkoutController(this._createWorkoutController.workoutToCreate(), true, this._service, this.errorMessager));
      });


      this._chooseExistingWorkoutController.selectedWorkout.subscribe((selectedWorkout: WorkoutViewModel) => {
        this._canSeeLoggingContainer(false);
        if (selectedWorkout == undefined || selectedWorkout == null) {
          return;
        }
        this._logWorkoutController(new LogWorkoutController(this._chooseExistingWorkoutController.workoutToDisplay(), false, this._service, this.errorMessager));

        this._canSeeLoggingContainer(true);
      });
    }
    private manageWorkoutClick = (isCreateNewWorkout: boolean) => {
      this._chooseExistingWorkoutController.clearState();
      this._createWorkoutController.clearState();

      this._isCreateNewWorkout(isCreateNewWorkout);
      this._logWorkoutController(null);
    };

  }
}