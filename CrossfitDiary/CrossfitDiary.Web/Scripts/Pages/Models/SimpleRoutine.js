var Models;
(function (Models) {
    var SimpleRoutine = (function () {
        function SimpleRoutine(exerciseModel, isFieldsRequired) {
            if (isFieldsRequired === void 0) { isFieldsRequired = null; }
            var _this = this;
            this.toJSON = function () {
                var model = {
                    exerciseMeasures: [],
                    id: _this.exercise.id,
                    title: _this.exercise.title,
                    isAlternative: _this.exercise.isAlternative
                };
                $.each(_this.exerciseMeasures(), function (index, measure) {
                    model.exerciseMeasures.push(measure.toJSON());
                });
                return model;
            };
            this.exercise = exerciseModel;
            this.exerciseMeasures = ko.observableArray([]);
            var isMeasuresRequired = isFieldsRequired != null ? isFieldsRequired : true;
            for (var i = 0; i < this.exercise.exerciseMeasures.length; i++) {
                var exerciseMeasureType = this.exercise.exerciseMeasures[i].exerciseMeasureType;
                if (exerciseMeasureType.measureType === Models.ExerciseMeasureType.Weight) {
                    isMeasuresRequired = false;
                }
                var exerciseMeasureTypeValue = new Models.ExerciseMeasureTypeValue(exerciseMeasureType, isMeasuresRequired);
                this.exerciseMeasures.push(exerciseMeasureTypeValue);
            }
        }
        return SimpleRoutine;
    }());
    Models.SimpleRoutine = SimpleRoutine;
})(Models || (Models = {}));
//# sourceMappingURL=SimpleRoutine.js.map