module Pages {
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import ToLogWorkoutViewModelObservable = Models.ToLogWorkoutViewModelObservable;
  import CrossfitterService = General.CrossfitterService;

  export class LogWorkoutController {
    /* Сivilians */
    _logWorkoutText: string;

    /* Observables */
    private _logToCreate: KnockoutObservable<ToLogWorkoutViewModelObservable>;

    /* Computeds */


    constructor(public workoutToUse: WorkoutViewModelObservable, public isCreateAndLogWorkout: boolean, public service: CrossfitterService) {
      this._logToCreate = ko.observable(new ToLogWorkoutViewModelObservable(workoutToUse.model.workoutType, workoutToUse.getId()));

      this._logWorkoutText = "Log workout";
      if (isCreateAndLogWorkout) {
        this._logWorkoutText = "Create and log workout";
      }
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
        .fail((error) => {
          console.log(error);
        });
    }
    private logExistingWorkout() {
      
      this.service.logWorkout(this._logToCreate().toPlainObject())
        .then(() => {
          window.location.href = "/Home";
        })
        .fail((error) => {
          console.log(error);
        });
    }


    private _logWorkout = () => {
      if (this.checkAndShowErrors()) {
        return;
      }
      if (this.isCreateAndLogWorkout) {
        this.createAndLogWorkout();
      } else {
        this.logExistingWorkout();
      }
    }
  }
}