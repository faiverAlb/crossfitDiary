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
var ExerciseViewModel = /** @class */ (function () {
    function ExerciseViewModel(params) {
        this.id = 0;
        this.exerciseMeasures = [];
        this.isAlternative = false;
        this.isNewWeightMaximum = false;
        this.isDoUnbroken = false;
        if (params == null) {
            return;
        }
        this.id = params.id;
        this.title = params.title;
        this.exerciseMeasures = params.exerciseMeasures;
        this.isAlternative = params.isAlternative;
        this.isNewWeightMaximum = params.isNewWeightMaximum;
        this.isDoUnbroken = params.isDoUnbroken;
        this.addedToMaxWeightString = params.addedToMaxWeightString;
    }
    ExerciseViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return null;
        }
        return new ExerciseViewModel({
            id: input.id,
            title: input.title,
            exerciseMeasures: input.exerciseMeasures.map(function (x) {
                return new ExerciseMeasureViewModel().deserialize(x);
            }),
            isAlternative: input.isAlternative,
            isNewWeightMaximum: input.isNewWeightMaximum,
            isDoUnbroken: input.isDoUnbroken,
            addedToMaxWeightString: input.addedToMaxWeightString
        });
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