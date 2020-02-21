import { WorkoutViewModel } from "./WorkoutViewModel";
import { WodSubType } from "./WodSubType";
var PlanningWorkoutViewModel = /** @class */ (function () {
    function PlanningWorkoutViewModel(input) {
        this.id = 0;
        this.subTypeClass = "";
        this.isPrivatePlanning = false;
        this.getDefaultDate = function () {
            var date = new Date();
            var result = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
            return result;
        };
        this.displayPlanDate = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }
    PlanningWorkoutViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return;
        }
        Object.assign(this, input);
        this.workoutViewModel = new WorkoutViewModel().deserialize(input.workoutViewModel);
        return this;
    };
    return PlanningWorkoutViewModel;
}());
export { PlanningWorkoutViewModel };
//# sourceMappingURL=PlanningWorkoutViewModel.js.map