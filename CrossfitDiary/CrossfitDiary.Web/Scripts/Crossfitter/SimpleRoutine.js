var SimpleRoutine = function (exerciseModel) {
    var self = this;
    self.exercise = exerciseModel;
    self.exerciseMeasures = ko.observableArray([]);
    function init() {
        for (var i = 0; i < self.exercise.exerciseMeasures.length; i++) {
            self.exerciseMeasures.push(new ExerciseMeasureTypeValue(self.exercise.exerciseMeasures[i].exerciseMeasureType));
        }
    };

    self.toJSON = function() {
        var model = {
            exerciseMeasures: [],
            id: self.exercise.id,
            title:self.exercise.title
        };
        $.each(self.exerciseMeasures(), function (index, measure) {
            model.exerciseMeasures.push(measure.toJSON());
        });
        return model;

    };

    init();
    return self;
};

