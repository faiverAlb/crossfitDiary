import { WorkoutViewModel } from "./WorkoutViewModel";
export var PlanningWorkoutLevel;
(function (PlanningWorkoutLevel) {
    PlanningWorkoutLevel[PlanningWorkoutLevel["Scaled"] = 0] = "Scaled";
    PlanningWorkoutLevel[PlanningWorkoutLevel["Rx"] = 1] = "Rx";
    PlanningWorkoutLevel[PlanningWorkoutLevel["RxPlus"] = 2] = "RxPlus";
})(PlanningWorkoutLevel || (PlanningWorkoutLevel = {}));
var ToLogWorkoutViewModel = /** @class */ (function () {
    function ToLogWorkoutViewModel(params) {
        this.id = 0;
        this.crossfitterWorkoutId = 0;
        this.date = new Date().toLocaleDateString();
        this.selectedWorkoutId = 0;
        this.timePassed = null;
        this.isEditMode = false;
        this.isPlanned = false;
        this.displayDate = new Date().toLocaleDateString(); // default value for new model
        if (params == null) {
            return;
        }
        this.id = params.id;
        this.crossfitterWorkoutId = params.crossfitterWorkoutId;
        this.date = params.date;
        this.partialRepsFinished = params.partialRepsFinished;
        this.roundsFinished = params.roundsFinished;
        this.selectedWorkoutId = params.selectedWorkoutId;
        this.timePassed = params.timePassed;
        this.isEditMode = params.isEditMode;
        this.isPlanned = params.isPlanned;
        this.repsToFinishOnCapTime = params.repsToFinishOnCapTime;
        this.canBeRemovedByCurrentUser = params.canBeRemovedByCurrentUser;
        this.workouterName = params.workouterName;
        this.workouterId = params.workouterId;
        this.displayDate = params.displayDate;
        this.workoutViewModel = params.workoutViewModel;
        this.planningWorkoutLevel = params.planningWorkoutLevel;
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
            isPlanned: jsonInput.isPlanned,
            planningWorkoutLevel: jsonInput.planningWorkoutLevel
        });
    };
    return ToLogWorkoutViewModel;
}());
export { ToLogWorkoutViewModel };
//# sourceMappingURL=ToLogWorkoutViewModel.js.map