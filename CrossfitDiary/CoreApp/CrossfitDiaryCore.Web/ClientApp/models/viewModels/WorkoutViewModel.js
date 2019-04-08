import { WorkoutType } from "./WorkoutType";
import { ExerciseViewModel } from "./ExerciseViewModel";
export var PlanningWorkoutLevel;
(function (PlanningWorkoutLevel) {
    PlanningWorkoutLevel[PlanningWorkoutLevel["Scaled"] = 0] = "Scaled";
    PlanningWorkoutLevel[PlanningWorkoutLevel["Rx"] = 1] = "Rx";
    PlanningWorkoutLevel[PlanningWorkoutLevel["RxPlus"] = 2] = "RxPlus";
})(PlanningWorkoutLevel || (PlanningWorkoutLevel = {}));
var WorkoutViewModel = /** @class */ (function () {
    function WorkoutViewModel(params) {
        var _this = this;
        this.id = 0;
        this.title = "";
        this.workoutType = WorkoutType.ForTime;
        this.exercisesToDoList = [];
        this.children = [];
        this.isInnerWorkout = false;
        this.getDefaultDate = function () {
            var date = new Date();
            var result = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
            return result;
        };
        this.IsForTime = function () {
            return _this.workoutType == WorkoutType.ForTime;
        };
        this.IsAMRAP = function () {
            return _this.workoutType == WorkoutType.AMRAP;
        };
        this.IsEmoms = function () {
            return _this.workoutType == WorkoutType.E2MOM || _this.workoutType == WorkoutType.EMOM;
        };
        this.IsHaveCapTime = function () {
            return _this.workoutType == WorkoutType.ForTime || _this.workoutType == WorkoutType.ForTimeManyInners;
        };
        this.displayPlanDate = this.getDefaultDate();
        if (params == null) {
            return;
        }
        this.id = params.id;
        this.title = params.title;
        this.roundsCount = params.roundsCount;
        this.timeToWork = params.timeToWork;
        this.timeCap = params.timeCap;
        this.restBetweenExercises = params.restBetweenExercises;
        this.restBetweenRounds = params.restBetweenRounds;
        this.workoutType = params.workoutType;
        this.exercisesToDoList = params.exercisesToDoList;
        this.workoutTypeDisplay = params.workoutTypeDisplay;
        this.children = params.children;
        this.isInnerWorkout = params.isInnerWorkout;
        this.planningWorkoutLevel = params.planningWorkoutLevel;
        this.displayPlanDate = params.displayPlanDate;
        this.comment = params.comment;
        this.resultsCount = params.resultsCount;
        this.canSeeLeaderboard = params.canSeeLeaderboard;
    }
    WorkoutViewModel.prototype.deserialize = function (jsonInput) {
        if (jsonInput == null) {
            return null;
        }
        return new WorkoutViewModel({
            id: jsonInput.id,
            title: jsonInput.title,
            roundsCount: jsonInput.roundsCount,
            timeToWork: jsonInput.timeToWork,
            timeCap: jsonInput.timeCap,
            restBetweenExercises: jsonInput.restBetweenExercises,
            restBetweenRounds: jsonInput.restBetweenRounds,
            workoutType: jsonInput.workoutType,
            workoutTypeDisplay: jsonInput.workoutTypeDisplay,
            children: jsonInput.children.map(function (x) { return new WorkoutViewModel().deserialize(x); }),
            exercisesToDoList: jsonInput.exercisesToDoList.map(function (x) { return new ExerciseViewModel().deserialize(x); }),
            isInnerWorkout: false,
            planningWorkoutLevel: jsonInput.planningWorkoutLevel,
            displayPlanDate: jsonInput.displayPlanDate,
            comment: jsonInput.comment,
            resultsCount: jsonInput.resultsCount,
            canSeeLeaderboard: jsonInput.canSeeLeaderboard
        });
    };
    return WorkoutViewModel;
}());
export { WorkoutViewModel };
//# sourceMappingURL=WorkoutViewModel.js.map