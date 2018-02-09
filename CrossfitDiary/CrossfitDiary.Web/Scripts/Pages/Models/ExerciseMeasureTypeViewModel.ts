module Models {
  export class IExerciseMeasureTypeViewModel {
    measureType: ExerciseMeasureType;
    measureValue: string;
    description: string;
    shortMeasureDescription: string;
    isRequired: boolean;
  }
  export class ExerciseMeasureTypeViewModelObservable {
    measureType: KnockoutObservable<ExerciseMeasureType>;
    measureValue: KnockoutObservable<string>;
    measureDesciption: KnockoutObservable<string>;
    shortMeasureDescription: KnockoutObservable<string>;

    constructor(model: IExerciseMeasureTypeViewModel) {
      this.measureType = ko.observable(model.measureType);
      this.measureValue = ko.observable();
      this.measureValue.extend({ required: model.isRequired });

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