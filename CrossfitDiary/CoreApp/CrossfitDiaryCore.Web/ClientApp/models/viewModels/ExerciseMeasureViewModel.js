// import { ExerciseMeasureTypeViewModel } from "./ExerciseMeasureTypeViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";
var ExerciseMeasureViewModel = /** @class */ (function () {
    function ExerciseMeasureViewModel(params) {
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
        if (params == null) {
            return;
        }
        this.measureType = params.measureType;
        this.measureValue = params.measureValue;
        this.description = params.description;
        this.shortMeasureDescription = params.shortMeasureDescription;
        this.isRequired = params.isRequired;
    }
    ExerciseMeasureViewModel.prototype.deserialize = function (jsonInput) {
        if (jsonInput == null) {
            return null;
        }
        return new ExerciseMeasureViewModel({
            measureType: jsonInput.measureType,
            measureValue: jsonInput.measureValue,
            description: jsonInput.description,
            isRequired: jsonInput.isRequired,
            shortMeasureDescription: jsonInput.shortMeasureDescription
        });
    };
    return ExerciseMeasureViewModel;
}());
export { ExerciseMeasureViewModel };
//# sourceMappingURL=ExerciseMeasureViewModel.js.map