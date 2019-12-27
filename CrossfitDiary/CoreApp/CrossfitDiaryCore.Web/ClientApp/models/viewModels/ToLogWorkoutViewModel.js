import { WorkoutViewModel } from "./WorkoutViewModel";
import { WodSubType } from "./WodSubType";
var ToLogWorkoutViewModel = /** @class */ (function () {
    function ToLogWorkoutViewModel(input) {
        this.id = 0;
        this.crossfitterWorkoutId = 0;
        this.date = new Date().toLocaleDateString();
        this.selectedWorkoutId = 0;
        this.timePassed = null;
        this.isEditMode = false;
        this.getDefaultDate = function () {
            var date = new Date();
            return ('0' + date.getDate()).slice(-2) + '.'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '.'
                + date.getFullYear();
        };
        this.displayDate = this.getDefaultDate();
        this.date = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;
        if (input == null) {
            return;
        }
        // seems that it doesn't used
        Object.assign(this, input);
    }
    ToLogWorkoutViewModel.prototype.deserialize = function (jsonInput) {
        if (jsonInput == null) {
            return null;
        }
        Object.assign(this, jsonInput);
        this.workoutViewModel = new WorkoutViewModel().deserialize(jsonInput.workoutViewModel);
        return this;
    };
    return ToLogWorkoutViewModel;
}());
export { ToLogWorkoutViewModel };
//# sourceMappingURL=ToLogWorkoutViewModel.js.map