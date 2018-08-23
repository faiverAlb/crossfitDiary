import { ExerciseMeasureTypeViewModel } from "./ExerciseMeasureTypeViewModel";
var ExerciseMeasureViewModel = /** @class */ (function () {
    function ExerciseMeasureViewModel(params) {
        this.exerciseMeasureType = null;
        if (params == null) {
            return;
        }
        this.exerciseMeasureType = params.exerciseMeasureType;
    }
    ExerciseMeasureViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return (null);
        }
        return new ExerciseMeasureViewModel({
            exerciseMeasureType: new ExerciseMeasureTypeViewModel().deserialize(input.exerciseMeasureType)
        });
    };
    return ExerciseMeasureViewModel;
}());
export { ExerciseMeasureViewModel };
//# sourceMappingURL=ExerciseMeasureViewModel.js.map