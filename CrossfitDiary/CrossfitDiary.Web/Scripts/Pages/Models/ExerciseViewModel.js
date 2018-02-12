var Models;
(function (Models) {
    var ExerciseMeasureViewModelObservable = (function () {
        function ExerciseMeasureViewModelObservable(model) {
            this._exerciseMeasureType = new Models.ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
        }
        return ExerciseMeasureViewModelObservable;
    }());
    var ExerciseViewModelObservable = (function () {
        function ExerciseViewModelObservable(model) {
            this.model = model;
            this._exerciseMeasures = model.exerciseMeasures.map(function (item) { return new ExerciseMeasureViewModelObservable(item); });
        }
        return ExerciseViewModelObservable;
    }());
    Models.ExerciseViewModelObservable = ExerciseViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseViewModel.js.map