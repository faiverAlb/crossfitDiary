import { WorkoutType } from "./WorkoutType";
import { ExerciseViewModel } from "./ExerciseViewModel";
export var PlanningWorkoutLevel;
(function (PlanningWorkoutLevel) {
    PlanningWorkoutLevel[PlanningWorkoutLevel["Scaled"] = 0] = "Scaled";
    PlanningWorkoutLevel[PlanningWorkoutLevel["Rx"] = 1] = "Rx";
    PlanningWorkoutLevel[PlanningWorkoutLevel["RxPlus"] = 2] = "RxPlus";
})(PlanningWorkoutLevel || (PlanningWorkoutLevel = {}));
var WorkoutViewModel = /** @class */ (function () {
    function WorkoutViewModel(input) {
        var _this = this;
        this.id = 0;
        this.title = "";
        this.workoutType = WorkoutType.ForTime;
        this.exercisesToDoList = [];
        this.children = [];
        this.isInnerWorkout = false;
        this.haveCollapsedVersion = false;
        this.canShowCountOnce = true;
        this.groupedDictionary = {};
        this.getDefaultDate = function () {
            var date = new Date();
            var result = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
            return result;
        };
        this.IsForTime = function () {
            return _this.workoutType === WorkoutType.ForTime;
        };
        this.IsAMRAP = function () {
            return _this.workoutType === WorkoutType.AMRAP || _this.workoutType === WorkoutType.AMRAPN;
        };
        this.IsEmoms = function () {
            return _this.workoutType === WorkoutType.E2MOM || _this.workoutType === WorkoutType.EMOM;
        };
        this.IsHaveCapTime = function () {
            return _this.workoutType === WorkoutType.ForTime || _this.workoutType === WorkoutType.ForTimeManyInners;
        };
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }
    WorkoutViewModel.prototype.tryCollapseWorkout = function () {
        var exercisedToUse = this.exercisesToDoList;
        var distinctFirst = [];
        var _loop_1 = function (index) {
            var element = exercisedToUse[index];
            var foundInDistinct = distinctFirst.find(function (x) {
                return x.id === element.id;
            });
            if (!foundInDistinct) {
                distinctFirst.push(element);
            }
            else {
                return "break";
            }
        };
        for (var index = 0; index < exercisedToUse.length; index++) {
            var state_1 = _loop_1(index);
            if (state_1 === "break")
                break;
        }
        var canBeCollapsed = true;
        if (distinctFirst.length === 0) {
            return;
        }
        if (exercisedToUse.length === distinctFirst.length) {
            // trivial case
            return;
        }
        if (exercisedToUse.length % distinctFirst.length !== 0) {
            return;
        }
        for (var i = 0; i < exercisedToUse.length; i++) {
            if (canBeCollapsed === false) {
                break;
            }
            for (var j = 0; j < distinctFirst.length; j++) {
                var distinctItem = distinctFirst[j];
                if (i + j < exercisedToUse.length && exercisedToUse[i + j].id !== distinctItem.id) {
                    canBeCollapsed = false;
                    break;
                }
            }
            i = i + distinctFirst.length - 1;
        }
        this.haveCollapsedVersion = canBeCollapsed;
        if (this.haveCollapsedVersion === false) {
            return;
        }
        for (var i = 0; i < exercisedToUse.length; i++) {
            var exercise = exercisedToUse[i];
            if (this.groupedDictionary[exercise.id]) {
                this.groupedDictionary[exercise.id].push(exercise);
            }
            else {
                this.groupedDictionary[exercise.id] = [];
                this.groupedDictionary[exercise.id].push(exercise);
            }
        }
        debugger;
        var generalExercisesLength = this.groupedDictionary[exercisedToUse[0].id].length;
        for (var index = 0; index < generalExercisesLength; index++) {
            if (this.canShowCountOnce == false) {
                break;
            }
            for (var key in this.groupedDictionary) {
                var groupArray = this.groupedDictionary[key];
                if (index + 1 >= generalExercisesLength) {
                    break;
                }
                if (groupArray[index].count != groupArray[index + 1].count) {
                    this.canShowCountOnce = false;
                    break;
                }
            }
        }
    };
    WorkoutViewModel.prototype.deserialize = function (input) {
        if (input == null) {
            return;
        }
        Object.assign(this, input);
        this.children = input.children.map(function (x) { return new WorkoutViewModel().deserialize(x); });
        this.exercisesToDoList = input.exercisesToDoList.map(function (x) { return new ExerciseViewModel().deserialize(x); });
        this.tryCollapseWorkout();
        return this;
    };
    return WorkoutViewModel;
}());
export { WorkoutViewModel };
//# sourceMappingURL=WorkoutViewModel.js.map