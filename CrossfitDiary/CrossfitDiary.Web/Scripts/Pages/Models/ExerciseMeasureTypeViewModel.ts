module Models {
  import Serializable = General.Serializable;

  export interface IExerciseMeasureTypeViewModel {
    measureType: ExerciseMeasureType;
    measureValue: string;
    description: string;
    shortMeasureDescription: string;
    isRequired: boolean;
  }

  export class ExerciseMeasureTypeViewModel implements Serializable<ExerciseMeasureTypeViewModel>{

    measureType: ExerciseMeasureType;
    measureValue: string;
    description: string;
    shortMeasureDescription: string;
    isRequired: boolean;

    constructor(params?: IExerciseMeasureTypeViewModel) {
      if (params == null) {
        return;
      }
      this.measureType = params.measureType;
      this.measureValue = params.measureValue;
      this.description = params.description;
      this.shortMeasureDescription = params.shortMeasureDescription;
      this.isRequired = params.isRequired;
    }

    deserialize(input): ExerciseMeasureTypeViewModel {
      if (input == null) {
        return null;
      }

      return new ExerciseMeasureTypeViewModel({
        measureType: input.measureType,
        measureValue: input.measureValue,
        description: input.description,
        isRequired: input.isRequired,
        shortMeasureDescription: input.shortMeasureDescription,
      });
    }

  };


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