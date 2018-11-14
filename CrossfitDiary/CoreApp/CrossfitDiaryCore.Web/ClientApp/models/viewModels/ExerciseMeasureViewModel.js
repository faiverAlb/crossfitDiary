import { ExerciseMeasureTypeViewModel } from "./ExerciseMeasureTypeViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";
var ExerciseMeasureViewModel = /** @class */ (function () {
    function ExerciseMeasureViewModel(params) {
        this.exerciseMeasureType = null;
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
        if (params == null) {
            return;
        }
        this.exerciseMeasureType = params.exerciseMeasureType;
        this.displayMeasure = this.setDisplayByMeasureType(this.exerciseMeasureType.measureType);
    }
    ExerciseMeasureViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return null;
        }
        return new ExerciseMeasureViewModel({
            exerciseMeasureType: new ExerciseMeasureTypeViewModel().deserialize(input.exerciseMeasureType)
        });
    };
    return ExerciseMeasureViewModel;
}());
export { ExerciseMeasureViewModel };
//# sourceMappingURL=ExerciseMeasureViewModel.js.map