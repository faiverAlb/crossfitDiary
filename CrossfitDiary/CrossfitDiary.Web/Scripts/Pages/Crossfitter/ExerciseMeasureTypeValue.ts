module Crossfitter {
  export class ExerciseMeasureTypeValue {
    measureType: KnockoutObservable<any>;
    measureValue: KnockoutObservable<any>;
    measureDesciption: KnockoutObservable<any>;
    shortMeasureDescription: KnockoutObservable<any>;

    constructor(model, isRequired) {
      this.measureType = ko.observable(model.measureType);
      this.measureValue = ko.observable().extend({ required: isRequired });
      if (parseFloat(model.measureValue)) {
        this.measureValue(parseFloat(model.measureValue));
      }
      this.measureDesciption = ko.observable(model.description);
      this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
    }
    public toJSON = () => ({
      exerciseMeasureType: {
        measureType: this.measureType(),
        measureValue: this.measureValue()
      }
    });
  }
}