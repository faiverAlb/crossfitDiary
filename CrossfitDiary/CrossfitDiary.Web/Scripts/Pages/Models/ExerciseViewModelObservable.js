var Models;
(function (Models) {
    var ExerciseViewModelObservable = (function () {
        function ExerciseViewModelObservable(model, personMaximums) {
            var _this = this;
            this.model = model;
            this.getPersonMaximum = function (exerciseId, personMaximums) {
                for (var i = 0; i < personMaximums.length; i++) {
                    if (personMaximums[i].exerciseId === exerciseId) {
                        return personMaximums[i].maximumWeightValue;
                    }
                }
                return null;
            };
            this.toPlainObject = function () {
                var plainExercise = new Models.ExerciseViewModel({
                    id: _this.model.id,
                    title: _this.model.title,
                    exerciseMeasures: _this._exerciseMeasures.map(function (item) { return item.toPlainObject(); }),
                    isAlternative: _this.model.isAlternative
                });
                return plainExercise;
            };
            var personMaximumWeight = this.getPersonMaximum(model.id, personMaximums);
            this._exerciseMeasures = model.exerciseMeasures.map(function (item) { return new Models.ExerciseMeasureViewModelObservable(item, personMaximumWeight); });
        }
        return ExerciseViewModelObservable;
    }());
    Models.ExerciseViewModelObservable = ExerciseViewModelObservable;
})(Models || (Models = {}));
//# sourceMappingURL=ExerciseViewModelObservable.js.map