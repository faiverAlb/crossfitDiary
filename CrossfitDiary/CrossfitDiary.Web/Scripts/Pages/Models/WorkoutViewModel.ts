module Models {
  declare var ko;

  export class WorkoutViewModel {
    id: number;
    title?: string;
    roundsCount?: number;
    timeToWork?: any;
    restBetweenExercises?: any;
    restBetweenRounds?: any;
    workoutType: WorkoutType;
    exercisesToDoList: ExerciseViewModel[];

    constructor(workoutType: WorkoutType, exercises: ExerciseViewModel[]) {
      this.workoutType = workoutType;
      this.exercisesToDoList = exercises;
    }
  }

  export class WorkoutViewModelObservable {
    
    /* Сivilians */
    private _isReadOnlyMode: boolean;
    public errors:any;

    /* Observables */
    private _id: KnockoutObservable<number>;
    private _title: KnockoutObservable<string>;
    private _roundsCount: KnockoutObservable<number>;
    private _timeToWork: KnockoutObservable<string>;
    private _restBetweenExercises: KnockoutObservable<string>;
    private _restBetweenRounds: KnockoutObservable<string>;
    private _exercisesToBeDone: KnockoutObservableArray<ExerciseViewModelObservable>;

    /* Computeds */
    private _canSeeRoundsCount: KnockoutComputed<false | boolean>;
    private _canSeeTimeToWork: KnockoutComputed<false | boolean>;
    private _anyUsualExercises: KnockoutComputed<false | boolean>;
    private _canSeeGeneralWorkoutInfo: KnockoutComputed<false | boolean>;

    private _workoutTypeTitle: string;


    constructor(public model: WorkoutViewModel, isReadOnlyMode: boolean) {
      /* Сivilians */
      this._isReadOnlyMode = isReadOnlyMode;
      this._workoutTypeTitle = WorkoutType[model.workoutType];
      this._exercisesToBeDone = ko.observableArray(model.exercisesToDoList.map((item) => {
        return new ExerciseViewModelObservable(item);
      }));

      /* Observables */
      this._id = ko.observable(model.id);
      this._title = ko.observable(model.title);
      this._restBetweenExercises = ko.observable(model.restBetweenExercises);
      this._restBetweenRounds = ko.observable(model.restBetweenRounds);

      this._timeToWork = ko.observable(model.timeToWork);
      this._roundsCount = ko.observable(model.roundsCount);


      /* Computeds */
      this._canSeeRoundsCount = ko.computed(() => {
        return this.model.workoutType === WorkoutType.ForTime;
      });


      this._anyUsualExercises = ko.computed(() => {
        return ko.utils.arrayFirst(this._exercisesToBeDone(), exercise => exercise.model.isAlternative === false) != null;
      });

      this._canSeeTimeToWork = ko.computed(() => {
        return this.model.workoutType === WorkoutType.AMRAP || this.model.workoutType === WorkoutType.EMOM;
      });

      this._canSeeGeneralWorkoutInfo = ko.computed(() => {
        return this._canSeeTimeToWork() || this._canSeeRoundsCount();
      });

      this._timeToWork.extend({
        required: {
          onlyIf: () => {
            return this._canSeeTimeToWork();
          }
        }
      });
      this._roundsCount.extend({
        required: {
          onlyIf: () => {
            return this._canSeeRoundsCount();
          }
        }
      });

      this._exercisesToBeDone.extend({
        minLength: {
          message: "At least one exercise is required"
        }
      });
      this.errors = ko.validation.group(this);
    }

    public addExerciseToList(exerciseViewModel: ExerciseViewModel) {
      this._exercisesToBeDone.push(new ExerciseViewModelObservable(exerciseViewModel));
    }

    public removeSimpleRoutineFromToDo(index:KnockoutObservable<number>) {
      this._exercisesToBeDone.splice(index(), 1);
    }

    public getId():number {
      return this._id();
    }


    public toPlainObject = (): WorkoutViewModel => {
      let workoutToCreate = new WorkoutViewModel(this.model.workoutType, this._exercisesToBeDone().map(item => item.toPlainObject()));
      workoutToCreate.title = this._title();
      workoutToCreate.roundsCount = this._roundsCount();
      workoutToCreate.timeToWork = this._timeToWork();
      workoutToCreate.restBetweenExercises = this._restBetweenExercises();
      workoutToCreate.restBetweenRounds = this._restBetweenRounds();

      return workoutToCreate;
    }
  }
}