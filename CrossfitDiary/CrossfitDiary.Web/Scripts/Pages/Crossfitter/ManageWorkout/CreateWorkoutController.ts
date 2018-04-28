module Pages {
  import CrossfitterService = General.CrossfitterService;
  import IExerciseViewModel = Models.ExerciseViewModel;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutType = Models.WorkoutType;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class CreateWorkoutController {
    /* Сivilians */
    private _isEditMode: boolean;
    private _isRepeatMode: boolean;
    private _isDefaultMode: boolean;

    /* Observables */
    public workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;
    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    private selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;
    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;

    private _availableWorkouts: KnockoutObservableArray<WorkoutViewModel>;
    private selectedWorkout: KnockoutObservable<WorkoutViewModel>;

    /* Computeds */


    constructor(public service: CrossfitterService
      , public readonly errorMessager: ErrorMessageViewModel
      , public onWorkoutToShowAction: (isCleanLogModel: boolean, isEditMode: boolean, logModel?: ToLogWorkoutViewModel) => void
      , public preselectedWorkoutId: number | null = null
      , public preselectedCrossfitterWorkoutId: number | null = null) {
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
        this.workoutToDisplay(new WorkoutViewModelObservable(model));
      });

      ko.computed(() => {
        let workout = this.selectedWorkout();
        if (!workout) {
          this.workoutToDisplay(null);
          return;
        }
        this.selectedWorkoutType(null);
        this.workoutToDisplay(new WorkoutViewModelObservable(workout));

      });

      ko.computed(() => {
        let exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this.workoutToDisplay().addExerciseToList(exercise);
        this._selectedExercise(null);
      });

      this.selectedWorkoutType.subscribe((selectedWorkoutType: BaseKeyValuePairModel<number, string>) => {
        if (selectedWorkoutType == undefined || selectedWorkoutType == null) {
          this.onWorkoutToShowAction(true, this._isEditMode);
          return;
        }
        this.onWorkoutToShowAction(false, this._isEditMode);
      });


      this.selectedWorkout.subscribe((selectedWorkout: WorkoutViewModel) => {
        if (selectedWorkout == undefined || selectedWorkout == null) {
          this.onWorkoutToShowAction(true, this._isEditMode);
          return;
        }
        this.onWorkoutToShowAction(false, this._isEditMode);
      });

      this.loadExercises()
        .then(() => {
          return this.loadAvailableWorkouts();
        })
        .then(() => {
          return this._isEditMode ? this.loadPersonLogging() : null;
        });

    }

    private loadExercises = () => {
      return this.service
        .getExercises()
        .then((exercises: IExerciseViewModel[]) => {
          this._exercises(exercises);
        });
    };

    private loadPersonLogging = () => {
      return this.service.getPersonLoggingInfo(this.preselectedCrossfitterWorkoutId)
        .then((logModel: ToLogWorkoutViewModel) => {
          this.onWorkoutToShowAction(false, this._isEditMode, logModel);
        })
      .fail((response) => {
        this.errorMessager.addMessage(response.responseText, false);
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
      return this.service.getAvailableWorkouts()
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
