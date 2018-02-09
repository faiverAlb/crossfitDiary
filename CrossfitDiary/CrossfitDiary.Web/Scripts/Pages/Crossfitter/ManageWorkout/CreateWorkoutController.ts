module Pages {
  import BasicParameters = General.BasicParameters;
  import CrossfitterService = General.CrossfitterService;
  import IExerciseViewModel = Models.IExerciseViewModel;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutType = Models.WorkoutType;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import WorkoutViewModel = Models.WorkoutViewModel;

  export class CreateWorkoutController {
    private errors;
    private _service: CrossfitterService;

    private _workoutToCreate: KnockoutObservable<WorkoutViewModelObservable>;

    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    private _selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;

    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;

    private _alternativeExercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedAlternativeExercise: KnockoutObservable<IExerciseViewModel>;



    constructor(parameters: BasicParameters, service: CrossfitterService) {
      this._service = service;

      this._workoutTypes = ko.observable(
        new Array(
          new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime])
          , new BaseKeyValuePairModel(WorkoutType.AMRAP, WorkoutType[WorkoutType.AMRAP])
          , new BaseKeyValuePairModel(WorkoutType.EMOM, WorkoutType[WorkoutType.EMOM])
          , new BaseKeyValuePairModel(WorkoutType.E2MOM, WorkoutType[WorkoutType.E2MOM])
          , new BaseKeyValuePairModel(WorkoutType.NotForTime, WorkoutType[WorkoutType.NotForTime])
          , new BaseKeyValuePairModel(WorkoutType.Tabata, WorkoutType[WorkoutType.Tabata])
        ));
      this._selectedWorkoutType = ko.observable(null);
      this._workoutToCreate = ko.observable(null);
      this._exercises = ko.observableArray([]);
      this._alternativeExercises = ko.observableArray([]);

      this._selectedExercise = ko.observable();
      this._selectedAlternativeExercise = ko.observable();

      ko.computed(() => {
        if (!this._selectedWorkoutType()) {
          return;
        }
        if (this._exercises().length === 0) {
          return;
        }

        let model = new WorkoutViewModel(this._selectedWorkoutType().id, this._exercises());
        this._workoutToCreate(new WorkoutViewModelObservable(model, false));
      });
      this.loadExercises();
    }

    loadExercises = () => {
      this._service
        .getExercises()
        .then((exercises: IExerciseViewModel[]) => {
          this._exercises(exercises);
          //  HACK for copy
          let alternativeExercises = JSON.parse(JSON.stringify(exercises));
          for (let i = 0; i < alternativeExercises.length; i++) {
            alternativeExercises[i].isAlternative = true;
          }
          this._alternativeExercises(alternativeExercises);
        });
    };

    canCreateWorkout = () => {
      if (this.errors().length > 0) {
        this.errors.showAllMessages();
        return false;
      }
      return true;
    };

    createWorkout = () => {
//      if (this.errors().length > 0) {
//        this.errors.showAllMessages();
//        return;
//      }
//      this._service.createWorkout(this.toJSON());
    };


//    clearState = () => {
//      this.selectedWorkoutType(null);
//    };

//    getCreateWorkoutModel = () => {
//      return this.toJSON();
//    };


  }

  



}
