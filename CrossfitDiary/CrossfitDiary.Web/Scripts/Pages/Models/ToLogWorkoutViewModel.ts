module Models {
  declare var ko;

  export class ToLogWorkoutViewModel {
    canBeRemovedByCurrentUser: boolean;
    crossfitterWorkoutId: number;
    date: Date;
    displayDate: string;
    distance?: number;
    isRx: boolean;
    measureDisplayName: string;
    partialRepsFinished?: number;
    roundsFinished?: number;
    selectedWorkoutId: string;
    selectedWorkoutName: string;
    timePassed: string;
    wasFinished: boolean;
    workouterName: string;
  }

  export class ToLogWorkoutViewModelObservable {

    /* Сivilians */
    _canSeeTotalRounds: boolean;
    _canSeeTotalTime: boolean;
    public errors:any;

    /* Observables */
    _plannedDate: KnockoutObservable<Date>;
    _totalTime: KnockoutObservable<string>;
    _totalRoundsFinished: KnockoutObservable<number>;
    _partialRepsFinished?: KnockoutObservable<number>;

    /* Computeds */

    constructor(public workoutType: WorkoutType) {
      this._plannedDate = ko.observable(new Date());
      this._canSeeTotalRounds = workoutType === WorkoutType.AMRAP;
      this._canSeeTotalTime = workoutType === WorkoutType.ForTime;

    

      this._totalTime = ko.observable(null)
        .extend({
          required: {
            onlyIf: () => {
              return this._canSeeTotalTime;
            }
          }
        });

      this._totalRoundsFinished = ko.observable(null)
        .extend({
          required: {
            onlyIf: () => {
              return this._canSeeTotalRounds;
            }
          }
        });

      this._partialRepsFinished = ko.observable();

      this.errors = ko.validation.group(this);
    }


    public toPlainObject = () => {

    }
  }
}