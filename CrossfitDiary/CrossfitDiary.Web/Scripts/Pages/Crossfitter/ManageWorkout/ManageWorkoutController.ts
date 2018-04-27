module Pages {
  import CrossfitterService = General.CrossfitterService;
  import BasicParameters = General.BasicParameters;
  import BaseController = General.BaseController;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

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
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    /* Observables */
    private _logWorkoutController: KnockoutObservable<LogWorkoutController>;
    private _isCreateNewWorkout: KnockoutObservable<boolean>;

    /* Computeds */

    constructor(public parameters: BasicParameters, preselectedWorkoutId: number|null = null, preselectedCrossfitterWorkoutId: number|null = null) {
      super();

      /* Сivilians */
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
      this.errorMessager = new ErrorMessageViewModel();
     

      this._createWorkoutController = new CreateWorkoutController(this._service, this.errorMessager, this.onWorkoutToShowAction, preselectedWorkoutId, preselectedCrossfitterWorkoutId);

      /* Observables */
      this._logWorkoutController = ko.observable(null);
      this._isCreateNewWorkout = ko.observable(true);
    }

    private onWorkoutToShowAction = (isCleanLogModel: boolean, isEditMode: boolean, logModel?: ToLogWorkoutViewModel) => {
      if (isCleanLogModel) {
        this._logWorkoutController(null);
        return;
      }
      this._logWorkoutController(new LogWorkoutController(this._createWorkoutController.workoutToDisplay(), true, this._service, this.errorMessager, isEditMode, logModel));
    }
  }
}