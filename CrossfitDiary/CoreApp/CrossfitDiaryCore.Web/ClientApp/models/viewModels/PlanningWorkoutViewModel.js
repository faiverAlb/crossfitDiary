import { PlanningWorkoutLevel, WorkoutViewModel } from "./WorkoutViewModel";
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
    Object.defineProperty(PlanningWorkoutViewModel.prototype, "workoutSubTypeDisplayValue", {
        get: function () {
            this.subTypeClass = this.getSubTypeClass();
            switch (this.wodSubType) {
                case WodSubType.Skill:
                    return "Skill";
                case WodSubType.Wod:
                    return "WOD";
                case WodSubType.AccessoryWork:
                    return "Accessory";
            }
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(PlanningWorkoutViewModel.prototype, "planningLevelDisplayValue", {
        get: function () {
            switch (this.planningWorkoutLevel) {
                case PlanningWorkoutLevel.Scaled:
                    return "Scaled";
                case PlanningWorkoutLevel.Rx:
                    return "Rx";
                case PlanningWorkoutLevel.RxPlus:
                    return "Rx+";
            }
        },
        enumerable: true,
        configurable: true
    });
    PlanningWorkoutViewModel.prototype.getSubTypeClass = function () {
        switch (this.wodSubType) {
            case WodSubType.Skill:
                return 'bg-info text-white';
            case WodSubType.Wod:
                return 'bg-danger text-white';
            case WodSubType.AccessoryWork:
                return 'bg-warning text-white';
        }
    };
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