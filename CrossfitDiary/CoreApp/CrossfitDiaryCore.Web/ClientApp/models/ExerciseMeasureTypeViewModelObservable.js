//module Models {
//  export class ExerciseMeasureTypeViewModelObservable {
//    _canSeePersonalRecord: boolean;
//
//    measureType: KnockoutObservable<ExerciseMeasureType>;
//    measureValue: KnockoutObservable<string>;
//    measureDesciption: KnockoutObservable<string>;
//    shortMeasureDescription: KnockoutObservable<string>;
//
//    personalRecordPercent: KnockoutComputed<string>;
//
//    constructor(public model: ExerciseMeasureTypeViewModel, personMaximumWeight?:number) {
//      this.measureType = ko.observable(model.measureType);
//      this.measureValue = ko.observable();
//      this.measureValue.extend({ required: model.isRequired });
//
//      if (parseFloat(model.measureValue)) {
//        this.measureValue(parseFloat(model.measureValue).toString());
//      }
//      this.measureDesciption = ko.observable(model.description);
//      this.shortMeasureDescription = ko.observable(model.shortMeasureDescription);
//      this._canSeePersonalRecord = personMaximumWeight != null && personMaximumWeight > 0;
//
//      if (this._canSeePersonalRecord) {
//        this.personalRecordPercent = ko.computed(() => {
//          let inputValueString = this.measureValue();
//          if (parseFloat(inputValueString)) {
//            let actualValue = parseFloat(inputValueString);
//            let calc = (actualValue / personMaximumWeight) * 100;
//            let calcString = Math.round((calc + 0.0001) * 10) / 10;
//            return `${calcString}% of Personal Record`;
//          }
//        });
//      }
//    }
//
//
//
//    public toPlainObject = (): ExerciseMeasureTypeViewModel => {
//      let plainObject = new ExerciseMeasureTypeViewModel({
//        measureType: this.measureType(),
//        measureValue: this.measureValue(),
//        description: this.measureDesciption(),
//        shortMeasureDescription: this.shortMeasureDescription(),
//        isRequired: this.model.isRequired
//      });
//
//      return plainObject;
//    }
//  }
//}
//# sourceMappingURL=ExerciseMeasureTypeViewModelObservable.js.map