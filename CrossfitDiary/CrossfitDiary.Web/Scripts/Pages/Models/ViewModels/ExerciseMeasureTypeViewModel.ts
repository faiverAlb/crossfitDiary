module Models {
  import Serializable = General.Serializable;

  interface IExerciseMeasureTypeViewModel {
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
        measureType: <ExerciseMeasureType>input.measureType,
        measureValue: input.measureValue,
        description: input.description,
        isRequired: input.isRequired,
        shortMeasureDescription: input.shortMeasureDescription,
      });
    }

  };
}