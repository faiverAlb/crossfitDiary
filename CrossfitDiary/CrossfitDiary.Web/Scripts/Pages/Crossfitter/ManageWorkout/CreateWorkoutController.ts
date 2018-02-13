module Pages {
  import BasicParameters = General.BasicParameters;
  import CrossfitterService = General.CrossfitterService;
  import IExerciseViewModel = Models.ExerciseViewModel;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutType = Models.WorkoutType;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import WorkoutViewModel = Models.WorkoutViewModel;

  export class CreateWorkoutController {
    private _service: CrossfitterService;

    private _workoutToCreate: KnockoutObservable<WorkoutViewModelObservable>;

    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    private _selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;

    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;




    constructor(parameters: BasicParameters, service: CrossfitterService) {
      this._service = service;

      this._workoutTypes = ko.observable(
        new Array(
          new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime])
          , new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP])
          , new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM])
          , new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])
        ));
      this._selectedWorkoutType = ko.observable(null);
      this._workoutToCreate = ko.observable(null);
      this._exercises = ko.observableArray([]);

      this._selectedExercise = ko.observable();

      ko.computed(() => {
        this._selectedExercise(null);
          if (!this._selectedWorkoutType()) {
          return;
        }
        if (this._exercises().length === 0) {
          return;
        }

        let model = new WorkoutViewModel(this._selectedWorkoutType().id, []);
        this._workoutToCreate(new WorkoutViewModelObservable(model, false));
      });

      ko.computed(() => {
        let exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this._workoutToCreate().addExerciseToList(exercise);
        this._selectedExercise(null);
      });

      this.loadExercises();
    }

    loadExercises = () => {
      this._service
        .getExercises()
        .then((exercises: IExerciseViewModel[]) => {
          this._exercises(exercises);
        });
    };

    canCreateWorkout = () => {
      if (this._workoutToCreate().errors().length > 0) {
        this._workoutToCreate().errors.showAllMessages();
        return false;
      }
      return true;
    };

    createWorkout = () => {
      if (this._workoutToCreate().errors().length > 0) {
        this._workoutToCreate().errors.showAllMessages();
        return;
      }
      let workoutToCreate = this._workoutToCreate().toPlainObject();
      debugger;
//      this._service.createWorkout(workoutToCreate);
    };


//    clearState = () => {
//      this.selectedWorkoutType(null);
//    };

//    getCreateWorkoutModel = () => {
//      return this.toJSON();
//    };


  }

  



}
