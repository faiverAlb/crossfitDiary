module Models {
  declare var ko;
  export class WorkoutViewModelObservable {
    
    /* Сivilians */
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


    constructor(public model: WorkoutViewModel) {
      /* Сivilians */
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

    public addExerciseToList = (exerciseViewModel: ExerciseViewModel) => {
      this._exercisesToBeDone.push(new ExerciseViewModelObservable(exerciseViewModel));
    }

    public removeSimpleRoutineFromToDo = (index:KnockoutObservable<number>) => {
      this._exercisesToBeDone.splice(index(), 1);
    }

    public addSimpleRoutineFromToDo = (exerciseViewModel: ExerciseViewModelObservable) => {
      this._exercisesToBeDone.push(new ExerciseViewModelObservable(exerciseViewModel.model));
    }

    public moveSimpleRoutineUp = (index: number) => {
      if (index > 0) {
        let rowList = this._exercisesToBeDone();
        this._exercisesToBeDone.splice(index - 1, 2, rowList[index], rowList[index - 1]);
      }
    }
    public moveSimpleRoutineDown = (index: number) => {
      let rowList = this._exercisesToBeDone();
      if (index < rowList.length - 1) {
        this._exercisesToBeDone.splice(index, 2, rowList[index + 1], rowList[index]);

      }
    }

    public getId():number {
      return this._id();
    }


    public toPlainObject = (): WorkoutViewModel => {
      let workoutToCreate = new WorkoutViewModel({
        id: this.model.id,
        title: this._title(),
        detailedTitle: this.model.detailedTitle,
        roundsCount: this._roundsCount(),
        timeToWork: this._timeToWork(),
        restBetweenExercises: this._restBetweenExercises(),
        restBetweenRounds: this._restBetweenRounds(),
        workoutType: this.model.workoutType,
        exercisesToDoList: this._exercisesToBeDone().map(item => item.toPlainObject())
      });

      return workoutToCreate;
    }
  }
}