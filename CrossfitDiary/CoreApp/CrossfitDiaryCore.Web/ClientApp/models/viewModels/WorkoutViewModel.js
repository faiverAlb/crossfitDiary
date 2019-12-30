import { WorkoutType } from "./WorkoutType";
import { ExerciseViewModel } from "./ExerciseViewModel";
import { WodSubType } from "./WodSubType";
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
        this.oneTimeSchema = {};
        this.subTypeClass = "";
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
        this.getHaveCollapsedVersion = function (exercisedToUse, distinctFirst) {
            var canBeCollapsed = true;
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
            return canBeCollapsed;
        };
        this.getDistinctItems = function (exercisedToUse) {
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
            return distinctFirst;
        };
        this.groupExercises = function (exercisedToUse) {
            var groupedDictionary = {};
            for (var i = 0; i < exercisedToUse.length; i++) {
                var exercise = exercisedToUse[i];
                if (groupedDictionary[exercise.id]) {
                    groupedDictionary[exercise.id].push(exercise);
                }
                else {
                    groupedDictionary[exercise.id] = [];
                    groupedDictionary[exercise.id].push(exercise);
                }
            }
            return groupedDictionary;
        };
        this.getCanShowCountOnce = function (exercisedToUse, distinctLength) {
            var canShowCountOnce = true;
            for (var i = 0; i < exercisedToUse.length; i++) {
                if (canShowCountOnce === false) {
                    break;
                }
                for (var j = 0; j < distinctLength; j++) {
                    if (i + j + 1 >= exercisedToUse.length || i + j + 1 >= distinctLength) {
                        break;
                    }
                    var currentExericseToUse = exercisedToUse[i + j];
                    var nextExericseToUse = exercisedToUse[i + j + 1];
                    if (currentExericseToUse.haveSameCountAndCalories(nextExericseToUse) === false) {
                        canShowCountOnce = false;
                        break;
                    }
                }
                i = i + distinctLength;
            }
            return canShowCountOnce;
        };
        this.displayPlanDate = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }
    Object.defineProperty(WorkoutViewModel.prototype, "workoutSubTypeDisplayValue", {
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
    WorkoutViewModel.prototype.getSubTypeClass = function () {
        switch (this.wodSubType) {
            case WodSubType.Skill:
                return 'bg-info text-white';
            case WodSubType.Wod:
                return 'bg-danger text-white';
            case WodSubType.AccessoryWork:
                return 'bg-warning text-white';
        }
    };
    Object.defineProperty(WorkoutViewModel.prototype, "planningLevelDisplayValue", {
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
    WorkoutViewModel.prototype.tryCollapseWorkout = function () {
        var exercisedToUse = this.exercisesToDoList;
        var distinctExercises = this.getDistinctItems(exercisedToUse);
        if (distinctExercises.length === 0) {
            return;
        }
        if (exercisedToUse.length === distinctExercises.length) {
            return;
        }
        if (exercisedToUse.length % distinctExercises.length !== 0) {
            return;
        }
        this.haveCollapsedVersion = this.getHaveCollapsedVersion(exercisedToUse, distinctExercises);
        if (this.haveCollapsedVersion === false) {
            return;
        }
        this.groupedDictionary = this.groupExercises(exercisedToUse);
        var distinctLength = distinctExercises.length;
        this.canShowCountOnce = this.getCanShowCountOnce(exercisedToUse, distinctLength);
        if (this.canShowCountOnce) {
            this.oneTimeSchema.schemaString = "";
            var firstExerciseArray = this.groupedDictionary[exercisedToUse[0].id];
            for (var index = 0; index < firstExerciseArray.length; index++) {
                var element = firstExerciseArray[index];
                if (element.count) {
                    this.oneTimeSchema.schemaString += element.count;
                }
                if (element.calories) {
                    this.oneTimeSchema.schemaString += element.calories;
                }
                if (index + 1 < firstExerciseArray.length) {
                    this.oneTimeSchema.schemaString += "-";
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