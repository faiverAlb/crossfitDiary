var SimpleRoutine = function (exerciseModel) {
    var self = this;
    self.exercise = exerciseModel;
    self.exerciseValue = ko.observable();
    self.exerciseMeasures = ko.observableArray([]);
    function init() {
        for (var i = 0; i < self.exercise.exerciseMeasures.length; i++) {
            self.exerciseMeasures.push(new ExerciseMeasureTypeValue(self.exercise.exerciseMeasures[i].exerciseMeasureType));
        }
    };

    self.toJSON = function() {
        debugger;
    };

    init();
    return self;
};

var ExerciseMeasureTypeValue = function(model) {
    var self = this;

    self.measureType = ko.observable(model.measureType);
    self.measureValue = ko.observable();
    self.measureDesciption = ko.observable(model.description);

/*
    ko.computed = function () {
        switch (self.measureType()) {
            case window.Crossfitter.ExerciseMeasureTypes.Distance:
            break;
            case window.Crossfitter.ExerciseMeasureTypes.Count:
            break;
        case window.Crossfitter.ExerciseMeasureTypes.Time:
            break;
        case window.Crossfitter.ExerciseMeasureTypes.Weight:
            break;
        }
    };
*/


    return self;
};