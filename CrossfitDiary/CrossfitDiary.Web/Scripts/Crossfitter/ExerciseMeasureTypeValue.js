var ExerciseMeasureTypeValue = function (model, isRequired) {
    var self = this;

    self.measureType = ko.observable(model.measureType);
    self.measureValue = ko.observable(model.measureValue).extend({ required: isRequired });
    self.measureDesciption = ko.observable(model.description);
    self.shortMeasureDescription = ko.observable(model.shortMeasureDescription);

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