var ExerciseMeasureTypeValue = function (model) {
    var self = this;

    self.measureType = ko.observable(model.measureType);
    self.measureValue = ko.observable();
    self.measureDesciption = ko.observable(model.description);

    self.toJSON = function () {
        return {
            exerciseMeasureType: {
                measureType: self.measureType(),
                measureValue: self.measureValue()
            }
        };
    };

    return self;
};