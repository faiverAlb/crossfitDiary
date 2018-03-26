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
    public workoutToCreate: KnockoutObservable<WorkoutViewModelObservable>;
    private _workoutTypes: KnockoutObservable<Array<BaseKeyValuePairModel<number, string>>>;
    public selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;
    private _exercises: KnockoutObservableArray<IExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<IExerciseViewModel>;

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
      this.workoutToCreate = ko.observable(null);
      this._exercises = ko.observableArray([]);

      this._selectedExercise = ko.observable(null);

      ko.computed(() => {
        this._selectedExercise(null);
          if (!this.selectedWorkoutType()) {
          return;
        }
        if (this._exercises().length === 0) {
          return;
        }

        let model = new WorkoutViewModel(this.selectedWorkoutType().id, []);
        this.workoutToCreate(new WorkoutViewModelObservable(model, false));
      });

      ko.computed(() => {
        let exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this.workoutToCreate().addExerciseToList(exercise);
        this._selectedExercise(null);
      });

      this.loadExercises();
    }

    private  loadExercises = () => {
      this.service
        .getExercises()
        .then((exercises: IExerciseViewModel[]) => {
          this._exercises(exercises);
        });
    };


    private createWorkout = () => {
      if (this.workoutToCreate().errors().length > 0) {
        this.workoutToCreate().errors.showAllMessages();
        return;
      }
      let workoutToCreate = this.workoutToCreate().toPlainObject();
      this.service.createWorkout(workoutToCreate)
        .then(() => {
          window.location.href = "/Home";
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });
    };


    public clearState = () => {
      this.selectedWorkoutType(null);
    };
  }
}
