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
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModel.js.map