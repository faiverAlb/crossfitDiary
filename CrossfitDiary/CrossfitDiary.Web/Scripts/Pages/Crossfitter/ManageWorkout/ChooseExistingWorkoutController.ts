module Pages {
  import WorkoutViewModel = Models.WorkoutViewModel;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;

  export class ChooseExistingWorkoutController {

    _availableWorkouts: KnockoutObservableArray<WorkoutViewModel>;
    public _selectedWorkout: KnockoutObservable<WorkoutViewModel>;
    public _workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;

    constructor(public _service: General.CrossfitterService) {
      this._availableWorkouts = ko.observableArray([]);
      this._selectedWorkout = ko.observable(null);
      this._workoutToDisplay = ko.observable(null);

      ko.computed(() => {
        let workout = this._selectedWorkout();
        if (!workout) {
          return;
        }
        this._workoutToDisplay(new WorkoutViewModelObservable(workout, true));

      });
      this.loadAvailableWorkouts();
    }

    private loadAvailableWorkouts = () => {
      this._service.getAvailableWorkouts()
        .then((availableWorkouts: WorkoutViewModel[]) => {
          this._availableWorkouts(availableWorkouts);
        });
    };



    public clearState = () => {
      this._selectedWorkout(null);
    };
  }
}