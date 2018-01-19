var SimpleRoutine = function (exerciseModel, isFieldsRequired) {
    var self = this;
    self.exercise = exerciseModel;
    self.exerciseMeasures = ko.observableArray([]);
    var isMeasuresRequired = isFieldsRequired != null ? isFieldsRequired : true;
    function init() {
        for (var i = 0; i < self.exercise.exerciseMeasures.length; i++) {
            var exerciseMeasureType = self.exercise.exerciseMeasures[i].exerciseMeasureType;
            if (exerciseMeasureType.measureType == Crossfitter.ExerciseMeasureTypes.Weight) {
                isMeasuresRequired = false;
            }

            var exerciseMeasureTypeValue = new ExerciseMeasureTypeValue(exerciseMeasureType, isMeasuresRequired);

            self.exerciseMeasures.push(exerciseMeasureTypeValue);
        }
    };

    self.toJSON = function() {
        var model = {
            exerciseMeasures: [],
            id: self.exercise.id,
            title: self.exercise.title,
            isAlternative: self.exercise.isAlternative
        };
        $.each(self.exerciseMeasures(), function (index, measure) {
            model.exerciseMeasures.push(measure.toJSON());
        });
        return model;

    };

    init();
    return self;
};

