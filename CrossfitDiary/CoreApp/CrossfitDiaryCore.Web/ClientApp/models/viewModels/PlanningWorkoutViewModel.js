import { WorkoutViewModel } from "./WorkoutViewModel";
import { WodSubType } from "./WodSubType";
var PlanningWorkoutViewModel = /** @class */ (function () {
    function PlanningWorkoutViewModel(input) {
        this.id = 0;
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
        debugger;
        this.workoutViewModel = new WorkoutViewModel().deserialize(input.workoutViewModel);
        return this;
    };
    return PlanningWorkoutViewModel;
}());
export { PlanningWorkoutViewModel };
//# sourceMappingURL=PlanningWorkoutViewModel.js.map