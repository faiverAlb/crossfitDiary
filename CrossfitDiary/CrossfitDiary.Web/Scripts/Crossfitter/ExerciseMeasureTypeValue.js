var ExerciseMeasureTypeValue = function (model, isRequired) {
    var self = this;

    self.measureType = ko.observable(model.measureType);
    self.measureValue = ko.observable().extend({ required: isRequired });
    if (parseFloat(model.measureValue)) {
        self.measureValue(parseFloat(model.measureValue));
    }
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