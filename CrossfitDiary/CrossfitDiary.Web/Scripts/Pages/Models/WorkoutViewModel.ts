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
    exercisesToDoList: IExerciseViewModel[];

    constructor(workoutType: WorkoutType, exercises: IExerciseViewModel[]) {
      this.workoutType = workoutType;
      this.exercisesToDoList = exercises;
    }
  }

  export class WorkoutViewModelObservable {

    /* Сivilians */
    private _isReadOnlyMode: boolean;
    private errors:any;

    /* Observables */
    private _title: KnockoutObservable<string>;
    private _roundsCount: KnockoutObservable<number>;
    private _timeToWork: KnockoutObservable<string>;
    private _restBetweenExercises: KnockoutObservable<string>;
    private _restBetweenRounds: KnockoutObservable<string>;
    private _exercisesToBeDone: KnockoutObservableArray<ExerciseViewModelObservable>;
    private _alternativeExercisesToBeDone: KnockoutObservableArray<ExerciseViewModelObservable>;

    /* Computeds */
    private _canSeeRoundsCount: KnockoutComputed<false | boolean>;
    private _canSeeTimeToWork: KnockoutComputed<false | boolean>;
    private _canSeeAlternativeExercises: KnockoutComputed<false | boolean>;
    private _anyUsualExercises: KnockoutComputed<false | boolean>;


    constructor(public model: WorkoutViewModel, isReadOnlyMode: boolean) {
      /* Сivilians */
      this._isReadOnlyMode = isReadOnlyMode;
      this.errors = ko.validation.group(this);
      this._exercisesToBeDone = ko.observableArray(model.exercisesToDoList.map((item) => {
        return new ExerciseViewModelObservable(item);
      }));

      /* Observables */
      this._title = ko.observable(model.title);
      this._restBetweenExercises = ko.observable(model.restBetweenExercises);
      this._restBetweenRounds = ko.observable(model.restBetweenRounds);

      this._timeToWork = ko.observable();
      this._roundsCount = ko.observable();


      /* Computeds */
      this._canSeeRoundsCount = ko.computed(() => {
        return this.model.workoutType === WorkoutType.ForTime || this.model.workoutType === WorkoutType.Tabata;
      });


      this._anyUsualExercises = ko.computed(() => {
        return ko.utils.arrayFirst(this._exercisesToBeDone(), exercise => exercise.model.isAlternative === false) != null;
      });

      this._canSeeAlternativeExercises = ko.computed(() => {
        let typeIsNeeded = this.model.workoutType === WorkoutType.EMOM || this.model.workoutType === WorkoutType.E2MOM;

        return typeIsNeeded && this._anyUsualExercises();
      });

      this._canSeeTimeToWork = ko.computed(() => {
        return this.model.workoutType === WorkoutType.AMRAP ||
          this.model.workoutType === WorkoutType.EMOM ||
          this.model.workoutType === WorkoutType.E2MOM;
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

    }
  }
}