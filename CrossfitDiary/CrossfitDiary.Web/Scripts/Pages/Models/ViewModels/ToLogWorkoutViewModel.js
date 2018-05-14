var Models;
(function (Models) {
    var ToLogWorkoutViewModel = (function () {
        function ToLogWorkoutViewModel(params) {
            if (params == null) {
                return;
            }
            this.crossfitterWorkoutId = params.crossfitterWorkoutId;
            this.date = params.date;
            this.partialRepsFinished = params.partialRepsFinished;
            this.roundsFinished = params.roundsFinished;
            this.selectedWorkoutId = params.selectedWorkoutId;
            this.timePassed = params.timePassed;
            this.isEditMode = params.isEditMode;
            this.repsToFinishOnCapTime = params.repsToFinishOnCapTime;
        }
        ToLogWorkoutViewModel.prototype.deserialize = function (jsonInput) {
            if (jsonInput == null) {
                return null;
            }
            return new ToLogWorkoutViewModel({
                crossfitterWorkoutId: jsonInput.crossfitterWorkoutId,
                date: jsonInput.date,
                isEditMode: jsonInput.isEditMode,
                partialRepsFinished: jsonInput.partialRepsFinished,
                roundsFinished: jsonInput.roundsFinished,
                selectedWorkoutId: jsonInput.selectedWorkoutId,
                timePassed: jsonInput.timePassed,
                repsToFinishOnCapTime: jsonInput.repsToFinishOnCapTime,
            });
        };
        return ToLogWorkoutViewModel;
    }());
    Models.ToLogWorkoutViewModel = ToLogWorkoutViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModel.js.map