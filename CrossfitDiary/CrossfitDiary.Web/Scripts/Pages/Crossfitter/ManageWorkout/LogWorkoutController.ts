module Pages {
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;

  export class LogWorkoutController {
    /* Сivilians */
    _logWorkoutText: string;

    /* Observables */
    private _logToCreate: KnockoutObservable<ToLogWorkoutViewModelObservable>;

    /* Computeds */


    constructor(public workoutToUse: WorkoutViewModelObservable, public createAndLogWorkout: boolean) {
      this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType));

      this._logWorkoutText = "Log workout";
      if (createAndLogWorkout) {
        this._logWorkoutText = "Create and log workout";
      }
    }


    private _logWorkout = () => {
      let hasErrors = false;
      if (this.workoutToUse.errors().length > 0) {
        this.workoutToUse.errors.showAllMessages();
        hasErrors = true;
      }
      if (this._logToCreate().errors().length > 0) {
        this._logToCreate().errors.showAllMessages();
        hasErrors = true;
      }
      if (hasErrors) {
        return;
      }


    }
  }
}