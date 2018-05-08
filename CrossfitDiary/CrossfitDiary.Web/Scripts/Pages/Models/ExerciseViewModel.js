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
            });
        };
        ;
        return ExerciseViewModel;
    }());
    Models.ExerciseViewModel = ExerciseViewModel;
    var ExerciseViewModelObservable = (function () {
        function ExerciseViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainExercise = new ExerciseViewModel({
                    id: _this.model.id,
                    title: _this.model.title,
                    exerciseMeasures: _this._exerciseMeasures.map(function (item) { return item.toPlainObject(); }),
                    isAlternative: _this.model.isAlternative
                });
                return plainExercise;
            };
            this._exerciseMeasures = model.exerciseMeasures.map(function (item) { return new Models.ExerciseMeasureViewModelObservable(item); });
        }
        return ExerciseViewModelObservable;
    }());
    Models.ExerciseViewModelObservable = ExerciseViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseViewModel.js.map