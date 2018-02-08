module Models {
  export class ExerciseMeasureTypeViewModel {
    measureType: KnockoutObservable<ExerciseMeasureType>;
    measureValue: KnockoutObservable<string>;
    measureDesciption: KnockoutObservable<string>;
    shortMeasureDescription: KnockoutObservable<string>;

    constructor(model, isRequired:boolean) {
      this.measureType = ko.observable(model.measureType);
      this.measureValue = ko.observable();
      this.measureValue.extend({ required: isRequired });

      if (parseFloat(model.measureValue)) {
        this.measureValue(parseFloat(model.measureValue).toString());
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