import { ExerciseMeasureType } from "./ExerciseMeasureType";
var ExerciseMeasureViewModel = /** @class */ (function () {
    function ExerciseMeasureViewModel(input) {
        this.measureType = ExerciseMeasureType.Weight;
        this.measureValue = "";
        this.description = "";
        this.shortMeasureDescription = "";
        this.isRequired = false;
        this.setDisplayByMeasureType = function (measure) {
            switch (measure) {
                case ExerciseMeasureType.Distance:
                    return "m";
                case ExerciseMeasureType.Count:
                    return "";
                case ExerciseMeasureType.Weight:
                    return "kgs";
                case ExerciseMeasureType.Calories:
                    return "calories";
                case ExerciseMeasureType.Height:
                    return "cm";
                default:
                    return "";
            }
        };
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }
    ExerciseMeasureViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return;
        }
        Object.assign(this, input);
        return this;
    };
    return ExerciseMeasureViewModel;
}());
export { ExerciseMeasureViewModel };
//# sourceMappingURL=ExerciseMeasureViewModel.js.map