var Models;
(function (Models) {
    var WorkoutViewModel = (function () {
        function WorkoutViewModel(params) {
            if (params == null) {
                return;
            }
            this.id = params.id;
            this.title = params.title;
            this.detailedTitle = params.detailedTitle;
            this.roundsCount = params.roundsCount;
            this.timeToWork = params.timeToWork;
            this.restBetweenExercises = params.restBetweenExercises;
            this.restBetweenRounds = params.restBetweenRounds;
            this.workoutType = params.workoutType;
            this.exercisesToDoList = params.exercisesToDoList;
        }
        WorkoutViewModel.prototype.deserialize = function (jsonInput) {
            if (jsonInput == null) {
                return null;
            }
            return new WorkoutViewModel({
                id: jsonInput.id,
                title: jsonInput.title,
                detailedTitle: jsonInput.detailedTitle,
                roundsCount: jsonInput.roundsCount,
                timeToWork: jsonInput.timeToWork,
                restBetweenExercises: jsonInput.restBetweenExercises,
                restBetweenRounds: jsonInput.restBetweenRounds,
                workoutType: jsonInput.workoutType,
                exercisesToDoList: jsonInput.exercisesToDoList.map(function (x) { return new Models.ExerciseViewModel().deserialize(x); })
            });
        };
        return WorkoutViewModel;
    }());
    Models.WorkoutViewModel = WorkoutViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=WorkoutViewModel.js.map