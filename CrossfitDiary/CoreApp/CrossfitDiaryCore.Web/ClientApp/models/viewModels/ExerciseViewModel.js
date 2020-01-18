import { ExerciseMeasureViewModel } from "./ExerciseMeasureViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";
import { WeightDisplayType } from "./WeightDisplayType";
var ExerciseViewModel = /** @class */ (function () {
    function ExerciseViewModel(input) {
        var _this = this;
        this.id = 0;
        this.exerciseMeasures = [];
        this.isAlternative = false;
        this.isNewWeightMaximum = false;
        this.isDoUnbroken = false;
        this.weightDisplayType = WeightDisplayType.Default;
        this.count = null;
        this.weight = null;
        this.calories = null;
        this.weightPercentValue = null;
        this.haveSameCountAndCalories = function (toCompareExercise) {
            if (_this.count == null && toCompareExercise.count == null) {
                if (_this.calories == null && toCompareExercise.calories == null) {
                    return false;
                }
                else {
                    return _this.calories === toCompareExercise.calories;
                }
            }
            else {
                if (_this.calories == null && toCompareExercise.calories == null) {
                    return _this.count === toCompareExercise.count;
                }
                else {
                    var result = _this.count === toCompareExercise.count ||
                        _this.count === toCompareExercise.calories ||
                        _this.calories === toCompareExercise.count ||
                        _this.calories === toCompareExercise.calories;
                    return result;
                }
            }
        };
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }
    ExerciseViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return;
        }
        Object.assign(this, input);
        this.exerciseMeasures = input.exerciseMeasures.map(function (x) { return new ExerciseMeasureViewModel().deserialize(x); });
        this.count = this.getMeasureValue(ExerciseMeasureType.Count);
        this.weight = this.getMeasureValue(ExerciseMeasureType.Weight);
        this.calories = this.getMeasureValue(ExerciseMeasureType.Calories);
        return this;
    };
    ExerciseViewModel.prototype.getMeasureValue = function (measureType) {
        var foundCountMeasure = this.exerciseMeasures.find(function (x) { return x.measureType === measureType; });
        var result = foundCountMeasure ? foundCountMeasure.measureValue : null;
        return result;
    };
    return ExerciseViewModel;
}());
export { ExerciseViewModel };
//# sourceMappingURL=ExerciseViewModel.js.map