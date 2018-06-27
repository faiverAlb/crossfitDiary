module Models {
  export class ExerciseMeasureTypeViewModelObservable {
    measureType: KnockoutObservable<ExerciseMeasureType>;
    measureValue: KnockoutObservable<string>;
    measureDesciption: KnockoutObservable<string>;
    shortMeasureDescription: KnockoutObservable<string>;
    _canSeePersonalRecord: boolean;

    constructor(public model: ExerciseMeasureTypeViewModel, personMaximumWeight?:number) {
      this.measureType = ko.observable(model.measureType);
      this.measureValue = ko.observable();
      this.measureValue.extend({ required: model.isRequired });

      if (parseFloat(model.measureValue)) {
        this.measureValue(parseFloat(model.measureValue).toString());
      }
      this.measureDesciption = ko.observable(model.description);
      this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
      this._canSeePersonalRecord = personMaximumWeight != null;
    }

    public toPlainObject = (): ExerciseMeasureTypeViewModel => {
      let plainObject = new ExerciseMeasureTypeViewModel({
        measureType: this.measureType(),
        measureValue: this.measureValue(),
        description: this.measureDesciption(),
        shortMeasureDescription: this.shortMeasureDescription(),
        isRequired: this.model.isRequired
      });

      return plainObject;
    }
  }
}