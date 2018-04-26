module Models {
  declare var ko;

  export class ToLogWorkoutViewModel {
    canBeRemovedByCurrentUser: boolean;
    crossfitterWorkoutId: number;
    date: string;
    displayDate: string;
    distance?: number;
    isRx: boolean;
    measureDisplayName: string;
    partialRepsFinished?: number;
    roundsFinished?: number;
    selectedWorkoutId: number;
    selectedWorkoutName: string;
    timePassed: string;
    wasFinished: boolean;
    workouterName: string;

    constructor(
      date: string,
      partialRepsFinished: number,
      roundsFinished: number,
      selectedWorkoutId: number,
      timePassed: string,
      ) {
      this.date = date;
      this.partialRepsFinished = partialRepsFinished;
      this.roundsFinished = roundsFinished;
      this.selectedWorkoutId = selectedWorkoutId;
      this.timePassed = timePassed;
    }
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

    constructor(public workoutType: WorkoutType, public selectedWorkoutId?: number) {
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


    public toPlainObject = (): ToLogWorkoutViewModel => {
      let date = this._plannedDate() as any;
      let toLogWorkoutViewModel = new ToLogWorkoutViewModel(
        date.toISOString(),
        this._partialRepsFinished(),
        this._totalRoundsFinished(),
        this.selectedWorkoutId,
        this._totalTime()
      );
      return toLogWorkoutViewModel;
    }
  }
}