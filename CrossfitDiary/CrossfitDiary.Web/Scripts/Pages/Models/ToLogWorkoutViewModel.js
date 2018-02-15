var Models;
(function (Models) {
    var ToLogWorkoutViewModel = (function () {
        function ToLogWorkoutViewModel() {
        }
        return ToLogWorkoutViewModel;
    }());
    Models.ToLogWorkoutViewModel = ToLogWorkoutViewModel;
    var ToLogWorkoutViewModelObservable = (function () {
        /* Computeds */
        function ToLogWorkoutViewModelObservable(workoutType) {
            var _this = this;
            this.workoutType = workoutType;
            this.toPlainObject = function () {
            };
            this._plannedDate = ko.observable(new Date());
            this._canSeeTotalRounds = workoutType === Models.WorkoutType.AMRAP;
            this._canSeeTotalTime = workoutType === Models.WorkoutType.ForTime;
            this._totalTime = ko.observable(null)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTotalTime;
                    }
                }
            });
            this._totalRoundsFinished = ko.observable(null)
                .extend({
                required: {
                    onlyIf: function () {
                        return _this._canSeeTotalRounds;
                    }
                }
            });
            this._partialRepsFinished = ko.observable();
            this.errors = ko.validation.group(this);
        }
        return ToLogWorkoutViewModelObservable;
    }());
    Models.ToLogWorkoutViewModelObservable = ToLogWorkoutViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModel.js.map