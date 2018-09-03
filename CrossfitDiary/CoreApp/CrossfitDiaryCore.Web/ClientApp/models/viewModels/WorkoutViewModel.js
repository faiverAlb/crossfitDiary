import { WorkoutType } from './WorkoutType';
import { ExerciseViewModel } from './ExerciseViewModel';
var WorkoutViewModel = /** @class */ (function () {
    function WorkoutViewModel(params) {
        var _this = this;
        this.id = 0;
        this.title = '';
        this.workoutType = WorkoutType.ForTime;
        this.exercisesToDoList = [];
        this.children = [];
        this.isInnerWorkout = false;
        this.IsForTime = function () {
            return _this.workoutType == WorkoutType.ForTime;
        };
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
    }
    WorkoutViewModel.prototype.deserialize = function (jsonInput) {
        if (jsonInput == null) {
            return (null);
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
            isInnerWorkout: false
        });
    };
    return WorkoutViewModel;
}());
export { WorkoutViewModel };
//# sourceMappingURL=WorkoutViewModel.js.map