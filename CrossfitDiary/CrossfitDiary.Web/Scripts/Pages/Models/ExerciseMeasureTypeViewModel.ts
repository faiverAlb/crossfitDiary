module Models {
  export class ExerciseMeasureTypeViewModel {
    measureType: ExerciseMeasureType;
    measureValue: string;
    description: string;
    shortMeasureDescription: string;
    isRequired: boolean;

    constructor(measureType: ExerciseMeasureType, measureValue: string, description: string, shortMeasureDescription: string, isRequired: boolean) {
      this.measureType = measureType;
      this.measureValue = measureValue;
      this.description = description;
      this.shortMeasureDescription = shortMeasureDescription;
      this.isRequired = isRequired;
    }
  }
  export class ExerciseMeasureTypeViewModelObservable {
    measureType: KnockoutObservable<ExerciseMeasureType>;
    measureValue: KnockoutObservable<string>;
    measureDesciption: KnockoutObservable<string>;
    shortMeasureDescription: KnockoutObservable<string>;

    constructor(public model: ExerciseMeasureTypeViewModel) {
      this.measureType = ko.observable(model.measureType);
      this.measureValue = ko.observable();
      this.measureValue.extend({ required: model.isRequired });

      if (parseFloat(model.measureValue)) {
        this.measureValue(parseFloat(model.measureValue).toString());
      }
      this.measureDesciption = ko.observable(model.description);
      this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
    }

    public toPlainObject = (): ExerciseMeasureTypeViewModel => {
      let plainObject = new ExerciseMeasureTypeViewModel(this.measureType(),
        this.measureValue(),
        this.measureDesciption(),
        this.shortMeasureDescription(),
        this.model.isRequired);

      return plainObject;
    }
  }
}