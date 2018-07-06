var Models;
(function (Models) {
    ;
    var ExerciseViewModel = (function () {
        function ExerciseViewModel(params) {
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
                exerciseMeasures: input.exerciseMeasures.map(function (x) { return new Models.ExerciseMeasureViewModel().deserialize(x); }),
                isAlternative: input.isAlternative,
                isNewWeightMaximum: input.isNewWeightMaximum,
                isDoUnbroken: input.isDoUnbroken,
                addedToMaxWeightString: input.addedToMaxWeightString,
            });
        };
        ;
        return ExerciseViewModel;
    }());
    Models.ExerciseViewModel = ExerciseViewModel;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseViewModel.js.map