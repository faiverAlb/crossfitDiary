var Models;
(function (Models) {
    var ExerciseMeasureViewModel = (function () {
        function ExerciseMeasureViewModel(exerciseMeasureType) {
            this.exerciseMeasureType = exerciseMeasureType;
        }
        return ExerciseMeasureViewModel;
    }());
    Models.ExerciseMeasureViewModel = ExerciseMeasureViewModel;
    var ExerciseMeasureViewModelObservable = (function () {
        function ExerciseMeasureViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var plainObject = new ExerciseMeasureViewModel(_this._exerciseMeasureType.toPlainObject());
                return plainObject;
            };
            this._exerciseMeasureType = new Models.ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
        }
        return ExerciseMeasureViewModelObservable;
    }());
    var ExerciseViewModel = (function () {
        function ExerciseViewModel(id, title, exerciseMeasures, isAlternative) {
            this.id = id;
            this.title = title;
            this.exerciseMeasures = exerciseMeasures;
            this.isAlternative = isAlternative;
        }
        return ExerciseViewModel;
    }());
    Models.ExerciseViewModel = ExerciseViewModel;
    var ExerciseViewModelObservable = (function () {
        function ExerciseViewModelObservable(model) {
            var _this = this;
            this.model = model;
            this.toPlainObject = function () {
                var planExercise = new ExerciseViewModel(_this.model.id, _this.model.title, _this._exerciseMeasures.map(function (item) { return item.toPlainObject(); }), _this.model.isAlternative);
                return planExercise;
            };
            this._exerciseMeasures = model.exerciseMeasures.map(function (item) { return new ExerciseMeasureViewModelObservable(item); });
        }
        return ExerciseViewModelObservable;
    }());
    Models.ExerciseViewModelObservable = ExerciseViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseViewModel.js.map