//module Models {
//  declare var ko;
//  export class ToLogWorkoutViewModelObservable {
//
//    /* Сivilians */
//    _canSeeTotalRounds: boolean;
//    _canSeeTotalTime: boolean;
//    public errors:any;
//
//    /* Observables */
//    _plannedDate: KnockoutObservable<Date>;
//    _totalTime: KnockoutObservable<string>;
//    _repsToFinishOnCapTime?: KnockoutObservable<number>;
//    _totalRoundsFinished: KnockoutObservable<number>;
//    _partialRepsFinished?: KnockoutObservable<number>;
//
//    /* Computeds */
//    _canSaveForTime: KnockoutComputed<boolean>;
//
//    constructor(public workoutType: WorkoutType, public isEditMode: boolean, public selectedWorkoutId?: number, public logModel?: ToLogWorkoutViewModel) {
//
//      let hasModel:boolean = logModel != null;
//      
//      this._plannedDate = ko.observable(hasModel ? new Date(logModel.date) : new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()));
//      this._canSeeTotalRounds = workoutType === WorkoutType.AMRAP;
//      this._canSeeTotalTime = workoutType === WorkoutType.ForTime || workoutType === WorkoutType.ForTimeManyInners;
//
//
//      this._totalTime = ko.observable(hasModel ? logModel.timePassed : null);
//
//      this._repsToFinishOnCapTime = ko.observable(hasModel ? logModel.repsToFinishOnCapTime : null);
//
//      this._totalRoundsFinished = ko.observable(hasModel ? logModel.roundsFinished : null)
//        .extend({
//          required: {
//            onlyIf: () => {
//              return this._canSeeTotalRounds;
//            }
//          }
//        });
//
//      this._partialRepsFinished = ko.observable(hasModel ? logModel.partialRepsFinished : null);
//
//
//      this._canSaveForTime = ko.computed(() => {
//        let totalTimeValue = this._totalTime(), capRepsValue = this._repsToFinishOnCapTime();
//        let shouldBeRequired = !ko.validation.rules.required.validator(totalTimeValue, true) &&  !ko.validation.rules.required.validator(capRepsValue, true);
//        let final = this._canSeeTotalTime && shouldBeRequired;
//        return final;
//      });
//
//      this._repsToFinishOnCapTime.extend({
//        required: {
//          onlyIf: () => {
//            return this._canSaveForTime();
//          }
//        },
//      });
//      this._totalTime.extend({
//        required: {
//          onlyIf: () => {
//            return this._canSaveForTime();
//          }
//        }
//      });
//
//      this._totalTime.subscribe((newValue) => {
//        if (newValue == null) {
//          return;
//        }
//        this._repsToFinishOnCapTime(null);
//      });
//
//      this._repsToFinishOnCapTime.subscribe((newValue) => {
//        if (newValue == null) {
//          return;
//        }
//        this._totalTime(null);
//      });
//
//      this.errors = ko.validation.group(this);
//    }
//
//
//    public toPlainObject = (): ToLogWorkoutViewModel => {
//      let date = this._plannedDate() as any;
//      let toLogWorkoutViewModel = new ToLogWorkoutViewModel(
//        {
//          crossfitterWorkoutId: this.logModel != null ? this.logModel.crossfitterWorkoutId : 0,
//          date: date.toDateString(),
//          partialRepsFinished: this._partialRepsFinished(),
//          roundsFinished: this._totalRoundsFinished(),
//          selectedWorkoutId: this.selectedWorkoutId,
//          timePassed: this._totalTime(),
//          isEditMode: this.isEditMode,
//          repsToFinishOnCapTime: this._repsToFinishOnCapTime(),
//        }
//      );
//      return toLogWorkoutViewModel;
//    }
//  }
//}