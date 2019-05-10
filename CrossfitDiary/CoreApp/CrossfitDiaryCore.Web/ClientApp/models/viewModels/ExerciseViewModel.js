var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
import { ExerciseMeasureViewModel } from "./ExerciseMeasureViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";
var ExerciseViewModel = /** @class */ (function () {
    function ExerciseViewModel(input) {
        var _this = this;
        this.id = 0;
        this.exerciseMeasures = [];
        this.isAlternative = false;
        this.isNewWeightMaximum = false;
        this.isDoUnbroken = false;
        this.count = null;
        this.weight = null;
        this.calories = null;
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
var DefaultExerciseViewModel = /** @class */ (function (_super) {
    __extends(DefaultExerciseViewModel, _super);
    function DefaultExerciseViewModel() {
        return _super.call(this, {
            id: -1,
            title: "Select exercise",
            exerciseMeasures: [],
            isAlternative: false,
            isDoUnbroken: false
        }) || this;
    }
    return DefaultExerciseViewModel;
}(ExerciseViewModel));
export { DefaultExerciseViewModel };
//# sourceMappingURL=ExerciseViewModel.js.map