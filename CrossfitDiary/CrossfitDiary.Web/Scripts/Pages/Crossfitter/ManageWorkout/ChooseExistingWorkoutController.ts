module Pages {
  import WorkoutViewModel = Models.WorkoutViewModel;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import BaseController = General.BaseController;

  export class ChooseExistingWorkoutController extends BaseController {
    /* Сivilians */

    /* Observables */
    private _availableWorkouts: KnockoutObservableArray<WorkoutViewModel>;
    public selectedWorkout: KnockoutObservable<WorkoutViewModel>;
    public workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;

    /* Computeds */

    constructor(public service: General.CrossfitterService) {
      super();
      this._availableWorkouts = ko.observableArray([]);
      this.selectedWorkout = ko.observable(null);
      this.workoutToDisplay = ko.observable(null);

      ko.computed(() => {
        let workout = this.selectedWorkout();
        if (!workout) {
          return;
        }
        this.workoutToDisplay(new WorkoutViewModelObservable(workout, true));

      });
      this.loadAvailableWorkouts();
    }

    private loadAvailableWorkouts = () => {
      this.service.getAvailableWorkouts()
        .then((availableWorkouts: WorkoutViewModel[]) => {
          this._availableWorkouts(availableWorkouts);
        });
    };



    public clearState = () => {
      this.selectedWorkout(null);
    };
  }
}