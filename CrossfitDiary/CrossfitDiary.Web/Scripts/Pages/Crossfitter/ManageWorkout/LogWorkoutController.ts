module Pages {
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;
  import CrossfitterService = General.CrossfitterService;
  import BaseController = General.BaseController;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class LogWorkoutController extends BaseController{
    /* Сivilians */
    /* Observables */
    private _logToCreate: KnockoutObservable<ToLogWorkoutViewModelObservable>;

    /* Computeds */


    constructor(public workoutToUse: WorkoutViewModelObservable
              , public isCreateAndLogWorkout: boolean
              , public service: CrossfitterService
              , public readonly errorMessager: ErrorMessageViewModel
              , logModel?: ToLogWorkoutViewModel) {
      super();
      this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType, workoutToUse.getId(), logModel));

    }

    private checkAndShowErrors():boolean {
      let hasErrors = false;
      if (this.workoutToUse.errors().length > 0) {
        this.workoutToUse.errors.showAllMessages();
        hasErrors = true;
      }
      if (this._logToCreate().errors().length > 0) {
        this._logToCreate().errors.showAllMessages();
        hasErrors = true;
      }
      return hasErrors;
    }

    private createAndLogWorkout() {
      let workoutModel = this.workoutToUse.toPlainObject();
      const model = {
        newWorkoutViewModel: workoutModel,
        logWorkoutViewModel: this._logToCreate().toPlainObject()
      };
      this.service.createAndLogWorkout(model)
        .then(() => {
          window.location.href = "/Home";
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });
    }

    private _logWorkout = () => {
      if (this.checkAndShowErrors()) {
        return;
      }
      this.createAndLogWorkout();
    }
  }
}