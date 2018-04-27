var Models;
(function (Models) {
    var ToLogWorkoutViewModel = (function () {
        function ToLogWorkoutViewModel(crossfitterWorkoutId, date, partialRepsFinished, roundsFinished, selectedWorkoutId, timePassed, isEdtiMode) {
            this.crossfitterWorkoutId = crossfitterWorkoutId;
            this.date = date;
            this.partialRepsFinished = partialRepsFinished;
            this.roundsFinished = roundsFinished;
            this.selectedWorkoutId = selectedWorkoutId;
            this.timePassed = timePassed;
            this.isEditMode = isEdtiMode;
        }
        return ToLogWorkoutViewModel;
    }());
    Models.ToLogWorkoutViewModel = ToLogWorkoutViewModel;
    var ToLogWorkoutViewModelObservable = (function () {
        /* Computeds */
        function ToLogWorkoutViewModelObservable(workoutType, isEditMode, selectedWorkoutId, logModel) {
            var _this = this;
            this.workoutType = workoutType;
            this.isEditMode = isEditMode;
            this.selectedWorkoutId = selectedWorkoutId;
            this.logModel = logModel;
            this.toPlainObject = function () {
                var date = _this._plannedDate();
                var toLogWorkoutViewModel = new ToLogWorkoutViewModel(_this.logModel != null ? _this.logModel.crossfitterWorkoutId : 0, date.toDateString(), _this._partialRepsFinished(), _this._totalRoundsFinished(), _this.selectedWorkoutId, _this._totalTime(), _this.isEditMode);
                return toLogWorkoutViewModel;
            };
            var hasModel = logModel != null;
            this._plannedDate = ko.observable(hasModel ? new Date(logModel.date) : new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()));
            this._canSeeTotalRounds = workoutType === Models.WorkoutType.AMRAP;
            this._canSeeTotalTime = workoutType === Models.WorkoutType.ForTime;
            this._totalTime = ko.observable(hasModel ? logModel.timePassed : null)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTotalTime;
                    }
                }
            });
            this._totalRoundsFinished = ko.observable(hasModel ? logModel.roundsFinished : null)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTotalRounds;
                    }
                }
            });
            this._partialRepsFinished = ko.observable(hasModel ? logModel.partialRepsFinished : null);
            this.errors = ko.validation.group(this);
        }
        return ToLogWorkoutViewModelObservable;
    }());
    Models.ToLogWorkoutViewModelObservable = ToLogWorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModel.js.map