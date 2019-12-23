import { WorkoutViewModel } from "./WorkoutViewModel";
import { WodSubType } from "./WodSubType";
var ToLogWorkoutViewModel = /** @class */ (function () {
    function ToLogWorkoutViewModel(params) {
        this.id = 0;
        this.crossfitterWorkoutId = 0;
        this.date = new Date().toLocaleDateString();
        this.selectedWorkoutId = 0;
        this.timePassed = null;
        this.isEditMode = false;
        this.getDefaultDate = function () {
            var date = new Date();
            var result = ('0' + date.getDate()).slice(-2) + '.'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '.'
                + date.getFullYear();
            return result;
        };
        this.displayDate = this.getDefaultDate();
        this.date = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;
        if (params == null) {
            return;
        }
        // seems that it doesn't used
        this.id = params.id;
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
        this.comment = params.comment;
        this.wodSubType = params.wodSubType;
    }
    ToLogWorkoutViewModel.prototype.deserialize = function (jsonInput) {
        if (jsonInput == null) {
            return null;
        }
        return new ToLogWorkoutViewModel({
            id: jsonInput.id,
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
            workoutViewModel: new WorkoutViewModel().deserialize(jsonInput.workoutViewModel),
            comment: jsonInput.comment,
            wodSubType: jsonInput.wodSubType
        });
    };
    return ToLogWorkoutViewModel;
}());
export { ToLogWorkoutViewModel };
//# sourceMappingURL=ToLogWorkoutViewModel.js.map