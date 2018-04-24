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
    private _isEditMode: boolean;
    private _isRepeatMode: boolean;
    private _isDefaultMode: boolean;

    /* Observables */
    public workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;
    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    public selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;
    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;

    private _availableWorkouts: KnockoutObservableArray<WorkoutViewModel>;
    public selectedWorkout: KnockoutObservable<WorkoutViewModel>;

    /* Computeds */


    constructor(public service: CrossfitterService, public readonly errorMessager: ErrorMessageViewModel, public preselectedWorkoutId: number | null = null, public preselectedCrossfitterWorkoutId: number|null = null) {
      this._isEditMode = preselectedWorkoutId != null && preselectedCrossfitterWorkoutId != null;
      this._isRepeatMode = preselectedWorkoutId != null && preselectedCrossfitterWorkoutId == null;
      this._isDefaultMode = preselectedWorkoutId == null && preselectedCrossfitterWorkoutId == null;

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
          this.workoutToDisplay(null);
          return;
        }
        if (this._exercises().length === 0) {
          return;
        }
        this.selectedWorkout(null);

        let model = new WorkoutViewModel(this.selectedWorkoutType().id, []);
        this.workoutToDisplay(new WorkoutViewModelObservable(model, false));
      });

      ko.computed(() => {
        let workout = this.selectedWorkout();
        if (!workout) {
          this.workoutToDisplay(null);
          return;
        }
        this.selectedWorkoutType(null);
        this.workoutToDisplay(new WorkoutViewModelObservable(workout, false));

      });

      ko.computed(() => {
        let exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this.workoutToDisplay().addExerciseToList(exercise);
        this._selectedExercise(null);
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
    private findAndSetSelectedWorkout = () => {
      for (let i = 0; i < this._availableWorkouts().length; i++) {
        let workoutToFind = this._availableWorkouts()[i];
        if (workoutToFind.id == this.preselectedWorkoutId) {
          this.selectedWorkout(workoutToFind);
          break;
        }
      }
    };

    private loadAvailableWorkouts = () => {
      this.service.getAvailableWorkouts()
        .then((availableWorkouts: WorkoutViewModel[]) => {
          this._availableWorkouts(availableWorkouts);
        })
        .then(() => {
          if (this._isEditMode || this._isRepeatMode) {
            this.findAndSetSelectedWorkout();
          }
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });
    };

  }
}
