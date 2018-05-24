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
            this.canBeRemovedByCurrentUser = params.canBeRemovedByCurrentUser;
            this.workouterName = params.workouterName;
            this.workouterId = params.workouterId;
            this.displayDate = params.displayDate;
            this.workoutViewModel = params.workoutViewModel;
            this.hasNewMaximum = params.hasNewMaximum;
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
                canBeRemovedByCurrentUser: jsonInput.canBeRemovedByCurrentUser,
                workouterName: jsonInput.workouterName,
                workouterId: jsonInput.workouterId,
                displayDate: jsonInput.displayDate,
                workoutViewModel: new Models.WorkoutViewModel().deserialize(jsonInput.workoutViewModel),
                hasNewMaximum: jsonInput.hasNewMaximum,
            });
        };
        return ToLogWorkoutViewModel;
    }());
    Models.ToLogWorkoutViewModel = ToLogWorkoutViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=ToLogWorkoutViewModel.js.map