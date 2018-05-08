module Models {
  declare var ko;
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

    constructor(public workoutType: WorkoutType, public isEditMode: boolean, public selectedWorkoutId?: number, public logModel?: ToLogWorkoutViewModel) {

      let hasModel:boolean = logModel != null;
      
      this._plannedDate = ko.observable(hasModel ? new Date(logModel.date) : new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()));
      this._canSeeTotalRounds = workoutType === WorkoutType.AMRAP;
      this._canSeeTotalTime = workoutType === WorkoutType.ForTime;

    

      this._totalTime = ko.observable(hasModel ? logModel.timePassed : null)
        .extend({
          required: {
            onlyIf: () => {
              return this._canSeeTotalTime;
            }
          }
        });

      this._totalRoundsFinished = ko.observable(hasModel? logModel.roundsFinished: null)
        .extend({
          required: {
            onlyIf: () => {
              return this._canSeeTotalRounds;
            }
          }
        });

      this._partialRepsFinished = ko.observable(hasModel ? logModel.partialRepsFinished : null);

      this.errors = ko.validation.group(this);
    }


    public toPlainObject = (): ToLogWorkoutViewModel => {
      let date = this._plannedDate() as any;
      let toLogWorkoutViewModel = new ToLogWorkoutViewModel(
        this.logModel != null ? this.logModel.crossfitterWorkoutId: 0,
        date.toDateString(),
        this._partialRepsFinished(),
        this._totalRoundsFinished(),
        this.selectedWorkoutId,
        this._totalTime(),
        this.isEditMode
      );
      return toLogWorkoutViewModel;
    }
  }
}