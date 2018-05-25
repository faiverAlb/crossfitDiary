var Models;
(function (Models) {
    var ToLogWorkoutViewModelObservable = (function () {
        function ToLogWorkoutViewModelObservable(workoutType, isEditMode, selectedWorkoutId, logModel) {
            var _this = this;
            this.workoutType = workoutType;
            this.isEditMode = isEditMode;
            this.selectedWorkoutId = selectedWorkoutId;
            this.logModel = logModel;
            this.toPlainObject = function () {
                var date = _this._plannedDate();
                var toLogWorkoutViewModel = new Models.ToLogWorkoutViewModel({
                    crossfitterWorkoutId: _this.logModel != null ? _this.logModel.crossfitterWorkoutId : 0,
                    date: date.toDateString(),
                    partialRepsFinished: _this._partialRepsFinished(),
                    roundsFinished: _this._totalRoundsFinished(),
                    selectedWorkoutId: _this.selectedWorkoutId,
                    timePassed: _this._totalTime(),
                    isEditMode: _this.isEditMode,
                    repsToFinishOnCapTime: _this._repsToFinishOnCapTime(),
                });
                return toLogWorkoutViewModel;
            };
            var hasModel = logModel != null;
            this._plannedDate = ko.observable(hasModel ? new Date(logModel.date) : new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()));
            this._canSeeTotalRounds = workoutType === Models.WorkoutType.AMRAP;
            this._canSeeTotalTime = workoutType === Models.WorkoutType.ForTime;
            this._totalTime = ko.observable(hasModel ? logModel.timePassed : null);
            this._repsToFinishOnCapTime = ko.observable(hasModel ? logModel.repsToFinishOnCapTime : null);
            this._totalRoundsFinished = ko.observable(hasModel ? logModel.roundsFinished : null)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTotalRounds;
                    }
                }
            });
            this._partialRepsFinished = ko.observable(hasModel ? logModel.partialRepsFinished : null);
            this._canSaveForTime = ko.computed(function () {
                var totalTimeValue = _this._totalTime(), capRepsValue = _this._repsToFinishOnCapTime();
                var shouldBeRequired = !ko.validation.rules.required.validator(totalTimeValue, true) && !ko.validation.rules.required.validator(capRepsValue, true);
                var final = _this._canSeeTotalTime && shouldBeRequired;
                return final;
            });
            this._repsToFinishOnCapTime.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSaveForTime();
                    }
                },
            });
            this._totalTime.extend({
                required: {
                    onlyIf: function () {
                        return _this._canSaveForTime();
                    }
                }
            });
            this._totalTime.subscribe(function (newValue) {
                if (newValue == null) {
                    return;
                }
                _this._repsToFinishOnCapTime(null);
            });
            this._repsToFinishOnCapTime.subscribe(function (newValue) {
                if (newValue == null) {
                    return;
                }
                _this._totalTime(null);
            });
            this.errors = ko.validation.group(this);
        }
        return ToLogWorkoutViewModelObservable;
    }());
    Models.ToLogWorkoutViewModelObservable = ToLogWorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModelObservable.js.map