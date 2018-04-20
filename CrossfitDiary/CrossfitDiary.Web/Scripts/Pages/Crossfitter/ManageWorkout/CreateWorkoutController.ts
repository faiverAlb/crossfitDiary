module Pages {
  import CrossfitterService = General.CrossfitterService;
  import IExerciseViewModel = Models.ExerciseViewModel;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutType = Models.WorkoutType;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import BaseController = General.BaseController;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;

  export class CreateWorkoutController {
    /* Сivilians */

    /* Observables */
    public workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;
    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    public selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;
    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;

    private _availableWorkouts: KnockoutObservableArray<WorkoutViewModel>;
    public selectedWorkout: KnockoutObservable<WorkoutViewModel>;

    /* Computeds */


    constructor(public service: CrossfitterService, public readonly errorMessager: ErrorMessageViewModel) {

      this._workoutTypes = ko.observable(
        new Array(
          new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime])
          , new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP])
          , new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM])
          , new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])
        ));
      this.selectedWorkoutType = ko.observable(null);
      this.workoutToDisplay = ko.observable(null);
      this._exercises = ko.observableArray([]);

      this._selectedExercise = ko.observable(null);
      this._availableWorkouts = ko.observableArray([]);
      this.selectedWorkout = ko.observable(null);

      ko.computed(() => {
        this._selectedExercise(null);
          if (!this.selectedWorkoutType()) {
          return;
        }
        if (this._exercises().length === 0) {
          return;
        }

        let model = new WorkoutViewModel(this.selectedWorkoutType().id, []);
        this.workoutToDisplay(new WorkoutViewModelObservable(model, false));
      });

      ko.computed(() => {
        let exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this.workoutToDisplay().addExerciseToList(exercise);
        this._selectedExercise(null);
      });

      ko.computed(() => {
        let workout = this.selectedWorkout();
        if (!workout) {
          return;
        }
        this.workoutToDisplay(new WorkoutViewModelObservable(workout, false));

      });

      Q.all([this.loadExercises(), this.loadAvailableWorkouts()]);
    }

    private  loadExercises = () => {
      this.service
        .getExercises()
        .then((exercises: IExerciseViewModel[]) => {
          this._exercises(exercises);
        });
    };

    private loadAvailableWorkouts = () => {
      this.service.getAvailableWorkouts()
        .then((availableWorkouts: WorkoutViewModel[]) => {
          this._availableWorkouts(availableWorkouts);
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });
    };

    public clearState = () => {
      this.selectedWorkoutType(null);
      this.selectedWorkout(null);
    };
  }
}
